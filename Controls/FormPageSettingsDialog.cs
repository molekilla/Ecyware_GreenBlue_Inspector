using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Summary description for FormPageSettingsDialog.
	/// </summary>
	public class FormPageSettingsDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtAction;
		private System.Windows.Forms.ComboBox cmbEnctype;
		private System.Windows.Forms.ComboBox cmbMethod;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormPageSettingsDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormPageSettingsDialog));
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.label10 = new System.Windows.Forms.Label();
			this.txtAction = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.cmbEnctype = new System.Windows.Forms.ComboBox();
			this.cmbMethod = new System.Windows.Forms.ComboBox();
			this.groupBox5.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.cmbMethod);
			this.groupBox5.Controls.Add(this.cmbEnctype);
			this.groupBox5.Controls.Add(this.label10);
			this.groupBox5.Controls.Add(this.txtAction);
			this.groupBox5.Controls.Add(this.label8);
			this.groupBox5.Controls.Add(this.label9);
			this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox5.Location = new System.Drawing.Point(9, 3);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(354, 108);
			this.groupBox5.TabIndex = 24;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Form Properties";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(12, 72);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(100, 18);
			this.label10.TabIndex = 13;
			this.label10.Text = "Method";
			// 
			// txtAction
			// 
			this.txtAction.Location = new System.Drawing.Point(138, 48);
			this.txtAction.Name = "txtAction";
			this.txtAction.Size = new System.Drawing.Size(210, 20);
			this.txtAction.TabIndex = 3;
			this.txtAction.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(12, 49);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100, 18);
			this.label8.TabIndex = 2;
			this.label8.Text = "Action";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(12, 26);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(120, 18);
			this.label9.TabIndex = 1;
			this.label9.Text = "Enctype";
			// 
			// btnOK
			// 
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(204, 120);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 23;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(288, 120);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 21;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// cmbEnctype
			// 
			this.cmbEnctype.Items.AddRange(new object[] {
															"application/x-www-form-urlencoded",
															"multipart/form-data"});
			this.cmbEnctype.Location = new System.Drawing.Point(138, 24);
			this.cmbEnctype.Name = "cmbEnctype";
			this.cmbEnctype.Size = new System.Drawing.Size(210, 21);
			this.cmbEnctype.TabIndex = 17;
			// 
			// cmbMethod
			// 
			this.cmbMethod.Items.AddRange(new object[] {
														   "GET",
														   "POST"});
			this.cmbMethod.Location = new System.Drawing.Point(138, 72);
			this.cmbMethod.Name = "cmbMethod";
			this.cmbMethod.Size = new System.Drawing.Size(210, 21);
			this.cmbMethod.TabIndex = 18;
			// 
			// FormPageSettingsDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(382, 158);
			this.Controls.Add(this.groupBox5);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormPageSettingsDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Form Properties Dialogs";
			this.groupBox5.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnApply_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}


		/// <summary>
		/// Gets the enctype.
		/// </summary>
		public string Enctype
		{
			get
			{
				return this.cmbEnctype.Text;
			}
			set
			{
				this.cmbEnctype.Text = value;
			}
		}

		/// <summary>
		/// Gets the method.
		/// </summary>
		public string Method
		{
			get
			{
				return this.cmbMethod.Text;
			}
			set
			{
				this.cmbMethod.Text = value;
			}
		}

		/// <summary>
		/// Gets the action.
		/// </summary>
		public string Action
		{
			get
			{
				return this.txtAction.Text;
			}
			set
			{
				txtAction.Text = value;
			}
		}
	}
}
