/*== Dupe PO Warning =========================================================
    
    Info: Check for Dupe PONum in past 90 Days
    Type: Method Directive (Pre-Processing), BO=SalesOrder.MasterUpdate

    Created: 11/20/2022

    --Kevin Veldman <KevinV@dienen.com>
============================================================================*/

var ttHedRow = ttOrderHed.Where(r => r.Added() || r.Updated()).FirstOrDefault();

if ( ttHedRow != null ) {

    int daysToCheck = 90;
    bool kChangePO = false;

    var addedRow = ttOrderHed.Where(x => x.RowMod=="A").FirstOrDefault();
    if ( addedRow != null ) kChangePO = addedRow.PONum != null;

    if ( !kChangePO ) {

        kChangePO = ttOrderHed.Any(r0 => r0.Updated() 
            && ttOrderHed.Any(r1 => r1.Unchanged() 
                && r1.SysRowID == r0.SysRowID 
                && r1.PONum    != r0.PONum));
    }

    if ( kChangePO ) {

        var dtCheck = BpmFunc.AddInterval(BpmFunc.Today(), (-1*daysToCheck), IntervalUnit.Days);
        var dupeRow = Db.OrderHed.Where(oh => oh.Company == ttHedRow.Company 
            && oh.CustNum  == ttHedRow.Company 
            && oh.PONum    == ttHedRow.PONum 
            && oh.OrderDate > dtCheck
            && oh.OrderNum != ttHedRow.OrderNum).FirstOrDefault()


        if ( dupeRow != null ) {

            var sWarn = String.Format(@"PO Num {0} has been used in the past {2} days (Order {1}). Please check if this is a duplicate or remake.",
              dupeRow.PONum, dupeRow.OrderNum, daysToCheck);

            this.PublishInfoMessage(sWarn, Ice.Common.BusinessObjectMessageType.Information, Ice.Bpm.InfoMessageDisplayMode.Individual,
              "SalesOrder", "CloseOrderLine");
        }
    }
}




/*== CHANGE LOG ====================================================================

    2022/11/30: Only check Dupe PO on PO Change; stops info warning on every save;

                                        See below info message for source.
=================================================================================*/



    /*____________________________________________________________________
    REFERENCE: C# code for  "...changed from any to another" BPM Widget
    https://www.epiusers.help/t/bpm-c-the-specified-field-has-changed-from-any-to-another/70079

        bool ReqDueDate_fieldChanged =
            ttJobHead.Any(r => r.Updated() &&
            ttJobHead.Any(r1 => r1.Unchanged() && r1.SysRowID == r.SysRowID &&
            r1.ReqDueDate != r.ReqDueDate));

        if ( ReqDueDate_fieldChanged ) {

        }

        if ( !ReqDueDate_fieldChanged ) {

        }
    ____________________________________________________________________*/


    /*____________________________________________________________________  
    REFERENCE: Code correction from previous version on EpiUsers Forum
    https://www.epiusers.help/t/bpm-to-warn-user-entering-a-duplicate-po-number/96972/2
    ____________________________________________________________________*/