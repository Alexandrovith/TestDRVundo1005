///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

///~~~~~~~~~	Проект:			Тест драйверов приборов
///~~~~~~~~~	Прибор:			Все реализованные приборы
///~~~~~~~~~	Модуль:			Эмулятор прибора Superflo
///~~~~~~~~~	Разработка:	Демешкевич С.А.
///~~~~~~~~~	Дата:				01.07.2019

///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXONIM.CONSTS;

namespace TestDRVtransGas.COMserver
{
	public class CSuperflo : IDevice
	{
		#region ..........................       ПОСТОЯННЫЕ         ............................

		enum ER42request { Synch, Addr, MessLen, FuncCode, CRCh, CRCl }
		enum ER42answer
		{
			Synch, Addr, MessLen, FuncCode, RunNumber, RunName = 6,
			GasDensity = 21, MolePercentCO = 41, MolePercentN2 = 29, AtmosphericPressure = 33, LowFlowCutoff = 37, NoFlowCutoff = 41,
			AMeterFactor = 45, SpecificEnergy = 49, ScalingFactor = 53, CorrectionStatus = 54, PressureTransmitterType,
			Month, Day, Year, Hour, Min, Sec, CRCh, CRCl
		}

		enum ER43request
		{
			Synch, Addr, MessLen, FuncCode, RunNumber, SuperFloWritePassword, RunName = 21,
			GasDensity = 37, MolePercentCO = 41, MolePercentN2 = 45, AtmosphericPressure = 49, LowFlowCutoff = 53, NoFlowCutoff = 57,
			AMeterFactor = 61, SpecificEnergy = 65, ScalingFactor = 69, CorrectionStatus = 70, PressureTransmitterType, CRCh, CRCl
		}

		enum ER46request { Synch, Addr, MessLen, FuncCode, RunNumber, CRCh, CRCl }

		enum ER47request
		{
			Synch, Addr, MessLen, FuncCode, RunNumber, DiffPressFlag, DiffPressVal,
			StatPressFlag = DiffPressVal + 4, StatPressVal, TemperFlag = StatPressVal + 4, TemperVal,
			SAFE_CRC = TemperVal + 4, CRCh = SAFE_CRC + 2, CRCl
		}

		enum ER46_47answer
		{
			Synch, Addr, MessLen, FuncCode, RunNumber, DiffPressFlag, DiffPressVal,
			StatPressFlag = DiffPressVal + 4, StatPressVal, TemperFlag = StatPressVal + 4,
			TemperVal, Month = TemperVal + 4, Day, Year, Hour, Min, Sec, CRCh, CRCl
		}
		#endregion
		#region ...........................      ПЕРЕМЕННЫЕ       ............................

		byte[] btaR1;
		byte[] btaR1_5 = { 0x55, 1, 65, 1 + 128, 2,
										(byte)'S', (byte)'F', (byte)'2', (byte)'1', (byte)'R', (byte)'U', (byte)'5', (byte)'C', 0,0,0,0,0,0,0,0, 1,
										(byte)'S', (byte)'F', (byte)'2', (byte)'1', (byte)'R', (byte)'U', (byte)'5', (byte)'C', 0,0,0,0,0,0,0,0, 1,
										(byte)'S', (byte)'F', (byte)'2', (byte)'1', (byte)'R', (byte)'U', (byte)'5', (byte)'C', 0,0,0,0,0,0,0,0, 1,
										0,0,0,0,0,0, 9, 0, 0 };

		byte[] btaR1_6 = { 0x55, 1, 65, 1 + 128, 2,
										(byte)'S', (byte)'F', (byte)'2', (byte)'1', (byte)'R', (byte)'U', (byte)'6', (byte)'C', 0,0,0,0,0,0,0,0, 1,
										(byte)'S', (byte)'F', (byte)'2', (byte)'1', (byte)'R', (byte)'U', (byte)'6', (byte)'C', 0,0,0,0,0,0,0,0, 1,
										(byte)'S', (byte)'F', (byte)'2', (byte)'1', (byte)'R', (byte)'U', (byte)'6', (byte)'C', 0,0,0,0,0,0,0,0, 1,
										0,0,0,0,0,0, 9, 0, 0 };

