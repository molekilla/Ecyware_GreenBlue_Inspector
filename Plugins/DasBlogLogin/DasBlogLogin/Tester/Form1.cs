using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Security.Cryptography;

namespace Tester
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox txtChallenge;
		private System.Windows.Forms.TextBox txtPass;
		private System.Windows.Forms.TextBox txtUserName;
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
			this.txtChallenge = new System.Windows.Forms.TextBox();
			this.txtPass = new System.Windows.Forms.TextBox();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// txtChallenge
			// 
			this.txtChallenge.Location = new System.Drawing.Point(32, 88);
			this.txtChallenge.Name = "txtChallenge";
			this.txtChallenge.TabIndex = 0;
			this.txtChallenge.Text = "4yqaau55kj3evhnwnvxclt3v";
			// 
			// txtPass
			// 
			this.txtPass.Location = new System.Drawing.Point(32, 112);
			this.txtPass.Name = "txtPass";
			this.txtPass.PasswordChar = '*';
			this.txtPass.TabIndex = 1;
			this.txtPass.Text = "mlkill2k1";
			// 
			// txtUserName
			// 
			this.txtUserName.Location = new System.Drawing.Point(32, 64);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.TabIndex = 2;
			this.txtUserName.Text = "molekilla";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(148, 112);
			this.button1.Name = "button1";
			this.button1.TabIndex = 3;
			this.button1.Text = "button1";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.txtUserName);
			this.Controls.Add(this.txtPass);
			this.Controls.Add(this.txtChallenge);
			this.Name = "Form1";
			this.Text = "Form1";
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


		private void button1_Click(object sender, System.EventArgs e)
		{
			string username = string.Empty;
			string password = string.Empty;
			string challenge = string.Empty;

			Byte[] clearBytes = new UnicodeEncoding().GetBytes(txtPass.Text);
			Byte[] hashedBytes = ((HashAlgorithm) CryptoConfig.CreateFromName("MD5")).ComputeHash(clearBytes);
			
			string t = BitConverter.ToString(hashedBytes);

			System.Security.Cryptography.MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
			t = BitConverter.ToString(md5.ComputeHash(Encoding.Unicode.GetBytes(txtPass.Text)));

			// Apply logic
			string hash = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(txtPass.Text,"md5");

			string str = txtChallenge.Text + hash + this.txtUserName.Text;
			str =  System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "md5");
			challenge = str;

			StringBuilder stb = new StringBuilder();

			for ( int i=0;i<str.Length;i=(i+2))
			{
				stb.Append(str.Substring(i,2));
				stb.Append("-");
			}
			
			password = string.Empty;

			// update fields
			MessageBox.Show(stb.ToString().TrimEnd('-'));
		}
	}
}
