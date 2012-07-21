// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: July 2004
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using Ecyware.GreenBlue.Controls;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Contains the definition for the CookieEditorDialog type.
	/// </summary>
	public class CookieEditorDialog : System.Windows.Forms.Form
	{
		private CookieCollection _cookies = null;

		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private Ecyware.GreenBlue.Controls.FlatPropertyGrid cookieProperties;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new Cookie editor dialog.
		/// </summary>
		public CookieEditorDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}
		private void button1_Click(object sender, System.EventArgs e)
		{
			PropertyTable bag = (PropertyTable)this.cookieProperties.SelectedObject;

			CookieCollection editedCookies = new CookieCollection();

			foreach ( Cookie cky in this.Cookies )
			{
				CookieWrapper cookieWrapper = (CookieWrapper)bag[cky.Name];
				editedCookies.Add(cookieWrapper.GetCookie());
			}

			this.Cookies = editedCookies;
		}
		/// <summary>
		/// Adds the cookie collection to the property grid.
		/// </summary>
		/// <param name="cookies"> The cookies.</param>
		public void SetCookies(CookieCollection cookies)
		{
			// Set current cookies
			Cookies = cookies;

			PropertyTable bag = new PropertyTable();
			// bag.GetValue += new PropertySpecEventHandler(bag_GetValue);
			// bag.SetValue += new PropertySpecEventHandler(bag_SetValue);
			string category = "Cookies";

			foreach ( Cookie cookie in cookies )
			{
				PropertySpec nameItem = new PropertySpec(cookie.Name,typeof(CookieWrapper),category,"Cookie");
				nameItem.ConverterTypeName = "Ecyware.GreenBlue.Controls.CookieWrapper";
				
				PropertySpec[] items = {nameItem};
				bag.Properties.AddRange(items);

//				// add values
				bag[cookie.Name] = new CookieWrapper(cookie);
			}

			this.cookieProperties.SelectedObject = bag;
		}

		/// <summary>
		/// Gets or sets the cookies
		/// </summary>
		public CookieCollection Cookies
		{
			get
			{
				return _cookies;
			}
			set
			{
				_cookies = value;
			}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CookieEditorDialog));
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.cookieProperties = new Ecyware.GreenBlue.Controls.FlatPropertyGrid();
			this.SuspendLayout();
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button2.Location = new System.Drawing.Point(316, 240);
			this.button2.Name = "button2";
			this.button2.TabIndex = 7;
			this.button2.Text = "Cancel";
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Location = new System.Drawing.Point(235, 240);
			this.button1.Name = "button1";
			this.button1.TabIndex = 6;
			this.button1.Text = "Save";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// cookieProperties
			// 
			this.cookieProperties.CommandsVisibleIfAvailable = true;
			this.cookieProperties.LargeButtons = false;
			this.cookieProperties.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.cookieProperties.Location = new System.Drawing.Point(0, 0);
			this.cookieProperties.Name = "cookieProperties";
			this.cookieProperties.Size = new System.Drawing.Size(396, 228);
			this.cookieProperties.TabIndex = 0;
			this.cookieProperties.Text = "PropertyGrid";
			this.cookieProperties.ToolbarVisible = false;
			this.cookieProperties.ViewBackColor = System.Drawing.SystemColors.Window;
			this.cookieProperties.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// CookieEditorDialog
			// 
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(394, 268);
			this.Controls.Add(this.cookieProperties);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "CookieEditorDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Cookie Editor Dialog";
			this.ResumeLayout(false);

		}
		#endregion


	}
}
