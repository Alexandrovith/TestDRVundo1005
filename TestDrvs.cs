/*@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

~~~~~~~~~	Проект:			Тест библиотеки опроса приборов (driver) BeltransgasDrv  _
~~~~~~~~~	Прибор:			Все приборы                           									 _
~~~~~~~~~	Модуль:			Головной                                       					 _
~~~~~~~~~	Разработка:	Демешкевич С.А.                                    		 	 _
~~~~~~~~~	Дата:				25.04.2015                                         		 	 _

@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@*/

using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using AXONIM.BelTransGasDRV;
using System.IO;
using DeviceInterfaces;
using System.Runtime.InteropServices;
using System.Reflection;
using AXONIM.ScanParamOfDevicves;
using AXONIM.CONSTS;
using TestDRVtransGas.TCPtoTCP;
using TestDRVtransGas.TCPtoComPort;
using TestDRVtransGas.ExtractZip;

namespace TestDRVtransGas
{
	public interface IUserControls
	{
		void ReSize ();
		void Close ();
	}

	public partial class FTestDrvs : Form
	{
		IUserControls[] ControlsList;
		TDevices Devices = null;
		TParams Params = null;
		internal CTestExch TestExch;
		//TCPserver.CTCPserver TCPserver;
		TestsDifferent.CTestDifferent TestsDiff = null;

		readonly string sCaption;
		System.Timers.Timer TrStatExchange = new System.Timers.Timer (420);

		CTCPtoTCP TCPtoTCP;
		CTCPtoComPort TCPtoComPort;


