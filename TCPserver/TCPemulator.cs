///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

///~~~~~~~~~	Проект:			Тест драйверов приборов
///~~~~~~~~~	Прибор:			Все приборы
///~~~~~~~~~	Модуль:			Отправка/получение данных по TCP/IP
///~~~~~~~~~	Разработка:	Демешкевич С.А.
///~~~~~~~~~	Дата:				06.08.2017

///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Reflection;
using AXONIM.CONSTS;
using AXONIM.ScanParamOfDevicves;
using TestDRVtransGas.TCPserver.TCPtoComPort;

namespace TestDRVtransGas.TCPserver
{
	public partial class FTCPserver : Form
	{
		const int iBeginData = 6;
		enum EState { Listen, Off, Waiting }

		//xxxxxxxxxxxxxxxxxxxxx    П Е Р Е М Е Н Н Ы Е      xxxxxxxxxxxxxxxxxxxxxxx

		volatile EState State = EState.Off;
		TcpListener Listener;
		System.Net.IPAddress IP;
		System.Timers.Timer TrRestartListener = new System.Timers.Timer (2 * 60 * 60 * 1000);

		public delegate void DReopenListener (object sender, EventArgs e);
		DReopenListener ReopenListener;

		Action DIncUD;
		byte[] btaBufTX;

		CTCPdevice CurrDev;

		delegate void DOutToWind (string asMess, bool bOutDate);
		DOutToWind OutToWind_d;

		//xxxxxxxxxxxxxxxxxxxxxxxx       К О Д        xxxxxxxxxxxxxxxxxxxxxxxxxxxxx
		public FTCPserver (Form Prnt)
		{
			InitializeComponent ();
			Owner = Prnt;
			CurrDev = new CTCPdevice (this);

			OutRecieve_d = ExchangeMessIN;
			ListenStop_d = ListenRestart;
			OutToWind_d = OutToWind;
			DIncUD = IncUD;

			string[] asaIP = Properties.Settings.Default.IP.Split (';');
			CBIP.Items.Clear ();
			CBIP.Items.AddRange (asaIP);
			CBIP.SelectedIndex = Array.IndexOf (asaIP, Properties.Settings.Default.asIPserver);

			UDPort.Value = Properties.Settings.Default.iPort;
			TrRestartListener.Elapsed += TrRestartListener_Elapsed; // TODO: RESTORE?
			TrRestartListener.AutoReset = false;

			string[] asaDevs = Enum.GetNames (typeof (DEVICE));
			CBDev.Items.AddRange (asaDevs);
			string asD = Properties.Settings.Default.asTCPdeviceCurr;
			CBDev.SelectedIndex = Array.IndexOf (asaDevs, Properties.Settings.Default.asTCPdeviceCurr);//iTCPdeviceCurr
			CBDev_SelectedIndexChanged (this, null);

			UDSizeBufOut.Value = Properties.Settings.Default.dmSizeBufOut;
			UDWriteTimeout.Value = Properties.Settings.Default.dmWriteTimeout;

			ReopenListener = BStartStop_Click;
			CounterServers++;
		}
		//_________________________________________________________________________
		private void BClose_Click (object sender, EventArgs e)
		{
			Close ();
		}
		//___________________________________________________________________________

