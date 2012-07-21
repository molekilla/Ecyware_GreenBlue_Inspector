using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Security.Cryptography;

namespace KeyGenerator
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox txtPublic;
		private System.Windows.Forms.TextBox txtPrivate;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
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
			this.button1 = new System.Windows.Forms.Button();
			this.txtPublic = new System.Windows.Forms.TextBox();
			this.txtPrivate = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(36, 12);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(216, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Generate Keys";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// txtPublic
			// 
			this.txtPublic.Location = new System.Drawing.Point(6, 66);
			this.txtPublic.Multiline = true;
			this.txtPublic.Name = "txtPublic";
			this.txtPublic.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtPublic.Size = new System.Drawing.Size(270, 90);
			this.txtPublic.TabIndex = 1;
			this.txtPublic.Text = "";
			// 
			// txtPrivate
			// 
			this.txtPrivate.Location = new System.Drawing.Point(6, 198);
			this.txtPrivate.Multiline = true;
			this.txtPrivate.Name = "txtPrivate";
			this.txtPrivate.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtPrivate.Size = new System.Drawing.Size(270, 102);
			this.txtPrivate.TabIndex = 2;
			this.txtPrivate.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 42);
			this.label1.Name = "label1";
			this.label1.TabIndex = 3;
			this.label1.Text = "Public";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 174);
			this.label2.Name = "label2";
			this.label2.TabIndex = 4;
			this.label2.Text = "Private";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(280, 302);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtPrivate);
			this.Controls.Add(this.txtPublic);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Key Generator";
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
			// creates the CspParameters object and sets the key container name used to store the RSA key pair
			CspParameters cp = new CspParameters(1);
			//cp.Flags = CspProviderFlags.UseMachineKeyStore;
			//cp.KeyContainerName = "GSI.Licensing.LicenseManager.KeyPair";
			//cp.KeyNumber = 2;

			// instantiates the rsa instance accessing the key container MyKeyContainerName
			RSA key = new RSACryptoServiceProvider(cp);			
			
			// Save private key pair
			this.txtPrivate.Text = key.ToXmlString(true);

			// Save public key
			this.txtPublic.Text = key.ToXmlString(false);
		}
	}
}
