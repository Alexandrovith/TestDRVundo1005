///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

///~~~~~~~~~	Проект:			Слушатель порта TCP
///~~~~~~~~~	Прибор:			Все описанные приборы
///~~~~~~~~~	Модуль:			Эмулятор прибора ИРГА-2
///~~~~~~~~~	Разработка:	Демешкевич С.А.
///~~~~~~~~~	Дата:				13.12.2016

///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

//#define SET_VP_ZERO
//#define SET_VP_FF
//#define FillFFFF

using System;
using System.Collections.Generic;
using System.Linq;
using AXONIM.CONSTS;
using AXONIM.BelTransGasDRV;
using System.Threading;

namespace TestDRVtransGas.TCPserver
{
	public class CDevIRGA2 :CTCPdevice
	{
		//xxxxxxxxxxxxxxxxxxxxx    П О С Т О Я Н Н Ы Е      xxxxxxxxxxxxxxxxxxxxxxx		

		#region  //xxxxxxxxxxxxxxxxxxxxx    П Е Р Е М Е Н Н Ы Е      xxxxxxxxxxxxxxxxxxxxxxx

		byte btFlags = 0;
		byte btNS = (byte)'O';
		int iCanal = 1;
		#endregion

		//xxxxxxxxxxxxxxxxxxxxxxxx       К О Д        xxxxxxxxxxxxxxxxxxxxxxxxxxxxx
		public CDevIRGA2(FTCPserver Prnt) : base(Prnt)
		{
			iBeginData = 0;
			FormatsInterfer[0] = Format2;
			FormatsInterfer[1] = Format3;
			FormatsInterfer[2] = Format4;
			FormatsInterfer[3] = Format5;
			FormatsInterfer[4] = Format6;
			FormatsInterfer[5] = Format8;
			FormatsInterfer[6] = Format9;
		}
		//_________________________________________________________________________

		const float T_START = 260.0f;
		object oПолучОтвет = new object();
		int iCountErr = 0;
		float fValT = T_START;

