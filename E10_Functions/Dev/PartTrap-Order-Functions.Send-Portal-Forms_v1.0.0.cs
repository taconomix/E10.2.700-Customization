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
				var taskLog = Db.SysTaskLog.FirstOrDefault(t => t.SysTaskNum == task.SysTaskNum && t.MsgText.ToLower().Contains(this.LibraryID.ToLower()));
				if ( taskLog != null ) this.CallService<Ice.Contracts.SysMonitorTasksSvcContract>(sm=>{sm.WriteToTaskLog(s, taskLog.SysTaskNum, Epicor.ServiceModel.Utilities.MsgType.Info);});
			}
		};
	//__ Write to Trace Log ______________________________________________
		Action<string> addTrace = s => { 
			Ice.Diagnostics.Log.WriteEntry(s);
		};
	//____________________________________________________________________



using ( var svc = Ice.Assemblies.ServiceRenderer.GetService<Ice.Contracts.DynamicQuerySvcContract>(context) ) {

	var dsQuery = svc.GetByID (QueryName);  // BAQ Name for Orders

	if ( dsQuery == null ) {

		addLog("Unable to get query " + QueryName);

	} else {

		var p = svc.GetQueryExecutionParameters (dsQuery);
		var results = svc.Execute (dsQuery, p); 
		addLog("Query Successful");

		if ( results.Tables["Results"].Rows.Count > 0 ) { 

			//__ Set variables, Functions for email ____________________
			var rowCount = results.Tables["Results"].Rows.Count;
			addLog( String.Format("Query returned {0} rows", rowCount) );

			var SS = Db.Company.Where(x => x.Name.Contains("SURESTEP")).FirstOrDefault();

			var ConnectionString = (string)SS[""]; // "Server=EPAPP02;Database=DienenProdSP;Integrated Security=True;";
			var ssrsPrefix = (string)SS["ssrsPrefix_c"] + "Sales Management/"; // "/E10Prod/Sales Management/";
			var company = "SS";
			var ssrsServer = (string)SS["ssrsServer_c"]; // "EPAPP02";
			var svcAddress = string.Format("http://{0}/reportserver/ReportExecution2005.asmx", ssrsServer);
			var savePath = @"C:\EpicorData\Companies\SS\Processes\kevinv\";

			Func<string,string> getRptPath = s => string.Format("{0}{1}", ssrsPrefix, s);

			Func<string,int,string> getOFormFiles = (s,i) => { //i==1: Background file

				switch ( s ) {

					case "webTLSO":
						return ( i == 1 ) ? "ACTIVE-TLSO.pdf": "OForm TLSO_Active";
						break;

					case "webHEKO":
						return ( i == 1 ) ? "ACTIVE-HEKO.pdf": "OForm HEKO_Active";
						break;

					case "webKAFO":
						return ( i == 1 ) ? "ACTIVE-KAFO.pdf": "OForm KAFO_Active";
						break;

					default:
						return ( i == 1 ) ? "ACTIVE-SMOAFO.pdf": "OForm SMO-AFO_Active";
						break;
				}
			};

			Func<int,int,string> getFileName = (oNum,oLn) => {
				string sLine = ( oLn == 0 )? "": (", Line " + oLn.ToString());
				string sRpt  = ( oLn == 0 )? "Order Confirmation": "O-Form";
				return string.Format(@"Surestep {0} - Order {1}{2}.pdf",sRpt,oNum,sLine);
			};

			List<string> AttachmentFiles = new List<string>();

			addLog("Ready to create reports");

			foreach ( DataRow r in results.Tables["Results"].Rows ) {
				// This is where the magic happens

				var orderNum = (int)r["OrderDtl_OrderNum"];
				var orderLine = (int)r["OrderDtl_OrderLine"];
				var basePN = (string)r["OrderDtl_BasePartNum"];

				AttachmentFiles.Add(string.Format(@"{0}{1}",savePath,getFileName(orderNum, orderLine)));
				// Add O-Form file name to List
				addLog(string.Format("OForm {0} Added to List",AttachmentFiles.LastOrDefault()));

				var ofParms = new System.Collections.Specialized.OrderedDictionary();
				ofParms.Add( "Company"  , "SS");
				ofParms.Add( "OrderNum" , orderNum.ToString());
				ofParms.Add( "OrderLine", orderLine.ToString());

				var ofBytes = CodaBears.Epicor.QuickPrint.Reports.GetSSRSReport(svcAddress, getRptPath(getOFormFiles(basePN,0)), ofParms);

				System.IO.File.WriteAllBytes(AttachmentFiles.LastOrDefault(), ofBytes);
				// Create O-Form PDF
				addLog("OForm created (no background)");

				var bkgPath = string.Format ( @"\\Epapp02\oform_pdf-do_not_change\"  );
				var watermarkFile = string.Format( @"{0}{1}", bkgPath, getOFormFiles(basePN,1) );

				var watermarkImage = XImage.FromFile ( watermarkFile );
				var source = PdfReader.Open ( AttachmentFiles.LastOrDefault() );
				var gfx = XGraphics.FromPdfPage ( source.Pages[0], XGraphicsPdfPageOptions.Prepend );
				var width = watermarkImage.PixelWidth * 72 / watermarkImage.HorizontalResolution;
				var height = watermarkImage.PixelHeight * 72 / watermarkImage.VerticalResolution;
				gfx.DrawImage ( watermarkImage, 0, -180, width, height );

				source.Save ( AttachmentFiles.LastOrDefault() );
				addLog("Background watermark added to OForm"); 

				if ( (int)r["Calculated_MaxOL"] == orderLine ) {
						// Get OrderConf, send email on last line for order
						addLog("Final Line Started");

					AttachmentFiles.Add(string.Format(@"{0}{1}",savePath,getFileName(orderNum, 0)));
						// Add Order Confirmation file name to List
						addLog(string.Format("SOAck {0} Added to List",AttachmentFiles.LastOrDefault()));

					var soaParms = new System.Collections.Specialized.OrderedDictionary();
						soaParms.Add("Company", "SS");
						soaParms.Add("OrderNum", orderNum.ToString());

					var soaBytes = CodaBears.Epicor.QuickPrint.Reports.GetSSRSReport(svcAddress, getRptPath("SOAck_New"), soaParms);

					System.IO.File.WriteAllBytes(AttachmentFiles.LastOrDefault(), soaBytes);
						// Create OrderConf PDF
						addLog("SOAck created and saved");

					var sFrom = "weborders@surestep.net";
					var smtp  = new SmtpClient();
					var cred  = new NetworkCredential(sFrom, this.EfxLib.Send_Portal_Forms.get_smpt_auth("SMTP"));
					var SendFrom = new MailAddress(sFrom);

					smtp.Host = "smtp.office365.com";
					smtp.Port = 587;
					smtp.UseDefaultCredentials = false;
					smtp.EnableSsl = true;
					smtp.Credentials = cred;

					var message = new MailMessage();

					message.From = SendFrom;
					message.To.Add("kevinv@dienen.com"); //message.To.Add((string)r["OrderHed_PTUserEmail_c"]);
					message.Subject = "Attachment Test";
					message.Body = "Test with attachments";

					addLog("Message Created to kevinv@dienen.com from " + sFrom);

					foreach ( string fileName in AttachmentFiles ) {

						message.Attachments.Add(new Attachment(fileName));
							addLog(fileName + " attachment added");
					}

					//string userState = "Sending Email";
					smtp.Send(message);
					addLog("Email sent");

					message.Dispose();

					foreach ( string fileName in AttachmentFiles ) {

						System.IO.File.Delete( fileName );
						addLog(fileName + " file deleted");
					}

					AttachmentFiles.Clear();
					addLog("Attachments list emptied. Current # of elements: " + AttachmentFiles.Count.ToString());
				}
			}
		}
	}
}
