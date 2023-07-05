/*============================================================================

	Custom code for CustShipForm (Customer Shipment Tracker)

	Created: 07/26/2022
	Author:  Karen S.
	Info:    QuickPrint PackSlip Form Toolbar Buttons

	Changed: 05/02/2023
	By:      Kevin Veldman

============================================================================*/

extern alias Erp_Contracts_BO_CustShip;
extern alias Erp_Contracts_BO_SerialNo;
extern alias Erp_Contracts_BO_PickedOrders;
extern alias Erp_Contracts_BO_Company;
extern alias Erp_Contracts_BO_Customer;

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

	bool appendstring = true;
	
	private void baseToolbarsManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs args)
	{
		if (args.Tool.Key == "CBPrintButton" || args.Tool.Key == "CBEmailButton")
		{
			var server = ssrsServer;

			if (appendstring){
			
				ssrsPrefix += "Production Management/";
				appendstring = false;
			}
		
			var reportName = "PackSlipKV"; // "PackSlip";
			var reportPath = string.Format ("{0}{1}", ssrsPrefix, reportName);
	
			// get the PO Number
			var txtPackNum = (EpiTextBox)csm.GetNativeControlReference ("46567b2e-6bc0-4967-be35-a0ec6843838f");
			var packNum = txtPackNum.Text;	// "150000"
			if (string.IsNullOrEmpty(packNum)) {
				MessageBox.Show ("Please enter a Pack Num");
				return;
			}

			if (args.Tool.Key == "CBPrintButton")
			{
				bool displayReport = true;  //show report in Adobe or Foxit; False to display the pdf to view or save like used to be
					
				if (displayReport == false) {
					var reportUrl = string.Format ("http://{0}/ReportServer?{1}&rs:Format=PDF&Company={2}&PackNum={3}", server, reportPath, company, packNum);
					Process.Start(reportUrl);
				}
				else {
					// set the parameter values and filename
					filename = string.Format(@"{2}PackNum_{0}_{1:yyyyMMdd_HHmmss}.pdf", packNum, DateTime.Now, System.IO.Path.GetTempPath());
						
					//may need to be https depending on the client setup
					var serviceAddress = string.Format("http://{0}/reportserver/ReportExecution2005.asmx", server);
					
					// create the parameters
					var session = (Ice.Core.Session)CustShipForm.Session;
					var parmList = new System.Collections.Specialized.OrderedDictionary();
					parmList.Add("Company", session.CompanyID);
					parmList.Add("PackNum", packNum);
		
					// execute the report
					oTrans.PushStatusText ("Executing Report", true);
					var reportBytes = CodaBears.Epicor.QuickPrint.Reports.GetSSRSReport (serviceAddress, reportPath, parmList);
		
					if (reportBytes == null) { MessageBox.Show ("No report was returned."); }
	
					System.IO.File.WriteAllBytes(filename, reportBytes);
	
					Process proce = new Process();
					proce.StartInfo.FileName = filename;
					proce.Start();

				}
			}
			else 
			{
				//case when len (h.NotifyEMail) = 0 then c.EMailAddress else h.NotifyEMail end as NotifyEmail
				oTrans.PushStatusText ("Get email", true);
				var ShipHeadView = (EpiDataView)oTrans.EpiDataViews["ShipHead"];
				var shipHeadEmail = (string)ShipHeadView.dataView[ShipHeadView.Row]["NotifyEMail"];
				var custNum = (int)ShipHeadView.dataView[ShipHeadView.Row]["CustNum"];

				string emailTo = "";
				using (var svc = WCFServiceSupport.CreateImpl<Erp.Proxy.BO.CustomerImpl>(session, Epicor.ServiceModel.Channels.ImplBase<Erp.Contracts.CustomerSvcContract>.UriPath))
				{

					var custDs = svc.GetByID(custNum);
					emailTo = (string)custDs.Tables["Customer"].Rows[0]["EMailAddress"];
					if (string.IsNullOrEmpty(emailTo)) {
						emailTo = shipHeadEmail;
					}

					oTrans.PushStatusText ("Set up email parameters", true);
					var emailSubject = "Pack Slip";
					var emailBody = "<p>Here is your Packing Slip.</p>";
	
					// set the parameter values and filename
					var filename = string.Format(@"{2}PackSlip_{0}_{1:yyyyMMdd_HHmmss}.pdf", packNum, DateTime.Now, System.IO.Path.GetTempPath());
	
					//THIS MAY NEED TO CHANGE TO HTTPS: DEPENDING ON HOW THE CLIENT IS SETUP
					var serviceAddress = string.Format("http://{0}/reportserver/ReportExecution2005.asmx", server);
				
					// create the parameters
					var parmList = new System.Collections.Specialized.OrderedDictionary();
					parmList.Add("Company", company);
					parmList.Add("PackNum", packNum);
	
					// execute the report
					oTrans.PushStatusText ("Executing Report", true);
					var reportBytes = CodaBears.Epicor.QuickPrint.Reports.GetSSRSReport (serviceAddress, reportPath, parmList);
	
					if (reportBytes == null) 
					{
						MessageBox.Show ("No report was returned.");
					}
					else 
					{						
						oTrans.PushStatusText ("Set up email message", true);
						// set up the email message (using outlook); saves to sent items, but requires file IO
						var application = new Microsoft.Office.Interop.Outlook.Application();
						var message = application.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem) as Microsoft.Office.Interop.Outlook.MailItem;
						
						if (message != null)
						{
							// set up the email parameters
							message.To = emailTo;
							message.Subject = emailSubject;
							message.HTMLBody = emailBody;
		
							// outlook requires there to be a physical file.
							System.IO.File.WriteAllBytes(filename, reportBytes);
							message.Attachments.Add(filename);
							System.IO.File.Delete(filename);
		
							message.ReadReceiptRequested = true;
							message.Recipients.ResolveAll();
							message.Display();
							oTrans.PushStatusText(string.Format ("Created Email to {0}", emailTo), true);
						}
					}
				}
				Cursor.Current = System.Windows.Forms.Cursors.Default;
				//break;

			}
			Cursor.Current = System.Windows.Forms.Cursors.Default;
		}
		

	}

	private void CustShipForm_Load(object sender, EventArgs args)
	{
		// Add Event Handler Code
		var button = new ButtonTool("CBEmailButton");
		baseToolbarsManager.Tools.Add(button);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("CBEmailButton");
		baseToolbarsManager.Tools["CBEmailButton"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["CBEmailButton"].SharedProps.Caption = "Email Pack Slip";
		baseToolbarsManager.Tools["CBEmailButton"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(CodaBears.Epicor.QuickPrint.Properties.Resources.cbi_email.Handle); 
		
		// print button
		var printButton = new ButtonTool("CBPrintButton");
		baseToolbarsManager.Tools.Add(printButton);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("CBPrintButton");
		baseToolbarsManager.Tools["CBPrintButton"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["CBPrintButton"].SharedProps.Caption = "Print Pack Slip";
		baseToolbarsManager.Tools["CBPrintButton"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(CodaBears.Epicor.QuickPrint.Properties.Resources.print.Handle);	

		// get ssrs data
		session = (Ice.Core.Session)CustShipForm.Session;
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

	    // Handle Exited event and display process information.
    private void myProcess_Exited(object sender, System.EventArgs e)
    {
        
		System.IO.File.Delete(filename);
    }

}

/*== CHANGE LOG =============================================================================
	
	2023/05/02: +use "PackSlipKV";

===========================================================================================*/