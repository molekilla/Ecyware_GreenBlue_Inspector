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
	/// Summary description for HeaderTransformValueDialog.
	/// </summary>
	public class HeaderTransformValueDialog : System.Windows.Forms.Form
	{
		private TransformValue _tvalue;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ComboBox cmbHeaderName;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new HeaderTransformValueDialog.
		/// </summary>
		public HeaderTransformValueDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}


		/// <summary>
		/// Loads the header list.
		/// </summary>
		/// <param name="headers"></param>
		public void LoadHeaders(ArrayList headers)
		{
			this.cmbHeaderName.Items.Clear();
			// Load headers.
			this.cmbHeaderName.Items.AddRange((string[])headers.ToArray(typeof(string)));
		}

		/// <summary>
		/// Gets a description.
		/// </summary>
		public string Description
		{
			get
			{
				return "Uses a header value from header \"" + ((HeaderTransformValue)this.TransformValue).HeaderName + "\"";
			}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(HeaderTransformValueDialog));
			this.cmbHeaderName = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// cmbHeaderName
			// 
			this.cmbHeaderName.Location = new System.Drawing.Point(150, 57);
			this.cmbHeaderName.Name = "cmbHeaderName";
			this.cmbHeaderName.Size = new System.Drawing.Size(174, 21);
			this.cmbHeaderName.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(18, 60);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 18);
			this.label2.TabIndex = 1;
			this.label2.Text = "Header Name";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 12);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(372, 23);
			this.label4.TabIndex = 0;
			this.label4.Text = "Select a header value to map.";
			// 
			// btnOK
			// 
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(162, 96);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(246, 96);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// HeaderTransformValueDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(382, 134);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.cmbHeaderName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HeaderTransformValueDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Header Value Dialog";
			this.TopMost = true;
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			HeaderTransformValue tvalue = new HeaderTransformValue();
			tvalue.HeaderName = this.cmbHeaderName.Text.ToString().Replace(" ","");
			//tvalue.WebRequestName = this.cmbWebRequests.SelectedValue.ToString().Split(':')[1].Trim();
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
				if ( this.TransformValue is HeaderTransformValue )
				{
					this.cmbHeaderName.Text = ((HeaderTransformValue)_tvalue).HeaderName;
				}
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
