namespace TestDRVtransGas
{
	partial class CWrParams
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			this.GVDataWr = new System.Windows.Forms.DataGridView();
			this.Прибор = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.DataToWrite = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Тип = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.ParamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.TimeResp = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.SequentBites = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.label3 = new System.Windows.Forms.Label();
			this.BClose = new System.Windows.Forms.Button();
			this.BWritePar = new System.Windows.Forms.Button();
			this.NUDIDwritePar = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.NUDInterval = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.GVDataWr)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NUDIDwritePar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.NUDInterval)).BeginInit();
			this.SuspendLayout();
			// 
			// GVDataWr
			// 
			this.GVDataWr.AllowUserToOrderColumns = true;
			this.GVDataWr.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.GVDataWr.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.GVDataWr.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Прибор,
            this.Data,
            this.DataToWrite,
            this.Тип,
            this.ParamName,
            this.TimeResp,
            this.SequentBites});
			this.GVDataWr.Location = new System.Drawing.Point(16, 61);
			this.GVDataWr.Margin = new System.Windows.Forms.Padding(4);
			this.GVDataWr.Name = "GVDataWr";
			this.GVDataWr.Size = new System.Drawing.Size(822, 241);
			this.GVDataWr.TabIndex = 0;
			// 
			// Прибор
			// 
			this.Прибор.HeaderText = "Прибор";
			this.Прибор.Items.AddRange(new object[] {
            "FloBoss407",
            "ERZ2000",
            "ERZ2004",
            "KP2",
            "KP10",
            "SitransCV",
            "Vympel500",
            "OxyIQ",
            "IRGA2",
            "Mgate",
            "ioLogik",
            "MAG",
            "UFGF"});
			this.Прибор.Name = "Прибор";
			this.Прибор.Width = 110;
			// 
			// Data
			// 
			this.Data.HeaderText = "Data";
			this.Data.Name = "Data";
			// 
			// DataToWrite
			// 
			this.DataToWrite.HeaderText = "DataToWrite";
			this.DataToWrite.Name = "DataToWrite";
			this.DataToWrite.Width = 130;
			// 
			// Тип
			// 
			this.Тип.HeaderText = "Тип";
			this.Тип.Items.AddRange(new object[] {
            "float",
            "int",
            "double",
            "string"});
			this.Тип.Name = "Тип";
			// 
			// ParamName
			// 
			this.ParamName.HeaderText = "ParamName";
			this.ParamName.Name = "ParamName";
			this.ParamName.Width = 120;
			// 
			// TimeResp
			// 
			this.TimeResp.DataPropertyName = "1000";
			this.TimeResp.HeaderText = "TimeResp";
			this.TimeResp.Name = "TimeResp";
			this.TimeResp.Width = 80;
			// 
			// SequentBites
			// 
			dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.SequentBites.DefaultCellStyle = dataGridViewCellStyle5;
			this.SequentBites.HeaderText = "Последоват.байт";
			this.SequentBites.Items.AddRange(new object[] {
            "Прямая",
            "Обратная",
            "Обратная_по_2_байта"});
			this.SequentBites.Name = "SequentBites";
			this.SequentBites.Width = 110;
			// 
			// label3
			// 
			this.label3.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label3.Location = new System.Drawing.Point(16, 22);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(43, 24);
			this.label3.TabIndex = 91;
			this.label3.Text = "ID";
			this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// BClose
			// 
			this.BClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BClose.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BClose.Location = new System.Drawing.Point(645, 310);
			this.BClose.Margin = new System.Windows.Forms.Padding(4);
			this.BClose.Name = "BClose";
			this.BClose.Size = new System.Drawing.Size(191, 54);
			this.BClose.TabIndex = 98;
			this.BClose.Text = "Закрыть";
			this.BClose.UseVisualStyleBackColor = true;
			this.BClose.Click += new System.EventHandler(this.BClose_Click);
			// 
			// BWritePar
			// 
			this.BWritePar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BWritePar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.BWritePar.Image = global::TestDRVtransGas.Properties.Resources.hi_fi_5_p05;
			this.BWritePar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.BWritePar.Location = new System.Drawing.Point(267, 310);
			this.BWritePar.Margin = new System.Windows.Forms.Padding(4);
			this.BWritePar.Name = "BWritePar";
			this.BWritePar.Size = new System.Drawing.Size(195, 54);
			this.BWritePar.TabIndex = 99;
			this.BWritePar.Text = "Записать\r\nпараметры";
			this.BWritePar.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.BWritePar.UseVisualStyleBackColor = true;
			this.BWritePar.Click += new System.EventHandler(this.BWritePar_Click);
			// 
			// NUDIDwritePar
			// 
			this.NUDIDwritePar.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.NUDIDwritePar.Location = new System.Drawing.Point(59, 22);
			this.NUDIDwritePar.Margin = new System.Windows.Forms.Padding(4);
			this.NUDIDwritePar.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.NUDIDwritePar.Name = "NUDIDwritePar";
			this.NUDIDwritePar.Size = new System.Drawing.Size(95, 23);
			this.NUDIDwritePar.TabIndex = 101;
			this.NUDIDwritePar.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.NUDIDwritePar.Value = new decimal(new int[] {
            600,
            0,
            0,
            0});
			this.NUDIDwritePar.ValueChanged += new System.EventHandler(this.NUDIDwritePar_ValueChanged);
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label1.Location = new System.Drawing.Point(181, 22);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(202, 24);
			this.label1.TabIndex = 91;
			this.label1.Text = "Интервал между записями";
			this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// NUDInterval
			// 
			this.NUDInterval.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.NUDInterval.Location = new System.Drawing.Point(384, 22);
			this.NUDInterval.Margin = new System.Windows.Forms.Padding(4);
			this.NUDInterval.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
			this.NUDInterval.Name = "NUDInterval";
			this.NUDInterval.Size = new System.Drawing.Size(90, 23);
			this.NUDInterval.TabIndex = 101;
			this.NUDInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// CWrParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(851, 379);
			this.Controls.Add(this.NUDInterval);
			this.Controls.Add(this.NUDIDwritePar);
			this.Controls.Add(this.BWritePar);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.BClose);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.GVDataWr);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "CWrParams";
			this.Text = "Запись набора параметров";
			((System.ComponentModel.ISupportInitialize)(this.GVDataWr)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NUDIDwritePar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.NUDInterval)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView GVDataWr;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button BClose;
		private System.Windows.Forms.Button BWritePar;
		private System.Windows.Forms.NumericUpDown NUDIDwritePar;
		private System.Windows.Forms.DataGridViewComboBoxColumn Прибор;
		private System.Windows.Forms.DataGridViewTextBoxColumn Data;
		private System.Windows.Forms.DataGridViewTextBoxColumn DataToWrite;
		private System.Windows.Forms.DataGridViewComboBoxColumn Тип;
		private System.Windows.Forms.DataGridViewTextBoxColumn ParamName;
		private System.Windows.Forms.DataGridViewTextBoxColumn TimeResp;
		private System.Windows.Forms.DataGridViewComboBoxColumn SequentBites;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown NUDInterval;
	}
}