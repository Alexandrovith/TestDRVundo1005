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

namespace TestDRVtransGas
{
	public partial class TDevices : UserControl
	{
		const string sFile = "Devices.txt";
		private bool bChanged = false;

		public TDevices(object Owner, FTestDrivers ParentForm)
		{
			InitializeComponent();
			Parent = (Control)Owner;
			Read ();
			ReSize ();
		}

		public void Save()
		{
			if (bChanged == true)
			{
				bChanged = false;
				TSerialize.WriteFl (sFile, GV);
			}
		}

		public void Read()
		{
			if (File.Exists(sFile))
			{
				GV.Rows.Clear();
				TSerialize.ReadFl (sFile, GV);
			}
		}

		private void GVDevices_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			bChanged = true;
		}
		public string InitStr()
		{
			string[] sNamesFB = { CONST.DEV_SETUPS.Name.ToString(), CONST.DEV_SETUPS.Type.ToString(),
														CONST.DEV_SETUPS.ROCAddressUnit.ToString(), CONST.DEV_SETUPS.ROCAddressGroupp.ToString(),
														CONST.DEV_SETUPS.HostAddressUnit.ToString(), CONST.DEV_SETUPS.HostAddressGroupp.ToString(),
														CONST.DEV_SETUPS.BaudRate.ToString(), CONST.DEV_SETUPS.Port.ToString(), 
														CONST.DEV_SETUPS.DataBits.ToString(),	CONST.DEV_SETUPS.Parity.ToString(), 
														CONST.DEV_SETUPS.ModeConn.ToString()//, CONST.DEV_SETUPS.UrlSingleParam.ToString() 
													};//Name Type Address UrlSinglePar	PortTCP HostGr	BaudRate Port Pass1			Pass2 
														//Name Type ROCun		ROCgr					HostUn	HostGr	BaudRate Port DataBits	Parity 
			string[] sNames = { CONST.DEV_SETUPS.Name.ToString (), CONST.DEV_SETUPS.Type.ToString (), 
													CONST.DEV_SETUPS.Address.ToString(), CONST.DEV_SETUPS.UrlSingleParam.ToString(), 
													CONST.DEV_SETUPS.PortTCP.ToString(), "", 
													CONST.DEV_SETUPS.BaudRate.ToString(), CONST.DEV_SETUPS.Port.ToString(),
													CONST.DEV_SETUPS.Pass1.ToString(), CONST.DEV_SETUPS.Pass2.ToString(),
													CONST.DEV_SETUPS.ModeConn.ToString(),
                        }; //"ArchHour", "ArchDay", "ArchAlarm", "ArchInterfer"

			int iNumDev = GV.Rows.Count - 1;
			string s = "";
			for (int iRow = 0; iRow < iNumDev; iRow++)
			{
				s += "[";
				string[] sNam = (string)GV.Rows[iRow].Cells[1].Value == "FloBoss407" ? sNamesFB : sNames;
				int iNumParDev = sNam.Length;
				for (int col = 0; col < iNumParDev; col++)
				{
					s += "\"" + sNam[col] + "\":\"" + GV.Rows[iRow].Cells[col].Value + "\",";
				}
				for (int col = 0; col < 4; col++)
				{
					s += "\"" + asaTD[col] + "\":\"" + asaTDVal[col] + "\",";
				}
				s += "]";
			}
			return s;
		}
		//_________________________________________________________________________
		public void ReSize()
		{
			Width = this.Parent.Width;
			Height = this.Parent.Height;
		}
		//_________________________________________________________________________

		string[] asaTD = { "TD", "TH", "TI", "TA" };
		string[] asaTDVal = { "30082016", "30082016", "30082016", "30082016" };   //{ "01062015", "01072015", "01052015", "01052015" };

		public void SetNewDTreadArch (DateTimePicker TPDay, DateTimePicker TPHour, DateTimePicker TPInterfer, DateTimePicker TPAlarm)
		{
			asaTDVal[0] = DateToStr (TPDay);
			asaTDVal[1] = DateToStr (TPHour);
			asaTDVal[2] = DateToStr (TPInterfer);
			asaTDVal[3] = DateToStr (TPAlarm);
		}
		//_________________________________________________________________________
		public string DateToStr (DateTimePicker TP)
		{
			return string.Format ("{0:00}{1:00}{2}", TP.Value.Day, TP.Value.Month, TP.Value.Year);
		}
		//_________________________________________________________________________
	}
}
