using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AXONIM.ScanParamOfDevicves;

namespace TestDRVtransGas.TestEXCH
{
	public class CClientTCP : CExchange
	{
		/// <summary>
		/// Подписка на получение данных из приёмного буфера
		/// </summary>
		public EventHandler EvRecieve;

		public CClientTCP (CTestExch Own) : base (Own)
		{
		}
		//_________________________________________________________________________
		public override bool HandlingData (CModbusTCP MTCP)
		{
			if (base.HandlingData (MTCP))
			{
				EvRecieve?.Invoke (BufRX, EventArgs.Empty);
				return true;
			}
			return false;
		}
	}
}
