// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004 - July 2004
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Text;
using Ecyware.GreenBlue.Controls;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Engine.HtmlCommand;
using Ecyware.GreenBlue.WebUnitTestManager;
using Ecyware.GreenBlue.WebUnitTestCommand;
using Ecyware.GreenBlue.Protocols.Http;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.Scripting;
using mshtml;


namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Contains the Forms Editor Form.
	/// </summary>
	public class FormsEditor : BasePluginForm
	{
		private InspectorConfiguration _inspectorConfig = null;
		private ResponseBuffer _sessionData=null;
		private HtmlParserProperties _parserSettings = null;

		internal delegate void RequestPostEventHandler(object sender,RequestPostEventArgs e);
		internal event RequestPostEventHandler RequestPostEvent;
		internal event ApplyMenuSettingsEventHandler ApplyMenuSettingsEvent;

		private System.Windows.Forms.ContextMenu mnuFormNode;
		private System.Windows.Forms.MenuItem mnuSubmitNormal;
		private System.Windows.Forms.MenuItem mnuSubmitPost;
		private System.Windows.Forms.MenuItem mnuSubmitGet;
		private System.Windows.Forms.ContextMenu mnuFormChild;
		private System.Windows.Forms.MenuItem mnuApplyTest;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem mnuApplyBO;
		private System.Windows.Forms.MenuItem mnuApplySQL;
		private Ecyware.GreenBlue.Controls.FormEditor tree;
		private System.Windows.Forms.MenuItem mnuApplyXSS;
		private System.Windows.Forms.MenuItem mnuChangeAction;
		private System.ComponentModel.IContainer components = null;

		#region Constructors
		private FormsEditor()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();
			InitializeTreeEditor();
		}
		/// <summary>
		/// Creates a new FormsEditor.
		/// </summary>
		/// <param name="forms"> Form collection to load.</param>
		/// <param name="sessionData"> ResponseBuffer data.</param>
		/// <param name="parserSettings"> Parser Settings.</param>
		/// <param name="inspectorConfiguration"> The inspector configuration.</param>
		public FormsEditor(HtmlFormTagCollection forms, ResponseBuffer sessionData, HtmlParserProperties parserSettings, InspectorConfiguration inspectorConfiguration):this()
		{
			this.SessionData = sessionData;
			this.InspectorConfig = inspectorConfiguration;

			// set parser settings 
			_parserSettings = parserSettings;

			// Load tree
			LoadFormTree(forms);
		}


		#endregion
		private void InitializeTreeEditor()
		{
			this.IsUnique=true;
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

		#region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.mnuFormNode = new System.Windows.Forms.ContextMenu();
			this.mnuSubmitNormal = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.mnuSubmitPost = new System.Windows.Forms.MenuItem();
			this.mnuSubmitGet = new System.Windows.Forms.MenuItem();
			this.mnuApplyTest = new System.Windows.Forms.MenuItem();
			this.mnuChangeAction = new System.Windows.Forms.MenuItem();
			this.mnuFormChild = new System.Windows.Forms.ContextMenu();
			this.mnuApplyBO = new System.Windows.Forms.MenuItem();
			this.mnuApplySQL = new System.Windows.Forms.MenuItem();
			this.mnuApplyXSS = new System.Windows.Forms.MenuItem();
			this.tree = new Ecyware.GreenBlue.Controls.FormEditor();
			this.SuspendLayout();
			// 
			// mnuFormNode
			// 
			this.mnuFormNode.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						this.mnuSubmitNormal,
																						this.mnuApplyTest,
																						this.mnuChangeAction});
			// 
			// mnuSubmitNormal
			// 
			this.mnuSubmitNormal.Index = 0;
			this.mnuSubmitNormal.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							this.menuItem3,
																							this.mnuSubmitPost,
																							this.mnuSubmitGet});
			this.mnuSubmitNormal.Text = "&Submit form";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 0;
			this.menuItem3.Text = "&Submit form";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// mnuSubmitPost
			// 
			this.mnuSubmitPost.Index = 1;
			this.mnuSubmitPost.Text = "Submit form as a &POST";
			this.mnuSubmitPost.Click += new System.EventHandler(this.mnuSubmitPost_Click);
			// 
			// mnuSubmitGet
			// 
			this.mnuSubmitGet.Index = 2;
			this.mnuSubmitGet.Text = "Submit form as a &GET";
			this.mnuSubmitGet.Click += new System.EventHandler(this.mnuSubmitGet_Click);
			// 
			// mnuApplyTest
			// 
			this.mnuApplyTest.Index = 1;
			this.mnuApplyTest.Text = "&Set tests...";
			this.mnuApplyTest.Visible = false;
			// 
			// mnuChangeAction
			// 
			this.mnuChangeAction.Index = 2;
			this.mnuChangeAction.Text = "&Form Properties...";
			this.mnuChangeAction.Click += new System.EventHandler(this.mnuChangeAction_Click);
			// 
			// mnuFormChild
			// 
			this.mnuFormChild.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.mnuApplyBO,
																						 this.mnuApplySQL,
																						 this.mnuApplyXSS});
			// 
			// mnuApplyBO
			// 
			this.mnuApplyBO.Index = 0;
			this.mnuApplyBO.Text = "Generate &Buffer Overflow...";
			this.mnuApplyBO.Click += new System.EventHandler(this.mnuApplyBO_Click);
			// 
			// mnuApplySQL
			// 
			this.mnuApplySQL.Index = 1;
			this.mnuApplySQL.Text = "Select SQL &Injection...";
			this.mnuApplySQL.Click += new System.EventHandler(this.mnuApplySQL_Click);
			// 
			// mnuApplyXSS
			// 
			this.mnuApplyXSS.Index = 2;
			this.mnuApplyXSS.Text = "Select &XSS Value...";
			this.mnuApplyXSS.Click += new System.EventHandler(this.mnuApplyXSS_Click);
			// 
			// tree
			// 
			this.tree.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tree.ImageIndex = -1;
			this.tree.ItemHeight = 20;
			this.tree.Location = new System.Drawing.Point(0, 0);
			this.tree.Name = "tree";
			this.tree.SelectedImageIndex = -1;
			this.tree.SelectedNode = null;
			this.tree.Size = new System.Drawing.Size(450, 288);
			this.tree.TabIndex = 0;
			this.tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tree_AfterSelect);
			// 
			// FormsEditor
			// 
			this.Controls.Add(this.tree);
			this.Name = "FormsEditor";
			this.Size = new System.Drawing.Size(450, 288);
			this.ResumeLayout(false);

		}
		#endregion
		#region Properties
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
		/// Gets or sets the current session data.
		/// </summary>
		public ResponseBuffer SessionData
		{
			get
			{
				return _sessionData;
			}
			set
			{
				_sessionData = value;
			}
		}

		/// <summary>
		/// Gets the current HtmlFormTag.
		/// </summary>
		public HtmlFormTag GetFormTag
		{
			get
			{
				// check for any form editor node with HtmlFormTag
		//		if ( tree.SelectedNode.BaseHtmlTag is HtmlFormTag )
		//		{
					return (HtmlFormTag)tree.SelectedNode.BaseHtmlTag;
				//}
			}

		}
		#endregion
		private void tree_AfterSelect(object sender, TreeViewEventArgs e)
		{
			// check for any form editor node with HtmlFormTag
			if ( tree.SelectedNode.BaseHtmlTag is HtmlFormTag )
			{
				tree.ContextMenu = mnuFormNode;
			} 
			else 
			{
				if ( !(tree.SelectedNode.BaseHtmlTag is HtmlFormTag) )
				{
					if ( tree.SelectedNode.BaseHtmlTag != null )
					{
						tree.ContextMenu = this.mnuFormChild;
					} else {
						tree.ContextMenu = null;
					}
				} 
				else 
				{
					tree.ContextMenu=null;
				}
			}
		}

		#region FormTree Building methods
		/// <summary>
		/// Loads the form tree.
		/// </summary>
		/// <param name="forms"> The HtmlFormTagCollection to load.</param>
		private void LoadFormTree(HtmlFormTagCollection forms)
		{
			foreach (DictionaryEntry de in forms)
			{
				HtmlFormTag form = (HtmlFormTag)de.Value;

				StringBuilder label = new StringBuilder();
				// add Form node
				label.Append("<form name=");
				label.Append((string)de.Key);
				label.Append(" method=");
				label.Append(form.Method);
				label.Append(" action=");
				label.Append(form.Action);

				if ( form.OnSubmit != null )
				{
					if ( form.OnSubmit.Length > 0 )
					{
						label.Append(" onsubmit=");
						label.Append(form.OnSubmit);
					}
				}
				label.Append(">");

				FormEditorNode formNode = tree.AddFormNode(label.ToString(),form);
				
				for (int i=0;i<form.Count;i++)
				{
					FormEditorNode child = new FormEditorNode();
					HtmlTagBaseList controlArray = (HtmlTagBaseList)((DictionaryEntry)form[i]).Value;

					foreach (HtmlTagBase tag in controlArray)
					{
						if (tag is HtmlALinkTag)
						{
							HtmlALinkTag a=(HtmlALinkTag)tag;
							AddALinkNode(formNode, a);
						}

						if (tag is HtmlInputTag)
						{
							HtmlInputTag input=(HtmlInputTag)tag;
							AddInputNode(formNode,input);

						}

						if (tag is HtmlButtonTag)
						{
							HtmlButtonTag button = (HtmlButtonTag)tag;
							AddButtonNode(formNode,button);
						}

						if (tag is HtmlSelectTag)
						{
							HtmlSelectTag select = (HtmlSelectTag)tag;
							AddSelectNode(formNode,select);
						}
					
						if (tag is HtmlTextAreaTag)
						{
							HtmlTextAreaTag textarea=(HtmlTextAreaTag)tag;
							AddTextAreaNode(formNode,textarea);
						}
					}
				}
			}

			if ( tree.Nodes.Count == 1)
			tree.ExpandAll();
		}

		/// <summary>
		/// Adds a select node to the tree.
		/// </summary>
		/// <param name="node"> The FormEditorNode.</param>
		/// <param name="select"> The HtmlSelectTag.</param>
		private void AddSelectNode(FormEditorNode node,HtmlSelectTag select)
		{
			string label;
			label = "<select ";
			label +=" name="+ select.Name;
			if ( select.Multiple )
			{
				label += " multiple";
			}
			label +="/>";

			tree.AddSelect(node,label,select);
		}
		/// <summary>
		/// Adds a button node to the tree.
		/// </summary>
		/// <param name="node"> The FormEditorNode.</param>
		/// <param name="button"> The HtmlButtonTag.</param>
		private void AddButtonNode(FormEditorNode node, HtmlButtonTag button)
		{
			string label;
			label = "<button ";
			label +=" name="+ button.Name;
			label +=" value="+ button.Value;
			label +="/>";

			//tree.AddButton(node,button);
		}
		
		/// <summary>
		/// Adds a anchor node.
		/// </summary>
		/// <param name="node"> The FormEditorNode.</param>
		/// <param name="a"> The HtmlALinkTag.</param>
		private void AddALinkNode(FormEditorNode node, HtmlALinkTag a)
		{
			
			if ( a.HRef.IndexOf("javascript") > -1 )
			{
				string label;

				label = "<a ";
				label +=" href="+ a.HRef;
				label +=" id="+ a.Id;
				label +=" onclick="+ a.OnClick;
				label +="/>";

				tree.AddALink(node,label,a);
			}
		}

		/// <summary>
		/// Adds a input node.
		/// </summary>
		/// <param name="node"> The FormEditorNode.</param>
		/// <param name="input"> The HtmlInputTag.</param>
		private void AddInputNode(FormEditorNode node, HtmlInputTag input)
		{
			string label;
			label = "<input ";
			label +=" type="+ input.Type;
			label +=" name="+ input.Name;
			label +=" value="+ input.Value;
			label +="/>";

			tree.AddInput(node,label,input);
		}
		/// <summary>
		/// Adds a textarea node.
		/// </summary>
		/// <param name="node"> The FormEditorNode.</param>
		/// <param name="textarea"> The HtmlTextAreaTag.</param>
		private void AddTextAreaNode(FormEditorNode node,HtmlTextAreaTag textarea)
		{
			string label;
			label = "<textarea ";
			label +=" name="+ textarea.Name;
			label +=">";
			label +=textarea.Value;
			label +="</textarea>";

			tree.AddTextArea(node,label,textarea);
		}
		#endregion
		#region Methods

		/// <summary>
		/// Displays the form properties.
		/// </summary>
		private void FormProperties()
		{			
			HtmlFormTag formtag = tree.SelectedNode.BaseHtmlTag as HtmlFormTag;

			FormPageSettingsDialog formSettings = new FormPageSettingsDialog();

			formSettings.Action = formtag.Action;
			formSettings.Enctype = formtag.Enctype;
			formSettings.Method = formtag.Method;

			if ( formSettings.ShowDialog() == DialogResult.OK )
			{
				// Get the form tag and send				
				formtag.Action = formSettings.Action;
				formtag.Enctype = formSettings.Enctype;
				formtag.Method = formSettings.Method;

				StringBuilder label = new StringBuilder();
				// add Form node
				label.Append("<form name=");
				label.Append(formtag.Name);
				label.Append(" method=");
				label.Append(formtag.Method);
				label.Append(" action=");
				label.Append(formtag.Action);
				label.Append(" enctype=");
				label.Append(formtag.Enctype);

				if ( formtag.OnSubmit != null )
				{
					if ( formtag.OnSubmit.Length > 0 )
					{
						label.Append(" onsubmit=");
						label.Append(formtag.OnSubmit);
					}
				}
				label.Append(">");

				tree.SelectedNode.Text = label.ToString();
			}
		}
		#endregion

		#region Menus
		/// <summary>
		/// Changes the action.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuChangeAction_Click(object sender, System.EventArgs e)
		{
			FormProperties();
		}


		/// <summary>
		/// Submit form.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			// Get the form tag and send
			HtmlFormTag formtag = tree.SelectedNode.BaseHtmlTag as HtmlFormTag;

			// post form
			RequestPostEventArgs args = new RequestPostEventArgs();
			args.Method=formtag.Method;
			args.Form=formtag.CloneTag();
			RequestPostEvent(this,args);		
		}

		/// <summary>
		/// Submit form as a POST.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuSubmitPost_Click(object sender, System.EventArgs e)
		{
			//1: Get the form tag and send
			HtmlFormTag formtag=tree.SelectedNode.BaseHtmlTag as HtmlFormTag;

			RequestPostEventArgs args = new RequestPostEventArgs();
			args.Form = formtag.CloneTag();
			args.Method="post";
			RequestPostEvent(this,args);		
		}

		/// <summary>
		/// Submit form as a GET.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuSubmitGet_Click(object sender, System.EventArgs e)
		{
			//1: Get the form tag and send
			HtmlFormTag formtag=tree.SelectedNode.BaseHtmlTag as HtmlFormTag;

			RequestPostEventArgs args = new RequestPostEventArgs();
			args.Method="get";
			args.Form = formtag.CloneTag();
			RequestPostEvent(this,args);		
		}

