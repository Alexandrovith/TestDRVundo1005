///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

///~~~~~~~~~	Проект:			Тест драйверов приборов
///~~~~~~~~~	Прибор:			Все приборы
///~~~~~~~~~	Модуль:			Передача данных TCP <-> ComPort
///~~~~~~~~~	Разработка:	Демешкевич С.А.
///~~~~~~~~~	Дата:				13.12.2018

///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net.Sockets;
using AXONIM.ScanParamOfDevicves;
using AXONIM.CONSTS;
using System.Threading;
using System.IO.Ports;
using TestDRVtransGas.TCPtoTCP;

namespace TestDRVtransGas.TCPtoComPort
{
	public partial class CTCPtoComPort : /*UserControl, IUserControls*/ CTCPtoAny
	{
		/// <summary>
		/// Сервер активного устройства
		/// </summary>
		CTCPlistener Server;
		/// <summary>
		/// Отправка команд пассивному устройству
		/// </summary>
		CComPortClient Client;

		public event CTCPlistener.DExch EvSendAnswerToServer, EvSendDataToDev;
		bool bFirstInput = true;
		bool bAddActiveIP;

		FTestDrvs Own;

		//wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww

		public CTCPtoComPort (FTestDrvs Own)
		{
			InitializeComponent ();
			this.Own = Own;
			ChWordWrap.Checked = Properties.Settings.Default.bWordWrap;
			TBOut.WordWrap = ChWordWrap.Checked;

			CBIP1.Items.AddRange (Properties.Settings.Default.asIPactiveListComP.Split (';'));
			CBIP1.SelectedIndex = CBIP1.FindString (Properties.Settings.Default.asIPactive);
			if (CBIP1.SelectedIndex == -1 && CBIP1.Items.Count > 0)
				CBIP1.SelectedIndex = 0;

			CBComPort.Items.AddRange (SerialPort.GetPortNames ());
			CBComPort.SelectedIndex = CBComPort.FindString (Properties.Settings.Default.asComPortToComP);
			if (CBComPort.SelectedIndex == -1 && CBComPort.Items.Count > 0)
				CBComPort.SelectedIndex = 0;

			UDPort1.Value = Properties.Settings.Default.iPortActive;

			NUDFont.Value = Properties.Settings.Default.dmSiseFontTCTtoTCT;

			CBBaud.SelectedIndex = CBBaud.FindString (Properties.Settings.Default.asBaudToComP);
			if (CBBaud.SelectedIndex == -1 && CBBaud.Items.Count > 0)
				CBBaud.SelectedIndex = 0;

			CBParity.Items.AddRange (Enum.GetNames (typeof (Parity)));
			CBParity.SelectedIndex = CBParity.FindString (Properties.Settings.Default.asParityTCTtoComP);
			if (CBParity.SelectedIndex == -1 && CBParity.Items.Count > 0)
				CBParity.SelectedIndex = 0;

			CBStopBits.Items.AddRange (Enum.GetNames (typeof (StopBits)));
			CBStopBits.SelectedIndex = CBStopBits.FindString (Properties.Settings.Default.asStopBitsTCPtoComP);
			if (CBStopBits.SelectedIndex == -1 && CBStopBits.Items.Count > 0)
				CBStopBits.SelectedIndex = 0;

			CBDataBit.SelectedIndex = CBDataBit.FindString (Properties.Settings.Default.asDataBit);
			if (CBDataBit.SelectedIndex == -1 && CBDataBit.Items.Count > 0)
				CBDataBit.SelectedIndex = 0;

			ChWordWrap.Checked = Properties.Settings.Default.bWordWrapByComPort;

			OutMess = Inv_OutMess;
			bFirstInput = false;
		}
		//_________________________________________________________________________
		private void BTCP_TCP_Click (object sender, EventArgs e)
		{
			try
			{
				if (Server == null)
				{
					Server = new CTCPlistener (CBIP1.Text, (int)UDPort1.Value, ref EvSendAnswerToServer, this);
					if (Server.IsConnect)
					{
						Server.EvGetData += Server1_GetData;
						BConnect1.Text = "Разъединить";
						if (bAddActiveIP)
						{
							Properties.Settings.Default.asIPactiveList = String.Join (";", Properties.Settings.Default.asIPactiveList, CBIP1.Text);
						}
						OutMess ($"Подкл. {CBIP1.Text}:{UDPort1.Value}");
					}
					else
					{
						OutMess ($"Нет соединения с {CBIP1.Text}:{UDPort1.Value}");
					}
				}
				else
				{
					ServerClose ();
					Inv_OutMess ($"Откл. {CBIP1.Text}");
					BConnect1.Text = "Соединить";
				}
			}
			catch (Exception exc)
			{
				Inv_OutMess ($"{CBIP1.Text}: {exc.Message}{Environment.NewLine}{exc.StackTrace}");
			}
		}
		//_________________________________________________________________________
		private void ServerClose ()
		{
			if (Server != null)
			{
				Server.Close ();
				Server = null;
			}
		}
		//_________________________________________________________________________
		/// <summary>
		/// Обработка команды от программы (или драйвера)
		/// </summary>
		private void Server1_GetData (object Buf, object SizeBuf)
		{
			int iSizeData = (int)SizeBuf;
			byte[] btaBuf = Buf as byte[];
			if (btaBuf == null)
				return;

			if (!(iSizeData == 1 && btaBuf[0] == 0))
				Inv_OutMess ($"Active: {Global.ByteArToStr (btaBuf, 0, iSizeData)} [{Global.BytesToInt_Char (btaBuf, iSizeData)}");//{CBIP1.Text}

			if (Client != null)
			{
				Global.Conv8n1To7e1 (btaBuf, iSizeData);
				Client.SendAsync (btaBuf, iSizeData, RecieveDataPassive);
				EvSendDataToDev?.Invoke (Buf, SizeBuf);
			}
			else Inv_OutMess ($"Не подключен {GetTextCB (CBIP1)}");
		}
		//_________________________________________________________________________