		public FTestDrvs ()
		{
			InitializeComponent ();
			if (Global.LogServ == null)
				Global.LogServ = new AXONIM.CONSTS.LOG.CLogs ("LogServ", "");

			DTBeginWork = DateTime.Now;
			TrWorkTime.Elapsed += Elapsed_CalcWorkTime;
			TrWorkTime.Start ();
			asCurrentDirectory = Directory.GetCurrentDirectory ();

			Devices = new TDevices (TPDevices, this);
			// Фирст даты архивов 
			if (Properties.Settings.Default.DTArchDay > TPArchDay.MinDate)
				TPArchDay.Value = Properties.Settings.Default.DTArchDay;
			else
				Properties.Settings.Default.DTArchDay = TPArchDay.Value;
			if (Properties.Settings.Default.DTArchHour > TPArchHour.MinDate)
				TPArchHour.Value = Properties.Settings.Default.DTArchHour;
			else
				Properties.Settings.Default.DTArchHour = TPArchHour.Value;
			if (Properties.Settings.Default.DTInterfer > TPInterfer.MinDate)
				TPInterfer.Value = Properties.Settings.Default.DTInterfer;
			else
				Properties.Settings.Default.DTInterfer = TPInterfer.Value;
			if (Properties.Settings.Default.DTAlarm > TPAlarm.MinDate)
				TPAlarm.Value = Properties.Settings.Default.DTAlarm;
			else
				Properties.Settings.Default.DTAlarm = TPAlarm.Value;
			SetNewDTreadArch ();

			Params = new TParams (TPParams, this);
			Text += " [driver v " + DRV.GetVersion () + "]";
			LVersion.Text = "v " + Version;
			sCaption = Text;
			Move -= FTestDrivers_Move;
			Resize -= FTestDrivers_Resize;
			Left = Properties.Settings.Default.iLeftForm;
			//Height = Params.GV.RowCount * 18 + 200;
			//if (Height > Screen.PrimaryScreen.WorkingArea.Height)
			//	Height = Screen.PrimaryScreen.WorkingArea.Height;
			//else
			//	Top = Screen.PrimaryScreen.WorkingArea.Height - Height;
			Height = Properties.Settings.Default.iHeightForm;
			Width = Properties.Settings.Default.iWidthForm;
			Top = Properties.Settings.Default.iTopForm;
			Move += FTestDrivers_Move;
			Resize += FTestDrivers_Resize;

			BTest1.Select ();

			CreateTemplateIndic ();
			CBTypeData.Items.AddRange (Enum.GetNames (typeof (TYPE)));
			CBTypeData.Items.RemoveAt (CBTypeData.Items.Count - 1);
			int iTypeData = Properties.Settings.Default.iTypeData;
			if (iTypeData < CBTypeData.Items.Count)
				CBTypeData.SelectedIndex = iTypeData;
			else CBTypeData.SelectedIndex = 0;

			CBDataWritePar.SelectedIndexChanged -= CBDataWritePar_SelectedIndexChanged;
			CBDataWritePar.Items.AddRange (Properties.Settings.Default.asDataWritePar.Split (';'));
			if (Properties.Settings.Default.iDataWritePar >= CBDataWritePar.Items.Count)
			{
				CBDataWritePar.SelectedIndex = 0;
				Properties.Settings.Default.iDataWritePar = 0;
				Properties.Settings.Default.Save ();
			}
			else CBDataWritePar.SelectedIndex = Properties.Settings.Default.iDataWritePar;
			CBDataWritePar.SelectedIndexChanged += CBDataWritePar_SelectedIndexChanged;

			CBData.TextChanged -= CBData_TextChanged;
			CBData.SelectedIndexChanged -= CBData_SelectedIndexChanged;
			CBData.Items.AddRange (Properties.Settings.Default.asData.Split (';'));
			CBData.SelectedIndex = Properties.Settings.Default.iData;
			CBData.SelectedIndexChanged += CBData_SelectedIndexChanged;
			CBData.TextChanged += CBData_TextChanged;

			CBParameterName.TextChanged -= CBParameterName_TextChanged;
			CBParameterName.SelectedIndexChanged -= CBParameterName_SelectedIndexChanged;
			CBParameterName.Items.AddRange (Properties.Settings.Default.asParameterName.Split (';'));
			CBParameterName.SelectedIndex = Properties.Settings.Default.iParameterName;
			CBParameterName.SelectedIndexChanged += CBParameterName_SelectedIndexChanged;
			CBParameterName.TextChanged += CBParameterName_TextChanged;

			NTimeResp.Value = Properties.Settings.Default.dmTimeResp;

			TestExch = new CTestExch (TPTestExch);
			TestsDiff = new TestsDifferent.CTestDifferent (this, TPTest);

			TrStatExchange.Elapsed += TrStatExchange_Elapsed;
			FillDevices ();
			OutData = StatExchan;

			FTestDrivers_Resize (this, null);

			UDIntervalScan.ValueChanged -= NIntervalScan_ValueChanged;
			TScanParam.Interval = Properties.Settings.Default.iIntervalScan;
			UDIntervalScan.Value = TScanParam.Interval;
			UDIntervalScan.ValueChanged += NIntervalScan_ValueChanged;

			CBReverceValWr.SelectedIndexChanged -= CBReverceValWr_SelectedIndexChanged;
			CBReverceValWr.SelectedIndex = Properties.Settings.Default.iReverceValWr;
			CBReverceValWr.SelectedIndexChanged += CBReverceValWr_SelectedIndexChanged;

			NUDIntervalSendFHPDev.Value = Properties.Settings.Default.iIntervalSendFHPDev;
			UDRepeatWrite.Value = Properties.Settings.Default.dmRepeatWrite;

			TCPtoTCP = new CTCPtoTCP (this);
			TCPtoTCP.Parent = TPTCPtoTCP;

			TCPtoComPort = new CTCPtoComPort (this);
			TCPtoComPort.Parent = TPTCPtoComPort;


						// Этот блок всегда в конце !!! 
						// Вносить сюда новые UserControls
						// UserControls yнаследовать от IUserControls
			IUserControls[] ControlsListCInit = { Devices, Params, TestExch, TestsDiff, TCPtoTCP, TCPtoComPort };
			ControlsList = ControlsListCInit;
		}
		//___________________________________________________________________________
		public static string Version
		{
			get
			{
				Assembly asm = Assembly.GetExecutingAssembly ();
				FileVersionInfo fvi = FileVersionInfo.GetVersionInfo (asm.Location);
				return fvi.FileVersion;
			}
		}
		//_________________________________________________________________________
		public void FillDevices ()
		{
			int iRows = Devices.GV.RowCount;
			CBDevicesStat.Items.Clear ();
			CBDeviceWr.Items.Clear ();
			TestsDiff.CBDevsWr.Items.Clear ();
			for (int i = 1; i < iRows; i++)
			{
				string asDev = (string)Devices.GV.Rows[i - 1].Cells[0].Value;
				CBDevicesStat.Items.Add (asDev);
				CBDeviceWr.Items.Add (asDev);
				TestsDiff.CBDevsWr.Items.Add (asDev);
			}
			if (CBDevicesStat.Items.Count > 0)
			{
				CBDevicesStat.SelectedIndex = CBDevicesStat.FindString (Properties.Settings.Default.asDeviceStat);
				if (CBDevicesStat.SelectedIndex == -1)
					CBDevicesStat.SelectedIndex = 0;
				CBDeviceWr.SelectedIndex = CBDeviceWr.FindString (Properties.Settings.Default.asDeviceWr);
				if (CBDeviceWr.SelectedIndex == -1)
					CBDeviceWr.SelectedIndex = 0;
				TestsDiff.CBDevsWr.SelectedIndex = CBDeviceWr.SelectedIndex;
			}
			ArchToFIle = OutArchToFile;
		}
		//_________________________________________________________________________

		delegate void DOutData (string asStr);
		DOutData OutData;