//		private void mnuApplyTest_Click(object sender, System.EventArgs e)
//		{
//			Get the form tag and send
//			HtmlFormTag formtag=tree.SelectedNode.BaseHtmlTag as HtmlFormTag;
//			UnitTestManagerForm manager = new UnitTestManagerForm(formtag.CloneTag(),this.SessionData);
//
//			manager.CurrentUnitTestSession = this.CurrentUnitTestSession;
//
//			manager.ShowDialog();
//			UpdateApplyTestMenus();
//			manager.Close();
//		}
//		private void UpdateCloseMenus()
//		{
//			// new Arguments
//			ApplyMenuSettingsEventArgs newArgs = new ApplyMenuSettingsEventArgs();
//	
//			// identify the shortcut
//			newArgs.RootShortcut=Shortcut.CtrlShiftF;		
//
//			// get menu item by index
//			Ecyware.GreenBlue.Controls.MenuItem saveMenu = this.PluginMenus.GetByIndex(1);
//			// get menu item by index
//			Ecyware.GreenBlue.Controls.MenuItem runMenu = this.PluginMenus.GetByIndex(2);
//			// get menu item by index
//			Ecyware.GreenBlue.Controls.MenuItem reportMenu = this.PluginMenus.GetByIndex(3);
//
//			saveMenu.Enabled=false;
//			runMenu.Enabled=false;
//			reportMenu.Enabled=false;
//			newArgs.MenuItems.Add(saveMenu.Name,saveMenu);
//			newArgs.MenuItems.Add(runMenu.Name,runMenu);
//			newArgs.MenuItems.Add(reportMenu.Name,reportMenu);
//
//			// update menu
//			this.ApplyMenuSettingsEvent(this,newArgs);
//
//		}
//
//		/// <summary>
//		/// Toggles the RunTest menu.
//		/// </summary>
//		/// <param name="enabled"></param>
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
//			runMenu.Enabled=enabled;
//			newArgs.MenuItems.Add(runMenu.Name,runMenu);
//
//			// update menu
//			this.ApplyMenuSettingsEvent(this,newArgs);
//		}
//


	
		#endregion
		#region JavaScript methods
