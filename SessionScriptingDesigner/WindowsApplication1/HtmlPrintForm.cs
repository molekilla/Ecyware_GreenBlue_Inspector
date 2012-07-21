using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;
using Ecyware.GreenBlue.Controls;
using mshtml;

namespace Ecyware.GreenBlue.SessionScriptingDesigner
{
	/// <summary>
	/// Handles the new window events.
	/// </summary>
	public delegate void HtmlResponseViewEventHandler(object sender, HtmlPrintForm htmlPrintPreviewForm);

	/// <summary>
	/// Summary description for HtmlPrintForm.
	/// </summary>
	public class HtmlPrintForm : BasePluginForm
	{
		static int objectCount = 0;

		private bool _isNewWindow = false;
		internal AxSHDocVw.AxWebBrowser web;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private string tempfile = string.Empty;
		private string[] htmlContentReports = null;

		internal HtmlResponseViewEventHandler HtmlResponseViewEvent;
		internal ApplyMenuSettingsEventHandler ApplyMenuSettingsEvent;
		internal ApplyToolbarSettingsEventHandler ApplyToolbarSettingsEvent;

		/// <summary>
		/// Creates a new HtmlPrintForm.
		/// </summary>
		public HtmlPrintForm()
		{
			objectCount++;

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			//this.web.BeforeNavigate2 += new AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2EventHandler(web_BeforeNavigate2);
			this.web.NewWindow2 += new AxSHDocVw.DWebBrowserEvents2_NewWindow2EventHandler(web_NewWindow2);
			//this.web.DocumentComplete += new AxSHDocVw.DWebBrowserEvents2_DocumentCompleteEventHandler(web_DocumentComplete);

		}

		/// <summary>
		/// Creates a new HtmlPrintForm.
		/// </summary>
		/// <param name="isNewWindow"> Toggles the new window event.</param>
		public HtmlPrintForm(bool isNewWindow) : this()
		{
			_isNewWindow = isNewWindow;
		}

		#region Report Resources Location
		/// <summary>
		/// Gets or sets the html content for each request
		/// </summary>
		internal string[] HtmlContentResults
		{
			get
			{
				return htmlContentReports;
			}
			set
			{
				htmlContentReports = value;
			}
		}
		/// <summary>
		/// Gets the report file location
		/// </summary>
		internal string GetReportFileLocation
		{
			get
			{
				return tempfile;
			}
		}

		/// <summary>
		/// Get the report resources.
		/// </summary>
		internal string[] GetResources
		{
			get
			{
				ArrayList resources = new ArrayList(4);
				string reportErrorIcon = Application.UserAppDataPath + "\\request error.gif";
				string reportOkIcon = Application.UserAppDataPath + "\\request ok.gif";
				string reportLogo = Application.UserAppDataPath + "\\gb logo.gif";
				string reportStyleSheet = Application.UserAppDataPath + "\\report.css";

				if ( File.Exists(reportLogo) )
				{
					resources.Add(reportLogo);
				}
				if ( File.Exists(reportErrorIcon) )
				{
					resources.Add(reportErrorIcon);
				}
				if ( File.Exists(reportOkIcon) )
				{
					resources.Add(reportOkIcon);
				}
				if ( File.Exists(reportStyleSheet) )
				{
					resources.Add(reportStyleSheet);
				}

				return (string[])resources.ToArray(typeof(string));
			}
		}

		#endregion

