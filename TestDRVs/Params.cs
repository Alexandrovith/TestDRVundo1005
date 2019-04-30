using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AXONIM.ScanParamOfDevicves;
using AXONIM.CONSTS;
//using Opc.Ua.Client;

namespace TestDRVtransGas
{//ParName DeviceName"":""Minsk1"",""ParamName"":""Time Clock"",""RequestName"":""3"",""Data"":""3 2000"",""RequestType"":"""",""ParameterType"":""FLOAT"",""MinTimeRequest"
	public partial class TParams : UserControl, IUserControls
	{
		FTestDrvs Own = null;
		string sFile;// = "Params.prmd";
		private bool bChanged = false;
		//_________________________________________________________________________
		public TParams (object Parent, object Owner)
		{
			InitializeComponent();

			Own = (FTestDrvs)Owner;
			sFile = Properties.Settings.Default.sFileParam;
			FDOpenFl.Filter = "Файлы параметров|*.prmd|Все файлы|*.*";
			FDOpenFl.Title = "Открытие файла параметров";
			CBFlDev.SelectedIndexChanged -= CBFlDev_SelectedIndexChanged;
			CBFlDev.Items.Add (sFile);
			CBFlDev.SelectedIndex = 0;
			CBFlDev.SelectedIndexChanged += CBFlDev_SelectedIndexChanged;

			this.Parent = (Control)Parent;
			Read();
			//ReSize ();
    }
		//_________________________________________________________________________
		public void Save()
		{
			if (bChanged == true)
			{
				if (TSerialize.WriteFl (sFile, GV))
					bChanged = false;
			}
		}
		//_________________________________________________________________________
		public void Read()
		{
			TSerialize.ReadFl (sFile, GV);
		}
		//_________________________________________________________________________	
		public string InitStr(int iRow)
		{
			string[] sNames = { CONST.ПАРАМЕТРЫ.DeviceName.ToString(), CONST.ПАРАМЕТРЫ.ParameterName.ToString(), 
													CONST.ПАРАМЕТРЫ.RequestName.ToString(), CONST.ПАРАМЕТРЫ.Data.ToString(), "RequestType",
													CONST.ПАРАМЕТРЫ.ParameterType.ToString(), CONST.ПАРАМЕТРЫ.TimeRequest.ToString(),
													CONST.ПАРАМЕТРЫ.INorOUT.ToString(), CONST.ПАРАМЕТРЫ.NameP.ToString(),		//"TableName", 
													CONST.ПАРАМЕТРЫ.NamePrus.ToString()
													};	//
			int iNumDev = GV.Rows.Count - 1;
			string s = "";
			//for (int iRow = 0; iRow < iNumDev; iRow++)
			{
				s += "[";
				for (int col = 0; col < GV.ColumnCount; col++)
				{
					s += "\"" + sNames[col] + "\":\"" + GV.Rows[iRow].Cells[col].Value + "\",";
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
		private void GV_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			bChanged = true;
		}
		//_________________________________________________________________________
		private void BOpenFl_Click (object sender, EventArgs e)
		{
			DialogResult Dr = FDOpenFl.ShowDialog();
			if (Dr == DialogResult.OK)
			{
				sFile = FDOpenFl.FileName;
				CBFlDev.SelectedIndexChanged -= CBFlDev_SelectedIndexChanged;
				CBFlDev.Items.Add (FDOpenFl.FileName);
				CBFlDev.SelectedIndex = CBFlDev.Items.Count - 1;
				CBFlDev.SelectedIndexChanged += CBFlDev_SelectedIndexChanged;

				FileChanging ();
			}
		}
		//_________________________________________________________________________
		private void CBFlDev_SelectedIndexChanged (object sender, EventArgs e)
		{
			sFile = CBFlDev.Text;
			FileChanging ();
		}
		//_________________________________________________________________________
		private void FileChanging ()
		{
			Properties.Settings.Default.sFileParam = sFile;
			Properties.Settings.Default.Save ();
			Read ();
			Own.DestroyLabelsByParam ();
			Own.CreateTemplateIndic ();
			Own.FillDevices ();
		}

		//_________________________________________________________________________
		public void Close ()
		{
		}
	}
}