		//_________________________________________________________________________
		public void StatExchan (string asStr)
		{
			LStatExchan.Text = DRV.Statistic (CBDevicesStat.Text).ToString ();
		}
		//_________________________________________________________________________
		public void TrStatExchange_Elapsed (object sender, System.Timers.ElapsedEventArgs e)
		{
			try
			{
				Invoke (OutData, new object[] { "" });
			}
			catch { }
		}
		//_________________________________________________________________________
		public void CreateTemplateIndic ()
		{
			ВсегоПараметров = Params.GV.RowCount - 1;
			//baIsSingle = new bool[ВсегоПараметров];
			for (int i = 1; i <= ВсегоПараметров; i++)
			{
				string sParams = Params.InitStr (i - 1);
				CFindPair FindPair = new CFindPair (sParams);
				if (FindPair[CONST.ПАРАМЕТРЫ.RequestType.ToString ()] == CONST.RequestType.Single.ToString ())
					CreateLabel ((string)Params.GV.Rows[i - 1].Cells[1].Value, PParameters);
				else
					CreateComboBox ((string)Params.GV.Rows[i - 1].Cells[1].Value, PParameters);
			}
		}
		//_________________________________________________________________________
		private void BClose_Click (object sender, EventArgs e)
		{
			Close ();
		}
		//_________________________________________________________________________
		private void FTestDrivers_FormClosing (object sender, FormClosingEventArgs e)
		{
			if (MessageBox.Show ("Кончить тестирование драйвера?", "Завершение работы программы", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				if (DRV != null)
					DRV.Close ();
				TScanParam.Dispose ();
				TScanParam.Stop ();
				TrStatExchange.Dispose ();
				TrStatExchange.Stop ();
				TrWorkTime.Stop ();
				Devices.Save ();
				Params.Save ();
				Properties.Settings.Default.iIntervalScan = TScanParam.Interval;
				Properties.Settings.Default.Save ();
				TestExch.Saving ();
				if (ControlsList != null)
					foreach (var item in ControlsList)
					{
						if (item != null)
							item.Close ();
					}
			}
			else e.Cancel = true;
		}
		//_________________________________________________________________________
		public string CreateFileName ()
		{
			string asConvDT = DateTime.Now.ToString ().Replace (':', '_');
			return /*asDirLog + */@"\" + asConvDT.Replace ('/', '_')/* + asExtLog*/;
		}
		//_________________________________________________________________________

		Dictionary<int, string> NameFileArch = new Dictionary<int, string> ();
		List<Dictionary<int, string>> ArchFiles = new List<Dictionary<int, string>> ();

		public Tdrv DRV = new Tdrv ();
		private void BTest1_Click (object sender, EventArgs e)
		{
			try
			{
				BStopDrv_Click (sender, e);
				Properties.Settings.Default.Save ();
				CBErrors.Items.Clear ();

				DRV.Init (null, Devices.InitStr ());

				asNameFlAsDate = asCurrentDirectory + @"\" + CreateFileName ();
				NameFileArch.Clear ();
				for (int i = 1; i <= ВсегоПараметров; i++)
				{
					string sParams = Params.InitStr (i - 1);
					if (CBReadArch.Checked == false)
					{
						CFindPair FindPair = new CFindPair (sParams);
						if (FindPair[CONST.ПАРАМЕТРЫ.RequestType.ToString ()] == CONST.RequestType.Single.ToString ())
							DRV.Subscribe (i, sParams);
					}
					else
					{
						DRV.Subscribe (i, sParams);
						CFindPair FindPair = new CFindPair (sParams);
						string asRequestType = FindPair[CONST.ПАРАМЕТРЫ.RequestType.ToString ()];
						if (asRequestType != CONST.RequestType.Single.ToString ())
							NameFileArch.Add (i, asNameFlAsDate + asRequestType + ".arch");
					}
					CBErrors.Items.Add (i + "." + sParams);
				}
				Text = sCaption + " РАБОТАЕТ";
				TrStatExchange.Start ();
				if (CBErrors.Items.Count > 0)
					CBErrors.SelectedIndex = 0;
				LPuskTime.Text = "0";
				DTPusk = DateTime.Now;
				//TrWorkTime.Start ();
				BWritePar.Enabled = true;
				//TestsDiff.BStartWr.Enabled = true;
				bReadingValues = false;
				//File.Create (asNameFlAsDate + ".arch");
			}
			catch (Exception exc)
			{
				MessageBox.Show (exc.Message + Environment.NewLine + exc.StackTrace);
			}
		}
		//_________________________________________________________________________

		DateTime DTPusk;
		DateTime DTBeginWork;
		System.Timers.Timer TrWorkTime = new System.Timers.Timer (1900);
		delegate void DWorkTime ();
		DWorkTime DCalcWorkTime;
		void Elapsed_CalcWorkTime (object sender, System.Timers.ElapsedEventArgs e)
		{
			try
			{
				DCalcWorkTime = CalcWorkTime;
				Invoke (DCalcWorkTime);
			}
			catch { }
		}
		//-------------------------------------------------------------------------
		void CalcWorkTime ()
		{
			DateTime DT = DateTime.Now;
			if (TrStatExchange.Enabled)
			{
				LPuskTime.Text = TimeToStr (DT - DTPusk);
			}
			LWorkTime.Text = TimeToStr (DT - DTBeginWork);
		}
		//_________________________________________________________________________
		public string TimeToStr (TimeSpan TS)
		{
			return TS.Hours.ToString () + ":" + TS.Minutes.ToString () + ":" + TS.Seconds.ToString ();
		}
		//_________________________________________________________________________
		private void BStopDrv_Click (object sender, EventArgs e)
		{
			BWritePar.Enabled = false;
			//TestsDiff.BStartWr.Enabled = false;
			TrStatExchange.Stop ();
			DRV.Close ();                             //DestroyLabelsByParam();			
			BSwithLog_Click (sender, e);
			Text = sCaption + " ОСТАНОВЛЕН";
		}
		//_________________________________________________________________________
		private void BGetErr_Click (object sender, EventArgs e)
		{
			CBErrors.Items.Clear ();
			TErrors Err = Global.Errors;//AXONIM.ScanParamOfDevicves.
			int iNumErr = 0;
			if (Err == null)
			{
				CBErrors.Items.Add ("Модуль ошибок не создан.");
			}
			else
			{
				List<Exception> ErrDRV = Err.Get ();
				iNumErr = ErrDRV.Count;
				for (int i = 0; i < iNumErr; i++)
				{
					try
					{
						CBErrors.Items.Add (ErrDRV[i].Message);
					}
					catch (Exception) { }
				}
			}
			LNumErr.Text = iNumErr.ToString ();
			if (CBErrors.Items.Count > 0)
				CBErrors.SelectedIndex = 0;
		}
		//_________________________________________________________________________

		private int iNumParam = 1, ВсегоПараметров = 0;
		const int iYdef = 4; int iX = 10, iY = iYdef, iYlab = iYdef + 5; const int iIncY = 18;
		const int iXData = 180;
		int iHightL_CB = 16;
		public void CreateLabel (string sNameParam, Control Prnt)
		{
			string sNum = iNumParam.ToString ();

			Label L2 = new Label ();
			L2.Parent = Prnt;
			L2.Name = sPrefixCB + sNum;
			L2.Location = new Point (iX, iY);
			L2.BorderStyle = BorderStyle.Fixed3D;
			L2.Size = new Size (Prnt.Width - L2.Location.X - iXData - 20, iHightL_CB);
			L2.AutoSize = false;
			L2.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
			L2.SendToBack ();

			CreateLabelHeader (sNameParam, Prnt, Prnt.Width - L2.Location.X - iXData);

			iY += iIncY; iYlab += iIncY; iNumParam++;
		}
		const string sPrefixCB = "CB_L";
		//.........................................................................
		public void CreateComboBox (string sNameParam, Control Prnt)
		{
			ComboBox CB = new ComboBox ();
			CB.Parent = Prnt;
			CB.Name = sPrefixCB + iNumParam.ToString ();
			CB.DropDownStyle = ComboBoxStyle.DropDownList;
			CB.AutoSize = false;
			//CB.ItemHeight = iIncY;
			CB.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
			CB.Location = new Point (iX, iY);
			//CB.Location = new Point (iX + iXData, iY);
			CB.Size = new Size (Prnt.Width - CB.Location.X - iXData, iHightL_CB - 2);

			CreateLabelHeader (sNameParam, Prnt, CB.Size.Width);
			CB.SendToBack ();
			iY += iIncY; iYlab += iIncY; iNumParam++;
		}
		//_________________________________________________________________________
		void CreateLabelHeader (string sNameParam, Control Prnt, int CBSizeWidth)
		{
			Label L1 = new Label ();
			L1.Parent = Prnt;
			L1.AutoSize = true;
			L1.Location = new Point (iX + CBSizeWidth, iYlab/* + iY / 100*/);
			//L1.Location = new Point (iX, iYlab/* + iY / 100*/);
			L1.Text = sNameParam;
			L1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
			L1.SendToBack ();
		}
		//_________________________________________________________________________
		public void DestroyLabelsByParam ()
		{
			PParameters.Controls.Clear ();
			iNumParam = 1; iX = 10; iY = iYdef; iYlab = iYdef + 5;
			ВсегоПараметров = 0;
		}
		//_________________________________________________________________________
		public string ConvertVal (dynamic btData, TYPE TypeData)
		{
			//btData.Reverse();
			if (btData == null || btData.Length <= 0)
				return "Нет данных";
			switch (TypeData)
			{
			case TYPE.BIN:
			case TYPE.TLP:
			case TYPE.DT: return BitConverter.ToString (btData);
			case TYPE.@float: float f = BitConverter.ToSingle (btData, 0); return f.ToString ();
			case TYPE.@double:
				byte[] bta = new byte[8]; int iSize = btData.Length < 8 ? btData.Length : 8;
				for (int j = 0; j < iSize; j++)
				{
					bta[j] = btData[j];
				}
				double d = BitConverter.ToDouble (bta, 0); return d.ToString ();
			case TYPE.@short: short s = BitConverter.ToInt16 (btData, 0); return s.ToString ();
			case TYPE.@ushort: ushort us = BitConverter.ToUInt16 (btData, 0); return us.ToString ();
			case TYPE.DateTimeAsYMDHMS12: int iDT = BitConverter.ToInt32 (btData, 0); return Global.IntToDateTime (iDT).ToString ();
			case TYPE.@int: int i = BitConverter.ToInt32 (btData, 0); return i.ToString ();
			case TYPE.@uint: uint ui = BitConverter.ToUInt32 (btData, 0); return ui.ToString ();
			case TYPE.@long: long l = BitConverter.ToInt64 (btData, 0); return l.ToString ();
			case TYPE.@ulong: ulong ul = BitConverter.ToUInt64 (btData, 0); return ul.ToString ();
			case TYPE.INT16: Int16 i16 = BitConverter.ToInt16 (btData, 0); return i16.ToString ();
			case TYPE.INT32: Int32 ui32 = BitConverter.ToInt16 (btData, 0); return ui32.ToString ();
			case TYPE.UINT16: UInt16 ui16 = BitConverter.ToUInt16 (btData, 0); return ui16.ToString ();
			case TYPE.UINT32: UInt32 i32 = BitConverter.ToUInt16 (btData, 0); return i32.ToString ();
			case TYPE.INT8:// char c = BitConverter.ToChar(btData, 0); return c.ToString();
			case TYPE.UINT8: /*byte bt = (byte)BitConverter.ToChar(btData, 0);*/ return btData[0].ToString ();
			case TYPE.DateAsYMD:
			case TYPE.DateTime:
			case TYPE.DateTime1970:
			case TYPE.DateTimeAsYMDHMS:
			case TYPE.TimeAsHMS:
			case TYPE.string8:
			case TYPE.DT14:
			case TYPE.@string: return Global.ByteToStr (btData, 0, btData.Length);
			//Encoding Endoding1251 = System.Text.Encoding.GetEncoding (1251);			//Encoding.ASCII;
			//char[] asciiChars = new char[Endoding1251.GetCharCount (btData, 0, btData.Length)];
			//Endoding1251.GetChars (btData, 0, btData.Length, asciiChars, 0); return new string (asciiChars);
			default:
				string asVal = "";
				foreach (var item in btData)
				{
					asVal += item + " ";
				}
				return asVal;
			}
		}
		//_________________________________________________________________________

		volatile bool bReadingValues = false;
		private void TScanParam_Tick (object sender, EventArgs e)
		{
			if (bReadingValues)
				return;
			bReadingValues = true;      //ThreadPool.QueueUserWorkItem (new WaitCallback (OutputValues), this);
			Thread ЧитаемИзДривера = new Thread (TheПоток);
			ЧитаемИзДривера.IsBackground = true;
			ЧитаемИзДривера.Start ();
		}
		//_________________________________________________________________________

		Action DOutputValues;
		private void TheПоток ()
		{
			DOutputValues = OutputValues;
			Invoke (DOutputValues);
		}
		//_________________________________________________________________________
		void OutToCB (object Control, DataMessage DM, int i)
		{
			ComboBox CB = (ComboBox)Control;
			CB.Items.Clear ();
			if (DM != null)
			{
				string asData = string.Format ("{0:D2}. {1} {2}; {3}", DM.id, DM.time.TimeOfDay,
											DM.quality, ConvertVal (DM.value, DRV.GetTypeVal (i)));
				CB.Items.Add (asData);
				CB.SelectedIndex = 0;
			}
		}
		//_________________________________________________________________________
		void OutToLabel (object Control, DataMessage DM, int i)
		{
			Label L = (Label)Control;
			if (DM != null)
			{
				int iLenToDots = L.Text.IndexOf (';') + 2;
				if (iLenToDots >= L.Text.Length)
					iLenToDots = L.Text.Length;
				string asOld = L.Text.Substring (iLenToDots, L.Text.Length - iLenToDots);
				string asNew = ConvertVal (DM.value, DRV.GetTypeVal (i)).TrimEnd ('\0');

				if (asOld == asNew)
					L.ForeColor = Color.Black;
				else L.ForeColor = Color.Brown;

				L.Text = string.Format ("{0:D2}. {1} {2}; {3}", DM.id, DM.time.TimeOfDay,
																DM.quality, asNew);
			}
		}
		//_________________________________________________________________________
		void OutArchToFile (string asData, int iPosArch)
		{
			File.AppendAllText (NameFileArch[iPosArch], asData + Environment.NewLine);
		}
		//_________________________________________________________________________

		delegate void DOutArchToFile (string asData, int iPosArch);
		DOutArchToFile ArchToFIle;
		private void OutputValues ()
		{
			int i = 1;
			try
			{
				RBScan.Checked = !RBScan.Checked;
				Thread.Sleep (0);
				for (; i <= ВсегоПараметров; i++)
				{
					Control[] C = PParameters.Controls.Find (sPrefixCB + i.ToString (), false);
					if (C != null && C.Length > 0)
					{
						//if (i == 2) Global.LogWriteLine ("");
						if (C[0] is Label)      //if (DRV.GetRequestType (i) == CONST.RequestType.Single)
						{
							DataMessage DM = DRV.GetValue (i);    // List (i); //DM.value = new byte[] { 0x3b, 0xdf, 0x47, 0x40 };
							if (CBShowInd.Checked)
							{
								OutToLabel (C[0], DM, i);
							}
						}
						else
						{
							if (!CBIndArchOff.Checked)
							{
								ComboBox CB = (ComboBox)C[0];
								DeviceArchiveRecord[] Archs = DRV.GetNewArchiveData (i);
								if (Archs != null)
								{
									if (CBAccumArch.Checked == false)
										CB.Items.Clear ();
									bool bOutHeaders = true;
									string asHead = "";
									foreach (DeviceArchiveRecord Row in Archs)
									{
										Thread.Sleep (0);
										string asRow = "";
										if (Row.data != null)
										{
											foreach (DeviceArchiveParameter Par in Row.data)
											{
												if (bOutHeaders)
												{
													asHead += Par.name + " ";       // Заголовки 
												}
												asRow += Par.value + " ";         // Значения архива построчно 
											}
										}
										if (bOutHeaders)
										{
											bOutHeaders = false;
											CB.Items.Add (asHead);
										}
										CB.Items.Add (asRow);
										if (ChBArcToFile.Checked)
											Invoke (ArchToFIle, new object[] { asRow, i });
									}
									if (CB.Items.Count > 0)
										CB.SelectedIndex = 0;
								}
								else if (CB.Items.Count < 2) //CB.Items.Add ("Нет данных архива за период.");
								{
									CB.Items.Add ("Нет новых данных архива.");  //CB.Text = "Нет данных архива за период.";
									CB.SelectedIndex = CB.Items.Count - 1;
								}
							}
						}
					}
				}
			}
			catch (Exception exc)
			{
				MessageBox.Show ("OutputValues: " + i.ToString () + " " + exc.Message + Environment.NewLine + exc.StackTrace);  //   Global.LogWriteLine ("OutputValues: " + i.ToString() + " " + exc.Message);
			}
			bReadingValues = false;
		}
		//_________________________________________________________________________
		private void BStartScan_Click (object sender, EventArgs e)
		{
			if (TScanParam.Enabled == true)
			{
				TScanParam.Stop ();
				BStartScan.Text = "Пуск";
			}
			else
			{
				if (ВсегоПараметров > 0)
				{
					TScanParam.Interval = (int)UDIntervalScan.Value;
					TScanParam.Enabled = true;
					BStartScan.Text = "Стоп";
					Properties.Settings.Default.Save ();
				}
			}
		}
		//_________________________________________________________________________
		private void NIntervalScan_ValueChanged (object sender, EventArgs e)
		{
			TScanParam.Interval = (int)UDIntervalScan.Value;
			Properties.Settings.Default.iIntervalScan = (int)UDIntervalScan.Value;
		}
		//_________________________________________________________________________
		private void BClrErr_Click (object sender, EventArgs e)
		{
			//DRV.Init (null);
			TErrors Err = Global.Errors;
			Err.Clear ();
			LNumErr.Text = "0";
		}
		//_________________________________________________________________________
		private void BSaveDevices_Click (object sender, EventArgs e)
		{
			Devices.Save ();
		}
		//_________________________________________________________________________
		private void FTestDrivers_Resize (object sender, EventArgs e)
		{
			if (ControlsList != null)
				foreach (var item in ControlsList)
				{
					if (item != null)
						item.ReSize ();
				}
			Properties.Settings.Default.iHeightForm = Height;
			Properties.Settings.Default.iWidthForm = Width;
			Properties.Settings.Default.iTopForm = Top;
		}
		//_________________________________________________________________________
		private void BSaveParams_Click (object sender, EventArgs e)
		{
			Params.Save ();
		}
		//_________________________________________________________________________
		bool bLogON = false;
		private void BSwithLog_Click (object sender, EventArgs e)
		{
			if (!bLogON)
			{
				bLogON = true;
				Global.SetSwitchLog ((Global.STATE_WRITE_MESS)CBStateLog.SelectedIndex);
				BSwithLog.Text = "Отключить Log";
			}
			else
			{
				Global.CloseLog ();
				bLogON = false;
				BSwithLog.Text = "Включить Log";
			}
		}
		//_________________________________________________________________________
		// { 0xcd, 0xcc, 0xf6, 0x42 };	//, 0x11,0, 1, 0, 2, 0, 10, 0, 15, 0, 1, 0 };// { 0x0, 0x0, 0, 0x1};	// {  0x30, 0x31, 0x2f, 0x30, 0x31, 0x2f, 0x31, 0x39, 0x37, 0x30, 0x00, 0x30, 0x3a, 0x30, 0x30, 0x3a, 0x30, 0x30, 0x00 };//{,  0x00, 0x00, 0xe4, 0x43 }; //0x00, 0x80, 0xa0, 0x43  		
		private void BWritePar_Click (object sender, EventArgs e)
		{
			CBDataSave ();
			CBDataWriteParSave ();
			CBParameterNameSave ();
			Properties.Settings.Default.Save ();

			int iID = int.Parse (TBIDwritePar.Text);
			string CBParameterName_Text;

			if (UDRepeatWrite.Value == 1)
			{
				CBParameterName_Text = CBParameterName.Text;
				WriteToDev (iID, CBData.Text, CBParameterName_Text, CBDeviceWr.Text);
			}
			else
			{
				for (int i = 0; i < UDRepeatWrite.Value; i++)
				{
					iID = WriteParams (iID, CBDeviceWr.Text + i, (i + 1).ToString ());
					Thread.Sleep ((int)NUDIntervalSendFHPDev.Value);
				}
			}
		}
		//_________________________________________________________________________
		/// <summary>
		/// Создание блока ФХП и отправка драйверу
		/// </summary>
		/// <param name="iID">ID параметра</param>
		/// <param name="asDevice">Наименование объекта, на котором прибор</param>
		/// <param name="asDataByWr">Значение параметра, в соответствии с типом (напр 1,23 или 5)</param>
		/// <returns>ID параметра, отправленного последним</returns>
		private int WriteParams (int iID, string asDevice = "", string asDataByWr = "")
		{
			string[] asaCBData = { "WrFHP 480", "WrFHP 481", "WrFHP 482" };
			string[] asaCBParameterName = { "WrCO2", "WrN2", "WrRc" };
			int i = 0;
			foreach (var Data in asaCBData)
			{
				WriteToDev (iID++, Data, asaCBParameterName[i], asDevice, asDataByWr);
				i++;
			}

			return iID;
		}
		//_________________________________________________________________________
		enum EReverce { NO, YES, TWO_BITE }
		byte[] StrToTypeThenBytes (string asVal, string asTypeVal, EReverce Reverce)
		{
			try
			{
				dynamic oVal;
				TYPE TheType = (TYPE)Enum.Parse (typeof (TYPE), CBTypeData.Text);
				if (TheType != TYPE.DateTimeAsYMDHMS && TheType != TYPE.DateTimeAsHMSMDY12 && TheType != TYPE.DateTime1970)
				{
					oVal = Global.StrToType (asVal, TheType);
					if (oVal.GetType () is string)// .ToString ().Equals ("System.String"))// TODO RESTORE?
						return new byte[1];
				}
				else
				{
					return new byte[4];
				}
				if (Reverce == EReverce.YES)
				{
					byte[] btaRev = BitConverter.GetBytes (oVal);
					return btaRev.Reverse ().ToArray ();
				}

				if (Reverce == EReverce.TWO_BITE)
				{
					int iTypeSize = CONST.SizeTypeData (TheType);
					byte[] btRet = new byte[iTypeSize];
					Global.AppendTwoBytesRev (BitConverter.GetBytes (oVal), 0, btRet, 0, iTypeSize);
					return btRet;
				}
				return BitConverter.GetBytes (oVal);
			}
			catch (Exception exc)
			{
				MessageBox.Show (string.Format ("Значение параметра [{0}] не соответствует типу [{1}].{2}",
													asVal, asTypeVal, exc.StackTrace));
				return null;
			}
		}
		//_________________________________________________________________________
		void WriteToDev (int iID, string CBData_Text, string CBParameterName_Text, string asDevice = "", string asDataByWr = "")
		{
			asDevice = (asDevice == "") ? CBDeviceWr.Text + "1" : asDevice;
			string asParams = $"[\"{CONST.ПАРАМЕТРЫ.DeviceName.ToString ()}\":\"{asDevice}\"," +
												$"\"{CONST.ПАРАМЕТРЫ.Data.ToString ()}\":\"{CBData_Text}\"," +
												$"\"{CONST.ПАРАМЕТРЫ.ParameterType.ToString ()}\":\"{CBTypeData.Text}\"," +
												$"\"{CONST.ПАРАМЕТРЫ.ParameterName.ToString ()}\":\"{CBParameterName_Text}\"," +
												$"\"{CONST.ПАРАМЕТРЫ.RequestName.ToString ()}\":\"MT16\"," +
												$"\"{CONST.ПАРАМЕТРЫ.INorOUT.ToString ()}\":\"out\"," +
												$"\"{CONST.ПАРАМЕТРЫ.RequestType.ToString ()}\":\"{CONST.RequestType.Single.ToString ()}\"," +
												$"\"{CONST.ПАРАМЕТРЫ.TimeRequest.ToString ()}\":\"{NTimeResp.Text}\"" +
												"]";
			asDataByWr = (asDataByWr == "") ? CBDataWritePar.Text : asDataByWr;
			byte[] btaDataWritePar = StrToTypeThenBytes (asDataByWr, CBTypeData.Text, (EReverce)CBReverceValWr.SelectedIndex);
			if (btaDataWritePar != null)
			{
				DRV.WriteValue (iID, asParams, btaDataWritePar);
			}
		}
		//_________________________________________________________________________
		void CBParameterNameSave ()
		{
			if (bDataWritePar_TextChanged)
			{
				bDataWritePar_TextChanged = false;
				if (Properties.Settings.Default.asParameterName.Contains (CBParameterName.Text/* + ";"*/) == false)
				{
					Properties.Settings.Default.asParameterName += ";" + CBParameterName.Text;
					CBParameterName.Items.Add (CBParameterName.Text);
					Properties.Settings.Default.iParameterName = CBParameterName.Items.Count - 1;
				}
			}
		}
		//_________________________________________________________________________
		void CBDataSave ()
		{
			if (bCBData_TextChanged)
			{
				bCBData_TextChanged = false;
				if (Properties.Settings.Default.asData.Contains (CBData.Text) == false)
				{
					Properties.Settings.Default.asData += ";" + CBData.Text;
					CBData.Items.Add (CBData.Text);
					int iPosNewVal = CBData.FindString (CBData.Text);
					if (iPosNewVal == -1)
						Properties.Settings.Default.iData = CBData.Items.Count - 1;
					else Properties.Settings.Default.iData = iPosNewVal;
				}
			}
		}
		//_________________________________________________________________________
		void CBDataWriteParSave ()
		{
			if (bDataWritePar_TextChanged)
			{
				//bDataWritePar_TextChanged = false;
				if (Properties.Settings.Default.asDataWritePar.Contains (CBDataWritePar.Text) == false)
				{
					Properties.Settings.Default.asDataWritePar += ";" + CBDataWritePar.Text;
					CBDataWritePar.Items.Add (CBDataWritePar.Text);
					int iPosNewVal = CBData.FindString (CBDataWritePar.Text);
					if (iPosNewVal == -1)
						Properties.Settings.Default.iDataWritePar = CBDataWritePar.Items.Count - 1;
					else Properties.Settings.Default.iDataWritePar = iPosNewVal;
				}
			}
		}
		//_________________________________________________________________________
		void CBSave (ref bool bChanged, ref string asPropert, ref ComboBox CB, ref int iPos)
		{
			if (bChanged)
			{
				bChanged = false;
				if (asPropert.Contains (CB.Text + ";") == false)
				{
					asPropert += ";" + CB.Text;
					CB.Items.Add (CB.Text);
					int iPosNewVal = CB.FindString (CB.Text);
					if (iPosNewVal == -1)
						iPos = CBData.Items.Count - 1;
					else iPos = iPosNewVal;
				}
			}
		}
		//_________________________________________________________________________
		private void FTestDrivers_Move (object sender, EventArgs e)
		{
			Properties.Settings.Default.iLeftForm = Left;
			Properties.Settings.Default.iTopForm = Top;
		}
		//_________________________________________________________________________
		private void CBDataWritePar_SelectedIndexChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.iDataWritePar = CBDataWritePar.SelectedIndex;
		}
		//_________________________________________________________________________
		private void CBParameterName_SelectedIndexChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.iParameterName = CBParameterName.SelectedIndex;
		}
		//_________________________________________________________________________
		bool bDataWritePar_TextChanged = false;
		private void CBDataWritePar_TextChanged (object sender, EventArgs e)
		{
			bDataWritePar_TextChanged = true;
		}
		//_________________________________________________________________________
		private void CBData_SelectedIndexChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.iData = CBData.SelectedIndex;
		}
		//_________________________________________________________________________
		bool bCBData_TextChanged = false;
		private void CBData_TextChanged (object sender, EventArgs e)
		{
			bCBData_TextChanged = true;
		}
		//_________________________________________________________________________
		private void CBDevices_SelectedIndexChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.asDeviceStat = CBDevicesStat.Text;
		}
		//_________________________________________________________________________
		private void CBDevicesWr_SelectedIndexChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.asDeviceWr = CBDeviceWr.Text;
		}
		//_________________________________________________________________________
		private void BSetFirstDateArchs_Click (object sender, EventArgs e)
		{
			SetNewDTreadArch ();
		}
		//_________________________________________________________________________
		public void SetNewDTreadArch ()
		{
			Devices.SetNewDTreadArch (TPArchDay, TPArchHour, TPInterfer, TPAlarm);
			Properties.Settings.Default.DTArchDay = TPArchDay.Value;
			Properties.Settings.Default.DTArchHour = TPArchHour.Value;
			Properties.Settings.Default.DTInterfer = TPInterfer.Value;
			Properties.Settings.Default.DTAlarm = TPAlarm.Value;
			Properties.Settings.Default.Save ();
			BSetFirstDateArchs.BackColor = Color.CadetBlue; //LVersion.BackColor;
		}
		//_________________________________________________________________________

		//public TCPserver.FTCPserver TCPserver;
		private string asNameFlAsDate;
		private string asCurrentDirectory;

		private void BTCPserver_Click (object sender, EventArgs e)
		{
			TCPserver.FTCPserver TCPserver;
			TCPserver = new TCPserver.FTCPserver (this);
			//if (TCPserver == null)
			//	TCPserver = new TCPserver.FTCPserver (this);
			//else return;
			TCPserver.Show (this);
		}
		//_________________________________________________________________________
		private void TPArchDay_ValueChanged (object sender, EventArgs e)
		{
			BSetFirstDateArchs.BackColor = Color.Tomato;
		}
		//_________________________________________________________________________
		private void CBParameterName_TextChanged (object sender, EventArgs e)
		{
			bDataWritePar_TextChanged = true;
		}
		//_________________________________________________________________________
		private void NTimeResp_ValueChanged_1 (object sender, EventArgs e)
		{
			Properties.Settings.Default.dmTimeResp = NTimeResp.Value;
		}
		//_________________________________________________________________________
		private void CBReverceValWr_SelectedIndexChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.iReverceValWr = CBReverceValWr.SelectedIndex;
		}
		//_________________________________________________________________________
		private void BCOMserver_Click (object sender, EventArgs e)
		{
			COMserver.FCOMserver COMServer = new COMserver.FCOMserver (this);
			//if (COMServer == null)
			//	COMServer = new COMserver.FCOMserver (this);
			//else return;
			COMServer.Show (this);
		}
		//_________________________________________________________________________
		private void IntervalSendFHPDev_ValueChanged_1 (object sender, EventArgs e)
		{
			Properties.Settings.Default.iIntervalSendFHPDev = (int)NUDIntervalSendFHPDev.Value;
		}
		//_________________________________________________________________________
		private void BStopStartDev_Click (object sender, EventArgs e)
		{
			string s = "";
			string asInitDev = Devices.GetStrInitDevice (s, CBDeviceWr.SelectedIndex);
			CFindPair FindPair = new CFindPair (asInitDev);
			asInitDev = FindPair[CONST.DEV_SETUPS.Name.ToString ()]/* + "," + FindPair[CONST.DEV_SETUPS.UrlSingleParam.ToString ()]*/;
			DRV.StopStartDev (asInitDev, ChStopStartDev.Checked);
		}
		//_________________________________________________________________________

		public CExtractZip Zip;

		private void button1_Click (object sender, EventArgs e)
		{
			if (Zip == null)
				Zip = new CExtractZip (this);
			else Zip.Focus ();
			Zip.Show ();
		}
		//.........................................................................
		private void PAddingPanel_MouseEnter (object sender, EventArgs e)
		{
			PAddingPanel.Width = 400;
			PAddingPanel.Height = 260;
		}
		//.........................................................................
		private void PAddingPanel_MouseLeave (object sender, EventArgs e)
		{
			PAddingPanel.Width = 244;
			PAddingPanel.Height = 23;
		}

		//_________________________________________________________________________
		private void CBErrors_MouseEnter (object sender, EventArgs e)
		{
			toolTip1.SetToolTip (CBErrors, CBErrors.Text);
		}
		//_________________________________________________________________________
		private void CBTypeData_SelectedIndexChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.iTypeData = CBTypeData.SelectedIndex;
		}
		//_________________________________________________________________________
		private void NTimeResp_ValueChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.dmRepeatWrite = UDRepeatWrite.Value;
		}
		//_________________________________________________________________________
	}
}