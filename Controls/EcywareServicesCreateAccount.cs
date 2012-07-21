using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
//using Ecyware.GreenBlue.LicenseServices.Client;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Summary description for EcywareServicesCreateAccount.
	/// </summary>
	public class EcywareServicesCreateAccount : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnCreate;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.TextBox txtFullName;
		private System.Windows.Forms.TextBox txtRepeatPassword;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtEmail;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public EcywareServicesCreateAccount()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.txtPassword.PasswordChar = '\u25CF';
			this.txtRepeatPassword.PasswordChar = '\u25CF';
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


		private void CreateAccount()
		{
			if ( txtPassword.Text == txtRepeatPassword.Text )
			{
				AccountMessage accountMessage = new AccountMessage();
				accountMessage.CurrentAccount.Username = this.txtUsername.Text;
				accountMessage.CurrentAccount.Password = txtPassword.Text;
				accountMessage.CurrentAccount.Name = this.txtFullName.Text;
				accountMessage.CurrentAccount.Email = this.txtEmail.Text;

				LicenseServiceClient client = Utils.ServicesProxy.GetClientProxy();
				
				client.BeginCreateAccount(accountMessage, new MessageResultHandler(CreateAccountInvoker), null);
			} 
			else 
			{
				MessageBox.Show("Password settings incorrect. Please try again.",Utils.AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void CreateAccountInvoker(object sender, EventArgs e)
		{
			Invoke(new MessageResultHandler(CreateAccountResult), new object[] {sender, e});
		}

		private void CreateAccountResult(object sender, EventArgs e)
		{
			MessageEventArgs args = (MessageEventArgs)e;
			AccountMessage account = (AccountMessage)args.Message;

			if ( account.AccountExists )
			{
				MessageBox.Show("Username already exists. Please change your username.",Utils.AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			} 
			else 
			{
				MessageBox.Show("Account created.",Utils.AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		/// <summary>
		/// Gets the user name.
		/// </summary>
		public string UserName
		{
			get
			{
				return this.txtUsername.Text;
			}
		}
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(EcywareServicesCreateAccount));
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnCreate = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.txtFullName = new System.Windows.Forms.TextBox();
			this.txtRepeatPassword = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtEmail = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(228, 144);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 11;
			this.btnCancel.Text = "Close";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnCreate
			// 
			this.btnCreate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCreate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCreate.Location = new System.Drawing.Point(144, 144);
			this.btnCreate.Name = "btnCreate";
			this.btnCreate.TabIndex = 10;
			this.btnCreate.Text = "Create";
			this.btnCreate.Click += new System.EventHandler(this.btnCreate_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(18, 18);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "Username";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(18, 42);
			this.label2.Name = "label2";
			this.label2.TabIndex = 2;
			this.label2.Text = "Password";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(18, 66);
			this.label3.Name = "label3";
			this.label3.TabIndex = 4;
			this.label3.Text = "Repeat Password";
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(18, 90);
			this.label4.Name = "label4";
			this.label4.TabIndex = 6;
			this.label4.Text = "Full Name";
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.Location = new System.Drawing.Point(18, 114);
			this.label5.Name = "label5";
			this.label5.TabIndex = 8;
			this.label5.Text = "E-Mail";
			// 
			// txtUsername
			// 
			this.txtUsername.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtUsername.ForeColor = System.Drawing.Color.DarkGreen;
			this.txtUsername.Location = new System.Drawing.Point(120, 18);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(186, 22);
			this.txtUsername.TabIndex = 1;
			this.txtUsername.Text = "";
			// 
			// txtFullName
			// 
			this.txtFullName.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtFullName.ForeColor = System.Drawing.Color.DarkGreen;
			this.txtFullName.Location = new System.Drawing.Point(120, 90);
			this.txtFullName.Name = "txtFullName";
			this.txtFullName.Size = new System.Drawing.Size(186, 22);
			this.txtFullName.TabIndex = 7;
			this.txtFullName.Text = "";
			// 
			// txtRepeatPassword
			// 
			this.txtRepeatPassword.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtRepeatPassword.ForeColor = System.Drawing.Color.DarkGreen;
			this.txtRepeatPassword.Location = new System.Drawing.Point(120, 66);
			this.txtRepeatPassword.Name = "txtRepeatPassword";
			this.txtRepeatPassword.PasswordChar = '*';
			this.txtRepeatPassword.Size = new System.Drawing.Size(186, 22);
			this.txtRepeatPassword.TabIndex = 5;
			this.txtRepeatPassword.Text = "";
			// 
			// txtPassword
			// 
			this.txtPassword.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtPassword.ForeColor = System.Drawing.Color.DarkGreen;
			this.txtPassword.Location = new System.Drawing.Point(120, 42);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(186, 22);
			this.txtPassword.TabIndex = 3;
			this.txtPassword.Text = "";
			// 
			// txtEmail
			// 
			this.txtEmail.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtEmail.ForeColor = System.Drawing.Color.DarkGreen;
			this.txtEmail.Location = new System.Drawing.Point(120, 114);
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.Size = new System.Drawing.Size(186, 22);
			this.txtEmail.TabIndex = 9;
			this.txtEmail.Text = "";
			// 
			// EcywareServicesCreateAccount
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(340, 182);
			this.Controls.Add(this.txtEmail);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.txtRepeatPassword);
			this.Controls.Add(this.txtFullName);
			this.Controls.Add(this.txtUsername);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnCreate);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EcywareServicesCreateAccount";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Ecyware GreenBlue Services - New Account";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnCreate_Click(object sender, System.EventArgs e)
		{
			CreateAccount();
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}
}
