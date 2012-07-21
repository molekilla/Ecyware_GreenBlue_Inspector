// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: November 2003-June 2004
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Text;
using System.Net;
using Ecyware.GreenBlue.Controls;
using Ecyware.GreenBlue.Protocols.Http;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Engine.HtmlCommand;
using mshtml;
using Ecyware.GreenBlue.Engine;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Handles the new window events.
	/// </summary>
	public delegate void NewWindowEventHandler(object sender, NavigableWebForm webForm);

	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public sealed class NavigableWebForm : BasePluginForm
	{
		private bool isLinkNavigation = true;
		private bool isNewWindow = false;
		private bool _canNewWindow = true;
		
		private InspectorAction inspectorState;

		delegate void OnQueryStringMatchEventHandler(string query);
		delegate void OnCompleteEventHandler(IHTMLDocument2 htmlDoc);
		delegate void OnPostEventHandler(HTMLFormElementClass form, IHTMLDocument2 htmlDoc, byte[] postData);
		delegate void OnPostDataMatchEventHandler(byte[] postData);

		internal event NewWindowEventHandler NewWindowEvent;
		internal event InspectorStartRequestEventHandler StartEvent;
		//internal event InspectorCancelRequestEventHandler CancelEvent;

		// Events to InspectorWorkspace
		internal event OnFormConvertionEventHandler FormConvertionEvent;
		internal event OnFormHeuristicEventHandler FormHeuristicEvent;
		internal event LoadFormsEditorEventHandler LoadFormsEditorEvent;
		internal event LoadLinksEventHandler LoadLinksEvent;
		internal event OnLoadHtmlDocumentEventHandler LoadDocumentEvent;
		internal event EventHandler DisplayBrowserUrlSync;

		// private
		HtmlFormTagCollection formCollection = null;
		byte[] postData = null;
		private HtmlFormTag _getformTag;

		// HTML Document
		private IHTMLDocument2 htmlDocument;
		private ArrayList formEvents = new ArrayList();
		private AxSHDocVw.AxWebBrowser web;
		private FormHeuristic formHeuristicEngine = new FormHeuristic();
		private string targetFrame = string.Empty;
		private string requestedUrl = string.Empty;

		//private int laps=0;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.ComponentModel.IContainer components;

		private int navigateCount = 0;

		/// <summary>
		/// Opens up a new web browser window.
		/// </summary>
		public NavigableWebForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			requestedUrl = string.Empty;
			Navigate("about:blank");

			while (this.web.ReadyState != SHDocVw.tagREADYSTATE.READYSTATE_COMPLETE )
			{
				Application.DoEvents();
			}

			this.web.NavigateComplete2 +=new AxSHDocVw.DWebBrowserEvents2_NavigateComplete2EventHandler(web_NavigateComplete2);
			this.web.DocumentComplete  += new AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEventHandler(DocumentComplete);
			this.web.BeforeNavigate2 +=new AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2EventHandler(web_BeforeNavigate2);
			this.web.NewWindow2 += new AxSHDocVw.DWebBrowserEvents2_NewWindow2EventHandler(web_NewWindow2);
			this.web.NavigateError += new AxSHDocVw.DWebBrowserEvents2_NavigateErrorEventHandler(web_NavigateError);
		}

		/// <summary>
		/// Opens up a new web browser window.
		/// </summary>
		/// <param name="isNewWindow"> The new window flag.</param>
		public NavigableWebForm(bool isNewWindow) : this()
		{
			this.isNewWindow = isNewWindow;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(NavigableWebForm));
			this.web = new AxSHDocVw.AxWebBrowser();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			((System.ComponentModel.ISupportInitialize)(this.web)).BeginInit();
			this.SuspendLayout();
			// 
			// web
			// 
			this.web.Dock = System.Windows.Forms.DockStyle.Fill;
			this.web.Enabled = true;
			this.web.Location = new System.Drawing.Point(3, 3);
			this.web.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("web.OcxState")));
			this.web.Size = new System.Drawing.Size(612, 294);
			this.web.TabIndex = 9;
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1,
																						 this.menuItem2});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Test";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "Test 2";
			// 
			// NavigableWebForm
			// 
			this.Controls.Add(this.web);
			this.DockPadding.All = 3;
			this.Name = "NavigableWebForm";
			this.Size = new System.Drawing.Size(618, 300);
			((System.ComponentModel.ISupportInitialize)(this.web)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Opens up a new web browser window and navigates to uri.
		/// </summary>
		/// <param name="uri"> The uri to navigate to.</param>
		public NavigableWebForm(string uri) : this()
		{
			Application.DoEvents();
			Navigate(uri);
		}

		#region OnComplete and OnPost Delegate Event
	 // Fires when the browser ActiveX control finishes loading a document		
		private void OnComplete(IHTMLDocument2 htmlDoc)
		{	
			// Adds some event handlers to the DOM Document
			AddEventsToDoc(htmlDoc);	
			
			formCollection = HtmlDomTransformation.TransformFormElements(htmlDoc, new Uri(htmlDoc.location.toString()));
			// FIX: This might be no longer neccesary because
			// it injects code into the document.
			//SetFormOnSubmit(htmlDoc);

			// check if it is NewWindow
			if ( isNewWindow )
			{
				RequestGetEventArgs args = new RequestGetEventArgs();
				args.Url = (string)web.LocationURL;
				args.InspectorRequestAction = InspectorAction.WebBrowserGet;
				this.StartEvent(this, args);

				isNewWindow = false;
			}

			// raise load links event
			if ( LoadLinksEvent != null )
			{
				LoadLinksEventArgs args = new LoadLinksEventArgs();
				args.Frames = HtmlDomTransformation.TransformFrameElements(htmlDoc);
				args.Anchors = HtmlDomTransformation.TransformAnchorElements(htmlDoc);
				args.Links = HtmlDomTransformation.TransformLinksElements(htmlDoc);
				LoadLinksEvent.BeginInvoke(this, args, new AsyncCallback(FireAndForget), null);
			}

			// raise load forms event
			if ( LoadFormsEditorEvent != null )
			{
				// Transform form elements to HTML DOM Document.
				LoadFormsEditorEvent.BeginInvoke(this, new LoadFormsEditorEventArgs(postData, formCollection), new AsyncCallback(FireAndForget),null);
			}

			// reset
			inspectorState = InspectorAction.Idle;
		}

		/// <summary>
		/// The fire and forget callback
		/// </summary>
		/// <param name="result"> The IAsyncResult.</param>
		private void FireAndForget(IAsyncResult result)
		{
			result.AsyncWaitHandle.WaitOne();
		}

		#endregion
		#region Post Matching
		/// <summary>
		/// Occurs when post data is called by javascript.
		/// </summary>
		/// <param name="form"></param>
		/// <param name="htmlDoc"></param>
		/// <param name="postData"></param>
		private void OnPost(HTMLFormElementClass form, IHTMLDocument2 htmlDoc, byte[] postData)
		{					
			FormConvertionArgs args = new FormConvertionArgs();
			args.FormElement = form;
			args.SiteUri = new Uri(this.web.LocationURL);
			args.PostData = System.Text.Encoding.UTF8.GetString((byte[])postData);
			// MessageBox.Show(args.PostData);
			FormConvertionEvent(this, args);
		}

		

		/// <summary>
		/// Occurs when post data is to match.
		/// </summary>
		/// <param name="data"></param>
		private void OnQueryStringSubmitMatch(string data)
		{
			try
			{
				// Check for any form
				HtmlFormTag formTag = formHeuristicEngine.MatchPostDataToForm(formCollection, data.TrimStart('?'));
			
				// if null, we know this is not a form get post.
				if ( formTag != null )
				{
					_getformTag = formTag;
				}
			}
			catch ( Exception ex )
			{
				MessageBox.Show(ex.ToString());
			}
		}

		/// <summary>
		/// Occurs when post data is to match.
		/// </summary>
		/// <param name="data"></param>
		private void OnPostDataSubmitMatch(byte[] data)
		{
			// another event not from Form
			string postData = System.Text.Encoding.UTF8.GetString((byte[])data);

			if ( postData.IndexOf("Content-Disposition: form-data") > -1 )
			{
				postData = formHeuristicEngine.FilterPostDataMultiPart(postData);
				HtmlFormTag formTag = formHeuristicEngine.MatchPostDataToForm(formCollection, postData);
				formTag = FormHeuristic.NormalizeFormTag(formTag, postData);
				
				FormHeuristicArgs args = new FormHeuristicArgs();
				args.FormTag = formTag;
				args.PostData = postData;
				args.SiteUri = new Uri(this.web.LocationURL);
				FormHeuristicEvent(this, args);
			} 
			else 
			{
				HtmlFormTag formTag = formHeuristicEngine.MatchPostDataToForm(formCollection, postData);
				
				FormHeuristicArgs args = new FormHeuristicArgs();
				args.FormTag = formTag;
				args.PostData = postData;
				args.SiteUri = new Uri(this.web.LocationURL);
				FormHeuristicEvent(this, args);
			}
		}
		#endregion
		#region Events
		// Fires when the document has completely loaded. If you don't attempt to assign
		// htmlDocActiveX somewhere other than this handler you will get a null object reference 
		// if a document has not been completely loaded.
		private void DocumentComplete(object sender, AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEvent e)
		{
			navigateCount--;
//			if ( navigateCount <= 0 )
//			{
//				navigateCount = 0;
//				isLinkNavigation = true;
//				this.LoadDocumentEvent(this, new EventArgs());
//			}

			if ( this.web.ReadyState == SHDocVw.tagREADYSTATE.READYSTATE_COMPLETE )
			{				
				navigateCount = 0;
				isLinkNavigation = true;
				htmlDocument =(IHTMLDocument2)web.Document;
				
				this.DisplayBrowserUrlSync(this, EventArgs.Empty);
				this.LoadDocumentEvent(this, EventArgs.Empty);	
//
				//this.requestedUrl = htmlDocument.url;
				this.Invoke(new OnCompleteEventHandler(OnComplete), new Object[] {htmlDocument});
			} 
		}

		// Navigate complete 2 event
		private void web_NavigateComplete2(object sender, AxSHDocVw.DWebBrowserEvents2_NavigateComplete2Event e)
		{			
			// Occurs after BeforeNavigate2, here we can get the LocationUrl. 
			// Calls GoUrl for editor request.
			if ( navigateCount == 0 )
				requestedUrl = (string)e.uRL;

			navigateCount++;
		}

		// BeforeNavigate event used for post data mapping to forms editor.
		private void web_BeforeNavigate2(object sender, AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2Event e)
		{	
			if ( e.postData != null )
			{
				this.postData = (byte[])e.postData;

				Invoke(new OnPostDataMatchEventHandler(OnPostDataSubmitMatch),
							new Object[] {(byte[])e.postData});
			} 
			else 
			{
				string url = (string)e.uRL;
				if ( isLinkNavigation )
				{		
					Uri uri = new Uri(url);
					string queryString = uri.Query;

					if ( queryString.Length > 0 )
					{
						Invoke(new OnQueryStringMatchEventHandler(OnQueryStringSubmitMatch),
							new Object[] {queryString});
					}

					if ( (url.StartsWith("http:"))
						||
						(url.StartsWith("https:")) )
					{
						RequestGetEventArgs args = new RequestGetEventArgs();
						if ( _getformTag != null )
						{
							args.Form = _getformTag.CloneTag();
							_getformTag = null;
						}
						args.Url = (string)url;
						args.InspectorRequestAction = InspectorAction.WebBrowserGet;
						this.StartEvent(this, args);
					
						isLinkNavigation = false;						
					}
				} 
			}
		}

		#region Posting and Navigation
		/// <summary>
		/// Web Browser POST request.
		/// </summary>
		/// <param name="url"> URL to query.</param>
		/// <param name="postData"> Post Data in bytes.</param>
		/// <param name="cookies"> Cookie collection.</param>
		/// <param name="inspectorAction"> Inspector Action. </param>
		public void PostForm(string url, byte[] postData, CookieCollection cookies, InspectorAction inspectorAction)
		{
			isLinkNavigation = false;			
			inspectorState = inspectorAction;

			object flags = new Object();
			object targetFrame = new Object();
			object data = postData;
			object headers = "Content-Type: application/x-www-form-urlencoded" + "\n" + "\r";;

			this.web.Navigate(url,ref flags,ref targetFrame, ref data, ref headers);
		}


		/// <summary>
		/// Web Browser GET Request.
		/// </summary>
		/// <param name="url"> URL to query.</param>
		/// <param name="cookies"> Cookie collection.</param>
		/// <param name="inspectorAction"> Inspector Action.</param>
		public void Navigate(string url, CookieCollection cookies, InspectorAction inspectorAction)
		{
			isLinkNavigation = false;
			inspectorState = inspectorAction;
			object flags = new Object();
			object targetFrame = new Object();
			object postData = new Object();
			object headers = new Object();		

			this.web.Navigate(url,ref flags,ref targetFrame, ref postData, ref headers);
		}

		/// <summary>
		/// Web Browser GET Request.
		/// </summary>
		/// <param name="url"> URL to query.</param>
		public void Navigate(string url)
		{
			isLinkNavigation = false;
			object flags = new Object();
			object targetFrame = new Object();
			object postData = new Object();
			object headers = new Object();
			
			this.web.Navigate(url,ref flags,ref targetFrame, ref postData, ref headers);
		}

		/// <summary>
		/// Stops navigation.
		/// </summary>
		public void StopNavigation()
		{
			this.navigateCount = 0;
			requestedUrl = string.Empty;
			this.web.Stop();
		}
		#endregion
		private void web_NavigateError(object sender, AxSHDocVw.DWebBrowserEvents2_NavigateErrorEvent e)
		{
			//MessageBox.Show((string)e.statusCode);
			this.navigateCount = 0;
		}

		private void web_NewWindow2(object sender, AxSHDocVw.DWebBrowserEvents2_NewWindow2Event e)
		{

			if ( AllowNewWindow )
			{	
				// Create browser
				NavigableWebForm navForm2 = new NavigableWebForm(true); 
				navForm2.web.RegisterAsBrowser = false;

				e.ppDisp = navForm2.web.Application;
				navForm2.web.Visible = true;				

				// check isLinkNavigation to true
				isLinkNavigation = true;

				// add and remove current
				this.NewWindowEvent(this, navForm2);			
			}
		}

	#endregion
		#region Methods
//		private void SetIECookie(IHTMLDocument2 doc, CookieCollection cookies)
//		{
//			foreach ( DictionaryEntry de in cookies )
//			{
//				Cookie cookie = (Cookie)de.Value;
//				doc.cookie = cookie.Name + "=" + cookie.Value;
//			}
//		}
//
//		private string ConvertToIECookie(CookieCollection cookies)
//		{
//			StringBuilder cookieString = new StringBuilder();
//			foreach ( DictionaryEntry de in cookies )
//			{
//				Cookie cookie = (Cookie)de.Value;
//				cookieString.AppendFormat("{0}={1};",cookie.Name,cookie.Value);
//			}
//
//			return cookieString.ToString();
//		}

		private void AddEventsToDoc(IHTMLDocument2 htmlDoc)
		{
			// mshtml.FramesCollection frames = htmlDoc.frames;
			// mshtml.IHTMLFramesCollection2 frames = (mshtml.IHTMLFramesCollection2)htmlDoc.frames;
			// mshtml.HTMLFrameSiteEvents_Event frameEvents = (mshtml.HTMLFrameSiteEvents_Event)htmlDoc;
			//frameEvents.oncontextmenu += new HTMLFrameSiteEvents_oncontextmenuEventHandler(iEvent_oncontextmenu);

			// IHTMLDocument2 htmlDoc = (IHTMLDocument2)this.web.Document;

			// On Error Event
			mshtml.HTMLWindowEvents2_Event windowEvents 
				= (mshtml.HTMLWindowEvents2_Event)htmlDoc.parentWindow;
			windowEvents.onerror += new HTMLWindowEvents2_onerrorEventHandler(windowEvents_onerror);


			// OnClick and OnContextMenu events.
			// NOTE: This order is significant.
			mshtml.HTMLDocumentEvents2_Event iEvent 
				= (mshtml.HTMLDocumentEvents2_Event)htmlDoc;

			// TODO: Add Loading Blocker
			windowEvents.onload += new HTMLWindowEvents2_onloadEventHandler(windowEvents_onload);
			//iEvent.onclick += new HTMLDocumentEvents2_onclickEventHandler(iEvent_onclick);
			//iEvent.oncontextmenu += new HTMLDocumentEvents2_oncontextmenuEventHandler(iEvent_oncontextmenu);
		}


//		/// <summary>
//		/// Sets some form on submit event to GB Event.
//		/// </summary>
//		/// <param name="htmlDoc"> The HTML DOM Document to process.</param>
//		private void SetFormOnSubmit(IHTMLDocument2 htmlDoc)
//		{
//			StringBuilder code = new StringBuilder();
//
//			// Add Dummy Function
//			
//			// This is kind of override for submit
//			code.Append("function myGbEvent() { this.submit();return false; }");
//
//			code.Append("function checkForms()");
//			code.Append("{");
//			code.Append("for (i=0;i<document.forms.length;i++)");
//			code.Append("{");
//			// code.Append("alert(document.forms[i].id);");			
//			code.Append("var test = document.forms[i].onsubmit;");
//			code.Append("if ( test == null )");
//			code.Append("{");
//			code.Append("document.forms[i].onsubmit = myGbEvent;");
//			code.Append("}");
//			// code.Append("alert(document.forms[i].onsubmit);");
//			code.Append("}");
//			code.Append("}");
//			code.Append("checkForms();");
//
//			htmlDoc.parentWindow.execScript(code.ToString(), "javascript");
//		}

		#endregion
		#region DOM events
		// TODO: Fix this
		private bool iEvent_oncontextmenu(IHTMLEventObj pEvtObj)
		{
//			int x = pEvtObj.screenX - this.web.Location.X;
//			int y = pEvtObj.screenY - this.web.Location.Y;
//			this.contextMenu1.Show(this.web,new Point(x,y));
			return true;
		}

		// TODO: Fix this
		private void windowEvents_onerror(string description, string url, int line)
		{
			MessageBox.Show(description,AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private bool iEvent_onclick(IHTMLEventObj pEvtObj)
		{
			return true;
		}

		private void windowEvents_onload(IHTMLEventObj pEvtObj)
		{	
			//MessageBox.Show("On load complete");
//			isLinkNavigation = true;
//			//allowNavigate = false;
//			this.LoadDocumentEvent(this, new EventArgs());
		}
		#endregion

		/// <summary>
		/// Gets or sets the setting for navigating.
		/// </summary>
		public bool CanLinkNavigate
		{
			get
			{
				return isLinkNavigation;
			}
			set
			{
				isLinkNavigation = value;
			}
		}

		/// <summary>
		/// Gets or sets the setting for allowing new windows.
		/// </summary>
		public bool AllowNewWindow
		{
			get
			{
				return _canNewWindow;
			}
			set
			{
				_canNewWindow = value;
			}
		}

		/// <summary>
		/// Gets the location of the web browser.
		/// </summary>
		public string GetLocation
		{
			get
			{
				return requestedUrl;
			}
		}
	}
}
