﻿///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

///~~~~~~~~~	Проект:			Тест драйверов приборов и др. утилиты
///~~~~~~~~~	Прибор:			Все приборы
///~~~~~~~~~	Модуль:			Эмулятор прибора на Сом-порту  
///~~~~~~~~~	Разработка:	Демешкевич С.А.
///~~~~~~~~~	Дата:				23.01.2019

///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AXONIM.CONSTS;

namespace TestDRVtransGas.COMserver
{
	public partial class FCOMserver : Form
	{
		public enum EDevs { SF21RU5D, SF21RU6D, SF21RU7С };

		COMPort ComPort;
		CTrace Log;

		COMPort.DHandlingRecieve DHandlingRecieve;
		Color ColorCBPort;

		//wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww

		public FCOMserver (Form own)
		{
			InitializeComponent ();
			Owner = own;
			ColorCBPort = CBPort.BackColor;

			CBBaud.SelectedIndex = CBBaud.FindString (Properties.Settings.Default.asBaud);
			CBPort.Items.AddRange (SerialPort.GetPortNames ());
			CBPort.SelectedIndex = CBPort.FindString (Properties.Settings.Default.asCOMport);
			CBDevice.Items.AddRange (Enum.GetNames (typeof (EDevs)));
			CBDevice.SelectedIndex = CBDevice.FindString (Properties.Settings.Default.asDevComPort);
			NUDFont.Value = Properties.Settings.Default.dmSizeFontByComPort;
			ChBChangePar.CheckedChanged -= ChBChangePar_CheckedChanged;
			ChBChangePar.Checked = Properties.Settings.Default.bChangeVal_ComP;
			ChBChangePar.CheckedChanged += ChBChangePar_CheckedChanged;
			//Log = new TTrace ();
		}
		//___________________________________________________________________________
		private void FCOMserver_FormClosing (object sender, FormClosingEventArgs e)
		{
			Owner.Visible = true;
			//CloseAll ();
			//((FTestDrvs)Owner).TCPserver = null;
			if (bFormMove)
			{
				Properties.Settings.Default.iPosComPortX = Left;
				Properties.Settings.Default.iPosComPortY = Top;
			}
			Properties.Settings.Default.Save ();
			if (ComPort != null)
			{
				ComPort.ClosePort ();
			}
		}
		//___________________________________________________________________________
		private void FCOMserver_Load (object sender, EventArgs e)
		{
			//Location = new Point (Owner.Left + 110, Owner.Top + Owner.Height - Height);
			Move -= FCOMserver_Move;
			Location = new Point (Properties.Settings.Default.iPosComPortX, Properties.Settings.Default.iPosComPortY);
			Move += FCOMserver_Move;
		}
		//___________________________________________________________________________
		private void BHideOwner_Click (object sender, EventArgs e)
		{
			if (Owner.Visible)
			{
				Owner.Visible = false;
				BHideOwner.Text = "Показать";
			}
			else
			{
				Owner.Visible = true;
				BHideOwner.Text = "Сокрыть";
			}
		}
		//_________________________________________________________________________
		string AddDTtoMessage (string asMess, bool bDataTimeShow = true)
		{
			if (bDataTimeShow)
			{
				DateTime DT = DateTime.Now;
				//return string.Format ("{0:dd_MM_yy hh_mm_ss}.{1:000}{2}", DT, DT.Millisecond, asMess);
				return string.Format ("{2:hh:mm:ss}.{0:000} {1}", DT.Millisecond, asMess, DT);
			}
			return asMess;
		}
		//___________________________________________________________________________

		public delegate void DMessageShow (string asMess, bool bTateTimeShow = true);
		DMessageShow MS;

		private void OutToTB (string asMess, bool bTateTimeShow)
		{
			TBOut.AppendText (AddDTtoMessage (asMess, bTateTimeShow) + Environment.NewLine);
		}
		//___________________________________________________________________________
		public void MessShow (string asMess, bool bTateTimeShow = true)
		{
			MS = OutToTB;
			Invoke (MS, new object[] { asMess, bTateTimeShow });
		}
		//___________________________________________________________________________
		private void BConnect_Click (object sender, EventArgs e)
		{
			if (CBPort.Text.Length == 0)
			{
				MessageBox.Show ("Не задан порт");
				CBPort.Focus ();
			}
			else
			{
				if (ComPort == null)
				{
					ComPort = new COMPort (MessShow, DHandlingRecieve);
				}
				if (ComPort.PortIsOpen ())
				{
					ComPort.ClosePort ();
					TBOut.AppendText (string.Format ("Порт [{0}] закрыт\n", ComPort.PortName ()));
					BConnect.Text = "Открыть";
				}
				else
				{
					if (ComPort.OpenPort (CBPort.Text, CBBaud.Text) != null)
					{
						BConnect.Text = CBPort.Text;
						TBOut.AppendText (string.Format ("Порт [{0}] открыт\n", ComPort.PortName ()));
					}
				}
			}
		}
		//___________________________________________________________________________
		private void CBPort_SelectedIndexChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.asCOMport = CBPort.Text;
		}
		//___________________________________________________________________________
		private void CBBaud_SelectedIndexChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.asBaud = CBBaud.Text;
		}
		//___________________________________________________________________________
		IDevice DevCurr;
		private void CBDevice_SelectedIndexChanged (object sender, EventArgs e)
		{
			switch (CBDevice.SelectedIndex)
			{
			case (int)EDevs.SF21RU5D:
			case (int)EDevs.SF21RU6D:
			case (int)EDevs.SF21RU7С: DevCurr = new CSuperflo (MessShow, (EDevs)CBDevice.SelectedIndex); break;
			default:
				return;
			}
			Properties.Settings.Default.asDevComPort = CBDevice.Text;
			DHandlingRecieve = DevCurr.HandlingRecieve;
			ComPort?.InitHandlingRecieve (DHandlingRecieve);
		}
		//___________________________________________________________________________
		private void BToClipbrd_Click (object sender, EventArgs e)
		{
			if (TBOut.Text.Length > 4)
			{
				Clipboard.SetText (TBOut.Text);
				TBOut.Text = "";
			}
		}
		//___________________________________________________________________________
		private void CheckBox1_CheckedChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.bWordWrap = ChWordWrap.Checked;
			TBOut.WordWrap = ChWordWrap.Checked;
		}
		//___________________________________________________________________________
		private void Button2_Click (object sender, EventArgs e)
		{
			TBOut.Text = "";
		}
		//___________________________________________________________________________
		private void NUDFont_ValueChanged (object sender, EventArgs e)
		{
			TBOut.Font = new Font (TBOut.Font.FontFamily, (float)NUDFont.Value, TBOut.Font.Style);
			Properties.Settings.Default.dmSizeFontByComPort = NUDFont.Value;
		}
		//___________________________________________________________________________
		bool bFormMove;
		private void FCOMserver_Move (object sender, EventArgs e)
		{
			bFormMove = true;
		}
		//___________________________________________________________________________
		private void BClose_Click (object sender, EventArgs e)
		{
			Owner.Visible = true;
			Close ();
		}
		//___________________________________________________________________________
		private void ChBChangePar_CheckedChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.bChangeVal_ComP = ChBChangePar.Checked;
			if (DevCurr != null)
				DevCurr.bChageParams = ChBChangePar.Checked;
		}
		//___________________________________________________________________________
	}
}
