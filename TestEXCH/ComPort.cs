///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

///~~~~~~~~~	Проект:			Тест драйверов приборов
///~~~~~~~~~	Прибор:			Все приборы
///~~~~~~~~~	Модуль:			Отправка/получение данных по ComPort
///~~~~~~~~~	Разработка:	Демешкевич С.А.
///~~~~~~~~~	Дата:				06.08.2017

///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using AXONIM.CONSTS;
using TestDRVtransGas.TCPserver.TCPtoComPort;
using System.Net.Sockets;

namespace TestDRVtransGas
{
	public class CComPort
	{
		public SerialPort SP { get; set; }
		CTestExch Own;
		protected int iNumRequest = 1;
		int iCountRequest = 0;

		protected byte[] btaBufRX;
		/// <summary>
		/// Выдачача полученных по Com-порту данных подписчику
		/// </summary>
		public CTCP_Com.DRecieve EvRecieve;

		//wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww
		public CComPort (CTestExch Own)
		{
			this.Own = Own as CTestExch;
			btaBufRX = new byte[4096];
		}
		//_________________________________________________________________________
		/// <summary>
		/// Минимально ожидаемое количество байт ответа
		/// </summary>
		public int LenRecievedMin { get; set; }
		int iBytesRecieved;

		protected virtual void SP_DataReceived (object sender, SerialDataReceivedEventArgs e)//object sender, SerialDataReceivedEventArgs e
		{
			int iBytes = SP.Read (btaBufRX, 0, SP.BytesToRead);
			OutMess ("[" + SP.PortName + "] Получено " + iBytes + "\t[" + Global.ByteArToStr (btaBufRX, 0, iBytes) + "]", ""/*Environment.NewLine*/);
			iBytesRecieved += iBytes;
			if (++iCountRequest < iNumRequest/* && iBytesRecieved >= LenRecievedMin*/)
			{
				byte[] btaTX = Own.ConvStrToComm (Own.TBCommand.Lines[iCountRequest], iCountRequest);
				SendComm (btaTX, 0, btaTX.Length);
			}
			else
			{
				SP.DtrEnable = false;
				EvRecieve?.Invoke (btaBufRX, iBytes); 
			}
		}
		//_________________________________________________________________________
		/// <summary>
		/// Дополнительные настройки перед отправкой запроса прибору
		/// </summary>
		public virtual void BeforeRequest ()
		{
			iCountRequest = 0;
			PreviousComm = EPreviousComm.Begin;//iBytesRecieved
			iNumRequest = Own.TBCommand.Lines.Count ();
		}
		//_________________________________________________________________________
		private void SP_ErrorReceived (object sender, SerialErrorReceivedEventArgs e)
		{
			OutMess ("[" + SP.PortName + "] Ошибка запроса: " + e.EventType.ToString (), Environment.NewLine);
		}
		//_________________________________________________________________________
		public bool SendComm (byte[] btaBuf, int iOffset, int iCount)
		{
			try
			{
				if (SP.IsOpen)
				{
					if (Own.ChBModeRequest.Checked)
					{
						SP.ReceivedBytesThreshold = GetReceivedBytesThreshold (btaBuf, 0);
						SP.DtrEnable = true;  
					}
					else
					{
						SP.ReceivedBytesThreshold = 10;						//SP.DtrEnable = true;  
					}
					SP.Write (btaBuf, iOffset, iCount);
				}
			}
			catch (Exception exc)
			{
				OutMess ("[COM] Ошибка при отправке: " + exc.Message + ". " + exc.StackTrace, Environment.NewLine);
				return false;
			}
			OutMess ("[" + SP.PortName + "] Ком. " + iCountRequest + "\t[" + Global.ByteArToStr (btaBuf, iOffset, iCount) + "]", iCountRequest == 0 ? Environment.NewLine : "");
			return true;
		}
		//_________________________________________________________________________
		public bool IsOpen
		{
			get
			{
				if (SP == null)
					return false;
				return SP.IsOpen;
			}
		}
		//_________________________________________________________________________
		public void OutMess (string asMess, string asBeforeDT, bool bOutTime = true)
		{
			Own.Invoke (Own.OutMess, asMess, asBeforeDT, bOutTime);
		}
		//_________________________________________________________________________
		public enum EPreviousComm { SYS, N, Arch, Date, Begin }
		EPreviousComm PreviousComm = EPreviousComm.Begin;
		public virtual int GetReceivedBytesThreshold (byte[] btaBuf, int iStartPosData)
		{
			if (PreviousComm == EPreviousComm.Begin)
			{
				if (btaBuf[0 + iStartPosData] == 'S' && btaBuf[1 + iStartPosData] == 'Y' && btaBuf[2 + iStartPosData] == 'S')
				{
					PreviousComm = EPreviousComm.SYS;
					return 17;
				}
			}
			else
			{
				if (PreviousComm == EPreviousComm.SYS || PreviousComm == EPreviousComm.N)
				{
					PreviousComm = EPreviousComm.N;
					return btaBuf[11 + iStartPosData] + 2;
				}
			}
			return 5;
		}
		//_________________________________________________________________________
		public virtual bool PortCreate ()
		{
			try
			{
				SP = new SerialPort ("COM" + Own.UDCOM.Value, int.Parse (Own.CBBaudCOM.Text),
					(Parity)Own.CBParity.SelectedIndex, (int)Own.NUDDataBits.Value, (StopBits)Own.CBStopBits.SelectedIndex);// Parity.None, 8, StopBits.One);
				SP.ErrorReceived += SP_ErrorReceived;
				SP.Handshake = Handshake.None;
				SP.DataReceived += SP_DataReceived;
				//SP.Open ();
				//Own.TBListExchange.AppendText ("Порт [" + SP.PortName + "] открыт.");
			}
			catch (Exception exc)
			{
				OutMess ("Открытие порта [" + Own.UDCOM.Value + "]: " + exc.Message + Environment.NewLine + exc.StackTrace, ""); return false;
			}
			return true;
		}
		//_________________________________________________________________________
		public bool ChangeParams (Action Func)
		{
			if (SP != null)
			{
				Func ();
				return true;
			}
			return false;
		}
		//_________________________________________________________________________
		private void SetPort ()
		{
			SP.PortName = "COM" + Own.UDCOM.Value.ToString ();
			OutMess ($"PortName changing: {SP.PortName}{Environment.NewLine}", "");
		}
		//_________________________________________________________________________
		public bool ChangePort ()
		{
			return ChangeParams (SetPort);
		}
		//_________________________________________________________________________
		private void SetBaud ()
		{
			SP.BaudRate = int.Parse (Own.CBBaudCOM.Text);
			OutMess ($"BaudRate changing: {SP.BaudRate}{Environment.NewLine}", "");
		}
		//_________________________________________________________________________
		public bool ChangeBaud ()
		{
			return ChangeParams (SetBaud);
		}
		//_________________________________________________________________________
		private void SetDataBits ()
		{
			SP.DataBits = (int)Own.NUDDataBits.Value;
			OutMess ($"DataBits changing: {SP.DataBits}{Environment.NewLine}", "");
		}
		//_________________________________________________________________________
		public bool ChangeDataBits ()
		{
			return ChangeParams (SetDataBits);
		}
		//_________________________________________________________________________
		private void SetStopBits ()
		{
			if (Own.CBStopBits.SelectedIndex > 0)
				SP.StopBits = (StopBits)Own.CBStopBits.SelectedIndex;
			OutMess ($"StopBits changing: {SP.StopBits}{Environment.NewLine}", "");
		}
		//_________________________________________________________________________
		public bool ChangeStopBits ()
		{
			return ChangeParams (SetStopBits);
		}
		//_________________________________________________________________________
		private void SetParity ()
		{
			SP.Parity = (Parity)Own.CBParity.SelectedIndex;
			OutMess ($"Parity changing: {SP.Parity}{Environment.NewLine}", "");
		}
		//_________________________________________________________________________
		public bool ChangeParity ()
		{
			return ChangeParams (SetParity);
		}
		//_________________________________________________________________________
		public bool PortOpen ()
		{
			try
			{
				if (SP.IsOpen == false)
				{
					SP.Open ();
					OutMess ("Порт [" + SP.PortName + "] открыт." + Environment.NewLine, "");//Own.TBListExchange.AppendText
				}
			}
			catch (Exception exc)
			{
				OutMess ($"Открытие СОМ-порта {SP.PortName}: {exc.Message}", "");
			}
			return SP.IsOpen;
		}
		//_________________________________________________________________________
		public void PortClose ()
		{
			if (SP != null)
			{
				SP.DtrEnable = false;
				if (SP.IsOpen)
				{
					SP.Close ();
					Own.TBListExchange.AppendText ("Порт [" + SP.PortName + "] закрыт.");
				}
			}
		}
	}
}
