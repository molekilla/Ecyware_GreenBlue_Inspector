using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private AxSHDocVw.AxWebBrowser web;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(Form1));
			this.web = new AxSHDocVw.AxWebBrowser();
			((System.ComponentModel.ISupportInitialize)(this.web)).BeginInit();
			this.SuspendLayout();
			// 
			// web
			// 
			this.web.Dock = System.Windows.Forms.DockStyle.Fill;
			this.web.Enabled = true;
			this.web.Location = new System.Drawing.Point(0, 0);
			this.web.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("web.OcxState")));
			this.web.Size = new System.Drawing.Size(292, 266);
			this.web.TabIndex = 10;
			this.web.BeforeNavigate2 += new AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2EventHandler(this.web_BeforeNavigate2);
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(292, 266);
			this.Controls.Add(this.web);
			this.Name = "Form1";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.web)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void web_BeforeNavigate2(object sender, AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2Event e)
		{
			if ( e.postData != null )
			{
				this.postData = (byte[])e.postData;
			}
		}
	}
}
