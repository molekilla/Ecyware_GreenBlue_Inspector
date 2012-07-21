// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: June 2004
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Ecyware.GreenBlue.Engine;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Contains the definition for UrlSpiderControl control.
	/// </summary>
	public class UrlSpiderControl : System.Windows.Forms.UserControl
	{
		public event EventHandler DoubleClickNode;
		private SessionRequestList _urlRequests;
		private System.Windows.Forms.TreeView tvResources;
		private System.Windows.Forms.ToolTip toolTip;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Creates a new UrlSpiderControl.
		/// </summary>
		public UrlSpiderControl()
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
			this.components = new System.ComponentModel.Container();
			this.tvResources = new System.Windows.Forms.TreeView();
			this.toolTip = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// tvResources
			// 
			this.tvResources.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvResources.ImageIndex = -1;
			this.tvResources.Location = new System.Drawing.Point(0, 0);
			this.tvResources.Name = "tvResources";
			this.tvResources.SelectedImageIndex = -1;
			this.tvResources.Size = new System.Drawing.Size(210, 510);
			this.tvResources.TabIndex = 0;
			this.tvResources.DoubleClick += new System.EventHandler(this.tvResources_DoubleClick);
			this.tvResources.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvResources_AfterSelect);
			this.tvResources.MouseMove += new System.Windows.Forms.MouseEventHandler(this.tvResources_MouseMove);
			// 
			// UrlSpiderControl
			// 
			this.Controls.Add(this.tvResources);
			this.Name = "UrlSpiderControl";
			this.Size = new System.Drawing.Size(210, 510);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Gets or sets the imagelist.
		/// </summary>
		[Browsable(true)]
		public ImageList ImageList
		{
			get
			{
				return this.tvResources.ImageList;
			}
			set
			{
				tvResources.ImageList = value;
			}
		}


		/// <summary>
		/// Gets or sets the url requests.
		/// </summary>
		public SessionRequestList UrlRequests
		{
			get
			{
				return _urlRequests;
			}
			set
			{
				_urlRequests = value;
			}
		}

		#region Methods
		/// <summary>
		/// Gets the url link for the selected node.
		/// </summary>
		/// <returns> A string with the node link.</returns>
		public string GetNodeLink()
		{
			try
			{
				if ( tvResources.SelectedNode.Parent == null )
				{
					return string.Empty;
				} 
				else 
				{
					return (string)this.tvResources.SelectedNode.Tag;
				}
			}
			catch
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// Adds a root node.
		/// </summary>
		/// <param name="name">The root node name.</param>
		/// <returns> A tree node.</returns>
		public TreeNode AddRootNode(string name)
		{			
			return this.tvResources.Nodes.Add(name);
		}


		/// <summary>
		/// Clear nodes.
		/// </summary>
		public void Clear()
		{
			tvResources.Nodes.Clear();
		}


		/// <summary>
		/// Adds a children to the tree control.
		/// </summary>
		/// <param name="parent"> The parent index.</param>
		/// <param name="name"> The node name.</param>
		/// <param name="link"> The node url link.</param>
		/// <param name="imageIndex"> The node image index.</param>
		public void AddChildren(int parent, string name, string link, int imageIndex)
		{			
			TreeNode node = new TreeNode(name);
			node.Tag = link;
			node.ImageIndex = imageIndex;
			node.SelectedImageIndex = imageIndex;
			this.tvResources.Nodes[parent].Nodes.Add(node);
		}

		#endregion
		#region Tree Control Events
		private void tvResources_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			if ( e.Node.Tag != null )
			{
				this.toolTip.SetToolTip(this.tvResources, "Url: " + (string)e.Node.Tag);
			}			
		}

		private void tvResources_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			Point pt = new Point(e.X,e.Y);
			//pt = tvResources.PointToClient(pt);
			TreeNode node = tvResources.GetNodeAt(pt);

			if ( node != null )
			{
				if ( node.Tag != null )
				{
					toolTip.SetToolTip(tvResources, "Url: " + (string)node.Tag);
				}
			}			
		}

		#endregion

		private void tvResources_DoubleClick(object sender, System.EventArgs e)
		{
			this.DoubleClickNode(sender, e);
		}


//		private void tvResources_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
//		{
	
//		}
	}
}
