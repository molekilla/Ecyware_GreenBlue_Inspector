using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	internal enum ReportDialogOption
	{
		HTML,
		XML
	}
	/// <summary>
	/// Summary description for ReportDialog.
	/// </summary>
	internal class ReportDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rbHTML;
		private System.Windows.Forms.RadioButton rbXML;
		private System.Windows.Forms.Button btnPrint;
		private System.Windows.Forms.Button btnCancel;

		private ReportDialogOption _selectedReportType;
		private System.Windows.Forms.ComboBox cmbReportFormatType;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ReportDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.cmbReportFormatType.SelectedIndex = 0;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ReportDialog));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cmbReportFormatType = new System.Windows.Forms.ComboBox();
			this.rbXML = new System.Windows.Forms.RadioButton();
			this.rbHTML = new System.Windows.Forms.RadioButton();
			this.btnPrint = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cmbReportFormatType);
			this.groupBox1.Controls.Add(this.rbXML);
			this.groupBox1.Controls.Add(this.rbHTML);
			this.groupBox1.Location = new System.Drawing.Point(6, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(402, 84);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Select a report type";
			// 
			// cmbReportFormatType
			// 
			this.cmbReportFormatType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbReportFormatType.Items.AddRange(new object[] {
																	 "Basic Report",
																	 "Advanced Report"});
			this.cmbReportFormatType.Location = new System.Drawing.Point(144, 21);
			this.cmbReportFormatType.Name = "cmbReportFormatType";
			this.cmbReportFormatType.Size = new System.Drawing.Size(252, 21);
			this.cmbReportFormatType.TabIndex = 3;
			// 
			// rbXML
			// 
			this.rbXML.Location = new System.Drawing.Point(30, 48);
			this.rbXML.Name = "rbXML";
			this.rbXML.TabIndex = 1;
			this.rbXML.Text = "XML";
			this.rbXML.Click += new System.EventHandler(this.rbXML_Click);
			// 
			// rbHTML
			// 
			this.rbHTML.Checked = true;
			this.rbHTML.Location = new System.Drawing.Point(30, 18);
			this.rbHTML.Name = "rbHTML";
			this.rbHTML.TabIndex = 0;
			this.rbHTML.TabStop = true;
			this.rbHTML.Text = "HTML";
			this.rbHTML.Click += new System.EventHandler(this.rbHTML_Click);
			// 
			// btnPrint
			// 
			this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnPrint.Location = new System.Drawing.Point(246, 96);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.TabIndex = 1;
			this.btnPrint.Text = "&OK";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(330, 96);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "&Cancel";
			// 
			// ReportDialog
			// 
			this.AcceptButton = this.btnPrint;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(412, 122);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ReportDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Report Preview";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		internal ReportDialogOption SelectedReportType
		{
			get
			{
				return _selectedReportType;
			}
			set
			{
				_selectedReportType = value;
			}
		}

		public string ReportFormatType
		{
			get
			{
				return this.cmbReportFormatType.Text;
			}
		}
		private void rbHTML_Click(object sender, System.EventArgs e)
		{
			_selectedReportType = ReportDialogOption.HTML;
			this.cmbReportFormatType.Enabled = true;
		}

		private void rbXML_Click(object sender, System.EventArgs e)
		{
			_selectedReportType = ReportDialogOption.XML;
			this.cmbReportFormatType.Enabled = false;
		}

		private void btnPrint_Click(object sender, System.EventArgs e)
		{
		
		}
	}
}
