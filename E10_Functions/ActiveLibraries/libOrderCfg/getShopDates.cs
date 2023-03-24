/*== Return Calculated Turnaround/Promised Dates =============================

	Library:  libOrderCfg
	Function: getShopDates
	Version:  v1.0.1

		Request Parameters: 7
			sKey:     System.String
			k0day:    System.Bool
			kRush:    System.Bool
			ModType:  System.String
			kBoot:    System.Bool
			kKAFO:    System.Bool
			AntShell: System.String

		Response Parameters: 2
			TurnDate: System.DateTime?
			PromDate: System.DateTime?


	Created: 03/01/2023
	Changed: 

		Kevin Veldman <KVeldman@Hanger.com>
============================================================================*/

Func<string,string,string> lsCol = (t,c) => 
	this.EfxLib.libPcLkp.lsCol(t,c);

Func<string,string,string,string> sLkp = (t,c,r) => 
	this.EfxLib.libPcLkp.sLkp(t,c,r);

DateTime dtTmp = Convert.ToDateTime(BpmFunc.Today());
string sYear = BpmFunc.Year(dtTmp).ToString();


//== Date increment Function =================================================
	Action<int> plusDays = (iDays) => {

		Action<int> DateCheck = (checkDay) => {

			string[] Holidays = lsCol("holidays",sYear).Split('~');
			dtTmp = dtTmp.AddDays(checkDay);

			for (int i = 0; i < Holidays.Length; i++) {
				if(Holidays[i]==dtTmp.ToString("yyyyMMdd")) dtTmp = dtTmp.AddDays(1);
			}

			if (dtTmp.DayOfWeek == DayOfWeek.Friday  ) dtTmp = dtTmp.AddDays(3);
			if (dtTmp.DayOfWeek == DayOfWeek.Saturday) dtTmp = dtTmp.AddDays(2);
			if (dtTmp.DayOfWeek == DayOfWeek.Sunday  ) dtTmp = dtTmp.AddDays(1);
		};

		for (int i = 0; i < iDays; i++){

			DateCheck(1);
			DateCheck(0);
		}
	};
//================================ Skip weekend/holidays =====================

Func<string,bool> kStrInt = s => int.TryParse(s, out int i); 
Func<string,int> iStr = s => kStrInt(s)? Convert.ToInt32(s): 0;
Func<string,bool> kp = s => s!="" && s.ToUpper()!="NONE";
Func<string[],string,bool> sAIO  = (a,s) => ( Array.IndexOf(a,s) >= 0 );


if ( !kRush && !k0day ) {

	//__ Turnaround Date _____________________________________________
		int iTurn = iStr( sLkp("DefaultSpecs", "TurnDays", sKey) );
		
		if ( ModType!="M" ) iTurn += 2;
		if ( kKAFO        ) iTurn += 2;
		if ( kp(AntShell) ) iTurn += 2;
		
		string[] bootSkip = { 
			"122","124","126","211","212","216","221","226","230", "BIG", "BSL"
			};
		
		if ( kBoot && !sAIO(bootSkip, sKey) ) iTurn += kStrInt(sKey)? 2: 1;
		
		plusDays(iTurn);
		TurnDate = dtTmp;
	//________________________________________________________________
	
	//__ Promised Date _______________________________________________
		int iProm = iStr( sLkp("Standards", "Value", "PromiseDays") );
		
		if ( kKAFO ) iProm += ModType=="M"? 2: 4;
		if ( kStrInt(sKey) ) iProm += 1;
		
		plusDays(iProm);
		PromDate = dtTmp;
	//________________________________________________________________

} else {

	plusDays(k0day? 0: 1);
	TurnDate = dtTmp;
	PromDate = dtTmp;
}


/*== CHANGE LOG ==============================================================


============================================================================*/

	/*__ Added Usings ____________________________________________________
	____________________________________________________________________*/

	/*__ Reference _______________________________________________________

		Config UDMethod: drGetShopDates (Config=AFO7)
		Config UDMethod: drGetShopDates (Config=SMO11)

		BPM Method Directive: Hold-Tracker-v3 (SalesOrder.BO.MasterUpdate)

	____________________________________________________________________*/