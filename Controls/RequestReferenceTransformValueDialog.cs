using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Summary description for RequestReferenceTransformValueDialog.
	/// </summary>
	public class RequestReferenceTransformValueDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.ComboBox comboBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public RequestReferenceTransformValueDialog()
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
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.comboBox2 = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// comboBox1
			// 
			this.comboBox1.Location = new System.Drawing.Point(162, 78);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(121, 21);
			this.comboBox1.TabIndex = 0;
			this.comboBox1.Text = "comboBox1";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(162, 108);
			this.textBox1.Name = "textBox1";
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "textBox1";
			// 
			// comboBox2
			// 
			this.comboBox2.Location = new System.Drawing.Point(162, 48);
			this.comboBox2.Name = "comboBox2";
			this.comboBox2.Size = new System.Drawing.Size(121, 21);
			this.comboBox2.TabIndex = 2;
			this.comboBox2.Text = "comboBox2";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(18, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 23);
			this.label1.TabIndex = 3;
			this.label1.Text = "Web Request Name";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(18, 78);
			this.label2.Name = "label2";
			this.label2.TabIndex = 4;
			this.label2.Text = "Member";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(18, 108);
			this.label3.Name = "label3";
			this.label3.TabIndex = 5;
			this.label3.Text = "Collection Name";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(18, 12);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(480, 23);
			this.label4.TabIndex = 6;
			this.label4.Text = "Select a value from a web request to map to the current transform value.";
			// 
			// RequestReferenceTransformValueDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(550, 206);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.comboBox2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.comboBox1);
			this.Name = "RequestReferenceTransformValueDialog";
			this.Text = "Web Request Value Mapping Dialog";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
