///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

///~~~~~~~~~	Проект:			Слушатель порта TCP
///~~~~~~~~~	Прибор:			UFG_F
///~~~~~~~~~	Модуль:			Эмулятор прибора UFG_F
///~~~~~~~~~	Разработка:	Демешкевич С.А.
///~~~~~~~~~	Дата:				11.05.2017

///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

using System;
using System.Collections.Generic;
using System.Linq;
using AXONIM.CONSTS;
using AXONIM.ScanParamOfDevicves;
using AXONIM.BelTransGasDRV;
using System.Threading;
using System.Collections;

namespace TestDRVtransGas.TCPserver
{
	public class CDevUFG_F : CTCPdevice
	{
		//xxxxxxxxxxxxxxxxxxxxx    П О С Т О Я Н Н Ы Е      xxxxxxxxxxxxxxxxxxxxxxx

		byte[] btaRequestFactory = { 2, 0x11, 0x45, 0x57, 0xF2, 0x4F, 0x1E, 0x1, 0x44, 0x1, 0x0, 0x0, 0x0, 0x10, 0x1, 0xA, 0x1, 0x0, 0xB4, 0x56, 0x58, 0x8F, 0x62, 0x70, 0x74, 0x66, 0x67, 0x5F, 0x76, 0x34, 0x5F, 0x31, 0x20, 0x31, 0x32, 0x30, 0x37, 0x32, 0x30, 0x31, 0x32, 0x0, 0x39, 0x30, 0x30, 0x37, 0x32, 0x0, 0x0, 0x1, 0x0, 0x3, 0x0, 0xC, 0x0, 0x12, 0x9F, 0xD, 0x0, 0x0, 0x0, 0x0, 0x7, 0xE3, 0x1, 0x3, 0xF, 0x6, 0x30, 0x4, 0x2, 0x67, 0x20 };

		//xxxxxxxxxxxxxxxxxxxxx    П Е Р Е М Е Н Н Ы Е      xxxxxxxxxxxxxxxxxxxxxxx

		//xxxxxxxxxxxxxxxxxxxxxxxx       К О Д        xxxxxxxxxxxxxxxxxxxxxxxxxxxxx
		public CDevUFG_F (FTCPserver Prnt) : base (Prnt)
		{
			iBeginData = 0;
			uiDateArchBeg = Global.DateTimeAsInt (new DateTime (2019, 5, 29, 5, 0, 0));
		}
		//_________________________________________________________________________

