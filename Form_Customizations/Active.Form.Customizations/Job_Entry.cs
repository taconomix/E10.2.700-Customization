/*============================================================================

	Custom code for Job Entry form

	Created: 07/26/2022
	Author:  Fred Zelhart
	Info:    Create custom JobTraveler/O-Form using QuickPrint DLL,
			 Print to PDF or open Outlook item as PDF Attachment. 

	Changed: 03/24/2023
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

		/*
		var button = new ButtonTool("btnEmailTrav");
		baseToolbarsManager.Tools.Add(button);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("btnEmailTrav");
		baseToolbarsManager.Tools["btnEmailTrav"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["btnEmailTrav"].SharedProps.Caption = "Email JobTraveler";
		baseToolbarsManager.Tools["btnEmailTrav"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(CodaBears.Epicor.QuickPrint.Properties.Resources.cbi_email.Handle);
		*/
		// print button
		var printButton = new ButtonTool("btnPrintTrav");
		baseToolbarsManager.Tools.Add(printButton);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("btnPrintTrav");
		baseToolbarsManager.Tools["btnPrintTrav"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["btnPrintTrav"].SharedProps.Caption = "Print Job Traveler";
		baseToolbarsManager.Tools["btnPrintTrav"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(CodaBears.Epicor.QuickPrint.Properties.Resources.print.Handle);

		/* // watermark email button
		var watermarkEmailButton = new ButtonTool("btnEmailOF");
		baseToolbarsManager.Tools.Add(watermarkEmailButton);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("btnEmailOF");
		baseToolbarsManager.Tools["btnEmailOF"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["btnEmailOF"].SharedProps.Caption = "Email O-Form";
		baseToolbarsManager.Tools["btnEmailOF"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(CodaBears.Epicor.QuickPrint.Properties.Resources.cbi_email_blue.Handle);
		*/
		// print button
		var watermarkPrintButton = new ButtonTool("btnPrintOF");
		baseToolbarsManager.Tools.Add(watermarkPrintButton);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("btnPrintOF");
		baseToolbarsManager.Tools["btnPrintOF"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["btnPrintOF"].SharedProps.Caption = "Print O-Form";
		baseToolbarsManager.Tools["btnPrintOF"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(CodaBears.Epicor.QuickPrint.Properties.Resources.print_blue.Handle);

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
	private void baseToolbarsManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs args)
	{
		var isOForm = args.Tool.Key == "btnPrintOF" || args.Tool.Key == "btnEmailOF";
		var printButton = args.Tool.Key == "btnPrintOF" || args.Tool.Key == "btnPrintTrav";
		var emailButton = args.Tool.Key == "btnEmailOF" || args.Tool.Key == "btnEmailTrav"; 
		
		if ( printButton || emailButton ) {

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
			var watermarkName = string.Empty;
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
							watermarkName = dr.GetString (dr.GetOrdinal ("oForm")) + ".pdf";
					}
				}
			}
			
			if (orderNum <= 0 || orderLine <= 0) {
				MessageBox.Show (string.Format ("No order was found for job {0}", jobNum), "ToolClick");
				return;
			}

			// set the parameter values and filename
			string reportNamePrefix = isOForm? "OForm": "JobTrav";
			filename = string.Format(@"{2}{3}_{0}_{1:yyyyMMdd_HHmmss}.pdf", orderNum, DateTime.Now, System.IO.Path.GetTempPath(),reportNamePrefix);

			//may need to be https depending on the client setup
			var serviceAddress = string.Format("http://{0}/reportserver/ReportExecution2005.asmx", server);

			// create the parameters
			var session = (Ice.Core.Session)JobEntryForm.Session;
			var parmList = new System.Collections.Specialized.OrderedDictionary();
				parmList.Add("Company", session.CompanyID);

			if ( isOForm ) {
				parmList.Add("OrderNum", orderNum.ToString());
				parmList.Add("OrderLine", orderLine.ToString());
			} else {
				parmList.Add("JobNum", jobNum);
			}

			// execute the report
			var ssrsRptName = string.Empty;
			if (isOForm) {
				switch (watermarkName) {
					case "ACTIVE-TLSO.pdf":
						ssrsRptName = "OForm TLSO_Active";
						break;
					case "ACTIVE-HEKO.pdf":
						ssrsRptName = "OForm HEKO_Active";
						break;
					case "ACTIVE-KAFO.pdf":
						ssrsRptName = "OForm KAFO_Active";
						break;
					default:
						ssrsRptName = "OForm SMO-AFO_Active";
						break;
				}
			} else {
				ssrsRptName = "JobTravQP";
			}
			var reportPath = string.Format ("{0}{1}", ssrsPrefix, ssrsRptName);

			oTrans.PushStatusText ("Executing Report", true);
			var reportBytes = CodaBears.Epicor.QuickPrint.Reports.GetSSRSReport (serviceAddress, reportPath, parmList);
			
			if (reportBytes == null) {
				MessageBox.Show ("No report was returned.");
			} else {
				System.IO.File.WriteAllBytes(filename, reportBytes);
				
				if (isOForm) {
					// 2022725 Kevin Veldman has mentioned that the background image may change per custom item; code will need to be modified; not part of current scope
					var watermarkPath = string.Format(@"\\Epapp02\oform_pdf-do_not_change");
					string backgroundFile = string.Format(@"{0}\{1}", watermarkPath, watermarkName);
					
					var watermarkImage = XImage.FromFile(backgroundFile);
					oTrans.PushStatusText ("opening background image", true);
					var source = PdfReader.Open (filename);
					oTrans.PushStatusText ("putting in watermark", true);
					var gfx = XGraphics.FromPdfPage (source.Pages[0], XGraphicsPdfPageOptions.Prepend);
					var width = watermarkImage.PixelWidth * 72 / watermarkImage.HorizontalResolution;
					var height = watermarkImage.PixelHeight * 72 / watermarkImage.VerticalResolution;
					gfx.DrawImage (watermarkImage, 0, -180, width, height);
					
					oTrans.PushStatusText ("saving image with watermark", true);
					source.Save (filename);
				}
				
				if (printButton) {		

					Process proce = new Process();
					proce.StartInfo.FileName = filename;
					proce.Start();

					if ( !isOForm ) setTravFields();

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

					string formPreface = isOForm? "O-Form": "Confirmation";
					var emailSubject = string.Format("Surestep, LLC – {1} for Order #{0} Attached | CONFIDENTIAL/PROTECTED HEALTH INFORMATION ATTACHED", orderNum, formPreface);

					string formInfo = isOForm? "O-Form": "order details";
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
				}
			}
			Cursor.Current = System.Windows.Forms.Cursors.Default;
		}
	}

	public void setTravFields() {

		string guidMassPrint = "e56504ae-c608-4841-962a-5b540fdae4d3";
		string guidLastPrint = "e2113199-7e79-4018-94ef-47a212282a86";

		var massPrint = (EpiCheckBox)csm.GetNativeControlReference(guidMassPrint);
		var lastPrint = (EpiDateTimeEditor)csm.GetNativeControlReference(guidLastPrint);

		massPrint.Checked = false;
		lastPrint.Value = DateTime.Today;
		oTrans.Update();
	}
}


/*== CHANGE LOG =======================================================================

	10/12/2022: +Using...Outlook; +OrderDtl_UD.EmailOForm_c as SendTo if populated; 
	10/12/2022: +set SendFrom addr as noreply@surestep.net; Update email subject/body;
	11/02/2022: +Handle different O-Form reports/backgrounds based on BasePartNum;
	03/24/2023: Change orange buttons to JobTraveler;

=====================================================================================*/
