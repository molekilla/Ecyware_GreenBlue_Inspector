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
	/// Summary description for Test.
	/// </summary>
	public class ByteViewerTest : System.Windows.Forms.UserControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// ByteViewer Test Class.
		/// </summary>
		public ByteViewerTest()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			GBByteViewer byteViewer = new GBByteViewer();
			byteViewer.Dock=DockStyle.Fill;
			byteViewer.SetBytes(System.Text.Encoding.ASCII.GetBytes("TESTING ESTA VAINA"));
			this.Controls.Add(byteViewer);

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
			// 
			// Test
			// 
			this.Name = "Test";
			this.Size = new System.Drawing.Size(336, 258);

		}
		#endregion
	}
}
