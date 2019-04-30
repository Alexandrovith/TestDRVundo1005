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
using BelTransGasDRV;
using System.IO;
using DeviceInterfaces;
using System.Runtime.InteropServices;

namespace TestDRVtransGas
{
	public partial class FTestDrivers : Form
	{
		TDevices Devices = null;
		TParams Params = null;
		readonly string sCaption;
		public FTestDrivers()
		{
			InitializeComponent();
			sCaption = this.Text;
			Devices = new TDevices(TPDevices);
			Params = new TParams(TPParams, this);
			Left = 60;
			this.Top = 0;			//Size resolution = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
			Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;
			BTest1.Select();

			CreateTemplateIndic ();
			CBStateLog.SelectedIndex = 1;
		}
		//_________________________________________________________________________
		public void CreateTemplateIndic()
		{
			ВсегоПараметров = Params.GV.RowCount - 1;
			for (int i = 1; i <= ВсегоПараметров; i++)
			{
				CreateComboBox ((string)Params.GV.Rows[i - 1].Cells[1].Value, PParameters);		
			}
		}
		//_________________________________________________________________________
		private void BClose_Click(object sender, EventArgs e)
		{
			Close();
		}
		//_________________________________________________________________________
		private void FTestDrivers_FormClosing(object sender, FormClosingEventArgs e)
		{
			TScanParam.Enabled = false;
			Devices.Save();
			Params.Save();
			DRV.Close ();
		}
		//_________________________________________________________________________

