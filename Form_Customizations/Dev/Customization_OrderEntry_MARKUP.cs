// **************************************************
// Custom code for SalesOrderForm
// Created: 9/26/2019 5:16:29 PM
// **************************************************

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
		this.baseToolbarsManager.ToolClick += new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.baseToolbarsManager_ToolClick);
		// End Wizard Added Variable Initialization

		// Begin Wizard Added Custom Method Calls

		this.cchkRemake.CheckStateChanged += new System.EventHandler(this.cchkRemake_CheckStateChanged);
		// End Wizard Added Custom Method Calls
	}

	public void DestroyCustomCode()
	{
		// ** Wizard Insert Location - Do not delete 'Begin/End Wizard Added Object Disposal' lines **
		// Begin Wizard Added Object Disposal
		this.cchkRemake.CheckStateChanged -= new System.EventHandler(this.cchkRemake_CheckStateChanged);
		this.baseToolbarsManager.ToolClick -= new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.baseToolbarsManager_ToolClick);
		// End Wizard Added Object Disposal

		// Begin Custom Code Disposal

		// End Custom Code Disposal
	}

	private void SalesOrderForm_Load(object sender, EventArgs args)
	{
		// Add Event Handler Code
		cbaqReasonAct.Enabled = false;
		cbaqReasonList.Enabled = false;
		cbaqRemPractitioner.Enabled = false;
	
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
		session = (Ice.Core.Session)SalesOrderForm.Session;
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

	private void cchkRemake_CheckStateChanged(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
		if (cchkRemake.Checked) 
		{
		cbaqReasonAct.Enabled = true;
		cbaqReasonList.Enabled = true;
		cbaqRemPractitioner.Enabled = true;
		
		}	
		else 
		{
		cbaqReasonAct.Enabled = false;
		cbaqReasonList.Enabled = false;
		cbaqRemPractitioner.Enabled = false;
		
		}
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
			
			var reportPath = string.Format ("{0}{1}", ssrsPrefix, needWatermark ? "O-Form rev 1" : "Sales Order Ack");
	
			// get the Order Number
			var txtOrderNumber = (EpiTextBox)csm.GetNativeControlReference ("4fceeeec-518c-4256-932e-34a4c1a584ee"); 
				// PTEK - Is the NativeControlReference a field reference? Would appreciate a rundown of how this works and how to find appropriate NativeControlReference for fields that I wish to reference
			
			var orderNum = txtOrderNumber.Text;	
			if (string.IsNullOrEmpty(orderNum)) {
				MessageBox.Show ("Please enter a Sales Order Number");
				return;
			}

			// set the parameter values and filename
			filename = string.Format(@"{2}SOAck_{0}_{1:yyyyMMdd_HHmmss}.pdf", orderNum, DateTime.Now, System.IO.Path.GetTempPath());

			//may need to be https depending on the client setup
			var serviceAddress = string.Format("http://{0}/reportserver/ReportExecution2005.asmx", server);

			// create the parameters
			var session = (Ice.Core.Session)SalesOrderForm.Session;
			var parmList = new System.Collections.Specialized.OrderedDictionary();
			parmList.Add("Company", session.CompanyID);
			parmList.Add("OrderNum", orderNum);
			if (needWatermark)
			{
				// PTEK - Parameter OrderLine==null. Should pass focused OrderLine. How to get OrderDtl.OrderLine based on Focused Line?
				parmList.Add("OrderLine", null);
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
				
				// todo: handle merging watermark pdf if needed
				if (needWatermark)
				{

					// PTEK - backgroundFile needs to be different based on the field "BasePartNum" attached to Order Line. How to get OrderDtl.BasePartNum as string variable?
					// PTEK - Why use 'const'? That means I can't change the variable with an if/else statement, correct?
					const string backgroundFile = @"\\Epapp02\oform_pdf-do_not_change\ACTIVE-SMOAFO.pdf";
		
					var watermarkImage = XImage.FromFile(backgroundFile); 

					// PTEK - Alternative to above:
						// const string bgFile2 = @"\\Epapp02\oform_pdf-do_not_change\ACTIVE-TLSO.pdf";
						// var watermarkImage = xImage.FromFile(OrderDtl.BasePartNum == "webTLSO"? bgFile2: backgroundFile);

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
   
   				var txtPONumber = (EpiTextBox)csm.GetNativeControlReference ("4fceeeec-518c-4256-932e-34a4c1a584ee");
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
   						if (dr != null && dr.Read()) {
   							if (!dr.IsDBNull(dr.GetOrdinal("ShipToEmail")))
   							{
   								emailTo = dr.GetString (dr.GetOrdinal ("ShipToEmail"));
   							}
   							else if(!dr.IsDBNull(dr.GetOrdinal("FaxEmail")))
   							{
   								emailTo = dr.GetString (dr.GetOrdinal ("FaxEmail")) + "@metrofax.com";
   							}								
   
   							if (!dr.IsDBNull(dr.GetOrdinal("ShipToName"))) custName = dr.GetString (dr.GetOrdinal ("ShipToName"));
   							if (dr != null && !dr.IsClosed)
   							{
   								dr.Close();
   							}
   						}
   					}
   				}
   
   				oTrans.PushStatusText ("Set up email parameters", true);
   				var emailSubject = string.Format ("Surestep, LLC Sales Order {0} Attached-CONFIDENTIAL/PROTECTED HEALTH INFORMATION (PHI) ATTACHED", poNum);
   				var emailBody = string.Format ("<p>Surestep Sales Order {0} has been received and processed. Details are attached.</p><p>Thank you for your order – we appreciate it very much!</p><p>Sincerely,</p><p>Customer Service Department</p><p>orders@surestep.net</p><p>Important Notice:</p><p>This message is intended only for the use of the individual or entity to which it is addressed. The documents in this e-mail or facsimile transmission may contain confidential health information that is privileged and legally protected from disclosure by the Health Insurance Portability and Accountability Act (HIPAA). This information is intended only for the use of the individual or entity named above. If you are not the intended recipient, you are hereby notified that reading, disseminating, disclosing, distributing, copying, acting upon or otherwise using the information contained in this facsimile is strictly prohibited. If you have received this information in error, please notify the sender.</p>", poNum);
   
   				oTrans.PushStatusText ("Set up email message", true);
   				// set up the email message (using outlook); saves to sent items, but requires file IO
   				var application = new Microsoft.Office.Interop.Outlook.Application();
   				var message = application.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem) as Microsoft.Office.Interop.Outlook.MailItem;
   				if (message != null)
   				{
   					// set up the email parameters
   					message.To = emailTo;
   					message.Subject = emailSubject;
   					
					   if (needWatermark)
					   {
						   message.CC = "orders@surestep.net";
					   }

   					// outlook requires there to be a physical file.
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

