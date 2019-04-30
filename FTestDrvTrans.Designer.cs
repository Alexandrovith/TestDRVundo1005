namespace TestDRVtransGas
{
    partial class FTestDrivers
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.Label label2;
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FTestDrivers));
			this.TScanParam = new System.Windows.Forms.Timer(this.components);
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.TPParametersScan = new System.Windows.Forms.TabPage();
			this.CBDeviceWr = new System.Windows.Forms.ComboBox();
			this.NTimeResp = new System.Windows.Forms.NumericUpDown();
			this.CBData = new System.Windows.Forms.ComboBox();
			this.CBParameterName = new System.Windows.Forms.ComboBox();
			this.CBDataWritePar = new System.Windows.Forms.ComboBox();
			this.label7 = new System.Windows.Forms.Label();
			this.CBTypeData = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.TBIDwritePar = new System.Windows.Forms.TextBox();
			this.BWritePar = new System.Windows.Forms.Button();
			this.CBStateLog = new System.Windows.Forms.ComboBox();
			this.BSwithLog = new System.Windows.Forms.Button();
			this.label10 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.BSetFirstDateArchs = new System.Windows.Forms.Button();
			this.label17 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.TPAlarm = new System.Windows.Forms.DateTimePicker();
			this.TPInterfer = new System.Windows.Forms.DateTimePicker();
			this.label15 = new System.Windows.Forms.Label();
			this.TPArchHour = new System.Windows.Forms.DateTimePicker();
			this.label13 = new System.Windows.Forms.Label();
			this.TPArchDay = new System.Windows.Forms.DateTimePicker();
			this.LWorkTime = new System.Windows.Forms.Label();
			this.LPuskTime = new System.Windows.Forms.Label();
			this.CBAccumArch = new System.Windows.Forms.CheckBox();
			this.CBDevicesStat = new System.Windows.Forms.ComboBox();
			this.LStatExchan = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.CBShowInd = new System.Windows.Forms.CheckBox();
			this.CBReadArch = new System.Windows.Forms.CheckBox();
			this.CBIndArchOff = new System.Windows.Forms.CheckBox();
			this.PParameters = new System.Windows.Forms.Panel();
			this.BClrErr = new System.Windows.Forms.Button();
			this.LNumErr = new System.Windows.Forms.Label();
			this.CBErrors = new System.Windows.Forms.ComboBox();
			this.RBScan = new System.Windows.Forms.RadioButton();
			this.BStartScan = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.NIntervalScan = new System.Windows.Forms.NumericUpDown();
			this.BGetErr = new System.Windows.Forms.Button();
			this.BStopDrv = new System.Windows.Forms.Button();
			this.BClose = new System.Windows.Forms.Button();
			this.BTest1 = new System.Windows.Forms.Button();
			this.TPDevices = new System.Windows.Forms.TabPage();
			this.BSaveDevices = new System.Windows.Forms.Button();
			this.TPParams = new System.Windows.Forms.TabPage();
			this.BSaveParams = new System.Windows.Forms.Button();
			this.TTest = new System.Windows.Forms.TabPage();
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
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			label2 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.TPParametersScan.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NTimeResp)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NIntervalScan)).BeginInit();
			this.TPDevices.SuspendLayout();
			this.TPParams.SuspendLayout();
			this.TTest.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NUDIDstart)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NUDIntervalWr)).BeginInit();
			this.SuspendLayout();
			// 
			// label2
			// 
			label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			label2.Location = new System.Drawing.Point(80, 152);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(180, 20);
			label2.TabIndex = 30;
			label2.Text = "Опрос параметров из драйвера";
			label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// TScanParam
			// 
			this.TScanParam.Tick += new System.EventHandler(this.TScanParam_Tick);
			// 
			// tabControl1
			// 
			this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Left;
			this.tabControl1.Controls.Add(this.TPParametersScan);
			this.tabControl1.Controls.Add(this.TPDevices);
			this.tabControl1.Controls.Add(this.TPParams);
			this.tabControl1.Controls.Add(this.TTest);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Multiline = true;
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(986, 733);
			this.tabControl1.TabIndex = 0;
			// 
			// TPParametersScan
			// 
			this.TPParametersScan.Controls.Add(this.CBDeviceWr);
			this.TPParametersScan.Controls.Add(this.NTimeResp);
			this.TPParametersScan.Controls.Add(this.CBData);
			this.TPParametersScan.Controls.Add(this.CBParameterName);
			this.TPParametersScan.Controls.Add(this.CBDataWritePar);
			this.TPParametersScan.Controls.Add(this.label7);
			this.TPParametersScan.Controls.Add(this.CBTypeData);
			this.TPParametersScan.Controls.Add(this.label6);
			this.TPParametersScan.Controls.Add(this.label8);
			this.TPParametersScan.Controls.Add(this.label5);
			this.TPParametersScan.Controls.Add(this.label4);
			this.TPParametersScan.Controls.Add(this.label3);
			this.TPParametersScan.Controls.Add(this.TBIDwritePar);
			this.TPParametersScan.Controls.Add(this.BWritePar);
			this.TPParametersScan.Controls.Add(this.CBStateLog);
			this.TPParametersScan.Controls.Add(this.BSwithLog);
			this.TPParametersScan.Controls.Add(this.label10);
			this.TPParametersScan.Controls.Add(this.panel1);
			this.TPParametersScan.Controls.Add(this.LWorkTime);
			this.TPParametersScan.Controls.Add(this.LPuskTime);
			this.TPParametersScan.Controls.Add(this.CBAccumArch);
			this.TPParametersScan.Controls.Add(this.CBDevicesStat);
			this.TPParametersScan.Controls.Add(this.LStatExchan);
			this.TPParametersScan.Controls.Add(this.label9);
			this.TPParametersScan.Controls.Add(this.CBShowInd);
			this.TPParametersScan.Controls.Add(this.CBReadArch);
			this.TPParametersScan.Controls.Add(this.CBIndArchOff);
			this.TPParametersScan.Controls.Add(label2);
			this.TPParametersScan.Controls.Add(this.PParameters);
			this.TPParametersScan.Controls.Add(this.BClrErr);
			this.TPParametersScan.Controls.Add(this.LNumErr);
			this.TPParametersScan.Controls.Add(this.CBErrors);
			this.TPParametersScan.Controls.Add(this.RBScan);
			this.TPParametersScan.Controls.Add(this.BStartScan);
			this.TPParametersScan.Controls.Add(this.label1);
			this.TPParametersScan.Controls.Add(this.NIntervalScan);
			this.TPParametersScan.Controls.Add(this.BGetErr);
			this.TPParametersScan.Controls.Add(this.BStopDrv);
			this.TPParametersScan.Controls.Add(this.BClose);
			this.TPParametersScan.Controls.Add(this.BTest1);
			this.TPParametersScan.Location = new System.Drawing.Point(23, 4);
			this.TPParametersScan.Name = "TPParametersScan";
			this.TPParametersScan.Padding = new System.Windows.Forms.Padding(3);
			this.TPParametersScan.Size = new System.Drawing.Size(959, 725);
			this.TPParametersScan.TabIndex = 0;
			this.TPParametersScan.Text = "Опрос параметров";
			this.TPParametersScan.UseVisualStyleBackColor = true;
			// 
			// CBDeviceWr
			// 
			this.CBDeviceWr.FormattingEnabled = true;
			this.CBDeviceWr.Location = new System.Drawing.Point(379, 116);
			this.CBDeviceWr.Name = "CBDeviceWr";
			this.CBDeviceWr.Size = new System.Drawing.Size(95, 21);
			this.CBDeviceWr.TabIndex = 89;
			// 
			// NTimeResp
			// 
			this.NTimeResp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.NTimeResp.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.NTimeResp.Location = new System.Drawing.Point(888, 116);
			this.NTimeResp.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
			this.NTimeResp.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.NTimeResp.Name = "NTimeResp";
			this.NTimeResp.Size = new System.Drawing.Size(63, 21);
			this.NTimeResp.TabIndex = 84;
			this.NTimeResp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.NTimeResp.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// CBData
			// 
			this.CBData.FormattingEnabled = true;
			this.CBData.Location = new System.Drawing.Point(472, 116);
			this.CBData.Name = "CBData";
			this.CBData.Size = new System.Drawing.Size(77, 21);
			this.CBData.TabIndex = 88;
			// 
			// CBParameterName
			// 
			this.CBParameterName.FormattingEnabled = true;
			this.CBParameterName.Location = new System.Drawing.Point(773, 116);
			this.CBParameterName.Name = "CBParameterName";
			this.CBParameterName.Size = new System.Drawing.Size(117, 21);
			this.CBParameterName.TabIndex = 87;
			this.CBParameterName.Text = "TempParName";
			// 
			// CBDataWritePar
			// 
			this.CBDataWritePar.FormattingEnabled = true;
			this.CBDataWritePar.Location = new System.Drawing.Point(549, 116);
			this.CBDataWritePar.Name = "CBDataWritePar";
			this.CBDataWritePar.Size = new System.Drawing.Size(128, 21);
			this.CBDataWritePar.TabIndex = 86;
			this.CBDataWritePar.Text = "0 2";
			// 
			// label7
			// 
			this.label7.BackColor = System.Drawing.Color.Gainsboro;
			this.label7.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label7.Location = new System.Drawing.Point(888, 100);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(65, 16);
			this.label7.TabIndex = 85;
			this.label7.Text = "TimeResp";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// CBTypeData
			// 
			this.CBTypeData.FormattingEnabled = true;
			this.CBTypeData.Location = new System.Drawing.Point(677, 116);
			this.CBTypeData.Name = "CBTypeData";
			this.CBTypeData.Size = new System.Drawing.Size(96, 21);
			this.CBTypeData.TabIndex = 83;
			// 
			// label6
			// 
			this.label6.BackColor = System.Drawing.Color.Gainsboro;
			this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label6.Location = new System.Drawing.Point(677, 100);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(96, 16);
			this.label6.TabIndex = 82;
			this.label6.Text = "Тип";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label8
			// 
			this.label8.BackColor = System.Drawing.Color.Gainsboro;
			this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label8.Location = new System.Drawing.Point(549, 100);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(128, 16);
			this.label8.TabIndex = 79;
			this.label8.Text = "DataToWrite";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label5
			// 
			this.label5.BackColor = System.Drawing.Color.Gainsboro;
			this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label5.Location = new System.Drawing.Point(473, 100);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(76, 16);
			this.label5.TabIndex = 80;
			this.label5.Text = "Data";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.Gainsboro;
			this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label4.Location = new System.Drawing.Point(379, 100);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(95, 16);
			this.label4.TabIndex = 81;
			this.label4.Text = "Прибор";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.Color.Gainsboro;
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label3.Location = new System.Drawing.Point(347, 100);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(32, 16);
			this.label3.TabIndex = 78;
			this.label3.Text = "ID";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// TBIDwritePar
			// 
			this.TBIDwritePar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.TBIDwritePar.Location = new System.Drawing.Point(347, 116);
			this.TBIDwritePar.Name = "TBIDwritePar";
			this.TBIDwritePar.Size = new System.Drawing.Size(32, 21);
			this.TBIDwritePar.TabIndex = 77;
			this.TBIDwritePar.Text = "600";
			this.TBIDwritePar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// BWritePar
			// 
			this.BWritePar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BWritePar.Enabled = false;
			this.BWritePar.Location = new System.Drawing.Point(203, 99);
			this.BWritePar.Name = "BWritePar";
			this.BWritePar.Size = new System.Drawing.Size(144, 39);
			this.BWritePar.TabIndex = 76;
			this.BWritePar.Text = "Записать параметр";
			this.BWritePar.UseVisualStyleBackColor = true;
			this.BWritePar.Click += new System.EventHandler(this.BWritePar_Click);
			// 
			// CBStateLog
			// 
			this.CBStateLog.FormattingEnabled = true;
			this.CBStateLog.Items.AddRange(new object[] {
            "BelTranzDRV.log",
            "Mem",
            "NONE"});
			this.CBStateLog.Location = new System.Drawing.Point(570, 4);
			this.CBStateLog.Name = "CBStateLog";
			this.CBStateLog.Size = new System.Drawing.Size(99, 21);
			this.CBStateLog.TabIndex = 75;
			// 
			// BSwithLog
			// 
			this.BSwithLog.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BSwithLog.Location = new System.Drawing.Point(484, 3);
			this.BSwithLog.Name = "BSwithLog";
			this.BSwithLog.Size = new System.Drawing.Size(86, 23);
			this.BSwithLog.TabIndex = 74;
			this.BSwithLog.Text = "Включить Log";
			this.BSwithLog.UseVisualStyleBackColor = true;
			this.BSwithLog.Click += new System.EventHandler(this.BSwithLog_Click);
			// 
			// label10
			// 
			this.label10.BackColor = System.Drawing.Color.Gainsboro;
			this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label10.Location = new System.Drawing.Point(773, 100);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(116, 16);
			this.label10.TabIndex = 90;
			this.label10.Text = "ParamName";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.DarkGray;
			this.panel1.Controls.Add(this.BSetFirstDateArchs);
			this.panel1.Controls.Add(this.label17);
			this.panel1.Controls.Add(this.label16);
			this.panel1.Controls.Add(this.TPAlarm);
			this.panel1.Controls.Add(this.TPInterfer);
			this.panel1.Controls.Add(this.label15);
			this.panel1.Controls.Add(this.TPArchHour);
			this.panel1.Controls.Add(this.label13);
			this.panel1.Controls.Add(this.TPArchDay);
			this.panel1.Location = new System.Drawing.Point(140, 56);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(812, 36);
			this.panel1.TabIndex = 67;
			// 
			// BSetFirstDateArchs
			// 
			this.BSetFirstDateArchs.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BSetFirstDateArchs.Image = global::TestDRVtransGas.Properties.Resources.DaBlue_2006_042_p6;
			this.BSetFirstDateArchs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BSetFirstDateArchs.Location = new System.Drawing.Point(636, 4);
			this.BSetFirstDateArchs.Name = "BSetFirstDateArchs";
			this.BSetFirstDateArchs.Size = new System.Drawing.Size(168, 29);
			this.BSetFirstDateArchs.TabIndex = 75;
			this.BSetFirstDateArchs.Text = "Даты архивов применить";
			this.BSetFirstDateArchs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BSetFirstDateArchs.UseVisualStyleBackColor = true;
			this.BSetFirstDateArchs.Click += new System.EventHandler(this.BSetFirstDateArchs_Click);
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.ForeColor = System.Drawing.Color.White;
			this.label17.Location = new System.Drawing.Point(488, 12);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(44, 13);
			this.label17.TabIndex = 71;
			this.label17.Text = "Аварий";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.ForeColor = System.Drawing.Color.White;
			this.label16.Location = new System.Drawing.Point(296, 12);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(82, 13);
			this.label16.TabIndex = 74;
			this.label16.Text = "Вмешательств";
			// 
			// TPAlarm
			// 
			this.TPAlarm.CustomFormat = "DDMMYYYY";
			this.TPAlarm.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.TPAlarm.Location = new System.Drawing.Point(532, 8);
			this.TPAlarm.MinDate = new System.DateTime(1900, 2, 1, 0, 0, 0, 0);
			this.TPAlarm.Name = "TPAlarm";
			this.TPAlarm.Size = new System.Drawing.Size(96, 20);
			this.TPAlarm.TabIndex = 69;
			// 
			// TPInterfer
			// 
			this.TPInterfer.CustomFormat = "DDMMYYYY";
			this.TPInterfer.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.TPInterfer.Location = new System.Drawing.Point(380, 8);
			this.TPInterfer.MinDate = new System.DateTime(1900, 2, 1, 0, 0, 0, 0);
			this.TPInterfer.Name = "TPInterfer";
			this.TPInterfer.Size = new System.Drawing.Size(96, 20);
			this.TPInterfer.TabIndex = 73;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.ForeColor = System.Drawing.Color.White;
			this.label15.Location = new System.Drawing.Point(160, 12);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(27, 13);
			this.label15.TabIndex = 72;
			this.label15.Text = "Час";
			// 
			// TPArchHour
			// 
			this.TPArchHour.CustomFormat = "DDMMYYYY";
			this.TPArchHour.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.TPArchHour.Location = new System.Drawing.Point(189, 8);
			this.TPArchHour.MinDate = new System.DateTime(1900, 2, 1, 0, 0, 0, 0);
			this.TPArchHour.Name = "TPArchHour";
			this.TPArchHour.Size = new System.Drawing.Size(96, 20);
			this.TPArchHour.TabIndex = 70;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.ForeColor = System.Drawing.Color.White;
			this.label13.Location = new System.Drawing.Point(16, 12);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(34, 13);
			this.label13.TabIndex = 68;
			this.label13.Text = "День";
			// 
			// TPArchDay
			// 
			this.TPArchDay.CustomFormat = "DDMMYYYY";
			this.TPArchDay.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.TPArchDay.Location = new System.Drawing.Point(56, 8);
			this.TPArchDay.MinDate = new System.DateTime(1900, 2, 1, 0, 0, 0, 0);
			this.TPArchDay.Name = "TPArchDay";
			this.TPArchDay.Size = new System.Drawing.Size(96, 20);
			this.TPArchDay.TabIndex = 67;
			// 
			// LWorkTime
			// 
			this.LWorkTime.AutoSize = true;
			this.LWorkTime.Location = new System.Drawing.Point(9, 54);
			this.LWorkTime.Name = "LWorkTime";
			this.LWorkTime.Size = new System.Drawing.Size(13, 13);
			this.LWorkTime.TabIndex = 59;
			this.LWorkTime.Text = "0";
			// 
			// LPuskTime
			// 
			this.LPuskTime.AutoSize = true;
			this.LPuskTime.Location = new System.Drawing.Point(9, 67);
			this.LPuskTime.Name = "LPuskTime";
			this.LPuskTime.Size = new System.Drawing.Size(13, 13);
			this.LPuskTime.TabIndex = 58;
			this.LPuskTime.Text = "0";
			// 
			// CBAccumArch
			// 
			this.CBAccumArch.AutoSize = true;
			this.CBAccumArch.Location = new System.Drawing.Point(395, 152);
			this.CBAccumArch.Name = "CBAccumArch";
			this.CBAccumArch.Size = new System.Drawing.Size(132, 17);
			this.CBAccumArch.TabIndex = 54;
			this.CBAccumArch.Text = "Накопление архивов";
			this.CBAccumArch.UseVisualStyleBackColor = true;
			// 
			// CBDevicesStat
			// 
			this.CBDevicesStat.FormattingEnabled = true;
			this.CBDevicesStat.Location = new System.Drawing.Point(284, 4);
			this.CBDevicesStat.Name = "CBDevicesStat";
			this.CBDevicesStat.Size = new System.Drawing.Size(100, 21);
			this.CBDevicesStat.TabIndex = 53;
			this.CBDevicesStat.SelectedIndexChanged += new System.EventHandler(this.CBDevices_SelectedIndexChanged);
			// 
			// LStatExchan
			// 
			this.LStatExchan.BackColor = System.Drawing.Color.Transparent;
			this.LStatExchan.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.LStatExchan.Location = new System.Drawing.Point(252, 4);
			this.LStatExchan.Name = "LStatExchan";
			this.LStatExchan.Size = new System.Drawing.Size(32, 21);
			this.LStatExchan.TabIndex = 52;
			this.LStatExchan.Text = "100";
			this.LStatExchan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(140, 6);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(106, 13);
			this.label9.TabIndex = 51;
			this.label9.Text = "Статистика обмена";
			// 
			// CBShowInd
			// 
			this.CBShowInd.AutoSize = true;
			this.CBShowInd.Checked = true;
			this.CBShowInd.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CBShowInd.Location = new System.Drawing.Point(838, 152);
			this.CBShowInd.Name = "CBShowInd";
			this.CBShowInd.Size = new System.Drawing.Size(116, 17);
			this.CBShowInd.TabIndex = 47;
			this.CBShowInd.Text = "Вывод индикации";
			this.CBShowInd.UseVisualStyleBackColor = true;
			// 
			// CBReadArch
			// 
			this.CBReadArch.AutoSize = true;
			this.CBReadArch.Checked = true;
			this.CBReadArch.CheckState = System.Windows.Forms.CheckState.Checked;
			this.CBReadArch.Location = new System.Drawing.Point(552, 152);
			this.CBReadArch.Name = "CBReadArch";
			this.CBReadArch.Size = new System.Drawing.Size(102, 17);
			this.CBReadArch.TabIndex = 44;
			this.CBReadArch.Text = "Читать архивы";
			this.CBReadArch.UseVisualStyleBackColor = true;
			// 
			// CBIndArchOff
			// 
			this.CBIndArchOff.AutoSize = true;
			this.CBIndArchOff.Location = new System.Drawing.Point(690, 152);
			this.CBIndArchOff.Name = "CBIndArchOff";
			this.CBIndArchOff.Size = new System.Drawing.Size(128, 17);
			this.CBIndArchOff.TabIndex = 43;
			this.CBIndArchOff.Text = "Индик. архива откл.";
			this.CBIndArchOff.UseVisualStyleBackColor = true;
			// 
			// PParameters
			// 
			this.PParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PParameters.AutoScroll = true;
			this.PParameters.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PParameters.Location = new System.Drawing.Point(80, 172);
			this.PParameters.Name = "PParameters";
			this.PParameters.Size = new System.Drawing.Size(878, 553);
			this.PParameters.TabIndex = 0;
			// 
			// BClrErr
			// 
			this.BClrErr.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BClrErr.Location = new System.Drawing.Point(708, 3);
			this.BClrErr.Name = "BClrErr";
			this.BClrErr.Size = new System.Drawing.Size(98, 23);
			this.BClrErr.TabIndex = 29;
			this.BClrErr.Text = "Сброс ошибок";
			this.BClrErr.UseVisualStyleBackColor = true;
			this.BClrErr.Click += new System.EventHandler(this.BClrErr_Click);
			// 
			// LNumErr
			// 
			this.LNumErr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.LNumErr.Location = new System.Drawing.Point(809, 4);
			this.LNumErr.Name = "LNumErr";
			this.LNumErr.Size = new System.Drawing.Size(39, 21);
			this.LNumErr.TabIndex = 28;
			this.LNumErr.Text = "_____";
			this.LNumErr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// CBErrors
			// 
			this.CBErrors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CBErrors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBErrors.FormattingEnabled = true;
			this.CBErrors.Location = new System.Drawing.Point(140, 30);
			this.CBErrors.MaxDropDownItems = 24;
			this.CBErrors.Name = "CBErrors";
			this.CBErrors.Size = new System.Drawing.Size(811, 21);
			this.CBErrors.TabIndex = 27;
			this.toolTip1.SetToolTip(this.CBErrors, "e3e3");
			this.CBErrors.MouseEnter += new System.EventHandler(this.CBErrors_MouseEnter);
			// 
			// RBScan
			// 
			this.RBScan.AutoSize = true;
			this.RBScan.Location = new System.Drawing.Point(10, 238);
			this.RBScan.Name = "RBScan";
			this.RBScan.Size = new System.Drawing.Size(57, 17);
			this.RBScan.TabIndex = 26;
			this.RBScan.TabStop = true;
			this.RBScan.Text = "Опрос";
			this.RBScan.UseVisualStyleBackColor = true;
			// 
			// BStartScan
			// 
			this.BStartScan.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BStartScan.Image = global::TestDRVtransGas.Properties.Resources.Spell_p8;
			this.BStartScan.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.BStartScan.Location = new System.Drawing.Point(4, 182);
			this.BStartScan.Name = "BStartScan";
			this.BStartScan.Size = new System.Drawing.Size(70, 50);
			this.BStartScan.TabIndex = 25;
			this.BStartScan.Text = "Пуск";
			this.BStartScan.UseVisualStyleBackColor = true;
			this.BStartScan.Click += new System.EventHandler(this.BStartScan_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(4, 135);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(121, 18);
			this.label1.TabIndex = 24;
			this.label1.Text = "Интервал опроса, мс";
			// 
			// NIntervalScan
			// 
			this.NIntervalScan.Increment = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.NIntervalScan.Location = new System.Drawing.Point(4, 156);
			this.NIntervalScan.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
			this.NIntervalScan.Minimum = new decimal(new int[] {
            40,
            0,
            0,
            0});
			this.NIntervalScan.Name = "NIntervalScan";
			this.NIntervalScan.Size = new System.Drawing.Size(61, 20);
			this.NIntervalScan.TabIndex = 23;
			this.NIntervalScan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.NIntervalScan.Value = new decimal(new int[] {
            400,
            0,
            0,
            0});
			this.NIntervalScan.ValueChanged += new System.EventHandler(this.NIntervalScan_ValueChanged);
			// 
			// BGetErr
			// 
			this.BGetErr.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BGetErr.Location = new System.Drawing.Point(853, 3);
			this.BGetErr.Name = "BGetErr";
			this.BGetErr.Size = new System.Drawing.Size(98, 23);
			this.BGetErr.TabIndex = 21;
			this.BGetErr.Text = "Запрос ошибок";
			this.BGetErr.UseVisualStyleBackColor = true;
			this.BGetErr.Click += new System.EventHandler(this.BGetErr_Click);
			// 
			// BStopDrv
			// 
			this.BStopDrv.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BStopDrv.Image = ((System.Drawing.Image)(resources.GetObject("BStopDrv.Image")));
			this.BStopDrv.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.BStopDrv.Location = new System.Drawing.Point(4, 84);
			this.BStopDrv.Name = "BStopDrv";
			this.BStopDrv.Size = new System.Drawing.Size(116, 44);
			this.BStopDrv.TabIndex = 20;
			this.BStopDrv.Text = "Стоп перводвигатель";
			this.BStopDrv.UseVisualStyleBackColor = true;
			this.BStopDrv.Click += new System.EventHandler(this.BStopDrv_Click);
			// 
			// BClose
			// 
			this.BClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.BClose.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BClose.Image = global::TestDRVtransGas.Properties.Resources.ExitTranspar;
			this.BClose.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.BClose.Location = new System.Drawing.Point(4, 281);
			this.BClose.Name = "BClose";
			this.BClose.Size = new System.Drawing.Size(70, 436);
			this.BClose.TabIndex = 19;
			this.BClose.Text = "Выход";
			this.BClose.UseCompatibleTextRendering = true;
			this.BClose.UseVisualStyleBackColor = true;
			this.BClose.Click += new System.EventHandler(this.BClose_Click);
			// 
			// BTest1
			// 
			this.BTest1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BTest1.Image = global::TestDRVtransGas.Properties.Resources.Gris_GlowV1_5_Icon_19_p7;
			this.BTest1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.BTest1.Location = new System.Drawing.Point(4, 6);
			this.BTest1.Name = "BTest1";
			this.BTest1.Size = new System.Drawing.Size(116, 45);
			this.BTest1.TabIndex = 18;
			this.BTest1.Text = "Старт\r\nперводвигатель";
			this.BTest1.UseVisualStyleBackColor = true;
			this.BTest1.Click += new System.EventHandler(this.BTest1_Click);
			// 
			// TPDevices
			// 
			this.TPDevices.AutoScroll = true;
			this.TPDevices.Controls.Add(this.BSaveDevices);
			this.TPDevices.Location = new System.Drawing.Point(23, 4);
			this.TPDevices.Name = "TPDevices";
			this.TPDevices.Padding = new System.Windows.Forms.Padding(3);
			this.TPDevices.Size = new System.Drawing.Size(959, 725);
			this.TPDevices.TabIndex = 1;
			this.TPDevices.Text = "Приборы";
			this.TPDevices.UseVisualStyleBackColor = true;
			// 
			// BSaveDevices
			// 
			this.BSaveDevices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BSaveDevices.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BSaveDevices.Location = new System.Drawing.Point(573, 705);
			this.BSaveDevices.Name = "BSaveDevices";
			this.BSaveDevices.Size = new System.Drawing.Size(123, 23);
			this.BSaveDevices.TabIndex = 0;
			this.BSaveDevices.Text = "Сохранить";
			this.BSaveDevices.UseVisualStyleBackColor = true;
			this.BSaveDevices.Click += new System.EventHandler(this.BSaveDevices_Click);
			// 
			// TPParams
			// 
			this.TPParams.AutoScroll = true;
			this.TPParams.Controls.Add(this.BSaveParams);
			this.TPParams.Location = new System.Drawing.Point(23, 4);
			this.TPParams.Name = "TPParams";
			this.TPParams.Padding = new System.Windows.Forms.Padding(3);
			this.TPParams.Size = new System.Drawing.Size(959, 725);
			this.TPParams.TabIndex = 2;
			this.TPParams.Text = "ПАРАМЕТРЫ";
			this.TPParams.UseVisualStyleBackColor = true;
			// 
			// BSaveParams
			// 
			this.BSaveParams.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BSaveParams.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BSaveParams.Location = new System.Drawing.Point(570, 705);
			this.BSaveParams.Name = "BSaveParams";
			this.BSaveParams.Size = new System.Drawing.Size(123, 23);
			this.BSaveParams.TabIndex = 0;
			this.BSaveParams.Text = "Сохранить";
			this.BSaveParams.UseVisualStyleBackColor = true;
			this.BSaveParams.Click += new System.EventHandler(this.BSaveParams_Click);
			// 
			// TTest
			// 
			this.TTest.BackColor = System.Drawing.Color.Chocolate;
			this.TTest.Controls.Add(this.RBWriting);
			this.TTest.Controls.Add(this.NUDIDstart);
			this.TTest.Controls.Add(this.label14);
			this.TTest.Controls.Add(this.CBDevsWr);
			this.TTest.Controls.Add(this.label12);
			this.TTest.Controls.Add(this.NUDIntervalWr);
			this.TTest.Controls.Add(this.label11);
			this.TTest.Controls.Add(this.TBAddresses);
			this.TTest.Controls.Add(this.BStartWr);
			this.TTest.Controls.Add(this.BFindVal);
			this.TTest.Cursor = System.Windows.Forms.Cursors.Hand;
			this.TTest.Location = new System.Drawing.Point(23, 4);
			this.TTest.Name = "TTest";
			this.TTest.Padding = new System.Windows.Forms.Padding(3);
			this.TTest.Size = new System.Drawing.Size(959, 725);
			this.TTest.TabIndex = 3;
			this.TTest.Text = "Тест";
			// 
			// RBWriting
			// 
			this.RBWriting.AutoSize = true;
			this.RBWriting.Location = new System.Drawing.Point(32, 104);
			this.RBWriting.Name = "RBWriting";
			this.RBWriting.Size = new System.Drawing.Size(66, 17);
			this.RBWriting.TabIndex = 67;
			this.RBWriting.TabStop = true;
			this.RBWriting.Text = "Стоим-с";
			this.RBWriting.UseVisualStyleBackColor = true;
			// 
			// NUDIDstart
			// 
			this.NUDIDstart.Location = new System.Drawing.Point(140, 184);
			this.NUDIDstart.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.NUDIDstart.Name = "NUDIDstart";
			this.NUDIDstart.Size = new System.Drawing.Size(68, 20);
			this.NUDIDstart.TabIndex = 66;
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
			this.label14.Location = new System.Drawing.Point(140, 168);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(41, 13);
			this.label14.TabIndex = 65;
			this.label14.Text = "ID start";
			// 
			// CBDevsWr
			// 
			this.CBDevsWr.FormattingEnabled = true;
			this.CBDevsWr.Location = new System.Drawing.Point(220, 60);
			this.CBDevsWr.Name = "CBDevsWr";
			this.CBDevsWr.Size = new System.Drawing.Size(95, 21);
			this.CBDevsWr.TabIndex = 64;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(28, 152);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(76, 32);
			this.label12.TabIndex = 63;
			this.label12.Text = "Интервал записи, сек";
			// 
			// NUDIntervalWr
			// 
			this.NUDIntervalWr.Location = new System.Drawing.Point(28, 184);
			this.NUDIntervalWr.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
			this.NUDIntervalWr.Name = "NUDIntervalWr";
			this.NUDIntervalWr.Size = new System.Drawing.Size(76, 20);
			this.NUDIntervalWr.TabIndex = 62;
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
			this.label11.Location = new System.Drawing.Point(152, 40);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(44, 13);
			this.label11.TabIndex = 61;
			this.label11.Text = "Адреса";
			// 
			// TBAddresses
			// 
			this.TBAddresses.Location = new System.Drawing.Point(148, 60);
			this.TBAddresses.Multiline = true;
			this.TBAddresses.Name = "TBAddresses";
			this.TBAddresses.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TBAddresses.Size = new System.Drawing.Size(60, 100);
			this.TBAddresses.TabIndex = 60;
			this.TBAddresses.Text = "5007\r\n5009\r\n5013\r\n481\r\n5003\r\n5011";
			this.TBAddresses.WordWrap = false;
			// 
			// BStartWr
			// 
			this.BStartWr.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BStartWr.Location = new System.Drawing.Point(28, 60);
			this.BStartWr.Name = "BStartWr";
			this.BStartWr.Size = new System.Drawing.Size(108, 31);
			this.BStartWr.TabIndex = 59;
			this.BStartWr.Text = "Старт записи";
			this.BStartWr.UseVisualStyleBackColor = true;
			this.BStartWr.Click += new System.EventHandler(this.BStartWr_Click);
			// 
			// BFindVal
			// 
			this.BFindVal.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BFindVal.Location = new System.Drawing.Point(460, 60);
			this.BFindVal.Name = "BFindVal";
			this.BFindVal.Size = new System.Drawing.Size(75, 23);
			this.BFindVal.TabIndex = 58;
			this.BFindVal.Text = "FindVal";
			this.BFindVal.UseVisualStyleBackColor = true;
			this.BFindVal.Click += new System.EventHandler(this.BTest_Click);
			// 
			// toolTip1
			// 
			this.toolTip1.AutoPopDelay = 35000;
			this.toolTip1.InitialDelay = 500;
			this.toolTip1.ReshowDelay = 100;
			this.toolTip1.ShowAlways = true;
			// 
			// FTestDrivers
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(986, 733);
			this.Controls.Add(this.tabControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(400, 350);
			this.Name = "FTestDrivers";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Тест перводвигателей (драйверов)";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FTestDrivers_FormClosing);
			this.Move += new System.EventHandler(this.FTestDrivers_Move);
			this.Resize += new System.EventHandler(this.FTestDrivers_Resize);
			this.tabControl1.ResumeLayout(false);
			this.TPParametersScan.ResumeLayout(false);
			this.TPParametersScan.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.NTimeResp)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.NIntervalScan)).EndInit();
			this.TPDevices.ResumeLayout(false);
			this.TPParams.ResumeLayout(false);
			this.TTest.ResumeLayout(false);
			this.TTest.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.NUDIDstart)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NUDIntervalWr)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

				private System.Windows.Forms.Timer TScanParam;
				private System.Windows.Forms.TabControl tabControl1;
				private System.Windows.Forms.TabPage TPParametersScan;
				private System.Windows.Forms.Button BClrErr;
				private System.Windows.Forms.Label LNumErr;
				private System.Windows.Forms.ComboBox CBErrors;
				private System.Windows.Forms.RadioButton RBScan;
				private System.Windows.Forms.Button BStartScan;
				private System.Windows.Forms.Label label1;
				private System.Windows.Forms.NumericUpDown NIntervalScan;
				private System.Windows.Forms.Button BGetErr;
				private System.Windows.Forms.Button BStopDrv;
				private System.Windows.Forms.Button BClose;
				private System.Windows.Forms.Button BTest1;
				private System.Windows.Forms.TabPage TPDevices;
				private System.Windows.Forms.TabPage TPParams;
				private System.Windows.Forms.Button BSaveDevices;
				private System.Windows.Forms.Button BSaveParams;
				private System.Windows.Forms.Panel PParameters;
				private System.Windows.Forms.CheckBox CBIndArchOff;
				private System.Windows.Forms.CheckBox CBReadArch;
		private System.Windows.Forms.CheckBox CBShowInd;
		private System.Windows.Forms.Label LStatExchan;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.ComboBox CBDevicesStat;
    private System.Windows.Forms.CheckBox CBAccumArch;
    private System.Windows.Forms.TabPage TTest;
    private System.Windows.Forms.Button BFindVal;
    public System.Windows.Forms.ToolTip toolTip1;
    private System.Windows.Forms.Label LPuskTime;
    private System.Windows.Forms.Label LWorkTime;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.NumericUpDown NUDIntervalWr;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox TBAddresses;
		private System.Windows.Forms.Button BStartWr;
		private System.Windows.Forms.ComboBox CBDevsWr;
		private System.Windows.Forms.NumericUpDown NUDIDstart;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.RadioButton RBWriting;
		private System.Windows.Forms.ComboBox CBDeviceWr;
		private System.Windows.Forms.NumericUpDown NTimeResp;
		private System.Windows.Forms.ComboBox CBData;
		private System.Windows.Forms.ComboBox CBParameterName;
		private System.Windows.Forms.ComboBox CBDataWritePar;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.ComboBox CBTypeData;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox TBIDwritePar;
		private System.Windows.Forms.Button BWritePar;
		private System.Windows.Forms.ComboBox CBStateLog;
		private System.Windows.Forms.Button BSwithLog;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button BSetFirstDateArchs;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label16;
		public System.Windows.Forms.DateTimePicker TPAlarm;
		public System.Windows.Forms.DateTimePicker TPInterfer;
		private System.Windows.Forms.Label label15;
		public System.Windows.Forms.DateTimePicker TPArchHour;
		private System.Windows.Forms.Label label13;
		public System.Windows.Forms.DateTimePicker TPArchDay;
	}
}

