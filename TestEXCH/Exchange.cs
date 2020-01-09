using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AXONIM.ScanParamOfDevicves;
using AXONIM.CONSTS;
using System.Windows.Forms;
using System.Threading;
using System.Net.Sockets;

namespace TestDRVtransGas
{
	public abstract class CExchange : CModbusTCP
	{
		// Наименования значений по ГОСТ61107
		public enum EComGOST { SOH = 1, STX = 2, ETX, EOT, ACK = 6, NAK = 0x15, LF = 0x0A, CR = 0x0D }

		public readonly CTestExch Владелец;
		protected CRecieveMODtcp Recieve;
		/// <summary>
		/// Количество команд для запроса текущих данных
		/// </summary>
		protected int iQuantComm = 1;
		/// <summary>
		/// Номер текущего запроса
		/// </summary>
		int iCurrRequest = 0;

		public System.Timers.Timer TrStopWait = new System.Timers.Timer (5000);

		//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

		public CExchange (CTestExch Own)
		{
			Владелец = Own;
			TrStopWait.Elapsed += TrStopWait_Elapsed;
			TrStopWait.AutoReset = false;
			BufRX = new byte[1024 * 4];
			Recieve = new CRecieveMODtcp (BufRX, HandlingData, this);
		}
		//_______________________________________________________________________
		private void TrStopWait_Elapsed (object sender, System.Timers.ElapsedEventArgs e)
		{
			Disconnect ();
			OutMess ("[TCP] Прибор не ответил", Environment.NewLine);
		}
		//_________________________________________________________________________
		/// <summary>
		/// Дополнительные настройки перед отправкой запроса прибору
		/// </summary>
		public virtual void BeforeRequest ()
		{
			iCurrRequest = 0;
			iQuantComm = Владелец.TBCommand.Lines.Count ();
			PreviousComm = ECommand.Begin;
			NumRecieveByte = 0;

			Recieve.iCountHandle = 0; /*Recieve.iCntPieces = 0; */Recieve.RecieveByte = 0; Recieve.iShow = 0;
		}
		//_______________________________________________________________________

		/// <summary>
		/// Символ команды запроса / записи
		/// </summary>
		enum ESymComm : byte { CurrVals = 0x6E, ФХП = 0x6D, }
		/// <summary>
		/// Текущее количество полученных байт ответа
		/// </summary>
		protected int NumRecieveByte;
		/// <summary>
		/// Длина ответа ожидаемая
		/// </summary>
		int iLenAnswer;
		object oHandl = new object ();
		int iPosData;
		public virtual bool HandlingData (CModbusTCP MTCP)
		{
			if (Владелец.Closing)
				return false;
			TrStopWait.Stop ();

			//OutMess ("CountHandl " + Recieve.iCountHandle + " ___" + Recieve.iCnt.ToString() + " iCntPeaces=" + Recieve.iCntPieces, "");
			OutMess ("TCP.RX " + Recieve.RecieveByte + ": [" + Global.ByteArToStr (BufRX, 0, Recieve.RecieveByte) + "]", "");

			if (Владелец.RMG != null)   //if (Владелец.CBDev.SelectedIndex == (int)DEVICE.IRGA2) 
			{
				if (BufTX[iPosData] == (byte)ESymComm.CurrVals)
				{
					if (BufRX[0] == 0xC9)
						iLenAnswer = Global.ToInt16rev (BufRX, 1);

					NumRecieveByte += Recieve.RecieveByte; //OutMess ("Full=" + iRecieveFull + " LenAnswer=" + iLenAnswer, "");
					if (iLenAnswer > NumRecieveByte)
					{
						//OutMess ("ReadNext", "");
						ReadNext ();    // SendComm (iPosData, usLenData);
					}
					return false;
				}
				else// if (BufTX[iPosData] == (byte)ESymComm.ФХП)
				{
					if (NextRequest () == false)
						return false;
				}
			}
			else
			{
				if (NextRequest () == false)
					return false;
			}

			if (Recieve.LenAnswer == 1)
				Thread.Sleep ((int)Владелец.NUDTimeout.Value);//OutMess ("Buf2 " + ": [" + Global.ByteArToStr (Recieve.bta2, 0, Recieve.RecieveByte) + "]", "");
			if (++iCurrRequest < iQuantComm)
			{
				//OutMess ("iCountHandle " + Recieve.iCountHandle + " DataAvailable " + bNWstream, "");
				NumRecieveByte = 0;
				Request (GetStrFromArr (Владелец.TBCommand.Lines, iCurrRequest)); //Request (Владелец.TBCommand.Lines[iCurrRequest]);
				return false;
			}
			return true;
		}
		//_______________________________________________________________________
		delegate void DGetTextCB (string[] Lines, int iLine);
		public string GetStrFromArr (string[] Lines, int iLine)
		{
			string asRet = "";
			try
			{
				if (Владелец.InvokeRequired)
					Владелец.Invoke (new DGetTextCB (delegate
					{
						asRet = Lines[iLine];
					}), Lines, iLine);
				else asRet = Lines[iLine];
			}
			catch (Exception ex)
			{
			}
			return asRet;
		}

