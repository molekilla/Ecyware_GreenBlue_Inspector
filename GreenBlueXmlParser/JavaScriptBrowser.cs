using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using mshtml;
using System.Runtime.InteropServices;

namespace Ecyware.GreenBlue.HtmlProcessor
{

	#region Delegates
	public delegate void WebBrowserElementEventHandler(object sender, mshtml.IHTMLEventObj e);
	#endregion

	/// <summary>
	/// Summary description for JavaScriptBrowser.
	/// </summary>
	public class JavaScriptBrowser : System.Windows.Forms.UserControl
	{
		private AxSHDocVw.AxWebBrowser webMain;
		#region Enumerators
		public enum ieScrollBars {None, Always, Auto};
		#endregion
		#region Event Handler Declarations
		public event WebBrowserElementEventHandler WebBrowserElementEvent;
		#endregion

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public JavaScriptBrowser()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			// Go to the blank page, initializes the WebBrowser control to handle HTML document
			Navigate("about:blank", true);
			// Wait for the control to initialize to HTML and load the blank page.
			while (body == null) {Application.DoEvents();}

			this.Text = "";
			this.HTML = "";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(JavaScriptBrowser));
			this.webMain = new AxSHDocVw.AxWebBrowser();
			((System.ComponentModel.ISupportInitialize)(this.webMain)).BeginInit();
			this.SuspendLayout();
			// 
			// webMain
			// 
			this.webMain.Enabled = true;
			this.webMain.Location = new System.Drawing.Point(6, 6);
			this.webMain.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("webMain.OcxState")));
			this.webMain.Size = new System.Drawing.Size(102, 84);
			this.webMain.TabIndex = 0;
			// 
			// JavaScriptBrowser
			// 
			this.Controls.Add(this.webMain);
			this.Name = "JavaScriptBrowser";
			this.Size = new System.Drawing.Size(348, 240);
			((System.ComponentModel.ISupportInitialize)(this.webMain)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region Public Control Properties
		// Sets the scroll bars of body of the document.
		/// <summary>
		/// Special Internet Explorer BODY.Scroll Property
		/// </summary>
		[DefaultValue(ieScrollBars.None)]
		[Description("Internet Explorer's BODY.Scroll Property")]
		public ieScrollBars ScrollBars
		{
			get
			{
				if (body.scroll.Equals("yes")) return ieScrollBars.Always;
				if (body.scroll.Equals("auto")) return ieScrollBars.Auto;
				return ieScrollBars.None;
			}
			set
			{
				if (value == ieScrollBars.Auto) body.scroll = "auto";
				if (value == ieScrollBars.None) body.scroll = "no";
				if (value == ieScrollBars.Always) body.scroll = "yes";
			}
		}
		#endregion
		#region Public HTML Properties
		[Browsable(false)] // Do not show in the properties box
		public HTMLDocument document
		{ get {return (HTMLDocument) webMain.Document;} }
		[Browsable(false)] // Do not show in the properties box
		public HTMLBody body
		{ get {return (HTMLBody) document.body;} }
		// Document Body's innerText
		[DefaultValue("")] // Let the properties box know the default value
		[Browsable(true)] // Do show in the properties box
		[Description("Internet Explorer's BODY.innerText Property")]
		public override string Text
		{
			get {return body.innerText;}
			set {body.innerText = value;}
		}
		// Document Body's innerHTML
		[DefaultValue("")]
		[Description("Internet Explorer's BODY.innerHTML Property")]
		public string HTML
		{
			get {return body.innerHTML;}
			set {body.innerHTML = value;}
		}
		#endregion
		#region COM Event Handler for WebBrowser HTML Elements
		// COM Event Handler for HTML Element Events
		[DispId(0)]
		public void DefaultMethod()
		{
			// Call the custom Web Browser HTML event
			WebBrowserElementEvent(this, document.parentWindow.@event);
		}
		#endregion
		#region Public Methods
		/// <summary>
		/// Navigates to a web page, defaults to waiting for the page to load before continuing
		/// </summary>
		/// <param name="url">The URL to be redirected to</param>
		public void Navigate(string url)
		{ Navigate(url, true); }
		/// <summary>
		/// Navigates to a web page
		/// </summary>
		/// <param name="url">The URL to be redirected to</param>
		/// <param name="wait">Wait for the page to load before continuing</param>
		public void Navigate(string url, bool wait)
		{
			// Creates the null missing value object
			object o = System.Reflection.Missing.Value;
			// Resets the browser to an empty container, cleaning the slate
			if (wait) webMain.Navigate(null, ref o, ref o, ref o, ref o);
			// Wait until the browser is empty
			if (wait) while (document != null) {Application.DoEvents();}
			// Go to the new URL
			webMain.Navigate(url, ref o, ref o, ref o, ref o);
			while (webMain.ReadyState !=SHDocVw.tagREADYSTATE.READYSTATE_COMPLETE ) {Application.DoEvents();}
			if (wait) while (document.body == null) {Application.DoEvents();}
			//if (wait) while (webMain.Busy) {Application.DoEvents();}
		}
		#endregion
	}
}
