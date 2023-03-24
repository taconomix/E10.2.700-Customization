/*== Call Automated Email Functions for Web Orders ===========================

	Library:  Send-Portal-Forms
	Function: AutoSend-WebForms
	Version:  v1.2.0

		Request Parameters: 1
			QueryName: System.String

		Response Parameters: None

	Created: 02/27/2023
	Changed: 03/08/2023

		--Kevin Veldman <KVeldman@Hanger.com>
============================================================================*/

Action<string> addLog = s => { // Write to TaskLog for Tracing
	this.EfxLib.libAutoRpt.AddLog_SysMonitor( s, this.LibraryID.ToLower() ); 
};

var context = Ice.Services.ContextFactory.CreateContext<ErpContext>();
using ( var svc = Ice.Assemblies.ServiceRenderer.GetService<Ice.Contracts.DynamicQuerySvcContract>(context) ) {

	var dsQuery = svc.GetByID( QueryName );  // BAQ Name for Orders

	if ( dsQuery==null ) {

		addLog( "Unable to get query " + QueryName );

	} else {

		// Call BAQ, create O-form for each row + 1 Order Confirmation per Order
		var p = svc.GetQueryExecutionParameters( dsQuery );
		var results = svc.Execute( dsQuery, p ); 

		if ( results.Tables["Results"].Rows.Count > 0 ) {

			//__ Return new PDF Report Path, Name ________________________
				Func<int,int,string> getFileName = (oNum,oLn) => {

					string savePath = @"C:\EpicorData\Companies\SS\Temp\webforms\";
					return savePath + this.EfxLib.libAutoRpt.getFileName( oNum, oLn );
				};

			//__ List for new PDFs _______________________________________

				List<string> AttachmentFiles = new List<string>();
			//____________________________________________________________
			
			addLog( "Ready to create reports" );


			foreach ( DataRow r in results.Tables["Results"].Rows ) {
				//-- This is where the magic happens --//

				// Values from BAQ Fields
				var orderNum  = (int)r["OrderDtl_OrderNum"   ];
				var orderLine = (int)r["OrderDtl_OrderLine"  ];
				var basePN = (string)r["OrderDtl_BasePartNum"];

				// Fix PartTrap OrderDate Bug
				if ( BpmFunc.Today() > (DateTime)r["OrderHed_OrderDate"] ) {
					this.EfxLib.libOrderCfg.fixOrderDate( orderNum );
						addLog( "Updated OrderDate on SO#" + orderNum.ToString() );
				}
				

				//__ Generate, Save O-Form ___________________________________
					
					// Report Parameters
					string ofName   = getFileName( orderNum, orderLine );
					string ofReport = this.EfxLib.libAutoRpt.getRptOF( basePN );
					string ofImage  = this.EfxLib.libAutoRpt.getBkgOF( basePN );
					
					// Add New PDF Name to list of Attachments
					AttachmentFiles.Add( ofName );
					
					// Call saveReport Function
					this.EfxLib.libAutoRpt.saveReport( orderNum, orderLine, ofName, ofReport, ofImage );

					addLog(string.Format( "O-form {0}-{1} Created", orderNum, orderLine) ); 
				//____________________________________________________________


				if ( (int)r["Calculated_MaxOL"]==orderLine ) { 
					//-- Get OrderConf, send email on last OrderLine --//

					//__ Generate, Save O-Form ___________________________________
					
						// OrderConf File Name
						string soaName = getFileName( orderNum, 0 );
						
						// Add new OrderConf PDF Name to list of Attachments
						AttachmentFiles.Add( soaName );

						// Call saveReport Function
						this.EfxLib.libAutoRpt.saveReport( orderNum, orderLine, soaName, "SOAck_New", "" );

						addLog( string.Format("OrderConf {0} created", orderNum) );
					//____________________________________________________________


					//__ Create, send email w/ attachments _______________________
						// Old SmtpClient & MailMessage classes
							// --Necessary to specify sendFrom email different than Company SMTP Settings
					
						var from = "weborders@surestep.net";
						var dest = (string)r["OrderHed_PTUserEmail_c"];
						var smtp = new SmtpClient( "smtp.office365.com", 587 );

						if ( dest == "" ) dest = "kevinv@surestep.net";

						smtp.UseDefaultCredentials = false;
						smtp.EnableSsl = true;
						smtp.Credentials = new NetworkCredential( from, this.EfxLib.libAutoRpt.getSmtpCreds("SMTP") );

						var message = new MailMessage( from, dest );
						//message.Bcc.Add(new MailAddress("kevinv@surestep.net"));
						message.Subject = string.Format( "Surestep Order #{0}", orderNum );
						message.Body = this.EfxLib.libAutoRpt.getMsgBody( orderNum );

						// Attach all files in AttachmentFiles List
						foreach ( string fileName in AttachmentFiles ) {
							message.Attachments.Add( new Attachment(fileName) );
						}

						// Send email
						smtp.Send( message );
							addLog( string.Format("{2} attachments for Order {0} emailed to {1}.", orderNum, dest, AttachmentFiles.Count) );

						// Dispose of mailMessage - otherwise new files are locked
						message.Dispose();
					//____________________________________________________________


					// Delete this order's attachment files
					foreach ( string fileName in AttachmentFiles ) {
						System.IO.File.Delete( fileName );
					}

					// Empty AttachmentFiles list, ready for next order
					AttachmentFiles.Clear();

					// Uncheck "AutoPrintReady" to remove from BAQ
					this.EfxLib.libOrderCfg.clearAPReady( orderNum );
						addLog( "Files deleted. Attachments List cleared." );
				}
			}
		} else {
			addLog( string.Format("Query {0} returned 0 Rows. End Function.", QueryName) );
		}
	}
}

/*== CHANGE LOG ==============================================================

	03/03/2023: Add line to fix PartTrap OrderDate Bug;
	03/07/2023: Removed kevinv@dienen.com from Blind CC;
	03/07/2023: Move Report PDF Creation to separate library function;
	03/07/2023: Use clearAPReady function from libOrderCfg;

============================================================================*/

	/*__ Added Usings ____________________________________________________

		using CodaBears.Epicor.QuickPrint;
		using PdfSharp.Drawing;
		using PdfSharp.Pdf.IO;
		using System.Net;
		using System.Net.Mail;
	____________________________________________________________________*/

	/*__ Reference _______________________________________________________

		Call BAQ and get list from results:
			https://www.epiusers.help/t/epicor-functions-and-baqs/83803/21

		logging to event log reference: 
			https://www.epiusers.help/t/bpm-logging-snippet/93004

		logging to trace file: 
			https://www.epiusers.help/t/how-to-write-your-own-trace-logs/44613/3 

		QuickPrint and PdfSharp usage:
			https://github.com/taconomix/E10-SS-FormCustomizations/blob/main/Active-SalesOrderEntry-20221102.cs
	____________________________________________________________________*/