		//_______________________________________________________________________
		bool NextRequest ()
		{
			NumRecieveByte += Recieve.RecieveByte;
			if (Recieve.LenAnswer > NumRecieveByte)
			{
				//OutMess ("ReadNext", ""); //OutMess ("LenAnswer " + Recieve.LenAnswer + " NumRecieveByte " + NumRecieveByte, "");
				ReadNext ();    // SendComm (iPosData, usLenData);
				return false;
			}
			return true;
		}
		//_______________________________________________________________________
		public void InitBufs (byte[] btaBufTX)
		{
			base.BufTX = btaBufTX;
		}
		//_______________________________________________________________________
		public virtual void Close ()
		{
			Disconnect ();
		}
		ushort usLenData;

		//_________________________________________________________________________
		public virtual bool Request (string asCommand)
		{
			try
			{
				iPosData = 0;// (int)EResponseMOD_TCP.AddrSlave;
				string[] asaComm = asCommand.Split (' ');
				if (Владелец.UDInverNum.Value <= iCurrRequest)
					usLenData = (ushort)(asaComm.Length * 2 - Владелец.UDInverShift.Value);
				else usLenData = (ushort)asaComm.Length;

				InitBufs (new byte[iPosData + usLenData]);

				if (Владелец.RMG == null)
				{
					if (GetSelIndexOfCB (Владелец.CBDev) == (int)DEVICE.IRGA2)      //if (Владелец.CBDev.SelectedIndex == (int)DEVICE.IRGA2)
					{
						if (asCommand.Length > 0)
						{
							if (FillTXirga (asaComm, asCommand) == false)
								return false;
						}
						else usLenData = 0;
					}
					else
					{
						BufTX = Global.StrHexToBytes (asCommand);
						if (Владелец.RMG != null)   //if (Владелец.CBDev.Text == "RMG")
							Global.Conv8n1To7e1 (BufTX, usLenData);
					}
				}

				if (IsConnect ())
				{
					Recieve.LenAnswer = GetReceivedBytesThreshold (BufTX, iPosData);
					if (SendComm ())
					{
						string asCom = Global.ByteArToStr (BufTX, 0, iPosData + usLenData);
						OutMess (string.Format ("TCP.TX {0} [{1}]", iCurrRequest + 1, asCom), iCurrRequest == 0 ? Environment.NewLine : "");
					}
				}
				else
				{
					MessageBox.Show ("[TCP] Нет соединения", "Connect()");
					return false;
				}
			}
			catch (Exception exc)
			{
				OutMess ($"{exc.Message}{Environment.NewLine}{exc.StackTrace}", Environment.NewLine);
				return false;
			}
			return true;
		}
		//_________________________________________________________________________

		delegate void DGetSelIndexOfCB (ComboBox CB);
		public int GetSelIndexOfCB (ComboBox CB)
		{
			int Ret = 0;
			try
			{
				if (Владелец.InvokeRequired)
					Владелец.Invoke (new DGetSelIndexOfCB (delegate
					{
						Ret = CB.SelectedIndex;
					}), CB);
			}
			catch (Exception ex)
			{
			}
			return Ret;
		}

