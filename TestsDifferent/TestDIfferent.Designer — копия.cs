namespace TestDRVtransGas.TestsDifferent
{
	partial class CTestDifferent
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.TBCRCcalc = new System.Windows.Forms.TextBox();
			this.BCountCRC = new System.Windows.Forms.Button();
			this.RBWriting = new System.Windows.Forms.RadioButton();
			this.NUDIDstart = new System.Windows.Forms.NumericUpDown();
			this.label14 = new System.Windows.Forms.Label();
			this.CBDevsWr = new System.Windows.Forms.ComboBox();
			this.label12 = new System.Windows.Forms.Label();
			this.NUDIntervalWr = new System.Windows.Forms.NumericUpDown();
			this.label11 = new System.Windows.Forms.Label();
			this.TBAddresses = new System.Windows.Forms.TextBox();
			this.BStartWr = new System.Windows.Forms.Button();
			this.BFindVal = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.TBValFromBytes = new System.Windows.Forms.TextBox();
			this.BBytesToVal = new System.Windows.Forms.Button();
			this.TBBytes = new System.Windows.Forms.TextBox();
			this.BTest = new System.Windows.Forms.Button();
			this.UDYear = new System.Windows.Forms.NumericUpDown();
			this.UDMonth = new System.Windows.Forms.NumericUpDown();
			this.UDDay = new System.Windows.Forms.NumericUpDown();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.TBOut = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.RB_LH = new System.Windows.Forms.RadioButton();
			this.RB_HL = new System.Windows.Forms.RadioButton();
			this.BCalcCRC = new System.Windows.Forms.Button();
			this.TBCalc8005 = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NUDIDstart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NUDIntervalWr)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.UDYear)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.UDMonth)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.UDDay)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox5.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.numericUpDown1);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.TBCRCcalc);
			this.groupBox1.Controls.Add(this.BCountCRC);
			this.groupBox1.Location = new System.Drawing.Point(369, 69);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(345, 113);
			this.groupBox1.TabIndex = 80;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "CRC";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Hexadecimal = true;
			this.numericUpDown1.Location = new System.Drawing.Point(242, 26);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(94, 20);
			this.numericUpDown1.TabIndex = 71;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(186, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(43, 13);
			this.label1.TabIndex = 70;
			this.label1.Text = "InitStart";
			// 
			// TBCRCcalc
			// 
			this.TBCRCcalc.Location = new System.Drawing.Point(24, 50);
			this.TBCRCcalc.Name = "TBCRCcalc";
			this.TBCRCcalc.Size = new System.Drawing.Size(126, 20);
			this.TBCRCcalc.TabIndex = 69;
			// 
			// BCountCRC
			// 
			this.BCountCRC.Location = new System.Drawing.Point(26, 78);
			this.BCountCRC.Name = "BCountCRC";
			this.BCountCRC.Size = new System.Drawing.Size(75, 26);
			this.BCountCRC.TabIndex = 68;
			this.BCountCRC.Text = "Считать";
			this.BCountCRC.UseVisualStyleBackColor = true;
			this.BCountCRC.Click += new System.EventHandler(this.BCountCRC_Click);
			// 
			// RBWriting
			// 
			this.RBWriting.AutoSize = true;
			this.RBWriting.Location = new System.Drawing.Point(30, 102);
			this.RBWriting.Name = "RBWriting";
			this.RBWriting.Size = new System.Drawing.Size(66, 17);
			this.RBWriting.TabIndex = 79;
			this.RBWriting.TabStop = true;
			this.RBWriting.Text = "Стоим-с";
			this.RBWriting.UseVisualStyleBackColor = true;
			// 
			// NUDIDstart
			// 
			this.NUDIDstart.Location = new System.Drawing.Point(138, 182);
			this.NUDIDstart.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.NUDIDstart.Name = "NUDIDstart";
			this.NUDIDstart.Size = new System.Drawing.Size(68, 20);
			this.NUDIDstart.TabIndex = 78;
			this.NUDIDstart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.NUDIDstart.Value = new decimal(new int[] {
            202,
            0,
            0,
            0});
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(138, 166);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(41, 13);
			this.label14.TabIndex = 77;
			this.label14.Text = "ID start";
			// 
			// CBDevsWr
			// 
			this.CBDevsWr.FormattingEnabled = true;
			this.CBDevsWr.Location = new System.Drawing.Point(218, 58);
			this.CBDevsWr.Name = "CBDevsWr";
			this.CBDevsWr.Size = new System.Drawing.Size(95, 21);
			this.CBDevsWr.TabIndex = 76;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(26, 150);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(76, 32);
			this.label12.TabIndex = 75;
			this.label12.Text = "Интервал записи, сек";
			// 
			// NUDIntervalWr
			// 
			this.NUDIntervalWr.Location = new System.Drawing.Point(26, 182);
			this.NUDIntervalWr.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
			this.NUDIntervalWr.Name = "NUDIntervalWr";
			this.NUDIntervalWr.Size = new System.Drawing.Size(76, 20);
			this.NUDIntervalWr.TabIndex = 74;
			this.NUDIntervalWr.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.NUDIntervalWr.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(150, 38);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(44, 13);
			this.label11.TabIndex = 73;
			this.label11.Text = "Адреса";
			// 
			// TBAddresses
			// 
			this.TBAddresses.Location = new System.Drawing.Point(146, 58);
			this.TBAddresses.Multiline = true;
			this.TBAddresses.Name = "TBAddresses";
			this.TBAddresses.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TBAddresses.Size = new System.Drawing.Size(60, 100);
			this.TBAddresses.TabIndex = 72;
			this.TBAddresses.Text = "5007\r\n5009\r\n5013\r\n481\r\n5003\r\n5011";
			this.TBAddresses.WordWrap = false;
			// 
			// BStartWr
			// 
			this.BStartWr.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BStartWr.Location = new System.Drawing.Point(26, 58);
			this.BStartWr.Name = "BStartWr";
			this.BStartWr.Size = new System.Drawing.Size(108, 31);
			this.BStartWr.TabIndex = 71;
			this.BStartWr.Text = "Старт записи";
			this.BStartWr.UseVisualStyleBackColor = true;
			this.BStartWr.Click += new System.EventHandler(this.BStartWr_Click);
			// 
			// BFindVal
			// 
			this.BFindVal.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BFindVal.Location = new System.Drawing.Point(438, 32);
			this.BFindVal.Name = "BFindVal";
			this.BFindVal.Size = new System.Drawing.Size(75, 23);
			this.BFindVal.TabIndex = 70;
			this.BFindVal.Text = "FindVal";
			this.BFindVal.UseVisualStyleBackColor = true;
			this.BFindVal.Click += new System.EventHandler(this.BFindVal_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.TBValFromBytes);
			this.groupBox2.Controls.Add(this.BBytesToVal);
			this.groupBox2.Controls.Add(this.TBBytes);
			this.groupBox2.Location = new System.Drawing.Point(28, 266);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(200, 130);
			this.groupBox2.TabIndex = 81;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Байты - число";
			// 
			// TBValFromBytes
			// 
			this.TBValFromBytes.Location = new System.Drawing.Point(10, 52);
			this.TBValFromBytes.Name = "TBValFromBytes";
			this.TBValFromBytes.Size = new System.Drawing.Size(172, 20);
			this.TBValFromBytes.TabIndex = 2;
			// 
			// BBytesToVal
			// 
			this.BBytesToVal.Location = new System.Drawing.Point(14, 94);
			this.BBytesToVal.Name = "BBytesToVal";
			this.BBytesToVal.Size = new System.Drawing.Size(75, 23);
			this.BBytesToVal.TabIndex = 1;
			this.BBytesToVal.Text = "Перевести";
			this.BBytesToVal.UseVisualStyleBackColor = true;
			this.BBytesToVal.Click += new System.EventHandler(this.BBytesToVal_Click);
			// 
			// TBBytes
			// 
			this.TBBytes.Location = new System.Drawing.Point(12, 24);
			this.TBBytes.Name = "TBBytes";
			this.TBBytes.Size = new System.Drawing.Size(170, 20);
			this.TBBytes.TabIndex = 0;
			this.TBBytes.Text = "68 25 23 41";
			// 
			// BTest
			// 
			this.BTest.Location = new System.Drawing.Point(24, 79);
			this.BTest.Name = "BTest";
			this.BTest.Size = new System.Drawing.Size(75, 23);
			this.BTest.TabIndex = 82;
			this.BTest.Text = "button1";
			this.BTest.UseVisualStyleBackColor = true;
			this.BTest.Click += new System.EventHandler(this.BTest_Click);
			// 
			// UDYear
			// 
			this.UDYear.Location = new System.Drawing.Point(24, 53);
			this.UDYear.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.UDYear.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
			this.UDYear.Name = "UDYear";
			this.UDYear.Size = new System.Drawing.Size(67, 20);
			this.UDYear.TabIndex = 83;
			this.UDYear.Value = new decimal(new int[] {
            17,
            0,
            0,
            0});
			// 
			// UDMonth
			// 
			this.UDMonth.Location = new System.Drawing.Point(101, 53);
			this.UDMonth.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.UDMonth.Minimum = new decimal(new int[] {
            500,
            0,
            0,
            -2147483648});
			this.UDMonth.Name = "UDMonth";
			this.UDMonth.Size = new System.Drawing.Size(67, 20);
			this.UDMonth.TabIndex = 83;
			this.UDMonth.Value = new decimal(new int[] {
            3,
            0,
            0,
            -2147483648});
			// 
			// UDDay
			// 
			this.UDDay.Location = new System.Drawing.Point(174, 53);
			this.UDDay.Minimum = new decimal(new int[] {
            31,
            0,
            0,
            -2147483648});
			this.UDDay.Name = "UDDay";
			this.UDDay.Size = new System.Drawing.Size(67, 20);
			this.UDDay.TabIndex = 83;
			this.UDDay.Value = new decimal(new int[] {
            9,
            0,
            0,
            0});
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.TBOut);
			this.groupBox3.Controls.Add(this.BTest);
			this.groupBox3.Controls.Add(this.UDDay);
			this.groupBox3.Controls.Add(this.UDYear);
			this.groupBox3.Controls.Add(this.UDMonth);
			this.groupBox3.Location = new System.Drawing.Point(369, 208);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(344, 129);
			this.groupBox3.TabIndex = 84;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "groupBox3";
			// 
			// TBOut
			// 
			this.TBOut.Location = new System.Drawing.Point(26, 24);
			this.TBOut.Name = "TBOut";
			this.TBOut.Size = new System.Drawing.Size(215, 20);
			this.TBOut.TabIndex = 84;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(412, 384);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 85;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// groupBox4
			// 
			this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox4.Controls.Add(this.groupBox5);
			this.groupBox4.Controls.Add(this.BCalcCRC);
			this.groupBox4.Controls.Add(this.TBCalc8005);
			this.groupBox4.Location = new System.Drawing.Point(20, 436);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(816, 112);
			this.groupBox4.TabIndex = 86;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Контрольная сумма 8005";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.RB_LH);
			this.groupBox5.Controls.Add(this.RB_HL);
			this.groupBox5.Location = new System.Drawing.Point(200, 52);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(176, 56);
			this.groupBox5.TabIndex = 3;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "КС";
			// 
			// RB_LH
			// 
			this.RB_LH.AutoSize = true;
			this.RB_LH.Checked = true;
			this.RB_LH.Location = new System.Drawing.Point(32, 12);
			this.RB_LH.Name = "RB_LH";
			this.RB_LH.Size = new System.Drawing.Size(118, 17);
			this.RB_LH.TabIndex = 1;
			this.RB_LH.TabStop = true;
			this.RB_LH.Text = "Младший-старший";
			this.RB_LH.UseVisualStyleBackColor = true;
			// 
			// RB_HL
			// 
			this.RB_HL.AutoSize = true;
			this.RB_HL.Location = new System.Drawing.Point(32, 32);
			this.RB_HL.Name = "RB_HL";
			this.RB_HL.Size = new System.Drawing.Size(118, 17);
			this.RB_HL.TabIndex = 2;
			this.RB_HL.Text = "Старший-младший";
			this.RB_HL.UseVisualStyleBackColor = true;
			// 
			// BCalcCRC
			// 
			this.BCalcCRC.Location = new System.Drawing.Point(8, 56);
			this.BCalcCRC.Name = "BCalcCRC";
			this.BCalcCRC.Size = new System.Drawing.Size(140, 31);
			this.BCalcCRC.TabIndex = 0;
			this.BCalcCRC.Text = "Рассчітать";
			this.BCalcCRC.UseVisualStyleBackColor = true;
			this.BCalcCRC.Click += new System.EventHandler(this.BCalcCRC_Click);
			// 
			// TBCalc8005
			// 
			this.TBCalc8005.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TBCalc8005.Location = new System.Drawing.Point(8, 28);
			this.TBCalc8005.Name = "TBCalc8005";
			this.TBCalc8005.Size = new System.Drawing.Size(796, 20);
			this.TBCalc8005.TabIndex = 0;
			this.TBCalc8005.Enter += new System.EventHandler(this.TBCalc8005_Enter);
			// 
			// CTestDifferent
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.RBWriting);
			this.Controls.Add(this.NUDIDstart);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.CBDevsWr);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.NUDIntervalWr);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.TBAddresses);
			this.Controls.Add(this.BStartWr);
			this.Controls.Add(this.BFindVal);
			this.Name = "CTestDifferent";
			this.Size = new System.Drawing.Size(853, 692);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NUDIDstart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NUDIntervalWr)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.UDYear)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.UDMonth)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.UDDay)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button BCountCRC;
		private System.Windows.Forms.RadioButton RBWriting;
		private System.Windows.Forms.NumericUpDown NUDIDstart;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.NumericUpDown NUDIntervalWr;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox TBAddresses;
		private System.Windows.Forms.Button BFindVal;
		private System.Windows.Forms.TextBox TBCRCcalc;
		public System.Windows.Forms.ComboBox CBDevsWr;
		public System.Windows.Forms.Button BStartWr;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox TBValFromBytes;
		private System.Windows.Forms.Button BBytesToVal;
		private System.Windows.Forms.TextBox TBBytes;
        public System.Windows.Forms.Button BTest;
    private System.Windows.Forms.NumericUpDown UDYear;
    private System.Windows.Forms.NumericUpDown UDMonth;
    private System.Windows.Forms.NumericUpDown UDDay;
    private System.Windows.Forms.GroupBox groupBox3;
    private System.Windows.Forms.TextBox TBOut;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button BCalcCRC;
		private System.Windows.Forms.TextBox TBCalc8005;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.RadioButton RB_LH;
		private System.Windows.Forms.RadioButton RB_HL;
	}
}
