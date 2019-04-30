namespace TestDRVtransGas
{
	partial class CTestExch
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CTestExch));
			this.UDCOM = new System.Windows.Forms.NumericUpDown();
			this.label19 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.UDInverShift = new System.Windows.Forms.NumericUpDown();
			this.UDInverNum = new System.Windows.Forms.NumericUpDown();
			this.CBClearOut = new System.Windows.Forms.CheckBox();
			this.TBPort = new System.Windows.Forms.TextBox();
			this.label21 = new System.Windows.Forms.Label();
			this.CBIP = new System.Windows.Forms.ComboBox();
			this.label22 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.TBListExchange = new System.Windows.Forms.TextBox();
			this.TBCommand = new System.Windows.Forms.TextBox();
			this.label24 = new System.Windows.Forms.Label();
			this.IL24 = new System.Windows.Forms.ImageList(this.components);
			this.TBFile = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.CBDev = new System.Windows.Forms.ComboBox();
			this.PDopFunc = new System.Windows.Forms.Panel();
			this.BRMG = new System.Windows.Forms.Button();
			this.CkBModbusTCP = new System.Windows.Forms.CheckBox();
			this.NUDTimeout = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.BConnect = new System.Windows.Forms.Button();
			this.BRequest = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.CBBaudCOM = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.CBParity = new System.Windows.Forms.ComboBox();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.NUDDataBits = new System.Windows.Forms.NumericUpDown();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.CBStopBits = new System.Windows.Forms.ComboBox();
			this.ChBModeRequest = new System.Windows.Forms.CheckBox();
			this.label2 = new System.Windows.Forms.Label();
			this.UDAddr = new System.Windows.Forms.NumericUpDown();
			this.BReadCOM = new System.Windows.Forms.Button();
			this.BOpenCom = new System.Windows.Forms.Button();
			this.CBWrap = new System.Windows.Forms.CheckBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.NUDFontSize = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.BSaveFile = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.UDCOM)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.UDInverShift)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.UDInverNum)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.PDopFunc.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NUDTimeout)).BeginInit();
			this.groupBox2.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.tabPage3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NUDDataBits)).BeginInit();
			this.tabPage4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.UDAddr)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NUDFontSize)).BeginInit();
			this.SuspendLayout();
			// 
			// UDCOM
			// 
			this.UDCOM.Location = new System.Drawing.Point(4, 10);
			this.UDCOM.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.UDCOM.Name = "UDCOM";
			this.UDCOM.Size = new System.Drawing.Size(53, 20);
			this.UDCOM.TabIndex = 111;
			this.UDCOM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.UDCOM.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.UDCOM.ValueChanged += new System.EventHandler(this.UDCOM_ValueChanged);
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(100, 238);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(66, 13);
			this.label19.TabIndex = 107;
			this.label19.Text = "+ Инверсия";
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(84, 218);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(88, 13);
			this.label20.TabIndex = 106;
			this.label20.Text = "Сдвиг инверсии";
			// 
			// UDInverShift
			// 
			this.UDInverShift.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.UDInverShift.Location = new System.Drawing.Point(172, 218);
			this.UDInverShift.Name = "UDInverShift";
			this.UDInverShift.Size = new System.Drawing.Size(44, 16);
			this.UDInverShift.TabIndex = 105;
			this.UDInverShift.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// UDInverNum
			// 
			this.UDInverNum.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.UDInverNum.Location = new System.Drawing.Point(172, 236);
			this.UDInverNum.Name = "UDInverNum";
			this.UDInverNum.Size = new System.Drawing.Size(44, 16);
			this.UDInverNum.TabIndex = 104;
			this.UDInverNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.UDInverNum.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
			// 
			// CBClearOut
			// 
			this.CBClearOut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.CBClearOut.AutoSize = true;
			this.CBClearOut.Location = new System.Drawing.Point(244, 516);
			this.CBClearOut.Name = "CBClearOut";
			this.CBClearOut.Size = new System.Drawing.Size(154, 17);
			this.CBClearOut.TabIndex = 101;
			this.CBClearOut.Text = "Очищать при сохранении";
			this.CBClearOut.UseVisualStyleBackColor = true;
			// 
			// TBPort
			// 
			this.TBPort.Location = new System.Drawing.Point(40, 43);
			this.TBPort.Name = "TBPort";
			this.TBPort.Size = new System.Drawing.Size(80, 20);
			this.TBPort.TabIndex = 100;
			this.TBPort.Text = "502";
			this.TBPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(8, 44);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(32, 13);
			this.label21.TabIndex = 99;
			this.label21.Text = "Порт";
			// 
			// CBIP
			// 
			this.CBIP.FormattingEnabled = true;
			this.CBIP.Location = new System.Drawing.Point(28, 20);
			this.CBIP.Name = "CBIP";
			this.CBIP.Size = new System.Drawing.Size(172, 21);
			this.CBIP.TabIndex = 98;
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(8, 24);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(17, 13);
			this.label22.TabIndex = 97;
			this.label22.Text = "IP";
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Location = new System.Drawing.Point(232, 4);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(45, 13);
			this.label23.TabIndex = 96;
			this.label23.Text = "Прибор";
			this.label23.Visible = false;
			// 
			// TBListExchange
			// 
			this.TBListExchange.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TBListExchange.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.TBListExchange.Location = new System.Drawing.Point(229, 15);
			this.TBListExchange.Multiline = true;
			this.TBListExchange.Name = "TBListExchange";
			this.TBListExchange.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.TBListExchange.Size = new System.Drawing.Size(568, 491);
			this.TBListExchange.TabIndex = 93;
			// 
			// TBCommand
			// 
			this.TBCommand.Location = new System.Drawing.Point(8, 256);
			this.TBCommand.Multiline = true;
			this.TBCommand.Name = "TBCommand";
			this.TBCommand.Size = new System.Drawing.Size(211, 89);
			this.TBCommand.TabIndex = 92;
			this.TBCommand.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TBCommand_KeyPress);
			// 
			// label24
			// 
			this.label24.AutoSize = true;
			this.label24.Location = new System.Drawing.Point(8, 240);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(52, 13);
			this.label24.TabIndex = 90;
			this.label24.Text = "Команда";
			// 
			// IL24
			// 
			this.IL24.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("IL24.ImageStream")));
			this.IL24.TransparentColor = System.Drawing.Color.Transparent;
			this.IL24.Images.SetKeyName(0, "Core IP theme Icon 19_p7a.png");
			this.IL24.Images.SetKeyName(1, "Gris&GlowV1.5 Icon 19_p6.png");
			// 
			// TBFile
			// 
			this.TBFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TBFile.Location = new System.Drawing.Point(364, 544);
			this.TBFile.Name = "TBFile";
			this.TBFile.Size = new System.Drawing.Size(432, 20);
			this.TBFile.TabIndex = 112;
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.Transparent;
			this.groupBox1.Controls.Add(this.CBDev);
			this.groupBox1.Controls.Add(this.PDopFunc);
			this.groupBox1.Controls.Add(this.CkBModbusTCP);
			this.groupBox1.Controls.Add(this.NUDTimeout);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.BConnect);
			this.groupBox1.Controls.Add(this.CBIP);
			this.groupBox1.Controls.Add(this.label22);
			this.groupBox1.Controls.Add(this.label21);
			this.groupBox1.Controls.Add(this.TBPort);
			this.groupBox1.Controls.Add(this.BRequest);
			this.groupBox1.Location = new System.Drawing.Point(8, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(211, 200);
			this.groupBox1.TabIndex = 113;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "TCP";
			this.groupBox1.MouseHover += new System.EventHandler(this.PDopFunc_MouseLeave);
			// 
			// CBDev
			// 
			this.CBDev.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBDev.FormattingEnabled = true;
			this.CBDev.Location = new System.Drawing.Point(4, 66);
			this.CBDev.Name = "CBDev";
			this.CBDev.Size = new System.Drawing.Size(80, 21);
			this.CBDev.TabIndex = 107;
			this.CBDev.SelectedIndexChanged += new System.EventHandler(this.CBDev_SelectedIndexChanged);
			// 
			// PDopFunc
			// 
			this.PDopFunc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.PDopFunc.Controls.Add(this.BRMG);
			this.PDopFunc.Location = new System.Drawing.Point(1, 120);
			this.PDopFunc.Name = "PDopFunc";
			this.PDopFunc.Size = new System.Drawing.Size(27, 77);
			this.PDopFunc.TabIndex = 105;
			this.PDopFunc.MouseEnter += new System.EventHandler(this.PDopFunc_MouseEnter);
			// 
			// BRMG
			// 
			this.BRMG.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.BRMG.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BRMG.Location = new System.Drawing.Point(8, 32);
			this.BRMG.Name = "BRMG";
			this.BRMG.Size = new System.Drawing.Size(124, 36);
			this.BRMG.TabIndex = 0;
			this.BRMG.Text = "RMG on";
			this.BRMG.UseVisualStyleBackColor = true;
			this.BRMG.Click += new System.EventHandler(this.BRMG_Click);
			// 
			// CkBModbusTCP
			// 
			this.CkBModbusTCP.AutoSize = true;
			this.CkBModbusTCP.Location = new System.Drawing.Point(100, 120);
			this.CkBModbusTCP.Name = "CkBModbusTCP";
			this.CkBModbusTCP.Size = new System.Drawing.Size(97, 17);
			this.CkBModbusTCP.TabIndex = 104;
			this.CkBModbusTCP.Text = "MODBUS TCP";
			this.CkBModbusTCP.UseVisualStyleBackColor = true;
			// 
			// NUDTimeout
			// 
			this.NUDTimeout.Increment = new decimal(new int[] {
            40,
            0,
            0,
            0});
			this.NUDTimeout.Location = new System.Drawing.Point(84, 90);
			this.NUDTimeout.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.NUDTimeout.Name = "NUDTimeout";
			this.NUDTimeout.Size = new System.Drawing.Size(116, 20);
			this.NUDTimeout.TabIndex = 103;
			this.NUDTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.toolTip1.SetToolTip(this.NUDTimeout, "Минимальный интервал между  посылками\r\nкоманд после прихода ответа");
			this.NUDTimeout.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.NUDTimeout.ValueChanged += new System.EventHandler(this.NUDTimeout_ValueChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(12, 96);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(70, 13);
			this.label3.TabIndex = 102;
			this.label3.Text = "Таймаут, мс";
			this.toolTip1.SetToolTip(this.label3, "Минимальный интервал между  посылками\r\nкоманд после прихода ответа");
			// 
			// BConnect
			// 
			this.BConnect.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BConnect.Location = new System.Drawing.Point(84, 65);
			this.BConnect.Name = "BConnect";
			this.BConnect.Size = new System.Drawing.Size(116, 23);
			this.BConnect.TabIndex = 101;
			this.BConnect.Text = "Соединить";
			this.BConnect.UseVisualStyleBackColor = true;
			this.BConnect.Click += new System.EventHandler(this.BConnect_Click);
			// 
			// BRequest
			// 
			this.BRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.BRequest.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BRequest.Image = global::TestDRVtransGas.Properties.Resources.Connections_p6;
			this.BRequest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BRequest.Location = new System.Drawing.Point(56, 152);
			this.BRequest.Name = "BRequest";
			this.BRequest.Size = new System.Drawing.Size(140, 41);
			this.BRequest.TabIndex = 94;
			this.BRequest.Text = "Запрос TCP";
			this.BRequest.UseVisualStyleBackColor = true;
			this.BRequest.Click += new System.EventHandler(this.BRequest_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.Color.Transparent;
			this.groupBox2.Controls.Add(this.tabControl1);
			this.groupBox2.Controls.Add(this.ChBModeRequest);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.UDAddr);
			this.groupBox2.Controls.Add(this.BReadCOM);
			this.groupBox2.Controls.Add(this.BOpenCom);
			this.groupBox2.Location = new System.Drawing.Point(8, 352);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(211, 216);
			this.groupBox2.TabIndex = 114;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "ComPort";
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Location = new System.Drawing.Point(8, 64);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(200, 68);
			this.tabControl1.TabIndex = 116;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.CBBaudCOM);
			this.tabPage1.Controls.Add(this.label1);
			this.tabPage1.Controls.Add(this.UDCOM);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(192, 42);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Порт";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// CBBaudCOM
			// 
			this.CBBaudCOM.FormattingEnabled = true;
			this.CBBaudCOM.Items.AddRange(new object[] {
            "38400",
            "9600",
            "1200"});
			this.CBBaudCOM.Location = new System.Drawing.Point(112, 10);
			this.CBBaudCOM.Name = "CBBaudCOM";
			this.CBBaudCOM.Size = new System.Drawing.Size(76, 21);
			this.CBBaudCOM.TabIndex = 113;
			this.CBBaudCOM.SelectedIndexChanged += new System.EventHandler(this.CBBaudCOM_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(76, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 13);
			this.label1.TabIndex = 112;
			this.label1.Text = "Baud";
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.CBParity);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(192, 42);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Parity";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// CBParity
			// 
			this.CBParity.FormattingEnabled = true;
			this.CBParity.Location = new System.Drawing.Point(12, 12);
			this.CBParity.Name = "CBParity";
			this.CBParity.Size = new System.Drawing.Size(96, 21);
			this.CBParity.TabIndex = 0;
			this.CBParity.SelectedIndexChanged += new System.EventHandler(this.CBParity_SelectedIndexChanged);
			// 
			// tabPage3
			// 
			this.tabPage3.Controls.Add(this.NUDDataBits);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage3.Size = new System.Drawing.Size(192, 42);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "DataBits";
			this.tabPage3.UseVisualStyleBackColor = true;
			// 
			// NUDDataBits
			// 
			this.NUDDataBits.Location = new System.Drawing.Point(84, 12);
			this.NUDDataBits.Maximum = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.NUDDataBits.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
			this.NUDDataBits.Name = "NUDDataBits";
			this.NUDDataBits.Size = new System.Drawing.Size(52, 20);
			this.NUDDataBits.TabIndex = 0;
			this.NUDDataBits.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.NUDDataBits.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.NUDDataBits.ValueChanged += new System.EventHandler(this.NUDDataBits_ValueChanged);
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.CBStopBits);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage4.Size = new System.Drawing.Size(192, 42);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "StopBits";
			this.tabPage4.UseVisualStyleBackColor = true;
			// 
			// CBStopBits
			// 
			this.CBStopBits.FormattingEnabled = true;
			this.CBStopBits.Location = new System.Drawing.Point(92, 12);
			this.CBStopBits.Name = "CBStopBits";
			this.CBStopBits.Size = new System.Drawing.Size(92, 21);
			this.CBStopBits.TabIndex = 0;
			this.CBStopBits.SelectedIndexChanged += new System.EventHandler(this.CBStopBits_SelectedIndexChanged);
			// 
			// ChBModeRequest
			// 
			this.ChBModeRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.ChBModeRequest.AutoSize = true;
			this.ChBModeRequest.Checked = true;
			this.ChBModeRequest.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ChBModeRequest.Location = new System.Drawing.Point(128, 180);
			this.ChBModeRequest.Name = "ChBModeRequest";
			this.ChBModeRequest.Size = new System.Drawing.Size(77, 30);
			this.ChBModeRequest.TabIndex = 115;
			this.ChBModeRequest.Text = "Исходный\r\nзапрос";
			this.ChBModeRequest.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(4, 196);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(29, 13);
			this.label2.TabIndex = 115;
			this.label2.Text = "Addr";
			// 
			// UDAddr
			// 
			this.UDAddr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.UDAddr.Location = new System.Drawing.Point(36, 192);
			this.UDAddr.Maximum = new decimal(new int[] {
            254,
            0,
            0,
            0});
			this.UDAddr.Name = "UDAddr";
			this.UDAddr.Size = new System.Drawing.Size(60, 20);
			this.UDAddr.TabIndex = 114;
			// 
			// BReadCOM
			// 
			this.BReadCOM.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BReadCOM.Image = global::TestDRVtransGas.Properties.Resources.DaBlue_2006_047_p4;
			this.BReadCOM.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BReadCOM.Location = new System.Drawing.Point(8, 16);
			this.BReadCOM.Name = "BReadCOM";
			this.BReadCOM.Size = new System.Drawing.Size(184, 44);
			this.BReadCOM.TabIndex = 109;
			this.BReadCOM.Text = "Запрос ComPort";
			this.BReadCOM.UseVisualStyleBackColor = true;
			this.BReadCOM.Click += new System.EventHandler(this.BReadCOM_Click);
			// 
			// BOpenCom
			// 
			this.BOpenCom.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BOpenCom.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BOpenCom.ImageIndex = 1;
			this.BOpenCom.ImageList = this.IL24;
			this.BOpenCom.Location = new System.Drawing.Point(12, 140);
			this.BOpenCom.Name = "BOpenCom";
			this.BOpenCom.Size = new System.Drawing.Size(144, 32);
			this.BOpenCom.TabIndex = 108;
			this.BOpenCom.Text = "Открыть";
			this.BOpenCom.UseVisualStyleBackColor = true;
			this.BOpenCom.Click += new System.EventHandler(this.BDisconn_Click);
			// 
			// CBWrap
			// 
			this.CBWrap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.CBWrap.AutoSize = true;
			this.CBWrap.Location = new System.Drawing.Point(560, 516);
			this.CBWrap.Name = "CBWrap";
			this.CBWrap.Size = new System.Drawing.Size(108, 17);
			this.CBWrap.TabIndex = 115;
			this.CBWrap.Text = "Перенос строки";
			this.CBWrap.UseVisualStyleBackColor = true;
			this.CBWrap.CheckedChanged += new System.EventHandler(this.CBWrap_CheckedChanged);
			// 
			// NUDFontSize
			// 
			this.NUDFontSize.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.NUDFontSize.Location = new System.Drawing.Point(741, 506);
			this.NUDFontSize.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.NUDFontSize.Minimum = new decimal(new int[] {
            4,
            0,
            0,
            0});
			this.NUDFontSize.Name = "NUDFontSize";
			this.NUDFontSize.Size = new System.Drawing.Size(56, 20);
			this.NUDFontSize.TabIndex = 116;
			this.NUDFontSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.toolTip1.SetToolTip(this.NUDFontSize, "Размер шрифта");
			this.NUDFontSize.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
			this.NUDFontSize.ValueChanged += new System.EventHandler(this.NUDFontSize_ValueChanged);
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(696, 512);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 13);
			this.label4.TabIndex = 117;
			this.label4.Text = "Шрифт";
			// 
			// BSaveFile
			// 
			this.BSaveFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.BSaveFile.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BSaveFile.Image = global::TestDRVtransGas.Properties.Resources.DaBlue_2006_042_p6;
			this.BSaveFile.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BSaveFile.Location = new System.Drawing.Point(228, 542);
			this.BSaveFile.Name = "BSaveFile";
			this.BSaveFile.Size = new System.Drawing.Size(132, 24);
			this.BSaveFile.TabIndex = 103;
			this.BSaveFile.Text = "Сохранить в файл";
			this.BSaveFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BSaveFile.UseVisualStyleBackColor = true;
			this.BSaveFile.Click += new System.EventHandler(this.BSaveFile_Click);
			// 
			// CTestExch
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.label4);
			this.Controls.Add(this.NUDFontSize);
			this.Controls.Add(this.CBWrap);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.TBFile);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.label20);
			this.Controls.Add(this.UDInverShift);
			this.Controls.Add(this.UDInverNum);
			this.Controls.Add(this.BSaveFile);
			this.Controls.Add(this.CBClearOut);
			this.Controls.Add(this.label23);
			this.Controls.Add(this.TBListExchange);
			this.Controls.Add(this.TBCommand);
			this.Controls.Add(this.label24);
			this.Name = "CTestExch";
			this.Size = new System.Drawing.Size(823, 577);
			this.MouseEnter += new System.EventHandler(this.PDopFunc_MouseLeave);
			((System.ComponentModel.ISupportInitialize)(this.UDCOM)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.UDInverShift)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.UDInverNum)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.PDopFunc.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.NUDTimeout)).EndInit();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage1.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.tabPage3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.NUDDataBits)).EndInit();
			this.tabPage4.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.UDAddr)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NUDFontSize)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.NumericUpDown UDCOM;
		private System.Windows.Forms.Button BReadCOM;
		private System.Windows.Forms.Button BOpenCom;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		public System.Windows.Forms.NumericUpDown UDInverShift;
		public System.Windows.Forms.NumericUpDown UDInverNum;
		private System.Windows.Forms.Button BSaveFile;
		private System.Windows.Forms.CheckBox CBClearOut;
		private System.Windows.Forms.TextBox TBPort;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.ComboBox CBIP;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Button BRequest;
		public System.Windows.Forms.TextBox TBListExchange;
		public System.Windows.Forms.TextBox TBCommand;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.TextBox TBFile;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button BConnect;
        private System.Windows.Forms.GroupBox groupBox2;
    private System.Windows.Forms.Label label1;
    public System.Windows.Forms.ComboBox CBBaudCOM;
    public System.Windows.Forms.CheckBox ChBModeRequest;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.NumericUpDown UDAddr;
		private System.Windows.Forms.CheckBox CBWrap;
		public System.Windows.Forms.NumericUpDown NUDTimeout;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ImageList IL24;
		private System.Windows.Forms.CheckBox CkBModbusTCP;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private System.Windows.Forms.TabPage tabPage4;
		public System.Windows.Forms.ComboBox CBParity;
		public System.Windows.Forms.ComboBox CBStopBits;
		public System.Windows.Forms.NumericUpDown NUDDataBits;
		private System.Windows.Forms.Panel PDopFunc;
		private System.Windows.Forms.Button BRMG;
		private System.Windows.Forms.NumericUpDown NUDFontSize;
		private System.Windows.Forms.Label label4;
		public System.Windows.Forms.ComboBox CBDev;
	}
}
