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
using Ecyware.GreenBlue.HtmlDom;
using Ecyware.GreenBlue.HtmlCommand;
using Ecyware.GreenBlue.WebUnitTestManager;
using Ecyware.GreenBlue.WebUnitTestCommand;
using Ecyware.GreenBlue.Protocols.Http;
using Ecyware.GreenBlue.Protocols.Http.Scripting;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Contains the session designer.
	/// </summary>
	public class SessionDesigner : Ecyware.GreenBlue.Controls.BasePluginForm
	{		
		static int objectCount = 0;
		Cursor tempCursor;

		int sessionRequestItemIndex = -1;
		private InspectorConfiguration _inspectorConfig = null;
		private Session _session = null;
		private Session _safeSession = null;
		private SessionRequest _sessionRequest = null;
		private BaseSessionDesignerUserControl _designerUserControl = null;

		private SessionFormEditor formControl = new SessionFormEditor();
		private SessionCookieEditor cookieControl = new SessionCookieEditor();
		private SessionPostDataEditor postDataControl = new SessionPostDataEditor();
		private SessionQueryStringEditor queryStringControl = new SessionQueryStringEditor();
		private SessionWebUnitTestItemEditor testManagerControl = new SessionWebUnitTestItemEditor();
		private SessionRequestHeaderEditor headersEditorControl = new SessionRequestHeaderEditor();
		private SessionInfoForm sessionInfoControl = new SessionInfoForm();

		internal event ApplyMenuSettingsEventHandler ApplyMenuSettingsEvent;
		internal event ApplyToolbarSettingsEventHandler ApplyToolbarSettingsEvent;
		internal event HttpGetPageEventHandler HttpGetPageEvent;

		private System.Windows.Forms.TreeView tvSessionTree;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Panel pnUserControl;
		private System.Windows.Forms.ImageList icons;
		private System.Windows.Forms.ContextMenu mnuSessionRequest;
		private System.Windows.Forms.MenuItem mnuRemoveSessionRequest;
		private System.Windows.Forms.MenuItem mnuSessionProperties;
		private System.Windows.Forms.MenuItem mnuAllowUpdateSessionCookies;
		private System.Windows.Forms.MenuItem mnuAllowBacktracking;
		private System.Windows.Forms.MenuItem mnuSaveSessionScriptingData;
		private System.Windows.Forms.SaveFileDialog dlgSaveFile;
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Creates a new Session Designer.
		/// </summary>
		public SessionDesigner()
		{
			objectCount++;

			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// Add SessionFormEditor UserControl			
			formControl.Dock = DockStyle.Fill;
			formControl.Visible = false;
			formControl.UpdateSessionRequestEvent += new UpdateSessionRequestEventHandler(formControl_UpdateSessionRequestEvent);
			this.pnUserControl.Controls.Add(formControl);

			// Add SessionCookieEditor UserControl			
			cookieControl.Dock = DockStyle.Fill;
			cookieControl.Visible = false;
			cookieControl.UpdateSessionRequestEvent += new UpdateSessionRequestEventHandler(cookieControl_UpdateSessionRequestEvent);
			this.pnUserControl.Controls.Add(cookieControl);

			// Add SessionPostDataEditor UserControl			
			postDataControl.Dock = DockStyle.Fill;
			postDataControl.Visible = false;
			postDataControl.UpdateSessionRequestEvent += new UpdateSessionRequestEventHandler(postDataControl_UpdateSessionRequestEvent);
			this.pnUserControl.Controls.Add(postDataControl);

			// Add SessionQueryStringEditor UserControl			
			queryStringControl.Dock = DockStyle.Fill;
			queryStringControl.Visible = false;
			queryStringControl.UpdateSessionRequestEvent += new UpdateSessionRequestEventHandler(queryStringControl_UpdateSessionRequestEvent);
			this.pnUserControl.Controls.Add(queryStringControl);

			// Add SessionWebUnitTestItemEditor UserControl			
			testManagerControl.Dock = DockStyle.Fill;
			testManagerControl.Visible = false;
			this.pnUserControl.Controls.Add(testManagerControl);

			// Add SessionRequestHeaderEditor UserControl			
			headersEditorControl.Dock = DockStyle.Fill;
			headersEditorControl.HttpGetEvent += new HttpGetPageEventHandler(HttpGetPage);
			headersEditorControl.Visible = false;
			this.pnUserControl.Controls.Add(headersEditorControl);

			// Add SessionInfoForm UserControl			
			sessionInfoControl.Dock = DockStyle.Fill;
			sessionInfoControl.UpdateSessionEvent += new UpdateSessionEventHandler(sessionInfoControl_UpdateSessionEvent);
			sessionInfoControl.Visible = false;
			this.pnUserControl.Controls.Add(sessionInfoControl);
		}

		#region Properties
		/// <summary>
		/// Gets or sets a recording session.
		/// </summary>
		public Session WebSession
		{
			get
			{
				return _session;
			}
			set
			{
				_session = value;
			}
		}

		/// <summary>
		/// Gets or sets the backup session.
		/// </summary>
		public Session SafeSession
		{
			get
			{
				return _safeSession;
			}
		}

		/// <summary>
		/// Gets or sets the Inspector Configuration.
		/// </summary>
		public InspectorConfiguration InspectorConfig
		{
			get
			{
				return _inspectorConfig;
			}
			set
			{
				_inspectorConfig = value;
			}
		}


		/// <summary>
		/// Gets or sets the selected designer user control.
		/// </summary>
		private BaseSessionDesignerUserControl SelectedDesignerControl
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

		/// <summary>
		/// Gets or sets the current session request.
		/// </summary>
		private SessionRequest CurrentSessionRequest
		{
			get
			{
				return _sessionRequest;
			}
			set
			{
				_sessionRequest = value;
			}
		}
		#endregion
		#region Loading Session Tree
		/// <summary>
		/// Loads a session.
		/// </summary>
		public void LoadSession()
		{
			// save safe session
			if ( this._safeSession == null )
			{
				this._safeSession = this.WebSession.CloneSession();
			}

			this.mnuAllowBacktracking.Checked = this.WebSession.AllowSafeRequestBacktracking;
			this.mnuAllowUpdateSessionCookies.Checked = this.WebSession.IsCookieUpdatable;

			// create parent node
			TreeNode parentNode = new TreeNode();
			parentNode.Text = "Session";
			parentNode.ImageIndex = 3;
			parentNode.SelectedImageIndex = 3;

			for (int i=0;i<WebSession.SessionRequests.Count;i++)
			{
				SessionRequest request = WebSession.SessionRequests[i];
				TreeNode node = new TreeNode();

				if ( request.RequestType == HttpRequestType.GET )
				{
					node.Text = "GET " + request.Url.ToString();
					node.Tag = i;
					node.ImageIndex = 3;
					node.SelectedImageIndex = 3;
					parentNode.Nodes.Add(node);

					node.Nodes.Add(new TreeNode("Posted Form",1,1));
					//node.Nodes.Add("Query String");
					node.Nodes.Add(new TreeNode("Cookies",5,5));
					node.Nodes.Add(new TreeNode("Web Unit Tests",4,4));
				} 
				else if ( request.RequestType == HttpRequestType.POST )
				{
					node.Text = "POST " + request.Url.ToString();
					node.Tag = i;
					node.ImageIndex = 3;
					node.SelectedImageIndex = 3;
					parentNode.Nodes.Add(node);

					node.Nodes.Add(new TreeNode("Posted Form",1,1));
					node.Nodes.Add(new TreeNode("Post Data",6,6));
					node.Nodes.Add(new TreeNode("Cookies",5,5));
					node.Nodes.Add(new TreeNode("Web Unit Tests",4,4));
				}
			}

			parentNode.Expand();
			this.tvSessionTree.Nodes.Add(parentNode);
		}
		/// <summary>
		/// Loads a from a file stream.
		/// </summary>
		/// <param name="stm"> Sesion Stream to load.</param>
		public void LoadSession(Stream stm)
		{
			// Load stream into Session
			SessionDocument document = SessionDocument.OpenSessionDocument(stm);			
			this.WebSession = document.TestSession;			
			this._safeSession = document.SafeSession;

			LoadSession();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SessionDesigner));
			this.tvSessionTree = new System.Windows.Forms.TreeView();
			this.mnuSessionRequest = new System.Windows.Forms.ContextMenu();
			this.mnuRemoveSessionRequest = new System.Windows.Forms.MenuItem();
			this.mnuSessionProperties = new System.Windows.Forms.MenuItem();
			this.mnuAllowUpdateSessionCookies = new System.Windows.Forms.MenuItem();
			this.mnuAllowBacktracking = new System.Windows.Forms.MenuItem();
			this.icons = new System.Windows.Forms.ImageList(this.components);
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.pnUserControl = new System.Windows.Forms.Panel();
			this.mnuSaveSessionScriptingData = new System.Windows.Forms.MenuItem();
			this.dlgSaveFile = new System.Windows.Forms.SaveFileDialog();
			this.SuspendLayout();
			// 
			// tvSessionTree
			// 
			this.tvSessionTree.ContextMenu = this.mnuSessionRequest;
			this.tvSessionTree.Dock = System.Windows.Forms.DockStyle.Left;
			this.tvSessionTree.HideSelection = false;
			this.tvSessionTree.ImageList = this.icons;
			this.tvSessionTree.Location = new System.Drawing.Point(0, 0);
			this.tvSessionTree.Name = "tvSessionTree";
			this.tvSessionTree.Size = new System.Drawing.Size(234, 354);
			this.tvSessionTree.TabIndex = 0;
			this.tvSessionTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSessionTree_AfterSelect);
			// 
			// mnuSessionRequest
			// 
			this.mnuSessionRequest.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							  this.mnuRemoveSessionRequest,
																							  this.mnuSessionProperties,
																							  this.mnuSaveSessionScriptingData});
			this.mnuSessionRequest.Popup += new System.EventHandler(this.mnuSessionRequest_Popup);
			// 
			// mnuRemoveSessionRequest
			// 
			this.mnuRemoveSessionRequest.Index = 0;
			this.mnuRemoveSessionRequest.Text = "&Remove Session Request";
			this.mnuRemoveSessionRequest.Visible = false;
			this.mnuRemoveSessionRequest.Click += new System.EventHandler(this.mnuRemoveSessionRequest_Click);
			// 
			// mnuSessionProperties
			// 
			this.mnuSessionProperties.Index = 1;
			this.mnuSessionProperties.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																								 this.mnuAllowUpdateSessionCookies,
																								 this.mnuAllowBacktracking});
			this.mnuSessionProperties.Text = "Session Properties";
			this.mnuSessionProperties.Visible = false;
			// 
			// mnuAllowUpdateSessionCookies
			// 
			this.mnuAllowUpdateSessionCookies.Index = 0;
			this.mnuAllowUpdateSessionCookies.Text = "Always update session cookies";
			this.mnuAllowUpdateSessionCookies.Click += new System.EventHandler(this.mnuAllowUpdateSessionCookies_Click);
			// 
			// mnuAllowBacktracking
			// 
			this.mnuAllowBacktracking.Index = 1;
			this.mnuAllowBacktracking.Text = "Allow safe request backtracking";
			this.mnuAllowBacktracking.Click += new System.EventHandler(this.mnuAllowBacktracking_Click);
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
			// mnuSaveSessionScriptingData
			// 
			this.mnuSaveSessionScriptingData.Index = 2;
			this.mnuSaveSessionScriptingData.Text = "Save Scripting Application...";
			this.mnuSaveSessionScriptingData.Visible = false;
			this.mnuSaveSessionScriptingData.Click += new System.EventHandler(this.mnuSaveSessionScriptingData_Click);
			// 
			// SessionDesigner
			// 
			this.Controls.Add(this.pnUserControl);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.tvSessionTree);
			this.Name = "SessionDesigner";
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

			if ( objectCount == 0 )
			{
				UpdateRunTestMenu(false);
				UpdateSaveWebSessionMenu(false);
			}
		}

		private void SessionDesigner_Load(object sender, System.EventArgs e)
		{
			UpdateRunTestMenu(true);
			UpdateSaveWebSessionMenu(true);
		}

		/// <summary>
		/// Hides the controls not currently selected.
		/// </summary>
		/// <param name="userControlType"> The current selected control type.</param>
		private void HideControls(Type userControlType)
		{
			for (int i=0;i<pnUserControl.Controls.Count;i++ )
			{
				if ( !(pnUserControl.Controls[i].GetType() == userControlType) )
				{
					// pnUserControl.Controls[i].Visible = false;
					pnUserControl.Controls[i].Hide();
				}
			}
		}

		/// <summary>
		/// Navigates the requested url.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HttpGetPage(object sender, RequestGetEventArgs e)
		{
			HttpGetPageEvent(this, e);
		}

		/// <summary>
		/// Updates the session designer edtion.
		/// </summary>
		public void UpdateSessionDesignerEdition()
		{
			// update from session info panel
			this.sessionInfoControl.UpdateSessionData();

			if ( this.SelectedDesignerControl != null )
			{
				this.SelectedDesignerControl.UpdateSessionRequestData();
			}
		}
		#region User Control Methods Callbacks
		private void sessionInfoControl_UpdateSessionEvent(object sender, UpdateSessionEventArgs e)
		{
			this.WebSession.IsCookieUpdatable = e.WebSession.IsCookieUpdatable;
			this.WebSession.AllowSafeRequestBacktracking = e.WebSession.AllowSafeRequestBacktracking;

			this.SafeSession.IsCookieUpdatable = e.WebSession.IsCookieUpdatable;
			this.SafeSession.AllowSafeRequestBacktracking = e.WebSession.AllowSafeRequestBacktracking;

		}
		private void postDataControl_UpdateSessionRequestEvent(object sender, UpdateSessionRequestEventArgs e)
		{
			if ( e.UpdateType == UpdateSessionRequestType.PostData )
			{
				if ( this.CurrentSessionRequest is PostSessionRequest )
				{
					PostSessionRequest session = (PostSessionRequest)this.CurrentSessionRequest;
					session.PostData = e.PostData;
				}
			}
		}

		private void queryStringControl_UpdateSessionRequestEvent(object sender, UpdateSessionRequestEventArgs e)
		{
			if ( e.UpdateType == UpdateSessionRequestType.QueryString )
			{
				if ( this.CurrentSessionRequest is GetSessionRequest )
				{
					GetSessionRequest session = (GetSessionRequest)this.CurrentSessionRequest;
					session.QueryString = e.QueryString;
				}
			}
		}

		private void formControl_UpdateSessionRequestEvent(object sender, UpdateSessionRequestEventArgs e)
		{
			if ( e.UpdateType == UpdateSessionRequestType.Form )
			{
				if ( this.CurrentSessionRequest is PostSessionRequest )
				{
					PostSessionRequest session = (PostSessionRequest)this.CurrentSessionRequest;
					session.Form = e.Form;
				}
			}
		}

		private void cookieControl_UpdateSessionRequestEvent(object sender, UpdateSessionRequestEventArgs e)
		{
			if ( e.UpdateType == UpdateSessionRequestType.Cookies )
			{
				this.CurrentSessionRequest.RequestCookies = e.Cookies;
			}
		}

		#endregion
		#region ContextMenu Methods
		/// <summary>
		/// Removes a session request from the session.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuRemoveSessionRequest_Click(object sender, System.EventArgs e)
		{
			//string testName = tvSessionTree.SelectedNode.Text;

			if ( tvSessionTree.Nodes[0].Nodes.Count > 2 )
			{			
				if ( MessageBox.Show("Are you sure you want to remove the selected session request?","Ecyware GreenBlue Inspector", MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes )
				{	
					int index = tvSessionTree.Nodes[0].Nodes.IndexOf(tvSessionTree.SelectedNode);

					// remove from tree, safe session and test session
					tvSessionTree.Nodes[0].Nodes.Remove(tvSessionTree.SelectedNode);

					try
					{
						this.WebSession.SessionRequests.RemoveAt(index);
						this.SafeSession.SessionRequests.RemoveAt(index);
					}
					catch (Exception ex)
					{
						Utils.ExceptionHandler.RegisterException(ex);
						MessageBox.Show(ex.Message,"Ecyware GreenBlue Inspector", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			} 
			else 
			{
				MessageBox.Show("You must have at least two session requests in a session.","Ecyware GreenBlue Inspector", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		/// <summary>
		/// Applies the settings the for updating the cookies for the new session run.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuAllowUpdateSessionCookies_Click(object sender, System.EventArgs e)
		{
			this.SafeSession.IsCookieUpdatable = !this.SafeSession.IsCookieUpdatable;
			this.WebSession.IsCookieUpdatable = this.SafeSession.IsCookieUpdatable;

			this.mnuAllowUpdateSessionCookies.Checked = !this.mnuAllowUpdateSessionCookies.Checked;
		}

		/// <summary>
		/// Applies the setting for allowing backtracking of safe sessions.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuAllowBacktracking_Click(object sender, System.EventArgs e)
		{
			this.SafeSession.AllowSafeRequestBacktracking = !this.SafeSession.AllowSafeRequestBacktracking;
			this.WebSession.AllowSafeRequestBacktracking = this.SafeSession.AllowSafeRequestBacktracking;

			this.mnuAllowBacktracking.Checked = !this.mnuAllowBacktracking.Checked;		
		}
		#endregion		
		#region Tree Events
		/// <summary>
		/// Displays the selected designer editor.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tvSessionTree_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if ( e.Node.Text == "Session" )
			{
				//this.mnuSessionProperties.Visible = true;
				//this.mnuRemoveSessionRequest.Visible = false;

				DisplaySessionInfoUserControl();
			} else {			
				// load session request window, parent nodes only
				if ( e.Node.Tag != null  )
				{
					this.mnuSessionProperties.Visible = false;
					this.mnuRemoveSessionRequest.Visible = true;

					if ( sessionRequestItemIndex != -1 )
					{
						// Update usercontrol data.
						this.SelectedDesignerControl.UpdateSessionRequestData();
					} 

					// sessionRequestItemIndex = (int)e.Node.Tag;					 
					sessionRequestItemIndex = e.Node.Index;
					this.CurrentSessionRequest = this.WebSession.SessionRequests[sessionRequestItemIndex];

					// is a parent, that is a SesssionRequest was selected
					DisplaySessionRequestInfoUserControl();
				}
				else 
				{
					this.mnuSessionProperties.Visible = false;
					this.mnuRemoveSessionRequest.Visible = false;

					#region Display User Control
					// else is a child
					string selectedNodeType = e.Node.Text.ToLower();

					if ( sessionRequestItemIndex != -1 )
					{
						// Update usercontrol data.
						this.SelectedDesignerControl.UpdateSessionRequestData();
					}				
				
					// set selected request item.
					sessionRequestItemIndex = e.Node.Parent.Index;
					this.CurrentSessionRequest = this.WebSession.SessionRequests[sessionRequestItemIndex];

					switch ( selectedNodeType )
					{
						case "posted form":
							// Load SessionFormEditor UserControl
							DisplayFormUserControl();
							break;
						case "cookies":
							DisplayCookieUserControl();
							break;
						case "query string":
							DisplayQueryStringUserControl();
							break;
						case "post data":
							DisplayPostDataUserControl();
							break;
						case "web unit tests":
							DisplayTestEditorUserControl();
							break;
						default:
							break;
					}
					#endregion
				}
			}
			

			
		}
//		private void tvSessionTree_BeforeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
//		{
//			// switch statement for node type
//			if ( e.Node.Parent == null )
//			{
//				// is a parent, that is a SesssionRequest was selected
//			} 
//			else 
//			{
//				// else is a child
//				string selectedNodeType = e.Node.Text.ToLower();
//				int sessionRequestItemIndex = (int)e.Node.Parent.Tag;
//
//				switch ( selectedNodeType )
//				{
//					case "posted form":
//						// Load SessionFormEditor UserControl
//						MessageBox.Show("Leaving Posted Form");
//						//DisplayFormUserControl(sessionRequestItemIndex);
//						break;
//					case "cookies":
//						// DisplayCookieUserControl(sessionRequestItemIndex);
//						break;
//					case "post data":
//						// DisplayPostDataUserControl(sessionRequestItemIndex);
//						break;
//					default:
//						break;
//				}
//			}		
//		}

		#endregion
		#region Designer Editors

		/// <summary>
		/// Displays the session info panel.
		/// </summary>
		private void DisplaySessionInfoUserControl()
		{
			SessionRequest sessionRequest = this.CurrentSessionRequest;

			this.sessionInfoControl.SelectedSession = this.WebSession;
			this.sessionInfoControl.SetSessionSettings();
			this.sessionInfoControl.Show();			

			this.SelectedDesignerControl = sessionInfoControl;

			HideControls(typeof(SessionInfoForm));
		}

		/// <summary>
		/// Displays the header editor.
		/// </summary>
		private void DisplaySessionRequestInfoUserControl()
		{
			SessionRequest sessionRequest = this.CurrentSessionRequest;

			headersEditorControl.WebSessionRequest = sessionRequest;
			headersEditorControl.LoadHeaderEditor();
			headersEditorControl.Show();

			this.SelectedDesignerControl = headersEditorControl;

			HideControls(typeof(SessionRequestHeaderEditor));
		}

		/// <summary>
		/// Displays the Test Editor.
		/// </summary>
		private void DisplayTestEditorUserControl()
		{	
			SessionRequest sessionRequest = this.CurrentSessionRequest;

			testManagerControl.WebSessionRequest = sessionRequest;
			testManagerControl.LoadTestManager(this.InspectorConfig);
			testManagerControl.Show();
			
			this.SelectedDesignerControl = testManagerControl;

			HideControls(typeof(SessionWebUnitTestItemEditor));
		}

		/// <summary>
		/// Displays the QueryString Editor.
		/// </summary>
		private void DisplayQueryStringUserControl()
		{	
			SessionRequest sessionRequest = this.CurrentSessionRequest;
			//SessionQueryStringEditor queryStringEditor = (SessionQueryStringEditor)this.pnUserControl.Controls[3];

			queryStringControl.Show();
			this.SelectedDesignerControl = queryStringControl;
			HideControls(typeof(SessionQueryStringEditor));

			if ( sessionRequest.RequestType == HttpRequestType.GET )
			{
				queryStringControl.QueryString = ((GetSessionRequest)sessionRequest).QueryString;

				queryStringControl.LoadCurrentView();				
			} 
			else 
			{
				queryStringControl.DisplayNoDataMessage();
			}
		}

		/// <summary>
		/// Displays the PostData Editor.
		/// </summary>
		private void DisplayPostDataUserControl()
		{	
			SessionRequest sessionRequest = this.CurrentSessionRequest;
			//SessionPostDataEditor postDataEditor = (SessionPostDataEditor)this.pnUserControl.Controls[2];

			if ( sessionRequest.RequestType == HttpRequestType.POST )
			{
				postDataControl.PostData = ((PostSessionRequest)sessionRequest).PostData;								

				postDataControl.LoadPostData();				
			} 
			else 
			{
				postDataControl.DisplayNoDataMessage();
			}

			postDataControl.Show();
			this.SelectedDesignerControl = postDataControl;
			HideControls(typeof(SessionPostDataEditor));
		}

		/// <summary>
		/// Displays the Cookie Editor.
		/// </summary>
		private void DisplayCookieUserControl()
		{
			SessionRequest sessionRequest = this.CurrentSessionRequest;
			//SessionCookieEditor cookieEditor = (SessionCookieEditor)this.pnUserControl.Controls[1];
			cookieControl.Cookies = sessionRequest.RequestCookies;

			if ( cookieControl.Cookies != null )
			{
				cookieControl.DisplayCookies();
				// cookieControl.Visible = true;
			} 
			else 
			{
				cookieControl.DisplayNoDataMessage();
			}

			cookieControl.Show();
			this.SelectedDesignerControl = cookieControl;
			HideControls(typeof(SessionCookieEditor));
		}

		/// <summary>
		/// Displays the Form Editor.
		/// </summary>
		private void DisplayFormUserControl()
		{	
			SessionRequest sessionRequest = this.CurrentSessionRequest;

			//SessionFormEditor formEditor = (SessionFormEditor)this.pnUserControl.Controls[0];
			formControl.Form = sessionRequest.Form;

			if ( formControl.Form != null )
			{
				HtmlFormTagCollection forms = new HtmlFormTagCollection(1);
				forms.Add(formControl.Form.Name, formControl.Form);
				formControl.LoadFormTree(forms);				
			} 
			else 
			{
				formControl.DisplayNoDataMessage();
			}

			formControl.Show();
			this.SelectedDesignerControl = formControl;
			HideControls(typeof(SessionFormEditor));
		}

		#endregion
		#region Menus
		/// <summary>
		/// Toggles the RunTest menu.
		/// </summary>
		/// <param name="enabled"></param>
		public void UpdateRunTestMenu(bool enabled)
		{
			// new Arguments
			ApplyMenuSettingsEventArgs newArgs = new ApplyMenuSettingsEventArgs();
	
			// identify the shortcut
			newArgs.RootShortcut=Shortcut.CtrlShiftF;		

			// get menu item by index
			Ecyware.GreenBlue.Controls.MenuItem runMenu = this.PluginMenus.GetByIndex(2);

			runMenu.Enabled = enabled;
			newArgs.MenuItems.Add(runMenu.Name,runMenu);

			// update menu
			this.ApplyMenuSettingsEvent(this,newArgs);
		}


		/// <summary>
		/// Toggles the SaveWebSession Menu.
		/// </summary>
		/// <param name="enabled"></param>
		public void UpdateSaveWebSessionMenu(bool enabled)
		{
			// new Arguments
			ApplyMenuSettingsEventArgs newArgs = new ApplyMenuSettingsEventArgs();
	
			// identify the shortcut
			newArgs.RootShortcut = Shortcut.CtrlShiftF;		

			// get menu item by index
			Ecyware.GreenBlue.Controls.MenuItem saveMenu = this.PluginMenus.GetByIndex(1);

			saveMenu.Enabled = enabled;
			newArgs.MenuItems.Add(saveMenu.Name,saveMenu);

			// update menu
			this.ApplyMenuSettingsEvent(this,newArgs);
		}

		#endregion

		private void mnuSessionRequest_Popup(object sender, System.EventArgs e)
		{
			if ( this.tvSessionTree.SelectedNode.Parent == null )
			{
				this.mnuRemoveSessionRequest.Visible = false;
				this.mnuSaveSessionScriptingData.Visible = true;
			} else {
				this.mnuSaveSessionScriptingData.Visible = false;
				this.mnuRemoveSessionRequest.Visible = true;
			}
		}

		private void mnuSaveSessionScriptingData_Click(object sender, System.EventArgs e)
		{
			string fileName = CreateScriptingDataFile();

			if ( fileName != string.Empty )
				SaveSessionAsScriptingData(fileName);
		}


		private string CreateScriptingDataFile()
		{
			// send this to disk
			//System.IO.Stream stream = null;

			dlgSaveFile.InitialDirectory = Application.UserAppDataPath;
			dlgSaveFile.RestoreDirectory = true;
			dlgSaveFile.Filter = "Ecyware GreenBlue Scripting Data (*.xml)|*.xml";
			dlgSaveFile.Title = "Save Scripting Data";

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
		private void SaveSessionAsScriptingData(string fileName)
		{
			try
			{
				ScriptingApplication sd = new ScriptingApplication();

				foreach (SessionRequest req in WebSession.SessionRequests )
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

				sd.Save(fileName);				
			}
			catch ( Exception ex )
			{
				MessageBox.Show(ex.ToString());
			}
		}
	}
}

