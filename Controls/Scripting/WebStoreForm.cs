using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.LicenseServices.Client;
using Ecyware.GreenBlue.Protocols.Http.Scripting;

namespace Ecyware.GreenBlue.Controls.Scripting
{
	/// <summary>
	/// Summary description for WebStoreForm.
	/// </summary>
	public class WebStoreForm : System.Windows.Forms.Form
	{
		IAsyncResult async = null;
		//bool _isConnected = false;
		string currentScriptingApplicationName = string.Empty;
		Cursor tempCursor;
		private string _selectedApplicationFilePath = string.Empty;
		private System.Windows.Forms.TabControl tabPages;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListView lvLocal;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.ContextMenu mnuLocalStore;
		private System.Windows.Forms.ContextMenu mnuPublicStore;
		private System.Windows.Forms.ImageList icons;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem mnuDelete;
		private System.Windows.Forms.MenuItem mnuPublish;
		private System.Windows.Forms.MenuItem mnuOpenFromLocalStore;
		private System.Windows.Forms.MenuItem mnuOpen;
		private System.Windows.Forms.MenuItem mnuSave;
		private System.Windows.Forms.SaveFileDialog dlgSaveFile;
		private System.Windows.Forms.OpenFileDialog dlgOpenFile;
		private System.Windows.Forms.ToolTip toolTip;
		private System.Windows.Forms.StatusBar panel;
		private System.Windows.Forms.StatusBarPanel panel1;
		private System.Windows.Forms.ProgressBar progress;
		private System.Windows.Forms.Timer progressBarTimer;
		private System.Windows.Forms.MenuItem mnuDownload;
		private System.Windows.Forms.MenuItem mnuMyApplications;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem mnuSearch;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem mnuRemoveScriptingApplication;
		private System.Windows.Forms.MenuItem mnuRun;
		private System.Windows.Forms.MenuItem mnuRunScript;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem mnuViewDetails;
		private System.Windows.Forms.MenuItem mnuViewIcons;
		private System.Windows.Forms.ColumnHeader fileName;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Creates a new WebStoreForm.
		/// </summary>
		public WebStoreForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();


			if ( !Directory.Exists(AppLocation.DocumentFolder) )
			{
				Directory.CreateDirectory(AppLocation.DocumentFolder);
			}
			
