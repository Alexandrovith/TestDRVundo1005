using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using AXONIM.ScanParamOfDevicves;
using AXONIM.CONSTS;
using Ionic.Zip;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using AXONIM.CONSTS.LOG;
using System.ServiceProcess;

namespace TestDRVtransGas.TestsDifferent
{
	public partial class CTestDifferent : UserControl, IUserControls
	{
		FTestDrvs Own;
		//System.Collections.ArrayList l;
		delegate void DWrParams ();
		DWrParams WrParams;
		byte btReg = 1;

		public CTestDifferent (FTestDrvs Owner, TabPage Prnt)
		{
			InitializeComponent ();
			Own = Owner;
			Parent = Prnt;
			// l = new System.Collections.ArrayList ();
			//TBOut.Text = l.Capacity.ToString();
			//l.Add (5);
			//TBOut.Text = TBOut.Text + " " + l.Capacity.ToString();
			//l.Add (6);
			//TBOut.Text = TBOut.Text + " " + l.Capacity.ToString();
			CBDirBites.SelectedIndex = CBDirBites.FindString (Properties.Settings.Default.asDirBites);

			CBDayHour.Items.AddRange (Enum.GetNames (typeof (EDeltaDT)));
			CBDayHour.SelectedIndex = CBDayHour.FindString (Properties.Settings.Default.DeltaDayHour);

			string[] asRows = Properties.Settings.Default.as8n1To7e1.Split (';');
			int iVal = 0;
			int iCountRows = asRows.Count () / 2;
			for (int i = 0; i < iCountRows; i++)
			{
				GV8n1To7e1.Rows.Add ();
				GV8n1To7e1.Rows[i].Cells[0].Value = asRows[iVal++];
				GV8n1To7e1.Rows[i].Cells[1].Value = asRows[iVal++];
			}
			CB7bit.Checked = Properties.Settings.Default.b7bit;
			ChCalcBCC.Checked = Properties.Settings.Default.bCalcBCC;

			asRows = Properties.Settings.Default.asDGBite8nTo7e.Split (';');
			iVal = 0;
			iCountRows = asRows.Count () / 2;
			for (int i = 0; i < iCountRows; i++)
			{
				GVBites8nTo7e.Rows.Add ();
				GVBites8nTo7e.Rows[i].Cells[0].Value = asRows[iVal++];
				GVBites8nTo7e.Rows[i].Cells[1].Value = asRows[iVal++];
			}

			asRows = Properties.Settings.Default.asBtToStr.Split (';');
			iVal = 0;
			iCountRows = asRows.Count () / 2;
			for (int i = 0; i < iCountRows; i++)
			{
				GVBtToStr.Rows.Add ();
				GVBtToStr.Rows[i].Cells[0].Value = asRows[iVal++];
				GVBtToStr.Rows[i].Cells[1].Value = asRows[iVal++];
			}
			ChDataAsHex.Checked = Properties.Settings.Default.bChDataAsHex;
		}

		//ccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccc

