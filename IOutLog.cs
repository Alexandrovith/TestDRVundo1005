using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDRVtransGas
{
	public interface IOutLog
	{
		void Out (string asMess, bool bDateOut = true);
	}

	//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

	public class COutLog : IOutLog
	{
		public delegate void DOutLog (string asMess, bool bDateOut = true);
		DOutLog LogAction;

		//wwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwwww

		public COutLog (DOutLog LogAction)
		{
			this.LogAction = LogAction;
		}
		//_________________________________________________________________________
		public void Out (string asMess, bool bDateOut = true)
		{
			LogAction?.Invoke (asMess, bDateOut);
		}
	}
}
