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
			this.SuspendLayout();
			// 
			// CTestExch
			// 
			this.Name = "CTestExch";
			this.Size = new System.Drawing.Size(789, 636);
			this.ResumeLayout(false);

		}

		#endregion

		public System.Windows.Forms.NumericUpDown UDCOM;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Button BReadCOM;
		private System.Windows.Forms.Button BDisconn;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label20;
		public System.Windows.Forms.NumericUpDown UDInverShift;
		public System.Windows.Forms.NumericUpDown UDInverNum;
		private System.Windows.Forms.Button BSaveFile;
		private System.Windows.Forms.TextBox TBFile;
		private System.Windows.Forms.CheckBox CBClearOut;
		private System.Windows.Forms.TextBox TBPort;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.ComboBox CBIP;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label23;
		public System.Windows.Forms.ComboBox CBDevice;
		private System.Windows.Forms.Button BRequest;
		public System.Windows.Forms.TextBox TBListExchange;
		public System.Windows.Forms.TextBox TBCommand;
		private System.Windows.Forms.Label label24;
	}
}
