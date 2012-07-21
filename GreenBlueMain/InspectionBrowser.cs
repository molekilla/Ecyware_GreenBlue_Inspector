using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Summary description for InspectionBrowser.
	/// </summary>
	public class InspectionBrowser : System.Windows.Forms.Form
	{
		private DockingSuite.DockHost dockHost1;
		private DockingSuite.DockPanel dockPanel1;
		private DockingSuite.DockControl dockControl1;
		private DockingSuite.DockControl dockControl2;
		private DockingSuite.DockControl dockControl3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public InspectionBrowser()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(InspectionBrowser));
			this.dockHost1 = new DockingSuite.DockHost();
			this.dockPanel1 = new DockingSuite.DockPanel();
			this.dockControl1 = new DockingSuite.DockControl();
			this.dockControl2 = new DockingSuite.DockControl();
			this.dockControl3 = new DockingSuite.DockControl();
			this.dockHost1.SuspendLayout();
			this.dockPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// dockHost1
			// 
			this.dockHost1.Controls.Add(this.dockPanel1);
			this.dockHost1.Dock = System.Windows.Forms.DockStyle.Left;
			this.dockHost1.Location = new System.Drawing.Point(0, 0);
			this.dockHost1.Name = "dockHost1";
			this.dockHost1.Size = new System.Drawing.Size(438, 284);
			this.dockHost1.TabIndex = 0;
			// 
			// dockPanel1
			// 
			this.dockPanel1.AutoHide = false;
			this.dockPanel1.Controls.Add(this.dockControl1);
			this.dockPanel1.Controls.Add(this.dockControl2);
			this.dockPanel1.Controls.Add(this.dockControl3);
			this.dockPanel1.DockedHeight = 284;
			this.dockPanel1.DockedWidth = 0;
			this.dockPanel1.Location = new System.Drawing.Point(0, 0);
			this.dockPanel1.Name = "dockPanel1";
			this.dockPanel1.SelectedTab = this.dockControl3;
			this.dockPanel1.Size = new System.Drawing.Size(434, 284);
			this.dockPanel1.TabIndex = 0;
			this.dockPanel1.Text = "Docked Panel";
			// 
			// dockControl1
			// 
			this.dockControl1.Guid = new System.Guid("d9ad3f59-d57a-41b9-aa2b-e5ddcf83b560");
			this.dockControl1.Location = new System.Drawing.Point(0, 20);
			this.dockControl1.Name = "dockControl1";
			this.dockControl1.PrimaryControl = null;
			this.dockControl1.Size = new System.Drawing.Size(434, 241);
			this.dockControl1.TabImage = ((System.Drawing.Image)(resources.GetObject("dockControl1.TabImage")));
			this.dockControl1.TabIndex = 0;
			this.dockControl1.Text = "Docked Control";
			// 
			// dockControl2
			// 
			this.dockControl2.Guid = new System.Guid("f14117da-833f-40ec-88c9-7c6940eeb932");
			this.dockControl2.Location = new System.Drawing.Point(0, 20);
			this.dockControl2.Name = "dockControl2";
			this.dockControl2.PrimaryControl = null;
			this.dockControl2.Size = new System.Drawing.Size(434, 241);
			this.dockControl2.TabImage = ((System.Drawing.Image)(resources.GetObject("dockControl2.TabImage")));
			this.dockControl2.TabIndex = 1;
			this.dockControl2.Text = "Docked Control";
			// 
			// dockControl3
			// 
			this.dockControl3.Guid = new System.Guid("feafc49f-1439-4bc2-b95b-1a376421f752");
			this.dockControl3.Location = new System.Drawing.Point(0, 20);
			this.dockControl3.Name = "dockControl3";
			this.dockControl3.PrimaryControl = null;
			this.dockControl3.Size = new System.Drawing.Size(434, 241);
			this.dockControl3.TabImage = ((System.Drawing.Image)(resources.GetObject("dockControl3.TabImage")));
			this.dockControl3.TabIndex = 2;
			this.dockControl3.Text = "Docked Control";
			// 
			// InspectionBrowser
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(472, 284);
			this.Controls.Add(this.dockHost1);
			this.Name = "InspectionBrowser";
			this.Text = "InspectionBrowser";
			this.dockHost1.ResumeLayout(false);
			this.dockPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
