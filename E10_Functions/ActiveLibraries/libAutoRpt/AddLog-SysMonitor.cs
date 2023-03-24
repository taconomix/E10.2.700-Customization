/*== Add Message to System Monitor log =======================================

	Library:  libAutoRpt
	Function: AddLog-SysMonitor
	Version:  v1.0.0

		Request Parameters: 
			msgText (System.String)
			libID (System.String)

		Response Parameters: None

	Created: 02/27/2023
	Changed: 

		--Kevin Veldman <KVeldman@Hanger.com>
============================================================================*/


foreach ( var task in Db.SysTask.Where(t => t.Company==Session.CompanyID && t.TaskDescription.ToLower() == "run epicor function" && t.TaskStatus.ToLower() == "active")) {
	
	var taskLog = Db.SysTaskLog.FirstOrDefault(t => t.SysTaskNum == task.SysTaskNum && t.MsgText.ToLower().Contains(libID));
	
	if ( taskLog != null ) {
		this.CallService<Ice.Contracts.SysMonitorTasksSvcContract>(sm=>{sm.WriteToTaskLog(msgText, taskLog.SysTaskNum, Epicor.ServiceModel.Utilities.MsgType.Info);});
	}
}