		byte[] btaRespon = { /*0xC9, */ 0x2B, 0x0,0x4D, 0x0, 0x4F, 0x0, 0x68, 0x25, 0x23, 0x41, 0x9A, 0x9C, 0xA1, 0x43, 0x90, 0x38, 0x62, 0x44, 0x68, 0x25, 0x23, 0x41, 0x62, 0xEC, 0xC7, 0x42, 0xFF, 0xFF, 0xFF, 0xFF, 0xBA, 0x20, 0x7D, 0x0, 0x0, 0x80, 0x40, 0xFF, 0x0, 0xB0, 0x2, 0x62, 0x45, 0xFF, 0xFF,
												0x58, 0x7E };
		private void BCountCRC_Click (object sender, EventArgs e)
		{
			ushort usCRC = Global.CRCirga2 (btaRespon, btaRespon.Length - 2, btaRespon.Length, 0);
			//ushort usCRC = Global.CRC8_8 (btaRespon, btaRespon.Length - 2);	//(btaRespon, btaRespon.Length - 2);
			//ushort usCRC = Global.CRC (btaRespon, btaRespon.Length - 2, btaRespon.Length, Global.Table1281, 0xFFFF, 0);
			//ushort usCRC = Global.CRC (btaRespon, btaRespon.Length - 2, btaRespon.Length, Global.Table8005, 0, 0);
			TBCRCcalc.Text = usCRC.ToString ("X");
		}
		//ccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccc
		//_________________________________________________________________________
		public void ReSize ()
		{
			Width = this.Parent.Width;
			Height = this.Parent.Height;
		}
		//____________________      Байты - число      ____________________________
		private void BBytesToVal_Click (object sender, EventArgs e)
		{
			try
			{
				float fVal = Global.ByteToType (StrHexToBytes (TBBytes.Text), TYPE.@float);
				TBValFromBytes.Text = fVal.ToString ();
			}
			catch (Exception)
			{
			}
		}
		//_________________________________________________________________________
		static public byte[] StrHexToBytes (string asHex)
		{
			try
			{
				string[] asaBytes = asHex.Split (' ');
				byte[] btaBytes = new byte[asaBytes.Length];
				int i = 0;
				foreach (var item in asaBytes)
				{
					btaBytes[i++] = (byte)Global.StrToType (item, TYPE.@byte);
				}
				return btaBytes;
			}
			catch (Exception exc)
			{
				MessageBox.Show ($"{exc.Message}");
			}
			return new byte[1];
		}
		//_________________________________________________________________________
		byte[] btaBufConv = new byte[64];
		private void AllocationValsDoubl (byte[] btBufRX)
		{
			string asData = GetData (btBufRX);
			if (asData.Length > 0)
			{
				double dVal = double.Parse (asData);
				byte[] btaBufVal = BitConverter.GetBytes (dVal);
				//Value.Recieve.FillValDef (btaBufVal);
			}
		}
		//_________________________________________________________________________
		public enum EComGOST : byte { SOH = 1, STX = 2, ETX, EOT, ACK = 6, NAK = 0x15, LF = 0x0A, CR = 0x0D }
		/// <summary>
		/// Получение строки данных из ответа прибора
		/// </summary>
		/// <param name="btBufRX">Буфер с ответом от прибора</param>
		private string GetData (byte[] btBufRX)
		{
			//if (CRCtrue (btBufRX))
			{
				int iPosGap = 3;
				if (btBufRX.Contains ((byte)'(') && btBufRX.Contains ((byte)')') &&
						btBufRX.First () == (byte)EComGOST.STX && btBufRX.Contains ((byte)EComGOST.ETX))
				{
					while (btBufRX[iPosGap++] != '(') ;
					int iBeginData = iPosGap;
					byte bt;
					do
					{
						bt = btBufRX[iPosGap];
						//if (bt == '.')
						//	bt = (byte)',';
						btaBufConv[iPosGap++] = bt;
					}
					while (Char.IsNumber ((char)bt) || bt == '.' || bt == ',' || bt == '-' || bt == ':');
					string asData = Global.ByteToStr (btaBufConv, iBeginData, iPosGap - iBeginData - 1);
					return asData;
				}
			}
			return "";
		}
		//_________________________________________________________________________2 31 3A 34 30 30 2E 32 28 32 30 31 38 2D 31 31 2D 30 31 2C 31 30 3A 35 36 3A 30 36 29 3 3  [[2]1:400.2(2018-11-01,10:56:06)[3][3]
		private void AllocationValsDT (byte[] btBufRX)
		{
			//if (CComEK270.StateFirst == CComEK270.EStateFirstRequest.End && btBufRX[0] != 0x7F && CRCtrue (btBufRX))
			{
				string asData = GetData (btBufRX);
				if (asData.Length > 0)
				{
					//DateTime DT;
					//if (DateTime.TryParse (asData, out DT))
					{
						byte[] btaBufVal = Global.EncodingCurr.GetBytes (asData.Replace (',', ' '));
						//Value.Recieve.FillValDef (btaBufVal);}
					}
				}
			}
		}
		//_________________________________________________________________________
		const byte b0 = (byte)'0';
		const byte b1 = (byte)'1';
		const byte b2 = (byte)'2';
		const byte b3 = (byte)'3';
		const byte b4 = (byte)'4';
		const byte b5 = (byte)'5';
		const byte b6 = (byte)'6';
		const byte b7 = (byte)'7';
		const byte b8 = (byte)'8';
		const byte b9 = (byte)'9';
		static readonly byte[] btaWr = { 1, (byte)'W', b1, 2, (byte)'{', b1, b4, (byte)':', b0, b3, b1, b4, (byte)'.', b0, (byte)'}', (byte)'(', (byte)'1', (byte)')', 0x3 };
		byte[] CreateComByWr (string asAddr, byte[] btaVal)
		{
			double dVal = BitConverter.ToDouble (btaVal, 0);
			string asVal = Convert.ToString (dVal);
			byte[] btaValASCI = Encoding.ASCII.GetBytes (asVal);
			asAddr += ".0";
			byte[] btaRet = new byte[7 + asAddr.Length + asVal.Length + 5];
			// Заголовок 
			int iPos = 0;
			for (; iPos < 7; iPos++)
			{
				btaRet[iPos] = btaWr[iPos];
			}
			// Адрес 
			for (int i = 0; i < asAddr.Length; i++, iPos++)
			{
				btaRet[iPos] = Convert.ToByte (asAddr[i]);
			}
			btaRet[iPos++] = (byte)'}';
			btaRet[iPos++] = (byte)'(';

			// Значение 
			for (int i = 0; i < asVal.Length; i++, iPos++)
			{
				btaRet[iPos] = Convert.ToByte (asVal[i]);
			}
			// Завершение команды 
			btaRet[iPos++] = (byte)')';
			btaRet[iPos++] = (byte)3;// CRecieveEK270.EComGOST.ETX;
			btaRet[iPos] = Global.CalcBCC_EK270 (btaRet, 0, iPos + 1);
			return btaRet;
		}
		//___________________________________________________________________________
		byte[] btaRowPart = new byte[300];
		int iLenRowPart;
		int iQuantRowBlock;

		string TstEK (byte[] btaFrom, int iPos)
		{
			byte bt = btaFrom[iPos++];
			while (!(bt == (byte)'(' && btaFrom[iPos] == (byte)'C' && btaFrom[iPos + 1] == (byte)'R'))
			{
				bt = btaFrom[iPos++];
				if (bt == 0)              // Если получена неполная строка 
				{
					return null;
				}
				btaRowPart[iLenRowPart++] = bt;
			}
			return Global.ByteToStr (btaRowPart, 0, iPos);
		}
		//=========================================================================

