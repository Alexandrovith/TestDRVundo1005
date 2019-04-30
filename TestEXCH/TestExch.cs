using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AXONIM.CONSTS;
using TestDRVtransGas.TestEXCH;
using System.Threading;

namespace TestDRVtransGas
{
	public partial class CTestExch : UserControl, IUserControls
	{
		internal CClientTCP Прибор;
		CComPort CP;
		int iPort;

		public delegate void DOutMess (string asMess, string asDelimiter, bool bOutTime = true);
		public DOutMess OutMess;

		public CTestExch (object TheControl)
		{
			InitializeComponent ();
			Parent = (Control)TheControl;

			CBIP.Items.AddRange (Properties.Settings.Default.asIPs.Split (';'));
			if (Properties.Settings.Default.asIP.Length > 7)
			{
				CBIP.SelectedIndex = CBIP.FindString (Properties.Settings.Default.asIP);
			}
			else
			{
				if (CBIP.Items.Count > 0)
					CBIP.SelectedIndex = 0;
			}
			TBPort.Text = Properties.Settings.Default.asPort;
			TBCommand.Text = Properties.Settings.Default.asCommand;

			OutMess = Inv_OutMess;
			CBBaudCOM.SelectedIndex = CBBaudCOM.FindString (Properties.Settings.Default.asBaudCom);
			if (CBBaudCOM.SelectedIndex == -1)
				CBBaudCOM.SelectedIndex = 0;
			NUDTimeout.Value = Properties.Settings.Default.dTimeoutBetwen;
			UDCOM.Value = Properties.Settings.Default.mComPort;

			CBParity.Items.AddRange (Enum.GetNames (typeof (System.IO.Ports.Parity)));
			CBParity.SelectedIndex = Properties.Settings.Default.iParity;
			CBStopBits.Items.AddRange (Enum.GetNames (typeof (System.IO.Ports.StopBits)));
			CBStopBits.SelectedIndex = Properties.Settings.Default.iStopBits;
			NUDDataBits.Value = Properties.Settings.Default.iDataBits;
			//ReSize ();
			BeginDopFunc = new SPDopFunc (PDopFunc.Top, PDopFunc.Width, PDopFunc.Height);

			NUDFontSize.Value = (decimal)TBListExchange.Font.Size;

			CBDev.Items.AddRange (Enum.GetNames (typeof (DEVICE)));
			CBDev.SelectedIndex = CBDev.FindString (Properties.Settings.Default.asDevExch);
			if (CBDev.SelectedIndex < 0)
				CBDev.SelectedIndex = 0;
		}
		//_____________________________________________________________
		private void TBCommand_KeyPress (object sender, KeyPressEventArgs e)
		{
			if (Control.ModifierKeys == Keys.Control && e.KeyChar == (char)Keys.Return)
			{
				BRequest_Click (sender, e);
			}
		}
		//_____________________________________________________________
		private void BRequest_Click (object sender, EventArgs e)
		{
			if (int.TryParse (TBPort.Text, out iPort))
			{
				Properties.Settings.Default.asPort = TBPort.Text;
				Properties.Settings.Default.asIP = CBIP.Text;
				if (CBIP.FindString (CBIP.Text) < 0)
				{
					CBIP.Items.Add (CBIP.Text);
					Properties.Settings.Default.asIPs += ";" + CBIP.Text;
				}
				Properties.Settings.Default.asCommand = TBCommand.Text;
				//switch ((EDevice)CBDevice.SelectedIndex)
				//{
				//case EDevice.ИРГА2: RequestIRGA2 (); break;
				//default: break;
				//}
				Request ();
			}
			else
			{
				MessageBox.Show ("Неправильный номер порта: [" + TBPort.Text + "]");
			}
		}
		//_________________________________________________________________________
		private void BReadCOM_Click (object sender, EventArgs e)
		{
			try
			{
				if (CP == null)
				{
					CP = new CComPort (this);
					if (CP.PortCreate () == false)
					{
						CP = null;
						return;
					}
					OpeningPort ();
				}
				//CP?.PortClose ();
				//CP = new CComPort (this);
				//if (CP.CreatePort () == false)
				//{
				//	MessageBox.Show ("Порт [" + CP.SP?.PortName + "] не открыт");
				//	CP = null;
				//	return;
				//}
				byte[] btaTX;
				bool bRet = false;
				int iCountRequest = 0;
				if (ChBModeRequest.Checked)
					bRet = SendTopRequest (out btaTX, ref iCountRequest);
				else
					bRet = SendTestRequest (out btaTX);
				if (bRet)
				{
					CP.SendComm (btaTX, 0, btaTX.Length);
					if (Properties.Settings.Default.asCommand.Contains (TBCommand.Text) == false)
						Properties.Settings.Default.asCommand = TBCommand.Text;
				}
				else TBCommand.AppendText ("Неверная команда " + TBCommand.Lines[iCountRequest]);

				if (bSavePropertyes)
				{
					bSavePropertyes = false;
					Properties.Settings.Default.Save ();
				}
			}
			catch (Exception exc)
			{
				MessageBox.Show (exc.Message + Environment.NewLine + exc.StackTrace);
				if (CP != null && CP.PortOpen ())
				{
					CP.PortClose ();
				}
			}
		}
		//_________________________________________________________________________
		private bool SendTestRequest (out byte[] btaTX)
		{
			btaTX = new byte[8];
			int iPos = 0;
			btaTX[iPos++] = 0xAA; btaTX[iPos++] = (byte)UDAddr.Value; btaTX[iPos++] = 0x8; btaTX[iPos++] = 0x2;
			ushort usCanal = 1;
			byte[] btaCanal = BitConverter.GetBytes (usCanal);
			btaTX[iPos++] = btaCanal[0];
			btaTX[iPos++] = btaCanal[1];
			ushort usCRC =
					//Global.CRCirga2(btaTX, 2, btaTX.Length, 4);
					//Global.CRC (btaTX, btaTX.Length - 2, btaTX.Length, Global.Table8005, 0xFFFF);
					Global.CRC (btaTX, 2, btaTX.Length, Global.Table8005, 0, 4);
			byte[] btaCRC = BitConverter.GetBytes (usCRC);
			btaTX[iPos++] = btaCRC[0];
			btaTX[iPos++] = btaCRC[1];
			return true;
		}
		//_________________________________________________________________________
		bool SendTopRequest (out byte[] btaTX, ref int iCountRequest)
		{
			iCountRequest = 0;
			btaTX = ConvStrToComm (TBCommand.Lines[iCountRequest], iCountRequest);
			if (btaTX != null)
			{
				CP.BeforeRequest ();
				return true;
			}
			return false;
		}
		//_________________________________________________________________________
		private void Request ()
		{
			if (Прибор != null)
			{
				Прибор.BeforeRequest ();
				if (TBCommand.Lines.Count () > 0)
				{
					string asM_TCP_Head = "";
					if (CkBModbusTCP.Checked)
					{
						byte[] btaBuf = new byte[6];
						FillHeader (/*16, */6, btaBuf);
						asM_TCP_Head = Global.ByteArToStr (btaBuf);
					}
					Прибор.Request (asM_TCP_Head + TBCommand.Lines[0]);
				}
				else Прибор.Request ("");
			}
			else Inv_OutMess ("Прибор не подключен", "");
		}
		//_______________________________________________________________________

