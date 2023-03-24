//New Dupe Check
	//Custom code for SO Num string Variable
	var myHedRow = (from ttOrderHed_Row in ttOrderHed 
    	where ttOrderHed_Row.RowMod == "A" || ttOrderHed_Row.RowMod == "U" 
    	select ttOrderHed_Row).FirstOrDefault(); 

	var myOrderRow = (from OrderHed_Row in Db.OrderHed 
		where OrderHed_Row.OrderNum != myHedRow.OrderNum && (OrderHed_Row.OrderDate > PastDays) && OrderHed_Row.PONum == myHedRow.PONum && OrderHed_Row.Company == myHedRow.Company 
		select OrderHed_Row).FirstOrDefault();
    
	if (myHedRow != null && myOrderRow != null) {
    
		DupeOrders = myOrderRow.OrderNum.ToString();
	}

	//Extra Conditional
		return DupeOrders != null;

//Import Tariff Code BPM. 
	//Set variable "intlShip" to Boolean
		/* Set UD Tariff Code Field if order is International */
			var myDtlRow = (from ttOrderDtl_Row in ttOrderDtl
			  where ttOrderDtl_Row.RowMod == "A" || ttOrderDtl_Row.RowMod == "U"
			  select ttOrderDtl_Row).FirstOrDefault();

			var myOrderRow = (from OrderHed_Row in Db.OrderHed 
			  where OrderHed_Row.OrderNum == myDtlRow.OrderNum && OrderHed_Row.Company == myDtlRow.Company
			  select OrderHed_Row).FirstOrDefault();

			var myPartRow = (from Part_Row in Db.Part
			  where Part_Row.PartNum == partNum && Part_Row.Company == myDtlRow.Company
			  select Part_Row).FirstOrDefault();

			intlShip = myOrderRow.IntrntlShip;
			if(intlShip)
			{
			  myDtlRow["cIntlCustomsCode_c"] = myPartRow["UserChar1"];
			}


//Set UD On- & Off-Hold Date fields. 
	//Condition0: OrderHeld changes False >> True; Condition1: OrderHeld changes True >> False
		//If Condition0 == true
		/*Update UD On-Hold Date field only if field is blank*/
		  var myHedRow = (from ttOrderHed_Row in ttOrderHed
		   where ttOrderHed_Row.RowMod == "A" || ttOrderHed_Row.RowMod == "U"
		   select ttOrderHed_Row).FirstOrDefault();

		  if (myHedRow["dtOnHold_c"] == null)
		    {
		      myHedRow["dtOnHold_c"] = BpmFunc.Today();
		       /*TESTING PURPOSES ONLY*/
		       /* myHedRow["dtOnHold_c"] = BpmFunc.AddInterval(BpmFunc.Today(),7,IntervalUnit.Days);*/
		    }


		//If Condition0 == false && Condition1 == true
		/*Update UD Off-Hold Date field even if field is populated*/
		  var myHedRow = (from ttOrderHed_Row in ttOrderHed
		   where ttOrderHed_Row.RowMod == "A" || ttOrderHed_Row.RowMod == "U"
		   select ttOrderHed_Row).FirstOrDefault();

		  myHedRow["dtOffHold_c"] = BpmFunc.Today();
		 
		 /*TESTING PURPOSES ONLY*/
		 /* myHedRow["dtOffHold_c"] = BpmFunc.AddInterval(BpmFunc.Today() ,7 ,IntervalUnit.Days); */





/*Set Create Date for New ShipTo*/
	foreach (var ttShipTo_iterator in (from ttShipTo_Row in ttShipTo
	where string.Compare(ttShipTo_Row.RowMod, "A", true) == 0
	select ttShipTo_Row))
	{
	var ttShipTo_xRow = ttShipTo_iterator;
	ttShipTo_xRow["CreateDate_c"] = BpmFunc.Today();

	Db.Validate();
	}


//BPM Function to add days
	BpmFunc.AddInterval  ( BpmFunc.Today  (  ) ,  -90 , IntervalUnit.Days ) 



//CODE FOR SO MASTERUPDATE CUSTOMER WARNINGS

	/*set variable for initial save*/
		var myHedRow = (from ttOrderHed_Row in ttOrderHed
		  where ttOrderHed_Row.RowMod == "A"
		  select ttOrderHed_Row).FirstOrDefault();
		   
		if (myHedRow != null) {
		  saveCust = myHedRow.CustNum != null?true:false;
		} else {
		  saveCust = false;
		}

	/*Set Terms Warning*/
		var myHedRow = (from ttOrderHed_Row in ttOrderHed
			where ttOrderHed_Row.RowMod == "A" || ttOrderHed_Row.RowMod == "U"
			select ttOrderHed_Row).FirstOrDefault();

		if (myHedRow.TermsCode == "COD"){
			termsWarning = "This is a COD customer. Add appropriate charges to sales order.";
		} else if (myHedRow.TermsCode == "PP") {
			termsWarning = "This is a prepay customer, please add lines and contact Kim so she can get payment.";
		} else if (myHedRow.TermsCode == "PPP") {
			termsWarning = "This customer is on a prepay plus payment plan. Please let accounting know of the new order and place on hold until we receive payment to release order.";
		}


1073,1181,1533,1125,1418,815,852,891,830,956,1808,1313,1554,2244,1195,1057

