/*== QP O-Form Auto-Email v0.9 =================================================================

	Library:  Send-Portal-Forms
	Function: QPoForm
	Version:  Send-Portal-Forms.QPoForm.v0.9.0.cs

	Created: 02/21/2023
		-Kevin Veldman <KVeldman@Hanger.com>
	
	Assemblies:
		CodaBears.Epicor.QuickPrint.dll
		PdfPrintingNet.dll
		PdfSharp.dll

	Using (custom code): 
		using CodaBears.Epicor.QuickPrint;
		using PdfSharp.Drawing;
		using PdfSharp.Pdf.IO;

	Info:
		Invoke from OrderDtl DataDirective on added Line if OrderHed.EntryPerson=="PARTTRAP";
		Pass in OrderNum + OrderLine + BasePartNum + PTUserEmail_c; Send QP O-Form via SMTP;
================================================================================================*/

//__ Set Parameters for Report ___________________________________________
//Use the Company_UD fields to replace these strings;
	var SS = Db.Company.Where(x => x.Name.Contains("SURESTEP")).FirstOrDefault();
	var ConnectionString = "Server=EPAPP02;Database=DienenProdSP;Integrated Security=True;";
	var ssrsPrefix = "/E10Prod/Sales Management/";
	var company = "SS";
	var ssrsServer = "EPAPP02";
	var bkgFile = string.Empty;
	var rptName = string.Empty;

	switch ( sBasePartNum ) {

		case "webTLSO":
			bkgFile = "ACTIVE-TLSO.pdf";
			rptName = "OForm TLSO_Active";
			break;

		case "webHEKO":
			bkgFile = "ACTIVE-HEKO.pdf";
			rptName = "OForm HEKO_Active";
			break;

		case "webKAFO":
			bkgFile = "ACTIVE-KAFO.pdf";
			rptName = "OForm KAFO_Active";
			break;

		default:
			bkgFile = "ACTIVE-SMOAFO.pdf";
			rptName = "OForm SMO-AFO_Active";
			break;
	}


	var rptPath = string.Format( "{0}{1}", ssrsPrefix, rptName );
	var serviceAddress = string.Format("http://{0}/reportserver/ReportExecution2005.asmx", ssrsServer);

	var savePath = @"C:\EpicorData\Companies\SS\Processes\kevinv\";
	var saveName = string.Format(@"OForm_{0}_{1:yyyyMMdd_HHmmss}.pdf", sOrderNum, DateTime.Now);
	var fileName = savePath+saveName;

	var parmList = new System.Collections.Specialized.OrderedDictionary();
	parmList.Add( "Company"  , "SS");
	parmList.Add( "OrderNum" , sOrderNum);
	parmList.Add( "OrderLine", sOrderLine);
//________________________________________________________________________

//__ Create + save Report_________________________________________________
	var reportBytes = CodaBears.Epicor.QuickPrint.Reports.GetSSRSReport (serviceAddress, rptPath, parmList);

	System.IO.File.WriteAllBytes(fileName, reportBytes);

	var bkgPath = string.Format ( @"\\Epapp02\oform_pdf-do_not_change"  );
	var watermarkFile = string.Format( @"{0}\{1}", bkgPath, bkgFile );

	var watermarkImage = XImage.FromFile ( watermarkFile );
	var source = PdfReader.Open ( fileName );
	var gfx = XGraphics.FromPdfPage ( source.Pages[0], XGraphicsPdfPageOptions.Prepend );
	var width = watermarkImage.PixelWidth * 72 / watermarkImage.HorizontalResolution;
	var height = watermarkImage.PixelHeight * 72 / watermarkImage.VerticalResolution;
	gfx.DrawImage ( watermarkImage, 0, -180, width, height );

	source.Save ( fileName );
//________________________________________________________________________

//__ Set Email Parameters ________________________________________________
	var SendTo = "kevinv@dienen.com; kveldman@hanger.com"; // sSendToEmail
	var SendCC = string.Empty;
	var SendBCC = string.Empty;
	var Subject = "Email Sent";

	System.Text.StringBuilder body = new System.Text.StringBuilder();

	body.Append("Email Body").AppendLine().AppendLine();
	body.Append("Best Regards,").AppendLine();
	body.Append("Epicor Function Test");

	var mailer = this.GetMailer(async:true);
	var message = new Ice.Mail.SmtpMail();

	message.SetFrom("noreply@surestep.net");
	message.SetTo(SendTo);
	message.SetCC(SendCC);
	message.SetBcc(SendBCC);
	message.SetSubject(Subject);
	message.SetBody(body.ToString());

	Dictionary<string, string> emailAttachments = new Dictionary <string,string>();

	string attachmentName = String.Format("OFormTest-{0}-{1}.pdf",sOrderNum,sOrderLine);

	emailAttachments.Add(attachmentName, fileName); 

	mailer.Send(message, emailAttachments);

	System.IO.File.Delete(fileName); // Delete Temporary Report PDF
//________________________________________________________________________





var od = ttOrderDtl.Where(x => x.RowMod=="A").FirstOrDefault();

if ( od != null ) {

	This.InvokeFunction()

}