		delegate void DGetTextCB (ComboBox CB);
		public string GetTextCB (ComboBox CB)
		{
			string asRet = "";
			try
			{
				if (this.InvokeRequired)
					Invoke (new DGetTextCB (delegate
					{
						asRet = CB.Text;
					}), CB);
				else asRet = CB.Text;
			}
			catch (Exception ex)
			{
				MessageBox.Show (ex.Message);
			}
			return asRet;
		}
		//_________________________________________________________________________

		delegate void DSetTextB (Button CB, string asText);
		public void SetTextB (Button CB, string asText)
		{
			try
			{
				if (this.InvokeRequired)
					Invoke (new DSetTextB (delegate
					{
						CB.Text = asText;
					}), CB, asText);
				else CB.Text = asText;
			}
			catch (Exception ex)
			{
				MessageBox.Show (ex.Message);
			}
		}
		//_________________________________________________________________________
		private void Server2_GetData (object Buf, object SizeBuf)
		{
			EvSendDataToDev?.Invoke (Buf, SizeBuf);
			TBOut.AppendText ($"{CBComPort.Text}: {Global.ByteArToStr ((byte[])Buf, 0, (int)SizeBuf)}");
		}
		//_________________________________________________________________________
		/// <summary>
		/// Обработка ответа от прибора
		/// </summary>
		private void RecieveDataPassive (byte[] BufRX, int iLenRX)
		{
			lock (this)
			{
				try
				{
					EvSendAnswerToServer?.Invoke (BufRX, iLenRX);
					Inv_OutMess ($"Прибор: {Global.ByteArToStr (BufRX, 0, iLenRX)} [{Global.BytesToInt_Char (BufRX, iLenRX)}");
				}
				catch (Exception exc)
				{
					Inv_OutMess ($"RecieveDataPassive: {exc.Message}{Environment.NewLine}{exc.StackTrace}");
				} 
			}
		}
		//_________________________________________________________________________

