﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDRVtransGas.COMserver
{
	public interface IDevice
	{
		bool bChageParams { get; set; }
		byte[] HandlingRecieve (byte[] BufRX, int iLenData);
	}
}
