///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

///~~~~~~~~~	Проект:			Тест драйверов приборов
///~~~~~~~~~	Прибор:			Все приборы
///~~~~~~~~~	Модуль:			Получение данных по TCP/IP
///~~~~~~~~~	Разработка:	Демешкевич С.А.
///~~~~~~~~~	Дата:				22.10.2018

///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace TestDRVtransGas.TCPtoTCP
{
	public class CTCPlistener
	{
		IPAddress LocalAddr;
		int iPort;
		TcpListener Listener;
		TcpClient Client;
		NetworkStream Stream;
		volatile private bool bLoop = true;
		public delegate void DExch (object Buf, object SizeBuf);
		public event DExch EvGetData;

		const int SIZE_BUF = 4096;
		Byte[] btaRXcurr = new Byte[SIZE_BUF];
		Byte[] btaRX = new Byte[SIZE_BUF];
		CTCPtoAny Владелец; //CTCPtoTCP
		public bool IsConnect { get; private set; }

		//wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww

		public CTCPlistener (string asIP, int iPort, ref DExch EvSendAnswer, /*CTCPtoTCP*/ CTCPtoAny Own)
		{
			Владелец = Own;
			this.iPort = iPort;
			LocalAddr = IPAddress.Parse (asIP);
			SetThread ();

			EvSendAnswer += SendAnswerToServer;
			try
			{
				Listener = new TcpListener (LocalAddr, iPort);
			}
			catch (Exception exc)
			{
				OutMess ($"Соединение с {asIP}:{iPort}: {exc.Message}");
				return;
			}
			IsConnect = true;
			ThreadPool.QueueUserWorkItem (Start, iPort);
			/*Task.Factory.StartNew (() => Start ());*/ // Запускаем TcpListener и начинаем слушать клиентов.
		}
		//_________________________________________________________________________
		public void SetThread (int iNumThreadByProcessor = 4, int iMinThreads = 2)
		{
			int MaxThreadsCount = Environment.ProcessorCount * iNumThreadByProcessor;// Определим нужное максимальное количество потоков	
			ThreadPool.SetMaxThreads (MaxThreadsCount, MaxThreadsCount);  // Установим максимальное количество рабочих потоков		
			ThreadPool.SetMinThreads (iMinThreads, iMinThreads);          // Установим минимальное количество рабочих потоков
		}
		//_________________________________________________________________________
		public void Start (object iPort)
		{
			try
			{
				Listener.Start ();
				// Принимаем клиентов в бесконечном цикле.
				//OutMess ($"Подкл. {LocalAddr.ToString ()}:{iPort}");
				while (bLoop)
				{
					// При появлении клиента добавляем в очередь потоков его обработку.
					ThreadPool.QueueUserWorkItem (ObrabotkaZaprosa, Listener.AcceptTcpClient ());
				}
			}
			catch (Exception exc)
			{
				OutMess ($"{exc.Message}{Environment.NewLine}{exc.StackTrace}");
			}
			finally
			{
				Listener?.Stop ();
			}
		}
		//_________________________________________________________________________

		int iLenRX;
		int iLenRXfull = 0;

		private void ObrabotkaZaprosa (object oClient)
		{
			//Можно раскомментировать Thread.Sleep(1000); 			//Запустить несколько клиентов и наглядно увидеть как они обрабатываются в очереди. 

			try
			{
				Client = oClient as TcpClient;
				if (Client == null)
					return;

				Stream = Client.GetStream ();     // Получаем информацию от клиента
																					//Stream.ReadTimeout = 300;
				iLenRXfull = 0;
				// Принимаем данные от клиента в цикле, пока не дойдём до конца.
				while ((iLenRX = Stream.Read (btaRXcurr, iLenRXfull, SIZE_BUF - iLenRXfull)) != 0)
				{
					EvGetData?.Invoke (btaRXcurr, iLenRX);
				} //while (Stream.DataAvailable);
			}
			catch { }
			//	// Преобразуем данные в ASCII string.				data = System.Text.Encoding.ASCII.GetString (bytes, 0, i)
			//	// Преобразуем полученную строку в массив Байт.			byte[] msg = System.Text.Encoding.ASCII.GetBytes (data);	
			OutMess ("Exit ObrabotkaZaprosa");
		}
		//_________________________________________________________________________
		/// <summary>
		/// Отправка ответа серверу 
		/// </summary>
		private void SendAnswerToServer (object Buf, object SizeBuf)
		{
			if (Client != null)
			{
				if (Stream != null)
				{
					Stream.BeginWrite ((byte[])Buf, 0, (int)SizeBuf, WrCompleted, Stream);//.Write ((byte[])Buf, 0, (int)SizeBuf);
				}
			}
		}
		//_________________________________________________________________________
		private void WrCompleted (IAsyncResult ar)
		{
			var Str = ar as NetworkStream;
			if (Str != null)
			{
				Str.EndWrite (ar);
			}
		}
		//_________________________________________________________________________
		public void OutMess (string asMess)
		{
			try
			{
				Владелец.Invoke (Владелец.OutMess, asMess);
			}
			catch { }
		}
		//_________________________________________________________________________
		public void Close ()
		{
			bLoop = false;
			Listener?.Stop ();
			Stream?.Close ();
			Client?.Close ();
			Client = null;
			//if (Client != null)
			//{
			//	//var Strm = Client.GetStream ();
			//	Stream?.Close ();
			//	Client?.Close ();
			//	Client = null;
			//}
		}

	}
}
