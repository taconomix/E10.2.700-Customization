/*============================================================================

	Custom code for Order Entry form

	Created: 01/05/2020
	Author:  Dylan Anderson
	Info:    Create custom SO Acknowledgement/O-Form using QuickPrint DLL,
	         Print to PDF or open Outlook item as PDF Attachment.
	         Add tab for credit card ordering, processing of credit cards

	Changed: 05/04/2023
		--Kevin Veldman <KVeldman@Hanger.com>
============================================================================*/

extern alias Erp_Contracts_BO_CustShip;
extern alias Erp_Contracts_BO_MiscShip;
extern alias Erp_Contracts_BO_DropShip;
extern alias Erp_Contracts_BO_SalesOrder;
extern alias Erp_Contracts_BO_Company;
extern alias Erp_Contracts_BO_Customer;
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

using System.Drawing;
using Infragistics.Win;
using Infragistics.Win.UltraWinToolbars;
using System.IO;
using Ice.Core;
using System.Data.SqlClient;

using PdfSharp.Drawing;
using PdfSharp.Pdf.IO;
using Outlook = Microsoft.Office.Interop.Outlook;
using cbqp = CodaBears.Epicor.QuickPrint;

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

		// End Wizard Added Variable Initialization
		this.baseToolbarsManager.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.baseToolbarsManager_ToolClick);
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
	
	bool appendpath = true;
	private void baseToolbarsManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs args)
	{
		var needWatermark = args.Tool.Key == "CBWatermarkPrintButton" || args.Tool.Key == "CBWatermarkEmailButton";
		var printButton = args.Tool.Key == "CBWatermarkPrintButton" || args.Tool.Key == "CBPrintButton";
		var emailButton = args.Tool.Key == "CBWatermarkEmailButton" || args.Tool.Key == "CBEmailButton"; 

		if (printButton || emailButton)
		{
			Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
			// variables to change for each report
			var server = ssrsServer;
			// Set to false for single folder, true otherwise
			if (appendpath){
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
				ssrsRptName = "SOAck_New";
			}

			var reportPath = string.Format ("{0}{1}", ssrsPrefix, ssrsRptName);


			// set the parameter values and filename
			string reportNamePrefix = needWatermark? "OForm": "SOAck";
			filename = string.Format(@"{2}{3}_{0}_{1:yyyyMMdd_HHmmss}.pdf", orderNum, DateTime.Now, System.IO.Path.GetTempPath(),reportNamePrefix);

			//may need to be https depending on the client setup
			var serviceAddress = string.Format("http://{0}/reportserver/ReportExecution2005.asmx", server);

			// create the parameters
			var session = (Ice.Core.Session)SalesOrderTrackerForm.Session;
			var parmList = new System.Collections.Specialized.OrderedDictionary();
			parmList.Add("Company", session.CompanyID);
			parmList.Add("OrderNum", orderNum);
			if (needWatermark) 
			{
			   parmList.Add ("OrderLine", null);
			}

			// execute the report
			oTrans.PushStatusText ("Executing Report", true);
			var reportBytes = CodaBears.Epicor.QuickPrint.Reports.GetSSRSReport (serviceAddress, reportPath, parmList);
			
			if (reportBytes == null) 
			{
				MessageBox.Show ("No report was returned.");
			}
			else 
			{
				System.IO.File.WriteAllBytes(filename, reportBytes);
				
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
				
   			if (printButton)
   			{							
   				Process proce = new Process();
   				proce.StartInfo.FileName = filename;
   				proce.Start();
   			}
   			else 
   			{
   				oTrans.PushStatusText ("Get order's email", true);
   
   				var txtPONumber = (EpiTextBox)csm.GetNativeControlReference ("e5d73cb9-1d94-45ce-85be-db4c85fe3264");
   				var poNum = txtPONumber.Text;	
				   oTrans.PushStatusText ("Get customer's email", true);
  			 	string emailTo = "";
 				  string custName = "";
 				  using (var conn = new SqlConnection (ConnectionString))					  
				   {
					   var cmd = new SqlCommand();
					   cmd.Connection = conn;
					   cmd.CommandText = "ReportSalesOrderAck";
					   cmd.CommandType = CommandType.StoredProcedure;
					   cmd.Parameters.Add("@Company", SqlDbType.Char).Value = company;
					   cmd.Parameters.Add("@OrderNum", SqlDbType.Int).Value = orderNum;
					   conn.Open();
					   using(var dr = cmd.ExecuteReader())
					   {
						   if (dr != null && dr.Read()) 
						   {
							   if (!dr.IsDBNull(dr.GetOrdinal("BillToEmail"))) emailTo = dr.GetString (dr.GetOrdinal ("BillToEmail"));
							   if (!dr.IsDBNull(dr.GetOrdinal("BillToName"))) custName = dr.GetString (dr.GetOrdinal ("BillToName"));
							   if (dr != null && !dr.IsClosed)
							   {
								   dr.Close();
							   }
						   }
 					  }
 					}
   	
   				oTrans.PushStatusText ("Set up email parameters", true);
				string formPreface = needWatermark? "O-Form": "Confirmation";
				var emailSubject = string.Format("Surestep, LLC – {1} for Order #{0} Attached | CONFIDENTIAL/PROTECTED HEALTH INFORMATION ATTACHED", orderNum, formPreface);

				string formInfo = needWatermark? "O-Form": "order details";
				var emailBody = string.Format ("<p>Surestep Sales Order #{0} has been received and processed. Please find attached the {1} for this order. Thank you for your order – we appreciate it very much!</p><p>Sincerely,</p><p>Customer Service Department</p><p>orders@surestep.net</p><p>Important Notice:</p><p>This message is intended only for the use of the individual or entity to which it is addressed. The documents in this e-mail or facsimile transmission may contain confidential health information that is privileged and legally protected from disclosure by the Health Insurance Portability and Accountability Act (HIPAA). This information is intended only for the use of the individual or entity named above. If you are not the intended recipient, you are hereby notified that reading, disseminating, disclosing, distributing, copying, acting upon or otherwise using the information contained in this facsimile is strictly prohibited. If you have received this information in error, please notify the sender.</p>", orderNum,formInfo);
	
   				oTrans.PushStatusText ("Set up email message", true);

   				// set up the email message (using outlook); saves to sent items, but requires file IO
   				var application = new Outlook.Application();
				var message = application.CreateItem(Outlook.OlItemType.olMailItem) as Outlook.MailItem;
				Outlook.Account account = application.Session.Accounts["noreply@surestep.net"];
   				
   				if (message != null)
   				{
   					// set up the email parameters
   					if ( account!=null ) message.SendUsingAccount = account;
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
	
	private void SalesOrderTrackerForm_Load(object sender, EventArgs args)
	{
		// Add Event Handler Code
		var button = new ButtonTool("CBEmailButton");
		baseToolbarsManager.Tools.Add(button);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("CBEmailButton");
		baseToolbarsManager.Tools["CBEmailButton"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["CBEmailButton"].SharedProps.Caption = "Email Sales Order Ack";
		baseToolbarsManager.Tools["CBEmailButton"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(CodaBears.Epicor.QuickPrint.Properties.Resources.cbi_email.Handle);

		// print button
		var printButton = new ButtonTool("CBPrintButton");
		baseToolbarsManager.Tools.Add(printButton);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("CBPrintButton");
		baseToolbarsManager.Tools["CBPrintButton"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["CBPrintButton"].SharedProps.Caption = "Print Sales Order Ack";
		baseToolbarsManager.Tools["CBPrintButton"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(CodaBears.Epicor.QuickPrint.Properties.Resources.print.Handle);

	   // watermark email button
		var watermarkEmailButton = new ButtonTool("CBWatermarkEmailButton");
		baseToolbarsManager.Tools.Add(watermarkEmailButton);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("CBWatermarkEmailButton");
		baseToolbarsManager.Tools["CBWatermarkEmailButton"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["CBWatermarkEmailButton"].SharedProps.Caption = "Email Sales Order Ack Watermark";
		baseToolbarsManager.Tools["CBWatermarkEmailButton"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(CodaBears.Epicor.QuickPrint.Properties.Resources.cbi_email_blue.Handle);

		// print button
		var watermarkPrintButton = new ButtonTool("CBWatermarkPrintButton");
		baseToolbarsManager.Tools.Add(watermarkPrintButton);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("CBWatermarkPrintButton");
		baseToolbarsManager.Tools["CBWatermarkPrintButton"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["CBWatermarkPrintButton"].SharedProps.Caption = "Print Sales Order Ack Watermark";
		baseToolbarsManager.Tools["CBWatermarkPrintButton"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(CodaBears.Epicor.QuickPrint.Properties.Resources.print_blue.Handle);

		// get ssrs data
		session = (Ice.Core.Session)SalesOrderTrackerForm.Session;
		company = session.CompanyID;
		using (var svc = WCFServiceSupport.CreateImpl<Erp.Proxy.BO.CompanyImpl>(session, Epicor.ServiceModel.Channels.ImplBase<Erp.Contracts.CompanySvcContract>.UriPath))
		{
			var companyDs = svc.GetByID(session.CompanyID);
			if (companyDs != null) {
				ssrsServer = (string)companyDs.Tables[0].Rows[0]["ssrsServer_c"];
				ssrsPrefix = (string)companyDs.Tables[0].Rows[0]["ssrsPrefix_c"];
				ConnectionString = (string)companyDs.Tables[0].Rows[0]["sqlConnectionString_c"];
			}
		}
	
		if (ssrsPrefix.Length == 0) { MessageBox.Show ("ssrsPrefix cannot be blank or null on the company table."); }
		if (ssrsServer.Length == 0) { MessageBox.Show ("ssrs Server cannot be blank or null on the company table."); }
		if (ConnectionString.Length == 0) { MessageBox.Show ("sqlConnectionString cannot be blank or null on the company table."); }
	}
}



/*== Change Log ===================================================================================

	05/04/2023: +Dynamic Report Names; +minor refactoring;

=================================================================================================*/