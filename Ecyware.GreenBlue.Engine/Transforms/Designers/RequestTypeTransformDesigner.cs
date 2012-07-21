using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace Ecyware.GreenBlue.Protocols.Http.Transforms.Designers
{
	/// <summary>
	/// Summary description for RequestTypeTransformDesigner.
	/// </summary>
	public class RequestTypeTransformDesigner : UITransformEditor
	{
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmbTransformValue;
		private System.Windows.Forms.ComboBox cmbTransformAction;
		private System.Windows.Forms.TextBox txtHeaderName;
		private System.Windows.Forms.TextBox txtTransformDescription;
		private System.Windows.Forms.ContextMenu mnuHeaderActions;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.Button btnUpdate;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new HeaderTransformDesigner.
		/// </summary>
		public RequestTypeTransformDesigner()
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
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.txtTransformDescription = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.cmbTransformValue = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbTransformAction = new System.Windows.Forms.ComboBox();
			this.txtHeaderName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.mnuHeaderActions = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnUpdate);
			this.groupBox2.Controls.Add(this.linkLabel1);
			this.groupBox2.Controls.Add(this.txtTransformDescription);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.cmbTransformValue);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.cmbTransformAction);
			this.groupBox2.Controls.Add(this.txtHeaderName);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(480, 204);
			this.groupBox2.TabIndex = 11;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Request Type Editor";
			// 
			// btnUpdate
			// 
			this.btnUpdate.Location = new System.Drawing.Point(395, 12);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.TabIndex = 19;
			this.btnUpdate.Text = "Update";
			// 
			// linkLabel1
			// 
			this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.linkLabel1.Location = new System.Drawing.Point(293, 84);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(30, 18);
			this.linkLabel1.TabIndex = 18;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "[...]";
			// 
			// txtTransformDescription
			// 
			this.txtTransformDescription.Location = new System.Drawing.Point(167, 102);
			this.txtTransformDescription.Multiline = true;
			this.txtTransformDescription.Name = "txtTransformDescription";
			this.txtTransformDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtTransformDescription.Size = new System.Drawing.Size(210, 90);
			this.txtTransformDescription.TabIndex = 17;
			this.txtTransformDescription.Text = "textBox2";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(5, 102);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(150, 24);
			this.label4.TabIndex = 16;
			this.label4.Text = "Transform Value Description";
			// 
			// cmbTransformValue
			// 
			this.cmbTransformValue.Items.AddRange(new object[] {
																   "Add",
																   "Update",
																   "Remove"});
			this.cmbTransformValue.Location = new System.Drawing.Point(167, 72);
			this.cmbTransformValue.Name = "cmbTransformValue";
			this.cmbTransformValue.Size = new System.Drawing.Size(121, 21);
			this.cmbTransformValue.TabIndex = 15;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(5, 78);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(132, 18);
			this.label3.TabIndex = 14;
			this.label3.Text = "Transform Value";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(5, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 20);
			this.label2.TabIndex = 13;
			this.label2.Text = "Transform Action";
			// 
			// cmbTransformAction
			// 
			this.cmbTransformAction.Items.AddRange(new object[] {
																	"Add",
																	"Update",
																	"Remove"});
			this.cmbTransformAction.Location = new System.Drawing.Point(167, 48);
			this.cmbTransformAction.Name = "cmbTransformAction";
			this.cmbTransformAction.Size = new System.Drawing.Size(121, 21);
			this.cmbTransformAction.TabIndex = 12;
			// 
			// txtHeaderName
			// 
			this.txtHeaderName.Location = new System.Drawing.Point(167, 24);
			this.txtHeaderName.Name = "txtHeaderName";
			this.txtHeaderName.TabIndex = 11;
			this.txtHeaderName.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(5, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 18);
			this.label1.TabIndex = 10;
			this.label1.Text = "Header Name";
			// 
			// mnuHeaderActions
			// 
			this.mnuHeaderActions.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							 this.menuItem1,
																							 this.menuItem2,
																							 this.menuItem3});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Add";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.Text = "Edit";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "Remove";
			// 
			// RequestTypeTransformDesigner
			// 
			this.Controls.Add(this.groupBox2);
			this.Name = "RequestTypeTransformDesigner";
			this.Size = new System.Drawing.Size(480, 210);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
