// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Ecyware.GreenBlue.Controls;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Engine.HtmlCommand;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.Scripting;

namespace Ecyware.GreenBlue.Controls.Scripting
{
	/// <summary>
	/// Contains the definition for the ScriptingMainPage control.
	/// </summary>
	public class FormPage : BaseScriptingDataPage
	{
		private Uri _currentRequestUri;
		private HtmlFormTag _form = null;
		private System.Windows.Forms.GroupBox grpInfo;
		private Ecyware.GreenBlue.Controls.FormEditor formEditor;
		private System.Windows.Forms.ContextMenu mnuForm;
		private System.Windows.Forms.MenuItem mnuCreateFormFromQueryString;
		private System.Windows.Forms.MenuItem mnuFormProperties;
		private System.Windows.Forms.MenuItem mnuChangeFormField;
		private System.Windows.Forms.MenuItem mnuText;
		private System.Windows.Forms.MenuItem mnuFile;
		private System.Windows.Forms.MenuItem mnuHidden;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new SessionRequestHeaderEditor
		/// </summary>
		public FormPage()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(FormPage));
			this.grpInfo = new System.Windows.Forms.GroupBox();
			this.formEditor = new Ecyware.GreenBlue.Controls.FormEditor();
			this.mnuForm = new System.Windows.Forms.ContextMenu();
			this.mnuCreateFormFromQueryString = new System.Windows.Forms.MenuItem();
			this.mnuFormProperties = new System.Windows.Forms.MenuItem();
			this.mnuChangeFormField = new System.Windows.Forms.MenuItem();
			this.mnuText = new System.Windows.Forms.MenuItem();
			this.mnuFile = new System.Windows.Forms.MenuItem();
			this.mnuHidden = new System.Windows.Forms.MenuItem();
			this.grpInfo.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpInfo
			// 
			this.grpInfo.Controls.Add(this.formEditor);
			this.grpInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpInfo.Location = new System.Drawing.Point(0, 0);
			this.grpInfo.Name = "grpInfo";
			this.grpInfo.Size = new System.Drawing.Size(600, 400);
			this.grpInfo.TabIndex = 1;
			this.grpInfo.TabStop = false;
			this.grpInfo.Text = "Form";
			// 
			// formEditor
			// 
			this.formEditor.ContextMenu = this.mnuForm;
			this.formEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.formEditor.ImageIndex = -1;
			this.formEditor.ItemHeight = 20;
			this.formEditor.Location = new System.Drawing.Point(3, 16);
			this.formEditor.Name = "formEditor";
			this.formEditor.SelectedImageIndex = -1;
			this.formEditor.SelectedNode = null;
			this.formEditor.Size = new System.Drawing.Size(594, 381);
			this.formEditor.TabIndex = 1;
			// 
			// mnuForm
			// 
			this.mnuForm.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					this.mnuCreateFormFromQueryString,
																					this.mnuFormProperties,
																					this.mnuChangeFormField});
			this.mnuForm.Popup += new System.EventHandler(this.mnuForm_Popup);
			// 
			// mnuCreateFormFromQueryString
			// 
			this.mnuCreateFormFromQueryString.Index = 0;
			this.mnuCreateFormFromQueryString.Text = "Create Form from URL QueryString";
			this.mnuCreateFormFromQueryString.Click += new System.EventHandler(this.mnuCreateFormFromQueryString_Click);
			// 
			// mnuFormProperties
			// 
			this.mnuFormProperties.Index = 1;
			this.mnuFormProperties.Text = "Form Properties...";
			this.mnuFormProperties.Click += new System.EventHandler(this.mnuChangeAction_Click);
			// 
			// mnuChangeFormField
			// 
			this.mnuChangeFormField.Index = 2;
			this.mnuChangeFormField.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							   this.mnuText,
																							   this.mnuFile,
																							   this.mnuHidden});
			this.mnuChangeFormField.Text = "&Change Form Field Type";
			this.mnuChangeFormField.Visible = false;
			// 
			// mnuText
			// 
			this.mnuText.Index = 0;
			this.mnuText.Text = "&Text";
			this.mnuText.Click += new System.EventHandler(this.mnuText_Click);
			// 
			// mnuFile
			// 
			this.mnuFile.Index = 1;
			this.mnuFile.Text = "&File";
			this.mnuFile.Click += new System.EventHandler(this.mnuFile_Click);
			// 
			// mnuHidden
			// 
			this.mnuHidden.Index = 2;
			this.mnuHidden.Text = "&Hidden";
			this.mnuHidden.Click += new System.EventHandler(this.mnuHidden_Click);
			// 
			// FormPage
			// 
			this.Controls.Add(this.grpInfo);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "FormPage";
			this.Size = new System.Drawing.Size(600, 400);
			this.grpInfo.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		#region FormTree Building methods

		/// <summary>
		/// Loads the forms into the form tree.
		/// </summary>
		/// <param name="forms"></param>
		public void LoadFormTree(HtmlFormTagCollection forms)
		{
			formEditor.Clear();

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

				FormEditorNode formNode = formEditor.AddFormNode(label.ToString(),form);
				
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

			formEditor.ExpandAll();
		}

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

			formEditor.AddSelect(node,label,select);
		}
		private void AddButtonNode(FormEditorNode node, HtmlButtonTag button)
		{
			string label;
			label = "<button ";
			label +=" name="+ button.Name;
			label +=" value="+ button.Value;
			label +="/>";

			//tree.AddButton(node,button);
		}
		
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

				formEditor.AddALink(node,label,a);
			}
		}

		private void AddInputNode(FormEditorNode node, HtmlInputTag input)
		{
			string label;
			label = "<input ";
			label +=" type="+ input.Type;
			label +=" name="+ input.Name;
			label +=" value="+ input.Value;
			label +="/>";

			formEditor.AddInput(node,label,input);
		}
		private void AddTextAreaNode(FormEditorNode node,HtmlTextAreaTag textarea)
		{
			string label;
			label = "<textarea ";
			label +=" name="+ textarea.Name;
			label +=">";
			label +=textarea.Value;
			label +="</textarea>";

			formEditor.AddTextArea(node,label,textarea);
		}
		#endregion

		/// <summary>
		/// Displays a message indicating that no data could be showed.
		/// </summary>
		public void DisplayNoDataMessage()
		{
			_form = null;
			formEditor.Clear();

			FormEditorNode node = new FormEditorNode();
			node.Text = "No data available for display.";
			formEditor.Nodes.Add(node);
		}

		/// <summary>
		/// Loads the request.
		/// </summary>
		/// <param name="index"> The current request index.</param>
		/// <param name="scripting"> The scripting application.</param>
		/// <param name="request"> The current web request.</param>
		public override void LoadRequest(int index, ScriptingApplication scripting ,Ecyware.GreenBlue.Engine.Scripting.WebRequest request)
		{
			base.LoadRequest (index, scripting, request);

			try
			{
				//_httpRequestType = request.RequestType;
				_currentRequestUri = new Uri(request.Url);

				if ( request.Form.Elements.Length > 0 )
				{				
					HtmlFormTagCollection forms = new HtmlFormTagCollection(1);
					_form = request.Form.WriteHtmlFormTag();
					forms.Add(request.Form.Name, _form);

					// Load tree
					LoadFormTree(forms);
				} 
				else 
				{
					DisplayNoDataMessage();
				}
			}
			catch ( Exception ex )
			{
				MessageBox.Show(ex.ToString(), AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		/// <summary>
		/// Gets or sets the WebRequest.
		/// </summary>
		public override Ecyware.GreenBlue.Engine.Scripting.WebRequest WebRequest
		{
			get
			{
				if ( base.WebRequest != null )
				{
					UpdateForm(base.WebRequest);
					//UpdateHttpPropertiesFromPropertyGrid(base.WebRequest);
				}

				return base.WebRequest;
			}
		}


		private void UpdateForm(Ecyware.GreenBlue.Engine.Scripting.WebRequest request)
		{			
			if ( _form != null )
			{
				request.ClearForm();
				request.Form.ReadHtmlFormTag(_form);
			}
		}

		private void mnuChangeAction_Click(object sender, System.EventArgs e)
		{
			FormProperties();
		}

		/// <summary>
		/// Displays the form properties.
		/// </summary>
		private void FormProperties()
		{
			HtmlFormTag formtag = formEditor.SelectedNode.BaseHtmlTag as HtmlFormTag;

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

				formEditor.SelectedNode.Text = label.ToString();
			}
		}

		private void mnuCreateFormFromQueryString_Click(object sender, System.EventArgs e)
		{
			if ( _currentRequestUri.Query.Length > 0 )
			{
				HtmlFormTag formTag = CreateFormFromQueryString(_currentRequestUri);

				base.WebRequest.Form.ReadHtmlFormTag(formTag);
				HtmlFormTagCollection collection = new HtmlFormTagCollection();
				collection.Add(formTag.Name, formTag);
				LoadFormTree(collection);
			}
			else 
			{
				MessageBox.Show("No query string found.", AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}


		/// <summary>
		/// Creates a form from a query string.
		/// </summary>
		/// <param name="url"> The complete url.</param>
		/// <returns> A HtmlFormTag.</returns>
		private HtmlFormTag CreateFormFromQueryString(Uri url)
		{
			string action = url.Scheme + "://" + url.Host + url.AbsolutePath;
			FormConverter converter = new FormConverter();
			PostDataCollection postData = converter.GetPostDataCollection(url.Query.TrimStart('?'));
			
			HtmlFormTag formTag = new HtmlFormTag();
			formTag.Action = action;
			formTag.Name = "docform";
			formTag.Method = base.WebRequest.RequestType.ToString();

			foreach ( string key in postData.Keys )
			{
				ArrayList items = postData[key];

				HtmlTagBaseList list = new HtmlTagBaseList();
				formTag.Add(key, list);
				foreach ( string value in items )
				{
					HtmlInputTag hiddenField = new HtmlInputTag();
					hiddenField.Type = HtmlInputType.Hidden;
					hiddenField.Name = key;
					hiddenField.Value = value.Trim().TrimEnd('\0');
					list.Add(hiddenField);
				}
			}
			

			return formTag;
		}

		private void mnuText_Click(object sender, System.EventArgs e)
		{			
			HtmlInputTag tag = (HtmlInputTag)formEditor.SelectedNode.BaseHtmlTag;
			tag.Type = HtmlInputType.Text;

			string label;
			label = "<input ";
			label +=" type="+ tag.Type;
			label +=" name="+ tag.Name;
			label +=" value="+ tag.Value;
			label +="/>";

			formEditor.SelectedNode.Text = label;
		}

		private void mnuFile_Click(object sender, System.EventArgs e)
		{
			HtmlInputTag tag = (HtmlInputTag)formEditor.SelectedNode.BaseHtmlTag;
			tag.Type = HtmlInputType.File;	

			string label;
			label = "<input ";
			label +=" type="+ tag.Type;
			label +=" name="+ tag.Name;
			label +=" value="+ tag.Value;
			label +="/>";

			formEditor.SelectedNode.Text = label;	
		}

		private void mnuHidden_Click(object sender, System.EventArgs e)
		{
			HtmlInputTag tag = (HtmlInputTag)formEditor.SelectedNode.BaseHtmlTag;
			tag.Type = HtmlInputType.Hidden;	
	
			string label;
			label = "<input ";
			label +=" type="+ tag.Type;
			label +=" name="+ tag.Name;
			label +=" value="+ tag.Value;
			label +="/>";

			formEditor.SelectedNode.Text = label;
		}

		private void mnuForm_Popup(object sender, System.EventArgs e)
		{
			if ( formEditor.SelectedNode.Parent == null )
			{
				this.mnuFormProperties.Visible = true;
				this.mnuCreateFormFromQueryString.Visible = true;
				this.mnuChangeFormField.Visible = false;
			} 
			else 
			{				
				if ( formEditor.SelectedNode.BaseHtmlTag is HtmlInputTag )
				{
					this.mnuChangeFormField.Visible = true;
				} 
				else 
				{
					this.mnuChangeFormField.Visible = false;
				}

				this.mnuFormProperties.Visible = false;
				this.mnuCreateFormFromQueryString.Visible = false;							
			}
		}
	}
}
