
var ttHedRow = ttOrderHed.Where(r => r.Added() || r.Updated()).FirstOrDefault();

if ( ttHedRow != null ) {

    int daysToCheck = 90;

    var addedRow = ttOrderHed.Where(x => x.RowMod=="A").FirstOrDefault();

    bool kChangePO = (addedRow != null) ? 
                     (addedRow.PONum != null ? true : false) : 
                     (ttOrderHed.Any(r0 => r0.Updated() 
                            && ttOrderHed.Any(r1 => r1.Unchanged() 
                                && r1.SysRowID == r0.SysRowID 
                                && r1.PONum != r0.PONum)));

    if ( kChangePO ) {

        var dtCheck = BpmFunc.AddInterval(BpmFunc.Today(), (-1*daysToCheck), IntervalUnit.Days);
        var dupeRow = Db.OrderHed.Where(oh => oh.Company == ttHedRow.Company 
            && oh.CustNum  == ttHedRow.CustNum 
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











