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
			this.BClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// TBOut
			// 
			this.TBOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TBOut.BackColor = System.Drawing.Color.Ivory;
			this.TBOut.Location = new System.Drawing.Point(8, 8);
			this.TBOut.Multiline = true;
			this.TBOut.Name = "TBOut";
			this.TBOut.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.TBOut.Size = new System.Drawing.Size(452, 320);
			this.TBOut.TabIndex = 0;
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
			this.CBPort.TabIndex = 4;
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
			this.CBBaud.TabIndex = 5;
			this.CBBaud.SelectedIndexChanged += new System.EventHandler(this.CBBaud_SelectedIndexChanged);
			// 
			// BConnect
			// 
			this.BConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BConnect.Image = ((System.Drawing.Image)(resources.GetObject("BConnect.Image")));
			this.BConnect.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BConnect.Location = new System.Drawing.Point(476, 68);
			this.BConnect.Name = "BConnect";
			this.BConnect.Size = new System.Drawing.Size(140, 36);
			this.BConnect.TabIndex = 6;
			this.BConnect.Text = "Подключить";
			this.BConnect.UseVisualStyleBackColor = true;
			this.BConnect.Click += new System.EventHandler(this.BConnect_Click);
			// 
			// BHideOwner
			// 
			this.BHideOwner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BHideOwner.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BHideOwner.Image = global::TestDRVtransGas.Properties.Resources.Icon_06_p6;
			this.BHideOwner.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BHideOwner.Location = new System.Drawing.Point(476, 240);
			this.BHideOwner.Name = "BHideOwner";
			this.BHideOwner.Size = new System.Drawing.Size(140, 36);
			this.BHideOwner.TabIndex = 7;
			this.BHideOwner.Text = "Скрыть";
			this.BHideOwner.UseVisualStyleBackColor = true;
			this.BHideOwner.Click += new System.EventHandler(this.BHideOwner_Click);
			// 
			// BClose
			// 
			this.BClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BClose.ImageKey = "ExitTranspar.png";
			this.BClose.Location = new System.Drawing.Point(476, 288);
			this.BClose.Name = "BClose";
			this.BClose.Size = new System.Drawing.Size(140, 36);
			this.BClose.TabIndex = 8;
			this.BClose.Text = "Закрыть";
			this.BClose.UseVisualStyleBackColor = true;
			this.BClose.Click += new System.EventHandler(this.BClose_Click);
			// 
			// FCOMserver
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(628, 331);
			this.Controls.Add(this.BClose);
			this.Controls.Add(this.BHideOwner);
			this.Controls.Add(this.BConnect);
			this.Controls.Add(this.CBBaud);
			this.Controls.Add(this.CBPort);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.TBOut);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FCOMserver";
			this.Text = "COM-port server";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FCOMserver_FormClosing);
			this.Load += new System.EventHandler(this.FCOMserver_Load);
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
		private System.Windows.Forms.Button BClose;
		public System.Windows.Forms.TextBox TBOut;
	}
}