		ushort TransactionID = 0;

		/// <summary>
		/// Build MBAP header for Modbus TCP/UDP
		/// </summary>
		/// <param name="dest_address">Destination address</param>
		/// <param name="message_len">Message length</param>
		public void FillHeader (/*byte dest_address, */ushort message_len, byte[] btaBuf)
		{
			int iPosAdd = 0;
			PutTransactionID (ref iPosAdd, btaBuf);
			AddRange (ref iPosAdd, 0, btaBuf);   // Protocol ID (fixed value)
			AddRange (ref iPosAdd, message_len, btaBuf);   // Message length 
																										 //btaBuf[iPosAdd] = dest_address;      // Remote unit ID 
		}
		//_____________________________________________________________________
		protected void AddRange (ref int iPosAdd, ushort usValue, byte[] btaDest)
		{
			btaDest[iPosAdd++] = (byte)(usValue >> 8);
			btaDest[iPosAdd++] = (byte)(usValue & 0x00FF);
		}
		//_________________________________________________________________________
		void PutTransactionID (ref int iPosAdd, byte[] btaBufTX)
		{
			AddRange (ref iPosAdd, TransactionID, btaBufTX);
			TransactionID++;
		}
		//_________________________________________________________________________
		public void Saving ()
		{
			if (bSavePropertyes)
				Properties.Settings.Default.Save ();
			CP?.SP?.Close (); // BDisconn_Click (null, null);
			BSaveFile_Click (null, null);

			Closing = true;
			BDisconn_Click (null, null);
			Прибор?.Disconnect ();
		}
		//_________________________________________________________________________

