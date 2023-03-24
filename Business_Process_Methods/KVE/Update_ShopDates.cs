/*== Update_ShopDates v1 =====================================================
	 
	Object: Erp.BO.SalesOrder
	Method: MasterUpdate
	
	Type: Post-Processing
	Name: Update_ShopDates

	Created: 03/06/2023 -Kevin Veldman
	Changed: 
	
	File: Update_ShopDates.cs
	Info: Update RequestDate, NeedByDate on OrderHed + Configured Lines

		--Kevin Veldman <kevinv@surestep.net>
============================================================================*/

var oh = ds.OrderHed.Where(x => x.OrderNum == ttOrderHed.FirstOrDefault().OrderNum).FirstOrDefault();

if ( Db.OrderDtl.Where(x => x.OrderNum == oh.OrderNum && x.LastConfigDate != null).Any() ) {

	foreach ( var od in Db.OrderDtl.Where(x => x.OrderNum == oh.OrderNum && x.LastConfigDate != null) ) {
						
		Func<string,string> sVal = s => (string)od[s];
		Func<string,bool  > kVal = s => (bool)od[s];
		string sKey = sVal("cDeviceCode_c");
						
		if ( sKey.Length > 0 ) { 
			var shopDates =
				(Tuple<System.DateTime?, System.DateTime?>)this.InvokeFunction( 
					"libOrderCfg", "getShopDates", Tuple.Create(
						sKey, kVal("kRush0D_c"), kVal("kRush1D_c"), sVal("ModType_c"), kVal("kBoot_c"), kVal("kKAFO_c"), sVal("cAntShell_c")
					) 
				);


			od.RequestDate = shopDates.Item1;
			od.NeedByDate  = shopDates.Item2;
												
			if( oh.RequestDate == null || oh.RequestDate < shopDates.Item1 ) oh.RequestDate = shopDates.Item1;
			if( oh.NeedByDate  == null || oh.NeedByDate  < shopDates.Item2 ) oh.NeedByDate  = shopDates.Item2;
		}
	}
}

/*== CHANGE LOG ==============================================================

============================================================================*/

	/*__ Additional BPM Widgets ________________________________________________

	1. Condition (BEFORE CUSTOM CODE WIDGET)
		This directive has been enabled from the Enable_Hold_Tracker directive
	__________________________________________________________________________*/