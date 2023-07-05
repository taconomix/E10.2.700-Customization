/*============================================================================

	Custom code for Order Entry form

	Created: 09/26/2019
	Author:  Dylan Anderson
	Info:    Create custom SO Acknowledgement/O-Form using QuickPrint DLL,
	         Print to PDF or open Outlook item as PDF Attachment.
	         Add tab for credit card ordering, processing of credit cards

	Changed: 07/03/2023
		--Kevin Veldman <kevinv@surestep.net>
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
using Infragistics.Win.UltraWinToolbars;
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

		
		this.baseToolbarsManager.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.baseToolbarsManager_ToolClick);
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
		
		this.baseToolbarsManager.ToolClick -= new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.baseToolbarsManager_ToolClick);
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

		-----------------------------------------------------------------*/
		
		// Create Toolbar Button for SO Acknowledgement Email
		var button = new ButtonTool("btnEmailSOAck");
		baseToolbarsManager.Tools.Add(button);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("btnEmailSOAck");
		baseToolbarsManager.Tools["btnEmailSOAck"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["btnEmailSOAck"].SharedProps.Caption = "Email Order Acknowledgment";
		baseToolbarsManager.Tools["btnEmailSOAck"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(cbqp.Properties.Resources.cbi_email.Handle);

		
		// Create Toolbar Button for SO Acknowledgement print
		var printButton = new ButtonTool("btnPrintSOAck");
		baseToolbarsManager.Tools.Add(printButton);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("btnPrintSOAck");
		baseToolbarsManager.Tools["btnPrintSOAck"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["btnPrintSOAck"].SharedProps.Caption = "Print Order Acknowledgment";
		baseToolbarsManager.Tools["btnPrintSOAck"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(cbqp.Properties.Resources.print.Handle);
		

		// Create Toolbar Button for O-Form (watermark) email.
		var watermarkEmailButton = new ButtonTool("btnEmailOform");
		baseToolbarsManager.Tools.Add(watermarkEmailButton);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("btnEmailOform");
		baseToolbarsManager.Tools["btnEmailOform"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["btnEmailOform"].SharedProps.Caption = "Email O-Form";
		baseToolbarsManager.Tools["btnEmailOform"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(cbqp.Properties.Resources.cbi_email_blue.Handle);


		// Create Toolbar Button for O-Form print button.
		var watermarkPrintButton = new ButtonTool("btnPrintOform");
		baseToolbarsManager.Tools.Add(watermarkPrintButton);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("btnPrintOform");
		baseToolbarsManager.Tools["btnPrintOform"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["btnPrintOform"].SharedProps.Caption = "Print O-Form";
		baseToolbarsManager.Tools["btnPrintOform"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(cbqp.Properties.Resources.print_blue.Handle);


		// Get SSRS Data
		session = (Ice.Core.Session)SalesOrderForm.Session;
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
	private void baseToolbarsManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs args) {

		// Button Conditions for this Method
		bool qpOForm = args.Tool.Key == "btnPrintOform" || args.Tool.Key == "btnEmailOform";
		bool qpSOAck = args.Tool.Key == "btnPrintSOAck" || args.Tool.Key == "btnEmailSOAck";
		bool printButton = args.Tool.Key == "btnPrintOform" || args.Tool.Key == "btnPrintSOAck";
		bool emailButton = args.Tool.Key == "btnEmailOform" || args.Tool.Key == "btnEmailSOAck";
		bool needWatermark = qpOForm;            // Forms requiring Watermark Image

		if ( !printButton && !emailButton ) return;
		Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;

		if ( appendpath ) {
			ssrsPrefix += "Sales Management/";
			appendpath = false;
		}

		// get Order Field Data
		var orderNum = ((EpiTextBox)csm.GetNativeControlReference ("4fceeeec-518c-4256-932e-34a4c1a584ee")).Text;

		if ( ExitMethod (string.IsNullOrEmpty(orderNum), "Please enter a Sales Order Number") ) return;

		
		var orderLine   = string.Empty;
		var basePartNum = string.Empty;

		var grdOrderLines = (EpiDataView)oTrans.EpiDataViews["OrderDtl"];
		if ( grdOrderLines.dataView.Count > 0 && grdOrderLines.CurrentDataRow != null ) {	
			orderLine   = grdOrderLines.CurrentDataRow["OrderLine"].ToString();
			basePartNum = grdOrderLines.CurrentDataRow["BasePartNum"].ToString();
		}

		if ( ExitMethod (string.IsNullOrEmpty(orderLine), "Please enter a Sales Order Line") ) return;


		var watermarkName = string.Empty;
		var ssrsRptName = string.Empty;

		if ( qpOForm ) {

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

			ssrsRptName = "SOAck_New";
		}

		var reportPath = string.Format ("{0}{1}", ssrsPrefix, ssrsRptName);

		// set the parameter values and filename
		string rptType = qpOForm? "OForm": "SOAck";
		string tmpPath = System.IO.Path.GetTempPath();
		string fileName = string.Format(@"{2}{3}_{0}_{1:yyyyMMdd_HHmmss}.pdf", orderNum, DateTime.Now, tmpPath, rptType);

		//may need to be https depending on the client setup
		var serviceAddress = string.Format("http://{0}/reportserver/ReportExecution2005.asmx", ssrsServer);

		// create the parameters
		var session = (Ice.Core.Session)SalesOrderForm.Session;
		var parmList = new System.Collections.Specialized.OrderedDictionary();
			parmList.Add("Company", session.CompanyID);
			parmList.Add("OrderNum", orderNum);
		if (qpOForm) parmList.Add("OrderLine", orderLine);

		// execute the report
		oTrans.PushStatusText ("Executing Report", true);
		
		var reportBytes = cbqp.Reports.GetSSRSReport (serviceAddress, reportPath, parmList);
		if ( ExitMethod( reportBytes == null, "No report was returned.") ) return;
		
		System.IO.File.WriteAllBytes(fileName, reportBytes); 
		
		if ( needWatermark ) {

			oTrans.PushStatusText ("saving image with watermark", true);

			var watermarkPath = string.Format(@"\\Epapp02\oform_pdf-do_not_change\");
			var pdfImage = XImage.FromFile(watermarkPath + watermarkName);
			var width = pdfImage.PixelWidth * 72 / pdfImage.HorizontalResolution;
			var height = pdfImage.PixelHeight * 72 / pdfImage.VerticalResolution;

			var source = PdfReader.Open (fileName);
			XGraphics.FromPdfPage (source.Pages[0], XGraphicsPdfPageOptions.Prepend).DrawImage (pdfImage, 0, -180, width, height);
			source.Save (fileName);
		}


		if ( printButton ) { // Open PDF (Default Viewer)

			Process proce = new Process();
			proce.StartInfo.FileName = fileName;
			proce.Start();

		} else { // Open Email (Outlook)

			var txtPONumber = (EpiTextBox)csm.GetNativeControlReference ("4fceeeec-518c-4256-932e-34a4c1a584ee");
			var txtCustID = (EpiTextBox)csm.GetNativeControlReference("7ece91c9-dc93-4df4-9591-f6cad7562b71");
			var dtePromiseDate = (EpiDateTimeEditor)csm.GetNativeControlReference("47b19f90-8d13-4fd9-8310-ab75ad6f8224");
						
			string emailTo = "";
			string custName = "";

			// Get Customer Email
			using (var conn = new SqlConnection (ConnectionString)) {

				var cmd = new SqlCommand();
				cmd.Connection = conn;
				cmd.CommandText = "ReportSalesOrderAck";
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@Company", SqlDbType.Char).Value = session.CompanyID;
				cmd.Parameters.Add("@OrderNum", SqlDbType.Int).Value = orderNum;
				conn.Open();
				using(var dr = cmd.ExecuteReader()) {

					if (dr != null && dr.Read()) {

						var sqlVar = string.Empty;
						sqlVar = ( dr.GetString(dr.GetOrdinal("OFEmail")).Length > 0 ) ? "OFEmail":
						         ( !dr.IsDBNull (dr.GetOrdinal("ShipToEmail"))       ) ? "ShipToEmail":
						         ( !dr.IsDBNull (dr.GetOrdinal ("FaxEmail"))         ) ? "FaxEmail": "";

						if ( sqlVar.Length > 0 ) emailTo = dr.GetString( dr.GetOrdinal(sqlVar) );
						if ( sqlVar == "FaxEmail" ) emailTo += "@metrofax.com";

						if ( !dr.IsDBNull(dr.GetOrdinal("ShipToName")) ) custName = dr.GetString( dr.GetOrdinal("ShipToName") );
						if ( dr != null && !dr.IsClosed ) dr.Close();
					}
				}
			}

			if ( txtCustID.Text == "CAS95928" && qpSOAck ) emailTo = "customorders@cascade-usa.com";

			// Set up Email Parameters
			oTrans.PushStatusText ("Set up email parameters", true);

			string _shortForm = qpOForm? "O-Form": "Confirmation";
			string _longForm = qpOForm? "Surestep O-Form": "Order Confirmation form";

			var emailSubject = string.Format( "Surestep, LLC - {0} for Order #{1} Attached", _shortForm, orderNum );
			var emailBody = string.Format ( "<p>Dear Customer, </p><p>Your Surestep order has been received and processed as Sales Order #{0}. Your {1} is attached, and the expected shipping date is {2}.</p><p>Thank you for your order – we appreciate it very much!</p><p>Sincerely,</p><p>Customer Service Department</p><p>P: (877)462-0711 | F: (866)700-7837</p><p>orders@surestep.net</p>", orderNum, _longForm, dtePromiseDate.Text );

			if ( qpSOAck && getCredHoldStatus() ) { 

				var _poNum = txtPONumber.Text;
				var _custNum = txtCustID.Text;

				emailSubject = string.Format("Surestep, LLC - Issue with Account #{0}", _custNum); 
				emailBody = string.Format("<p>Dear Customer, </p><p><b>Thank you for your recent order. Your account #{0} is ON HOLD</b>. While we very much appreciate your business, PO# {1} will not be processed for fabrication until the matter with your account is resolved. Please have someone from your accounting team contact us at (877) 462-0711.</p><p>The order will ship between 4 and 8 days <b>after the issue with your account</b> is resolved.</p><p>Sincerely,</p><p>Kimberly Sante</p><p>SureStep Accounts Receivable</p><p>P: (877) 462-0711 | F: (866) 700-7837</p><p>kimberlys@surestep.net</p>", _custNum, _poNum );
			}

			// Create Email Message
			oTrans.PushStatusText ("Set up email message", true);

			// set up the email message (using outlook); saves to sent items.
			var application = new Outlook.Application();
			var message = application.CreateItem(Outlook.OlItemType.olMailItem) as Outlook.MailItem;

			if ( ExitMethod( message == null, "Unable to send email. Contact your Epicor Systems Administrator.") ) return;

			Outlook.Account account = application.Session.Accounts["noreply@surestep.net"];	
			if ( account != null ) message.SendUsingAccount = account;

			message.To = emailTo;
			message.Subject = emailSubject;
			message.Attachments.Add(fileName);
			
			System.IO.File.Delete(fileName);

			message.ReadReceiptRequested = false; // Set to false 2023/07/03 -KV
			message.Recipients.ResolveAll();
			message.Display();
			message.HTMLBody = emailBody + message.HTMLBody;

			oTrans.PushStatusText(string.Format ("Created Email to {0}", emailTo), true);
		}

		Cursor.Current = System.Windows.Forms.Cursors.Default;
		if ( qpSOAck ) setSOAckPrintDate();
	}



	private bool getCredHoldStatus () { // Return Credit Hold Status
		
		var edv = (EpiDataView)oTrans.EpiDataViews["OrderHed"];
		return (bool)edv.dataView[edv.Row]["CustOnCreditHold"];
	}



	public void setSOAckPrintDate() { // Set PrintDate (UserDate4) for SOAck Print Date

		var edv = (EpiDataView)oTrans.EpiDataViews["OrderHed"];
		edv.dataView[edv.Row]["UserDate4"] = DateTime.Today;
	}



	private bool ExitMethod ( bool ExitCondition, string ExitMessage ) { // If true, show message and return true

		if ( ExitCondition ) MessageBox.Show( ExitMessage );
		return ExitCondition;
	}
}



/*== Change Log ===================================================================================

	10/12/2022: +New Email Subject/Body, +EpiDataView for OrderLine, +Refactor usings, email code;
				+OD.EmailOForm_c as SendTo; +SendFrom noReply@surestep.net;
	11/02/2022: +Dynamically change SSRS rpt/background for O-Form, rows on grdSummaryOrderLines;
				Update Stored Proc, SSRS Rpt name for O-Forms;	
	11/22/2022: -CodaBears Remake Fields & SOAck QP buttons; +Update Comments, Minor Refactoring;
	03/29/2023: +SOAck QP Print Button;
	06/13/2023: +SOAck QP Email Button, Update Email Body to include Promised Date (oh.NeedByDate);
	06/20/2023: +New function to log print date for SO Acknowledgment;
	07/03/2023' Minor Refactor, add getCredHoldStatus & ExitMethod methods;

=================================================================================================*/