		private bool bSavePropertyes;

		private void BSaveFile_Click (object sender, EventArgs e)
		{
			if (TBListExchange.Text.Length > 100)
			{
				string asFile = CreateNameFile ();
				System.IO.File.WriteAllText (asFile, TBListExchange.Text);
				if (CBClearOut.Checked)
					TBListExchange.Text = "";
				if (sender != null)
					MessageBox.Show ("Записано в файл [" + asFile + "]");
			}
		}
		//_________________________________________________________________________

		public const string asExt = ".exch";

		protected string CreateNameFile ()
		{
			if (TBFile.Text.Length == 0)
			{
				string asConvDT = DateTime.Now.ToString ().Replace (':', '_');
				return Directory.GetCurrentDirectory () + @"\" + asConvDT.Replace ('/', '_') + asExt;
			}
			else return Directory.GetCurrentDirectory () + @"\" + TBFile.Text + asExt;
		}
		//_________________________________________________________________________
		public void Inv_OutMess (string asMess, string asBeforeDT, bool bOutTime = true)
		{
			DateTime DT = DateTime.Now;
			if (bOutTime)
				asMess = asBeforeDT + "[" + DT.ToString () + "." + DT.Millisecond.ToString () + "]\t" + asMess;
			else asMess = asBeforeDT + asMess;
			DOutMess OutMess = (m,d, o) => TBListExchange.AppendText (Environment.NewLine + asMess);
			Invoke (OutMess, asMess, " ", bOutTime);

			//TBListExchange.AppendText (Environment.NewLine + asMess);
		}
		//_________________________________________________________________________
		private void BDisconn_Click (object sender, EventArgs e)
		{
			if (CP == null)
			{
				CP = new CComPort (this);
				if (CP.PortCreate ())
					OpeningPort ();
			}
			else
			{
				if (CP.IsOpen)
				{
					CP.PortClose ();
					BOpenCom.Text = "Открыть";
					BOpenCom.ImageIndex = 1;
				}
				else
				{
					OpeningPort ();
				}
			}
		}
		//_________________________________________________________________________
		private void OpeningPort ()
		{
			if (CP.PortOpen ())
			{
				BOpenCom.Text = "Закрыть";
				BOpenCom.ImageIndex = 0;
			}
		}
		//_________________________________________________________________________
		public void ReSize ()
		{
			Width = this.Parent.Width;
			Height = this.Parent.Height;
		}
		//_________________________________________________________________________
		public bool Closing { get; private set; } = false;

