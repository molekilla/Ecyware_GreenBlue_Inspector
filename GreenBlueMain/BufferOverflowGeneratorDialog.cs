// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: July 2004
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Ecyware.GreenBlue.Engine;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Contains the definition for the BufferOverflowGeneratorDialog type.
	/// </summary>
	public class BufferOverflowGeneratorDialog : System.Windows.Forms.Form
	{
		private BufferOverflowGenerator generator = new BufferOverflowGenerator();
		private string _selectedValue = string.Empty;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.NumericUpDown numBufferLen;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new BufferOverflowGeneratorDialog.
		/// </summary>
		public BufferOverflowGeneratorDialog()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BufferOverflowGeneratorDialog));
			this.label1 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.numBufferLen = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.numBufferLen)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 18);
			this.label1.TabIndex = 1;
			this.label1.Text = "Buffer Length:";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(188, 48);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 12;
			this.btnCancel.Text = "Cancel";
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.Location = new System.Drawing.Point(102, 48);
			this.btnSave.Name = "btnSave";
			this.btnSave.TabIndex = 11;
			this.btnSave.Text = "Set";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// numBufferLen
			// 
			this.numBufferLen.Location = new System.Drawing.Point(102, 18);
			this.numBufferLen.Maximum = new System.Decimal(new int[] {
																		 1000000,
																		 0,
																		 0,
																		 0});
			this.numBufferLen.Minimum = new System.Decimal(new int[] {
																		 100,
																		 0,
																		 0,
																		 0});
			this.numBufferLen.Name = "numBufferLen";
			this.numBufferLen.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.numBufferLen.Size = new System.Drawing.Size(78, 20);
			this.numBufferLen.TabIndex = 13;
			this.numBufferLen.Value = new System.Decimal(new int[] {
																	   1000,
																	   0,
																	   0,
																	   0});
			// 
			// BufferOverflowGeneratorDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(270, 74);
			this.Controls.Add(this.numBufferLen);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BufferOverflowGeneratorDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Buffer Overflow Generator Dialog";
			((System.ComponentModel.ISupportInitialize)(this.numBufferLen)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		/// <summary>
		/// Gets or sets the selected value
		/// </summary>
		public string SelectedValue
		{
			get
			{
				return _selectedValue;
			}
			set
			{
				_selectedValue = value;
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			this.SelectedValue = generator.GenerateStringBuffer(Convert.ToInt32(this.numBufferLen.Value));
		}
	}
}
