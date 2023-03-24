/*== Save Report for AutoSend-WebForms =======================================

	Library:  libAutoRpt
	Function: saveReport
	Version:  v1.0.0

		Request Parameters: 1
			orderNum:  System.Int32
			orderLine: System.Int32
			pdfName:   System.String
			rptFile:   System.String
			rptImage:  System.String

		Response Parameters: None

	Created: 02/27/2023
	Changed: 03/03/2023

		--Kevin Veldman <KVeldman@Hanger.com>
============================================================================*/

var ssrsPrefix = Db.Company.FirstOrDefault(x => x.Name.Contains("SURESTEP"))["ssrsPrefix_c"];
var svcAddress = "http://EPAPP02/reportserver/ReportExecution2005.asmx";
bool isOForm = !string.IsNullOrEmpty( rptImage );

var rptPath = string.Format( "{0}{1}{2}", ssrsPrefix, "Sales Management/", rptFile );

var rptParms = new System.Collections.Specialized.OrderedDictionary();
	rptParms.Add( "Company", "SS" );
	rptParms.Add( "OrderNum", orderNum.ToString() );
	if (isOForm) {
		rptParms.Add( "OrderLine", orderLine.ToString() );
	}

var rptBytes = CodaBears.Epicor.QuickPrint.Reports.GetSSRSReport(svcAddress, rptPath, rptParms);

System.IO.File.WriteAllBytes( pdfName, rptBytes );

if ( isOForm ) {

	// Add watermark/background to O-Form (Report has values only)
	var ofImage = XImage.FromFile ( rptImage );
	var source  = PdfReader.Open ( pdfName );
	var gfx     = XGraphics.FromPdfPage ( source.Pages[0], XGraphicsPdfPageOptions.Prepend );
	var width   = ofImage.PixelWidth * 72 / ofImage.HorizontalResolution;
	var height  = ofImage.PixelHeight * 72 / ofImage.VerticalResolution;
	gfx.DrawImage ( ofImage, 0, -180, width, height );

	// Save O-Form with Watermark
	source.Save ( pdfName );
}

/*== CHANGE LOG ==============================================================

	03/07/2023: 
	
============================================================================*/

	/*__ Added Usings ____________________________________________________

		using CodaBears.Epicor.QuickPrint;
		using PdfSharp.Drawing;
		using PdfSharp.Pdf.IO;
	____________________________________________________________________*/

	/*__ Reference _______________________________________________________

		QuickPrint and PdfSharp usage:
			https://github.com/taconomix/E10-SS-FormCustomizations/blob/main/Active-SalesOrderEntry-20221102.cs
	____________________________________________________________________*/