		object oПолучОтвет = new object ();
		int iNumEw = 0;
		private int iQuantEvAlarm = oRows.Length;
		//2 11 45 57 F2 4F 1E 1 44 1 0 0 0 10 1 A 1 0 B4 56 58 8F 62 70 74 66 67 5F 76 34 5F 31 20 31 32 30 37 32 30 31 32 0 39 30 30 37 32 0 0 1 0 3 0 C 0 12 9F D 0 0 0 0 7 E3 1 3 F 6 30 4 2 67 20
		override public bool GetAnswer (ref byte[] btaTX, byte[] btaRX, ref int iNumPartOfAnswerBySend)
		{
			lock (oПолучОтвет)
			{
				DTCurr = DateTime.Now;
				int iSizeByCRC = 0;
				int iBeginBuf = 0;
				iPosTX = (int)MODBUS3_ANSW.Data;

				ushort usAddrReg = Global.ToUInt16rev (btaRX, (int)MODBUS3_RESP.RegStartH);
				if (usAddrReg == 0x2003 && btaRX[(int)MODBUS3_RESP.Func] == 3)					// Фармавання архiваў H/D 
				{
					iPosTX = (int)CArchVympel.EAnswer.Data;
					int iArch = btaRX[(int)CArchVympel.ERequest.IDarch + 1];
					int iQuantRow = (Global.ToUInt16rev (btaRX, (int)CArchVympel.ERequest.QuantRegRd) - 3) /
														CArchVympel.iaQuantRegInRec[iArch];
					int iQuantBytes = iQuantRow * CArchVympel.iaQuantRegInRec[iArch] * 2;
					btaTX = new byte[(int)CArchVympel.EAnswer.Data + iQuantBytes + 2];    // Добавлять 3 регистра? 
					btaTX[(int)CArchVympel.EAnswer.QuantBitesRd] = (byte)(iQuantBytes + 6);

					FillArch (ref iPosTX, btaRX, ref btaTX, iQuantRow);
					iSizeByCRC = btaTX.Length - 2;
				}
				else if (btaRX[(int)MODBUS3_RESP.Func] == 3 && (usAddrReg >= 0x2200 && usAddrReg <= 0x220E))	// Аварый  
				{
					//int iRegStart = Global.ToUInt16rev (btaRX, (int)MODBUS3_RESP.RegStartH);
					if (usAddrReg == 0x2201)                      // Чтение кол-ва записей 
					{
						btaTX = new byte[(int)MODBUS3_ANSW.Data + 4];
						btaTX[iPosTX] = 0;
						btaTX[++iPosTX] = (byte)iQuantEvAlarm;
						++iPosTX;
					}
					else if (usAddrReg == 0x2202)                 // Запрос сведений 
					{
						FillAlarm (btaRX, ref btaTX);
						iPosTX = btaTX.Length - 2;
						btaTX[(int)MODBUS3_ANSW.NumByteData] = (byte)(btaTX.Length - (int)MODBUS3_ANSW.Data - 2);
					}
					else
					{

					}
					iSizeByCRC = btaTX.Length - 2;
				}
				else																								// Фармавання дыскрэтных параметраў 
				{
					if (btaRX[(int)MODBUS3_RESP.Func] == 16)					// Запіс параметраў 
					{
						Parent.OutToWind ("Запіс параметраў", false);
						btaTX = new byte[(int)MODBUS16_ANSW.SIZE];
						iSizeByCRC = btaTX.Length - 2;
						iPosTX = (int)MODBUS16_ANSW.CRCh;
						Parent.OutToWind ("ЗАПІС: " + Global.ByteArToStr (btaRX));
          }
					else											// Чтение мгновенных 
					{
						Parent.OutToWind ("Имгненныя", false);
						ushort usQuantityOfRegisters = Global.ToUInt16rev (btaRX, (int)MODBUS3_RESP.NumRegH);
						int iNumByteData = usQuantityOfRegisters * 2;
						btaTX = new byte[iPosTX + iNumByteData + 2];
						btaTX[(int)MODBUS3_ANSW.NumByteData] = (byte)iNumByteData;
						iSizeByCRC = btaTX.Length - 2;

						if (btaRX[(int)MODBUS3_RESP.Func] == 3)
							FillHoldingReg (btaRX, ref btaTX, usQuantityOfRegisters);
						else if (btaRX[(int)MODBUS3_RESP.Func] == 4)
						{
							FillInputReg (ref iPosTX, btaRX, ref btaTX, usQuantityOfRegisters);
						}
						else if (btaRX[(int)MODBUS3_RESP.Func] == 0x11)
						{
							Parent.OutToWind ("Factory");
							btaTX = btaRequestFactory;
							return false;
						}
					}
				}
				if (btaTX.Length < (int)MODBUS3_ANSW.Func)					// Если сбой заполнения данными 
				{
					btaTX = Global.EncodingCurr.GetBytes ("Сбой заполнения данными");
					Parent.OutToWind ("СБОЙ ЗАПОЛНЕНИЯ ДАННЫМИ");
          return false;
				}
				for (int i = 0; i <= (int)MODBUS3_ANSW.Func; i++)
				{
					btaTX[i] = btaRX[i];
				}
				ushort usCRC = Global.CRC (btaTX, iSizeByCRC, btaTX.Length, Global.Table8005, 0xFFFF, (ushort)iBeginBuf);
				byte[] btaCRC = BitConverter.GetBytes (usCRC);
				btaTX[iPosTX++] = btaCRC[0];
				btaTX[iPosTX++] = btaCRC[1];
				
				ChangeVal ();
				//Thread.Sleep (12000);
				return false;
			}
		}
		//_________________________________________________________________________
		class TDateTime
		{
			public byte[] Val = new byte[8];
			public TDateTime(int ms, int cc, int mm, int hh, ushort YYYY, int MM, int DD)
			{
				int i = 0;
				Val[i] = (byte)ms;
				Val[++i] = (byte)cc;
				Val[++i] = (byte)mm;
				Val[++i] = (byte)hh;
				Val[++i] = (byte)(YYYY >> 8);
				Val[++i] = (byte)YYYY;
				Val[++i] = (byte)MM;
				Val[++i] = (byte)DD;
			}
		}
		class TRowAlarm
		{
			static public int iSize = 24;
			public byte[] Val = new byte[iSize];
			public TRowAlarm(int NumEv, int CodeEv, TDateTime DT, int CodePar, params ushort[] Data)
			{
				int i = 0;
				Val[i] = (byte)0;
				Val[++i] = (byte)0;
				Val[++i] = (byte)0;
				Val[++i] = (byte)NumEv;
				Val[++i] = (byte)0;
				Val[++i] = (byte)CodeEv;
				Global.Append (DT.Val, 0, Val, ++i, DT.Val.Length);
				i += DT.Val.Length;
				Val[i] = (byte)0;
				Val[++i] = (byte)CodePar;
				int Pos = 0;
				while (Data[Pos] != ushort.MaxValue)
				{
					Val[++i] = (byte)Data[Pos++];
				}	
			}
		}
		//.........................................................................

