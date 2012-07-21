// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: March 2005
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Security;
using System.Security.Principal;
//using Ecyware.GreenBlue.LicenseServices.Client;
using Microsoft.Web.Services2.Security;
using Microsoft.Web.Services2.Security.Tokens;
using Microsoft.Web.Services2;
using Microsoft.Web.Services2.Addressing;
using Microsoft.Web.Services2.Messaging;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Contains the definition for the BasicAuthenticationDialog type.
	/// </summary>
	public class EcywareServicesLogin : System.Windows.Forms.Form
	{
		//LicenseServiceClient client;
		private System.Windows.Forms.Button btnCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.Button btnLogin;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.CheckBox chkSaveLogin;
		private System.Windows.Forms.LinkLabel lnkCreateNewAccount;

		/// <summary>
		/// Creates a new BasicAuthenticationDialog.
		/// </summary>
		public EcywareServicesLogin()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if ( IsSecureLogin )
			{
				this.chkSaveLogin.Checked = true;
			}
			else
			{
				this.chkSaveLogin.Checked = false;
			}

			client = Utils.ServicesProxy.GetClientProxy();
			Utils.ServicesProxy.RegisterExceptionEventHandler(new ExceptionHandler(DisplayServiceErrors));

			this.txtPassword.PasswordChar = '\u25CF';
			this.txtUsername.SelectAll();
			this.txtUsername.Focus();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(EcywareServicesLogin));
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnLogin = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.lnkCreateNewAccount = new System.Windows.Forms.LinkLabel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.chkSaveLogin = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(354, 120);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 13;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnLogin
			// 
			this.btnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnLogin.Location = new System.Drawing.Point(270, 120);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.TabIndex = 12;
			this.btnLogin.Text = "Login";
			this.btnLogin.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(150, 66);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(84, 18);
			this.label3.TabIndex = 17;
			this.label3.Text = "Password";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(150, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(84, 18);
			this.label2.TabIndex = 16;
			this.label2.Text = "Username";
			// 
			// txtPassword
			// 
			this.txtPassword.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtPassword.ForeColor = System.Drawing.Color.DarkGreen;
			this.txtPassword.Location = new System.Drawing.Point(240, 64);
			this.txtPassword.MaxLength = 200;
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(192, 22);
			this.txtPassword.TabIndex = 15;
			this.txtPassword.Text = "";
			this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
			// 
			// txtUsername
			// 
			this.txtUsername.Font = new System.Drawing.Font("Verdana", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtUsername.ForeColor = System.Drawing.Color.DarkGreen;
			this.txtUsername.Location = new System.Drawing.Point(240, 34);
			this.txtUsername.MaxLength = 200;
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(192, 22);
			this.txtUsername.TabIndex = 14;
			this.txtUsername.Text = "";
			this.txtUsername.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUsername_KeyPress);
			// 
			// lnkCreateNewAccount
			// 
			this.lnkCreateNewAccount.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lnkCreateNewAccount.Location = new System.Drawing.Point(324, 6);
			this.lnkCreateNewAccount.Name = "lnkCreateNewAccount";
			this.lnkCreateNewAccount.Size = new System.Drawing.Size(114, 18);
			this.lnkCreateNewAccount.TabIndex = 18;
			this.lnkCreateNewAccount.TabStop = true;
			this.lnkCreateNewAccount.Text = "Create new account";
			this.lnkCreateNewAccount.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkCreateNewAccount_LinkClicked);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(6, 6);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(138, 120);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 19;
			this.pictureBox1.TabStop = false;
			// 
			// chkSaveLogin
			// 
			this.chkSaveLogin.Location = new System.Drawing.Point(246, 90);
			this.chkSaveLogin.Name = "chkSaveLogin";
			this.chkSaveLogin.Size = new System.Drawing.Size(186, 24);
			this.chkSaveLogin.TabIndex = 20;
			this.chkSaveLogin.Text = "Sign me automatically";
			// 
			// EcywareServicesLogin
			// 
			this.AcceptButton = this.btnLogin;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.White;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(444, 148);
			this.Controls.Add(this.chkSaveLogin);
			this.Controls.Add(this.lnkCreateNewAccount);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.txtUsername);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnLogin);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EcywareServicesLogin";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Ecyware GreenBlue Services - Login";
			this.ResumeLayout(false);

		}
		#endregion


		private void btnSave_Click(object sender, System.EventArgs e)
		{
			string password = this.txtPassword.Text;
			string username = this.txtUsername.Text;

			if ( username.Length > 0 && password.Length > 0 )
			{
				if ( this.chkSaveLogin.Checked )
				{	
					Login(username, password);
				} 
				else 
				{
					SecureLoginCredentials.Remove();
					Login(username, password);
				}
			}
		}

		/// <summary>
		/// Saves the login credentials securely.
		/// </summary>
		private void SecureSaveLoginCredentials()
		{
			SecureLoginCredentials secureCreds = new SecureLoginCredentials(this.txtUsername.Text, this.txtPassword.Text);
			secureCreds.Save();
		}

		/// <summary>
		/// Returns true if secure login is enabled, else false.
		/// </summary>
		/// <returns>Returns true if secure login is enabled, else false.</returns>
		public bool IsSecureLogin
		{
			get
			{
				return SecureLoginCredentials.Exists();
			}
		}

		/// <summary>
		/// Automatic secure login.
		/// </summary>
		public void AutoLogin()
		{
			SecureLoginCredentials sec = SecureLoginCredentials.Load();
			string username = sec.Username;
			string password = sec.Password;

			this.txtUsername.Text = username;
			this.txtPassword.Text = password;

			if ( username.Length > 0 && password.Length > 0 )
			{
				Login(username, password);
			}
		}
		/// <summary>
		/// Login user.
		/// </summary>
		public void Login(string username, string password)
		{			
			Utils.ServicesProxy.AddUserToken(username, password);
			client.BeginGetUserDetails(
				new MessageResultHandler(GetUserDetailsInvoker),
				null);
			this.txtUsername.Enabled = false;
			this.txtPassword.Enabled = false;
			this.btnLogin.Enabled = false;
			this.btnCancel.Enabled = false;
			this.lnkCreateNewAccount.Enabled = false;
		}

		/// <summary>
		/// Gets the user details.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The event args.</param>
		private void GetUserDetailsInvoker(object sender, EventArgs e)
		{
			Invoke(new MessageResultHandler(GetUserDetailsResult), new object[] {sender, e});
		}

		private void GetUserDetailsResult(object sender, EventArgs e)
		{
			MessageEventArgs args = (MessageEventArgs)e;
			AccountMessage account = (AccountMessage)args.Message;
			this.DialogResult = DialogResult.OK;
			Utils.ServicesProxy.IsConnected = true;

			if ( this.chkSaveLogin.Checked )
			{
				SecureSaveLoginCredentials();
			}
			this.Close();
		}

		private void DisplayServiceErrors(object sender, Exception ex)
		{
			this.txtUsername.Enabled = true;
			this.txtPassword.Enabled = true;
			this.btnLogin.Enabled = true;
			this.btnCancel.Enabled = true;
			this.lnkCreateNewAccount.Enabled = true;

			if ( ex.Message.IndexOf("Missing") > -1 )
			{
				MessageBox.Show("Invalid login or server is offline.",Utils.AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			} 
			else 
			{
				MessageBox.Show(ex.ToString(),Utils.AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		private void txtUsername_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ( e.KeyChar==(char)13 )
			{
				Login(this.txtUsername.Text, this.txtPassword.Text);
				e.Handled=true;
			}
		}

		private void txtPassword_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ( e.KeyChar==(char)13 )
			{
				Login(this.txtUsername.Text, this.txtPassword.Text);
				e.Handled=true;
			}		
		}

		private void lnkCreateNewAccount_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			EcywareServicesCreateAccount createAccount = new EcywareServicesCreateAccount();
			if ( createAccount.ShowDialog() == DialogResult.OK )
			{
				txtUsername.Text = createAccount.UserName;
			}
		}

		private void menuItem5_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			EcywareServicesCreateAccount createAccount = new EcywareServicesCreateAccount();
			if ( createAccount.ShowDialog() == DialogResult.OK )
			{
				txtUsername.Text = createAccount.UserName;
			}		
		}

		private void mnuProxy_Click(object sender, System.EventArgs e)
		{			
//			ProxyDialog proxyDialog = new ProxyDialog(this.ProxySettings);
//			proxyDialog.IsProxySettingSet = this.IsProxyEnabled;
//			proxyDialog.ShowDialog();
//			if ( proxyDialog.IsProxySettingSet )
//			{
//				this.IsProxyEnabled = proxyDialog.IsProxySettingSet;
//				this.ProxySettings = proxyDialog.ProxySettings;
//			}
//
//			proxyDialog.Close();								
		}
	}
}
