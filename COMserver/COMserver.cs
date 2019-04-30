using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AXONIM.CONSTS;

namespace TestDRVtransGas.COMserver
{
	public partial class FCOMserver : Form
	{
		COMPort ComPort;
		CTrace Log;
		public FCOMserver (Form own)
		{
			InitializeComponent ();
			Owner = own;
			ColorCBPort = CBPort.BackColor;
			CBBaud.SelectedIndex = CBBaud.FindString (Properties.Settings.Default.asBaud);
			CBPort.Items.AddRange (COMPort.GetPortNames ());
			CBPort.SelectedIndex = CBPort.FindString (Properties.Settings.Default.asCOMport);
			//Log = new TTrace ();
		}
		//___________________________________________________________________________
		private void FCOMserver_FormClosing (object sender, FormClosingEventArgs e)
		{
			Owner.Visible = true;
			//CloseAll ();
			//((FTestDrvs)Owner).TCPserver = null;
			Properties.Settings.Default.Save ();
			if (ComPort != null)
			{
				ComPort.ClosePort ();
			}
		}
		//___________________________________________________________________________
		private void FCOMserver_Load (object sender, EventArgs e)
		{
			Location = new Point (Owner.Left + 110, Owner.Top + Owner.Height - Height);
		}
		//___________________________________________________________________________
		private void BHideOwner_Click (object sender, EventArgs e)
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
		//___________________________________________________________________________
		private void BClose_Click (object sender, EventArgs e)
		{
			Owner.Visible = true;
			Close ();
		}
		//_________________________________________________________________________
		string AddDTtoMessage (string asMess, bool bDataTimeShow = true)
		{
			if (bDataTimeShow)
			{
				DateTime DT = DateTime.Now;
				//return string.Format ("{0:dd_MM_yy hh_mm_ss}.{1:000}{2}", DT, DT.Millisecond, asMess);
				return string.Format ("{2:hh:mm:ss}.{0:000} {1}", DT.Millisecond, asMess, DT);
			}
			return asMess;
		}
		//___________________________________________________________________________

		public delegate void DMessageShow(string asMess);
		DMessageShow MS;

		private void OutToTB (string asMess)
		{
			TBOut.AppendText (AddDTtoMessage(asMess) + Environment.NewLine);
		}
		//___________________________________________________________________________
		public void MessShow(string asMess)
		{
			MS = OutToTB;
			Invoke (MS, new object[] { asMess });
		}
		//___________________________________________________________________________

		Color ColorCBPort;
		private void BConnect_Click (object sender, EventArgs e)
		{
			if (CBPort.Text.Length == 0)
			{
				MessageBox.Show ("Не задан порт");
				CBPort.Focus ();
			}
			else
			{
				if (ComPort == null)
				{
					ComPort = new COMPort (this);
				}
				if (ComPort.PortIsOpen())
				{
					ComPort.ClosePort ();
					TBOut.AppendText (string.Format ("Порт [{0}] закрыт\n", ComPort.PortName ()));
					BConnect.Text = "Открыть";
				}
				else
				{
					if (ComPort.OpenPort (CBPort.Text, CBBaud.Text) != null)
					{
						BConnect.Text = CBPort.Text;
						TBOut.AppendText (string.Format ("Порт [{0}] открыт\n", ComPort.PortName ()));
					}
				}		
			}			
		}
		//___________________________________________________________________________
		private void CBPort_SelectedIndexChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.asCOMport = CBPort.Text;
		}
		//___________________________________________________________________________
		private void CBBaud_SelectedIndexChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.asBaud = CBBaud.Text;
		}
		//___________________________________________________________________________
	}
}