		static TRowAlarm[] oRows = { new TRowAlarm (1, 1, new TDateTime(1,1,1,1,(ushort)2015,1,1), 0, ushort.MaxValue),
													 new TRowAlarm (2, 2, new TDateTime(2,2,2,2,(ushort)2016,2,2), 0, ushort.MaxValue),
													 new TRowAlarm (3, 3, new TDateTime(3,3,3,3,(ushort)2017,3,3), 1, ushort.MaxValue),
													 new TRowAlarm (4, 3, new TDateTime(3,4,3,3,(ushort)2017,3,3), 0x40, ushort.MaxValue),
													 new TRowAlarm (5, 4, new TDateTime(3,5,3,3,(ushort)2017,3,3), 4098, ushort.MaxValue),
													 new TRowAlarm (6, 4, new TDateTime(3,6,3,3,(ushort)2017,3,3), 4143, ushort.MaxValue),
													 new TRowAlarm (7, 5, new TDateTime(3,7,3,3,(ushort)2017,3,3), 0, ushort.MaxValue),
													 new TRowAlarm (8, 6, new TDateTime(3,8,3,3,(ushort)2017,3,3), 0, ushort.MaxValue),
													 new TRowAlarm (9, 7, new TDateTime(3,9,3,3,(ushort)2017,3,3), 1, ushort.MaxValue),
													 new TRowAlarm (10, 7, new TDateTime(4,3,3,3,(ushort)2017,3,3), 2, ushort.MaxValue),
													 new TRowAlarm (11, 8, new TDateTime(5,3,3,3,(ushort)2017,3,3), 1, ushort.MaxValue),
													 new TRowAlarm (12, 8, new TDateTime(6,3,3,3,(ushort)2017,3,3), 2, ushort.MaxValue),
													 new TRowAlarm (13, 9, new TDateTime(7,3,3,3,(ushort)2017,3,3), 1, ushort.MaxValue),
													 new TRowAlarm (14, 9, new TDateTime(8,3,3,3,(ushort)2017,3,3), 3, ushort.MaxValue),
													 new TRowAlarm (15, 10, new TDateTime(9,3,3,3,(ushort)2017,3,3), 1, ushort.MaxValue),
													 new TRowAlarm (16, 10, new TDateTime(3,3,4,3,(ushort)2017,3,3), 3, ushort.MaxValue),
													 new TRowAlarm (17, 11, new TDateTime(3,3,5,3,(ushort)2017,3,3), 1, ushort.MaxValue),
													 new TRowAlarm (18, 11, new TDateTime(3,3,6,3,(ushort)2017,3,3), 5, ushort.MaxValue),
													 new TRowAlarm (19, 12, new TDateTime(3,3,7,3,(ushort)2017,3,3), 1, ushort.MaxValue),
													 new TRowAlarm (20, 12, new TDateTime(3,3,8,3,(ushort)2017,3,3), 7, ushort.MaxValue),
													 new TRowAlarm (21, 13, new TDateTime(3,3,9,3,(ushort)2017,3,3), 0, ushort.MaxValue),
													 new TRowAlarm (22, 13, new TDateTime(3,3,3,4,(ushort)2017,3,3), 1, ushort.MaxValue),
													 new TRowAlarm (23, 14, new TDateTime(3,3,3,5,(ushort)2017,3,3), 5, ushort.MaxValue),
													 new TRowAlarm (24, 14, new TDateTime(3,3,3,6,(ushort)2017,3,3), 3, ushort.MaxValue),
													 new TRowAlarm (25, 15, new TDateTime(3,3,3,7,(ushort)2017,3,3), 2, ushort.MaxValue),
													 new TRowAlarm (26, 15, new TDateTime(3,3,3,8,(ushort)2017,3,3), 4, ushort.MaxValue),
													 new TRowAlarm (27, 16, new TDateTime(3,3,3,9,(ushort)2017,3,3), 1, ushort.MaxValue),
													 new TRowAlarm (28, 16, new TDateTime(3,3,3,10,(ushort)2017,3,3), 2, ushort.MaxValue),
													 new TRowAlarm (29, 16, new TDateTime(3,3,3,20,(ushort)2017,3,3), 3, ushort.MaxValue),
			};
		void FillAlarm (byte[] btaRX, ref byte[] btaTX)
		{
			btaTX = new byte[(int)MODBUS3_ANSW.Data + TRowAlarm.iSize + 2];// * iQuantEvAlarm
			int i = (int)MODBUS3_ANSW.Data;

			if (++iNumEw >= oRows.Length)
			{
				iNumEw = 0;
				byte[] btZero = { 0 };
				Global.Append (btZero, 0, btaTX, i, 1);
			}
			else
			{
				Global.Append (oRows[iNumEw].Val, 0, btaTX, i, TRowAlarm.iSize);
			}
			//foreach (var item in oRows)
			//{
			//	 Global.Append (item.Val, 0, btaTX, i, TRowAlarm.iSize);
			//	i += TRowAlarm.iSize;
			//}		
		}
		//_________________________________________________________________________
		void FillHoldingReg (byte[] btaRX, ref byte[] btaTX, int iQuantReg)
		{
			ushort usAddrReg = Global.ToUInt16rev (btaRX, (int)MODBUS3_RESP.RegStartH);
			int iEnd = iQuantReg + usAddrReg;
			for (; usAddrReg < iEnd; /*usAddrReg++*/)
			{
				switch (usAddrReg)
				{
				case 2:					// Расход рабочий, м3/ч 
				case 4:					// Температура, °C 
				case 8:					// Давление избыточное, МПа 
				case 0xE:       // Коэффициент сжимаемости 
				case 0x3002:     // Плотность 
				case 0x3004:     // Содержание азота (N2), молярные доли 
				case 0x3006:     // Содержание диоксида углерода (CO2), молярные доли 
				case 6: FillByVal (fVal++, ref btaTX, ref iPosTX); usAddrReg += 2; break; // Давление абсолютное, МПа 
				case 31:        // Накопленный расход в рабочих условиях, м3 
				case 0x200A:    // Прямой объем рабочий, м3 
				case 8206:      // Прямой объем стандартный, м3   0x200E
				case 26: FillByVal ((double)fVal++, ref btaTX, ref iPosTX); usAddrReg += 4; break; // Накопленный расход в нормальных условиях, м3 
				case 0x100A: FillDT (ref btaTX, ref iPosTX); usAddrReg += 7; break;				// Системное время 
				case 0x1012: btaTX[iPosTX++] = 9; btaTX[iPosTX++] = 0; usAddrReg += 1;    // Контрактный 
					break;
				case 0x3000:     // Метод расчёта плотности коэф-та сж-ти 
					FillByVal ((uint)sVal++, ref btaTX, ref iPosTX); usAddrReg += 2; break;
				default: iPosTX += 2; usAddrReg++; break;
				}
			}
		}
		//_________________________________________________________________________
		private void ДатаВремя (uint v, ref byte[] btaTX, ref int iPosTX)
		{
			throw new NotImplementedException ();
		}
		//_________________________________________________________________________
		private void FillDT (ref byte[] btaTX, ref int iPosTX)
		{
			DateTime DT = DateTime.Now;
			btaTX[iPosTX++] = (byte)DT.Millisecond;
			btaTX[iPosTX++] = (byte)DT.Second;
			btaTX[iPosTX++] = (byte)DT.Minute;
			btaTX[iPosTX++] = (byte)DT.Hour;
			btaTX[iPosTX++] = (byte)(DT.Year >> 8);
			btaTX[iPosTX++] = (byte)(DT.Year & 0xFF);
			btaTX[iPosTX++] = (byte)DT.Month;
			btaTX[iPosTX++] = (byte)DT.Day;
		}
		//_________________________________________________________________________
		private void FillDTold (ref byte[] btaTX, ref int iPosTX)
		{
			DateTime DT = DateTime.Now;
			btaTX[iPosTX++] = 0;
			btaTX[iPosTX++] = (byte)DT.Second;
			btaTX[iPosTX++] = 0;
			btaTX[iPosTX++] = (byte)DT.Minute;
			btaTX[iPosTX++] = 0;
			btaTX[iPosTX++] = (byte)DT.Hour;
			btaTX[iPosTX++] = 0;
			btaTX[iPosTX++] = (byte)DT.DayOfWeek;
			btaTX[iPosTX++] = 0;
			btaTX[iPosTX++] = (byte)DT.Day;
			btaTX[iPosTX++] = 0;
			btaTX[iPosTX++] = (byte)DT.Month;
			btaTX[iPosTX++] = 0;
			btaTX[iPosTX++] = (byte)(DT.Year - 2000);
		}
		//_________________________________________________________________________
		//private void FillDT (ref byte[] btaTX, ref int iPosTX)
		//{
		//	DateTime DT = DateTime.Now;
		//	btaTX[iPosTX++] = (byte)DT.Second;
		//	btaTX[iPosTX++] = 0;
		//	btaTX[iPosTX++] = (byte)DT.Minute;
		//	btaTX[iPosTX++] = 0;
		//	btaTX[iPosTX++] = (byte)DT.Hour;
		//	btaTX[iPosTX++] = 0;
		//	btaTX[iPosTX++] = (byte)DT.DayOfWeek;
		//	btaTX[iPosTX++] = 0;
		//	btaTX[iPosTX++] = (byte)DT.Day;
		//	btaTX[iPosTX++] = 0;
		//	btaTX[iPosTX++] = (byte)DT.Month;
		//	btaTX[iPosTX++] = 0;
		//	btaTX[iPosTX++] = (byte)DT.Year;
		//	btaTX[iPosTX++] = 0;
		//}
		//_________________________________________________________________________
		void FillInputReg (ref int iPosTX, byte[] btaRX, ref byte[] btaTX, int iQuantReg)
		{
			//ushort usAddrReg = Global.ToUInt16rev (btaRX, (int)MODBUS3_RESP.RegStartH);
			//int iEnd = iQuantReg + usAddrReg;
			//for (; usAddrReg < iEnd; /*usAddrReg++*/)
			//{
			//	switch (usAddrReg)
			//	{
			//	case 2:				// Расход рабочий, м3/ч
			//	case 4:       // Измеренная температура, °C 
			//	case 6: FillByVal (fVal++, ref btaTX, ref iPosTX); usAddrReg += 2; break; // Давление абсолютное, МПа 
			//	case 31:       // Накопленный расход в рабочих условиях, м3 
			//	case 26: FillByVal ((double)fVal++, ref btaTX, ref iPosTX); usAddrReg += 4; break; // Накопленный расход в нормальных условиях, м3 
			//	default: iPosTX += 2; usAddrReg++; break;
			//	}
			//}
		}
		//_________________________________________________________________________
		private void FillArch (ref int iPos, byte[] btaRX, ref byte[] btaTX, int iQuantRow)
		{
			//if (btaRX[14] == 1 || btaRX[14] == 2)  // Old arch 
			if (btaRX[9] == 1 || btaRX[9] == 2)
			{
				//if (btaRX[14] == 1)  // Old arch 
				if (btaRX[9] == 1)
				Parent.OutToWind ("Часовой");
				else Parent.OutToWind ("Суточный");
				FillArchH_D (ref iPos, btaRX, ref btaTX, iQuantRow);
			}
			else if (btaRX[14] == 3)
			{
				Parent.OutToWind ("Вмешательств");
				FIllArchInterfer (ref iPos, btaRX, ref btaTX, iQuantRow);
			}
			else if (btaRX[14] == 4)
			{
				Parent.OutToWind ("Аварий");
				FIllArchAlarm (ref iPos, btaRX, ref btaTX, iQuantRow);
			}
    }
		//_________________________________________________________________________

