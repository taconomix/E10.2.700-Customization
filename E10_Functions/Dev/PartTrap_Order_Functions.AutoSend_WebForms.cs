/*== Call Automated Email Functions for Web Orders ===========================

	Library:  Send-Portal-Forms
	Function: AutoSend_WebForms

	Created: 02/27/2023
	Changed: 03/01/2023

		Kevin Veldman <KVeldman@Hanger.com>
============================================================================*/

Action<string> addLog = s => { // Write to TaskLog for Tracing
	this.EfxLib.libAutoRpt.AddLog_SysMonitor(s,this.LibraryID.ToLower()); 
};

var context = Ice.Services.ContextFactory.CreateContext<ErpContext>();
using ( var svc = Ice.Assemblies.ServiceRenderer.GetService<Ice.Contracts.DynamicQuerySvcContract>(context) ) {

	var dsQuery = svc.GetByID (QueryName);  // BAQ Name for Orders

	if ( dsQuery == null ) {

		addLog("Unable to get query " + QueryName);

	} else {

		// Call BAQ, create O-form for each row + 1 Order Confirmation per Order
		var p = svc.GetQueryExecutionParameters (dsQuery);
		var results = svc.Execute (dsQuery, p); 

		if ( results.Tables["Results"].Rows.Count > 0 ) { 

			// Get company data
			var SS = Db.Company.Where(x => x.Name.Contains("SURESTEP")).FirstOrDefault();

			// Report calling parameters for current server/company
			var ssrsPrefix = (string)SS["ssrsPrefix_c"] + "Sales Management/"; 
			var svcAddress = "http://EPAPP02/reportserver/ReportExecution2005.asmx";
			var savePath = @"C:\EpicorData\Companies\SS\Temp\webforms\";
			
			// Functions to get Report|Attachment string values (libAutoRpt)
			Func<string,string> getRptPath = s => string.Format("{0}{1}", ssrsPrefix, s);
			Func<string,string> getOFRpt   = s => this.EfxLib.libAutoRpt.getRptOF(s);
			Func<string,string> getOFBkg   = s => this.EfxLib.libAutoRpt.getBkgOF(s);
			Func<int,int,string> getFileName = (oNum,oLn) => this.EfxLib.libAutoRpt.getFileName(oNum,oLn);

			// List<string> to hold attachment file names. Emptied after each email send.
			List<string> AttachmentFiles = new List<string>();

			addLog("Ready to create reports");

			foreach ( DataRow r in results.Tables["Results"].Rows ) {
				// This is where the magic happens

				// Order & Order Line values from BAQ Fields
				var orderNum  = (int)r["OrderDtl_OrderNum"   ];
				var orderLine = (int)r["OrderDtl_OrderLine"  ];
				var basePN = (string)r["OrderDtl_BasePartNum"];

				// Correct OrderDate bug from PartTrap setting OrderDate to previous day.
				this.EfxLib.libOrderCfg.fixOrderDate(orderNum);
				
				// File name for current row O-Form
				AttachmentFiles.Add(string.Format(@"{0}{1}",savePath,getFileName(orderNum, orderLine)));
				
				// O-Form report parameters
				var ofParms = new System.Collections.Specialized.OrderedDictionary();
				ofParms.Add( "Company"  , "SS");
				ofParms.Add( "OrderNum" , orderNum.ToString());
				ofParms.Add( "OrderLine", orderLine.ToString());

				var ofBytes = CodaBears.Epicor.QuickPrint.Reports.GetSSRSReport(svcAddress, getRptPath(getOFRpt(basePN)), ofParms);

				// Save O-Form
				System.IO.File.WriteAllBytes(AttachmentFiles.LastOrDefault(), ofBytes);

				// Add watermark/background to O-Form (Report has values only)
				var oformImage = XImage.FromFile ( getOFBkg(basePN) );
				var source = PdfReader.Open ( AttachmentFiles.LastOrDefault() );
				var gfx = XGraphics.FromPdfPage ( source.Pages[0], XGraphicsPdfPageOptions.Prepend );
				var width = oformImage.PixelWidth * 72 / oformImage.HorizontalResolution;
				var height = oformImage.PixelHeight * 72 / oformImage.VerticalResolution;
				gfx.DrawImage ( oformImage, 0, -180, width, height );

				source.Save ( AttachmentFiles.LastOrDefault() );
					addLog(string.Format("O-form {0}-{1} Created",orderNum, orderLine)); 


				// Get OrderConf, send email on last line for order
				if ( (int)r["Calculated_MaxOL"] == orderLine ) { 
						
					// File name for Order Confirmation
					AttachmentFiles.Add(string.Format(@"{0}{1}",savePath,getFileName(orderNum, 0)));

					// Order Confirmation report parameters
					var soaParms = new System.Collections.Specialized.OrderedDictionary();
						soaParms.Add("Company", "SS");
						soaParms.Add("OrderNum", orderNum.ToString());

					var soaBytes = CodaBears.Epicor.QuickPrint.Reports.GetSSRSReport(svcAddress, getRptPath("SOAck_New"), soaParms);

					// Save Order Confirmation
					System.IO.File.WriteAllBytes(AttachmentFiles.LastOrDefault(), soaBytes);
						addLog(string.Format("OrderConf {0} created",orderNum));

					// Create and send email with attachments
					// Uses old SmtpClient and MailMessage classes, necessary to use different Smtp than the one in company configuration
					var from = "weborders@surestep.net";
					var dest = (string)r["OrderHed_PTUserEmail_c"];
					var smtp  = new SmtpClient("smtp.office365.com", 587);

					smtp.UseDefaultCredentials = false;
					smtp.EnableSsl = true;
					smtp.Credentials = new NetworkCredential(from, this.EfxLib.libAutoRpt.getSmtpCreds("SMTP"));

					var message = new MailMessage(from, dest);
					message.Bcc.Add(new MailAddress("kevinv@surestep.net"));
					message.Subject = string.Format("Surestep Order #{0}", orderNum);
					message.Body = this.EfxLib.libAutoRpt.getMsgBody(orderNum);

					// Attach all files in AttachmentFiles List
					foreach ( string fileName in AttachmentFiles ) {
						message.Attachments.Add(new Attachment(fileName));
					}

					// Send email
					smtp.Send(message);
						addLog(string.Format("Order {0} Email sent to {1} with {2} attachments.", orderNum, (string)r["OrderHed_PTUserEmail_c"], AttachmentFiles.Count));

					// Dispose of mailMessage - otherwise new files are locked
					message.Dispose();

					// Delete this order's attachment files
					foreach ( string fileName in AttachmentFiles ) {
						System.IO.File.Delete( fileName );
					}

					// Empty AttachmentFiles list, ready for next order
					AttachmentFiles.Clear();

					// Uncheck "AutoPrintReady" to remove from BAQ
					this.EfxLib.libAutoRpt.clearAPReady(orderNum);
						addLog("Files deleted. Attachments List cleared.");
				}
			}
		}
	}
}


	/*------------------------------------------------------------------------
		Call BAQ and get list from results:
			https://www.epiusers.help/t/epicor-functions-and-baqs/83803/21

		logging to event log reference: 
			https://www.epiusers.help/t/bpm-logging-snippet/93004

		logging to trace file: 
			https://www.epiusers.help/t/how-to-write-your-own-trace-logs/44613/3 
	-------------------------------------------------------------------------*/