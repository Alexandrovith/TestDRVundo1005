namespace TestDRVtransGas.TCPserver
{
	partial class FTCPserver
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTCPserver));
			this.label5 = new System.Windows.Forms.Label();
			this.ChBChange = new System.Windows.Forms.CheckBox();
			this.label4 = new System.Windows.Forms.Label();
			this.CBDev = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.CBIP = new System.Windows.Forms.ComboBox();
			this.RTBOut = new System.Windows.Forms.RichTextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.UDPort = new System.Windows.Forms.NumericUpDown();
			this.PSetups = new System.Windows.Forms.Panel();
			this.PBOpacity = new System.Windows.Forms.ProgressBar();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.BHideOwner = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.BTCP_Com = new System.Windows.Forms.Button();
			this.BClose = new System.Windows.Forms.Button();
			this.BClearOut = new System.Windows.Forms.Button();
			this.BStartStop = new System.Windows.Forms.Button();
			this.LReconnects = new System.Windows.Forms.Label();
			this.LQuantConn = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.UDSizeBufOut = new System.Windows.Forms.NumericUpDown();
			this.UDWriteTimeout = new System.Windows.Forms.NumericUpDown();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.UDPort)).BeginInit();
			this.PSetups.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.UDSizeBufOut)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.UDWriteTimeout)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(42, 20);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(52, 13);
			this.label5.TabIndex = 44;
			this.label5.Text = "Запросы";
			// 
			// ChBChange
			// 
			this.ChBChange.AutoSize = true;
			this.ChBChange.Checked = true;
			this.ChBChange.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ChBChange.Cursor = System.Windows.Forms.Cursors.Hand;
			this.ChBChange.Location = new System.Drawing.Point(232, 52);
			this.ChBChange.Name = "ChBChange";
			this.ChBChange.Size = new System.Drawing.Size(114, 30);
			this.ChBChange.TabIndex = 43;
			this.ChBChange.Text = "Менять значения\r\nвыходные";
			this.ChBChange.UseVisualStyleBackColor = true;
			this.ChBChange.CheckedChanged += new System.EventHandler(this.ChBChange_CheckedChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(7, 4);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(45, 13);
			this.label4.TabIndex = 42;
			this.label4.Text = "Прибор";
			// 
			// CBDev
			// 
			this.CBDev.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBDev.FormattingEnabled = true;
			this.CBDev.Location = new System.Drawing.Point(53, 5);
			this.CBDev.Name = "CBDev";
			this.CBDev.Size = new System.Drawing.Size(144, 21);
			this.CBDev.TabIndex = 41;
			this.CBDev.SelectedIndexChanged += new System.EventHandler(this.CBDev_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 4);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(82, 13);
			this.label3.TabIndex = 40;
			this.label3.Text = "Переключения";
			// 
			// CBIP
			// 
			this.CBIP.FormattingEnabled = true;
			this.CBIP.Items.AddRange(new object[] {
            "127.0.0.1",
            "DEMESHKEVICH",
            "192.168.226.1",
            "192.168.111.1"});
			this.CBIP.Location = new System.Drawing.Point(53, 48);
			this.CBIP.Name = "CBIP";
			this.CBIP.Size = new System.Drawing.Size(108, 21);
			this.CBIP.TabIndex = 35;
			// 
			// RTBOut
			// 
			this.RTBOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.RTBOut.DetectUrls = false;
			this.RTBOut.HideSelection = false;
			this.RTBOut.Location = new System.Drawing.Point(8, 84);
			this.RTBOut.Name = "RTBOut";
			this.RTBOut.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
			this.RTBOut.ShowSelectionMargin = true;
			this.RTBOut.Size = new System.Drawing.Size(544, 136);
			this.RTBOut.TabIndex = 34;
			this.RTBOut.Text = "";
			this.RTBOut.WordWrap = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 51);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(17, 13);
			this.label2.TabIndex = 33;
			this.label2.Text = "IP";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 31);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 13);
			this.label1.TabIndex = 32;
			this.label1.Text = "Порт";
			// 
			// UDPort
			// 
			this.UDPort.Location = new System.Drawing.Point(53, 27);
			this.UDPort.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.UDPort.Name = "UDPort";
			this.UDPort.Size = new System.Drawing.Size(68, 20);
			this.UDPort.TabIndex = 31;
			this.UDPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.UDPort.Value = new decimal(new int[] {
            502,
            0,
            0,
            0});
			// 
			// PSetups
			// 
			this.PSetups.BackColor = System.Drawing.Color.WhiteSmoke;
			this.PSetups.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PSetups.Controls.Add(this.CBIP);
			this.PSetups.Controls.Add(this.UDPort);
			this.PSetups.Controls.Add(this.label1);
			this.PSetups.Controls.Add(this.label2);
			this.PSetups.Controls.Add(this.label4);
			this.PSetups.Controls.Add(this.CBDev);
			this.PSetups.Location = new System.Drawing.Point(8, 4);
			this.PSetups.Name = "PSetups";
			this.PSetups.Size = new System.Drawing.Size(212, 75);
			this.PSetups.TabIndex = 46;
			// 
			// PBOpacity
			// 
			this.PBOpacity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PBOpacity.Cursor = System.Windows.Forms.Cursors.SizeWE;
			this.PBOpacity.Location = new System.Drawing.Point(6, 15);
			this.PBOpacity.Name = "PBOpacity";
			this.PBOpacity.Size = new System.Drawing.Size(200, 15);
			this.PBOpacity.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.PBOpacity.TabIndex = 47;
			this.PBOpacity.Value = 100;
			this.PBOpacity.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PBOpacity_MouseMove);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
			this.groupBox1.Controls.Add(this.PBOpacity);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.groupBox1.Location = new System.Drawing.Point(345, 223);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(214, 35);
			this.groupBox1.TabIndex = 48;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Прозрачность";
			// 
			// BHideOwner
			// 
			this.BHideOwner.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.BHideOwner.BackColor = System.Drawing.SystemColors.ControlLight;
			this.BHideOwner.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BHideOwner.Image = global::TestDRVtransGas.Properties.Resources.Icon_06_p6;
			this.BHideOwner.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BHideOwner.Location = new System.Drawing.Point(116, 222);
			this.BHideOwner.Name = "BHideOwner";
			this.BHideOwner.Size = new System.Drawing.Size(96, 35);
			this.BHideOwner.TabIndex = 49;
			this.BHideOwner.Text = "Скрыть\r\nвладельца";
			this.BHideOwner.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTip1.SetToolTip(this.BHideOwner, "Скрыть / показать владельца");
			this.BHideOwner.UseVisualStyleBackColor = false;
			this.BHideOwner.Click += new System.EventHandler(this.BHideOwner_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(36, 40);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(58, 13);
			this.label7.TabIndex = 40;
			this.label7.Text = "Задержка";
			this.toolTip1.SetToolTip(this.label7, "Интервал между отправками частей ответа");
			// 
			// BTCP_Com
			// 
			this.BTCP_Com.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.BTCP_Com.BackColor = System.Drawing.SystemColors.ControlLight;
			this.BTCP_Com.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BTCP_Com.Image = ((System.Drawing.Image)(resources.GetObject("BTCP_Com.Image")));
			this.BTCP_Com.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BTCP_Com.Location = new System.Drawing.Point(224, 222);
			this.BTCP_Com.Name = "BTCP_Com";
			this.BTCP_Com.Size = new System.Drawing.Size(96, 35);
			this.BTCP_Com.TabIndex = 98;
			this.BTCP_Com.Text = "TCP - Com";
			this.BTCP_Com.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTip1.SetToolTip(this.BTCP_Com, "Передача данных от TCP в ComPort");
			this.BTCP_Com.UseVisualStyleBackColor = false;
			this.BTCP_Com.Click += new System.EventHandler(this.BTCP_Com_Click);
			// 
			// BClose
			// 
			this.BClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.BClose.BackColor = System.Drawing.SystemColors.ControlLight;
			this.BClose.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BClose.Image = global::TestDRVtransGas.Properties.Resources.ExitTranspar;
			this.BClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BClose.Location = new System.Drawing.Point(8, 222);
			this.BClose.Name = "BClose";
			this.BClose.Size = new System.Drawing.Size(96, 35);
			this.BClose.TabIndex = 45;
			this.BClose.Text = "Закрыть";
			this.BClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BClose.UseVisualStyleBackColor = false;
			this.BClose.Click += new System.EventHandler(this.BClose_Click);
			// 
			// BClearOut
			// 
			this.BClearOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BClearOut.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BClearOut.Image = global::TestDRVtransGas.Properties.Resources.Orange_System_Icon_06_p6;
			this.BClearOut.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BClearOut.Location = new System.Drawing.Point(512, 46);
			this.BClearOut.Name = "BClearOut";
			this.BClearOut.Size = new System.Drawing.Size(40, 40);
			this.BClearOut.TabIndex = 37;
			this.BClearOut.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BClearOut.UseVisualStyleBackColor = true;
			this.BClearOut.Click += new System.EventHandler(this.BClearOut_Click);
			// 
			// BStartStop
			// 
			this.BStartStop.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
			this.BStartStop.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BStartStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.BStartStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.BStartStop.ForeColor = System.Drawing.SystemColors.ControlText;
			this.BStartStop.Image = global::TestDRVtransGas.Properties.Resources.call_progress_p4;
			this.BStartStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BStartStop.Location = new System.Drawing.Point(232, 4);
			this.BStartStop.Name = "BStartStop";
			this.BStartStop.Size = new System.Drawing.Size(108, 45);
			this.BStartStop.TabIndex = 30;
			this.BStartStop.Text = "Слушать!";
			this.BStartStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BStartStop.UseVisualStyleBackColor = false;
			this.BStartStop.Click += new System.EventHandler(this.BStartStop_Click);
			// 
			// LReconnects
			// 
			this.LReconnects.ForeColor = System.Drawing.Color.DarkGreen;
			this.LReconnects.Location = new System.Drawing.Point(96, 5);
			this.LReconnects.Name = "LReconnects";
			this.LReconnects.Size = new System.Drawing.Size(52, 13);
			this.LReconnects.TabIndex = 50;
			this.LReconnects.Text = "0";
			// 
			// LQuantConn
			// 
			this.LQuantConn.ForeColor = System.Drawing.Color.DarkGreen;
			this.LQuantConn.Location = new System.Drawing.Point(96, 21);
			this.LQuantConn.Name = "LQuantConn";
			this.LQuantConn.Size = new System.Drawing.Size(52, 13);
			this.LQuantConn.TabIndex = 51;
			this.LQuantConn.Text = "0";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(8, 58);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(86, 13);
			this.label6.TabIndex = 52;
			this.label6.Text = "Размер буфера";
			// 
			// UDSizeBufOut
			// 
			this.UDSizeBufOut.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.UDSizeBufOut.Location = new System.Drawing.Point(96, 56);
			this.UDSizeBufOut.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.UDSizeBufOut.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.UDSizeBufOut.Name = "UDSizeBufOut";
			this.UDSizeBufOut.Size = new System.Drawing.Size(60, 20);
			this.UDSizeBufOut.TabIndex = 53;
			this.UDSizeBufOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.UDSizeBufOut.Value = new decimal(new int[] {
            400,
            0,
            0,
            0});
			this.UDSizeBufOut.ValueChanged += new System.EventHandler(this.UDSizeBufOut_ValueChanged);
			// 
			// UDWriteTimeout
			// 
			this.UDWriteTimeout.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.UDWriteTimeout.Location = new System.Drawing.Point(96, 36);
			this.UDWriteTimeout.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
			this.UDWriteTimeout.Name = "UDWriteTimeout";
			this.UDWriteTimeout.Size = new System.Drawing.Size(60, 20);
			this.UDWriteTimeout.TabIndex = 54;
			this.UDWriteTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.UDWriteTimeout.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
			this.UDWriteTimeout.ValueChanged += new System.EventHandler(this.UDWriteTimeout_ValueChanged);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.UDWriteTimeout);
			this.panel1.Controls.Add(this.UDSizeBufOut);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.LQuantConn);
			this.panel1.Controls.Add(this.LReconnects);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Location = new System.Drawing.Point(348, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(160, 80);
			this.panel1.TabIndex = 99;
			// 
			// FTCPserver
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(559, 258);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.BTCP_Com);
			this.Controls.Add(this.BHideOwner);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.PSetups);
			this.Controls.Add(this.BClose);
			this.Controls.Add(this.ChBChange);
			this.Controls.Add(this.BClearOut);
			this.Controls.Add(this.RTBOut);
			this.Controls.Add(this.BStartStop);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FTCPserver";
			this.Text = "TCP/IP server";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FTCPserver_FormClosing);
			this.Load += new System.EventHandler(this.FTCPserver_Load);
			((System.ComponentModel.ISupportInitialize)(this.UDPort)).EndInit();
			this.PSetups.ResumeLayout(false);
			this.PSetups.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.UDSizeBufOut)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.UDWriteTimeout)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label5;
		public System.Windows.Forms.CheckBox ChBChange;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button BClearOut;
		private System.Windows.Forms.ComboBox CBIP;
		private System.Windows.Forms.RichTextBox RTBOut;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown UDPort;
		private System.Windows.Forms.Button BStartStop;
		private System.Windows.Forms.Button BClose;
		private System.Windows.Forms.Panel PSetups;
    private System.Windows.Forms.ProgressBar PBOpacity;
    private System.Windows.Forms.GroupBox groupBox1;
    private System.Windows.Forms.Button BHideOwner;
    private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label LReconnects;
		private System.Windows.Forms.Label LQuantConn;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown UDSizeBufOut;
		private System.Windows.Forms.Label label7;
		public System.Windows.Forms.NumericUpDown UDWriteTimeout;
		private System.Windows.Forms.Button BTCP_Com;
		public System.Windows.Forms.ComboBox CBDev;
		private System.Windows.Forms.Panel panel1;
	}
}