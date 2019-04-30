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
using AXONIM.BelTransGasDRV;
using System.Threading;

namespace TestDRVtransGas
{
	public partial class CWrParams : Form
	{
		Tdrv DRV;

		//wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww

		public CWrParams ()
		{
			InitializeComponent ();
		}	
		//_________________________________________________________________________
		public CWrParams (Tdrv DRV):this()
		{
			this.DRV = DRV;

			string[] Row = Properties.Settings.Default.DataArToWr.Split (SeparRow);
			for (int iRow = 0; iRow < Row.Length; iRow++)
			{
				GVDataWr.Rows.Add (Row[iRow].Split (';'));
			}
			NUDIDwritePar.Value = Properties.Settings.Default.iIDstart;
		}		
		//_________________________________________________________________________
		private void BClose_Click (object sender, EventArgs e)
		{
			Close ();
		}
		//_________________________________________________________________________
		const char SeparRow = ':';
		const char SeparCol = ';';
		void PrepareDataToStore()
		{
			string asData = "";
			for (int iRow = 0; iRow < GVDataWr.RowCount; iRow++)
			{
				for (int iCol = 0; iCol < (int)Params.SIZE; iCol++)
				{
					asData += GVDataWr.Rows[iRow].Cells[iCol] + ";";
				}
				asData += ";";
			}
			Properties.Settings.Default.asDataWritePar = asData;
		}
		//_________________________________________________________________________
		private void BWritePar_Click (object sender, EventArgs e)
		{
			Properties.Settings.Default.Save ();

			int iID = (int)NUDIDwritePar.Value;

			for (int iRow = 0; iRow < GVDataWr.RowCount; iRow++)
			{
				try
				{
					WriteToDev (iID++, iRow);
					Thread.Sleep ((int)NUDInterval.Value);
				}
				catch (Exception exc)
				{
				}
			}
		}
		//_________________________________________________________________________

		enum Params { Прибор, Data, DataToWr, Typ, ParamName, TimeResp, ПоследовБайт,
		SIZE};
		enum EReverce { Прямая, Обратная, Обратная_по_2_байта };

		public void WriteToDev (int iID, int iRow)
		{
			string sParams = "[\"" + CONST.ПАРАМЕТРЫ.DeviceName.ToString () + "\":\"" + GVDataWr.Rows[iRow].Cells[(int)Params.Прибор].Value + "\"," +
															 CONST.ПАРАМЕТРЫ.Data.ToString () + "\":\"" + GVDataWr.Rows[iRow].Cells[(int)Params.Data].Value + "\"," +
															 CONST.ПАРАМЕТРЫ.ParameterType.ToString () + "\":\"" + GVDataWr.Rows[iRow].Cells[(int)Params.Typ].Value + "\"," +
															 CONST.ПАРАМЕТРЫ.ParameterName.ToString () + "\":\"" + GVDataWr.Rows[iRow].Cells[(int)Params.ParamName].Value + "\"," +
															 CONST.ПАРАМЕТРЫ.RequestName.ToString () + "\":\"MT16\"," +
															 CONST.ПАРАМЕТРЫ.INorOUT.ToString () + "\":\"out" + "\"," +
															 CONST.ПАРАМЕТРЫ.RequestType.ToString () + "\":\"" + CONST.RequestType.Single.ToString () + "\"," +
															 CONST.ПАРАМЕТРЫ.TimeRequest.ToString () + "\":\"" + GVDataWr.Rows[iRow].Cells[(int)Params.TimeResp].Value +
				"\"]";

			FTestDrvs FormOwn = Owner as FTestDrvs;

			EReverce Reverce = (EReverce)Enum.Parse(typeof (EReverce), GVDataWr.Rows[iRow].Cells[(int)Params.ПоследовБайт].Value.ToString());
			byte[] btaDataWritePar = StrToTypeThenBytes (GVDataWr.Rows[iRow].Cells[(int)Params.DataToWr].Value.ToString (),
				GVDataWr.Rows[iRow].Cells[(int)Params.Typ].Value.ToString (),
				Reverce, iRow);

			if (btaDataWritePar != null)
			{
				DRV.WriteValue (iID, sParams, btaDataWritePar);
			}
		}		
		//_________________________________________________________________________
		byte[] StrToTypeThenBytes (string asVal, string asTypeVal, EReverce Reverce, int iRow)
		{
			try
			{
				TYPE TheType = (TYPE)Enum.Parse (typeof (TYPE), GVDataWr.Rows[iRow].Cells[(int)Params.Typ].Value.ToString());
				dynamic oVal = Global.StrToType (asVal, TheType);
				if (oVal.GetType ().ToString ().Equals ("System.String"))
					return new byte[1];
				if (Reverce == EReverce.Обратная)
				{
					byte[] btaRev = BitConverter.GetBytes (oVal);
					return btaRev.Reverse ().ToArray ();
				}

				if (Reverce == EReverce.Обратная_по_2_байта)
				{
					int iTypeSize = CONST.SizeTypeData (TheType);
					byte[] btRet = new byte[iTypeSize];
					Global.AppendTwoBytesRev (BitConverter.GetBytes (oVal), 0, btRet, 0, iTypeSize);
					return btRet;
				}
				return BitConverter.GetBytes (oVal);
			}
			catch (Exception exc)
			{
				MessageBox.Show (string.Format ("Значение параметра [{0}] не соответствует типу [{1}].{2}",
													asVal, asTypeVal, exc.StackTrace));
				return null;
			}
		}
		//_________________________________________________________________________
		private void NUDIDwritePar_ValueChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.iIDstart = (int)NUDIDwritePar.Value;
		}
		//_________________________________________________________________________

	}
}