		/// <summary>
		/// Loads html data into the web browser.
		/// </summary>
		/// <param name="data"> Text data to load.</param>
		/// <param name="type"> The type of report.</param>
		public void LoadData(string data, string type)
		{
			// save as temp
			if ( type == "xml" )
			{
				tempfile = Application.UserAppDataPath + "\\temphtml\\_temp" + DateTime.Now.Ticks + ".xml";
			} 
			else 
			{
				tempfile = Application.UserAppDataPath + "\\temphtml\\_temp" + DateTime.Now.Ticks + ".htm";
			}
	
			StreamWriter writer = new StreamWriter(tempfile,false,System.Text.Encoding.Default);
			writer.Write(data);
			writer.Flush();
			writer.Close();

			Object n = null;

			web.Navigate("file:///" + tempfile.Replace(@"\",@"/"), ref n, ref n, ref n, ref n);	
		}


		/// <summary>
		/// Appends html data into the web browser.
		/// </summary>
		/// <param name="data"> Text data to load.</param>
		public void AppendData(string data, bool clear)
		{		
			HTMLDocumentClass doc = (mshtml.HTMLDocumentClass)web.Document;
			if ( clear )
			{
				doc.body.innerHTML = data;
			} 
			else 
			{
				doc.body.innerHTML += data;
			}
		}

		/// <summary>
		/// Toggles the ReportDialogTest menu.
		/// </summary>
		/// <param name="enabled"></param>
		public void UpdateReportDialogTestMenu(bool enabled)
		{
			// new Arguments
			ApplyMenuSettingsEventArgs newArgs = new ApplyMenuSettingsEventArgs();
	
			// identify the shortcut
			newArgs.RootShortcut = Shortcut.CtrlShiftI;

			// get menu item by index
			Ecyware.GreenBlue.Controls.MenuItem reportDialogMenu = this.PluginMenus.GetByIndex(5);

			reportDialogMenu.Enabled = enabled;
			newArgs.MenuItems.Add(reportDialogMenu.Name,reportDialogMenu);

			ToolbarItem reportButton = (ToolbarItem)this.PluginMenus.GetByIndex(7);
			reportButton.Enabled = enabled;

			ApplyToolbarSettingsEventArgs args = new ApplyToolbarSettingsEventArgs(reportButton);

			// update toolbar
			this.ApplyToolbarSettingsEvent(this, args);
			
			// update menu
			this.ApplyMenuSettingsEvent(this, newArgs);
		}

		/// <summary>
		/// Toggles the save and print report menu.
		/// </summary>
		/// <param name="enabled"></param>
		public void UpdateSavePrintReportMenu(bool enabled)
		{
			// new Arguments
			ApplyMenuSettingsEventArgs newArgs = new ApplyMenuSettingsEventArgs();
	
			// identify the shortcut
			newArgs.RootShortcut = Shortcut.CtrlShiftI;		

			// get menu item by index
			Ecyware.GreenBlue.Controls.MenuItem saveReport = this.PluginMenus.GetByIndex(1);
			Ecyware.GreenBlue.Controls.MenuItem printReport = this.PluginMenus.GetByIndex(2);

			saveReport.Enabled = enabled;
			printReport.Enabled = enabled;
			// printPreviewReport.Enabled = enabled;

			newArgs.MenuItems.Add(saveReport.Name, saveReport);
			newArgs.MenuItems.Add(printReport.Name, printReport);
			// newArgs.MenuItems.Add(printPreviewReport.Name, printPreviewReport);

			// update menu
			this.ApplyMenuSettingsEvent(this, newArgs);
		}


		// Print a document, telling the WebBrowser whether or not to display
		// the UI. MSHTML exposes a method to print as well, but we want these
		// semantics to work for any document server (Word, Excel, etc.), so we
		// perform the op against the doc host instead.
		/// <summary>
		/// Prints the report.
		/// </summary>
		/// <param name="doUI"></param>
		public void Print(bool doUI)
		{
			object o = new object(); 
			SHDocVw.OLECMDEXECOPT doOpt;
			if(doUI)
			{
				doOpt = SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_PROMPTUSER;
			}
			else
			{
				doOpt = SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER;
			}
			this.web.ExecWB(SHDocVw.OLECMDID.OLECMDID_PRINT, doOpt, ref o, ref o);
		}

		/// <summary>
		/// Closes the form.
		/// </summary>
		public override void Close()
		{
			objectCount--;

			if ( !_isNewWindow )
			{				
				if ( objectCount == 0 )
				{
					// MessageBox.Show("TODO: Do you want to save the report before exiting?");
					UpdateSavePrintReportMenu(false);
					UpdateReportDialogTestMenu(false);

					string[] files = Directory.GetFiles(Application.UserAppDataPath + "\\temphtml");

					foreach ( string file in files )
					{
						File.Delete(file);
					}
				}
			}
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				Close();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(HtmlPrintForm));
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
			this.web.Size = new System.Drawing.Size(462, 288);
			this.web.TabIndex = 1;
			// 
			// HtmlPrintForm
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.web);
			this.Name = "HtmlPrintForm";
			this.Size = new System.Drawing.Size(462, 288);
			((System.ComponentModel.ISupportInitialize)(this.web)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		#region Browser Events
		private void web_BeforeNavigate2(object sender, AxSHDocVw.DWebBrowserEvents2_BeforeNavigate2Event e)
		{
			e.cancel = true;
		}
		private void web_NewWindow2(object sender, AxSHDocVw.DWebBrowserEvents2_NewWindow2Event e)
		{
			// Create browser
			HtmlPrintForm htmlForm = new HtmlPrintForm(true); 
			htmlForm.web.RegisterAsBrowser = false;

			e.ppDisp = htmlForm.web.Application;
			htmlForm.web.Visible = true;

			this.HtmlResponseViewEvent(this, htmlForm);
		}
		#endregion
	}
}