		private void GetValsInRow (byte[] btaBufRX)
		{
			btaBufRX[LenRecieve] = 0;
			byte bt;

			if (iLenRowPart > 0)        // Если была ранее недополучена строка данных 
			{
				// Заполняем недостающие байты 
				do
				{
					bt = btaBufRX[iPosRX++];
					if (bt == 0)            // Если получена неполная строка 
					{
						return;
					}
					btaRowPart[iLenRowPart++] = bt;
				} while (!(bt == (byte)')' && btaBufRX[iPosRX - 2] == (byte)'k' && btaBufRX[iPosRX - 3] == (byte)'O'));
				//++iPosRX;
				iLenRowPart = 0;
				int iIncPos = 0;
				GetValsInBracks (btaRowPart, ref iIncPos);
				if (iLenRowPart == 0)
				{
					iPosRX += iIncPos;
				}
				return;
			}
			else                        // Если предыдущий ответ не был обрывочным 
			{
				if (btaBufRX[iPosRX] != (byte)'(')
				{
					CurrBuf = (int)ECurrBuf.ACK;
					iLenRowPart = 0;
					iQuantRowBlock = 0;

					return;
				}
				GetValsInBracks (btaBufRX, ref iPosRX);       //return asRet;
			}
		}
		//..........................................................................
		byte[] btaVal = new byte[32];
		void GetValsInBracks (byte[] btaFrom, ref int iPosRX)
		{
			int iStatementValsPos = 0;
			int iCurrValPos = 0;
			bool bFillVal = false;
			int iPosInVal = 0;
			//byte[] btaVal = Value.Прибор.btaCurr;
			byte bt;

			do
			{
				bt = btaFrom[iPosRX++];
				if (bt == 0)                        // Если получена неполная строка 
				{         //asaRow = null;
					return;
				}
				btaRowPart[iLenRowPart++] = bt;     // На случай неполного ответа 

				// Выделение значений требуемых (iaStatementVals) параметров 
				if (bt == (byte)'(')                // Начало значения очередного 
				{
					if (iCurrValPos++ == StatemenstVals[iStatementValsPos])
					{
						bFillVal = true;                // Очередное требуемое значение наступило 
					}
				}
				else if (bFillVal)
				{
					if (Char.IsNumber ((char)bt) || bt == '.' || bt == ',' || bt == '-' || bt == ':' || bt == 'x')
					{
						btaVal[iPosInVal++] = bt;       // Заполняем значение 
					}
					else
					{
						asaRow[iStatementValsPos] = Global.ByteToStr (btaVal, 0, iPosInVal);
						bFillVal = false;
						if (++iStatementValsPos >= StatemenstVals.Length)
						{
							// Ищем конец строки 
							while (!(bt == (byte)'O' && btaFrom[iPosRX + 1] == (byte)'k' && btaFrom[iPosRX + 2] == (byte)')'))
							{
								bt = btaFrom[++iPosRX];
								if (bt == 0)              // Если получена неполная строка 
								{                 //asaRow = null;
									return;
								}
								btaRowPart[iLenRowPart++] = bt;
							}
							iPosRX += 3 + 2;            // Добираем Ok)da
							iLenRowPart = 0;
							break;
						}
						iPosInVal = 0;                // Конец 
					}
				}
			} while (!(bt == (byte)'O' && btaFrom[iPosRX] == (byte)'k' && btaFrom[iPosRX + 1] == (byte)')'));     //return asaRow;
		}
		//..........................................................................
		int iPosRX;
		string[] asaRow = new string[5];
		public enum ECurrBuf { QuantReq, Data, NAK, ACK }
		public int CurrBuf { get; protected set; }
		int[] StatemenstVals = { 1 };
		int LenRecieve;

		//_________________________________________________________________________0x2, 0x37, 0x2D, 0x36, 0x3A, 0x33, 0x31, 0x30, 0x5F, 0x31, 0x2E, 0x31, 0x28, 0x32, 0x30, 0x2E, 0x31, 0x36, 0x2A, 0x7B, 0x43, 0x29, 0x3, 0x6E  [[2]7-6:310_1.1(20.16*{C)[3]n
		byte[] btaBufRX = { 1, 1, 1, 1, 2 };
		DateTime DT = DateTime.Now;
		string[] asLines;
		List<int> Li = new List<int> { 1, 2, 3, 4, 5, 6, 7 };

