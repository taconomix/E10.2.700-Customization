// Warning: Some assembly references could not be resolved automatically. This might lead to incorrect decompilation of some parts,
// for ex. property getter/setter access. To get optimal decompilation results, please manually add the missing references to the list of loaded assemblies.

// /home/kve/Documents/Dienen Misc/Epicor/CodaBears_Custom_DLLs/CodaBears.Epicor10.2.dll
// CodaBears.Epicor10.2, Version=10.2.7835.27171, Culture=neutral, PublicKeyToken=null
// Global type: <Module>
// Architecture: AnyCPU (64-bit preferred)
// Runtime: v4.0.30319
// Hash algorithm: SHA1
// Debug info: Loaded from PDB file: /home/kve/Documents/Dienen Misc/Epicor/CodaBears_Custom_DLLs/CodaBears.Epicor10.2.pdb

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security.Principal;
using System.Threading;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using CodaBears.Common;
using CodaBears.Epicor10.Properties;
using CodaBears.Epicor10.ReportServices;
using Epicor.ServiceModel.Channels;
using Erp.BO;
using Erp.Contracts;
using Erp.Proxy.BO;
using Ice.Core;
using Ice.Lib.Framework;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: AssemblyTitle("CodaBears.Epicor10.2")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("CodaBears, Inc.")]
[assembly: AssemblyProduct("CodaBears.Epicor10.2")]
[assembly: AssemblyCopyright("Copyright © CodaBears, Inc. 2019")]
[assembly: AssemblyTrademark("CodaBears, Inc.")]
[assembly: ComVisible(false)]
[assembly: Guid("4626d4bb-86f4-4572-b397-a88280f033a0")]
[assembly: TargetFramework(".NETFramework,Version=v4.8", FrameworkDisplayName = ".NET Framework 4.8")]
[assembly: AssemblyVersion("10.2.7835.27171")]
namespace CodaBears.Epicor10
{
	public class Auth : Form
	{
		private IContainer components = null;

		private Label lblMessage;

		private Label label2;

		private TextBox tbUser;

		private Label label3;

		private TextBox tbPassword;

		private Button Button1;

		private Button Button2;

		public string Password => ((Control)tbPassword).get_Text();

		public string UserName => ((Control)tbUser).get_Text();

		private Auth()
			: this()
		{
			InitializeComponent();
		}

		public Auth(string message)
			: this()
		{
			InitializeComponent();
			((Control)lblMessage).set_Text(message);
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			((Form)this).Close();
		}

		private void Button2_Click(object sender, EventArgs e)
		{
			//IL_005a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0085: Unknown result type (might be due to invalid IL or missing references)
			if (!string.IsNullOrWhiteSpace(((Control)tbUser).get_Text()) && !string.IsNullOrWhiteSpace(((Control)tbPassword).get_Text()))
			{
				((Form)this).Close();
			}
			if (string.IsNullOrWhiteSpace(((Control)tbUser).get_Text()))
			{
				MessageBox.Show("Please enter a User ID.", "Missing Info", (MessageBoxButtons)0, (MessageBoxIcon)16, (MessageBoxDefaultButton)0, (MessageBoxOptions)0);
			}
			if (string.IsNullOrWhiteSpace(((Control)tbPassword).get_Text()))
			{
				MessageBox.Show("Please enter a Password.", "Missing Info", (MessageBoxButtons)0, (MessageBoxIcon)16, (MessageBoxDefaultButton)0, (MessageBoxOptions)0);
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			((Form)this).Dispose(disposing);
		}

		private void InitializeComponent()
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Expected O, but got Unknown
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Expected O, but got Unknown
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Expected O, but got Unknown
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Expected O, but got Unknown
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Expected O, but got Unknown
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Expected O, but got Unknown
			//IL_0044: Unknown result type (might be due to invalid IL or missing references)
			//IL_004e: Expected O, but got Unknown
			lblMessage = new Label();
			label2 = new Label();
			tbUser = new TextBox();
			label3 = new Label();
			tbPassword = new TextBox();
			Button1 = new Button();
			Button2 = new Button();
			((Control)this).SuspendLayout();
			((Control)lblMessage).set_AutoSize(true);
			((Control)lblMessage).set_Location(new Point(12, 24));
			((Control)lblMessage).set_Name("lblMessage");
			((Control)lblMessage).set_Size(new Size(50, 13));
			((Control)lblMessage).set_TabIndex(0);
			((Control)lblMessage).set_Text("Message");
			((Control)label2).set_AutoSize(true);
			((Control)label2).set_Location(new Point(20, 58));
			((Control)label2).set_Name("label2");
			((Control)label2).set_Size(new Size(43, 13));
			((Control)label2).set_TabIndex(1);
			((Control)label2).set_Text("User ID");
			((Control)tbUser).set_Location(new Point(69, 55));
			((Control)tbUser).set_Name("tbUser");
			((Control)tbUser).set_Size(new Size(143, 20));
			((Control)tbUser).set_TabIndex(2);
			((Control)label3).set_AutoSize(true);
			((Control)label3).set_Location(new Point(10, 84));
			((Control)label3).set_Name("label3");
			((Control)label3).set_Size(new Size(53, 13));
			((Control)label3).set_TabIndex(3);
			((Control)label3).set_Text("Password");
			((Control)tbPassword).set_Location(new Point(69, 81));
			((Control)tbPassword).set_Name("tbPassword");
			((Control)tbPassword).set_Size(new Size(143, 20));
			((Control)tbPassword).set_TabIndex(4);
			Button1.set_DialogResult((DialogResult)2);
			((Control)Button1).set_Location(new Point(127, 117));
			((Control)Button1).set_Name("Button1");
			((Control)Button1).set_Size(new Size(70, 22));
			((Control)Button1).set_TabIndex(5);
			((Control)Button1).set_Text("&Cancel");
			((ButtonBase)Button1).set_UseVisualStyleBackColor(true);
			((Control)Button1).add_Click((EventHandler)Button1_Click);
			((Control)Button2).set_Location(new Point(39, 117));
			((Control)Button2).set_Name("Button2");
			((Control)Button2).set_Size(new Size(70, 22));
			((Control)Button2).set_TabIndex(6);
			((Control)Button2).set_Text("&OK");
			((ButtonBase)Button2).set_UseVisualStyleBackColor(true);
			((Control)Button2).add_Click((EventHandler)Button2_Click);
			((ContainerControl)this).set_AutoScaleDimensions(new SizeF(6f, 13f));
			((ContainerControl)this).set_AutoScaleMode((AutoScaleMode)1);
			((Form)this).set_ClientSize(new Size(237, 151));
			((Control)this).get_Controls().Add((Control)(object)Button2);
			((Control)this).get_Controls().Add((Control)(object)Button1);
			((Control)this).get_Controls().Add((Control)(object)tbPassword);
			((Control)this).get_Controls().Add((Control)(object)label3);
			((Control)this).get_Controls().Add((Control)(object)tbUser);
			((Control)this).get_Controls().Add((Control)(object)label2);
			((Control)this).get_Controls().Add((Control)(object)lblMessage);
			((Control)this).set_Name("Auth");
			((Control)this).set_Text("Authorization Required");
			((Control)this).ResumeLayout(false);
			((Control)this).PerformLayout();
		}
	}
	internal sealed class Db
	{
		internal static GenericDatabase GetDatabase(string sqlDatabaseString)
		{
			return new GenericDatabase(sqlDatabaseString, DbProviderFactories.GetFactory("System.Data.SqlClient"));
		}
	}
	public sealed class ManagerOverride
	{
		private ManagerOverride()
		{
		}

