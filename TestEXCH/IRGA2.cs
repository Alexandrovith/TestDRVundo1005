using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AXONIM.ScanParamOfDevicves;
using AXONIM.CONSTS;
using TestDRVtransGas.TestEXCH;

namespace TestDRVtransGas
{
	public class CIRGA2 : CClientTCP
	{
		public CIRGA2 (CTestExch Own) : base (Own)
		{
		}
		////_________________________________________________________________________
		//public override bool Request (string asCommand)
		//{
		//	return base.Request (asCommand);
		//}
		////_________________________________________________________________________
		//public override void HandlingData (CModbusTCP MTCP)
		//{
		//	base.HandlingData (MTCP);
		//}
		////_________________________________________________________________________
		//public override void RecieveData (IAsyncResult ar)
		//{
		//	throw new NotImplementedException ();
		//}
	}
}
