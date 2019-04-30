///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

///~~~~~~~~~	Проект:			Слушатель порта TCP
///~~~~~~~~~	Прибор:			EK270
///~~~~~~~~~	Модуль:			Эмулятор прибора EK270
///~~~~~~~~~	Разработка:	Демешкевич С.А.
///~~~~~~~~~	Дата:				09.08.2018

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
	public class CDevEK270 : CTCPdevice
	{
		//xxxxxxxxxxxxxxxxxxxxx    П О С Т О Я Н Н Ы Е      xxxxxxxxxxxxxxxxxxxxxxx

		//xxxxxxxxxxxxxxxxxxxxx    П Е Р Е М Е Н Н Ы Е      xxxxxxxxxxxxxxxxxxxxxxx

		//xxxxxxxxxxxxxxxxxxxxxxxx       К О Д        xxxxxxxxxxxxxxxxxxxxxxxxxxxxx
		public CDevEK270 (FTCPserver Prnt) : base (Prnt)
		{
			iBeginData = (int)MODBUS3_RESP.RegStartH;
			uiDateArchBeg = Global.DateTimeAsInt (new DateTime (2017, 4, 24, 5, 0, 0));
		}
		//_________________________________________________________________________

		object oПолучОтвет = new object ();
		int iSizeByCRC;

		override public bool GetAnswer (ref byte[] btaTX, byte[] btaRX, ref int iNumPartOfAnswerBySend)
		{
			lock (oПолучОтвет)
			{

				btaTX = new byte[] { 0x2F, 0x45, 0x6C, 0x73, 0x36, 0x45, 0x4B, 0x32, 0x37, 0x30, 0xD, 0xA };
				//	int iBeginBuf = 0;
				//	iPosTX = (int)MODBUS3_ANSW.Data;

				//	ushort usAddrReg = Global.ToUInt16rev (btaRX, (int)MODBUS3_RESP.RegStartH);

				//	if (btaRX[(int)MODBUS3_RESP.Func] == 16)          // Запіс параметраў 
				//	{
				//		Parent.OutToWind ("Запіс параметраў", false);
				//		btaTX = new byte[(int)MODBUS16_ANSW.SIZE];
				//		iSizeByCRC = btaTX.Length - 2;
				//		iPosTX = (int)MODBUS16_ANSW.CRCh;
				//		Parent.OutToWind ("ЗАПІС: " + Global.ByteArToStr (btaRX));
				//	}
				//	else if (btaRX[(int)MODBUS3_RESP.Func] == 3)
				//	{
				//		Parent.OutToWind ("Имгненныя", false);
				//		ushort usQuantityOfRegisters = Global.ToUInt16rev (btaRX, (int)MODBUS3_RESP.NumRegH);
				//		int iNumByteData = usQuantityOfRegisters * 2;
				//		btaTX = new byte[iPosTX + iNumByteData + 2];
				//		btaTX[(int)MODBUS3_ANSW.NumByteData] = (byte)iNumByteData;
				//		iSizeByCRC = btaTX.Length - 2;

				//		if (btaRX[(int)MODBUS3_RESP.Func] == 3)
				//			FillHoldingReg (btaRX, ref btaTX, usQuantityOfRegisters);
				//		else if (btaRX[(int)MODBUS3_RESP.Func] == 4)
				//		{
				//			FillInputReg (ref iPosTX, btaRX, ref btaTX, usQuantityOfRegisters);
				//		}
				//	}
				//	else                                      // АРХИВЫ  
				//	{
				//		if (usAddrReg == 0x2000 && btaRX[(int)MODBUS3_RESP.Func] == 3)    // Фармiравання архiваў H/D 
				//		{
				//			Parent.OutToWind ("Фармiравання архiваў H/D", false);
				//			iSizeByCRC = btaTX.Length - 2;

				//		}
				//		else if (btaRX[(int)MODBUS3_RESP.Func] == 3 && (usAddrReg >= 0x2200 && usAddrReg <= 0x220E))  // Аварый  
				//		{
				//			Parent.OutToWind ("Аварый", false);

				//			iSizeByCRC = btaTX.Length - 2;
				//		}
				//		else                                                              // Фармiравання дыскрэтных або iншых параметраў 
				//		{
				//		}
				//	}

				//	if (btaTX.Length < (int)MODBUS3_ANSW.Func)                          // Если сбой заполнения данными 
				//	{
				//		btaTX = Global.EncodingCurr.GetBytes ("Сбой заполнения данными");
				//		Parent.OutToWind ("СБОЙ ЗАПОЛНЕНИЯ ДАННЫМИ");
				//		return false;
				//	}
				//	InitHead_CRC (btaTX, btaRX, iBeginBuf);

				//	ChangeVal ();
				return false;
			}
		}
		//_________________________________________________________________________
		private void InitHead_CRC (byte[] btaTX, byte[] btaRX, int iBeginBuf)
		{
			for (int i = 0; i <= (int)MODBUS3_ANSW.Func; i++)
			{
				btaTX[i] = btaRX[i];
			}
			ushort usCRC = Global.CRC (btaTX, iSizeByCRC, btaTX.Length, Global.Table8005, 0xFFFF, (ushort)iBeginBuf);
			byte[] btaCRC = BitConverter.GetBytes (usCRC);
			btaTX[iPosTX++] = btaCRC[0];
			btaTX[iPosTX++] = btaCRC[1];
		}
		//_________________________________________________________________________
		void FillAlarm (byte[] btaRX, ref byte[] btaTX)
		{
			btaTX = new byte[(int)MODBUS3_ANSW.Data + 333 + 2];

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
		private void FillDT (ref byte[] btaTX, ref int iPosTX)
		{
			DateTime DT = DateTime.Now;
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
			btaTX[iPosTX++] = (byte)DT.Year;
			btaTX[iPosTX++] = 0;
		}
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
			if (btaRX[14] == 1 || btaRX[14] == 2)
				FillArchH_D (ref iPos, btaRX, ref btaTX, iQuantRow);
			else if (btaRX[14] == 3)
				FIllArchInterfer (ref iPos, btaRX, ref btaTX, iQuantRow);
			else if (btaRX[14] == 4)
				FIllArchAlarm (ref iPos, btaRX, ref btaTX, iQuantRow);
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