		int iCodeAlarm = 1;
		private void FIllArchAlarm (ref int iPosData, byte[] btaRX, ref byte[] btaTX, int iQuantRow)
		{
			const uint uiDateInc = 60 * 60;
			uint uiBeginRec = Global.ToUInt16rev (btaRX, (int)CArchVympel.ERequest.BeginRec);
			uint uiDateArch = uiDateArchBeg + uiDateInc * uiBeginRec;
//#if LOG_VYMPEL
//			Global.LogWriteLine ("BeginRec " + uiBeginRec);
//			#endif
      for (int iRow = 0; iRow < iQuantRow; iRow++)
			{
				// Дата и время 
				//Global.LogWriteLine (Global.IntToDateTime (uiDateArch).ToString());
        Global.AppendRev (BitConverter.GetBytes (uiDateArch), 0, btaTX, iPosData, 4);
				uiDateArch += (uint)uiDateInc;
				iPosData += 4;
				// Код тревоги 
				btaTX[iPosData + 3] = (byte)iCodeAlarm;
				if (++iCodeAlarm == 27)
					iCodeAlarm = 1;
				iPosData += 4;
				// Параметр 1 
				btaTX[iPosData + 3] = (byte)1;
				iPosData += 4;
				// Параметр 2 
				btaTX[iPosData + 3] = (byte)2;
				iPosData += 4;
			}
		}
		//_________________________________________________________________________

