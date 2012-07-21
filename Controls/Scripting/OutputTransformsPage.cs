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
using Ecyware.GreenBlue.Engine.Transforms;
using Ecyware.GreenBlue.Engine.Transforms.Designers;
using Ecyware.GreenBlue.Engine.Scripting;

namespace Ecyware.GreenBlue.Controls.Scripting
{
	/// <summary>
	/// Contains the definition for the ScriptingMainPage control.
	/// </summary>
	public class OutputTransformsPage : TransformPage
	{

		System.Windows.Forms.MenuItem pasteMenu = new System.Windows.Forms.MenuItem();
		System.Windows.Forms.MenuItem removeMenu = new System.Windows.Forms.MenuItem();
		System.Windows.Forms.MenuItem copyMenu = new System.Windows.Forms.MenuItem();

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new SessionRequestHeaderEditor
		/// </summary>
		public OutputTransformsPage()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			EventHandler onclickEvent = new EventHandler(AddTransform);
			WebTransformPageUIHelper.LoadOutputTransformProviders(mnuOutputMenu,onclickEvent);

			// Add Copy Menu
			copyMenu.Text = "&Copy";
			copyMenu.Click += new EventHandler(copyMenu_Click);
			mnuOutputMenu.MenuItems.Add(copyMenu);

			// Add Paste Menu			
			pasteMenu.Text = "&Paste";
			pasteMenu.Click += new EventHandler(pasteMenu_Click);
			mnuOutputMenu.MenuItems.Add(pasteMenu);

			// Add Remove Menu
			removeMenu.Text = "&Remove";
			removeMenu.Click += new EventHandler(removeMenu_Click);
			mnuOutputMenu.MenuItems.Add(removeMenu);


			this.mnuOutputMenu.Popup += new EventHandler(mnuOutputMenu_Popup);
		}

		internal void HideParentMenus()
		{
			if ( tvTransforms.SelectedNode.Parent == null )
			{
				copyMenu.Visible = false;
				removeMenu.Visible = false;
				pasteMenu.Visible = true;
			} 
			else 
			{
				copyMenu.Visible = true;
				removeMenu.Visible = true;
				pasteMenu.Visible = false;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(OutputTransformsPage));
			// 
			// OutputTransformsPage
			// 
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "OutputTransformsPage";
			this.Size = new System.Drawing.Size(624, 396);
		}
		#endregion


		/// <summary>
		/// Loads the request.
		/// </summary>
		/// <param name="request"></param>
		public override void LoadRequest(int index, ScriptingApplication scripting ,Ecyware.GreenBlue.Engine.Scripting.WebRequest request)
		{
			base.LoadRequest (index, scripting, request);

			this.SuspendLayout();
			tvTransforms.ContextMenu = this.mnuOutputMenu;

			pnTransformEditor.Controls.Clear();
			tvTransforms.Nodes.Clear();

			tvTransforms.Nodes.Add(new TreeNode("Transforms"));

			if ( request.OutputTransforms.Length > 0 )
			{				
				// Load transforms
				WebTransformPageUIHelper.LoadTransforms(tvTransforms.Nodes[0],request.OutputTransforms);
			}
			
			tvTransforms.ExpandAll();
			tvTransforms.SelectedNode = tvTransforms.Nodes[0];
			this.ResumeLayout(false);
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
					UpdateCurrentTransform();
					base.WebRequest.OutputTransforms = WebTransformPageUIHelper.GetTransforms(tvTransforms);
				}

				return base.WebRequest;
			}
		}

		private void mnuOutputMenu_Popup(object sender, EventArgs e)
		{
			MenuPopup();
			HideParentMenus();
		}
	}
}
