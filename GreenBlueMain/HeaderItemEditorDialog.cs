using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Summary description for ListViewItemEditorDialog.
	/// </summary>
	public class HeaderItemEditorDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtValue;
		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.TextBox txtDomain;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HeaderItemEditorDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.txtValue = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.txtDomain = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(66, 12);
			this.label2.TabIndex = 1;
			this.label2.Text = "Value:";
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(78, 8);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(234, 20);
			this.txtName.TabIndex = 2;
			this.txtName.Text = "";
			// 
			// txtValue
			// 
			this.txtValue.Location = new System.Drawing.Point(78, 32);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(234, 20);
			this.txtValue.TabIndex = 3;
			this.txtValue.Text = "";
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(348, 6);
			this.button1.Name = "button1";
			this.button1.TabIndex = 4;
			this.button1.Text = "Save";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(348, 36);
			this.button2.Name = "button2";
			this.button2.TabIndex = 5;
			this.button2.Text = "Cancel";
			// 
			// txtPath
			// 
			this.txtPath.Location = new System.Drawing.Point(78, 78);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(234, 20);
			this.txtPath.TabIndex = 9;
			this.txtPath.Text = "";
			// 
			// txtDomain
			// 
			this.txtDomain.Location = new System.Drawing.Point(78, 54);
			this.txtDomain.Name = "txtDomain";
			this.txtDomain.Size = new System.Drawing.Size(234, 20);
			this.txtDomain.TabIndex = 8;
			this.txtDomain.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 84);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(66, 12);
			this.label3.TabIndex = 7;
			this.label3.Text = "Path:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 60);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(54, 12);
			this.label4.TabIndex = 6;
			this.label4.Text = "Domain:";
			// 
			// HeaderItemEditorDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(430, 116);
			this.Controls.Add(this.txtPath);
			this.Controls.Add(this.txtDomain);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.txtValue);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HeaderItemEditorDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit cookie";
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
		
		}

		public string Path
		{
			get
			{
				return txtPath.Text;
			}
			set
			{
				txtPath.Text = value;
			}
		}
		public string Domain
		{
			get
			{
				return txtDomain.Text;
			}
			set
			{
				txtDomain.Text = value;
			}
		}
		public string HeaderName
		{
			get
			{
				return txtName.Text;
			}
			set
			{
				txtName.Text=value;
			}
		}

		public string Value
		{
			get
			{
				return txtValue.Text;
			}
			set
			{
				txtValue.Text = value;
			}
		}
	}
}
