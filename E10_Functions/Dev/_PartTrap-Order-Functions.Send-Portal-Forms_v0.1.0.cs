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

string QueryName = "WebFormsQueue";

Ice.Diagnostics.Log.WriteEntry ("Send-Portal-Forms: Starting");

var context = Ice.Services.ContextFactory.CreateContext<ErpContext>();

using ( var svc = Ice.Assemblies.ServiceRenderer.GetService<Ice.Contracts.DynamicQuerySvcContract>(context) ) {

	var dsQuery = svc.GetByID (QueryName);  // BAQ Name for Orders

	if ( dsQuery == null ) {

		Ice.Diagnostics.Log.WriteEntry ("Send-Portal-Forms - unable to get query " + QueryName);    
	
	} else {

		var p = svc.GetQueryExecutionParameters (dsQuery);
		var results = svc.Execute (dsQuery, p); 

		if ( results.Tables["Results"].Rows.Count > 0 ) {

			int lastSO = 0;

			foreach ( DataRow r in results.Tables["Results"].Rows ) {

				var orderNum = (int)r["OrderDtl_OrderNum"];
				var orderLine = (int)r["OrderDtl_OrderLine"];
				var sendTo = "kevinv@dienen.com";
				//var sendTo = (string)r["OrderHed_PTUserEmail_c"];
				var basePN = (string)r["OrderDtl_BasePartNum"];
				var rptStyle = 1008;

				if ( sendTo.Length > 0 ) {
				
					if ( orderNum != lastSO ) {

						this.EfxLib.Send_Portal_Forms.Send_OrderAck(orderNum, 1008, sendTo);
						this.EfxLib.Send_Portal_Forms.Unset_APReady(orderNum);
					}

					this.EfxLib.Send_Portal_Forms.Send_QPOForm(orderNum.ToString(), orderLine.ToString(), basePN, sendTo);
				}
				
				lastSO = orderNum;
			}
		}
	}
}