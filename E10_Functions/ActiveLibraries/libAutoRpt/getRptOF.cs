/*== Get O-Form Report Name from BasePN ======================================

	Library:  libAutoRpt
	Function: getRptOF
	Version:  v1.0.0

		Request Parameter:  basePN  (System.String)
		Response Parameter: rptName (System.String) 

	Created: 02/21/2023
	Changed: 

		--Kevin Veldman <KVeldman@Hanger.com>
============================================================================*/

switch ( basePN ) {

	case "webTLSO":
		rptName = "OForm TLSO_Active";
		break;

	case "webHEKO":
		rptName = "OForm HEKO_Active";
		break;

	case "webKAFO":
		rptName = "OForm KAFO_Active";
		break;

	default:
		rptName = "OForm SMO-AFO_Active";
		break;
}