		//_________________________________________________________________________
		bool ConvStrAsCharToInt (string[] asaComm)
		{
			//int i = iPosData;
			//foreach (var item in asaComm)
			//{
			//	if (item[0] == '\'')            // Чтение символа 
			//	{
			//		byte bt = (byte)item[1];
			//		BufTX[i++] = ConvCpecSymToInt(item[1]);
			//	}
			//	else
			//	{
			//		byte bt;
			//		if (byte.TryParse (item, System.Globalization.NumberStyles.HexNumber, null as IFormatProvider, out bt))
			//		{
			//			if (Владелец.UDInverNum.Value <= iCurrRequest &&
			//					i >= iPosData + Владелец.UDInverShift.Value)
			//				BufTX[i++] = (byte)(~bt);
			//			BufTX[i++] = bt;
			//		}
			//		else
			//		{
			//			MessageBox.Show ($"Request. Неверный код [{item}] команды {asCommand}");
			//			return false;
			//		}
			//	}
			//}
			return true;
		}
		//_________________________________________________________________________
		bool FillTXirga (string[] asaComm, string asCommand)
		{
			int i = iPosData;
			foreach (var item in asaComm)
			{
				if (item[0] == '\'')            // Чтение символа 
				{
					byte bt = (byte)item[1];
					if (Владелец.UDInverNum.Value <= iCurrRequest && i >= iPosData + Владелец.UDInverShift.Value)
						BufTX[i++] = (byte)(~bt);
					BufTX[i++] = bt;
				}
				else
				{
					byte bt;
					if (byte.TryParse (item, System.Globalization.NumberStyles.HexNumber, null as IFormatProvider, out bt))
					{
						if (Владелец.UDInverNum.Value <= iCurrRequest &&
								i >= iPosData + Владелец.UDInverShift.Value)
							BufTX[i++] = (byte)(~bt);
						BufTX[i++] = bt;
					}
					else
					{
						MessageBox.Show ($"Request. Неверный код [{item}] команды {asCommand}");
						return false;
					}
				}
			}
			return true;
		}
		//_________________________________________________________________________
		public bool SendComm ()
		{
			if (SendResponse (Recieve.RecieveAnswer) == AXONIM.CONSTS.SEND_COMM.Error)
			{
				MessageBox.Show ("Сбой при отправке по TCP.");
				return false;
			}
			return true;
		}
		//_________________________________________________________________________
		public void InitIPport (string asIP, int iPort)
		{
			this.asIP = asIP;
			this.iPort = iPort;
		}
		//_________________________________________________________________________
		public enum ECommand { Begin, SYS, Arch_Date, ФХП }
		ECommand PreviousComm = ECommand.Begin;

