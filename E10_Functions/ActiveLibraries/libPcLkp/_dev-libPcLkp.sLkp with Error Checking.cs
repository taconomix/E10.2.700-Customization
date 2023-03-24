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


//--------------

// Get SeqNum for first Column
	var keyList = Db.PcLookupColSetDtl.Where(x => x.LookupTblID==Tbl).ToList();
		
	if ( keyList.Count == 0 ) return "NOPE";

	int keySeq = keyList.Min(x => x.SeqNum);

// Get Column Name from KeySeq
	string keyCol = Db.PcLookupColSetDtl
		.FirstOrDefault(x => x.LookupTblID == Tbl && x.SeqNum == keySeq)
		.DefaultIfEmpty("Already Checked")
		.ColName.ToString();

// Get Return Value
	return Db.PcLookupTblValues
		.Join(Db.PcLookupTblValues, 
			x => new { x.LookupTblID, x.RowNum }, 
			y => new { y.LookupTblID, y.RowNum }, 
			(x,y) => new { ID = x, TD = y})
		.Where(z => 
			   z.ID.ColName == keyCol 
			&& z.TD.ColName == Col 
			&& z.ID.DataValue == Row 
			&& z.TD.LookupTblID == Tbl)
		.Select(s => s.TD.DataValue)
		.DefaultIfEmpty("NOPE")
		.FirstOrDefault();