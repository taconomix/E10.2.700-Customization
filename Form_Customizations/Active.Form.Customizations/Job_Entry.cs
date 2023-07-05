/*============================================================================

	Custom code for Job Entry form

	Created: 07/26/2022
	Author:  Fred Zelhart
	Info:    Create custom JobTraveler/O-Form using QuickPrint DLL,
			 Print to PDF or open Outlook item as PDF Attachment. 

	Changed: 03/28/2023
	By:      Kevin Veldman

============================================================================*/

extern alias Erp_Contracts_BO_Company;
extern alias Erp_Contracts_BO_Project;
extern alias Erp_Contracts_BO_Part;
extern alias Microsoft_Office_Interop_Outlook;

using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using Erp.Adapters;
using Erp.UI;
using Ice.Lib;
using Ice.Adapters;
using Ice.Core;
using Ice.Lib.Customization;
using Ice.Lib.ExtendedProps;
using Ice.Lib.Framework;
using Ice.Lib.Searches;
using Ice.UI.FormFunctions;

using Infragistics.Win;
using Infragistics.Win.UltraWinToolbars;
using PdfSharp.Drawing;
using PdfSharp.Pdf.IO;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using Outlook = Microsoft.Office.Interop.Outlook;

public class Script
{
	// ** Wizard Insert Location - Do Not Remove 'Begin/End Wizard Added Module Level Variables' Comments! **
	// Begin Wizard Added Module Level Variables **

	// End Wizard Added Module Level Variables **

	// Add Custom Module Level Variables Here **
	string ConnectionString = string.Empty;
	string ssrsPrefix = string.Empty;
	string company = string.Empty;
	string filename = string.Empty;
	string ssrsServer = string.Empty;
	Session session;

	public void InitializeCustomCode()
	{
		// ** Wizard Insert Location - Do not delete 'Begin/End Wizard Added Variable Initialization' lines **
		// Begin Wizard Added Variable Initialization

		this.baseToolbarsManager.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.baseToolbarsManager_ToolClick);
		// End Wizard Added Variable Initialization

		// Begin Wizard Added Custom Method Calls

