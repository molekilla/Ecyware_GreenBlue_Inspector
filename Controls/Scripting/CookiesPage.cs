// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: June 2004 - July 2004
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Ecyware.GreenBlue.Controls;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Engine.HtmlCommand;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.Scripting;

namespace Ecyware.GreenBlue.Controls.Scripting
{
	/// <summary>
	/// Contains the definition for the ScriptingMainPage control.
	/// </summary>
	public class CookiesPage : BaseScriptingDataPage
	{
		private System.Windows.Forms.GroupBox grpCookies;
		private Ecyware.GreenBlue.Controls.FlatPropertyGrid pgCookies;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new SessionRequestHeaderEditor
		/// </summary>
		public CookiesPage()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(CookiesPage));
			this.grpCookies = new System.Windows.Forms.GroupBox();
			this.pgCookies = new Ecyware.GreenBlue.Controls.FlatPropertyGrid();
			this.grpCookies.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpCookies
			// 
			this.grpCookies.Controls.Add(this.pgCookies);
			this.grpCookies.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpCookies.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpCookies.Location = new System.Drawing.Point(0, 0);
			this.grpCookies.Name = "grpCookies";
			this.grpCookies.Size = new System.Drawing.Size(600, 400);
			this.grpCookies.TabIndex = 4;
			this.grpCookies.TabStop = false;
			this.grpCookies.Text = "Cookies";
			// 
			// pgCookies
			// 
			this.pgCookies.CommandsVisibleIfAvailable = true;
			this.pgCookies.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pgCookies.LargeButtons = false;
			this.pgCookies.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.pgCookies.Location = new System.Drawing.Point(3, 16);
			this.pgCookies.Name = "pgCookies";
			this.pgCookies.Size = new System.Drawing.Size(594, 381);
			this.pgCookies.TabIndex = 9;
			this.pgCookies.Text = "PropertyGrid";
			this.pgCookies.ViewBackColor = System.Drawing.SystemColors.Window;
			this.pgCookies.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// CookiesPage
			// 
			this.Controls.Add(this.grpCookies);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "CookiesPage";
			this.Size = new System.Drawing.Size(600, 400);
			this.grpCookies.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		/// <summary>
		/// Loads the request.
		/// </summary>
		/// <param name="request"></param>
		public override void LoadRequest(int index, ScriptingApplication scripting ,Ecyware.GreenBlue.Engine.Scripting.WebRequest request)
		{
			base.LoadRequest (index, scripting, request);
			this.SetCookies(request.Cookies);
		}

		/// <summary>
		/// Gets or sets the WebRequest.
		/// </summary>
		public override Ecyware.GreenBlue.Engine.Scripting.WebRequest WebRequest
		{
			get
			{
				if ( base.WebRequest != null )
				{
					UpdateCookies(base.WebRequest);
					//UpdateHttpPropertiesFromPropertyGrid(base.WebRequest);
				}

				return base.WebRequest;
			}
		}

		private void UpdateCookies(Ecyware.GreenBlue.Engine.Scripting.WebRequest request)
		{
			PropertyTable bag = (PropertyTable)this.pgCookies.SelectedObject;

			Ecyware.GreenBlue.Engine.Scripting.Cookies editedCookies = new Ecyware.GreenBlue.Engine.Scripting.Cookies();
			
			foreach ( Ecyware.GreenBlue.Engine.Scripting.Cookie cky in request.Cookies)
			{
				CookieWrapperExtended cookieWrapper = (CookieWrapperExtended)bag[cky.Name];
				editedCookies.CookieList().Add(cookieWrapper.GetCookie());				
			}

			request.ClearCookies();
			request.Cookies = editedCookies.GetCookies();
		}

		/// <summary>
		/// Adds the cookie collection to the property grid.
		/// </summary>
		/// <param name="cookies"> The cookies.</param>
		private void SetCookies(Ecyware.GreenBlue.Engine.Scripting.Cookie[] cookies)
		{
			PropertyTable bag = new PropertyTable();
			bag.Properties.Clear();
			// bag.GetValue += new PropertySpecEventHandler(bag_GetValue);
			// bag.SetValue += new PropertySpecEventHandler(bag_SetValue);
			string category = "Cookies";

			foreach ( Ecyware.GreenBlue.Engine.Scripting.Cookie cookie in cookies )
			{
				PropertySpec nameItem = new PropertySpec(cookie.Name,typeof(CookieWrapper),category,"Cookie");
				nameItem.ConverterTypeName = "Ecyware.GreenBlue.Controls.CookieWrapperExtended";
				
				PropertySpec[] items = {nameItem};
				bag.Properties.AddRange(items);

				// add values
				bag[cookie.Name] = new CookieWrapperExtended(cookie);
			}

			this.pgCookies.SelectedObject = bag;
		}

	
	}
}