		int iReconnects = 0;
		private void IncUD ()
		{
			LReconnects.Text = (++iReconnects).ToString (); // UDReconnect.Value++;
		}
		//___________________________________________________________________________
		IPEndPoint GetEndPoint ()
		{
			return new IPEndPoint (IP, (int)UDPort.Value);
		}
		//___________________________________________________________________________
		void ApplyProperties ()
		{
			if (Properties.Settings.Default.asIPserver != CBIP.Text)
			{
				Properties.Settings.Default.asIPserver = CBIP.Text;
				if (Properties.Settings.Default.IP.IndexOf (CBIP.Text) == -1)
				{
					CBIP.Items.Add (CBIP.Text);
					Properties.Settings.Default.IP = Properties.Settings.Default.IP + ";" + CBIP.Text;
				}
			}

			if (Properties.Settings.Default.iPort != (int)UDPort.Value)
				Properties.Settings.Default.iPort = (int)UDPort.Value;
		}
		//_________________________________________________________________________
		private void BStartStop_Click (object sender, EventArgs e)
		{
			Properties.Settings.Default.asTCPdeviceCurr = CBDev.Text;
			//Properties.Settings.Default.asIP = CBIP.Text;

			if (State == EState.Listen || State == EState.Waiting)
			{
				bStopListen = true;
				ListenStop ();
			}
			else
			{
				if (System.Net.IPAddress.TryParse (CBIP.Text, out IP))
				{
					bStopListen = false;
					try
					{
						ListenerStart ();
						PSetups.Enabled = false;
						State = EState.Listen;
						BStartStop.Image = Properties.Resources.call_hang_p4;
						BStartStop.Text = "Не слушать";
						ApplyProperties ();
					}
					catch (Exception exc)
					{
						OutToWind ("BStartStop_Click: " + exc.Message + ".\n" + exc.StackTrace);
						ListenStop ();
					}
				}
				else
				{
					OutToWind ("Ошибка IP.");
					ListenStop ();
				}
			}
		}
		//___________________________________________________________________________
		IAsyncResult ListenerStart ()
		{
			lock (_serverLock)
			{
				CloseListener ();
				Listener = new TcpListener (GetEndPoint ()); //  TcpListener.Create ((int)UDPort.Value);                    //Listener = new TcpListener (IP, (int)UDPort.Value);
				Listener.Start ();
				TrRestartListener.Start ();
				return Listener.BeginAcceptTcpClient (RecieveListen, Listener);  //.BeginAcceptSocket (RecieveListen, Listener); 
			}
		}
		//_________________________________________________________________________
		void ListenStop ()
		{
			TrRestartListener.Stop ();
			State = EState.Off;
			CloseListener ();
			BStartStop.Image = Properties.Resources.call_progress_p4;
			BStartStop.Text = "Слушать";
			PSetups.Enabled = true;
		}
		//,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,

		private readonly object _serverLock = new object ();
		volatile bool bStopListen = false;
		TcpClient ClientTCP;

		byte[] btaBufIN = new byte[512];

		//_________________________    Приём по TCP    ____________________________

		protected int iLenFirstPartBySend;
		public NetworkStream Stream { get; private set; }

		async private void RecieveListen (IAsyncResult ar)
		{
			TrRestartListener.Stop ();
			if (bStopListen)
			{
				return;
			}
			if (ar.IsCompleted)
			{
				try
				{
					TcpListener Listener = ar.AsyncState as TcpListener;
					while (State == EState.Listen)
					{
						if (Listener != null)
						{
							lock (_serverLock)
							{
								ClientTCP = Listener.EndAcceptTcpClient (ar);
							}
							Stream = ClientTCP.GetStream ();
							for (int i = 1; i < 16; i++) { btaBufIN[i] = 0; }
							int iLength;
							while (State == EState.Listen && (iLength = Stream.Read (btaBufIN, 0, btaBufIN.Length)) != 0)
							{
								if (Stream.DataAvailable == false)
								{
									OutRecieve (btaBufIN, iLength);
									if (btaBufTX != null)       // Если не идёт трансляция TCP <-> ComPort
									{
										if (iLenFirstPartBySend == 0 || iLenFirstPartBySend >= btaBufTX.Length)
										{
											Invoke_OutToWind ("TX: (" + btaBufTX.Length + ") " + Global.ByteArToStr (btaBufTX));
											await Stream.WriteAsync (btaBufTX, 0, btaBufTX.Length);     //Stream.BeginWrite (btaBufTX, 0, btaBufTX.Length, EndAnswer, ClientTCP);   // SocketListener.Send (btaBufTX);
										}
										else
										{
											Invoke_OutToWind ("TX: (" + btaBufTX.Length + ") " + Global.ByteArToStr (btaBufTX, 0, iLenFirstPartBySend));
											Stream.BeginWrite (btaBufTX, 0, iLenFirstPartBySend, EndAnswer, ClientTCP);
										}
									}
								}
							}
							CloseClientTCP ();
							TrRestartListener.Start ();
						}
					}
				}
				catch (Exception exc)
				{
					Invoke_OutToWind ("RecieveListen: " + exc.Message + Environment.NewLine + exc.StackTrace);	//TrRestartListener_Elapsed(this, null);
				}
			}
		}
		//___________________________________________________________________________