		uint uiCodeInterfer = 0x101;
		private void FIllArchInterfer (ref int iPosData, byte[] btaRX, ref byte[] btaTX, int iQuantRow)
		{
			const uint uiDateInc = 30 * 60;
			uint uiBeginRec = Global.ToUInt16rev (btaRX, (int)CArchVympel.ERequest.BeginRec);
			uint uiDateArch = uiDateArchBeg + uiDateInc * uiBeginRec;
			//#if LOG_VYMPEL
			//			Global.LogWriteLine ("BeginRec " + uiBeginRec);
			//			#endif
			for (int iRow = 0; iRow < iQuantRow; iRow++)
			{
				// Дата и время 
				Global.LogWriteLine (Global.IntToDateTime (uiDateArch).ToString ());
				Global.AppendRev (BitConverter.GetBytes (uiDateArch), 0, btaTX, iPosData, 4);
				uiDateArch += uiDateInc;
				iPosData += 4;
				// Код вмешательства 
				FillByVal (uiCodeInterfer, ref btaTX, ref iPosData);

				HeadsArchInterfer TheTypePar = GetTypeOfParam (uiCodeInterfer);
				if (TheTypePar == HeadsArchInterfer.Old_Value_Int)
				{
					// Старое значение параметра 
					FillByVal ((uint)++iVal, ref btaTX, ref iPosData);
					// Новое значение параметра 
					FillByVal ((uint)++iVal, ref btaTX, ref iPosData);
				}
				else
				{
					// Старое значение параметра 
					FillByVal (++fVal, ref btaTX, ref iPosData);
					// Новое значение параметра 
					FillByVal (++fVal, ref btaTX, ref iPosData);
				}
				if (++uiCodeInterfer == 0x112)
					uiCodeInterfer = 0x201;
				else if (uiCodeInterfer == 0x20A)
					uiCodeInterfer = 0x301;
				else if (uiCodeInterfer == 0x31C)
					uiCodeInterfer = 0x401;
				else if (uiCodeInterfer == 0x422)
					uiCodeInterfer = 0x501;
				else if (uiCodeInterfer == 0x504)
					uiCodeInterfer = 0x601;
				else if (uiCodeInterfer == 0x608)
					uiCodeInterfer = 0x701;
				else if (uiCodeInterfer == 0x702)
					uiCodeInterfer = 0x1000;
				else if (uiCodeInterfer == 0x1001)
					uiCodeInterfer = 0xF001;
				else if (uiCodeInterfer == 0xF002)
					uiCodeInterfer = 0x101;
			}
		}
		//_________________________________________________________________________
		/// <summary>
		/// Вычисление первого столбца для записи текущего значения параметра (архив вмешательств)
		/// </summary>
		private HeadsArchInterfer GetTypeOfParam (uint uiCode)
		{
			if (((uiCode >= 0x201 && uiCode <= 0x303) && uiCode != 0x204) ||
					(uiCode >= 0x401 && uiCode <= 0x406) ||
					(uiCode == 0x40A) ||
					(uiCode == 0x501) ||
					(uiCode >= 0x601 && uiCode <= 0x603) ||
					(uiCode >= 0x606 && uiCode <= 0x701))
				return HeadsArchInterfer.Old_Value_Int;
			return HeadsArchInterfer.Old_Value_Float;
		}
		//_________________________________________________________________________

