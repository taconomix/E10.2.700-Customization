// **************************************************
// Custom code for RQForm
// Created: 11/25/2016 3:08:47 PM
// **************************************************

extern alias Erp_Contracts_BO_JobEntry;
extern alias Erp_Contracts_BO_JobAsmSearch;
extern alias Erp_Contracts_BO_JobMtlSearch;
extern alias Erp_Contracts_BO_JobOperSearch;

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

public class Script
{
	// ** Wizard Insert Location - Do Not Remove 'Begin/End Wizard Added Module Level Variables' Comments! **
	// Begin Wizard Added Module Level Variables **

	private EpiBaseAdapter oTrans_adapter;
	// End Wizard Added Module Level Variables **

	// Add Custom Module Level Variables Here **
	EpiButton okButton;
	EpiButton submitButton;
	public void InitializeCustomCode()
	{
		// ** Wizard Insert Location - Do not delete 'Begin/End Wizard Added Variable Initialization' lines **
		// Begin Wizard Added Variable Initialization
		okButton = (EpiButton)csm.GetNativeControlReference("711a58ec-0b5b-453a-8ee3-ac318ab572d3");
		submitButton = (EpiButton)csm.GetNativeControlReference("2990b4f2-45f7-4155-ad6a-80fefb2c3a8e");
		
		this.ReportQty_Column.ColumnChanged += new DataColumnChangeEventHandler(this.ReportQty_AfterFieldChange);
		
		this.oTrans_adapter = ((EpiBaseAdapter)(this.csm.TransAdaptersHT["oTrans_adapter"]));
		
		// End Wizard Added Variable Initialization

		// Begin Wizard Added Custom Method Calls

		this.okButton.Click += new System.EventHandler(this.okButton_Click);
		
		// End Wizard Added Custom Method Calls
	}

	public void DestroyCustomCode()
	{
		// ** Wizard Insert Location - Do not delete 'Begin/End Wizard Added Object Disposal' lines **
		// Begin Wizard Added Object Disposal

		this.ReportQty_Column.ColumnChanged -= new DataColumnChangeEventHandler(this.ReportQty_AfterFieldChange);
		this.oTrans_adapter = null;
		this.okButton.Click -= new System.EventHandler(this.okButton_Click);
		// End Wizard Added Object Disposal

		// Begin Custom Code Disposal

		// End Custom Code Disposal
	}

	private void SearchOnJobOperSearchAdapterFillDropDown()
	{
		// Wizard Generated Search Method
		// You will need to call this method from another method in custom code
		// For example, [Form]_Load or [Button]_Click
		EpiDataView edvRQ = oTrans.EpiDataViews["RQ"] as EpiDataView;
		bool recSelected;

		string whereClause = string.Format("JobNum = '{0}' and AssemblySeq = {1}", 
				edvRQ.dataView[edvRQ.Row]["JobNum"],  
				edvRQ.dataView[edvRQ.Row]["AssemblySeq"]);

		System.Data.DataSet dsJobOperSearchAdapter = Ice.UI.FormFunctions.SearchFunctions.listLookup(this.oTrans, "JobOperSearchAdapter", out recSelected, false, whereClause);
		if (recSelected)
		{
			// Set EpiUltraCombo Properties
			this.cboJobOper.ValueMember = "OprSeq";
			this.cboJobOper.DataSource = dsJobOperSearchAdapter;
			this.cboJobOper.DisplayMember = "OprSeq";
			string[] fields = new string[] {
					"OprSeq", "OpCode", "OpCodeOpDesc"};
			this.cboJobOper.SetColumnFilter(fields);
		}
	}

	private void ReportQty_AfterFieldChange(object sender, DataColumnChangeEventArgs args)
	{
		// ** Argument Properties and Uses **
		// args.Row["FieldName"]
		// args.Column, args.ProposedValue, args.Row
		// Add Event Handler Code
		switch (args.Column.ColumnName)
		{
			case "JobNum":
				SearchOnJobOperSearchAdapterFillDropDown();
				break;
			case "AssemblySeq":
				SearchOnJobOperSearchAdapterFillDropDown();
				break;
		}
	}


	private void RQForm_Load(object sender, EventArgs args)
	{
		
	}

	private void okButton_Click(object sender, System.EventArgs args)
	{
		
		EpiDataView edvRQ = oTrans.EpiDataViews["RQ"] as EpiDataView;
		
		string jobNum = (string)edvRQ.dataView[edvRQ.Row]["JobNum"];
		
		int oprSeq = (int)edvRQ.dataView[edvRQ.Row]["OprSeq"];
		

		string laborNoteTxt = LaborNotes.Text;
		

		JobEntryAdapter jobEntry = new JobEntryAdapter(this.oTrans);
		

		jobEntry.BOConnect();
		
		jobEntry.GetByID(jobNum);
		
		Erp.BO.JobEntryDataSet jobEntryData = jobEntry.JobEntryData;
		

		foreach(DataRow row in jobEntryData.Tables["JobOper"].Rows)
		{
			if((int)row["OprSeq"] == oprSeq)
			{
				row["CommentText"] += laborNoteTxt +  Environment.NewLine;
				row["RowMod"] = "U";
				break;
			}
		}

		jobEntry.Update();
		jobEntry.Dispose();

		oTrans.NotifyAll();
		((EpiNumericEditor)csm.GetNativeControlReference("94e152f3-dbc7-4180-a2c5-ea03f8d12aa2")).Focus();

		submitButton.PerformClick();
	}
}

