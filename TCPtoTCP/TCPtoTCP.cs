///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

///~~~~~~~~~	Проект:			Тест драйверов приборов
///~~~~~~~~~	Прибор:			Все приборы
///~~~~~~~~~	Модуль:			Передача данных TCP <-> TCP
///~~~~~~~~~	Разработка:	Демешкевич С.А.
///~~~~~~~~~	Дата:				22.10.2018

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

namespace TestDRVtransGas.TCPtoTCP
{
	public partial class CTCPtoTCP : /*UserControl, IUserControls,*/ CTCPtoAny
	{
		/// <summary>
		/// Сервер активного устройства
		/// </summary>
		CTCPlistener Server;
		/// <summary>
		/// Отправка команд пассивному устройству
		/// </summary>
		CTCPclient Client;

		public event CTCPlistener.DExch EvSendAnswerToServer, EvSendDataToDev;

		//public delegate void DOutMess (string asMess/*, string asDelimiter, bool bOutTime = true*/);
		//public DOutMess OutMess;

		bool bAddActiveIP, bAddPassiveIP;

		FTestDrvs Own;

		//wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww

		public CTCPtoTCP (FTestDrvs Own)
		{
			InitializeComponent ();
			this.Own = Own;
			ChWordWrap.Checked = Properties.Settings.Default.bWordWrap;
			TBOut.WordWrap = ChWordWrap.Checked;

			CBIP1.Items.AddRange (Properties.Settings.Default.asIPactiveList.Split (';'));
			CBIP1.SelectedIndex = CBIP1.FindString (Properties.Settings.Default.asIPactive);
			if (CBIP1.SelectedIndex == -1 && CBIP1.Items.Count > 0)
				CBIP1.SelectedIndex = 0;

			CBIP2.Items.AddRange (Properties.Settings.Default.asIPpassiveList.Split (';'));
			CBIP2.SelectedIndex = CBIP2.FindString (Properties.Settings.Default.asIPpassive);
			if (CBIP2.SelectedIndex == -1 && CBIP2.Items.Count > 0)
				CBIP2.SelectedIndex = 0;

			UDPort1.Value = Properties.Settings.Default.iPortActive;
			UDPort2.Value = Properties.Settings.Default.iPortPassive;

			NUDFont.Value = Properties.Settings.Default.dmSiseFontTCTtoTCT;

			OutMess = Inv_OutMess;
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
			TBOut.AppendText ($"{CBIP2.Text}: {Global.ByteArToStr ((byte[])Buf, 0, (int)SizeBuf)}");
		}
		//_________________________________________________________________________
		/// <summary>
		/// Обработка ответа от прибора
		/// </summary>
		private void RecieveDataPassive (IAsyncResult ar)
		{
			try
			{
				if (ar.IsCompleted == false)
					return;
				CTCPclient ClientCurr = ar.AsyncState as CTCPclient;
				if (ClientCurr == null)
					return;
				NetworkStream Stream = ClientCurr.Stream;
				if (Stream != null)
				{
					int iLenRX = Stream.EndRead (ar);
					if (iLenRX == 0)
					{
						Stream.Close ();
						return;
					}

					EvSendAnswerToServer?.Invoke (ClientCurr.BufRX, iLenRX);
					Inv_OutMess ($"Прибор: {Global.ByteArToStr (ClientCurr.BufRX, 0, iLenRX)} [{Global.BytesToInt_Char (ClientCurr.BufRX, iLenRX)}");//{CBIP2.Text}

				}
			}
			catch (Exception exc)
			{
				Inv_OutMess ($"RecieveDataPassive: {exc.Message}{Environment.NewLine}{exc.StackTrace}");
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
						Client = new CTCPclient ();
					}
					ThreadPool.QueueUserWorkItem ((Object oInfo) =>
					{
						string asIP = (string)oInfo;
						Inv_OutMess ($"Подключение к {asIP} ...");
						bConnClient = Client.Connect (asIP, (int)UDPort2.Value);
						if (bConnClient)
						{
							SetTextB (BConnect2, "Разъединить");
							Inv_OutMess ($"Подкл. {asIP}");
							if (bAddPassiveIP)
							{
								Properties.Settings.Default.asIPpassiveList = String.Join (";", Properties.Settings.Default.asIPpassiveList, CBIP2.Text);
							}
						}
						else
						{
							Inv_OutMess ($"Соединение с {asIP} не установлено");
						}
					}
				, CBIP2.Text);
				}
				else
				{
					ClientClose ();
					BConnect2.Text = "Соединить";
					Inv_OutMess ($"Откл. {CBIP2.Text}");
					bConnClient = false;
				}
			}
			catch (Exception exc)
			{
				Inv_OutMess ($"{CBIP2.Text}: {exc.Message}{Environment.NewLine}{exc.StackTrace}");
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
			Properties.Settings.Default.bWordWrap = ChWordWrap.Checked;
			TBOut.WordWrap = ChWordWrap.Checked;
		}
		//_________________________________________________________________________
		private void CTCPtoTCP_Load (object sender, EventArgs e)
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
			Properties.Settings.Default.asIPactive = CBIP1.Text;
		}
		//_________________________________________________________________________
		private void CBIP2_SelectedIndexChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.asIPpassive = CBIP2.Text;
		}
		//_________________________________________________________________________
		private void CBIP2_TextUpdate (object sender, EventArgs e)
		{
			bAddPassiveIP = true;
		}
		//_________________________________________________________________________
		private void UDPort1_ValueChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.iPortActive = (int)UDPort1.Value;
		}
		//_________________________________________________________________________
		private void UDPort2_ValueChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.iPortPassive = (int)UDPort2.Value;
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
		public override void Close ()
		{
			ServerClose ();
			ClientClose ();
		}
		//_________________________________________________________________________
		//public void ReSize ()
		//{
		//	Width = this.Parent.Width;
		//	Height = this.Parent.Height;
		//}
	}
}
