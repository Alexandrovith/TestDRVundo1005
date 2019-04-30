///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

///~~~~~~~~~	Проект:			Тест драйверов приборов
///~~~~~~~~~	Прибор:			Все приборы
///~~~~~~~~~	Модуль:			Передача данных из TCP/IP в ComPort
///~~~~~~~~~	Разработка:	Демешкевич С.А.
///~~~~~~~~~	Дата:				06.08.2018

///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDRVtransGas.TCPserver.TCPtoComPort
{
	public class CRMG_Pass : CTCPdevice
	{
		CTCP_Com TCP_Com;

		public CRMG_Pass (FTCPserver Prnt, CTCP_Com TCPCom) : base (Prnt)
		{
			TCP_Com = TCPCom;
			//TCP_Com = new CTCP_Com ((CTestExch)Prnt.Owner, Evrecieve);
		}
		//_________________________________________________________________________

		object oПолучОтвет = new object ();

		override public bool GetAnswer (ref byte[] btaTX, byte[] btaRX, ref int iNumPartOfAnswerBySend)
		{
			lock (oПолучОтвет)
			{
				btaTX = null;
				int iSize = GetSize (btaRX, 0x8d);
				if (iSize != int.MaxValue)
					TCP_Com.SendComm (btaRX, 0, iSize);
			}
			return false;
		}
		//_________________________________________________________________________
		int GetSize (byte[] btaBuf, byte btSym = 0)
		{
			int i = 0;
			for (; i < btaBuf.Length; i++)
			{
				if (btaBuf[i] == 0)
				{
					return i;
				}
				if (btaBuf[i] == btSym && btaBuf[i + 1] == 10)
				{
					return i + 2;
				}
			}
			return int.MaxValue;
		}
	}
}
