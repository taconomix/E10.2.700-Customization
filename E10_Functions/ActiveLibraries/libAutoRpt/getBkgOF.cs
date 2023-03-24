/*== Return Watermark File Path for O-Forms ==================================

	Library:  libAutoRpt
	Function: getBkgOF
	Version:  v1.0.0

		Request Parameter: basePN (System.String)
		Response Parameter: bkgFile (System.String) 

	Created: 02/22/2023
	Changed: 

		--Kevin Veldman <KVeldman@Hanger.com>
============================================================================*/

var bkgPath = string.Format ( @"\\Epapp02\oform_pdf-do_not_change\"  );

switch ( basePN ) {

	case "webTLSO":
		bkgFile = bkgPath + "ACTIVE-TLSO.pdf";
		break;

	case "webHEKO":
		bkgFile = bkgPath + "ACTIVE-HEKO.pdf";
		break;

	case "webKAFO":
		bkgFile = bkgPath + "ACTIVE-KAFO.pdf";
		break;

	default:
		bkgFile = bkgPath + "ACTIVE-SMOAFO.pdf";
		break;
}