		byte[] btaR1_7 = { 0x55, 1, 65, 1 + 128, 2,
										(byte)'S', (byte)'F', (byte)'2', (byte)'1', (byte)'R', (byte)'U', (byte)'7', (byte)'C', 0,0,0,0,0,0,0,0, 1,
										(byte)'S', (byte)'F', (byte)'2', (byte)'1', (byte)'R', (byte)'U', (byte)'7', (byte)'C', 0,0,0,0,0,0,0,0, 1,
										(byte)'S', (byte)'F', (byte)'2', (byte)'1', (byte)'R', (byte)'U', (byte)'7', (byte)'C', 0,0,0,0,0,0,0,0, 1,
										0,0,0,0,0,0, 9, 0, 0 };

		byte[] btaR4 = { 0x55, 1, 141, 4 + 128, 0, 0xCD,0xCC,0x8C,0x3F, 0xCD,0xCC,0x0C,0x40, 0x33,0x33,0x53,0x40, 0xCD,0xCC,0x8C,0x40, 0,0x00,0xB0,0x40, 0x33,0x33,0xD3,0x40,
		0xCD,0xCC,0x8C,0x3F, 0xCD,0xCC,0x0C,0x40, 0x33,0x33,0x53,0x40, 0xCD,0xCC,0x8C,0x40, 0,0x00,0xB0,0x40, 0x33,0x33,0xD3,0x40,
		0xCD,0xCC,0x8C,0x3F, 0xCD,0xCC,0x0C,0x40, 0x33,0x33,0x53,0x40, 0xCD,0xCC,0x8C,0x40, 0,0x00,0xB0,0x40, 0x33,0x33,0xD3,0x40,
		0xCD,0xCC,0x8C,0x3F, 0xCD,0xCC,0x0C,0x40, 0x33,0x33,0x53,0x40, 0xCD,0xCC,0x8C,0x40, 0,0x00,0xB0,0x40, 0x33,0x33,0xD3,0x40,
		0xCD,0xCC,0x8C,0x3F, 0xCD,0xCC,0x0C,0x40, 0x33,0x33,0x53,0x40, 0xCD,0xCC,0x8C,0x40, 0,0x00,0xB0,0x40, 0x33,0x33,0xD3,0x40,
		0xCD,0xCC,0x8C,0x3F, 0xCD,0xCC,0x0C,0x40, 1,2,19,4,5,6, 0,0 };

