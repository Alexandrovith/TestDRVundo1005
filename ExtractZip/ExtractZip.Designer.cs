namespace TestDRVtransGas.ExtractZip
{
	partial class CExtractZip
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose (bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose ();
			}
			base.Dispose (disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CExtractZip));
			this.CBZipDir = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.CBZipExtr = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.BClose = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.BZipExtr = new System.Windows.Forms.Button();
			this.BZipDir = new System.Windows.Forms.Button();
			this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
			this.LMess = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// CBZipDir
			// 
			this.CBZipDir.FormattingEnabled = true;
			this.CBZipDir.Location = new System.Drawing.Point(164, 20);
			this.CBZipDir.Name = "CBZipDir";
			this.CBZipDir.Size = new System.Drawing.Size(576, 21);
			this.CBZipDir.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(24, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(133, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Каталолг с Zip-архивами";
			// 
			// CBZipExtr
			// 
			this.CBZipExtr.FormattingEnabled = true;
			this.CBZipExtr.Location = new System.Drawing.Point(164, 46);
			this.CBZipExtr.Name = "CBZipExtr";
			this.CBZipExtr.Size = new System.Drawing.Size(576, 21);
			this.CBZipExtr.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(24, 52);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(117, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Каталолг распаковки";
			// 
			// BClose
			// 
			this.BClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BClose.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BClose.Location = new System.Drawing.Point(613, 176);
			this.BClose.Name = "BClose";
			this.BClose.Size = new System.Drawing.Size(163, 35);
			this.BClose.TabIndex = 3;
			this.BClose.Text = "Выход";
			this.BClose.UseVisualStyleBackColor = true;
			this.BClose.Click += new System.EventHandler(this.BClose_Click);
			// 
			// button1
			// 
			this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button1.Image = global::TestDRVtransGas.Properties.Resources.box_out;
			this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button1.Location = new System.Drawing.Point(520, 92);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(220, 36);
			this.button1.TabIndex = 4;
			this.button1.Text = "Извлечь";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// BZipExtr
			// 
			this.BZipExtr.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BZipExtr.Image = global::TestDRVtransGas.Properties.Resources.books_preferences;
			this.BZipExtr.Location = new System.Drawing.Point(740, 45);
			this.BZipExtr.Name = "BZipExtr";
			this.BZipExtr.Size = new System.Drawing.Size(36, 32);
			this.BZipExtr.TabIndex = 2;
			this.BZipExtr.UseVisualStyleBackColor = true;
			this.BZipExtr.Click += new System.EventHandler(this.BZipExtr_Click);
			// 
			// BZipDir
			// 
			this.BZipDir.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BZipDir.Image = global::TestDRVtransGas.Properties.Resources.DaBlue_2006_042_p5;
			this.BZipDir.Location = new System.Drawing.Point(740, 9);
			this.BZipDir.Name = "BZipDir";
			this.BZipDir.Size = new System.Drawing.Size(36, 32);
			this.BZipDir.TabIndex = 2;
			this.BZipDir.UseVisualStyleBackColor = true;
			this.BZipDir.Click += new System.EventHandler(this.BZipDir_Click);
			// 
			// LMess
			// 
			this.LMess.AutoSize = true;
			this.LMess.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.LMess.ForeColor = System.Drawing.Color.SaddleBrown;
			this.LMess.Location = new System.Drawing.Point(72, 108);
			this.LMess.Name = "LMess";
			this.LMess.Size = new System.Drawing.Size(167, 20);
			this.LMess.TabIndex = 5;
			this.LMess.Text = "Разархивирование ...";
			this.LMess.Visible = false;
			// 
			// CExtractZip
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 226);
			this.Controls.Add(this.LMess);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.BClose);
			this.Controls.Add(this.BZipExtr);
			this.Controls.Add(this.BZipDir);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.CBZipExtr);
			this.Controls.Add(this.CBZipDir);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CExtractZip";
			this.Text = "Разархивирование каталога с zip";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CExtractZip_FormClosed);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox CBZipDir;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button BZipDir;
		private System.Windows.Forms.ComboBox CBZipExtr;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button BZipExtr;
		private System.Windows.Forms.Button BClose;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.Label LMess;
	}
}