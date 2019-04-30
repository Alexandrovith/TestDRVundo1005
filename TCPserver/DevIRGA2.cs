///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

///~~~~~~~~~	Проект:			Слушатель порта TCP
///~~~~~~~~~	Прибор:			Все описанные приборы
///~~~~~~~~~	Модуль:			Эмулятор прибора ИРГА-2
///~~~~~~~~~	Разработка:	Демешкевич С.А.
///~~~~~~~~~	Дата:				13.12.2016

///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

using System;
using System.Collections.Generic;
using System.Linq;
using AXONIM.CONSTS;
using AXONIM.BelTransGasDRV;
using System.Threading;

namespace TestDRVtransGas.TCPserver
{
	public class CDevIRGA2 : CTCPdevice
	{
		//xxxxxxxxxxxxxxxxxxxxx    П О С Т О Я Н Н Ы Е      xxxxxxxxxxxxxxxxxxxxxxx

		

		//xxxxxxxxxxxxxxxxxxxxx    П Е Р Е М Е Н Н Ы Е      xxxxxxxxxxxxxxxxxxxxxxx
		
		byte btFlags = 0;
		byte btNS = (byte)'O';
		int iCanal = 1;

		//xxxxxxxxxxxxxxxxxxxxxxxx       К О Д        xxxxxxxxxxxxxxxxxxxxxxxxxxxxx
		public CDevIRGA2 (FTCPserver Prnt) : base (Prnt)
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

