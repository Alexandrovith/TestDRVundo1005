using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace TestDRVtransGas.TCPtoTCP
{
	public class CTCPclient
	{
		const int SIZE_BUF = 4096;
		public const int DEF_READ_TIMEOUT = 300;

		TcpClient Client;

		bool bWasClose;
		/// Буфер для хранения принятого массива bytes.
		public byte[] BufRX { get; protected set; }

		private int iReadTimeout = DEF_READ_TIMEOUT;
		public int ReadTimeout
		{
			get => iReadTimeout;
			set => iReadTimeout = (value > 3 && value < 30000) ? value : DEF_READ_TIMEOUT;
		}
		public NetworkStream Stream { get; protected set; }

		//wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww

		public CTCPclient (int iSizeRX = SIZE_BUF)
		{
			BufRX = new Byte[SIZE_BUF];
		}
		//_________________________________________________________________________
		~CTCPclient ()
		{
			if (bWasClose == false)
				Close ();
		}
		//_________________________________________________________________________
		public bool Connect (string asIP, int iPort)
		{
			try
			{
				if (Client == null)
				{
					if (Stream != null)
					{
						Stream.Close ();
						Stream = null;
					}
					Client = new TcpClient (asIP, iPort);
				}
				if (Client != null)
				{
					if (Client.Connected == false)
					{
						if (Stream != null)
						{
							Stream.Close ();
							Stream = null;
						}
						Client.Close ();
						Client = new TcpClient (asIP, iPort);
					}

					Stream = Client.GetStream ();
					Stream.ReadTimeout = ReadTimeout;
					return Client.Connected;
				}
			}
			catch /*(Exception exc)*/	{ }
			return false;
		}
		//_________________________________________________________________________

		AsyncCallback TheRecieveData;

		public void SendAsync (byte[] data, int iLenData, AsyncCallback RecieveData, int iOffset = 0)
		{
			try
			{
				if (Client != null && Client.Connected && Stream.CanWrite)
				{
					TheRecieveData = RecieveData;
					Stream.BeginWrite (data, iOffset, iLenData, RecieveAsync, this);      // Отправляем сообщение нашему серверу.
				}
			}
			catch 	{		}
		}
		//_________________________________________________________________________
		public byte[] Recieve ()
		{
			// Читаем первый пакет ответа сервера. 
			// Можно читать всё сообщение.
			// Для этого надо организовать чтение в цикле как на сервере.
			Int32 bytes = Stream.Read (BufRX, 0, BufRX.Length);
			//String responseData = System.Text.Encoding.ASCII.GetString (data, 0, bytes);
			//GetRecieve?.Invoke (btaBufRX, bytes);
			return BufRX;
		}
		//_________________________________________________________________________
		public void RecieveAsync (IAsyncResult ar)
		{
			if (Stream != null)
			{
				Stream.EndWrite (ar);
				if (Stream.CanRead)
					Stream.BeginRead (BufRX, 0, BufRX.Length, TheRecieveData, this);
			}
		}
		//_________________________________________________________________________
		public void Close ()
		{
			bWasClose = true;
			// Закрываем всё.
			Stream?.Close ();
			Client?.Close ();
		}
	}
}