		//.........................................................................
		private void BTest_Click (object sender, EventArgs e)
		{
			//int iVal = Global.BCDtoInt(0x25);
			try
			{
				//List<string> FileNames = new List<string> { "1", "2", "3" };
				//TBResult.Text = string.Join (Environment.NewLine, FileNames);
				GetParFromHTM ();
				//Str_Byte_Str ();

				//TestService ();
				//TestRdFilesFromHttp ();
				//TestFtpWebRequest ();
				//TestNameColToNum ();
				//TestEK270 ();

				//string asPart = "(0x8104)(CRC Ok)  ";//(0.9967)(0)(114.74)(0)(0)(0)(0)(0)
				//TstEK (Encoding.ASCII.GetBytes (asPart), 0);

				//CreateComByWr ("5:432", BitConverter.GetBytes (1.234567));

				//AllocationValsDT (new byte[] { 2, 0x31, 0x3A, 0x34, 0x30, 0x30, 0x2E, 0x32, 0x28, 0x32, 0x30, 0x31, 0x38, 0x2D, 0x31, 0x31, 0x2D, 0x30, 0x31, 0x2C, 0x31, 0x30, 0x3A, 0x35, 0x36, 0x3A, 0x30, 0x36, 0x29, 3, 3 });

				//int Error = 123, HandleClient = 456;
				//StringBuilder sb = new StringBuilder (" ", 256);
				//sb.AppendFormat ("{0:x} {1:x}", Error, HandleClient);
				//TBOut.Text = sb.ToString () + " : ";

				//StringBuilder sb2 = new StringBuilder ($"{Error:x} {HandleClient:x}", 256);
				//TBOut.AppendText (sb2.ToString ());

				//string asV = $"{Error:0:x} {HandleClient:1:x}";
				////TBOut.Text = string.Format ("20{2,2:X2}-{1,X2}-{0,2:X2}", (byte)UDDay.Value, (byte)UDMonth.Value, (byte)UDYear.Value);//System.Globalization.NumberStyles

				//DateTime DT = DateTime.Now;
				//DT = DT.AddDays(-1);
				//DT = DT.AddHours ((double)UDDay.Value);
				//double dNextTime = DT.Day;

				//DateTime DT = DateTime.Now.AddHours ((double)UDMonth.Value);
				//uint uiContractHour = (uint)UDDay.Value;
				//double dNextTime = uiContractHour > DT.Hour ? uiContractHour - DT.Hour : uiContractHour + 24 - DT.Hour;
				//dNextTime = dNextTime * 60 * 60000 + IncAddInterval (); /*+ (65 - DT.Minute) * 60000 + Dev.AddInterval*/
				//TBOut.Text = dNextTime.ToString ();

				//TimeSpan TS = TimeSpan.Zero;
				//DateTime DTDev;
				//bool bConv = DateTime.TryParse ("2017-02-14 12:28:12", out DTDev);
				//if (bConv)
				//{
				//  TS = DTDev - DateTime.Now;
				//}
				//TBOut.Text = TS.ToString ();
				//DTDev = DateTime.Now + TS;

				//DateTime DT = DateTime.Now.AddHours(-7);
				//DT = DT.AddDays (-1);
				//uint uiContractHour = (uint)UDDay.Value;
				//DT = DT.AddHours (-uiContractHour);
				//TBOut.Text = DT.ToString();

				//DateTime DTDev = DateTime.Now;
				//string asDate = DTDev.ToString ("s").Replace ('T', ' ');
			}
			catch (Exception exc)
			{
				MessageBox.Show (exc.Message + ". " + exc.StackTrace);
			}
		}
		//_________________________________________________________________________
		private void GetParFromHTM ()
		{
			string asE = "<!DOCTYPE html PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\">\n<!-- saved from url=(0032)http://10.8.12.181/dynkoo_38.htm -->\n<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=windows-1252\">\n<script src=\"./dynkoo_38_files/jquery.js.&#1041;&#1077;&#1079; &#1085;&#1072;&#1079;&#1074;&#1072;&#1085;&#1080;&#1103;\"></script><script src=\"./dynkoo_38_files/svn_rev_js.js.&#1041;&#1077;&#1079; &#1085;&#1072;&#1079;&#1074;&#1072;&#1085;&#1080;&#1103;\"></script><script src=\"./dynkoo_38_files/ajax_skripte.js.&#1041;&#1077;&#1079; &#1085;&#1072;&#1079;&#1074;&#1072;&#1085;&#1080;&#1103;\"></script>\n    <style type=\"text/css\">\n      h1 {font-size: 17pt; font-family: verdana,helvetica; text-decoration: none; white-space:nowrap;}\n      h2 {font-size: 15pt; font-family: verdana,helvetica; text-decoration: none; white-space:nowrap;}\n      h3 {font-size: 13pt; font-family: verdana,helvetica; text-decoration: none; white-space:nowrap;}\n      tr {font-size: 10pt; font-family: verdana,helvetica; text-decoration: none; white-space:nowrap;}\n      td {font-size: 10pt; font-family: verdana,helvetica; text-decoration: none; white-space:nowrap;}\n      p  {font-size: 10pt; font-family: verdana,helvetica; text-decoration: none; white-space:nowrap;}\n      ul {font-size: 10pt; font-family: verdana,helvetica; text-decoration: none; white-space:nowrap;}\n      a  {font-family: verdana,helvetica;}\n      td.gross {font-size: 12pt; font-weight:bold; font-family: verdana, helvetica; text-decoration: none; white-space:nowrap;}\n      td.Blau  {color: blue; font-family: verdana, helvetica; text-decoration: none; white-space:nowrap;}\n    </style>\n<meta http-equiv=\"expires\" content=\"0\">\n\n<meta http-equiv=\"cache-control\" content=\"no-cache\">\n<meta http-equiv=\"pragma\" content=\"no-cache\">\n<link rel=\"shortcut icon\" type=\"image/x-icon\" href=\"http://10.8.12.181/rmg.ico\">\n<title>Parameterization drkaDim\n</title>\n</head><body>\n<table class=\"tabWithoutEvents\" border=\"0\" bgcolor=\"#EEEEEE\"><tbody><tr bgcolor=\"#E4E4E4\">\n<td id=\"hersteller\">RMG Messtechnik</td>\n<td id=\"gerSerie\">ERZ 2000-NG</td>\n<td id=\"versionAP\">1.7.0</td>\n<td id=\"baujahr\">2018</td>\n<td id=\"schiene\">Ultrasonicmeter</td>\n<td id=\"messort\">ГРС Ельск</td>\n<td id=\"fabrikNr\">607428</td>\n<td id=\"now_2\">04-03-2019 15:56:42</td>\n</tr><tr bgcolor=\"#E4E4E4\"><td><input class=\"button\" type=\"button\" value=\"Print\" onclick=\"print();\"></td>\n<td id=\"actAccess2\">Closed</td>\n<td id=\"profil\">Service</td>\n<td><a href=\"http://10.8.12.181/dynerrs.htm\">Fault display</a></td>\n<td></td><td id=\"actErr_2\">no error</td>\n<td id=\"actAjaxConn\">1</td>\n<td align=\"right\"><input class=\"button\" type=\"button\" value=\"Refresh\" onclick=\"history.go(0);\"></td>\n</tr></tbody></table>\n<form action=\"http://10.8.12.181/dyndoparm_2\" method=\"post\" name=\"param\">\n<table bgcolor=\"#EEEEEE\">\n<tbody><tr><th>Access</th><th>Line</th><th>Designation</th><th>Value</th><th>Unit</th></tr><tr bgcolor=\"#DA70D6\"><td>G *</td><td>4</td><td>Unit</td><td id=\"drkaDim\" align=\"right\" class=\"gross\">MPa</td>\n<td></td></tr>\n</tbody></table>\n</form><script>function AjaxSpalteSetzen(){g_spalte=-3;g_sessionId=169604;}; function SVNRev_Setzen(){g_SVN_ERZ=1219};</script>\n\n</body></html>";
			//Regex regex = new Regex ($"(?<=drkaDim)>[*]<");
			//Regex regex = new Regex ($"(?<=drkaDim[.*])MPa");
			//Regex regex = new Regex ($"(?<=drkaDim\" align=\"right\" class=\"gross\")>(?<name>.*)<");//[.*] work
			//<td id=\"drkaDim\" align=\"right\" class=\"gross\">MPa</td>
			Regex regex = new Regex ("(?<=td id=\"drkaDim\"[\\w\\s\\W]*>)(?<name>.*)</td>");
			MatchCollection matches = regex.Matches (asE);
			List<string> ListFiles = null;

			if (matches.Count > 0)
			{
				string asLine = matches[0].Groups[1].ToString ();
				ListFiles = new List<string> (matches.Count);

				int iMatch = 0;
				foreach (Match match in matches)
				{
					if (match.Success)
					{
						++iMatch;
						//string asLine = match.Groups[0].Value;// SyncRoot.ToString ();
						//string asLine = match.Groups[0].ToString ();
						string asLine1 = match.Groups[1].ToString ();
						//string asLine1 = match.Groups["name"].ToString ();
						ListFiles.Add (asLine1);
						TBResult.AppendText (asLine1 + Environment.NewLine);
					}
				}
				TBResult.AppendText ("Всего совпадений " + iMatch.ToString ());
			}
			else TBResult.Text = "Нет данных";
		}
		//_________________________________________________________________________
		private static void Str_Byte_Str ()
		{
			string SrcStr = "йцукResource1.String1";
			byte[] srcbyte = System.Text.ASCIIEncoding.ASCII.GetBytes (SrcStr);
			System.Text.UTF8Encoding utf = new UTF8Encoding ();
			string UtfStr = utf.GetString (srcbyte);
		}
		//_________________________________________________________________________
		bool TestService ()
		{
			//ServiceController[] scServices;
			//scServices = ServiceController.GetServices ();

			//// Display the list of services currently running on this computer.

			//Console.WriteLine ("Services running on the local computer:");
			//foreach (ServiceController scTemp in scServices)
			//{
			//	if (scTemp.Status == ServiceControllerStatus.Running)
			//	{
			//		// Write the service name and the display name
			//		// for each running service.
			//		Console.WriteLine ();
			//		Console.WriteLine ("  Service :        {0}", scTemp.ServiceName);
			//		Console.WriteLine ("    Display name:    {0}", scTemp.DisplayName);
			//	}
			//}
			ServiceController sc = new ServiceController ("FlexVisServer");
			TBResult.Text = sc.Status.ToString ();
			return sc.Status == ServiceControllerStatus.Running;
		}
		//_________________________________________________________________________
		private void TestRdFilesFromHttp ()
		{
			ThreadPool.QueueUserWorkItem ((o) =>
			{
				Action<List<string>> D = RecieveFiles;
				Invoke (D, GetFiles (TBOut.Text));
			}, null);
		}
		//_________________________________________________________________________
		void RecieveFiles (List<string> asFiles)
		{
			TBResult.Lines = asFiles.ToArray ();
		}
		//_________________________________________________________________________
		private void TestReadDirFromHttp ()
		{
			string url = @"http://localhost:81/NG";
			//HttpWebRequest request = (HttpWebRequest)WebRequest.Create (url);
			//using (HttpWebResponse response = (HttpWebResponse)request.GetResponse ())
			//{
			//	using (StreamReader reader = new StreamReader (response.GetResponseStream ()))
			//	{
			//		string html = reader.ReadToEnd ();
			//		Regex regex = new Regex (GetDirectoryListingRegexForUrl (url));
			//		MatchCollection matches = regex.Matches (html);
			//		if (matches.Count > 0)
			//		{
			//			foreach (Match match in matches)
			//			{
			//				if (match.Success)
			//				{
			//					TBResult.AppendText (match.Groups["name"].ToString());
			//				}
			//			}
			//		}
			//	}
			//}
			//HtmlDocument doc = new HtmlDocument ();
			//doc.Load (strURL);
			//foreach (HtmlNode link in doc.DocumentElement.SelectNodes ("//a@href")
			//{
			//	HtmlAttribute att = link"href";
			//	//do something with att.Value;
			//}
		}
		//_________________________________________________________________________
		public static List<string> GetFiles (string asUrl)
		{
			List<string> ListFiles = new List<string> (32);
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create (asUrl);
			try
			{
				using (HttpWebResponse response = (HttpWebResponse)request.GetResponse ())
				{
					using (StreamReader reader = new StreamReader (response.GetResponseStream ()))
					{
						string html = reader.ReadToEnd ();

						Regex regex = new Regex ("<a href=\".*\">(?<name>.*)</a>");
						//Regex regex = new Regex ("<a href=\"?.tsv\">(?<name>.tsv)</a>");
						MatchCollection matches = regex.Matches (html);

						if (matches.Count > 0)
						{
							foreach (Match match in matches)
							{
								if (match.Success)
								{
									string[] matchData = match.Groups[0].ToString ().Split ('\"');
									string asLine = matchData[1];
									if (asLine.Length > "dyntsvag_0_".Length)
										ListFiles.Add (asLine);
								}
							}
						}
					}
				}
			}
			catch (Exception exc)
			{
				Global.LogWriteLine ($"{exc.Message}{Environment.NewLine}{exc.StackTrace}");
			}
			return ListFiles;
		}
		//_________________________________________________________________________
		private void TestFtpWebRequest ()
		{
			FtpWebRequest request = (FtpWebRequest)WebRequest.Create ("ftp://127.0.0.1/");

			request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
			request.Credentials = new NetworkCredential ("ftp", "");
			FtpWebResponse response = (FtpWebResponse)request.GetResponse ();
			//Console.WriteLine ("Содержимое сервера:");

			Stream responseStream = response.GetResponseStream ();
			StreamReader reader = new StreamReader (responseStream);
			TBResult.AppendText (reader.ReadToEnd ());

			reader.Close ();
			responseStream.Close ();
			response.Close ();
			//Console.Read ();
		}
		//_________________________________________________________________________
		private void TestNameColToNum ()
		{
			AXONIM.BelTransGasDRV.Values.CNameColToNum NameColToNum = new AXONIM.BelTransGasDRV.Values.CNameColToNum ();
			string asHeadTab = "Дата	Время	Порядковый №	baaa	st	baca	st	lzStd_vu	st	drkaHmiw	st	tempHmiw	st	rhonHmiw	st	co2Hmiw	st	n2Hmiw	st	baae	st	bacb	st	baag	st	badi	st	bbb	st	bba	st	bbe	st	bdde	st	bdfe	st	bacb	st	bdce	st	bdbe	st";
			string[] asaHeadNeed = { "baag", "baae", "baaa", "baac", "bddd", "bdfd" };
			string[] asaHeadName = { "D2_V", "D2_Vn", "D2_p", "D2_t", "CO2", "N2" };
			Dictionary<int, string> Numbs = NameColToNum.Get (asHeadTab, asaHeadNeed, asaHeadName);
			string asOut = "";
			foreach (var item in Numbs)
			{
				asOut += item.Key + " " + item.Value + Environment.NewLine;
			}
			MessageBox.Show (asOut);
		}
		//_________________________________________________________________________
		private void TestEK270 ()
		{
			iPosRX = 0;
			string asData = "(0)(1x8)(CRC Ok) ";
			LenRecieve = asData.Length - 1;
			GetValsInRow (Encoding.ASCII.GetBytes (asData));
			string asPart = Global.BytesToInt_Char (btaRowPart, iLenRowPart);
			iPosRX = 0;
			asData = "(32891)(31  ";
			LenRecieve = asData.Length - 2;
			GetValsInRow (Encoding.ASCII.GetBytes (asData));
			asPart = Global.BytesToInt_Char (btaRowPart, iLenRowPart);
			iPosRX = 0;
			asData = "04)(CRC Ok)  ";
			LenRecieve = asData.Length - 2;
			GetValsInRow (Encoding.ASCII.GetBytes (asData));
			asPart = Global.BytesToInt_Char (btaRowPart, iLenRowPart);
			iPosRX = 0;
			asData = "(3)(4x5)(CRC Ok) ";
			LenRecieve = asData.Length - 1;
			GetValsInRow (Encoding.ASCII.GetBytes (asData));
			asPart = Global.BytesToInt_Char (btaRowPart, iLenRowPart);
		}
		//_________________________________________________________________________
		void Encode_Decode ()
		{
			String s = "Прювет.";   // Кодируемая строка
															// Получаем объект, производный от Encoding, который "умеет" выполнять кодирование и декодирование с использованием UTF-8
			Encoding encodingUTF8 = Encoding.UTF8;
			Byte[] encodedBytes = encodingUTF8.GetBytes (s);              // Выполняем кодирование строки в массив байтов	
			TBOut.Text = "Encoded bytes: " + BitConverter.ToString (encodedBytes);  // Показываем значение закодированных байтов 
			String decodedString = encodingUTF8.GetString (encodedBytes); // Выполняем декодирование массива байтов обратно в строку 
			TBOut.Text = "Decoded string: " + decodedString;
		}
		//_________________________________________________________________________