		int iCount = 0;
		override public bool GetAnswer(ref byte[] btaTX, byte[] btaRX, ref int iNumPartOfAnswerBySend)
		{
			lock (oПолучОтвет)
			{
				int iByNeg = 0;
				byte Neg2 = (byte)(~iByNeg);
				iByNeg = 0x52;
				byte Neg52 = (byte)(~iByNeg);
				iByNeg = 4;
				byte Neg4 = (byte)(~iByNeg);

				DTCurr = DateTime.Now;
				int iSizeByCRC = 0;
				int iBeginBuf = 1;
				int iPos = 0;
				const byte btNeg1 = unchecked((byte)(~1));
				if (btaRX[0] == 0x6D)         // Чтение ФХП  старое 
				{
					btaBufTX = new byte[1];
					btaBufTX[0] = 0xC9;
					goto lbExit;
				}
				else if (btaRX[0] == Neg2 && btaRX[1] == 2 && btaRX[(int)CComIRGA2.EComWrFHP.WrParN] == Neg52
					&& btaRX[(int)CComIRGA2.EComWrFHP.WrPar] == 0x52 &&
					btaRX[(int)CComIRGA2.EComWrFHP.NbytesN] == Neg4 && btaRX[(int)CComIRGA2.EComWrFHP.Nbytes] == 4)
				{                                   // Запись ФХП 
					btaBufTX = new byte[1];
					btaBufTX[0] = (byte)'K';
					bWrData = true;
					Parent.OutToWind("Запись ФХП", false);
					goto lbExit;
				}
				//else if (btaRX[0] == 'R')     // Чтение ФХП старое 
				else if (btaRX[0] == 0xFE && btaRX[1] == 1 && btaRX[2] == 0xAD && btaRX[3] == 0x52 &&
					btaRX[7] == 20)
				//if (btaRX[1] == 0x1 && btaRX[3] == 0x52 && btaRX[5] == 0x17 && btaRX[7] == 0x14)
				{
					Parent.OutToWind("Чтение ФХП старое", false);
					FillФХП(ref iPos);
					goto lbExit;
					//iSizeByCRC = 23;				iBeginBuf = 0;
				}
				else if (btaRX[0] == 'W')     // Запись ФХП старая 
				{
					Parent.OutToWind("Запись ФХП старая", false);
					btaBufTX = new byte[1];
					btaBufTX[0] = 0x99;
					goto lbExit;
				}
				else if (btaRX[0] == 'S' && btaRX[1] == 'Y' && btaRX[2] == 'S')
				{
					Parent.OutToWind("SYS", false);
					FillArchDateFirst(ref iPos);
					goto lbExit;
				}
				else if (btaRX[(int)CArchIRGA2.EReqFlash.Sector] == 0 &&
					btaRX[(int)CArchIRGA2.EReqFlash.CodeCom1] == 1 && btaRX[(int)CArchIRGA2.EReqFlash.CodeCom1N] == btNeg1  //0xFE
								 && btaRX[(int)CArchIRGA2.EReqFlash.CodeComR_F] == 'F') // Flash 
				{
					Parent.OutToWind("Flash", false);
					iSizeByCRC = btaRX[(int)CArchIRGA2.EReqFlash.Nbytes];
					btaBufTX = new byte[iSizeByCRC + 2];
					Random Rn;
					btaBufTX[0] = 9;
					btaBufTX[1] = (byte)'C';
					for (int i = 2; i < iSizeByCRC; i++)
					{
						btaBufTX[i] = (byte)i;
					}
					iBeginBuf = 0;
					iPos = 2;
				}
				else if (btaRX[0] == 0xFE && btaRX[1] == 1 && btaRX[2] == 0xAD && btaRX[3] == 0x52)     // Время прибора 
				{
					Parent.OutToWind("Время прибора", false);
					FillDate(ref iPos, btaRX);
					iSizeByCRC = (int)(btaBufTX.Length - 2);
				}
				else if (btaRX[0] == 0xFE && btaRX[1] == 1 && btaRX[2] == 0xB9 && btaRX[3] == 0x46 && btaRX[(int)CArchIRGA2.EReqFlash.Sector] == 5)
				{
					Parent.OutToWind("Архив Interfer", false);// Архив Interfer
					FillArchInterfer(ref iPos, btaRX);
					iSizeByCRC = (int)(btaBufTX.Length - 2);
				}
				else if (btaRX[0] == 0xFE && btaRX[1] == 1 && btaRX[2] == 0xB9 && btaRX[3] == 0x46 && btaRX[11] == 8)//&& btaRX[4] == 0x43 && btaRX[5] == 0xBC)
				{                                                                       //VpE VcE
					Parent.OutToWind("VpE VcE", false);
					btaBufTX = new byte[10];
#if SET_VP_ZERO
						float fV = 0f;
						FillByFloat (fV, ref btaBufTX, ref iPos);
						FillByFloat (fV, ref btaBufTX, ref iPos);
#elif SET_VP_FF
						byte[] btaVal = { 0xFF, 0xFF, 0xFF, 0xFF, };
						FillByFloat (btaVal, ref btaBufTX, ref iPos);
						FillByFloat (btaVal, ref btaBufTX, ref iPos);
#else
					if (iCountErr < 0)
					{
						iCountErr++;
						//float fV = 0f;
						//FillByFloat (fV, ref btaBufTX, ref iPos);
						//FillByFloat (fV, ref btaBufTX, ref iPos);
						byte[] btaVal = { 0xFF, 0xFF, 0xFF, 0xFF, };
						FillByFloat(btaVal, ref btaBufTX, ref iPos);
						FillByFloat(btaVal, ref btaBufTX, ref iPos);
					}
					else
					{
						FillByFloat(fVal, ref btaBufTX, ref iPos);
						FillByFloat(fVal + 1, ref btaBufTX, ref iPos);
						if (++iCountErr >= 4)
							iCountErr = 0;
					}
#endif
					iBeginBuf = 0;
				}
				else if (btaRX[0] == 0xFE && btaRX[1] == 1 && btaRX[2] == 0xB9 && btaRX[3] == 0x46)   // Архив 
				{
					Parent.OutToWind("Архив", false);
					if ((iCount++ % 2) == 0)
					{
						FillArch(ref iPos, btaRX);
					}
					else
					{
						FillArchBad(ref iPos, btaRX);
					}
					iSizeByCRC = (int)(btaBufTX.Length - 2);
					iBeginBuf = 0;
				}
				else if (btaRX[0] == 0x6E)                                                // Текущие
				{
					Parent.OutToWind("Текущие", false);
					const ushort LEN_RESPONSE = 48;
					btaBufTX = new byte[LEN_RESPONSE];
					btaBufTX[iPos++] = 0xC9;
					byte[] bta = BitConverter.GetBytes(LEN_RESPONSE - 3);
					btaBufTX[iPos++] = bta[0];
					btaBufTX[iPos++] = bta[1];
					btaBufTX[iPos++] = (byte)'M';
					btaBufTX[iPos++] = (byte)iCanal;    // NumCanal (iCanal); // Канал №1 
					if (++iCanal == 2)
						iCanal = 0;
					btaBufTX[iPos++] = btNS;                // Штатный режим работы 
					btaBufTX[iPos++] = btFlags;
					//Random Rnd = new Random (DateTime.Now.Millisecond);
					//fVal = (float)(Rnd.NextDouble () * 4.0);
					if (bWrData)
					{
						bWrData = false;
						fVal = 0.6932f;
					}
					FillByFloat(fVal, ref btaBufTX, ref iPos);
					FillByFloat(fValT, ref btaBufTX, ref iPos);
					fValT += 0.1f;
					if (fValT >= 273.16f + 5.0)
						fValT = T_START;
					FillByFloat(fVal + 2.0f, ref btaBufTX, ref iPos);
					FillByFloat(fVal + 3.0f, ref btaBufTX, ref iPos);
					FillByFloat(fVal + 4.0f, ref btaBufTX, ref iPos);
					FillByFloat(fVal + 5.0f, ref btaBufTX, ref iPos);
					FillByFloat(fVal + 6.0f, ref btaBufTX, ref iPos);
					for (; iPos < LEN_RESPONSE - 2; iPos++)
					{
						btaBufTX[iPos] = (byte)iPos;
					}
					iBeginBuf = 1;
					iSizeByCRC = LEN_RESPONSE - 2;
					//for (int i = 0; i < 2000000; i++)
					//{
					//  Thread.Sleep (0);
					//}
				}
				else
				{
					Parent.OutToWind("UNKNOWN", false);
					btaBufTX = new byte[1];
					btaBufTX[0] = 0x55;
					bWrData = true;
					goto lbExit;
				}
				ushort usCRC = Global.CRCirga2(btaBufTX, iSizeByCRC, iSizeByCRC + 10, iBeginBuf);
				byte[] btaCRC = BitConverter.GetBytes(usCRC);
				btaBufTX[iPos++] = btaCRC[0];
				btaBufTX[iPos++] = btaCRC[1];
				lbExit:
				ChangeVal();
				//Thread.Sleep (12000);
				btaTX = btaBufTX;
				return false;
			}
		}
		//_________________________________________________________________________
		void Format2(ref int iPos)
		{
			btaBufTX[iPos++] = (byte)'P';
			btaBufTX[iPos++] = 0xFF;
			btaBufTX[iPos++] = (byte)'R';
			btaBufTX[iPos++] = IntToBCD(DTCurr.Minute);   // minute 
			btaBufTX[iPos++] = IntToBCD(DTCurr.Hour);    // hour 
			btaBufTX[iPos++] = 7;   // day 
			btaBufTX[iPos++] = 8;   // month
			btaBufTX[iPos++] = 0x16;    // year 
			FillByFloat(fVal, ref btaBufTX, ref iPos);  // new val 
			FillByFloat(fVal + 1, ref btaBufTX, ref iPos);  // old val 
		}
		//_________________________________________________________________________
		void Format3(ref int iPos)
		{
			btaBufTX[iPos++] = (byte)'P';
			btaBufTX[iPos++] = 16;  // 2-й канал 
			btaBufTX[iPos++] = 2;   // корр нуля
			btaBufTX[iPos++] = IntToBCD(DTCurr.Minute);   // minute 
			btaBufTX[iPos++] = IntToBCD(DTCurr.Hour);    // hour 
			btaBufTX[iPos++] = 7;   // day 
			btaBufTX[iPos++] = 8;   // month
			btaBufTX[iPos++] = 0x16;    // year 
			FillByFloat(fVal + 1, ref btaBufTX, ref iPos);  // new val 
			FillByFloat(fVal + 2, ref btaBufTX, ref iPos);  // old val 
		}
		//_________________________________________________________________________
		void Format4(ref int iPos)
		{
			btaBufTX[iPos++] = (byte)'P';
			btaBufTX[iPos++] = 0xFF;
			btaBufTX[iPos++] = (byte)'E';
			btaBufTX[iPos++] = IntToBCD(DTCurr.Minute);   // minute 
			btaBufTX[iPos++] = IntToBCD(DTCurr.Hour);    // hour 
			btaBufTX[iPos++] = 7;   // day 
			btaBufTX[iPos++] = 8;   // month
			btaBufTX[iPos++] = 0x16;   // year 
			iPos += 8;
		}
		//_________________________________________________________________________
		void Format5(ref int iPos)
		{
			btaBufTX[iPos++] = (byte)'D';
			btaBufTX[iPos++] = (byte)'T';
			iPos++;
			btaBufTX[iPos++] = IntToBCD(DTCurr.Minute);   // minute 
			btaBufTX[iPos++] = IntToBCD(DTCurr.Hour);   // hour 
			btaBufTX[iPos++] = 7;   // day 
			btaBufTX[iPos++] = 9;   // month
			btaBufTX[iPos++] = 0x16;   // year 
			iPos += 7;
			btaBufTX[iPos++] = 0xFF;
		}
		//_________________________________________________________________________
		void Format6(ref int iPos)
		{
			btaBufTX[iPos++] = (byte)'D';
			btaBufTX[iPos++] = (byte)'C';
			iPos++;
			btaBufTX[iPos++] = 9;   // minute 
			btaBufTX[iPos++] = 6;   // hour 
			btaBufTX[iPos++] = 7;   // day 
			btaBufTX[iPos++] = 8;   // month
			btaBufTX[iPos++] = 0x16;   // year 
			btaBufTX[iPos++] = IntToBCD(DTCurr.Minute);   // minute 
			btaBufTX[iPos++] = IntToBCD(DTCurr.Hour);    // hour 
			btaBufTX[iPos++] = 7;   // day 
			btaBufTX[iPos++] = 0x10;   // month
			btaBufTX[iPos++] = 0x16;   // year 
			iPos += 2;
			btaBufTX[iPos++] = 0xFF;
		}
		//_________________________________________________________________________
		void Format8(ref int iPos)
		{
			btaBufTX[iPos++] = (byte)'C';
			btaBufTX[iPos++] = 0x12;
			btaBufTX[iPos++] = 0x34;
			btaBufTX[iPos++] = IntToBCD(DTCurr.Minute);   // minute 
			btaBufTX[iPos++] = IntToBCD(DTCurr.Hour);   // hour 
			btaBufTX[iPos++] = 7;   // day 
			btaBufTX[iPos++] = 0x11;   // month
			btaBufTX[iPos++] = 0x16;   // year 
			FillByFloat(fVal + 3, ref btaBufTX, ref iPos);
			btaBufTX[iPos++] = (byte)'E';   // month
			iPos += 2;
			btaBufTX[iPos++] = 32;    // 3 canal 
		}
		//_________________________________________________________________________
		void Format9(ref int iPos)
		{
			btaBufTX[iPos++] = (byte)'H';
			iPos++;
			btaBufTX[iPos++] = (byte)'E';
			btaBufTX[iPos++] = IntToBCD(DTCurr.Minute);     // minute 
			btaBufTX[iPos++] = IntToBCD(DTCurr.Hour);    // hour 
			btaBufTX[iPos++] = 3;   // day 
			btaBufTX[iPos++] = 0x12;   // month
			btaBufTX[iPos++] = 0x16;   // year 
			iPos += 6;
			btaBufTX[iPos++] = 48;  // 4 canal 
		}
		//_________________________________________________________________________

