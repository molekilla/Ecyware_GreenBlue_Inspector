// All rights reserved.
// Title: GreenBlue Project
// Author(s): Rogelio Morrell C.
// Date: November 2003
// Add additional authors here
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Ecyware.GreenBlue.Controls;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Summary description for WebForm.
	/// </summary>
	public class HtmlEditor : BasePluginForm
	{
		private onlyconnect.HtmlEditor viewer;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HtmlEditor()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

		}

		public HtmlEditor(string data):this()
		{			
			this.LoadHtml(data);
			
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
			this.viewer = new onlyconnect.HtmlEditor();
			this.SuspendLayout();
			// 
			// viewer
			// 
			this.viewer.DefaultComposeSettings.BackColor = System.Drawing.Color.White;
			this.viewer.DefaultComposeSettings.DefaultFont = new System.Drawing.Font("Arial", 10F);
			this.viewer.DefaultComposeSettings.Enabled = false;
			this.viewer.DefaultComposeSettings.ForeColor = System.Drawing.Color.Black;
			this.viewer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.viewer.DocumentEncoding = onlyconnect.EncodingType.WindowsCurrent;
			this.viewer.Location = new System.Drawing.Point(3, 3);
			this.viewer.Name = "viewer";
			this.viewer.OpenLinksInNewWindow = true;
			this.viewer.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.viewer.SelectionBackColor = System.Drawing.Color.Empty;
			this.viewer.SelectionBullets = false;
			this.viewer.SelectionFont = null;
			this.viewer.SelectionForeColor = System.Drawing.Color.Empty;
			this.viewer.SelectionNumbering = false;
			this.viewer.Size = new System.Drawing.Size(432, 312);
			this.viewer.TabIndex = 1;
			this.viewer.Text = "htmlEditor1";
			// 
			// XmlViewer
			// 
			this.Controls.Add(this.viewer);
			this.DockPadding.All = 3;
			this.Name = "XmlViewer";
			this.Size = new System.Drawing.Size(438, 318);
			this.ResumeLayout(false);

		}
		#endregion

		#region Web Form Methods
		private void LoadHtml(string data)
		{
			this.viewer.IsDesignMode=false;
			this.viewer.LoadDocument(data);
		}
		#endregion

//		private void menuItem1_Click(object sender, System.EventArgs e)
//		{
//			this.txtSource.Text = this.viewer.GetDocumentSource();
//		}


	}
}
