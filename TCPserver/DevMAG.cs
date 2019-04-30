///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

///~~~~~~~~~	Проект:			Слушатель порта TCP
///~~~~~~~~~	Прибор:			Все описанные приборы
///~~~~~~~~~	Модуль:			Эмулятор прибора МАГ (БАКС)
///~~~~~~~~~	Разработка:	Демешкевич С.А.
///~~~~~~~~~	Дата:				27.10.2016

///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

using System;
using System.Collections.Generic;
using System.Linq;
using AXONIM.CONSTS;
using AXONIM.BelTransGasDRV;
using AXONIM.ScanParamOfDevicves;

namespace TestDRVtransGas.TCPserver
{
	public class CDevMAG : CTCPdevice
	{
		//xxxxxxxxxxxxxxxxxxxxx    П О С Т О Я Н Н Ы Е      xxxxxxxxxxxxxxxxxxxxxxxxx

		const int iQuantRow = 2;
		const int iLenData = 40;
		const int iNumByte = iLenData*iQuantRow;

		//xxxxxxxxxxxxxxxxxxxxx    П Е Р Е М Е Н Н Ы Е      xxxxxxxxxxxxxxxxxxxxxxxxx
		
		int iNext = 1;
		double dCountDate = 0.0;
		int iCountComment = 1;

		//xxxxxxxxxxxxxxxxxxxxxxxx       К О Д        xxxxxxxxxxxxxxxxxxxxxxxxxxxxx
		public CDevMAG (FTCPserver Prnt) : base (Prnt)
		{
			iBeginData = (int)EResponseTCP.AddrSlave;
		}
		//_________________________________________________________________________
		override public bool GetAnswer (ref byte[] btaTX, byte[] btaRX)
		{
			btaBufTX = new byte[iBeginData + 6 + iNumByte + 2];
			btaTX = btaBufTX;
			//ushort usLenPacket = 6+iNumByte+2;
			//byte[] btaLenPacket = BitConverter.GetBytes(usLenPacket);
			//btaBufTX[4] = btaLenPacket[1];
			//btaBufTX[5] = btaLenPacket[0];

			btaBufTX[iBeginData + (int)TArchInterferMAG.EResponse.Addr] = 1;
			btaBufTX[iBeginData + (int)TArchInterferMAG.EResponse.Func] = 107;
			btaBufTX[iBeginData + (int)TArchInterferMAG.EResponse.QuantByteData] = 1 + 2 + iNumByte;
			btaBufTX[iBeginData + (int)TArchInterferMAG.EResponse.Next] = (byte)iNext;
			iNext = 1 - iNext;
			btaBufTX[iBeginData + (int)TArchInterferMAG.EResponse.QuantRowHi] = 2;
			btaBufTX[iBeginData + (int)TArchInterferMAG.EResponse.QuantRowLo] = 0;

			for (int iRow = 0; iRow < iQuantRow; iRow++)
			{
				int iStartData = iBeginData + (int)TArchInterferMAG.EResponse.Data + iRow * iLenData;
				double dDateE = 42600.123456 + dCountDate++;
				byte[] btaDateE = BitConverter.GetBytes(dDateE);
				foreach (var item in btaDateE)
				{
					btaBufTX[(int)TArchInterferMAG.EStructRow.DateE + iStartData++] = item;
				}

				btaBufTX[(int)TArchInterferMAG.EStructRow.CodeE + iStartData++] = 1;       // Код события
				btaBufTX[(int)TArchInterferMAG.EStructRow.TypeE + iStartData++] = 0;
				// Подтип события
				btaBufTX[(int)TArchInterferMAG.EStructRow.SubTypeE + iStartData++] = 2;
				btaBufTX[(int)TArchInterferMAG.EStructRow.SubTypeE + iStartData++] = 0;
				string asNameE = "Примечание " + iCountComment++;
				byte[] btaNameE = Global.CurrEncoding.GetBytes (asNameE);
				foreach (var item in btaNameE)
				{
					btaBufTX[iStartData++/* + (int)TArchInterferMAG.EStructRow.Comment*/] = item;
				}
			}
			return true;
		}
	}
}
