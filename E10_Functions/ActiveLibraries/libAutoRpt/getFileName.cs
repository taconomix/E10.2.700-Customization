/*== Return O-Form/OrderConfmessage FileName =================================

	Library:  libAutoRpt
	Function: getFileName
	Version:  v1.0.0

		Request Parameters:  
			orderNum  (System.Int32)
			orderLine (System.Int32)

		Response Parameter: filename (System.String) 

	Created: 02/22/2023
	Changed: 

		--Kevin Veldman <KVeldman@Hanger.com>
============================================================================*/


if ( orderLine == 0 ) {

	filename = string.Format("Order Confirmation - Surestep Order #{0}.pdf",orderNum);
	
} else {
	
	filename = string.Format("O-Form - Surestep Order #{0} Line {1}.pdf",orderNum,orderLine);
}