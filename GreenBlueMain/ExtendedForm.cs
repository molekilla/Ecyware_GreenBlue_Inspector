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

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Summary description for ExtendedWebSniffer.
	/// </summary>
	public class ExtendedWebSniffer : System.Windows.Forms.UserControl
	{
		private DockingSuite.DockHost dockHost1;
		private DockingSuite.DockHost dockHost3;
		private DockingSuite.DockHost dockHost4;
		private DockingSuite.DockPanel dockPanel1;
		private DockingSuite.DockPanel dockPanel2;
		private DockingSuite.DockPanel dpHeader;
		private DockingSuite.DockControl dockControl1;
		private DockingSuite.DockControl dockControl2;
		private DockingSuite.DockControl dockControl3;
		private DockingSuite.DockControl dockControl4;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ExtendedWebSniffer()
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
			this.dockHost1 = new DockingSuite.DockHost();
			this.dockHost3 = new DockingSuite.DockHost();
			this.dockHost4 = new DockingSuite.DockHost();
			this.dockPanel1 = new DockingSuite.DockPanel();
			this.dockPanel2 = new DockingSuite.DockPanel();
			this.dpHeader = new DockingSuite.DockPanel();
			this.dockControl1 = new DockingSuite.DockControl();
			this.dockControl2 = new DockingSuite.DockControl();
			this.dockControl3 = new DockingSuite.DockControl();
			this.dockControl4 = new DockingSuite.DockControl();
			this.dockHost1.SuspendLayout();
			this.dockHost4.SuspendLayout();
			this.dockPanel2.SuspendLayout();
			this.dpHeader.SuspendLayout();
			this.SuspendLayout();
			// 
			// dockHost1
			// 
			this.dockHost1.Controls.Add(this.dockPanel1);
			this.dockHost1.Controls.Add(this.dockPanel2);
			this.dockHost1.Dock = System.Windows.Forms.DockStyle.Right;
			this.dockHost1.Location = new System.Drawing.Point(426, 0);
			this.dockHost1.Name = "dockHost1";
			this.dockHost1.Size = new System.Drawing.Size(150, 384);
			this.dockHost1.TabIndex = 0;
			// 
			// dockHost3
			// 
			this.dockHost3.Dock = System.Windows.Forms.DockStyle.Top;
			this.dockHost3.Location = new System.Drawing.Point(0, 0);
			this.dockHost3.Name = "dockHost3";
			this.dockHost3.Size = new System.Drawing.Size(426, 75);
			this.dockHost3.TabIndex = 2;
			// 
			// dockHost4
			// 
			this.dockHost4.Controls.Add(this.dpHeader);
			this.dockHost4.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.dockHost4.Location = new System.Drawing.Point(0, 270);
			this.dockHost4.Name = "dockHost4";
			this.dockHost4.Size = new System.Drawing.Size(426, 114);
			this.dockHost4.TabIndex = 3;
			// 
			// dockPanel1
			// 
			this.dockPanel1.AutoHide = false;
			this.dockPanel1.DockedHeight = 150;
			this.dockPanel1.DockedWidth = 0;
			this.dockPanel1.Location = new System.Drawing.Point(4, 0);
			this.dockPanel1.Name = "dockPanel1";
			this.dockPanel1.SelectedTab = null;
			this.dockPanel1.Size = new System.Drawing.Size(146, 147);
			this.dockPanel1.TabIndex = 0;
			this.dockPanel1.Text = "Docked Panel";
			// 
			// dockPanel2
			// 
			this.dockPanel2.AutoHide = false;
			this.dockPanel2.Controls.Add(this.dockControl1);
			this.dockPanel2.DockedHeight = 234;
			this.dockPanel2.DockedWidth = 150;
			this.dockPanel2.Location = new System.Drawing.Point(4, 150);
			this.dockPanel2.Name = "dockPanel2";
			this.dockPanel2.SelectedTab = this.dockControl1;
			this.dockPanel2.Size = new System.Drawing.Size(146, 234);
			this.dockPanel2.TabIndex = 1;
			this.dockPanel2.Text = "Docked Panel";
			// 
			// dpHeader
			// 
			this.dpHeader.AutoHide = false;
			this.dpHeader.Controls.Add(this.dockControl2);
			this.dpHeader.Controls.Add(this.dockControl3);
			this.dpHeader.Controls.Add(this.dockControl4);
			this.dpHeader.DockedHeight = 0;
			this.dpHeader.DockedWidth = 426;
			this.dpHeader.Location = new System.Drawing.Point(0, 4);
			this.dpHeader.Name = "dpHeader";
			this.dpHeader.SelectedTab = this.dockControl2;
			this.dpHeader.Size = new System.Drawing.Size(426, 110);
			this.dpHeader.TabIndex = 0;
			this.dpHeader.Text = "Docked Panel";
			// 
			// dockControl1
			// 
			this.dockControl1.Guid = new System.Guid("01ab6fab-dfe1-402d-a49c-2ed14d2f06d8");
			this.dockControl1.Location = new System.Drawing.Point(0, 20);
			this.dockControl1.Name = "dockControl1";
			this.dockControl1.PrimaryControl = null;
			this.dockControl1.Size = new System.Drawing.Size(146, 191);
			this.dockControl1.TabImage = null;
			this.dockControl1.TabIndex = 0;
			this.dockControl1.Text = "Docked Control";
			// 
			// dockControl2
			// 
			this.dockControl2.Guid = new System.Guid("3162e1c8-c606-4466-837b-9af3488529ec");
			this.dockControl2.Location = new System.Drawing.Point(0, 20);
			this.dockControl2.Name = "dockControl2";
			this.dockControl2.PrimaryControl = null;
			this.dockControl2.Size = new System.Drawing.Size(426, 67);
			this.dockControl2.TabImage = null;
			this.dockControl2.TabIndex = 0;
			this.dockControl2.Text = "Docked Control";
			// 
			// dockControl3
			// 
			this.dockControl3.Guid = new System.Guid("54d81e37-a909-41ee-9527-6c2041f7e5c5");
			this.dockControl3.Location = new System.Drawing.Point(0, 20);
			this.dockControl3.Name = "dockControl3";
			this.dockControl3.PrimaryControl = null;
			this.dockControl3.Size = new System.Drawing.Size(426, 67);
			this.dockControl3.TabImage = null;
			this.dockControl3.TabIndex = 1;
			this.dockControl3.Text = "Docked Control";
			// 
			// dockControl4
			// 
			this.dockControl4.Guid = new System.Guid("6dc54a38-2769-43aa-ae73-3846bbd97557");
			this.dockControl4.Location = new System.Drawing.Point(0, 20);
			this.dockControl4.Name = "dockControl4";
			this.dockControl4.PrimaryControl = null;
			this.dockControl4.Size = new System.Drawing.Size(426, 67);
			this.dockControl4.TabImage = null;
			this.dockControl4.TabIndex = 2;
			this.dockControl4.Text = "Docked Control";
			// 
			// ExtendedWebSniffer
			// 
			this.Controls.Add(this.dockHost4);
			this.Controls.Add(this.dockHost3);
			this.Controls.Add(this.dockHost1);
			this.Name = "ExtendedWebSniffer";
			this.Size = new System.Drawing.Size(576, 384);
			this.dockHost1.ResumeLayout(false);
			this.dockHost4.ResumeLayout(false);
			this.dockPanel2.ResumeLayout(false);
			this.dpHeader.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