		DateTime DTCurr = DateTime.Now;
    object oПолучОтвет = new object ();
		override public bool GetAnswer (ref byte[] btaTX, byte[] btaRX)
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
        else if (btaRX[0] == Neg2 && btaRX[1] == 2 && btaRX[(int)TBufCommIRGA2.EComWrFHP.WrParN] == Neg52
          && btaRX[(int)TBufCommIRGA2.EComWrFHP.WrPar] == 0x52 &&
          btaRX[(int)TBufCommIRGA2.EComWrFHP.NbytesN] == Neg4 && btaRX[(int)TBufCommIRGA2.EComWrFHP.Nbytes] == 4)
        {                                   // Запись ФХП 
          btaBufTX = new byte[1];
          btaBufTX[0] = (byte)'K';
          goto lbExit;
        }
        //else if (btaRX[0] == 'R')     // Чтение ФХП старое 
        else if (btaRX[0] == 0xFE && btaRX[1] == 1 && btaRX[2] == 0xAD && btaRX[3] == 0x52 &&
          btaRX[7] == 4)
        //if (btaRX[1] == 0x1 && btaRX[3] == 0x52 && btaRX[5] == 0x17 && btaRX[7] == 0x14)
        {
          FillФХП (ref iPos);
          goto lbExit;
          //iSizeByCRC = 23;				iBeginBuf = 0;
        }
        else if (btaRX[0] == 'W')     // Запись ФХП старая 
        {
          btaBufTX = new byte[1];
          btaBufTX[0] = 0x99;
          goto lbExit;
        }
        else if (btaRX[0] == 'S' && btaRX[1] == 'Y' && btaRX[2] == 'S')
        {
          FillArchDateFirst (ref iPos);
          goto lbExit;
        }
        else if (btaRX[(int)CArchIRGA2.EReqFlash.Sector] == 0 &&
          btaRX[(int)CArchIRGA2.EReqFlash.CodeCom1] == 1 && btaRX[(int)CArchIRGA2.EReqFlash.CodeCom1N] == btNeg1  //0xFE
                 && btaRX[(int)CArchIRGA2.EReqFlash.CodeComR_F] == 'F') // Flash 
        {
          iSizeByCRC = btaRX[(int)CArchIRGA2.EReqFlash.Nbytes];
          btaBufTX = new byte[iSizeByCRC + 2];
          btaBufTX[0] = 1;
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
          FillDate (ref iPos, btaRX);
          iSizeByCRC = (int)(btaBufTX.Length - 2);
        }
        else if (btaRX[0] == 0xFE && btaRX[1] == 1 && btaRX[2] == 0xB9 && btaRX[3] == 0x46 && btaRX[(int)CArchIRGA2.EReqFlash.Sector] == 5)
        {                                                                       // Архив Interfer
          FillArchInterfer (ref iPos, btaRX);
          iSizeByCRC = (int)(btaBufTX.Length - 2);
        }
        else if (btaRX[0] == 0xFE && btaRX[1] == 1 && btaRX[2] == 0xB9 && btaRX[3] == 0x46 && btaRX[11] == 8)//&& btaRX[4] == 0x43 && btaRX[5] == 0xBC)
        {                                                                       //VpE VcE
          btaBufTX = new byte[10];
          FillByFloat (fVal, ref btaBufTX, ref iPos);
          FillByFloat (fVal + 1, ref btaBufTX, ref iPos);
          iBeginBuf = 0;
        }
        else if (btaRX[0] == 0xFE && btaRX[1] == 1 && btaRX[2] == 0xB9 && btaRX[3] == 0x46)   // Архив 
        {
          FillArch (ref iPos, btaRX);
          iSizeByCRC = (int)(btaBufTX.Length - 2);
          iBeginBuf = 0;
        }
        else if (btaRX[0] == 0x6E)                                                // Текущие
        {
          const ushort LEN_RESPONSE = 48;
          btaBufTX = new byte[LEN_RESPONSE];
          btaBufTX[iPos++] = 0xC9;
          byte[] bta = BitConverter.GetBytes (LEN_RESPONSE - 3);
          btaBufTX[iPos++] = bta[0];
          btaBufTX[iPos++] = bta[1];
          btaBufTX[iPos++] = (byte)'M';
          btaBufTX[iPos++] = (byte)iCanal;    // NumCanal (iCanal); // Канал №1 
          if (++iCanal == 4)
            iCanal = 0;
          btaBufTX[iPos++] = btNS;                // Штатный режим работы 
          btaBufTX[iPos++] = btFlags;
          //Random Rnd = new Random (DateTime.Now.Millisecond);
          //fVal = (float)(Rnd.NextDouble () * 4.0);
          FillByFloat (fVal, ref btaBufTX, ref iPos);
          FillByFloat (fVal + 1.0f, ref btaBufTX, ref iPos);
          FillByFloat (fVal + 2.0f, ref btaBufTX, ref iPos);
          FillByFloat (fVal + 3.0f, ref btaBufTX, ref iPos);
          FillByFloat (fVal + 4.0f, ref btaBufTX, ref iPos);
          FillByFloat (fVal + 5.0f, ref btaBufTX, ref iPos);
          FillByFloat (fVal + 6.0f, ref btaBufTX, ref iPos);
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
          btaBufTX = new byte[1];
          btaBufTX[0] = 0x55;
          goto lbExit;
        }
        ushort usCRC = Global.CRCirga2 (btaBufTX, iSizeByCRC, iSizeByCRC + 10, iBeginBuf);
        byte[] btaCRC = BitConverter.GetBytes (usCRC);
        btaBufTX[iPos++] = btaCRC[0];
        btaBufTX[iPos++] = btaCRC[1];
        lbExit:
        ChangeVal ();
        //Thread.Sleep (12000);
        btaTX = btaBufTX;
        return false; 
      }
    }
		//_________________________________________________________________________
		void Format2 (ref int iPos)
		{
			btaBufTX[iPos++] = (byte)'P';
			btaBufTX[iPos++] = 0xFF;
			btaBufTX[iPos++] = (byte)'R';
			btaBufTX[iPos++] = IntToBCD (DTCurr.Minute);		// minute 
			btaBufTX[iPos++] = IntToBCD (DTCurr.Hour);    // hour 
			btaBufTX[iPos++] = 7;   // day 
			btaBufTX[iPos++] = 8;   // month
			btaBufTX[iPos++] = 0x16;		// year 
			FillByFloat (fVal, ref btaBufTX, ref iPos);	// new val 
			FillByFloat (fVal + 1, ref btaBufTX, ref iPos);	// old val 
    }
		//_________________________________________________________________________
		void Format3 (ref int iPos)
		{
			btaBufTX[iPos++] = (byte)'P';
			btaBufTX[iPos++] = 16;  // 2-й канал 
			btaBufTX[iPos++] = 2;		// корр нуля
			btaBufTX[iPos++] = IntToBCD (DTCurr.Minute);		// minute 
			btaBufTX[iPos++] = IntToBCD (DTCurr.Hour);    // hour 
      btaBufTX[iPos++] = 7;   // day 
			btaBufTX[iPos++] = 8;   // month
			btaBufTX[iPos++] = 0x16;		// year 
			FillByFloat (fVal + 1, ref btaBufTX, ref iPos);	// new val 
			FillByFloat (fVal + 2, ref btaBufTX, ref iPos);	// old val 
    }
		//_________________________________________________________________________
		void Format4 (ref int iPos)
		{
			btaBufTX[iPos++] = (byte)'P';
			btaBufTX[iPos++] = 0xFF;
			btaBufTX[iPos++] = (byte)'E';
			btaBufTX[iPos++] = IntToBCD (DTCurr.Minute);		// minute 
			btaBufTX[iPos++] = IntToBCD (DTCurr.Hour);    // hour 
      btaBufTX[iPos++] = 7;   // day 
			btaBufTX[iPos++] = 8;   // month
			btaBufTX[iPos++] = 0x16;   // year 
			iPos += 8;
    }
		//_________________________________________________________________________
		void Format5 (ref int iPos)
		{
			btaBufTX[iPos++] = (byte)'D';
			btaBufTX[iPos++] = (byte)'T';
			iPos++;
			btaBufTX[iPos++] = IntToBCD (DTCurr.Minute);		// minute 
			btaBufTX[iPos++] = IntToBCD (DTCurr.Hour);   // hour 
      btaBufTX[iPos++] = 7;   // day 
			btaBufTX[iPos++] = 9;   // month
			btaBufTX[iPos++] = 0x16;   // year 
			iPos += 7;
			btaBufTX[iPos++] = 0xFF;
    }
		//_________________________________________________________________________
		void Format6 (ref int iPos)
		{
			btaBufTX[iPos++] = (byte)'D';
			btaBufTX[iPos++] = (byte)'C';
			iPos++;
			btaBufTX[iPos++] = 9;		// minute 
			btaBufTX[iPos++] = 6;   // hour 
			btaBufTX[iPos++] = 7;   // day 
			btaBufTX[iPos++] = 8;   // month
			btaBufTX[iPos++] = 0x16;   // year 
			btaBufTX[iPos++] = IntToBCD (DTCurr.Minute);		// minute 
			btaBufTX[iPos++] = IntToBCD (DTCurr.Hour);    // hour 
      btaBufTX[iPos++] = 7;   // day 
			btaBufTX[iPos++] = 0x10;   // month
			btaBufTX[iPos++] = 0x16;   // year 
			iPos += 2;
			btaBufTX[iPos++] = 0xFF;
    }
		//_________________________________________________________________________
		void Format8 (ref int iPos)
		{
			btaBufTX[iPos++] = (byte)'C';
			btaBufTX[iPos++] = 0x12;
			btaBufTX[iPos++] = 0x34;   
			btaBufTX[iPos++] = IntToBCD (DTCurr.Minute);		// minute 
			btaBufTX[iPos++] = IntToBCD (DTCurr.Hour);   // hour 
      btaBufTX[iPos++] = 7;   // day 
			btaBufTX[iPos++] = 0x11;   // month
			btaBufTX[iPos++] = 0x16;   // year 
			FillByFloat (fVal + 3, ref btaBufTX, ref iPos);
			btaBufTX[iPos++] = (byte)'E';   // month
			iPos += 2;
			btaBufTX[iPos++] = 32;		// 3 canal 
    }
		//_________________________________________________________________________
		void Format9 (ref int iPos)
		{
			btaBufTX[iPos++] = (byte)'H';
			iPos++;
			btaBufTX[iPos++] = (byte)'E';
			btaBufTX[iPos++] = IntToBCD (DTCurr.Minute);     // minute 
			btaBufTX[iPos++] = IntToBCD (DTCurr.Hour);    // hour 
      btaBufTX[iPos++] = 3;   // day 
			btaBufTX[iPos++] = 0x12;   // month
			btaBufTX[iPos++] = 0x16;   // year 
			iPos += 6;
			btaBufTX[iPos++] = 48;	// 4 canal 
    }
		//_________________________________________________________________________

		delegate void DFomatsInterfer (ref int iPos);
		DFomatsInterfer[] FormatsInterfer = new DFomatsInterfer[(int)TArchInterferIRGA2.EFormat.SIZE];
		private void FillArchInterfer (ref int iPos, byte[] btaRX)
		{
			int iQuantByte = btaRX[11] + 2;
			if (iQuantByte == 2)
				iQuantByte = 258;
			int iQuantRow = iQuantByte / (int)TArchHourDayIRGA2.ERow.SIZE;
			btaBufTX = new byte[iQuantByte];
			TArchInterferIRGA2.EFormat Format = TArchInterferIRGA2.EFormat.Num2;
			for (int iRow = 0; iRow < iQuantRow; iRow++, Format++)
			{
				if (Format == TArchInterferIRGA2.EFormat.SIZE)
					Format = TArchInterferIRGA2.EFormat.Num2;
				FormatsInterfer[(int)Format] (ref iPos);
			}
		}
		//_________________________________________________________________________
		private void FillArch (ref int iPos, byte[] btaRX)
		{
			int iQuantByte = btaRX[11] + 2;
			if (iQuantByte == 2)
				iQuantByte = 258;
			int iQuantRow = iQuantByte / (int)TArchHourDayIRGA2.ERow.SIZE;
			btaBufTX = new byte[iQuantByte];
			for (int iRow = 0; iRow < iQuantRow; iRow++)
			{
				FillByFloat (fVal, ref btaBufTX, ref iPos);
				FillByShort (sVal, ref btaBufTX, ref iPos);
				FillByFloat (fVal + 1, ref btaBufTX, ref iPos);
				FillByFloat (fVal + 2, ref btaBufTX, ref iPos);
				iPos += 6;
				FillByShort ((short)(sVal + 1), ref btaBufTX, ref iPos);
				FillByShort ((short)(sVal + 2), ref btaBufTX, ref iPos);
				FillByShort ((short)(sVal + 3), ref btaBufTX, ref iPos);
				fVal += 1;
				sVal += 1;
			}
		}
		//_________________________________________________________________________
		private void FillDate (ref int iPos, byte[] btaRX)
		{
      int iLenData = btaRX[7];
      if (iLenData == 0)
        iLenData = 256;
			btaBufTX = new byte[iLenData + 2];
			DateTime DT = DateTime.Now;
      btaBufTX[iPos++] = IntToBCD (DT.Second);
      btaBufTX[iPos++] = 0;
			btaBufTX[iPos++] = IntToBCD (DT.Minute);
      btaBufTX[iPos++] = 0;
			btaBufTX[iPos++] = IntToBCD (DT.Hour);
      btaBufTX[iPos++] = 0;
      btaBufTX[iPos++] = 0;
			btaBufTX[iPos++] = IntToBCD (DT.Day);
			btaBufTX[iPos++] = IntToBCD (DT.Month);
			btaBufTX[iPos++] = IntToBCD (DT.Year - 2000);
      for (; iPos < iLenData; iPos++)
      {
        btaBufTX[iPos] = (byte)iPos;
      }
		}
		//_________________________________________________________________________
    byte IntToBCD (int iVal)
    {
      return (byte)((iVal / 10 << 4) + (iVal % 10));
    }
		//_________________________________________________________________________
		private void FillArchDateFirst (ref int iPos)
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
		private void FillФХП (ref int iPos)
		{
      btaBufTX = new byte[4];
      FillByFloat (fVal, ref btaBufTX, ref iPos);
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
		void FillByFloat (float fVal, ref byte[] btaTo, ref int iPos)
		{
			byte[] bta = BitConverter.GetBytes (fVal);
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
		void FillByShort (short sVal, ref byte[] btaTo, ref int iPos)
		{
			byte[] bta = BitConverter.GetBytes (sVal);
			btaBufTX[iPos++] = bta[0];
			btaBufTX[iPos++] = bta[1];
		}
		//_________________________________________________________________________
		byte NumCanal (int iCanal)
		{
			return (byte)(iCanal * 16 - 1);
		}
		//_________________________________________________________________________
		public override void CreateBufTX ()
		{
			btaBufTX = new byte[258];
		}
	}
}
