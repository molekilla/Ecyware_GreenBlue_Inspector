using System;
using System.Drawing;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using System.Windows.Forms;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms.Designers;
using Ecyware.GreenBlue.Engine.Transforms;

namespace Ecyware.GreenBlue.Engine.Transforms.Designers
{
	/// <summary>
	/// Summary description for DefaultTransformValueDialog.
	/// </summary>
	public class DefaultTransformValueDialog : System.Windows.Forms.Form
	{
		private TransformValue _tvalue;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtValue;
		private System.Windows.Forms.CheckBox chkEnabledInputArgument;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new DefaultTransformValueDialog.
		/// </summary>
		public DefaultTransformValueDialog()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DefaultTransformValueDialog));
			this.label4 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtValue = new System.Windows.Forms.TextBox();
			this.chkEnabledInputArgument = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 12);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(372, 23);
			this.label4.TabIndex = 0;
			this.label4.Text = "Sets a default value.";
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(162, 98);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 4;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(246, 98);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 5;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(18, 42);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 18);
			this.label1.TabIndex = 1;
			this.label1.Text = "Value";
			// 
			// txtValue
			// 
			this.txtValue.Location = new System.Drawing.Point(150, 42);
			this.txtValue.Name = "txtValue";
			this.txtValue.Size = new System.Drawing.Size(210, 20);
			this.txtValue.TabIndex = 2;
			this.txtValue.Text = "";
			// 
			// chkEnabledInputArgument
			// 
			this.chkEnabledInputArgument.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkEnabledInputArgument.Location = new System.Drawing.Point(150, 66);
			this.chkEnabledInputArgument.Name = "chkEnabledInputArgument";
			this.chkEnabledInputArgument.Size = new System.Drawing.Size(210, 24);
			this.chkEnabledInputArgument.TabIndex = 3;
			this.chkEnabledInputArgument.Text = "Enabled for input argument";
			// 
			// DefaultTransformValueDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(382, 130);
			this.Controls.Add(this.chkEnabledInputArgument);
			this.Controls.Add(this.txtValue);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DefaultTransformValueDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Default Value Dialog";
			this.TopMost = true;
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			DefaultTransformValue tvalue = new DefaultTransformValue();
			tvalue.EnabledInputArgument = chkEnabledInputArgument.Checked;
			tvalue.Value = this.txtValue.Text;
			_tvalue = tvalue;
			DialogResult = DialogResult.OK;
		}

		/// <summary>
		/// Loads the transform values.
		/// </summary>
		public void LoadTransformValue()
		{
			if ( this.TransformValue != null )
			{
				if ( this.TransformValue is DefaultTransformValue )
				{
					this.chkEnabledInputArgument.Checked = ((DefaultTransformValue)_tvalue).EnabledInputArgument;
					this.txtValue.Text = ((DefaultTransformValue)_tvalue).Value;
				}
			}
		}

		/// <summary>
		/// Gets a description.
		/// </summary>
		public string Description
		{
			get
			{
				return "Uses a default value of \"" + ((DefaultTransformValue)this.TransformValue).Value + "\"";
			}
		}

		/// <summary>
		/// Gets the transform value.
		/// </summary>
		public TransformValue TransformValue
		{
			get
			{
				return _tvalue;
			}
			set
			{
				_tvalue = value;
			}
		}
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}


	}
}
