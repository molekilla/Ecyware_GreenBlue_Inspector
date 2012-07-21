// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: November 2003 - July 2004
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Ecyware.GreenBlue.Controls;
using Ecyware.GreenBlue.Controls.Scripting;
using Ecyware.GreenBlue.Protocols.Http.Scripting;
using Ecyware.GreenBlue.Protocols.Http;
using Reflector.UserInterface;


namespace Ecyware.GreenBlue.SessionScriptingDesigner
{
	/// <summary>
	/// Updates the address bar with the current url.
	/// </summary>
	public delegate void UpdateAddressEventHandler(object sender, RequestGetEventArgs e);

	/// <summary>
	/// Disables the address bar.
	/// </summary>
	public delegate void DisableAddressBarEventHandler(object sender, EventArgs e);

	/// <summary>
	/// Enables the address bar.
	/// </summary>
	public delegate void EnableAddressBarEventHandler(object sender, EventArgs e);

	/// <summary>
	/// Contains the definition for the Ecyware GreenBlue Inspector Start Form
	/// </summary>
	public sealed class StartForm : BaseStartForm
	{
		MenuItemCollection mnHelp;
		MenuItemCollection mnSessionMenus;
		ScriptingDataDesigner scriptingDataDesigner;
		TextEditorForm textViewerForm = new TextEditorForm();

		private CommandBarManager commandBarManager = new CommandBarManager();
		private CommandBar menubar = new CommandBar(CommandBarStyle.Menu);	
		private CommandBar toolbar = new CommandBar(CommandBarStyle.ToolBar);		

//		internal event InspectorStartRequestEventHandler StartEvent;
//		internal event InspectorCancelRequestEventHandler CancelEvent;

