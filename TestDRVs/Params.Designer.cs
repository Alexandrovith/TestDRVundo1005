namespace TestDRVtransGas
{
	partial class TParams
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TParams));
			this.GV = new System.Windows.Forms.DataGridView();
			this.DeviceName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ParamName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.RequestName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.RequestType = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ParameterType = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.MinTimeRequest = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.INorOUT = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.NameP = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.NamePrus = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.FDOpenFl = new System.Windows.Forms.OpenFileDialog();
			this.CBFlDev = new System.Windows.Forms.ComboBox();
			this.BOpenFl = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.GV)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// GV
			// 
			this.GV.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GV.BackgroundColor = System.Drawing.Color.LemonChiffon;
			this.GV.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.GV.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
			dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
			dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
			dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
			dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
			this.GV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
			this.GV.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.GV.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DeviceName,
            this.ParamName,
            this.RequestName,
            this.Data,
            this.RequestType,
            this.ParameterType,
            this.MinTimeRequest,
            this.INorOUT,
            this.NameP,
            this.NamePrus});
			this.GV.Location = new System.Drawing.Point(0, 36);
			this.GV.Name = "GV";
			this.GV.RowHeadersWidth = 24;
			this.GV.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.GV.Size = new System.Drawing.Size(947, 650);
			this.GV.TabIndex = 2;
			this.GV.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.GV_CellValueChanged);
			// 
			// DeviceName
			// 
			this.DeviceName.HeaderText = "DeviceName";
			this.DeviceName.Name = "DeviceName";
			this.DeviceName.Width = 80;
			// 
			// ParamName
			// 
			this.ParamName.HeaderText = "ParamName";
			this.ParamName.Name = "ParamName";
			this.ParamName.Width = 130;
			// 
			// RequestName
			// 
			this.RequestName.HeaderText = "RequestName";
			this.RequestName.Name = "RequestName";
			this.RequestName.Width = 80;
			// 
			// Data
			// 
			this.Data.HeaderText = "Data";
			this.Data.Name = "Data";
			// 
			// RequestType
			// 
			this.RequestType.HeaderText = "RequestType";
			this.RequestType.Name = "RequestType";
			this.RequestType.Width = 80;
			// 
			// ParameterType
			// 
			this.ParameterType.HeaderText = "ParameterType";
			this.ParameterType.Name = "ParameterType";
			this.ParameterType.Width = 88;
			// 
			// MinTimeRequest
			// 
			this.MinTimeRequest.HeaderText = "MinTimeRequest";
			this.MinTimeRequest.Name = "MinTimeRequest";
			this.MinTimeRequest.Width = 90;
			// 
			// INorOUT
			// 
			this.INorOUT.HeaderText = "in/out";
			this.INorOUT.Name = "INorOUT";
			this.INorOUT.Width = 50;
			// 
			// NameP
			// 
			this.NameP.HeaderText = "NameP";
			this.NameP.Name = "NameP";
			this.NameP.Width = 85;
			// 
			// NamePrus
			// 
			this.NamePrus.HeaderText = "NamePrus";
			this.NamePrus.Name = "NamePrus";
			this.NamePrus.Width = 120;
			// 
			// FDOpenFl
			// 
			this.FDOpenFl.FileName = "openFileDialog1";
			// 
			// CBFlDev
			// 
			this.CBFlDev.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CBFlDev.BackColor = System.Drawing.Color.LightGreen;
			this.CBFlDev.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.CBFlDev.Location = new System.Drawing.Point(4, 4);
			this.CBFlDev.Name = "CBFlDev";
			this.CBFlDev.Size = new System.Drawing.Size(891, 21);
			this.CBFlDev.TabIndex = 4;
			this.CBFlDev.SelectedIndexChanged += new System.EventHandler(this.CBFlDev_SelectedIndexChanged);
			// 
			// BOpenFl
			// 
			this.BOpenFl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BOpenFl.Image = ((System.Drawing.Image)(resources.GetObject("BOpenFl.Image")));
			this.BOpenFl.Location = new System.Drawing.Point(895, 3);
			this.BOpenFl.Name = "BOpenFl";
			this.BOpenFl.Size = new System.Drawing.Size(24, 23);
			this.BOpenFl.TabIndex = 5;
			this.BOpenFl.UseVisualStyleBackColor = true;
			this.BOpenFl.Click += new System.EventHandler(this.BOpenFl_Click);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.MediumSeaGreen;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.CBFlDev);
			this.panel1.Controls.Add(this.BOpenFl);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(950, 36);
			this.panel1.TabIndex = 6;
			// 
			// TParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.GV);
			this.Name = "TParams";
			this.Size = new System.Drawing.Size(950, 689);
			((System.ComponentModel.ISupportInitialize)(this.GV)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.DataGridView GV;
		private System.Windows.Forms.OpenFileDialog FDOpenFl;
		private System.Windows.Forms.ComboBox CBFlDev;
		private System.Windows.Forms.Button BOpenFl;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.DataGridViewTextBoxColumn DeviceName;
		private System.Windows.Forms.DataGridViewTextBoxColumn ParamName;
		private System.Windows.Forms.DataGridViewTextBoxColumn RequestName;
		private System.Windows.Forms.DataGridViewTextBoxColumn Data;
		private System.Windows.Forms.DataGridViewTextBoxColumn RequestType;
		private System.Windows.Forms.DataGridViewTextBoxColumn ParameterType;
		private System.Windows.Forms.DataGridViewTextBoxColumn MinTimeRequest;
		private System.Windows.Forms.DataGridViewTextBoxColumn INorOUT;
		private System.Windows.Forms.DataGridViewTextBoxColumn NameP;
		private System.Windows.Forms.DataGridViewTextBoxColumn NamePrus;
	}
}
