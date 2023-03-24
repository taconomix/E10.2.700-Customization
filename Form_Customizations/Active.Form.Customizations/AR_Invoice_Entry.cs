/*============================================================================

	Custom code for AR Invoice Entry form

	Created: 08/01/2019
	Author:  Dylan Anderson
	Info:    Customization includes quickprint emails and printed invoices

	Changed: 11/21/2022
	By:      Kevin Veldman

============================================================================*/

extern alias Erp_Contracts_BO_SalesOrder;
extern alias Erp_Contracts_BO_OrderRelSearch;
extern alias Erp_Contracts_BO_ARInvoice;
extern alias Erp_Contracts_BO_ARPromissoryNotes;
extern alias Erp_Contracts_BO_ARInvSearch;
extern alias Erp_Contracts_BO_ARInvcDtlSearch;
extern alias Erp_Contracts_BO_Customer;
extern alias Erp_Contracts_BO_Company;
extern alias Erp_Contracts_BO_Part;
extern alias Microsoft_Office_Interop_Outlook;


using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;
using Erp.UI;
using Ice.Lib.Customization;
using Ice.Lib.ExtendedProps;
using Ice.Lib.Framework;
using Ice.Lib.Searches;
using Ice.UI.FormFunctions;

using System.Drawing;
using Infragistics.Win;
using Infragistics.Win.UltraWinToolbars;
using System.IO;
using System.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;

public class Script {

	string ConnectionString = string.Empty;
	string ssrsPrefix = string.Empty;
	string company = string.Empty;
	string filename = string.Empty;
	string ssrsServer = string.Empty;

	bool bAutoPrint = false;
	const int EMAIL_PREVIEW_MAX = 3;
	string sTestEmail = "";  

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