		object oEndAnsw = new object ();
		/// <summary>
		/// Отправка ответа кончилась
		/// </summary>
		/// <param name="ar">Статус асинхронной операции</param>
		async private void EndAnswer (IAsyncResult ar)
		{
			NetworkStream NS = null;
			lock (oEndAnsw)
			{
				try
				{
					if (State == EState.Listen && ar.IsCompleted)
					{
						TcpClient CurClient = ar.AsyncState as TcpClient;
						if (CurClient.Connected)
						{
							NS = CurClient.GetStream ();
							if (NS != null)
								NS.EndWrite (ar);
						}
					}
				}
				catch /*(Exception)*/    { return; }
			}
			if (NS != null)
			{
				Thread.Sleep ((int)UDWriteTimeout.Value);
				Invoke_OutToWind ("TX next " + UDWriteTimeout.Value.ToString () + "ms. " +
					Global.ByteArToStr (btaBufTX, iLenFirstPartBySend, btaBufTX.Length - iLenFirstPartBySend));
				if (NS.CanWrite)
					await NS.WriteAsync (btaBufTX, iLenFirstPartBySend, btaBufTX.Length - iLenFirstPartBySend);
				iLenFirstPartBySend = 0;
			}
		}
		//___________________________________________________________________________
		void CloseClientTCP ()
		{
			if (ClientTCP != null && ClientTCP.Connected)
			{
				CloseStream ();
				ClientTCP.Close ();
				ClientTCP = null;
			}
		}
		//___________________________________________________________________________
		void CloseStream ()
		{
			if (ClientTCP != null && ClientTCP.Connected)
			{
				NetworkStream NS = ClientTCP.GetStream ();
				if (NS != null)
				{
					NS.Dispose ();
					NS.Close ();
				}
			}
		}
		//___________________________________________________________________________
		void CloseListener ()
		{
			if (Listener != null)
			{
				CloseStream ();
				Listener.Stop ();
				Listener.Server.Close ();
				CloseClientTCP ();
			}
		}
		//___________________________________________________________________________
		public void CloseAll ()
		{
			TrRestartListener.Stop ();
			CloseListener ();
			Properties.Settings.Default.Save ();
		}
		//___________________________________________________________________________
		private void TrRestartListener_Elapsed (object sender, System.Timers.ElapsedEventArgs e)
		{
			try
			{
				Invoke (ReopenListener, this, null);
				Invoke (ReopenListener, this, null);
				Invoke (DIncUD);
			}
			catch { }
		}
		//___________________________________________________________________________
		void Invoke_OutToWind (string asMess, bool bOutDate = true)
		{
			try
			{
				Invoke (OutToWind_d, new object[] { asMess, bOutDate });
			}
			catch { }
		}
		//...........................................................................
		public void OutToWind (string asMess, bool bOutDate = true)
		{
			DateTime DT = DateTime.Now;
			int iCount = RTBOut.Lines.Count ();
			if (iCount > UDSizeBufOut.Value)
			{
				int iLen = 0;
				int iEnd = iCount - (int)(UDSizeBufOut.Value * 1.1m);
				for (int i = 0; i < iEnd; i++)
				{
					iLen += RTBOut.Lines[i].Length;
				}
				RTBOut.Text = RTBOut.Text.Remove (0, iLen);
			}
			RTBOut.AppendText ((bOutDate ? "[" + DT.ToLongTimeString () + "." + DT.Millisecond.ToString () + "] " : "") + asMess + Environment.NewLine);
		}
		//___________________________________________________________________________
		void InvokeListenStop ()
		{
			try
			{
				Invoke (ListenStop_d);
			}
			catch (Exception exc)
			{
				MessageBox.Show (exc.Message + ". " + exc.StackTrace);
			}
		}
		//___________________________________________________________________________
		void ListenRestart ()
		{
			TrRestartListener.Stop ();      //State = EState.Off;
			CloseListener ();
			ListenerStart ();
		}
		//___________________________________________________________________________

		public delegate void DOutRecieve (byte[] btaBufIN, int iLength);
		DOutRecieve OutRecieve_d;
		delegate void DListenStop ();
		DListenStop ListenStop_d;

		public void OutRecieve (byte[] btaBufIN, int iLength)
		{
			try
			{
				Invoke (OutRecieve_d, new object[] { btaBufIN, iLength });
			}
			catch (Exception exc)
			{
				MessageBox.Show (exc.Message + ". " + exc.StackTrace);
			}
		}
		//___________________________________________________________________________
		public static string Version
		{
			get
			{
				Assembly asm = Assembly.GetExecutingAssembly ();
				FileVersionInfo fvi = FileVersionInfo.GetVersionInfo (asm.Location);
				return fvi.FileVersion;
			}
		}
		//___________________________________________________________________________