		//_________________________________________________________________________
		public byte[] ConvStrToComm (string asCommand, int iCountRequest)
		{
			string[] asaComm = asCommand.Split (' ');
			ushort usLenData;
			if (/*Владелец.ChBInvers.Checked && */UDInverNum.Value <= iCountRequest)
				usLenData = (ushort)(asaComm.Length * 2 - UDInverShift.Value);
			else usLenData = (ushort)asaComm.Length;

			byte[] btaBufTX = new byte[usLenData];
			int i = 0;
			foreach (var item in asaComm)
			{
				if (item[0] == '\'')            // Чтение символа 
				{
					byte bt = (byte)item[1];
					if (/*Владелец.ChBInvers.Checked && */UDInverNum.Value <= iCountRequest && i >= UDInverShift.Value)
						btaBufTX[i++] = (byte)(~bt);
					btaBufTX[i++] = bt;
				}
				else
				{
					byte bt;
					if (byte.TryParse (item, System.Globalization.NumberStyles.HexNumber, null as IFormatProvider, out bt))
					{
						if (/*Владелец.ChBInvers.Checked && */UDInverNum.Value <= iCountRequest &&
								i >= UDInverShift.Value)
							btaBufTX[i++] = (byte)(~bt);
						btaBufTX[i++] = bt;
					}
					else
					{
						MessageBox.Show ("ConvStrToComm. Неверный код команды [" + asCommand + "]");
						return null;
					}
				}
			}
			return btaBufTX;
		}
		//___________________________________________________________________________
		private void BConnect_Click (object sender, EventArgs e)
		{
			if (Прибор == null)
			{
				if (int.TryParse (TBPort.Text, out iPort))
				{
					Прибор = new CIRGA2 (this);
				}
				else
				{
					Inv_OutMess ("Неправильный номер порта: [" + TBPort.Text + "]", "");
					return;
				}
			}
			Properties.Settings.Default.asPort = TBPort.Text;
			Properties.Settings.Default.asIP = CBIP.Text;
			if (CBIP.FindString (CBIP.Text) < 0)
			{
				CBIP.Items.Add (CBIP.Text);
				Properties.Settings.Default.asIPs += ";" + CBIP.Text;
			}
			if (Properties.Settings.Default.asCommand.Contains (TBCommand.Text) == false)
				Properties.Settings.Default.asCommand = TBCommand.Text;
			Прибор.InitIPport (CBIP.Text, iPort);

			ThreadPool.QueueUserWorkItem ((Object oInfo) =>
			{
				Inv_OutMess ($"Подключение к {GetTextCB (CBIP)} ...", "");
				if (ConnDev (oInfo) == false)
				{
					Прибор = null;
				}
			}
			, sender);
		}
		//_________________________________________________________________________

		delegate void DGetTextCB (ComboBox CB);
		public string GetTextCB (ComboBox CB)
		{
			string asRet = "";
			try
			{
				if (this.InvokeRequired)
					Invoke (new DGetTextCB (delegate
					{
						asRet = CB.Text;
					}), CB);
				else asRet = CB.Text;
			}
			catch (Exception ex)
			{
				MessageBox.Show (ex.Message);
			}
			return asRet;
		}
		//_________________________________________________________________________

