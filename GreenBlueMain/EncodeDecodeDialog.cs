using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Summary description for EncodeDecodeDialog.
	/// </summary>
	public class EncodeDecodeDialog : System.Windows.Forms.Form
	{
		private Ecyware.GreenBlue.GreenBlueMain.EncodeDecodeUtil encodeDecodeUtil1;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public EncodeDecodeDialog()
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(EncodeDecodeDialog));
			this.encodeDecodeUtil1 = new Ecyware.GreenBlue.GreenBlueMain.EncodeDecodeUtil();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// encodeDecodeUtil1
			// 
			this.encodeDecodeUtil1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.encodeDecodeUtil1.DockPadding.All = 3;
			this.encodeDecodeUtil1.Location = new System.Drawing.Point(0, 0);
			this.encodeDecodeUtil1.Name = "encodeDecodeUtil1";
			this.encodeDecodeUtil1.Size = new System.Drawing.Size(580, 398);
			this.encodeDecodeUtil1.TabIndex = 0;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2});
			this.menuItem1.Text = "&File";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "&Close";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// EncodeDecodeDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(580, 398);
			this.Controls.Add(this.encodeDecodeUtil1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Menu = this.mainMenu1;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(586, 430);
			this.Name = "EncodeDecodeDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Encode Decode Tool";
			this.ResumeLayout(false);

		}
		#endregion

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
