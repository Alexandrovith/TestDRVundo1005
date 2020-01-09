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
		#region //xxxxxxxxxxxxxxxxxxxxx    П О С Т О Я Н Н Ы Е      xxxxxxxxxxxxxxxxxxxxxxxxx

		const int iQuantRow = 2;
		const int iLenData = 40;
		const int iNumByte = iLenData*iQuantRow;
		#endregion

		#region //xxxxxxxxxxxxxxxxxxxxx    П Е Р Е М Е Н Н Ы Е      xxxxxxxxxxxxxxxxxxxxxxxxx

		int iNext = 1;
		double dIncDate = 0.0;
		int iCountComment = 1;
		int iSizeByCRC = 0;
		int iPos;
		DateTime DT = DateTime.Now;
		int КодСобытия = 0;
		int ТипСобытия = 0;
		#endregion

		//xxxxxxxxxxxxxxxxxxxxxxxx       К О Д        xxxxxxxxxxxxxxxxxxxxxxxxxxxxx
		public CDevMAG (FTCPserver Prnt) : base (Prnt)
		{
			iBeginData = 0;	// (int)EHeadMOD_TCP.AddrSlave;
		}
		//_________________________________________________________________________
		enum EDTbase { Month, YearH, Year, Day, Hour, SIZE };
		byte[] btaDTbase = new byte[(int)EDTbase.SIZE];
		private int iCountNext;

		override public bool GetAnswer (ref byte[] btaTX, byte[] btaRX, ref int iNumPartOfAnswerBySend)
		{
			if (btaRX[iBeginData + (int)MODBUS3_RESP.Func] == 16)
			{
				btaTX = new byte[(int)MODBUS16_ANSW.SIZE/* + (int)EHeadMOD_TCP.AddrSlave*/];
				iSizeByCRC = btaTX.Length - 2;
				iPosTX = (int)MODBUS16_ANSW.CRCh;
				int iStart = /*(int)EHeadMOD_TCP.AddrSlave + */(int)MODBUS16_RESP.Data;
				if (btaRX[/*(int)EHeadMOD_TCP.AddrSlave + */(int)MODBUS3_RESP.RegStartL] == 170)
				{
				}
				else
				{
					btaDTbase[(int)EDTbase.Hour] = btaRX[iStart + (int)CArchHourDayMag.ERequestSetups.HourL];
					btaDTbase[(int)EDTbase.Day] = btaRX[iStart + (int)CArchHourDayMag.ERequestSetups.DayL];
					btaDTbase[(int)EDTbase.Month] = btaRX[iStart + (int)CArchHourDayMag.ERequestSetups.MonthL];
					btaDTbase[(int)EDTbase.YearH] = btaRX[iStart + (int)CArchHourDayMag.ERequestSetups.YearH];
					btaDTbase[(int)EDTbase.Year] = btaRX[iStart + (int)CArchHourDayMag.ERequestSetups.YearL];
				}

				Parent.OutToWind ("ЗАПІС, атрымана:" + Global.ByteArToStr (btaRX));
			}
			else if (btaRX[iBeginData + (int)CArchInterferMAG.EResponse.Func] == 107)      // Вмешательства
			{
				Parent.OutToWind ("Вмешательства", false);
				iNumPartOfAnswerBySend = 50;
				double dStartDate = Global.DateTimeToDouble (new DateTime (2018, 1, 15));
				DateTime StartDate = Global.DoubleToDateTime (dStartDate);
				Parent.OutToWind ("Вмешательства. StartDate " + StartDate.ToString(), false);
				const int iQuantRows = 6;
				int iNumByte = iQuantRows * iLenData;
				btaBufTX = new byte[iBeginData + 6 + iNumByte + 2];
				btaTX = btaBufTX;

				btaBufTX[iBeginData + (int)CArchInterferMAG.EResponse.QuantByteData] = (byte)(1 + 2 + iNumByte);
				btaBufTX[iBeginData + (int)CArchInterferMAG.EResponse.Next] = (byte)iNext;
				if (iCountNext == 3)
				{
					iCountNext = 0;
					iNext = 0;
				}
				else
				{
					iCountNext++;
					iNext = 1;
				}
				btaBufTX[iBeginData + (int)CArchInterferMAG.EResponse.QuantRowHi] = 0;
				btaBufTX[iBeginData + (int)CArchInterferMAG.EResponse.QuantRowLo] = iQuantRows;

				DateTime EndDate = Global.DoubleToDateTime (dStartDate) + new TimeSpan(2, 2, 0, 0);
				dIncDate = 0.1;
				for (int iRow = 0; iRow < iQuantRows; iRow++)
				{
					int iStartData = iBeginData + (int)CArchInterferMAG.EResponse.Data + iRow * iLenData;
					dStartDate += dIncDate;
					if (StartDate > EndDate)
					{
						btaBufTX[iBeginData + (int)CArchInterferMAG.EResponse.Next] = 0;
						btaBufTX[iBeginData + (int)CArchInterferMAG.EResponse.QuantRowLo] = (byte)(iRow);
						break;
					}
					byte[] btaDateE = BitConverter.GetBytes (dStartDate);
					int i = 0;
					foreach (var item in btaDateE)
					{
						btaBufTX[(int)CArchInterferMAG.EStructRow.DateE + iStartData + i] = item;
						i++;
					}

					btaBufTX[(int)CArchInterferMAG.EStructRow.CodeE + iStartData] = (byte)(КодСобытия);       // Код события
					btaBufTX[(int)CArchInterferMAG.EStructRow.TypeE + iStartData] = (byte)(ТипСобытия);
					if (КодСобытия == 11)
					{
						КодСобытия = 0;
					}
					else
					{
						КодСобытия++;
					}
					// Подтип события
					btaBufTX[(int)CArchInterferMAG.EStructRow.SubTypeE + iStartData] = 2;
					btaBufTX[(int)CArchInterferMAG.EStructRow.SubTypeE + iStartData] = 0;
					string asNameE = "Примечание " + iCountComment++;
					byte[] btaNameE = Global.EncodingCurr.GetBytes (asNameE);
					i = iStartData;
					foreach (var item in btaNameE)
					{
						btaBufTX[i + (int)CArchInterferMAG.EStructRow.Comment] = item;
						i++;
					}
				}
			}
			else if (btaRX[/*(int)EHeadMOD_TCP.AddrSlave + */(int)MODBUS3_RESP.RegStartL] == (int)CArchHourDayMag.EAddrRegs.RecordTime)			 
			{                                                                         // Часовой/сут
				Parent.OutToWind ("Часовой/сут", false);
				const int iQuantByteDT = 12;
				int iQuantBites = ((btaRX[(int)MODBUS3_RESP.NumRegH] << 8) +	btaRX[(int)MODBUS3_RESP.NumRegL]) * 2;
				btaTX = new byte[(int)MODBUS3_ANSW.Data + iQuantBites + 2];
				iPos = (int)MODBUS3_ANSW.Data;
				// DT 
				btaTX[iPos++] = 0;
				btaTX[iPos++] = btaDTbase[(int)EDTbase.Day];
				btaTX[iPos++] = 0;
				btaTX[iPos++] = btaDTbase[(int)EDTbase.Month];
				btaTX[iPos++] = btaDTbase[(int)EDTbase.YearH];
				btaTX[iPos++] = btaDTbase[(int)EDTbase.Year];
				btaTX[iPos++] = 0;
				btaTX[iPos++] = btaDTbase[(int)EDTbase.Hour];
				btaTX[iPos++] = 0;
				btaTX[iPos++] = 0;
				btaTX[iPos++] = 0;
				btaTX[iPos++] = 0;

				// Data 
				int i = 0;
				try
				{
					float[] Params = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49 };
					Random R = new Random ();
					for (; i < (iQuantBites - iQuantByteDT) / 4; i++)
					{
						FillByVal (Params[i] + R.Next (100) / 100.0f, ref btaTX, ref iPos, ESequent.два_байта);
					}
				}
				catch (Exception)
				{
					Parent.OutToWind ("EE " + i + " " + (iQuantBites - iQuantByteDT) / 4);
					throw;
				}
				//foreach (var item in Params)
				//{
				//	FillByVal (item + R.Next(100) / 100.0f, ref btaTX, ref iPos);
				//}
			}
			else																											// Iмгненныя, чытанне 
			{
				Parent.OutToWind ("Iмгненныя, чытанне");
				int iQuantBites = (btaRX[(int)MODBUS3_RESP.NumRegH] * 256 + 
																			btaRX[(int)MODBUS3_RESP.NumRegL]) * 2;
				btaTX = new byte[(int)MODBUS3_ANSW.Data + iQuantBites + 2];
				iPos = (int)MODBUS3_ANSW.NumByteData;	//EHeadMOD_TCP.NumBytes;
				btaTX[iPos] = (byte)iQuantBites;

				// DT 
				if (btaRX[(int)MODBUS3_RESP.RegStartL] == 170)
				{
					double dDT = DateTime.Now.AddDays(1).ToOADate ();
					byte[] btaDT = BitConverter.GetBytes (dDT).Reverse().ToArray();
					foreach (var item in btaDT)
					{
						btaTX[++iPos] = item;
					}					
				}
				else
				{
					float[] faVals = { 0.1f, 0.11f, 0.12f, 0.13f, 0.14f, 0.15f, 0.16f, 0.17f, 0.18f, 0.19f, 0.20f, 0.21f, 0.22f, 0.23f, 0.24f, 0.25f, 0.26f, 0.27f, 0.28f, 0.29f,
						0.30f, 0.31f, 0.32f, 0.33f, 0.34f, 0.35f, 0.36f, 0.37f, 0.38f, 0.39f, 0.40f, 0.41f, 0.42f, 0.43f, 0.44f, 0.45f, 0.46f, 0.47f, 0.48f, 0.49f, 0.50f,
					0.51f, 0.52f, 0.53f, 0.54f, 0.55f, 0.56f, 0.57f, 0.58f, 0.59f, 0.60f, 0.61f, 0.62f, 0.63f, 0.64f, 0.65f, 0.66f, 0.67f, 0.68f, 0.69f, 0.70f, 0.71f, 0.72f, 0.73f, 0.74f };
					++iPos;
					for (int i = 0; i < iQuantBites / 4; i++)
					{
						FillByVal (faVals[i], ref btaTX, ref iPos, ESequent.два_байта);
					}
				}

			}

			FillHead_CRC(btaTX, ref btaRX, btaTX.Length - 2, 0);
			//FillHead_CRC (btaTX, btaRX, iSizeByCRC, 0, (int)MODBUS3_ANSW.Func + (int)EResponseMOD_TCP.Func);
			return false;
		}
		void FillHead(byte[] btaTX, byte[] btaRX)
		{
			for (int i = 0; i < (int)MODBUS3_ANSW.NumByteData/* + (int)EHeadMOD_TCP.AddrSlave*/; i++)
			{
				btaTX[i] = btaRX[i];
			}
		}
	}
}
