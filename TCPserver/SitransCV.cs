﻿///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

///~~~~~~~~~	Проект:			Слушатель порта TCP
///~~~~~~~~~	Прибор:			Хроматограф SitransCV
///~~~~~~~~~	Модуль:			Эмулятор прибора Хроматограф SitransCV
///~~~~~~~~~	Разработка:	Демешкевич С.А.
///~~~~~~~~~	Дата:				30.07.2018

///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

using System;
using System.Collections.Generic;
using System.Linq;
using AXONIM.CONSTS;
using AXONIM.ScanParamOfDevicves;
using AXONIM.BelTransGasDRV;
using System.Threading;

namespace TestDRVtransGas.TCPserver
{
	public class CDevSitransCV : CTCPdevice
	{
		//xxxxxxxxxxxxxxxxxxxxx    П О С Т О Я Н Н Ы Е      xxxxxxxxxxxxxxxxxxxxxxx

		//xxxxxxxxxxxxxxxxxxxxx    П Е Р Е М Е Н Н Ы Е      xxxxxxxxxxxxxxxxxxxxxxx

		//xxxxxxxxxxxxxxxxxxxxxxxx       К О Д        xxxxxxxxxxxxxxxxxxxxxxxxxxxxx
		public CDevSitransCV (FTCPserver Prnt) : base (Prnt)
		{
			iBeginData = 0;
			uiDateArchBeg = Global.DateTimeAsInt (new DateTime (2018, 2, 1, 5, 0, 0));
		}
		//_________________________________________________________________________

		object oПолучОтвет = new object ();

