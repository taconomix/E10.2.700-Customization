//Method directive - Erp.CustShip.Update
//Pre-processing

var sd = ttShipDtl.Where( x => x.Added() || x.Updated() ).FirstOrDefault();

if ( sd != null ) {

	if ( sd.CustNum == 1007 || sd.CustNum == 1110 ) {

		var od = Db.OrderHed.Where( x => x.Company == CompanyID
				&& x.OrderNum == sd.OrderNum 
				&& x.PONum == string.Empty)
			.FirstOrDefault();

		if ( od != null ) {
			throw new Ice.BLException("Hanger order does not have required PO. Hold Shipping until Valid PO is added.");
		}
	}
}