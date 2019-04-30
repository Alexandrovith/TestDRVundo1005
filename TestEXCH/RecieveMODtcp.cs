using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using AXONIM.ScanParamOfDevicves;
using AXONIM.CONSTS;
using AXONIM.BelTransGasDRV;

namespace TestDRVtransGas
{
	/// <summary>
	/// Обработка ответа от прибора	по MODBUS TCP/IP
	/// </summary>
	public class CRecieveMODtcp
	{
		volatile int iLenRecievePass = 0;
		/// <summary>
		/// Было получено байт ответа
		/// </summary>
		public int RecieveByte { get; set; }
		public int LenAnswer { get; set; }

		byte[] btaBufRX;
		public delegate bool DHandlingData (CModbusTCP mTCP);
		DHandlingData HandlingData;
		public readonly CExchange Владелец;

		//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

		public CRecieveMODtcp (byte[] btaRX, DHandlingData HandlingData, CExchange Own)
		{
			btaBufRX = btaRX;
			this.HandlingData = HandlingData;
			Владелец = Own;
		}
		//_______________________________________________________________________
		public CModbusTCP CloseAsyncRecieve (IAsyncResult Res)
		{
			try
			{
				MTCP = Res.AsyncState as CModbusTCP;
				MTCP.StateExch = EStateRecieve.OK;
				NetworkStream NWstream = MTCP.NwStream;
				if (NWstream != null)
				{
					int iRd = NWstream.EndRead (Res);
					iLenRecievePass += iRd;
					int iLenBufMax = MTCP.BufRX.Length;
					if (RecieveByte == LenAnswer)// DELETE?
						return null;
					RecieveByte = iLenRecievePass;
					if (NWstream.DataAvailable)
					{
						if (iLenBufMax <= iLenRecievePass)
						{
							MTCP.StateExch = EStateRecieve.ERROR;//              NWstream.Flush ();
							iLenRecievePass = 0;
							return MTCP;
						}
						MTCP.ReadNext (iLenRecievePass);
						return null;
					}
					//else if (bRecieveModbus && iLenRecievePass >= (int)EResponseMOD_TCP.Func && ((MTCP.BufRX[(int)EResponseMOD_TCP.Func] & 0x80) > 0))
					//{
					//  string asErr = iLenRecievePass >= (int)EResponseMOD_TCP.NumBytes ?
					//                  ((ErrMODBUS)MTCP.BufRX[(int)EResponseMOD_TCP.NumBytes]).ToString () : "Ошибка";
					//  Владелец.OutMess (string.Format ("MODBUS: {0}", asErr), "");

					//  MTCP.StateExch = EStateRecieve.ERROR;
					//  return MTCP;    // Если сообщение об ошибке пришло 
					//}
					else
					{
						iLenRecievePass = 0;
					}
				}
			}
			catch (Exception exc)
			{
				Владелец.OutMess (string.Format ("CloseAsyncRecieve: {0}.{1}", exc.Message, exc.StackTrace), "");
			}
			return MTCP;
		}
		//_______________________________________________________________________

		public CModbusTCP MTCP;
		public int iCountHandle;

		public int iShow = 0;
		/// <summary>
		/// Пришёл ответ на запрос параметров
		/// </summary>
		/// <param name="Res">The status of an asynchronous operation</param>
		public void RecieveAnswer (IAsyncResult Res)
		{
			if (Res.IsCompleted == false)
				return;
			MTCP = Free (Res);
			try
			{
				if (MTCP != null && Res.IsCompleted && MTCP.StateExch == EStateRecieve.OK)
				{
					//          if (/*Protocol == EProtocol.IRGA2 && */RecieveByte > 0 && RecieveByte < LenAnswer)
					//          {
					//#if LOG_IRGA
					//						Global.LogWriteLine ("LenRecieve= " + LenRecieve + ", LenAnswer= " + LenAnswer);
					//#endif
					//            MTCP.ReadNext (iRecieveByte);
					//            return;
					//          }
					Владелец.TrStopWait.Stop ();
					//if (MTCP.StateExch == EStateRecieve.OK)
					{
						iCountHandle++;
						//if (iCountHandle >1)                MTCP.Stream.Read (MTCP.BufRX, 0, MTCP.BufRX.Length);
						HandlingData (MTCP);
					}
				}
			}
			catch (Exception exc)
			{
				StopWaitRecieve ();
				Владелец.OutMess ($"RecDataRd: {exc.Message}{Environment.NewLine}{exc.StackTrace}", "");
			}
		}
		//_________________________________________________________________________
		CModbusTCP Free (IAsyncResult Res/*, AsyncCallback CurrFunc*/)
		{
			//if (Global.Closing)				return null;
			MTCP = CloseAsyncRecieve (Res/*, CurrFunc*/);
			Владелец.Flush ();//if (MTCP != null)				MTCP.Flush ();
			return MTCP;
		}
		//_________________________________________________________________________
		/// <summary>
		/// Отмена ожидания ответа WebClient
		/// </summary>
		public void StopWaitRecieve ()    //override
		{
			//TConnModTCP MTCP = Value.Прибор.DevSend as TConnModTCP;
			//if (MTCP != null)
			//  MTCP.Flush ();
			Владелец.Flush ();
			iLenRecievePass = 0;
		}
	}
}
