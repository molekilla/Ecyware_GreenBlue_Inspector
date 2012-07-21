// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: November 2003 - September 2004 - January 2005.
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Xml;
using System.Text;
using System.Net;
using Compona.Windows.Forms;
using Ecyware.GreenBlue.Controls;
using Ecyware.GreenBlue.Controls.Scripting;
using Ecyware.GreenBlue.Engine.HtmlCommand;
using Ecyware.GreenBlue.Protocols.Http;
using Ecyware.GreenBlue.Protocols.Http.Scripting;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.WebUnitTestManager;
using Ecyware.GreenBlue.WebUnitTestCommand;
using Ecyware.GreenBlue.ReportEngine;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Utils;using Ecyware.GreenBlue.Engine;




namespace Ecyware.GreenBlue.GreenBlueMain
{	
	/// <summary>
	/// Request Complete Event Handler.
	/// </summary>
	internal delegate void RequestCompleteEventHandler(object sender, EventArgs e);
	
	/// <summary>
	/// Occurs when a submits raises a OnSubmit Event.
	/// </summary>
	internal delegate void OnFormConvertionEventHandler(object sender, FormConvertionArgs e);

	/// <summary>
	/// Occurs while posting data and no event could be found.
	/// </summary>
	internal delegate void OnFormHeuristicEventHandler(object sender, FormHeuristicArgs e);
	
	/// <summary>
	/// User Request Initiated.
	/// </summary>
	internal delegate void InspectorStartRequestEventHandler(object sender, RequestGetEventArgs e);

	/// <summary>
	/// Cancels the http request.
	/// </summary>
	internal delegate void InspectorCancelRequestEventHandler(object sender, EventArgs e);

	/// <summary>
	/// Document complete.
	/// </summary>
	internal delegate void InspectorRequestCompleteEventHandler(object sender, RequestCompleteEventArgs e);

	/// <summary>
	/// Loads the Forms Editor.
	/// </summary>
	internal delegate void LoadFormsEditorEventHandler(object sender, LoadFormsEditorEventArgs e);

	/// <summary>
	/// Loads the URL Spider
	/// </summary>
	internal delegate void LoadLinksEventHandler(object sender, LoadLinksEventArgs e);

	/// <summary>
	/// Raises when the html document onload is called.
	/// </summary>
	internal delegate void OnLoadHtmlDocumentEventHandler(object sender, EventArgs e);

	/// <summary>
	/// The available states for the Inspector Workspace.
	/// </summary>
	public enum GBInspectorState
	{
		/// <summary>
		/// Initialization state.
		/// </summary>
		Idle,
		/// <summary>
		/// Inspector is requesting or posting to an url.
		/// </summary>
		Requesting,
		/// <summary>
		/// Inspector return an error while requesting.
		/// </summary>
		Error,
		/// <summary>
		/// Inspector completed request.
		/// </summary>
		Complete
	}
	/// <summary>
	/// Contains the GBInspectorWorkspace.
	/// </summary>
	public class GBInspectorWorkspace : BasePluginForm, IWorkspacePlugin
	{		

		PostForm postForm;
		GetForm getForm;
		private GBInspectorState _state = GBInspectorState.Idle;

		/// <summary>
		/// Handles the html text editor.
		/// </summary>
		private bool dirtyBit = false;


		// Inspector Configuration Settings
		private InspectorConfiguration inspectorConfig = null;
		//private HttpClientConfiguration httpClientConfig = null;

		// menus
		MenuItemCollection mnuToolMenu = null;
		MenuItemCollection mnSessionMenus = null;
		MenuItemCollection mnEditMenus = null;
		MenuItemCollection mnHelp = null;
		MenuItemCollection mnFile = null;
		MenuRootHashtable mnRoot = null;

		// private delegates
		private delegate void LoadFormsEditorCallback(HtmlFormTagCollection forms, bool showMessage);
		private delegate void GetResponseCallback(ResponseBuffer response);
		private delegate void GetRunTestCallback(ResponseBuffer response,bool lastItem);
		private delegate void GetReportCallback(ArrayList al);

		// GBInspectorWorksapce delegates and events

		// Interface delegates and events
		/// <summary>
		/// Stops the progress bar.
		/// </summary>
		public event StopProgressBarEventHandler StopProgressBarEvent;
		/// <summary>
		/// Starts the progress bar.
		/// </summary>
		public event StartProgressBarEventHandler StartProgressBarEvent;
		/// <summary>
		/// Changes the status bar text.
		/// </summary>
		public event ChangeStatusBarEventHandler ChangeStatusBarPanelEvent;
		/// <summary>
		/// Loads the plugin for the workspace.
		/// </summary>
		public event LoadPluginMenusEventHandler LoadPluginMenusEvent;
		/// <summary>
		/// Applies the menu settings for the workspace.
		/// </summary>
		public event ApplyMenuSettingsEventHandler ApplyMenuSettingsEvent;
		/// <summary>
		/// Applies the toolbar settings for the workspace.
		/// </summary>
		public event ApplyToolbarSettingsEventHandler ApplyToolbarSettingsEvent;
		/// <summary>
		/// Applies the menu root settings for the workspace.
		/// </summary>
		public event ApplyMenuRootSettingsEventHandler ApplyMenuRootSettingsEvent;
		/// <summary>
		/// Updates the address bar.
		/// </summary>
		public event UpdateAddressEventHandler UpdateAddressEvent;

		/// <summary>
		/// Disables the address bar
		/// </summary>
		public event DisableAddressBarEventHandler DisableAddressBar;

		/// <summary>
		/// Enables the address bar
		/// </summary>
		public event EnableAddressBarEventHandler EnableAddressBar;

		/// <summary>
		/// Closes the application.
		/// </summary>
		internal EventHandler GBExit;

		/// <summary>
		/// Starts a Http request.
		/// </summary>
		internal InspectorStartRequestEventHandler StartEventDelegate;

		/// <summary>
		/// Cancels a Http request.
		/// </summary>
		internal InspectorCancelRequestEventHandler CancelEventDelegate;

		// inspector settings types
		private HttpProperties ClientProperties;
		private SnifferOptions SnifferProperties;
		private HttpProxy ProxySettings;

		// allow browser navigate first and session variables
		private bool AllowBrowserFirst = false;
		private bool IsRecording = false;
		private Session CurrentSessionRecording;

		/// <summary>
		/// Parser type.
		/// </summary>
		private HtmlParser parser = new HtmlParser();
		/// <summary>
		/// The Forms Editor type.
		/// </summary>
		private FormsEditor formeditor;
		/// <summary>
		/// The SessionDesigner type.
		/// </summary>
		private ScriptingDataDesigner _scriptingDataDesigner;

		/// <summary>
		/// The form tag collection.
		/// </summary>
		private HtmlFormTagCollection formCollection;

		private Cursor tempCursor;
		/// <summary>
		/// The Html Editor Document.
		/// </summary>
		private DocumentManager.Document htmlEditorDocument;
		/// <summary>
		/// Contains the current report arraylist.
		/// </summary>
		private ArrayList _currentReportList;
		/// <summary>
		/// The text editor form.
		/// </summary>
		private TextEditorForm textViewerForm = new TextEditorForm();
		/// <summary>
		/// The navigator form.
		/// </summary>
		private NavigableWebForm navForm;
		/// <summary>
		/// The report form.
		/// </summary>
		private HtmlPrintForm printForm;
		/// <summary>
		/// The current response buffer.
		/// </summary>
		private ResponseBuffer _currentResponse;
		private bool IsProxyEnabled = false;
		/// <summary>
		/// The session command object.
		/// </summary>
		private SessionCommand sessionCommand;

		/// <summary>
		/// The test template.
		/// </summary>
		private TestCollection _testTemplate;

