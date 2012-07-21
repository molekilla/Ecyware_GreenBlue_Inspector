using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Summary description for RecentSitesEditor.
	/// </summary>
	public class RecentSitesEditor : System.Windows.Forms.UserControl
	{
		private Ecyware.GreenBlue.Controls.HistoryTree historyTree1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Recent Sites Editor.
		/// </summary>
		public RecentSitesEditor()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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
			this.historyTree1 = new Ecyware.GreenBlue.Controls.HistoryTree();
			this.SuspendLayout();
			// 
			// historyTree1
			// 
			this.historyTree1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.historyTree1.IconNodeIndex = 0;
			this.historyTree1.IconSiteIndex = 0;
			this.historyTree1.ImageIndex = -1;
			this.historyTree1.Location = new System.Drawing.Point(0, 0);
			this.historyTree1.Name = "historyTree1";
			this.historyTree1.SelectedImageIndex = -1;
			this.historyTree1.SelectedNode = null;
			this.historyTree1.Size = new System.Drawing.Size(150, 150);
			this.historyTree1.TabIndex = 0;
			// 
			// RecentSitesEditor
			// 
			this.Controls.Add(this.historyTree1);
			this.Name = "RecentSitesEditor";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