		delegate void DFomatsInterfer(ref int iPos);
		DFomatsInterfer[] FormatsInterfer = new DFomatsInterfer[(int)CArchInterferIRGA2.EFormat.SIZE];
		private bool bWrData;

		private void FillArchInterfer(ref int iPos, byte[] btaRX)
		{
			int iQuantByte = btaRX[11] + 2;
			if (iQuantByte == 2)
				iQuantByte = 258;
			int iQuantRow = iQuantByte / (int)CArchHourDayIRGA2.ERow.SIZE;
			btaBufTX = new byte[iQuantByte];
			CArchInterferIRGA2.EFormat Format = CArchInterferIRGA2.EFormat.Num2;
			for (int iRow = 0; iRow < iQuantRow; iRow++, Format++)
			{
				if (Format == CArchInterferIRGA2.EFormat.SIZE)
					Format = CArchInterferIRGA2.EFormat.Num2;
				FormatsInterfer[(int)Format](ref iPos);
			}
		}
		//_________________________________________________________________________
		private void FillArch(ref int iPos, byte[] btaRX)
		{
			int iQuantByte = btaRX[11] + 2;
			if (iQuantByte == 2)
				iQuantByte = 258;
			int iQuantRow = iQuantByte / (int)CArchHourDayIRGA2.ERow.SIZE;
			btaBufTX = new byte[iQuantByte];
			for (int iRow = 0; iRow < iQuantRow; iRow++)
			{
#if FillFFFF
				FillByVal (0xff, ref btaBufTX, ref iPos);
				FillByShort (-1, ref btaBufTX, ref iPos);
				FillByVal (0xff, ref btaBufTX, ref iPos);
				FillByVal (0xff, ref btaBufTX, ref iPos);
				iPos += 6;
				FillByShort (-1, ref btaBufTX, ref iPos);
				FillByShort (-1, ref btaBufTX, ref iPos);
				FillByShort (-1, ref btaBufTX, ref iPos);
#else
				FillByFloat(fVal, ref btaBufTX, ref iPos);
				FillByShort(sVal, ref btaBufTX, ref iPos);
				FillByFloat(fVal + 1, ref btaBufTX, ref iPos);
				FillByFloat(fVal + 2, ref btaBufTX, ref iPos);
				iPos += 6;
				FillByShort((short)(sVal + 1), ref btaBufTX, ref iPos);
				FillByShort((short)(sVal + 2), ref btaBufTX, ref iPos);
				FillByShort((short)(sVal + 3), ref btaBufTX, ref iPos);
#endif
				fVal += 1;
				sVal += 1;
			}
		}
		//_________________________________________________________________________
		private void FillArchBad(ref int iPos, byte[] btaRX)
		{
			sVal = 24 * 32 * 10;
			byte[] btaFF = { 255, 255, 255, 255 };
			byte[] btaFloatErr = { 255, 5, 255, 9 };

			int iQuantByte = btaRX[11] + 2;
			if (iQuantByte == 2)
				iQuantByte = 258;
			int iQuantRow = iQuantByte / (int)CArchHourDayIRGA2.ERow.SIZE;
			btaBufTX = new byte[iQuantByte];
			for (int iRow = 0; iRow < iQuantRow; iRow++)
			{
				FillByFloat(btaFF, ref btaBufTX, ref iPos);
				FillByShort(sVal, ref btaBufTX, ref iPos);
				FillByFloat(btaFloatErr, ref btaBufTX, ref iPos);
				FillByFloat(btaFF, ref btaBufTX, ref iPos);
				iPos += 6;
				FillByShort((short)(sVal + 1), ref btaBufTX, ref iPos);
				FillByShort((short)(sVal + 2), ref btaBufTX, ref iPos);
				FillByShort((short)(sVal + 3), ref btaBufTX, ref iPos);
			}
		}
		//_________________________________________________________________________
		private void FillDate(ref int iPos, byte[] btaRX)
		{
			int iLenData = btaRX[7];
			if (iLenData == 0)
				iLenData = 256;
			btaBufTX = new byte[iLenData + 2];
			DateTime DT = DateTime.Now;
			btaBufTX[iPos++] = IntToBCD(DT.Second);
			btaBufTX[iPos++] = 0;
			btaBufTX[iPos++] = IntToBCD(DT.Minute);
			btaBufTX[iPos++] = 0;
			btaBufTX[iPos++] = IntToBCD(DT.Hour);
			btaBufTX[iPos++] = 0;
			btaBufTX[iPos++] = 0;
			btaBufTX[iPos++] = IntToBCD(DT.Day);
			btaBufTX[iPos++] = IntToBCD(DT.Month);
			btaBufTX[iPos++] = IntToBCD(DT.Year - 2000);
			for (; iPos < iLenData; iPos++)
			{
				btaBufTX[iPos] = (byte)iPos;
			}
		}
		//_________________________________________________________________________
		private void FillArchDateFirst(ref int iPos)
		{
			btaBufTX = new byte[17];
			btaBufTX[iPos++] = 0x53;
			btaBufTX[iPos++] = 0x88;
			btaBufTX[iPos++] = 0xE0;
			btaBufTX[iPos++] = 0xA3;
			btaBufTX[iPos++] = 0xA0;
			btaBufTX[iPos++] = 0x2D;
			btaBufTX[iPos++] = 0x32;
			btaBufTX[iPos++] = 0x0;
			btaBufTX[iPos++] = 0x0;
			btaBufTX[iPos++] = 0x33;
			btaBufTX[iPos++] = 0x38;
			btaBufTX[iPos++] = 0x31;
			btaBufTX[iPos++] = 0x36;
			btaBufTX[iPos++] = 0x0;
			btaBufTX[iPos++] = 0x0;
			btaBufTX[iPos++] = 0x0;
			btaBufTX[iPos++] = 0x0;
		}
		//_________________________________________________________________________
		private void FillФХП(ref int iPos)
		{
			const int QUANT_BITE = 22;
			btaBufTX = new byte[QUANT_BITE];
			for (int i = 0; i < QUANT_BITE / 4; i++)
			{
				if (bWrData)
					FillByFloat(fVal++, ref btaBufTX, ref iPos);
			}
			iPos += 2;
			//btaBufTX = new byte[TBufCommIRGA2.LEN_DATA_FHP];
			//   for (int i = 0; i < (int)TBufCommIRGA2.LEN_DATA_FHP / 4; i++)
			//   {
			//  FillByFloat (fVal + i, ref btaBufTX, ref iPos);
			//   }
			//ushort usSizeData = 23;
			//btaBufTX = new byte[usSizeData + 2];
			//byte[] bta = BitConverter.GetBytes (usSizeData);
			//btaBufTX[iPos++] = bta[0];
			//btaBufTX[iPos++] = bta[1];
			//FillByFloat (fVal, ref btaBufTX, ref iPos);
			//FillByFloat (fVal + 1, ref btaBufTX, ref iPos);
			//FillByFloat (fVal + 2, ref btaBufTX, ref iPos);
			//FillByFloat (fVal + 3, ref btaBufTX, ref iPos);
			//DateTime DT = DateTime.Now;
			//btaBufTX[iPos++] = (byte)DT.Minute;
			//btaBufTX[iPos++] = (byte)DT.Hour;
			//btaBufTX[iPos++] = (byte)DT.Day;
			//btaBufTX[iPos++] = (byte)DT.Month;
			//btaBufTX[iPos++] = (byte)(DT.Year - 2000);
		}
		//_________________________________________________________________________
		void FillByVal(byte btVal, ref byte[] btaTo, ref int iPos)
		{
			byte[] bta = new byte[4];
			btaBufTX[iPos++] = btVal;
			btaBufTX[iPos++] = btVal;
			btaBufTX[iPos++] = btVal;
			btaBufTX[iPos++] = btVal;
			//btaBufTX[iPos++] = bta[3];
			//btaBufTX[iPos++] = bta[2];
			//btaBufTX[iPos++] = bta[1];
			//btaBufTX[iPos++] = bta[0];
		}
		//_________________________________________________________________________
		void FillByFloat(float fVal, ref byte[] btaTo, ref int iPos)
		{
			byte[] bta = BitConverter.GetBytes(fVal);
			btaBufTX[iPos++] = bta[0];
			btaBufTX[iPos++] = bta[1];
			btaBufTX[iPos++] = bta[2];
			btaBufTX[iPos++] = bta[3];
			//btaBufTX[iPos++] = bta[3];
			//btaBufTX[iPos++] = bta[2];
			//btaBufTX[iPos++] = bta[1];
			//btaBufTX[iPos++] = bta[0];
		}
		//_________________________________________________________________________
		void FillByFloat(byte[] ValFloatByBad, ref byte[] btaTo, ref int iPos)
		{
			btaBufTX[iPos++] = ValFloatByBad[0];
			btaBufTX[iPos++] = ValFloatByBad[1];
			btaBufTX[iPos++] = ValFloatByBad[2];
			btaBufTX[iPos++] = ValFloatByBad[3];
		}
		//_________________________________________________________________________
		byte NumCanal(int iCanal)
		{
			return (byte)(iCanal * 16 - 1);
		}
		//_________________________________________________________________________
		public override void CreateBufTX(byte[] btaRX)
		{
			btaBufTX = new byte[258];
		}
	}
}
