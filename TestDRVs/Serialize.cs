///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

///~~~~~~~~~	Проект:			Тестирование драйверов приборов
///~~~~~~~~~	Прибор:			Все описанные приборы
///~~~~~~~~~	Модуль:			Сохранение тегов и настроек приборов
///~~~~~~~~~	Разработка:	Демешкевич С.А.
///~~~~~~~~~	Дата:				2014

///@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace TestDRVtransGas
{
	public static class TSerialize
	{
		//___________________________________________________________________________
		public static bool ReadFl (string asNameFl, DataGridView GV)
		{
			if (File.Exists (asNameFl))
			{
				GV.Rows.Clear ();
				using (StreamReader SR = File.OpenText (asNameFl))
				{
					string sLine = null;
					while (true)
					{
						sLine = SR.ReadLine ();
						if (sLine != null)
						{
							string[] sRow = sLine.Split (';');
							GV.Rows.Add (sRow);
						}
						else break;
					}
				}
				//s = File.ReadAllLines (sFile);
				//int iNumRows = s.Length;
				//for (int iRow = 0; iRow < iNumRows; iRow++)
				//{
				//	string[] sRow = s[iRow].Split (';');
				//	GV.Rows.Add(sRow);
				//}
				return true;
			}
			return false;
		}
		//___________________________________________________________________________
		public static bool WriteFl (string asNameFl, DataGridView GV)
		{
			int iNumDev = GV.Rows.Count - 1;
			StringBuilder sb = new StringBuilder ("");

			try
			{
				for (int iRow = 0; iRow < iNumDev; iRow++)
				{
					for (int col = 0; col < GV.ColumnCount; col++)
					{
						sb.Append (GV.Rows[iRow].Cells[col].Value + ";");
					}
					sb.Append (Environment.NewLine);
				}
				File.WriteAllText (asNameFl, sb.ToString ());
			}
			catch (Exception ex)
			{
				MessageBox.Show ("Ошибка при сохранении файла [" + asNameFl + "]." + ex.Message);
				return false;
			}

			//int iNumDev = GV.Rows.Count - 1;
			//StringBuilder sb = new StringBuilder ("");

			//for (int iRow = 0; iRow < iNumDev; iRow++)
			//{
			//	for (int col = 0; col < GV.ColumnCount; col++)
			//	{
			//		sb.Append (GV.Rows[iRow].Cells[col].Value + ";");
			//	}
			//	sb.Append (Environment.NewLine);
			//}
			//File.WriteAllText (sFile, sb.ToString ());
			return true;
		}
	}
}
