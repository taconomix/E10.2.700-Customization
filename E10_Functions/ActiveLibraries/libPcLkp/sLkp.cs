/*== Return Lookup Value from PcLookupTable ==================================

	Library:  libPcLkp
	Function: sLkp
	Version:  v1.0.1

		Request Parameters: 3
			Tbl: System.String
			Col: System.String
			Row: System.String

		Response Parameters: 1
			lkpVal: System.String

	Created: 03/02/2023
	Changed: 

		Kevin Veldman <KVeldman@Hanger.com>
============================================================================*/

// Get SeqNum for first Column
	int keySeq = Db.PcLookupColSetDtl
		.Where(x => x.LookupTblID == Tbl)
		.ToList()
		.Min(x => x.SeqNum);

// Get Column Name from KeySeq
	string keyCol = Db.PcLookupColSetDtl
		.Where(x => x.LookupTblID == Tbl && x.SeqNum == keySeq)
		.FirstOrDefault()
		.ColName.ToString();

// Get Return Value
	lkpVal = Db.PcLookupTblValues
		.Join(Db.PcLookupTblValues, 
			x => new { x.LookupTblID, x.RowNum }, 
			y => new { y.LookupTblID, y.RowNum }, 
			(x,y) => new { ID = x, TD = y})
		.Where(z => 
			   z.ID.ColName == keyCol 
			&& z.TD.ColName == Col 
			&& z.ID.DataValue == Row 
			&& z.TD.LookupTblID == Tbl)
		.FirstOrDefault()
		.TD.DataValue;


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