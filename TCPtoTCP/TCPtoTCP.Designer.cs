﻿namespace TestDRVtransGas.TCPtoTCP
{
	partial class CTCPtoTCP
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent ()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CTCPtoTCP));
			this.CBIP1 = new System.Windows.Forms.ComboBox();
			this.UDPort1 = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.CBIP2 = new System.Windows.Forms.ComboBox();
			this.UDPort2 = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.TBOut = new System.Windows.Forms.TextBox();
			this.ChWordWrap = new System.Windows.Forms.CheckBox();
			this.BClose = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.BToClipbrd = new System.Windows.Forms.Button();
			this.BClearOut = new System.Windows.Forms.Button();
			this.BConnect2 = new System.Windows.Forms.Button();
			this.BConnect1 = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.NUDFont = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.UDPort1)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.UDPort2)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NUDFont)).BeginInit();
			this.SuspendLayout();
			// 
			// CBIP1
			// 
			this.CBIP1.FormattingEnabled = true;
			this.CBIP1.Items.AddRange(new object[] {
            "127.0.0.1",
            "DEMESHKEVICH",
            "192.168.226.1",
            "192.168.111.1"});
			this.CBIP1.Location = new System.Drawing.Point(60, 28);
			this.CBIP1.Name = "CBIP1";
			this.CBIP1.Size = new System.Drawing.Size(108, 21);
			this.CBIP1.TabIndex = 39;
			this.CBIP1.SelectedIndexChanged += new System.EventHandler(this.CBIP1_SelectedIndexChanged);
			this.CBIP1.TextUpdate += new System.EventHandler(this.CBIP1_TextUpdate);
			// 
			// UDPort1
			// 
			this.UDPort1.Location = new System.Drawing.Point(60, 52);
			this.UDPort1.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.UDPort1.Name = "UDPort1";
			this.UDPort1.Size = new System.Drawing.Size(68, 20);
			this.UDPort1.TabIndex = 36;
			this.UDPort1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.UDPort1.Value = new decimal(new int[] {
            502,
            0,
            0,
            0});
			this.UDPort1.ValueChanged += new System.EventHandler(this.UDPort1_ValueChanged);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 13);
			this.label2.TabIndex = 37;
			this.label2.Text = "Порт";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(14, 31);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(17, 13);
			this.label3.TabIndex = 38;
			this.label3.Text = "IP";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.CBIP1);
			this.groupBox1.Controls.Add(this.BConnect1);
			this.groupBox1.Controls.Add(this.UDPort1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.groupBox1.Location = new System.Drawing.Point(8, 84);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(188, 120);
			this.groupBox1.TabIndex = 40;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Активный";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.CBIP2);
			this.groupBox2.Controls.Add(this.BConnect2);
			this.groupBox2.Controls.Add(this.UDPort2);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Location = new System.Drawing.Point(8, 236);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(188, 116);
			this.groupBox2.TabIndex = 40;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Пассивный";
			// 
			// CBIP2
			// 
			this.CBIP2.FormattingEnabled = true;
			this.CBIP2.Items.AddRange(new object[] {
            "127.0.0.1",
            "DEMESHKEVICH",
            "192.168.226.1",
            "192.168.111.1"});
			this.CBIP2.Location = new System.Drawing.Point(60, 24);
			this.CBIP2.Name = "CBIP2";
			this.CBIP2.Size = new System.Drawing.Size(108, 21);
			this.CBIP2.TabIndex = 39;
			this.CBIP2.SelectedIndexChanged += new System.EventHandler(this.CBIP2_SelectedIndexChanged);
			this.CBIP2.TextUpdate += new System.EventHandler(this.CBIP2_TextUpdate);
			// 
			// UDPort2
			// 
			this.UDPort2.Location = new System.Drawing.Point(60, 48);
			this.UDPort2.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.UDPort2.Name = "UDPort2";
			this.UDPort2.Size = new System.Drawing.Size(68, 20);
			this.UDPort2.TabIndex = 36;
			this.UDPort2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.UDPort2.Value = new decimal(new int[] {
            502,
            0,
            0,
            0});
			this.UDPort2.ValueChanged += new System.EventHandler(this.UDPort2_ValueChanged);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(14, 52);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(32, 13);
			this.label4.TabIndex = 37;
			this.label4.Text = "Порт";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(14, 27);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(17, 13);
			this.label5.TabIndex = 38;
			this.label5.Text = "IP";
			// 
			// TBOut
			// 
			this.TBOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TBOut.Location = new System.Drawing.Point(204, 0);
			this.TBOut.Multiline = true;
			this.TBOut.Name = "TBOut";
			this.TBOut.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TBOut.Size = new System.Drawing.Size(836, 592);
			this.TBOut.TabIndex = 100;
			// 
			// ChWordWrap
			// 
			this.ChWordWrap.AutoSize = true;
			this.ChWordWrap.Location = new System.Drawing.Point(32, 64);
			this.ChWordWrap.Name = "ChWordWrap";
			this.ChWordWrap.Size = new System.Drawing.Size(91, 17);
			this.ChWordWrap.TabIndex = 101;
			this.ChWordWrap.Text = "Завернуть ->";
			this.ChWordWrap.UseVisualStyleBackColor = true;
			this.ChWordWrap.CheckedChanged += new System.EventHandler(this.ChWordWrap_CheckedChanged);
			// 
			// BClose
			// 
			this.BClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.BClose.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BClose.Location = new System.Drawing.Point(4, 560);
			this.BClose.Name = "BClose";
			this.BClose.Size = new System.Drawing.Size(160, 32);
			this.BClose.TabIndex = 102;
			this.BClose.Text = "Выход";
			this.BClose.UseVisualStyleBackColor = true;
			this.BClose.Click += new System.EventHandler(this.BClose_Click);
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(188, 52);
			this.label1.TabIndex = 103;
			this.label1.Text = "Обмен иной программы с прибором";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// BToClipbrd
			// 
			this.BToClipbrd.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BToClipbrd.Image = global::TestDRVtransGas.Properties.Resources.Cut_p7;
			this.BToClipbrd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BToClipbrd.Location = new System.Drawing.Point(0, 0);
			this.BToClipbrd.Name = "BToClipbrd";
			this.BToClipbrd.Size = new System.Drawing.Size(123, 44);
			this.BToClipbrd.TabIndex = 105;
			this.BToClipbrd.Text = "В память ->";
			this.BToClipbrd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BToClipbrd.UseVisualStyleBackColor = true;
			this.BToClipbrd.Click += new System.EventHandler(this.BToClipbrd_Click);
			// 
			// BClearOut
			// 
			this.BClearOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BClearOut.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BClearOut.Image = global::TestDRVtransGas.Properties.Resources.Orange_System_Icon_06_p6;
			this.BClearOut.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BClearOut.Location = new System.Drawing.Point(80, 94);
			this.BClearOut.Name = "BClearOut";
			this.BClearOut.Size = new System.Drawing.Size(43, 40);
			this.BClearOut.TabIndex = 104;
			this.BClearOut.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BClearOut.UseVisualStyleBackColor = true;
			this.BClearOut.Click += new System.EventHandler(this.BClearOut_Click);
			// 
			// BConnect2
			// 
			this.BConnect2.BackColor = System.Drawing.SystemColors.ControlLight;
			this.BConnect2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BConnect2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.BConnect2.Image = ((System.Drawing.Image)(resources.GetObject("BConnect2.Image")));
			this.BConnect2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BConnect2.Location = new System.Drawing.Point(3, 78);
			this.BConnect2.Name = "BConnect2";
			this.BConnect2.Size = new System.Drawing.Size(182, 35);
			this.BConnect2.TabIndex = 99;
			this.BConnect2.Text = "Соединить";
			this.BConnect2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BConnect2.UseVisualStyleBackColor = false;
			this.BConnect2.Click += new System.EventHandler(this.BConnect2_Click);
			// 
			// BConnect1
			// 
			this.BConnect1.BackColor = System.Drawing.SystemColors.ControlLight;
			this.BConnect1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BConnect1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.BConnect1.Image = ((System.Drawing.Image)(resources.GetObject("BConnect1.Image")));
			this.BConnect1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BConnect1.Location = new System.Drawing.Point(3, 82);
			this.BConnect1.Name = "BConnect1";
			this.BConnect1.Size = new System.Drawing.Size(182, 35);
			this.BConnect1.TabIndex = 99;
			this.BConnect1.Text = "Соединить";
			this.BConnect1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BConnect1.UseVisualStyleBackColor = false;
			this.BConnect1.Click += new System.EventHandler(this.BTCP_TCP_Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.NUDFont);
			this.panel1.Controls.Add(this.BToClipbrd);
			this.panel1.Controls.Add(this.BClearOut);
			this.panel1.Controls.Add(this.ChWordWrap);
			this.panel1.Location = new System.Drawing.Point(77, 380);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(128, 136);
			this.panel1.TabIndex = 106;
			// 
			// NUDFont
			// 
			this.NUDFont.Location = new System.Drawing.Point(1, 113);
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
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(0, 96);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(41, 13);
			this.label6.TabIndex = 107;
			this.label6.Text = "Шрифт";
			// 
			// CTCPtoTCP
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Controls.Add(this.TBOut);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.BClose);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "CTCPtoTCP";
			this.Size = new System.Drawing.Size(1045, 599);
			this.Load += new System.EventHandler(this.CTCPtoTCP_Load);
			((System.ComponentModel.ISupportInitialize)(this.UDPort1)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.UDPort2)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.NUDFont)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ComboBox CBIP1;
		private System.Windows.Forms.NumericUpDown UDPort1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ComboBox CBIP2;
		private System.Windows.Forms.NumericUpDown UDPort2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button BConnect1;
		private System.Windows.Forms.TextBox TBOut;
		private System.Windows.Forms.Button BConnect2;
		private System.Windows.Forms.CheckBox ChWordWrap;
		private System.Windows.Forms.Button BClose;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button BClearOut;
		private System.Windows.Forms.Button BToClipbrd;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown NUDFont;
	}
}
