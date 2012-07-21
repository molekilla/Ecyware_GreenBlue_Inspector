using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Ecyware.GreenBlue.Controls.Scripting
{
	/// <summary>
	/// Summary description for ScriptingApplicationMetadataDialog.
	/// </summary>
	public class ScriptingApplicationMetadataDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtApplicationName;
		private System.Windows.Forms.TextBox txtDescription;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtKeywords;
		private System.Windows.Forms.Button btnPublish;
		private System.Windows.Forms.Label label4;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new ScriptingApplicationMetadataDialog.
		/// </summary>
		public ScriptingApplicationMetadataDialog()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ScriptingApplicationMetadataDialog));
			this.txtApplicationName = new System.Windows.Forms.TextBox();
			this.txtDescription = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtKeywords = new System.Windows.Forms.TextBox();
			this.btnPublish = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// txtApplicationName
			// 
			this.txtApplicationName.AcceptsReturn = true;
			this.txtApplicationName.Location = new System.Drawing.Point(126, 60);
			this.txtApplicationName.Name = "txtApplicationName";
			this.txtApplicationName.Size = new System.Drawing.Size(246, 20);
			this.txtApplicationName.TabIndex = 1;
			this.txtApplicationName.Text = "";
			// 
			// txtDescription
			// 
			this.txtDescription.Location = new System.Drawing.Point(126, 84);
			this.txtDescription.Multiline = true;
			this.txtDescription.Name = "txtDescription";
			this.txtDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtDescription.Size = new System.Drawing.Size(246, 96);
			this.txtDescription.TabIndex = 3;
			this.txtDescription.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 60);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "Application Name";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 84);
			this.label2.Name = "label2";
			this.label2.TabIndex = 2;
			this.label2.Text = "Description";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 186);
			this.label3.Name = "label3";
			this.label3.TabIndex = 4;
			this.label3.Text = "Keywords";
			// 
			// txtKeywords
			// 
			this.txtKeywords.Location = new System.Drawing.Point(126, 186);
			this.txtKeywords.Multiline = true;
			this.txtKeywords.Name = "txtKeywords";
			this.txtKeywords.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtKeywords.Size = new System.Drawing.Size(246, 66);
			this.txtKeywords.TabIndex = 5;
			this.txtKeywords.Text = "";
			// 
			// btnPublish
			// 
			this.btnPublish.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnPublish.Location = new System.Drawing.Point(294, 258);
			this.btnPublish.Name = "btnPublish";
			this.btnPublish.TabIndex = 6;
			this.btnPublish.Text = "&Publish";
			this.btnPublish.Click += new System.EventHandler(this.btnPublish_Click);
			// 
			// label4
			// 
			this.label4.BackColor = System.Drawing.Color.White;
			this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.Red;
			this.label4.Location = new System.Drawing.Point(12, 12);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(372, 42);
			this.label4.TabIndex = 7;
			this.label4.Text = "Warning: Be sure to remove any security sensitive information from the scripting " +
				"application as usernames, passwords or cookie tokens before publishing.";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ScriptingApplicationMetadataDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(396, 298);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.btnPublish);
			this.Controls.Add(this.txtKeywords);
			this.Controls.Add(this.txtDescription);
			this.Controls.Add(this.txtApplicationName);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ScriptingApplicationMetadataDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Publis Scripting Application";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnPublish_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}


		/// <summary>
		/// Gets the application name.
		/// </summary>
		public string ApplicationName
		{
			get
			{
				return txtApplicationName.Text;
			}
		}

		/// <summary>
		/// Gets the description.
		/// </summary>
		public string Description
		{
			get
			{
				return txtDescription.Text;
			}
		}

		/// <summary>
		/// Gets the keywords.
		/// </summary>
		public string Keywords
		{
			get
			{
				return txtKeywords.Text;
			}
		}
	}
}