			LoadLocalStore();
			//LoadPublicStore();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WebStoreForm));
			this.tabPages = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lvLocal = new System.Windows.Forms.ListView();
			this.fileName = new System.Windows.Forms.ColumnHeader();
			this.mnuLocalStore = new System.Windows.Forms.ContextMenu();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.mnuViewDetails = new System.Windows.Forms.MenuItem();
			this.mnuViewIcons = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.mnuOpenFromLocalStore = new System.Windows.Forms.MenuItem();
			this.mnuRun = new System.Windows.Forms.MenuItem();
			this.mnuRunScript = new System.Windows.Forms.MenuItem();
			this.mnuPublish = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.mnuOpen = new System.Windows.Forms.MenuItem();
			this.mnuSave = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.mnuDelete = new System.Windows.Forms.MenuItem();
			this.icons = new System.Windows.Forms.ImageList(this.components);
			this.mnuPublicStore = new System.Windows.Forms.ContextMenu();
			this.mnuDownload = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.mnuMyApplications = new System.Windows.Forms.MenuItem();
			this.mnuSearch = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.mnuRemoveScriptingApplication = new System.Windows.Forms.MenuItem();
			this.btnClose = new System.Windows.Forms.Button();
			this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
			this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.panel = new System.Windows.Forms.StatusBar();
			this.panel1 = new System.Windows.Forms.StatusBarPanel();
			this.progress = new System.Windows.Forms.ProgressBar();
			this.progressBarTimer = new System.Windows.Forms.Timer(this.components);
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.tabPages.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
			this.SuspendLayout();
			// 
			// tabPages
			// 
			this.tabPages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tabPages.Controls.Add(this.tabPage1);
			this.tabPages.ItemSize = new System.Drawing.Size(96, 18);
			this.tabPages.Location = new System.Drawing.Point(1, 0);
			this.tabPages.Name = "tabPages";
			this.tabPages.SelectedIndex = 0;
			this.tabPages.Size = new System.Drawing.Size(630, 402);
			this.tabPages.TabIndex = 1;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(622, 376);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Local";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lvLocal);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(622, 376);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Scripting Applications";
			// 
			// lvLocal
			// 
			this.lvLocal.AllowColumnReorder = true;
			this.lvLocal.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					  this.fileName,
																					  this.columnHeader1,
																					  this.columnHeader2});
			this.lvLocal.ContextMenu = this.mnuLocalStore;
			this.lvLocal.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvLocal.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lvLocal.ForeColor = System.Drawing.Color.RoyalBlue;
			this.lvLocal.FullRowSelect = true;
			this.lvLocal.HideSelection = false;
			this.lvLocal.LabelEdit = true;
			this.lvLocal.LargeImageList = this.icons;
			this.lvLocal.Location = new System.Drawing.Point(3, 16);
			this.lvLocal.MultiSelect = false;
			this.lvLocal.Name = "lvLocal";
			this.lvLocal.Size = new System.Drawing.Size(616, 357);
			this.lvLocal.SmallImageList = this.icons;
			this.lvLocal.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvLocal.TabIndex = 4;
			this.lvLocal.Click += new System.EventHandler(this.lvLocal_Click);
			this.lvLocal.DoubleClick += new System.EventHandler(this.lvLocal_DoubleClick);
			// 
			// fileName
			// 
			this.fileName.Text = "File Name";
			this.fileName.Width = 190;
			// 
			// mnuLocalStore
			// 
			this.mnuLocalStore.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						  this.menuItem4,
																						  this.menuItem3,
																						  this.mnuOpenFromLocalStore,
																						  this.mnuRun,
																						  this.mnuRunScript,
																						  this.mnuPublish,
																						  this.menuItem11,
																						  this.mnuOpen,
																						  this.mnuSave,
																						  this.menuItem10,
																						  this.mnuDelete});
			this.mnuLocalStore.Popup += new System.EventHandler(this.mnuLocalStore_Popup);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 0;
			this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mnuViewDetails,
																					  this.mnuViewIcons});
			this.menuItem4.Text = "View";
			// 
			// mnuViewDetails
			// 
			this.mnuViewDetails.Index = 0;
			this.mnuViewDetails.Text = "&Details";
			this.mnuViewDetails.Click += new System.EventHandler(this.mnuViewDetails_Click);
			// 
			// mnuViewIcons
			// 
			this.mnuViewIcons.Index = 1;
			this.mnuViewIcons.Text = "&Icons";
			this.mnuViewIcons.Click += new System.EventHandler(this.mnuViewIcons_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "-";
			// 
			// mnuOpenFromLocalStore
			// 
			this.mnuOpenFromLocalStore.Index = 2;
			this.mnuOpenFromLocalStore.Text = "&Open";
			this.mnuOpenFromLocalStore.Click += new System.EventHandler(this.mnuOpenFromLocalStore_Click);
			// 
			// mnuRun
			// 
			this.mnuRun.Index = 3;
			this.mnuRun.Text = "&Run";
			this.mnuRun.Click += new System.EventHandler(this.mnuRun_Click);
			// 
			// mnuRunScript
			// 
			this.mnuRunScript.Index = 4;
			this.mnuRunScript.Text = "Run With &JScript...";
			this.mnuRunScript.Visible = false;
			this.mnuRunScript.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// mnuPublish
			// 
			this.mnuPublish.Index = 5;
			this.mnuPublish.Text = "&Publish to Public Web Store...";
			this.mnuPublish.Visible = false;
			this.mnuPublish.Click += new System.EventHandler(this.mnuPublish_Click);
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 6;
			this.menuItem11.Text = "-";
			// 
			// mnuOpen
			// 
			this.mnuOpen.Index = 7;
			this.mnuOpen.Text = "&Import...";
			this.mnuOpen.Click += new System.EventHandler(this.mnuOpen_Click);
			// 
			// mnuSave
			// 
			this.mnuSave.Index = 8;
			this.mnuSave.Text = "&Export...";
			this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 9;
			this.menuItem10.Text = "-";
			// 
			// mnuDelete
			// 
			this.mnuDelete.Index = 10;
			this.mnuDelete.Text = "&Delete";
			this.mnuDelete.Click += new System.EventHandler(this.mnuDelete_Click);
			// 
			// icons
			// 
			this.icons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.icons.ImageSize = new System.Drawing.Size(32, 32);
			this.icons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("icons.ImageStream")));
			this.icons.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// mnuPublicStore
			// 
			this.mnuPublicStore.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						   this.mnuDownload,
																						   this.menuItem2,
																						   this.mnuMyApplications,
																						   this.mnuSearch,
																						   this.menuItem1,
																						   this.mnuRemoveScriptingApplication});
			this.mnuPublicStore.Popup += new System.EventHandler(this.mnuPublicStore_Popup);
			// 
			// mnuDownload
			// 
			this.mnuDownload.Index = 0;
			this.mnuDownload.Text = "&Download...";
			this.mnuDownload.Click += new System.EventHandler(this.mnuDownload_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "-";
			// 
			// mnuMyApplications
			// 
			this.mnuMyApplications.Index = 2;
			this.mnuMyApplications.Text = "&View My Applications";
			this.mnuMyApplications.Click += new System.EventHandler(this.mnuMyApplications_Click);
			// 
			// mnuSearch
			// 
			this.mnuSearch.Index = 3;
			this.mnuSearch.Text = "&Search...";
			this.mnuSearch.Click += new System.EventHandler(this.mnuSearch_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 4;
			this.menuItem1.Text = "-";
			// 
			// mnuRemoveScriptingApplication
			// 
			this.mnuRemoveScriptingApplication.Index = 5;
			this.mnuRemoveScriptingApplication.Text = "";
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.Location = new System.Drawing.Point(546, 414);
			this.btnClose.Name = "btnClose";
			this.btnClose.TabIndex = 5;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// toolTip
			// 
			this.toolTip.AutomaticDelay = 350;
			// 
			// panel
			// 
			this.panel.Location = new System.Drawing.Point(0, 448);
			this.panel.Name = "panel";
			this.panel.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																					 this.panel1});
			this.panel.ShowPanels = true;
			this.panel.Size = new System.Drawing.Size(632, 22);
			this.panel.TabIndex = 7;
			this.panel.Text = "statusBar1";
			// 
			// panel1
			// 
			this.panel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.panel1.Width = 616;
			// 
			// progress
			// 
			this.progress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.progress.Location = new System.Drawing.Point(12, 414);
			this.progress.Name = "progress";
			this.progress.Size = new System.Drawing.Size(516, 23);
			this.progress.TabIndex = 6;
			this.progress.Visible = false;
			// 
			// progressBarTimer
			// 
			this.progressBarTimer.Tick += new System.EventHandler(this.progressBarTimer_Tick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Date Created";
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Date Updated";
			// 
			// WebStoreForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(632, 470);
			this.Controls.Add(this.panel);
			this.Controls.Add(this.progress);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.tabPages);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(640, 480);
			this.Name = "WebStoreForm";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Scripting Application Manager";
			this.tabPages.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			if ( async == null )
			{
				this.Close();
			} 
			else 
			{
				if ( async.IsCompleted )
				{
					this.Close();
				}		
			}
		}

		private void mnuSave_Click(object sender, System.EventArgs e)
		{
			Export();
		}


		private void mnuOpen_Click(object sender, System.EventArgs e)
		{
			OpenFile();
		}

