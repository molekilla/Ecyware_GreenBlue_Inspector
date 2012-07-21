// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: May 2004

using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Contains the ListBoxInputForm use for the Forms Editor.
	/// </summary>
	public class ListBoxInputForm : System.Windows.Forms.Form
	{
		private string _listBoxValue = String.Empty;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TextBox txtValue;
		private System.Windows.Forms.Button btnCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new ListBoxInputForm.
		/// </summary>
		public ListBoxInputForm()
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

		/// <summary>
		/// Gets or sets the listbox value.
		/// </summary>
		public string ListBoxValue
		{
			get
			{
				return _listBoxValue;
			}
			set
			{
				this.txtValue.Text = value;
				_listBoxValue = value;
			}
		}
		#region Windows Form Designer generated code
			/// <summary>
			/// Required method for Designer support - do not modify
			/// the contents of this method with the code editor.
			/// </summary>
			private void InitializeComponent()
		{
				System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ListBoxInputForm));
				this.btnOk = new System.Windows.Forms.Button();
				this.txtValue = new System.Windows.Forms.TextBox();
				this.label1 = new System.Windows.Forms.Label();
				this.btnCancel = new System.Windows.Forms.Button();
				this.SuspendLayout();
				// 
				// btnOk
				// 
				this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
				this.btnOk.Location = new System.Drawing.Point(186, 36);
				this.btnOk.Name = "btnOk";
				this.btnOk.TabIndex = 0;
				this.btnOk.Text = "&OK";
				this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
				// 
				// txtValue
				// 
				this.txtValue.Location = new System.Drawing.Point(99, 12);
				this.txtValue.Name = "txtValue";
				this.txtValue.Size = new System.Drawing.Size(246, 20);
				this.txtValue.TabIndex = 1;
				this.txtValue.Text = "";
				// 
				// label1
				// 
				this.label1.Location = new System.Drawing.Point(18, 12);
				this.label1.Name = "label1";
				this.label1.Size = new System.Drawing.Size(84, 23);
				this.label1.TabIndex = 2;
				this.label1.Text = "Option Value:";
				// 
				// btnCancel
				// 
				this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
				this.btnCancel.Location = new System.Drawing.Point(270, 36);
				this.btnCancel.Name = "btnCancel";
				this.btnCancel.TabIndex = 3;
				this.btnCancel.Text = "&Cancel";
				// 
				// ListBoxInputForm
				// 
				this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
				this.ClientSize = new System.Drawing.Size(360, 64);
				this.Controls.Add(this.txtValue);
				this.Controls.Add(this.btnCancel);
				this.Controls.Add(this.label1);
				this.Controls.Add(this.btnOk);
				this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
				this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
				this.MaximizeBox = false;
				this.MinimizeBox = false;
				this.Name = "ListBoxInputForm";
				this.ShowInTaskbar = false;
				this.Text = "Set Select value";
				this.ResumeLayout(false);

			}
		#endregion

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			this.ListBoxValue=this.txtValue.Text;
		}
	}
}