		public static bool IsAuthorized(string message, string udField, string connectionString)
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			bool result = false;
			Auth auth = new Auth(message);
			((Form)auth).ShowDialog();
			string userName = auth.UserName;
			string password = auth.Password;
			if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password))
			{
				try
				{
					GenericDatabase database = Db.GetDatabase(connectionString);
					string query = string.Format("SELECT DCDUserID FROM dbo.UserFile WHERE DCDUserID='{0}' and {2}='{1}'", userName, password, udField);
					DbCommand sqlStringCommand = database.GetSqlStringCommand(query);
					DataSet val = database.ExecuteDataSet(sqlStringCommand);
					result = ((InternalDataCollectionBase)val.get_Tables().get_Item(0).get_Rows()).get_Count() > 0;
				}
				catch (Exception arg)
				{
					MessageBox.Show($"There was an error working with the database.\r\n{arg}");
				}
			}
			return result;
		}
	}
	public class PrintManager : IDisposable
	{
		private readonly ReportExecutionService _rs = new ReportExecutionService();

		private byte[][] _renderedReport;

		private EnumerateMetafileProc _delegate;

		private MemoryStream _currentPageStream;

		private Metafile _metafile;

		private int _numberOfPages;

		private int _currentPrintingPage;

		private int _lastPrintingPage;

		private PrintManager()
		{
			((WebClientProtocol)_rs).set_Credentials((ICredentials)(object)CredentialCache.get_DefaultNetworkCredentials());
		}

		public PrintManager(ICredentials credential)
		{
			((WebClientProtocol)_rs).set_Credentials(credential);
		}

		public PrintManager(string serverName)
			: this()
		{
			_rs.Url = "http://" + serverName + "/ReportServer/ReportExecution2005.asmx";
		}

		public bool PrintReport(string reportPath, Dictionary<string, string> paramDictionary, int fromPage, int toPage, string printerName)
		{
			return PrintReport(reportPath, paramDictionary, fromPage, toPage, printerName, 0, 0);
		}

		public bool PrintReport(string reportPath, Dictionary<string, string> paramDictionary, int fromPage, int toPage, string printerName, int widthHundrethOfInches, int heightHundrethOfInches)
		{
			//IL_0082: Unknown result type (might be due to invalid IL or missing references)
			//IL_0087: Unknown result type (might be due to invalid IL or missing references)
			//IL_0094: Unknown result type (might be due to invalid IL or missing references)
			//IL_009c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a5: Expected O, but got Unknown
			//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00ac: Expected O, but got Unknown
			//IL_017e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0188: Expected O, but got Unknown
			//IL_0193: Unknown result type (might be due to invalid IL or missing references)
			//IL_019d: Expected O, but got Unknown
			//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
			List<ParameterValue> list = new List<ParameterValue>();
			if (paramDictionary != null && paramDictionary.Count > 0)
			{
				list.AddRange(paramDictionary.Keys.Select((string key) => new ParameterValue
				{
					Name = key,
					Value = paramDictionary[key]
				}));
			}
			_renderedReport = RenderReport(reportPath, list.ToArray());
			try
			{
				if (_numberOfPages < 1)
				{
					return false;
				}
				PrinterSettings val = new PrinterSettings();
				val.set_MaximumPage(_numberOfPages);
				val.set_MinimumPage(1);
				val.set_PrintRange((PrintRange)0);
				PrinterSettings val2 = val;
				PrintDocument val3 = new PrintDocument();
				if (toPage != -1 && fromPage != -1)
				{
					_currentPrintingPage = fromPage;
					_lastPrintingPage = toPage;
					if (_numberOfPages < toPage)
					{
						toPage = _numberOfPages;
						_lastPrintingPage = toPage;
					}
					if (_numberOfPages < fromPage)
					{
						fromPage = _numberOfPages;
						_currentPrintingPage = fromPage;
					}
					val2.set_FromPage(fromPage);
					val2.set_ToPage(toPage);
				}
				else
				{
					_currentPrintingPage = 1;
					_lastPrintingPage = _numberOfPages;
				}
				WindowsImpersonationContext val4 = WindowsIdentity.Impersonate(IntPtr.Zero);
				try
				{
					val2.set_PrinterName(printerName);
					val3.set_PrinterSettings(val2);
					if (widthHundrethOfInches != 0 && heightHundrethOfInches != 0)
					{
						val3.get_DefaultPageSettings().set_PaperSize(new PaperSize("label", widthHundrethOfInches, heightHundrethOfInches));
					}
					val3.add_PrintPage(new PrintPageEventHandler(pd_PrintPage));
					val3.Print();
				}
				finally
				{
					((IDisposable)val4)?.Dispose();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return true;
		}

		private byte[][] RenderReport(string reportPath, ParameterValue[] parameters)
		{
			//IL_00ea: Expected O, but got Unknown
			//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_010a: Unknown result type (might be due to invalid IL or missing references)
			ExecutionHeader executionHeaderValue = new ExecutionHeader();
			try
			{
				((WebClientProtocol)_rs).set_Timeout(300000);
				_rs.ExecutionHeaderValue = executionHeaderValue;
				_rs.LoadReport(reportPath, null);
				if (parameters != null)
				{
					_rs.SetExecutionParameters(parameters, "en_us");
				}
				byte[][] array = new byte[0][];
				int num = 1;
				byte[] array2 = new byte[1];
				while (array2.Length != 0)
				{
					string deviceInfo = $"<DeviceInfo><OutputFormat>EMF</OutputFormat><PrintDpiX>200</PrintDpiX><PrintDpiY>200</PrintDpiY><StartPage>{num}</StartPage></DeviceInfo>";
					array2 = _rs.Render("IMAGE", deviceInfo, out var _, out var _, out var _, out var _, out var _);
					if (array2.Length == 0 && num == 1)
					{
						break;
					}
					if (array2.Length != 0)
					{
						Array.Resize(ref array, array.Length + 1);
						array[^1] = array2;
						num++;
					}
				}
				_numberOfPages = num - 1;
				return array;
			}
			catch (SoapException val)
			{
				SoapException val2 = val;
				MessageBox.Show(val2.get_Detail().InnerXml);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return null;
		}

		private void pd_PrintPage(object sender, PrintPageEventArgs ev)
		{
			ev.set_HasMorePages(false);
			if (_currentPrintingPage <= _lastPrintingPage && MoveToPage(_currentPrintingPage))
			{
				ReportDrawPage(ev.get_Graphics());
				if (Interlocked.Increment(ref _currentPrintingPage) <= _lastPrintingPage)
				{
					ev.set_HasMorePages(true);
				}
			}
		}

		private void ReportDrawPage(Graphics g)
		{
			//IL_005b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0065: Expected O, but got Unknown
			if (_currentPageStream != null && _currentPageStream.Length != 0L && _metafile != null)
			{
				lock (this)
				{
					int width = ((Image)_metafile).get_Width();
					int height = ((Image)_metafile).get_Height();
					_delegate = new EnumerateMetafileProc(MetafileCallback);
					Point[] array = new Point[3];
					Point point = new Point(0, 0);
					Point point2 = new Point(width / 2, 0);
					Point point3 = new Point(0, height / 2);
					array[0] = point;
					array[1] = point2;
					array[2] = point3;
					g.EnumerateMetafile(_metafile, array, _delegate);
					_delegate = null;
				}
			}
		}

		private bool MoveToPage(int page)
		{
			//IL_0068: Unknown result type (might be due to invalid IL or missing references)
			//IL_0072: Expected O, but got Unknown
			if (_renderedReport[_currentPrintingPage - 1] == null)
			{
				return false;
			}
			_currentPageStream = new MemoryStream(_renderedReport[_currentPrintingPage - 1])
			{
				Position = 0L
			};
			if (_metafile != null)
			{
				((Image)_metafile).Dispose();
				_metafile = null;
			}
			_metafile = new Metafile((Stream)_currentPageStream);
			return true;
		}

		private bool MetafileCallback(EmfPlusRecordType recordType, int flags, int dataSize, IntPtr data, PlayRecordCallback callbackData)
		{
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			byte[] array = null;
			if (data != IntPtr.Zero)
			{
				array = new byte[dataSize];
				Marshal.Copy(data, array, 0, dataSize);
			}
			_metafile.PlayRecord(recordType, flags, dataSize, array);
			return true;
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				((Component)(object)_rs)?.Dispose();
				Metafile metafile = _metafile;
				if (metafile != null)
				{
					((Image)metafile).Dispose();
				}
			}
		}
	}
	public class Shipping
	{
		private class ShipInfo
		{
			public readonly string TrackingNumber;

			public readonly string PackNumber;

			public readonly decimal TotalCharges;

			public readonly decimal Weight;

			public readonly int NumberOfPackages;

			public ShipInfo(string trackNumber, string packNum, decimal totalCharges, decimal weight, int packageCount)
			{
				TrackingNumber = trackNumber.Replace("\"", "");
				PackNumber = packNum.Replace("\"", "");
				TotalCharges = totalCharges;
				Weight = weight;
				NumberOfPackages = packageCount;
			}
		}

		private Shipping()
		{
		}

		public static void UpdateUPSTracking(Session session, string connectionString, string shipDate, string outputPath, string upsConnectionstring, int minLength, int maxLength, int minValue, int maxValue, string freightCode)
		{
			UpdateUPSTracking(session, connectionString, shipDate, outputPath, upsConnectionstring, minLength, maxLength, minValue, maxValue, freightCode, "");
		}

		public static void UpdateUPSTracking(Session session, string connectionString, string shipDate, string outputPath, string upsConnectionstring, int minLength, int maxLength, int minValue, int maxValue, string freightCode, string numberOfPackagesField)
		{
			string path = StringHelper.FormatString("{0}{1:yyyyMMdd-HHmm-ssff}-UPS.log", outputPath, DateTime.Now);
			StreamWriter streamWriter = new StreamWriter(path, append: true);
			streamWriter.WriteLine("UPS Data processed on: {0}", DateTime.Now);
			SqlDatabase sqlDatabase = new SqlDatabase(upsConnectionstring);
			DbCommand storedProcCommand = sqlDatabase.GetStoredProcCommand("ShipmentsGet");
			((Database)sqlDatabase).AddInParameter(storedProcCommand, "@SearchDateText", (DbType)16, (object)shipDate);
			IDataReader val = null;
			try
			{
				val = sqlDatabase.ExecuteReader(storedProcCommand);
				if (val == null)
				{
					return;
				}
				int num = 0;
				while (val.Read())
				{
					num++;
					string logErr = " was not found to update ";
					string @string = DataAccess.GetString((IDataRecord)(object)val, "TrackingNumber");
					string string2 = DataAccess.GetString((IDataRecord)(object)val, "ThePackId");
					string string3 = DataAccess.GetString((IDataRecord)(object)val, "AccountNumber");
					decimal @decimal = DataAccess.GetDecimal((IDataRecord)(object)val, "Weight");
					int @int = DataAccess.GetInt32((IDataRecord)(object)val, "NumberOfPackages");
					bool flag = StringHelper.IsNullEmptyOrSpaces(string3);
					if (!string.IsNullOrEmpty(string2))
					{
						string[] separator = new string[1] { "," };
						string[] array = string2.Split(separator, StringSplitOptions.RemoveEmptyEntries);
						string[] array2 = array;
						foreach (string text in array2)
						{
							bool flag2 = false;
							bool flag3 = false;
							bool flag4 = false;
							int length = text.Length;
							string text2 = ((minLength > 0 && length < minLength) ? "0" : ((maxLength != 0 && length > maxLength) ? text.Substring(0, maxLength).Trim() : text.Substring(0, length).Trim()));
							int result;
							if (text2.StartsWith("M"))
							{
								flag3 = true;
								if (int.TryParse(text2.Substring(1), out result))
								{
									flag2 = true;
								}
								else
								{
									logErr = " couldn't convert misc packnum to int";
								}
							}
							else if (text2.StartsWith("S"))
							{
								flag4 = true;
								if (int.TryParse(text2.Substring(1), out result))
								{
									flag2 = true;
								}
								else
								{
									logErr = " couldn't convert subcon packnum to int";
								}
							}
							else if (int.TryParse(text2, out result))
							{
								if ((minValue <= 0 || result >= minValue) && (maxValue <= 0 || result <= maxValue))
								{
									flag2 = true;
								}
								else
								{
									logErr = " was not permitted to update ";
								}
							}
							else
							{
								logErr = " is an invalid pack number";
							}
							if (!flag2)
							{
								continue;
							}
							if (!flag3 && !flag4)
							{
								UpdateTrackingNumber(result, @string, @decimal, streamWriter, session, @int, numberOfPackagesField);
								if (!StringHelper.IsNullEmptyOrSpaces(text2) && !StringHelper.IsNullEmptyOrSpaces(freightCode) && flag)
								{
									decimal decimal2 = DataAccess.GetDecimal((IDataRecord)(object)val, "TotalCharges");
									if (decimal2 == 0m)
									{
										streamWriter.WriteLine(StringHelper.FormatString("\tUPS Charge is $0, not updating PackNum = ", text2));
										continue;
									}
									UpdateMiscShipCharges(connectionString, session.get_CompanyID(), result, freightCode, decimal2);
									streamWriter.WriteLine(StringHelper.FormatString("\tPackNum {0} MiscCharge updated; code = {1}; amount = {2:C}", text2, freightCode, decimal2));
								}
								else
								{
									WriteError(streamWriter, text2, logErr, @string);
								}
							}
							else if (flag3)
							{
								UpdateTrackingMisc(result, @string, streamWriter, session);
							}
							else
							{
								UpdateTrackingSubCon(result, @string, streamWriter, session);
							}
						}
					}
					else
					{
						WriteError(streamWriter, string2, logErr, @string);
					}
				}
				if (num == 0)
				{
					string logErr = "NO DATA FROM UPS TO PROCESS";
					streamWriter.WriteLine(logErr);
				}
			}
			catch (Exception arg)
			{
				streamWriter.WriteLine("FAILURE: {0}", arg);
			}
			finally
			{
				if (val != null && !val.get_IsClosed())
				{
					val.Close();
				}
				streamWriter.Flush();
				streamWriter.Close();
			}
		}

		private static void WriteError(TextWriter writer, int value, string logErr, string trackingNumber)
		{
			WriteError(writer, value.ToString(CultureInfo.InvariantCulture), logErr, trackingNumber);
		}

		private static void WriteError(TextWriter writer, string value, string logErr, string trackingNumber)
		{
			writer.WriteLine(StringHelper.FormatString("*Pack Slip: '{0}' {1} with the Tracking Number: {2}", value, logErr, trackingNumber));
		}

		private static void WriteSuccess(TextWriter writer, int value, string trackingNumber)
		{
			WriteSuccess(writer, value.ToString(CultureInfo.InvariantCulture), trackingNumber);
		}

		private static void WriteSuccess(TextWriter writer, string value, string trackingNumber)
		{
			writer.WriteLine(StringHelper.FormatString("Pack Slip: {0} updated with the Tracking Number: {1}", value, trackingNumber));
		}

		private static void UpdateTrackingNumber(int packNumber, string trackingNumber, decimal weight, TextWriter logWriter, Session session, int numberOfPackages, string numberOfPackagesField)
		{
			try
			{
				if (!string.IsNullOrEmpty(packNumber.ToString(CultureInfo.InvariantCulture)))
				{
					CustShipImpl val = WCFServiceSupport.CreateImpl<CustShipImpl>(session, ImplBase<CustShipSvcContract>.UriPath);
					try
					{
						CustShipDataSet byID = val.GetByID(packNumber);
						byID.get_ShipHead().get_Item(0).set_TrackingNumber(trackingNumber);
						byID.get_ShipHead().get_Item(0).set_Weight(weight);
						if (!string.IsNullOrWhiteSpace(numberOfPackagesField))
						{
							((DataRow)byID.get_ShipHead().get_Item(0)).set_Item(numberOfPackagesField, (object)numberOfPackages);
						}
						int bTCustNum = byID.get_ShipHead().get_Item(0).get_BTCustNum();
						string text = default(string);
						string text2 = default(string);
						string text3 = default(string);
						string text4 = default(string);
						string text5 = default(string);
						string text6 = default(string);
						string text7 = default(string);
						string text8 = default(string);
						string text9 = default(string);
						string text10 = default(string);
						bool flag = default(bool);
						bool flag2 = default(bool);
						string text11 = default(string);
						string text12 = default(string);
						bool flag3 = default(bool);
						bool flag4 = default(bool);
						bool flag5 = default(bool);
						bool flag6 = default(bool);
						val.UpdateMaster(byID, false, false, false, false, false, false, false, packNumber, bTCustNum, ref text, ref text2, ref text3, ref text4, ref text5, ref text6, ref text7, ref text8, ref text9, ref text10, ref flag, ref flag2, ref text11, ref text12, ref flag3, ref flag4, ref flag5, ref flag6);
					}
					finally
					{
						((IDisposable)val)?.Dispose();
					}
					WriteSuccess(logWriter, packNumber, trackingNumber);
				}
				else
				{
					WriteError(logWriter, packNumber, " could not be updated ", trackingNumber);
				}
			}
			catch (Exception arg)
			{
				WriteError(logWriter, packNumber, string.Format("{0}\r\nUpdateUPS\r\n{1}", " could not be updated ", arg), trackingNumber);
			}
		}

		private static void UpdateTrackingMisc(int packNumber, string trackingNumber, TextWriter logWriter, Session session)
		{
			try
			{
				if (!string.IsNullOrEmpty(packNumber.ToString(CultureInfo.InvariantCulture)))
				{
					MiscShipImpl val = WCFServiceSupport.CreateImpl<MiscShipImpl>(session, ImplBase<MiscShipSvcContract>.UriPath);
					try
					{
						MiscShipDataSet byID = val.GetByID(packNumber);
						byID.get_MscShpHd().get_Item(0).set_TrackingNumber(trackingNumber);
						val.Update(byID);
					}
					finally
					{
						((IDisposable)val)?.Dispose();
					}
					WriteSuccess(logWriter, packNumber, trackingNumber);
				}
				else
				{
					WriteError(logWriter, packNumber, " could not be updated ", trackingNumber);
				}
			}
			catch (Exception arg)
			{
				WriteError(logWriter, packNumber, string.Format("{0}\r\nUpdateUPS\r\n{1}", " could not be updated ", arg), trackingNumber);
			}
		}

		private static void UpdateTrackingSubCon(int packNumber, string trackingNumber, TextWriter logWriter, Session session)
		{
			try
			{
				if (!string.IsNullOrEmpty(packNumber.ToString(CultureInfo.InvariantCulture)))
				{
					SubConShipEntryImpl val = WCFServiceSupport.CreateImpl<SubConShipEntryImpl>(session, ImplBase<SubConShipEntrySvcContract>.UriPath);
					try
					{
						SubConShipEntryDataSet byID = val.GetByID(packNumber);
						byID.get_SubShipH().get_Item(0).set_TrackingNumber(trackingNumber);
						val.Update(byID);
					}
					finally
					{
						((IDisposable)val)?.Dispose();
					}
					WriteSuccess(logWriter, packNumber, trackingNumber);
				}
				else
				{
					WriteError(logWriter, packNumber, " could not be updated ", trackingNumber);
				}
			}
			catch (Exception arg)
			{
				WriteError(logWriter, packNumber, string.Format("{0}\r\nUpdateUPS\r\n{1}", " could not be updated ", arg), trackingNumber);
			}
		}

		private static void UpdateMiscShipCharges(string connectionString, string company, int packNum, string miscCode, decimal amount)
		{
			GenericDatabase genericDatabase = new GenericDatabase(connectionString, DbProviderFactories.GetFactory("System.Data.SqlClient"));
			DbCommand storedProcCommand = genericDatabase.GetStoredProcCommand("ShipMiscUpdateCharge");
			genericDatabase.AddInParameter(storedProcCommand, "@Company", (DbType)16, company);
			genericDatabase.AddInParameter(storedProcCommand, "@Packnum", (DbType)11, packNum);
			genericDatabase.AddInParameter(storedProcCommand, "@MiscCode", (DbType)16, miscCode);
			genericDatabase.AddInParameter(storedProcCommand, "@Amt", (DbType)7, amount);
			genericDatabase.ExecuteNonQuery(storedProcCommand);
		}

		public static void UpdateFedExTracking(Session session, string connectionString, string inputFileName, int minLength, int maxLength, int minValue, int maxValue, string freightCode, decimal percentUpcharge)
		{
			UpdateFedExTracking(session, connectionString, inputFileName, minLength, maxLength, minValue, maxValue, freightCode, percentUpcharge, "");
		}

		public static void UpdateFedExTracking(Session session, string connectionString, string inputFileName, int minLength, int maxLength, int minValue, int maxValue, string freightCode, decimal percentUpcharge, string numberOfPackagesField)
		{
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Expected O, but got Unknown
			string text = inputFileName.Replace(".csv", ".log");
			List<ShipInfo> list = new List<ShipInfo>();
			string logErr = null;
			using StreamWriter streamWriter = new StreamWriter(text);
			try
			{
				FileInfo val = new FileInfo(inputFileName);
				streamWriter.WriteLine("Data file processed: " + inputFileName);
				streamWriter.WriteLine("Processed on: " + DateTime.Now);
				using (StreamReader streamReader = val.OpenText())
				{
					string text2;
					while ((text2 = streamReader.ReadLine()) != null)
					{
						streamWriter.WriteLine("Read in " + text2);
						string[] array = text2.Split(new string[1] { "\",\"" }, StringSplitOptions.RemoveEmptyEntries);
						if (StringHelper.CompareInsensitive(array[0], "Tracking #"))
						{
							continue;
						}
						array[2] = array[2].Replace("\"", "");
						decimal.TryParse(array[2], out var result);
						if (array[1].Length <= 2)
						{
							continue;
						}
						int num = array[1].IndexOf(" ", StringComparison.OrdinalIgnoreCase);
						string text3 = ((num < 0) ? array[1] : array[1].Substring(0, array[1].IndexOf(" ", StringComparison.OrdinalIgnoreCase)));
						text3 = text3.Replace("\"", "");
						decimal weight = default(decimal);
						int packageCount = 0;
						if (!string.IsNullOrEmpty(text3))
						{
							if (text3.Contains(","))
							{
								char[] separator = new char[1] { ',' };
								string[] array2 = text3.Split(separator);
								string text4 = array2[0];
								if (!string.IsNullOrEmpty(text4) && int.TryParse(text4, out var _))
								{
									list.Add(new ShipInfo(array[0], text4, result, weight, packageCount));
								}
								else
								{
									list.Add(new ShipInfo(array[0], "0", result, weight, packageCount));
								}
							}
							else if ((minLength == 0 || text3.Length >= minLength) && (maxLength == 0 || text3.Length <= maxLength))
							{
								string packNum = text3;
								list.Add(new ShipInfo(array[0], packNum, result, weight, packageCount));
							}
							else
							{
								WriteError(streamWriter, text3, "Invalid pack slip", array[0]);
							}
						}
						else
						{
							WriteError(streamWriter, "", "Blank pack slip #", array[0]);
						}
					}
					streamReader.Close();
				}
				foreach (ShipInfo item in list)
				{
					string trackingNumber = item.TrackingNumber;
					if (string.IsNullOrEmpty(item.PackNumber))
					{
						continue;
					}
					bool flag = false;
					bool flag2 = false;
					bool flag3 = false;
					int length = item.PackNumber.Length;
					string text5 = ((minLength > 0 && length < minLength) ? "0" : ((maxLength != 0 && length > maxLength) ? item.PackNumber.Substring(0, maxLength).Trim() : item.PackNumber.Substring(0, length).Trim()));
					int result3;
					if (text5.StartsWith("M"))
					{
						flag2 = true;
						if (int.TryParse(text5.Substring(1), out result3))
						{
							flag = true;
						}
						else
						{
							logErr = " couldn't convert misc packnum to int";
						}
					}
					else if (text5.StartsWith("S"))
					{
						flag3 = true;
						if (int.TryParse(text5.Substring(1), out result3))
						{
							flag = true;
						}
						else
						{
							logErr = " couldn't convert subcon packnum to int";
						}
					}
					else if (int.TryParse(text5, out result3))
					{
						if ((minValue <= 0 || result3 >= minValue) && (maxValue <= 0 || result3 <= maxValue))
						{
							flag = true;
						}
						else
						{
							logErr = " was not permitted to update ";
						}
					}
					else
					{
						logErr = " is an invalid pack number";
					}
					if (!flag)
					{
						continue;
					}
					if (!flag2 && !flag3)
					{
						UpdateTrackingNumber(result3, trackingNumber, item.Weight, streamWriter, session, item.NumberOfPackages, numberOfPackagesField);
						WriteSuccess(streamWriter, result3, trackingNumber);
						if (!StringHelper.IsNullEmptyOrSpaces(result3.ToString()) && !StringHelper.IsNullEmptyOrSpaces(freightCode))
						{
							decimal totalCharges = item.TotalCharges;
							if (totalCharges == 0m)
							{
								streamWriter.WriteLine(StringHelper.FormatString("\tCharge is $0, not updating PackNum = ", text5));
								continue;
							}
							totalCharges += Math.Round(totalCharges * (percentUpcharge / 100m), 2);
							UpdateMiscShipCharges(connectionString, session.get_CompanyID(), result3, freightCode, totalCharges);
							streamWriter.WriteLine(StringHelper.FormatString("\tPackNum {0} MiscCharge updated; code = {1}; amount = {2:C}", result3, freightCode, totalCharges));
						}
						else
						{
							WriteError(streamWriter, text5, logErr, trackingNumber);
						}
					}
					else if (flag2)
					{
						UpdateTrackingMisc(result3, item.TrackingNumber, streamWriter, session);
					}
					else
					{
						UpdateTrackingSubCon(result3, item.TrackingNumber, streamWriter, session);
					}
				}
			}
			catch (Exception ex)
			{
				streamWriter.WriteLine("File: " + inputFileName + " could not be processed.");
				streamWriter.WriteLine("FAILURE: " + ex.Message);
			}
			finally
			{
				streamWriter.Flush();
				streamWriter.Close();
				string text6 = $"{DateTime.Now:MMddyyyy-HHmm}";
				if (File.Exists(inputFileName))
				{
					string text7 = Path.GetDirectoryName(inputFileName) + "\\Processed\\" + text6 + "-" + Path.GetFileName(inputFileName);
					File.Move(inputFileName, text7);
				}
				if (File.Exists(text))
				{
					string text8 = Path.GetDirectoryName(text) + "\\Processed\\" + text6 + "-" + Path.GetFileName(text);
					File.Move(text, text8);
				}
			}
		}
	}
}
namespace CodaBears.Epicor10.ReportServices
{
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[WebServiceBinding(Name = "ReportExecutionServiceSoap", Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[XmlInclude(typeof(ParameterValueOrFieldReference))]
	public class ReportExecutionService : SoapHttpClientProtocol
	{
		private TrustedUserHeader trustedUserHeaderValueField;

		private ServerInfoHeader serverInfoHeaderValueField;

		private SendOrPostCallback ListSecureMethodsOperationCompleted;

		private ExecutionHeader executionHeaderValueField;

		private SendOrPostCallback LoadReportOperationCompleted;

		private SendOrPostCallback LoadReport2OperationCompleted;

		private SendOrPostCallback LoadReportDefinitionOperationCompleted;

		private SendOrPostCallback LoadReportDefinition2OperationCompleted;

		private SendOrPostCallback SetExecutionCredentialsOperationCompleted;

		private SendOrPostCallback SetExecutionCredentials2OperationCompleted;

		private SendOrPostCallback SetExecutionParametersOperationCompleted;

		private SendOrPostCallback SetExecutionParameters2OperationCompleted;

		private SendOrPostCallback ResetExecutionOperationCompleted;

		private SendOrPostCallback ResetExecution2OperationCompleted;

		private SendOrPostCallback RenderOperationCompleted;

		private SendOrPostCallback Render2OperationCompleted;

		private SendOrPostCallback RenderStreamOperationCompleted;

		private SendOrPostCallback GetExecutionInfoOperationCompleted;

		private SendOrPostCallback GetExecutionInfo2OperationCompleted;

		private SendOrPostCallback GetDocumentMapOperationCompleted;

		private SendOrPostCallback LoadDrillthroughTargetOperationCompleted;

		private SendOrPostCallback LoadDrillthroughTarget2OperationCompleted;

		private SendOrPostCallback ToggleItemOperationCompleted;

		private SendOrPostCallback NavigateDocumentMapOperationCompleted;

		private SendOrPostCallback NavigateBookmarkOperationCompleted;

		private SendOrPostCallback FindStringOperationCompleted;

		private SendOrPostCallback SortOperationCompleted;

		private SendOrPostCallback Sort2OperationCompleted;

		private SendOrPostCallback GetRenderResourceOperationCompleted;

		private SendOrPostCallback ListRenderingExtensionsOperationCompleted;

		private SendOrPostCallback LogonUserOperationCompleted;

		private SendOrPostCallback LogoffOperationCompleted;

		private bool useDefaultCredentialsSetExplicitly;

		public TrustedUserHeader TrustedUserHeaderValue
		{
			get
			{
				return trustedUserHeaderValueField;
			}
			set
			{
				trustedUserHeaderValueField = value;
			}
		}

		public ServerInfoHeader ServerInfoHeaderValue
		{
			get
			{
				return serverInfoHeaderValueField;
			}
			set
			{
				serverInfoHeaderValueField = value;
			}
		}

		public ExecutionHeader ExecutionHeaderValue
		{
			get
			{
				return executionHeaderValueField;
			}
			set
			{
				executionHeaderValueField = value;
			}
		}

		public string Url
		{
			get
			{
				return ((WebClientProtocol)this).get_Url();
			}
			set
			{
				if (IsLocalFileSystemWebService(((WebClientProtocol)this).get_Url()) && !useDefaultCredentialsSetExplicitly && !IsLocalFileSystemWebService(value))
				{
					((WebClientProtocol)this).set_UseDefaultCredentials(false);
				}
				((WebClientProtocol)this).set_Url(value);
			}
		}

		public bool UseDefaultCredentials
		{
			get
			{
				return ((WebClientProtocol)this).get_UseDefaultCredentials();
			}
			set
			{
				((WebClientProtocol)this).set_UseDefaultCredentials(value);
				useDefaultCredentialsSetExplicitly = true;
			}
		}

		public event ListSecureMethodsCompletedEventHandler ListSecureMethodsCompleted;

		public event LoadReportCompletedEventHandler LoadReportCompleted;

		public event LoadReport2CompletedEventHandler LoadReport2Completed;

		public event LoadReportDefinitionCompletedEventHandler LoadReportDefinitionCompleted;

		public event LoadReportDefinition2CompletedEventHandler LoadReportDefinition2Completed;

		public event SetExecutionCredentialsCompletedEventHandler SetExecutionCredentialsCompleted;

		public event SetExecutionCredentials2CompletedEventHandler SetExecutionCredentials2Completed;

		public event SetExecutionParametersCompletedEventHandler SetExecutionParametersCompleted;

		public event SetExecutionParameters2CompletedEventHandler SetExecutionParameters2Completed;

		public event ResetExecutionCompletedEventHandler ResetExecutionCompleted;

		public event ResetExecution2CompletedEventHandler ResetExecution2Completed;

		public event RenderCompletedEventHandler RenderCompleted;

		public event Render2CompletedEventHandler Render2Completed;

		public event RenderStreamCompletedEventHandler RenderStreamCompleted;

		public event GetExecutionInfoCompletedEventHandler GetExecutionInfoCompleted;

		public event GetExecutionInfo2CompletedEventHandler GetExecutionInfo2Completed;

		public event GetDocumentMapCompletedEventHandler GetDocumentMapCompleted;

		public event LoadDrillthroughTargetCompletedEventHandler LoadDrillthroughTargetCompleted;

		public event LoadDrillthroughTarget2CompletedEventHandler LoadDrillthroughTarget2Completed;

		public event ToggleItemCompletedEventHandler ToggleItemCompleted;

		public event NavigateDocumentMapCompletedEventHandler NavigateDocumentMapCompleted;

		public event NavigateBookmarkCompletedEventHandler NavigateBookmarkCompleted;

		public event FindStringCompletedEventHandler FindStringCompleted;

		public event SortCompletedEventHandler SortCompleted;

		public event Sort2CompletedEventHandler Sort2Completed;

		public event GetRenderResourceCompletedEventHandler GetRenderResourceCompleted;

		public event ListRenderingExtensionsCompletedEventHandler ListRenderingExtensionsCompleted;

		public event LogonUserCompletedEventHandler LogonUserCompleted;

		public event LogoffCompletedEventHandler LogoffCompleted;

		public ReportExecutionService()
			: this()
		{
			Url = Settings.Default.CodaBears_Epicor10_ReportServices_ReportExecutionService;
			if (IsLocalFileSystemWebService(Url))
			{
				UseDefaultCredentials = true;
				useDefaultCredentialsSetExplicitly = false;
			}
			else
			{
				useDefaultCredentialsSetExplicitly = true;
			}
		}

		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		public string[] ListSecureMethods()
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("ListSecureMethods", new object[0]);
			return (string[])array[0];
		}

		public void ListSecureMethodsAsync()
		{
			ListSecureMethodsAsync(null);
		}

		public void ListSecureMethodsAsync(object userState)
		{
			if (ListSecureMethodsOperationCompleted == null)
			{
				ListSecureMethodsOperationCompleted = OnListSecureMethodsOperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("ListSecureMethods", new object[0], ListSecureMethodsOperationCompleted, userState);
		}

		private void OnListSecureMethodsOperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.ListSecureMethodsCompleted != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.ListSecureMethodsCompleted(this, new ListSecureMethodsCompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader("TrustedUserHeaderValue")]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo LoadReport(string Report, string HistoryID)
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("LoadReport", new object[2] { Report, HistoryID });
			return (ExecutionInfo)array[0];
		}

		public void LoadReportAsync(string Report, string HistoryID)
		{
			LoadReportAsync(Report, HistoryID, null);
		}

		public void LoadReportAsync(string Report, string HistoryID, object userState)
		{
			if (LoadReportOperationCompleted == null)
			{
				LoadReportOperationCompleted = OnLoadReportOperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("LoadReport", new object[2] { Report, HistoryID }, LoadReportOperationCompleted, userState);
		}

		private void OnLoadReportOperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.LoadReportCompleted != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.LoadReportCompleted(this, new LoadReportCompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader("TrustedUserHeaderValue")]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo2 LoadReport2(string Report, string HistoryID)
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("LoadReport2", new object[2] { Report, HistoryID });
			return (ExecutionInfo2)array[0];
		}

		public void LoadReport2Async(string Report, string HistoryID)
		{
			LoadReport2Async(Report, HistoryID, null);
		}

		public void LoadReport2Async(string Report, string HistoryID, object userState)
		{
			if (LoadReport2OperationCompleted == null)
			{
				LoadReport2OperationCompleted = OnLoadReport2OperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("LoadReport2", new object[2] { Report, HistoryID }, LoadReport2OperationCompleted, userState);
		}

		private void OnLoadReport2OperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.LoadReport2Completed != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.LoadReport2Completed(this, new LoadReport2CompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader("TrustedUserHeaderValue")]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo LoadReportDefinition([XmlElement(DataType = "base64Binary")] byte[] Definition, out Warning[] warnings)
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("LoadReportDefinition", new object[1] { Definition });
			warnings = (Warning[])array[1];
			return (ExecutionInfo)array[0];
		}

		public void LoadReportDefinitionAsync(byte[] Definition)
		{
			LoadReportDefinitionAsync(Definition, null);
		}

		public void LoadReportDefinitionAsync(byte[] Definition, object userState)
		{
			if (LoadReportDefinitionOperationCompleted == null)
			{
				LoadReportDefinitionOperationCompleted = OnLoadReportDefinitionOperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("LoadReportDefinition", new object[1] { Definition }, LoadReportDefinitionOperationCompleted, userState);
		}

		private void OnLoadReportDefinitionOperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.LoadReportDefinitionCompleted != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.LoadReportDefinitionCompleted(this, new LoadReportDefinitionCompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader("TrustedUserHeaderValue")]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo2 LoadReportDefinition2([XmlElement(DataType = "base64Binary")] byte[] Definition, out Warning[] warnings)
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("LoadReportDefinition2", new object[1] { Definition });
			warnings = (Warning[])array[1];
			return (ExecutionInfo2)array[0];
		}

		public void LoadReportDefinition2Async(byte[] Definition)
		{
			LoadReportDefinition2Async(Definition, null);
		}

		public void LoadReportDefinition2Async(byte[] Definition, object userState)
		{
			if (LoadReportDefinition2OperationCompleted == null)
			{
				LoadReportDefinition2OperationCompleted = OnLoadReportDefinition2OperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("LoadReportDefinition2", new object[1] { Definition }, LoadReportDefinition2OperationCompleted, userState);
		}

		private void OnLoadReportDefinition2OperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.LoadReportDefinition2Completed != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.LoadReportDefinition2Completed(this, new LoadReportDefinition2CompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader("TrustedUserHeaderValue")]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo SetExecutionCredentials(DataSourceCredentials[] Credentials)
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("SetExecutionCredentials", new object[1] { Credentials });
			return (ExecutionInfo)array[0];
		}

		public void SetExecutionCredentialsAsync(DataSourceCredentials[] Credentials)
		{
			SetExecutionCredentialsAsync(Credentials, null);
		}

		public void SetExecutionCredentialsAsync(DataSourceCredentials[] Credentials, object userState)
		{
			if (SetExecutionCredentialsOperationCompleted == null)
			{
				SetExecutionCredentialsOperationCompleted = OnSetExecutionCredentialsOperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("SetExecutionCredentials", new object[1] { Credentials }, SetExecutionCredentialsOperationCompleted, userState);
		}

		private void OnSetExecutionCredentialsOperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.SetExecutionCredentialsCompleted != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.SetExecutionCredentialsCompleted(this, new SetExecutionCredentialsCompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader("TrustedUserHeaderValue")]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo2 SetExecutionCredentials2(DataSourceCredentials[] Credentials)
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("SetExecutionCredentials2", new object[1] { Credentials });
			return (ExecutionInfo2)array[0];
		}

		public void SetExecutionCredentials2Async(DataSourceCredentials[] Credentials)
		{
			SetExecutionCredentials2Async(Credentials, null);
		}

		public void SetExecutionCredentials2Async(DataSourceCredentials[] Credentials, object userState)
		{
			if (SetExecutionCredentials2OperationCompleted == null)
			{
				SetExecutionCredentials2OperationCompleted = OnSetExecutionCredentials2OperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("SetExecutionCredentials2", new object[1] { Credentials }, SetExecutionCredentials2OperationCompleted, userState);
		}

		private void OnSetExecutionCredentials2OperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.SetExecutionCredentials2Completed != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.SetExecutionCredentials2Completed(this, new SetExecutionCredentials2CompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader("TrustedUserHeaderValue")]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo SetExecutionParameters(ParameterValue[] Parameters, string ParameterLanguage)
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("SetExecutionParameters", new object[2] { Parameters, ParameterLanguage });
			return (ExecutionInfo)array[0];
		}

		public void SetExecutionParametersAsync(ParameterValue[] Parameters, string ParameterLanguage)
		{
			SetExecutionParametersAsync(Parameters, ParameterLanguage, null);
		}

		public void SetExecutionParametersAsync(ParameterValue[] Parameters, string ParameterLanguage, object userState)
		{
			if (SetExecutionParametersOperationCompleted == null)
			{
				SetExecutionParametersOperationCompleted = OnSetExecutionParametersOperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("SetExecutionParameters", new object[2] { Parameters, ParameterLanguage }, SetExecutionParametersOperationCompleted, userState);
		}

		private void OnSetExecutionParametersOperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.SetExecutionParametersCompleted != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.SetExecutionParametersCompleted(this, new SetExecutionParametersCompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader("TrustedUserHeaderValue")]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo2 SetExecutionParameters2(ParameterValue[] Parameters, string ParameterLanguage)
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("SetExecutionParameters2", new object[2] { Parameters, ParameterLanguage });
			return (ExecutionInfo2)array[0];
		}

		public void SetExecutionParameters2Async(ParameterValue[] Parameters, string ParameterLanguage)
		{
			SetExecutionParameters2Async(Parameters, ParameterLanguage, null);
		}

		public void SetExecutionParameters2Async(ParameterValue[] Parameters, string ParameterLanguage, object userState)
		{
			if (SetExecutionParameters2OperationCompleted == null)
			{
				SetExecutionParameters2OperationCompleted = OnSetExecutionParameters2OperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("SetExecutionParameters2", new object[2] { Parameters, ParameterLanguage }, SetExecutionParameters2OperationCompleted, userState);
		}

		private void OnSetExecutionParameters2OperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.SetExecutionParameters2Completed != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.SetExecutionParameters2Completed(this, new SetExecutionParameters2CompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader("TrustedUserHeaderValue")]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo ResetExecution()
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("ResetExecution", new object[0]);
			return (ExecutionInfo)array[0];
		}

		public void ResetExecutionAsync()
		{
			ResetExecutionAsync(null);
		}

		public void ResetExecutionAsync(object userState)
		{
			if (ResetExecutionOperationCompleted == null)
			{
				ResetExecutionOperationCompleted = OnResetExecutionOperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("ResetExecution", new object[0], ResetExecutionOperationCompleted, userState);
		}

		private void OnResetExecutionOperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.ResetExecutionCompleted != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.ResetExecutionCompleted(this, new ResetExecutionCompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader("TrustedUserHeaderValue")]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo2 ResetExecution2()
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("ResetExecution2", new object[0]);
			return (ExecutionInfo2)array[0];
		}

		public void ResetExecution2Async()
		{
			ResetExecution2Async(null);
		}

		public void ResetExecution2Async(object userState)
		{
			if (ResetExecution2OperationCompleted == null)
			{
				ResetExecution2OperationCompleted = OnResetExecution2OperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("ResetExecution2", new object[0], ResetExecution2OperationCompleted, userState);
		}

		private void OnResetExecution2OperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.ResetExecution2Completed != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.ResetExecution2Completed(this, new ResetExecution2CompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader("TrustedUserHeaderValue")]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("Result", DataType = "base64Binary")]
		public byte[] Render(string Format, string DeviceInfo, out string Extension, out string MimeType, out string Encoding, out Warning[] Warnings, out string[] StreamIds)
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("Render", new object[2] { Format, DeviceInfo });
			Extension = (string)array[1];
			MimeType = (string)array[2];
			Encoding = (string)array[3];
			Warnings = (Warning[])array[4];
			StreamIds = (string[])array[5];
			return (byte[])array[0];
		}

		public void RenderAsync(string Format, string DeviceInfo)
		{
			RenderAsync(Format, DeviceInfo, null);
		}

		public void RenderAsync(string Format, string DeviceInfo, object userState)
		{
			if (RenderOperationCompleted == null)
			{
				RenderOperationCompleted = OnRenderOperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("Render", new object[2] { Format, DeviceInfo }, RenderOperationCompleted, userState);
		}

		private void OnRenderOperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.RenderCompleted != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.RenderCompleted(this, new RenderCompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader("TrustedUserHeaderValue")]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("Result", DataType = "base64Binary")]
		public byte[] Render2(string Format, string DeviceInfo, PageCountMode PaginationMode, out string Extension, out string MimeType, out string Encoding, out Warning[] Warnings, out string[] StreamIds)
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("Render2", new object[3] { Format, DeviceInfo, PaginationMode });
			Extension = (string)array[1];
			MimeType = (string)array[2];
			Encoding = (string)array[3];
			Warnings = (Warning[])array[4];
			StreamIds = (string[])array[5];
			return (byte[])array[0];
		}

		public void Render2Async(string Format, string DeviceInfo, PageCountMode PaginationMode)
		{
			Render2Async(Format, DeviceInfo, PaginationMode, null);
		}

		public void Render2Async(string Format, string DeviceInfo, PageCountMode PaginationMode, object userState)
		{
			if (Render2OperationCompleted == null)
			{
				Render2OperationCompleted = OnRender2OperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("Render2", new object[3] { Format, DeviceInfo, PaginationMode }, Render2OperationCompleted, userState);
		}

		private void OnRender2OperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.Render2Completed != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.Render2Completed(this, new Render2CompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader("TrustedUserHeaderValue")]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("Result", DataType = "base64Binary")]
		public byte[] RenderStream(string Format, string StreamID, string DeviceInfo, out string Encoding, out string MimeType)
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("RenderStream", new object[3] { Format, StreamID, DeviceInfo });
			Encoding = (string)array[1];
			MimeType = (string)array[2];
			return (byte[])array[0];
		}

		public void RenderStreamAsync(string Format, string StreamID, string DeviceInfo)
		{
			RenderStreamAsync(Format, StreamID, DeviceInfo, null);
		}

		public void RenderStreamAsync(string Format, string StreamID, string DeviceInfo, object userState)
		{
			if (RenderStreamOperationCompleted == null)
			{
				RenderStreamOperationCompleted = OnRenderStreamOperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("RenderStream", new object[3] { Format, StreamID, DeviceInfo }, RenderStreamOperationCompleted, userState);
		}

		private void OnRenderStreamOperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.RenderStreamCompleted != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.RenderStreamCompleted(this, new RenderStreamCompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader("TrustedUserHeaderValue")]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo GetExecutionInfo()
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("GetExecutionInfo", new object[0]);
			return (ExecutionInfo)array[0];
		}

		public void GetExecutionInfoAsync()
		{
			GetExecutionInfoAsync(null);
		}

		public void GetExecutionInfoAsync(object userState)
		{
			if (GetExecutionInfoOperationCompleted == null)
			{
				GetExecutionInfoOperationCompleted = OnGetExecutionInfoOperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("GetExecutionInfo", new object[0], GetExecutionInfoOperationCompleted, userState);
		}

		private void OnGetExecutionInfoOperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.GetExecutionInfoCompleted != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.GetExecutionInfoCompleted(this, new GetExecutionInfoCompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader("TrustedUserHeaderValue")]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("executionInfo")]
		public ExecutionInfo2 GetExecutionInfo2()
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("GetExecutionInfo2", new object[0]);
			return (ExecutionInfo2)array[0];
		}

		public void GetExecutionInfo2Async()
		{
			GetExecutionInfo2Async(null);
		}

		public void GetExecutionInfo2Async(object userState)
		{
			if (GetExecutionInfo2OperationCompleted == null)
			{
				GetExecutionInfo2OperationCompleted = OnGetExecutionInfo2OperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("GetExecutionInfo2", new object[0], GetExecutionInfo2OperationCompleted, userState);
		}

		private void OnGetExecutionInfo2OperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.GetExecutionInfo2Completed != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.GetExecutionInfo2Completed(this, new GetExecutionInfo2CompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader("TrustedUserHeaderValue")]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("result")]
		public DocumentMapNode GetDocumentMap()
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("GetDocumentMap", new object[0]);
			return (DocumentMapNode)array[0];
		}

		public void GetDocumentMapAsync()
		{
			GetDocumentMapAsync(null);
		}

		public void GetDocumentMapAsync(object userState)
		{
			if (GetDocumentMapOperationCompleted == null)
			{
				GetDocumentMapOperationCompleted = OnGetDocumentMapOperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("GetDocumentMap", new object[0], GetDocumentMapOperationCompleted, userState);
		}

		private void OnGetDocumentMapOperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.GetDocumentMapCompleted != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.GetDocumentMapCompleted(this, new GetDocumentMapCompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader("TrustedUserHeaderValue")]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("ExecutionInfo")]
		public ExecutionInfo LoadDrillthroughTarget(string DrillthroughID)
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("LoadDrillthroughTarget", new object[1] { DrillthroughID });
			return (ExecutionInfo)array[0];
		}

		public void LoadDrillthroughTargetAsync(string DrillthroughID)
		{
			LoadDrillthroughTargetAsync(DrillthroughID, null);
		}

		public void LoadDrillthroughTargetAsync(string DrillthroughID, object userState)
		{
			if (LoadDrillthroughTargetOperationCompleted == null)
			{
				LoadDrillthroughTargetOperationCompleted = OnLoadDrillthroughTargetOperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("LoadDrillthroughTarget", new object[1] { DrillthroughID }, LoadDrillthroughTargetOperationCompleted, userState);
		}

		private void OnLoadDrillthroughTargetOperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.LoadDrillthroughTargetCompleted != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.LoadDrillthroughTargetCompleted(this, new LoadDrillthroughTargetCompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader("TrustedUserHeaderValue")]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("ExecutionInfo")]
		public ExecutionInfo2 LoadDrillthroughTarget2(string DrillthroughID)
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("LoadDrillthroughTarget2", new object[1] { DrillthroughID });
			return (ExecutionInfo2)array[0];
		}

		public void LoadDrillthroughTarget2Async(string DrillthroughID)
		{
			LoadDrillthroughTarget2Async(DrillthroughID, null);
		}

		public void LoadDrillthroughTarget2Async(string DrillthroughID, object userState)
		{
			if (LoadDrillthroughTarget2OperationCompleted == null)
			{
				LoadDrillthroughTarget2OperationCompleted = OnLoadDrillthroughTarget2OperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("LoadDrillthroughTarget2", new object[1] { DrillthroughID }, LoadDrillthroughTarget2OperationCompleted, userState);
		}

		private void OnLoadDrillthroughTarget2OperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.LoadDrillthroughTarget2Completed != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.LoadDrillthroughTarget2Completed(this, new LoadDrillthroughTarget2CompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader("TrustedUserHeaderValue")]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("Found")]
		public bool ToggleItem(string ToggleID)
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("ToggleItem", new object[1] { ToggleID });
			return (bool)array[0];
		}

		public void ToggleItemAsync(string ToggleID)
		{
			ToggleItemAsync(ToggleID, null);
		}

		public void ToggleItemAsync(string ToggleID, object userState)
		{
			if (ToggleItemOperationCompleted == null)
			{
				ToggleItemOperationCompleted = OnToggleItemOperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("ToggleItem", new object[1] { ToggleID }, ToggleItemOperationCompleted, userState);
		}

		private void OnToggleItemOperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.ToggleItemCompleted != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.ToggleItemCompleted(this, new ToggleItemCompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader("TrustedUserHeaderValue")]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("PageNumber")]
		public int NavigateDocumentMap(string DocMapID)
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("NavigateDocumentMap", new object[1] { DocMapID });
			return (int)array[0];
		}

		public void NavigateDocumentMapAsync(string DocMapID)
		{
			NavigateDocumentMapAsync(DocMapID, null);
		}

		public void NavigateDocumentMapAsync(string DocMapID, object userState)
		{
			if (NavigateDocumentMapOperationCompleted == null)
			{
				NavigateDocumentMapOperationCompleted = OnNavigateDocumentMapOperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("NavigateDocumentMap", new object[1] { DocMapID }, NavigateDocumentMapOperationCompleted, userState);
		}

		private void OnNavigateDocumentMapOperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.NavigateDocumentMapCompleted != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.NavigateDocumentMapCompleted(this, new NavigateDocumentMapCompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader("TrustedUserHeaderValue")]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("PageNumber")]
		public int NavigateBookmark(string BookmarkID, out string UniqueName)
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("NavigateBookmark", new object[1] { BookmarkID });
			UniqueName = (string)array[1];
			return (int)array[0];
		}

		public void NavigateBookmarkAsync(string BookmarkID)
		{
			NavigateBookmarkAsync(BookmarkID, null);
		}

		public void NavigateBookmarkAsync(string BookmarkID, object userState)
		{
			if (NavigateBookmarkOperationCompleted == null)
			{
				NavigateBookmarkOperationCompleted = OnNavigateBookmarkOperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("NavigateBookmark", new object[1] { BookmarkID }, NavigateBookmarkOperationCompleted, userState);
		}

		private void OnNavigateBookmarkOperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.NavigateBookmarkCompleted != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.NavigateBookmarkCompleted(this, new NavigateBookmarkCompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader("TrustedUserHeaderValue")]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("PageNumber")]
		public int FindString(int StartPage, int EndPage, string FindValue)
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("FindString", new object[3] { StartPage, EndPage, FindValue });
			return (int)array[0];
		}

		public void FindStringAsync(int StartPage, int EndPage, string FindValue)
		{
			FindStringAsync(StartPage, EndPage, FindValue, null);
		}

		public void FindStringAsync(int StartPage, int EndPage, string FindValue, object userState)
		{
			if (FindStringOperationCompleted == null)
			{
				FindStringOperationCompleted = OnFindStringOperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("FindString", new object[3] { StartPage, EndPage, FindValue }, FindStringOperationCompleted, userState);
		}

		private void OnFindStringOperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.FindStringCompleted != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.FindStringCompleted(this, new FindStringCompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader("TrustedUserHeaderValue")]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("PageNumber")]
		public int Sort(string SortItem, SortDirectionEnum Direction, bool Clear, out string ReportItem, out int NumPages)
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("Sort", new object[3] { SortItem, Direction, Clear });
			ReportItem = (string)array[1];
			NumPages = (int)array[2];
			return (int)array[0];
		}

		public void SortAsync(string SortItem, SortDirectionEnum Direction, bool Clear)
		{
			SortAsync(SortItem, Direction, Clear, null);
		}

		public void SortAsync(string SortItem, SortDirectionEnum Direction, bool Clear, object userState)
		{
			if (SortOperationCompleted == null)
			{
				SortOperationCompleted = OnSortOperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("Sort", new object[3] { SortItem, Direction, Clear }, SortOperationCompleted, userState);
		}

		private void OnSortOperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.SortCompleted != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.SortCompleted(this, new SortCompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader("TrustedUserHeaderValue")]
		[SoapHeader("ExecutionHeaderValue")]
		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("PageNumber")]
		public int Sort2(string SortItem, SortDirectionEnum Direction, bool Clear, PageCountMode PaginationMode, out string ReportItem, out ExecutionInfo2 ExecutionInfo)
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("Sort2", new object[4] { SortItem, Direction, Clear, PaginationMode });
			ReportItem = (string)array[1];
			ExecutionInfo = (ExecutionInfo2)array[2];
			return (int)array[0];
		}

		public void Sort2Async(string SortItem, SortDirectionEnum Direction, bool Clear, PageCountMode PaginationMode)
		{
			Sort2Async(SortItem, Direction, Clear, PaginationMode, null);
		}

		public void Sort2Async(string SortItem, SortDirectionEnum Direction, bool Clear, PageCountMode PaginationMode, object userState)
		{
			if (Sort2OperationCompleted == null)
			{
				Sort2OperationCompleted = OnSort2OperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("Sort2", new object[4] { SortItem, Direction, Clear, PaginationMode }, Sort2OperationCompleted, userState);
		}

		private void OnSort2OperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.Sort2Completed != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.Sort2Completed(this, new Sort2CompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlElement("Result", DataType = "base64Binary")]
		public byte[] GetRenderResource(string Format, string DeviceInfo, out string MimeType)
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("GetRenderResource", new object[2] { Format, DeviceInfo });
			MimeType = (string)array[1];
			return (byte[])array[0];
		}

		public void GetRenderResourceAsync(string Format, string DeviceInfo)
		{
			GetRenderResourceAsync(Format, DeviceInfo, null);
		}

		public void GetRenderResourceAsync(string Format, string DeviceInfo, object userState)
		{
			if (GetRenderResourceOperationCompleted == null)
			{
				GetRenderResourceOperationCompleted = OnGetRenderResourceOperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("GetRenderResource", new object[2] { Format, DeviceInfo }, GetRenderResourceOperationCompleted, userState);
		}

		private void OnGetRenderResourceOperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.GetRenderResourceCompleted != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.GetRenderResourceCompleted(this, new GetRenderResourceCompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapHeader("TrustedUserHeaderValue")]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		[return: XmlArray("Extensions")]
		public Extension[] ListRenderingExtensions()
		{
			object[] array = ((SoapHttpClientProtocol)this).Invoke("ListRenderingExtensions", new object[0]);
			return (Extension[])array[0];
		}

		public void ListRenderingExtensionsAsync()
		{
			ListRenderingExtensionsAsync(null);
		}

		public void ListRenderingExtensionsAsync(object userState)
		{
			if (ListRenderingExtensionsOperationCompleted == null)
			{
				ListRenderingExtensionsOperationCompleted = OnListRenderingExtensionsOperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("ListRenderingExtensions", new object[0], ListRenderingExtensionsOperationCompleted, userState);
		}

		private void OnListRenderingExtensionsOperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			if (this.ListRenderingExtensionsCompleted != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.ListRenderingExtensionsCompleted(this, new ListRenderingExtensionsCompletedEventArgs(val.get_Results(), ((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		public void LogonUser(string userName, string password, string authority)
		{
			((SoapHttpClientProtocol)this).Invoke("LogonUser", new object[3] { userName, password, authority });
		}

		public void LogonUserAsync(string userName, string password, string authority)
		{
			LogonUserAsync(userName, password, authority, null);
		}

		public void LogonUserAsync(string userName, string password, string authority, object userState)
		{
			if (LogonUserOperationCompleted == null)
			{
				LogonUserOperationCompleted = OnLogonUserOperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("LogonUser", new object[3] { userName, password, authority }, LogonUserOperationCompleted, userState);
		}

		private void OnLogonUserOperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Expected O, but got Unknown
			if (this.LogonUserCompleted != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.LogonUserCompleted(this, new AsyncCompletedEventArgs(((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		[SoapHeader(/*Could not decode attribute arguments.*/)]
		[SoapDocumentMethod(/*Could not decode attribute arguments.*/)]
		public void Logoff()
		{
			((SoapHttpClientProtocol)this).Invoke("Logoff", new object[0]);
		}

		public void LogoffAsync()
		{
			LogoffAsync(null);
		}

		public void LogoffAsync(object userState)
		{
			if (LogoffOperationCompleted == null)
			{
				LogoffOperationCompleted = OnLogoffOperationCompleted;
			}
			((SoapHttpClientProtocol)this).InvokeAsync("Logoff", new object[0], LogoffOperationCompleted, userState);
		}

		private void OnLogoffOperationCompleted(object arg)
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Expected O, but got Unknown
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Expected O, but got Unknown
			if (this.LogoffCompleted != null)
			{
				InvokeCompletedEventArgs val = (InvokeCompletedEventArgs)arg;
				this.LogoffCompleted(this, new AsyncCompletedEventArgs(((AsyncCompletedEventArgs)val).get_Error(), ((AsyncCompletedEventArgs)val).get_Cancelled(), ((AsyncCompletedEventArgs)val).get_UserState()));
			}
		}

		public void CancelAsync(object userState)
		{
			((HttpWebClientProtocol)this).CancelAsync(userState);
		}

		private bool IsLocalFileSystemWebService(string url)
		{
			if (url == null || url == string.Empty)
			{
				return false;
			}
			Uri uri = new Uri(url);
			if (uri.Port >= 1024 && string.Compare(uri.Host, "localHost", StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}
			return false;
		}
	}
	[Serializable]
	[GeneratedCode("System.Xml", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[XmlRoot(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", IsNullable = false)]
	public class TrustedUserHeader : SoapHeader
	{
		private string userNameField;

		private byte[] userTokenField;

		private XmlAttribute[] anyAttrField;

		public string UserName
		{
			get
			{
				return userNameField;
			}
			set
			{
				userNameField = value;
			}
		}

		[XmlElement(DataType = "base64Binary")]
		public byte[] UserToken
		{
			get
			{
				return userTokenField;
			}
			set
			{
				userTokenField = value;
			}
		}

		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr
		{
			get
			{
				return anyAttrField;
			}
			set
			{
				anyAttrField = value;
			}
		}

		public TrustedUserHeader()
			: this()
		{
		}
	}
	[Serializable]
	[GeneratedCode("System.Xml", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	public class Extension
	{
		private ExtensionTypeEnum extensionTypeField;

		private string nameField;

		private string localizedNameField;

		private bool visibleField;

		private bool isModelGenerationSupportedField;

		public ExtensionTypeEnum ExtensionType
		{
			get
			{
				return extensionTypeField;
			}
			set
			{
				extensionTypeField = value;
			}
		}

		public string Name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		public string LocalizedName
		{
			get
			{
				return localizedNameField;
			}
			set
			{
				localizedNameField = value;
			}
		}

		public bool Visible
		{
			get
			{
				return visibleField;
			}
			set
			{
				visibleField = value;
			}
		}

		public bool IsModelGenerationSupported
		{
			get
			{
				return isModelGenerationSupportedField;
			}
			set
			{
				isModelGenerationSupportedField = value;
			}
		}
	}
	[Serializable]
	[GeneratedCode("System.Xml", "4.8.4084.0")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	public enum ExtensionTypeEnum
	{
		Delivery,
		Render,
		Data,
		All
	}
	[Serializable]
	[GeneratedCode("System.Xml", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	public class DocumentMapNode
	{
		private string labelField;

		private string uniqueNameField;

		private DocumentMapNode[] childrenField;

		public string Label
		{
			get
			{
				return labelField;
			}
			set
			{
				labelField = value;
			}
		}

		public string UniqueName
		{
			get
			{
				return uniqueNameField;
			}
			set
			{
				uniqueNameField = value;
			}
		}

		public DocumentMapNode[] Children
		{
			get
			{
				return childrenField;
			}
			set
			{
				childrenField = value;
			}
		}
	}
	[Serializable]
	[XmlInclude(typeof(ParameterValue))]
	[GeneratedCode("System.Xml", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	public class ParameterValueOrFieldReference
	{
	}
	[Serializable]
	[GeneratedCode("System.Xml", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	public class ParameterValue : ParameterValueOrFieldReference
	{
		private string nameField;

		private string valueField;

		private string labelField;

		public string Name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		public string Value
		{
			get
			{
				return valueField;
			}
			set
			{
				valueField = value;
			}
		}

		public string Label
		{
			get
			{
				return labelField;
			}
			set
			{
				labelField = value;
			}
		}
	}
	[Serializable]
	[GeneratedCode("System.Xml", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	public class DataSourceCredentials
	{
		private string dataSourceNameField;

		private string userNameField;

		private string passwordField;

		public string DataSourceName
		{
			get
			{
				return dataSourceNameField;
			}
			set
			{
				dataSourceNameField = value;
			}
		}

		public string UserName
		{
			get
			{
				return userNameField;
			}
			set
			{
				userNameField = value;
			}
		}

		public string Password
		{
			get
			{
				return passwordField;
			}
			set
			{
				passwordField = value;
			}
		}
	}
	[Serializable]
	[GeneratedCode("System.Xml", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	public class Warning
	{
		private string codeField;

		private string severityField;

		private string objectNameField;

		private string objectTypeField;

		private string messageField;

		public string Code
		{
			get
			{
				return codeField;
			}
			set
			{
				codeField = value;
			}
		}

		public string Severity
		{
			get
			{
				return severityField;
			}
			set
			{
				severityField = value;
			}
		}

		public string ObjectName
		{
			get
			{
				return objectNameField;
			}
			set
			{
				objectNameField = value;
			}
		}

		public string ObjectType
		{
			get
			{
				return objectTypeField;
			}
			set
			{
				objectTypeField = value;
			}
		}

		public string Message
		{
			get
			{
				return messageField;
			}
			set
			{
				messageField = value;
			}
		}
	}
	[Serializable]
	[GeneratedCode("System.Xml", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	public class ReportMargins
	{
		private double topField;

		private double bottomField;

		private double leftField;

		private double rightField;

		public double Top
		{
			get
			{
				return topField;
			}
			set
			{
				topField = value;
			}
		}

		public double Bottom
		{
			get
			{
				return bottomField;
			}
			set
			{
				bottomField = value;
			}
		}

		public double Left
		{
			get
			{
				return leftField;
			}
			set
			{
				leftField = value;
			}
		}

		public double Right
		{
			get
			{
				return rightField;
			}
			set
			{
				rightField = value;
			}
		}
	}
	[Serializable]
	[GeneratedCode("System.Xml", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	public class ReportPaperSize
	{
		private double heightField;

		private double widthField;

		public double Height
		{
			get
			{
				return heightField;
			}
			set
			{
				heightField = value;
			}
		}

		public double Width
		{
			get
			{
				return widthField;
			}
			set
			{
				widthField = value;
			}
		}
	}
	[Serializable]
	[GeneratedCode("System.Xml", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	public class PageSettings
	{
		private ReportPaperSize paperSizeField;

		private ReportMargins marginsField;

		public ReportPaperSize PaperSize
		{
			get
			{
				return paperSizeField;
			}
			set
			{
				paperSizeField = value;
			}
		}

		public ReportMargins Margins
		{
			get
			{
				return marginsField;
			}
			set
			{
				marginsField = value;
			}
		}
	}
	[Serializable]
	[GeneratedCode("System.Xml", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	public class DataSourcePrompt
	{
		private string nameField;

		private string dataSourceIDField;

		private string promptField;

		public string Name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		public string DataSourceID
		{
			get
			{
				return dataSourceIDField;
			}
			set
			{
				dataSourceIDField = value;
			}
		}

		public string Prompt
		{
			get
			{
				return promptField;
			}
			set
			{
				promptField = value;
			}
		}
	}
	[Serializable]
	[GeneratedCode("System.Xml", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	public class ValidValue
	{
		private string labelField;

		private string valueField;

		public string Label
		{
			get
			{
				return labelField;
			}
			set
			{
				labelField = value;
			}
		}

		public string Value
		{
			get
			{
				return valueField;
			}
			set
			{
				valueField = value;
			}
		}
	}
	[Serializable]
	[GeneratedCode("System.Xml", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	public class ReportParameter
	{
		private string nameField;

		private ParameterTypeEnum typeField;

		private bool typeFieldSpecified;

		private bool nullableField;

		private bool nullableFieldSpecified;

		private bool allowBlankField;

		private bool allowBlankFieldSpecified;

		private bool multiValueField;

		private bool multiValueFieldSpecified;

		private bool queryParameterField;

		private bool queryParameterFieldSpecified;

		private string promptField;

		private bool promptUserField;

		private bool promptUserFieldSpecified;

		private string[] dependenciesField;

		private bool validValuesQueryBasedField;

		private bool validValuesQueryBasedFieldSpecified;

		private ValidValue[] validValuesField;

		private bool defaultValuesQueryBasedField;

		private bool defaultValuesQueryBasedFieldSpecified;

		private string[] defaultValuesField;

		private ParameterStateEnum stateField;

		private bool stateFieldSpecified;

		private string errorMessageField;

		public string Name
		{
			get
			{
				return nameField;
			}
			set
			{
				nameField = value;
			}
		}

		public ParameterTypeEnum Type
		{
			get
			{
				return typeField;
			}
			set
			{
				typeField = value;
			}
		}

		[XmlIgnore]
		public bool TypeSpecified
		{
			get
			{
				return typeFieldSpecified;
			}
			set
			{
				typeFieldSpecified = value;
			}
		}

		public bool Nullable
		{
			get
			{
				return nullableField;
			}
			set
			{
				nullableField = value;
			}
		}

		[XmlIgnore]
		public bool NullableSpecified
		{
			get
			{
				return nullableFieldSpecified;
			}
			set
			{
				nullableFieldSpecified = value;
			}
		}

		public bool AllowBlank
		{
			get
			{
				return allowBlankField;
			}
			set
			{
				allowBlankField = value;
			}
		}

		[XmlIgnore]
		public bool AllowBlankSpecified
		{
			get
			{
				return allowBlankFieldSpecified;
			}
			set
			{
				allowBlankFieldSpecified = value;
			}
		}

		public bool MultiValue
		{
			get
			{
				return multiValueField;
			}
			set
			{
				multiValueField = value;
			}
		}

		[XmlIgnore]
		public bool MultiValueSpecified
		{
			get
			{
				return multiValueFieldSpecified;
			}
			set
			{
				multiValueFieldSpecified = value;
			}
		}

		public bool QueryParameter
		{
			get
			{
				return queryParameterField;
			}
			set
			{
				queryParameterField = value;
			}
		}

		[XmlIgnore]
		public bool QueryParameterSpecified
		{
			get
			{
				return queryParameterFieldSpecified;
			}
			set
			{
				queryParameterFieldSpecified = value;
			}
		}

		public string Prompt
		{
			get
			{
				return promptField;
			}
			set
			{
				promptField = value;
			}
		}

		public bool PromptUser
		{
			get
			{
				return promptUserField;
			}
			set
			{
				promptUserField = value;
			}
		}

		[XmlIgnore]
		public bool PromptUserSpecified
		{
			get
			{
				return promptUserFieldSpecified;
			}
			set
			{
				promptUserFieldSpecified = value;
			}
		}

		[XmlArrayItem("Dependency")]
		public string[] Dependencies
		{
			get
			{
				return dependenciesField;
			}
			set
			{
				dependenciesField = value;
			}
		}

		public bool ValidValuesQueryBased
		{
			get
			{
				return validValuesQueryBasedField;
			}
			set
			{
				validValuesQueryBasedField = value;
			}
		}

		[XmlIgnore]
		public bool ValidValuesQueryBasedSpecified
		{
			get
			{
				return validValuesQueryBasedFieldSpecified;
			}
			set
			{
				validValuesQueryBasedFieldSpecified = value;
			}
		}

		public ValidValue[] ValidValues
		{
			get
			{
				return validValuesField;
			}
			set
			{
				validValuesField = value;
			}
		}

		public bool DefaultValuesQueryBased
		{
			get
			{
				return defaultValuesQueryBasedField;
			}
			set
			{
				defaultValuesQueryBasedField = value;
			}
		}

		[XmlIgnore]
		public bool DefaultValuesQueryBasedSpecified
		{
			get
			{
				return defaultValuesQueryBasedFieldSpecified;
			}
			set
			{
				defaultValuesQueryBasedFieldSpecified = value;
			}
		}

		[XmlArrayItem("Value")]
		public string[] DefaultValues
		{
			get
			{
				return defaultValuesField;
			}
			set
			{
				defaultValuesField = value;
			}
		}

		public ParameterStateEnum State
		{
			get
			{
				return stateField;
			}
			set
			{
				stateField = value;
			}
		}

		[XmlIgnore]
		public bool StateSpecified
		{
			get
			{
				return stateFieldSpecified;
			}
			set
			{
				stateFieldSpecified = value;
			}
		}

		public string ErrorMessage
		{
			get
			{
				return errorMessageField;
			}
			set
			{
				errorMessageField = value;
			}
		}
	}
	[Serializable]
	[GeneratedCode("System.Xml", "4.8.4084.0")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	public enum ParameterTypeEnum
	{
		Boolean,
		DateTime,
		Integer,
		Float,
		String
	}
	[Serializable]
	[GeneratedCode("System.Xml", "4.8.4084.0")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	public enum ParameterStateEnum
	{
		HasValidValue,
		MissingValidValue,
		HasOutstandingDependencies,
		DynamicValuesUnavailable
	}
	[Serializable]
	[XmlInclude(typeof(ExecutionInfo2))]
	[GeneratedCode("System.Xml", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	public class ExecutionInfo
	{
		private bool hasSnapshotField;

		private bool needsProcessingField;

		private bool allowQueryExecutionField;

		private bool credentialsRequiredField;

		private bool parametersRequiredField;

		private DateTime expirationDateTimeField;

		private DateTime executionDateTimeField;

		private int numPagesField;

		private ReportParameter[] parametersField;

		private DataSourcePrompt[] dataSourcePromptsField;

		private bool hasDocumentMapField;

		private string executionIDField;

		private string reportPathField;

		private string historyIDField;

		private PageSettings reportPageSettingsField;

		private int autoRefreshIntervalField;

		public bool HasSnapshot
		{
			get
			{
				return hasSnapshotField;
			}
			set
			{
				hasSnapshotField = value;
			}
		}

		public bool NeedsProcessing
		{
			get
			{
				return needsProcessingField;
			}
			set
			{
				needsProcessingField = value;
			}
		}

		public bool AllowQueryExecution
		{
			get
			{
				return allowQueryExecutionField;
			}
			set
			{
				allowQueryExecutionField = value;
			}
		}

		public bool CredentialsRequired
		{
			get
			{
				return credentialsRequiredField;
			}
			set
			{
				credentialsRequiredField = value;
			}
		}

		public bool ParametersRequired
		{
			get
			{
				return parametersRequiredField;
			}
			set
			{
				parametersRequiredField = value;
			}
		}

		public DateTime ExpirationDateTime
		{
			get
			{
				return expirationDateTimeField;
			}
			set
			{
				expirationDateTimeField = value;
			}
		}

		public DateTime ExecutionDateTime
		{
			get
			{
				return executionDateTimeField;
			}
			set
			{
				executionDateTimeField = value;
			}
		}

		public int NumPages
		{
			get
			{
				return numPagesField;
			}
			set
			{
				numPagesField = value;
			}
		}

		public ReportParameter[] Parameters
		{
			get
			{
				return parametersField;
			}
			set
			{
				parametersField = value;
			}
		}

		public DataSourcePrompt[] DataSourcePrompts
		{
			get
			{
				return dataSourcePromptsField;
			}
			set
			{
				dataSourcePromptsField = value;
			}
		}

		public bool HasDocumentMap
		{
			get
			{
				return hasDocumentMapField;
			}
			set
			{
				hasDocumentMapField = value;
			}
		}

		public string ExecutionID
		{
			get
			{
				return executionIDField;
			}
			set
			{
				executionIDField = value;
			}
		}

		public string ReportPath
		{
			get
			{
				return reportPathField;
			}
			set
			{
				reportPathField = value;
			}
		}

		public string HistoryID
		{
			get
			{
				return historyIDField;
			}
			set
			{
				historyIDField = value;
			}
		}

		public PageSettings ReportPageSettings
		{
			get
			{
				return reportPageSettingsField;
			}
			set
			{
				reportPageSettingsField = value;
			}
		}

		public int AutoRefreshInterval
		{
			get
			{
				return autoRefreshIntervalField;
			}
			set
			{
				autoRefreshIntervalField = value;
			}
		}
	}
	[Serializable]
	[GeneratedCode("System.Xml", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	public class ExecutionInfo2 : ExecutionInfo
	{
		private PageCountMode pageCountModeField;

		public PageCountMode PageCountMode
		{
			get
			{
				return pageCountModeField;
			}
			set
			{
				pageCountModeField = value;
			}
		}
	}
	[Serializable]
	[GeneratedCode("System.Xml", "4.8.4084.0")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	public enum PageCountMode
	{
		Actual,
		Estimate
	}
	[Serializable]
	[GeneratedCode("System.Xml", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[XmlRoot(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", IsNullable = false)]
	public class ServerInfoHeader : SoapHeader
	{
		private string reportServerVersionNumberField;

		private string reportServerEditionField;

		private string reportServerVersionField;

		private string reportServerDateTimeField;

		private XmlAttribute[] anyAttrField;

		public string ReportServerVersionNumber
		{
			get
			{
				return reportServerVersionNumberField;
			}
			set
			{
				reportServerVersionNumberField = value;
			}
		}

		public string ReportServerEdition
		{
			get
			{
				return reportServerEditionField;
			}
			set
			{
				reportServerEditionField = value;
			}
		}

		public string ReportServerVersion
		{
			get
			{
				return reportServerVersionField;
			}
			set
			{
				reportServerVersionField = value;
			}
		}

		public string ReportServerDateTime
		{
			get
			{
				return reportServerDateTimeField;
			}
			set
			{
				reportServerDateTimeField = value;
			}
		}

		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr
		{
			get
			{
				return anyAttrField;
			}
			set
			{
				anyAttrField = value;
			}
		}

		public ServerInfoHeader()
			: this()
		{
		}
	}
	[Serializable]
	[GeneratedCode("System.Xml", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	[XmlRoot(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices", IsNullable = false)]
	public class ExecutionHeader : SoapHeader
	{
		private string executionIDField;

		private XmlAttribute[] anyAttrField;

		public string ExecutionID
		{
			get
			{
				return executionIDField;
			}
			set
			{
				executionIDField = value;
			}
		}

		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr
		{
			get
			{
				return anyAttrField;
			}
			set
			{
				anyAttrField = value;
			}
		}

		public ExecutionHeader()
			: this()
		{
		}
	}
	[Serializable]
	[GeneratedCode("System.Xml", "4.8.4084.0")]
	[XmlType(Namespace = "http://schemas.microsoft.com/sqlserver/2005/06/30/reporting/reportingservices")]
	public enum SortDirectionEnum
	{
		None,
		Ascending,
		Descending
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void ListSecureMethodsCompletedEventHandler(object sender, ListSecureMethodsCompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListSecureMethodsCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public string[] Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (string[])results[0];
			}
		}

		internal ListSecureMethodsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void LoadReportCompletedEventHandler(object sender, LoadReportCompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class LoadReportCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public ExecutionInfo Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (ExecutionInfo)results[0];
			}
		}

		internal LoadReportCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void LoadReport2CompletedEventHandler(object sender, LoadReport2CompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class LoadReport2CompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public ExecutionInfo2 Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (ExecutionInfo2)results[0];
			}
		}

		internal LoadReport2CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void LoadReportDefinitionCompletedEventHandler(object sender, LoadReportDefinitionCompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class LoadReportDefinitionCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public ExecutionInfo Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (ExecutionInfo)results[0];
			}
		}

		public Warning[] warnings
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (Warning[])results[1];
			}
		}

		internal LoadReportDefinitionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void LoadReportDefinition2CompletedEventHandler(object sender, LoadReportDefinition2CompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class LoadReportDefinition2CompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public ExecutionInfo2 Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (ExecutionInfo2)results[0];
			}
		}

		public Warning[] warnings
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (Warning[])results[1];
			}
		}

		internal LoadReportDefinition2CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void SetExecutionCredentialsCompletedEventHandler(object sender, SetExecutionCredentialsCompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class SetExecutionCredentialsCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public ExecutionInfo Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (ExecutionInfo)results[0];
			}
		}

		internal SetExecutionCredentialsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void SetExecutionCredentials2CompletedEventHandler(object sender, SetExecutionCredentials2CompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class SetExecutionCredentials2CompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public ExecutionInfo2 Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (ExecutionInfo2)results[0];
			}
		}

		internal SetExecutionCredentials2CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void SetExecutionParametersCompletedEventHandler(object sender, SetExecutionParametersCompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class SetExecutionParametersCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public ExecutionInfo Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (ExecutionInfo)results[0];
			}
		}

		internal SetExecutionParametersCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void SetExecutionParameters2CompletedEventHandler(object sender, SetExecutionParameters2CompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class SetExecutionParameters2CompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public ExecutionInfo2 Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (ExecutionInfo2)results[0];
			}
		}

		internal SetExecutionParameters2CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void ResetExecutionCompletedEventHandler(object sender, ResetExecutionCompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ResetExecutionCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public ExecutionInfo Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (ExecutionInfo)results[0];
			}
		}

		internal ResetExecutionCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void ResetExecution2CompletedEventHandler(object sender, ResetExecution2CompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ResetExecution2CompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public ExecutionInfo2 Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (ExecutionInfo2)results[0];
			}
		}

		internal ResetExecution2CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void RenderCompletedEventHandler(object sender, RenderCompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class RenderCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public byte[] Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (byte[])results[0];
			}
		}

		public string Extension
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (string)results[1];
			}
		}

		public string MimeType
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (string)results[2];
			}
		}

		public string Encoding
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (string)results[3];
			}
		}

		public Warning[] Warnings
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (Warning[])results[4];
			}
		}

		public string[] StreamIds
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (string[])results[5];
			}
		}

		internal RenderCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void Render2CompletedEventHandler(object sender, Render2CompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class Render2CompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public byte[] Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (byte[])results[0];
			}
		}

		public string Extension
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (string)results[1];
			}
		}

		public string MimeType
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (string)results[2];
			}
		}

		public string Encoding
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (string)results[3];
			}
		}

		public Warning[] Warnings
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (Warning[])results[4];
			}
		}

		public string[] StreamIds
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (string[])results[5];
			}
		}

		internal Render2CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void RenderStreamCompletedEventHandler(object sender, RenderStreamCompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class RenderStreamCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public byte[] Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (byte[])results[0];
			}
		}

		public string Encoding
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (string)results[1];
			}
		}

		public string MimeType
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (string)results[2];
			}
		}

		internal RenderStreamCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void GetExecutionInfoCompletedEventHandler(object sender, GetExecutionInfoCompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetExecutionInfoCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public ExecutionInfo Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (ExecutionInfo)results[0];
			}
		}

		internal GetExecutionInfoCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void GetExecutionInfo2CompletedEventHandler(object sender, GetExecutionInfo2CompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetExecutionInfo2CompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public ExecutionInfo2 Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (ExecutionInfo2)results[0];
			}
		}

		internal GetExecutionInfo2CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void GetDocumentMapCompletedEventHandler(object sender, GetDocumentMapCompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetDocumentMapCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public DocumentMapNode Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (DocumentMapNode)results[0];
			}
		}

		internal GetDocumentMapCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void LoadDrillthroughTargetCompletedEventHandler(object sender, LoadDrillthroughTargetCompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class LoadDrillthroughTargetCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public ExecutionInfo Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (ExecutionInfo)results[0];
			}
		}

		internal LoadDrillthroughTargetCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void LoadDrillthroughTarget2CompletedEventHandler(object sender, LoadDrillthroughTarget2CompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class LoadDrillthroughTarget2CompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public ExecutionInfo2 Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (ExecutionInfo2)results[0];
			}
		}

		internal LoadDrillthroughTarget2CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void ToggleItemCompletedEventHandler(object sender, ToggleItemCompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ToggleItemCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public bool Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (bool)results[0];
			}
		}

		internal ToggleItemCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void NavigateDocumentMapCompletedEventHandler(object sender, NavigateDocumentMapCompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class NavigateDocumentMapCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public int Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (int)results[0];
			}
		}

		internal NavigateDocumentMapCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void NavigateBookmarkCompletedEventHandler(object sender, NavigateBookmarkCompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class NavigateBookmarkCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public int Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (int)results[0];
			}
		}

		public string UniqueName
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (string)results[1];
			}
		}

		internal NavigateBookmarkCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void FindStringCompletedEventHandler(object sender, FindStringCompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class FindStringCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public int Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (int)results[0];
			}
		}

		internal FindStringCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void SortCompletedEventHandler(object sender, SortCompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class SortCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public int Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (int)results[0];
			}
		}

		public string ReportItem
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (string)results[1];
			}
		}

		public int NumPages
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (int)results[2];
			}
		}

		internal SortCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void Sort2CompletedEventHandler(object sender, Sort2CompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class Sort2CompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public int Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (int)results[0];
			}
		}

		public string ReportItem
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (string)results[1];
			}
		}

		public ExecutionInfo2 ExecutionInfo
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (ExecutionInfo2)results[2];
			}
		}

		internal Sort2CompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void GetRenderResourceCompletedEventHandler(object sender, GetRenderResourceCompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class GetRenderResourceCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public byte[] Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (byte[])results[0];
			}
		}

		public string MimeType
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (string)results[1];
			}
		}

		internal GetRenderResourceCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void ListRenderingExtensionsCompletedEventHandler(object sender, ListRenderingExtensionsCompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	public class ListRenderingExtensionsCompletedEventArgs : AsyncCompletedEventArgs
	{
		private object[] results;

		public Extension[] Result
		{
			get
			{
				((AsyncCompletedEventArgs)this).RaiseExceptionIfNecessary();
				return (Extension[])results[0];
			}
		}

		internal ListRenderingExtensionsCompletedEventArgs(object[] results, Exception exception, bool cancelled, object userState)
			: this(exception, cancelled, userState)
		{
			this.results = results;
		}
	}
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void LogonUserCompletedEventHandler(object sender, AsyncCompletedEventArgs e);
	[GeneratedCode("System.Web.Services", "4.8.4084.0")]
	public delegate void LogoffCompletedEventHandler(object sender, AsyncCompletedEventArgs e);
}
namespace CodaBears.Epicor10.Properties
{
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	public class Resources
	{
		private static ResourceManager resourceMan;

		private static CultureInfo resourceCulture;

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static ResourceManager ResourceManager
		{
			get
			{
				if (resourceMan == null)
				{
					ResourceManager resourceManager = (resourceMan = new ResourceManager("CodaBears.Epicor10.Properties.Resources", typeof(Resources).Assembly));
				}
				return resourceMan;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static CultureInfo Culture
		{
			get
			{
				return resourceCulture;
			}
			set
			{
				resourceCulture = value;
			}
		}

		public static Icon bluetooth
		{
			get
			{
				//IL_0017: Unknown result type (might be due to invalid IL or missing references)
				//IL_001d: Expected O, but got Unknown
				object @object = ResourceManager.GetObject("bluetooth", resourceCulture);
				return (Icon)@object;
			}
		}

		public static Icon cbi_email
		{
			get
			{
				//IL_0017: Unknown result type (might be due to invalid IL or missing references)
				//IL_001d: Expected O, but got Unknown
				object @object = ResourceManager.GetObject("cbi_email", resourceCulture);
				return (Icon)@object;
			}
		}

		public static Icon CBIIcon
		{
			get
			{
				//IL_0017: Unknown result type (might be due to invalid IL or missing references)
				//IL_001d: Expected O, but got Unknown
				object @object = ResourceManager.GetObject("CBIIcon", resourceCulture);
				return (Icon)@object;
			}
		}

		public static Icon letter
		{
			get
			{
				//IL_0017: Unknown result type (might be due to invalid IL or missing references)
				//IL_001d: Expected O, but got Unknown
				object @object = ResourceManager.GetObject("letter", resourceCulture);
				return (Icon)@object;
			}
		}

		public static Icon mail
		{
			get
			{
				//IL_0017: Unknown result type (might be due to invalid IL or missing references)
				//IL_001d: Expected O, but got Unknown
				object @object = ResourceManager.GetObject("mail", resourceCulture);
				return (Icon)@object;
			}
		}

		public static Icon mail_alt
		{
			get
			{
				//IL_0017: Unknown result type (might be due to invalid IL or missing references)
				//IL_001d: Expected O, but got Unknown
				object @object = ResourceManager.GetObject("mail_alt", resourceCulture);
				return (Icon)@object;
			}
		}

		public static Icon mail_unread
		{
			get
			{
				//IL_0017: Unknown result type (might be due to invalid IL or missing references)
				//IL_001d: Expected O, but got Unknown
				object @object = ResourceManager.GetObject("mail_unread", resourceCulture);
				return (Icon)@object;
			}
		}

		public static Icon print
		{
			get
			{
				//IL_0017: Unknown result type (might be due to invalid IL or missing references)
				//IL_001d: Expected O, but got Unknown
				object @object = ResourceManager.GetObject("print", resourceCulture);
				return (Icon)@object;
			}
		}

		[SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
		internal Resources()
		{
		}
	}
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.8.1.0")]
	internal sealed class Settings : ApplicationSettingsBase
	{
		private static Settings defaultInstance = (Settings)(object)SettingsBase.Synchronized((SettingsBase)(object)new Settings());

		public static Settings Default => defaultInstance;

		[ApplicationScopedSetting]
		[DebuggerNonUserCode]
		[SpecialSetting(/*Could not decode attribute arguments.*/)]
		[DefaultSettingValue("http://sql:80/ReportServer/ReportExecution2005.asmx")]
		public string CodaBears_Epicor10_ReportServices_ReportExecutionService => (string)((SettingsBase)this).get_Item("CodaBears_Epicor10_ReportServices_ReportExecutionService");

		public Settings()
			: this()
		{
		}
	}
}
