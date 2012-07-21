using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using System.IO;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Configuration.XmlTypeSerializer;
using Ecyware.GreenBlue.Controls;
using Ecyware.GreenBlue.Protocols.Http.Scripting;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Engine.HtmlCommand;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Controls.DesignerPageProvider;

namespace Ecyware.GreenBlue.Controls.Scripting
{
	/// <summary>
	/// Contains the session designer.
	/// </summary>
	public class ScriptingDataDesigner : Ecyware.GreenBlue.Controls.BasePluginForm
	{		
		int objectCount = 0;
		Cursor tempCursor;

		HttpProperties _defaultHttpProperties;
		string currentScriptingApplicationName = string.Empty;
		private ScriptingApplicationArgs _scriptingArgumentsDef = null;
		private BaseScriptingDataPage[] _loadedPages;
		int sessionRequestItemIndex = -1;			
		private ScriptingApplication _scriptingData = new ScriptingApplication();
		private BaseScriptingDataPage _designerUserControl = null;

		private WebRequestPage requestPage = new WebRequestPage();
		private ScriptingMainPage mainPage = new ScriptingMainPage();

		//public event ApplyMenuSettingsEventHandler ApplyMenuSettingsEvent;
		//public event ApplyToolbarSettingsEventHandler ApplyToolbarSettingsEvent;		

		private System.Windows.Forms.TreeView tvSessionTree;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel pnUserControl;
		private System.Windows.Forms.ImageList icons;
		private System.Windows.Forms.ContextMenu mnuSessionRequest;
		private System.Windows.Forms.MenuItem mnuRemoveSessionRequest;
		private System.Windows.Forms.MenuItem mnuSaveSessionScriptingData;
		private System.Windows.Forms.SaveFileDialog dlgSaveFile;
		private System.Windows.Forms.ImageList pageIcons;
		private System.Windows.Forms.MenuItem mnuTestSessionRequest;
		private System.Windows.Forms.OpenFileDialog dlgOpenFile;
		private System.Windows.Forms.MenuItem mnuCopy;
		private System.Windows.Forms.MenuItem mnuInsertNew;
		private System.Windows.Forms.MenuItem mnuCopyTop;
		private System.Windows.Forms.MenuItem mnuCopyBottom;
		private System.Windows.Forms.MenuItem mnuScriptingArgs;
		private System.Windows.Forms.MenuItem mnuInsertTop;
		private System.Windows.Forms.MenuItem mnuInsertBottom;
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Creates a new Scripting Designer.
		/// </summary>
		public ScriptingDataDesigner()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			objectCount++;

			// Add MainPage UserControl			
			mainPage.Dock = DockStyle.Fill;
			mainPage.Visible = false;
			pnUserControl.Controls.Add(mainPage);
			pageIcons.Images.Add(mainPage.Icon);

			// Add Request page
			requestPage.Dock = DockStyle.Fill;
			requestPage.Visible = false;
			pnUserControl.Controls.Add(requestPage);
			pageIcons.Images.Add(requestPage.Icon);
			
			// Load custom pages.
			ScriptingPageManager pageManager = new ScriptingPageManager();
			BaseScriptingDataPage[] controls = pageManager.LoadDesignerPages("ScriptingDesigner");
			ArrayList loadedPages = new ArrayList();
			foreach ( BaseScriptingDataPage page in controls )
			{
				page.Dock = DockStyle.Fill;
				page.Visible = false;
				pnUserControl.Controls.Add(page);
				loadedPages.Add(page);

				if ( page.Icon == null )
				{
					throw new ArgumentNullException("Icon","The Icon parameter in BaseScriptingDataPage is null.");
				} 
				else 
				{
					pageIcons.Images.Add(page.Icon);
				}
			}

			_loadedPages = (BaseScriptingDataPage[])loadedPages.ToArray(typeof(BaseScriptingDataPage));
			_defaultHttpProperties = (HttpProperties)ConfigManager.Read("greenBlue/httpClient", true);
		}


		/// <summary>
		/// Creates a new Scripting Designer.
		/// </summary>
		/// <param name="fileName"> The file name to open.</param>
		public ScriptingDataDesigner(string fileName) : this()
		{
			currentScriptingApplicationName = fileName;
			_scriptingData = new ScriptingApplication();
			LoadFile(fileName);
		}


		/// <summary>
		/// Loads a scripting file.
		/// </summary>
		/// <param name="fileName"> The file name to load.</param>
		public void LoadFile(string fileName)
		{
			string contentType = AppLocation.GetMIMEType(fileName);
			
			// xml file
			if ( contentType.IndexOf("xml") > -1 )
			{
				_scriptingData.Load(fileName);
			} 
			else
			{
				ScriptingApplicationPackage package 
					= new ScriptingApplicationPackage(fileName);
				_scriptingData = package.ScriptingApplication;
				_scriptingArgumentsDef = package.ScriptingApplicationArguments;
			}
		}


