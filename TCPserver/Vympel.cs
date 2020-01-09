///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

///~~~~~~~~~	Проект:			Слушатель порта TCP
///~~~~~~~~~	Прибор:			Вымпе 500
///~~~~~~~~~	Модуль:			Эмулятор прибора Вымпе 500
///~~~~~~~~~	Разработка:	Демешкевич С.А.
///~~~~~~~~~	Дата:				27.02.2017

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
	public class CDevVympel : CTCPdevice
	{
		//xxxxxxxxxxxxxxxxxxxxx    П О С Т О Я Н Н Ы Е      xxxxxxxxxxxxxxxxxxxxxxx
		enum EArchive { Hour, Day, Alarm, Interfer }

		//xxxxxxxxxxxxxxxxxxxxx    П Е Р Е М Е Н Н Ы Е      xxxxxxxxxxxxxxxxxxxxxxx

		object oПолучОтвет = new object ();
		uint uiDateArchBeg;
		private int[] iaCounterZeroData = new int[4];
		EArchive Arch;

		//xxxxxxxxxxxxxxxxxxxxxxxx       К О Д        xxxxxxxxxxxxxxxxxxxxxxxxxxxxx
		public CDevVympel (FTCPserver Prnt) : base (Prnt)
		{
			iBeginData = 0;
			uiDateArchBeg = Global.DateTimeAsInt (DateTime.Now.AddDays(-10));
		}
		//_________________________________________________________________________

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
					int iQuantRow = (Global.ToUInt16rev (btaRX, (int)CArchVympel.ERequest.QuantRegRd) - 3) /
														CArchVympel.iaQuantRegByRow[iArch];
					btaTX = new byte[(int)CArchVympel.EAnswer.Data + iQuantRow * CArchVympel.iaQuantRegByRow[iArch] * 2 + 2];    // Добавлять 3 регистра? 

					FillArch (ref iPosTX, btaRX, ref btaTX, iQuantRow);

					int iQuantBytes = iPosTX + 2 - (int)CArchVympel.EAnswer.CodeServiceFunc;    // iQuantRow * CArchVympel.iaQuantRegByRow[iArch] * 2;
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
				case 1464: uint uiDT = Global.DateTimeAsInt (DateTime.Now); // 
					FillByVal (uiDT, ref btaTX, ref iPosTX); usAddrReg += 2;
					Parent.OutToWind ("Системное время", false); break;
				case 366: sVal = 9; usAddrReg += 2;   //  
					FillByVal ((uint)sVal++, ref btaTX, ref iPosTX); Parent.OutToWind ("Контрактный", false); break;
				case 374:     //  
					FillByVal ((uint)2, ref btaTX, ref iPosTX); usAddrReg += 2;					//FillByVal ((uint)sVal++, ref btaTX, ref iPosTX); usAddrReg += 2;
					Parent.OutToWind ("Метод расчёта плотности природного газа", false); break;
				case 386: FillByVal (fVal++, ref btaTX, ref iPosTX); usAddrReg += 2; Parent.OutToWind ("Барометрическое давление", false); break;    // , кПа 
				case 388: FillByVal (fVal++, ref btaTX, ref iPosTX); usAddrReg += 2; Parent.OutToWind ("Плотность газа в с.у.", false); break;    // тандартных условиях, кг/м3 
				case 390: FillByVal (fVal++, ref btaTX, ref iPosTX); usAddrReg += 2; Parent.OutToWind ("Содержание N2", false); break;    // , молярные доли 
				case 392: FillByVal (fVal++, ref btaTX, ref iPosTX); usAddrReg += 2; Parent.OutToWind ("Содержание CO2", false); break;    // ), молярные доли 
				case 490: FillByVal (fVal++, ref btaTX, ref iPosTX); usAddrReg += 2; Parent.OutToWind ("Температура", false); break;    // , °C 
				case 492:FillByVal (fVal++, ref btaTX, ref iPosTX); usAddrReg += 2;      // , МПа 
					FillByVal (fVal++, ref btaTX, ref iPosTX); usAddrReg += 2; Parent.OutToWind ("Абсолютное давление", false); break;
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
				case 0: FillByVal (123456, ref btaTX, ref iPosTX); usAddrReg += 2; Parent.OutToWind ("Заводской номер прибора", false); break; //  
				case 124: FillByVal (fVal++, ref btaTX, ref iPosTX); usAddrReg += 2; Parent.OutToWind ("Измеренная температура", false);  break;// Измеренная температура, °C 
				case 874: FillByVal (fVal++, ref btaTX, ref iPosTX); usAddrReg += 2; Parent.OutToWind ("Часовой расход в р.у.", false); break;      // абочих условиях, м3 
				case 980: FillByVal (fVal++, ref btaTX, ref iPosTX); usAddrReg += 2; Parent.OutToWind ("Часовой расход в н.у.", false); break;      // ормальных условиях, м3 
				case 1100: FillByVal (fVal++, ref btaTX, ref iPosTX); usAddrReg += 2; Parent.OutToWind ("Суточный расход в р", false); break;      // абочих условиях, м3
				case 1102: FillByVal (fVal++, ref btaTX, ref iPosTX); usAddrReg += 2; Parent.OutToWind ("Суточный расход в н.у.", false); break;      // Суточный расход: расход в нормальных словиях, м3 
				case 780: FillByVal (fVal++, ref btaTX, ref iPosTX); usAddrReg += 2; Parent.OutToWind ("Мгновенный расход в рабочих условиях", false); break;    // , м3/час 
				case 126: FillByVal (fVal++, ref btaTX, ref iPosTX); usAddrReg += 2; Parent.OutToWind ("Измеренное давление", false); break; // , МПа 
				case 854: FillByVal ((double)fVal++, ref btaTX, ref iPosTX); usAddrReg += 4; Parent.OutToWind ("", false); break;     // Накопленный расход в рабочих условиях, м3 
				case 858: FillByVal ((double)fVal++, ref btaTX, ref iPosTX); usAddrReg += 4; Parent.OutToWind ("Накопленный расход в н.у.", false); break;// , м3 
				case 976:       // 
					FillByVal (Global.DateTimeAsInt (DateTime.Now.AddHours (-1)), ref btaTX, ref iPosTX); usAddrReg += 2; Parent.OutToWind ("Часовой расход:  Дата и время", false); break;
				case 1098:	//  
					FillByVal (Global.DateTimeAsInt (DateTime.Now.AddDays (-1)), ref btaTX, ref iPosTX); usAddrReg += 2; Parent.OutToWind ("Суточный расход: Дата и время", false); break;
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
			DateTime DTnow = DateTime.Now;
			uint uiDateNow = Global.DateTimeAsInt (DTnow);
			uint uiBeginRec = Global.ToUInt16rev (btaRX, (int)CArchVympel.ERequest.BeginRec);
			uint uiDateArch = uiDateArchBeg + uiDateInc * uiBeginRec;

			int uiQuantRowOut = (int)((uiDateNow - uiDateArch) / uiDateInc);
			if (uiQuantRowOut >= iQuantRow)
				uiQuantRowOut = iQuantRow;
			else uiQuantRowOut = (uiQuantRowOut % (iQuantRow + 1));

			string asDTArch = "";
			if (uiQuantRowOut < 0)
				uiQuantRowOut = 0;
			for (int iRow = 0; iRow < uiQuantRowOut; iRow++)
			{
				if (uiDateArch >= uiDateNow)
					break;
				// Дата и время 
				//Global.LogWriteLine (Global.IntToDateTime (uiDateArch).ToString());
        Global.AppendRev (BitConverter.GetBytes (uiDateArch), 0, btaTX, iPosData, 4);
				asDTArch += Global.IntToDateTime (uiDateArch) + ";";
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
			Parent.OutToWind (asDTArch);
			Global.AppendRev (BitConverter.GetBytes (uiQuantRowOut * CArchVympel.iaQuantRegByRow[(int)CArchVympel.EArchID.Alarm] * 2), 0, btaTX, (int)CArchVympel.ERequest.NumByteData, 1);
		}
		//_________________________________________________________________________

		uint uiCodeInterfer = 0x101;

		private void FIllArchInterfer (ref int iPosData, byte[] btaRX, ref byte[] btaTX, int iQuantRow)
		{
			const uint uiDateInc = 30 * 60;
			uint uiBeginRec = Global.ToUInt16rev (btaRX, (int)CArchVympel.ERequest.BeginRec);
			uint uiDateArch = uiDateArchBeg + uiDateInc * uiBeginRec;
			uint uiDateNow = Global.DateTimeAsInt (DateTime.Now);
			int uiQuantRowOut = (int)((uiDateNow - uiDateArch) / uiDateInc);
			if (uiQuantRowOut >= iQuantRow)
				uiQuantRowOut = iQuantRow;
			else uiQuantRowOut = (uiQuantRowOut % (iQuantRow + 1));

			if (uiQuantRowOut < 0)
				uiQuantRowOut = 0;
			string asDTArch = "";
			for (int iRow = 0; iRow < uiQuantRowOut; iRow++)
			{
				if (uiDateArch >= uiDateNow)
					break;
				// Дата и время 
				//Global.LogWriteLine (Global.IntToDateTime (uiDateArch).ToString ());
				Global.AppendRev (BitConverter.GetBytes (uiDateArch), 0, btaTX, iPosData, 4);
				asDTArch += Global.IntToDateTime (uiDateArch) + ";";
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
			Parent.OutToWind (asDTArch);
			Global.AppendRev (BitConverter.GetBytes (uiQuantRowOut * CArchVympel.iaQuantRegByRow[(int)CArchVympel.EArchID.Alarm]*2), 0, btaTX, (int)CArchVympel.ERequest.NumByteData, 1);
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
		private void FillArchH_D (ref int iPosData, byte[] btaRX, ref byte[] btaTX, int iQuantRow)
		{
			long uiDateInc = (btaRX[(int)CArchVympel.ERequest.IDarch + 1] == 1) ? (uint)(60 * 60) : (24 * 60 * 60);// 6 * 60 : 24 * 60 * 6;
			long uiBeginRec = Global.ToUInt16rev (btaRX, (int)CArchVympel.ERequest.BeginRec);
			long uiDateArch = uiDateArchBeg + uiDateInc * uiBeginRec;
			long uiDateNow = Global.DateTimeAsInt (DateTime.Now);
			long lSub = uiDateNow - uiBeginRec;
			long lDiv = lSub / uiDateInc;

			int uiQuantRowOut = (int)((uiDateNow - uiDateArch) / uiDateInc);
			if (uiQuantRowOut >= iQuantRow)
				uiQuantRowOut = iQuantRow;
			else uiQuantRowOut = (uiQuantRowOut % (iQuantRow + 1));
			if (uiQuantRowOut < 0)
				uiQuantRowOut = 0;

			string asDTArch = "";
			//Parent.OutToWind ((btaRX[14] == 1 ? "Часовой: " : "Суточный: ") + Global.IntToDateTime (uiDateArch - (btaRX[14] == 1 ? 60*60:24*60*60)).ToString());
			for (int iRow = 0; iRow < uiQuantRowOut; iRow++)
			{
				if (uiDateArch >= uiDateNow)
					break;
				Global.AppendRev (BitConverter.GetBytes (uiDateArch), 0, btaTX, iPosData, 4);

				asDTArch += Global.IntToDateTime (uiDateArch) + ";";
				uiDateArch += (uint)uiDateInc;
				iPosData += 4;
				for (int iPosVal = 0; iPosVal < 5; iPosVal++)
				{
					FillByVal (fVal++, ref btaTX, ref iPosData);	//FillByFloat
				}
			}
			Parent.OutToWind (asDTArch);
			Global.AppendRev (BitConverter.GetBytes (uiQuantRowOut * CArchVympel.iaQuantRegByRow[(int)CArchVympel.EArchID.Alarm]*2), 0, btaTX, (int)CArchVympel.ERequest.NumByteData, 1);
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