if(!bA){sA = "";}
if(!bB){sB = "";}
if(!bC){sC = "";} 
if(!bD){sD = "";} 
if(!bE){sE = "";} 


//Customer Messages BPM

	/* SET TRUE-FALSE VALUES FOR ALERT MESSAGES */

		//Priority Customers
			int[] listPriorityCust = {1073, 1181, 1533, 1125, 1418, 815, 852, 891, 830, 956, 1808, 1313, 1554, 2244, 1195, 1057};

			isPriorityCust = (Array.IndexOf(listPriorityCust,iCustNum) >= 0);

		//Check Before Shipping Customers
			int[] listShipCheck = {1533, 1554, 1336};
			
			isShipCheck = (Array.IndexOf(listShipCheck,iCustNum) >= 0);
		
		//Address Warning Customers
			int[] listAddressWarn = {945, 1772, 898};
			
			isAddressWarn = (Array.IndexOf(listAddressWarn,iCustNum) >= 0);
			
		//Require PO Number Customers
			int[] listRequirePO = {956, 1007, 891};
			
			isRequirePO = (Array.IndexOf(listRequirePO,iCustNum) >= 0);

		//Customers with Customer-Specific Alert
			int[] listCustAlert = {966, 1546};
			
			isCustAlert = (Array.IndexOf(listCustAlert,iCustNum) >= 0);



	/* DEFINE CUSTOMER-SPECIFIC ALERTS */

		//Sets unique alert for each customer. 
			switch (iCustNum) {
				case 966:  /*Falk*/
					custAlert = "Print second copy of this order form on white paper for shipping.";
					break;
				case 1546:  /*Tayco*/
					custAlert = "Add the PO number from Tayco's email before the patient name on this order. Thanks!";
					break;
				default:  /*All others Blank*/
					custAlert = "";
					break;
			};

		/*
			To add new customer-specific messages:
			
			Copy a Case-Statement (3 lines, ends at "break;") and paste it above the "default:" case.
			Replace the customer number (number just after "case"), name (green comment between "/* * /"), and alert message (blue text in quotations).
		
		*/



	/* OLD CODE - NOT IN USE */

		// Select customers that require Alerts 
			//Select active rows from Sales Order & Customer
				var myHedRow = (from ttOrderHed_Row in ttOrderHed
					where ttOrderHed_Row.RowMod == "A" || ttOrderHed_Row.RowMod == "U"
					select ttOrderHed_Row).FirstOrDefault();

				var myCustRow = (from Customer_Row in Db.Customer
					where Customer_Row.CustNum == iCustNum && Customer_Row.Company == myHedRow.Company
					select Customer_Row).FirstOrDefault();

		// Set conditional variables for alerts
			isPriorityCust = myCustRow.kPriorityCust_c;
			isAddressWarn  = myCustRow.kAddressWarn_c;
			isShipCheck    = myCustRow.kShipCheck_c;
			isRequirePO    = myCustRow.kRequirePO_c;
			custAlert      = myCustRow.cCustAlert_c;

		// Set customer-specific Alerts
			if (iCustNum == 966)  
			{ /*Falk*/
				custAlert = "Print second copy of this order form on white paper for shipping.";
			} 
			else if (iCustNum == 1546) 
			{ /*Tayco*/ 
				custAlert = "Add the PO number from Tayco's email before the patient name on this order. Thanks!";
			} 
			else  
			{ /*All others Blank*/
				custAlert = "";
			}

		/*To add customer specific alert, copy the lines starting with "else if" down to the next "}", and replace the custNum, message, and comment with the customer name*/


/*Erp.BO.CashRec.UpdateMaster/Pre-Processing*/
	//Customer-Based warning in "Cash Receipts Entry" > "New Invoice Payment"

	//Start > ExecuteCustomCode0 > Condition0 > ExecuteCustomCode1 > Condition1 > ShowMessage0

		//Start (Variables)
			bool saveCust;
			bool kSurcharge;

		//ExecuteCustomCode0
		    var myHedRow = (from ttCashHead_Row in ttCashHead
		      where ttCashHead_Row.RowMod == "A" || ttCashHead_Row.RowMod == "U"
		      select ttCashHead_Row).FirstOrDefault();
		       
		    if (myHedRow != null) {
		      saveCust = myHedRow.CustNum != null? true: false;      
		    } else {
		      saveCust = false;
		    }

		//Condition0
		    "The ttCashHead.CustNum field has been changed from any to another"
		    OR "The saveCust argument/variable is equal to the true expression";

		//ExecuteCustomCode1
			var myHedRow = (from ttCashHead_Row in ttCashHead
			  where ttCashHead_Row.RowMod == "A" || ttCashHead_Row.RowMod == "U"
			  select ttCashHead_Row).FirstOrDefault();

			int[] listSurchCust = {716, 792, 817, 891, 910, 930, 954, 955, 971, 975, 988, 989, 991, 998, 1033, 1078, 1079, 1094, 1103, 1109, 1122, 1176, 1207, 1211, 1213, 1216, 1252, 1261, 1314, 1319, 1336, 1408, 1441, 1450, 1475, 1479, 1490, 1557, 1590, 1666, 1770, 1846, 2022, 2175, 2244, 2368, 2451, 2514, 2555};

			kSurcharge = (Array.IndexOf(listSurchCust,myHedRow.CustNum) >= 0);

		//Condition1
		    "The kSurcharge argument/variable is equal to the true expression";

		//ShowMessage0
		    "Surcharge";