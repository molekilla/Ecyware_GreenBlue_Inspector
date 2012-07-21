// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: July 2004
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Contains the definition for the BasicAuthenticationDialog type.
	/// </summary>
	public class BasicAuthenticationDialog : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox grpBasicAuthentication;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtDomain;
		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnSave;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private bool _isSet = false;

		/// <summary>
		/// Creates a new BasicAuthenticationDialog.
		/// </summary>
		public BasicAuthenticationDialog()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BasicAuthenticationDialog));
			this.grpBasicAuthentication = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtDomain = new System.Windows.Forms.TextBox();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.grpBasicAuthentication.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpBasicAuthentication
			// 
			this.grpBasicAuthentication.Controls.Add(this.label4);
			this.grpBasicAuthentication.Controls.Add(this.label3);
			this.grpBasicAuthentication.Controls.Add(this.label2);
			this.grpBasicAuthentication.Controls.Add(this.txtPassword);
			this.grpBasicAuthentication.Controls.Add(this.txtDomain);
			this.grpBasicAuthentication.Controls.Add(this.txtUsername);
			this.grpBasicAuthentication.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpBasicAuthentication.Location = new System.Drawing.Point(6, 6);
			this.grpBasicAuthentication.Name = "grpBasicAuthentication";
			this.grpBasicAuthentication.Size = new System.Drawing.Size(306, 108);
			this.grpBasicAuthentication.TabIndex = 0;
			this.grpBasicAuthentication.TabStop = false;
			this.grpBasicAuthentication.Text = "Authentication";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 78);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(84, 18);
			this.label4.TabIndex = 4;
			this.label4.Text = "Domain:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 54);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(84, 18);
			this.label3.TabIndex = 2;
			this.label3.Text = "Password:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(84, 18);
			this.label2.TabIndex = 0;
			this.label2.Text = "Username:";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(102, 52);
			this.txtPassword.MaxLength = 200;
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(186, 20);
			this.txtPassword.TabIndex = 3;
			this.txtPassword.Text = "";
			// 
			// txtDomain
			// 
			this.txtDomain.Location = new System.Drawing.Point(102, 76);
			this.txtDomain.MaxLength = 200;
			this.txtDomain.Name = "txtDomain";
			this.txtDomain.Size = new System.Drawing.Size(186, 20);
			this.txtDomain.TabIndex = 5;
			this.txtDomain.Text = "";
			// 
			// txtUsername
			// 
			this.txtUsername.Location = new System.Drawing.Point(102, 28);
			this.txtUsername.MaxLength = 200;
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(186, 20);
			this.txtUsername.TabIndex = 1;
			this.txtUsername.Text = "";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(234, 120);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.Location = new System.Drawing.Point(150, 120);
			this.btnSave.Name = "btnSave";
			this.btnSave.TabIndex = 1;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// BasicAuthenticationDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(316, 152);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.grpBasicAuthentication);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BasicAuthenticationDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Basic Authentication Dialog";
			this.grpBasicAuthentication.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			if  ( ( this.txtUsername.Text.Length > 0 ) && ( this.txtPassword.Text.Length > 0 ) )
			{
				this.IsBasicAuthenticationSet = true;
			} else {
				this.IsBasicAuthenticationSet = false;
			}
		}

		/// <summary>
		/// Gets or sets the boolean setting if the basic authentication has been set correctly.
		/// </summary>
		public bool IsBasicAuthenticationSet
		{
			get
			{
				return _isSet;
			}
			set
			{
				_isSet = value;
			}
		}


		/// <summary>
		/// Gets or sets the domain.
		/// </summary>
		public string Domain
		{
			get
			{
				return this.txtDomain.Text;
			}
			set
			{
				this.txtDomain.Text = value;
			}
		}
		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		public string Password
		{
			get
			{
				return this.txtPassword.Text;
			}
			set
			{
				this.txtPassword.Text = value;
			}
		}
		/// <summary>
		/// Gets or sets the username.
		/// </summary>
		public string Username
		{	
			get
			{
				return this.txtUsername.Text;
			}
			set
			{
				this.txtUsername.Text = value;
			}
		}
	}
}
