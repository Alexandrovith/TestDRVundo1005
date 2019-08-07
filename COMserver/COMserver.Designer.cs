namespace TestDRVtransGas.COMserver
{
	partial class FCOMserver
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FCOMserver));
			this.TBOut = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.CBPort = new System.Windows.Forms.ComboBox();
			this.CBBaud = new System.Windows.Forms.ComboBox();
			this.BConnect = new System.Windows.Forms.Button();
			this.BHideOwner = new System.Windows.Forms.Button();
			this.CBDevice = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.BToClipbrd = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label6 = new System.Windows.Forms.Label();
			this.NUDFont = new System.Windows.Forms.NumericUpDown();
			this.button2 = new System.Windows.Forms.Button();
			this.ChWordWrap = new System.Windows.Forms.CheckBox();
			this.BClose = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NUDFont)).BeginInit();
			this.SuspendLayout();
			// 
			// TBOut
			// 
			this.TBOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TBOut.BackColor = System.Drawing.Color.Ivory;
			this.TBOut.ForeColor = System.Drawing.Color.Indigo;
			this.TBOut.Location = new System.Drawing.Point(8, 8);
			this.TBOut.Multiline = true;
			this.TBOut.Name = "TBOut";
			this.TBOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.TBOut.Size = new System.Drawing.Size(452, 394);
			this.TBOut.TabIndex = 9;
			this.TBOut.WordWrap = false;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(472, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Порт";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(472, 40);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(55, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Скорость";
			// 
			// CBPort
			// 
			this.CBPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CBPort.BackColor = System.Drawing.SystemColors.Control;
			this.CBPort.FormattingEnabled = true;
			this.CBPort.Location = new System.Drawing.Point(520, 12);
			this.CBPort.Name = "CBPort";
			this.CBPort.Size = new System.Drawing.Size(96, 21);
			this.CBPort.TabIndex = 2;
			this.CBPort.SelectedIndexChanged += new System.EventHandler(this.CBPort_SelectedIndexChanged);
			// 
			// CBBaud
			// 
			this.CBBaud.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CBBaud.FormattingEnabled = true;
			this.CBBaud.Items.AddRange(new object[] {
            "1200",
            "7200",
            "9600",
            "14400",
            "19200",
            "38400",
            "57600",
            "115200"});
			this.CBBaud.Location = new System.Drawing.Point(536, 36);
			this.CBBaud.Name = "CBBaud";
			this.CBBaud.Size = new System.Drawing.Size(80, 21);
			this.CBBaud.TabIndex = 3;
			this.CBBaud.SelectedIndexChanged += new System.EventHandler(this.CBBaud_SelectedIndexChanged);
			// 
			// BConnect
			// 
			this.BConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BConnect.Image = ((System.Drawing.Image)(resources.GetObject("BConnect.Image")));
			this.BConnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BConnect.Location = new System.Drawing.Point(476, 64);
			this.BConnect.Name = "BConnect";
			this.BConnect.Size = new System.Drawing.Size(140, 36);
			this.BConnect.TabIndex = 0;
			this.BConnect.Text = "Подключить";
			this.BConnect.UseVisualStyleBackColor = true;
			this.BConnect.Click += new System.EventHandler(this.BConnect_Click);
			// 
			// BHideOwner
			// 
			this.BHideOwner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BHideOwner.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BHideOwner.Image = global::TestDRVtransGas.Properties.Resources.Icon_06_p6;
			this.BHideOwner.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BHideOwner.Location = new System.Drawing.Point(476, 320);
			this.BHideOwner.Name = "BHideOwner";
			this.BHideOwner.Size = new System.Drawing.Size(140, 36);
			this.BHideOwner.TabIndex = 4;
			this.BHideOwner.Text = "Скрыть";
			this.BHideOwner.UseVisualStyleBackColor = true;
			this.BHideOwner.Click += new System.EventHandler(this.BHideOwner_Click);
			// 
			// CBDevice
			// 
			this.CBDevice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.CBDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBDevice.FormattingEnabled = true;
			this.CBDevice.Location = new System.Drawing.Point(476, 128);
			this.CBDevice.Name = "CBDevice";
			this.CBDevice.Size = new System.Drawing.Size(140, 21);
			this.CBDevice.TabIndex = 1;
			this.CBDevice.SelectedIndexChanged += new System.EventHandler(this.CBDevice_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(476, 112);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(45, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Прибор";
			// 
			// BToClipbrd
			// 
			this.BToClipbrd.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BToClipbrd.Dock = System.Windows.Forms.DockStyle.Top;
			this.BToClipbrd.Image = global::TestDRVtransGas.Properties.Resources.Cut_p7;
			this.BToClipbrd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BToClipbrd.Location = new System.Drawing.Point(0, 0);
			this.BToClipbrd.Name = "BToClipbrd";
			this.BToClipbrd.Size = new System.Drawing.Size(119, 32);
			this.BToClipbrd.TabIndex = 106;
			this.BToClipbrd.Text = "<-  В память";
			this.BToClipbrd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BToClipbrd.UseVisualStyleBackColor = true;
			this.BToClipbrd.Click += new System.EventHandler(this.BToClipbrd_Click);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackColor = System.Drawing.Color.Ivory;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.NUDFont);
			this.panel1.Controls.Add(this.button2);
			this.panel1.Controls.Add(this.BToClipbrd);
			this.panel1.Controls.Add(this.ChWordWrap);
			this.panel1.Location = new System.Drawing.Point(459, 170);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(121, 106);
			this.panel1.TabIndex = 109;
			// 
			// label6
			// 
			this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(56, 66);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(41, 13);
			this.label6.TabIndex = 107;
			this.label6.Text = "Шрифт";
			// 
			// NUDFont
			// 
			this.NUDFont.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.NUDFont.Location = new System.Drawing.Point(56, 83);
			this.NUDFont.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
			this.NUDFont.Name = "NUDFont";
			this.NUDFont.Size = new System.Drawing.Size(60, 20);
			this.NUDFont.TabIndex = 106;
			this.NUDFont.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.NUDFont.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.NUDFont.ValueChanged += new System.EventHandler(this.NUDFont_ValueChanged);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.button2.Image = global::TestDRVtransGas.Properties.Resources.Orange_System_Icon_06_p6;
			this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.button2.Location = new System.Drawing.Point(0, 64);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(43, 40);
			this.button2.TabIndex = 104;
			this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Button2_Click);
			// 
			// ChWordWrap
			// 
			this.ChWordWrap.AutoSize = true;
			this.ChWordWrap.Location = new System.Drawing.Point(8, 40);
			this.ChWordWrap.Name = "ChWordWrap";
			this.ChWordWrap.Size = new System.Drawing.Size(91, 17);
			this.ChWordWrap.TabIndex = 101;
			this.ChWordWrap.Text = "<- Завернуть";
			this.ChWordWrap.UseVisualStyleBackColor = true;
			this.ChWordWrap.CheckedChanged += new System.EventHandler(this.CheckBox1_CheckedChanged);
			// 
			// BClose
			// 
			this.BClose.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BClose.Image = global::TestDRVtransGas.Properties.Resources.ExitTranspar;
			this.BClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BClose.Location = new System.Drawing.Point(476, 360);
			this.BClose.Name = "BClose";
			this.BClose.Size = new System.Drawing.Size(140, 36);
			this.BClose.TabIndex = 110;
			this.BClose.Text = "Закрыть";
			this.BClose.UseVisualStyleBackColor = true;
			this.BClose.Click += new System.EventHandler(this.BClose_Click);
			// 
			// FCOMserver
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(628, 405);
			this.Controls.Add(this.BClose);
			this.Controls.Add(this.TBOut);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.CBDevice);
			this.Controls.Add(this.BHideOwner);
			this.Controls.Add(this.BConnect);
			this.Controls.Add(this.CBBaud);
			this.Controls.Add(this.CBPort);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FCOMserver";
			this.Text = "COM-port server";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FCOMserver_FormClosing);
			this.Load += new System.EventHandler(this.FCOMserver_Load);
			this.Move += new System.EventHandler(this.FCOMserver_Move);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.NUDFont)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button BConnect;
		public System.Windows.Forms.ComboBox CBPort;
		public System.Windows.Forms.ComboBox CBBaud;
		private System.Windows.Forms.Button BHideOwner;
		public System.Windows.Forms.TextBox TBOut;
		private System.Windows.Forms.ComboBox CBDevice;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button BToClipbrd;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown NUDFont;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.CheckBox ChWordWrap;
		private System.Windows.Forms.Button BClose;
	}
}