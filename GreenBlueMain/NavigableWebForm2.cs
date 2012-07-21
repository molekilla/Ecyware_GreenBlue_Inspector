// All rights reserved.
// Title: GreenBlue Project
// Author(s): Rogelio Morrell C.
// Date: November 2003
// Add additional authors here
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Html;
using System.Data;
using System.Text;
using System.Net;
using Ecyware.GreenBlue.Controls;
using Ecyware.GreenBlue.Protocols.Http;
using mshtml;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public sealed class NavigableWebForm2 : BasePluginForm
	{

		static bool isLinkNavigation = true;
		static bool allowNavigate = false;
		static bool allowPostContinue = false;
		private int postContinueCount = 0;
		private bool _pendingRedirection = false;
		
		delegate void OnCompleteEventHandler(IHTMLDocument2 htmlDoc);
		delegate void OnPostEventHandler(IHTMLDocument2 htmlDoc, byte[] postData);

		//internal event InspectorRequestCompleteEventHandler CompleteEvent;
		internal event InspectorStartRequestEventHandler StartEvent;
		internal event InspectorCancelRequestEventHandler CancelEvent;
		internal event StartProgressBarEventHandler StartProgressBarEvent;

		internal event OnFormMappingEventHandler FormMappingEvent;

		// HTML Document
		private IHTMLDocument2 htmlDocument;
		private ArrayList formEvents = new ArrayList();
		private System.Windows.Forms.Html.HtmlControl web;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Opens up a new web browser window.
		/// </summary>
		public NavigableWebForm2()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Navigate("about:blank");

			
//			while (this.web.ReadyState != System.Windows.Forms.Html.Interop.READYSTATE.READYSTATE_COMPLETE )
//			{
//				Application.DoEvents();
//			}

//			this.web.NavigateComplete += new System.Windows.Forms.Html.BrowserNavigateEventHandler(web_NavigateComplete2);
//			this.web.DocumentComplete  += new AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEventHandler(DocumentComplete);
//			this.web.BeforeNavigate2 +=new AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2EventHandler(web_BeforeNavigate2);
//			this.web.NewWindow2 += new AxSHDocVw.DWebBrowserEvents2_NewWindow2EventHandler(web_NewWindow2);
			//this.web.NavigateError += new AxSHDocVw.DWebBrowserEvents2_NavigateErrorEventHandler(web_NavigateError);

			//			Navigate("http://www.ecyware.com");

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(NavigableWebForm2));
			this.web = new System.Windows.Forms.Html.HtmlControl();
			((System.ComponentModel.ISupportInitialize)(this.web)).BeginInit();
			this.SuspendLayout();
			// 
			// web
			// 
			this.web.ActiveXEnabled = false;
			this.web.AllowInPlaceNavigation = false;
			this.web.BackroundSoundEnabled = false;
			this.web.Body = "";
			this.web.Border3d = true;
			this.web.Dock = System.Windows.Forms.DockStyle.Fill;
			this.web.Enabled = true;
			this.web.FlatScrollBars = true;
			this.web.Html = "";
			this.web.ImagesDownloadEnabled = true;
			this.web.JavaEnabled = false;
			this.web.Location = new System.Drawing.Point(3, 3);
			this.web.Name = "web";
			this.web.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("web.OcxState")));
			this.web.ScriptEnabled = false;
			this.web.ScriptObject = null;
			this.web.ScrollBarsEnabled = true;
			this.web.Size = new System.Drawing.Size(612, 294);
			this.web.TabIndex = 9;
			this.web.VideoEnabled = false;
			// 
			// NavigableWebForm2
			// 
			this.Controls.Add(this.web);
			this.DockPadding.All = 3;
			this.Name = "NavigableWebForm2";
			this.Size = new System.Drawing.Size(618, 300);
			((System.ComponentModel.ISupportInitialize)(this.web)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Opens up a new web browser window and navigates to uri.
		/// </summary>
		/// <param name="uri"> The uri to navigate to.</param>
		public NavigableWebForm2(string uri) : this()
		{
			Application.DoEvents();
			Navigate(uri);
		}

		// Fires when the browser ActiveX control finishes loading a document		
		private void OnComplete(IHTMLDocument2 htmlDoc)
		{	
			isLinkNavigation = true;
			allowNavigate = false;
			allowPostContinue = false;
		}

		private void OnPost(IHTMLDocument2 htmlDoc, byte[] postData)
		{	
			//this.textBox2.Text = ((IHTMLDocument3)htmlDoc).documentElement.outerHTML;
			//cmbUrl.Text = htmlDocument.location.href;
			//MessageBox.Show("We are posting here");			
			FormMappingEventArgs args = new FormMappingEventArgs();
			args.FormCount = htmlDoc.forms.length;
			args.PostData = System.Text.Encoding.ASCII.GetString((byte[])postData);
			FormMappingEvent(this,args);
		}


		#region Events
		// Fires when the document has completely loaded. If you don't attempt to assign
		// htmlDocActiveX somewhere other than this handler you will get a null object reference 
		// if a document has not been completely loaded.
		private void DocumentComplete(object sender, AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent e)
		{
//			if ( this.web.ReadyState == SHDocVw.tagREADYSTATE.READYSTATE_COMPLETE )
//			{
//				htmlDocument =(IHTMLDocument2) web.Document;
//				this.Invoke(new OnCompleteEventHandler(OnComplete),new Object[] {htmlDocument});
//			} 
		}

		private void web_NavigateComplete2(object sender, AxSHDocVw.DWebBrowserEvents2_NavigateComplete2Event e)
		{			
			// Occurs after BeforeNavigate2, here we can get the LocationUrl. 
			// Calls GoUrl for editor request.
			if ( allowNavigate )
			{
				allowNavigate = false;
			}
		}

		// BeforeNavigate event used for post data mapping to forms editor.
		private void web_BeforeNavigate2(object sender, AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2Event e)
		{
			if ( allowNavigate )
			{
				#region User Action Code
				if ( e.postData != null )
				{
					if ( allowPostContinue )
					{
						// Posting here
						htmlDocument =(IHTMLDocument2) web.Document;
						this.Invoke(new OnPostEventHandler(OnPost),new Object[] {htmlDocument,(byte[])e.postData});
						e.cancel = false;
					}
				}
				else 
				{
					#region Link Navigation
					if ( isLinkNavigation )
					{
						// link navigation, cancel and let GBWorkspace handle it
						RequestGetEventArgs args = new RequestGetEventArgs();
						args.Url = (string)e.uRL;
						this.StartEvent(this, args);
						e.cancel=true;
					}
					#endregion
				}
				#endregion
			} 
			else 
			{
				#region Non User Action Code
				if ( e.postData != null )
				{
					if ( !PendingRedirection )
					{
						isLinkNavigation = false;
						htmlDocument =(IHTMLDocument2) web.Document;
						this.Invoke(new OnPostEventHandler(OnPost),new Object[] {htmlDocument,(byte[])e.postData});
						e.cancel = false;
					} 
					else 
					{
						e.cancel = true;
					}
				}
				else
				{
					if ( (isLinkNavigation) && (!PendingRedirection) )
					{
						// link navigation, cancel and let GBWorkspace handle it
						RequestGetEventArgs args = new RequestGetEventArgs();
						args.Url = (string)e.uRL;
						this.StartEvent(this, args);
						e.cancel=true;
						isLinkNavigation = false;
					}
				}
				#endregion
			}
		}

		/// <summary>
		/// Web Browser POST request.
		/// </summary>
		/// <param name="url"> URL to query.</param>
		/// <param name="postData"> Post Data in bytes.</param>
		/// <param name="cookies"> Cookie collection.</param>
		public void PostForm(string url, byte[] postData, CookieCollection cookies)
		{
			allowNavigate = true;
			allowPostContinue = true;
			isLinkNavigation = false;

			object flags = new Object();
			object targetFrame = new Object();
			object data = postData;
			object headers = new Object();

			//this.web.Navigate(url,ref flags,ref targetFrame, ref data, ref headers);
		}


		/// <summary>
		/// Web Browser GET Request.
		/// </summary>
		/// <param name="url"> URL to query.</param>
		/// <param name="cookies"> Cookie collection.</param>
		public void Navigate(string url, CookieCollection cookies)
		{
			allowNavigate = true;
			isLinkNavigation = false;
			object flags = new Object();
			object targetFrame = new Object();
			object postData = new Object();
			object headers = new Object();		

			//this.web.Navigate(url,ref flags,ref targetFrame, ref postData, ref headers);
		}

		/// <summary>
		/// Web Browser GET Request.
		/// </summary>
		/// <param name="url"> URL to query.</param>
		public void Navigate(string url)
		{
			allowNavigate = true;
			isLinkNavigation = false;
			object flags = new Object();
			object targetFrame = new Object();
			object postData = new Object();
			object headers = new Object();
			
			//this.web.Navigate(url,ref flags,ref targetFrame, ref postData, ref headers);
		}

		private void web_NavigateError(object sender, AxSHDocVw.DWebBrowserEvents2_NavigateErrorEvent e)
		{
			MessageBox.Show((string)e.statusCode);
		}

		private void web_NewWindow2(object sender, AxSHDocVw.DWebBrowserEvents2_NewWindow2Event e)
		{
//			// TODO: Later
//			NavigableWebForm navForm = new NavigableWebForm();			 
//			navForm.web.RegisterAsBrowser = true;
//
//			e.ppDisp = navForm.web.Application;
//			navForm.web.Visible = true;
		}
		#endregion

		private void SetIECookie(IHTMLDocument2 doc, CookieCollection cookies)
		{
			foreach ( DictionaryEntry de in cookies )
			{
				Cookie cookie = (Cookie)de.Value;
				doc.cookie = cookie.Name + "=" + cookie.Value;
			}
		}

		private string ConvertToIECookie(CookieCollection cookies)
		{
			StringBuilder cookieString = new StringBuilder();
			foreach ( DictionaryEntry de in cookies )
			{
				Cookie cookie = (Cookie)de.Value;
				cookieString.AppendFormat("{0}={1};",cookie.Name,cookie.Value);
			}

			return cookieString.ToString();
		}

		public bool PendingRedirection
		{
			get
			{
				return _pendingRedirection;
			}
			set
			{
				_pendingRedirection = value;
			}
		}
	}
}
