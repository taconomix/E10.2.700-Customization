/*== Enable_HoldTracker v1 ===================================================
	 
	Object: Erp.BO.SalesOrder
	Method: MasterUpdate
	
	Type: Pre-Processing
	Name: Enable_Hold_Tracker

	Created: 03/06/2023 -Kevin Veldman
	Changed: 
	
	File: Enable_HoldTracker.cs
	Info: Set custom on/off hold OrderHed fields;
		Also enables Req/NeedBy Date Post-Processing Method;

		--Kevin Veldman <kevinv@surestep.net>
============================================================================*/

kUpdateShopDates = false; // BPM Variable

var oh = ttOrderHed.Where(r => r.Added() || r.Updated()).FirstOrDefault();

if ( oh != null ) {

	var HeldChange = (oh.RowMod == "A") ? oh.OrderHeld :
					ttOrderHed.Any(r1 => r1.Unchanged() 
						&& r1.SysRowID	== oh.SysRowID 
						&& r1.OrderHeld != oh.OrderHeld);


	if ( HeldChange ) {

		// Prevent changes for shipped orders. 
		if ( !(Db.OrderHed.Join(Db.ShipDtl, x => x.OrderNum, y => y.OrderNum, (x,y) => new {ohSO=x, sdSO=y }).Where(z => z.ohSO.OrderNum==oh.OrderNum).Any()) ) {

			if ( oh.OrderHeld ) { //update dtOnHold_c when put on hold for the first time.

				if (oh["dtOnHold_c"] == null) oh["dtOnHold_c"] = BpmFunc.Today();

			} else { // update dtOffHold_c 

				oh["dtOffHold_c"] = BpmFunc.Today();	

				kUpdateShopDates = true; // Set to true to enable Post-Processing;
			}
		}
	}
}

/*== CHANGE LOG ==============================================================

============================================================================*/