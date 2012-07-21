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
	/// Contains the definition for the about window.
	/// </summary>
	public class AboutWindow : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox splashScreenImage;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.Label lblCopyright;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button btnOk;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new AboutWindow.
		/// </summary>
		public AboutWindow()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			this.Size = this.splashScreenImage.Size;
			this.lblVersion.Text += " " + Application.ProductVersion;

			this.textBox1.Text = "Copyright© Ecyware Solutions 2003-2005.\r\n";
			this.textBox1.Text += "All rights reserved.\r\n";
			this.textBox1.Text += "Contains controls by Tim Dawson (Document Manager).\r\n";
			this.textBox1.Text += "Contains controls by Tim Anderson (HTML Editor).\r\n";
			this.textBox1.Text += "Contains an implementation of SgmlReader by Chris Lovett. \r\n";
			this.textBox1.Text += "Contains an implementation of CommandBar by Lutz Roeder. \r\n";

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AboutWindow));
			this.splashScreenImage = new System.Windows.Forms.PictureBox();
			this.lblVersion = new System.Windows.Forms.Label();
			this.lblCopyright = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
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
			this.splashScreenImage.TabIndex = 0;
			this.splashScreenImage.TabStop = false;
			// 
			// lblVersion
			// 
			this.lblVersion.BackColor = System.Drawing.Color.White;
			this.lblVersion.Location = new System.Drawing.Point(168, 74);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(150, 18);
			this.lblVersion.TabIndex = 1;
			this.lblVersion.Text = "Version";
			// 
			// lblCopyright
			// 
			this.lblCopyright.BackColor = System.Drawing.Color.White;
			this.lblCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCopyright.Location = new System.Drawing.Point(6, 270);
			this.lblCopyright.Name = "lblCopyright";
			this.lblCopyright.Size = new System.Drawing.Size(192, 36);
			this.lblCopyright.TabIndex = 3;
			this.lblCopyright.Text = "This program is protected by U.S. and international law as described in the about" +
				" box. Copyright© Ecyware Solutions 2003-2006.";
			// 
			// textBox1
			// 
			this.textBox1.AcceptsReturn = true;
			this.textBox1.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.textBox1.Location = new System.Drawing.Point(90, 144);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(288, 72);
			this.textBox1.TabIndex = 4;
			this.textBox1.Text = "";
			// 
			// btnOk
			// 
			this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOk.Location = new System.Drawing.Point(300, 222);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 9;
			this.btnOk.Text = "OK";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click_1);
			// 
			// AboutWindow
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(420, 336);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.lblCopyright);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.splashScreenImage);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimizeBox = false;
			this.Name = "AboutWindow";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOk_Click_1(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
