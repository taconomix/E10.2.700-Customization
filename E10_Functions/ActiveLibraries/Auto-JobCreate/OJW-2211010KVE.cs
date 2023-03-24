/*== Create Jobs for OrderLines in BAQ =======================================

	Library:  Auto-JobCreate
	Function: OJW-20221101KVE
	Version:  v1.1.1

		Request Parameters: None
		Response Parameters: None

	Created: 09/26/2022 -Fred Zelhart (CodaBears, LLC)
	Changed: 11/01/2022

		Kevin Veldman <KVeldman@Hanger.com>
============================================================================*/


	Ice.Diagnostics.Log.WriteEntry ("Auto-JobCreate - Starting");

var context = Ice.Services.ContextFactory.CreateContext<ErpContext>();

using (var svc = Ice.Assemblies.ServiceRenderer.GetService<Ice.Contracts.DynamicQuerySvcContract>(context)) {

	var query = svc.GetByID ("CBI4608");  // BAQ Name for Orders

	if (query == null) {

		Ice.Diagnostics.Log.WriteEntry ("Auto-JobCreate - unable to get query CBI4608");    
	
	} else {

		var p = svc.GetQueryExecutionParameters (query);
		var results = svc.Execute (query, p); 

		if (results.Tables["Results"].Rows.Count > 0) {

			var sb = new System.Text.StringBuilder("Auto-JobCreate - Processed orders: ");
			
			using (var jobWizard = Ice.Assemblies.ServiceRenderer.GetService<Erp.Contracts.OrderJobWizSvcContract>(context)) {

				foreach (DataRow r in results.Tables["Results"].Rows) {

					var orderNumber = (int) r["OrderRel_OrderNum"];
					var ds = jobWizard.SelectAll(orderNumber, true, true, true, true);
						// Params=(int OrderNum, bool CreateJob, bool GetDetails, bool ScheduleJob, bool ReleaseJob)

					string warnMessage;
					string errorMessages;
					jobWizard.ValidateJobs(ref ds, out warnMessage);

					if (warnMessage.Length > 0) {

						sb.AppendFormat ($"\r\n\t{orderNumber} warnings from ValidateJobs: {warnMessage}");

					} else {

						jobWizard.CreateJobs(ref ds, out errorMessages);

						if (errorMessages.Length > 0) {

							sb.AppendFormat ($"\r\n\t{orderNumber} errors from CreateJobs: {errorMessages}");

						} else {

							sb.AppendFormat ($"\r\n\t{orderNumber}");
						}
					}
				}
			}

			Ice.Diagnostics.Log.WriteEntry (sb.ToString());
		}
	}
}
Ice.Diagnostics.Log.WriteEntry ("Auto-JobCreate - DONE");







/*== CHANGE LOG ==============================================================

	11/01/2022: Set ReleaseJob & ScheduleJob params to true;
	03/03/2023: Update Documentation;

============================================================================*/

	/*__ Added Usings ____________________________________________________

	____________________________________________________________________*/

	/*__ Reference _______________________________________________________
		Call BAQ and get list from results:
			https://www.epiusers.help/t/epicor-functions-and-baqs/83803/21

		logging to event log reference: 
			https://www.epiusers.help/t/bpm-logging-snippet/93004

		logging to trace file: 
			https://www.epiusers.help/t/how-to-write-your-own-trace-logs/44613/3 
	____________________________________________________________________*/