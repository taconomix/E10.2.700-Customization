/*== Return All Values from Column in PcLookupTables =========================

	Library:  libPcLkp
	Function: lsCol
	Version:  v1.0.1

		Request Parameters: 2
			Tbl: System.String
			Col: System.String

		Response Parameters: 1
			output: System.String

	Created: 03/02/2023
	Changed: 

		Kevin Veldman <KVeldman@Hanger.com>
============================================================================*/

output = string.Join("~", 
	Db.PcLookupTblValues
		.Where( x => x.LookupTblID == Tbl && x.ColName == Col )
		.AsEnumerable()
		.Select( s => s.DataValueString )
		.OrderBy( s => s )
		.ToArray<string>() );


/*== CHANGE LOG ==============================================================

	

============================================================================*/

	/*__ Added Usings ____________________________________________________

	____________________________________________________________________*/

	/*__ Reference _______________________________________________________
	
		Return LINQ Query Data as Array
			https://codereview.stackexchange.com/a/72053

		Return Array using Lambda Expression
			https://stackoverflow.com/a/1378823
	____________________________________________________________________*/