		// End Wizard Added Custom Method Calls
	}

	public void DestroyCustomCode()
	{
		// ** Wizard Insert Location - Do not delete 'Begin/End Wizard Added Object Disposal' lines **
		// Begin Wizard Added Object Disposal

		this.baseToolbarsManager.ToolClick -= new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.baseToolbarsManager_ToolClick);
		// End Wizard Added Object Disposal

		// Begin Custom Code Disposal

		// End Custom Code Disposal
	}

	private void JobEntryForm_Load(object sender, EventArgs args) {

		/* email buttons removed 2023/03/28

		// Email Traveler Button
		var button = new ButtonTool("btnEmailTrav");
		baseToolbarsManager.Tools.Add(button);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("btnEmailTrav");
		baseToolbarsManager.Tools["btnEmailTrav"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["btnEmailTrav"].SharedProps.Caption = "Email JobTraveler";
		baseToolbarsManager.Tools["btnEmailTrav"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(CodaBears.Epicor.QuickPrint.Properties.Resources.cbi_email.Handle);
		
		// O-Form Email Button 
		var watermarkEmailButton = new ButtonTool("btnEmailOF");
		baseToolbarsManager.Tools.Add(watermarkEmailButton);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("btnEmailOF");
		baseToolbarsManager.Tools["btnEmailOF"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["btnEmailOF"].SharedProps.Caption = "Email O-Form";
		baseToolbarsManager.Tools["btnEmailOF"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(CodaBears.Epicor.QuickPrint.Properties.Resources.cbi_email_blue.Handle);
		*/


		// print Job Traveler button
		var jtPrintButton = new ButtonTool("btnPrintTrav");
		baseToolbarsManager.Tools.Add(jtPrintButton);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("btnPrintTrav");
		baseToolbarsManager.Tools["btnPrintTrav"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["btnPrintTrav"].SharedProps.Caption = "Print Job Traveler";
		baseToolbarsManager.Tools["btnPrintTrav"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(CodaBears.Epicor.QuickPrint.Properties.Resources.print.Handle);

		// print O-Form button
		var ofPrintButton = new ButtonTool("btnPrintOF");
		baseToolbarsManager.Tools.Add(ofPrintButton);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("btnPrintOF");
		baseToolbarsManager.Tools["btnPrintOF"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["btnPrintOF"].SharedProps.Caption = "Print O-Form";
		baseToolbarsManager.Tools["btnPrintOF"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(CodaBears.Epicor.QuickPrint.Properties.Resources.print_blue.Handle);

		// Print Traveler and O-Form Button
		var doublePrintButton = new ButtonTool("btnPrintAll");
		baseToolbarsManager.Tools.Add(doublePrintButton);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("btnPrintAll");
		baseToolbarsManager.Tools["btnPrintAll"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["btnPrintAll"].SharedProps.Caption = "Print Traveler && O-Form";
		baseToolbarsManager.Tools["btnPrintAll"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(CodaBears.Epicor.QuickPrint.Properties.Resources._2.Handle);

		// get ssrs data
		session = (Ice.Core.Session)JobEntryForm.Session;
		company = session.CompanyID;
		using (var svc = WCFServiceSupport.CreateImpl<Erp.Proxy.BO.CompanyImpl>(session, Epicor.ServiceModel.Channels.ImplBase<Erp.Contracts.CompanySvcContract>.UriPath)) {
			var companyDs = svc.GetByID(session.CompanyID);
			if (companyDs != null) {
				ssrsServer = (string)companyDs.Tables[0].Rows[0]["ssrsServer_c"];
				ssrsPrefix = (string)companyDs.Tables[0].Rows[0]["ssrsPrefix_c"];
				ConnectionString = (string)companyDs.Tables[0].Rows[0]["sqlConnectionString_c"];
			}
		}
	
		if (ssrsPrefix.Length == 0)  MessageBox.Show ("ssrsPrefix cannot be blank or null on the company table.");
		if (ssrsServer.Length == 0)  MessageBox.Show ("ssrs Server cannot be blank or null on the company table.");
		if (ConnectionString.Length == 0)  MessageBox.Show ("sqlConnectionString cannot be blank or null on the company table.");
	}

	bool appendpath = true;
	private void baseToolbarsManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs args) {

		/* Old
			var isOForm = args.Tool.Key == "btnPrintOF" || args.Tool.Key == "btnEmailOF";
			var printButton = args.Tool.Key == "btnPrintOF" || args.Tool.Key == "btnPrintTrav";
			var emailButton = args.Tool.Key == "btnEmailOF" || args.Tool.Key == "btnEmailTrav"; 
		*/

		var printOF = args.Tool.Key == "btnPrintOF";
		var printJT = args.Tool.Key == "btnPrintTrav";
		var printBoth = args.Tool.Key == "btnPrintAll"; 
		var printAny = printOF || printJT || printBoth;
		
		
		//if ( printButton || emailButton ) { //email removed
		if ( printAny ) {

			Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
			
			var server = ssrsServer; // variables to change for each report

			// Set to false for single folder, true otherwise
			if ( appendpath ) {
				ssrsPrefix += "Sales Management/";
				appendpath = false;
			}
			
			var txtJobNumber = (EpiTextBox)csm.GetNativeControlReference ("46567b2e-6bc0-4967-be35-a0ec6843838f");
			var jobNum = txtJobNumber.Text;	

			if (string.IsNullOrEmpty(jobNum)) {
				MessageBox.Show ("Please enter a Job Number");
				return;
			}

			// get the Order Number, OrderLine, Watermark filename
			int orderNum = 0;
			int orderLine = 0;
			var bkgName = string.Empty;
			using (var conn = new SqlConnection (ConnectionString)) {
				var cmd = new SqlCommand();
				cmd.Connection = conn;
				cmd.CommandText = "OrderNumLineFromJobNum";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add ("@Company", SqlDbType.Char).Value = company;
				cmd.Parameters.Add ("@JobNum", SqlDbType.Char).Value = jobNum;
				conn.Open();
				using (var dr = cmd.ExecuteReader()) {

					if (dr != null && dr.Read()) {
						if (!dr.IsDBNull (dr.GetOrdinal ("OrderNum")))
							orderNum = dr.GetInt32 (dr.GetOrdinal ("OrderNum"));
						
						if (!dr.IsDBNull (dr.GetOrdinal ("OrderLine")))
							orderLine = dr.GetInt32 (dr.GetOrdinal ("OrderLine"));

						if (!dr.IsDBNull (dr.GetOrdinal ("oForm")))
							bkgName = dr.GetString (dr.GetOrdinal ("oForm")) + ".pdf";
					}
				}
			}
			

			if ( (printBoth || printOF) && (orderNum <= 0 || orderLine <= 0) ) {
				MessageBox.Show (string.Format ("No order was found for job {0}", jobNum), "ToolClick");
				return;
			}

			// set the parameter values and filename
			var ofFileName = string.Format(@"{0}{1}_{2}_{3:yyyyMMdd_HHmmss}.pdf", System.IO.Path.GetTempPath(), "OForm", orderNum, DateTime.Now);
			var jtFileName = string.Format(@"{0}{1}_{2}_{3:yyyyMMdd_HHmmss}.pdf", System.IO.Path.GetTempPath(), "JobTrav", jobNum, DateTime.Now);
				//string reportNamePrefix = printBoth? "OForm": "JobTrav";
				//var fileName = string.Format(@"{0}{1}_{2}_{3:yyyyMMdd_HHmmss}.pdf", System.IO.Path.GetTempPath(), reportNamePrefix, orderNum, DateTime.Now);

			//may need to be https depending on the client setup
			var serviceAddress = string.Format("http://{0}/reportserver/ReportExecution2005.asmx", server);

			// create the parameters
			var session = (Ice.Core.Session)JobEntryForm.Session;

			var parmListJT = new System.Collections.Specialized.OrderedDictionary();
				parmListJT.Add("Company", session.CompanyID);
				parmListJT.Add("JobNum", jobNum);

			var parmListOF = new System.Collections.Specialized.OrderedDictionary();
				parmListOF.Add("Company", session.CompanyID);
				parmListOF.Add("OrderNum", orderNum.ToString());
				parmListOF.Add("OrderLine", orderLine.ToString());

			// execute the report
			var rptNameJT = "JobTravQP";
			var rptNameOF = string.Empty;
			switch (bkgName) {
				case "ACTIVE-TLSO.pdf":
					rptNameOF = "OForm TLSO_Active";
					break;
				case "ACTIVE-HEKO.pdf":
					rptNameOF = "OForm HEKO_Active";
					break;
				case "ACTIVE-KAFO.pdf":
					rptNameOF = "OForm KAFO_Active";
					break;
				default:
					rptNameOF = "OForm SMO-AFO_Active";
					break;
			}

			var reportPathJT = string.Format ("{0}{1}", ssrsPrefix, rptNameJT);
			var reportPathOF = string.Format ("{0}{1}", ssrsPrefix, rptNameOF);

			oTrans.PushStatusText ("Executing Report", true);
			var reportBytesJT = CodaBears.Epicor.QuickPrint.Reports.GetSSRSReport (serviceAddress, reportPathJT, parmListJT);
			var reportBytesOF = CodaBears.Epicor.QuickPrint.Reports.GetSSRSReport (serviceAddress, reportPathOF, parmListOF);
			
			if ( reportBytesJT == null && reportBytesOF == null ) {
				MessageBox.Show ("No report was returned.");

			} else {

				if ( printJT || printBoth ) System.IO.File.WriteAllBytes(jtFileName, reportBytesJT);
				
				if ( printOF || printBoth ) {
					
					System.IO.File.WriteAllBytes(ofFileName, reportBytesOF);	
					
					string bkgFile = string.Format(@"\\Epapp02\oform_pdf-do_not_change\{0}", bkgName);
					
					var bkgImage = XImage.FromFile(bkgFile);
					var source = PdfReader.Open (ofFileName);
					var gfx = XGraphics.FromPdfPage (source.Pages[0], XGraphicsPdfPageOptions.Prepend);
					var width = bkgImage.PixelWidth * 72 / bkgImage.HorizontalResolution;
					var height = bkgImage.PixelHeight * 72 / bkgImage.VerticalResolution;
					gfx.DrawImage (bkgImage, 0, -180, width, height);
					
					oTrans.PushStatusText ("saving image with watermark", true);
					source.Save (ofFileName);
				}

				if ( printJT || printBoth ) {

					Process jtProce = new Process();
					jtProce.StartInfo.FileName = jtFileName;
					jtProce.Start();

					setTravFields();
				}


				if ( printOF || printBoth ) {

					Process ofProce = new Process();
					ofProce.StartInfo.FileName = ofFileName;
					ofProce.Start();
				}

				
				
				/* Removed email buttons, options are print Traveler or Print OForm and Traveler

				if ( printButton ) {	

					Process jtProce = new Process();
					jtProce.StartInfo.FileName = jtFileName;
					jtProce.Start();

					if ( printBoth ) {
						Process ofProce = new Process();
						ofProce.StartInfo.FileName = ofFileName;
						ofProce.Start();
					}

					setTravFields();

				} else {
					oTrans.PushStatusText ("Get customer's email", true);
					string emailTo = "";
					string custName = "";
					using (var conn = new SqlConnection (ConnectionString)) {
						var cmd = new SqlCommand();
						cmd.Connection = conn;
						cmd.CommandText = "ReportSalesOrderAck";
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.Add("@Company", SqlDbType.Char).Value = company;
						cmd.Parameters.Add("@OrderNum", SqlDbType.Int).Value = orderNum;
						conn.Open();
						using(var dr = cmd.ExecuteReader()) {
							if (dr != null && dr.Read()) {
								if (!string.IsNullOrEmpty (dr.GetString(dr.GetOrdinal("OFEmail")))) {

									emailTo = dr.GetString (dr.GetOrdinal("OFEmail"));

								} else if (!dr.IsDBNull (dr.GetOrdinal("ShipToEmail"))) {

									emailTo = dr.GetString (dr.GetOrdinal ("ShipToEmail"));

								} else if(!dr.IsDBNull (dr.GetOrdinal ("FaxEmail"))) {

									emailTo = dr.GetString (dr.GetOrdinal ("FaxEmail")) + "@metrofax.com";
								}								

								if (!dr.IsDBNull(dr.GetOrdinal("ShipToName"))) custName = dr.GetString (dr.GetOrdinal ("ShipToName"));
								if (dr != null && !dr.IsClosed) dr.Close();
							}
						}
					}
						
					oTrans.PushStatusText ("Set up email parameters", true);

					string formPreface = printBoth? "O-Form": "Confirmation";
					var emailSubject = string.Format("Surestep, LLC – {1} for Order #{0} Attached | CONFIDENTIAL/PROTECTED HEALTH INFORMATION ATTACHED", orderNum, formPreface);

					string formInfo = printBoth? "O-Form": "order details";
					var emailBody = string.Format ("<p>Surestep Sales Order #{0} has been received and processed. Please find attached the {1} for this order. Thank you for your order – we appreciate it very much!</p><p>Sincerely,</p><p>Customer Service Department</p><p>orders@surestep.net</p><p>Important Notice:</p><p>This message is intended only for the use of the individual or entity to which it is addressed. The documents in this e-mail or facsimile transmission may contain confidential health information that is privileged and legally protected from disclosure by the Health Insurance Portability and Accountability Act (HIPAA). This information is intended only for the use of the individual or entity named above. If you are not the intended recipient, you are hereby notified that reading, disseminating, disclosing, distributing, copying, acting upon or otherwise using the information contained in this facsimile is strictly prohibited. If you have received this information in error, please notify the sender.</p>", orderNum,formInfo);

					oTrans.PushStatusText ("Set up email message", true);

					// set up the email message (using outlook); saves to sent items, but requires file IO
					var application = new Outlook.Application();
					var message = application.CreateItem(Outlook.OlItemType.olMailItem) as Outlook.MailItem;
					Outlook.Account account = application.Session.Accounts["noreply@surestep.net"];
					if (message != null) {
						// set up the email parameters
						if(account != null) message.SendUsingAccount = account;
						message.To = emailTo;
						message.Subject = emailSubject;
						
						// outlook requires there to be a physical file.
						//System.IO.File.WriteAllBytes(filename, reportBytes);
						message.Attachments.Add(filename);
						System.IO.File.Delete(filename);

						message.ReadReceiptRequested = true;
						message.Recipients.ResolveAll();
						message.Display();
						
						message.HTMLBody = emailBody + message.HTMLBody;
						oTrans.PushStatusText(string.Format ("Created Email to {0}", emailTo), true);
					}
				} */
			}
			Cursor.Current = System.Windows.Forms.Cursors.Default;
		}
	}

	public void setTravFields() { //Remove TravelerLastPrinted, now in BPM.

		var massPrint = (EpiCheckBox)csm.GetNativeControlReference("e56504ae-c608-4841-962a-5b540fdae4d3");
		massPrint.Checked = false;
		oTrans.Update();
	}
}


/*== CHANGE LOG =======================================================================

	10/12/2022: +Using...Outlook; +OrderDtl_UD.EmailOForm_c as SendTo if populated; 
	10/12/2022: +set SendFrom addr as noreply@surestep.net; Update email subject/body;
	11/02/2022: +Handle different O-Form reports/backgrounds based on BasePartNum;
	03/24/2023: Change orange buttons to JobTraveler;
	03/28/2023: -Set TravelerLastPrinted from setTravFields method, moved to BPM;
	03/28/2023: -Email handling, +Print O-Form && JobTrav button;

=====================================================================================*/