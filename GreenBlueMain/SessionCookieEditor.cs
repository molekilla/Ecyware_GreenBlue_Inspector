using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Net;
using Ecyware.GreenBlue.Controls;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Summary description for SessionCookieEditor.
	/// </summary>
	public class SessionCookieEditor : BaseSessionDesignerUserControl			
	{
		internal event UpdateSessionRequestEventHandler UpdateSessionRequestEvent;
		private CookieCollection _cookies = null;
		private Ecyware.GreenBlue.Controls.FlatPropertyGrid cookieProperties;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new SessionCookieEditor.
		/// </summary>
		public SessionCookieEditor()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}

		/// <summary>
		/// Creates a new SessionCookieEditor.
		/// </summary>
		/// <param name="cookies"></param>
		private SessionCookieEditor(CookieCollection cookies)
		{
			this.Cookies = cookies;
			this.DisplayCookies();
		}

		/// <summary>
		/// Gets or sets the cookies.
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
		/// Displays a message indicating that no data could be showed.
		/// </summary>
		public void DisplayNoDataMessage()
		{
			this.cookieProperties.SelectedObject = null;
		}

		private CookieCollection GetCookies()
		{
			PropertyTable bag = (PropertyTable)this.cookieProperties.SelectedObject;

			CookieCollection editedCookies = new CookieCollection();

			foreach ( Cookie cky in this.Cookies )
			{
				CookieWrapper cookieWrapper = (CookieWrapper)bag[cky.Name];
				editedCookies.Add(cookieWrapper.GetCookie());
			}

			return editedCookies;
		}

		/// <summary>
		/// Displays the cookies.
		/// </summary>
		public void DisplayCookies()
		{
			PropertyTable bag = new PropertyTable();
			string category = "Cookies";

			foreach ( Cookie cookie in this.Cookies )
			{
				PropertySpec nameItem = new PropertySpec(cookie.Name,typeof(CookieWrapper),category,"Cookie");
				nameItem.ConverterTypeName = "Ecyware.GreenBlue.Controls.CookieWrapper";
				
				PropertySpec[] items = {nameItem};
				bag.Properties.AddRange(items);

				// add values
				bag[cookie.Name] = new CookieWrapper(cookie);
			}

			this.cookieProperties.SelectedObject = bag;
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
			this.cookieProperties = new Ecyware.GreenBlue.Controls.FlatPropertyGrid();
			this.SuspendLayout();
			// 
			// cookieProperties
			// 
			this.cookieProperties.CommandsVisibleIfAvailable = true;
			this.cookieProperties.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cookieProperties.LargeButtons = false;
			this.cookieProperties.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.cookieProperties.Location = new System.Drawing.Point(0, 0);
			this.cookieProperties.Name = "cookieProperties";
			this.cookieProperties.Size = new System.Drawing.Size(438, 354);
			this.cookieProperties.TabIndex = 8;
			this.cookieProperties.Text = "PropertyGrid";
			this.cookieProperties.ViewBackColor = System.Drawing.SystemColors.Window;
			this.cookieProperties.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// SessionCookieEditor
			// 
			this.Controls.Add(this.cookieProperties);
			this.Name = "SessionCookieEditor";
			this.Size = new System.Drawing.Size(438, 354);
			this.Leave += new System.EventHandler(this.SessionCookieEditor_Leave);
			this.ResumeLayout(false);

		}
		#endregion

		private void SessionCookieEditor_Leave(object sender, System.EventArgs e)
		{
		}

		public override void UpdateSessionRequestData()
		{
			UpdateSessionRequestEventArgs args = new UpdateSessionRequestEventArgs();
			args.UpdateType = UpdateSessionRequestType.Cookies;
			args.Cookies = GetCookies();

			this.UpdateSessionRequestEvent(this, args);
		}
	}
}
