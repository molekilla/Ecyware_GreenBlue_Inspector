using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace Ecyware.GreenBlue.Controls.Scripting
{
	/// <summary>
	/// Summary description for SetWebTransformTypeDialog.
	/// </summary>
	public class SetWebTransformTypeDialog : System.Windows.Forms.Form
	{
		private string _currentFileName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.RadioButton rbInput;
		private System.Windows.Forms.RadioButton rbOutput;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new SetWebTransformTypeDialog.
		/// </summary>
		public SetWebTransformTypeDialog()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SetWebTransformTypeDialog));
			this.label1 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.rbInput = new System.Windows.Forms.RadioButton();
			this.rbOutput = new System.Windows.Forms.RadioButton();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(18, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(180, 18);
			this.label1.TabIndex = 11;
			this.label1.Text = "Set Web Transform Type";
			// 
			// btnOK
			// 
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(222, 90);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 12;
			this.btnOK.Text = "&Set";
			this.btnOK.Click += new System.EventHandler(this.button1_Click);
			// 
			// rbInput
			// 
			this.rbInput.Checked = true;
			this.rbInput.Location = new System.Drawing.Point(48, 48);
			this.rbInput.Name = "rbInput";
			this.rbInput.TabIndex = 13;
			this.rbInput.TabStop = true;
			this.rbInput.Text = "Input";
			// 
			// rbOutput
			// 
			this.rbOutput.Location = new System.Drawing.Point(168, 48);
			this.rbOutput.Name = "rbOutput";
			this.rbOutput.TabIndex = 14;
			this.rbOutput.Text = "Output";
			// 
			// SetWebTransformTypeDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(306, 124);
			this.Controls.Add(this.rbOutput);
			this.Controls.Add(this.rbInput);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SetWebTransformTypeDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Set Web Transform Type";
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// Gets the web transform type
		/// </summary>
		public string WebTransformType
		{
			get
			{
				if ( rbInput.Checked )
				{
					return "input";
				} 
				else 
				{
					return "output";
				}
			}
		}
	}
}