		Tdrv DRV = new Tdrv();
		private void BTest1_Click(object sender, EventArgs e)
		{
			//DRV.Close();
			//DRV.GetValue(1, ref ip);
			//unsafe {	int* f = (int*)ip.ToPointer();}			
			CBErrors.Items.Clear ();

			DRV.Init (null, Devices.InitStr());
			for (int i = 1; i <= ВсегоПараметров; i++)
			{
				string sParams = Params.InitStr (i - 1);
				DRV.Subscribe (i, sParams);
				CBErrors.Items.Add (sParams);
			}
			Text = sCaption + " РАБОТАЕТ";
		}
		//_________________________________________________________________________
		private void BStopDrv_Click (object sender, EventArgs e)
		{
			DRV.Close();															//DestroyLabelsByParam();			
			BSwithLog_Click (sender, e);
			Text = sCaption + " ОСТАНОВЛЕН";
		}
		//_________________________________________________________________________
		private void BGetErr_Click(object sender, EventArgs e)
		{
			CBErrors.Items.Clear();
			TErrors Err = Tdrv.Errors;
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
		const int iYdef = 4; int iX = 10, iY = iYdef; const int iIncY = 15;
		const int iXData = 180;
		public void CreateLabel(string sNameParam, Control Prnt)
		{
			Label L1 = new Label();
			L1.Parent = Prnt;
			L1.Location = new Point(iX, iY);
			string sNum = iNumParam.ToString();
			//Font F = new System.Drawing.Font("Tahoma", 8.0f);// (FontFamily.GenericSansSerif, 8.0f, FontStyle.Regular);Arial Narrow
			//L1.Font = F;
			L1.AutoSize = true;
			L1.Text = sNameParam;

			Label L2 = new Label();
			L2.Parent = Prnt;
			L2.Name = "L" + sNum;
			L2.Location = new Point (iX + iXData, iY);
			L2.AutoSize = true;

			iY += iIncY; iNumParam++;
		}
		const string sPrefixCB = "CB";
		//.........................................................................
		public void CreateComboBox(string sNameParam, Control Prnt)
		{
			ComboBox CB = new ComboBox();
			CB.Parent = Prnt;
			CB.Name = sPrefixCB + iNumParam.ToString ();
			CB.DropDownStyle = ComboBoxStyle.DropDownList;
			CB.AutoSize = false;
			//CB.ItemHeight = iIncY;
			CB.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
			CB.Location = new Point (iX + iXData, iY);
			CB.Size = new Size(CB.Parent.Width - CB.Location.X, 14);

			Label L1 = new Label();
			L1.Parent = Prnt;
			L1.AutoSize = true;
			L1.Location = new Point(iX, iY + iY / 70);
			L1.Text = sNameParam;

			iY += iIncY; iNumParam++;
		}
		//_________________________________________________________________________
		public void DestroyLabelsByParam()
		{
			PParameters.Controls.Clear();
			iNumParam = 1; iX = 10; iY = iYdef;
			ВсегоПараметров = 0;
		}
		//_________________________________________________________________________
		public string ConvertVal (byte[] btData, CONST.TYPE_DATA TypeData)
		{
			//btData.Reverse();
			if (btData.Length <= 0)
				return "Нет данных";
			switch (TypeData)
			{
			case CONST.TYPE_DATA.AC10:
			case CONST.TYPE_DATA.AC20:
			case CONST.TYPE_DATA.AC30:
			case CONST.TYPE_DATA.BIN:
			case CONST.TYPE_DATA.TLP:
			case CONST.TYPE_DATA.DT: return BitConverter.ToString(btData);
			case CONST.TYPE_DATA.@float: float f = BitConverter.ToSingle(btData, 0); return f.ToString();
			case CONST.TYPE_DATA.@double: double d = BitConverter.ToDouble(btData, 0); return d.ToString();
			case CONST.TYPE_DATA.@short: short s = BitConverter.ToInt16(btData, 0); return s.ToString();
			case CONST.TYPE_DATA.@ushort: ushort us = BitConverter.ToUInt16(btData, 0); return us.ToString();
			case CONST.TYPE_DATA.@int: int i = BitConverter.ToInt32(btData, 0); return i.ToString();
			case CONST.TYPE_DATA.@uint: uint ui = BitConverter.ToUInt32(btData, 0); return ui.ToString();
			case CONST.TYPE_DATA.@long: long l = BitConverter.ToInt64(btData, 0); return l.ToString();
			case CONST.TYPE_DATA.@ulong: ulong ul = BitConverter.ToUInt64(btData, 0); return ul.ToString();
			case CONST.TYPE_DATA.INT16: Int16 i16 = BitConverter.ToInt16(btData, 0); return i16.ToString();
			case CONST.TYPE_DATA.INT32: Int32 ui32 = BitConverter.ToInt16(btData, 0); return ui32.ToString();
			case CONST.TYPE_DATA.INT8: char c = BitConverter.ToChar(btData, 0); return c.ToString();
			case CONST.TYPE_DATA.UINT16: UInt16 ui16 = BitConverter.ToUInt16(btData, 0); return ui16.ToString();
			case CONST.TYPE_DATA.UINT32: UInt32 i32 = BitConverter.ToUInt16(btData, 0); return i32.ToString();
			case CONST.TYPE_DATA.UINT8: byte bt = (byte)BitConverter.ToChar(btData, 0); return bt.ToString();
			}
			return "Нет типа";
			//dynamic Val;
			//switch (TYPE_DATA)
			//{
			//case CONST.TYPE_DATA.AC10: 
			//case CONST.TYPE_DATA.AC20:
			//case CONST.TYPE_DATA.AC30: 
			//case CONST.TYPE_DATA.BIN:
			//case CONST.TYPE_DATA.TLP: 
			//case CONST.TYPE_DATA.DT:	 return BitConverter.ToString(btData);
			//case CONST.TYPE_DATA.FLOAT: Val = BitConverter.ToSingle(btData, 0); break;
			//case CONST.TYPE_DATA.INT16: Val = BitConverter.ToInt16(btData, 0); break;
			//case CONST.TYPE_DATA.INT32: Val = BitConverter.ToInt16(btData, 0); break;
			//case CONST.TYPE_DATA.INT8: Val = BitConverter.ToChar(btData, 0); break;
			//case CONST.TYPE_DATA.UINT16: Val = BitConverter.ToUInt16(btData, 0); break;
			//case CONST.TYPE_DATA.UINT32: Val = BitConverter.ToUInt16(btData, 0); break;
			//case CONST.TYPE_DATA.UINT8: Val = (byte)BitConverter.ToChar(btData, 0); break;
			//default: return "ErrType";
			//}
			//return Val.ToString();
		}
		//_________________________________________________________________________
		private void TScanParam_Tick(object sender, EventArgs e)
		{
			RBScan.Checked = !RBScan.Checked;
			for (int i = 1; i <= ВсегоПараметров; i++)
			{
				Control[] C = PParameters.Controls.Find(sPrefixCB + i.ToString(), false);
				if (C != null)
				{
					if (C.Length > 0)
					{
						ComboBox L = (ComboBox)C[0];
						if (L != null)
						{
							L.Items.Clear();
							if (DRV.GetTypeArch(i) == TArchive.NAMES.Single)
							{
								DeviceInterfaces.DataMessage DM = DRV.GetValue (i);		// List (i); //DM.value = new byte[] { 0x3b, 0xdf, 0x47, 0x40 };
								if (DM != null)
								{
									L.Items.Add (string.Format ("{0:D2}. [{1}] {2}; {3}", DM.id, DM.time, DM.quality, ConvertVal(DM.value, DRV.GetTypeVal(i))));
									L.SelectedIndex = 0;
								}
							}
							else
							{
								if (i == 2) continue;			// TODO: EDIT / DELETE 

								DeviceArchiveRecord[] Archs = DRV.GetNewArchiveData (i);
								if (Archs != null)
								{
									bool bOutHeaders = true;
									string asHead = "";
									foreach (DeviceArchiveRecord Row in Archs)
									{
										//Application.DoEvents ();
										Thread.Sleep (0);
										string asRow = "";
										foreach (DeviceArchiveParameter Par in Row.data)
										{
											if (bOutHeaders)
											{
												asHead += Par.name + " ";				// Заголовки			 
											}
											asRow += Par.value + " ";					// Значения архива построчно 
										}
										if (bOutHeaders)
										{
											bOutHeaders = false;
											L.Items.Add (asHead);
										}
										L.Items.Add (asRow);
									}
									if (L.Items.Count > 0)
										L.SelectedIndex = 0;
								}
							}
						}
					}
				}
			}
		}
		////_________________________________________________________________________
		//private void TScanParam_Tick(object sender, EventArgs e)
		//{
		//	RBScan.Checked = !RBScan.Checked;
		//	for (int i = 1; i <= ВсегоПараметров; i++)
		//	{
		//		Control[] C = PParameters.Controls.Find(sPrefixCB + i.ToString(), false);
		//		if (C != null)
		//		{
		//			if (C.Length > 0)
		//			{
		//				ComboBox L = (ComboBox)C[0];
		//				if (L != null)
		//				{
		//					L.Items.Clear();
		//					List<DataMessage> DM = DRV.GetValueList(i); //DM.value = new byte[] { 0x3b, 0xdf, 0x47, 0x40 };
		//					if (DM != null)
		//					{
		//						foreach (DataMessage item in DM)
		//						{
		//							L.Items.Add (string.Format ("{0:D2}. [{1}] {2}; {3}", 
		//													 item.id, item.time, item.quality, ConvertVal(item.value, DRV.GetTypeVal(i))));
		//						}
		//						L.SelectedIndex = 0;
		//					}
		//				}
		//			}
		//		}
		//	}
		//}
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
      TErrors Err = Tdrv.Errors;
      Err.Clear();
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
				Tdrv.SetSwitchLog ((STATE_WRITE_MESS)CBStateLog.SelectedIndex);
				BSwithLog.Text = "Отключить Log";
			}
			else 
			{
				Tdrv.CloseLog ();
				bLogON = false;
				BSwithLog.Text = "Включить Log";
			}
		}
		//_________________________________________________________________________
	}
}