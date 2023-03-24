/*== Import TariffCode v1.1 ==================================================
   
	Object: Erp.BO.SalesOrder
	Method: ChangePartNumMaster
	
	Type: Pre-Processing
	Name: Import-TariffCode   

	Created: 06/13/2018 -Kevin Veldman
	Changed: 03/06/2023
	
	File: Import_TariffCode.cs
	Info: Set Tariff Code from Part.UserChar1 > OrderDtl.cIntlCustomsCode_c

		--Kevin Veldman <kevinv@surestep.net>
============================================================================*/

/* Set intlShip to true for International Orders */
	
	var od = ttOrderDtl.Where(x => x.Added() || x.Updated()).FirstOrDefault();

	if ( Db.OrderHed.FirstOrDefault(x => x.OrderNum == od.OrderNum && x.Company == od.Company).IntrntlShip ) {

		string tariffCode = Db.Part.FirstOrDefault(x => x.PartNum == od.PartNum && x.Company == od.Company).UserChar1 ?? "";
		od["cIntlCustomsCode_c"] = tariffCode;
	}


/*== CHANGE LOG ==============================================================

	03/06/2023: Refactored;

============================================================================*/