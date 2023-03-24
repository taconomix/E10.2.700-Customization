/*== Set AutoPrintReady to false after Auto-email ============================

	Library:  libOrderCfg
	Function: unsetAPReady
	Version:  v1.0.0

		Request Parameter: orderNum (System.Int32)
		Response Parameter: none

	Created: 02/22/2023
	Changed: 

		--Kevin Veldman <KVeldman@Hanger.com>
============================================================================*/

var oh = this.Db.OrderHed.Where(x => x.Company == "SS" && x.OrderNum == orderNum).FirstOrDefault();

if ( oh != null ) {
	oh.AutoPrintReady = false;
	Db.SaveChanges();
}