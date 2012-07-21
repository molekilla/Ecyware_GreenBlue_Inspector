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
	/// Contains the definition for the StartSplashScreen type.
	/// </summary>
	public class StartSplashScreen : System.Windows.Forms.Form
	{
		private System.Windows.Forms.PictureBox splashScreenImage;
		private System.Windows.Forms.Label lblVersion;
		internal System.Windows.Forms.Label lblLoadingComponents;
		private System.Windows.Forms.Label lblCopyright;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Contains the definition for the StartSplashScreen.
		/// </summary>
		public StartSplashScreen()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StartSplashScreen));
			this.splashScreenImage = new System.Windows.Forms.PictureBox();
			this.lblVersion = new System.Windows.Forms.Label();
			this.lblLoadingComponents = new System.Windows.Forms.Label();
			this.lblCopyright = new System.Windows.Forms.Label();
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
			// lblLoadingComponents
			// 
			this.lblLoadingComponents.BackColor = System.Drawing.Color.White;
			this.lblLoadingComponents.Location = new System.Drawing.Point(96, 168);
			this.lblLoadingComponents.Name = "lblLoadingComponents";
			this.lblLoadingComponents.Size = new System.Drawing.Size(288, 18);
			this.lblLoadingComponents.TabIndex = 2;
			this.lblLoadingComponents.Text = "Loading...";
			// 
			// lblCopyright
			// 
			this.lblCopyright.BackColor = System.Drawing.Color.White;
			this.lblCopyright.Font = new System.Drawing.Font("Microsoft Sans Serif", 5.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCopyright.Location = new System.Drawing.Point(6, 282);
			this.lblCopyright.Name = "lblCopyright";
			this.lblCopyright.Size = new System.Drawing.Size(192, 24);
			this.lblCopyright.TabIndex = 3;
			this.lblCopyright.Text = "This program is protected by U.S. and international law as described in the about" +
				" box. Copyright© Ecyware Solutions 2003-2005.";
			// 
			// StartSplashScreen
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(420, 336);
			this.Controls.Add(this.lblCopyright);
			this.Controls.Add(this.lblLoadingComponents);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.splashScreenImage);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimizeBox = false;
			this.Name = "StartSplashScreen";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
