/*== Return message body for O-Form email ====================================

	Library:  libAutoRpt
	Function: getMsgBody
	Version:  v1.0.0

		Request Parameter:  orderNum (System.Int32)
		Response Parameter: output   (System.String) 

	Created: 03/04/2023
	Changed: 

		--Kevin Veldman <KVeldman@Hanger.com>
============================================================================*/

System.Text.StringBuilder body = new System.Text.StringBuilder();

Action<string,int> sAdd = (s,l) => {
    body.Append(s);
    for ( var i = 0; i < l; i++ ) { body.AppendLine(); }
};

sAdd("Dear Customer,", 2);
sAdd(String.Format( "Your order #{0} has been received. Attached you will find the Order Confirmation and all O-Forms. ", orderNum ), 0);
sAdd("Please review, and let us know as quickly as possible if you find any incorrect information. Note that remake orders and any order with free-form Order Notes will be reviewed prior to fabrication, so this O-Form may change slightly before fabrication.", 2);
sAdd("If this order requires a PO and you have not yet provided one, please send the PO Number and Amount to orders@surestep.net as soon as possible. Please reference this order number, as PO Numbers submitted without an order number may cause significant delays. Thank you.", 2);
sAdd("Sincerely,", 2);
sAdd("Surestep Customer Service", 1);
sAdd("P: (877)462-0711 | F: (866)700-7837", 1);
sAdd("orders@surestep.net", 0);

output = body.ToString();