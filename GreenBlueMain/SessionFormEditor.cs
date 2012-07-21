// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: June 2004 - July 2004
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Text;
using Ecyware.GreenBlue.Controls;
using Ecyware.GreenBlue.HtmlCommand;
using Ecyware.GreenBlue.HtmlDom;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Contains the definition for the SessionFormEditor control.
	/// </summary>
	public class SessionFormEditor : BaseSessionDesignerUserControl
	{
		private HtmlFormTag form = null;

		internal event UpdateSessionRequestEventHandler UpdateSessionRequestEvent;
		private Ecyware.GreenBlue.Controls.FormEditor formEditor;
		public System.Windows.Forms.ContextMenu mnuFormParent;
		private System.Windows.Forms.ContextMenu mnuFormChild;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new SessionFormEditor.
		/// </summary>
		public SessionFormEditor()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		}
		/// <summary>
		/// Creates a new SessionFormEditor.
		/// </summary>
		/// <param name="form"> Form collection to load.</param>
		public SessionFormEditor(HtmlFormTag form) : this()
		{
			if ( form != null )
			{
				HtmlFormTagCollection forms = new HtmlFormTagCollection(1);
				forms.Add(form.Name, form);

				// Load tree
				LoadFormTree(forms);
			}
		}

		/// <summary>
		/// Gets or sets the form tag.
		/// </summary>
		public HtmlFormTag Form
		{
			get
			{
				return form;
			}
			set
			{
				form = value;
			}
		}
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
			this.formEditor = new Ecyware.GreenBlue.Controls.FormEditor();
			this.mnuFormParent = new System.Windows.Forms.ContextMenu();
			this.mnuFormChild = new System.Windows.Forms.ContextMenu();
			this.SuspendLayout();
			// 
			// formEditor
			// 
			this.formEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.formEditor.ImageIndex = -1;
			this.formEditor.ItemHeight = 20;
			this.formEditor.Location = new System.Drawing.Point(0, 0);
			this.formEditor.Name = "formEditor";
			this.formEditor.SelectedImageIndex = -1;
			this.formEditor.SelectedNode = null;
			this.formEditor.Size = new System.Drawing.Size(582, 408);
			this.formEditor.TabIndex = 0;
			this.formEditor.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.FormEditor_AfterSelect);
			// 
			// SessionFormEditor
			// 
			this.Controls.Add(this.formEditor);
			this.Name = "SessionFormEditor";
			this.Size = new System.Drawing.Size(582, 408);
			this.ResumeLayout(false);

		}
		#endregion
		/// <summary>
		/// Displays a message indicating that no data could be showed.
		/// </summary>
		public void DisplayNoDataMessage()
		{
			formEditor.Clear();

			FormEditorNode node = new FormEditorNode();
			node.Text = "No data available for display";
			formEditor.Nodes.Add(node);
		}
		private void FormEditor_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			// check for any form editor node with HtmlFormTag
			if ( formEditor.SelectedNode.BaseHtmlTag is HtmlFormTag )
			{
				formEditor.ContextMenu=mnuFormParent;
			} 
			else 
			{
				if ( !(formEditor.SelectedNode.BaseHtmlTag is HtmlFormTag) )
				{
					formEditor.ContextMenu=this.mnuFormChild;
				} 
				else 
				{
					formEditor.ContextMenu=null;
				}
			}		
		}

		/// <summary>
		/// Updates the session request data.
		/// </summary>
		public override void UpdateSessionRequestData()
		{
			UpdateSessionRequestEventArgs args = new UpdateSessionRequestEventArgs();
			
			args.UpdateType = UpdateSessionRequestType.Form;
			args.Form = this.Form;

			this.UpdateSessionRequestEvent(this, args);
		}
	}
}
