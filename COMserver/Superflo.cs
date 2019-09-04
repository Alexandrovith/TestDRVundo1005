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
		enum ER34_5answer
		{
			Synch, Addr, MessLen, FuncCode, RelayFunction, CycleTime, Contract_Hour, LowBatteryAlarmLimit, RadioKeyDelay=11, OpenSmallRunFlowRateforTubeSwitching=15,
			CloseBigRunFlowRateforTubeSwitching=19, PulseDurationforTubeSwitching=23, TubeSwitchingDeadBand=27, CRCh=34, CRCl
		}
		enum ER34_6_7answer
		{
			Synch, Addr, MessLen, FuncCode, RelayFunction, CycleTime, Contract_Hour, LowBatteryAlarmLimit, RadioKeyDelay=11, OpenSmallRunFlowRateforTubeSwitching=15,
			CloseBigRunFlowRateforTubeSwitching=19, RunsTotalSamplerVolume=23, PulseDurationforTubeSwitching=27, TubeSwitchingDeadBand=31, CRCh=35, CRCl
		}
		enum ER35_5request
		{
			Synch, Addr, MessLen, FuncCode, SuperFlo_Write_Password, Relay_Function=20, Cycle_Time, Contract_Hour, Low_Battery_AlarmLimit, Radio_Key_Delay=27,Open_Small_Run_Flow_Rate_for_Tube_Switching=31, 
			Close_Big_Run_Flow_Rate_for_Tube_Switching=35, Pulse_Duration_for_Tube_Switching=39, Tube_Switching_Dead_Band=43, CRCh=50, CRCl
		}
		enum ER35_6_7request
		{
			Synch, Addr, MessLen, FuncCode, SuperFlo_Write_Password, Relay_Function=20, Cycle_Time, Contract_Hour, Low_Battery_AlarmLimit, Radio_Key_Delay=27,Open_Small_Run_Flow_Rate_for_Tube_Switching=31, 
			Close_Big_Run_Flow_Rate_for_Tube_Switching=35, Runs_Total_Sampler_Volume =39, Pulse_Duration_for_Tube_Switching=43, Tube_Switching_Dead_Band=47, CRCh=51, CRCl
		}

		enum ER42request { Synch, Addr, MessLen, FuncCode ,RunNumber, CRCh, CRCl }
		enum ER42answer
		{
			Synch, Addr, MessLen, FuncCode, RunNumber, RunName,
			GasDensity = 21, MolePercentCO = 25, MolePercentN2 = 29, PipeInsideDiameter = 33, OrificeDiameter = 37, AtmosphericPressure = 41, LowDPCutoff = 45, a2Pipe = 49,
			a1Pipe = 53, a0Pipe  = 57, DPSwitchingLevel = 61, TapLocation = 65, a2Orifice, a1Orifice = 70, a0Orifice = 74, RoughnessRadius= 78, RoundingRadiusrn = 82,
			PressureTransmitterType =86, IntercheckInterval,SpecificEnergy = 91, dPLowLevelAlarmLimit=95,
			Month=99, Day, Year, Hour, Min, Sec, CRCh, CRCl
		}

		enum ER43request
		{
			Synch, Addr, MessLen, FuncCode, RunNumber, SuperFloWritePassword, RunName = 21,
			GasDensity = 37, MolePercentCO = 41, MolePercentN2 = 45, PipeInsideDiameter =49, OrificeDiameter=53,AtmosphericPressure = 57, LowDPCutoff = 61, a2Pipe = 65,
			a1Pipe = 69, a0Pipe = 73, DPSwitchingLevel = 77, TapLocation = 81, a2Orifice, a1Orifice = 86, a0Orifice = 90, RoughnessRadius = 94, RoundingRadius = 98,
			PressureTransmitterType = 102, IntercheckInterval, SpecificEnergy = 107, dPLowLevelAlarmLimit =111, CRCh, CRCl
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

		enum  ER4answer
		{
			Synch, Addr, MessLen, FuncCode, RunNumber, DiffPress, StatPress = DiffPress + 4, Temper = StatPress + 4,
			Energy = Temper + 4, InstantaneousFlowRt = Temper + 4, CurrDayFlowTotal = InstantaneousFlowRt + 4,
			YesterdayFlowTotal = CurrDayFlowTotal + 4, TotalAccumulatedFlow = YesterdayFlowTotal + 4, K = TotalAccumulatedFlow + 4,
			Zc = K + 4, HsActual = Zc +4, AbsPressure = HsActual+4, HsHigher=AbsPressure+4, PipeDiameter=HsHigher+4, OrifDiameter=PipeDiameter+4,
			KtPipe=OrifDiameter+4, KtOrifice=KtPipe+4, Beta=KtOrifice+4, ActualGasDensity=Beta+4, GasViscosity=ActualGasDensity+4, Kappa=GasViscosity+4,
			Epsilon=Kappa+4, C=Epsilon+4, Kp=C+4, Ksh=Kp+4, rk=Ksh+4, Re=rk+4, Ppk=Re+4, Tpk=Ppk+4, PrevHourVolume=Tpk+4, PrevDayVolume=PrevHourVolume+4,
			PrevMinuteVolume=PrevDayVolume+4, Month=PrevMinuteVolume+4, Day, Year, Hour, Minute, Second,
			CRCh, CRCl
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

		byte[] btaR4 = new byte[114] { 0x55, 1, 114, 4 + 128, 0,
		0xCD,0xCC,0x8C,0x3F, 0xCD,0xCC,0x0C,0x40, 0x33,0x33,0x53,0x40, 0xCD,0xCC,0x8C,0x40, 0,0x00,0xB0,0x40, 0x33,0x33,0xD3,0x40,
		0xCD,0xCC,0x8C,0x3F, 0xCD,0xCC,0x0C,0x40, 0x33,0x33,0x53,0x40, 0xCD,0xCC,0x8C,0x40, 0,0x00,0xB0,0x40, 0x33,0x33,0xD3,0x40,
		0xCD,0xCC,0x8C,0x3F, 0xCD,0xCC,0x0C,0x40, 0x33,0x33,0x53,0x40, 0xCD,0xCC,0x8C,0x40, 0,0x00,0xB0,0x40, 0x33,0x33,0xD3,0x40,
		0xCD,0xCC,0x8C,0x3F, 0xCD,0xCC,0x0C,0x40, 0x33,0x33,0x53,0x40, 0xCD,0xCC,0x8C,0x40, 0,0x00,0xB0,0x40, 0x33,0x33,0xD3,0x40,
		0xCD,0xCC,0x8C,0x3F, 0, 1,2,19,4,5,6, 0,0 };

		byte[] btaR20 = { 0x55, 1, 37, 20 + 128, 0, 0, 0, 7, 2, 19, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
		byte[] btaR21 = { 0x55, 1, 37, 21 + 128, 0, 0, 0, 7, 2, 19, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
		byte[] btaR22 = { 0x55, 1, 26, 22 + 128, 0, 0, 0, 7, 2, 19, 0, 0, 0, 0, 0, 0xCD, 0xCC, 0x0C, 0x40, 0x33, 0x33, 0x53, 0x40, 0, 0 };
		byte[] btaR23 = { 0x55, 1, 23, 23 + 128, 0, 0, 0, 7, 2, 19, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

		byte[] btaR34_5 = new byte[36] { 0x55, 1, 114, 34 + 128, 0, 1, 9, 0xCD,0xCC,0x8C,0x3F, 0xCD,0xCC,0x0C,0x40, 0x33,0x33,0x53,0x40,
			0xCD,0xCC,0x8C,0x40, 0x00,0x00,0xB0,0x40, 0,3, 0,0, 0,1,3, 0,0
		};
		byte[] btaR34_6_7 = new byte[37] { 0x55, 1, 114, 34 + 128, 0, 1, 9, 0xCD,0xCC,0x8C,0x3F, 0xCD,0xCC,0x0C,0x40, 0x33,0x33,0x53,0x40,
			0xCD,0xCC,0x8C,0x40, 0x00,0x00,0xB0,0x40, 0x33,0x33,0xD3,0x40, 0,3, 0,0, 0,0
		};

		byte[] btaR35 = { 0x55, 1, 6, 35 + 128, 0, 0 };
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

		byte[] btaR42;
		byte[] btaR34;

		//wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww

		public CSuperflo (FCOMserver.DMessageShow ShowFunc, FCOMserver.EDevs DevType)
		{
			ShowMess = ShowFunc;
			this.DevType = DevType;
			switch (DevType)
			{
			case FCOMserver.EDevs.SF21RU5D:
				btaR1 = btaR1_5;
				btaR42 = btaR42_5;
				btaR34 = btaR34_5;
				break;
			case FCOMserver.EDevs.SF21RU6D:
				btaR1 = btaR1_6;
				btaR42 = btaR42_6;
				btaR34 = btaR34_6_7;
				break;
			case FCOMserver.EDevs.SF21RU7С:
				btaR1 = btaR1_7;
				btaR42 = btaR42_7;
				btaR34 = btaR34_6_7;
				break;
			default: break;
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
			case 34: HandlR34 (BufRX); btaRet = btaR34; ShowMess ("\tR34", false); break;
			case 35: HandlR35 (BufRX); btaRet = btaR35; ShowMess ("\tR35", false); break;
			case 42: HandlR42 (BufRX); btaRet = btaR42; ShowMess ("\tR42", false); break;
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
		private void HandlR35 (byte[] bufRX)
		{
			if (DevType == FCOMserver.EDevs.SF21RU5D)
				Global.Append (bufRX, (int)ER35_5request.Relay_Function, btaR34, (int)ER34_5answer.RelayFunction, (int)(ER34_5answer.CRCh - ER34_5answer.RelayFunction));
			else Global.Append (bufRX, (int)ER35_6_7request.Relay_Function, btaR34, (int)ER34_6_7answer.RelayFunction, (int)(ER34_6_7answer.CRCh - ER34_6_7answer.RelayFunction));
		}
		//_________________________________________________________________________
		private void HandlR34 (byte[] bufRX)
		{
		}
		//_________________________________________________________________________
		private void HandlR22 (byte[] bufRX)
		{
			btaR22[4] = btaR22[14] = bufRX[(int)ER46_47answer.RunNumber];
		}
		//_________________________________________________________________________
		private void HandlR23 (byte[] bufRX)
		{
			btaR23[4] = btaR23[15] = bufRX[(int)ER46_47answer.RunNumber];
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
			ChangeParams (btaR4, new int[] { (int)ER4answer.AbsPressure, (int)ER4answer.ActualGasDensity, (int)ER4answer.Temper, (int)ER4answer.TotalAccumulatedFlow });
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
			int iPosDate;
			if (DevType == FCOMserver.EDevs.SF21RU5D)	{	iPosDate = 56; }
			else if (DevType == FCOMserver.EDevs.SF21RU6D) { iPosDate = 99;	}
			else { iPosDate = 99;	}

			ChangeParams (btaR42, new int[] { (int)ER42answer.AtmosphericPressure, (int)ER42answer.GasDensity, (int)ER42answer.MolePercentCO, (int)ER42answer.MolePercentN2 });
			btaR42[(int)ER42answer.RunNumber] = bufRX[(int)ER42request.RunNumber];
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
			btaR42[(int)ER42answer.RunNumber] = BufRX[(int)ER43request.RunNumber];
			Global.Append (BufRX, (int)ER43request.RunName, btaR42, (int)ER42answer.RunName, ER43request.CRCh - ER43request.RunName);
		}
		//_________________________________________________________________________

		const float VAL_MAX = 5.0f;
		const float VAL_MIN = 0.0123f;
		private const float VAL_INC = 0.1f;

		public bool bChageParams { get; set; }

		private void ChangeParams (byte[] bufRX, int[] iaPosVals)
		{
			if (bChageParams)
				foreach (var iPosVal in iaPosVals)
				{
					float fVal = BitConverter.ToSingle (bufRX, iPosVal);
					if (fVal > VAL_MAX)
						fVal = VAL_MIN;
					else fVal += VAL_INC;
					Global.Append (BitConverter.GetBytes (fVal), bufRX, iPosVal, sizeof (float));
				}
		}
		//_________________________________________________________________________
		private void HandlR47 (byte[] BufRX)
		{
			Global.Append (BufRX, (int)ER47request.RunNumber, btaR46, (int)ER46_47answer.RunNumber,
				ER47request.SAFE_CRC - ER47request.RunNumber);
			Global.Append (BufRX, (int)ER47request.RunNumber, btaR47, (int)ER46_47answer.RunNumber,
				ER47request.SAFE_CRC - ER47request.RunNumber);

			// Если запись константы - устанавливаем флаги в 0 
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
