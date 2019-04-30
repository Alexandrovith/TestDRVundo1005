using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXONIM.CONSTS;
using AXONIM.ScanParamOfDevicves;

namespace TestDRVtransGas.TestEXCH
{
	public class CRMG : CClientTCP
	{
		System.Timers.Timer TrTimeoutRX = new System.Timers.Timer (100);

		enum EComm { Service, Отправка_заголовка, Запись_данных }
		EComm CurrCom = EComm.Service;
		//яяяяяяяяяяяяяяяяяя/?000R122222!\r\n
		const string asAddr = "000";
		const string asPauseFF = "FFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFFF";
		public string asR1 = asPauseFF + $"/?{asAddr}R122222!\r\n";
		string asFirstComm = "W0";
		const string asUser = "01";

		public CRMG (CTestExch Owner) : base (Owner)
		{
			////*ClientTCP.*/EvRecieve += RecieveDev;
			TrTimeoutRX.Elapsed += TrTimeoutRX_Elapsed;
			TrTimeoutRX.AutoReset = false;
		}
		//_________________________________________________________________________
		private void TrTimeoutRX_Elapsed (object sender, System.Timers.ElapsedEventArgs e)
		{
			RecieveDev (btaRX, e);
		}
		//_________________________________________________________________________
		//byte[] btaPas = "33333";
		const int SIZE_PAS = 5;
		private void RecieveDev (object sender, EventArgs e)
		{
			byte[] btaBufRX = (byte[])sender;
			int iPosRight = 0;

			btaBufRX = Conv7to8byt (btaBufRX, Recieve.RecieveByte);
			OutMess ($"RX: {Global.ByteToStr(btaBufRX, 0, Recieve.RecieveByte)}"//(btaBufRX, 0, Recieve.RecieveByte, Base64FormattingOptions.InsertLineBreaks)
				, " ");//Convert.ToBase64String 

			int iPosLeft = Array.FindIndex (btaBufRX, s => s == '(');
			if (iPosLeft > 0)
			{
				iPosRight = Array.FindIndex (btaBufRX, s => s == ')');
				if ((iPosRight > iPosLeft + 1) == false)
				{
					OutMess ("Bad recieve", " ");
					return;
				}
			}
			else return;
													// CRC пока не считаем
			if (CurrCom == EComm.Service && btaBufRX[0] == (byte)'s' && btaBufRX[1] == (byte)'e')   // Получение ответа на Reading of service data (heading)
			{
				OutMess (EComm.Отправка_заголовка.ToString(), " ");
				for (int i = iPosRight; i < iPosRight + SIZE_PAS; i++)
				{
					btaBufRX[i] = (byte)'3';
				}
				ushort usCRC = CRC (btaBufRX, iPosLeft, iPosRight - iPosLeft + SIZE_PAS);
				string asCRCascii = CRCtoASCII (usCRC);
				string asHeadWr = /*asPauseFF + */ $"/?{asAddr}{asFirstComm}{asCRCascii + asCRCascii[3]}!\r\n";
				byte[] btaHead = Global.EncodingCurr .GetBytes (asHeadWr);// ASCIIEncoding.ASCII UTF8Encoding.UTF8.
				InitBufTX (Global.Conv8n1To7e1 (btaHead, btaHead.Length));
				CurrCom = EComm.Отправка_заголовка;
				OutMess (asHeadWr, " ");
				SendComm ();
			}
			else if (CurrCom == EComm.Отправка_заголовка && btaBufRX[0] == (byte)'m' && btaBufRX[1] == (byte)'s')  // Получение ответа на заголовок записи параметра 
			{
				OutMess (EComm.Запись_данных.ToString (), " ");
				string asW0 = /*asPauseFF + */$"W1({asUser}\tK200212)";//{}!\r\n";asFirstComm
				byte[] btaW0 = ASCIIEncoding.ASCII.GetBytes (asW0);// Global.StrToBytes
				ushort usCRC = CRC (btaW0, 0, btaW0.Length);
				asW0 += CRCtoASCII (usCRC) + "!\r\n";
				//OutMess (asW0, " ");
				btaW0 = System.Text.Encoding.ASCII.GetBytes (asW0);//Global.StrToBytes ASCIIEncoding.ASCII
				InitBufTX (Global.Conv8n1To7e1 (btaW0, btaW0.Length));// Размер 
				CurrCom = EComm.Запись_данных;
				OutMess (asW0, " ");
				SendComm ();
			}
			else
			{
				if (CurrCom == EComm.Запись_данных)
				{
					if (btaBufRX[0] == (byte)'m' && btaBufRX[1] == (byte)'w' && btaBufRX[3] == 'W') // Если запись успешна 
					{
						OutMess ("запись успешна", " ");
					}
					else
					{
						OutMess ($"{btaBufRX}", " ");
					}
				}
			}
		}
		//_________________________________________________________________________

		byte[] btaRX = new byte[9196];
		public override bool HandlingData (CModbusTCP MTCP)
		{
			TrTimeoutRX.Stop ();
			//OutMess ($"TCP.RX: [{Global.ByteArToStr (BufRX, NumRecieveByte, Recieve.RecieveByte)}]", "");
			NumRecieveByte += Recieve.RecieveByte;
			Global.Append (BufRX, btaRX, 0, Recieve.RecieveByte);
			TrTimeoutRX.Start ();
			return false;
		}
		//_________________________________________________________________________
		public override void BeforeRequest ()
		{
			base.BeforeRequest ();
			CurrCom = EComm.Service;
			TrTimeoutRX.Stop ();
			TrTimeoutRX.Start ();
		}
		//_________________________________________________________________________
		public override void Close ()
		{
			base.Close ();
			/*ClientTCP.*/
			EvRecieve -= RecieveDev;
		}
		//_________________________________________________________________________
		ushort CRC (byte[] data, int iFrom, int length, byte btMask = 0x7f, bool isbinary = false)
		{
			ushort poly = 0x8005; ushort init = 0;
			ushort crc = init;

			for (; iFrom < length; iFrom++)
			{
				if (!isbinary && (data[iFrom] == '\r' || data[iFrom] == '\n'))
					crc ^= 0x30 << 8;
				else
				{
					//crc ^= (ushort)(data[i] << 8);
					crc ^= (ushort)((data[iFrom] & btMask) << 8);
				}
				for (int k = 0; k < 8; k++)
				{
					if ((crc & 0x8000) != 0)
					{
						crc = (ushort)(crc << 1);
						crc ^= poly;
					}
					else
					{
						crc = (ushort)(crc << 1);
					}
				}
			}
			return crc;
		}
		//_________________________________________________________________________

		static public string CRCtoASCII (ushort crc)
		{
			string res = "";
			for (int i = 12; i >= 0; i -= 4)
			{
				res += (char)(0x30 + ((crc >> i) & 0xf));
			}
			return res;
		}
		//____________________________________________________________________________
		//Преобразоание 7-битной посылки в 8 битную 
		byte[] Conv7to8byt (byte[] btaBuf, int iSize)
		{
			byte[] btaRet = new byte[iSize];
			for (int i = 0; i < iSize; i++)
			{
				btaRet[i] = (byte)(btaBuf[i] & 0x7f);
			}
			return btaRet;
		}
		//_________________________________________________________________________
		//public void OutMess (string asMess, string asDelimiter = " ", bool bOutTime = true)
		//{
		//	try
		//	{
		//		Own.Invoke (Own.OutMess, asMess, asDelimiter, bOutTime);
		//	}
		//	catch { }
		//}
		//_________________________________________________________________________

	}
}