//		private void btnPrevious_Click(object sender, System.EventArgs e)
//		{
//			if ( bmbOrders.Count > 0 )
//			{
//				DataRowView drv = (DataRowView)bmbOrders.Current;
//				_currentPosition = Convert.ToInt32(drv.DataView.Table.Rows[0]["OrderSequenceID"]);
//			}
//
//			_currentPosition -= 10 + 1;
//			if ( _currentPosition < 0 )
//				_currentPosition = 0;
//
//			BindGrid(_currentPosition);
//		}
//
//		private void btnNext_Click(object sender, System.EventArgs e)
//		{
//			if ( bmbOrders.Count > 0 )
//			{
//				DataRowView drv = (DataRowView)bmbOrders.Current;
//				if ( drv.DataView.Table.Rows.Count < 10 )
//				{
//					_currentPosition = Convert.ToInt32(drv.DataView.Table.Rows[drv.DataView.Table.Rows.Count - 1]["OrderSequenceID"]);
//				}
//				else 
//				{
//					_currentPosition = Convert.ToInt32(drv.DataView.Table.Rows[10 - 1]["OrderSequenceID"]);
//				}
//			}
//
//			BindGrid(_currentPosition);
//		}
		#region Public Store
		/// <summary>
		/// Loads the public store.
		/// </summary>
//		public void SearchPublicStore(Ecyware.GreenBlue.LicenseServices.Client.WebStoreViewMessage.SearchType searchType, string searchValue)
//		{
//			LicenseServiceClient client = Utils.ServicesProxy.GetClientProxy();
//
//			this.btnClose.Enabled = false;
//			//_isConnected = true;
//			WebStoreViewMessage message = new WebStoreViewMessage();
//			message.UsePaging = true;
//			message.StartFromIndex = 0;
//			message.SearchByType = searchType;
//			message.SearchValue = searchValue;
//
//			MessageResultHandler callback = new MessageResultHandler(LoadPublicStoreResultInvoker);
//			async = client.BeginSearchWebStoreView(message, callback, null);
//		}

		/// <summary>
		/// Loads the public store.
		/// </summary>
