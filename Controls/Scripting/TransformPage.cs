// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using Ecyware.GreenBlue.Controls;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Engine.HtmlCommand;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms;
using Ecyware.GreenBlue.Engine.Transforms.Designers;


namespace Ecyware.GreenBlue.Controls.Scripting
{
	/// <summary>
	/// Summary description for TransformPage.
	/// </summary>
	public class TransformPage : BaseScriptingDataPage
	{		
		TransformDocumentationControl transformMainPage = new TransformDocumentationControl();
		static WebTransform _copyObject = null;
		UITransformEditorManager manager;
		private UITransformEditor[] _editors;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Splitter splitter1;
		protected System.Windows.Forms.Panel pnTransformEditor;
		protected System.Windows.Forms.TreeView tvTransforms;
		protected System.Windows.Forms.ContextMenu mnuInputMenu;
		protected System.Windows.Forms.ContextMenu mnuOutputMenu;
		private System.Windows.Forms.ImageList imageList1;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Creates a TransformPage.
		/// </summary>
		public TransformPage()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();


			if ( !this.DesignMode )
			{
				manager = new UITransformEditorManager();
				ArrayList list = new ArrayList();
				list.AddRange(manager.LoadTransformEditors());
			
				foreach ( UITransformEditor editor in list )
				{
					editor.Dock = DockStyle.Fill;
					editor.Visible = false;
					pnTransformEditor.Controls.Add(editor);
				}
				
				transformMainPage.Dock = DockStyle.Fill;				
				pnTransformEditor.Controls.Add(transformMainPage);
				list.Add(transformMainPage);

				_editors = (UITransformEditor[])list.ToArray(typeof(UITransformEditor));
			}
		}
		/// <summary>
		/// Adds a new transform.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void AddTransform(object sender, EventArgs e)
		{
			TransformProviderMenuItem menu = (TransformProviderMenuItem)sender;

			// Create Transform.
			ConstructorInfo ci = Type.GetType(menu.TransformProvider.Type).GetConstructor(System.Type.EmptyTypes);
			WebTransform transform = (WebTransform)ci.Invoke(new object[] {});
			transform.Name = menu.TransformProvider.Name;

			TreeNode node = new TreeNode(transform.Name);			
			node.Tag = transform;
			node.SelectedImageIndex = 0;
			node.ImageIndex = 0;

			// Add to Tree View
			int i = tvTransforms.Nodes[0].Nodes.Add(node);
			tvTransforms.SelectedNode = tvTransforms.Nodes[0].Nodes[i];
			
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TransformPage));
			this.panel1 = new System.Windows.Forms.Panel();
			this.tvTransforms = new System.Windows.Forms.TreeView();
			this.mnuInputMenu = new System.Windows.Forms.ContextMenu();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.pnTransformEditor = new System.Windows.Forms.Panel();
			this.mnuOutputMenu = new System.Windows.Forms.ContextMenu();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.tvTransforms);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(600, 156);
			this.panel1.TabIndex = 0;
			// 
			// tvTransforms
			// 
			this.tvTransforms.ContextMenu = this.mnuInputMenu;
			this.tvTransforms.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvTransforms.HideSelection = false;
			this.tvTransforms.ImageList = this.imageList1;
			this.tvTransforms.ItemHeight = 20;
			this.tvTransforms.Location = new System.Drawing.Point(0, 0);
			this.tvTransforms.Name = "tvTransforms";
			this.tvTransforms.Size = new System.Drawing.Size(600, 156);
			this.tvTransforms.TabIndex = 2;
			this.tvTransforms.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvTransforms_AfterSelect);
			this.tvTransforms.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvTransforms_BeforeSelect);
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter1.Location = new System.Drawing.Point(0, 156);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(600, 3);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// pnTransformEditor
			// 
			this.pnTransformEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnTransformEditor.Location = new System.Drawing.Point(0, 159);
			this.pnTransformEditor.Name = "pnTransformEditor";
			this.pnTransformEditor.Size = new System.Drawing.Size(600, 273);
			this.pnTransformEditor.TabIndex = 2;
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// TransformPage
			// 
			this.Controls.Add(this.pnTransformEditor);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.panel1);
			this.Name = "TransformPage";
			this.Size = new System.Drawing.Size(600, 432);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void tvTransforms_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{		
			if ( this.pnTransformEditor.Controls.Count == 0 )
			{
				this.pnTransformEditor.Controls.AddRange(_editors);
			}

			if ( e.Node != null )
			{
				if ( e.Node.Tag != null )
				{
					Type t = manager.GetTransformEditorType(e.Node.Tag.GetType());
					UITransformEditor control = WebTransformPageUIHelper.LookupTransformEditorControl(this.pnTransformEditor, t);
					control.LoadTransformEditorValues(base.SelectedWebRequestIndex,base.SessionScripting,(WebTransform)e.Node.Tag);
					WebTransformPageUIHelper.ShowTransformEditorControl(this.pnTransformEditor,control);
				}
				else 
				{
					// Hide the transforms controls
					foreach ( Control ctl in this.pnTransformEditor.Controls )
					{
						if ( ctl is TransformDocumentationControl )
						{
							ctl.Visible = true;
						} 
						else 
						{
							ctl.Visible = false;
						}
					}
				}
			}
		}

		protected void MenuPopup()
		{
//			if ( ( base.SelectedWebRequestIndex == 0 ) && ( this is InputTransformsPage ) )
//			{
//				tvTransforms.ContextMenu = null;
//			}		
		}
		private void tvTransforms_BeforeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			UpdateCurrentTransform();
		}

		/// <summary>
		/// Updates the current transform.
		/// </summary>
		protected void UpdateCurrentTransform()
		{
			if ( this.pnTransformEditor.Controls.Count == 0 )
			{
				this.pnTransformEditor.Controls.AddRange(_editors);
			}

			// Update Previous Page
			if ( tvTransforms.SelectedNode != null )
			{
				if ( tvTransforms.SelectedNode.Tag != null )
				{
					Type t = manager.GetTransformEditorType(tvTransforms.SelectedNode.Tag.GetType());
					UITransformEditor control = WebTransformPageUIHelper.LookupTransformEditorControl(this.pnTransformEditor, t);

					if ( control.WebTransform != null )
					{
						// Update request with transforms value.
						tvTransforms.Nodes[0].Nodes[tvTransforms.SelectedNode.Index].Tag = control.WebTransform;
					}
				}
			}
		}

		/// <summary>
		/// Copies a transform to the clipboard.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void copyMenu_Click(object sender, EventArgs e)
		{
			_copyObject = ((WebTransform)tvTransforms.SelectedNode.Tag).Clone();			
		}

		/// <summary>
		/// Pastes a transform.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void pasteMenu_Click(object sender, EventArgs e)
		{	
			if ( _copyObject != null )
			{	

				if ( WebTransformPageUIHelper.IsOutputTransform(_copyObject.GetType().ToString()) )
				{
					if ( this is InputTransformsPage )
					{
						MessageBox.Show("Cannot paste an output transform in the Input Transforms collection.");
						return;
					} 
				} 
				else 
				{
					if ( this is OutputTransformsPage )
					{
						MessageBox.Show("Cannot paste an input transform in the Output Transforms collection.");
						return;
					} 
				}

				TreeNode node = new TreeNode(_copyObject.Name);
				node.Tag = _copyObject;
				node.SelectedImageIndex = 0;
				node.ImageIndex = 0;

				// Add to Tree View
				int i = tvTransforms.Nodes[0].Nodes.Add(node);
				tvTransforms.SelectedNode = tvTransforms.Nodes[0].Nodes[i];
			} 
			else 
			{
				MessageBox.Show("No web transform found to paste.");
			}
		}

		/// <summary>
		/// Removes a transfrom from the tree editor.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void removeMenu_Click(object sender, EventArgs e)
		{
			if ( MessageBox.Show("Are you sure you want to remove the selected transform?",AppLocation.ApplicationName,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes )
			{
				WebTransform transform = (WebTransform)tvTransforms.SelectedNode.Tag;				
				if ( this is InputTransformsPage )
				{
					// Remove from Input
					base.WebRequest.RemoveInputTransform(transform);
				} 
				else 
				{
					// Remove from Output
					base.WebRequest.RemoveOutputTransform(transform);
				}
			
				// Clear editor
				Type t = manager.GetTransformEditorType(transform.GetType());
				UITransformEditor control = WebTransformPageUIHelper.LookupTransformEditorControl(pnTransformEditor, t);
				control.Clear();

				// Remove Node
				tvTransforms.Nodes.Remove(tvTransforms.SelectedNode);	
			}
		}


	}
}