		uint uiDateArchBeg;

		private void FillArchH_D (ref int iPosData, byte[] btaRX, ref byte[] btaTX, int iQuantRow)
		{			
			int iDateInc = (btaRX[(int)CArchVympel.ERequest.IDarch + 1] == 1) ? 60 * 60 : 24 * 60 * 60;
			uint uiDateArch = uiDateArchBeg + (uint)iDateInc * Global.ToUInt16rev (btaRX, (int)CArchVympel.ERequest.BeginRec);

			for (int iRow = 0; iRow < iQuantRow; iRow++)
			{
				Global.AppendRev (BitConverter.GetBytes (uiDateArch), 0, btaTX, iPosData, 4);
				uiDateArch += (uint)iDateInc;
				iPosData += 4;
				for (int iPosVal = 0; iPosVal < 5; iPosVal++)
				{
					FillByVal (fVal++, ref btaTX, ref iPosData);	//FillByFloat
				}
			}
		}
		//_________________________________________________________________________
		void FillByFloat (float fVal, ref byte[] btaTo, ref int iPos)
		{
			byte[] bta = BitConverter.GetBytes (fVal);
			//foreach (var item in bta)
			//{
			//	btaTo[iPos++] = item;
			//}
			btaTo[iPos++] = bta[3];
			btaTo[iPos++] = bta[2];
			btaTo[iPos++] = bta[1];
			btaTo[iPos++] = bta[0];
		}
	}
}