		byte[] btBCD = { 0xA, 0x12, 0x34, 0x5, 0xf };
		private void button1_Click (object sender, EventArgs e)
		{
			try
			{
				using (ZipFile zip = new ZipFile ())
				{
					string asFileName = "01.09.2017 10_54_30ArchDay.arch";
					ZipEntry files = zip.AddFile (asFileName);
					files.Comment = "Дополнение Cheeso's CreateZip utility.";
					zip.Comment = $"Этот архив создан by the CreateZip on machine [{System.Net.Dns.GetHostName ()}]";

					zip.Save (asFileName + ".zip");
					MessageBox.Show ("Cоздан " + System.Net.Dns.GetHostName () + " " + asFileName + ".zip");
				}
			}
			catch (Exception exc)
			{
				MessageBox.Show (exc.Message);
			}
		}
		//xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

		private void BCalcCRC_Click (object sender, EventArgs e)
		{
			byte[] btaData = Global.StrToBytes (TBCalc8005.Text);
			//ushort usCRC = Global.CRC8_8 (btaRespon, btaRespon.Length - 2);	//(btaRespon, btaRespon.Length - 2);
			ushort usCRC = Global.CRC (btaData, btaData.Length, btaData.Length, Global.Table8005, 0xFFFF, 0);
			string asCRC = usCRC.ToString ("X04");
			if (RB_LH.Checked)
				TBCalc8005.Text += " " + asCRC.Substring (2, 2) + " " + asCRC.Substring (0, 2);
			else
				TBCalc8005.Text += " " + asCRC.Substring (0, 2) + " " + asCRC.Substring (2, 2);
			TBCalc8005.SelectionStart = 0;
			TBCalc8005.SelectionLength = TBCalc8005.Text.Length;
			TBCalc8005.Copy ();
		}
		//_________________________________________________________________________
		private void TBCalc8005_Enter (object sender, EventArgs e)
		{
		}
		//_________________________________________________________________________
		enum EDirect { прямое, обратное }
		private void BHexIntToDate_Click (object sender, EventArgs e)
		{
			try
			{
				int iVal;
				if (CBDirBites.Text.Equals ("прямое"))
					iVal = Global.ByteToType (StrHexToBytes (TBHexAsInt.Text), TYPE.@int);
				else
					iVal = Global.ByteToTypeReverse (StrHexToBytes (TBHexAsInt.Text), TYPE.@int, 0);
				TBDate.Text = Global.IntToDateTime (iVal).ToString ();
				//TBValFromBytes.Text = Global.IntAsStrToDateTime(TBHexAsInt.Text).ToString();
			}
			catch (Exception exc)
			{
				MessageBox.Show (exc.Message);
			}
		}
		//_________________________________________________________________________
		private void TBCalc8005_KeyPress (object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')    // the ENTER
				BCalcCRC_Click (sender, e);
		}
		//_________________________________________________________________________
		private void CBDirBites_SelectedIndexChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.asDirBites = CBDirBites.Text;
			Properties.Settings.Default.Save ();
		}
		//_________________________________________________________________________
		private void CBDayHour_SelectedIndexChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.DeltaDayHour = CBDayHour.Text;
			Properties.Settings.Default.Save ();
		}
		//_________________________________________________________________________
		enum EDeltaDT
		{
			дни,
			часы,
			минуты,
			секунды,
			милисекунды
		}
		private void BCalcDeltaDT_Click (object sender, EventArgs e)
		{
			TimeSpan TSDelta = DTPFirst.Value - DTPSecond.Value;
			switch ((EDeltaDT)CBDayHour.SelectedIndex)
			{
			case EDeltaDT.дни:
				TBDeltaDT.Text = TSDelta.TotalDays.ToString ();
				break;
			case EDeltaDT.часы:
				TBDeltaDT.Text = TSDelta.TotalHours.ToString ();
				break;
			case EDeltaDT.минуты:
				TBDeltaDT.Text = TSDelta.TotalMinutes.ToString ();
				break;
			case EDeltaDT.секунды:
				TBDeltaDT.Text = TSDelta.TotalSeconds.ToString ();
				break;
			case EDeltaDT.милисекунды:
				TBDeltaDT.Text = TSDelta.TotalMilliseconds.ToString ();
				break;
			default:
				break;
			}
		}
		//VVVVVVVVVVVVV    Для передачи 8-битных данных как 7-битных    VVVVVVVVVVV
		private void B8n1To7e1_Click (object sender, EventArgs e)
		{
			//int iLastLine = TB8n1To7e1.Lines.Count () - 1;
			string asStr = GV8n1To7e1.CurrentRow.Cells[0].Value.ToString ();
			byte[] btaStr;
			if (ChDataAsHex.Checked)            // Данные в виде hex цифры 
			{
				btaStr = StrHexToBytes (asStr);   // Строка остаётся неизменной 
			}
			else                                // Данные в виде строки 
			{
				byte[] bta = Global.EncodingCurr.GetBytes (asStr.Substring (1, asStr.Length - 2));
				btaStr = new byte[2 + bta.Length];
				btaStr[0] = Convert.ToByte (asStr[0]);  // Вставляем 1-е число команды 
																								//btaStr[btaStr.Length - 1] = Convert.ToByte (asStr.Substring(asStr.Length - 2)); // Вставляем последнее число команды RESTORE?
				Global.Append (bta, 0, btaStr, 1, bta.Length);
			}
			int iQuantData = btaStr.Length;
			bool bAppendEndLine = btaStr[iQuantData - 1] == (byte)'\\';// Какое число для \  ? 

			if (bAppendEndLine)
			{   // Вставляем символы перевода каретки и конца строки, если в конце было два \\ 
				if (CB7bit.Checked)
				{
					btaStr[iQuantData - 2] = 0x8D;
				}
				else
				{
					btaStr[iQuantData - 2] = 0xD;
				}
				btaStr[iQuantData - 1] = 0xA;
			}
			string asBCC = "";
			if (ChCalcBCC.Checked)
			{
				byte btBCC = CExchange.CalcBCC (btaStr, 0, btaStr.Length);
				//byte btBCC = CExchange.CalcBCC (btaStr, 4, btaStr.Length - 4);
				if (CB7bit.Checked)
					btBCC = Global._7e1_to_8n1[btBCC];
				asBCC = Convert.ToString (btBCC, 16);
			}
			if (CB7bit.Checked)
			{
				Global.Conv8n1To7e1 (btaStr, (ushort)(iQuantData));
			};

			asStr = Global.ByteArToStr (btaStr, 0, btaStr.Length) + asBCC;
			Clipboard.SetText (asStr);
			GV8n1To7e1.CurrentRow.Cells[1].Value = asStr;

			// Запоминаем на века данные
			string asRes = "";
			foreach (DataGridViewRow item in GV8n1To7e1.Rows)
			{
				asRes += item.Cells[0].Value + ";" + item.Cells[1].Value + ";";
			}
			Properties.Settings.Default.as8n1To7e1 = asRes.Substring (0, asRes.Length - 1);
		}
		//-------------------------------------------------------------------------
		private void CB7bit_CheckedChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.b7bit = CB7bit.Checked;
		}
		//-------------------------------------------------------------------------
		private void ChDataAsHex_CheckedChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.bChDataAsHex = ChDataAsHex.Checked;
		}
		//_________________________________________________________________________
		private void ChCalcBCC_CheckedChanged (object sender, EventArgs e)
		{
			Properties.Settings.Default.bCalcBCC = ChCalcBCC.Checked;
		}
		//___________________     Байты - строка     ______________________________
		private void BBitesToStr_Click (object sender, EventArgs e)
		{
			string asIN = GVBtToStr.CurrentRow?.Cells[0]?.Value.ToString ();
			if (asIN == null)
			{
				MessageBox.Show ("Байты - символьная строка", "   Пустая строка   ");
				return;
			}
			byte[] btaStr = StrHexToBytes (asIN);
			GVBtToStr.CurrentRow.Cells[1].Value = Global.BytesToInt_Char (btaStr, btaStr.Length); // Global.ByteToStr (btaStr, 0, btaStr.Length); //(new UTF32Encoding ()).GetString (btaStr);

			string asRes = "";
			foreach (DataGridViewRow item in GVBtToStr.Rows)
			{
				asRes += item.Cells[0].Value + ";" + item.Cells[1].Value + ";";
			}
			Properties.Settings.Default.asBtToStr = asRes.Substring (0, asRes.Length - 1);
		}
		//____________________      8n1 to 7e1       _____________________________
		private void BBites8nTo7e_Click (object sender, EventArgs e)
		{
			byte[] btaStr = StrHexToBytes (GVBites8nTo7e.CurrentRow.Cells[0].Value.ToString ());
			Global.Conv8n1To7e1 (btaStr, (ushort)btaStr.Length);
			GVBites8nTo7e.CurrentRow.Cells[1].Value = Global.ByteArToStr (btaStr, 0, btaStr.Length);

			string asRes = "";
			foreach (DataGridViewRow item in GVBites8nTo7e.Rows)
			{
				asRes += item.Cells[0].Value + ";" + item.Cells[1].Value + ";";
			}
			Properties.Settings.Default.asDGBite8nTo7e = asRes.Substring (0, asRes.Length - 1);
		}
		//_________________________________________________________________________
		public void Close ()
		{
		}

		//ccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccccc
		//_________________________________________________________________________
		//delegate void DWrParams ();
		//DWrParams WrParams;
		//byte btReg = 1;
		////.........................................................................
		//void WriteParams()
		//{
		//	int iParName = 0;
		//	int iID = (int)NUDIDstart.Value;
		//	foreach (var addrs in TBAddresses.Lines)
		//	{
		//		string asTag = 
		//		 string.Format ("\"DeviceName\":\"{0},\"ParameterName\":\"{1}\",\"INorOUT\":\"out\",\"RequestType\":\"Single\",\"RequestName\":\"MT16\",\"ParameterType\":\"int\",\"Data\":\"0 {2}\",\"TimeRequest\":\"1000\"", 
		//			CBDevsWr.Text, iParName, addrs);

		//		byte[] btaWrPar = new byte[4];
		//		//string[] asaVals = TBWrPars.Lines[iParName].Split(' ');
		//		//int i = 0;
		//		//foreach (var item in asaVals)
		//		//{
		//		//	btaWrPar[i++] = byte.Parse (item);
		//		//}
		//		for (int i = 0; i < 4; i++)
		//		{
		//			btaWrPar[i] = btReg++;
		//		}
		//		DRV.WriteValue (iID++, asTag, btaWrPar);
		//		iParName++;
		//    }
		//	RBWriting.Checked = !RBWriting.Checked;
		//  }
		////.........................................................................
		//System.Timers.Timer TrWrPars;
	}
}