		bool bConnClient;
		private void BConnect2_Click (object sender, EventArgs e)
		{
			try
			{
				if (bConnClient == false)
				{
					if (Client == null)
					{
						Client = new CComPortClient (CBComPort.Text, int.Parse (CBBaud.Text), (Parity)Enum.Parse (typeof (Parity), CBParity.Text),
																				 int.Parse (CBDataBit.Text), (StopBits)Enum.Parse (typeof (StopBits), CBStopBits.Text));
						Client.Log += Inv_OutMess;
					}
					//if (Client == null)						return;
					// Соединение с портом COM 
					ThreadPool.QueueUserWorkItem ((object oInfo) =>
					{
						string asComPort = (string)oInfo;
						Inv_OutMess ($"Подключение к {asComPort} ...");
						bConnClient = Client.Connect ();
						if (bConnClient)
						{
							SetTextB (BConnect2, "Разъединить");
							Inv_OutMess ($"Подкл. {asComPort}");
							//if (bAddPassiveIP)
							//{
							//	Properties.Settings.Default.asIPpassiveList = string.Join (";", Properties.Settings.Default.asIPpassiveList, CBComPort.Text);
							//}
						}
						else
						{
							Inv_OutMess ($"Соединение с {asComPort} не установлено");
							Client.Close ();
							Client = null;
						}
					}
				, CBComPort.Text);
				}
				else
				{
					ClientClose ();
					BConnect2.Text = "Соединить";
					Inv_OutMess ($"Откл. {CBComPort.Text}");
					bConnClient = false;
				}
			}
			catch (Exception exc)
			{
				Inv_OutMess ($"{CBComPort.Text}: {exc.Message}{Environment.NewLine}{exc.StackTrace}");
			}
		}
		//_________________________________________________________________________
		private void ClientClose ()
		{
			if (Client != null)
			{
				Client.Close ();
				Client = null;
			}
		}
		//_________________________________________________________________________
		private void ChWordWrap_CheckedChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.bWordWrapByComPort = ChWordWrap.Checked;
			TBOut.WordWrap = ChWordWrap.Checked;
		}
		//_________________________________________________________________________
		private void CTCPtoComPort_Load (object sender, EventArgs e)
		{
			Width = Parent.Width;
			Height = Parent.Height;
		}
		//_________________________________________________________________________
		public void Inv_OutMess (string asMess, bool bDateOut = true)
		{
			DateTime DT = DateTime.Now;
			string asDT = bDateOut ? $"{DT:ddMMyy HH:mm:ss}.{DT.Millisecond:000}" : "";
			DOutMess OutMess = (m, DateOut) => TBOut.AppendText ($"[{asDT}] {asMess}{Environment.NewLine}");
			Invoke (OutMess, asMess/*, asDelimiter*/, bDateOut);
		}
		//_________________________________________________________________________
		private void CBIP1_SelectedIndexChanged (object sender, EventArgs e)
		{
			if (bFirstInput == false)
				Properties.Settings.Default.asIPactive = CBIP1.Text;
		}
		//_________________________________________________________________________
		private void CBPort_SelectedIndexChanged (object sender, EventArgs e)
		{
			if (bFirstInput == false)
				Properties.Settings.Default.asComPortToComP = CBComPort.Text;
		}
		//_________________________________________________________________________
		private void UDPort1_ValueChanged (object sender, EventArgs e)
		{
			if (bFirstInput == false)
				Properties.Settings.Default.iPortActive = (int)UDPort1.Value;
		}
		//_________________________________________________________________________
		private void BClose_Click (object sender, EventArgs e)
		{
			Own.Close ();
		}
		//_________________________________________________________________________
		private void CBIP1_TextUpdate (object sender, EventArgs e)
		{
			bAddActiveIP = true;
		}
		//_________________________________________________________________________
		private void BClearOut_Click (object sender, EventArgs e)
		{
			TBOut.Text = "";
		}
		//_________________________________________________________________________
		private void BToClipbrd_Click (object sender, EventArgs e)
		{
			if (TBOut.Text.Length > 4)
			{
				Clipboard.SetText (TBOut.Text);
				TBOut.Text = "";
			}
		}
		//_________________________________________________________________________
		private void NUDFont_ValueChanged (object sender, EventArgs e)
		{
			TBOut.Font = new Font (TBOut.Font.FontFamily, (float)NUDFont.Value, TBOut.Font.Style);
			Properties.Settings.Default.dmSiseFontTCTtoTCT = NUDFont.Value;
		}
		//_________________________________________________________________________
		private void CBParity_SelectedIndexChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.asParityTCTtoComP = CBParity.Text;
		}
		//_________________________________________________________________________
		private void CBBaud_SelectedIndexChanged (object sender, EventArgs e)
		{
			if (bFirstInput == false)
				Properties.Settings.Default.asBaudToComP = CBBaud.Text;
		}
		//_________________________________________________________________________
		private void CBDataBit_SelectedIndexChanged (object sender, EventArgs e)
		{
			if (bFirstInput == false)
				Properties.Settings.Default.asDataBit = CBDataBit.Text;
		}
		//_________________________________________________________________________
		private void CBStopBits_SelectedIndexChanged (object sender, EventArgs e)
		{
			if (bFirstInput == false)
				Properties.Settings.Default.asStopBitsTCPtoComP = CBStopBits.Text;
		}
		//_________________________________________________________________________
		public override void Close ()
		{
			Properties.Settings.Default.Save ();
			ServerClose ();
			ClientClose ();
		}
		//_________________________________________________________________________
		//public new void ReSize ()
		//{
		//	Width = this.Parent.Width;
		//	Height = this.Parent.Height;
		//}
	}
}
