/*@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

~~~~~~~~~	Проект:			Тест библиотеки опроса приборов (driver) BeltransgasDrv  _
~~~~~~~~~	Прибор:			Все приборы                           									 _
~~~~~~~~~	Модуль:			Головной                                       					 _
~~~~~~~~~	Разработка:	Демешкевич С.А.                                    		 	 _
~~~~~~~~~	Дата:				25.04.2015                                         		 	 _

@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using AXONIM.BelTransGasDRV;
using System.IO;
using DeviceInterfaces;
using System.Runtime.InteropServices;
using AXONIM.ScanParamOfDevicves;
using AXONIM.CONSTS;

namespace TestDRVtransGas
{
  /// <summary>
  /// Типы данных приборов (всевозможных) 
  /// </summary>
  public enum TYPE_DATA
  {
    @float,
    @decimal,
    @double,
    @int,
    @uint,
    @short,
    @ushort,
    @long,
    @ulong,
    @bool,
    @char,
    @byte,
    @string,
    /// <summary>
    /// Дата / время (sec  min  hr  day  mo  yr  lyr  dwk)
    /// </summary>
    DT,
    /// <summary>
    /// Дата
    /// </summary>
    Date,
    /// <summary>
    /// Время
    /// </summary>
    Time,
    /// <summary>
    /// Секунды от 1.01.1970 г. (целое беззнаковое 4 байта)
    /// </summary>
    DateTime1970,
    DateAsYMD,
    TimeAsHMS,
    DateTimeAsYMDHMS,
    DateTimeAsYMDHMS12,
    DateTime,
    hex,
    TAlarmFB,
    TArchVympel,
    ERROR
  };
  public partial class FTestDrivers : Form
	{
    TDevices Devices = null;
		TParams Params = null;
		readonly string sCaption;
		System.Timers.Timer TrStatExchange = new System.Timers.Timer (420);

    public FTestDrivers()
		{
			InitializeComponent();

      DTBeginWork = DateTime.Now;
      TrWorkTime.Elapsed += Elapsed_CalcWorkTime;
      TrWorkTime.Start ();

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
			Text += " [Драйвер V" + DRV.GetVersion () + "]";
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

      BTest1.Select();

			CreateTemplateIndic ();
			CBTypeData.Items.AddRange (Enum.GetNames (typeof(TYPE_DATA)));
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

			CBData.SelectedIndexChanged -= CBData_SelectedIndexChanged;
			CBData.Items.AddRange (Properties.Settings.Default.asData.Split (';'));
      CBData.SelectedIndex = Properties.Settings.Default.iData;
			CBData.SelectedIndexChanged += CBData_SelectedIndexChanged;

			CBParameterName.SelectedIndexChanged -= CBParameterName_SelectedIndexChanged;
			CBParameterName.Items.AddRange (Properties.Settings.Default.asParameterName.Split (';'));
			CBParameterName.SelectedIndex = Properties.Settings.Default.iParameterName;
			CBParameterName.SelectedIndexChanged += CBParameterName_SelectedIndexChanged;

			NTimeResp.Value = Properties.Settings.Default.dmTimeResp;

			TrStatExchange.Elapsed += TrStatExchange_Elapsed;
			FillDevices ();
			OutData = StatExchan;
		}
    //_________________________________________________________________________
    public void FillDevices ()
		{
      int iRows = Devices.GV.RowCount;
      CBDevicesStat.Items.Clear ();
      CBDeviceWr.Items.Clear ();
			CBDevsWr.Items.Clear ();
      for (int i = 1; i < iRows; i++)
			{
				string asDev = (string)Devices.GV.Rows[i - 1].Cells[0].Value;
				CBDevicesStat.Items.Add (asDev);
				CBDeviceWr.Items.Add (asDev);
				CBDevsWr.Items.Add (asDev);
			}
			if (CBDevicesStat.Items.Count > 0)
			{
				CBDevicesStat.SelectedIndex = CBDevicesStat.FindString (Properties.Settings.Default.asDeviceStat);
				if (CBDevicesStat.SelectedIndex == -1)
					CBDevicesStat.SelectedIndex = 0;
				CBDeviceWr.SelectedIndex = CBDeviceWr.FindString (Properties.Settings.Default.asDeviceWr);
				if (CBDeviceWr.SelectedIndex == -1)
					CBDeviceWr.SelectedIndex = 0;
				CBDevsWr.SelectedIndex = CBDeviceWr.SelectedIndex;
      }
		}
		//_________________________________________________________________________
		delegate void DOutData (string asStr);
		DOutData OutData;

		//_________________________________________________________________________
		public void StatExchan (string asStr)
		{
			LStatExchan.Text = DRV.Statistic (CBDevicesStat.Text).ToString();
		}
		//_________________________________________________________________________
		public void TrStatExchange_Elapsed (object sender, System.Timers.ElapsedEventArgs e)
		{
			try
			{
				Invoke (OutData, new object[] { "" });
			}
			catch	{}
		}
		//_________________________________________________________________________
		public void CreateTemplateIndic()
		{
			ВсегоПараметров = Params.GV.RowCount - 1;
      //baIsSingle = new bool[ВсегоПараметров];
			for (int i = 1; i <= ВсегоПараметров; i++)
			{
        string sParams = Params.InitStr (i - 1);
        TFindPair FindPair = new TFindPair (sParams);
        if (FindPair.Value (CONST.ПАРАМЕТРЫ.RequestType.ToString ()) == CONST.RequestType.Single.ToString ())
          CreateLabel ((string)Params.GV.Rows[i - 1].Cells[1].Value, PParameters);
        else
          CreateComboBox ((string)Params.GV.Rows[i - 1].Cells[1].Value, PParameters);		
			}
		}
		//_________________________________________________________________________
		private void BClose_Click(object sender, EventArgs e)
		{
			Close ();
		}
		//_________________________________________________________________________
		private void FTestDrivers_FormClosing(object sender, FormClosingEventArgs e)
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
				Properties.Settings.Default.Save ();
			}
			else e.Cancel = true;
    }
    //_________________________________________________________________________

		Tdrv DRV = new Tdrv ();
		private void BTest1_Click(object sender, EventArgs e)
		{
			try
			{
        BStopDrv_Click (sender, e);
        Properties.Settings.Default.Save ();
				CBErrors.Items.Clear ();

				DRV.Init (null, Devices.InitStr ());

				for (int i = 1; i <= ВсегоПараметров; i++)
				{
					string sParams = Params.InitStr (i - 1);
					if (CBReadArch.Checked == false)
					{
						TFindPair FindPair = new TFindPair(sParams);
						if (FindPair.Value (CONST.ПАРАМЕТРЫ.RequestType.ToString ()) == CONST.RequestType.Single.ToString ())
							DRV.Subscribe (i, sParams);
					}
					else DRV.Subscribe (i, sParams);
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
				BStartWr.Enabled = true;
				bReadingValues = false;
      }
			catch (Exception exc)
			{
				MessageBox.Show (exc.Message);
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
			catch 	{	}
    }
    //-------------------------------------------------------------------------
    void CalcWorkTime()
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
			BStartWr.Enabled = false;
      TrStatExchange.Stop ();
			DRV.Close();															//DestroyLabelsByParam();			
			BSwithLog_Click (sender, e);
			Text = sCaption + " ОСТАНОВЛЕН";
		}
		//_________________________________________________________________________
		private void BGetErr_Click(object sender, EventArgs e)
		{
			CBErrors.Items.Clear();
			TErrors Err = Global.Errors;//AXONIM.ScanParamOfDevicves.
			int iNumErr = 0;
			if (Err == null)
			{
				CBErrors.Items.Add ("Модуль ошибок не создан.");
			}
			else
			{
				List<Exception> ErrDRV = Err.Get();
				iNumErr = ErrDRV.Count;
				for (int i = 0; i < iNumErr; i++)
				{
					try
					{
						CBErrors.Items.Add(ErrDRV[i].Message);
					}
					catch (Exception)	{	}
				}
			}
			LNumErr.Text = iNumErr.ToString();
			if (CBErrors.Items.Count > 0)
				CBErrors.SelectedIndex = 0;
		}
		//_________________________________________________________________________

		private int iNumParam = 1, ВсегоПараметров = 0;
		const int iYdef = 4; int iX = 10, iY = iYdef, iYlab = iYdef + 5; const int iIncY = 18;
		const int iXData = 180;
		public void CreateLabel(string sNameParam, Control Prnt)
		{
      string sNum = iNumParam.ToString ();

      Label L2 = new Label();
			L2.Parent = Prnt;
			L2.Name = sPrefixCB + sNum;
			L2.Location = new Point (iX, iY);
      L2.BorderStyle = BorderStyle.Fixed3D;
      L2.Size = new Size (Prnt.Width - L2.Location.X - iXData - 20, 14);
			L2.AutoSize = false;
			L2.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;

			CreateLabelHeader (sNameParam, Prnt, Prnt.Width - L2.Location.X - iXData);

			iY += iIncY; iYlab += iIncY; iNumParam++;
		}
		const string sPrefixCB = "CB_L";
		//.........................................................................
		public void CreateComboBox (string sNameParam, Control Prnt)
		{
			ComboBox CB = new ComboBox();
			CB.Parent = Prnt;
			CB.Name = sPrefixCB + iNumParam.ToString ();
			CB.DropDownStyle = ComboBoxStyle.DropDownList;
			CB.AutoSize = false;
			//CB.ItemHeight = iIncY;
			CB.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
			CB.Location = new Point (iX, iY);
			//CB.Location = new Point (iX + iXData, iY);
			CB.Size = new Size(Prnt.Width - CB.Location.X - iXData, 14);

      CreateLabelHeader (sNameParam, Prnt, CB.Size.Width);

			iY += iIncY; iYlab += iIncY; iNumParam++;
		}
		//_________________________________________________________________________
    void CreateLabelHeader(string sNameParam, Control Prnt, int CBSizeWidth)
    {
      Label L1 = new Label();
      L1.Parent = Prnt;
      L1.AutoSize = true;
      L1.Location = new Point (iX + CBSizeWidth, iYlab/* + iY / 100*/);
      //L1.Location = new Point (iX, iYlab/* + iY / 100*/);
      L1.Text = sNameParam;
      L1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
    }
		//_________________________________________________________________________
		public void DestroyLabelsByParam()
		{
			PParameters.Controls.Clear();
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
			case TYPE.DT:				return BitConverter.ToString(btData);
			case TYPE.@float:		float f = BitConverter.ToSingle(btData, 0); return f.ToString();
			case TYPE.@double:	byte[] bta = new byte[8]; int iSize = btData.Length < 8 ? btData.Length : 8; 
													for (int j = 0; j < iSize; j++)
													{ 
														bta[j] = btData[j];
													}
													double d = BitConverter.ToDouble(bta, 0); return d.ToString();
			case TYPE.@short:		short s = BitConverter.ToInt16(btData, 0); return s.ToString();
			case TYPE.@ushort:	ushort us = BitConverter.ToUInt16(btData, 0); return us.ToString();
			case TYPE.DateTimeAsYMDHMS12: int iDT = BitConverter.ToInt32(btData, 0); return Global.IntToDateTime (iDT).ToString(); 
			case TYPE.@int:			int i = BitConverter.ToInt32(btData, 0); return i.ToString();
			case TYPE.@uint:		uint ui = BitConverter.ToUInt32(btData, 0); return ui.ToString();
			case TYPE.@long:		long l = BitConverter.ToInt64(btData, 0); return l.ToString();
			case TYPE.@ulong:		ulong ul = BitConverter.ToUInt64(btData, 0); return ul.ToString();
			case TYPE.INT16:		Int16 i16 = BitConverter.ToInt16(btData, 0); return i16.ToString();
			case TYPE.INT32:		Int32 ui32 = BitConverter.ToInt16(btData, 0); return ui32.ToString();
			case TYPE.UINT16:		UInt16 ui16 = BitConverter.ToUInt16(btData, 0); return ui16.ToString();
			case TYPE.UINT32:		UInt32 i32 = BitConverter.ToUInt16(btData, 0); return i32.ToString();
			case TYPE.INT8:// char c = BitConverter.ToChar(btData, 0); return c.ToString();
			case TYPE.UINT8: /*byte bt = (byte)BitConverter.ToChar(btData, 0);*/ return btData[0].ToString();
			case TYPE.DateAsYMD:
			case TYPE.DateTime:
			case TYPE.DateTime1970:
			case TYPE.DateTimeAsYMDHMS:
			case TYPE.TimeAsHMS:
			case TYPE.@string:	return Global.ByteToStr (btData, 0, btData.Length);
			//Encoding Endoding1251 = System.Text.Encoding.GetEncoding (1251);			//Encoding.ASCII;
				//char[] asciiChars = new char[Endoding1251.GetCharCount (btData, 0, btData.Length)];
				//Endoding1251.GetChars (btData, 0, btData.Length, asciiChars, 0); return new string (asciiChars);
			default: 					string asVal = ""; 
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
      bReadingValues = true;			//ThreadPool.QueueUserWorkItem (new WaitCallback (OutputValues), this);
			Thread ЧитаемИзДривера = new Thread (TheПоток);
			ЧитаемИзДривера.IsBackground = true;
			ЧитаемИзДривера.Start ();
		}
		//_________________________________________________________________________
		delegate void DOutputValues ();
		DOutputValues StartOutputValues;
    private void TheПоток ()
		{
			StartOutputValues = OutputValues;
			Invoke (StartOutputValues);
		}
    //_________________________________________________________________________

    delegate void DOutTo (object Control, DataMessage DM, int i);
    DOutTo OutTo;

    //_________________________________________________________________________
    void OutToCB (object Control, DataMessage DM, int i)
    {
      ComboBox CB = (ComboBox)Control;
      CB.Items.Clear ();
      if (DM != null)
      {
        CB.Items.Add (string.Format ("{0:D2}. [{1}] {2}; {3}", DM.id, DM.time.TimeOfDay.ToString (),
                      DM.quality, ConvertVal (DM.value, DRV.GetTypeVal (i))));
        CB.SelectedIndex = 0;
      }
    }
    //_________________________________________________________________________
    void OutToLabel (object Control, DataMessage DM, int i)
    {
      Label L = (Label)Control;
      if (DM != null)
      {
        L.Text = string.Format ("{0:D2}. [{1}] {2}; {3}", DM.id, DM.time.TimeOfDay.ToString (),
                      DM.quality, ConvertVal (DM.value, DRV.GetTypeVal (i)));
      }
    }
		//_________________________________________________________________________
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
        MessageBox.Show ("OutputValues: " + i.ToString() + " " + exc.Message);  //   Global.LogWriteLine ("OutputValues: " + i.ToString() + " " + exc.Message);
      }
      bReadingValues = false;
		}
		//_________________________________________________________________________
		private void BStartScan_Click(object sender, EventArgs e)
		{
			if (TScanParam.Enabled == true)
			{
				TScanParam.Stop();
				BStartScan.Text = "Пуск";
			}
			else
			{
				if (ВсегоПараметров > 0)
				{
					TScanParam.Interval = (int)NIntervalScan.Value;
					TScanParam.Enabled = true;
					BStartScan.Text = "Стоп";
				}
			}
		}
		//_________________________________________________________________________
		private void NIntervalScan_ValueChanged(object sender, EventArgs e)
		{
			TScanParam.Interval = (int)NIntervalScan.Value;
		}
		//_________________________________________________________________________
    private void BClrErr_Click(object sender, EventArgs e)
    {
			//DRV.Init (null);
			TErrors Err = Global.Errors;
			Err.Clear ();
			LNumErr.Text = "0";
		}
		//_________________________________________________________________________
		private void BSaveDevices_Click(object sender, EventArgs e)
		{
			Devices.Save();
		}
		//_________________________________________________________________________
		private void FTestDrivers_Resize(object sender, EventArgs e)
		{
			if (Devices != null)
				Devices.ReSize();
			if (Params != null)
				Params.ReSize();
      Properties.Settings.Default.iHeightForm = Height;
      Properties.Settings.Default.iWidthForm = Width;
      Properties.Settings.Default.iTopForm = Top;
    }
		//_________________________________________________________________________
		private void BSaveParams_Click(object sender, EventArgs e)
		{
			Params.Save();
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
			string sParams = "[\"" + CONST.ПАРАМЕТРЫ.DeviceName.ToString () + "\":\"" + CBDeviceWr.Text + "\"," +
															 CONST.ПАРАМЕТРЫ.Data.ToString () + "\":\"" + CBData.Text + "\"," +
															 CONST.ПАРАМЕТРЫ.ParameterType.ToString () + "\":\"" + CBTypeData.Text + "\"," +
															 CONST.ПАРАМЕТРЫ.ParameterName.ToString () + "\":\"" + CBParameterName.Text + "\"," +
															 CONST.ПАРАМЕТРЫ.RequestName.ToString() + "\":\"MT16\"," + 
                               CONST.ПАРАМЕТРЫ.INorOUT.ToString () + "\":\"out" + "\"," + 
                               CONST.ПАРАМЕТРЫ.RequestType.ToString () + "\":\"" + CONST.RequestType.Single.ToString() + "\"," + 
															 CONST.ПАРАМЕТРЫ.TimeRequest.ToString() + "\":\"" + NTimeResp.Text + 
				"\"]"; 
			string[] asaDataWritePar = CBDataWritePar.Text.Split (' ');
			int iSizeData = CONST.SizeTypeData((TYPE)Enum.Parse(typeof(TYPE), CBTypeData.Text));
			if (iSizeData != asaDataWritePar.Length)
			{
				MessageBox.Show (string.Format ("Длина данных [{0}] не равна размеру типа [{1}]", iSizeData, asaDataWritePar.Length));
				return;
			}
			byte[] btaDataWritePar = new byte[iSizeData];
			int i = 0;
			foreach (var item in asaDataWritePar)
			{
				btaDataWritePar[i++] = byte.Parse (item);
			}
			DRV.WriteValue (iID, sParams, btaDataWritePar);
		}
		//_________________________________________________________________________
		void CBParameterNameSave()
		{
			if (bDataWritePar_TextChanged)
			{
				bDataWritePar_TextChanged = false;
				if (Properties.Settings.Default.asParameterName.Contains (CBParameterName.Text + ";") == false)
				{
					Properties.Settings.Default.asParameterName += ";" + CBParameterName.Text;
					CBParameterName.Items.Add (CBParameterName.Text);
					Properties.Settings.Default.iParameterName = CBParameterName.Items.Count - 1;
				}
			}
		}
		//_________________________________________________________________________
		void CBDataWriteParSave()
		{
			if (bDataWritePar_TextChanged)
			{
				bDataWritePar_TextChanged = false;
				if (Properties.Settings.Default.asDataWritePar.Contains (CBDataWritePar.Text + ";") == false)
				{
					Properties.Settings.Default.asDataWritePar += ";" + CBDataWritePar.Text;
					CBDataWritePar.Items.Add (CBDataWritePar.Text);
					Properties.Settings.Default.iDataWritePar = CBDataWritePar.Items.Count - 1;
				}
			}
		}
		//_________________________________________________________________________
		void CBDataSave()
		{
			if (bCBData_TextChanged)
			{
				bCBData_TextChanged = false;
				if (Properties.Settings.Default.asData.Contains (CBData.Text + ";") == false)
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
    int[] iaID;
    private void BTest_Click (object sender, EventArgs e)
    {
      //TParameters Pars = (TParameters)DRV.Test (null);
      // TFindPair FindPair = new TFindPair ("Name\":\"ERZ2004a\",\"Type\":\"ERZ2004\",\"Address\":\"0\",\"UrlSingleParam\":\"localhost\",\"PortTCP\":\"503\",\"\":\"\",\"BaudRate\":\"\",\"Port\":\"\",\"Pass1\":\"123\",\"Pass2\":\"123\",\"ModeConn\":\"Ethernet\",\"TD\":\"01062015\",\"TH\":\"01072015\",\"TI\":\"01052015\",\"TA\":\"01052015\",][\"Name\":\"ERZ2004b\",\"Type\":\"ERZ2004\",\"Address\":\"0\",\"UrlSingleParam\":\"localhost\",\"PortTCP\":\"502\",\"\":\"\",\"BaudRate\":\"\",\"Port\":\"\",\"Pass1\":\"123\",\"Pass2\":\"123\",\"ModeConn\":\"Ethernet\",\"TD\":\"01062015\",\"TH\":\"01072015\",\"TI\":\"01052015\",\"TA\":\"01052015\",][\"Name\":\"ERZ2004c\",\"Type\":\"ERZ2004\",\"Address\":\"0\",\"UrlSingleParam\":\"localhost\",\"PortTCP\":\"503\",\"\":\"\",\"BaudRate\":\"\",\"Port\":\"\",\"Pass1\":\"123\",\"Pass2\":\"123\",\"ModeConn\":\"Ethernet\",\"TD\":\"01062015\",\"TH\":\"01072015\",\"TI\":\"01052015\",\"TA\":\"01052015\",][\"Name\":\"ERZ2004d\",\"Type\":\"ERZ2004\",\"Address\":\"0\",\"UrlSingleParam\":\"localhost\",\"PortTCP\":\"502\",\"\":\"\",\"BaudRate\":\"\",\"Port\":\"\",\"Pass1\":\"123\",\"Pass2\":\"123\",\"ModeConn\":\"Ethernet\",\"TD\":\"01062015\",\"TH\":\"01072015\",\"TI\":\"01052015\",\"TA\":\"01052015\",][\"Name\":\"ERZ20046\",\"Type\":\"ERZ2004\",\"Address\":\"0\",\"UrlSingleParam\":\"localhost\",\"PortTCP\":\"503\",\"\":\"\",\"BaudRate\":\"\",\"Port\":\"\",\"Pass1\":\"123\",\"Pass2\":\"123\",\"ModeConn\":\"Ethernet\",\"TD\":\"01062015\",\"TH\":\"01072015\",\"TI\":\"01052015\",\"TA\":\"01052015\",][\"Name\":\"ERZ20047\",\"Type\":\"ERZ2004\",\"Address\":\"0\",\"UrlSingleParam\":\"localhost\",\"PortTCP\":\"502\",\"\":\"\",\"BaudRate\":\"\",\"Port\":\"\",\"Pass1\":\"123\",\"Pass2\":\"123\",\"ModeConn\":\"Ethernet\",\"TD\":\"01062015\",\"TH\":\"01072015\",\"TI\":\"01052015\",\"TA\":\"01052015\",][\"Name\":\"ERZ20048\",\"Type\":\"ERZ2004\",\"Address\":\"0\",\"UrlSingleParam\":\"localhost\",\"PortTCP\":\"503\",\"\":\"\",\"BaudRate\":\"\",\"Port\":\"\",\"Pass1\":\"123\",\"Pass2\":\"123\",\"ModeConn\":\"Ethernet\",\"TD\":\"01062015\",\"TH\":\"01072015\",\"TI\":\"01052015\",\"TA\":\"01052015\",][\"Name\":\"ERZ20049\",\"Type\":\"ERZ2004\",\"Address\":\"0\",\"UrlSingleParam\":\"localhost\",\"PortTCP\":\"502\",\"\":\"\",\"BaudRate\":\"\",\"Port\":\"\",\"Pass1\":\"123\",\"Pass2\":\"123\",\"ModeConn\":\"Ethernet\",\"TD\":\"01062015\",\"TH\":\"01072015\",\"TI\":\"01052015\",\"TA\":\"01052015\",][\"Name\":\"ERZ200410\",\"Type\":\"ERZ2004\",\"Address\":\"0\",\"UrlSingleParam\":\"localhost\",\"PortTCP\":\"503\",\"\":\"\",\"BaudRate\":\"\",\"Port\":\"\",\"Pass1\":\"123\",\"Pass2\":\"123\",\"ModeConn\":\"Ethernet\",\"TD\":\"01062015\",\"TH\":\"01072015\",\"TI\":\"01052015\",\"TA\":\"01052015\",][\"Name\":\"ERZ200411\",\"Type\":\"ERZ2004\",\"Address\":\"0\",\"UrlSingleParam\":\"localhost\",\"PortTCP\":\"502\",\"\":\"\",\"BaudRate\":\"\",\"Port\":\"\",\"Pass1\":\"123\",\"Pass2\":\"123\",\"ModeConn\":\"Ethernet\",\"TD\":\"01062015\",\"TH\":\"01072015\",\"TI\":\"01052015\",\"TA\":\"01052015\",][\"Name\":\"SitransCV\",\"Type\":\"SitransCV\",\"Address\":\"1\",\"UrlSingleParam\":\"localhost\",\"PortTCP\":\"502\",\"\":\"0\",\"BaudRate\":\"115200\",\"Port\":\"COM3\",\"Pass1\":\"\",\"Pass2\":\"\",\"ModeConn\":\"Ethernet\",\"TD\":\"01062015\",\"TH\":\"01072015\",\"TI\":\"01052015\",\"TA\":\"01052015\",][\"Name\":\"KP10\",\"Type\":\"KP10\",\"Address\":\"0\",\"UrlSingleParam\":\"localhost\",\"PortTCP\":\"502\",\"\":\"\",\"BaudRate\":\"\",\"Port\":\"\",\"Pass1\":\"\",\"Pass2\":\"\",\"ModeConn\":\"Ethernet\",\"TD\":\"01062015\",\"TH\":\"01072015\",\"TI\":\"01052015\",\"TA\":\"01052015\",][\"Name\":\"ioLogik\",\"Type\":\"ioLogik\",\"Address\":\"0\",\"UrlSingleParam\":\"localhost\",\"PortTCP\":\"502\",\"\":\"\",\"BaudRate\":\"\",\"Port\":\"\",\"Pass1\":\"\",\"Pass2\":\"\",\"ModeConn\":\"Ethernet\",\"TD\":\"01062015\",\"TH\":\"01072015\",\"TI\":\"01052015\",\"TA\":\"01052015\"");
      // TDevice Dev = new TDevice ("ERZ2004a", CONST.DEVICE.ERZ2004, FindPair);
      // string asFP = "\"DeviceName\":\"ERZ2004\",\"ParameterName\":\"ERZ 14\",\"RequestName\":\"MT3\",\"Data\":\"0 14\",\"RequestType\":\"Single\",\"ParameterType\":\"float\",\"TimeRequest\":\"500\",\"INorOUT\":\"in\",\"NameP\":\"\",\"NamePrus\":\"\"";
      // FindPair = new TFindPair (asFP);
      int iVal;
      int iMax = 6;
      //iaID = new int[iMax];
      for (int i = 0; i < 6; i++)
      {
        iVal = FindValue (i, i);
        iaID = new int[i + 1];
        for (int j = 0; j <= i; j++)
        {
          iaID[j] = j;
        }
        //Pars.Params.Add (new TValue (i, FindPair, Dev));   Pars.FindValue (i, Pars.Params.Count / 2, Pars.Params.Count / 4);
      }
      for (int i = -2; i < 8; i++)
        iVal = FindValue (i, iMax);
    }
    //_________________________________________________________________________
    int FindValue (int iIDpar, int iMaxPos)
    {
      if (iaID == null)
        return 99;
      if (iMaxPos == 0)
        return 99;
      int iIDcurr = iaID[iMaxPos / 2];
      if (iIDcurr > iIDpar)
        return FindValueTop (iIDpar, iMaxPos / 2, iMaxPos / 4, iMaxPos / 2);
      return FindValueBot (iIDpar, iMaxPos / 2, iMaxPos / 4, iMaxPos / 2);
    }
    //_________________________________________________________________________
    public int FindValueTop (int iIDpar, int iPos, int iDelta, int iMaxPos)
    {
      int iIDcurr = iaID[iPos];

      if (iIDcurr > iIDpar)
      {
        iPos -= iDelta;
        if (iPos < 0)
          return 99;
        iDelta /= 2;
        return FindValueTop (iIDpar, iPos, iDelta > 0 ? iDelta : 1, iMaxPos);
      }
      else if (iIDcurr < iIDpar)
      {
        iPos += iDelta;
        if (iPos >= iMaxPos)     //if (iPos >= Params.Count)
          return 99;
        iDelta /= 2;
        return FindValueTop (iIDpar, iPos, iDelta > 0 ? iDelta : 1, iMaxPos);
      }
      return iIDcurr;
    }
    //_________________________________________________________________________
    public int FindValueBot (int iIDpar, int iPos, int iDelta, int iMinPos)
    {
      int iIDcurr = iaID[iPos];

      if (iIDcurr > iIDpar)
      {
        iPos -= iDelta;
        if (iPos < iMinPos)
          return 99;
        iDelta /= 2;
        return FindValueBot (iIDpar, iPos, iDelta > 0 ? iDelta : 1, iMinPos);
      }
      else if (iIDcurr < iIDpar)
      {
        iPos += iDelta;
        if (iPos >= iaID.Length)     //if (iPos >= Params.Count)
          return 99;
        iDelta /= 2;
        return FindValueBot (iIDpar, iPos, iDelta > 0 ? iDelta : 1, iMinPos);
      }
      return iIDcurr;
    }
		//_________________________________________________________________________
		delegate void DWrParams ();
		DWrParams WrParams;
		byte btReg = 1;
		//.........................................................................
		void WriteParams()
		{
			int iParName = 0;
			int iID = (int)NUDIDstart.Value;
			foreach (var addrs in TBAddresses.Lines)
			{
				string asTag = 
				 string.Format ("\"DeviceName\":\"{0},\"ParameterName\":\"{1}\",\"INorOUT\":\"out\",\"RequestType\":\"Single\",\"RequestName\":\"MT16\",\"ParameterType\":\"int\",\"Data\":\"0 {2}\",\"TimeRequest\":\"1000\"", 
					CBDevsWr.Text, iParName, addrs);
				
				byte[] btaWrPar = new byte[4];
				//string[] asaVals = TBWrPars.Lines[iParName].Split(' ');
				//int i = 0;
				//foreach (var item in asaVals)
				//{
				//	btaWrPar[i++] = byte.Parse (item);
				//}
				for (int i = 0; i < 4; i++)
				{
					btaWrPar[i] = btReg++;
				}
				DRV.WriteValue (iID++, asTag, btaWrPar);
				iParName++;
      }
			RBWriting.Checked = !RBWriting.Checked;
    }
		//.........................................................................
		System.Timers.Timer TrWrPars;
		private void BStartWr_Click (object sender, EventArgs e)
		{//{"DeviceName":"%DeviceName%","ParameterName":"IJ02","INorOUT":"out","RequestType":"Single","RequestName":"MT16","ParameterType":"float","Data":"0 5003","TimeRequest":"200"}
			if (TrWrPars != null)	
			{
				TrWrPars.Stop ();
				TrWrPars = null;
				BStartWr.Text = "Старт записи";
				RBWriting.Text = "Стоим-с";
      }
			else
			{
				TrWrPars = new System.Timers.Timer ();
				TrWrPars.Elapsed += TrWrPars_Elapsed;	
				TrWrPars.Interval = (double)NUDIntervalWr.Value * 1000;
				TrWrPars.Start ();
				BStartWr.Text = "Стоп записи";
				RBWriting.Text = "Запись";
				TrWrPars_Elapsed (sender, null);
			}
		}
		//.........................................................................
		private void TrWrPars_Elapsed (object sender, System.Timers.ElapsedEventArgs e)
		{
			WrParams = WriteParams;
			Invoke (WrParams);
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
			Properties.Settings.Default.dmTimeResp = NTimeResp.Value;
		}
		//_________________________________________________________________________
	}
}