		delegate void DSetTextB (Button CB, string asText);
		public void SetTextB (Button But, string asText)
		{
			try
			{
				if (this.InvokeRequired)
					Invoke (new DSetTextB (delegate
					{
						But.Text = asText;
					}), But, asText);
				else But.Text = asText;
			}
			catch (Exception ex)
			{
				MessageBox.Show (ex.Message);
			}
		}
		//___________________________________________________________________________
		private bool ConnDev (object sender, string asConn = "Соединить", string asDisconn = "Отключить")
		{
			Button TheBtn = sender as Button;
			if (Прибор.IsConnect ())
			{
				Прибор.Disconnect ();
				SetTextB (TheBtn, asConn);
				Inv_OutMess ("[TCP] Прибор отключен", "");
			}
			else
			{
				if (Прибор.Connect ())
				{
					Inv_OutMess ("[TCP] Прибор подключен", "");
					SetTextB (TheBtn, asDisconn);
					return true;
				}
				else Inv_OutMess ("[TCP] Прибор не соединяется", "");
			}
			return false;
		}
		//___________________________________________________________________________
		private void CBWrap_CheckedChanged (object sender, EventArgs e)
		{
			TBListExchange.WordWrap = CBWrap.Checked;
		}
		//___________________________________________________________________________
		private void NUDTimeout_ValueChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.dTimeoutBetwen = NUDTimeout.Value;
		}
		//___________________________________________________________________________
		private void UDCOM_ValueChanged (object sender, EventArgs e)
		{
			CP?.ChangePort ();
			Properties.Settings.Default.mComPort = UDCOM.Value;
			bSavePropertyes = true;
		}
		//___________________________________________________________________________
		private void CBParity_SelectedIndexChanged (object sender, EventArgs e)
		{
			bSavePropertyes = true;
			Properties.Settings.Default.iParity = CBParity.SelectedIndex;
			CP?.ChangeParity ();
		}
		//___________________________________________________________________________
		private void NUDDataBits_ValueChanged (object sender, EventArgs e)
		{
			bSavePropertyes = true;
			Properties.Settings.Default.iDataBits = (int)NUDDataBits.Value;
			CP?.ChangeDataBits ();
		}
		//___________________________________________________________________________
		private void CBStopBits_SelectedIndexChanged (object sender, EventArgs e)
		{
			bSavePropertyes = true;
			Properties.Settings.Default.iStopBits = CBStopBits.SelectedIndex;

			CP?.ChangeStopBits ();
		}
		//___________________________________________________________________________
		private void CBBaudCOM_SelectedIndexChanged (object sender, EventArgs e)
		{
			bSavePropertyes = true;
			Properties.Settings.Default.asBaudCom = CBBaudCOM.Text;
			CP?.ChangeBaud ();
		}
		//>>>>>>>>>>>>>>>>>>>     Дополнительная панель ввода   >>>>>>>>>>>>>>>>>>>>>>
		/// <summary>
		/// Исходные настройки доп.панели PDopFunc
		/// </summary>
		struct SPDopFunc
		{
			public int iTop;
			public int iWidth;
			public int iHeight;
			public SPDopFunc (int iTop, int iWidth, int iHeight)
			{
				this.iTop = iTop;
				this.iHeight = iHeight;
				this.iWidth = iWidth;
			}
		}
		readonly SPDopFunc BeginDopFunc;

		//___________________________________________________________________________
		private void PDopFunc_MouseEnter (object sender, EventArgs e)
		{
			PDopFunc.Top = 120;
			PDopFunc.Height = PDopFunc.Parent.Height - (PDopFunc.Top + 4);
			PDopFunc.Width = PDopFunc.Parent.Width - 3;
		}
		//___________________________________________________________________________
		private void PDopFunc_MouseLeave (object sender, EventArgs e)
		{
			PDopFunc.Top = BeginDopFunc.iTop;
			PDopFunc.Width = BeginDopFunc.iWidth;
			PDopFunc.Height = BeginDopFunc.iHeight;
		}

		//=====================            R M G              =======================

		public CRMG RMG;
		private void BRMG_Click (object sender, EventArgs e)
		{
			if (RMG == null && Прибор != null)
			{
				Inv_OutMess ("Уже подключен не EC605", "");
				return;
			}

			if (RMG == null)
			{
				if (int.TryParse (TBPort.Text, out iPort))
				{
					Прибор = RMG = new CRMG (this);
					Прибор.InitIPport (CBIP.Text, iPort);
				}
				else
				{
					Inv_OutMess ("Неправильный номер порта: [" + TBPort.Text + "]", "");
					return;
				}
			}
			if (ConnDev (BRMG, "RMG on", "RMG off") == false)
			{
				RMG.Close ();
				Прибор = RMG = null;
			}
		}
		//___________________________________________________________________________
		private void NUDFontSize_ValueChanged (object sender, EventArgs e)
		{
			TBListExchange.Font = new Font (TBListExchange.Font.FontFamily, (float)NUDFontSize.Value);
		}
		//___________________________________________________________________________
		private void CBDev_SelectedIndexChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.asDevExch = CBDev.Text;
		}
		//___________________________________________________________________________
		public void Close ()
		{
			CP?.PortClose ();

		}
	}
}
