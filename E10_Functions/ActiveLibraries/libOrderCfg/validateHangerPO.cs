/*== Validate PO for Hanger Direct ===========================================

	Library:  libOrderCfg
	Function: validateHangerPO
	Version:  v1.0.0

		Request Parameter: poNum (System.String)

		Response Parameter: isValid (System.Bool)

	Created: 03/06/2023
	Changed: 

		Kevin Veldman <KVeldman@Hanger.com>
============================================================================*/

int POMax = 16000000;

if ( poNum.Length >= 8 ) {

	var lPO = poNum.Substring(0,8);	
	var iPO = int.TryParse(lPO, out int i)? Convert.ToInt32(lPO): 0;
	
	isValid = (iPO >= 14000000 && iPO <= POMax);

} else {

	isValid = false;
}


/*== CHANGE LOG ==============================================================


============================================================================*/

	/*__ Added Usings ____________________________________________________
	____________________________________________________________________*/

	/*__ Reference _______________________________________________________

		//
	____________________________________________________________________*/