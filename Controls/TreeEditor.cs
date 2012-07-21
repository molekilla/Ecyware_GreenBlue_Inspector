// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
using System.ComponentModel;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Base tree class use in FormEditor and HistoryTree.
	/// </summary>
	public class TreeEditor : TreeView
	{
		TreeNode lastNode=null;
		
		private const int WM_HSCROLL = 0x114;
		private const int WM_VSCROLL = 0x115;
		private const int WM_MOUSEWHEEL = 0x20A;

		private Hashtable expandedNodes = null;
		private Hashtable rootNodes = null;

		#region Constructors
		/// <summary>
		/// Creates a new TreeEditor.
		/// </summary>
		public TreeEditor() : base()
		{
			expandedNodes = new Hashtable();
			rootNodes = new Hashtable();
		}

		/// <summary>
		/// Creates a new TreeEditor.
		/// </summary>
		/// <param name="itemHeight"> The item height for a tree node.</param>
		public TreeEditor(int itemHeight) : this()
		{
			this.ItemHeight = itemHeight;
		}
		#endregion

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
			
				this.Clear();
				expandedNodes = null;
				rootNodes = null;
				lastNode = null;
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// Clears the tree editor controls.
		/// </summary>
		public void Clear()
		{
			this.Nodes.Clear();
			this.Controls.Clear();
			expandedNodes.Clear();
			rootNodes.Clear();
			lastNode = null;
		}

		/// <summary>
		/// Expand all the tree nodes.
		/// </summary>
		public new void ExpandAll()
		{
			base.ExpandAll();

			// select first node
			base.SelectedNode = this.Nodes[0];

			if ( this.SelectedNode != null )
			{
				ShowNodes(this.SelectedNode.Nodes);
				AlignTreeNodes();
			}
		}

		/// <summary>
		/// Generates a TextBoxArray by quantity.
		/// </summary>
		/// <param name="number"> The quantity of textboxes to generate.</param>
		/// <returns> Returns an array of textboxes.</returns>
		protected TextBox[] GetTextBoxArray(int number)
		{
			TextBox[] textboxes=new TextBox[number];
			for (int i=0;i<number;i++)
			{
				TextBox t=new TextBox();
				t.Size = new Size(300,10);
				t.ForeColor = Color.Blue;
				t.BackColor = Color.WhiteSmoke;
				textboxes[i]=t;
			}

			return textboxes;
		}

		/// <summary>
		/// Gets the text box value from a node.
		/// </summary>
		/// <param name="node"></param>
		/// <returns></returns>
		public string GetTextBoxValue(TreeEditorNode node)
		{
			return node.NodeControl.Text;
		}
		/// <summary>
		/// Adds a text box control.
		/// </summary>
		/// <param name="root"> The parent TreeEditorNode.</param>
		/// <param name="text"> The text to set.</param>
		/// <param name="value"> The value to set.</param>
		public void AddTextBoxControl(TreeEditorNode root, string text,string value)
		{
			TreeEditorNode node = new TreeEditorNode();

			// add it to root node
			root.Nodes.Add(node);
			node.Text = text;
			
			TextBox[] textboxes;

			textboxes = GetTextBoxArray(1);
			textboxes[0].Size = new Size(300, this.ItemHeight - 5);
			textboxes[0].Name = "txtValue";
			textboxes[0].Text = value;

			node.NodeControl = textboxes[0];

			Label label = new Label();
			label.Text = value;
			label.Size = new Size(300,10);
			node.LabelControl = label;
		}

		/// <summary>
		/// Adds a text box control.
		/// </summary>
		/// <param name="root"> The parent TreeEditorNode.</param>
		/// <param name="text"> The text to set.</param>
		/// <param name="value"> The value to set.</param>
		/// <param name="controlWidth"> The control width.</param>
		public void AddTextBoxControl(TreeEditorNode root, string text,string value, int controlWidth)
		{
			TreeEditorNode node = new TreeEditorNode();

			// add it to root node
			root.Nodes.Add(node);
			node.Text = text;
			
			TextBox[] textboxes;

			textboxes = GetTextBoxArray(1);
			textboxes[0].Size = new Size(controlWidth, this.ItemHeight - 5);
			textboxes[0].Name = "txtValue";
			textboxes[0].Text = value;

			node.NodeControl = textboxes[0];

			Label label = new Label();
			label.Text = value;
			label.Size = new Size(controlWidth,10);
			node.LabelControl = label;
		}

		/// <summary>
		/// Adds a control node.
		/// </summary>
		/// <param name="text"> The node text.</param>
		/// <param name="control"> The node control.</param>
		public void AddControlNode(string text, System.Windows.Forms.Control control)
		{
			TreeEditorNode newNode = new TreeEditorNode();
			newNode.Text = text;
			newNode.NodeControl = control;

			Label label = new Label();
			label.Text=text;
			label.Size=new Size(300,10);
			newNode.LabelControl=label;

			this.Nodes.Add(newNode);
		}

		/// <summary>
		/// Adds a control node.
		/// </summary>
		/// <param name="node"> The root node.</param>
		/// <param name="text"> The node text.</param>
		/// <param name="control"> The node control.</param>
		public void AddControlNode(TreeEditorNode node,string text, System.Windows.Forms.Control control)
		{
			TreeEditorNode newNode = new TreeEditorNode();
			newNode.Text = text;
			newNode.NodeControl = control;
			
			Label label = new Label();
			label.Text=text;
			label.Size=new Size(300,10);
			newNode.LabelControl=label;

			node.Nodes.Add(newNode);
		}

		#region Helper methods
		/// <summary>
		/// Gets or sets the SelectedNode.
		/// </summary>
		public new TreeEditorNode SelectedNode
		{
			get
			{
				if ( base.SelectedNode is TreeEditorNode )
				{
					return (TreeEditorNode)base.SelectedNode;
				} 
				else 
				{
					return null;
				}
			}
			set
			{
				base.SelectedNode=value;
			}
		}

		/// <summary>
		/// Looks for a LabelControl in a TreeNode
		/// </summary>
		/// <param name="node"> The TreeNode.</param>
		/// <returns> Returns true if found, else returns false.</returns>
		private bool HasLabelControl(TreeNode node)
		{
			// check that control has a NodeControl
			Control ctl;
			
			if (node is TreeEditorNode)
			{
				ctl=((TreeEditorNode)node).LabelControl;
				if (ctl!=null)
				{
					return true;
				} 
				else 
				{
					return false;
				}
			} 
			else 
			{
				// this is not a TreeEditorNode
				return false;
			}
		}

		/// <summary>
		/// Looks for a NodeControl in a TreeNode.
		/// </summary>
		/// <param name="node"> The TreeNode.</param>
		/// <returns> Returns true if found, else returns false.</returns>
		private bool HasNodeControl(TreeNode node)
		{
			// check that control has a NodeControl
			Control ctl;
			
			if (node is TreeEditorNode)
			{
				ctl=((TreeEditorNode)node).NodeControl;
				if (ctl!=null)
				{
					return true;
				} else {
					return false;
				}
			} 
			else 
			{
				// this is not a TreeEditorNode
				return false;
			}
		}

		#endregion
		#region overrided events
		/// <summary>
		/// Raises after a node is collapseds.
		/// </summary>
		/// <param name="e"> The TreeViewEventArgs.</param>
		protected override void OnAfterCollapse(TreeViewEventArgs e)
		{
			base.OnAfterCollapse (e);
			
			// hide labels
			this.SuspendLayout();
			//this.BeginUpdate();
			
			if ( e.Node.Parent==null )
			{
				rootNodes.Remove(e.Node);
				foreach (TreeNode n in e.Node.Nodes)
				{
					HideNodes(n.Nodes);
				}
			} else {
				// remove
				expandedNodes.Remove(e.Node);

				HideNodes(e.Node.Nodes);

				// Move nodes to correct position
				AlignTreeNodes();
			}

			//this.EndUpdate();
			this.ResumeLayout(true);
		}

		/// <summary>
		/// Raises after a node is expanded.
		/// </summary>
		/// <param name="e"> The TreeViewEventArgs.</param>
		protected override void OnAfterExpand(TreeViewEventArgs e)
		{
			base.OnAfterExpand (e);

			this.SuspendLayout();
			//this.BeginUpdate();

			// search for nodes down this one and update their position???
			if ( e.Node.Parent==null )
			{
				rootNodes.Add(e.Node,e.Node);
				foreach (TreeNode n in e.Node.Nodes)
				{
					ShowNodes(n.Nodes);
				}
			} 
			else 
			{

				// add
				expandedNodes.Add(e.Node,e.Node);

				ShowNodes(e.Node.Nodes);

				AlignTreeNodes();
			}

			//this.EndUpdate();
			this.ResumeLayout(true);
		}

		/// <summary>
		/// Raises after a node is selected.
		/// </summary>
		/// <param name="e">The TreeViewEventArgs.</param>
		protected override void OnAfterSelect(TreeViewEventArgs e)
		{
			base.OnAfterSelect (e);

			// add at right side
			this.SuspendLayout();
			//this.BeginUpdate();

			// set last node value used in OnBeforeSelect
			lastNode=e.Node;

			if ( HasLabelControl(e.Node) )
			{
				Label l=((TreeEditorNode)e.Node).LabelControl;

				l.Hide();
			}
			
			// add control if found
			if (HasNodeControl(e.Node))
			{
					Rectangle rectangle = e.Node.Bounds;
					Control t =((TreeEditorNode)e.Node).NodeControl;

					// location
					t.Location = new System.Drawing.Point(rectangle.X+rectangle.Width+2,rectangle.Y);

					// only add a new control
					if ( !this.Controls.Contains(t) )
					{
						this.Controls.Add(t);
					} else {
						// already in controls
						// show only
						int i=this.Controls.IndexOf(t);

						if ( i>-1 )
						{
							this.Controls[i].Show();
						}
					}
			}

			//this.EndUpdate();
			this.ResumeLayout(true);
		}

		/// <summary>
		/// Raises before a node is selected.
		/// </summary>
		/// <param name="e">The TreeViewEventArgs.</param>
		protected override void OnBeforeSelect(TreeViewCancelEventArgs e)
		{
			base.OnBeforeSelect (e);

			// exit if last node is null
			if ( lastNode == null ) return;


			// if node control is not null, then remove
			if (HasNodeControl(lastNode))
			{
				this.SuspendLayout();
				//this.BeginUpdate();

				// hide textbox
				Control ctl=((TreeEditorNode)lastNode).NodeControl;
				int i=this.Controls.IndexOf(ctl);

				if ( i>-1 )
				{
					this.Controls[i].Hide();
				}

				Label l=((TreeEditorNode)lastNode).LabelControl;

				// add text to label control
				if ( ctl is ListBox )
				{
					l.Text = "";
					ListBox list = (ListBox)ctl;
					for (int k=0;k<list.SelectedItems.Count;k++)
					{
						l.Text += (string)list.SelectedItems[k];
						if (!(k ==list.SelectedItems.Count-1))
						{
							l.Text += "; ";
						}
					}
				} 
				else 
				{
					l.Text = ctl.Text;
				}
				// location
				l.Location = ctl.Location;

				// only add a new label control
				if ( !this.Controls.Contains(l) )
				{				
					l.Size = ctl.Size;
					l.TextAlign = ContentAlignment.MiddleLeft;
					//l.BorderStyle=BorderStyle.FixedSingle;
					l.ForeColor = Color.Blue;
					this.Controls.Add(l);
				} 
				else 
				{					
					int j=this.Controls.IndexOf(l);

					if ( j>-1 )
					{
						this.Controls[j].Show();
					}
				}

				//this.EndUpdate();
				this.ResumeLayout(true);
			}
		}


		/// <summary>
		/// WndProc Event overrided to capture scroll and mousewheel events.
		/// </summary>
		/// <param name="m"> The WindowMessage.</param>
		protected override void WndProc(ref Message m)
		{
			base.WndProc (ref m);						

			// Horizontal Scroll
			if ( (m.Msg == WM_HSCROLL) || (m.Msg == WM_VSCROLL) || (m.Msg == WM_MOUSEWHEEL) )
			{
				this.SuspendLayout();
				//this.BeginUpdate();
				TreeNode n = this.SelectedNode.Parent;

				// search for nodes down this one and update their position???
				if ( (n!=null) && (n.Parent==null) )
				{
					foreach (TreeNode nn in n.Nodes)
					{
						ShowNodes(nn.Nodes);
					}
				} 
				else 
				{
					if ( n!= null )
					{
						ShowNodes(n.Nodes);
						AlignTreeNodes();
					} 
					else 
					{
						ShowNodes(this.SelectedNode.Nodes);
						AlignTreeNodes();
					}
				}

				//this.EndUpdate();
				this.ResumeLayout(true);
			}
		}

		#endregion
		#region display methods
		/// <summary>
		/// Hides the nodes that contains LabelControl. For use with Collapse events.
		/// </summary>
		/// <param name="nodes"> The TreeNodeCollection type.</param>
		private void HideNodes(TreeNodeCollection nodes)
		{
			foreach (TreeNode n in nodes)
			{
				if ( HasLabelControl(n) )
				{
					TreeEditorNode tnode = (TreeEditorNode)n;

					Label l = tnode.LabelControl;

					// only add a new label control
					if ( this.Controls.Contains(l) )
					{							
						int j = this.Controls.IndexOf(l);

						if ( j>-1 )
						{
							this.Controls[j].Hide();
						}
					}
				}
			}

		}
		/// <summary>
		/// Shows the nodes that contains LabelControl. For use with Expand events.
		/// </summary>
		/// <param name="nodes"> The TreeNodeCollection type.</param>
		private void ShowNodes(TreeNodeCollection nodes)
		{
			foreach (TreeNode n in nodes)
			{
				if (n.Bounds.X==0) continue;

				// show labels
				if ( HasLabelControl(n) )
				{
					TreeEditorNode tnode = (TreeEditorNode)n;

					Label l = tnode.LabelControl;

					// location
					Rectangle rectangle = n.Bounds;
					l.Location = new Point(rectangle.X+rectangle.Width+2, rectangle.Y);

					// only add a new label control
					if ( !this.Controls.Contains(l) )
					{	
						l.Size = new Size(l.Width,20);
						l.TextAlign=ContentAlignment.MiddleLeft;
						//l.BorderStyle=BorderStyle.FixedSingle;
						//l.Text="";
						l.ForeColor = Color.Blue;
						this.Controls.Add(l);
					} 
					else 
					{					
						int j=this.Controls.IndexOf(l);

						if ( j>-1 )
						{
							this.Controls[j].Show();
						}
					}
				}
			}
		}

		/// <summary>
		/// Aligns the tree nodes in the tree. Use when a user scrolls, expand and collapse nodes.
		/// </summary>
		private void AlignTreeNodes()
		{
			for (int i=0;i<rootNodes.Count;i++)
			{
				//TreeNode child = (TreeNode)de.Value;
				//if ( child.IsExpanded )
				//{
					#region Child loop
					// loop thru nodes
					foreach (DictionaryEntry d in expandedNodes)
					{
						TreeNode tn = (TreeNode)d.Value;

						// if expanded
						//if ( tn.IsExpanded )
						//{
							// get innernode
							TreeEditorNode innerNode = (TreeEditorNode)tn.Nodes[0];
							// check label control
							if ( innerNode.LabelControl!=null )
							{
								// update position
								// location
								Rectangle rec = innerNode.Bounds;
								Point p = new Point(rec.X+rec.Width+2,rec.Y);
								innerNode.LabelControl.Location = p;
								innerNode.NodeControl.Location = p;
							}
						//}
					}
					#endregion
				//}
			}
		}



		#endregion
	}
}