		int iQuantConn = 0;
		/// <summary>
		/// Обработка приёма 
		/// </summary>
		void ExchangeMessIN (byte[] btaBufIN, int iLength)
		{
			try
			{
				OutToWind ("RX: (" + iLength + ") " + Global.ByteArToStr (btaBufIN, 0, iLength));
				LQuantConn.Text = (++iQuantConn).ToString ();
				AccumRecieve?.Reset ();

				if (CurrDev.GetAnswer (ref btaBufTX, btaBufIN, ref iLenFirstPartBySend))
				{
					// Делаем шапку ответа 
					for (int i = 0; i < 4; i++)
					{
						btaBufTX[i] = btaBufIN[i];
					}
					ushort usLenPacket = (ushort)(btaBufTX.Length - (int)EHeadMOD_TCP.AddrSlave);
					byte[] bta = BitConverter.GetBytes (usLenPacket);
					btaBufTX[4] = bta[1];
					btaBufTX[5] = bta[0];
				}
			}
			catch (Exception exc)
			{
				OutToWind ("ExchangeMessIN: " + exc.Message + Environment.NewLine + exc.StackTrace);
			}
		}
		//___________________________________________________________________________
		private void BClearOut_Click (object sender, EventArgs e)
		{
			RTBOut.Text = "";
			iQuantConn = 0;
			LQuantConn.Text = "0";
			iReconnects = 0;
			LReconnects.Text = "0";
		}
		//___________________________________________________________________________
		private void CBDev_SelectedIndexChanged (object sender, EventArgs e)
		{
			switch (CBDev.Text)
			{
			case "IRGA2": CurrDev = new CDevIRGA2 (this); break;
			case "MAG": CurrDev = new CDevMAG (this); break;
			case "Vympel500": CurrDev = new CDevVympel (this); break;
			case "UFGF": CurrDev = new CDevUFG_F (this); break;
			case "EK270": CurrDev = new CDevEK270 (this); break;
			default: CurrDev = new CTCPdevice (this); break;
			}
		}
		//___________________________________________________________________________
		private void ChBChange_CheckedChanged (object sender, EventArgs e)
		{
			CheckBox ChB = sender as CheckBox;
			if (ChB != null && ChB.Checked == false)
				CurrDev.RestoreVal ();
		}
		//___________________________________________________________________________

		static int CounterServers;
		private void FTCPserver_FormClosing (object sender, FormClosingEventArgs e)
		{
			if (Owner != null)
				if (--CounterServers == 0)
					Owner.Visible = true;
			CloseAll ();
			TCP_Com?.PortClose ();
			//((FTestDrvs)Owner).TCPserver = null;
		}
		//___________________________________________________________________________
		private void FTCPserver_Load (object sender, EventArgs e)
		{
			if (Owner != null)
				Location = new Point (Owner.Left + 110, Owner.Top + Owner.Height - Height);
		}
		//___________________________________________________________________________
		private void PBOpacity_MouseMove (object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				double dOpacity = e.X / 2.0;
				if (dOpacity > 100.0)
					dOpacity = 100.0;
				else if (dOpacity < 30.0)
					dOpacity = 30.0;
				Opacity = dOpacity * 0.01;
				PBOpacity.Value = (int)dOpacity;
			}
		}
		//_________________________________________________________________________
		private void BHideOwner_Click (object sender, EventArgs e)
		{
			if (Owner != null)
			{
				if (Owner.Visible)
				{
					Owner.Visible = false;
					BHideOwner.Text = "Показать";
				}
				else
				{
					Owner.Visible = true;
					BHideOwner.Text = "Скрыть";
				}
			}
		}
		//_________________________________________________________________________
		private void UDSizeBufOut_ValueChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.dmSizeBufOut = UDSizeBufOut.Value;
		}
		//_________________________________________________________________________
		private void UDWriteTimeout_ValueChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.dmWriteTimeout = UDWriteTimeout.Value;
		}
		//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

		CTCP_Com TCP_Com;
		CAccumRecieve AccumRecieve;

		private void BTCP_Com_Click (object sender, EventArgs e)
		{
			BStartStop_Click (sender, e);
			if (BTCP_Com.BackColor == Color.BurlyWood)
			{
				TCP_Com.PortClose ();
				TCP_Com = null;
				AccumRecieve = null;
				BTCP_Com.BackColor = SystemColors.ControlLight;
			}
			else
			{
				BTCP_Com.BackColor = Color.BurlyWood;
				AccumRecieve = new CAccumRecieve (this, Invoke_OutToWind);
				if (Owner != null)
					TCP_Com = new CTCP_Com (((FTestDrvs)Owner).TestExch, AccumRecieve.RecieveTCP_Com);
				CurrDev = new CRMG_Pass (this, TCP_Com);
			}
		}
		//_________________________________________________________________________

	}
}
