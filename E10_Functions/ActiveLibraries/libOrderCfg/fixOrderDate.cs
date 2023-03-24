/*== Corerct Order Date on PartTrap Orders ===================================

	Library:  libOrderCfg
	Function: fixOrderDate
	Version:  v1.0.0

		Request Parameter: orderNum (System.Int32)
		Response Parameters: None

	Created: 03/03/2023
	Changed: 

		Kevin Veldman <KVeldman@Hanger.com>
============================================================================*/

string user = "PARTTRAP";
string comp = "SS";

var oh = this.Db.OrderHed
	.Where(x => 
			 x.OrderNum    == orderNum 
		&& x.Company     == comp 
		&& x.EntryPerson == user)
	.FirstOrDefault();
	
if ( oh != null ) {
	oh.OrderDate = BpmFunc.Today();
	Db.SaveChanges();
}


/*== CHANGE LOG ==============================================================


============================================================================*/

	/*__ Added Usings ____________________________________________________
	____________________________________________________________________*/

	/*__ Reference _______________________________________________________

		E10 Function: Send-Portal-Forms.AutoSend-PortalForms
	____________________________________________________________________*/