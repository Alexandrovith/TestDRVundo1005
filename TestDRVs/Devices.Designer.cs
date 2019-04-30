namespace TestDRVtransGas
{
	partial class TDevices
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			this.GV = new System.Windows.Forms.DataGridView();
			this.NameDev = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Type = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.HostAddressUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.PortTCP = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.HostAddressGroupp = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.BaudRate = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Port = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Pass1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Pass2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ModeConnOfDev = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.GV)).BeginInit();
			this.SuspendLayout();
			// 
			// GV
			// 
			this.GV.BackgroundColor = System.Drawing.Color.DarkOliveGreen;
			this.GV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.GV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.GV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.GV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NameDev,
            this.Type,
            this.Address,
            this.HostAddressUnit,
            this.PortTCP,
            this.HostAddressGroupp,
            this.BaudRate,
            this.Port,
            this.Pass1,
            this.Pass2,
            this.ModeConnOfDev});
			this.GV.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GV.Location = new System.Drawing.Point(0, 0);
			this.GV.Name = "GV";
			this.GV.RowHeadersWidth = 24;
			this.GV.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.GV.Size = new System.Drawing.Size(947, 849);
			this.GV.TabIndex = 1;
			this.GV.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.GVDevices_CellValueChanged);
			// 
			// NameDev
			// 
			this.NameDev.HeaderText = "Name";
			this.NameDev.Name = "NameDev";
			this.NameDev.Width = 70;
			// 
			// Type
			// 
			this.Type.HeaderText = "Type";
			this.Type.Name = "Type";
			this.Type.Width = 70;
			// 
			// Address
			// 
			this.Address.HeaderText = "Addr";
			this.Address.Name = "Address";
			this.Address.Width = 45;
			// 
			// HostAddressUnit
			// 
			this.HostAddressUnit.HeaderText = "UrlSinglePar,ROCgr";
			this.HostAddressUnit.Name = "HostAddressUnit";
			this.HostAddressUnit.Width = 120;
			// 
			// PortTCP
			// 
			this.PortTCP.HeaderText = "PortTCP,HostUn";
			this.PortTCP.Name = "PortTCP";
			this.PortTCP.Width = 90;
			// 
			// HostAddressGroupp
			// 
			this.HostAddressGroupp.HeaderText = "HostGr";
			this.HostAddressGroupp.Name = "HostAddressGroupp";
			this.HostAddressGroupp.Width = 40;
			// 
			// BaudRate
			// 
			this.BaudRate.HeaderText = "BaudRate";
			this.BaudRate.Name = "BaudRate";
			this.BaudRate.Width = 60;
			// 
			// Port
			// 
			this.Port.HeaderText = "Port";
			this.Port.Name = "Port";
			this.Port.Width = 55;
			// 
			// Pass1
			// 
			this.Pass1.HeaderText = "Pass1,DataBits";
			this.Pass1.Name = "Pass1";
			this.Pass1.Width = 80;
			// 
			// Pass2
			// 
			this.Pass2.HeaderText = "Pass2,Parity";
			this.Pass2.Name = "Pass2";
			this.Pass2.Width = 70;
			// 
			// ModeConnOfDev
			// 
			this.ModeConnOfDev.HeaderText = "ModeConnOfDev";
			this.ModeConnOfDev.Name = "ModeConnOfDev";
			// 
			// TDevices
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.Controls.Add(this.GV);
			this.Name = "TDevices";
			this.Size = new System.Drawing.Size(947, 849);
			((System.ComponentModel.ISupportInitialize)(this.GV)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.DataGridView GV;
		private System.Windows.Forms.DataGridViewTextBoxColumn NameDev;
		private System.Windows.Forms.DataGridViewTextBoxColumn Type;
		private System.Windows.Forms.DataGridViewTextBoxColumn Address;
		private System.Windows.Forms.DataGridViewTextBoxColumn HostAddressUnit;
		private System.Windows.Forms.DataGridViewTextBoxColumn PortTCP;
		private System.Windows.Forms.DataGridViewTextBoxColumn HostAddressGroupp;
		private System.Windows.Forms.DataGridViewTextBoxColumn BaudRate;
		private System.Windows.Forms.DataGridViewTextBoxColumn Port;
		private System.Windows.Forms.DataGridViewTextBoxColumn Pass1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Pass2;
		private System.Windows.Forms.DataGridViewTextBoxColumn ModeConnOfDev;
	}
}