		override public bool GetAnswer (ref byte[] btaTX, byte[] btaRX, ref int iLenFirstPartBySend)
		{
			lock (oПолучОтвет)
			{
				DTCurr = DateTime.Now;
				int iSizeByCRC = 0;
				int iBeginBuf = 0;
				iPosTX = (int)MODBUS3_ANSW.Data;
				//iLenFirstPartBySend = 20;

				if (btaRX[(int)CArchVympel.ERequest.Func] == 0x17)                 // Фармавання архiваў 
				{
					iPosTX = (int)CArchVympel.EAnswer.Data;
					int iArch = btaRX[(int)CArchVympel.ERequest.IDarch + 1];
					//TODO EDIT:
					int iQuantRow = 2; //(Global.ToUInt16rev (btaRX, (int)CArchVympel.ERequest.QuantRegRd) - 3) ///TODO EDIT
					//									CArchVympel.iaQuantRegInRec[iArch];
					//btaTX = new byte[(int)CArchVympel.EAnswer.Data + iQuantRow * CArchVympel.iaQuantRegInRec[iArch] * 2 + 2];    // Добавлять 3 регистра? //TODO EDIT
					btaTX = new byte[(int)CArchVympel.EAnswer.Data + iQuantRow * 5 * 2 + 2];    // Добавлять 3 регистра? 
					Parent.OutToWind ("ИСПРАВИТЬ выдачу архивов SitransCV");
					FillArch (ref iPosTX, btaRX, ref btaTX, iQuantRow);

					int iQuantBytes = iPosTX + 2 - (int)CArchVympel.EAnswer.CodeServiceFunc;    // iQuantRow * CArchVympel.iaQuantRegInRec[iArch] * 2;
					if (iPosTX == (int)CArchVympel.EAnswer.Data)
					{
						if (iaCounterZeroData[(int)Arch] == 1)
						{
							btaTX = new byte[0];
						}
						else
						{
							iaCounterZeroData[(int)Arch] = 1;
							btaTX = new byte[1];
							btaTX[0] = 0xFF;
						}
						return false;
					}
					else
						btaTX[(int)CArchVympel.EAnswer.QuantBitesRd] = (byte)(iQuantBytes/* + 6*/);
					iSizeByCRC = btaTX.Length - 2;
				}
				else                      // Фармавання дыскрэтных параметраў 
				{
					if (btaRX[(int)MODBUS3_RESP.Func] == 16)                          // Запись параметров 
					{
						btaTX = new byte[(int)MODBUS16_ANSW.SIZE];
						iSizeByCRC = btaTX.Length - 2;
						for (iPosTX = 0; iPosTX < (int)MODBUS16_ANSW.CRCh; iPosTX++)
						{
							btaTX[iPosTX] = btaRX[iPosTX];
						}
						Parent.OutToWind ("ЗАПІС: " + Global.ByteArToStr (btaRX), false);
					}
					else
					{
						ushort usQuantityOfRegisters = Global.ToUInt16rev (btaRX, (int)MODBUS3_RESP.NumRegH);// BitConverter.ToUInt16 (btaRX, iPos + (int)MODBUS3_RESP.NumRegH);
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
					}
				}
				if (btaTX.Length < (int)MODBUS3_ANSW.Func)      // Если сбой заполнения данными 
				{
					btaTX = Global.EncodingCurr.GetBytes ("Сбой заполнения данными");
					Parent.OutToWind ("СБОЙ ЗАПОЛНЕНИЯ ДАННЫМИ");
					return false;
				}
				for (int i = 0; i <= (int)MODBUS3_ANSW.Func; i++)
				{
					btaTX[i] = btaRX[i];
				}
				FillCRC (btaTX, iSizeByCRC, iBeginBuf);

				ChangeVal ();
				return false;
			}
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
				case 1464: uint uiDT = Global.DateTimeAsInt (DateTime.Now); //Системное время 
					FillByVal (uiDT, ref btaTX, ref iPosTX); usAddrReg += 2;
					break;
				case 366: sVal = 9; usAddrReg += 2;   // Контрактный 
					FillByVal ((uint)sVal++, ref btaTX, ref iPosTX); break;
				case 374:     // Метод расчёта плотности природного газа 
					FillByVal ((uint)2, ref btaTX, ref iPosTX); usAddrReg += 2;
					//FillByVal ((uint)sVal++, ref btaTX, ref iPosTX); usAddrReg += 2;
					break;
				case 386:     // Барометрическое давление, кПа 
				case 388:     // Плотность газа в стандартных условиях, кг/м3 
				case 390:     // Содержание азота (N2), молярные доли 
				case 392:     // Содержание диоксида углерода (CO2), молярные доли 
				case 490:     // Температура, °C 
				case 492:     // Абсолютное давление, МПа 
					FillByVal (fVal++, ref btaTX, ref iPosTX); usAddrReg += 2; break;
				default: iPosTX += 2; usAddrReg++; break;
				}
			}
		}
		//_________________________________________________________________________
		void FillInputReg (ref int iPosTX, byte[] btaRX, ref byte[] btaTX, int iQuantReg)
		{
			ushort usAddrReg = Global.ToUInt16rev (btaRX, (int)MODBUS3_RESP.RegStartH);
			int iEnd = iQuantReg + usAddrReg;
			for (; usAddrReg < iEnd; /*usAddrReg++*/)
			{
				switch (usAddrReg)
				{
				//case 0: FillByVal (iVal++, ref btaTX, ref iPosTX); usAddrReg += 2; break; // Заводской номер прибора 
				case 0: FillByVal (123456, ref btaTX, ref iPosTX); usAddrReg += 2; break; // Заводской номер прибора 
				case 124:       // Измеренная температура, °C 
				case 874:       // Часовой расход: расход в рабочих условиях, м3 
				case 980:       // Часовой расход: расход в нормальных условиях, м3 
				case 1100:      // Суточный расход: расход в рабочих условиях, м3
				case 1102:      // Суточный расход: расход в нормальных условиях, м3 
				case 780:     // Мгновенный расход в рабочих условиях, м3/час 
				case 126: FillByVal (fVal++, ref btaTX, ref iPosTX); usAddrReg += 2; break; // Измеренное давление, МПа 
				case 854:       // Накопленный расход в рабочих условиях, м3 
				case 858: FillByVal ((double)fVal++, ref btaTX, ref iPosTX); usAddrReg += 4; break; // Накопленный расход в нормальных условиях, м3 
				case 976:       //Часовой расход:  Дата и время 
					FillByVal (Global.DateTimeAsInt (DateTime.Now.AddHours (-1)), ref btaTX, ref iPosTX); usAddrReg += 2; break;
				case 1098:      // Суточный расход: Дата и время 
					FillByVal (Global.DateTimeAsInt (DateTime.Now.AddDays (-1)), ref btaTX, ref iPosTX); usAddrReg += 2; break;
				default: iPosTX += 2; usAddrReg++; break;
				}
			}
		}
		//_________________________________________________________________________
		private void FillArch (ref int iPos, byte[] btaRX, ref byte[] btaTX, int iQuantRow)
		{
			if (btaRX[14] == 1 || btaRX[14] == 2)
			{
				if (btaRX[(int)CArchVympel.ERequest.IDarch + 1] == 1)
				{
					Parent.OutToWind ("Часовой", false);
					Arch = EArchive.Hour;
				}
				else
				{
					Parent.OutToWind ("Суточный", false);
					Arch = EArchive.Day;
				}
				FillArchH_D (ref iPos, btaRX, ref btaTX, iQuantRow);
			}
			else if (btaRX[14] == 3)
			{
				Arch = EArchive.Interfer;
				FIllArchInterfer (ref iPos, btaRX, ref btaTX, iQuantRow);
				Parent.OutToWind ("Вмешательства", false);
			}
			else if (btaRX[14] == 4)
			{
				Arch = EArchive.Alarm;
				FIllArchAlarm (ref iPos, btaRX, ref btaTX, iQuantRow);
				Parent.OutToWind ("Arch. Alarm", false);
			}
    }
		//_________________________________________________________________________

		int iCodeAlarm = 1;
		private void FIllArchAlarm (ref int iPosData, byte[] btaRX, ref byte[] btaTX, int iQuantRow)
		{
			const uint uiDateInc = 60 * 60;
			uint uiBeginRec = Global.ToUInt16rev (btaRX, (int)CArchVympel.ERequest.BeginRec);
			uint uiDateArch = uiDateArchBeg + uiDateInc * uiBeginRec;
			uint uiDateNow = Global.DateTimeAsInt (DateTime.Now);

			for (int iRow = 0; iRow < iQuantRow; iRow++)
			{
				if (uiDateArch >= uiDateNow)
					break;
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
			uint uiDateNow = Global.DateTimeAsInt (DateTime.Now);

			for (int iRow = 0; iRow < iQuantRow; iRow++)
			{
				if (uiDateArch >= uiDateNow)
					break;
				// Дата и время 
				//Global.LogWriteLine (Global.IntToDateTime (uiDateArch).ToString ());
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
		private int[] iaCounterZeroData = new int[4];
		enum EArchive { Hour, Day, Alarm, Interfer }
		EArchive Arch;

		private void FillArchH_D (ref int iPosData, byte[] btaRX, ref byte[] btaTX, int iQuantRow)
		{
			int iDateInc = (btaRX[(int)CArchVympel.ERequest.IDarch + 1] == 1) ? 60 * 60:24 * 60 * 60;// 6 * 60 : 24 * 60 * 6;
			uint uiDateArch = uiDateArchBeg + (uint)iDateInc * Global.ToUInt16rev (btaRX, (int)CArchVympel.ERequest.BeginRec);
			uint uiDateNow = Global.DateTimeAsInt (DateTime.Now);

			//Parent.OutToWind ((btaRX[14] == 1 ? "Часовой: " : "Суточный: ") + Global.IntToDateTime (uiDateArch - (btaRX[14] == 1 ? 60*60:24*60*60)).ToString());
			for (int iRow = 0; iRow < iQuantRow; iRow++)
			{
				if (uiDateArch >= uiDateNow)
					break;
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
		void FillByVal (dynamic Val, ref byte[] btaTo, ref int iPos)
		{
			int iPosRev;
			if (Val is ushort || Val is short)
			{
				iPosRev = iPos + 2;
				iPos += 2;
			}
			else if (Val is float || Val is int || Val is uint)
			{
				iPosRev = iPos + 4;
				iPos += 4;
			}
			else
			{
				iPosRev = iPos + 8;
				iPos += 8;
			}
			byte[] bta = BitConverter.GetBytes (Val);
			foreach (var item in bta)
			{
				btaTo[--iPosRev] = item;
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
