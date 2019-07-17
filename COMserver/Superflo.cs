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
		enum ER43Request { Synch, Addr, MessLen, FuncCode, CRCh, CRCl }
		enum ER46Request { Synch, Addr, MessLen, FuncCode, RunNumber, CRCh, CRCl }
		enum ER47Request { Synch, Addr, MessLen, FuncCode, RunNumber, DiffPressFlag, DiffPressVal,
			StatPressFlag = DiffPressVal + 4, StatPressVal, TemperFlag = StatPressVal + 4, TemperVal,
			SAFE_CRC = TemperVal + 4, CRCh = SAFE_CRC + 2, CRCl }
		enum ER46_47Answer
		{
			Synch, Addr, MessLen, FuncCode, RunNumber, DiffPressFlag, DiffPressVal,
			StatPressFlag = DiffPressVal + 4, StatPressVal, TemperFlag = StatPressVal + 4,
			TemperVal, Month = TemperVal + 4, Day, Year, Hour, Min, Sec, CRCh, CRCl
		}

		byte[] btaR43 = { 0x55, 1, 0, 43 + 128, 0, 0 };
		byte[] btaR42 = { 0x55, 1, 0, 42 + 128, 1, (byte)'R',(byte)'4',(byte)'2',0,0,0,0,0,0,0,0,0,0,0,0,0,//Run Name 21
			0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0xa8, 0x41, 0, 0, 0xb0, 0x41, 0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0xa8, 0x41, 0, 0, 0xb0, 0x41,//45
			0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0xa8, 0x41, 0, 0, 0xb0, 0x41, 0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0xa8, 0x41,//65
			1, 0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0xa8, 0x41, 0, 0, 0xb0, 0x41, 0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0xa8, 0x41, //Tap Location 86
			0, 0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0xa8, 0x41, 0, 0, 0xb0, 0x41,	//Pressure Transmitter Type 99
			7, 10, 19, 9, 8, 7, 0, 0 };

		byte[] btaR46 = { 0x55, 1, 0, 46 + 128, 1, 0, 0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0, 0xa8, 0x41, 0, 0, 0, 0xb0, 0x41, 7, 0xa, 19, 9, 8, 7, 0, 0 };
		byte[] btaR47 = { 0x55, 1, 0, 47 + 128, 1, 0, 0xA4, 0x70, 0x9D, 0x3F, 0, 0, 0, 0xa8, 0x41, 0, 0, 0, 0xb0, 0x41, 7, 0xa, 19, 9, 8, 7, 0, 0 };

		FCOMserver.DMessageShow ShowMess;

		//wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww

		public CSuperflo (FCOMserver.DMessageShow ShowFunc)
		{
			ShowMess = ShowFunc;
		}
		//_________________________________________________________________________

		public byte[] HandlingRecieve (byte[] BufRX, int iLenData)
		{
			byte[] btaRet;
			switch (BufRX[(int)ER46Request.FuncCode])
			{
			case 42: btaRet = btaR42; ShowMess ("R42"); break;
			case 43: btaRet = btaR43; ShowMess ("R43"); break;
			case 46: HandlR46 (BufRX); btaRet = btaR46; ShowMess ("R46"); break;
			case 47: HandlR47 (BufRX); btaRet = btaR47; ShowMess ("R47"); break;
			default: return new byte[0];
			}

			btaRet[(int)ER46_47Answer.MessLen] = (byte)btaRet.Length;
			CalcCRC (btaRet);
			ShowMess ("TX: " + Global.ByteArToStr (btaRet, 0, btaRet.Length));
			return btaRet;
		}
		//_________________________________________________________________________
		private void HandlR47 (byte[] BufRX)
		{
			Global.Append (BufRX, (int)ER47Request.RunNumber, btaR46, (int)ER46_47Answer.RunNumber,
				ER47Request.SAFE_CRC - ER47Request.RunNumber);
			Global.Append (BufRX, (int)ER47Request.RunNumber, btaR47, (int)ER46_47Answer.RunNumber,
				ER47Request.SAFE_CRC - ER47Request.RunNumber);
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
			btaR46[(int)ER46_47Answer.RunNumber] = bufRX[(int)ER46Request.RunNumber];
			DateTime DT = DateTime.Now;
			btaR46[(int)ER46_47Answer.Day] = (byte)DT.Day;
			btaR46[(int)ER46_47Answer.Month] = (byte)DT.Month;
			btaR46[(int)ER46_47Answer.Year] = (byte)(DT.Year - 2000);
			btaR46[(int)ER46_47Answer.Hour] = (byte)DT.Hour;
			btaR46[(int)ER46_47Answer.Min] = (byte)DT.Minute;
			btaR46[(int)ER46_47Answer.Sec] = (byte)DT.Second;
		}
		//_________________________________________________________________________
		void CalcCRC (byte[] btaBuf)
		{
			int LenByCRC = btaBuf.Length - 2;
			ushort ui16 = Global.CRC (btaBuf, LenByCRC, btaBuf.Length - 2, Global.Table8005);
			btaBuf[LenByCRC] = (byte)(ui16 & 0xFF);
			btaBuf[LenByCRC + 1] = (byte)(ui16 >> 8);
		}
	}
}