		/// <summary>
		/// Opens a Web Session.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void OpenFile(object sender,EventArgs e)
		{
			dlgOpenFile.CheckFileExists = true;
			dlgOpenFile.InitialDirectory = Application.UserAppDataPath;
			dlgOpenFile.RestoreDirectory = true;
			dlgOpenFile.Filter = "GreenBlue Scripting Application (*.gbscr)|*.gbscr|GreenBlue Scripting Application XML (*.xml)|*.xml";
			dlgOpenFile.Title = "Open Scripting Application";

			if ( dlgOpenFile.ShowDialog() == DialogResult.OK )
			{
				Application.DoEvents();
				tempCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;

				try
				{
					LoadFile(dlgOpenFile.FileName);
					this.DisplayTreeView();
				}
				catch ( Exception ex )
				{
					MessageBox.Show(ex.ToString());
					ExceptionHandler.RegisterException(ex);
					MessageBox.Show("Error while opening the scripting application.", AppLocation.ApplicationName, MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
			}

			Cursor.Current = tempCursor;
		}


		/// <summary>
		/// Loads an exising Session type.
		/// </summary>
		/// <param name="session"> Loads a session type in the ScriptingDataDesigner.</param>
		public void LoadSession(Session session)
		{
			ScriptingApplication sd = new ScriptingApplication();

			foreach (SessionRequest req in session.SessionRequests )
			{				
				if ( req.RequestType == HttpRequestType.GET )
				{					
					GetWebRequest getRequest = new GetWebRequest((GetSessionRequest)req);
					sd.AddWebRequest(getRequest);
				}
				else if ( req.RequestType == HttpRequestType.POST )
				{
					PostWebRequest postRequest = new PostWebRequest((PostSessionRequest)req);
					sd.AddWebRequest(postRequest);
				}
			}

			_scriptingData = sd;
		}


		/// <summary>
		/// Displays the tree and nodes for the scripting data.
		/// </summary>
		public void DisplayTreeView()
		{			
			// create parent node
			TreeNode parentNode = new TreeNode();
			parentNode.Text = "Scripting Application Designer";
			parentNode.ImageIndex = 0;
			parentNode.SelectedImageIndex = 0;
			parentNode.Tag = typeof(ScriptingMainPage);			

			for (int i=0;i<_scriptingData.WebRequests.Length;i++)
			{
				WebRequest request = _scriptingData.WebRequests[i];
				TreeNode node = new TreeNode();

				node.Text = request.RequestType.ToString() + " " + request.Url.ToString();
				node.Tag = typeof(WebRequestPage);
				node.ImageIndex = 1;
				node.SelectedImageIndex = 1;
				parentNode.Nodes.Add(node);

				int j = 2;
				foreach ( BaseScriptingDataPage page in _loadedPages )
				{
					if ( page.Caption == string.Empty )
					{
						throw new ArgumentNullException("Caption", "The Caption in BaseScriptingDataPage is empty.");
					}

					// Form
					TreeNode pageNode = new TreeNode(page.Caption);
					pageNode.Tag = page.GetType();
					node.Nodes.Add(pageNode);
					
					pageNode.ImageIndex = j;
					pageNode.SelectedImageIndex = j;
					j++;
				}

			}

			parentNode.Expand();
			this.tvSessionTree.Nodes.Add(parentNode);
		}
		#region Properties

		/// <summary>
		/// Gets or sets the selected designer user control.
		/// </summary>
		private BaseScriptingDataPage SelectedDesignerControl
		{
			get
			{
				return this._designerUserControl;
			}
			set
			{
				_designerUserControl = value;
			}
		}

		#endregion		
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

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ScriptingDataDesigner));
			this.tvSessionTree = new System.Windows.Forms.TreeView();
			this.mnuSessionRequest = new System.Windows.Forms.ContextMenu();
			this.mnuTestSessionRequest = new System.Windows.Forms.MenuItem();
			this.mnuCopy = new System.Windows.Forms.MenuItem();
			this.mnuCopyTop = new System.Windows.Forms.MenuItem();
			this.mnuCopyBottom = new System.Windows.Forms.MenuItem();
			this.mnuInsertNew = new System.Windows.Forms.MenuItem();
			this.mnuInsertTop = new System.Windows.Forms.MenuItem();
			this.mnuInsertBottom = new System.Windows.Forms.MenuItem();
			this.mnuRemoveSessionRequest = new System.Windows.Forms.MenuItem();
			this.mnuSaveSessionScriptingData = new System.Windows.Forms.MenuItem();
			this.mnuScriptingArgs = new System.Windows.Forms.MenuItem();
			this.pageIcons = new System.Windows.Forms.ImageList(this.components);
			this.icons = new System.Windows.Forms.ImageList(this.components);
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.pnUserControl = new System.Windows.Forms.Panel();
			this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
			this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// tvSessionTree
			// 
			this.tvSessionTree.ContextMenu = this.mnuSessionRequest;
			this.tvSessionTree.Dock = System.Windows.Forms.DockStyle.Left;
			this.tvSessionTree.HideSelection = false;
			this.tvSessionTree.ImageList = this.pageIcons;
			this.tvSessionTree.Location = new System.Drawing.Point(0, 0);
			this.tvSessionTree.Name = "tvSessionTree";
			this.tvSessionTree.Size = new System.Drawing.Size(234, 354);
			this.tvSessionTree.TabIndex = 0;
			this.tvSessionTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSessionTree_AfterSelect);
			this.tvSessionTree.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvSessionTree_BeforeSelect);
			// 
			// mnuSessionRequest
			// 
			this.mnuSessionRequest.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							  this.mnuTestSessionRequest,
																							  this.mnuCopy,
																							  this.mnuInsertNew,
																							  this.mnuRemoveSessionRequest,
																							  this.mnuSaveSessionScriptingData,
																							  this.mnuScriptingArgs});
			this.mnuSessionRequest.Popup += new System.EventHandler(this.mnuSessionRequest_Popup);
			// 
			// mnuTestSessionRequest
			// 
			this.mnuTestSessionRequest.Index = 0;
			this.mnuTestSessionRequest.Text = "&Test Scripting Application";
			this.mnuTestSessionRequest.Visible = false;
			this.mnuTestSessionRequest.Click += new System.EventHandler(this.mnuTestSessionRequest_Click);
			// 
			// mnuCopy
			// 
			this.mnuCopy.Index = 1;
			this.mnuCopy.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnuCopyTop,
																					this.mnuCopyBottom});
			this.mnuCopy.Text = "&Copy Request";
			this.mnuCopy.Visible = false;
			this.mnuCopy.Click += new System.EventHandler(this.mnuCopy_Click);
			// 
			// mnuCopyTop
			// 
			this.mnuCopyTop.Index = 0;
			this.mnuCopyTop.Text = "&Top";
			this.mnuCopyTop.Click += new System.EventHandler(this.mnuCopyAfter_Click);
			// 
			// mnuCopyBottom
			// 
			this.mnuCopyBottom.Index = 1;
			this.mnuCopyBottom.Text = "&Bottom";
			this.mnuCopyBottom.Click += new System.EventHandler(this.mnuCopyBefore_Click);
			// 
			// mnuInsertNew
			// 
			this.mnuInsertNew.Index = 2;
			this.mnuInsertNew.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.mnuInsertTop,
																						 this.mnuInsertBottom});
			this.mnuInsertNew.Text = "Insert &Web Request";
			this.mnuInsertNew.Visible = false;
			// 
			// mnuInsertTop
			// 
			this.mnuInsertTop.Index = 0;
			this.mnuInsertTop.Text = "&Top";
			this.mnuInsertTop.Click += new System.EventHandler(this.mnuInsertAfter_Click);
			// 
			// mnuInsertBottom
			// 
			this.mnuInsertBottom.Index = 1;
			this.mnuInsertBottom.Text = "&Bottom";
			this.mnuInsertBottom.Click += new System.EventHandler(this.mnuInsertBefore_Click);
			// 
			// mnuRemoveSessionRequest
			// 
			this.mnuRemoveSessionRequest.Index = 3;
			this.mnuRemoveSessionRequest.Text = "&Remove";
			this.mnuRemoveSessionRequest.Visible = false;
			this.mnuRemoveSessionRequest.Click += new System.EventHandler(this.mnuRemoveSessionRequest_Click);
			// 
			// mnuSaveSessionScriptingData
			// 
			this.mnuSaveSessionScriptingData.Index = 4;
			this.mnuSaveSessionScriptingData.Text = "Save Scripting Application...";
			this.mnuSaveSessionScriptingData.Visible = false;
			this.mnuSaveSessionScriptingData.Click += new System.EventHandler(this.mnuSaveSessionScriptingData_Click);
			// 
			// mnuScriptingArgs
			// 
			this.mnuScriptingArgs.Index = 5;
			this.mnuScriptingArgs.Text = "Scripting Arguments Designer...";
			this.mnuScriptingArgs.Visible = false;
			this.mnuScriptingArgs.Click += new System.EventHandler(this.mnuScriptingArgs_Click);
			// 
			// pageIcons
			// 
			this.pageIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.pageIcons.ImageSize = new System.Drawing.Size(24, 24);
			this.pageIcons.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// icons
			// 
			this.icons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.icons.ImageSize = new System.Drawing.Size(16, 16);
			this.icons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("icons.ImageStream")));
			this.icons.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// splitter1
			// 
			this.splitter1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.splitter1.Location = new System.Drawing.Point(234, 0);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 354);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// pnUserControl
			// 
			this.pnUserControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnUserControl.Location = new System.Drawing.Point(237, 0);
			this.pnUserControl.Name = "pnUserControl";
			this.pnUserControl.Size = new System.Drawing.Size(399, 354);
			this.pnUserControl.TabIndex = 2;
			// 
			// ScriptingDataDesigner
			// 
			this.Controls.Add(this.pnUserControl);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.tvSessionTree);
			this.IsUnique = true;
			this.Name = "ScriptingDataDesigner";
			this.Size = new System.Drawing.Size(636, 354);
			this.Load += new System.EventHandler(this.SessionDesigner_Load);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Closes the form.
		/// </summary>
		public override void Close()
		{
			objectCount--;
		}

		private void SessionDesigner_Load(object sender, System.EventArgs e)
		{
			//UpdateRunTestMenu(true);
			//UpdateSaveWebSessionMenu(true);
		}


		#region ContextMenu Methods
		/// <summary>
		/// Removes a session request from the session.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The event arguments.</param>
		private void mnuTestSessionRequest_Click(object sender, System.EventArgs e)
		{
			if ( tvSessionTree.Nodes[0].Nodes.Count > 0 )
			{	
				int index = tvSessionTree.Nodes[0].Nodes.IndexOf(tvSessionTree.SelectedNode);				
				TestRequestDialog testRequestDialog = new TestRequestDialog();
				testRequestDialog.Show();
				testRequestDialog.TestRequestUntilIndex(_scriptingData, index);				
			}
		}

		/// <summary>
		/// Copies a new web request node.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The event arguments.</param>
		private void mnuCopy_Click(object sender, System.EventArgs e)
		{

		}

		/// <summary>
		/// Removes a session request from the session.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The event arguments.</param>
		private void mnuRemoveSessionRequest_Click(object sender, System.EventArgs e)
		{
			//string testName = tvSessionTree.SelectedNode.Text;

			if ( tvSessionTree.Nodes[0].Nodes.Count > 1 )
			{			
				if ( MessageBox.Show("Are you sure you want to remove the selected session request?",AppLocation.ApplicationName, MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes )
				{	
					int index = tvSessionTree.Nodes[0].Nodes.IndexOf(tvSessionTree.SelectedNode);

					try
					{
						//_scriptingData.RemoveWebRequest(index);

						// remove from tree, safe session and test session
						tvSessionTree.Nodes[0].Nodes.Remove(tvSessionTree.SelectedNode);
						if ( tvSessionTree.SelectedNode.PrevNode != null && tvSessionTree.SelectedNode.Nodes.Count == 5 )
						{							
							int i = tvSessionTree.SelectedNode.PrevNode.Index;
							tvSessionTree.SelectedNode = tvSessionTree.Nodes[0].Nodes[i];
							_scriptingData.RemoveWebRequest(index);
						} 
						else 
						{							
							tvSessionTree.SelectedNode = tvSessionTree.Nodes[0].Nodes[0];
							_scriptingData.RemoveWebRequest(index);
						}
						
//						BaseScriptingDataPage page = GetPageControl(typeof(ScriptingMainPage));			
//						ScriptingMainPage currentPage = (ScriptingMainPage)page;
//						currentPage.ShowScriptingDataXml(_scriptingData);
					}
					catch (Exception ex)
					{
						ExceptionHandler.RegisterException(ex);
						MessageBox.Show(ex.Message,AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			} 
			else 
			{
				MessageBox.Show("You must have at least one web request.",AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		#endregion		

		/// <summary>
		/// Shows the current control.
		/// </summary>
		/// <param name="control"> The current selected control type.</param>
		private void ShowControl(BaseScriptingDataPage control)
		{
			control.Show();

			for (int i=0;i<pnUserControl.Controls.Count;i++ )
			{
				if ( !(pnUserControl.Controls[i].GetType() == control.GetType() ) )
				{
					pnUserControl.Controls[i].Hide();
				}
			}
		}

		/// <summary>
		/// Gets the page control.
		/// </summary>
		/// <param name="pageType"> The type of the page.</param>
		/// <returns> A BaseScriptingDataPage type.</returns>
		private BaseScriptingDataPage GetPageControl(Type pageType)
		{

			BaseScriptingDataPage page = null;

			for (int i=0;i<pnUserControl.Controls.Count;i++ )
			{
				if ( pnUserControl.Controls[i].GetType() ==  pageType )
				{
					// Update Page Changes.
					page = (BaseScriptingDataPage)pnUserControl.Controls[i];
					break;
				}
			}

			return page;
		}


		/// <summary>
		/// Gets the current web request.
		/// </summary>
		/// <param name="index"> The web request index.</param>
		/// <returns> A WebRequest type.</returns>
		private WebRequest GetCurrentWebRequest(int index)
		{
			if ( _scriptingData.WebRequests.Length > index )
			{
				return _scriptingData.WebRequests[index];
			} 
			else 
			{
				return null;
			}
		}
		#region Tree Node Select Events
		/// <summary>
		/// Displays the selected designer editor.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The event arguments.</param>
		private void tvSessionTree_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			Type pageType = (Type)e.Node.Tag;	
			BaseScriptingDataPage page = GetPageControl(pageType);			

			this.SuspendLayout();

			if ( pageType == typeof(ScriptingMainPage) )
			{
				ScriptingMainPage currentPage = (ScriptingMainPage)page;
				currentPage.ShowScriptingDataXml(_scriptingData);				
			}
			else if ( pageType == typeof(WebRequestPage) )
			{			
				WebRequest request = GetCurrentWebRequest(e.Node.Index);	
			
				if ( request != null )
				{
					page.LoadRequest(e.Node.Index, this._scriptingData, request);

					// Update Node Index
					sessionRequestItemIndex = e.Node.Index;
					this.mnuRemoveSessionRequest.Visible = true;
				}
			}
			else
			{
				WebRequest request = GetCurrentWebRequest(e.Node.Parent.Index);

				if ( request != null )
				{
					page.LoadRequest(e.Node.Parent.Index, this._scriptingData, request);

					// Update Node Index
					sessionRequestItemIndex = e.Node.Parent.Index;
					this.mnuRemoveSessionRequest.Visible = false;
				}
			}

			SelectedDesignerControl = page;

			// Show Control
			ShowControl(page);

			this.ResumeLayout(false);
		}		

		/// <summary>
		/// Saves the changes in the page.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The event arguments.</param>
		private void tvSessionTree_BeforeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			if ( tvSessionTree.SelectedNode != null )
			{
				Type pageType = (Type)tvSessionTree.SelectedNode.Tag;	
				BaseScriptingDataPage page = GetPageControl(pageType);

				if ( pageType == typeof(ScriptingMainPage) )
				{
					// Save scripting application
					ScriptingMainPage control = (ScriptingMainPage)page;
					ScriptingApplication newApplication = control.LoadScriptingApplication();

					if ( newApplication != null )
					{
						_scriptingData = newApplication;
					}
				} 
				else if ( pageType == typeof(WebRequestPage) )
				{				
					// Update current changes
					_scriptingData.UpdateWebRequest(tvSessionTree.SelectedNode.Index, page.WebRequest);
				}
				else 
				{
					// Update current changes
					_scriptingData.UpdateWebRequest(tvSessionTree.SelectedNode.Parent.Index, page.WebRequest);
				}
			}
		}

		#endregion
		#region Menus
//		/// <summary>
//		/// Toggles the RunTest menu.
//		/// </summary>
//		/// <param name="enabled"> Enables or disables the save web session menu.</param>
//		public void UpdateRunTestMenu(bool enabled)
//		{
//			// new Arguments
//			ApplyMenuSettingsEventArgs newArgs = new ApplyMenuSettingsEventArgs();
//	
//			// identify the shortcut
//			newArgs.RootShortcut=Shortcut.CtrlShiftF;		
//
//			// get menu item by index
//			Ecyware.GreenBlue.Controls.MenuItem runMenu = this.PluginMenus.GetByIndex(2);
//
//			runMenu.Enabled = enabled;
//			newArgs.MenuItems.Add(runMenu.Name,runMenu);
//
//			// update menu
//			this.ApplyMenuSettingsEvent(this,newArgs);
//		}
		#endregion
		/// <summary>
		/// Handles the popup event of the context menu.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The event arguments.</param>
		private void mnuSessionRequest_Popup(object sender, System.EventArgs e)
		{
			if ( this.tvSessionTree.SelectedNode.Parent == null )
			{
				this.mnuTestSessionRequest.Visible = false;
				this.mnuRemoveSessionRequest.Visible = false;
				this.mnuCopy.Visible = false;
				this.mnuSaveSessionScriptingData.Visible = true;
				this.mnuScriptingArgs.Visible = true;
				this.mnuInsertNew.Visible = false;				
			} 
			else if ( ( tvSessionTree.SelectedNode.Parent != null ) && ( tvSessionTree.SelectedNode.Nodes.Count > 0 ) ) 
			{
				this.mnuScriptingArgs.Visible = false;
				this.mnuSaveSessionScriptingData.Visible = false;
				this.mnuRemoveSessionRequest.Visible = true;
				this.mnuCopy.Visible = true;
				this.mnuTestSessionRequest.Visible = true;
				this.mnuInsertNew.Visible = true;				
			} else {
				this.mnuSaveSessionScriptingData.Visible = false;
				this.mnuRemoveSessionRequest.Visible = false;
				this.mnuCopy.Visible = false;
				this.mnuTestSessionRequest.Visible = false;
				this.mnuInsertNew.Visible = false;
				this.mnuScriptingArgs.Visible = false;
			}
		}

		#region Save Scripting Data


		/// <summary>
		/// Save without results.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuSaveSessionScriptingData_Click(object sender, System.EventArgs e)
		{
			SaveApplication();
		}

		internal void ExportApplicationToXml()
		{
			// send this to disk
			System.IO.Stream stream = null;

			dlgSaveFile.InitialDirectory = Application.UserAppDataPath;
			dlgSaveFile.RestoreDirectory = true;
			dlgSaveFile.Filter = "XML (*.xml)|*.xml";
			dlgSaveFile.Title = "Export Scripting Application to XML";

			if ( dlgSaveFile.ShowDialog() == DialogResult.OK )
			{
				Application.DoEvents();
				tempCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;

				// file
				stream = dlgSaveFile.OpenFile();
				if ( stream != null )
				{
					try
					{
						_scriptingData.ToXmlDocument().Save(stream);

						if ( _scriptingArgumentsDef == null )
						{
							_scriptingArgumentsDef = _scriptingData.CreateArgumentDefinition();
						} 
						else 
						{
							_scriptingArgumentsDef = _scriptingData.UpdateArgumentDefinition(_scriptingArgumentsDef);
						}

						if ( _scriptingArgumentsDef.WebRequestArguments.Length > 0 )
						{
							string fileName = dlgSaveFile.FileName.Substring(0,dlgSaveFile.FileName.Length-4) + "_args.xml";
							_scriptingArgumentsDef.ToXmlDocument().Save(fileName);
						}
					}
					catch ( Exception ex )
					{
						ExceptionHandler.RegisterException(ex);
						MessageBox.Show("Error while saving the web unit test template file.", AppLocation.ApplicationName, MessageBoxButtons.OK,MessageBoxIcon.Error);
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
		/// Runs the application
		/// </summary>
		internal void RunApplication()
		{
//			if ( _scriptingArgumentsDef == null )
//			{
//				_scriptingArgumentsDef = _scriptingData.CreateArgumentDefinition();
//			} 
//			else 
//			{
//				_scriptingArgumentsDef = _scriptingData.UpdateArgumentDefinition(_scriptingArgumentsDef);
//			}
			ScriptingApplicationArgumentForm inputForm = new ScriptingApplicationArgumentForm(_scriptingData,null);

			if ( inputForm.ShowDialog() == DialogResult.OK )
			{
				TestRequestDialog testRequestDialog = new TestRequestDialog();
				testRequestDialog.Show();
				testRequestDialog.TestRequestUntilIndex(inputForm.ScriptingApplication, inputForm.ScriptingApplication.WebRequests.Length-1);
			}

//			if ( _scriptingArgumentsDef.WebRequestArguments.Length > 0 )
//			{
//
//			}
//			else 
//			{
//				// Display message
//				MessageBox.Show("There are no arguments assigned for this scripting application.", AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
//			}
		}
		/// <summary>
		/// Saves the application.
		/// </summary>
		internal void SaveApplication()
		{
			foreach ( WebRequest request in _scriptingData.WebRequests )
			{
				request.WebResponse = null;
			}

			bool isNew = false;
			bool doEncrypt = false;

			SaveApplicationDialog dialog = new SaveApplicationDialog(currentScriptingApplicationName);

			if ( dialog.ShowDialog() == DialogResult.OK )
			{
				isNew = dialog.IsNew;
				doEncrypt = dialog.DoEncrypt;
				currentScriptingApplicationName = dialog.ScriptingApplicationFilePath;
			}

			if ( currentScriptingApplicationName != string.Empty )
			{
				SaveSessionAsScriptingData(currentScriptingApplicationName, isNew, doEncrypt);
			} 
			else 
			{
				MessageBox.Show("No file name was set for the scripting application.", AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

//		private string CreateScriptingDataFile()
//		{
//			dlgSaveFile.InitialDirectory = Application.UserAppDataPath;
//			dlgSaveFile.RestoreDirectory = true;
//			dlgSaveFile.Filter = "GreenBlue Scripting Application (*.gbscr)|*.gbscr|GreenBlue Scripting Application XML (*.xml)|*.xml";
//			dlgSaveFile.Title = "Save Scripting Application";
//
//			string fileName = string.Empty;
//
//			if ( dlgSaveFile.ShowDialog() == DialogResult.OK )
//			{
//				Application.DoEvents();
//				tempCursor = Cursor.Current;
//				Cursor.Current = Cursors.WaitCursor;
//				// file
//				fileName = dlgSaveFile.FileName;
//			}
//
//			return fileName;
//		}

		/// <summary>
		/// Saves the session as scripting data.
		/// </summary>
		/// <param name="fileName"> The file name.</param>
		/// <param name="isNew"> Sets if the scripting application is new or existing.</param>
		/// <param name="encrypt"> Encrypts a scripting application.</param>
		private void SaveSessionAsScriptingData(string fileName, bool isNew, bool encrypt)
		{
			try
			{	
				if ( isNew )
				{
					_scriptingData.Header.ApplicationID = Guid.NewGuid().ToString();
				}
				if ( AppLocation.GetMIMEType(fileName).ToLower(System.Globalization.CultureInfo.InvariantCulture).IndexOf("xml") > -1 )
				{
					_scriptingData.Save(fileName);
				}
				else
				{
					ScriptingApplicationPackage.CreatePackage(_scriptingData, _scriptingArgumentsDef, fileName, encrypt);
				}

				Cursor.Current = tempCursor;
			}
			catch ( Exception ex )
			{
				MessageBox.Show(ex.ToString());
			}
		}
		#endregion

		#region Drag and Drop for Web Request Pages
		private void tvSessionTree_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
		{
			TreeView tree = (TreeView)sender;

			e.Effect = DragDropEffects.None;
			
			if (  e.Data.GetData(typeof(TreeNode)) != null )
			{
				Point pt = new Point(e.X, e.Y);
				pt = tree.PointToClient(pt);

				TreeNode node = tree.GetNodeAt(pt);

				if ( node != null )
				{
					e.Effect = DragDropEffects.Move;
					tree.SelectedNode = node;
				}
			}

		}

		private void tvSessionTree_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			TreeView tree = (TreeView)sender;


			Point pt = new Point(e.X, e.Y);
			pt = tree.PointToClient(pt);

			TreeNode node = tree.GetNodeAt(pt);

			int index = node.Index + 1;

			TreeNode nodeDragged = (TreeNode)e.Data.GetData(typeof(TreeNode));

			// Get web request.
			Type pageType = (Type)nodeDragged.Tag;			

			try
			{
				if ( pageType == typeof(WebRequestPage) )
				{				
					// Get WebRequest
					//WebRequest request = GetCurrentWebRequest(nodeDragged.Index);
			
					// Insert Tree clone
					tree.Nodes[0].Nodes.Insert(index,(TreeNode)nodeDragged.Clone());
		
					ScriptingApplication cloned = _scriptingData.Clone();

					if ( nodeDragged.Index > index )
					{
						WebRequest clonedRequest = cloned.WebRequests[nodeDragged.Index-1];
						// Insert clone to scripting application
						_scriptingData.InsertWebRequest(index, clonedRequest);
					} 
					else 
					{
						WebRequest clonedRequest = cloned.WebRequests[nodeDragged.Index];
						// Insert clone to scripting application
						_scriptingData.InsertWebRequest(index, clonedRequest);
					}

					if ( nodeDragged.Index > index )
					{
						// Remove from WebRequests.
						_scriptingData.RemoveWebRequest(nodeDragged.Index - 1);
					} 
					else 
					{
						// Remove from WebRequests.
						_scriptingData.RemoveWebRequest(nodeDragged.Index);						
					}

					// Remove tree node
					nodeDragged.Remove();
				}
			}
			catch ( Exception ex )
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void tvSessionTree_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			TreeView tree = (TreeView)sender;
			TreeNode node = tree.GetNodeAt(e.X, e.Y);
			//tree.SelectedNode = node;

			if ( e.Button == MouseButtons.Left )
			{			
				if  ( node.Parent != null )
				{
					if ( ((Type)node.Tag) == typeof(WebRequestPage) )
					{
						if ( node != null )
						{
							tree.DoDragDrop(node, DragDropEffects.Move);
						}
					}
				}
			}		
		}
		#endregion

		/// <summary>
		/// Copies a request at the top.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The event arguments.</param>
		private void mnuCopyAfter_Click(object sender, System.EventArgs e)
		{
			// Copy Top
			int index = tvSessionTree.Nodes[0].Nodes.IndexOf(tvSessionTree.SelectedNode);
			TreeNode copy = (TreeNode)tvSessionTree.SelectedNode.Clone();

			ScriptingApplication cloned = _scriptingData.Clone();
			WebRequest clonedRequest = cloned.WebRequests[index];
			copy.Text = "Copy of " + copy.Text;

			if ( index == 0 )
			{
				index = 1;
				// Add new copy
				tvSessionTree.Nodes[0].Nodes.Insert(index - 1,copy);
				_scriptingData.InsertWebRequest(index - 1,clonedRequest);
			} 
			else 
			{
				// Add new copy
				tvSessionTree.Nodes[0].Nodes.Insert(index,copy);
				_scriptingData.InsertWebRequest(index,clonedRequest);
			}
		}

		/// <summary>
		/// Copies a request at the bottom.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The event arguments.</param>
		private void mnuCopyBefore_Click(object sender, System.EventArgs e)
		{
			// Copy bottom
			int index = tvSessionTree.Nodes[0].Nodes.IndexOf(tvSessionTree.SelectedNode);
			TreeNode copy = (TreeNode)tvSessionTree.SelectedNode.Clone();

			ScriptingApplication cloned = _scriptingData.Clone();
			WebRequest clonedRequest = cloned.WebRequests[index];				

			copy.Text = "Copy of " + copy.Text;

			if ( (index+1) > tvSessionTree.Nodes[0].Nodes.Count )
			{
				index = index - 1;
			}

			// Add new copy
			tvSessionTree.Nodes[0].Nodes.Insert(index+1,copy);
			_scriptingData.InsertWebRequest(index + 1,clonedRequest);
		}

		/// <summary>
		/// Inserts at the top.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The event arguments.</param>
		private void mnuInsertAfter_Click(object sender, System.EventArgs e)
		{
			SetRequestTypeDialog dialog = new SetRequestTypeDialog();

			if ( dialog.ShowDialog() == DialogResult.OK )
			{
				HttpRequestType requestType = dialog.SelectedHttpRequestType;
				string url = dialog.Url;
				string contentType = dialog.ContentType;

				WebRequest request = WebRequest.Create(requestType, url);
				request.RequestHttpSettings = _defaultHttpProperties;
				request.RequestHttpSettings.ContentType = contentType;

				// Insert new top
				int index = tvSessionTree.Nodes[0].Nodes.IndexOf(tvSessionTree.SelectedNode);
				TreeNode copy = (TreeNode)tvSessionTree.SelectedNode.Clone();	

				// Set node text
				copy.Text = requestType.ToString() + " " + url;

				if ( index == 0 )
				{
					index = 1;

					// Add new copy
					tvSessionTree.Nodes[0].Nodes.Insert(index - 1,copy);
					_scriptingData.InsertWebRequest(index - 1,request);
				} 
				else 
				{
					// Add new copy
					tvSessionTree.Nodes[0].Nodes.Insert(index,copy);
					_scriptingData.InsertWebRequest(index,request);
				}
			}
		}

		/// <summary>
		/// Inserts at the bottom.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The event arguments.</param>
		private void mnuInsertBefore_Click(object sender, System.EventArgs e)
		{			
			SetRequestTypeDialog dialog = new SetRequestTypeDialog();

			if ( dialog.ShowDialog() == DialogResult.OK )
			{
				HttpRequestType requestType = dialog.SelectedHttpRequestType;
				string url = dialog.Url;
				string contentType = dialog.ContentType;

				WebRequest request = WebRequest.Create(requestType, url);
				request.RequestHttpSettings = _defaultHttpProperties;
				request.RequestHttpSettings.ContentType = contentType;

				// Copy bottom
				int index = tvSessionTree.Nodes[0].Nodes.IndexOf(tvSessionTree.SelectedNode);
				TreeNode copy = (TreeNode)tvSessionTree.SelectedNode.Clone();

				// Set node text
				copy.Text = requestType.ToString() + " " + url;

				if ( (index+1) > tvSessionTree.Nodes[0].Nodes.Count )
				{
					index = index - 1;
				}

				// Add new copy
				tvSessionTree.Nodes[0].Nodes.Insert(index+1,copy);
				_scriptingData.InsertWebRequest(index + 1,request);
			}
		}


		/// <summary>
		/// Displays the scripting arguments designer.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The event arguments.</param>
		private void mnuScriptingArgs_Click(object sender, System.EventArgs e)
		{
			ShowScriptingArgumentsDesigner();
		}


		/// <summary>
		/// Shows the scripting arguments designer.
		/// </summary>
		internal void ShowScriptingArgumentsDesigner()
		{
			if ( _scriptingArgumentsDef == null )
			{
				_scriptingArgumentsDef = _scriptingData.CreateArgumentDefinition();
			} 
			else 
			{
				_scriptingArgumentsDef = _scriptingData.UpdateArgumentDefinition(_scriptingArgumentsDef);
			}

			if ( _scriptingArgumentsDef.WebRequestArguments.Length > 0 )
			{
				// Load designer
				ScriptingApplicationArgumentDesignerForm designer = new ScriptingApplicationArgumentDesignerForm(_scriptingArgumentsDef);

				if( designer.ShowDialog() == DialogResult.OK )
				{
					_scriptingArgumentsDef = designer.ScriptingApplicationArgs;
				}
			} 
			else 
			{
				// Display message
				MessageBox.Show("There are no arguments assigned for this scripting application.", AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

	}
}

