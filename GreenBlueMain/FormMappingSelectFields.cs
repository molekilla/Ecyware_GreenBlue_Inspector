using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using Ecyware.GreenBlue.HtmlDom;
using Ecyware.GreenBlue.Controls;
using Ecyware.GreenBlue.FormMapping;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Summary description for FormMappingSelectFields.
	/// </summary>
	public class FormMappingSelectFields : System.Windows.Forms.Form
	{
		private string _postData = string.Empty;
		private HtmlFormTag _selectedForm = null;
		private Uri _uri = null;
		private int dragDropNodeIndex = -1;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnContinue;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TreeView postDataNodes;
		private System.Windows.Forms.TreeView formNodes;


		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public FormMappingSelectFields()
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
			this.postDataNodes = new System.Windows.Forms.TreeView();
			this.formNodes = new System.Windows.Forms.TreeView();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.LightSteelBlue;
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(570, 30);
			this.label1.TabIndex = 0;
			this.label1.Text = "2:  Select and Order Fields to Map";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnContinue
			// 
			this.btnContinue.Location = new System.Drawing.Point(462, 276);
			this.btnContinue.Name = "btnContinue";
			this.btnContinue.Size = new System.Drawing.Size(96, 23);
			this.btnContinue.TabIndex = 1;
			this.btnContinue.Text = "Save Mapping";
			this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(372, 276);
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
			this.label3.Location = new System.Drawing.Point(288, 36);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(138, 12);
			this.label3.TabIndex = 6;
			this.label3.Text = "Forms Editor Tree";
			// 
			// postDataNodes
			// 
			this.postDataNodes.AllowDrop = true;
			this.postDataNodes.CheckBoxes = true;
			this.postDataNodes.HideSelection = false;
			this.postDataNodes.ImageIndex = -1;
			this.postDataNodes.Location = new System.Drawing.Point(12, 54);
			this.postDataNodes.Name = "postDataNodes";
			this.postDataNodes.SelectedImageIndex = -1;
			this.postDataNodes.Size = new System.Drawing.Size(270, 210);
			this.postDataNodes.TabIndex = 8;
			// 
			// formNodes
			// 
			this.formNodes.AllowDrop = true;
			this.formNodes.CheckBoxes = true;
			this.formNodes.HideSelection = false;
			this.formNodes.ImageIndex = -1;
			this.formNodes.Location = new System.Drawing.Point(288, 54);
			this.formNodes.Name = "formNodes";
			this.formNodes.SelectedImageIndex = -1;
			this.formNodes.Size = new System.Drawing.Size(270, 210);
			this.formNodes.TabIndex = 9;
			this.formNodes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.formNodes_MouseDown);
			this.formNodes.DragOver += new System.Windows.Forms.DragEventHandler(this.formNodes_DragOver);
			this.formNodes.DragDrop += new System.Windows.Forms.DragEventHandler(this.formNodes_DragDrop);
			// 
			// FormMappingSelectFields
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(570, 310);
			this.Controls.Add(this.formNodes);
			this.Controls.Add(this.postDataNodes);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnContinue);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMappingSelectFields";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form Mapping Wizard";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		public void LoadTrees()
		{
			// Load the Form Tree First
			#region Form Tree Loading
			TreeNode rootNode = new TreeNode();
			rootNode.Text = "Form Fields";

			this.formNodes.Nodes.Add(rootNode);

			// select form from FormCollection
			foreach ( DictionaryEntry de in this.SelectedForm )
			{
				HtmlTagBaseList controlArray = (HtmlTagBaseList)de.Value;

				foreach (HtmlTagBase tag in controlArray)
				{
					if (tag is HtmlInputTag)
					{
						HtmlInputTag input=(HtmlInputTag)tag;
						TreeNode newNode = new TreeNode();
						newNode.Text = input.Name + "=" + input.Value;
						newNode.Tag = input;

						rootNode.Nodes.Add(newNode);
					}

					if (tag is HtmlButtonTag)
					{
						HtmlButtonTag button = (HtmlButtonTag)tag;
						TreeNode newNode = new TreeNode();
						newNode.Text = button.Name + "=" + button.Value;
						newNode.Tag = button;

						rootNode.Nodes.Add(newNode);
					}

					if (tag is HtmlSelectTag)
					{
						HtmlSelectTag select = (HtmlSelectTag)tag;
						TreeNode newNode = new TreeNode();
						newNode.Text = select.Name + "=" + select.Value;
						newNode.Tag = select;

						rootNode.Nodes.Add(newNode);
					}
					
					if (tag is HtmlTextAreaTag)
					{
						HtmlTextAreaTag textarea=(HtmlTextAreaTag)tag;
						TreeNode newNode = new TreeNode();
						newNode.Text = textarea.Name + "=" + textarea.Value;
						newNode.Tag = textarea;

						rootNode.Nodes.Add(newNode);
					}
				}
			}
			#endregion

			#region PostData Tree Loading
			TreeNode postDataRoot = new TreeNode();
			postDataRoot.Text = "Post Data Fields";
			this.postDataNodes.Nodes.Add(postDataRoot);

			foreach ( string s in PostDataValue.Split('&') )
			{
				if ( s.Length > 0 )
					postDataRoot.Nodes.Add(s);
			}	
			#endregion

			formNodes.Nodes[0].Expand();
			postDataNodes.Nodes[0].Expand();
		}

		private void formNodes_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			// Current node
			TreeView tree = (TreeView)sender;
			TreeNode node = tree.GetNodeAt(e.X,e.Y);
			tree.SelectedNode = node;

			
			if ( node != null )
			{
				if ( node.Parent != null )
				{
					object[] args = new Object[] { node.Clone(), node.Index };
					tree.DoDragDrop(args,DragDropEffects.Copy);
				}
			}			
		}

		private void formNodes_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
		{
			TreeView tree = (TreeView)sender;
			e.Effect = DragDropEffects.None;
			
			if ( e.Data.GetData(typeof(object[])) != null )
			{
				Point pt = new Point(e.X,e.Y);
				pt = tree.PointToClient(pt);
				TreeNode node = tree.GetNodeAt(pt);

				if ( node != null )
				{
					e.Effect = DragDropEffects.Copy;					
					tree.SelectedNode = node;					
				}
			}
		}

		private void formNodes_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			TreeView tree = (TreeView)sender;
			
			Point pt = new Point(e.X,e.Y);
			pt = tree.PointToClient(pt);
			
			TreeNode cursorNode = tree.GetNodeAt(pt);

			if ( cursorNode.Parent != null )
			{
				object[] args = (object[])e.Data.GetData(typeof(object[]));

				TreeNode moveNode = (TreeNode)args[0];
				int dragIndex = (int)args[1];
			
				int index = cursorNode.Index;
				// int dragIndex = moveNode.Index;

				// Remove cursor node and then add moveNode
				if ( ( index + 1 ) == tree.Nodes[0].Nodes.Count )
				{
					tree.Nodes[0].Nodes.Add(moveNode);
					tree.Nodes[0].Nodes.RemoveAt(dragIndex);
				} 
				else
				{
					tree.Nodes[0].Nodes.Insert(index, moveNode);
					tree.Nodes[0].Nodes.RemoveAt(dragIndex+1);
				}						
			}
		}

		private void btnContinue_Click(object sender, System.EventArgs e)
		{
			// Create a new HtmlFormTag and
			// Copy all fields and add new field mapping as selected by user
			HtmlFormTag formTagClone = CloneFormTag();

			// PostData
			Hashtable postData = new Hashtable();
			foreach ( TreeNode node in postDataNodes.Nodes[0].Nodes )
			{
				string[] values = node.Text.Split('=');
				postData.Add(values[0],values[1]);
			}

			// Create relations
			FormMappingDataRelationList relationList = CreateFormMappingRelations(postData, formTagClone);

			FormMappingManager formMappingManager = new FormMappingManager();
			formMappingManager.SaveFormMapping(this.SiteUri, relationList, postData, formTagClone);

			this.Close();
		}

		private FormMappingDataRelationList CreateFormMappingRelations(Hashtable postData, HtmlFormTag formTag)
		{
			FormMappingDataRelationList relationList = new FormMappingDataRelationList();

			foreach ( DictionaryEntry de in formTag )
			{
				HtmlTagBaseList controlArray = (HtmlTagBaseList)de.Value;

				#region Tag Collection
				foreach ( HtmlTagBase tag in controlArray )
				{
					string name = string.Empty;

					if (tag is HtmlInputTag)
					{
						HtmlInputTag input=(HtmlInputTag)tag;
						name = input.Name;
					}

					if (tag is HtmlButtonTag)
					{
						HtmlButtonTag button = (HtmlButtonTag)tag;
						name = button.Name;
					}

					if (tag is HtmlSelectTag)
					{
						HtmlSelectTag select = (HtmlSelectTag)tag;
						name = select.Name;
					}
					
					if (tag is HtmlTextAreaTag)
					{
						HtmlTextAreaTag textarea=(HtmlTextAreaTag)tag;
						name = textarea.Name;
					}

					FormMappingDataRelation relation = new FormMappingDataRelation();
					relation.CurrentValue = (string)postData[name];
					relation.FieldName = name;

					relationList.Add(relation);
					//relation.FormMappingCopyType = FormMappingDataTransformation.
				}
				#endregion
			}

			return relationList;
		}
		private HtmlFormTag CloneFormTag()
		{
			HtmlFormTag formTag = new HtmlFormTag();

			formTag.Action = this.SelectedForm.Action;
			formTag.Class = this.SelectedForm.Class;
			formTag.Enctype = this.SelectedForm.Enctype;
			formTag.FormIndex = this.SelectedForm.FormIndex;
			formTag.Id = this.SelectedForm.Id;
			formTag.Method = this.SelectedForm.Method;
			formTag.Name = this.SelectedForm.Name;
			formTag.OnClick = this.SelectedForm.OnClick;
			formTag.OnSubmit = this.SelectedForm.OnSubmit;
			formTag.Style = this.SelectedForm.Style;
			formTag.Title = this.SelectedForm.Title;

			HtmlTagBaseList tagList = new HtmlTagBaseList();

			#region Add Tags
			foreach ( TreeNode node in formNodes.Nodes[0].Nodes )
			{
				if (node.Tag is HtmlInputTag)
				{
					HtmlInputTag input=(HtmlInputTag)node.Tag;
					tagList.Add(input);
				}

				if (node.Tag is HtmlButtonTag)
				{
					HtmlButtonTag button = (HtmlButtonTag)node.Tag;
					tagList.Add(button);
				}

				if (node.Tag is HtmlSelectTag)
				{
					HtmlSelectTag select = (HtmlSelectTag)node.Tag;
					tagList.Add(select);
				}
					
				if (node.Tag is HtmlTextAreaTag)
				{
					HtmlTextAreaTag textarea=(HtmlTextAreaTag)node.Tag;
					tagList.Add(textarea);
				}				
			}
			#endregion

			formTag.Add(formTag.Name, tagList);

			return formTag;
			
		}
		#region Properties
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

		#endregion
	}
}
