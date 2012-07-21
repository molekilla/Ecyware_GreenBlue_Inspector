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
using Reflector.UserInterface;
using Ecyware.GreenBlue.Engine;


namespace Ecyware.GreenBlue.GreenBlueMain
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
		static string loadScriptingApp = string.Empty;
		private ComboBox cmbAddressUrl = new ComboBox();

		private CommandBarManager commandBarManager = new CommandBarManager();
		private CommandBar menubar = new CommandBar(CommandBarStyle.Menu);	
		private CommandBar toolbar = new CommandBar(CommandBarStyle.ToolBar);
		private CommandBar addressToolbar = new CommandBar(CommandBarStyle.ToolBar);

		internal event InspectorStartRequestEventHandler StartEvent;
		internal event InspectorCancelRequestEventHandler CancelEvent;

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
			this.dmPanels.Size = new System.Drawing.Size(795, 544);
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
			this.pgBrowserProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.pgBrowserProgress.Location = new System.Drawing.Point(452, 546);
			this.pgBrowserProgress.Name = "pgBrowserProgress";
			this.pgBrowserProgress.Size = new System.Drawing.Size(94, 20);
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
			this.Controls.Add(this.dmPanels);
			this.Controls.Add(this.stStatus);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(803, 600);
			this.Name = "StartForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Ecyware GreenBlue Services Designer";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
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
		static void Main(string[] args) 
		{
			if ( args.Length == 1 )
			{
				loadScriptingApp = (string)args[0];
				Application.Run(new StartForm());
			} 
			else 
			{

//				try
//				{
//					// Check User Credential
//					EcywareServicesLogin loginDialog = new EcywareServicesLogin();
//
//					if ( loginDialog.IsSecureLogin )
//					{
//						loginDialog.AutoLogin();
//					} 
//					
//					if ( loginDialog.ShowDialog() == DialogResult.OK  )
//					{				
						Application.Run(new StartForm());
//					}					
//				}
//				catch (LicenseException licEx)
//				{
//					Utils.ExceptionHandler.RegisterException(licEx);
//					LicenseInvalidSplashScreen licSplash = new LicenseInvalidSplashScreen();
//					licSplash.ShowDialog();
//				}
//				catch ( Exception ex)
//				{
//					Utils.ExceptionHandler.RegisterException(ex);
//					MessageBox.Show("An error ocurred in the application. Check the Event Log for more information.",AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
//				}	
			}
		}


		#region Methods
		/// <summary>
		/// Initializes the start form.
		/// </summary>
		void InitializeStartForm()
		{
			//SetStyle(ControlStyles.DoubleBuffer, true);
			//SetStyle(ControlStyles.AllPaintingInWmPaint, true);

			StartSplashScreen splash = new StartSplashScreen();
			splash.Show();

			this.SuspendLayout();
			cmbAddressUrl.Width = 750;
			// cmbAddressUrl.Anchor = AnchorStyles.Left | AnchorStyles.Right;
			cmbAddressUrl.Font = this.Font;
			cmbAddressUrl.Text = "Enter Url";

			// Address Url toolbar
			cmbAddressUrl.GotFocus += new EventHandler(cmbAddressUrl_GotFocus);
			cmbAddressUrl.SelectionChangeCommitted += new EventHandler(cmbAddressUrl_SelectionChangeCommitted);
			cmbAddressUrl.KeyPress += new KeyPressEventHandler(cmbAddressUrl_KeyPress);

			addressToolbar.Items.AddComboBox("Combo Box", cmbAddressUrl);
			addressToolbar.Items.AddButton(this.imgToolbar24.Images[4],"Go",new EventHandler(btnGo_Click),Keys.U);
			addressToolbar.Items.AddButton(this.imgToolbar24.Images[0],"Stop",new EventHandler(btnStop_Click));

			// Add menu, toolbar and address toolbar to command bar manager
			this.commandBarManager.CommandBars.Add(this.menubar);
			this.commandBarManager.CommandBars.Add(this.toolbar);
			this.commandBarManager.CommandBars.Add(this.addressToolbar);
			this.Controls.Add(this.commandBarManager);

			// Loading inspector workspace
			splash.lblLoadingComponents.Text = "Loading... Inspector Workspace.";
			GBInspectorWorkspace inspector = new GBInspectorWorkspace();
			this.StartEvent += inspector.StartEventDelegate;
			this.CancelEvent += inspector.CancelEventDelegate;
		
			inspector.GBExit += new EventHandler(this.mnuExit_Click);
			inspector.DisableAddressBar += new DisableAddressBarEventHandler(inspector_DisableAddressBar);
			inspector.EnableAddressBar += new EnableAddressBarEventHandler(inspector_EnableAddressBar);
			inspector.UpdateAddressEvent += new UpdateAddressEventHandler(inspector_UpdateAddressEvent);
			inspector.StartProgressBarEvent += new StartProgressBarEventHandler(inspector_StartProgressBarEvent);
			inspector.StopProgressBarEvent += new StopProgressBarEventHandler(inspector_StopProgressBarEvent);
			inspector.ChangeStatusBarPanelEvent += new ChangeStatusBarEventHandler(inspector_ChangeStatusBarPanelEvent);
			inspector.LoadPluginMenusEvent += new LoadPluginMenusEventHandler(inspector_LoadPluginMenusEvent);
			inspector.ApplyToolbarSettingsEvent += new ApplyToolbarSettingsEventHandler(inspector_ApplyToolbarSettingsEvent);
			inspector.ApplyMenuSettingsEvent += new ApplyMenuSettingsEventHandler(inspector_ApplyMenuSettingsEvent);
			inspector.ApplyMenuRootSettingsEvent += new ApplyMenuRootSettingsEventHandler(inspector_ApplyMenuRootSettingsEvent);
			
			DocumentManager.Document inspectorDoc;			
			inspectorDoc = new DocumentManager.Document(inspector,"Main Workspace");				
			dmPanels.AddDocument(inspectorDoc);		
			
			if ( loadScriptingApp.Length > 0 )
			{
				inspector.LoadScriptingDataDesigner(loadScriptingApp);
			}

			// set focused doc
			dmPanels.FocusedDocument = inspectorDoc;
			this.ResumeLayout(true);

			splash.Hide();
			splash.Close();
		}

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
		#region Address Bar
		/// <summary>
		/// Disables the address bar.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void inspector_DisableAddressBar(object sender, EventArgs e)
		{
			this.addressToolbar.Items[0].IsEnabled = false;
			this.addressToolbar.Items[1].IsEnabled = false;
			this.addressToolbar.Items[2].IsEnabled = false;
		}

		/// <summary>
		/// Enables the address bar.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void inspector_EnableAddressBar(object sender, EventArgs e)
		{
			this.addressToolbar.Items[0].IsEnabled = true;
			this.addressToolbar.Items[1].IsEnabled = true;
			this.addressToolbar.Items[2].IsEnabled = true;

			//this.addressToolbar.Enabled = true;
		}
		private void cmbAddressUrl_GotFocus(object sender, EventArgs e)
		{
			if ( cmbAddressUrl.Text.ToLower() == "enter url" )
			{
				cmbAddressUrl.Text = string.Empty;
			}
		}
		private void btnGo_Click(object sender, System.EventArgs e)
		{
			// apply only if enabled
			if ( this.cmbAddressUrl.Enabled )
			{
				// Set focus
				this.cmbAddressUrl.Focus();

				// Validate that selected item is not null
				string url = string.Empty;
				if ( cmbAddressUrl.SelectedItem == null )
				{
					url = cmbAddressUrl.Text;
				} 
				else 
				{
					url = cmbAddressUrl.SelectedItem.ToString();
				}

				// ir no url set, focus and exit 
				if ( url.Length == 0 )
				{
					this.cmbAddressUrl.Focus();
					return;
				}	

				#region Apply more integrated checks
				if ( UrlSchemeValidation(url) )
				{
					if ( url.IndexOf("://") > -1 )
					{
						Uri getScheme = new Uri(url);
						cmbAddressUrl.Text = url;

						// check scheme
						if (( getScheme.Scheme != "http" ) && (getScheme.Scheme != "https"))
						{
							MessageBox.Show("Protocol not supported, try with http or https instead.",AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
							return;
						}
					} 
					else 
					{
						if ( (!url.ToLower().StartsWith("http://")) || (!url.ToLower().StartsWith("https://")) )
						{
							// http as default
							cmbAddressUrl.Text = "http://" + url;
						} 
						else 
						{
							cmbAddressUrl.Text = url;
						}
					}

					// Add item to combo list
					if ( cmbAddressUrl.Items.IndexOf(cmbAddressUrl.Text) == -1 )
					{
						this.cmbAddressUrl.Items.Add(cmbAddressUrl.Text);
					}

					RequestGetEventArgs eventArgs = new RequestGetEventArgs();
					eventArgs.Url = this.cmbAddressUrl.Text;
					eventArgs.InspectorRequestAction = InspectorAction.UserGet;
					this.StartEvent(this, eventArgs);
				}
			}
			#endregion
		}

		/// <summary>
		/// Validates that is a valid url with HTTP scheme.
		/// </summary>
		/// <param name="url"> The url to validate.</param>
		/// <returns> Returns true if valid, else false.</returns>
		private bool UrlSchemeValidation(string url)
		{
			bool urlValidated = true;	

			try
			{
				Uri testUrl;
				if ( url.ToLower().StartsWith("http") )
				{
					testUrl = new Uri(cmbAddressUrl.Text);
				} 
				else 
				{
					testUrl = new Uri("http://" + cmbAddressUrl.Text);
				}
			}
			catch
			{				
				urlValidated = false;
				MessageBox.Show("Invalid url.", AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			return urlValidated;
		}
		private void btnStop_Click(object sender, System.EventArgs e)
		{
			this.CancelEvent(this, null);
		}

		private void cmbAddressUrl_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ( e.KeyChar==(char)13 )
			{
				btnGo_Click(this,null);
				e.Handled=true;
			}		
		}

		private void cmbAddressUrl_SelectionChangeCommitted(object sender, System.EventArgs e)
		{
			btnGo_Click(this,null);
		}

		private void inspector_UpdateAddressEvent(object sender, RequestGetEventArgs e)
		{
			this.cmbAddressUrl.Text = e.Url;
		}
		#endregion
		#region On Panel Focus Changed
		private void dmPanels_FocusedDocumentChanged(object sender, System.EventArgs e)
		{
			IWorkspacePlugin workspacePlugin = (IWorkspacePlugin)dmPanels.FocusedDocument.Control;

			if ( currentWorkspacePlugin != workspacePlugin )
			{
				// hide previous selected workspace
				if ( currentWorkspacePlugin != null )
					currentWorkspacePlugin.HideWorkspaceMenuToolbarItems();

				// display focused workspace and set as current workspace plugin
				workspacePlugin.ShowWorkspaceMenuToolbarItems();
				currentWorkspacePlugin = workspacePlugin;
			}
		}
		#endregion
	}
}
