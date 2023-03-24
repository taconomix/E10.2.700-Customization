/*== Save Job Traveler =======================================================

	Library:  libAutoRpt
	Function: saveJobTrav
	Version:  v1.0.0

		Request Parameters: 1
			JobNum: System.Int32

		Response Parameters: None

	Created: 03/09/2023
	Changed: 

		--Kevin Veldman <KVeldman@Hanger.com>
============================================================================*/


// QuickPrint Parameters
	var ssrsPrefix = Db.Company.FirstOrDefault(x => x.Name.Contains("SURESTEP"))["ssrsPrefix_c"];
	var svcAddress = "http://EPAPP02/reportserver/ReportExecution2005.asmx";

	var rptPath = string.Format( "{0}{1}{2}", ssrsPrefix, "Sales Management/", "JobTravQP" );

	var rptParms = new System.Collections.Specialized.OrderedDictionary();
	  rptParms.Add( "Company", "SS" );
	  rptParms.Add( "JobNum", JobNum );


// Generate QuickPrint Report
	var rptBytes = CodaBears.Epicor.QuickPrint.Reports.GetSSRSReport(svcAddress, rptPath, rptParms);


// Set PDF Name, Save Report
	string pdf = @"C:\EpicorData\Companies\SS\Temp\autoPrint\" + JobNum + ".pdf";
	System.IO.File.WriteAllBytes( pdf, rptBytes );



/*== CHANGE LOG ==============================================================

	03/09/2023: 
	
============================================================================*/

	/*__ Added Usings ____________________________________________________

		using CodaBears.Epicor.QuickPrint;
		using PdfSharp.Drawing;
		using PdfSharp.Pdf.IO;
	____________________________________________________________________*/

	/*__ Reference _______________________________________________________

		QuickPrint and PdfSharp usage:
			https://github.com/taconomix/E10-SS-FormCustomizations/blob/main/Active-SalesOrderEntry-20221102.cs
	____________________________________________________________________*/