// logging to event log reference: https://www.epiusers.help/t/bpm-logging-snippet/93004
// logging to trace file: https://www.epiusers.help/t/how-to-write-your-own-trace-logs/44613/3 

Ice.Diagnostics.Log.WriteEntry ("CreateJobsForWebOrders - Starting");

// call BAQ and get list CBI4608
// reference: https://www.epiusers.help/t/epicor-functions-and-baqs/83803/21
var context = Ice.Services.ContextFactory.CreateContext<ErpContext>();

using (var svc = Ice.Assemblies.ServiceRenderer.GetService<Ice.Contracts.DynamicQuerySvcContract>(context))
{
  var query = svc.GetByID ("CBI4608");
  if (query == null) 
  {
    Ice.Diagnostics.Log.WriteEntry ("CreateJobsForWebOrders - unable to get query CBI4608");    
  }
  else 
  {
    var p = svc.GetQueryExecutionParameters (query);
    var results = svc.Execute (query, p); 
    if (results.Tables["Results"].Rows.Count > 0)
    {
      var sb = new System.Text.StringBuilder("CreateJobsForWebOrders - Processed orders: ");
      
      using (var jobWizard = Ice.Assemblies.ServiceRenderer.GetService<Erp.Contracts.OrderJobWizSvcContract>(context))
      {
        foreach (DataRow r in results.Tables["Results"].Rows)
        {
          var orderNumber = (int) r["OrderRel_OrderNum"];
          var ds = jobWizard.SelectAll(orderNumber, true, true, false, false);
          string warnMessage;
          string errorMessages;
          jobWizard.ValidateJobs(ref ds, out warnMessage);
          if (warnMessage.Length > 0)
          {
            sb.AppendFormat ($"\r\n\t{orderNumber} warnings from ValidateJobs: {warnMessage}");
          }
          else 
          {
            jobWizard.CreateJobs(ref ds, out errorMessages);
            if (errorMessages.Length > 0) 
            {
              sb.AppendFormat ($"\r\n\t{orderNumber} errors from CreateJobs: {errorMessages}");
            }
            else 
            {
              sb.AppendFormat ($"\r\n\t{orderNumber}");
            }
          }
        }
      }

      Ice.Diagnostics.Log.WriteEntry (sb.ToString());
    }
  }
}
Ice.Diagnostics.Log.WriteEntry ("CreateJobsForWebOrders - DONE");
