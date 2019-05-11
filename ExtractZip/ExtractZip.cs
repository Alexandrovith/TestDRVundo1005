using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ionic.Zip;

namespace TestDRVtransGas.ExtractZip
{
	public partial class CExtractZip : Form
	{
		FTestDrvs ZipByOwn;

		public CExtractZip (FTestDrvs Zip)
		{
			InitializeComponent ();
			ZipByOwn = Zip;

			string[] asaData = Properties.Settings.Default.asZipDirs.Split (';');
			if (asaData.Length > 0)
			{
				CBZipDir.Items.AddRange (asaData);
				CBZipDir.SelectedIndex = CBZipDir.FindString (Properties.Settings.Default.asZipDir);
				if (CBZipDir.SelectedIndex < 0 && CBZipDir.Items.Count > 0)
					CBZipDir.SelectedIndex = 0;
			}

			CBZipExtr.Items.AddRange (Properties.Settings.Default.asZipDirsExtr.Split (';'));
			CBZipExtr.SelectedIndex = CBZipExtr.FindString (Properties.Settings.Default.asZipDirExtr);
			if (CBZipExtr.SelectedIndex < 0 && CBZipExtr.Items.Count > 0)
				CBZipExtr.SelectedIndex = 0;
		}
		//_________________________________________________________________________
		private void BClose_Click (object sender, EventArgs e)
		{
			Close ();
		}
		//_________________________________________________________________________
		private void BZipDir_Click (object sender, EventArgs e)
		{
			folderBrowserDialog1.SelectedPath = CBZipDir.Text;
			if (folderBrowserDialog1.ShowDialog () == DialogResult.OK)
			{
				if (CBZipDir.FindString (folderBrowserDialog1.SelectedPath) < 0)
				{
					CBZipDir.Items.Add (folderBrowserDialog1.SelectedPath);
					if (Properties.Settings.Default.asZipDirs.Length > 3)
						Properties.Settings.Default.asZipDirs = Properties.Settings.Default.asZipDirs + ";" + folderBrowserDialog1.SelectedPath;
					else
						Properties.Settings.Default.asZipDirs = folderBrowserDialog1.SelectedPath;
					Properties.Settings.Default.asZipDir = folderBrowserDialog1.SelectedPath;
					CBZipDir.SelectedIndex = CBZipDir.FindString (folderBrowserDialog1.SelectedPath);
				}
			}
		}
		//_________________________________________________________________________
		private void CExtractZip_FormClosed (object sender, FormClosedEventArgs e)
		{
			Properties.Settings.Default.Save ();
			ZipByOwn.Zip = null;
		}
		//_________________________________________________________________________
		private void BZipExtr_Click (object sender, EventArgs e)
		{
			folderBrowserDialog1.SelectedPath = CBZipExtr.Text;
			if (folderBrowserDialog1.ShowDialog () == DialogResult.OK)
			{
				if (CBZipExtr.FindString (folderBrowserDialog1.SelectedPath) < 0)
				{
					CBZipExtr.Items.Add (folderBrowserDialog1.SelectedPath);
					if (Properties.Settings.Default.asZipDirsExtr.Length > 3)
						Properties.Settings.Default.asZipDirsExtr = Properties.Settings.Default.asZipDirsExtr + ";" + folderBrowserDialog1.SelectedPath;
					else
						Properties.Settings.Default.asZipDirsExtr = folderBrowserDialog1.SelectedPath;
					Properties.Settings.Default.asZipDirExtr = folderBrowserDialog1.SelectedPath;
					CBZipExtr.SelectedIndex = CBZipExtr.FindString (folderBrowserDialog1.SelectedPath);
				}
			}
		}
		//_________________________________________________________________________
		private void button1_Click (object sender, EventArgs e)
		{
			LMess.Show ();
			LMess.Text = "Разархивирование ...";
			if (Directory.Exists (CBZipExtr.Text) == false)
				Directory.CreateDirectory (CBZipExtr.Text);

			foreach (var item in GetFilesNames("*.zip", CBZipDir.Text))
			{
				try
				{
					using (ZipFile zip = ZipFile.Read (item))
					{
						zip.FlattenFoldersOnExtract = true; // Без путей 
						zip.ExtractAll (CBZipExtr.Text);
					}
					LMess.Text = "Разархивирование завершено";
				}
				catch (Exception exc)
				{
					MessageBox.Show (exc.Message, "Разархивирование файлов");
				}
			}
		}
		//_________________________________________________________________________
		private List<string> GetFilesNames (string asExtFiles, string asDirLog)
		{
			List<string> ListOfFiles = new List<string> (1024);
			DirectoryInfo DirInfo = new DirectoryInfo (asDirLog);

			foreach (FileInfo f in DirInfo.GetFiles (asExtFiles))
			{
				ListOfFiles.Add (f.FullName);
			}

			return ListOfFiles;
		}
		//_________________________________________________________________________
	}
}