//		public void LoadPublicStore()
//		{
//
//			LicenseServiceClient client = Utils.ServicesProxy.GetClientProxy();
//
//			if ( Utils.ServicesProxy.IsConnected )
//			{
//				mnuPublish.Enabled = true;
//				btnClose.Enabled = false;
//				
//				WebStoreViewMessage message = new WebStoreViewMessage();
//				message.UsePaging = true;
//				message.StartFromIndex = 0;
//
//				MessageResultHandler callback = new MessageResultHandler(LoadPublicStoreResultInvoker);
//				async = client.BeginGetWebStoreView(message, callback, null);				
//			} 
//			else 
//			{
//				mnuPublish.Enabled = false;
//				panel1.Text = "Disconnected from public web store. To enable, login into the service.";
//				btnClose.Enabled = true;
//			}
//		}

//		private void LoadPublicStoreResultInvoker(object sender, EventArgs args)
//		{
//			try
//			{
//				this.Invoke(new MessageResultHandler(LoadPublicStoreResult),new object[] {sender,args});			
//			}
//			catch ( InvalidOperationException ex )
//			{
//				// ignore
//			}
//		}

		/// <summary>
		/// Loads the result from the retrieving from the public store.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
//		private void LoadPublicStoreResult(object sender, EventArgs e)
//		{
//			MessageEventArgs args = (MessageEventArgs)e;
//			WebStoreViewResultMessage message = (WebStoreViewResultMessage)args.Message;
//
//			this.btnClose.Enabled = true;
//			LoadWebStoreItems(message.WebStoreView);
//		}

		/// <summary>
		/// Loads the web store items.
		/// </summary>
		/// <param name="webStore"> The web store type.</param>
//		private void LoadWebStoreItems(WebStore webStore)
//		{
//			ArrayList list = new ArrayList();
//
//			lvPublic.Items.Clear();
//			foreach ( Ecyware.GreenBlue.LicenseServices.Client.WebStore.WebStoreApplicationsRow row in webStore.WebStoreApplications )
//			{
//				ListViewItem lv = new ListViewItem();
//				WebStoreItem item = new WebStoreItem();
//				item.ApplicationID = row.ApplicationID;
//				item.ApplicationName = row.ApplicationName;
//				item.CreateDate = row.CreateDate;
//				item.Description = row.Description;
//				item.Downloads = row.Downloads;
//				item.Publisher = row.Publisher;
//				item.Rating = row.Rating;
//				item.UpdateDate = row.UpdateDate;
//				item.UserRatingCount = row.UserRatingCount;
//
//				lv.Text = row.ApplicationName;
//				lv.Tag = item;
//				lv.ImageIndex = 0;
//				lv.StateImageIndex = 0;
//
//				list.Add(lv);
//			}
//
//			lvPublic.SuspendLayout();
//			lvPublic.Items.AddRange((ListViewItem[])list.ToArray(typeof(ListViewItem)));
//			lvPublic.ResumeLayout(false);
//		}

//		/// <summary>
//		/// Removes a scripting application from the public web store.
//		/// </summary>
//		public void RemoveScriptingApplication()
//		{
//			string applicationID = GetSelectedPublicApplication();
//
//			WebStoreRequestMessage message = new WebStoreRequestMessage();
//			message.ApplicationID = applicationID;
//			
//			LicenseServiceClient client = Utils.ServicesProxy.GetClientProxy();
//			MessageResultHandler callback = new MessageResultHandler(RemoveApplicationInvoker);
//			async = client.BeginRemoveScriptingApplication(message, callback, null);
//			StartProgress("Removing Scripting Application...Please wait");
//			this.btnClose.Enabled = false;
//		}
		/// <summary>
		/// Downloads the application.
		/// </summary>
