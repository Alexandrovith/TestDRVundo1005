///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

///~~~~~~~~~	Проект:			Тест драйверов приборов
///~~~~~~~~~	Прибор:			Все приборы
///~~~~~~~~~	Модуль:			Накопление буфера полученного ответа по таймауту
///~~~~~~~~~	Разработка:	Демешкевич С.А.
///~~~~~~~~~	Дата:				08.08.2018

///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXONIM.CONSTS;
using System.Threading;
using System.Net.Sockets;

namespace TestDRVtransGas.TCPserver.TCPtoComPort
{
	/// <summary>
	/// Накопление буфера полученного ответа по таймауту
	/// </summary>
	public class CAccumRecieve
	{
		const int iIntervalTimeOut = 50;

		System.Timers.Timer TrIntervalTimeOut = new System.Timers.Timer (iIntervalTimeOut);

		byte[] btaBufToTCP = new byte[1024];
		/// <summary>
		/// Суммарный текущий размер полученных данных
		/// </summary>
		int iSizeData;

		Action<string, bool> Invoke_OutToWind;
		FTCPserver Own;

		//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

		public CAccumRecieve (object Owner, Action<string, bool> Invoke_OutToWind)
		{
			Own = Owner as FTCPserver;
			this.Invoke_OutToWind = Invoke_OutToWind;

			TrIntervalTimeOut.Elapsed += TrIntervalTimeOut_Elapsed;
			TrIntervalTimeOut.AutoReset = false;
		}
		//_________________________________________________________________________
		async private void TrIntervalTimeOut_Elapsed (object sender, System.Timers.ElapsedEventArgs e)
		{
			OutToWindByThread (btaBufToTCP, iSizeData);
			await Own.Stream?.WriteAsync (btaBufToTCP, 0, iSizeData);
			iSizeData = 0;
		}
		//_________________________________________________________________________
		/// <summary>
		/// Сигнал начала приёма ответа
		/// </summary>
		public void Reset()
		{
			iSizeData = 0;
		}
		//_________________________________________________________________________
		/// <summary>
		/// Возврат ответа в TCP канал
		/// </summary>
		/// <param name="oData">Буфер с ответом</param>
		/// <param name="e"></param>
		public void RecieveTCP_Com (object oData, int iSize)
		{
			try
			{
				byte[] btaBufTX = oData as byte[];
				if (btaBufTX != null)
				{
					TrIntervalTimeOut.Stop ();
					// Если короткий буфер
					if (iSizeData + iSize > btaBufToTCP.Length)
					{
						byte[] btaTemp = new byte[btaBufToTCP.Length * 2];
						Global.Copy (btaTemp, btaBufToTCP, iSizeData);
						btaBufToTCP = btaTemp;
					}
					
					Global.Append (btaBufTX, btaBufToTCP, iSizeData, iSize);
					iSizeData += iSize;
					TrIntervalTimeOut.Start ();
				}
			}
			catch (Exception exc)
			{
				Invoke_OutToWind ($"{exc.Message}{Environment.NewLine}{exc.StackTrace}", true);
			}
		}
		//_________________________________________________________________________
		void OutToWindByThread (byte[] btaBufTX, int iSize)
		{
			Task.Factory.StartNew (() =>
			{
				Invoke_OutToWind?.Invoke ($"DEV's: ({iSize}) {Global.ByteArToStr (btaBufTX, 0, iSize)}", true);
			}
					);
		}
	}
}
