using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Xml;
using System.Security.Cryptography;
using Microsoft.Web.Services2;
using Microsoft.Web.Services2.Attachments;
using Microsoft.Web.Services2.Dime;
using Microsoft.Web.Services2.Addressing;
using Microsoft.Web.Services2.Messaging;
using Microsoft.Web.Services2.Security;
using Microsoft.Web.Services2.Security.Tokens;
using Ecyware.GreenBlue.LicenseServices.Client;
using Ecyware.GreenBlue.Protocols.Http.Scripting;
using EcyXmlEncryption = Ecyware.GreenBlue.Configuration.Encryption;

namespace LicenserServiceClientTest
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{		
		LicenseServiceClient client;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnActivate;
		private System.Windows.Forms.Button btnValidate;
		private System.Windows.Forms.TextBox txtEmail;
		private System.Windows.Forms.TextBox txtApplicationPath;
		private System.Windows.Forms.Button btnSign;
		private System.Windows.Forms.Button btnVerifyApplication;
		private System.Windows.Forms.TextBox txtSignedApplication;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
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
				if (components != null) 
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
			this.txtName = new System.Windows.Forms.TextBox();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.btnActivate = new System.Windows.Forms.Button();
			this.txtEmail = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.btnValidate = new System.Windows.Forms.Button();
			this.txtApplicationPath = new System.Windows.Forms.TextBox();
			this.btnSign = new System.Windows.Forms.Button();
			this.btnVerifyApplication = new System.Windows.Forms.Button();
			this.txtSignedApplication = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(186, 42);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(174, 20);
			this.txtName.TabIndex = 0;
			this.txtName.Text = "Rogelio Morrell";
			// 
			// txtUsername
			// 
			this.txtUsername.Location = new System.Drawing.Point(186, 66);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(174, 20);
			this.txtUsername.TabIndex = 1;
			this.txtUsername.Text = "molekilla";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(84, 42);
			this.label1.Name = "label1";
			this.label1.TabIndex = 2;
			this.label1.Text = "Name";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(84, 66);
			this.label2.Name = "label2";
			this.label2.TabIndex = 3;
			this.label2.Text = "Username";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(84, 89);
			this.label3.Name = "label3";
			this.label3.TabIndex = 5;
			this.label3.Text = "Password";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(186, 89);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(174, 20);
			this.txtPassword.TabIndex = 4;
			this.txtPassword.Text = "pirata2k3";
			// 
			// btnActivate
			// 
			this.btnActivate.Location = new System.Drawing.Point(84, 144);
			this.btnActivate.Name = "btnActivate";
			this.btnActivate.TabIndex = 6;
			this.btnActivate.Text = "Create";
			this.btnActivate.Click += new System.EventHandler(this.btnActivate_Click);
			// 
			// txtEmail
			// 
			this.txtEmail.Location = new System.Drawing.Point(186, 114);
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.Size = new System.Drawing.Size(174, 20);
			this.txtEmail.TabIndex = 7;
			this.txtEmail.Text = "rogelioc@ecyware.com";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(84, 114);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(60, 23);
			this.label4.TabIndex = 8;
			this.label4.Text = "E-Mail";
			// 
			// btnValidate
			// 
			this.btnValidate.Location = new System.Drawing.Point(342, 150);
			this.btnValidate.Name = "btnValidate";
			this.btnValidate.Size = new System.Drawing.Size(90, 23);
			this.btnValidate.TabIndex = 9;
			this.btnValidate.Text = "Get User Info";
			this.btnValidate.Click += new System.EventHandler(this.btnValidate_Click);
			// 
			// txtApplicationPath
			// 
			this.txtApplicationPath.Location = new System.Drawing.Point(24, 204);
			this.txtApplicationPath.Name = "txtApplicationPath";
			this.txtApplicationPath.Size = new System.Drawing.Size(390, 20);
			this.txtApplicationPath.TabIndex = 10;
			this.txtApplicationPath.Text = "C:\\Documents and Settings\\rogelio\\Desktop\\Scripting Applications\\h2.xml";
			// 
			// btnSign
			// 
			this.btnSign.Location = new System.Drawing.Point(312, 270);
			this.btnSign.Name = "btnSign";
			this.btnSign.Size = new System.Drawing.Size(108, 23);
			this.btnSign.TabIndex = 11;
			this.btnSign.Text = "Sign Application";
			this.btnSign.Click += new System.EventHandler(this.btnSign_Click);
			// 
			// btnVerifyApplication
			// 
			this.btnVerifyApplication.Location = new System.Drawing.Point(312, 300);
			this.btnVerifyApplication.Name = "btnVerifyApplication";
			this.btnVerifyApplication.Size = new System.Drawing.Size(108, 23);
			this.btnVerifyApplication.TabIndex = 12;
			this.btnVerifyApplication.Text = "Verify Application";
			this.btnVerifyApplication.Click += new System.EventHandler(this.btnVerifyApplication_Click);
			// 
			// txtSignedApplication
			// 
			this.txtSignedApplication.Location = new System.Drawing.Point(24, 234);
			this.txtSignedApplication.Name = "txtSignedApplication";
			this.txtSignedApplication.Size = new System.Drawing.Size(390, 20);
			this.txtSignedApplication.TabIndex = 13;
			this.txtSignedApplication.Text = "C:\\Documents and Settings\\rogelio\\Desktop\\Scripting Applications\\h2.xml.signed";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(478, 356);
			this.Controls.Add(this.txtSignedApplication);
			this.Controls.Add(this.btnVerifyApplication);
			this.Controls.Add(this.btnSign);
			this.Controls.Add(this.txtApplicationPath);
			this.Controls.Add(this.txtEmail);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.txtUsername);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.btnValidate);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.btnActivate);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "Form1";
			this.Text = "Product Activation & Signing Application Tool";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		/// <summary>
		/// Creates a hash for the password.
		/// </summary>
		/// <param name="sessionId"> The current session id.</param>
		/// <param name="password"> The password.</param>
		/// <returns></returns>
		private string HashPassword(string sessionId, string password)
		{
			SHA1CryptoServiceProvider hashProvider 
				= new SHA1CryptoServiceProvider();

			string salt = Convert.ToBase64String(hashProvider.ComputeHash(System.Text.Encoding.UTF8.GetBytes(sessionId)));
			byte[] hash = hashProvider.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password + salt));
			return Convert.ToBase64String(hash);
		}

		private void btnValidate_Click(object sender, System.EventArgs e)
		{		
			string hashedPassword = HashPassword(txtUsername.Text, txtPassword.Text);

			UsernameToken token = new UsernameToken(txtUsername.Text, hashedPassword, PasswordOption.SendPlainText);
			token.Id = "LicenseToken";

			client.Security = new Security();
			client.Security.Tokens.Add(token);
			client.BeginGetUserDetails(new MessageResultHandler(GetUserDetailsInvoker), null);
		}

		private void GetUserDetailsInvoker(object sender, EventArgs e)
		{
			Invoke(new MessageResultHandler(GetUserDetailsResult), new object[] {sender, e});
		}

		private void GetUserDetailsResult(object sender, EventArgs e)
		{
			MessageEventArgs args = (MessageEventArgs)e;
			AccountMessage account = (AccountMessage)args.Message;
			MessageBox.Show("Username: " + account.CurrentAccount.Name + "\r\nPassword: " + account.CurrentAccount.Password + "\r\nEmail: " + account.CurrentAccount.Email);
		}

		private void btnActivate_Click(object sender, System.EventArgs e)
		{	
			AccountMessage accountMessage = new AccountMessage();
			accountMessage.CurrentAccount.Username = this.txtUsername.Text;
			accountMessage.CurrentAccount.Password = txtPassword.Text;
			accountMessage.CurrentAccount.Name = this.txtName.Text;
			accountMessage.CurrentAccount.Email = this.txtEmail.Text;

			client.BeginCreateAccount(accountMessage, new MessageResultHandler(CreateAccountInvoker), null);
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
				MessageBox.Show("Username already taken. Please change your username.");
			} 
			else 
			{
				MessageBox.Show("Account created!");
			}
		}

		private void client_ExceptionEventHandler(object sender, Exception ex)
		{
			MessageBox.Show(ex.Message);
		}

		private void btnSign_Click(object sender, System.EventArgs e)
		{			
			ScriptingApplication application = new ScriptingApplication();
			application.Load(this.txtApplicationPath.Text);
			string encryptedXml = application.Encrypt();
		
			string hashedPassword = HashPassword(txtUsername.Text, txtPassword.Text);

			UsernameToken token = new UsernameToken(txtUsername.Text, hashedPassword, PasswordOption.SendPlainText);
			token.Id = "LicenseToken";

			// create client message.
			RegisterApplicationMessage message = new RegisterApplicationMessage();
			message.ApplicationID = application.Header.ApplicationID;
			message.EncryptedScriptingApplicationXml = encryptedXml;

			client.Security = new Security();
			client.Security.Tokens.Add(token);
			client.BeginRegisterScriptingApplication(
				message,
				new MessageResultHandler(SignScriptingAppInvoker),
				null);
		}
		private void SignScriptingAppInvoker(object sender, EventArgs e)
		{
			Invoke(new MessageResultHandler(SignScriptingAppResult), new object[] {sender, e});
		}

		private void SignScriptingAppResult(object sender, EventArgs e)
		{
			
			MessageEventArgs args = (MessageEventArgs)e;
			RegisterApplicationResultMessage result = (RegisterApplicationResultMessage)args.Message;

			if ( result.IsApplicationRegistered )
			{

				StreamWriter writer = new StreamWriter(this.txtApplicationPath.Text + ".signed",false);
				writer.Write(result.SignedScriptingApplicationXml);
				writer.Flush();
				writer.Close();

				MessageBox.Show("Application registered successfully");
			} 
			else 
			{
				MessageBox.Show(result.Message);
			}
		}

		private void Form1_Load(object sender, System.EventArgs e)
		{
			try
			{
				client = LicenseServicesClientProxy.GetClientProxy();
				client.GetSessionID();

				// exception handling registration
				// ExceptionHandlerEvent += new ExceptionHandler(OrderForm_ExceptionEventHandler);
				// LicenseServicesClientProxy.RegisterControlForExceptionHandling(this);
				client.ExceptionEventHandler += new ExceptionHandler(client_ExceptionEventHandler);
			}
			catch ( Exception ex )
			{
				MessageBox.Show(ex.ToString());
				this.Close();
			}		
		}

		/// <summary>
		/// Decrypts the body object.
		/// </summary>
		/// <param name="envelope"> The SoapEnvelope.</param>
		/// <returns> The XmlDocument with the decrypted Soap Body object.</returns>
		private XmlDocument Decrypt(XmlDocument encryptedDocument)
		{
			// add to new document
			XmlNode node = encryptedDocument.SelectSingleNode("//EncryptedData");
			XmlDocument document = new XmlDocument();
			document.AppendChild(document.ImportNode(node,true));

			// decrypt
			EcyXmlEncryption.EncryptXml decrypt = new EcyXmlEncryption.EncryptXml(document);
			decrypt.AddKeyNameMapping("scriptingApplication", decrypt.GetMachineStoreKey("Ecyware.ScrAppEncryption"));
			decrypt.DecryptDocument();

			return document;
		}

		private void btnVerifyApplication_Click(object sender, System.EventArgs e)
		{
			StreamReader reader = new StreamReader(this.txtSignedApplication.Text);

			string hashedPassword = HashPassword(txtUsername.Text, txtPassword.Text);

			UsernameToken token = new UsernameToken(txtUsername.Text, hashedPassword, PasswordOption.SendPlainText);
			token.Id = "LicenseToken";

			client.Security = new Security();
			client.Security.Tokens.Add(token);

			bool result = client.VerifyApplicationSignature(reader.BaseStream);
			reader.Close();

			if ( result )
			{
				XmlDocument enc = new XmlDocument();
				enc.Load(this.txtSignedApplication.Text);
				XmlDocument newDoc = Decrypt(enc);
				MessageBox.Show(newDoc.DocumentElement.InnerXml);
				MessageBox.Show("This is a scripting application.", "GB", MessageBoxButtons.OK, MessageBoxIcon.Information);
			} 
			else 
			{
				MessageBox.Show("This is an invalid scripting application.", "GB", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
	}
}