//		public void DownloadApplication()
//		{
//			SaveApplicationDialog dialog = new SaveApplicationDialog();			
//
//			if ( dialog.ShowDialog() == DialogResult.OK )
//			{
//				currentScriptingApplicationName = dialog.ScriptingApplicationFilePath;
//			}
//
//			if ( currentScriptingApplicationName.Length > 0 )
//			{
//				string applicationID = GetSelectedPublicApplication();
//
//				WebStoreRequestMessage message = new WebStoreRequestMessage();
//				message.ApplicationID = applicationID;
//			
//				LicenseServiceClient client = Utils.ServicesProxy.GetClientProxy();
//				MessageResultHandler callback = new MessageResultHandler(DownloadApplicationResultInvoker);
//				async = client.BeginDownloadApplication(message, callback, null);
//				StartProgress("Downloading Scripting Application...Please wait");
//				//_isConnected = true;
//				this.btnClose.Enabled = false;
//			}
//		}
		private void DownloadApplicationResultInvoker(object sender, EventArgs e)
		{
			if ( this != null )
			{
				Invoke(new MessageResultHandler(DownloadApplicationResult), new object[] {sender, e});
			}
		}

		/// <summary>
		/// The remove application invoker.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RemoveApplicationInvoker(object sender, EventArgs e)
		{
			if ( this != null )
			{
				Invoke(new MessageResultHandler(RemoveApplicationResult), new object[] {sender, e});
			}
		}

		/// <summary>
		/// The result from removing a scripting application.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RemoveApplicationResult(object sender, EventArgs e)
		{
			MessageEventArgs args = (MessageEventArgs)e;
			WebStoreResultMessage message = (WebStoreResultMessage)args.Message;

			StopProgress("");
			this.btnClose.Enabled = true;
			//this.SearchPublicStore(Ecyware.GreenBlue.LicenseServices.Client.WebStoreViewMessage.SearchType.ByPublisher, ServicesProxy.Username);
		}

		/// <summary>
		/// The result from downloading a scripting application.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DownloadApplicationResult(object sender, EventArgs e)
		{
			MessageEventArgs args = (MessageEventArgs)e;
			WebStoreResultMessage message = (WebStoreResultMessage)args.Message;
			
			// Copy to Local Store
			byte[] data = Convert.FromBase64String(message.ApplicationData);
			FileStream stream = new FileStream(currentScriptingApplicationName,FileMode.Create, FileAccess.ReadWrite);
			stream.Write(data, 0, data.Length);
			stream.Flush();
			stream.Close();
			StopProgress("");
			//_isConnected = false;
			this.btnClose.Enabled = true;
			LoadLocalStore();
		}

		#endregion
		#region Local Store
		/// <summary>
		/// Publish a scripting application to a web store.
		/// </summary>
		private void PublishApplication()
		{
			_selectedApplicationFilePath =
				GetSelectedApplicationFilePath();	

			LicenseServiceClient client = Utils.ServicesProxy.GetClientProxy();

			ScriptingApplicationMetadataDialog publishDialog = new ScriptingApplicationMetadataDialog();

			if ( publishDialog.ShowDialog() == DialogResult.OK )
			{			
				WebStoreRequestMessage message = new WebStoreRequestMessage();
				
//				ScriptingApplicationPackage package = new ScriptingApplicationPackage();
//				package.OpenPackage(_selectedApplicationFilePath);
//				ScriptingApplicationPackage.CreatePackage(package.ScriptingApplication, package.ScriptingApplicationArguments, "

				message.ApplicationData = Utils.ServicesProxy.ReadFileToBase64String(_selectedApplicationFilePath);
				message.ApplicationID = ScriptingApplicationPackage.ReadApplicationID(_selectedApplicationFilePath);
				message.ApplicationName = publishDialog.ApplicationName;
				message.Description = publishDialog.Description;
				message.Keywords = publishDialog.Keywords;
				message.Rating = 0;

				MessageResultHandler callback = new MessageResultHandler(UpdateScriptingApplicationResultInvoker);
				client.BeginUpdateScriptingApplicationToWebStore(message, callback, null);
				StartProgress("Uploading Scripting Application...Please wait");
			}
		}

		private void UpdateScriptingApplicationResultInvoker(object sender, EventArgs e)
		{
			Invoke(new MessageResultHandler(UpdateScriptingApplicationResult), new object[] {sender, e});
		}

		private void UpdateScriptingApplicationResult(object sender, EventArgs e)
		{
			MessageEventArgs args = (MessageEventArgs)e;
			WebStoreResultMessage message = (WebStoreResultMessage)args.Message;

			StopProgress("");
			MessageBox.Show(message.Message,AppLocation.ApplicationName,MessageBoxButtons.OK, MessageBoxIcon.Information);			
		}
		/// <summary>
		/// Opens a script file.
		/// </summary>
		public string OpenScriptFile()
		{
			string result = string.Empty;
			dlgOpenFile.CheckFileExists = true;
			dlgOpenFile.InitialDirectory = Application.UserAppDataPath;
			dlgOpenFile.RestoreDirectory = true;
			dlgOpenFile.Filter = "JScript (*.js)|*.js|All files (*.*)|*.*";
			dlgOpenFile.Title = "Open Script File";

			if ( dlgOpenFile.ShowDialog() == DialogResult.OK )
			{
				Application.DoEvents();
				tempCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;

				try
				{
					result = dlgOpenFile.FileName;
				}
				catch ( Exception ex )
				{
					MessageBox.Show(ex.ToString(),AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			Cursor.Current = tempCursor;

			return result;
		}
		/// <summary>
		/// Opens a scripting application.
		/// </summary>
		public void OpenFile()
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
					SaveApplicationToStore(dlgOpenFile.FileName);
					LoadLocalStore();
				}
				catch ( Exception ex )
				{
					MessageBox.Show(ex.ToString(),AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			Cursor.Current = tempCursor;
		}

		/// <summary>
		/// Exports a scripting application.
		/// </summary>
		private void Export()
		{
			string fileName = CreateScriptingDataFile();

			if ( fileName != string.Empty )
			{
				_selectedApplicationFilePath = GetSelectedApplicationFilePath();

				if ( fileName.ToLower().EndsWith("xml") )
				{
					ScriptingApplicationPackage package = new ScriptingApplicationPackage(_selectedApplicationFilePath);
					package.ScriptingApplication.Save(fileName);					
				} else {
					ExportApplication(_selectedApplicationFilePath, fileName);
				}
			}
		}

		/// <summary>
		/// Creates a scripting application file.
		/// </summary>
		/// <returns> The scripting application file.</returns>
		private string CreateScriptingDataFile()
		{
			dlgSaveFile.InitialDirectory = Application.UserAppDataPath;
			dlgSaveFile.RestoreDirectory = true;
			dlgSaveFile.Filter = "GreenBlue Scripting Application (*.gbscr)|*.gbscr|GreenBlue Scripting Application XML (*.xml)|*.xml";
			dlgSaveFile.Title = "Save Scripting Application";

			string fileName = string.Empty;

			if ( dlgSaveFile.ShowDialog() == DialogResult.OK )
			{
				Application.DoEvents();
				tempCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;
				// file
				fileName = dlgSaveFile.FileName;
			}

			return fileName;
		}

		/// <summary>
		/// Saves the session as scripting data.
		/// </summary>
		/// <param name="fileName"> The file name.</param>
		private void ExportApplication(string existingFilePath, string fileName)
		{
			File.Copy(existingFilePath, fileName);
		}
		/// <summary>
		/// Saves the application to store.
		/// </summary>
		/// <param name="fileName"> The filename.</param>
		public void SaveApplicationToStore(string fileName)
		{
			FileInfo fi = new FileInfo(fileName);
			string filePath = AppLocation.DocumentFolder + "\\" + fi.Name;
			
			try
			{
				File.Copy(fileName, filePath);
				string xml = AppLocation.GetMIMEType(fileName);

				if ( xml.CompareTo("text/xml") == 0 )
				{
					string packageFileName = AppLocation.DocumentFolder + "\\" + fi.Name.Replace(fi.Extension,"") + ".gbscr";
					ScriptingApplication app = new ScriptingApplication();
					app.Load(filePath);
					ScriptingApplicationPackage.CreatePackage(app, null, packageFileName, false);
					File.Delete(filePath);
				}				
			}
			catch ( IOException ex )
			{				
				System.Windows.Forms.MessageBox.Show("There is already a file with name " + fi.Name + " in the local store.",AppLocation.ApplicationName, MessageBoxButtons.OK,MessageBoxIcon.Warning);
			}
			catch ( Exception ex )
			{
				System.Windows.Forms.MessageBox.Show(ex.Message,AppLocation.ApplicationName, MessageBoxButtons.OK,MessageBoxIcon.Error);
			}

		}
		/// <summary>
		/// Gets the selected application from the local web store.
		/// </summary>
		/// <returns> The selected application file path.</returns>
		private string GetSelectedApplicationFilePath()
		{
			if ( this.lvLocal.SelectedIndices.Count > 0 )
			{
				ListViewItem item = lvLocal.SelectedItems[0];

				string fileName = item.Text;
				string filePath = AppLocation.DocumentFolder + "\\" + fileName + ".xml";

				if ( !File.Exists(filePath) )
				{
					filePath = AppLocation.DocumentFolder + "\\" + fileName + ".gbscr";
				}

				return filePath;
			} 
			else 
			{
				return string.Empty;
			}
		}

//		/// <summary>
//		/// Gets the selected application from the public store.
//		/// </summary>
//		/// <returns> The selected application id.</returns>
//		private string GetSelectedPublicApplication()
//		{
//			if ( this.lvPublic.SelectedIndices.Count > 0 )
//			{
//				ListViewItem item = lvPublic.SelectedItems[0];
//				WebStoreItem webStoreItem = (WebStoreItem)item.Tag;
//
//				return webStoreItem.ApplicationID.ToString();
//			} 
//			else 
//			{
//				return string.Empty;
//			}
//		}

		/// <summary>
		/// Loads the local store view.
		/// </summary>
		private void LoadLocalStore()
		{
			this.SuspendLayout();
			lvLocal.Items.Clear();
			string[] files = Directory.GetFiles(AppLocation.DocumentFolder, "*.gbscr");

			foreach ( string file in files )
			{
				FileInfo fi = new FileInfo(file);

				ListViewItem item = new ListViewItem();
				item.Text = fi.Name.Replace(fi.Extension, "");
				string updatedTime = fi.LastWriteTime.ToLongDateString();
				string createdTime = fi.CreationTime.ToLongDateString();

				string info = "Date Updated: " + updatedTime + "\r\nDate Created: " + createdTime;
				item.Tag = info;

				item.ImageIndex = 0;
				item.StateImageIndex = 0;
				item.SubItems.Add(createdTime);
				item.SubItems.Add(updatedTime);
				this.lvLocal.Items.Add(item);
			}

			this.ResumeLayout(false);
		}
		#endregion

		private void lvLocal_Click(object sender, System.EventArgs e)
		{
			if ( lvLocal.SelectedIndices.Count > 0 )
			{
				ListViewItem item = lvLocal.SelectedItems[0];
				string description = (string)item.Tag;

//				this.toolTip.SetToolTip(lvLocal, description);
			}
		}

		private void mnuOpenFromLocalStore_Click(object sender, System.EventArgs e)
		{				
			OpenFromLocalStore();
		}


		/// <summary>
		/// Opens a scripting application from the local store.
		/// </summary>
		private void OpenFromLocalStore()
		{
			if ( async != null )
			{
				if ( !async.IsCompleted ) 
				{
					async.AsyncWaitHandle.WaitOne(100,true);
				}
			}

			_selectedApplicationFilePath =
				GetSelectedApplicationFilePath();	
			this.DialogResult = DialogResult.OK;	
		}


		private void mnuPublish_Click(object sender, System.EventArgs e)
		{
			PublishApplication();
		}


		#region Properties
		/// <summary>
		/// Gets or sets the selected application file path.
		/// </summary>
		public string SelectedApplicationFilePath
		{
			get
			{
				return _selectedApplicationFilePath;
			}
			set
			{
				_selectedApplicationFilePath = value;
			}
		}

		#endregion
		#region Progress Bar Code		
		private void progressBarTimer_Tick(object sender, System.EventArgs e)
		{
			if ( this.progress.Value == this.progress.Maximum)
			{
				this.progress.Value = 0;
			}
			this.progress.Value++;		
		}

		/// <summary>
		/// Starts the progress bar.
		/// </summary>
		/// <param name="message"> Message to display.</param>
		public void StartProgress(string message)
		{
			if ( !progress.Visible )
			{
				progress.Value = 0;
				progress.Visible = true;
				this.panel1.Text = message;
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
				
			for(int i=progress.Value;i<progress.Maximum;i++)
			{
				progress.Value = i;
				System.Threading.Thread.Sleep(5);
			}
			progress.Visible = false;
			panel1.Text = message;
		}
		#endregion

		private void mnuLocalStore_Popup(object sender, System.EventArgs e)
		{
			LoadMenus();
		}

		private void mnuPublicStore_Popup(object sender, System.EventArgs e)
		{
			LoadMenus();
		}


		/// <summary>
		/// Load the menus.
		/// </summary>
		private void LoadMenus()
		{
			if ( this.lvLocal.SelectedIndices.Count == 0 )
			{
				this.mnuDelete.Enabled = false;
				// import
				this.mnuOpen.Enabled = true;
				this.mnuPublish.Enabled = false;
				this.mnuSave.Enabled = false;
				this.mnuOpenFromLocalStore.Enabled = false;
				mnuRun.Enabled = false;
				mnuRunScript.Enabled = false;
			} 
			else 
			{
				mnuRunScript.Enabled = true;
				mnuRun.Enabled = true;
				this.mnuDelete.Enabled = true;			
				this.mnuOpen.Enabled = true;
				this.mnuPublish.Enabled = false;
				this.mnuSave.Enabled = true;
				this.mnuOpenFromLocalStore.Enabled = true;
			}

//			if ( this.lvPublic.SelectedIndices.Count == 0 )
//			{
//				this.mnuDownload.Enabled = false;
//				//this.mnuSubscribe.Enabled = false;
//			} 
//			else 
//			{
//				this.mnuDownload.Enabled = true;
//				//this.mnuSubscribe.Enabled = true;
//			}
		}

		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			_selectedApplicationFilePath =
				GetSelectedApplicationFilePath();

			string scriptFile = OpenScriptFile(); 

			ScriptingApplicationArgumentForm inputForm = new ScriptingApplicationArgumentForm();
			inputForm.RunScriptingApplication(_selectedApplicationFilePath);

			int length = inputForm.ScriptingApplication.WebRequests.Length;
			if ( inputForm.ScriptingApplication.WebRequests.Length > 0 )
			{
				length = length - 1;
			}

			ScriptingCommand command = new ScriptingCommand();
			//command.ExecuteScriptedSession(inputForm.ScriptingApplication.Clone(),length, scriptFile); 
						

//
//			if ( inputForm.ShowDialog() == DialogResult.OK )
//			{
//				TestRequestDialog testRequestDialog = new TestRequestDialog();
//				testRequestDialog.Show();
//
//				int length = inputForm.ScriptingApplication.WebRequests.Length;
//				if ( inputForm.ScriptingApplication.WebRequests.Length > 0 )
//				{
//					length = length - 1;
//				}
//
//				testRequestDialog.TestRequest(inputForm.ScriptingApplication, length);
//			}
		
		}

		private void mnuRun_Click(object sender, System.EventArgs e)
		{
			_selectedApplicationFilePath =
				GetSelectedApplicationFilePath();
						
			ScriptingApplicationArgumentForm inputForm = new ScriptingApplicationArgumentForm();
			inputForm.RunScriptingApplication(_selectedApplicationFilePath);

			if ( inputForm.ShowDialog() == DialogResult.OK )
			{
				TestRequestDialog testRequestDialog = new TestRequestDialog();
				testRequestDialog.Show();

				int length = inputForm.ScriptingApplication.WebRequests.Length;
				if ( inputForm.ScriptingApplication.WebRequests.Length > 0 )
				{
					length = length - 1;
				}
				testRequestDialog.TestRequestUntilIndex(inputForm.ScriptingApplication, length);
			}
		}

		private void mnuDownload_Click(object sender, System.EventArgs e)
		{			
//			DownloadApplication();
		}

		private void lvLocal_DoubleClick(object sender, System.EventArgs e)
		{
			if ( this.lvLocal.SelectedIndices.Count > 0 )
			{
				OpenFromLocalStore();
			}
		}

//		private void lvPublic_Click(object sender, System.EventArgs e)
//		{
//			if ( lvPublic.SelectedIndices.Count > 0 )
//			{
//				ListViewItem item = lvPublic.SelectedItems[0];
//				WebStoreItem app = (WebStoreItem)item.Tag;
//
//				this.txtDescription.Text = app.Description;
//				this.txtUserRating.Text = app.Rating.ToString();
//				this.txtDownloads.Text = app.Downloads.ToString();
//				this.txtPublisher.Text = app.Publisher;
//				this.txtUpdate.Text = app.UpdateDate.ToString();
//			}		
//		}

		private void mnuSearch_Click(object sender, System.EventArgs e)
		{
			SearchWebStore();
		}


		/// <summary>
		/// Searchs the web store.
		/// </summary>
		private void SearchWebStore()
		{
//			SearchWebStore dialog = new SearchWebStore();
//
//			if ( dialog.ShowDialog() == DialogResult.OK  )
//			{
//				this.SearchPublicStore(dialog.SearchType, dialog.SearchValue);
//			}
		}

		private void mnuMyApplications_Click(object sender, System.EventArgs e)
		{
			//this.SearchPublicStore(Ecyware.GreenBlue.LicenseServices.Client.WebStoreViewMessage.SearchType.ByPublisher, ServicesProxy.Username);
		}

		private void mnuDelete_Click(object sender, System.EventArgs e)
		{
			if ( MessageBox.Show("Are you sure you want to delete this scripting application?", AppLocation.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes )
			{
				_selectedApplicationFilePath =
					GetSelectedApplicationFilePath();
				if ( File.Exists(_selectedApplicationFilePath) )
				{
					File.Delete(_selectedApplicationFilePath);
				}

				LoadLocalStore();
			}
		}

//		private void mnuRemoveScriptingApplication_Click(object sender, System.EventArgs e)
//		{
//			RemoveScriptingApplication();
//		}

		private void mnuViewDetails_Click(object sender, System.EventArgs e)
		{
			this.lvLocal.View = System.Windows.Forms.View.Details;
		}

		private void mnuViewIcons_Click(object sender, System.EventArgs e)
		{
			this.lvLocal.View = System.Windows.Forms.View.LargeIcon;
		}


	}
}
