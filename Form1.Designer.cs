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
			this.CBStateLog = new System.Windows.Forms.ComboBox();
			this.BSwithLog = new System.Windows.Forms.Button();
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
			this.dSDevices = new TestDRVtransGas.DSDevices();
			this.dSDevicesBindingSource = new System.Windows.Forms.BindingSource(this.components);
			label2 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.TPParametersScan.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.NIntervalScan)).BeginInit();
			this.TPDevices.SuspendLayout();
			this.TPParams.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dSDevices)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dSDevicesBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// label2
			// 
			label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			label2.Location = new System.Drawing.Point(159, 48);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(180, 21);
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
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Multiline = true;
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(794, 738);
			this.tabControl1.TabIndex = 0;
			// 
			// TPParametersScan
			// 
			this.TPParametersScan.Controls.Add(this.CBStateLog);
			this.TPParametersScan.Controls.Add(this.BSwithLog);
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
			this.TPParametersScan.Size = new System.Drawing.Size(767, 730);
			this.TPParametersScan.TabIndex = 0;
			this.TPParametersScan.Text = "Опрос параметров";
			this.TPParametersScan.UseVisualStyleBackColor = true;
			// 
			// CBStateLog
			// 
			this.CBStateLog.FormattingEnabled = true;
			this.CBStateLog.Items.AddRange(new object[] {
            "BelTranzDRV.log",
            "Mem",
            "NONE"});
			this.CBStateLog.Location = new System.Drawing.Point(654, 46);
			this.CBStateLog.Name = "CBStateLog";
			this.CBStateLog.Size = new System.Drawing.Size(105, 21);
			this.CBStateLog.TabIndex = 32;
			// 
			// BSwithLog
			// 
			this.BSwithLog.Location = new System.Drawing.Point(504, 46);
			this.BSwithLog.Name = "BSwithLog";
			this.BSwithLog.Size = new System.Drawing.Size(126, 23);
			this.BSwithLog.TabIndex = 31;
			this.BSwithLog.Text = "Включить Log";
			this.BSwithLog.UseVisualStyleBackColor = true;
			this.BSwithLog.Click += new System.EventHandler(this.BSwithLog_Click);
			// 
			// PParameters
			// 
			this.PParameters.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PParameters.AutoScroll = true;
			this.PParameters.Location = new System.Drawing.Point(78, 66);
			this.PParameters.Name = "PParameters";
			this.PParameters.Size = new System.Drawing.Size(689, 666);
			this.PParameters.TabIndex = 0;
			// 
			// BClrErr
			// 
			this.BClrErr.Location = new System.Drawing.Point(343, 0);
			this.BClrErr.Name = "BClrErr";
			this.BClrErr.Size = new System.Drawing.Size(98, 23);
			this.BClrErr.TabIndex = 29;
			this.BClrErr.Text = "Сброс ошибок";
			this.BClrErr.UseVisualStyleBackColor = true;
			this.BClrErr.Click += new System.EventHandler(this.BClrErr_Click);
			// 
			// LNumErr
			// 
			this.LNumErr.AutoSize = true;
			this.LNumErr.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.LNumErr.Location = new System.Drawing.Point(468, 3);
			this.LNumErr.Name = "LNumErr";
			this.LNumErr.Size = new System.Drawing.Size(39, 15);
			this.LNumErr.TabIndex = 28;
			this.LNumErr.Text = "_____";
			// 
			// CBErrors
			// 
			this.CBErrors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CBErrors.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBErrors.FormattingEnabled = true;
			this.CBErrors.Location = new System.Drawing.Point(141, 24);
			this.CBErrors.MaxDropDownItems = 24;
			this.CBErrors.Name = "CBErrors";
			this.CBErrors.Size = new System.Drawing.Size(618, 21);
			this.CBErrors.TabIndex = 27;
			// 
			// RBScan
			// 
			this.RBScan.AutoSize = true;
			this.RBScan.Location = new System.Drawing.Point(4, 236);
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
			this.BStartScan.Image = global::TestDRVtransGas.Properties.Resources.System_Control_Panel_p7;
			this.BStartScan.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.BStartScan.Location = new System.Drawing.Point(4, 122);
			this.BStartScan.Name = "BStartScan";
			this.BStartScan.Size = new System.Drawing.Size(61, 111);
			this.BStartScan.TabIndex = 25;
			this.BStartScan.Text = "Пуск";
			this.BStartScan.UseVisualStyleBackColor = true;
			this.BStartScan.Click += new System.EventHandler(this.BStartScan_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(5, 61);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 28);
			this.label1.TabIndex = 24;
			this.label1.Text = "Интервал опроса, мс";
			// 
			// NIntervalScan
			// 
			this.NIntervalScan.Increment = new decimal(new int[] {
            20,
            0,
            0,
            0});
			this.NIntervalScan.Location = new System.Drawing.Point(4, 96);
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
			this.BGetErr.Location = new System.Drawing.Point(518, 0);
			this.BGetErr.Name = "BGetErr";
			this.BGetErr.Size = new System.Drawing.Size(126, 23);
			this.BGetErr.TabIndex = 21;
			this.BGetErr.Text = "Запрос ошибок";
			this.BGetErr.UseVisualStyleBackColor = true;
			this.BGetErr.Click += new System.EventHandler(this.BGetErr_Click);
			// 
			// BStopDrv
			// 
			this.BStopDrv.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BStopDrv.Image = global::TestDRVtransGas.Properties.Resources.Core_IP_theme_Icon_19_p8;
			this.BStopDrv.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BStopDrv.Location = new System.Drawing.Point(141, 0);
			this.BStopDrv.Name = "BStopDrv";
			this.BStopDrv.Size = new System.Drawing.Size(147, 23);
			this.BStopDrv.TabIndex = 20;
			this.BStopDrv.Text = "Стоп перводвигатель";
			this.BStopDrv.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BStopDrv.UseVisualStyleBackColor = true;
			this.BStopDrv.Click += new System.EventHandler(this.BStopDrv_Click);
			// 
			// BClose
			// 
			this.BClose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.BClose.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BClose.Image = global::TestDRVtransGas.Properties.Resources.Log_Off_p12;
			this.BClose.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.BClose.Location = new System.Drawing.Point(4, 282);
			this.BClose.Name = "BClose";
			this.BClose.Size = new System.Drawing.Size(62, 413);
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
			this.BTest1.Location = new System.Drawing.Point(3, 0);
			this.BTest1.Name = "BTest1";
			this.BTest1.Size = new System.Drawing.Size(133, 45);
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
			this.TPDevices.Size = new System.Drawing.Size(767, 730);
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
			this.TPParams.Size = new System.Drawing.Size(767, 730);
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
			// dSDevices
			// 
			this.dSDevices.DataSetName = "DSDevices";
			this.dSDevices.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
			// 
			// dSDevicesBindingSource
			// 
			this.dSDevicesBindingSource.AllowNew = true;
			this.dSDevicesBindingSource.DataSource = this.dSDevices;
			this.dSDevicesBindingSource.Position = 0;
			// 
			// FTestDrivers
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(794, 738);
			this.Controls.Add(this.tabControl1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(400, 350);
			this.Name = "FTestDrivers";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Тест перводвигателей (драйверов)";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FTestDrivers_FormClosing);
			this.Resize += new System.EventHandler(this.FTestDrivers_Resize);
			this.tabControl1.ResumeLayout(false);
			this.TPParametersScan.ResumeLayout(false);
			this.TPParametersScan.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.NIntervalScan)).EndInit();
			this.TPDevices.ResumeLayout(false);
			this.TPParams.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dSDevices)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dSDevicesBindingSource)).EndInit();
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
				private System.Windows.Forms.BindingSource dSDevicesBindingSource;
				private System.Windows.Forms.TabPage TPParams;
				public DSDevices dSDevices;
				private System.Windows.Forms.Button BSaveDevices;
				private System.Windows.Forms.Button BSaveParams;
				private System.Windows.Forms.Panel PParameters;
				private System.Windows.Forms.Button BSwithLog;
				private System.Windows.Forms.ComboBox CBStateLog;
    }
}

