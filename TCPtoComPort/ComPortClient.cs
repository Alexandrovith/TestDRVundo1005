///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

///~~~~~~~~~	Проект:			Тест драйверов приборов
///~~~~~~~~~	Прибор:			Все приборы
///~~~~~~~~~	Модуль:			Передача данных через компорт прибору
///~~~~~~~~~	Разработка:	Демешкевич С.А.
///~~~~~~~~~	Дата:				13.12.2018

///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO.Ports;
using System.Threading;

namespace TestDRVtransGas.TCPtoComPort
{
	public class CComPortClient
	{
		const int SIZE_BUF = 4096 * 2;
		public const int DEF_READ_TIMEOUT = 300;

		SerialPort SP;
		string asPort;
		int iBaud;
		Parity Parity;
		int iDataBit;
		StopBits StopBits;

		bool bWasClose;
		/// Буфер для хранения принятого массива bytes.
		public byte[] BufRX { get; protected set; }
		public int ReadTimeout
		{
			get => SP.ReadTimeout;
			set => SP.ReadTimeout = (value > 3 && value < 30000) ? value : DEF_READ_TIMEOUT;
		}
		public delegate void DGetBuf (byte[] btaBuf, int iLen);
		DGetBuf TheRecieveData;
		public event COutLog.DOutLog Log;

		//wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww

		public CComPortClient (string asPort, int iBaud, Parity Parity, int iDataBit, StopBits StopBits, int iSizeRX = SIZE_BUF)
		{
			this.asPort = asPort;
			this.iBaud = iBaud;
			this.Parity = Parity;
			this.iDataBit = iDataBit;
			this.StopBits = StopBits;
			BufRX = new byte[iSizeRX];
			
		}
		//_________________________________________________________________________
		/// <summary>
		/// Получили ответ от прибора
		/// </summary>
		private void Client_DataReceived (object sender, SerialDataReceivedEventArgs e)
		{
			int iCount = SP.Read (BufRX, 0, SP.ReadBufferSize);// SP.BytesToRead);
			TheRecieveData (BufRX, iCount);
		}
		//_________________________________________________________________________
		~CComPortClient ()
		{
			if (bWasClose == false)
				Close ();
		}
		//_________________________________________________________________________
		public bool Connect ()
		{
			try
			{
				if (SP == null)
				{
					SP = new SerialPort (asPort, iBaud, Parity, iDataBit, StopBits);
					if (SP != null)
					{
						SP.DataReceived += Client_DataReceived;
						SP.ReadTimeout = ReadTimeout;
						SP.ReadBufferSize = SIZE_BUF;
						SP.ErrorReceived += SP_ErrorReceived;
					}
				}
				if (SP != null && SP.IsOpen == false)
					SP.Open ();
				return SP != null;
			}
			catch (Exception exc) { Log?.Invoke ($"({asPort}) {exc.Message}{Environment.NewLine}{exc.StackTrace}"); }
			return false;
		}
		//_________________________________________________________________________
		private void SP_ErrorReceived (object sender, SerialErrorReceivedEventArgs e)
		{
			Log?.Invoke ($"({asPort}) Error: {e.EventType}");
		}
		//_________________________________________________________________________
		/// <summary>
		/// Отправляем сообщение нашему серверу.
		/// </summary>
		/// <param name="btaData"></param>
		/// <param name="iLenData"></param>
		/// <param name="RecieveData"></param>
		/// <param name="iOffset"></param>
		public void SendAsync (byte[] btaData, int iLenData, DGetBuf RecieveData, int iOffset = 0)
		{
			try
			{
				if (SP == null)
				{
					Log?.Invoke ($"({asPort}) Клиент не создан");
				}
				else if (SP.IsOpen)
				{
					TheRecieveData = RecieveData;
					SP.Write (btaData, iOffset, iLenData); //Stream.BeginWrite (data, iOffset, iLenData, RecieveAsync, this);
				}
				else { Log?.Invoke ($"({asPort}) Клиент не открыт"); }
			}
			catch { }
		}
		//_________________________________________________________________________
		public void Close ()
		{
			bWasClose = true;
			// Закрываем всё.
			//Stream?.Close ();
			if (SP != null)
			{
				SP.Close ();
				SP = null;
			}
		}
	}
}

