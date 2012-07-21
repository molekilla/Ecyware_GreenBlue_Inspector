using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Summary description for LicenseInvalidSplashScreen.
	/// </summary>
	public class LicenseInvalidSplashScreen : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox splashScreenImage;
		private System.Windows.Forms.Label lblCopyright;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.LinkLabel txtLink;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnOk;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new LicenseInvalidSplashScreen.
		/// </summary>
		public LicenseInvalidSplashScreen()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.Size = this.splashScreenImage.Size;
			this.lblVersion.Text += " " + Application.ProductVersion;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(LicenseInvalidSplashScreen));
			this.splashScreenImage = new System.Windows.Forms.PictureBox();
			this.lblCopyright = new System.Windows.Forms.Label();
			this.lblVersion = new System.Windows.Forms.Label();
			this.txtLink = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// splashScreenImage
			// 
			this.splashScreenImage.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splashScreenImage.Image = ((System.Drawing.Image)(resources.GetObject("splashScreenImage.Image")));
			this.splashScreenImage.Location = new System.Drawing.Point(0, 0);
			this.splashScreenImage.Name = "splashScreenImage";
			this.splashScreenImage.Size = new System.Drawing.Size(400, 315);
			this.splashScreenImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.splashScreenImage.TabIndex = 1;
			this.splashScreenImage.TabStop = false;
			// 
			// lblCopyright
			// 
			this.lblCopyright.BackColor = System.Drawing.Color.White;
			this.lblCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCopyright.Location = new System.Drawing.Point(6, 270);
			this.lblCopyright.Name = "lblCopyright";
			this.lblCopyright.Size = new System.Drawing.Size(192, 36);
			this.lblCopyright.TabIndex = 4;
			this.lblCopyright.Text = "This program is protected by U.S. and international law as described in the about" +
				" box. Copyright© Ecyware Solutions 2003-2004.";
			// 
			// lblVersion
			// 
			this.lblVersion.BackColor = System.Drawing.Color.White;
			this.lblVersion.Location = new System.Drawing.Point(168, 74);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(150, 18);
			this.lblVersion.TabIndex = 5;
			this.lblVersion.Text = "Version";
			// 
			// txtLink
			// 
			this.txtLink.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
			this.txtLink.BackColor = System.Drawing.Color.White;
			this.txtLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtLink.Location = new System.Drawing.Point(264, 168);
			this.txtLink.Name = "txtLink";
			this.txtLink.Size = new System.Drawing.Size(126, 18);
			this.txtLink.TabIndex = 6;
			this.txtLink.TabStop = true;
			this.txtLink.Text = "http://www.ecyware.com";
			this.txtLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.txtLink_LinkClicked);
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.White;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 168);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(252, 18);
			this.label1.TabIndex = 7;
			this.label1.Text = "Your license key has expired. Please contact us at ";
			// 
			// btnOk
			// 
			this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOk.Location = new System.Drawing.Point(312, 222);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 8;
			this.btnOk.Text = "OK";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// LicenseInvalidSplashScreen
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(424, 332);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtLink);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.lblCopyright);
			this.Controls.Add(this.splashScreenImage);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "LicenseInvalidSplashScreen";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "LicenseInvalidSplashScreen";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			this.Close();		
		}

		private void txtLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			System.Diagnostics.Process process = new System.Diagnostics.Process();
			process.StartInfo.UseShellExecute = true;
			process.StartInfo.FileName = "http://www.ecyware.com";
			process.Start();
		}
	}
}
