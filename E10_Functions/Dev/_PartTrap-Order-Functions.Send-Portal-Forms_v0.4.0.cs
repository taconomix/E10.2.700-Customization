/*== Call Automated Email Functions for Web Orders ===========================

	Library:  PartTrap-Order-Functions
	Function: Send-Portal-Forms
	
	Assemblies:
		Ice.Contracts.BO.DynamicQuery.dll

	Tables:

	Services:
		ICE:BO:DynamicQuery

	Libraries:
		Send-Portal-Forms
	
	Created: 02/27/2023
	Changed: 

		Kevin Veldman <KVeldman@Hanger.com>
============================================================================*/

	/*------------------------------------------------------------------------
		Call BAQ and get list from results:
			https://www.epiusers.help/t/epicor-functions-and-baqs/83803/21

		logging to event log reference: 
			https://www.epiusers.help/t/bpm-logging-snippet/93004

		logging to trace file: 
			https://www.epiusers.help/t/how-to-write-your-own-trace-logs/44613/3 
	-------------------------------------------------------------------------*/

	//__ Write to TaskLog for Tracing ____________________________________
		Action<string> addLog = s => {
			foreach ( var task in Db.SysTask.Where(t => t.Company==Session.CompanyID && t.TaskDescription.ToLower() == "run epicor function" && t.TaskStatus.ToLower() == "active")) {
				var taskLog = Db.SysTaskLog.FirstOrDefault(t => t.SysTaskNum == task.SysTaskNum && t.MsgTest.ToLower().Contains(this.LibraryID.ToLower()));
				if ( taskLog != null ) this.CallService<Ice.Contracts.SysMonitorTasksSvcContract>(sm=>{sm.WriteToTaskLog(s, taskLog.SysTaskNum, Epicor.ServiceModel.Utilities.MsgType.Info);});
			}
		};
	//__ Write to Trace Log ______________________________________________
		Action<string> addTrace = s => { 
			Ice.Diagnostics.Log.WriteEntry(s);
		};
	//____________________________________________________________________


string QueryName = "WebFormsQueue";

addTrace("Send-Portal-Forms: Starting");

var context = Ice.Services.ContextFactory.CreateContext<ErpContext>();

using ( var svc = Ice.Assemblies.ServiceRenderer.GetService<Ice.Contracts.DynamicQuerySvcContract>(context) ) {

	var dsQuery = svc.GetByID (QueryName);  // BAQ Name for Orders

	if ( dsQuery == null ) {

		addTrace("Send-Portal-Forms - unable to get query " + QueryName);
		addLog("Unable to get query " + QueryName);
	
	} else {

		var p = svc.GetQueryExecutionParameters (dsQuery);
		var results = svc.Execute (dsQuery, p); 


		if ( results.Tables["Results"].Rows.Count > 0 ) { //This is where the magic happens
			
			var rowCount = results.Tables["Results"].Rows.Count;

			string[rowCount] = new String {}
			var SS = Db.Company.Where(x => x.Name.Contains("SURESTEP")).FirstOrDefault();

			var ConnectionString = (string)SS[""]; // "Server=EPAPP02;Database=DienenProdSP;Integrated Security=True;";
			var ssrsPrefix = (string)SS["ssrsPrefix_c"] + "Sales Management/"; // "/E10Prod/Sales Management/";
			var company = "SS";
			var ssrsServer = (string)SS["ssrsServer_c"]; // "EPAPP02";
			var svcAddress = string.Format("http://{0}/reportserver/ReportExecution2005.asmx", ssrsServer);
			var savePath = @"C:\EpicorData\Companies\SS\Processes\kevinv\";

			Func<string,string> getRptPath = s => string.Format("{0}{1}", ssrsPrefix, s);

			var mailer = this.GetMailer(async:true);
			var message = new Ice.Mail.SmtpMail();
			Dictionary<string, string> emailAttachments = new Dictionary <string,string>();

			foreach ( DataRow r in results.Tables["Results"].Rows ) {

				var bkgFile = string.Empty;
				var rptName = string.Empty;

				var orderNum = (int)r["OrderDtl_OrderNum"];
				var orderLine = (int)r["OrderDtl_OrderLine"];
				var lastLine = (int)r["Calculated_MaxOL"];
				var sendTo = "kevinv@dienen.com";
				//var sendTo = (string)r["OrderHed_PTUserEmail_c"];
				var basePN = (string)r["OrderDtl_BasePartNum"];

				/* Generate O-Form Here */
				emailAttachments.Add(attachmentName, fileName); // Add attachment
				
				if ( orderLine == lastLine ) {

					/* Generate SO Acknowledgment Here */

					message.SetFrom("noreply@surestep.net");
					message.SetTo(sendTo);
					message.SetSubject("Surestep Order #" + orderNum.ToString());

					System.Text.StringBuilder body = new System.Text.StringBuilder();
					body.Append("Dear Customer,").AppendLine().AppendLine();
					body.Append(String.Format("Your order #{0} has been received. Attached you will find the Order Confirmation and all Orthometry Forms. Please review, and let us know as quickly as possible if you find any incorrect information. Note that remake orders and any order with free-form Order Notes will be reviewed prior to fabrication, so this O-Form may slightly change before fabrication begins. If this order requires a PO and you have not yet provided one, please send the PO Number and Amount to orders@surestep.net as soon as possible. Please reference this order number, as PO Numbers submitted without an order number may cause significant delays. Thank you.",sOrderNum)).AppendLine();
					body.AppendLine().Append("Sincerely,").AppendLine().AppendLine();
					body.Append("Surestep Customer Service").AppendLine().Append("P 877.462.0711|F 866.700.7837");
					body.AppendLine().Append("orders@surestep.net");

					message.SetBody(body.ToString());

					mailer.Send(message, emailAttachments);

					foreach ( var item in emailAttachments.Values ) {
						System.IO.File.Delete(item);
					}

					emailAttachments.Clear();
					
				}
			}
		} else {

			addLog(String.Format("Query {0} returned 0 Rows",QueryName));
		}
	}
}