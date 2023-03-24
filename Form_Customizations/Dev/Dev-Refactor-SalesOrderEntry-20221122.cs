/*============================================================================

	Custom code for Order Entry form

	Created: 09/26/2019
	Author:  Dylan Anderson
	Info:    Create custom SO Acknowledgement/O-Form using QuickPrint DLL,
	         Print to PDF or open Outlook item as PDF Attachment.
	         Add tab for credit card ordering, processing of credit cards

	Changed: 11/22/2022
	By:      Kevin Veldman

============================================================================*/

extern alias Erp_Adapters_SalesOrder;
extern alias Erp_Adapters_ReviewJrn;
extern alias Erp_Adapters_Customer;
extern alias Erp_Contracts_BO_ARInvoice;
extern alias Erp_Contracts_BO_Currency;
extern alias Erp_Contracts_BO_CashRec;
extern alias Erp_Contracts_BO_CashRecSearch;
extern alias Erp_Adapters_CashRec;
extern alias Erp_Adapters_Currency;
extern alias Erp_Contracts_BO_Company;
extern alias Erp_Contracts_BO_AlternatePart;
extern alias Erp_Contracts_BO_SalesOrder;
extern alias Erp_Contracts_BO_Quote;
extern alias Erp_Contracts_BO_Part;
extern alias Erp_Contracts_BO_Customer;
extern alias Erp_Contracts_BO_OrderDtlSearch;
extern alias Erp_Contracts_BO_OrderHist;
extern alias Erp_Contracts_BO_RMAProc;
extern alias Erp_Contracts_BO_QuoteDtlSearch;
extern alias Erp_Contracts_BO_SerialNumberSearch;
extern alias Erp_Contracts_BO_ShipTo;
extern alias CodaBears_Epicor_QuickPrint;
extern alias Microsoft_Office_Interop_Outlook;
//extern alias PdfSharp;

using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using Erp.Adapters;
using Erp.UI;
using Ice.Lib;
using Ice.Adapters;
using Ice.Lib.Customization;
using Ice.Lib.ExtendedProps;
using Ice.Lib.Framework;
using Ice.Lib.Searches;
using Ice.UI.FormFunctions;

using System.Data.SqlClient;
using System.Drawing;
using Infragistics.Win;
using UWToolbars = Infragistics.Win.UltraWinToolbars;
using System.IO;
using Ice.Core;

using PdfSharp.Drawing;
using PdfSharp.Pdf.IO;
using Outlook = Microsoft.Office.Interop.Outlook;
using cbqp = CodaBears.Epicor.QuickPrint;

public class Script
{
	string ConnectionString = string.Empty;
	string ssrsPrefix = string.Empty;
	string company = string.Empty;
	string filename = string.Empty;
	string ssrsServer = string.Empty;
	Session session;
	// ** Wizard Insert Location - Do Not Remove 'Begin/End Wizard Added Module Level Variables' Comments! **
	// Begin Wizard Added Module Level Variables **

	// End Wizard Added Module Level Variables **

	// Add Custom Module Level Variables Here **

	public void InitializeCustomCode()
	{
		// ** Wizard Insert Location - Do not delete 'Begin/End Wizard Added Variable Initialization' lines **
		// Begin Wizard Added Variable Initialization

		
		this.baseToolbarsManager.ToolClick += new UWToolbars.ToolClickEventHandler(this.baseToolbarsManager_ToolClick);
		// End Wizard Added Variable Initialization

		// Begin Wizard Added Custom Method Calls

		/* Removing methods for remakes on Order Summary
			this.cchkRemake.CheckStateChanged += new System.EventHandler(this.cchkRemake_CheckStateChanged); */

		// End Wizard Added Custom Method Calls
	}

