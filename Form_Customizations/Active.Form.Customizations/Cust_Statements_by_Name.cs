// **************************************************
// Custom code for CustomerStatementsQPForm
// Created: 4/17/2017 1:26:35 PM
// **************************************************

extern alias Erp_Contracts_BO_Company;
extern alias Erp_Contracts_BO_Customer;

using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;
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
using Ice.BO;
using Ice.Adapters;




public class Script
{
	string ConnectionString = string.Empty;
	string ssrsPrefix = string.Empty;
	string company = string.Empty;
	string filename = string.Empty;
	string ssrsServer = string.Empty;
	Session session;


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

		this.btnSelectNone.Click += new System.EventHandler(this.btnSelectNone_Click);
		this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
		this.cboTerritory.TextChanged += new System.EventHandler(this.cboTerritory_TextChanged);
		this.cboNameInput.TextChanged += new System.EventHandler(this.cboNameInput_TextChanged);
		this.FindBy.TextChanged += new System.EventHandler(this.FindBy_TextChanged);
		// End Wizard Added Custom Method Calls
	}


	public void DestroyCustomCode()
	{
		// ** Wizard Insert Location - Do not delete 'Begin/End Wizard Added Object Disposal' lines **
		// Begin Wizard Added Object Disposal

		this.baseToolbarsManager.ToolClick -= new Infragistics.Win.UltraWinToolbars.ToolClickEventHandler(this.baseToolbarsManager_ToolClick);
		this.btnSelectNone.Click -= new System.EventHandler(this.btnSelectNone_Click);
		this.btnSelectAll.Click -= new System.EventHandler(this.btnSelectAll_Click);
		this.cboTerritory.TextChanged -= new System.EventHandler(this.cboTerritory_TextChanged);
		this.cboNameInput.TextChanged -= new System.EventHandler(this.cboNameInput_TextChanged);
		this.FindBy.TextChanged -= new System.EventHandler(this.FindBy_TextChanged);
		// End Wizard Added Object Disposal

		// Begin Custom Code Disposal

		// End Custom Code Disposal
	}


	private void UD01Form_Load(object sender, EventArgs args)
	{
		// Add Event Handler Code
		var button = new ButtonTool("CBEmailButton");
		baseToolbarsManager.Tools.Add(button);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("CBEmailButton");
		baseToolbarsManager.Tools["CBEmailButton"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["CBEmailButton"].SharedProps.Caption = "Email Statement of Account";
		baseToolbarsManager.Tools["CBEmailButton"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(CodaBears.Epicor.QuickPrint.Properties.Resources.cbi_email.Handle); 

		// print button
		var printButton = new ButtonTool("CBPrint");
		baseToolbarsManager.Tools.Add(printButton);
		baseToolbarsManager.Toolbars["Standard Tools"].Tools.AddTool("CBPrint");
		baseToolbarsManager.Tools["CBPrint"].SharedProps.DisplayStyle = ToolDisplayStyle.ImageOnlyOnToolbars;
		baseToolbarsManager.Tools["CBPrint"].SharedProps.Caption = "Print Statement of Account";
		baseToolbarsManager.Tools["CBPrint"].SharedProps.AppearancesSmall.Appearance.Image = Bitmap.FromHicon(CodaBears.Epicor.QuickPrint.Properties.Resources.print.Handle);

		// get the company
		session = (Ice.Core.Session)UD01Form.Session;
		company = session.CompanyID;
		using (var svc = WCFServiceSupport.CreateImpl<Erp.Proxy.BO.CompanyImpl>(session, Epicor.ServiceModel.Channels.ImplBase<Erp.Contracts.CompanySvcContract>.UriPath))
		{
			var companyDs = svc.GetByID(company);
			if (companyDs != null) {
				ssrsServer = (string)companyDs.Tables[0].Rows[0]["ssrsServer_c"];
				ssrsPrefix = (string)companyDs.Tables[0].Rows[0]["ssrsPrefix_c"];
				ConnectionString = (string)companyDs.Tables[0].Rows[0]["sqlConnectionString_c"];
				bAutoPrint = (bool)companyDs.Tables[0].Rows[0]["quickprintCustomerStatementAutoPrint_c"];
				sTestEmail = (string)companyDs.Tables[0].Rows[0]["quickprintCustomerStatementTestEmail_c"];
			}
		}
	
		if (ssrsPrefix.Length == 0) { MessageBox.Show ("ssrsPrefix cannot be blank or null on the company table."); }
		if (ssrsServer.Length == 0) { MessageBox.Show ("ssrs Server cannot be blank or null on the company table."); }
		if (ConnectionString.Length == 0) { MessageBox.Show ("sqlConnectionString cannot be blank or null on the company table."); }

		//set begin and end dates
		var today = DateTime.Today;
		var month = new DateTime(today.Year, today.Month, 1);       
		var first = month.AddMonths(-1);
		var last = month.AddDays(-1);
		//rdoDueDate.Checked = true;
		tdtBeginDate.Value = first;
		tdtEndDate.Value = last;

		//fill the territory dropdown
		SearchOnSalesTerAdapterFillDropDown();

		//fill the customers grid
		FillTheCustomerGrid();

		// format the grid
		custStatementsGrid.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
		custStatementsGrid.DisplayLayout.Bands[0].Override.CellAppearance.TextTrimming = TextTrimming.EllipsisCharacter;
		custStatementsGrid.DisplayLayout.Bands[0].Columns["Customer_Company"].Hidden = true;
		custStatementsGrid.DisplayLayout.Bands[0].Columns["Customer_CustID"].AutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand;
		custStatementsGrid.DisplayLayout.Bands[0].Columns["Customer_CustNum"].Hidden = true;
		custStatementsGrid.DisplayLayout.Bands[0].Columns["Customer_CreditHold"].AutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand;
		custStatementsGrid.DisplayLayout.Bands[0].Columns["Calculated_Balance"].Format = "$#,##0.00";
		custStatementsGrid.DisplayLayout.Bands[0].Columns["Calculated_Balance"].CellAppearance.TextHAlign = Infragistics.Win.HAlign.Right;
		custStatementsGrid.DisplayLayout.Bands[0].Columns["Calculated_Balance"].AutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand;
		//custStatementsGrid.DisplayLayout.Bands[0].Columns["Customer_EMailAddress"].Header.Caption = "EMail";
		
		EmailMessage.Text = "<p>Dear ~Name~</p>\r\n<p>Attached is your Statement of Account. Please note- All credit and debit card payments must be made via the <a href=https://connect.ebizcharge.net/surestep>Surestep Customer Portal</a>. </p>\r\n<p>Thank you for your business - we appreciate it very much!</p>\r\n<p>Sincerely,</p>\r\n<p>Surestep Accounts Receivable</p>\r\n<p><a href=mailto:ar@surestep.net>ar@surestep.net</a></p>";
		EmailMessageLabel.Text = "Email message:\r\n~Name~ is replaced with the AttnName as shown in the grid"; 
		
		//AgeBy.SelectedText = "Due Date";
		//FindBy.SelectedValue = 0; //.SelectedText = "Earliest open invoice";

		var lblDesc = (EpiLabel)csm.GetNativeControlReference ("5ee3c271-ff03-47d1-bc6b-44c02b5258fb");
		lblDesc.Text = "Invoice message:\r\nAppears on the PDF";

		UpdateDateRangeControls();
		tdtBeginDate.ReadOnly = true;
		tdtEndDate.ReadOnly = true;
	}


	private void baseToolbarsManager_ToolClick(object sender, Infragistics.Win.UltraWinToolbars.ToolClickEventArgs args)
	{
		if (args.Tool.Key == "CBEmailButton" || args.Tool.Key == "CBPrint") 
		{
			if (ssrsPrefix.Length == 0) {
				MessageBox.Show ("ssrsPrefix_c cannot be blank or null on the company table.");
				return;
			}
			
			if (custStatementsGrid.Selected.Rows.Count == 0)
			{
				MessageBox.Show("No Customers Selected. Please choose at least one.");
				return;
			}
			
			if (AgeBy.SelectedText == "")
			{
				MessageBox.Show ("AgeBy cannot be blank.");
				return;
			}
			
			if (FindBy.SelectedText == "")
			{
				MessageBox.Show ("Find Invoices by cannot be blank.");
				return;
			}
	
			Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
			oTrans.PushStatusText ("Set report variables", true);
		
			//may need to be https depending on the client setup
			var serviceAddress = string.Format("http://{0}/reportserver/ReportExecution2005.asmx", ssrsServer);
			var ageByType = "";
			switch (AgeBy.SelectedText) 
			{
				case "Due Date":
	   			ageByType = "DueDate";
	   			break;
				case "Invoice Date":
	   			ageByType = "InvoiceDate";
					break;
				default:
   				ageByType = "ApplyDate";
   				break;
			}
	
   		var txtMsg = (EpiTextBox)csm.GetNativeControlReference ("368fd000-b055-4183-9d70-25d986650bd9");
	   	var findByEarliestOpenInvoice = FindBy.SelectedText == "Earliest open invoice";
	   	var printCustNums = new System.Collections.Generic.List<string>();
	   	var emailCustNums = new System.Collections.Generic.List<string>();

	   	// figure out which are printed and which are emailed
	   	foreach (var row in custStatementsGrid.Selected.Rows) 
			{
				if (!chkZeroBalance.Checked && ((decimal)row.Cells["Calculated_Balance"].Value) == 0) 
			   {
				   continue;
			   }
				
				var custNum = row.Cells["Customer_CustNum"].Value.ToString();
				if (args.Tool.Key == "CBEmailButton" && !string.IsNullOrEmpty (row.Cells["Calculated_AttnEmail"].Value.ToString()))
				{
					emailCustNums.Add (custNum);
				}
				else 
				{
					printCustNums.Add (custNum);
				}
			}

			if (emailCustNums.Count == 0 && printCustNums.Count == 0)
			{
				MessageBox.Show ("Please select at least one valid customer.");
				return;
			}
			
			switch (args.Tool.Key) {
				case "CBPrint":
					CreatePrintStatements(serviceAddress, printCustNums.ToArray(), ageByType, findByEarliestOpenInvoice, txtMsg.Text);
					break;
	
				case "CBEmailButton":
					var custId = "";	
					var iLoopCount = 0;
					bool bDoAutoPrint = bAutoPrint;
	
					if (sTestEmail != "")
					{
						string message = string.Format ("The Quick Print Test Email Address is set to \"{0}\". All emails will be sent to this address. The address can be changed, or set to blank, in Company Configuration on the CBI tab. Continue this operation?", sTestEmail);
					    string caption = "Using Quick Print Test Email Address";
						MessageBoxButtons buttons = MessageBoxButtons.YesNo;
						DialogResult result;
					
						result = MessageBox.Show(message, caption, buttons);
						if (result == System.Windows.Forms.DialogResult.No)
						{
							return;
						}
					}
		
					if (bDoAutoPrint == false)
					{
						MessageBox.Show(string.Format ("The Auto Email option is turned Off. Please view the following {0} emails. The option can be turned On in Company Configuration on the CBI tab.", EMAIL_PREVIEW_MAX), "Auto Email Customer Statements");
					}
	
					oTrans.PushStatusText ("Get email", true);
	
					// verify that selected customers have email addresses
					if (printCustNums.Count > 0)
					{
						string message = "Some of your selections do not have an Email Address. Those Statements will be generated as a PDF. Continue this operation?";
					    string caption = "Email Address Missing";
						MessageBoxButtons buttons = MessageBoxButtons.YesNo;
						DialogResult result;
					
						result = MessageBox.Show(message, caption, buttons);
						if (result == System.Windows.Forms.DialogResult.No)
						{
							return;
						}
					}
      	   	   
      	   	   var emailReportPath = string.Format ("{0}Finance/Customer Statement", ssrsPrefix);
      	   	   var emailParamList = new System.Collections.Specialized.OrderedDictionary();
      	   	   emailParamList.Add("Company", company.ToString());
      	   	   emailParamList.Add("StartDate", findByEarliestOpenInvoice ? null : tdtBeginDate.Text);
      	   	   emailParamList.Add("EndDate", findByEarliestOpenInvoice ? null : tdtEndDate.Text);
      	   	   emailParamList.Add("AgeBy", ageByType);
      	   	   emailParamList.Add("UserMessage", txtMsg.Text);			
      	   	   emailParamList.Add("CustNum", "");
					emailParamList.Add ("ShowOnlyOpenInvoices", ShowOnlyOpenInvoices.Checked.ToString());

      	   	   foreach (var row in custStatementsGrid.Rows)
					{
						var custNum = row.Cells["Customer_CustNum"].Value.ToString();
						if (!emailCustNums.Contains (custNum)) continue;
						string emailTo = string.IsNullOrEmpty (sTestEmail) ? row.Cells["Calculated_AttnEmail"].Value.ToString() : sTestEmail;	

						if (bDoAutoPrint == false && (iLoopCount == emailCustNums.Count || iLoopCount == EMAIL_PREVIEW_MAX))
						{
							string message = "EMails have been generated for you to preview. Click YES to automatically SEND ALL EMails.";
						    string caption = "Quick Print Email Preview";
							MessageBoxButtons buttons = MessageBoxButtons.YesNo;
							DialogResult result;
						
							result = MessageBox.Show(message, caption, buttons);
							if (result == System.Windows.Forms.DialogResult.No)
							{
								return;
							}

							bDoAutoPrint = true;
						}

						iLoopCount++;
						oTrans.PushStatusText ("Set up email parameters", true);
						var emailSubject = "Statement of Account"; 
						var emailBody = EmailMessage.Text.Trim().Replace ("~Name~", row.Cells["Calculated_AttnName"].Value.ToString());
	
						// set the parameter values and filename
						filename = string.Format(@"{2}CustStatement_{0}_{1:yyyyMMdd_HHmmss}.pdf", custNum, DateTime.Now, System.IO.Path.GetTempPath());
						emailParamList["CustNum"] = custNum;
		
						// execute the report
						oTrans.PushStatusText ("Executing Report", true);
						var reportBytes = CodaBears.Epicor.QuickPrint.Reports.GetSSRSReport (serviceAddress, emailReportPath, emailParamList);
		
						if (reportBytes == null) 
						{
							MessageBox.Show ("No report was returned.");
						}
						else 
						{
							oTrans.PushStatusText ("Set up email message", true);
							// parmListset up the email message (using outlook); saves to sent items, but requires file IO
							var application = new Microsoft.Office.Interop.Outlook.Application();
							var message = application.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem) as Microsoft.Office.Interop.Outlook.MailItem;
							//var mailitem as Microsoft.Office.Interop.Outlook._MailItem;
							
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
			
								message.ReadReceiptRequested = false;
								message.Recipients.ResolveAll();

								if (bDoAutoPrint) // || iLoopCount > custStatementsGrid.Selected.Rows.Count || iLoopCount > EMAIL_PREVIEW_MAX)
								{
									message.Send();
								}
								else
								{
									message.Display();
								}
								
								oTrans.PushStatusText(string.Format ("Created Email to {0}", emailTo), true);
							}
						}
					}

	  			  if (printCustNums.Count > 0)
					{
						CreatePrintStatements(serviceAddress, printCustNums.ToArray(), ageByType, findByEarliestOpenInvoice, txtMsg.Text);
					}
					break;
			}
			
			Cursor.Current = System.Windows.Forms.Cursors.Default;
		}
	}
	
	private void CreatePrintStatements(string serviceAddress, string[] custNums, string ageByType, bool findByEarliestOpenInvoice, string reportMessage) 
	{
		// set the parameter values and filename
		var printReportPath = string.Format ("{0}Finance/Customer Statement Group", ssrsPrefix);
		var printParamList = new System.Collections.Specialized.OrderedDictionary();
		printParamList.Add("Company", company.ToString());
		printParamList.Add("CustomerNumbers", string.Join (",", custNums));
		printParamList.Add("StartDate", findByEarliestOpenInvoice ? null : tdtBeginDate.Text);
		printParamList.Add("EndDate", findByEarliestOpenInvoice ? null : tdtEndDate.Text);
		printParamList.Add("AgeBy", ageByType);
		printParamList.Add("UserMessage", reportMessage);
		printParamList.Add ("ShowOnlyOpenInvoices", ShowOnlyOpenInvoices.Checked.ToString());			

		filename = string.Format(@"{1}CustStatements_{0:yyyyMMdd_HHmmss}.pdf", DateTime.Now, System.IO.Path.GetTempPath());
		
		oTrans.PushStatusText ("Executing Report", true);

		var reportBytes = CodaBears.Epicor.QuickPrint.Reports.GetSSRSReport (serviceAddress, printReportPath, printParamList);

		if (reportBytes == null) { MessageBox.Show ("No report was returned."); return;}

		System.IO.File.WriteAllBytes(filename, reportBytes);

		var proce = new Process();
		proce.StartInfo.FileName = filename;
		proce.Start();
	}

    // Handle Exited event and display process information.
    private void myProcess_Exited(object sender, System.EventArgs e)
    {
		System.IO.File.Delete(filename);
    }

	private void btnSelectNone_Click(object sender, System.EventArgs args)
	{
		custStatementsGrid.Selected.Rows.Clear();
	}

	private void btnSelectAll_Click(object sender, System.EventArgs args)
	{
		custStatementsGrid.Selected.Rows.AddRange ((Infragistics.Win.UltraWinGrid.UltraGridRow[])custStatementsGrid.Rows.All);
	}

	private void FillTheCustomerGrid()
	{
		//fill the customers grid
		using (var dqa = new DynamicQueryAdapter(oTrans))
		{
   		dqa.BOConnect();
   		QueryExecutionDataSet qeds = dqa.GetQueryExecutionParametersByID("CustomerStatementProd"); //("CustomerByTerritory");
   		qeds.ExecutionParameter.Clear();
   
      	if (this.cboNameInput.Text.ToString().Length > 0)
   		{
   			qeds.ExecutionParameter.AddExecutionParameterRow("CustName", this.cboNameInput.Value.ToString().ToUpper(), "nvarchar", false, Guid.Empty,"A");
   		}
/*
   		else if (this.cboTerritory.Text.ToString() != "<All>")
   		{
   			qeds.ExecutionParameter.AddExecutionParameterRow("Terr", this.cboTerritory.Value.ToString(), "nvarchar", false, Guid.Empty,"A");
   		}

   		dqa.ExecuteByID("CustomerByTerritory", qeds); */
   		dqa.ExecuteByID("CustomerStatementProd", qeds);
   		custStatementsGrid.DataSource = dqa.QueryResults.Tables["Results"];		
		}
	}

	private void SearchOnSalesTerAdapterFillDropDown()
	{
		// Wizard Generated Search Method
		bool recSelected;
		string whereClause = string.Empty;
		System.Data.DataSet dsSalesTerAdapter = Ice.UI.FormFunctions.SearchFunctions.listLookup(this.oTrans, "SalesTerAdapter", out recSelected, false, whereClause);

        DataRow newRow = dsSalesTerAdapter.Tables[0].NewRow();
        newRow["TerritoryID"] = "##";
        newRow["TerritoryDesc"] = "<All>";
        dsSalesTerAdapter.Tables[0].Rows.Add(newRow);
        DataTable dt = dsSalesTerAdapter.Tables[0];
        dt.DefaultView.Sort = "TerritoryID";

		if (recSelected || !recSelected)
		{
			// Set EpiUltraCombo Properties
			this.cboTerritory.ValueMember = "TerritoryID";
			this.cboTerritory.DataSource = dt; 
			this.cboTerritory.DisplayMember = "TerritoryDesc";
			string[] fields = new string[] {"TerritoryDesc"};
			this.cboTerritory.SetColumnFilter(fields);
		}
	}


	private void cboTerritory_TextChanged(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
		FillTheCustomerGrid();
	}

	private void cboNameInput_TextChanged(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
		FillTheCustomerGrid();
	}

	private void FindBy_TextChanged(object sender, System.EventArgs args)
	{
		// ** Place Event Handling Code Here **
		UpdateDateRangeControls();
	}
	
	private void UpdateDateRangeControls() 
	{
		bool useDates = !(FindBy.SelectedText.ToLower() != "earliest open invoice");
		tdtBeginDate.ReadOnly = useDates;
		tdtEndDate.ReadOnly = useDates;
	}
}



