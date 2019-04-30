using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;
using AXONIM.CONSTS;

namespace TestDRVtransGas.COMserver
{
	class COMPort
	{
		FCOMserver Own;
		const int SIZE_RX = 4096;
		const int SIZE_TX = 4096;

		SerialPort SP;
		public byte[] BufRX { get; private set; }

		//VVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVVV
		public COMPort (FCOMserver Owner)
		{
			Own = Owner;
			BufRX = new byte[SIZE_RX];
		}
		//_________________________________________________________________________
		public SerialPort OpenPort(string sPortName, string asBaud)
		{
			int iBaud;
			if (int.TryParse (asBaud, out iBaud))
			{
				return OpenPort (sPortName, iBaud, Parity.None, 8, 1);
			}
			Own.MessShow (string.Format ("Ошибка в скорости порта: [{0}]", asBaud));
			return null;
		}
		//_________________________________________________________________________
		public static string[] GetPortNames()
		{
			return SerialPort.GetPortNames ();
		}
		//_________________________________________________________________________
		public bool PortIsOpen()
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
				SP.Handshake = Handshake.None;
				SP.DataReceived += new SerialDataReceivedEventHandler (DataReceived);

				SP.ReadTimeout = (int)CONST.TIMEOUT.Read;
				SP.WriteTimeout = (int)CONST.TIMEOUT.Write;
				SP.ParityReplace = (byte)Parity.None;
				SP.ReadBufferSize = (int)SIZE_RX;
				SP.WriteBufferSize = (int)SIZE_TX;
				SP.ErrorReceived += new SerialErrorReceivedEventHandler (ErrorReceived);
				SP.Open ();
			}
			catch (Exception Exc)
			{
				MessageBox.Show (string.Format ("[{0}]: {1}. {2}", sPortName, Exc.Message, Exc.StackTrace));   // Запись информации об ошибке при открытии порта 
				SP = null;
			}
			return SP;
		}
		//_________________________________________________________________________
		private void ErrorReceived (object sender, SerialErrorReceivedEventArgs e)
		{
			MessageBox.Show (string.Format ("{0}. {1}", e.EventType.ToString (), e.ToString ()));
		}
		//_________________________________________________________________________
		private void DataReceived (object sender, SerialDataReceivedEventArgs e)
		{
			try
			{
				int iBytesToRead = SP.Read (BufRX, 0, BufRX.Length);    //((TConnCOMport)ParentVal.Прибор.DevSend).
																																//if (ErrData ()) return;
				Own.MessShow (Global.ByteArToStr (BufRX, 0, iBytesToRead));
				for (int i = 0; i < iBytesToRead; i++)
				{
					BufRX[i] = 0;
				}
			}
			catch (Exception exc)
			{
				MessageBox.Show (string.Format ("{0}. {1}:\n{2}. {3}", e.EventType.ToString (), e.ToString (), exc.Message, exc.StackTrace));
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
		public void ClosePort()
		{
			if (SP != null)
			{
				SP.Close ();
			}
		}
		//_________________________________________________________________________
		public string PortName()
		{
			if (SP != null)
			{
				return SP.PortName;
			}
			return "NONAME";
		}
	}
}