	public void DestroyCustomCode()
	{
		// ** Wizard Insert Location - Do not delete 'Begin/End Wizard Added Object Disposal' lines **
		// Begin Wizard Added Object Disposal
		
		/* Removing methods for remakes on Order Summary
			this.cchkRemake.CheckStateChanged -= new System.EventHandler(this.cchkRemake_CheckStateChanged); */
		
		this.baseToolbarsManager.ToolClick -= new UWToolbars.ToolClickEventHandler(this.baseToolbarsManager_ToolClick);
		// End Wizard Added Object Disposal

		// Begin Custom Code Disposal

		// End Custom Code Disposal
	}



	/*============================================================================================
		Desc: Create O-Form Email/Print buttons on Form Load
		User: Kevin Veldman 
		Date: 11/22/2022

		Removed unused features, refactored
	============================================================================================*/	
	private void SalesOrderForm_Load(object sender, EventArgs args)	{
		
		/*-----------------------------------------------------------------
			>>>>>>>>REMOVED<<<<<<<<

			//Set custom Remake Fields to Disabled as starting state.
			cbaqReasonAct.Enabled = false;
			cbaqReasonList.Enabled = false;
			cbaqRemPractitioner.Enabled = false;

			// Create Toolbar Button for SO Acknowledgement Email
			var button = new ButtonTool("CBEmailButton");
			baseToolbarsManager.Tools.Add(button);
			baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("CBEmailButton");
			baseToolbarsManager.Tools["CBEmailButton"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
			baseToolbarsManager.Tools["CBEmailButton"].SharedProps.Caption = "Email Sales Order Ack";
			baseToolbarsManager.Tools["CBEmailButton"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(cbqp.Properties.Resources.cbi_email.Handle);

			// Create Toolbar Button for SO Acknowledgement print
			var printButton = new ButtonTool("CBPrintButton");
			baseToolbarsManager.Tools.Add(printButton);
			baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("CBPrintButton");
			baseToolbarsManager.Tools["CBPrintButton"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
			baseToolbarsManager.Tools["CBPrintButton"].SharedProps.Caption = "Print Sales Order Ack";
			baseToolbarsManager.Tools["CBPrintButton"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(cbqp.Properties.Resources.print.Handle);
		-----------------------------------------------------------------*/


		// Create Toolbar Button for O-Form (watermark) email.
		var watermarkEmailButton = new ButtonTool("btnOFEmail");
		baseToolbarsManager.Tools.Add(watermarkEmailButton);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("btnOFEmail");
		baseToolbarsManager.Tools["btnOFEmail"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["btnOFEmail"].SharedProps.Caption = "Email Sales Order Ack Watermark";
		baseToolbarsManager.Tools["btnOFEmail"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(cbqp.Properties.Resources.cbi_email_blue.Handle);

		// Create Toolbar Button for O-Form print button.
		var watermarkPrintButton = new ButtonTool("btnOFPrint");
		baseToolbarsManager.Tools.Add(watermarkPrintButton);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("btnOFPrint");
		baseToolbarsManager.Tools["btnOFPrint"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["btnOFPrint"].SharedProps.Caption = "Print Sales Order Ack Watermark";
		baseToolbarsManager.Tools["btnOFPrint"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(cbqp.Properties.Resources.print_blue.Handle);

		// Get SSRS Data
		session = (Ice.Core.Session)SalesOrderForm.Session;
		company = session.CompanyID;
		using (var svc = WCFServiceSupport.CreateImpl<Erp.Proxy.BO.CompanyImpl>(session, Epicor.ServiceModel.Channels.ImplBase<Erp.Contracts.CompanySvcContract>.UriPath)) {

			var companyDs = svc.GetByID(session.CompanyID);
			if (companyDs != null) {
				//From Custom "CBI" Tab on Company Configuration
				ssrsServer = (string)companyDs.Tables[0].Rows[0]["ssrsServer_c"];
				ssrsPrefix = (string)companyDs.Tables[0].Rows[0]["ssrsPrefix_c"];
				ConnectionString = (string)companyDs.Tables[0].Rows[0]["sqlConnectionString_c"];
			}
		}
	
		if (ssrsPrefix.Length == 0) MessageBox.Show ("ssrsPrefix cannot be blank or null on the company table.");
		if (ssrsServer.Length == 0) MessageBox.Show ("ssrs Server cannot be blank or null on the company table.");
		if (ConnectionString.Length == 0) MessageBox.Show ("sqlConnectionString cannot be blank or null on the company table.");
	}


	/*-----------------------------------------------------------------
			>>>>>>>>REMOVED<<<<<<<<

		//Check for Remake, enable custom remake fields
		private void cchkRemake_CheckStateChanged(object sender, System.EventArgs args)	{
			if (cchkRemake.Checked) {
				cbaqReasonAct.Enabled = true;
				cbaqReasonList.Enabled = true;
				cbaqRemPractitioner.Enabled = true;
			} else {
				cbaqReasonAct.Enabled = false;
				cbaqReasonList.Enabled = false;
				cbaqRemPractitioner.Enabled = false;
			}
		}
	-----------------------------------------------------------------*/

	bool appendpath = true;



	private void baseToolbarsManager_ToolClick(object sender, UWToolbars.ToolClickEventArgs args) {

		var needWatermark = args.Tool.Key == "btnOFPrint" || args.Tool.Key == "btnOFEmail";
		var printButton = args.Tool.Key == "btnOFPrint" || args.Tool.Key == "CBPrintButton";
		var emailButton = args.Tool.Key == "btnOFEmail" || args.Tool.Key == "CBEmailButton"; 

		if (printButton || emailButton) {
			Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
     			
			// variables to change for each report
			var server = ssrsServer;
			
			// Set to false for single folder, true otherwise
			if (appendpath) {
				ssrsPrefix += "Sales Management/";
				appendpath = false;
			}
	
			// get the Order Number
			var txtOrderNumber = (EpiTextBox)csm.GetNativeControlReference ("4fceeeec-518c-4256-932e-34a4c1a584ee");
			var orderNum = txtOrderNumber.Text;	
			if (string.IsNullOrEmpty(orderNum)) {
				MessageBox.Show ("Please enter a Sales Order Number");
				return;
			}

			// get the Order Line
			var orderLine   = string.Empty;
			var basePartNum = string.Empty;
			EpiDataView grdSummaryOrderLines = (EpiDataView)(oTrans.EpiDataViews["OrderDtl"]);
			if (grdSummaryOrderLines.dataView.Count>0 && grdSummaryOrderLines.CurrentDataRow != null) {
				DataRow CurrentSalesOrderLineRow = grdSummaryOrderLines.CurrentDataRow;
				orderLine   = CurrentSalesOrderLineRow["OrderLine"].ToString();
				basePartNum = CurrentSalesOrderLineRow["BasePartNum"].ToString();
			}
	
			if (string.IsNullOrEmpty(orderLine)) {
				MessageBox.Show ("Please enter a Sales Order Line");
				return;
			} 

			var watermarkName = string.Empty;
			var ssrsRptName = string.Empty;

			if (needWatermark) {

				switch (basePartNum) {
					case "webTLSO":
						ssrsRptName   = "OForm TLSO_Active";
						watermarkName = "ACTIVE-TLSO.pdf";
						break;
					case "webHEKO":
						ssrsRptName = "OForm HEKO_Active";
						watermarkName = "ACTIVE-HEKO.pdf";
						break;
					case "webKAFO":
						ssrsRptName = "OForm KAFO_Active";
						watermarkName = "ACTIVE-KAFO.pdf";
						break;
					default:
						ssrsRptName   = "OForm SMO-AFO_Active";
						watermarkName = "ACTIVE-SMOAFO.pdf";
						break;
				}
			} else {
				ssrsRptName = "Sales Order Ack";
			}

			var reportPath = string.Format ("{0}{1}", ssrsPrefix, ssrsRptName);

			// set the parameter values and filename
			string reportNamePrefix = needWatermark? "OForm": "SOAck";
			filename = string.Format(@"{2}{3}_{0}_{1:yyyyMMdd_HHmmss}.pdf", orderNum, DateTime.Now, System.IO.Path.GetTempPath(),reportNamePrefix);

			//may need to be https depending on the client setup
			var serviceAddress = string.Format("http://{0}/reportserver/ReportExecution2005.asmx", server);

			// create the parameters
			var session = (Ice.Core.Session)SalesOrderForm.Session;
			var parmList = new System.Collections.Specialized.OrderedDictionary();
			parmList.Add("Company", session.CompanyID);
			parmList.Add("OrderNum", orderNum);
			if (needWatermark) parmList.Add("OrderLine", orderLine);

			// execute the report
			oTrans.PushStatusText ("Executing Report", true);
			var reportBytes = cbqp.Reports.GetSSRSReport (serviceAddress, reportPath, parmList);
			
			if (reportBytes == null) {
				MessageBox.Show ("No report was returned.");
			} else {
				System.IO.File.WriteAllBytes(filename, reportBytes); //Outlook requires file IO
				
				if (needWatermark) {
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
	   			} else {
	   				oTrans.PushStatusText ("Get order's email", true);
					var txtPONumber = (EpiTextBox)csm.GetNativeControlReference ("4fceeeec-518c-4256-932e-34a4c1a584ee");
	   				var poNum = txtPONumber.Text;	

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

					string formPreface = needWatermark? "O-Form": "Confirmation";
					var emailSubject = string.Format("Surestep, LLC – {1} for Order #{0} Attached | CONFIDENTIAL/PROTECTED HEALTH INFORMATION ATTACHED", orderNum, formPreface);

					string formInfo = needWatermark? "O-Form": "order details";
					var emailBody = string.Format ("<p>Surestep Sales Order #{0} has been received and processed. Please find attached the {1} for this order. Thank you for your order – we appreciate it very much!</p><p>Sincerely,</p><p>Customer Service Department</p><p>orders@surestep.net</p><p>Important Notice:</p><p>This message is intended only for the use of the individual or entity to which it is addressed. The documents in this e-mail or facsimile transmission may contain confidential health information that is privileged and legally protected from disclosure by the Health Insurance Portability and Accountability Act (HIPAA). This information is intended only for the use of the individual or entity named above. If you are not the intended recipient, you are hereby notified that reading, disseminating, disclosing, distributing, copying, acting upon or otherwise using the information contained in this facsimile is strictly prohibited. If you have received this information in error, please notify the sender.</p>", orderNum,formInfo);

					oTrans.PushStatusText ("Set up email message", true);

					// set up the email message (using outlook); saves to sent items.
					var application = new Outlook.Application();
					var message = application.CreateItem(Outlook.OlItemType.olMailItem) as Outlook.MailItem;
					Outlook.Account account = application.Session.Accounts["noreply@surestep.net"];

					if (message != null) {
						
						if(account != null) message.SendUsingAccount = account;
						message.To = emailTo;
						message.Subject = emailSubject;
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
}







/*==================================================================================================================================================

	Change Log:

		User:  Date:     Changes:
		KV     20221012  Added 'Using ... Outlook'
		                   Add EpiDataView section to isolate selected OrderLine
		                   Add statement to use OrderDtl_UD.EmailOForm_c as SendTo address if not blank or empty
		                   Add 'account' variable to set SendFrom address as noreply@surestep.net if/when that account is attached to client's outlook
		                   Update email subject and body, with extra bit to specify O-Form. 
		KV     20221102  Update hidden rows on grdSummaryOrderLines
		                   Change SMO/AFO O-Form Report from "O-Form Rev 1" to "OForm SMO-AFO_Active" (using new Stored Procedure)
		                   Add code to dynamically change which SSRS report for O-Forms. 
		                   Add code to dynamically change watermark.
		KV     20221121  Adjust Sales Order EpiGrid fields
		KV     20221122  Removed code for CodaBears Remake Fields, Removed SO Acknowledgment QuickPrint toolbar buttons. 
		                   Removed the CB Remake fields from the Summary tab, and hid most UD_c fields from the SO Line EpiUltraGrid 
		                   Improved Documentation and indenting for clarity.
==================================================================================================================================================*/