	private void ARInvoiceForm_Load(object sender, EventArgs args) {

		var button = new ButtonTool("CBEmailButton");
		baseToolbarsManager.Tools.Add(button);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("CBEmailButton");
		baseToolbarsManager.Tools["CBEmailButton"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["CBEmailButton"].SharedProps.Caption = "Email Invoice Group";
		baseToolbarsManager.Tools["CBEmailButton"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(CodaBears.Epicor.QuickPrint.Properties.Resources.cbi_email.Handle);

		// print button
		var printButton = new ButtonTool("CBPrintButton");
		baseToolbarsManager.Tools.Add(printButton);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("CBPrintButton");
		baseToolbarsManager.Tools["CBPrintButton"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["CBPrintButton"].SharedProps.Caption = "Print Invoice Group";
		baseToolbarsManager.Tools["CBPrintButton"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(CodaBears.Epicor.QuickPrint.Properties.Resources.print.Handle);

		// get the company
		var session = (Ice.Core.Session)ARInvoiceForm.Session;
		company = session.CompanyID;
		using (var svc = WCFServiceSupport.CreateImpl<Erp.Proxy.BO.CompanyImpl>(session, Epicor.ServiceModel.Channels.ImplBase<Erp.Contracts.CompanySvcContract>.UriPath)) {

			var companyDs = svc.GetByID(company);
			if (companyDs != null) {
				ssrsServer = (string)companyDs.Tables[0].Rows[0]["ssrsServer_c"];
				ssrsPrefix = (string)companyDs.Tables[0].Rows[0]["ssrsPrefix_c"];
				ConnectionString = (string)companyDs.Tables[0].Rows[0]["sqlConnectionString_c"];
				bAutoPrint = (bool)companyDs.Tables[0].Rows[0]["cbiARInvoiceAutoPrint_c"];
				sTestEmail = (string)companyDs.Tables[0].Rows[0]["cbiARInvoiceTestEmail_c"];
			}
		}
	
		if (ssrsPrefix.Length == 0) MessageBox.Show ("ssrsPrefix cannot be blank or null on the company table.");
		if (ssrsServer.Length == 0) MessageBox.Show ("ssrs Server cannot be blank or null on the company table.");
		if (ConnectionString.Length == 0) MessageBox.Show ("sqlConnectionString cannot be blank or null on the company table.");
	}


	private void baseToolbarsManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs args) {

		if (args.Tool.Key == "CBPrintButton" || args.Tool.Key == "CBEmailButton") {

			// variables to change for each report
			/*
				for SSRS, this is just the server name
				example: cb-epicor10
	
				for SharePoint this must include _vti_bin
				example: sp.team-xxxx.com/_vti_bin
			*/
			var server = ssrsServer;
	
			/*
				for SSRS, this should be just the path and no extension
				example: /xxx/Materials/POForm
	
				for SharePoint, this has to be the FULL URL including the extension
				example: https://sp.team-xxxx.com/bi/Reports/Codabears/Pilot/POForm.rdl
			*/
			var reportPath = string.Format ("{0}{1}", ssrsPrefix + "Finance/", args.Tool.Key == "CBPrintButton" ? "ARInvoice Group" : "ARInvoice");
	
			// get the PO Number
			var txtGroupID = (EpiTextBox)csm.GetNativeControlReference ("c20af09f-9d42-413f-9bea-3d4d27e24c02");
			var groupId = txtGroupID.Text;	
			if (string.IsNullOrEmpty(groupId)) {
				MessageBox.Show ("Please enter a Group");
				return;
			}

			if (args.Tool.Key == "CBPrintButton") {

				// set the parameter values and filename
				filename = string.Format(@"{2}ARGroup_{0}_{1:yyyyMMdd_HHmmss}.pdf", groupId, DateTime.Now, System.IO.Path.GetTempPath());
					
				//may need to be https depending on the client setup
				var serviceAddress = string.Format("http://{0}/reportserver/ReportExecution2005.asmx", server);
				
				// create the parameters
				var parmList = new System.Collections.Specialized.OrderedDictionary();
				parmList.Add("Company", company);
				parmList.Add("GroupID", groupId);
	
				// execute the report
				oTrans.PushStatusText ("Executing Report", true);
				var reportBytes = CodaBears.Epicor.QuickPrint.Reports.GetSSRSReport (serviceAddress, reportPath, parmList);
	
				if (reportBytes == null) { MessageBox.Show ("No report was returned."); }

				System.IO.File.WriteAllBytes(filename, reportBytes);

				Process proce = new Process();
				proce.StartInfo.FileName = filename;
				proce.Start();
			
			} else {

				var iLoopCount = 0;
				bool bDoAutoPrint = bAutoPrint;
				var sCustsWithoutEmail = "";
				
				if (sTestEmail != "") {

					string message = "The AR Remit Test Email Address is set to \"" + sTestEmail + "\". All emails will be sent to this address. The address can be changed, or set to blank, in Company Configuration on the CBI tab. Continue this operation?";
					string caption = "Using AR Remit Test Email Address";
					MessageBoxButtons buttons = MessageBoxButtons.YesNo;
					DialogResult result;

					result = MessageBox.Show(message, caption, buttons);
					if (result == System.Windows.Forms.DialogResult.No) {
						return;
					}
				}
				
				if (bDoAutoPrint == false) {
					MessageBox.Show("The Auto Email option is turned off. Please view the following " + EMAIL_PREVIEW_MAX.ToString() + " emails. The option can be turned on in Company Configuration on the CBI tab.", "Auto Email AP Remit");
				}
				
				var invoiceList = new System.Collections.Generic.List<int>();
				using (var con = new SqlConnection (ConnectionString)) {

					var cmd = new SqlCommand();
					cmd.Connection = con;
					cmd.CommandText = "ReportARGetInvoicesFromGroupId";
					cmd.CommandType = CommandType.StoredProcedure;
					cmd.Parameters.Add("@Company", SqlDbType.Char).Value = company;
					cmd.Parameters.Add("@GroupID", SqlDbType.Char).Value = groupId;
					con.Open();
					var dr = cmd.ExecuteReader();

					if (dr != null)	{

						while (dr.Read()) {
							invoiceList.Add(dr.GetInt32 (dr.GetOrdinal ("InvoiceNum")));
						}

						dr.Close();
					}
				}
				
				// check each invoice for existing email, give option to stop processing
				foreach (var invoiceNum in invoiceList) {

					Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
					oTrans.PushStatusText ("Set report variables", true);
	
					oTrans.PushStatusText ("Get customer's email", true);
					string emailTo = "";
					string custName = "";
					string PONum = "";

					DateTime shipDate;
					using (var conn = new SqlConnection (ConnectionString)) {

						var cmd = new SqlCommand();
						cmd.Connection = conn;
						cmd.CommandText = "ReportARInvoiceGetHeader";
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.Add("@Company", SqlDbType.Char).Value = company;
						cmd.Parameters.Add("@InvoiceNum", SqlDbType.Int).Value = invoiceNum;
						conn.Open();
						var dr = cmd.ExecuteReader();
						if (dr != null) {

							if (dr.Read()) {
								if (!dr.IsDBNull(dr.GetOrdinal("SoldToEmailAddress"))) emailTo = dr.GetString (dr.GetOrdinal ("SoldToEmailAddress"));
								if (!dr.IsDBNull(dr.GetOrdinal("CustomerName"))) custName = dr.GetString (dr.GetOrdinal ("CustomerName"));
								if (!dr.IsDBNull(dr.GetOrdinal("PONum"))) PONum = dr.GetString (dr.GetOrdinal ("PONum"));
							}
	
							dr.Close();
						}
					}
					
					if (emailTo == "") {
						string message = "Some of your selections do not have an Email address. Those invoices will be generated as a PDF. Continue this operation?";
						string caption = "Email Address Missing";
						MessageBoxButtons buttons = MessageBoxButtons.YesNo;
						DialogResult result;
						
						result = MessageBox.Show(message, caption, buttons);
						if (result == System.Windows.Forms.DialogResult.No) {
							return;
						}
						
						break;
					}
				}
		
				foreach (var invoiceNum in invoiceList) {

					Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
					oTrans.PushStatusText ("Set report variables", true);
	
					oTrans.PushStatusText ("Get customer's email", true);
					string emailTo = "";
					string custName = "";
					string PONum = "";
					string custID = "";
					bool credMemo = false;
					string invCred = "";
					DateTime shipDate;
					using (var conn = new SqlConnection (ConnectionString)) {

						var cmd = new SqlCommand();
						cmd.Connection = conn;
						cmd.CommandText = "ReportARInvoiceGetHeader";
						cmd.CommandType = CommandType.StoredProcedure;
						cmd.Parameters.Add("@Company", SqlDbType.Char).Value = company;
						cmd.Parameters.Add("@InvoiceNum", SqlDbType.Int).Value = invoiceNum;
						conn.Open();
						var dr = cmd.ExecuteReader();
						if (dr != null) {

							if (dr.Read()) {
								if (!dr.IsDBNull(dr.GetOrdinal("SoldToEmailAddress"))) emailTo = dr.GetString (dr.GetOrdinal ("SoldToEmailAddress"));
								if (!dr.IsDBNull(dr.GetOrdinal("CustomerName"))) custName = dr.GetString (dr.GetOrdinal ("CustomerName"));
								if (!dr.IsDBNull(dr.GetOrdinal("PONum"))) PONum = dr.GetString (dr.GetOrdinal ("PONum"));
								if (!dr.IsDBNull(dr.GetOrdinal("CustID"))) custID = dr.GetString (dr.GetOrdinal ("CustID"));
								if (!dr.IsDBNull(dr.GetOrdinal("CreditMemo"))) credMemo = dr.GetBoolean (dr.GetOrdinal ("CreditMemo"));
							}
							
							invCred = (!credMemo)?("Invoice"):("Credit Memo");
							dr.Close();
						}
					}
					
					if (emailTo == "" && custID != "HANGER" && custID != "MID46635" && custID != "ONL46635") {

						if (sCustsWithoutEmail != "") sCustsWithoutEmail += ",";
						
						sCustsWithoutEmail += invoiceNum.ToString();
						
						if (invoiceList.Count <= EMAIL_PREVIEW_MAX) iLoopCount++;
					
					} else {
						
						if (sTestEmail != "") emailTo = sTestEmail;
						
						if (bDoAutoPrint == false && (iLoopCount == invoiceList.Count || iLoopCount == EMAIL_PREVIEW_MAX))
						{
							string message = "Emails have been generated for you to preview. Click YES to automatically SEND ALL Emails.";
							string caption = "AR Invoice Email Preview";
							MessageBoxButtons buttons = MessageBoxButtons.YesNo;
							DialogResult result;
							
							result = MessageBox.Show(message, caption, buttons);
							if (result == System.Windows.Forms.DialogResult.No)
							{
								return;
							}
							
							sCustsWithoutEmail = "";
							bDoAutoPrint = true;
							continue;
						}
						
						iLoopCount++;
						
						oTrans.PushStatusText ("Set up email parameters", true);
						var emailSubject = string.Format ("Surestep, LLC {2} # {1} for {0}", custName, invoiceNum, invCred);
						var emailBody = string.Format("<p>Surestep {1} # {0} is attached. Please remit payment at your earliest convenience.</p><p>Thank you for your business - we appreciate it very much!</p><p>Sincerely,</p><p>Surestep Accounts Receivable</p><p>ar@surestep.net</p><p>Important Notice:</p><p>This message is intended only for the use of the individual or entity to which it is addressed.  The documents in this e-mail or facsimile transmission may contain confidential health information that is privileged and legally protected from disclosure by the Health Insurance Portability and Accountability Act (HIPAA).  This information is intended only for the use of the individual or entity named above.  If you are not the intended recipient, you are hereby notified that reading, disseminating, disclosing, distributing, copying, acting upon or otherwise using the information contained in this facsimile is strictly prohibited.  If you have received this information in error, please notify the sender.</p>", invoiceNum, invCred);
	
						// set the parameter values and filename
						var filename = string.Format(@"{2}ARInvoice_{0}_{1:yyyyMMdd_HHmmss}.pdf", invoiceNum, DateTime.Now, System.IO.Path.GetTempPath());
						filename = (!credMemo)?(filename):(filename.Replace("ARInvoice_","ARCredit_")); //Change File to read "Credit" instead of "Invoice" --KV-20210706-180330
		
						//THIS MAY HAVE TO BE HTTPS DEPENDING ON HOW THE CLIENT IS SETUP
						var serviceAddress = string.Format("http://{0}/reportserver/ReportExecution2005.asmx", server);
					
						// create the parameters
						var parmList = new System.Collections.Specialized.OrderedDictionary();
						parmList.Add("Company", company);
						parmList.Add("InvoiceNum", invoiceNum.ToString());
		
						// execute the report
						oTrans.PushStatusText ("Executing Report", true);
						var reportBytes = CodaBears.Epicor.QuickPrint.Reports.GetSSRSReport (serviceAddress, reportPath, parmList);
		
						if (reportBytes == null) { 
							MessageBox.Show ("No report was returned."); 
						} else if (!string.IsNullOrEmpty(emailTo) && custID != "HANGER" && custID != "MID46635" && custID != "ONL46635") {

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
								message.HTMLBody = emailBody; // + message.HTMLBody;
			
								// outlook requires there to be a physical file.
								System.IO.File.WriteAllBytes(filename, reportBytes);
								message.Attachments.Add(filename);
								System.IO.File.Delete(filename);
			
								//message.ReadReceiptRequested = true;
								message.Recipients.ResolveAll();
								
								if (bDoAutoPrint || iLoopCount > invoiceList.Count || iLoopCount > EMAIL_PREVIEW_MAX) {
									message.Send();
								} else {
									message.Display();
								}
								
								oTrans.PushStatusText(string.Format ("Created Email to {0}", emailTo), true);
							}
						}
					}
				}
				
				if (sCustsWithoutEmail != "") {
					filename = string.Format(@"{2}ARInvoice_{0}_{1:yyyyMMdd_HHmmss}.pdf", "NoEmails", DateTime.Now, System.IO.Path.GetTempPath());
					var serviceAddress = string.Format("http://{0}/reportserver/ReportExecution2005.asmx", server);
					reportPath = string.Format ("{0}{1}", ssrsPrefix + "Finance/", "ARInvoice_NoEmail");
					
					// create the parameters
					var parmList = new System.Collections.Specialized.OrderedDictionary();
					parmList.Add("Company", company);
					parmList.Add("invoiceNum", sCustsWithoutEmail);
					
					oTrans.PushStatusText("Executing Report", true);
					
					var reportBytes = CodaBears.Epicor.QuickPrint.Reports.GetSSRSReport(serviceAddress, reportPath, parmList);
					
					if (reportBytes == null) {
						MessageBox.Show("No report was returned.");
						return;
					}
					
					System.IO.File.WriteAllBytes(filename, reportBytes);
					
					Process proce = new Process();
					proce.StartInfo.FileName = filename;
					proce.Start();
				}
			}
			Cursor.Current = System.Windows.Forms.Cursors.Default;
		}
	}	

    // Handle Exited event and display process information.
    private void myProcess_Exited(object sender, System.EventArgs e) {
		System.IO.File.Delete(filename);
    }
}






/*==================================================================================================================================================

	Change Log:

		User:  Date:     Changes:
		KV     20221121  Added 'Using ... Outlook'
		                 Added lines to try using account noreply@surestep.net when available

==================================================================================================================================================*/