		private IWorkspacePlugin currentWorkspacePlugin = null;
		private DocumentManager.DocumentManager dmPanels;
		private System.Windows.Forms.StatusBar stStatus;
		private System.Windows.Forms.StatusBarPanel pnStatusMessage;
		private System.Windows.Forms.StatusBarPanel pnOtherInfo;
		private System.Windows.Forms.StatusBarPanel statusBarPanel1;
		private System.Windows.Forms.StatusBarPanel pnProgressBar;
		private System.Windows.Forms.Timer progressBarTimer;
		private System.Windows.Forms.ProgressBar pgBrowserProgress;
		private System.Windows.Forms.ImageList imgToolbar24;
		private System.Windows.Forms.ImageList imgToolbar32;
		private System.Windows.Forms.ImageList imgToolbar16;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Creates a new StartForm.
		/// </summary>
		public StartForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			InitializeStartForm();
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(StartForm));
			this.stStatus = new System.Windows.Forms.StatusBar();
			this.pnStatusMessage = new System.Windows.Forms.StatusBarPanel();
			this.pnProgressBar = new System.Windows.Forms.StatusBarPanel();
			this.pnOtherInfo = new System.Windows.Forms.StatusBarPanel();
			this.statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
			this.imgToolbar24 = new System.Windows.Forms.ImageList(this.components);
			this.imgToolbar32 = new System.Windows.Forms.ImageList(this.components);
			this.dmPanels = new DocumentManager.DocumentManager();
			this.progressBarTimer = new System.Windows.Forms.Timer(this.components);
			this.pgBrowserProgress = new System.Windows.Forms.ProgressBar();
			this.imgToolbar16 = new System.Windows.Forms.ImageList(this.components);
			((System.ComponentModel.ISupportInitialize)(this.pnStatusMessage)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnProgressBar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pnOtherInfo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).BeginInit();
			this.SuspendLayout();
			// 
			// stStatus
			// 
			this.stStatus.Location = new System.Drawing.Point(0, 544);
			this.stStatus.Name = "stStatus";
			this.stStatus.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																						this.pnStatusMessage,
																						this.pnProgressBar,
																						this.pnOtherInfo,
																						this.statusBarPanel1});
			this.stStatus.ShowPanels = true;
			this.stStatus.Size = new System.Drawing.Size(795, 22);
			this.stStatus.TabIndex = 3;
			// 
			// pnStatusMessage
			// 
			this.pnStatusMessage.Icon = ((System.Drawing.Icon)(resources.GetObject("pnStatusMessage.Icon")));
			this.pnStatusMessage.MinWidth = 250;
			this.pnStatusMessage.Width = 450;
			// 
			// pnProgressBar
			// 
			this.pnProgressBar.Alignment = System.Windows.Forms.HorizontalAlignment.Center;
			this.pnProgressBar.MinWidth = 100;
			// 
			// pnOtherInfo
			// 
			this.pnOtherInfo.MinWidth = 50;
			this.pnOtherInfo.Width = 80;
			// 
			// statusBarPanel1
			// 
			this.statusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.statusBarPanel1.Width = 149;
			// 
			// imgToolbar24
			// 
			this.imgToolbar24.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imgToolbar24.ImageSize = new System.Drawing.Size(24, 24);
			this.imgToolbar24.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgToolbar24.ImageStream")));
			this.imgToolbar24.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// imgToolbar32
			// 
			this.imgToolbar32.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imgToolbar32.ImageSize = new System.Drawing.Size(32, 32);
			this.imgToolbar32.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgToolbar32.ImageStream")));
			this.imgToolbar32.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// dmPanels
			// 
			this.dmPanels.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dmPanels.Location = new System.Drawing.Point(0, 0);
			this.dmPanels.Name = "dmPanels";
			this.dmPanels.Size = new System.Drawing.Size(795, 566);
			this.dmPanels.TabIndex = 7;
			this.dmPanels.CloseButtonPressed += new DocumentManager.DocumentManager.CloseButtonPressedEventHandler(this.dmPanels_CloseButtonPressed);
			this.dmPanels.FocusedDocumentChanged += new System.EventHandler(this.dmPanels_FocusedDocumentChanged);
			// 
			// progressBarTimer
			// 
			this.progressBarTimer.Tick += new System.EventHandler(this.progressBarTimer_Tick);
			// 
			// pgBrowserProgress
			// 
			this.pgBrowserProgress.Dock = System.Windows.Forms.DockStyle.Right;
			this.pgBrowserProgress.Location = new System.Drawing.Point(701, 0);
			this.pgBrowserProgress.Name = "pgBrowserProgress";
			this.pgBrowserProgress.Size = new System.Drawing.Size(94, 544);
			this.pgBrowserProgress.TabIndex = 10;
			this.pgBrowserProgress.Visible = false;
			// 
			// imgToolbar16
			// 
			this.imgToolbar16.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imgToolbar16.ImageSize = new System.Drawing.Size(16, 16);
			this.imgToolbar16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgToolbar16.ImageStream")));
			this.imgToolbar16.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// StartForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(795, 566);
			this.Controls.Add(this.pgBrowserProgress);
			this.Controls.Add(this.stStatus);
			this.Controls.Add(this.dmPanels);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(803, 600);
			this.Name = "StartForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Ecyware GreenBlue Session Scripting Designer";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.Load += new System.EventHandler(this.StartForm_Load);
			((System.ComponentModel.ISupportInitialize)(this.pnStatusMessage)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnProgressBar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pnOtherInfo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.statusBarPanel1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new StartForm());
		}


		#region Methods
		/// <summary>
		/// Initializes the start form.
		/// </summary>
		void InitializeStartForm()
		{
			//SetStyle(ControlStyles.DoubleBuffer, true);
			//SetStyle(ControlStyles.AllPaintingInWmPaint, true);

			this.stStatus.Controls.Add(this.pgBrowserProgress);	
			this.SuspendLayout();

			// Add menu, toolbar and address toolbar to command bar manager
			this.commandBarManager.CommandBars.Add(this.menubar);
			this.commandBarManager.CommandBars.Add(this.toolbar);
			// this.commandBarManager.CommandBars.Add(this.addressToolbar);
			this.Controls.Add(this.commandBarManager);


//			GBInspectorWorkspace inspector = new GBInspectorWorkspace();
//			this.StartEvent += inspector.StartEventDelegate;
//			this.CancelEvent += inspector.CancelEventDelegate;
//		
//			inspector.GBExit += new EventHandler(this.mnuExit_Click);
//			inspector.DisableAddressBar += new DisableAddressBarEventHandler(inspector_DisableAddressBar);
//			inspector.EnableAddressBar += new EnableAddressBarEventHandler(inspector_EnableAddressBar);
//			inspector.UpdateAddressEvent += new UpdateAddressEventHandler(inspector_UpdateAddressEvent);
//			inspector.StartProgressBarEvent += new StartProgressBarEventHandler(inspector_StartProgressBarEvent);
//			inspector.StopProgressBarEvent += new StopProgressBarEventHandler(inspector_StopProgressBarEvent);
//			inspector.ChangeStatusBarPanelEvent += new ChangeStatusBarEventHandler(inspector_ChangeStatusBarPanelEvent);
//			inspector.LoadPluginMenusEvent += new LoadPluginMenusEventHandler(inspector_LoadPluginMenusEvent);
//			inspector.ApplyToolbarSettingsEvent += new ApplyToolbarSettingsEventHandler(inspector_ApplyToolbarSettingsEvent);
//			inspector.ApplyMenuSettingsEvent += new ApplyMenuSettingsEventHandler(inspector_ApplyMenuSettingsEvent);
//			inspector.ApplyMenuRootSettingsEvent += new ApplyMenuRootSettingsEventHandler(inspector_ApplyMenuRootSettingsEvent);
			scriptingDataDesigner = new ScriptingDataDesigner();
			// scriptingDataDesigner.HttpGetPageEvent += new HttpGetPageEventHandler(GetPageFromSessionDesigner);
			scriptingDataDesigner.ApplyMenuSettingsEvent += new ApplyMenuSettingsEventHandler(inspector_ApplyMenuSettingsEvent);
			scriptingDataDesigner.ApplyToolbarSettingsEvent += new ApplyToolbarSettingsEventHandler(inspector_ApplyToolbarSettingsEvent);

			DocumentManager.Document inspectorDoc;			
			inspectorDoc = new DocumentManager.Document(scriptingDataDesigner,"Scripting Application Designer");
			dmPanels.AddDocument(inspectorDoc);			
		
			// set focused doc
			dmPanels.FocusedDocument = inspectorDoc;
			this.ResumeLayout(true);

			LoadPluginMenuEventArgs args = new LoadPluginMenuEventArgs(LoadWorkspaceMenu());
			inspector_LoadPluginMenusEvent(this, args);
		}

		#region Scripting Designer
		/// <summary>
		/// Aborts the session.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SessionAbortedEvent(object sender, SessionAbortEventArgs e)
		{
			this.Invoke(new SessionAbortEventHandler(ShowSessionAborted),new object[] {sender, e});	
		}

		/// <summary>
		/// Display the session aborted message.
		/// </summary>
		/// <param name="message"> The abort message to display.</param>
		private void ShowSessionAborted(object sender, SessionAbortEventArgs e)
		{
			string message = e.ErrorMessage;

			//EnabledUnitTestView(false);

//			ListViewItem item = new ListViewItem();			
//			item.Text = "Session aborted - Error: " + message;
//			item.ForeColor = Color.Red;				
//			lvSessionEvents.Items.Add(item);

			// start progress bar
			StopProgress(e.Message);


			MessageBox.Show("Session Run Aborted", "Ecyware GreenBlue Inspector", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
		private void OnHtmlBrowserEvent(object sender, EventArgs e)
		{
			this.Invoke(new HtmlResultEventHandler(HtmlBrowserInvoker), new object[] {sender, e});
		}

		private void HtmlBrowserInvoker(object sender, EventArgs e)
		{
			HtmlTextResultEventArgs args = (HtmlTextResultEventArgs)e;

			HtmlPrintForm printForm = new HtmlPrintForm();
			// printForm.PluginMenus = this.mn;
//			printForm.HtmlResponseViewEvent += new HtmlResponseViewEventHandler(Report_HtmlResponseViewEvent);
//			printForm.ApplyToolbarSettingsEvent += new ApplyToolbarSettingsEventHandler(Report_ApplyToolbarSettingsEvent);
//			printForm.ApplyMenuSettingsEvent += new ApplyMenuSettingsEventHandler(Report_ApplyMenuSettingsEvent);

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
				AddWorkspace(printForm, "Display Results");
			}
			catch (Exception ex)
			{
				printForm.UpdateSavePrintReportMenu(false);
				MessageBox.Show(ex.Message,"Ecyware GreenBlue Inspector",MessageBoxButtons.OK, MessageBoxIcon.Error);
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

			AddWorkspace(textViewerForm, "Display Results");
		}
		#endregion
		/// <summary>
		/// Adds a new workspace.
		/// </summary>
		/// <param name="usercontrol"> User control to add.</param>
		/// <param name="label"> Name.</param>
		public override void AddWorkspace(UserControl usercontrol,string label)
		{
			DocumentManager.Document newdoc = new DocumentManager.Document(usercontrol,label);

			if ( !IsDocumentInTabPage(newdoc) )
			{
				dmPanels.AddDocument(newdoc);
			}
		}

		/// <summary>
		/// Checks if a document is in any tab page.
		/// </summary>
		/// <param name="document"> The document to lookup.</param>
		/// <returns> Returns true if found, else false.</returns>
		private bool IsDocumentInTabPage(DocumentManager.Document document)
		{
			// Verify that form doesn't belongs to collection
			bool hasTabForm = false;

			foreach (DocumentManager.MdiTabStrip t in dmPanels.TabStrips)
			{
				//foreach (DocumentManager.Document d in t.Documents)
				//{
				if  ( t.Documents.Contains(document) )
				{
					hasTabForm = true;
					break;
				}
				//}
			}
			return hasTabForm;
		}
		#endregion

		#region Control events
		private void dmPanels_CloseButtonPressed(object sender, DocumentManager.CloseButtonPressedEventArgs e)
		{	
			// e.TabStrip.Documents.Remove(e.TabStrip.SelectedDocument);
		}
		#endregion

		private void mnuExit_Click(object sender, EventArgs e)
		{
			this.Close();
			this.Dispose(true);
		}

		/// <summary>
		/// Change status bar event.
		/// </summary>
		/// <param name="sender"> Object sender.</param>
		/// <param name="e"> Event Args.</param>
		public void inspector_ChangeStatusBarPanelEvent(object sender,ChangeStatusBarEventArgs e)
		{
			stStatus.Panels[e.Index].Text=e.Text;
			if (e.ClickDelegate!=null)
			{
				stStatus.Click+=e.ClickDelegate;
			}
		}

		#region Menu and Toolbar Methods
		/// <summary>
		/// Applies the menu settings.
		/// </summary>
		/// <param name="sender"> Sender object.</param>
		/// <param name="e"> ApplyMenuSettingsEventArgs arguments.</param>
		public void inspector_ApplyMenuSettingsEvent(object sender, ApplyMenuSettingsEventArgs e)
		{
			this.ApplySettingsMenuItems(e.RootShortcut, e.MenuItems);
		}

		/// <summary>
		/// Applies the toolbar settings.
		/// </summary>
		/// <param name="sender"> Sender object.</param>
		/// <param name="e"> ApplyMenuSettingsEventArgs arguments.</param>
		public void inspector_ApplyToolbarSettingsEvent(object sender, ApplyToolbarSettingsEventArgs e)
		{
			this.ApplySettingsToolbarItems(e.ToolbarCommand);
		}

		/// <summary>
		/// Loads the plugin menus.
		/// </summary>
		/// <param name="sender"> Sender object.</param>
		/// <param name="e"> LoadPluginMenuEventArgs arguments.</param>
		public void inspector_LoadPluginMenusEvent(object sender, LoadPluginMenuEventArgs e)
		{
			// Root Menus
			MenuRootHashtable ht = e.MenuRoot;

			SortedList root = new SortedList(ht,null);

			this.SuspendLayout();

			//1: For each MenuRoot, add MenuItems
			foreach (DictionaryEntry de in root)
			{
				MenuRoot r = (MenuRoot)de.Value;

				// Create Menu Root
				CommandBarMenu menuRoot = new CommandBarMenu(r.Text);
				menuRoot.MenuShortcut = r.Shortcut;				

				// add MenuRoot
				menubar.Items.Add(menuRoot);

				// if null then exit
				if ( r.MenuItems == null ) break;

				#region Create commands and commandlinks
				//2: Add Menu Children
				for (int i=0;i<r.MenuItems.Count;i++)
				{
					Ecyware.GreenBlue.Controls.MenuItem mn = (Ecyware.GreenBlue.Controls.MenuItem)r.MenuItems[r.MenuItems.GetKey(i)];

					if ( mn is Ecyware.GreenBlue.Controls.ToolbarItem )
					{
						#region Add Toolbar item

						// Create Command and add to CommandHolder
						Ecyware.GreenBlue.Controls.ToolbarItem tbItem = (Ecyware.GreenBlue.Controls.ToolbarItem)mn;

						// add any submenu toolbar
						if ( tbItem.IsDropDown )
						{
							/*
							// for command with submenu
							if ( tbItem.ImageIndex > -1 )
							{
								Image img = this.imgToolbar24.Images[tbItem.ImageIndex];
							}
							CommandBarMenu m = toolbar.Items.AddMenu(img, tbItem.Text);
							m.DropDown += tbItem.DropDownDelegate;
							*/
						} 
						else 
						{
							if ( tbItem.Toggle )
							{
								#region Single Command for toggle or checked type
								CommandBarCheckBox toolbarItem = new CommandBarCheckBox(tbItem.Text);						
							
								toolbarItem.IsEnabled = tbItem.Enabled;
								toolbarItem.IsVisible = tbItem.Visible;
								toolbarItem.MenuShortcut = tbItem.Shortcut;
								if ( tbItem.ImageIndex > -1 )					
									toolbarItem.Image = this.imgToolbar24.Images[tbItem.ImageIndex];

								toolbarItem.Click += tbItem.CheckedChangedDelegate;
								toolbar.Items.Add(toolbarItem);
								#endregion

							} 
							else 
							{
								#region Single Command not checked
								CommandBarButton toolbarItem = new CommandBarButton(tbItem.Text);
						
								toolbarItem.IsEnabled = tbItem.Enabled;
								toolbarItem.IsVisible = tbItem.Visible;
								toolbarItem.MenuShortcut = tbItem.Shortcut;	
								if ( tbItem.ImageIndex > -1 )		
									toolbarItem.Image = this.imgToolbar24.Images[tbItem.ImageIndex];

								toolbarItem.Click += tbItem.ClickDelegate;
								toolbar.Items.Add(toolbarItem);
								#endregion
							}
							// Add delimiter
							if ( mn.Delimiter )
							{
								toolbar.Items.AddSeparator();
							}
						}					
						#endregion
					} 
					else 
					{
						if ( mn.Toggle )
						{
							#region Add menu with checked
							CommandBarCheckBox menuItem = new CommandBarCheckBox(mn.Text);
							menuItem.IsVisible = mn.Visible;
							menuItem.IsEnabled = mn.Enabled;
							//if ( mn.ImageIndex > -1 )
							//	menuItem.Image = this.imgToolbar16.Images[mn.ImageIndex];
							menuItem.MenuShortcut = mn.Shortcut;
							menuItem.Click += mn.CheckedChangedDelegate;
							#endregion
							// Add delimiter
							if ( mn.Delimiter )
							{
								menuRoot.Items.AddSeparator();
							}

							menuRoot.Items.Add(menuItem);
						} 
						else 
						{
							#region Add Menu
							CommandBarButton menuItem = new CommandBarButton(mn.Text);
							menuItem.IsVisible = mn.Visible;
							menuItem.IsEnabled = mn.Enabled;
							if ( mn.ImageIndex > -1 )
								menuItem.Image = this.imgToolbar16.Images[mn.ImageIndex];
							menuItem.MenuShortcut = mn.Shortcut;
							menuItem.Click += mn.ClickDelegate;
							#endregion
							// Add delimiter
							if ( mn.Delimiter )
							{
								menuRoot.Items.AddSeparator();
							}

							menuRoot.Items.Add(menuItem);
						}
					}
				}
				#endregion
			}		
			this.ResumeLayout(false);			
		}
		private void inspector_ApplyMenuRootSettingsEvent(object sender, ApplyMenuRootSettingsEventArgs e)
		{
			this.ApplySettingsMenuRootItems(e.MenuRootItems);
		}
		private void ApplySettingsToolbarItems(ToolbarItem toolbarItem)
		{
			// toolbars
			foreach ( CommandBarItem cmdTool in toolbar.Items )
			{
				if ( cmdTool.Text == toolbarItem.Text )
				{
					cmdTool.IsEnabled = toolbarItem.Enabled;
					cmdTool.IsVisible = toolbarItem.Visible;
					break;
				}
			}
		}
		/// <summary>
		/// Apply settings to menu items.
		/// </summary>
		/// <param name="commandShortcut"> The root menu shortcut.</param>
		/// <param name="menus"> The menu items collection.</param>
		private void ApplySettingsMenuItems(Shortcut commandShortcut,MenuItemCollection menus)
		{
			CommandBarMenu rootMenu = null;

			// Get the root menu first
			for (int i=0;i<this.menubar.Items.Count;i++)
			{
				// check if found
				if ( menubar.Items[i].MenuShortcut == commandShortcut )
				{
					rootMenu = (CommandBarMenu)menubar.Items[i];
					break;
				}
			}
			
			if ( rootMenu != null )
			{
				foreach ( DictionaryEntry de in menus )
				{
					Ecyware.GreenBlue.Controls.MenuItem mn = (Ecyware.GreenBlue.Controls.MenuItem)de.Value;

					// text to match
					string match = mn.Text;
					bool isMatch = false;
					CommandBarItem menuItem = null;
					int j=0;

					while ( isMatch == false )
					{
						if ( rootMenu.Items[j].Text == match )
						{
							menuItem = rootMenu.Items[j];
							menuItem.IsEnabled = mn.Enabled;
							menuItem.IsVisible = mn.Visible;
							break;
						}

						j++;
					}
				}
			}
		}

		/// <summary>
		/// Apply settings to root menus.
		/// </summary>
		/// <param name="menuRoot"> The root menus collection.</param>
		private void ApplySettingsMenuRootItems(MenuRootHashtable menuRoot)
		{
			foreach (DictionaryEntry de in menuRoot)
			{
				Ecyware.GreenBlue.Controls.MenuRoot menuList = (MenuRoot)de.Value;
				
				CommandBarMenu rootMenu = null;

				// Get the root menu first
				for (int i=0;i<this.menubar.Items.Count;i++)
				{
					// check if found
					if ( menubar.Items[i].MenuShortcut == menuList.Shortcut )
					{
						rootMenu = (CommandBarMenu)menubar.Items[i];
						break;
					}
				}

				if ( rootMenu != null )
				{
					// Apply settings
					rootMenu.IsVisible = menuList.Visible;
					rootMenu.IsEnabled = menuList.Enabled;
				}
			}
		}

		#endregion
		#region Progress Bar Code
		
		/// <summary>
		/// Starts the progress bar.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void inspector_StartProgressBarEvent(object sender, ProgressBarControlEventArgs e)
		{
			StartProgress(e.Message);
		}

		/// <summary>
		/// Stops the progress bar.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void inspector_StopProgressBarEvent(object sender, ProgressBarControlEventArgs e)
		{
			StopProgress(e.Message);
		}

		private void progressBarTimer_Tick(object sender, System.EventArgs e)
		{
			if ( this.pgBrowserProgress.Value == this.pgBrowserProgress.Maximum)
			{
				this.pgBrowserProgress.Value = 0;
			}
			this.pgBrowserProgress.Value++;		
		}

		/// <summary>
		/// Starts the progress bar.
		/// </summary>
		/// <param name="message"> Message to display.</param>
		public void StartProgress(string message)
		{
			if ( !pgBrowserProgress.Visible )
			{
				pgBrowserProgress.Value = 0;
				pgBrowserProgress.Visible = true;
				this.pnStatusMessage.Text = message;
				this.progressBarTimer.Start();
			}
		}

		/// <summary>
		/// Stops the progress bar.
		/// </summary>
		/// <param name="message"> Message to display.</param>
		public void StopProgress(string message)
		{
			this.progressBarTimer.Stop();
				
			for(int i=pgBrowserProgress.Value;i<pgBrowserProgress.Maximum;i++)
			{
				pgBrowserProgress.Value = i;
				System.Threading.Thread.Sleep(5);
			}
			pgBrowserProgress.Visible = false;
			pnStatusMessage.Text = message;
		}
		#endregion
		#region On Panel Focus Changed
		private void dmPanels_FocusedDocumentChanged(object sender, System.EventArgs e)
		{
//			IWorkspacePlugin workspacePlugin = (BasePluginForm)dmPanels.FocusedDocument.Control;
//
//			if ( currentWorkspacePlugin != workspacePlugin )
//			{
//				// hide previous selected workspace
//				if ( currentWorkspacePlugin != null )
//					currentWorkspacePlugin.HideWorkspaceMenuToolbarItems();
//
//				// display focused workspace and set as current workspace plugin
//				workspacePlugin.ShowWorkspaceMenuToolbarItems();
//				currentWorkspacePlugin = workspacePlugin;
//			}
//			BasePluginForm plugin = (BasePluginForm)dmDocuments.FocusedDocument.Control;
			
//			if ( plugin is HtmlPrintForm )
//			{
//				printForm = (HtmlPrintForm)plugin;
//			}
		}
		#endregion
		#region Menus
		private void StartForm_Load(object sender, System.EventArgs e)
		{			

		}

		private MenuRootHashtable LoadWorkspaceMenu()
		{
			MenuRootHashtable mnRoot = new MenuRootHashtable();

			mnRoot.Add("3", new MenuRoot("Help","&Help",Shortcut.CtrlShiftH));						
			mnRoot.Add("2", new MenuRoot("View","&View",Shortcut.CtrlShiftW));
			mnRoot.Add("1",new MenuRoot("File","&File",Shortcut.CtrlShiftF));			


			mnRoot["3"].MenuItems = BuildHelpMenus();			
			mnRoot["2"].MenuItems = BuildWindowMenuItems();
			mnRoot["1"].MenuItems = BuildSessionMenuItems();
						
			return mnRoot;
		}

		/// <summary>
		/// Creates the menus for Help Menu.
		/// </summary>
		/// <returns> A MenuItemCollection.</returns>
		private MenuItemCollection BuildHelpMenus()
		{
			mnHelp = new MenuItemCollection();

			// Query Html Event
			EventHandler helpContentsEvt = null;// new EventHandler(OpenHelpContents);
			EventHandler aboutBoxEvt = null;// new EventHandler(ShowAboutBox);
			
			mnHelp.Add("01Contents",
				new Ecyware.GreenBlue.Controls.MenuItem("mnuContents","&Contents...",true,true,false,helpContentsEvt));
			mnHelp.Add("02About",
				new Ecyware.GreenBlue.Controls.MenuItem("mnuAbout","&About...",true,true,true,aboutBoxEvt));
			
			return mnHelp;
		}
	

		/// <summary>
		/// Creates the session menus.
		/// </summary>
		/// <returns> A MenuItemCollection.</returns>
		private MenuItemCollection BuildSessionMenuItems()
		{
			mnSessionMenus = new MenuItemCollection();

			// Save Web Session event
			// EventHandler saveEvt = new EventHandler(this.SessionDesigner_SaveWebSession);
			
			// Open Unit event
			EventHandler openEvt = new EventHandler(scriptingDataDesigner.OpenFile);
			
			// Run Unit event
			// EventHandler runEvt = new EventHandler(this.SessionDesigner_RunWebSession);
			
			// Stop Session Run
			// EventHandler stopSessionRunEvt = new EventHandler(SessionDesigner_StopSessionRun);
			
			mnSessionMenus.Add("1OpenSd", new Ecyware.GreenBlue.Controls.MenuItem("mnuOpenUTS","&Open Scripting Data...",true,true,openEvt));
			// mnSessionMenus.Add("2SaveSd", new Ecyware.GreenBlue.Controls.MenuItem("mnuSaveUTS","&Save Scripting Data As...",false,true,true,saveEvt));
			// mnSessionMenus.Add("3RunSd", new Ecyware.GreenBlue.Controls.MenuItem("mnuRunUTS","&Run Scripting Data",false,true,runEvt));
			// mnSessionMenus.Add("4StopSessionRun", new Ecyware.GreenBlue.Controls.MenuItem("mnuStopSessionRun","S&top Session Run",false,true,stopSessionRunEvt));
			mnSessionMenus.Add("5Exit", new Ecyware.GreenBlue.Controls.MenuItem("mnuExit", "E&xit", true, true, new EventHandler(this.mnuExit_Click)));
		
			return mnSessionMenus;
		}


		/// <summary>
		/// Creates the window panels menus.
		/// </summary>
		/// <returns> A MenuItemCollection.</returns>
		private MenuItemCollection BuildWindowMenuItems()
		{
			MenuItemCollection mnWindowItems = new MenuItemCollection();

			// event
			EventHandler evt = null;
			
			mnWindowItems.Add("1Designer",new Ecyware.GreenBlue.Controls.MenuItem("mnuSessionScripting","&Session Scripting Designer",evt));
			mnWindowItems.Add("2TextBrowser",new Ecyware.GreenBlue.Controls.MenuItem("mnuTextViewer","Text Viewer",evt));
			mnWindowItems.Add("3HtmlBrowser",new Ecyware.GreenBlue.Controls.MenuItem("mnuHtmlViewer","HTML Viewer",evt));

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
			recordSession.CheckedChangedDelegate = null;

			//  Browser Request First Button
			browserRequestFirstButton.Enabled = true;
			browserRequestFirstButton.Name = "tbBrowserFirst";
			browserRequestFirstButton.Text = "Allow Browser Navigate First";
			browserRequestFirstButton.Toggle = true;
			browserRequestFirstButton.ImageIndex = 13;
			browserRequestFirstButton.Delimiter = false;
			browserRequestFirstButton.CheckedChangedDelegate = null;

			//  Allow NewWindow Event
			permitPopupWindow.Enabled = true;
			permitPopupWindow.Name = "tbPermitPopup";
			permitPopupWindow.Text = "Block popups";
			permitPopupWindow.Toggle = true;
			permitPopupWindow.ImageIndex = 14;
			permitPopupWindow.Delimiter = false;
			permitPopupWindow.CheckedChangedDelegate = null;

			mnWindowItems.Add("4_RecordSession", recordSession);
			mnWindowItems.Add("5_BrowseFirst", browserRequestFirstButton);
			mnWindowItems.Add("6_NewWindow", permitPopupWindow);

			return mnWindowItems;
		}
		#endregion
	}
}