//		private void mnuRunClientSideScript_Click(object sender, System.EventArgs e)
//		{
//			if ( tree.SelectedNode.BaseHtmlTag is HtmlALinkTag )
//			{
//				HtmlALinkTag a = (HtmlALinkTag)tree.SelectedNode.BaseHtmlTag;
//
//				if ( a.HRef.ToLower().IndexOf("postback") > -1 )
//				{
//					// run doPostBack
//					AspNetDoPostBack(a.HRef.Replace("javascript:",""));
//				}
//			}
//		}

//		private void AspNetDoPostBack(string doPostBackValue)
//		{
//			// Get the form tag and send
//			HtmlFormTag formtag=((FormEditorNode)tree.SelectedNode.Parent).BaseHtmlTag as HtmlFormTag;
//
//
//			// parse
//			int startParens = doPostBackValue.IndexOf("(");
//			startParens++;
//			int endParens = doPostBackValue.IndexOf(")");
//
//
//			string[] postBackArgs = doPostBackValue.Substring(startParens,endParens-startParens).Split(',');
//			string[] temp = postBackArgs[0].Split('$');
//
//			StringBuilder eventTarget = new StringBuilder();
//
//			foreach (string s in temp)
//			{
//				eventTarget.Append(s.Trim('\''));
//				eventTarget.Append(":");
//			}
//
//			// remove last :
//			eventTarget.Remove(eventTarget.Length-1,1);
//
//			string eventArgument = postBackArgs[1].Trim('\'');
//
//			// set values
//			SetInputTextareaValue(formtag,"input","__EVENTTARGET",eventTarget.ToString());
//			SetInputTextareaValue(formtag,"input","__EVENTARGUMENT",eventArgument);
//
//			// post form
//			RequestPostEventArgs args = new RequestPostEventArgs();
//			args.Method="POST";
//			args.Form=formtag.CloneTag();
//			RequestPostEvent(this,args);
//		}
#endregion

		private void SetInputTextareaValue(HtmlFormTag form, string tagName, string name, string value)
		{
			for (int i=0;i<form.Count;i++)
			{
				FormEditorNode child = new FormEditorNode();
				HtmlTagBaseList controlArray = (HtmlTagBaseList)((DictionaryEntry)form[i]).Value;
				int controlIndex = 0;

				#region inner loop
				foreach (HtmlTagBase tag in controlArray)
				{
					// Input tags
					if ( (tag is HtmlInputTag) && (tagName.ToLower() == "input") )
					{
						HtmlInputTag input=(HtmlInputTag)tag;
						if ( input.Name == name )
						{
							input.Value = value;
							return;
						}						
					}
					
					// Textarea tags
					if ( (tag is HtmlTextAreaTag) && (tagName.ToLower()=="textarea") )
					{
						HtmlTextAreaTag textarea=(HtmlTextAreaTag)tag;

						if ( textarea.Name == name )
						{
							textarea.Value = value;
							return;
						}						
					}

					controlIndex++;
				}
				#endregion
			}
		}

		public override void Close()
		{
			this.tree.Clear();
			base.Close ();
		}



		
		#region Quick Test Generation Methods
		/// <summary>
		/// Displays the Buffer Overflow Dialog.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuApplyBO_Click(object sender, System.EventArgs e)
		{			
			BufferOverflowGeneratorDialog boDialog = new BufferOverflowGeneratorDialog();

			if ( boDialog.ShowDialog() == DialogResult.OK )
			{
				SetHtmlTagValue(tree.SelectedNode,boDialog.SelectedValue);
			}
			boDialog.Close();
		}
		
		/// <summary>
		/// Displays the XSS Test Dialog.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuApplyXSS_Click(object sender, System.EventArgs e)
		{
			XssTestDialog xssTestDialog = new XssTestDialog(this.InspectorConfig);

			if ( xssTestDialog.ShowDialog() == DialogResult.OK ) 
			{
				SetHtmlTagValue(tree.SelectedNode,xssTestDialog.SelectedValue);
			}

			xssTestDialog.Close();		
		}

		/// <summary>
		/// Displays the SQL Injection Dialog.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mnuApplySQL_Click(object sender, System.EventArgs e)
		{
			SqlTestDialog sqlTestDialog = new SqlTestDialog(this.InspectorConfig);
			
			if ( sqlTestDialog.ShowDialog() == DialogResult.OK )
			{
				SetHtmlTagValue(tree.SelectedNode,sqlTestDialog.SelectedValue);
			}

			sqlTestDialog.Close();		
		}



		/// <summary>
		/// Sets a value for a html tag.
		/// </summary>
		/// <param name="selectedNode"></param>
		/// <param name="value"></param>
		private void SetHtmlTagValue(FormEditorNode selectedNode, string value)
		{
			// Get the tag, label and control
			HtmlTagBase tag = selectedNode.BaseHtmlTag;
			Label label = ((FormEditorNode)selectedNode.Nodes[0]).LabelControl;
			Control control = ((FormEditorNode)selectedNode.Nodes[0]).NodeControl;

			if ( tag is HtmlInputTag )
			{
				((HtmlInputTag)tag).Value  = value;
				((TextBox)control).Text = value;
				label.Text = value;
			}
			if ( tag is HtmlSelectTag )
			{
				HtmlSelectTag selectTag = (HtmlSelectTag)tag;
				ListBox wfSelect = (ListBox)control;

				// set values to Windows Form Listbox					
				for (int i=0;i<wfSelect.SelectedIndices.Count;i++ )
				{
					int index = wfSelect.SelectedIndices[i];
					wfSelect.Items[index] = value;
					selectTag.UpdateOptionValue(index,value);
				}
			
				label.Text = value;
			}
			if ( tag is HtmlButtonTag )
			{
				((HtmlButtonTag)tag).Value = value;
				((TextBox)control).Text = value;
				label.Text = value;
			}
			if ( tag is HtmlTextAreaTag )
			{
				((HtmlTextAreaTag)tag).Value = value;
				((TextBox)control).Text = value;
				label.Text = value;
			}
		}

		#endregion

	}
}

