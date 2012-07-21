using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using Ecyware.GreenBlue.HtmlDom;
using Ecyware.GreenBlue.Controls;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Summary description for FormMappingSelectForm.
	/// </summary>
	public class FormMappingSelectForm : System.Windows.Forms.Form
	{
		private string _postData = string.Empty;
		private HtmlFormTagCollection _formTagCollection = null;
		private HtmlFormTag _selectedForm = null;
		private FormEditorNode tempSelectedNode = null;
		private Uri _uri=null;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnContinue;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private Ecyware.GreenBlue.Controls.FormEditor formsTree;
		private System.Windows.Forms.TreeView postDataTree;
		private System.Windows.Forms.ContextMenu mnuFormNodeSelection;
		private System.Windows.Forms.MenuItem mnuSelectForm;
		private System.Windows.Forms.ErrorProvider errorProvider;


		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormMappingSelectForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.label1 = new System.Windows.Forms.Label();
			this.btnContinue = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.formsTree = new Ecyware.GreenBlue.Controls.FormEditor();
			this.postDataTree = new System.Windows.Forms.TreeView();
			this.mnuFormNodeSelection = new System.Windows.Forms.ContextMenu();
			this.mnuSelectForm = new System.Windows.Forms.MenuItem();
			this.errorProvider = new System.Windows.Forms.ErrorProvider();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.LightSteelBlue;
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(420, 30);
			this.label1.TabIndex = 0;
			this.label1.Text = "1:  Select Form to Map";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnContinue
			// 
			this.btnContinue.Location = new System.Drawing.Point(324, 330);
			this.btnContinue.Name = "btnContinue";
			this.btnContinue.TabIndex = 1;
			this.btnContinue.Text = "Continue";
			this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(240, 330);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 36);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(126, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "Web Browser PostData";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 168);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(138, 12);
			this.label3.TabIndex = 6;
			this.label3.Text = "Forms Editor Tree";
			// 
			// formsTree
			// 
			this.formsTree.ImageIndex = -1;
			this.formsTree.ItemHeight = 20;
			this.formsTree.Location = new System.Drawing.Point(12, 186);
			this.formsTree.Name = "formsTree";
			this.formsTree.SelectedImageIndex = -1;
			this.formsTree.SelectedNode = null;
			this.formsTree.Size = new System.Drawing.Size(390, 138);
			this.formsTree.TabIndex = 7;
			this.formsTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.formsTree_AfterSelect);
			// 
			// postDataTree
			// 
			this.postDataTree.ImageIndex = -1;
			this.postDataTree.Location = new System.Drawing.Point(12, 54);
			this.postDataTree.Name = "postDataTree";
			this.postDataTree.SelectedImageIndex = -1;
			this.postDataTree.Size = new System.Drawing.Size(390, 108);
			this.postDataTree.TabIndex = 8;
			// 
			// mnuFormNodeSelection
			// 
			this.mnuFormNodeSelection.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																								 this.mnuSelectForm});
			// 
			// mnuSelectForm
			// 
			this.mnuSelectForm.Index = 0;
			this.mnuSelectForm.Text = "&Select Form";
			this.mnuSelectForm.Click += new System.EventHandler(this.mnuSelectForm_Click);
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// FormMappingSelectForm
			// 
			this.AcceptButton = this.btnContinue;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(420, 364);
			this.Controls.Add(this.postDataTree);
			this.Controls.Add(this.formsTree);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnContinue);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMappingSelectForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form Mapping Wizard";
			this.Load += new System.EventHandler(this.FormMappingSelectForm_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void FormMappingSelectForm_Load(object sender, System.EventArgs e)
		{
			//StringBuilder postData = new StringBuilder();
			
			TreeNode rootNode = new TreeNode();
			rootNode.Text = "Post Data";
			postDataTree.Nodes.Add(rootNode);

			foreach ( string s in PostDataValue.Split('&') )
			{
				rootNode.Nodes.Add(s);
			}	
			//this.txtPostData.Text = postData.ToString();
			LoadFormTree(this.FormCollection);
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		#region FormTree Building methods
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
				if ( form.OnSubmit.Length > 0 )
				{
					label.Append(" onsubmit=");
					label.Append(form.OnSubmit);
				}
				label.Append(">");

				FormEditorNode formNode = formsTree.AddFormNode(label.ToString(),form);
				
				foreach (DictionaryEntry dd in form)
				{
					FormEditorNode child = new FormEditorNode();
					HtmlTagBaseList controlArray = (HtmlTagBaseList)dd.Value;

					foreach (HtmlTagBase tag in controlArray)
					{
//						if (tag is HtmlALinkTag)
//						{
//							HtmlALinkTag a=(HtmlALinkTag)tag;
//							AddALinkNode(formNode, a);
//						}

						if (tag is HtmlInputTag)
						{
							HtmlInputTag input=(HtmlInputTag)tag;
							FormEditorNode node = new FormEditorNode();
							node.Text = input.Name + "=" + input.Value;
							formNode.Nodes.Add(node);
							//AddInputNode(formNode,input);

						}

						if (tag is HtmlButtonTag)
						{
							HtmlButtonTag button = (HtmlButtonTag)tag;
							FormEditorNode node = new FormEditorNode();
							node.Text = button.Name + "=" + button.Value;
							formNode.Nodes.Add(node);
							//AddButtonNode(formNode,button);
						}

						if (tag is HtmlSelectTag)
						{
							HtmlSelectTag select = (HtmlSelectTag)tag;
							FormEditorNode node = new FormEditorNode();
							node.Text = select.Name + "=" + select.Value;
							formNode.Nodes.Add(node);
							//AddSelectNode(formNode,select);
						}
					
						if (tag is HtmlTextAreaTag)
						{
							HtmlTextAreaTag textarea=(HtmlTextAreaTag)tag;
							FormEditorNode node = new FormEditorNode();
							node.Text = textarea.Name + "=" + textarea.Value;
							formNode.Nodes.Add(node);
							//AddTextAreaNode(formNode,textarea);
						}
					}
				}
			}
		}


		#endregion

		private bool ValidateForm()
		{
			bool b = true;

			if ( this.SelectedForm == null )
			{
				errorProvider.SetIconAlignment(this.formsTree, ErrorIconAlignment.TopRight);
				errorProvider.SetError(this.formsTree," Please select a form before continuing.");
				b = false;
			} 
			else 
			{
				errorProvider.SetError(this.formsTree,"");
			}
			return b;
		}

		private void btnContinue_Click(object sender, System.EventArgs e)
		{
			if ( ValidateForm() )
			{
				FormMappingSelectFields formMappingWizard2 = new FormMappingSelectFields();
				formMappingWizard2.PostDataValue = this.PostDataValue;
				formMappingWizard2.SiteUri = this.SiteUri;
				formMappingWizard2.SelectedForm = this.SelectedForm.CloneTag();

				this.Hide();
				formMappingWizard2.LoadTrees();
				formMappingWizard2.Show();
				this.Close();
			}
		}

		private void formsTree_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			// check for any form editor node with HtmlFormTag
			if ( formsTree.SelectedNode.BaseHtmlTag is HtmlFormTag )
			{
				formsTree.ContextMenu=mnuFormNodeSelection;
			} 
			else 
			{				
				formsTree.ContextMenu=null;
			}		
		}

		private void mnuSelectForm_Click(object sender, System.EventArgs e)
		{
			if ( tempSelectedNode != null )
			{
				// revert saved selection
				tempSelectedNode.BackColor = formsTree.BackColor;
			}

			formsTree.SelectedNode.BackColor = Color.Yellow;
			tempSelectedNode = formsTree.SelectedNode;
			this.SelectedForm = ((HtmlFormTag)formsTree.SelectedNode.BaseHtmlTag);
		}
		#region Properties
		public Uri SiteUri
		{
			get
			{
				return _uri;
			}
			set
			{
				_uri = value;
			}
		}

		public HtmlFormTag SelectedForm
		{
			get
			{
				return _selectedForm;
			}
			set
			{
				_selectedForm = value;
			}
		}
		public string PostDataValue
		{
			get
			{
				return _postData;
			}
			set
			{
				_postData = value;
			}
		}

		public HtmlFormTagCollection FormCollection
		{
			get
			{
				return _formTagCollection;
			}
			set
			{
				_formTagCollection = value;
			}
		}

		#endregion
	}
}