		// designer
		private DockingSuite.DockPanel dpHeaders;
		private DockingSuite.DockControl dpRequestHeader;
		private DockingSuite.DockControl dpResponseHeader;
		private DockingSuite.DockHost dhRightPanel;
		private DockingSuite.DockHost dhBottomPanel;
		private System.Windows.Forms.Panel pnMainView;
		private System.Windows.Forms.ListView lvResponseHeader;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private DocumentManager.DocumentManager dmDocuments;
		private DockingSuite.DockControl dpCookies;
		private System.Windows.Forms.ListView lvCookies;
		private System.Windows.Forms.ColumnHeader colCookieName;
		private System.Windows.Forms.ColumnHeader colCookieValue;
		private System.Windows.Forms.ColumnHeader colCookiePath;
		private System.Windows.Forms.ContextMenu mnuTextStream;
		private System.Windows.Forms.MenuItem mnuParseHTML;
		private System.Windows.Forms.MenuItem mnuViewBrowser;
		private System.Windows.Forms.MenuItem mnuBrowserEditor;
		private System.Windows.Forms.MenuItem mnuFormEditor;
		private DockingSuite.DockPanel dpHistory;
		private DockingSuite.DockControl dcHistory;
		private DockingSuite.DockControl dockControl1;
		private System.Windows.Forms.RichTextBox txtMessaging;
		private System.Windows.Forms.ColumnHeader colCookieDomain;
		private System.Windows.Forms.ContextMenu mnuQueryEditor;
		private System.Windows.Forms.MenuItem mnXmlTree;
		private System.Windows.Forms.ContextMenu mnuMessaging;
		private System.Windows.Forms.MenuItem mnuClearMessaging;
		private System.Windows.Forms.SaveFileDialog dlgSaveFile;
		private System.Windows.Forms.OpenFileDialog dlgOpenFile;
		private System.Windows.Forms.ImageList imgIcons;
		private Ecyware.GreenBlue.Controls.HistoryTree sitesTree;
		private System.Windows.Forms.ContextMenu mnuRecentSites;
		private System.Windows.Forms.MenuItem mnuEditCookies;
		private System.Windows.Forms.ContextMenu mnuRequestHeaders;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private Ecyware.GreenBlue.Controls.FlatPropertyGrid pgHeaders;
		private System.Windows.Forms.MenuItem mnuCopyEventConsole;
		private DockingSuite.DockControl dcUrlSpider;
		private System.Windows.Forms.Panel panel2;
		private Ecyware.GreenBlue.Controls.UrlSpiderControl urlSpiderControl;		
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Creates a new GreenBlue Inspector Workspace.
		/// </summary>
		public GBInspectorWorkspace()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.SuspendLayout();
			InitializeForm();	
			this.ResumeLayout(false);
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
						this.components = new System.ComponentModel.Container();
						System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(GBInspectorWorkspace));
						this.dhRightPanel = new DockingSuite.DockHost();
						this.dpHistory = new DockingSuite.DockPanel();
						this.dcHistory = new DockingSuite.DockControl();
						this.sitesTree = new Ecyware.GreenBlue.Controls.HistoryTree();
						this.dcUrlSpider = new DockingSuite.DockControl();
						this.panel2 = new System.Windows.Forms.Panel();
						this.urlSpiderControl = new Ecyware.GreenBlue.Controls.UrlSpiderControl();
						this.imgIcons = new System.Windows.Forms.ImageList(this.components);
						this.dhBottomPanel = new DockingSuite.DockHost();
						this.dpHeaders = new DockingSuite.DockPanel();
						this.dockControl1 = new DockingSuite.DockControl();
						this.txtMessaging = new System.Windows.Forms.RichTextBox();
						this.mnuMessaging = new System.Windows.Forms.ContextMenu();
						this.mnuCopyEventConsole = new System.Windows.Forms.MenuItem();
						this.mnuClearMessaging = new System.Windows.Forms.MenuItem();
						this.dpRequestHeader = new DockingSuite.DockControl();
						this.pgHeaders = new Ecyware.GreenBlue.Controls.FlatPropertyGrid();
						this.dpResponseHeader = new DockingSuite.DockControl();
						this.lvResponseHeader = new System.Windows.Forms.ListView();
						this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
						this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
						this.dpCookies = new DockingSuite.DockControl();
						this.lvCookies = new System.Windows.Forms.ListView();
						this.colCookieName = new System.Windows.Forms.ColumnHeader();
						this.colCookieValue = new System.Windows.Forms.ColumnHeader();
						this.colCookieDomain = new System.Windows.Forms.ColumnHeader();
						this.colCookiePath = new System.Windows.Forms.ColumnHeader();
						this.pnMainView = new System.Windows.Forms.Panel();
						this.dmDocuments = new DocumentManager.DocumentManager();
						this.mnuTextStream = new System.Windows.Forms.ContextMenu();
						this.mnuParseHTML = new System.Windows.Forms.MenuItem();
						this.mnuViewBrowser = new System.Windows.Forms.MenuItem();
						this.mnuBrowserEditor = new System.Windows.Forms.MenuItem();
						this.mnuFormEditor = new System.Windows.Forms.MenuItem();
						this.mnuQueryEditor = new System.Windows.Forms.ContextMenu();
						this.mnXmlTree = new System.Windows.Forms.MenuItem();
						this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
						this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
						this.mnuRecentSites = new System.Windows.Forms.ContextMenu();
						this.mnuEditCookies = new System.Windows.Forms.MenuItem();
						this.mnuRequestHeaders = new System.Windows.Forms.ContextMenu();
						this.menuItem2 = new System.Windows.Forms.MenuItem();
						this.menuItem3 = new System.Windows.Forms.MenuItem();
						this.menuItem4 = new System.Windows.Forms.MenuItem();
						this.dhRightPanel.SuspendLayout();
						this.dpHistory.SuspendLayout();
						this.dcHistory.SuspendLayout();
						this.dcUrlSpider.SuspendLayout();
						this.panel2.SuspendLayout();
						this.dhBottomPanel.SuspendLayout();
						this.dpHeaders.SuspendLayout();
						this.dockControl1.SuspendLayout();
						this.dpRequestHeader.SuspendLayout();
						this.dpResponseHeader.SuspendLayout();
						this.dpCookies.SuspendLayout();
						this.pnMainView.SuspendLayout();
						this.SuspendLayout();
						// 
						// dhRightPanel
						// 
						this.dhRightPanel.Controls.Add(this.dpHistory);
						this.dhRightPanel.Dock = System.Windows.Forms.DockStyle.Right;
						this.dhRightPanel.Location = new System.Drawing.Point(514, 0);
						this.dhRightPanel.Name = "dhRightPanel";
						this.dhRightPanel.Size = new System.Drawing.Size(200, 426);
						this.dhRightPanel.TabIndex = 0;
						// 
						// dpHistory
						// 
						this.dpHistory.AutoHide = false;
						this.dpHistory.Controls.Add(this.dcHistory);
						this.dpHistory.Controls.Add(this.dcUrlSpider);
						this.dpHistory.DockedHeight = 426;
						this.dpHistory.DockedWidth = 0;
						this.dpHistory.Location = new System.Drawing.Point(4, 0);
						this.dpHistory.Name = "dpHistory";
						this.dpHistory.SelectedTab = this.dcHistory;
						this.dpHistory.Size = new System.Drawing.Size(196, 426);
						this.dpHistory.TabIndex = 0;
						this.dpHistory.Text = "Docked Panel";
						// 
						// dcHistory
						// 
						this.dcHistory.Controls.Add(this.sitesTree);
						this.dcHistory.Guid = new System.Guid("765ec50a-ce53-4cd2-b965-dd88005c8a2e");
						this.dcHistory.Location = new System.Drawing.Point(0, 20);
						this.dcHistory.Name = "dcHistory";
						this.dcHistory.PrimaryControl = null;
						this.dcHistory.Size = new System.Drawing.Size(196, 383);
						this.dcHistory.TabImage = null;
						this.dcHistory.TabIndex = 1;
						this.dcHistory.Text = "Recent sites";
						// 
						// sitesTree
						// 
						this.sitesTree.Dock = System.Windows.Forms.DockStyle.Fill;
						this.sitesTree.IconNodeIndex = 0;
						this.sitesTree.IconSiteIndex = 0;
						this.sitesTree.ImageIndex = -1;
						this.sitesTree.Location = new System.Drawing.Point(0, 0);
						this.sitesTree.Name = "sitesTree";
						this.sitesTree.SelectedImageIndex = -1;
						this.sitesTree.Size = new System.Drawing.Size(196, 383);
						this.sitesTree.Sorted = true;
						this.sitesTree.TabIndex = 0;
						this.sitesTree.DoubleClick += new System.EventHandler(this.sitesTree_DoubleClick);
						// 
						// dcUrlSpider
						// 
						this.dcUrlSpider.Controls.Add(this.panel2);
						this.dcUrlSpider.Guid = new System.Guid("c974d67f-0884-47d1-bdd0-b667a39d8f68");
						this.dcUrlSpider.Location = new System.Drawing.Point(0, 20);
						this.dcUrlSpider.Name = "dcUrlSpider";
						this.dcUrlSpider.PrimaryControl = null;
						this.dcUrlSpider.Size = new System.Drawing.Size(196, 383);
						this.dcUrlSpider.TabImage = null;
						this.dcUrlSpider.TabIndex = 2;
						this.dcUrlSpider.Text = "Page References";
						// 
						// panel2
						// 
						this.panel2.Controls.Add(this.urlSpiderControl);
						this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
						this.panel2.Location = new System.Drawing.Point(0, 0);
						this.panel2.Name = "panel2";
						this.panel2.Size = new System.Drawing.Size(196, 383);
						this.panel2.TabIndex = 2;
						// 
						// urlSpiderControl
						// 
						this.urlSpiderControl.Dock = System.Windows.Forms.DockStyle.Fill;
						this.urlSpiderControl.ImageList = this.imgIcons;
						this.urlSpiderControl.Location = new System.Drawing.Point(0, 0);
						this.urlSpiderControl.Name = "urlSpiderControl";
						this.urlSpiderControl.Size = new System.Drawing.Size(196, 383);
						this.urlSpiderControl.TabIndex = 1;
						//this.urlSpiderControl.UrlRequests = null;
						this.urlSpiderControl.DoubleClickNode += new System.EventHandler(this.urlSpiderControl_DoubleClickNode);
						// 
						// imgIcons
						// 
						this.imgIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
						this.imgIcons.ImageSize = new System.Drawing.Size(16, 16);
						this.imgIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgIcons.ImageStream")));
						this.imgIcons.TransparentColor = System.Drawing.Color.Transparent;
						// 
						// dhBottomPanel
						// 
						this.dhBottomPanel.Controls.Add(this.dpHeaders);
						this.dhBottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
						this.dhBottomPanel.Location = new System.Drawing.Point(0, 251);
						this.dhBottomPanel.Name = "dhBottomPanel";
						this.dhBottomPanel.Size = new System.Drawing.Size(514, 175);
						this.dhBottomPanel.TabIndex = 3;
						// 
						// dpHeaders
						// 
						this.dpHeaders.AutoHide = false;
						this.dpHeaders.Controls.Add(this.dockControl1);
						this.dpHeaders.Controls.Add(this.dpRequestHeader);
						this.dpHeaders.Controls.Add(this.dpResponseHeader);
						this.dpHeaders.Controls.Add(this.dpCookies);
						this.dpHeaders.DockedHeight = 175;
						this.dpHeaders.DockedWidth = 514;
						this.dpHeaders.Location = new System.Drawing.Point(0, 4);
						this.dpHeaders.Name = "dpHeaders";
						this.dpHeaders.SelectedTab = this.dockControl1;
						this.dpHeaders.Size = new System.Drawing.Size(514, 171);
						this.dpHeaders.TabIndex = 0;
						this.dpHeaders.Text = "Docked Panel";
						// 
						// dockControl1
						// 
						this.dockControl1.Controls.Add(this.txtMessaging);
						this.dockControl1.Guid = new System.Guid("58786d43-48b4-48b2-a379-4844db0687fb");
						this.dockControl1.Location = new System.Drawing.Point(0, 20);
						this.dockControl1.Name = "dockControl1";
						this.dockControl1.PrimaryControl = null;
						this.dockControl1.TabImage = null;
						this.dockControl1.TabIndex = 3;
						this.dockControl1.Text = "Event Console";
						// 
						// txtMessaging
						// 
						this.txtMessaging.AutoWordSelection = true;
						this.txtMessaging.ContextMenu = this.mnuMessaging;
						this.txtMessaging.DetectUrls = false;
						this.txtMessaging.Dock = System.Windows.Forms.DockStyle.Fill;
						this.txtMessaging.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
						this.txtMessaging.Location = new System.Drawing.Point(0, 0);
						this.txtMessaging.Name = "txtMessaging";
						this.txtMessaging.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
						this.txtMessaging.Size = new System.Drawing.Size(0, 0);
						this.txtMessaging.TabIndex = 0;
						this.txtMessaging.Text = "";
						// 
						// mnuMessaging
						// 
						this.mnuMessaging.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 										 this.mnuCopyEventConsole,
																						 										 this.mnuClearMessaging});
						// 
						// mnuCopyEventConsole
						// 
						this.mnuCopyEventConsole.Index = 0;
						this.mnuCopyEventConsole.Text = "Co&py";
						this.mnuCopyEventConsole.Click += new System.EventHandler(this.mnuCopyEventConsole_Click);
						// 
						// mnuClearMessaging
						// 
						this.mnuClearMessaging.Index = 1;
						this.mnuClearMessaging.Text = "&Clear";
						this.mnuClearMessaging.Click += new System.EventHandler(this.mnuClearMessaging_Click);
						// 
						// dpRequestHeader
						// 
						this.dpRequestHeader.AutoScroll = true;
						this.dpRequestHeader.Controls.Add(this.pgHeaders);
						this.dpRequestHeader.Guid = new System.Guid("3162e1c8-c606-4466-837b-9af3488529ec");
						this.dpRequestHeader.Location = new System.Drawing.Point(0, 20);
						this.dpRequestHeader.Name = "dpRequestHeader";
						this.dpRequestHeader.PrimaryControl = null;
						this.dpRequestHeader.TabImage = null;
						this.dpRequestHeader.TabIndex = 0;
						this.dpRequestHeader.Text = "HTTP Request Header";
						// 
						// pgHeaders
						// 
						this.pgHeaders.CommandsVisibleIfAvailable = true;
						this.pgHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
						this.pgHeaders.HelpVisible = false;
						this.pgHeaders.LargeButtons = false;
						this.pgHeaders.LineColor = System.Drawing.SystemColors.ScrollBar;
						this.pgHeaders.Location = new System.Drawing.Point(0, 0);
						this.pgHeaders.Name = "pgHeaders";
						this.pgHeaders.Size = new System.Drawing.Size(0, 0);
						this.pgHeaders.TabIndex = 1;
						this.pgHeaders.Text = "flatPropertyGrid1";
						this.pgHeaders.ToolbarVisible = false;
						this.pgHeaders.ViewBackColor = System.Drawing.SystemColors.Window;
						this.pgHeaders.ViewForeColor = System.Drawing.SystemColors.WindowText;
						// 
						// dpResponseHeader
						// 
						this.dpResponseHeader.Controls.Add(this.lvResponseHeader);
						this.dpResponseHeader.Guid = new System.Guid("54d81e37-a909-41ee-9527-6c2041f7e5c5");
						this.dpResponseHeader.Location = new System.Drawing.Point(0, 20);
						this.dpResponseHeader.Name = "dpResponseHeader";
						this.dpResponseHeader.PrimaryControl = null;
						this.dpResponseHeader.TabImage = null;
						this.dpResponseHeader.TabIndex = 1;
						this.dpResponseHeader.Text = "HTTP Response Header";
						// 
						// lvResponseHeader
						// 
						this.lvResponseHeader.AllowColumnReorder = true;
						this.lvResponseHeader.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							   											   this.columnHeader1,
																							   											   this.columnHeader2});
						this.lvResponseHeader.Dock = System.Windows.Forms.DockStyle.Fill;
						this.lvResponseHeader.FullRowSelect = true;
						this.lvResponseHeader.LabelEdit = true;
						this.lvResponseHeader.Location = new System.Drawing.Point(0, 0);
						this.lvResponseHeader.Name = "lvResponseHeader";
						this.lvResponseHeader.Size = new System.Drawing.Size(0, 0);
						this.lvResponseHeader.Sorting = System.Windows.Forms.SortOrder.Ascending;
						this.lvResponseHeader.TabIndex = 1;
						this.lvResponseHeader.View = System.Windows.Forms.View.Details;
						// 
						// columnHeader1
						// 
						this.columnHeader1.Text = "Name";
						this.columnHeader1.Width = 110;
						// 
						// columnHeader2
						// 
						this.columnHeader2.Text = "Value";
						this.columnHeader2.Width = 350;
						// 
						// dpCookies
						// 
						this.dpCookies.Controls.Add(this.lvCookies);
						this.dpCookies.Guid = new System.Guid("6dc54a38-2769-43aa-ae73-3846bbd97557");
						this.dpCookies.Location = new System.Drawing.Point(0, 20);
						this.dpCookies.Name = "dpCookies";
						this.dpCookies.PrimaryControl = null;
						this.dpCookies.TabImage = null;
						this.dpCookies.TabIndex = 2;
						this.dpCookies.Text = "Cookies";
						// 
						// lvCookies
						// 
						this.lvCookies.Activation = System.Windows.Forms.ItemActivation.OneClick;
						this.lvCookies.AllowColumnReorder = true;
						this.lvCookies.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																																this.colCookieName,
																																this.colCookieValue,
																																this.colCookieDomain,
																																this.colCookiePath});
						this.lvCookies.Dock = System.Windows.Forms.DockStyle.Fill;
						this.lvCookies.FullRowSelect = true;
						this.lvCookies.LabelEdit = true;
						this.lvCookies.Location = new System.Drawing.Point(0, 0);
						this.lvCookies.MultiSelect = false;
						this.lvCookies.Name = "lvCookies";
						this.lvCookies.Size = new System.Drawing.Size(0, 0);
						this.lvCookies.Sorting = System.Windows.Forms.SortOrder.Ascending;
						this.lvCookies.TabIndex = 2;
						this.lvCookies.View = System.Windows.Forms.View.Details;
						this.lvCookies.DoubleClick += new System.EventHandler(this.lvCookies_DoubleClick);
						// 
						// colCookieName
						// 
						this.colCookieName.Text = "Name";
						this.colCookieName.Width = 110;
						// 
						// colCookieValue
						// 
						this.colCookieValue.Text = "Value";
						this.colCookieValue.Width = 200;
						// 
						// colCookieDomain
						// 
						this.colCookieDomain.Text = "Domain";
						// 
						// colCookiePath
						// 
						this.colCookiePath.Text = "Path";
						this.colCookiePath.Width = 80;
						// 
						// pnMainView
						// 
						this.pnMainView.Controls.Add(this.dmDocuments);
						this.pnMainView.Dock = System.Windows.Forms.DockStyle.Fill;
						this.pnMainView.Location = new System.Drawing.Point(0, 0);
						this.pnMainView.Name = "pnMainView";
						this.pnMainView.Size = new System.Drawing.Size(514, 251);
						this.pnMainView.TabIndex = 4;
						// 
						// dmDocuments
						// 
						this.dmDocuments.Dock = System.Windows.Forms.DockStyle.Fill;
						this.dmDocuments.Location = new System.Drawing.Point(0, 0);
						this.dmDocuments.Name = "dmDocuments";
						this.dmDocuments.Size = new System.Drawing.Size(514, 251);
						this.dmDocuments.TabIndex = 0;
						this.dmDocuments.CloseButtonPressed += new DocumentManager.DocumentManager.CloseButtonPressedEventHandler(this.dmDocuments_CloseButtonPressed);
						this.dmDocuments.FocusedDocumentChanged += new System.EventHandler(this.dmDocuments_FocusedDocumentChanged);
						// 
						// mnuTextStream
						// 
						this.mnuTextStream.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						  										  this.mnuParseHTML,
																						  										  this.mnuViewBrowser,
																						  										  this.mnuBrowserEditor,
																						  										  this.mnuFormEditor});
						this.mnuTextStream.Popup += new System.EventHandler(this.mnuTextStream_Popup);
						// 
						// mnuParseHTML
						// 
						this.mnuParseHTML.Index = 0;
						this.mnuParseHTML.Text = "&Query HTML";
						this.mnuParseHTML.Click += new System.EventHandler(this.mnuParseHTML_Click);
						// 
						// mnuViewBrowser
						// 
						this.mnuViewBrowser.Index = 1;
						this.mnuViewBrowser.Text = "&Navigate in Internet Explorer";
						this.mnuViewBrowser.Visible = false;
						// 
						// mnuBrowserEditor
						// 
						this.mnuBrowserEditor.Index = 2;
						this.mnuBrowserEditor.Text = "&Render HTML";
						this.mnuBrowserEditor.Visible = false;
						// 
						// mnuFormEditor
						// 
						this.mnuFormEditor.Index = 3;
						this.mnuFormEditor.Text = "Load &Forms Editor";
						this.mnuFormEditor.Visible = false;
						this.mnuFormEditor.Click += new System.EventHandler(this.mnuFormEditor_Click);
						// 
						// mnuQueryEditor
						// 
						this.mnuQueryEditor.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						   										   this.mnXmlTree});
						// 
						// mnXmlTree
						// 
						this.mnXmlTree.Index = 0;
						this.mnXmlTree.Text = "View as XML Tree";
						this.mnXmlTree.Click += new System.EventHandler(this.mnXmlTree_Click);
						// 
						// mnuRecentSites
						// 
						this.mnuRecentSites.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						   										   this.mnuEditCookies});
						// 
						// mnuEditCookies
						// 
						this.mnuEditCookies.Index = 0;
						this.mnuEditCookies.Text = "Edit cookies...";
						this.mnuEditCookies.Click += new System.EventHandler(this.mnuEditCookies_Click);
						// 
						// mnuRequestHeaders
						// 
						this.mnuRequestHeaders.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							  											  this.menuItem2,
																							  											  this.menuItem3,
																							  											  this.menuItem4});
						// 
						// menuItem2
						// 
						this.menuItem2.Index = 0;
						this.menuItem2.Text = "&Add Header";
						// 
						// menuItem3
						// 
						this.menuItem3.Index = 1;
						this.menuItem3.Text = "&Edit Header";
						// 
						// menuItem4
						// 
						this.menuItem4.Index = 2;
						this.menuItem4.Text = "&Remove Header";
						// 
						// GBInspectorWorkspace
						// 
						this.Controls.Add(this.pnMainView);
						this.Controls.Add(this.dhBottomPanel);
						this.Controls.Add(this.dhRightPanel);
						this.Name = "GBInspectorWorkspace";
						this.Size = new System.Drawing.Size(714, 426);
						this.Load += new System.EventHandler(this.ExtendedWebSniffer_Load);
						this.dhRightPanel.ResumeLayout(false);
						this.dpHistory.ResumeLayout(false);
						this.dcHistory.ResumeLayout(false);
						this.dcUrlSpider.ResumeLayout(false);
						this.panel2.ResumeLayout(false);
						this.dhBottomPanel.ResumeLayout(false);
						this.dpHeaders.ResumeLayout(false);
						this.dockControl1.ResumeLayout(false);
						this.dpRequestHeader.ResumeLayout(false);
						this.dpResponseHeader.ResumeLayout(false);
						this.dpCookies.ResumeLayout(false);
						this.pnMainView.ResumeLayout(false);
						this.ResumeLayout(false);

		}
		#endregion
		#region Form Init		
		private void ExtendedWebSniffer_Load(object sender, System.EventArgs e)
		{
			LoadWorkspaceMenu();
		}
		/// <summary>
		/// Initializes the form.
		/// </summary>
		private void InitializeForm()
		{
			// sets the IsUnique for the tab document
			this.IsUnique = true;

			// init settings types
			this.SnifferProperties = new SnifferOptions();

			// get settings
			InspectorConfig = (InspectorConfiguration)ConfigManager.Read("greenBlue/inspector", false);
			ClientProperties = (HttpProperties)ConfigManager.Read("greenBlue/httpClient", false);

			// Load Application Properties
			LoadApplicationProperties(InspectorConfig);
			
			// Load Http Properties
			LoadHttpProperties(ClientProperties);

			// Init Get and Post Command
			InitializeHttpCommands();

			// Start event for GBInspectorWorkspace
			this.StartEventDelegate += new InspectorStartRequestEventHandler(InspectorStartGetEvent);
			this.CancelEventDelegate += new InspectorCancelRequestEventHandler(InspectorCancelRequestEvent);

			// get request event
			textViewerForm.StartEvent += new InspectorStartRequestEventHandler(InspectorStartGetEvent);

			// Context menu
			textViewerForm.txtEditor.ContextMenu = this.mnuTextStream;
			textViewerForm.txtEditor.TextChanged += new EventHandler(txtHTTPStream_TextChanged);
			textViewerForm.txtEditor.CaretChange += new EventHandler(txtEditor_CaretChange);
			
			// Create browser
			navForm = new NavigableWebForm();
			AttachWebFormEvents(navForm);

			// Add browser
			DocumentManager.Document navigatorDoc = new DocumentManager.Document(navForm,"Web Browser");
			dmDocuments.AddDocument(navigatorDoc);
		
			// Add Html Text Editor
			htmlEditorDocument = new DocumentManager.Document(textViewerForm,"Html Browser");
			dmDocuments.AddDocument(htmlEditorDocument);

			// Load History Tree
			this.sitesTree.ImageList = this.imgIcons;
			this.sitesTree.IconNodeIndex = 1;
			this.sitesTree.IconSiteIndex = 7;
			this.sitesTree.LoadHistoryTree();
		}

		#endregion
		#region Properties
		/// <summary>
		/// Gets or sets the inspector configuration
		/// </summary>
		public InspectorConfiguration InspectorConfig
		{
			get
			{
				return inspectorConfig;
			}
			set
			{
				inspectorConfig = value;
			}
		}

		/// <summary>
		/// Gets or sets the current inspector state.
		/// </summary>
		public GBInspectorState InspectorState
		{
			get
			{
				return _state;
			}
			set
			{
				_state = value;
			}
		}

		/// <summary>
		/// Gets or sets the response buffer.
		/// </summary>
		private ResponseBuffer CurrentResponseBuffer
		{
			get
			{
				return _currentResponse;
			}
			set
			{
				_currentResponse = value;
			}
		}


		/// <summary>
		/// Gets or sets the report list.
		/// </summary>
		private ArrayList CurrentReportList
		{
			get
			{
				return _currentReportList;
			}
			set
			{
				_currentReportList = value;
			}
		}
		#endregion
		#region Requests and Responses
		/// <summary>
		/// Initializes the HTTP Commands.
		/// </summary>
		private void InitializeHttpCommands()
		{
			// get and post types init
			getForm = new GetForm();
			postForm = new PostForm();

			getForm.EndHttp += new ResponseCallbackDelegate(http_EndGetHttp);
			postForm.EndHttp += new ResponseCallbackDelegate(postRequest_EndPostHttp);
		}
		/// <summary>
		/// Handles HTTP Redirects.
		/// </summary>
		/// <param name="url"> Url to redirect to.</param>
		internal void GetHttpRedirectRequest(string url)
		{
			RequestGetEventArgs args = new RequestGetEventArgs();
			args.Url = url;
			args.InspectorRequestAction = InspectorAction.InspectorRedirection;

			this.InspectorStartGetEvent(this, args);
		}

		/// <summary>
		/// Sends POST Requests.
		/// </summary>
		/// <param name="postUri"> Post uri.</param>
		/// <param name="values"> Values to post.</param>
		/// <param name="stateData"> ResponseBuffer from site.</param>
		/// <param name="cookies"> Cookies from site.</param>
		/// <param name="formTag"> The HtmlFormTag value.</param>
		private void GetPostRequest(string postUri,ArrayList values,ResponseBuffer stateData, CookieCollection cookies, HtmlFormTag formTag)
		{	

			// disable
			DisableFormView();

			// add Messaging
			ShowPostRequestMessage(postUri);

			if ( values != null )
				ShowPostedData(values);

			try
			{
				InitializeHttpCommands();
				postForm.ProxySettings = this.ProxySettings;

				if ( formTag.Enctype.ToLower(System.Globalization.CultureInfo.InvariantCulture) == "multipart/form-data" )
				{
					HttpRequestResponseContext context = new HttpRequestResponseContext();
					PostWebRequest postReq = new PostWebRequest();
					postReq.Form.ReadHtmlFormTag(formTag);
					Ecyware.GreenBlue.Engine.Scripting.Cookies c = new Cookies(cookies);
					postReq.Cookies =  c.GetCookies();
					postReq.RequestType = HttpRequestType.POST;
					postReq.Url = postUri;
					postReq.RequestHttpSettings = this.GetHttpPropertiesFromPanel();
					context.Request = postReq;

					postForm.StartAsyncHttpPostFileUpload(context);
				} 
				else 
				{
					postForm.StartAsyncHttpPost(postUri,this.GetHttpPropertiesFromPanel(),values,cookies);
				}
			}
			catch ( Exception ex )
			{
				this.txtMessaging.SelectionColor = Color.Red;
				this.txtMessaging.SelectedText = "Html Browser Error: " + ex.Message + "\r\n";

				textViewerForm.EditorText = ex.Message;
				// enable
				EnableFormView();
				this.navForm.StopNavigation();
				this.StopProgressBarEvent(this,new ProgressBarControlEventArgs("Ready"));
			}
		}
		
		/// <summary>
		/// Sends GET Requests.
		/// </summary>
		/// <param name="url"> Post uri.</param>
		/// <param name="values"> Values to append to url.</param>
		/// <param name="stateData"> ResponseBuffer from site.</param>
		/// <param name="cookies"> Cookies from site.</param>
		private void GetHttpRequest(string url,ArrayList values,ResponseBuffer stateData, CookieCollection cookies)
		{			
			// disable
			DisableFormView();

			// add Messaging
			ShowGetRequestMessage(GetForm.AppendToUri(url,values));

			try
			{
				InitializeHttpCommands();
				getForm.ProxySettings=this.ProxySettings;				
				getForm.StartAsyncHttpGet(url, this.GetHttpPropertiesFromPanel(), values, cookies, false);
			}
			catch (Exception ex)
			{
				this.txtMessaging.SelectionColor = Color.Red;
				this.txtMessaging.SelectedText = "Html Browser Error: " + ex.Message  + "\r\n";

				textViewerForm.EditorText  = ex.Message;
				// enable
				EnableFormView();
				this.navForm.StopNavigation();
				this.StopProgressBarEvent(this,new ProgressBarControlEventArgs("Ready"));
			}			
		}

		/// <summary>
		/// Sends GET requests.
		/// </summary>
		/// <param name="url"> Post uri.</param>
		/// <param name="stateData"> ResponseBuffer from site.</param>
		/// <param name="cookies"> Cookies from site.</param>
		private void GetHttpRequest(string url,ResponseBuffer stateData, CookieCollection cookies)
		{			
			DisableFormView();

			// add Messaging
			ShowGetRequestMessage(url);

			try
			{
				if (stateData==null)
				{
					stateData=new ResponseBuffer();
				}

				InitializeHttpCommands();
				getForm.ProxySettings = this.ProxySettings;
				getForm.StartAsyncHttpGet(url, this.GetHttpPropertiesFromPanel(), null, cookies, false);			
			}
			catch (Exception ex)
			{
				this.txtMessaging.SelectionColor = Color.Red;
				this.txtMessaging.SelectedText = "Html Browser Error: " + ex.Message  + "\r\n";

				textViewerForm.EditorText  = ex.Message;
				// enable
				EnableFormView();
				this.navForm.StopNavigation();
				this.StopProgressBarEvent(this,new ProgressBarControlEventArgs("Ready"));
			}
		
		}

		/// <summary>
		/// The result for the GET Request.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The ResponseEventArgs.</param>
		private void http_EndGetHttp(object sender,ResponseEventArgs e)
		{			
			try
			{
				if ( e != null || e.Response != null )
				{
					object[] param={e.Response};
					Invoke(new GetResponseCallback(FillWorkspace),param);			
				}
			}
			catch (Exception ex)
			{
				string message = ExceptionHandler.RegisterException(ex);
				MessageBox.Show(message,AppLocation.ApplicationName,MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		/// <summary>
		/// The result for the POST Request.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The ResponseEventArgs.</param>
		private void postRequest_EndPostHttp(object sender,ResponseEventArgs e)
		{				
			try
			{
				if ( e != null || e.Response != null )
				{
					object[] param={e.Response};
					Invoke(new GetResponseCallback(FillWorkspace),param);
				}
			}
			catch (Exception ex)
			{
				string message = ExceptionHandler.RegisterException(ex);
				MessageBox.Show(message,AppLocation.ApplicationName,MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		#endregion
		#region UI Menu
		/// <summary>
		/// Shows the parse html.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuParseHTML_Click(object sender, System.EventArgs e)
		{
			DisplayParsedHtml();
		}

		/// <summary>
		/// Shows the form editor.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuFormEditor_Click(object sender, System.EventArgs e)
		{
			ParseForms(true);
		}

		#endregion
		#region UI Document Methods

		/// <summary>
		/// Raised when a document is changed
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void dmDocuments_FocusedDocumentChanged(object sender, System.EventArgs e)
		{
			BasePluginForm plugin = (BasePluginForm)dmDocuments.FocusedDocument.Control;
			
			if ( plugin is ScriptingDataDesigner )
			{
				// Hide
				dhRightPanel.Hide();
				dhBottomPanel.Hide();
				_scriptingDataDesigner = (ScriptingDataDesigner)plugin;
			}

			if ( plugin is NavigableWebForm )
			{				
				dhRightPanel.Show();
				dhBottomPanel.Show();
				navForm = (NavigableWebForm)plugin;
			}

			if ( plugin is HtmlPrintForm )
			{
				printForm = (HtmlPrintForm)plugin;
			}
		}

		/// <summary>
		/// Adds a document to the document manager control.
		/// </summary>
		/// <param name="usercontrol"> The user control to add.</param>
		/// <param name="label"> Label caption.</param>
		/// <param name="hasFocus"> Set focus for document.</param>
		public void AddDocument(UserControl usercontrol,string label, bool hasFocus )
		{
			DocumentManager.Document newdoc = new DocumentManager.Document(usercontrol,label);

			if (!IsDocumentFormUnique(newdoc) )
			{
				dmDocuments.AddDocument(newdoc);	
			
				if ( hasFocus )
				{
					dmDocuments.FocusedDocument=newdoc;
				}
			}
		}

		/// <summary>
		/// Remove existing document and add the new document to the document manager control.
		/// </summary>
		/// <param name="usercontrol"> The user control to add.</param>
		/// <param name="label"> Label caption.</param>
		/// <param name="hasFocus"> Set focus for document.</param>
		public void RemoveAndAddDocument(UserControl usercontrol,string label, bool hasFocus)
		{
			DocumentManager.Document newdoc = new DocumentManager.Document(usercontrol,label);

			IsDocumentFormUnique(newdoc,true);			
			dmDocuments.AddDocument(newdoc);

			if ( hasFocus )
			{
				dmDocuments.FocusedDocument=newdoc;
			}
		}


		/// <summary>
		/// Returns true if document form is the unique inside the document manager, else false.
		/// </summary>
		/// <param name="document"> The document to find.</param>
		/// <param name="remove"> Sets the remove tag.</param>
		/// <returns> Returns true if unique, else false.</returns>
		private bool IsDocumentFormUnique(DocumentManager.Document document,bool remove)
		{
			// Verify that form doesn't belongs to collection
			bool isFound = false;
			DocumentManager.Document tempDoc=null;

			foreach (DocumentManager.MdiTabStrip t in dmDocuments.TabStrips)
			{
				foreach (DocumentManager.Document doc in t.Documents)
				{
					BasePluginForm b = (BasePluginForm)doc.Control;
					BasePluginForm documentControl = (BasePluginForm)document.Control;
					// if is unique and is found in collection
					if ( ( b.IsUnique ) && (b.GetType() == documentControl.GetType()) )
					{
						tempDoc = doc;
						isFound = true;
						break;
					}
				}

				if ((isFound==true) && (remove==true))
				{
					// remove
					t.Documents.Remove(tempDoc);
				}
			}

			return isFound;
		}
		/// <summary>
		/// Returns true if document form is the unique inside the document manager, else false.
		/// </summary>
		/// <param name="document"> The document to find.</param>
		private bool IsDocumentFormUnique(DocumentManager.Document document)
		{
			// Verify that form doesn't belongs to collection
			bool isFound = false;

			foreach (DocumentManager.MdiTabStrip t in dmDocuments.TabStrips)
			{
				foreach (DocumentManager.Document doc in t.Documents)
				{
					BasePluginForm b=(BasePluginForm)doc.Control;
					BasePluginForm documentControl = (BasePluginForm)document.Control;
					// if is unique and is found in collection
					if ( ( b.IsUnique ) && (b.GetType()==documentControl.GetType()) )
					{
						isFound=true;
						break;
					}
				}
			}
			return isFound;
		}

		/// <summary>
		/// Displays the redirect option in status bar.
		/// </summary>
		private ChangeStatusBarEventArgs RedirectMessageArgs
		{
			get
			{
				ChangeStatusBarEventArgs e =new ChangeStatusBarEventArgs();
				e.Text="Continue header found, a redirect was issued. Click to continue.";
				e.Index=0;
				e.ClickDelegate=new EventHandler(OnDisplayRedirect_OnClick);

				return e;
			}
		}

		/// <summary>
		/// Cleans the statusbar panels.
		/// </summary>
		public ChangeStatusBarEventArgs CleanStatusArgs
		{
			get
			{
				ChangeStatusBarEventArgs e =new ChangeStatusBarEventArgs();
				e.Text="";
				e.Index=0;

				return e;
			}
		}

		/// <summary>
		/// Event that is called when status bar is clicked.
		/// </summary>
		/// <param name="sender"> The object sender.</param>
		/// <param name="e"> The EventArgs.</param>
		private void OnDisplayRedirect_OnClick(object sender,EventArgs e)
		{
			StatusBar statusbar = (StatusBar)sender;
			ApplyUrlRedirection();
			statusbar.Panels[0].Text = "";
			statusbar.Click-=new EventHandler(OnDisplayRedirect_OnClick);
		}

		/// <summary>
		/// Event that is called when a close button is pressed
		/// </summary>
		/// <param name="sender"> The object sender.</param>
		/// <param name="e"> The EventArgs.</param>
		private void dmDocuments_CloseButtonPressed(object sender, DocumentManager.CloseButtonPressedEventArgs e)
		{
			switch ( e.TabStrip.SelectedDocument.Text.ToLower() )
			{
				case "html browser":
					break;
				case "web browser":
					break;
				default:
					BasePluginForm pluginForm = (BasePluginForm)e.TabStrip.SelectedDocument.Control;
					pluginForm.Close();
					e.TabStrip.Documents.Remove(e.TabStrip.SelectedDocument);
					break;
			}
		}


		#endregion
		#region UI Main
		/// <summary>
		/// Sets the disable form view.
		/// </summary>
		private void DisableFormView()
		{
			// disable
			tempCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
		}
		/// <summary>
		/// Sets the enable form view.
		/// </summary>
		private void EnableFormView()
		{
			// enable
			Cursor.Current = tempCursor;
		}
		#endregion
		#region Methods

		/// <summary>
		/// Updates the http properties.
		/// </summary>
		/// <returns> A HttpProperties.</returns>
		private HttpProperties GetHttpPropertiesFromPanel()
		{
			HttpProperties props = this.ClientProperties;
			PropertyTable bag = (PropertyTable)this.pgHeaders.SelectedObject;
			props.Accept = (string)bag["Accept"];

			if ( this.ClientProperties.AuthenticationSettings.UseBasicAuthentication )
			{
				props.AuthenticationSettings = this.ClientProperties.AuthenticationSettings;
			} 
			else 
			{
				props.AuthenticationSettings.UseNTLMAuthentication = this.ClientProperties.AuthenticationSettings.UseNTLMAuthentication;
			}

			props.ContentLength = Convert.ToInt64(bag["Content Length"]);
			props.ContentType = (string)bag["Content Type"];
			props.IfModifiedSince = Convert.ToDateTime(bag["If Modified Since"]);
			props.KeepAlive = (bool)bag["Keep Alive"];
			props.MediaType = (string)bag["Media Type"];
			props.Pipeline = (bool)bag["Pipeline"];
			props.Referer = (string)bag["Referer"];
			props.SendChunked = (bool)bag["Send Chunked"];
			props.TransferEncoding = (string)bag["Transfer Encoding"];
			props.UserAgent = (string)bag["User Agent"];			

			// Additional Headers
			for (int i=0;i<props.AdditionalHeaders.Length;i++)
			{					
				// Update additional headers values.
				props.SetWebHeader(props.AdditionalHeaders[i].Name,(string)bag[props.AdditionalHeaders[i].Name]);				
			}

			return props;

		}
		/// <summary>
		/// Redirects to url.
		/// </summary>
		private void ApplyUrlRedirection()
		{	
			// change to use UriBuilder methods
			// get location
			string location = (string)this.CurrentResponseBuffer.ResponseHeaderCollection["Location"];
			Uri url = (Uri)CurrentResponseBuffer.ResponseHeaderCollection["Response Uri"];

			string postUri = UriResolver.ResolveUrl(url,location);
			GetHttpRedirectRequest(postUri);
		}

		/// <summary>
		/// Displays the parsed html.
		/// </summary>
		public void DisplayParsedHtml()
		{
			if ( this.CurrentResponseBuffer == null ) {return;};
			if ( this.CurrentResponseBuffer.HttpBody != String.Empty )
			{
				Application.DoEvents();
				tempCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;

				AddParsedHtml();

				Cursor.Current = tempCursor;
			}
		}

		#endregion
		#region Text Editor Events
		// TODO: Allow only text events to work with when the focused document is HTML Browser.
		/// <summary>
		/// Displays the find dialog.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ShowFindDialog(object sender, EventArgs e)
		{
			this.textViewerForm.ShowFindDialog();
		}

		/// <summary>
		/// Displays the replace dialog.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ShowReplaceDialog(object sender, EventArgs e)
		{
			this.textViewerForm.ShowReplaceDialog();
		}

		/// <summary>
		/// Enabled / Disabled popup menu for Text Document.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuTextStream_Popup(object sender, System.EventArgs e)
		{
			if ( dirtyBit == false )
			{
				this.mnuFormEditor.Enabled=false;
				this.mnuParseHTML.Enabled=false;
				this.mnuViewBrowser.Enabled=false;
				this.mnuBrowserEditor.Enabled=false;				
			} 
			else 
			{
				this.mnuFormEditor.Enabled=true;
				this.mnuParseHTML.Enabled=true;
				this.mnuViewBrowser.Enabled=true;
				this.mnuBrowserEditor.Enabled=true;
			}		
		}

		/// <summary>
		/// Sets the html text editor caret change.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtEditor_CaretChange(object sender, EventArgs e)
		{
			SyntaxBoxControl editor = (SyntaxBoxControl)sender;
			 
			ChangeStatusBarEventArgs args = new ChangeStatusBarEventArgs();

			if ( ( editor != null ) && ( editor.Caret != null ) && ( editor.Caret.CurrentRow != null ) )
			{
				args.Index=3;
				args.Text="Line " + (editor.Caret.LogicalPosition.Y+1) + " Col " +  (editor.Caret.LogicalPosition.X+1) + " Char " + (editor.Caret.CurrentRow.Index+1);

				// update status bar
				ChangeStatusBarPanelEvent(this,args);
			}
		}

		/// <summary>
		/// Html text editor text changed event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtHTTPStream_TextChanged(object sender, EventArgs e)
		{
			if ( dirtyBit == false )
			{
				// html query editor
				//UpdateMenu(Shortcut.CtrlShiftT, this.mnuToolMenu.GetByIndex(0), true);
				// save html menu
				UpdateMenu(Shortcut.CtrlShiftI, this.mnFile.GetByIndex(2), true);
				// print html menu
				UpdateMenu(Shortcut.CtrlShiftI, this.mnFile.GetByIndex(3), true);
				dirtyBit = true;
			}

			if ( textViewerForm.EditorText == String.Empty )
			{
				// html query editor
				//UpdateMenu(Shortcut.CtrlShiftT, this.mnuToolMenu.GetByIndex(0), false);
				// save html menu
				UpdateMenu(Shortcut.CtrlShiftI, this.mnFile.GetByIndex(2), false);
				// print html menu
				UpdateMenu(Shortcut.CtrlShiftI, this.mnFile.GetByIndex(3), false);
				dirtyBit = false;
			}
		}

		/// <summary>
		/// Save html source code.
		/// </summary>
		private void SaveHtml(object sender, EventArgs e)
		{
			this.textViewerForm.SaveHtml();
		}

		/// <summary>
		/// Prints the html source code.
		/// </summary>
		private void PrintHtml(object sender, EventArgs e)
		{
			this.textViewerForm.PrintDocument();
		}
		/// <summary>
		/// Displays the HTML query editor.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void texteditor_QueryHtml(object sender, EventArgs e)
		{
			DisplayParsedHtml();
		}
		/// <summary>
		/// Displays the Forms Editor.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void texteditor_LoadFormsEditor(object sender, EventArgs e)
		{
			ParseForms(true);
		}
		#endregion
		#region Bottom Panels

		/// <summary>
		/// Loads the application properties.
		/// </summary>
		/// <param name="applicationProperties"> The InspectorConfiguration type.</param>
		private void LoadApplicationProperties(InspectorConfiguration applicationProperties)
		{
			// set quick tests settings
			// Utils.BufferOverflowGenerator genBo = new BufferOverflowGenerator();

			// txtBOTest.Text = genBo.GenerateStringBuffer(applicationProperties.DefaultBufferOverflowLength);
			// txtSqlTest.Text = applicationProperties.DefaultSqlTest;
			// txtXssTest.Text = applicationProperties.DefaultXssTest;

			textViewerForm.EnabledRichTextParsing = applicationProperties.EnabledRichTextParsing;
		}
		/// <summary>
		/// Loads the http properties.
		/// </summary>
		/// <param name="httpProperties"> The HttpProperties type.</param>
		private void LoadHttpProperties(HttpProperties httpProperties)
		{
			PropertyTable bag = new PropertyTable();			
			string category = "Request Headers";
			// string category2 = "Authentication";

			//PropertySpec item = new PropertySpec("Headers",typeof(HttpPropertiesWrapper),category,"Request Headers");
			PropertySpec accept = new PropertySpec("Accept", typeof(string),category);
			PropertySpec contentLength = new PropertySpec("Content Length",typeof(string), category);
			PropertySpec contentType = new PropertySpec("Content Type",typeof(string), category);
			PropertySpec ifModifiedSince = new PropertySpec("If Modified Since",typeof(DateTime), category);
			PropertySpec keepAlive = new PropertySpec("Keep Alive",typeof(bool), category);
			PropertySpec mediaType = new PropertySpec("Media Type",typeof(string), category);
			PropertySpec pipeline = new PropertySpec("Pipeline",typeof(bool), category);
			PropertySpec referer = new PropertySpec("Referer",typeof(string), category);
			PropertySpec sendChunked = new PropertySpec("Send Chunked",typeof(bool), category);
			PropertySpec transferEncoding = new PropertySpec("Transfer Encoding",typeof(string), category);			
			PropertySpec userAgent = new PropertySpec("User Agent",typeof(string), category);
			
			bag.Properties.AddRange(new PropertySpec[] {accept,
														   contentLength, 
														   contentType, 
														   ifModifiedSince,
														   keepAlive,
														   mediaType,
														   pipeline,
														   referer,
														   sendChunked,
														   transferEncoding,
														   userAgent});

			// Additional Headers
			for (int i=0;i<httpProperties.AdditionalHeaders.Length;i++)
			{				
				PropertySpec property = new PropertySpec(httpProperties.AdditionalHeaders[i].Name,typeof(string),"Additional Headers");
				bag.Properties.Add(property);
				bag[httpProperties.AdditionalHeaders[i].Name] = httpProperties.AdditionalHeaders[i].Value;
			}
			// PropertySpec addonHeaders = new PropertySpec("Additional headers",typeof(string[]), category);
			// PropertySpec ntlmAuth = new PropertySpec("Windows Integrated Security", typeof(bool), category2);
			// accept.ConverterTypeName = "Ecyware.GreenBlue.Controls.HttpPropertiesWrapper";

			bag[accept.Name] = httpProperties.Accept;
			bag[contentLength.Name] = httpProperties.ContentLength;
			bag[contentType.Name] = httpProperties.ContentType;
			bag[ifModifiedSince.Name] = httpProperties.IfModifiedSince;
			bag[keepAlive.Name] = httpProperties.KeepAlive;
			bag[mediaType.Name] = httpProperties.MediaType;
			bag[pipeline.Name] = httpProperties.Pipeline;
			bag[referer.Name] = httpProperties.Referer;
			bag[sendChunked.Name] = httpProperties.SendChunked;
			bag[transferEncoding.Name] = httpProperties.TransferEncoding;
			bag[userAgent.Name] = httpProperties.UserAgent;
			//bag[addonHeaders.Name] = new string[0];
			// bag[ntlmAuth.Name] = httpProperties.AuthenticationSettings.UseNTLMAuthentication;

			this.pgHeaders.SelectedObject = bag;

		}
		/// <summary>
		/// Fills the cookies list view.
		/// </summary>
		/// <param name="cookies"> The cookie collection.</param>
		private void FillCookieListView(CookieCollection cookies)
		{
			lvCookies.Items.Clear();

			foreach (System.Net.Cookie ck in cookies)
			{
				ListViewItem lvi = new ListViewItem();
				lvCookies.Items.Add(lvi);

				lvi.Text = ck.Name;
				lvi.SubItems.Add(ck.Value);
				lvi.SubItems.Add(ck.Domain);
				lvi.SubItems.Add(ck.Path);
			}
		}
		/// <summary>
		/// Fills the Request and Response ListViews
		/// </summary>
		/// <param name="response"> The current response buffer.</param>
		private void FillListViews(ResponseBuffer response)
		{
			// Clear lists
			lvResponseHeader.Items.Clear();
			// lvRequestHeader.Items.Clear();

			// add Messaging
			System.Text.StringBuilder sb = new System.Text.StringBuilder();			

			txtMessaging.SelectionColor = Color.Teal;
			txtMessaging.SelectedText = "Server Response [" + DateTime.Now.ToShortTimeString() + "]: ";

			sb.Append("HTTP/");
			sb.Append(response.Version);
			sb.Append(" ");
			sb.Append(response.StatusCode);
			sb.Append(" ");
			sb.Append(response.StatusDescription);
			sb.Append("\r\n");

			txtMessaging.SelectionColor = Color.Black;
			txtMessaging.SelectedText = sb.ToString();

			txtMessaging.Focus();
			txtMessaging.SelectionStart = txtMessaging.Text.Length;
			txtMessaging.SelectionLength = 0;
			txtMessaging.ScrollToCaret();

			
			foreach (DictionaryEntry de in response.ResponseHeaderCollection)
			{
				ListViewItem lvi = new ListViewItem();
				lvResponseHeader.Items.Add(lvi);

				lvi.Text = (string)de.Key;
				lvi.SubItems.Add(Convert.ToString(de.Value));
			}			
		}

		
		/// <summary>
		/// Fills the workspace with data retrieve from site.
		/// </summary>
		/// <param name="response">ResponseBuffer</param>
		public void FillWorkspace(ResponseBuffer response)
		{
			if (response.ErrorMessage != String.Empty)
			{
				// reset statusbar
				ChangeStatusBarPanelEvent(this, CleanStatusArgs);

				this.txtMessaging.SelectionColor = Color.Red;
				this.txtMessaging.SelectedText = "Html Browser Error: " + response.ErrorMessage  + "\r\n";

				// Show Error
				textViewerForm.EditorText  = response.ErrorMessage;
				
				// Stop progress
				this.StopProgressBarEvent(this, new ProgressBarControlEventArgs("Ready"));

				this.InspectorState = GBInspectorState.Error;
			} 
			else
			{
				// Set Editor Text 
				try
				{
					textViewerForm.EditorText = response.HttpBody;

					// save buffer, do not save before error message check				
					this.CurrentResponseBuffer = response;

					// load forms editor if any
					if ( formCollection != null )
					{
						DisplayForms(formCollection, false);
					}

					Uri responseUri = (Uri)response.ResponseHeaderCollection["Response Uri"];

					// Get Settings from Panel
					ClientProperties = this.GetHttpPropertiesFromPanel();

					// Update referer
					ClientProperties.Referer = responseUri.ToString();

					// Load properties
					LoadHttpProperties(this.ClientProperties);

					// Add to history
					AddToHistory(response);

					//					// Add to cookies
					//					cookieManager.AddCookies(response.CookieCollection);

					// Fill Lists
					FillListViews(response);
					FillCookieListView(response.CookieCollection);

					// Update address bar
					RequestGetEventArgs requestArgs = new RequestGetEventArgs();
					requestArgs.Url = responseUri.ToString();
					this.UpdateAddressEvent(this, requestArgs);

					// Update Session Request
					UpdateSessionRequest(response);
			
					EnableFormView();
					this.InspectorState = GBInspectorState.Complete;

					// Stop progress only if navigator CanLinkNavigate is true.
					if ( navForm.CanLinkNavigate )
					{
						// reset statusbar
						ChangeStatusBarPanelEvent(this, CleanStatusArgs);
						// stop progress bar
						this.StopProgressBarEvent(this,new ProgressBarControlEventArgs("Ready"));
					}

					// Location is found in Hashtable
					if ( response.ResponseHeaderCollection.ContainsKey("Location") )
					{
						// Location is not empty
						if ( ((string)response.ResponseHeaderCollection["Location"])!=String.Empty )
						{	
							// Apply direct and log in recording sesion if any.
							this.ApplyUrlRedirection();
							
							// navForm.PendingRedirection = true;
							//ChangeStatusBarPanelEvent(this,RedirectMessageArgs);						
						}
					}
				}
				catch (Exception ex)
				{
					string message = ExceptionHandler.RegisterException(ex);
					MessageBox.Show(message,AppLocation.ApplicationName, MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
			}
		}

		#endregion
		#region Recent Sites Methods
		/// <summary>
		/// Loads a Recent Sites node
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void sitesTree_DoubleClick(object sender, EventArgs e)
		{
			// do not allow selection from root node
			if ( sitesTree.SelectedNode.Parent == null ) return;

			HistoryTreeNode node = (HistoryTreeNode)sitesTree.SelectedNode;
			
//			ResponseBuffer resp = sitesTree.GetHttpSiteData(node);
//			if ( resp != null )
//			{
				// get cookies
				//CookieCollection cookies = null;
				//cookies = cookieManager.GetCookies(node.Url);

				RequestGetEventArgs requestArgs = new RequestGetEventArgs();
				requestArgs.Url = node.Url.AbsoluteUri;
				this.InspectorStartGetEvent(this, requestArgs);
//			}
		}

		/// <summary>
		/// Adds a current web site to history.
		/// </summary>
		/// <param name="response"> The response buffer.</param>
		private void AddToHistory(ResponseBuffer response)
		{
			try
			{
				Uri requestUri = (Uri)response.RequestHeaderCollection["Request Uri"];
				sitesTree.AddHistoryNodeDisk(requestUri,response);
			}
			catch (Exception ex)
			{
				string message = ExceptionHandler.RegisterException(ex);
				MessageBox.Show(message,AppLocation.ApplicationName,MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}
		#endregion		
		#region Menus
		/// <summary>
		/// Loads the workspace menus.
		/// </summary>
		public void LoadWorkspaceMenu()
		{
			LoadPluginMenuEventArgs e = new LoadPluginMenuEventArgs(this.BuildPluginMenuRoot());
			LoadPluginMenusEvent(this,e);
		}
		/// <summary>
		/// Creates the menus for the workspace.
		/// </summary>
		/// <returns> A MenuRootHashtable.</returns>
		private MenuRootHashtable BuildPluginMenuRoot()
		{
			mnRoot = new MenuRootHashtable();

			mnRoot.Add("5", new MenuRoot("Help","&Help",Shortcut.CtrlShiftH));
			mnRoot.Add("4",new MenuRoot("Tools","&Tools",Shortcut.CtrlShiftT));
			// mnRoot.Add("3",new MenuRoot("Application","&Session",Shortcut.CtrlShiftF));
			mnRoot.Add("2", new MenuRoot("View","&View",Shortcut.CtrlShiftW));
			mnRoot.Add("1", new MenuRoot("Edit","&Edit",Shortcut.CtrlShiftE));
			mnRoot.Add("0", new MenuRoot("File","&File", Shortcut.CtrlShiftI));

			mnRoot["5"].MenuItems = BuildHelpMenus();			
			mnRoot["4"].MenuItems = BuildToolMenu();
			//mnRoot["3"].MenuItems = BuildSessionMenuItems();
			mnRoot["2"].MenuItems = BuildWindowMenuItems();
			mnRoot["1"].MenuItems = BuildEditMenuItems();
			mnRoot["0"].MenuItems = BuildFileMenuItems();
			
			return mnRoot;
		}

		/// <summary>
		/// Creates the menus for File Menu.
		/// </summary>
		/// <returns> A MenuItemCollection.</returns>
		private MenuItemCollection BuildFileMenuItems()
		{	
			mnFile = new MenuItemCollection();

			//EventHandler openReportEvt = new EventHandler(this.OpenReport);
			//EventHandler saveReportEvt = new EventHandler(this.SaveReport);
			//EventHandler printReportEvt = new EventHandler(this.PrintReport);
			EventHandler saveHtmlEvt = new EventHandler(this.SaveHtml);
			EventHandler printHtmlEvt = new EventHandler(this.PrintHtml);
			EventHandler openEvt = new EventHandler(this.SessionDesigner_OpenWebSession);			
			//EventHandler runEvt = new EventHandler(LoginGreenBlueServices);

			mnFile.Add("01OpenSd",
				new Ecyware.GreenBlue.Controls.MenuItem("mnuOpenApplication","&Open Web Store Manager...",true,true,false,openEvt));
//			mnFile.Add("02Login",
//				new Ecyware.GreenBlue.Controls.MenuItem("mnuLogin","&Login to GreenBlue Services...",true,true,false,runEvt));
//			mnFile.Add("03OpenReport",
//				new Ecyware.GreenBlue.Controls.MenuItem("mnuOpenReport","&Open Report...",true,true,true,openReportEvt));
//			mnFile.Add("04SaveReport",
//				new Ecyware.GreenBlue.Controls.MenuItem("mnuSaveReport","&Save Report...",false,true,false,saveReportEvt));
//			mnFile.Add("05PrintReport",
//				new Ecyware.GreenBlue.Controls.MenuItem("mnuPrintReport","&Print Report...",false,true,false,printReportEvt));
			mnFile.Add("06SaveHtmlSource",
				new Ecyware.GreenBlue.Controls.MenuItem("mnuSaveHtmlSource","&Save HTML Source...",false,true,true,saveHtmlEvt));
			mnFile.Add("07PrintHtmlSource",
				new Ecyware.GreenBlue.Controls.MenuItem("mnuPrintHtmlSource","&Print HTML Source...",false,true,false,printHtmlEvt));

			// Report Dialog event
			EventHandler reportDialogEvt = new EventHandler(formeditor_ReportDialog);

			// Report Button
//			ToolbarItem reportButton = new ToolbarItem();
//			reportButton.Enabled = false;
//			reportButton.Name = "tbReport";
//			reportButton.Text = "Preview Report";
//			reportButton.ImageIndex = 8;
//			reportButton.ClickDelegate = reportDialogEvt;
//
//			mnFile.Add("10_Button", reportButton);
//
//			// add preview report
//			mnFile.Add("08PreviewReport", new Ecyware.GreenBlue.Controls.MenuItem("mnuReportDialog","&Report Preview...",false,true,true,reportDialogEvt));

			mnFile.Add("09Exit",
				new Ecyware.GreenBlue.Controls.MenuItem("mnuExit","&Exit",true,true,true,this.GBExit));
			
			return mnFile;
		}
		/// <summary>
		/// Creates the menus for Help Menu.
		/// </summary>
		/// <returns> A MenuItemCollection.</returns>
		private MenuItemCollection BuildHelpMenus()
		{
			mnHelp = new MenuItemCollection();

			// Query Html Event
			EventHandler helpContentsEvt = new EventHandler(OpenHelpContents);
			EventHandler aboutBoxEvt = new EventHandler(ShowAboutBox);
			
			mnHelp.Add("01Contents",
				new Ecyware.GreenBlue.Controls.MenuItem("mnuContents","&Contents...",true,true,false,helpContentsEvt));
			mnHelp.Add("02About",
				new Ecyware.GreenBlue.Controls.MenuItem("mnuAbout","&About...",true,true,true,aboutBoxEvt));
			
			return mnHelp;
		}
		/// <summary>
		/// Creates the menus for Edit Menu.
		/// </summary>
		/// <returns> A MenuItemCollection.</returns>
		private MenuItemCollection BuildEditMenuItems()
		{
			mnEditMenus = new MenuItemCollection();

			// event
			EventHandler findDialogEvt = new EventHandler(ShowFindDialog);
			EventHandler replaceDialogEvt = new EventHandler(ShowReplaceDialog);
			
			EventHandler recordSesionToggleEvent = new EventHandler(RecordSessionChanged);
			EventHandler browserRequestFirstToggleEvent = new EventHandler(BrowserRequestFirstChanged);
			EventHandler permitPopupWindowEvent = new EventHandler(PermitPopupWindowChanged);

			mnEditMenus.Add("1_Find",new Ecyware.GreenBlue.Controls.MenuItem("mnuFind","&Find",true,true,findDialogEvt));
			mnEditMenus.Add("2_Replace",new Ecyware.GreenBlue.Controls.MenuItem("mnuReplace","&Replace",true,true,replaceDialogEvt));

			ToolbarItem recordSession = new ToolbarItem();
			ToolbarItem browserRequestFirstButton = new ToolbarItem();
			ToolbarItem permitPopupWindow = new ToolbarItem();

			// Record Session
			recordSession.Enabled = true;
			recordSession.Name = "tbRecordSession";
			recordSession.Text = "Record Session";
			recordSession.Toggle = true;
			recordSession.ImageIndex = 10;
			recordSession.Delimiter = true;
			recordSession.CheckedChangedDelegate = recordSesionToggleEvent;

			//  Browser Request First Button
			browserRequestFirstButton.Enabled = true;
			browserRequestFirstButton.Name = "tbBrowserFirst";
			browserRequestFirstButton.Text = "Allow Browser Navigate First";
			browserRequestFirstButton.Toggle = true;
			browserRequestFirstButton.ImageIndex = 13;
			browserRequestFirstButton.Delimiter = false;
			browserRequestFirstButton.CheckedChangedDelegate = browserRequestFirstToggleEvent;

			//  Allow NewWindow Event
			permitPopupWindow.Enabled = true;
			permitPopupWindow.Name = "tbPermitPopup";
			permitPopupWindow.Text = "Block popups";
			permitPopupWindow.Toggle = true;
			permitPopupWindow.ImageIndex = 14;
			permitPopupWindow.Delimiter = false;
			permitPopupWindow.CheckedChangedDelegate = permitPopupWindowEvent;

			mnEditMenus.Add("2_RecordSession", recordSession);
			mnEditMenus.Add("3_BrowseFirst", browserRequestFirstButton);
			mnEditMenus.Add("4_NewWindow", permitPopupWindow);

			return mnEditMenus;
		}
	

		/// <summary>
		/// Creates the menus for Tool Menu.
		/// </summary>
		/// <returns> A MenuItemCollection.</returns>
		private MenuItemCollection BuildToolMenu()
		{
			mnuToolMenu = new MenuItemCollection();

			// Load Encode Decode Tool
			EventHandler loadEncDecTool = new EventHandler(LoadEncodeDecodeTool);
			EventHandler loadRegExDesigner = new EventHandler(LoadRegExDesigner);
			EventHandler loadXSLTDesigner = new EventHandler(LoadXsltDesigner);
			EventHandler queryHtmlEvent = new EventHandler(LoadXPathDesigner);

			// Load test manager
			EventHandler loadTestManager = new EventHandler(LoadTestManager);
			
			// Proxy options
			EventHandler proxyOptionsEvent = new EventHandler(LoadProxyOptions);
			
			// Basic Authentication Options
			EventHandler basicAuthenticationOptionsEvent = new EventHandler(LoadBasicAuthenticationOptions);

			// Application options
			EventHandler appOptionsEvent = new EventHandler(LoadApplicationOptions);

			// Add Web References
			EventHandler addWebTransformReferencesEvent = new EventHandler(AddWebTransformReferencesDialog);
			
			mnuToolMenu.Add("001QueryHtml",
				new Ecyware.GreenBlue.Controls.MenuItem("mnuQueryHtml","&HTML Query Editor",true,true,false,queryHtmlEvent));
			mnuToolMenu.Add("004TestManager",
				new Ecyware.GreenBlue.Controls.MenuItem("mnuTestManager","&Web Unit Test Template Manager...",true,true,false,loadTestManager));
			mnuToolMenu.Add("005EncodeDecodeTool",
				new Ecyware.GreenBlue.Controls.MenuItem("mnuEncodeDecodeTool","&Encode Decode Tool...",true,true,false,loadEncDecTool));
			mnuToolMenu.Add("006ProxyOptions",
				new Ecyware.GreenBlue.Controls.MenuItem("mnuProxyOptions","&Proxy Options...",true,true,false,proxyOptionsEvent));
			mnuToolMenu.Add("007BasicAuthentication",
				new Ecyware.GreenBlue.Controls.MenuItem("mnuBasciAuthentication","&Basic Authentication Options...",true,true,false,basicAuthenticationOptionsEvent));
			mnuToolMenu.Add("009Options",
				new Ecyware.GreenBlue.Controls.MenuItem("mnuAppOptions","&Options...",true,true,true,appOptionsEvent));
			mnuToolMenu.Add("008AddWebTransformReferences",
				new Ecyware.GreenBlue.Controls.MenuItem("mnuAddWebTransformReferences","&Add Web Transform References...",true,true,false,addWebTransformReferencesEvent));
			mnuToolMenu.Add("002RegEx",
				new Ecyware.GreenBlue.Controls.MenuItem("mnuRegExDesigner","RegE&x Designer", true, true, false, loadRegExDesigner));
			mnuToolMenu.Add("003Xslt",
				new Ecyware.GreenBlue.Controls.MenuItem("mnuXsltDesigner", "XSL&T Designer", true, true, false, loadXSLTDesigner));
			
			return mnuToolMenu;
		}

		/// <summary>
		/// Creates the window panels menus.
		/// </summary>
		/// <returns> A MenuItemCollection.</returns>
		private MenuItemCollection BuildWindowMenuItems()
		{
			MenuItemCollection mnWindowItems = new MenuItemCollection();

			// event
			EventHandler evt = new EventHandler(ShowControlBottomPanel);
			
			mnWindowItems.Add("1EventConsole",new Ecyware.GreenBlue.Controls.MenuItem("mnuEvtConsole","&Event Console",evt));
			mnWindowItems.Add("2ResponseHeader",new Ecyware.GreenBlue.Controls.MenuItem("mnuResponseHeader","HTTP Res&ponse Header",evt));
			mnWindowItems.Add("3RequestHeader",new Ecyware.GreenBlue.Controls.MenuItem("mnuRequestHeader","HTTP Re&quest Header",evt));
			mnWindowItems.Add("4Cookies",new Ecyware.GreenBlue.Controls.MenuItem("mnuCookies","&Cookies",evt));
			//mnWindowItems.Add("5UnitTestEventConsole",new Ecyware.GreenBlue.Controls.MenuItem("mnuUnitTestEvtConsole","&Unit Test Event Console",evt));
			//mnWindowItems.Add("6QuickTests",new Ecyware.GreenBlue.Controls.MenuItem("mnuQuickTests","&Quick Tests",evt));
			mnWindowItems.Add("7RecentSites",new Ecyware.GreenBlue.Controls.MenuItem("mnuRecentSites","&Recent Sites",evt));
			mnWindowItems.Add("8UrlSpider",new Ecyware.GreenBlue.Controls.MenuItem("mnuUrlSpider","Url &Spider",evt));
			return mnWindowItems;
		}
		/// <summary>
		/// Toggles the BottomPanels and Recent Sites.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void ShowControlBottomPanel(object sender, EventArgs e)
		{
			Reflector.UserInterface.CommandBarItem cmd = (Reflector.UserInterface.CommandBarItem)sender;
			switch (cmd.Text)
			{
				case "&HTTP Request Header":
					this.dpRequestHeader.EnsureVisible(this.dhBottomPanel);
					break;
				case "HTTP Res&ponse Header":
					this.dpResponseHeader.EnsureVisible(this.dhBottomPanel);
					break;
				case "&Event Console":
					this.dockControl1.EnsureVisible(this.dhBottomPanel);
					break;
				case "&Cookies":
					this.dpCookies.EnsureVisible(this.dhBottomPanel);
					break;
					//				case "&Quick Tests":
					//					this.dcQuickTests.EnsureVisible(this.dhBottomPanel);
					//					break;
					//				case "&Unit Test Event Console":
					//					this.dcSessionEventConsole.EnsureVisible(this.dhBottomPanel);
					//					break;
				case "&Recent Sites":
					this.dcHistory.EnsureVisible(this.dhRightPanel);					
					break;
				case "Url &Spider":
					this.dcUrlSpider.EnsureVisible(this.dhRightPanel);
					break;
			}
		}

		/// <summary>
		/// Updates any menu
		/// </summary>
		/// <param name="parentLink"> The shortcut to root menu.</param>
		/// <param name="menu"> The menu item.</param>
		/// <param name="enabled"> Sets the enabled state.</param>
		public void UpdateMenu(Shortcut parentLink, Ecyware.GreenBlue.Controls.MenuItem menu,bool enabled)
		{
			// new Arguments
			ApplyMenuSettingsEventArgs newArgs = new ApplyMenuSettingsEventArgs();
	
			// identify the shortcut
			newArgs.RootShortcut = parentLink;

			menu.Enabled = enabled;
			newArgs.MenuItems.Add(menu.Name, menu);
			
			// update menu
			this.ApplyMenuSettingsEvent(this, newArgs);

		}

		/// <summary>
		/// Updates any toolbar command.
		/// </summary>
		/// <param name="toolbarCmd"> The toolbar command.</param>
		/// <param name="enabled"> The enabled setting.</param>
		public void UpdateToolbar(ToolbarItem toolbarCmd, bool enabled)
		{
			toolbarCmd.Enabled = enabled;
			ApplyToolbarSettingsEventArgs args = new ApplyToolbarSettingsEventArgs(toolbarCmd);
			// update toolbar
			this.ApplyToolbarSettingsEvent(this, args);
		}

		/// <summary>
		/// Toggles the ReportDialogTest menu.
		/// </summary>
		/// <param name="enabled"> The enabled setting.</param>
		[Obsolete]
		public void _UpdateReportDialogTestMenu(bool enabled)
		{
			// new Arguments
			ApplyMenuSettingsEventArgs newArgs = new ApplyMenuSettingsEventArgs();
	
			// identify the shortcut
			newArgs.RootShortcut = Shortcut.CtrlShiftI;

			// get menu item by index
			Ecyware.GreenBlue.Controls.MenuItem reportDialogMenu = this.mnFile.GetByIndex(5);

			reportDialogMenu.Enabled = enabled;
			newArgs.MenuItems.Add(reportDialogMenu.Name,reportDialogMenu);

			ToolbarItem reportButton = (ToolbarItem)this.mnFile.GetByIndex(9);
			reportButton.Enabled = enabled;

			ApplyToolbarSettingsEventArgs args = new ApplyToolbarSettingsEventArgs(reportButton);

			// update toolbar
			this.ApplyToolbarSettingsEvent(this, args);
			
			// update menu
			this.ApplyMenuSettingsEvent(this, newArgs);
		}

		#endregion
		#region Line and Column indices
		private void RichTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			RichTextBox t = (RichTextBox)sender;
			Point p = Cursor.Position;
			int currentCharIndex = t.GetCharIndexFromPosition(p);
			int lineStartCharIndex = t.GetCharIndexFromPosition(new Point(0,p.Y));
			SetLineColumnIndex(currentCharIndex,lineStartCharIndex,t);
		}
		private void RichTextBox_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			RichTextBox t = (RichTextBox)sender;
			Point p = t.GetPositionFromCharIndex(t.SelectionStart);
			int currentCharIndex = t.GetCharIndexFromPosition(p);
			int lineStartCharIndex = t.GetCharIndexFromPosition(new Point(0,p.Y));
			SetLineColumnIndex(currentCharIndex,lineStartCharIndex,t);

			//SetLineColumnIndex(textViewerForm.txtHTTPStream.SelectionStart);
		}
		private void RichTextBox_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			RichTextBox t = (RichTextBox)sender;
			int currentCharIndex = t.GetCharIndexFromPosition(new Point(e.X,e.Y));
			int lineStartCharIndex = t.GetCharIndexFromPosition(new Point(0,e.Y));
			SetLineColumnIndex(currentCharIndex,lineStartCharIndex,t);
		}
		private void RichTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			//RichTextBox t = (RichTextBox)sender;
			//Point p = t.GetPositionFromCharIndex(t.SelectionStart);
			//int currentCharIndex = t.GetCharIndexFromPosition(p);
			//int lineStartCharIndex = t.GetCharIndexFromPosition(new Point(0,p.Y));
			//SetLineColumnIndex(currentCharIndex,lineStartCharIndex,t);

			//SetLineColumnIndex(textViewerForm.txtHTTPStream.SelectionStart);
		}

		/// <summary>
		/// Sets the line column index.
		/// </summary>
		/// <param name="charIndex"> The char index.</param>
		/// <param name="lineStartCharIndex"> The line start char index.</param>
		/// <param name="textbox"> The textbox control.</param>
		private void SetLineColumnIndex(int charIndex, int lineStartCharIndex, RichTextBox textbox)
		{

			if ( textbox.Text == "" ) return;

			int cIndex = charIndex;
			int lineIndex = textbox.GetLineFromCharIndex(cIndex);		

			// new charIndex			
			int columns = 0;
			int lineCharIndex = 0;
			columns = cIndex - lineStartCharIndex;
			lineCharIndex = columns+1;

			// count tabs
			char[] chars = textbox.Lines[lineIndex].ToCharArray();

			if ( cIndex > 0 )
			{
				for (int k=lineStartCharIndex;k<cIndex;k++)
				{
					if ( textbox.Text.Substring(k,1) == "\t" )
					{
						columns += 3;
					}
				}
			}
					
			ChangeStatusBarEventArgs e = new ChangeStatusBarEventArgs();

			e.Index=3;
			e.Text="Line " + (lineIndex+1) + " Col " +  (columns+1) + " Char " + lineCharIndex + " Abs Char " + cIndex;

			ChangeStatusBarPanelEvent(this,e);
		}

		#endregion
		#region Query Editor Methods
		/// <summary>
		/// Add parsed HTML window.
		/// </summary>
		private void AddParsedHtml()
		{
			parser.ParserProperties.RemoveScriptTags = this.SnifferProperties.RemoveScriptTags;
			parser.ParserProperties.RemoveStyleTags = this.SnifferProperties.RemoveStyleTags;
			
			string parsedHtml;
			try
			{
				// load XmlView
				parsedHtml = parser.GetParsableString(textViewerForm.EditorText);

				// load view
				HtmlQueryForm form = new HtmlQueryForm(parsedHtml);
				form.txtEditor.ContextMenu=this.mnuQueryEditor;
				form.txtEditor.CaretChange += new EventHandler(txtEditor_CaretChange);

				Uri url = (Uri)this.CurrentResponseBuffer.RequestHeaderCollection["Request Uri"];
				AddDocument(form,"HTML Query Editor", true);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message,AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Shows the HTML Editor.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnViewHtmlEditor_Click(object sender, System.EventArgs e)
		{
			HtmlQueryForm queryEditor = (HtmlQueryForm)dmDocuments.FocusedDocument.Control;
		}
		/// <summary>
		/// Loads the XML Tree Window.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnXmlTree_Click(object sender, System.EventArgs e)
		{
			HtmlQueryForm queryEditor = (HtmlQueryForm)dmDocuments.FocusedDocument.Control;

			try
			{
				string nodes = queryEditor.GetXmlString();

				// load view
				HtmlXmlTreeForm form = new HtmlXmlTreeForm(nodes);
				Uri url = (Uri)this.CurrentResponseBuffer.RequestHeaderCollection["Request Uri"];
				AddDocument(form,"XML Tree", true);
			}
			catch
			{
				MessageBox.Show("Could not open the XML Tree. Check that the HTTP Body is valid for parsing.",AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		#endregion
		#region HTTP Messaging Window
		/// <summary>
		/// Copies the event console to Clipboard
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuCopyEventConsole_Click(object sender, System.EventArgs e)
		{
			Clipboard.SetDataObject(this.txtMessaging.Text,false);
		}

		/// <summary>
		/// Displays the post request message.
		/// </summary>
		/// <param name="url"> The uri.</param>
		private void ShowPostRequestMessage(string url)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			
			txtMessaging.SelectionColor = Color.Teal;
			txtMessaging.SelectedText = "Client Request [" + DateTime.Now.ToShortTimeString() + "]:     ";

			sb.Append("POST ");
			sb.Append(url);
			sb.Append(" ");
			sb.Append("HTTP/1.1");
			sb.Append("\r\n");

			txtMessaging.SelectionColor = Color.Black;
			this.txtMessaging.SelectedText = sb.ToString();

			txtMessaging.Focus();
			txtMessaging.SelectionStart = txtMessaging.Text.Length;
			txtMessaging.SelectionLength = 0;
			txtMessaging.ScrollToCaret();
		}

		/// <summary>
		/// Displays the GET request message.
		/// </summary>
		/// <param name="url"> The uri.</param>
		private void ShowGetRequestMessage(string url)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();

			txtMessaging.SelectionColor = Color.Teal;
			txtMessaging.SelectedText = "Client Request [" + DateTime.Now.ToShortTimeString() + "]:     ";

			sb.Append("GET ");
			sb.Append(url);
			sb.Append(" ");
			sb.Append("HTTP/1.1");
			sb.Append("\r\n");

			txtMessaging.SelectionColor = Color.Black;
			this.txtMessaging.SelectedText = sb.ToString();

			txtMessaging.Focus();
			txtMessaging.SelectionStart = txtMessaging.Text.Length;
			txtMessaging.SelectionLength = 0;
			txtMessaging.ScrollToCaret();
		}

		/// <summary>
		/// Displays a message.
		/// </summary>
		/// <param name="message"> The message to display.</param>
		private void ShowMessage(string message)
		{
			txtMessaging.SelectionColor = Color.Black;
			txtMessaging.SelectedText = message;

			txtMessaging.Focus();
			txtMessaging.SelectionStart = txtMessaging.Text.Length;
			txtMessaging.SelectionLength = 0;
			txtMessaging.ScrollToCaret();
		}

		/// <summary>
		/// Displays a message.
		/// </summary>
		/// <param name="color"> The message color.</param>
		/// <param name="message"> The message to display.</param>
		private void ShowMessage(Color color,string message)
		{
			txtMessaging.SelectionColor = color;
			txtMessaging.SelectedText = message;

			txtMessaging.Focus();
			txtMessaging.SelectionStart = txtMessaging.Text.Length;
			txtMessaging.SelectionLength = 0;
			txtMessaging.ScrollToCaret();
		}
		/// <summary>
		/// Displays the posted data in the messaging window.
		/// </summary>
		/// <param name="al"> The arraylist containing the values.</param>
		private void ShowPostedData(ArrayList al)
		{
			System.Text.StringBuilder sb=new System.Text.StringBuilder();
			for (int k=0;k<al.Count;k++)
			{
				sb.Append(al[k]);
				sb.Append("&");
			}

			sb.Append("\r\n\r\n");
			
			this.txtMessaging.SelectionColor = Color.Teal;
			this.txtMessaging.SelectedText = "Posted data:\r\n";
			this.txtMessaging.SelectionColor = Color.Black;
			this.txtMessaging.SelectedText = sb.ToString();

			txtMessaging.Focus();
			txtMessaging.SelectionStart = txtMessaging.Text.Length;
			txtMessaging.SelectionLength = 0;
			txtMessaging.ScrollToCaret();
		}

		/// <summary>
		/// Clears the event console.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuClearMessaging_Click(object sender, System.EventArgs e)
		{
			txtMessaging.Text = "";
		}
		#endregion
		#region Reporting
		/// <summary>
		/// Opens the report.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OpenReport(object sender, EventArgs e)
		{
			// send this to disk
			System.IO.Stream stream = null;

			dlgOpenFile.CheckFileExists = true;
			dlgOpenFile.InitialDirectory = Application.UserAppDataPath;
			dlgOpenFile.RestoreDirectory = true;
			dlgOpenFile.Filter = "Ecyware GreenBlue Report Files (*.gbr)|*.gbr";			 
			dlgOpenFile.Title = "Open Report";

			if ( dlgOpenFile.ShowDialog() == DialogResult.OK )
			{
				Application.DoEvents();
				tempCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;

				// file
				stream = dlgOpenFile.OpenFile();
				if ( stream != null )
				{
					try
					{						
						// close it
						stream.Close();
						OpenReportZip(dlgOpenFile.FileName);
					}
					catch ( Exception ex )
					{
						MessageBox.Show(ex.Message,AppLocation.ApplicationName,MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}

			if (stream != null)
			{
				Cursor.Current = tempCursor;
				stream.Close();
			}	
		}

		/// <summary>
		/// Saves the report
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SaveReport(object sender, EventArgs e)
		{
			bool isHtmlReport = false;

			// send this to disk
			System.IO.Stream stream = null;

			dlgSaveFile.InitialDirectory = Application.UserAppDataPath;
			dlgSaveFile.RestoreDirectory = true;

			if ( printForm.HtmlContentResults != null )
			{
				isHtmlReport = true;
				dlgSaveFile.Filter = "Ecyware GreenBlue Report Files (*.gbr)|*.gbr";
			} 
			else 
			{
				isHtmlReport = false;
				dlgSaveFile.Filter = "XML Output (*.xml)|*.xml";
			}

			dlgSaveFile.Title = "Save Report";

			if ( dlgSaveFile.ShowDialog() == DialogResult.OK )
			{
				Application.DoEvents();
				tempCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;

				// file
				stream = dlgSaveFile.OpenFile();
				if ( stream!=null )
				{
					try
					{
						// close it
						stream.Close();

						if ( isHtmlReport )
						{
							this.SaveReportZip(printForm.GetReportFileLocation,
								printForm.GetResources,
								printForm.HtmlContentResults,
								dlgSaveFile.FileName);
						} 
						else 
						{
							this.SaveReportXml(printForm.GetReportFileLocation,dlgSaveFile.FileName);
						}
					}
					catch ( Exception ex )
					{
						MessageBox.Show(ex.Message,AppLocation.ApplicationName,MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}

			if (stream != null)
			{
				Cursor.Current = tempCursor;
				stream.Close();
			}	
		}

		/// <summary>
		/// Prints a report.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PrintReport(object sender, EventArgs e)
		{
			this.printForm.Print(true);
		}

		/// <summary>
		/// Opens the report.
		/// </summary>
		/// <param name="filePath"> The file path.</param>
		private void OpenReportZip(string filePath)
		{
			C1.C1Zip.C1ZipFile zipFile = new C1.C1Zip.C1ZipFile();
			zipFile.Open(filePath);

			string htmlReportFile = string.Empty;
			ArrayList htmlResponseFiles = new ArrayList();

			// do not extract icons, extract only html data into temphtml
			for (int i=0;i<zipFile.Entries.Count;i++)
			{
				C1.C1Zip.C1ZipEntry entry = zipFile.Entries[i];

				if ( i == 0 )
				{
					htmlReportFile = Application.UserAppDataPath + "\\temphtml\\" + entry.FileName;
					zipFile.Entries.Extract(i, htmlReportFile);
				} 
				else 
				{
					if ( entry.FileName.IndexOf("htm") > -1 )
					{
						string destinationFilePath = Application.UserAppDataPath + "\\temphtml\\" + entry.FileName.Replace("temphtml/",""); 
						zipFile.Entries.Extract(i, destinationFilePath);
						htmlResponseFiles.Add(destinationFilePath);
					}
				}
			}
			
			zipFile.Close();

			// Open report preview viewer
			LoadHtmlReportFromFile(htmlReportFile, (string[])htmlResponseFiles.ToArray(typeof(string)));
			
		}
		/// <summary>
		/// Saves the report as zip file.
		/// </summary>
		/// <param name="htmlReport"> The html report file path.</param>
		/// <param name="images"> The string array with images.</param>
		/// <param name="htmlFiles"> The string array with html file path.</param>
		/// <param name="fileName"> The zip file path.</param>
		private void SaveReportZip(string htmlReport,string[] images, string[] htmlFiles, string fileName)
		{
			
			// get file name
			string[] parts = fileName.Split('\\');
			string name = parts[parts.Length - 1];

			// check for . dots
			parts = name.Split('.');
			name = parts[0] + ".htm";

			try
			{
				C1.C1Zip.C1ZipFile zipFile = new C1.C1Zip.C1ZipFile();
				zipFile.Create(fileName);			
				zipFile.Open(fileName);
				zipFile.Entries.Add(htmlReport, name);

				// resources 
				foreach ( string s in images )
				{
					if ( File.Exists(s) )
						zipFile.Entries.Add(s);
				}

				// html files
				foreach ( string file in htmlFiles )
				{
					if ( File.Exists(file) )
						zipFile.Entries.Add(file,1);
				}

				zipFile.Close();
			} 
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message,AppLocation.ApplicationName,MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		
		/// <summary>
		/// Saves the report as xml file.
		/// </summary>
		/// <param name="xmlReport"> The xml data.</param>
		/// <param name="fileName"> The xml file path.</param>
		private void SaveReportXml(string xmlReport, string fileName)
		{
			try
			{
				StreamReader reader = new StreamReader(xmlReport);
				StreamWriter writer = new StreamWriter(fileName,false, Encoding.UTF8);
				writer.Write(reader.ReadToEnd());
				writer.Flush();
				writer.Close();
				reader.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message,AppLocation.ApplicationName,MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		/// <summary>
		/// Applies menu settings for report preview.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void Report_ApplyMenuSettingsEvent(object sender, ApplyMenuSettingsEventArgs e)
		{
			this.ApplyMenuSettingsEvent(this,e);
		}

		/// <summary>
		/// Applies toolbar settings for report preview.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void Report_ApplyToolbarSettingsEvent(object sender, ApplyToolbarSettingsEventArgs e)
		{
			this.ApplyToolbarSettingsEvent(this, e);
		}

		/// <summary>
		/// Load html report from file.
		/// </summary>
		/// <param name="reportFilePath"> The html file report.</param>
		/// <param name="htmlReponseContentFiles"> The html response content files.</param>
		private void LoadHtmlReportFromFile(string reportFilePath,string[] htmlReponseContentFiles)
		{
			printForm = new HtmlPrintForm();
			printForm.PluginMenus = this.mnFile;
			printForm.HtmlResponseViewEvent += new HtmlResponseViewEventHandler(Report_HtmlResponseViewEvent);
			printForm.ApplyToolbarSettingsEvent += new ApplyToolbarSettingsEventHandler(Report_ApplyToolbarSettingsEvent);
			printForm.ApplyMenuSettingsEvent += new ApplyMenuSettingsEventHandler(Report_ApplyMenuSettingsEvent);

			try
			{
				StreamReader reader = new StreamReader(reportFilePath, Encoding.UTF8);
				string data = reader.ReadToEnd();
				reader.Close();

				printForm.UpdateSavePrintReportMenu(true);
				printForm.HtmlContentResults = htmlReponseContentFiles;
				printForm.LoadData(data, "html");

				AddDocument(printForm, "Report Preview - HTML", true);
			}
			catch (Exception ex)
			{
				printForm.UpdateSavePrintReportMenu(false);
				MessageBox.Show(ex.Message,AppLocation.ApplicationName,MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Loads the html report viewer.
		/// </summary>
		/// <param name="reportType"> The report type.</param>
		private void LoadHtmlReportViewer(string reportType)
		{
			ReportCommand reportCmd = new ReportCommand();
			
			printForm = new HtmlPrintForm();
			printForm.PluginMenus = this.mnFile;
			printForm.HtmlResponseViewEvent += new HtmlResponseViewEventHandler(Report_HtmlResponseViewEvent);
			printForm.ApplyToolbarSettingsEvent += new ApplyToolbarSettingsEventHandler(Report_ApplyToolbarSettingsEvent);
			printForm.ApplyMenuSettingsEvent += new ApplyMenuSettingsEventHandler(Report_ApplyMenuSettingsEvent);

			try
			{				
				string logo = 
					"<div class='ReportName'><img src='../gb logo.gif' align='middle'></img>Ecyware GreenBlue Inspector Report</div><br /><br /><br />";

				// merge reports
				StringBuilder sb = new StringBuilder();
				sb.Append("<html>");
				sb.Append("<head><link rel='stylesheet' type='text/css' href='../report.css'></head>");
				sb.Append("<body topmargin='40' leftmargin='10'>");
				sb.Append(logo);

				ArrayList htmlFiles = new ArrayList();

				for (int i=0;i<this.CurrentReportList.Count;i++)
				{
					HtmlUnitTestReport report = (HtmlUnitTestReport)this.CurrentReportList[i];

					string html = string.Empty;

					if ( reportType == "basic" )
					{
						html = reportCmd.CreateHtmlReport(report,
							InspectorConfig.BasicReportTemplate,
							InspectorConfig.SolutionDataFile,
							InspectorConfig.ReferenceDataFile);	
					} 
					else 
					{
						html = reportCmd.CreateHtmlReport(report,
							InspectorConfig.AdvancedReportTemplate,
							InspectorConfig.SolutionDataFile,
							InspectorConfig.ReferenceDataFile);	
					}

					sb.Append(html);

					// Add Html Response Content to array
					if ( report.ResponseDocument[0].IsHtmlResponseFile )
					{
						htmlFiles.Add(System.Windows.Forms.Application.UserAppDataPath + "\\temphtml\\" + report.ResponseDocument[0].HtmlResponse);
					}
				}

				sb.Append("</body>");
				sb.Append("</html>");
			
				printForm.UpdateSavePrintReportMenu(true);
				printForm.HtmlContentResults = (string[])htmlFiles.ToArray(typeof(string));
				printForm.LoadData(sb.ToString(), "html");

				AddDocument(printForm, "Report Preview - HTML", true);
			}
			catch (Exception ex)
			{
				printForm.UpdateSavePrintReportMenu(false);
				MessageBox.Show(ex.Message,AppLocation.ApplicationName,MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		/// <summary>
		/// Loads the xml report viewer.
		/// </summary>
		private void LoadXmlReportViewer()
		{
			printForm = new HtmlPrintForm();
			printForm.PluginMenus = this.mnFile;
			printForm.ApplyToolbarSettingsEvent +=	new ApplyToolbarSettingsEventHandler(Report_ApplyToolbarSettingsEvent); 
			printForm.ApplyMenuSettingsEvent += new ApplyMenuSettingsEventHandler(Report_ApplyMenuSettingsEvent);

			try
			{
				// merge reports
				//StringBuilder sb=new StringBuilder();
				HtmlUnitTestReport report = new HtmlUnitTestReport();
				for (int i=0;i<this.CurrentReportList.Count;i++)
				{
					report.Merge((HtmlUnitTestReport)this.CurrentReportList[i]);
				}

				printForm.LoadData(report.GetXml(),"xml");

				AddDocument(printForm,"Report Preview - XML",true);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message,AppLocation.ApplicationName,MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			
		}

		/// <summary>
		/// Loads the page in another window.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="webForm"></param>
		private void Report_HtmlResponseViewEvent(object sender, HtmlPrintForm webForm)
		{						
			this.AddDocument(webForm,"Print Report - HTML Response View",true);			
		}
		#endregion
		#region Form Editor
		/// <summary>
		/// Request Post Event from Form Editor.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void formeditor_RequestPostEvent(object sender,RequestPostEventArgs e)
		{
			if ( e.Form.Action == String.Empty )
			{
				MessageBox.Show("Not a postable form. Check source code for any scripting use.\r\nForm name:" + e.Form.Name,AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}	
			
			Uri uri = (Uri)CurrentResponseBuffer.ResponseHeaderCollection["Response Uri"];
			
			string formUri = UriResolver.ResolveUrl(uri,e.Form.Action);

			// FormConverter formConverter = new FormConverter();

			if ( e.Method.ToLower() == "post" )
			{
				e.PostData = Encoding.UTF8.GetBytes(parser.GetString(e.Form));
				e.InspectorRequestAction = InspectorAction.UserPost;
				e.CurrentUri = new Uri(formUri);
				this.InspectorStartPostEvent(this, e);
			}
			else 
			{
				// Get Request
				RequestGetEventArgs requestArgs = new RequestGetEventArgs();
				requestArgs.InspectorRequestAction = InspectorAction.UserGet;
				requestArgs.Url = formUri;
				requestArgs.Form = e.Form;
				this.InspectorStartGetEvent(this, requestArgs);				
			}

			// Add Easy Test options
			//this.RunQuickTests(e);
		}



		/// <summary>
		/// Display the form collection in editor.
		/// </summary>
		/// <param name="forms"></param>
		/// <param name="showCreateMessage"></param>
		private void DisplayForms(HtmlFormTagCollection forms, bool showCreateMessage)
		{
			if ( forms.Count > 0 )
			{
				// Validate Forms
				if ( ValidateForms(forms, (Uri)this.CurrentResponseBuffer.ResponseHeaderCollection["Response Uri"]))
				{
					// Create new FormEditor Window and apply events
					formeditor = new FormsEditor(forms, this.CurrentResponseBuffer, this.SnifferProperties, this.InspectorConfig);
					formeditor.RequestPostEvent += new Ecyware.GreenBlue.GreenBlueMain.FormsEditor.RequestPostEventHandler(formeditor_RequestPostEvent);
					formeditor.ApplyMenuSettingsEvent += new ApplyMenuSettingsEventHandler(formeditor_ApplyMenuSettingsEvent);
					formeditor.PluginMenus = this.mnSessionMenus;
			
					Uri url = (Uri)CurrentResponseBuffer.ResponseHeaderCollection["Response Uri"];
					RemoveAndAddDocument(formeditor, "Forms Editor", false);

					if ( showCreateMessage )
					{
						string message = "A Forms Editor Tree has been created.";
						ChangeStatusBarEventArgs args = new ChangeStatusBarEventArgs(0,message,null);
						ChangeStatusBarPanelEvent(this,args);
					}
				} 
				else 
				{
					string message = ValidateFormsReport(forms);
							
					ChangeStatusBarEventArgs args = new ChangeStatusBarEventArgs(0,message,null);
					ChangeStatusBarPanelEvent(this,args);
				}
			} 
			else 
			{
				if ( showCreateMessage )
				{
					string message = "This page has no forms.";
					ChangeStatusBarEventArgs args = new ChangeStatusBarEventArgs(0,message,null);
					ChangeStatusBarPanelEvent(this,args);
				}
			}
		}


		/// <summary>
		/// Loads the form result.
		/// </summary>
		/// <param name="result"> The IAsyncResult.</param>
		private void LoadFormResult(IAsyncResult result)
		{
			try
			{
				HtmlParser parser = (HtmlParser)result.AsyncState;
				HtmlFormTagCollection forms = parser.EndLoadFrom(result);			
				this.Invoke(new LoadFormsEditorCallback(DisplayForms), new object[] {forms, false} );
			}
			catch (Exception ex)
			{
				ChangeStatusBarEventArgs args = new ChangeStatusBarEventArgs(0,"Error while generating form.",null);
				
				this.txtMessaging.SelectionColor = Color.Red;
				this.txtMessaging.SelectedText = ex.Message;
				ChangeStatusBarPanelEvent(this,args);

				// update html
				textViewerForm.EditorText=CurrentResponseBuffer.GetHtmlXml;
			}
		}


		/// <summary>
		/// Parse the forms and load in editor.
		/// </summary>
		/// <param name="showCreateMessage"></param>
		private void ParseForms(bool showCreateMessage)
		{
			Application.DoEvents();
			tempCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;

			// if not response buffer exit
			if ( CurrentResponseBuffer == null ) {return;};

			// if HttpBody is not empty then process and load forms editor
			if ( this.CurrentResponseBuffer.HttpBody != String.Empty )
			{
				// set parser properties
				parser.ParserProperties.RemoveScriptTags = this.SnifferProperties.RemoveScriptTags;
				parser.ParserProperties.RemoveStyleTags = this.SnifferProperties.RemoveStyleTags;

				// text editor dirty bit
				if ( dirtyBit )
				{
					this.CurrentResponseBuffer.GetHtmlXml = "";
					this.CurrentResponseBuffer.HttpBody = textViewerForm.EditorText;
				}
				CurrentResponseBuffer.GetHtmlXml = parser.GetParsableString(textViewerForm.EditorText);

				// Async Call
				parser.BeginLoadForm(this.CurrentResponseBuffer.GetHtmlXml,
					new AsyncCallback(LoadFormResult),
					parser);		
			}

			Cursor.Current = tempCursor;
		}

		/// <summary>
		/// Validates that the forms has an action attribute available.
		/// </summary>
		/// <param name="forms"> The HtmlFormTagCollection to validate.</param>
		/// <param name="currentUri"> The current uri of the request.</param>
		/// <returns> Returns true if forms are valid, else false.</returns>
		private bool ValidateForms(HtmlFormTagCollection forms, Uri currentUri)
		{
			bool retVal = true;

			// check each form for correctness
			foreach (DictionaryEntry de in forms)
			{
				HtmlFormTag form = (HtmlFormTag)de.Value;

				if ( form.Action.Length == 0 )
				{
					form.Action = currentUri.Authority + currentUri.AbsolutePath;
					break;
				}
			}

			return retVal;
		}
		/// <summary>
		/// Validates that the forms has an action attribute available.
		/// </summary>
		/// <param name="forms"> The HtmlFormTagCollection to validate.</param>
		/// <returns> Returns a string with the form that needs to be checked.</returns>
		private string ValidateFormsReport(HtmlFormTagCollection forms)
		{
			StringBuilder report = new StringBuilder();
			// print validate forms report for correction
			// check each form for correctness
			foreach (DictionaryEntry de in forms)
			{
				HtmlFormTag form = (HtmlFormTag)de.Value;

				if ( form.Action.Length == 0 )
				{
					report.AppendFormat("Form {0} has no Action attribute. Please add value in Editor.",form.Name);
				}
			}

			return report.ToString();
		}

		/// <summary>
		/// Applies menu settings for FormEditor.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void formeditor_ApplyMenuSettingsEvent(object sender, ApplyMenuSettingsEventArgs e)
		{
			this.ApplyMenuSettingsEvent(this,e);
		}

		private void formeditor_ReportDialog(object sender, EventArgs e)
		{
			ReportDialog rptDialog = new ReportDialog();
 
			if ( rptDialog.ShowDialog() == DialogResult.OK )
			{
				Application.DoEvents();
				tempCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;

				switch (rptDialog.SelectedReportType)
				{
					case ReportDialogOption.HTML:
						string reportType = string.Empty;
						if ( rptDialog.ReportFormatType.IndexOf("Basic") > - 1 )
						{
							reportType = "basic";
						}
						if ( rptDialog.ReportFormatType.IndexOf("Advanced") > -1 )
						{
							reportType = "advanced";
						}

						this.LoadHtmlReportViewer(reportType);
						break;
					case ReportDialogOption.XML:
						this.LoadXmlReportViewer();
						break;
				}				

				Cursor.Current = tempCursor;
			}
		
			
		}

		private void navForm_LoadFormsEditorEvent(object sender, LoadFormsEditorEventArgs e)
		{
			object[] args = new object[] {sender, e};
			Invoke(new LoadFormsEditorEventHandler(LoadFormsEditor), args);
		}

		/// <summary>
		/// Loads the form editor.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The LoadFormsEditorEventArgs.</param>
		private void LoadFormsEditor(object sender, LoadFormsEditorEventArgs e)
		{
			// Add as a pending call if is still loading.
			if ( InspectorState == GBInspectorState.Complete )
			{
				DisplayForms(e.FormCollection, false);
				formCollection = null;
			} 
			else 
			{
				formCollection = e.FormCollection;
			}
		}
		#endregion
		#region Cookie Store and Tree Logic
		/// <summary>
		/// Opens the cookie editor dialog.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuEditCookies_Click(object sender, System.EventArgs e)
		{
			// Get history tree node
			HistoryTreeNode node = (HistoryTreeNode)sitesTree.SelectedNode;

			LoadCookieEditorDialog(node.Url);
		}

		/// <summary>
		/// Opens the cookie editor dialog. 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lvCookies_DoubleClick(object sender, System.EventArgs e)
		{
		
			Uri url = (Uri)this.CurrentResponseBuffer.RequestHeaderCollection["Request Uri"];
	
			// Load Cookie Editor Dialog
			LoadCookieEditorDialog(url);

			// Refresh Cookie List View
			this.FillCookieListView(getForm.CookieManager.GetCookies(url));
		}


		/// <summary>
		/// Loads the cookie editor dialog.
		/// </summary>
		/// <param name="url"> The url.</param>
		private void LoadCookieEditorDialog(Uri url)
		{
			// get cookies
			CookieCollection cookies = null;
			cookies = getForm.CookieManager.GetCookies(url);

			if ( cookies != null )
			{
				CookieEditorDialog cookieEditor = new CookieEditorDialog();
				cookieEditor.SetCookies(cookies);
				
				if ( cookieEditor.ShowDialog() == DialogResult.OK )
				{
					// save cookie
					getForm.CookieManager.AddCookies(cookieEditor.Cookies);
				}
			} 
			else 
			{
				MessageBox.Show("No cookies found for " + url.AbsoluteUri,AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
		#endregion	
		#region Workspace Members

		/// <summary>
		/// Displays the workspace menu and toolbar items.
		/// </summary>
		public void ShowWorkspaceMenuToolbarItems()
		{			
			// new Arguments
			ApplyMenuRootSettingsEventArgs newArgs = new ApplyMenuRootSettingsEventArgs();

			foreach ( DictionaryEntry de in this.mnRoot)
			{
				MenuRoot mnu = (MenuRoot)de.Value;

				mnu.Visible = true;
				mnu.Enabled = mnu.Enabled;
				newArgs.MenuRootItems.Add(mnu.Name,mnu);
			}

			// update menu
			this.ApplyMenuRootSettingsEvent(this,newArgs);
		}
		

		/// <summary>
		/// Hides the workspace menu and toolbar items.
		/// </summary>
		public void HideWorkspaceMenuToolbarItems()
		{
			// new Arguments
			ApplyMenuRootSettingsEventArgs newArgs = new ApplyMenuRootSettingsEventArgs();

			foreach ( DictionaryEntry de in this.mnRoot)
			{
				MenuRoot mnu = (MenuRoot)de.Value;

				mnu.Visible = false;
				mnu.Enabled = mnu.Enabled;
				newArgs.MenuRootItems.Add(mnu.Name,mnu);
			}

			// update menu
			this.ApplyMenuRootSettingsEvent(this,newArgs);
		}

		#endregion
		#region Form Convertion Events
		/// <summary>
		/// Called when a OnSubmit event is raised.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The FormConvertionEventArgs.</param>
		private void navForm_FormConvertionEvent(object sender, FormConvertionArgs e)
		{
			FormConverter converter = new FormConverter();

			HtmlFormTag form = converter.ConvertToHtmlFormTag(e.FormElement, e.SiteUri);
			form = converter.AddPostDataValues(form, e.PostData);

			// Just post
			RequestPostEventArgs postArgs = new RequestPostEventArgs();
			postArgs.InspectorRequestAction = InspectorAction.WebBrowserPost;
			postArgs.Form = form;
			postArgs.Method = form.Method;
			postArgs.PostData = Encoding.UTF8.GetBytes(e.PostData);
			postArgs.CurrentUri = e.SiteUri;

			this.InspectorStartPostEvent(this, postArgs);

			// Add Quick Test options
			//this.RunQuickTests(postArgs);
		}

		/// <summary>
		/// Called when no OnSubmit event is raised.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The FormHeuriscticEventArgs.</param>
		private void navForm_FormHeuristicEvent(object sender, FormHeuristicArgs e)
		{			
			FormConverter converter = new FormConverter();
			HtmlFormTag form = converter.AddPostDataValues(e.FormTag, e.PostData);

			if ( form.Action.Length == 0 )
			{
				form.Action = e.SiteUri.Scheme + "://" + e.SiteUri.Authority + e.SiteUri.AbsolutePath;
			}
			RequestPostEventArgs postArgs = new RequestPostEventArgs();

			// TODO: Check of Method is POST or GET
			// Just post				
			postArgs.InspectorRequestAction = InspectorAction.WebBrowserPost;
			postArgs.Form = form;
			postArgs.Method = form.Method;
			postArgs.PostData = Encoding.UTF8.GetBytes(e.PostData);
			postArgs.CurrentUri = e.SiteUri;

			// request
			this.InspectorStartPostEvent(this, postArgs);

			// Add Easy Test options
			//this.RunQuickTests(postArgs);
		}
		#endregion
		#region Inspector Methods and Events
		/// <summary>
		/// Starts the progress bar event for the navigator form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void navForm_StartProgressBarEvent(object sender, ProgressBarControlEventArgs e)
		{
			this.StartProgressBarEvent(this, e);
		}

		/// <summary>
		/// Starts the post event for the inspector.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The RequestPostEventArgs.</param>
		private void InspectorStartPostEvent(object sender, RequestPostEventArgs e)
		{
			// TODO: Set options here
			// TODO: Have options for AtLeastOneRadioChecked, AtLeastOneCheckbox, AllCheckboxesChecked
			ArrayList al = parser.GetArrayList(e.Form);

			Uri uri = e.CurrentUri;
			
			// Resolve url
			string formUri = UriResolver.ResolveUrl(uri, e.Form.Action);

			// get cookies
			CookieCollection cookies = null;
			cookies = postForm.CookieManager.GetCookies(new Uri(formUri));

			// navigate ie
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			for (int k=0;k<al.Count;k++)
			{
				sb.Append(al[k]);
				sb.Append("&");
			}

			if ((e.InspectorRequestAction == InspectorAction.UserPost)
				||
				(e.InspectorRequestAction == InspectorAction.InspectorRedirection))
			{
				navForm.PostForm(formUri,e.PostData,cookies, e.InspectorRequestAction);
			}

			// Allows the browser to request first.
			if (!AllowBrowserFirst)
			{
				// Save Web Session
				AddSessionPost(formUri, Encoding.UTF8.GetString(e.PostData), e.Form, cookies);

				// Set Inspector State
				InspectorState = GBInspectorState.Requesting;

				// Update Progress Bar
				StartProgressBarEvent(this,new ProgressBarControlEventArgs("Sending post..."));

				// Execute Post request
				GetPostRequest(formUri,al,this.CurrentResponseBuffer, cookies, e.Form);
			}
		}
		/// <summary>
		/// Starts the get event for the inspector.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The RequestGetEventArgs.</param>
		private void InspectorStartGetEvent(object sender, RequestGetEventArgs e)
		{	
			ArrayList values;
			string urlToNavigate = e.Url;
			if ( e.Form != null )
			{
				values = parser.GetArrayList(e.Form);
				if ( (new Uri(e.Url)).Query.Length == 0 )
				{
					urlToNavigate = GetForm.AppendToUri(e.Url, values);
				}
			} 

			// get cookies
			CookieCollection cookies = null;
			cookies = getForm.CookieManager.GetCookies(new Uri(e.Url));

			// navigate ie
			if  ( e.InspectorRequestAction == InspectorAction.UserGet || e.InspectorRequestAction == InspectorAction.Idle )
				navForm.Navigate(urlToNavigate, cookies, e.InspectorRequestAction);

			// Allows the browser to request first.
			if (!AllowBrowserFirst)
			{
				// Save Web Session
				AddSessionGet(urlToNavigate,(new Uri(urlToNavigate)).Query, e.Form, cookies);

				// Set Inspector State
				InspectorState = GBInspectorState.Requesting;

				// Update progress bar
				StartProgressBarEvent(this,new ProgressBarControlEventArgs("Requesting site..."));

				GetHttpRequest(urlToNavigate,this.CurrentResponseBuffer, cookies);
				//				if ( e.Form == null )
				//				{					
				//				} 
				//				else 
				//				{
				//					GetHttpRequest(e.Url, values, this.CurrentResponseBuffer, cookies);
				//				}
			}
		}

		/// <summary>
		/// Cancels a pending request.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void InspectorCancelRequestEvent(object sender, EventArgs e)
		{
			this.InspectorState = GBInspectorState.Complete;
			navForm.StopNavigation();
			getForm.AbortRequest();
			postForm.AbortRequest();
			textViewerForm.EditorText = "Request Canceled.";

			// enable
			EnableFormView();
			this.StopProgressBarEvent(this,new ProgressBarControlEventArgs("Ready"));

			// reinit http commands
			InitializeHttpCommands();
		}
		#endregion
		#region Session Methods and Events

		/// <summary>
		/// Execute a GET from the session designer.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		private void GetPageFromSessionDesigner(object sender, RequestGetEventArgs args)
		{
			this.InspectorStartGetEvent(this, args);
		}
		/// <summary>
		/// Displays the session process event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unitTestCommand_DisplaySessionProcessEvent(object sender, SessionCommandProcessEventArgs e)
		{			
			ListViewItem item = new ListViewItem();			
			
			item.UseItemStyleForSubItems = true;

			switch ( e.ProcessType )
			{
				case SessionProcessType.SafeRequest:
					item.ImageIndex = 9;				
					item.Text = e.Message;
					item.SubItems.Add(e.Detail);				
					item.ForeColor = Color.Green;
					break;
				case SessionProcessType.TestRequest:
					item.ImageIndex = 8;			
					item.Text = e.Message;
					item.SubItems.Add(e.Detail);
					item.ForeColor = Color.Blue;
					break;
				case SessionProcessType.TestResultOk:
					item.ImageIndex = 10;			
					item.Text = e.Message;
					item.SubItems.Add(e.Detail);
					item.ForeColor = Color.LightSlateGray;
					break;
				case SessionProcessType.TestResultError:
					item.ImageIndex = 6;			
					item.Text = e.Message;
					item.SubItems.Add(e.Detail);
					item.ForeColor = Color.LightSlateGray;
					break;
			}

			//			lvSessionEvents.Items.Add(item);
			//			//item.Selected = true;
			//			lvSessionEvents.Refresh();
		}

		/// <summary>
		/// Displays the process event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unitTestCommand_DisplayProcessEvent(object sender, ResponseEventArgs e)
		{
			object[] param={e.Response, ((HttpState)e.State).IsLastItem};

			this.Invoke(new GetRunTestCallback(TestRun),param);	
		}

		/// <summary>
		/// Aborts the session.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void unitTestCommand_SessionAbortedEvent(object sender, SessionAbortEventArgs e)
		{
			this.Invoke(new SessionAbortEventHandler(ShowSessionAborted),new object[] {sender, e});	
		}

		/// <summary>
		/// Callback to ShowReport method.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void processor_CreateReportEvent(object sender, UnitTestSessionReportEventArgs e)
		{
			object[] param={e.Report};

			this.Invoke(new GetReportCallback(ShowReport),param);
		}

		/// <summary>
		/// Shows the Preview Report Dialog.
		/// </summary>
		/// <param name="reports"></param>
		private void ShowReport(ArrayList reports)
		{
			CurrentReportList = reports;

			//UpdateReportDialogTestMenu(true);

			// start progress bar
			this.StopProgressBarEvent(this,new ProgressBarControlEventArgs(""));

			MessageBox.Show("Session Report Complete", AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		/// <summary>
		/// Displays the executed session run.
		/// </summary>
		/// <param name="resp"> The ResponseBuffer type.</param>
		/// <param name="lastItem"> The last item flag.</param>
		private void TestRun(ResponseBuffer resp, bool lastItem)
		{
			this.textViewerForm.EditorText  += resp.HttpBody + "\r\n\r\n";
			this.textViewerForm.EditorText  += resp.ResponseHeader + "\r\n\r\n";
			this.textViewerForm.EditorText  += resp.CookieData + "\r\n\r\n";
			
//			if ( lastItem )
//			{
//				_scriptingDataDesigner.UpdateRunTestMenu(true);				
//			}
		}

		/// <summary>
		/// Display the session aborted message.
		/// </summary>
		private void ShowSessionAborted(object sender, SessionAbortEventArgs e)
		{
			string message = e.ErrorMessage;

			// start progress bar
			this.StopProgressBarEvent(this,new ProgressBarControlEventArgs(""));

			MessageBox.Show("Session Run Aborted", AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		/// <summary>
		/// Updates the current session request.
		/// </summary>
		/// <param name="response"> The ResponseBuffer type.</param>
		private void UpdateSessionRequest(ResponseBuffer response)
		{
			if ( IsRecording )
			{
				SessionRequest sr = this.CurrentSessionRecording.SessionRequests[this.CurrentSessionRecording.SessionRequests.Count-1];

				sr.RequestHeaders = this.CurrentResponseBuffer.RequestHeaderCollection;
				sr.ResponseHeaders = this.CurrentResponseBuffer.ResponseHeaderCollection;
				sr.StatusCode = this.CurrentResponseBuffer.StatusCode;
				sr.StatusDescription = this.CurrentResponseBuffer.StatusDescription;
			}
		}
		/// <summary>
		/// Adds a PostSesionRequest to a recording session.
		/// </summary>
		/// <param name="url"> The requested url.</param>
		/// <param name="postData"> The post data in bytes.</param>
		/// <param name="form"> The post form.</param>
		/// <param name="cookies"> The current cookies.</param>
		private void AddSessionPost(string url, string postData, HtmlFormTag form, CookieCollection cookies)
		{
			if ( IsRecording )
			{
				PostSessionRequest postSessionRequest = new PostSessionRequest();
				postSessionRequest.PostData = postData;
				if ( form != null )
				{
					postSessionRequest.Form = form.CloneTag();
				}
				postSessionRequest.RequestCookies = cookies;
				postSessionRequest.Url = new Uri(url);
				postSessionRequest.RequestHttpSettings = this.ClientProperties.Clone();

				this.CurrentSessionRecording.SessionRequests.Add(postSessionRequest);
			}
		}

		/// <summary>
		/// Adds a GetSessionRequest to a recording session.
		/// </summary>
		/// <param name="url"> The requested url.</param>
		/// <param name="queryString"> The url query string.</param>
		/// <param name="form"> The get form.</param>
		/// <param name="cookies"> The current cookies.</param>
		private void AddSessionGet(string url, string queryString, HtmlFormTag form, CookieCollection cookies)
		{
			if ( IsRecording )
			{
				GetSessionRequest getSessionRequest = new GetSessionRequest();
				if ( form != null )
				{
					getSessionRequest.Form = form.CloneTag();
				}
				getSessionRequest.QueryString = queryString;
				getSessionRequest.RequestCookies = cookies;
				getSessionRequest.Url = new Uri(url);
				getSessionRequest.RequestHttpSettings = this.ClientProperties.Clone();

				this.CurrentSessionRecording.SessionRequests.Add(getSessionRequest);
			}
		}

		/// <summary>
		/// Raises when a Record Session button is toggle.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RecordSessionChanged(object sender, EventArgs e)
		{
			ChangeStatusBarEventArgs statusBarArgs =new ChangeStatusBarEventArgs();

			if ( !IsRecording )
			{
				statusBarArgs.Index = 2;
				statusBarArgs.Text = "Recording";
				this.ChangeStatusBarPanelEvent(this, statusBarArgs);

				this.CurrentSessionRecording = new Session();
				IsRecording = true;

			} 
			else 
			{

				statusBarArgs.Index = 2;
				statusBarArgs.Text = "";
				this.ChangeStatusBarPanelEvent(this, statusBarArgs);

				IsRecording = false;
				
				if ( this.CurrentSessionRecording.SessionRequests.Count > 0 )
				{
					_scriptingDataDesigner = new ScriptingDataDesigner();
					_scriptingDataDesigner.PluginMenus = this.mnSessionMenus;
					// _scriptingDataDesigner.ApplyMenuSettingsEvent += new ApplyMenuSettingsEventHandler(SessionDesigner_ApplyMenuSettingsEvent);
					// _scriptingDataDesigner.ApplyToolbarSettingsEvent += new ApplyToolbarSettingsEventHandler(sessionDesigner_ApplyToolbarSettingsEvent);

					// Loads the sesion into the ui.
					//UpdateReportDialogTestMenu(false);
					_scriptingDataDesigner.LoadSession(this.CurrentSessionRecording);
					_scriptingDataDesigner.DisplayTreeView();

					// Remove any existing document and add new document.
					this.RemoveAndAddDocument(_scriptingDataDesigner, "Scripting Application Designer", true);
				}
			}
		}

		/// <summary>
		/// Loads a session designer from a file stream.
		/// </summary>
		/// <param name="file"> Session file stream to load.</param>
		public void LoadScriptingDataDesigner(string file)
		{
			_scriptingDataDesigner = new ScriptingDataDesigner(file);
			// _scriptingDataDesigner.ApplyMenuSettingsEvent += new ApplyMenuSettingsEventHandler(SessionDesigner_ApplyMenuSettingsEvent);
			// _scriptingDataDesigner.ApplyToolbarSettingsEvent += new ApplyToolbarSettingsEventHandler(sessionDesigner_ApplyToolbarSettingsEvent);
			_scriptingDataDesigner.PluginMenus = this.mnSessionMenus;
			//UpdateReportDialogTestMenu(false);
			_scriptingDataDesigner.DisplayTreeView();			

			// Remove current document if found and add new
			RemoveAndAddDocument(_scriptingDataDesigner,"Scripting Application Designer",true);
		}

		private void OnHtmlBrowserEvent(object sender, EventArgs e)
		{
			this.Invoke(new HtmlResultEventHandler(HtmlBrowserInvoker), new object[] {sender, e});
		}

		private void HtmlBrowserInvoker(object sender, EventArgs e)
		{
			HtmlTextResultEventArgs args = (HtmlTextResultEventArgs)e;

			printForm = new HtmlPrintForm();
			printForm.PluginMenus = this.mnFile;
			printForm.HtmlResponseViewEvent += new HtmlResponseViewEventHandler(Report_HtmlResponseViewEvent);
			printForm.ApplyToolbarSettingsEvent += new ApplyToolbarSettingsEventHandler(Report_ApplyToolbarSettingsEvent);
			printForm.ApplyMenuSettingsEvent += new ApplyMenuSettingsEventHandler(Report_ApplyMenuSettingsEvent);

			try
			{
				if ( args.Append )
				{
					printForm.AppendData(args.HtmlText, false);
				} 
				else 
				{
					printForm.AppendData(args.HtmlText, true);
				}

				printForm.UpdateSavePrintReportMenu(true);			
				AddDocument(printForm, "Display Results", true);
			}
			catch (Exception ex)
			{
				printForm.UpdateSavePrintReportMenu(false);
				MessageBox.Show(ex.Message,AppLocation.ApplicationName,MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		private void OnTextBrowserEvent(object sender, EventArgs e)
		{
			this.Invoke(new TextResultEventHandler(TextBrowserInvoker), new object[] {sender, e});
		}

		private void TextBrowserInvoker(object sender, EventArgs e)
		{
			HtmlTextResultEventArgs args = (HtmlTextResultEventArgs)e;

			if ( args.Append )
			{
				textViewerForm.EditorText += args.HtmlText;
			} 
			else 
			{
				textViewerForm.EditorText = args.HtmlText;
			}
		}


		/// <summary>
		/// Applies menu settings for SessionDesigner.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void SessionDesigner_ApplyMenuSettingsEvent(object sender, ApplyMenuSettingsEventArgs e)
		{
			this.ApplyMenuSettingsEvent(this,e);
		}

		/// <summary>
		/// Applies toolbar settings for SessionDesigner.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void sessionDesigner_ApplyToolbarSettingsEvent(object sender, ApplyToolbarSettingsEventArgs e)
		{
			this.ApplyToolbarSettingsEvent(this, e);
		}

		/// <summary>
		/// Opens a Web Session.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SessionDesigner_OpenWebSession(object sender,EventArgs e)
		{
			WebStoreForm webStore = new WebStoreForm();
			if ( webStore.ShowDialog() == DialogResult.OK )
			{
				webStore.Hide();
				this.Refresh();
				LoadScriptingDataDesigner(webStore.SelectedApplicationFilePath);
			}			
			webStore.Close();
		}


		/// <summary>
		/// Executes the web session tests.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
//		private void LoginGreenBlueServices(object sender,EventArgs e)
//		{
//			EcywareServicesLogin login = new EcywareServicesLogin();
//			login.ShowDialog();
//			dlgOpenFile.CheckFileExists = true;
//			dlgOpenFile.InitialDirectory = Application.UserAppDataPath;
//			dlgOpenFile.RestoreDirectory = true;
//			dlgOpenFile.Filter = "GreenBlue Scripting Application (*.gbscr)|*.gbscr";
//			dlgOpenFile.Title = "Run Scripting Application";
//
//			if ( dlgOpenFile.ShowDialog() == DialogResult.OK )
//			{
//				Application.DoEvents();
//				tempCursor = Cursor.Current;
//				Cursor.Current = Cursors.WaitCursor;
//
//				try
//				{
//					ScriptingApplicationArgumentForm inputForm = new ScriptingApplicationArgumentForm(dlgOpenFile.FileName);
//					if ( inputForm.ShowDialog() == DialogResult.OK )
//					{						
//						TestRequestDialog testRequestDialog = new TestRequestDialog();
//						testRequestDialog.Show();
//						testRequestDialog.TestRequestUntilIndex(inputForm.ScriptingApplication, inputForm.ScriptingApplication.WebRequests.Length-1);
//					}
//				}
//				catch ( Exception ex )
//				{
//					MessageBox.Show(ex.ToString());
//					Utils.ExceptionHandler.RegisterException(ex);
//					MessageBox.Show("Error while opening the web session file.", AppLocation.ApplicationName, MessageBoxButtons.OK,MessageBoxIcon.Error);
//				}
//			}
//		}

		/// <summary>
		/// Stops the session run.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SessionDesigner_StopSessionRun(object sender, EventArgs e)
		{
			if ( sessionCommand != null )
			{
				if ( sessionCommand.IsRunning )
				{
					sessionCommand.Stop();
				}
			}
		}

		/// <summary>
		/// Saves a Web Session.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SessionDesigner_SaveWebSession(object sender, EventArgs e)
		{
//			// get current unit test session
//			Session testSession = sessionDesigner.WebSession;
//			Session safeSession = sessionDesigner.SafeSession;
//
//			// send this to disk
//			System.IO.Stream stream = null;
//
//			dlgSaveFile.InitialDirectory = Application.UserAppDataPath;
//			dlgSaveFile.RestoreDirectory = true;
//			dlgSaveFile.Filter = "Ecyware GreenBlue Session Files (*.gbsession)|*.gbsession";
//			dlgSaveFile.Title = "Save Web Session";
//
//			if ( dlgSaveFile.ShowDialog() == DialogResult.OK )
//			{
//				Application.DoEvents();
//				tempCursor = Cursor.Current;
//				Cursor.Current = Cursors.WaitCursor;
//
//				// file
//				stream = dlgSaveFile.OpenFile();
//				if ( stream!=null )
//				{
//					try
//					{
//						SessionDocument document = new SessionDocument(safeSession, testSession);
//						document.SaveSessionDocument(stream);
//					}
//					catch ( Exception ex )
//					{
//						Utils.ExceptionHandler.RegisterException(ex);
//						MessageBox.Show("Error while saving the web session file.", AppLocation.ApplicationName, MessageBoxButtons.OK,MessageBoxIcon.Error);
//					}
//				}
//			}
//
//			if (stream != null)
//			{
//				Cursor.Current = tempCursor;
//				stream.Close();
//			}	
		}

		#endregion
		#region Quick Tests Logic
		/// <summary>
		/// Displays the quick input dialog for SQL Injection.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
//		private void btnSetSqlValue_Click(object sender, System.EventArgs e)
//		{
//			SqlTestDialog sqlTestDialog = new SqlTestDialog(this.InspectorConfig);
//			
//			if ( sqlTestDialog.ShowDialog() == DialogResult.OK )
//			{
//				this.txtSqlTest.Text = sqlTestDialog.SelectedValue;
//			}
//
//			sqlTestDialog.Close();
//		}

		/// <summary>
		/// Displays the quick input dialog for XSS.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
//		private void btnSetXssValue_Click(object sender, System.EventArgs e)
//		{
//			XssTestDialog xssTestDialog = new XssTestDialog(this.InspectorConfig);
//
//			if ( xssTestDialog.ShowDialog() == DialogResult.OK ) 
//			{
//				this.txtXssTest.Text = xssTestDialog.SelectedValue;
//			}
//
//			xssTestDialog.Close();
//		}

		/// <summary>
		/// Displays the quick input for Buffer Overflow.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
//		private void btnBOValue_Click(object sender, System.EventArgs e)
//		{
//			BufferOverflowGeneratorDialog boDialog = new BufferOverflowGeneratorDialog();
//
//			if ( boDialog.ShowDialog() == DialogResult.OK )
//			{
//				this.txtBOTest.Text = boDialog.SelectedValue;
//			}
//
//			boDialog.Close();
//		}
		/// <summary>
		/// Raises when any quick test is checked.
		/// </summary>
		/// <param name="e"></param>
//		private void RunQuickTests(RequestPostEventArgs e)
//		{
//			bool isAnyCheckOn = this.chkBOTest.Checked || this.chkSqlTest.Checked || this.chkXssTest.Checked;
//
//			// at least one easy test must be checked
//			if ( isAnyCheckOn && this.chkQuickTests.Checked )
//			{
//				Uri uri = (Uri)CurrentResponseBuffer.ResponseHeaderCollection["Response Uri"];
//
//				// Create new EasyTest Command
//				SoftTestCommand softTestCommand = new SoftTestCommand(
//					uri,
//					e.Form,
//					this.ProxySettings,
//					this.ClientProperties,
//					this.txtSqlTest.Text,
//					this.txtXssTest.Text,
//					this.txtBOTest.Text.Length);
//			
//				softTestCommand.BufferTest = this.chkBOTest.Checked;
//				softTestCommand.SqlTest = this.chkSqlTest.Checked;
//				softTestCommand.XssTest = this.chkXssTest.Checked;
//				//EnabledUnitTestView(true);
//			
//				// this event shows the result in Html Editor
//				softTestCommand.DisplayProcessEvent += new DisplayProcessEventHandler(EasyTest_DisplayProcessEvent);
//			
//				// this event displays a report
//				softTestCommand.CreateReportEvent += new UnitTestSessionReportEventHandler(EasyTest_CreateReportEvent);
//
//				// start progress bar
//				this.StartProgressBarEvent(this,new ProgressBarControlEventArgs("Running Quick Tests..."));
//
//				// process unit tests
//				softTestCommand.Run();
//			}
//		}
		/// <summary>
		/// Displays the process event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EasyTest_DisplayProcessEvent(object sender, EventArgs e)
		{
			ResponseEventArgs r = (ResponseEventArgs)e;
			object[] param={r.Response, ((HttpState)r.State).IsLastItem};

			this.Invoke(new GetRunTestCallback(EasyTestRunDisplay),param);	
		}

		/// <summary>
		/// Callback to ShowReport method.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void EasyTest_CreateReportEvent(object sender, UnitTestSessionReportEventArgs e)
		{
			object[] param={e.Report};

			this.Invoke(new GetReportCallback(EasyTestReport),param);
		}

		
		/// <summary>
		/// Shows the Preview Report Dialog.
		/// </summary>
		/// <param name="reports"> The array list with the reports.</param>
		private void EasyTestReport(ArrayList reports)
		{
			CurrentReportList = reports;
			//EnabledUnitTestView(false);
			//UpdateReportDialogTestMenu(true);

			// start progress bar
			this.StopProgressBarEvent(this,new ProgressBarControlEventArgs(""));
		}

		/// <summary>
		/// Displays the executed session run.
		/// </summary>
		/// <param name="resp"> The ResponseBuffer type.</param>
		/// <param name="lastItem"> The last item flag.</param>
		private void EasyTestRunDisplay(ResponseBuffer resp, bool lastItem)
		{
			this.textViewerForm.EditorText  += resp.HttpBody + "\r\n\r\n";
			this.textViewerForm.EditorText  += resp.ResponseHeader + "\r\n\r\n";
			this.textViewerForm.EditorText  += resp.CookieData + "\r\n\r\n";			
		}

		#endregion
		#region Navigator events
		/// <summary>
		/// Displays the browser url synchronization information.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void navForm_DisplayBrowserUrlSync(object sender, EventArgs e)
		{
			string responseUri = ((Uri)this.CurrentResponseBuffer.ResponseHeaderCollection["Response Uri"]).ToString();

			// Check url sync
			if ( navForm.GetLocation != responseUri )
			{
				this.txtMessaging.SelectionColor = Color.Blue;
				this.txtMessaging.SelectedText = "Web Browser and Html Browser location not synchronized.\r\n";

				this.txtMessaging.SelectionColor = Color.Teal;
				this.txtMessaging.SelectedText = "Web Browser location: ";
						
				this.txtMessaging.SelectionColor = Color.Black;
				this.txtMessaging.SelectedText = navForm.GetLocation + "\r\n";

				this.txtMessaging.SelectionColor = Color.Teal;
				this.txtMessaging.SelectedText = "Html Browser location: ";
						
				this.txtMessaging.SelectionColor = Color.Black;
				this.txtMessaging.SelectedText = responseUri + "\r\n";
			} 
			else 
			{
				this.txtMessaging.SelectionColor = Color.Blue;
				this.txtMessaging.SelectedText = "Web Browser and Html Browser location synchronized.\r\n";
			}

			txtMessaging.Focus();
			txtMessaging.SelectionStart = txtMessaging.Text.Length;
			txtMessaging.SelectionLength = 0;
			txtMessaging.ScrollToCaret();
		}
		/// <summary>
		/// The new browser window event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="webForm"></param>
		private void navForm_NewWindowEvent(object sender, NavigableWebForm webForm)
		{			
			//webForm.IsUnique = true;
			AttachWebFormEvents(webForm);
			this.AddDocument(webForm,"Popup Browser",true);			
		}
		/// <summary>
		/// The load document event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void navForm_LoadDocumentEvent(object sender, EventArgs e)
		{
			// reset statusbar
			ChangeStatusBarPanelEvent(this, CleanStatusArgs);
			this.StopProgressBarEvent(this,new ProgressBarControlEventArgs("Ready"));
		}

		/// <summary>
		/// Attach the web form events.
		/// </summary>
		/// <param name="wb">The NavigableWebForm Type.</param>
		private void AttachWebFormEvents(NavigableWebForm wb)
		{
			wb.DisplayBrowserUrlSync += new EventHandler(navForm_DisplayBrowserUrlSync);
			wb.NewWindowEvent += new NewWindowEventHandler(navForm_NewWindowEvent);
			wb.StartEvent += new InspectorStartRequestEventHandler(InspectorStartGetEvent);		
			wb.FormConvertionEvent += new OnFormConvertionEventHandler(navForm_FormConvertionEvent);
			wb.FormHeuristicEvent += new OnFormHeuristicEventHandler(navForm_FormHeuristicEvent);
			wb.LoadFormsEditorEvent += new LoadFormsEditorEventHandler(navForm_LoadFormsEditorEvent);
			wb.LoadDocumentEvent += new OnLoadHtmlDocumentEventHandler(navForm_LoadDocumentEvent);
			wb.LoadLinksEvent += new LoadLinksEventHandler(navForm_LoadLinksEvent);
		}
		#endregion
		#region Proxy, Basic Authentication and Application Settings

		/// <summary>
		/// Loads the add web transform references dialog.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The event arguments.</param>
		private void AddWebTransformReferencesDialog(object sender, EventArgs e)
		{
			AddWebTransformReferences dialog = new AddWebTransformReferences();

			dialog.ShowDialog();
		}

		/// <summary>
		/// Loads the application options.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The event arguments.</param>
		private void LoadApplicationOptions(object sender, EventArgs e)
		{
			ApplicationOptions appOptions = new ApplicationOptions();
			appOptions.ClientSettings = ClientProperties;
			appOptions.ApplicationSettings = InspectorConfig;

			if ( appOptions.ShowDialog() == DialogResult.OK )
			{
				ClientProperties = appOptions.ClientSettings;
				InspectorConfig = appOptions.ApplicationSettings;				
			}

			LoadApplicationProperties(InspectorConfig);
			LoadHttpProperties(ClientProperties);
		}

		/// <summary>
		/// Loads the proxy dialog options.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LoadProxyOptions(object sender, EventArgs e)
		{
			ProxyDialog proxyDialog = new ProxyDialog(this.ProxySettings);
			proxyDialog.IsProxySettingSet = this.IsProxyEnabled;
			proxyDialog.ShowDialog();
			if ( proxyDialog.IsProxySettingSet )
			{
				this.IsProxyEnabled = proxyDialog.IsProxySettingSet;
				this.ProxySettings = proxyDialog.ProxySettings;
			}

			proxyDialog.Close();			
		}

		/// <summary>
		/// Loads the basic authentication dialog.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LoadBasicAuthenticationOptions(object sender, EventArgs e)
		{
			BasicAuthenticationDialog basicAuthenticationDialog = new BasicAuthenticationDialog();

			basicAuthenticationDialog.ShowDialog();

			// apply settings
			if ( basicAuthenticationDialog.IsBasicAuthenticationSet )
			{
				this.ClientProperties.AuthenticationSettings.UseBasicAuthentication = true;
				this.ClientProperties.AuthenticationSettings.Username = basicAuthenticationDialog.Username;
				this.ClientProperties.AuthenticationSettings.Password = basicAuthenticationDialog.Password;
				this.ClientProperties.AuthenticationSettings.Domain = basicAuthenticationDialog.Domain;
			}

			basicAuthenticationDialog.Close();
		}

		#endregion
		#region Toolbar and Quick Test Control Events
		/// <summary>
		/// Sets the allow browser navigate first.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BrowserRequestFirstChanged(object sender, EventArgs e)
		{
			this.AllowBrowserFirst = !this.AllowBrowserFirst;
		}

		/// <summary>
		/// Sets the block popups.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PermitPopupWindowChanged(object sender, EventArgs e)
		{
			navForm.AllowNewWindow = !navForm.AllowNewWindow;
		}


		/// <summary>
		/// Sets the Quick Test checkbox.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
//		private void chkQuickTests_CheckedChanged(object sender, System.EventArgs e)
//		{
//			this.groupBox1.Enabled = this.chkQuickTests.Checked;
//		}

		#endregion
		#region Help Events
		/// <summary>
		/// Displays the about box.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void ShowAboutBox(object sender, EventArgs e)
		{
			AboutWindow about = new AboutWindow();

			about.ShowDialog();
		}

		/// <summary>
		/// Opens the user guide help.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void OpenHelpContents(object sender, EventArgs e)
		{
			try
			{
				System.Diagnostics.Process process = new System.Diagnostics.Process();
				process.StartInfo.FileName = AppLocation.CommonFolder + "\\Ecyware GreenBlue Inspector User Guide.pdf";
				process.StartInfo.UseShellExecute = true;
				process.Start();
			}
			catch
			{
				MessageBox.Show("User Guide not found",AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
		#endregion
		#region Url Spider
		/// <summary>
		/// Navigates to a link.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void urlSpiderControl_DoubleClickNode(object sender, System.EventArgs e)
		{
			// do not allow selection from root node
			if ( urlSpiderControl.GetNodeLink() != string.Empty ) 
			{
				string nodeUrl = urlSpiderControl.GetNodeLink();
				if ( nodeUrl.Length > 0 )
				{
					Uri currentUrl = (Uri)this.CurrentResponseBuffer.ResponseHeaderCollection["Response Uri"];
					nodeUrl = UriResolver.ResolveUrl(currentUrl, nodeUrl);				
					// Navigate
					this.InspectorStartGetEvent(this, new RequestGetEventArgs(nodeUrl));
				}
			}
		}

		/// <summary>
		/// Loads the links into the URL spider control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void navForm_LoadLinksEvent(object sender, LoadLinksEventArgs e)
		{
			object[] args = new object[] {sender, e};
			this.Invoke(new LoadLinksEventHandler(LoadUrlSpider), args);
		}

		/// <summary>
		/// Loads the UrlSpider
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The LoadLinksEventArgs.</param>
		private void LoadUrlSpider(object sender, LoadLinksEventArgs e)
		{
			int requestCount = e.Anchors.Count + e.Links.Count +  CurrentResponseBuffer.Scripts.Count;

//			// enabled url spider commands
//			if ( requestCount == 0 ) 
//			{
//				// disabled 
//				this.btnOpenTemplate.Enabled = false;
//				this.btnRunUrlSpider.Enabled = false;
//			} 
//			else 
//			{
//				// enabled
//				this.btnOpenTemplate.Enabled = true;
//				//this.btnRunUrlSpider.Enabled = false;
//			}

			urlSpiderControl.SuspendLayout();
			urlSpiderControl.Clear();
			TreeNode frameParent = urlSpiderControl.AddRootNode("Frames");
			TreeNode anchorParent = urlSpiderControl.AddRootNode("Anchor Elements");
			TreeNode linkParent = urlSpiderControl.AddRootNode("Link Elements");		
			TreeNode scriptParent = urlSpiderControl.AddRootNode("External Scripts");

			this.urlSpiderControl.ImageList = this.imgIcons;
			anchorParent.ImageIndex = 7;
			anchorParent.SelectedImageIndex = 7;
			linkParent.ImageIndex = 7;
			linkParent.SelectedImageIndex = 7;
			scriptParent.SelectedImageIndex = 7;
			scriptParent.ImageIndex = 7;
			frameParent.ImageIndex = 7;
			frameParent.SelectedImageIndex = 7;

			// new session request collection to add urls
			SessionRequestList urlRequests = new SessionRequestList();
			string url = string.Empty;

			foreach ( HtmlAnchorTag tag in e.Anchors )
			{			
				if ( tag.HRef.StartsWith("http") )
				{
					// add node
					urlSpiderControl.AddChildren(anchorParent.Index,tag.HRef,tag.HRef,1);

					// add url request
					AddUrlRequest(urlRequests,tag.HRef);
				}
			}

			foreach ( HtmlLinkTag tag in e.Links )
			{
				// add node
				urlSpiderControl.AddChildren(linkParent.Index,tag.HRef + " (" + tag.MimeType + ")",tag.HRef,1);

				url = UriResolver.ResolveUrl((Uri)this.CurrentResponseBuffer.ResponseHeaderCollection["Response Uri"],tag.HRef);

				// add url request
				AddUrlRequest(urlRequests,url);
			}			

			foreach ( HtmlScript tag in CurrentResponseBuffer.Scripts )
			{				
				if ( tag.Source != string.Empty )
				{
					url = UriResolver.ResolveUrl((Uri)this.CurrentResponseBuffer.ResponseHeaderCollection["Response Uri"],tag.Source);

					// add node
					urlSpiderControl.AddChildren(scriptParent.Index,tag.Source,tag.Source,1);

					// add url request
					AddUrlRequest(urlRequests,url);
				}
			}

			foreach ( HtmlLinkTag frame in e.Frames  )
			{				
				if ( frame.HRef != string.Empty )
				{
					url = UriResolver.ResolveUrl((Uri)this.CurrentResponseBuffer.ResponseHeaderCollection["Response Uri"],frame.HRef);

					// add node
					urlSpiderControl.AddChildren(frameParent.Index,frame.HRef,frame.HRef,1);

					// add url request
					AddUrlRequest(urlRequests,url);
				}
			}

			urlSpiderControl.UrlRequests = urlRequests;
			urlSpiderControl.ResumeLayout(true);
		}

		/// <summary>
		/// Add url request to session request list.
		/// </summary>
		/// <param name="requests"> The SessionRequestList.</param>
		/// <param name="url"> The url.</param>
		private void AddUrlRequest(SessionRequestList requests, string url)
		{
			if ( url.StartsWith("http") )
			{
				GetSessionRequest getSessionRequest = new GetSessionRequest();
				getSessionRequest.QueryString = url;
				getSessionRequest.RequestCookies = getForm.CookieManager.GetCookies(new Uri(url));
				getSessionRequest.Url = new Uri(url);
				getSessionRequest.RequestHttpSettings = this.ClientProperties.Clone();
				requests.Add(getSessionRequest);
			}
		}


		/// <summary>
		/// Opens a template for the url spider.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
//		private void btnOpenTemplate_Click(object sender, System.EventArgs e)
//		{
//			// send this to disk
//			System.IO.Stream stream = null;
//
//			dlgOpenFile.CheckFileExists = true;
//			dlgOpenFile.InitialDirectory = Application.UserAppDataPath;
//			dlgOpenFile.RestoreDirectory = true;
//			dlgOpenFile.Filter = "Web Unit Test Template Files (*.gbtt)|*.gbtt";			 
//			dlgOpenFile.Title = "Open Web Unit Test Template";
//
//			if ( dlgOpenFile.ShowDialog() == DialogResult.OK )
//			{
//				Application.DoEvents();
//				tempCursor = Cursor.Current;
//				Cursor.Current = Cursors.WaitCursor;
//
//				// file
//				stream = dlgOpenFile.OpenFile();
//				if ( stream != null )
//				{
//					try
//					{			
//						// Load template
//						UnitTestTemplateManager testManager = new UnitTestTemplateManager();
//						_testTemplate = testManager.OpenTemplate(stream);
//
//						// Enabled run tests
//						btnRunUrlSpider.Enabled = true;
//
//						FileInfo file = new FileInfo(dlgOpenFile.FileName);
//						 
//						// Set template name.						
//						ShowMessage("Template '" + file.Name + "' loaded.\r\n");
//					}
//					catch ( Exception ex )
//					{
//						MessageBox.Show(ex.Message,AppLocation.ApplicationName,MessageBoxButtons.OK, MessageBoxIcon.Error);
//					}
//				}
//			}
//
//			if (stream != null)
//			{
//				Cursor.Current = tempCursor;
//				stream.Close();
//			}					
//		}

		/// <summary>
		/// Runs the url spider tests.
		/// </summary>
		private void RunUrlSpider()
		{
			Application.DoEvents();
			tempCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;

			foreach ( DictionaryEntry de in _testTemplate )
			{
				Test test = (Test)de.Value;
				test.UnitTestDataType = UnitTestDataContainer.NoPostData;
			}

			foreach ( SessionRequest sr in urlSpiderControl.UrlRequests )
			{
				sr.WebUnitTest.Tests = _testTemplate;
			}

			//EnabledUnitTestView(true);

			// Create new session
			Session safeSession =  new Session();
			safeSession.IsCookieUpdatable = false;
			safeSession.SessionDate = DateTime.Now;
			safeSession.AllowSafeRequestBacktracking = false;
			safeSession.SessionRequests = urlSpiderControl.UrlRequests;			

			// Run Session Command
			sessionCommand = new SessionCommand(safeSession, safeSession.CloneSession());
			sessionCommand.ProtocolProperties = this.ClientProperties;
			sessionCommand.Proxy = this.ProxySettings;

			// this event is for aborting
			sessionCommand.SessionAbortedEvent += new SessionAbortEventHandler(unitTestCommand_SessionAbortedEvent);			
			// this event displays a report
			sessionCommand.CreateReportEvent += new UnitTestSessionReportEventHandler(UrlSpider_CreateReportEvent);
			// this event displays the progress
			sessionCommand.DisplaySessionProcessEvent += new SessionCommandProcessEventHandler(unitTestCommand_DisplaySessionProcessEvent);			
			// start progress bar
			StartProgressBarEvent(this,new ProgressBarControlEventArgs("Running Url Spider..."));
			// process unit tests
			sessionCommand.Run();

			Cursor.Current = tempCursor;		

		}

		/// <summary>
		/// Callback to ShowReport method.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void UrlSpider_CreateReportEvent(object sender, UnitTestSessionReportEventArgs e)
		{
			object[] param={e.Report};

			this.Invoke(new GetReportCallback(UrlSpiderShowReport),param);
		}

		/// <summary>
		/// Shows the Preview Report Dialog.
		/// </summary>
		/// <param name="reports"> The reports.</param>
		private void UrlSpiderShowReport(ArrayList reports)
		{			
			CurrentReportList = reports;
			//EnabledUnitTestView(false);
			//UpdateReportDialogTestMenu(true);

			// start progress bar
			this.StopProgressBarEvent(this,new ProgressBarControlEventArgs(""));

			MessageBox.Show("Url Spider Report Complete", AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		/// <summary>
		/// Runs the url spider.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
//		private void btnRunUrlSpider_Click(object sender, System.EventArgs e)
//		{
//			RunUrlSpider();
//		}
		#endregion
		#region Test Manager and Encode Decode Tool
		/// <summary>
		/// Load Encode Decode Tool
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LoadEncodeDecodeTool(object sender, EventArgs e)
		{
			EncodeDecodeDialog encodeDecodeDialog = new EncodeDecodeDialog();
			encodeDecodeDialog.ShowDialog();
		}
		/// <summary>
		/// Loads the RegEx designer.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LoadRegExDesigner(object sender, System.EventArgs e)
		{
			RegExDesignerDialog dialog = new RegExDesignerDialog();
			bool displayDialog = false;
			
			if ( _currentResponse != null )
			{
				if ( _currentResponse.HttpBody.Length > 0 )
				{
					dialog.TextContent = _currentResponse.HttpBody;
					displayDialog = true;
					dialog.Show();
				}
			}			

			if ( displayDialog == false )
			{
				MessageBox.Show("No HTTP Body data found.", AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		/// <summary>
		/// Loads the XSLT designer.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LoadXsltDesigner(object sender, System.EventArgs e)
		{
			XsltDialog dialog = new XsltDialog();
			bool displayDialog = false;
			
			if ( _currentResponse != null )
			{
				string s;

				if ( _currentResponse.GetHtmlXml.Length > 0 )
				{
					s = _currentResponse.GetHtmlXml;
				}
				else 
				{
					s = this.parser.GetParsableString(_currentResponse.HttpBody);
				}

				dialog.XmlString = s;
				displayDialog = true;
				dialog.Show();
			}			

			if ( displayDialog == false )
			{
				MessageBox.Show("No HTTP Body data found.", AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}		
		}

		private void LoadXPathDesigner(object sender, System.EventArgs e)
		{
			XmlXpathDialog dialog = new XmlXpathDialog();
			bool displayDialog = false;
			
			if ( _currentResponse != null )
			{
				string s;

				if ( _currentResponse.GetHtmlXml.Length > 0 )
				{
					s = _currentResponse.GetHtmlXml;
				}
				else 
				{
					s = this.parser.GetParsableString(_currentResponse.HttpBody);
				}	

				dialog.XmlString = s;
				displayDialog = true;
				dialog.Show();

			}			

			if ( displayDialog == false )
			{
				MessageBox.Show("The HTML cannot be transformed to XML. Use the RegEx Query dialog instead.", AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}


		/// <summary>
		/// Load the test manager.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void LoadTestManager(object sender, EventArgs e)
		{
			UnitTestTemplateManager testManager = new UnitTestTemplateManager();
			testManager.LoadTestManager(this.InspectorConfig);
			testManager.ShowDialog();
		}
		#endregion

	}
}
