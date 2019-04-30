///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

///~~~~~~~~~	Проект:			Тест драйверов приборов
///~~~~~~~~~	Прибор:			Все приборы
///~~~~~~~~~	Модуль:			Отправка данных в Comport и получение/выдача данных из оного
///~~~~~~~~~	Разработка:	Демешкевич С.А.
///~~~~~~~~~	Дата:				06.08.2018

///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using AXONIM.CONSTS;

namespace TestDRVtransGas.TCPserver.TCPtoComPort
{
	/// <summary>
	/// Передача данных TCP <-> ComPort 
	/// </summary>
	public class CTCP_Com : CComPort
	{
		public delegate void DRecieve (object oData, int iSize);

		public CTCP_Com (CTestExch Owner, DRecieve Recieve) :
			base (Owner)
		{
			if (PortCreate () == false)
			{
				return;
			}
			EvRecieve += Recieve;   // Данные непосредственно передаются от ComPort

		}
		//_________________________________________________________________________
		//private void Recieve (object sender, EventArgs e)
		//{
		//	EvRecieve?.Invoke (sender, e);
		//}
		//_________________________________________________________________________
		//public void Send (byte[] btaBufTX, int iLenData)
		//{
		//	try
		//	{
		//		/*ComPort.*/SendComm (btaBufTX, 0, iLenData);
		//	}
		//	catch (Exception exc)
		//	{
		//		OutMess (exc.Message + Environment.NewLine + exc.StackTrace, "");
		//		if (/*ComPort != null && ComPort.*/PortOpen ())
		//		{
		//			/*ComPort.*/PortClose ();
		//		}
		//	}
		//}
		//_________________________________________________________________________
		public override bool PortCreate ()
		{
			bool bRet = base.PortCreate ();
			if (bRet)
			{
				SP.ReadTimeout = 100;
				SP.WriteTimeout = 100;
				PortOpen ();
			}
			return bRet;
		}
		//_________________________________________________________________________

		object oDataRec = new object ();
		protected override void SP_DataReceived (object sender, SerialDataReceivedEventArgs e)
		{
			int iBytes = SP.Read (btaBufRX, 0, SP.BytesToRead);
			EvRecieve?.Invoke (btaBufRX, iBytes);
			SP.DtrEnable = false;
			//OutMess ($"[{SP.PortName}] Прибор: {iBytes}\t[{Global.ByteArToStr (btaBufRX, 0, iBytes)}]", ""/*Environment.NewLine*/);
		}
		//_________________________________________________________________________
		public override int GetReceivedBytesThreshold (byte[] btaBuf, int iStartPosData)
		{
			return 5;
		}
		//_________________________________________________________________________
		public override void BeforeRequest ()
		{
		}
		////_________________________________________________________________________
		//public void OutMess (string asMess, string asBeforeDT, bool bOutTime = true)
		//{
		//	Own.Invoke (Own.OutMess, asMess, asBeforeDT, bOutTime);
		//}
		////_________________________________________________________________________
		//internal void PortClose ()
		//{
		//	/*ComPort.*/PortClose ();
		//}
	}
}
