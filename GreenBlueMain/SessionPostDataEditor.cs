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
	/// Contains the definition for the SessionPostDataEditor control.
	/// </summary>
	public class SessionPostDataEditor : BaseSessionDesignerUserControl
	{
		private FormConverter formConverter = new FormConverter();
		internal event UpdateSessionRequestEventHandler UpdateSessionRequestEvent;
		private PostDataCollection postDataItems = null;
		private string _postData = string.Empty;
		private Ecyware.GreenBlue.Controls.TreeEditor postDataEditor;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new SessionPostDataEditor.
		/// </summary>
		public SessionPostDataEditor()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			postDataEditor.ItemHeight = 20;
			postDataEditor.Sorted = false;
		}
		/// <summary>
		/// Creates a new SessionPostDataEditor.
		/// </summary>
		/// <param name="postData"> Form collection to load.</param>
		public SessionPostDataEditor(string postData) : this()
		{
			if ( postData != null )
			{
				// Load tree
				LoadPostData(postData);
			}
		}

		/// <summary>
		/// Displays a message indicating that no data could be showed.
		/// </summary>
		public void DisplayNoDataMessage()
		{
			postDataEditor.Clear();

			TreeEditorNode node = new TreeEditorNode();
			node.Text = "No data available for display";
			postDataEditor.Nodes.Add(node);
		}
		/// <summary>
		/// Loads the post data tree.
		/// </summary>
		/// <param name="postData"></param>
		public void LoadPostData(string postData)
		{
			this.PostData = postData;
			LoadPostData();
		}

		/// <summary>
		/// Loads the post data tree.
		/// </summary>
		public void LoadPostData()
		{
			postDataEditor.Clear();

			string postDataString = this.PostData;

			// TODO: Change to PostDataCollection method.
			postDataItems = formConverter.GetPostDataCollection(postDataString);

			// Create parent node
			TreeEditorNode parentNode = new TreeEditorNode();
			parentNode.Text = "Post Data";

			postDataEditor.Nodes.Add(parentNode);

			for (int j=0;j<postDataItems.Count;j++)
			{
				string name = postDataItems.Keys[j];
				ArrayList values = postDataItems[name];
				
				TreeEditorNode labelNode = new TreeEditorNode();
				labelNode.Text = name;
				parentNode.Nodes.Add(labelNode);

				int i = 0;
				foreach ( object val in values )
				{
					string value = (string)val;
					// TODO: Put format
					postDataEditor.AddTextBoxControl(labelNode, "Index " + i.ToString() + ":", value, 350);
					
					i++;
				}
			}

			postDataEditor.ExpandAll();
		}

		/// <summary>
		/// Save the post data changes
		/// </summary>
		private void SavePostDataChanges()
		{
			PostDataCollection values =  new PostDataCollection();

			foreach ( TreeEditorNode tn in postDataEditor.Nodes[0].Nodes )
			{
				// get key
				string key = tn.Text;

				ArrayList indices = new ArrayList(tn.Nodes.Count);

				foreach ( TreeEditorNode valueNode in tn.Nodes )
				{
					indices.Add(postDataEditor.GetTextBoxValue(valueNode));
				}

				// add item
				values.Add(key, indices);
			}

			postDataItems = values;
		}

		/// <summary>
		/// Gets or sets the post data.
		/// </summary>
		public string PostData
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

		/// <summary>
		/// Gets the post data values.
		/// </summary>
		public PostDataCollection GetPostDataValues
		{
			get
			{
				return this.postDataItems;
			}
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
			this.postDataEditor = new Ecyware.GreenBlue.Controls.TreeEditor();
			this.SuspendLayout();
			// 
			// postDataEditor
			// 
			this.postDataEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.postDataEditor.ImageIndex = -1;
			this.postDataEditor.Location = new System.Drawing.Point(0, 0);
			this.postDataEditor.Name = "postDataEditor";
			this.postDataEditor.SelectedImageIndex = -1;
			this.postDataEditor.SelectedNode = null;
			this.postDataEditor.Size = new System.Drawing.Size(510, 354);
			this.postDataEditor.Sorted = true;
			this.postDataEditor.TabIndex = 0;
			// 
			// SessionPostDataEditor
			// 
			this.Controls.Add(this.postDataEditor);
			this.Name = "SessionPostDataEditor";
			this.Size = new System.Drawing.Size(510, 354);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Updates the session request data.
		/// </summary>
		public override void UpdateSessionRequestData()
		{
			if ( postDataItems != null )
			{
				// save here
				SavePostDataChanges();

				UpdateSessionRequestEventArgs args = new UpdateSessionRequestEventArgs();
				args.UpdateType = UpdateSessionRequestType.PostData;
				args.PostData = formConverter.GetString(postDataItems);
				this.UpdateSessionRequestEvent(this, args);
			}		
		}
	}
}