		byte[] btaR20 = { 0x55, 1, 37, 20 + 128, 0, 0, 0, 7, 2, 19, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
		byte[] btaR21 = { 0x55, 1, 37, 21 + 128, 0, 0, 0, 7, 2, 19, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
		byte[] btaR22 = { 0x55, 1, 26, 22 + 128, 0, 0, 0, 7, 2, 19, 0, 0, 0, 0, 0, 0xCD, 0xCC, 0x0C, 0x40, 0x33, 0x33, 0x53, 0x40, 0,0 };
		byte[] btaR23 = { 0x55, 1, 23, 23 + 128, 0, 0, 0, 7, 2, 19, 0, 0, 0, 0, 0, 0 ,0,0,0,0, 0,0 };

		byte[] btaR43 = { 0x55, 1, 6, 43 + 128, 0, 0 };

		byte[] btaR42_7 = { 0x55, 1, 107, 42 + 128, 0, (byte)'R',(byte)'4',(byte)'2',0,0,0,0,0,0,0,0,0,0,0,0,0,//Run Name 21
			0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0xa8, 0x41, 0, 0, 0xb0, 0x41, 0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0xa8, 0x41, 0, 0, 0xb0, 0x41,//45
			0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0xa8, 0x41, 0, 0, 0xb0, 0x41, 0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0xa8, 0x41,//65
			1, 0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0xa8, 0x41, 0, 0, 0xb0, 0x41, 0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0xa8, 0x41, //Tap Location 86
			0, 0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0xa8, 0x41, 0, 0, 0xb0, 0x41,	//Pressure Transmitter Type 99
			7, 10, 19, 9, 8, 7, 0, 0 };
		byte[] btaR42_6 = { 0x55, 1, 107, 42 + 128, 0, (byte)'R',(byte)'4',(byte)'2',0,0,0,0,0,0,0,0,0,0,0,0,0,//Run Name 21
			0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0xa8, 0x41, 0, 0, 0xb0, 0x41, 0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0xa8, 0x41, 0, 0, 0xb0, 0x41,//45
			0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0xa8, 0x41, 0, 0, 0xb0, 0x41, 0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0xa8, 0x41,//65
			1, 0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0xa8, 0x41, 0, 0, 0xb0, 0x41, 0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0xa8, 0x41, //Tap Location 86
			0, 0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0xa8, 0x41, 0, 0, 0xb0, 0x41,	//Pressure Transmitter Type 99
			7, 10, 19, 9, 8, 7, 0, 0 };
		byte[] btaR42_5 = { 0x55, 1, 64, 42 + 128, 0, (byte)'R',(byte)'4',(byte)'2',0,0,0,0,0,0,0,0,0,0,0,0,0,//Run Name 21
			0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0xa8, 0x41, 0, 0, 0xb0, 0x41, 0xA4, 0x70, 0x9D, 0x3F, 0xCD, 0xCC, 0x8C, 0x3F, 0xCD, 0xCC, 0x0C, 0x40,
			0x33, 0x33, 0x53, 0x40, 0xCD, 0xCC, 0x8C, 0x40,
			1, 2, 3, // Scaling Factor 
			7, 10, 19, 9, 8, 7, 0, 0 };

		byte[] btaR46 = { 0x55, 1, 0, 46 + 128, 1, 0, 0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0, 0xa8, 0x41, 0, 0, 0, 0xb0, 0x41, 7, 0xa, 19, 9, 8, 7, 0, 0 };
		byte[] btaR47 = { 0x55, 1, 0, 47 + 128, 1, 0, 0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0, 0xa8, 0x41, 0, 0, 0, 0xb0, 0x41, 7, 0xa, 19, 9, 8, 7, 0, 0 };

		FCOMserver.DMessageShow ShowMess;
		FCOMserver.EDevs DevType;

		#endregion

		//wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww

		public CSuperflo (FCOMserver.DMessageShow ShowFunc, FCOMserver.EDevs DevType)
		{
			ShowMess = ShowFunc;
			this.DevType = DevType;
			switch (DevType)
			{
			case FCOMserver.EDevs.SF21RU5D:	btaR1 = btaR1_5;
				break;
			case FCOMserver.EDevs.SF21RU6D:	btaR1 = btaR1_6;
				break;
			case FCOMserver.EDevs.SF21RU7С:	btaR1 = btaR1_7;
				break;
			default:				break;
			}
		}
		//_________________________________________________________________________

		public byte[] HandlingRecieve (byte[] BufRX, int iLenData)
		{
			if (CRCisTrue (BufRX, iLenData) == false)
			{
				ShowMess ("Err CRC");
				return new byte[0];
			}
			byte[] btaRet;
			switch (BufRX[(int)ER46request.FuncCode])
			{
			case 1: HandlR1 (BufRX); btaRet = btaR1; ShowMess ("\tR1", false); break;
			case 4: HandlR4 (BufRX); btaRet = btaR4; ShowMess ("\tR4", false); break;
			case 20: HandlR20 (BufRX); btaRet = btaR20; ShowMess ("\tR20", false); break;
			case 21: HandlR21 (BufRX); btaRet = btaR21; ShowMess ("\tR21", false); break;
			case 22: HandlR22 (BufRX); btaRet = btaR22; ShowMess ("\tR22", false); break;
			case 23: HandlR23 (BufRX); btaRet = btaR23; ShowMess ("\tR23", false); break;
			case 42: HandlR42 (BufRX);
				if (DevType == FCOMserver.EDevs.SF21RU5D) btaRet = btaR42_5;
				else if (DevType == FCOMserver.EDevs.SF21RU6D) btaRet = btaR42_6;
				else btaRet = btaR42_7;
					ShowMess ("\tR42", false); break;
			case 43: HandlR43 (BufRX); btaRet = btaR43; ShowMess ("\tR43", false); break;
			case 46: HandlR46 (BufRX); btaRet = btaR46; ShowMess ("\tR46", false); break;
			case 47: HandlR47 (BufRX); btaRet = btaR47; ShowMess ("\tR47", false); break;
			default: return new byte[0];
			}

			btaRet[(int)ER46_47answer.MessLen] = (byte)btaRet.Length;
			CalcCRC (btaRet);
			ShowMess ("TX: " + Global.ByteArToStr (btaRet, 0, btaRet.Length));
			return btaRet;
		}
		//_________________________________________________________________________
		private void HandlR22 (byte[] bufRX)
		{
			btaR22[4] = btaR22[14] = bufRX[(int)ER46_47answer.RunNumber];
		}
		//_________________________________________________________________________
		private void HandlR23 (byte[] bufRX)
		{
			btaR22[4] = btaR22[15] = bufRX[(int)ER46_47answer.RunNumber];
		}
		//_________________________________________________________________________
		private void HandlR1 (byte[] bufRX)
		{
			DateTime DT = DateTime.Now;
			int iPosDate = 57;
			btaR1[iPosDate] = (byte)DT.Day;
			btaR1[++iPosDate] = (byte)DT.Month;
			btaR1[++iPosDate] = (byte)(DT.Year - 2000);
			btaR1[++iPosDate] = (byte)DT.Hour;
			btaR1[++iPosDate] = (byte)DT.Minute;
			btaR1[++iPosDate] = (byte)DT.Second;
		}
		//_________________________________________________________________________
		private void HandlR4 (byte[] bufRX)
		{
			btaR4[(int)ER46_47answer.RunNumber] = bufRX[(int)ER46_47answer.RunNumber];
		}
		//_________________________________________________________________________
		private void HandlR20 (byte[] bufRX)
		{
			btaR20[(int)ER46_47answer.RunNumber] = bufRX[(int)ER46_47answer.RunNumber];
		}
		//_________________________________________________________________________
		private void HandlR21 (byte[] bufRX)
		{
			btaR21[(int)ER46_47answer.RunNumber] = bufRX[(int)ER46_47answer.RunNumber];
		}
		//_________________________________________________________________________
		private void HandlR42 (byte[] bufRX)
		{
			byte[] btaR42;
			int iPosDate;
			if (DevType == FCOMserver.EDevs.SF21RU5D)
			{
				btaR42 = btaR42_5;
				iPosDate = 56;
			}
			else if (DevType == FCOMserver.EDevs.SF21RU6D)
			{
				btaR42 = btaR42_6;
				iPosDate = 99;
			}
			else
			{
				btaR42 = btaR42_7;
				iPosDate = 99;
			}
			btaR42[(int)ER46_47answer.RunNumber] = bufRX[(int)ER46_47answer.RunNumber];
			DateTime DT = DateTime.Now;
			btaR42[iPosDate] = (byte)DT.Day;
			btaR42[++iPosDate] = (byte)DT.Month;
			btaR42[++iPosDate] = (byte)(DT.Year - 2000);
			btaR42[++iPosDate] = (byte)DT.Hour;
			btaR42[++iPosDate] = (byte)DT.Minute;
			btaR42[++iPosDate] = (byte)DT.Second;
		}
		//_________________________________________________________________________
		private void HandlR43 (byte[] BufRX)
		{
			byte[] btaR42;
			if (DevType == FCOMserver.EDevs.SF21RU5D)
			{
				btaR42 = btaR42_5;
			}
			else if (DevType == FCOMserver.EDevs.SF21RU6D)
			{
				btaR42 = btaR42_6;
			}
			else
			{
				btaR42 = btaR42_7;
			}
			btaR42[(int)ER42answer.RunNumber] = BufRX[(int)ER43request.RunNumber];
			Global.Append (BufRX, (int)ER43request.RunName, btaR42, (int)ER43request.RunName,	ER43request.CRCh - ER43request.RunName);
		}
		//_________________________________________________________________________
		private void HandlR47 (byte[] BufRX)
		{
			Global.Append (BufRX, (int)ER47request.RunNumber, btaR46, (int)ER46_47answer.RunNumber,
				ER47request.SAFE_CRC - ER47request.RunNumber);
			Global.Append (BufRX, (int)ER47request.RunNumber, btaR47, (int)ER46_47answer.RunNumber,
				ER47request.SAFE_CRC - ER47request.RunNumber);

			// Если запись константы 
			if (btaR46[(int)ER46_47answer.DiffPressFlag] == 2)
			{
				btaR46[(int)ER46_47answer.DiffPressFlag] = 0;
				btaR47[(int)ER46_47answer.DiffPressFlag] = 0;
			}
			if (btaR46[(int)ER46_47answer.StatPressFlag] == 2)
			{
				btaR46[(int)ER46_47answer.StatPressFlag] = 0;
				btaR47[(int)ER46_47answer.StatPressFlag] = 0;
			}
			if (btaR46[(int)ER46_47answer.TemperFlag] == 2)
			{
				btaR46[(int)ER46_47answer.TemperFlag] = 0;
				btaR47[(int)ER46_47answer.TemperFlag] = 0;
			}
			//btaR46[(int)ER46_47Answer.DiffPressFlag] = [(int)ER47Request.DiffPressFlag];
			//SetFloat (BufRX, (int)ER47Request.DiffPressVal, btaR46, (int)ER46_47Answer.DiffPressVal);
			//btaR46[(int)ER46_47Answer.StatPressFlag] = BufRX[(int)ER47Request.StatPressFlag];
			//SetFloat (BufRX, (int)ER47Request.StatPressVal, btaR46, (int)ER46_47Answer.StatPressVal);
			//btaR46[(int)ER46_47Answer.TemperFlag] = BufRX[(int)ER47Request.TemperFlag];
			//SetFloat (BufRX, (int)ER47Request.TemperVal, btaR46, (int)ER46_47Answer.TemperVal);
		}
		//_________________________________________________________________________
		private void SetFloat (byte[] BufRX, int iSourFrom, byte[] btDest, int iFromDest)
		{
			try
			{
				Global.Append (BufRX, iSourFrom, btDest, iFromDest, 4);
			}
			catch (Exception exc)
			{
				ShowMess ($"iSourFrom={iSourFrom}. {exc.Message}");//{Environment.NewLine}{exc.StackTrace}
			}
		}
		//_________________________________________________________________________
		private void HandlR46 (byte[] bufRX)
		{
			btaR46[(int)ER46_47answer.RunNumber] = bufRX[(int)ER46request.RunNumber];
			DateTime DT = DateTime.Now;
			btaR46[(int)ER46_47answer.Day] = (byte)DT.Day;
			btaR46[(int)ER46_47answer.Month] = (byte)DT.Month;
			btaR46[(int)ER46_47answer.Year] = (byte)(DT.Year - 2000);
			btaR46[(int)ER46_47answer.Hour] = (byte)DT.Hour;
			btaR46[(int)ER46_47answer.Min] = (byte)DT.Minute;
			btaR46[(int)ER46_47answer.Sec] = (byte)DT.Second;
		}
		//_________________________________________________________________________
		void CalcCRC (byte[] btaBuf)
		{
			int LenByCRC = btaBuf.Length - 2;
			ushort ui16 = Global.CRC (btaBuf, LenByCRC, LenByCRC, Global.Table8005);
			btaBuf[LenByCRC] = (byte)(ui16 & 0xFF);
			btaBuf[LenByCRC + 1] = (byte)(ui16 >> 8);
		}
		//_________________________________________________________________________
		bool CRCisTrue (byte[] btaBuf, int LenByCRC)
		{
			ushort ui16 = Global.CRC (btaBuf, LenByCRC, LenByCRC, Global.Table8005);
			return ui16 == 0;
		}
	}
}
