			

// QuickPrint Params
var ssrsPrefix = "/E10Prod/Sales Management/"; 
var svcAddress = "http://EPAPP02/reportserver/ReportExecution2005.asmx";
var savePath = @"L:\";
var rptSMO = "OForm SMO-AFO_DEV";

string myFile = string.Format ( @"{0}{1}-{2}.pdf", savePath, orderNum, orderLine);
string wmFile = string.Format ( @"\\Epapp02\oform_pdf-do_not_change\ACTIVE-SMOAFO.pdf" );

// O-Form report parameters
var ofParms = new System.Collections.Specialized.OrderedDictionary();
ofParms.Add( "Company"  , "SS");
ofParms.Add( "OrderNum" , orderNum.ToString());
ofParms.Add( "OrderLine", orderLine.ToString());

var ofBytes = CodaBears.Epicor.QuickPrint.Reports.GetSSRSReport(svcAddress, (ssrsPrefix + rptSMO), ofParms);

// Save O-Form
System.IO.File.WriteAllBytes(myFile, ofBytes);

// Add watermark/background to O-Form (Report has values only)
var oformImage = XImage.FromFile ( wmFile );
var source = PdfReader.Open ( myFile );
var gfx = XGraphics.FromPdfPage ( source.Pages[0], XGraphicsPdfPageOptions.Prepend );
var width = oformImage.PixelWidth * 72 / oformImage.HorizontalResolution;
var height = oformImage.PixelHeight * 72 / oformImage.VerticalResolution;
gfx.DrawImage ( oformImage, 0, -180, width, height );

source.Save ( myFile );