		private int GetReceivedBytesThreshold (byte[] btaBuf, int iStartPosData)
		{
			if (PreviousComm == ECommand.Begin)
			{
				if (btaBuf[iStartPosData] == 'S' && btaBuf[1 + iStartPosData] == 'Y' && btaBuf[2 + iStartPosData] == 'S')
				{
					PreviousComm = ECommand.SYS;       // Чтение архивов, времени 
					return 17;
				}
				if (btaBuf[iStartPosData] == (byte)ESymComm.ФХП)        // ФХП 
				{
					PreviousComm = ECommand.ФХП;
					return 1;
				}
			}
			else
			{
				if (PreviousComm == ECommand.SYS || PreviousComm == ECommand.Arch_Date)
				{
					PreviousComm = ECommand.Arch_Date;
					return 254;
				}
				if (btaBuf[iStartPosData] == (byte)ESymComm.ФХП)
				{
					if (PreviousComm == ECommand.ФХП)
					{
						if (btaBuf[iStartPosData] == 'R')
							return 25;
						else return 35;
					}
				}
			}
			return 1;
		}
		//_________________________________________________________________________
		public void OutMess (string asMess, string asDelimiter, bool bOutTime = true)
		{
			try
			{
				Владелец.Invoke (Владелец.OutMess, asMess, asDelimiter, bOutTime);
			}
			catch { }
		}
		//_________________________________________________________________________
		public void InitBufTX (byte[] Buf)
		{
			BufTX = Buf;
		}
		//_________________________________________________________________________
		static byte CalcParity (byte btSym)
		{
			byte p = 1;
			byte mask = 1;
			do
				if ((mask & btSym) != 0) p = (byte)(1 - p);
			while ((mask = (byte)(mask << 1)) != 0);
			return p;
			//return (byte)(~((byte)(a & 1)));
		}
		//_________________________________________________________________________
		//public static byte CalcBCC (byte[] btaInput, int iStart, int iCount)
		//{
		//	int btBCC = 0;
		//	for (int i = iStart; i < iCount; i++)
		//	{
		//		btBCC = btBCC + btaInput[i];//btBCC = btBCC ^ btaInput[i];
		//		if (CalcParity ((byte)btBCC) != 0)
		//			btBCC = btBCC | 0x80;
		//	}
		//	return (byte)btBCC;
		//}
		//_________________________________________________________________________
		static int QuantBit (byte btVal)
		{
			byte btShift = 1;
			int iRet = 0;
			for (int i = 0; i < 8; i++, btShift = (byte)(btShift << 1))
			{
				if ((btVal & btShift) != 0)
					iRet++;
			}
			return iRet;
		}
		////_________________________________________________________________________
		/// <summary>
		/// Колич. бит
		/// </summary>
		//public static byte CalcBCC (byte[] btaInput, int iStart, int iCount)
		//{
		//	byte btBCC = 0;
		//	for (int i = iStart; i < iCount; i++)
		//	{
		//		btBCC ^= (byte)QuantBit (btaInput[i]);//btBCC += QuantBit (btaInput[i]);
		//			btBCC = AddOdd (btBCC);
		//	}
		//		return (byte)btBCC;
		//}
		////_________________________________________________________________________
		//public static byte CalcBCC (byte[] btaInput, int iStart, int iCount)
		//{
		//	byte btBCC = 0;
		//	for (int i = iStart; i < iCount; i++)
		//	{
		//		btBCC += AddOdd(btaInput[i]);  //btBCC ^= btTmp; 
		//		btBCC = AddOdd (btBCC);
		//	}
		//		return (byte)btBCC;
		//}
		//_________________________________________________________________________
		public static byte CalcBCC (byte[] btaInput, int iStart, int iCount)
		{
			byte btBCC = 0;
			for (int i = iStart; i < iCount; i++)
			{
				btBCC ^= btaInput[i];//				btBCC += btaInput[i];
			}
			return (byte)(btBCC ^ 1);// return (byte)((btBCC & 0xFF) ^ 1);//(byte)((btBCC & 0xFF) + 1);//(byte)((btBCC ^ 0xFF) + 1);
		}
		//_________________________________________________________________________
		//public static byte CalcBCC (byte[] btaInput, int iStart, int iCount)
		//{
		//	int btBCC = 0;
		//	for (int i = iStart; i < iCount; i++)
		//	{
		//		btBCC += btaInput[i];//btBCC ^= btaInput[i];
		//	}
		//	return (byte)(btBCC ^ 0x7F);
		//}
		//_________________________________________________________________________
		//public static byte CalcBCC (byte[] btaInput, int iStart, int iCount)
		//{
		//	int btBCC = 0;
		//	for (int i = iStart; i < iCount; i++)
		//	{
		//		//byte btTmp;
		//		//if (CalcParity (btaInput[i]) == 0)
		//		//	btTmp = (byte)(btaInput[i] & 0x7F);
		//		//else
		//		//	btTmp = (byte)(btaInput[i] | 0x80);
		//		//btBCC = (btBCC + btTmp) & 0xFF;
		//		btBCC = (btBCC + btaInput[i]) & 0xFF;//btBCC = (btBCC ^ btaInput[i]) & 0xFF;
		//	}
		//	return (byte)((((btBCC ^ 0xFF) + 1) & 0xFF));
		//}
		//_________________________________________________________________________
		//public static byte CalcBCC (byte[] btaInput, int iStart, int iCount)
		//{
		//	int btBCC = btaInput[iStart];
		//	for (int i = iStart + 1; i < iCount; i++)
		//	{
		//		byte btTmp;
		//		if (CalcParity (btaInput[i]) == 0)
		//			btTmp = (byte)(btaInput[i] & 0x7F);
		//		else
		//			btTmp = (byte)(btaInput[i] | 0x80);
		//		btBCC = (btBCC ^ btTmp)/* & 0xFF*/;
		//	}
		//	return (byte)((((btBCC ^ 0xFF) + 1) & 0xFF));   //btBCC
		//}
		//_________________________________________________________________________
		//public static byte CalcBCC (byte[] btaInput, int iStart, int iCount)
		//{
		//	byte btBCC = 0;//int
		//	for (int i = iStart; i < iCount; i++)
		//	{
		//		byte btTmp;
		//		if (CalcParity (btaInput[i]) == 0)
		//			btTmp = (byte)(btaInput[i] & 0x7F);
		//		else
		//			btTmp =(byte)(btaInput[i] | 0x80);
		//		btBCC =  (byte)(btBCC ^ btTmp);  //(btBCC ^ btTmp);
		//	}
		//	return (byte)((((btBCC ^ 0xFF) + 1) & 0xFF));   
		//}
		//_________________________________________________________________________
		//public static byte CalcBCC (byte[] btaInput, int iStart, int iCount)
		//{
		//	byte btBCC = 0;
		//	for (int i = iStart; i < iCount; i++)
		//	{
		//		byte btTmp = AddOdd (btaInput[i]);
		//		btBCC = AddOdd ((byte)(btBCC ^ btTmp));
		//	}
		//	return (byte)((((btBCC ^ 0xFF) + 1) & 0xFF));
		//}
		//_________________________________________________________________________
		private static byte AddOdd (byte btVal)
		{
			byte btTmp;
			if (CalcParity (btVal) == 0)
				btTmp = (byte)(btVal & 0x7F);
			else
				btTmp = (byte)(btVal | 0x80);
			return btTmp;
		}
		//_________________________________________________________________________
		/// <summary>
		/// без учёта чётности
		/// </summary>
		//public static byte CalcBCC (byte[] btaInput, int iStart, int iCount)
		//{
		//	byte btBCC = 0;
		//	for (int i = iStart; i < iCount; i++)
		//	{
		//		btBCC ^= (byte)btaInput[i];//btBCC += QuantBit (btaInput[i]);
		//	}
		//	return (byte)btBCC;
		//	//return (byte)((((btBCC ^ 0xFF) + 1) & 0xFF));
		//}
		//_________________________________________________________________________
	}
}
