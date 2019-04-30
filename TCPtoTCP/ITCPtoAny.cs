using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestDRVtransGas.TCPtoTCP
{
	public class CTCPtoAny : UserControl, IUserControls
	{
		public delegate void DOutMess (string asMess/*, string asDelimiter*/, bool bOutTime = true);
		public DOutMess OutMess;

		public void ReSize ()
		{
			Width = this.Parent.Width;
			Height = this.Parent.Height;
		}
		//_________________________________________________________________________
		public virtual void Close () { }
	}
}
