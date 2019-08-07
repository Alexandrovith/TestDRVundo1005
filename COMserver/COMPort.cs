///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

///~~~~~~~~~	Проект:			Тест драйверов приборов
///~~~~~~~~~	Прибор:			Все реализованные приборы
///~~~~~~~~~	Модуль:			Эмулятор прибора на Сом-порту. Настройка Сом-порта
///~~~~~~~~~	Разработка:	Демешкевич С.А.
///~~~~~~~~~	Дата:				04.09.2018

///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;
using AXONIM.CONSTS;
using System.Threading;

namespace TestDRVtransGas.COMserver
{
	class COMPort
	{
		FCOMserver.DMessageShow ShowMess;
		const int SIZE_RX = 4096;
		const int SIZE_TX = 4096;

		SerialPort SP;
		private object oRread = new object();

		public byte[] BufRX { get; private set; }

		public delegate byte[] DHandlingRecieve (byte[] BufRX, int iLenData);
		public event DHandlingRecieve EvHandlingRecieve = (byte[] Buf, int iLenData) => new byte[0];

		//VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV

		public COMPort (FCOMserver.DMessageShow ShowFunc, DHandlingRecieve HandlingRecieve)
		{
			ShowMess = ShowFunc; //Own = Owner;
			BufRX = new byte[SIZE_RX];
			if (HandlingRecieve != null)
				EvHandlingRecieve += HandlingRecieve;
		}
		//_________________________________________________________________________
		public void InitHandlingRecieve (DHandlingRecieve Handler)
		{
			EvHandlingRecieve = Handler;
		}
		//_________________________________________________________________________
		public SerialPort OpenPort (string sPortName, string asBaud)
		{
			int iBaud;
			if (int.TryParse (asBaud, out iBaud))
			{
				return OpenPort (sPortName, iBaud, Parity.None, 8, 1);
			}
			ShowMess ($"Ошибка в скорости порта: [{asBaud}]");
			return null;
		}
		//_________________________________________________________________________
		public bool PortIsOpen ()
		{
			if (SP == null)
				return false;
			return SP.IsOpen;
		}
		//_________________________________________________________________________
		/// <summary>
		/// Добавление нового СОМ-порта в список
		/// </summary>
		/// <param name="sPortName">Имя СОМ-порта</param>
		/// <param name="iBaud">Скорость обмена</param>
		/// <param name="Parity">Чётность</param>
		/// <param name="iDataBits">Количество бит данных</param>
		/// <param name="iStopBits">Количество стоп-бит</param>
		public SerialPort OpenPort (string sPortName, int iBaud, Parity Parity, int iDataBits, int iStopBits)
		{
			try
			{
				SP = new SerialPort (sPortName, iBaud, (Parity)Parity, iDataBits, (StopBits)iStopBits);
				//SP.Handshake = Handshake.None;

				SP.ReadTimeout = 100;
				//SP.WriteTimeout = (int)CONST.TIMEOUT.Write;
				SP.ParityReplace = (byte)Parity.None;
				SP.ReadBufferSize = SIZE_RX;
				SP.WriteBufferSize = SIZE_TX;
				//SP.ErrorReceived += new SerialErrorReceivedEventHandler (ErrorReceived);
				SP.DataReceived += new SerialDataReceivedEventHandler (DataReceived);
				SP.Open ();
			}
			catch (Exception Exc)
			{
				ShowMess ($"({sPortName}): {Exc.Message}{Environment.NewLine}{Exc.StackTrace}");   // Запись информации об ошибке при открытии порта 
				SP = null;
			}
			return SP;
		}
		//_________________________________________________________________________
		private void ErrorReceived (object sender, SerialErrorReceivedEventArgs e)
		{
			ShowMess ($"{e.EventType.ToString ()}. {e.ToString ()}");
		}
		//_________________________________________________________________________
		private void DataReceived (object sender, SerialDataReceivedEventArgs e)
		{
			try
			{
				lock (oRread)
				{
					Thread.Sleep (200);
					int iBytesToRead = SP.Read (BufRX, 0, BufRX.Length);  	//if (ErrData ()) return;
					ShowMess ("RX: " + Global.ByteArToStr (BufRX, 0, iBytesToRead));
					byte[] btaToSend = EvHandlingRecieve (BufRX, iBytesToRead);
					if (btaToSend.Length > 0)
					{
						SP.Write (btaToSend, 0, btaToSend.Length);
					}
					Array.Clear (BufRX, 0, iBytesToRead); 
				}
			}
			catch (Exception exc)
			{
				ShowMess ($"{e.EventType.ToString ()}. {e.ToString ()}:\n{exc.Message}\n{exc.StackTrace}");
			}
		}
		//_________________________________________________________________________
		/// <summary>
		/// Проверка контрольной суммы
		/// </summary>
		virtual public bool CRCisTrue (byte[] Arr, int LenByCRC, int iBeginBuf = 0)
		{
			return true;
			//ushort ui16 = Global.CRC (Arr, LenByCRC, SIZE_RX, TableCRC, InitCRC, iBeginBuf);
			//byte btCRC1 = Arr[iBeginBuf + LenByCRC],
			//		 btCRC2 = Arr[iBeginBuf + LenByCRC + 1];
			//return (btCRC2 == (ui16 >> 8)) && (btCRC1 == (ui16 & 0xFF));
		}
		//_________________________________________________________________________
		public void ClosePort ()
		{
			if (SP != null)
			{
				SP.Close ();
			}
		}
		//_________________________________________________________________________
		public string PortName ()
		{
			if (SP != null)
			{
				return SP.PortName;
			}
			return "NONAME";
		}
	}
}
