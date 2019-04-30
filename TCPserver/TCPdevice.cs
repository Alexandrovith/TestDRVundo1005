///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

///~~~~~~~~~	Проект:			Сервер TCP
///~~~~~~~~~	Прибор:			Все описанные приборы
///~~~~~~~~~	Модуль:			Родительский класс приборов
///~~~~~~~~~	Разработка:	Демешкевич С.А.
///~~~~~~~~~	Дата:				13.12.2016

///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXONIM.CONSTS;
using AXONIM.ScanParamOfDevicves;

namespace TestDRVtransGas.TCPserver
{
	public class CTCPdevice
	{
		#region //xxxxxxxxxxxxxxxxxxxxx    П О С Т О Я Н Н Ы Е      xxxxxxxxxxxxxxxxxxxxxxx

		public const float DEF_VAL = 1.12f;
		public const short DEF_S_VAL = 1;
		public const int DEF_I_VAL = 1;
		/// <summary>
		/// Последовательность переноса байт числа
		/// </summary>
		public enum ESequent { прямая, обратная, два_байта }
		#endregion

		#region //xxxxxxxxxxxxxxxxxxxxx    П Е Р Е М Е Н Н Ы Е      xxxxxxxxxxxxxxxxxxxxxxx

		public FTCPserver Parent;

		protected int iBeginData = (int)MODBUS3_RESP.RegStartH; //(int)EHeadMOD_TCP.NumBytes;
		protected float fVal = DEF_VAL;
		protected short sVal = DEF_S_VAL;
		protected int iVal = DEF_I_VAL;
		protected int iCountRequests;
		protected byte[] btaBufTX;
		public DateTime DTCurr = DateTime.Now;
		protected int iPosTX = (int)MODBUS3_ANSW.Data;
		#endregion

		//xxxxxxxxxxxxxxxxxxxxxxxx       К О Д        xxxxxxxxxxxxxxxxxxxxxxxxxxxxx

		//public CTCPdevice () 
		//{
		//	if (Parent == null)
		//		throw new SystemException ("Не задан родитель.");
		//}
		////_________________________________________________________________________
		public CTCPdevice (FTCPserver Prnt)
		{
			Parent = Prnt;
			CreateBufTX (null);			
		}
		//_________________________________________________________________________
		public virtual void FillResponseByWr(ref byte[] btaTX, ref int iSizeByCRC, byte[] btaRX)
		{
			btaTX = new byte[(int)MODBUS16_ANSW.SIZE];
			iSizeByCRC = btaTX.Length - 2;
			iPosTX = (int)MODBUS16_ANSW.CRCh;
			Parent.OutToWind ("ЗАПІС: " + Global.ByteArToStr (btaRX));
		}
		//_________________________________________________________________________
		virtual public void FillByVal (dynamic Val, ref byte[] btaTo, ref int iPos, ESequent Последовательность = ESequent.обратная)
		{
			int iFrom = iPos;
			if (Val is ushort || Val is short)
			{
				iPos += 2;
			}
			else if (Val is float || Val is int || Val is uint)
			{
				iPos += 4;
			}
			else if (Val is byte[])
			{
				int iPosFrom = iPos + Val.Lenght;
				iPos += Val.Lenght;
				foreach (var item in Val)
				{
					btaTo[--iPosFrom] = item;
				}
				return;
			}
			else
			{
				iPos += 8;
			}
			
			byte[] bta = BitConverter.GetBytes (Val);
			if (Последовательность == ESequent.обратная)
			{
				int iPosRev = iPos;
				foreach (var item in bta)
				{
					btaTo[--iPosRev] = item;
				}
			}
			else if (Последовательность == ESequent.два_байта)
			{
				int iTo = iFrom;
				for (int i = 0; i < bta.Length; i += 2)
				{
					btaTo[iTo++] = bta[i + 1];
					btaTo[iTo++] = bta[i];
				}
			}
			else
			{
				int iTo = iFrom;
				for (int i = 0; i < bta.Length; i++, iTo++)
				{
					btaTo[iTo] = bta[i];
				}
			}
		}
		//_________________________________________________________________________
		virtual public void FillHead_CRC (byte[] btaTX, byte[] btaRX, int iSizeByCRC, int iBeginBuf, int iLenHead = (int)MODBUS3_ANSW.NumByteData)
		{
			for (int i = 0; i < iLenHead; i++)
			{
				btaTX[i] = btaRX[i];
			}
			FillCRC (btaTX, iSizeByCRC, iBeginBuf);
			//ushort usCRC = Global.CRC (btaTX, iSizeByCRC, btaTX.Length, Global.Table8005, 0xFFFF, (ushort)iBeginBuf);
			//byte[] btaCRC = BitConverter.GetBytes (usCRC);
			//btaTX[iPosTX++] = btaCRC[0];
			//btaTX[iPosTX++] = btaCRC[1];
		}
		//_________________________________________________________________________
		public virtual void FillCRC (byte[] btaTX, int iSizeByCRC, int iBeginBuf)
		{
			ushort usCRC = Global.CRC (btaTX, iSizeByCRC, btaTX.Length, Global.Table8005, 0xFFFF, (ushort)iBeginBuf);
			byte[] btaCRC = BitConverter.GetBytes (usCRC);
			btaTX[iSizeByCRC++] = (byte)(usCRC & 0xFF);
			btaTX[iSizeByCRC] = (byte)(usCRC >> 8);// btaCRC[1];
		}
		//_________________________________________________________________________
		virtual public bool GetAnswer (ref byte[] btaTX, byte[] btaRX, ref int iNumPartOfAnswerBySend)
		{
			btaTX = btaBufTX;
			CreateBufTX (btaRX);
			return true;
		}
		//_________________________________________________________________________
		public void RestoreVal()
		{
			fVal = DEF_VAL;
			sVal = DEF_S_VAL;
			iVal = DEF_I_VAL;
		}
		//_________________________________________________________________________
		protected void ChangeVal()
		{
			if (Parent.ChBChange.Checked && ++iCountRequests == 1)
			{
				iCountRequests = 0;
				Random Rnd = new Random (/*DateTime.Now.Millisecond*/);
				fVal = (float)(Rnd.NextDouble () * 4.0);
				sVal = (short)Rnd.Next(-1000, 1000);
				iVal = sVal;
			}
		}
		//_________________________________________________________________________
		public virtual void CreateBufTX (byte[] btaRX)
		{
			if (btaRX != null)
			{
				btaBufTX = new byte[10];
				int i = 0;
				for (; i < iBeginData; i++)
				{
					btaBufTX[i] = btaRX[i];
				}
				for (; i < btaBufTX.Length; i++)
				{
					btaBufTX[i] = (byte)i;
				}
			}
		}
		//_________________________________________________________________________
		public byte IntToBCD (int iVal)
		{
			return (byte)((iVal / 10 << 4) + (iVal % 10));
		}
		//_________________________________________________________________________
		public void FillByShort (short sVal, ref byte[] btaTo, ref int iPos)
		{
			byte[] bta = BitConverter.GetBytes (sVal);
			btaBufTX[iPos++] = bta[0];
			btaBufTX[iPos++] = bta[1];
		}
	}
}
