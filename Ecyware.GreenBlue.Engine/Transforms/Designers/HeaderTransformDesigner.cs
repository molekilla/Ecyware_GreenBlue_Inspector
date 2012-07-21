using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms.Designers;
using Ecyware.GreenBlue.Engine.Transforms;


namespace Ecyware.GreenBlue.Engine.Transforms.Designers
{
	/// <summary>
	/// Summary description for UserControl1.
	/// </summary>
	public class HeaderTransformDesigner : UITransformEditor
	{
		bool _hasDescription = false;
		private bool _isEdit = false;
		private int _editIndex = -1;
		private ArrayList _headerList = new ArrayList();

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cmbTransformValue;
		private System.Windows.Forms.ComboBox cmbTransformAction;
		private System.Windows.Forms.TextBox txtTransformDescription;
		private System.Windows.Forms.ContextMenu mnuHeaderActions;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.MenuItem mnuEdit;
		private System.Windows.Forms.MenuItem mnuRemove;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colDescription;
		private System.Windows.Forms.ListView lstActions;
		private System.Windows.Forms.TextBox txtHeaderName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ErrorProvider requiredField;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new HeaderTransformDesigner.
		/// </summary>
		public HeaderTransformDesigner()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();			
	
			if ( cmbTransformValue.Items.Count == 0 )
			{
				cmbTransformValue.Items.AddRange(TransformValueDialogs);
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lstActions = new System.Windows.Forms.ListView();
			this.colName = new System.Windows.Forms.ColumnHeader();
			this.colDescription = new System.Windows.Forms.ColumnHeader();
			this.mnuHeaderActions = new System.Windows.Forms.ContextMenu();
			this.mnuEdit = new System.Windows.Forms.MenuItem();
			this.mnuRemove = new System.Windows.Forms.MenuItem();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtHeaderName = new System.Windows.Forms.TextBox();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.txtTransformDescription = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.cmbTransformValue = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbTransformAction = new System.Windows.Forms.ComboBox();
			this.requiredField = new System.Windows.Forms.ErrorProvider();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.lstActions);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(0, 198);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(480, 162);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Headers Transform Actions";
			// 
			// lstActions
			// 
			this.lstActions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						 this.colName,
																						 this.colDescription});
			this.lstActions.ContextMenu = this.mnuHeaderActions;
			this.lstActions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstActions.FullRowSelect = true;
			this.lstActions.Location = new System.Drawing.Point(3, 16);
			this.lstActions.MultiSelect = false;
			this.lstActions.Name = "lstActions";
			this.lstActions.Size = new System.Drawing.Size(474, 143);
			this.lstActions.TabIndex = 0;
			this.lstActions.View = System.Windows.Forms.View.Details;
			// 
			// colName
			// 
			this.colName.Text = "Header Transform Action";
			this.colName.Width = 220;
			// 
			// colDescription
			// 
			this.colDescription.Text = "Description";
			this.colDescription.Width = 350;
			// 
			// mnuHeaderActions
			// 
			this.mnuHeaderActions.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							 this.mnuEdit,
																							 this.mnuRemove});
			this.mnuHeaderActions.Popup += new System.EventHandler(this.mnuHeaderActions_Popup);
			// 
			// mnuEdit
			// 
			this.mnuEdit.Index = 0;
			this.mnuEdit.Text = "&Edit";
			this.mnuEdit.Visible = false;
			this.mnuEdit.Click += new System.EventHandler(this.mnuEdit_Click);
			// 
			// mnuRemove
			// 
			this.mnuRemove.Index = 1;
			this.mnuRemove.Text = "&Remove";
			this.mnuRemove.Visible = false;
			this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.txtHeaderName);
			this.groupBox2.Controls.Add(this.btnUpdate);
			this.groupBox2.Controls.Add(this.linkLabel1);
			this.groupBox2.Controls.Add(this.txtTransformDescription);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.cmbTransformValue);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.cmbTransformAction);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(480, 198);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Header Transform";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Header Name";
			// 
			// txtHeaderName
			// 
			this.txtHeaderName.Location = new System.Drawing.Point(168, 24);
			this.txtHeaderName.Name = "txtHeaderName";
			this.txtHeaderName.Size = new System.Drawing.Size(174, 20);
			this.txtHeaderName.TabIndex = 1;
			this.txtHeaderName.Text = "";
			// 
			// btnUpdate
			// 
			this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnUpdate.Location = new System.Drawing.Point(396, 18);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.TabIndex = 9;
			this.btnUpdate.Text = "Add";
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// linkLabel1
			// 
			this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.linkLabel1.Location = new System.Drawing.Point(348, 78);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(30, 18);
			this.linkLabel1.TabIndex = 6;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "[...]";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// txtTransformDescription
			// 
			this.txtTransformDescription.Location = new System.Drawing.Point(167, 102);
			this.txtTransformDescription.Multiline = true;
			this.txtTransformDescription.Name = "txtTransformDescription";
			this.txtTransformDescription.ReadOnly = true;
			this.txtTransformDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtTransformDescription.Size = new System.Drawing.Size(210, 90);
			this.txtTransformDescription.TabIndex = 8;
			this.txtTransformDescription.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 102);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(150, 24);
			this.label4.TabIndex = 7;
			this.label4.Text = "Value Description";
			// 
			// cmbTransformValue
			// 
			this.cmbTransformValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTransformValue.Location = new System.Drawing.Point(167, 72);
			this.cmbTransformValue.Name = "cmbTransformValue";
			this.cmbTransformValue.Size = new System.Drawing.Size(175, 21);
			this.cmbTransformValue.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(132, 18);
			this.label3.TabIndex = 4;
			this.label3.Text = "Value";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 20);
			this.label2.TabIndex = 2;
			this.label2.Text = "Action";
			// 
			// cmbTransformAction
			// 
			this.cmbTransformAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTransformAction.Items.AddRange(new object[] {
																	"Add",
																	"Update",
																	"Remove"});
			this.cmbTransformAction.Location = new System.Drawing.Point(167, 48);
			this.cmbTransformAction.Name = "cmbTransformAction";
			this.cmbTransformAction.Size = new System.Drawing.Size(175, 21);
			this.cmbTransformAction.TabIndex = 3;
			// 
			// requiredField
			// 
			this.requiredField.ContainerControl = this;
			// 
			// HeaderTransformDesigner
			// 
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "HeaderTransformDesigner";
			this.Size = new System.Drawing.Size(480, 366);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			txtTransformDescription.Text = ShowTransformValueDialog(this.cmbTransformValue.SelectedIndex);
			_hasDescription = true;
		}


		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			if ( ValidateFields() )
			{
				UpdateTransformAction();
			}
		}

		private bool ValidateFields()
		{
			bool result = true;
			if ( this.txtHeaderName.Text.Length == 0 )
			{
				requiredField.SetError(txtHeaderName, "Required field");
				result = false;
			} 
			else 
			{
				requiredField.SetError(txtHeaderName, "");
			}

			if ( this.cmbTransformAction.SelectedIndex == -1 )
			{
				requiredField.SetError(cmbTransformAction, "Required field");
				result = false;
			} 
			else 
			{
				requiredField.SetError(cmbTransformAction, "");
			}

			// Apply check to all other actions except remove
			if ( this.cmbTransformAction.SelectedIndex != 2 )
			{
				if ( this.txtTransformDescription.Text.Length == 0 )
				{
					requiredField.SetError(txtTransformDescription, "A value needs to be configured");
					result = false;
				} 
				else 
				{
					requiredField.SetError(txtTransformDescription, "");
				}
			}

			return result;
		}
		private void UpdateTransformAction()
		{
			HeaderTransform transform = (HeaderTransform)base.WebTransform;
			TransformAction action = null;
			
			switch ( this.cmbTransformAction.SelectedIndex )
			{
				case 0:
					// Add
					AddTransformAction a = new AddTransformAction();
					a.Name = this.txtHeaderName.Text;
					a.Value = TransformValue;
					action = a;

					if ( _hasDescription )
					{
						a.Description = txtTransformDescription.Text;// + " with an add transform action";
					}
					break;
				case 1:
					// Update
					UpdateTransformAction u = new UpdateTransformAction();
					u.Name = this.txtHeaderName.Text;
					u.Value = TransformValue;
					action = u;

					if ( _hasDescription )
					{
						u.Description = txtTransformDescription.Text;// + " with an update transform action";
					}
					break;
				case 2:
					// Remove
					RemoveTransformAction r = new RemoveTransformAction();
					r.Name = this.txtHeaderName.Text;						
					action = r;
					r.Description = "Removes a header";
					break;
			}

			if ( _isEdit )
			{
				ListViewItem updateItem = lstActions.Items[_editIndex];
				updateItem.Text = action.Name;
				if ( action.Description != null )
				{
					updateItem.SubItems[1].Text = action.Description;
				} 
				else 
				{
					action.Description = updateItem.SubItems[1].Text;
				}

				updateItem.Tag = action;

				this.btnUpdate.Text = "Add";
				_isEdit = false;
			} 
			else 
			{
				// Add List View Item
				#region Add List View Item
				ListViewItem listItem = new ListViewItem();
				listItem.Text = action.Name;
				listItem.SubItems.Add(action.Description);
				listItem.Tag = action;

				lstActions.Items.Add(listItem);	
				#endregion
			}

			_hasDescription = false;
			this.txtHeaderName.Text = "";
			this.txtTransformDescription.Text = "";
			
		}


		private void mnuHeaderActions_Popup(object sender, System.EventArgs e)
		{
			if ( this.lstActions.Items.Count > 0 )
			{
				this.mnuRemove.Visible = true;
				this.mnuEdit.Visible = true;
			} 
			else 
			{
				this.mnuRemove.Visible = false;
				this.mnuEdit.Visible = false;
			}
		}

		private void mnuEdit_Click(object sender, System.EventArgs e)
		{
			TransformAction transformAction = (TransformAction)this.lstActions.SelectedItems[0].Tag;
			
			this.btnUpdate.Text = "Update";
			_isEdit = true;
			_editIndex = this.lstActions.SelectedIndices[0];

			this.txtTransformDescription.Text = transformAction.Description;
			this.txtHeaderName.Text = transformAction.Name;
			
			if ( transformAction is AddTransformAction )
			{				
				this.TransformValue = ((AddTransformAction)transformAction).Value;
				this.cmbTransformAction.SelectedIndex = 0;
			}
			if ( transformAction is RemoveTransformAction )
			{
				this.TransformValue = new EmptyTransformValue();
				this.cmbTransformAction.SelectedIndex = 2;
			}
			if ( transformAction is UpdateTransformAction )
			{
				this.TransformValue = ((UpdateTransformAction)transformAction).Value;
				this.cmbTransformAction.SelectedIndex = 1;
			}

			this.cmbTransformValue.SelectedIndex = GetTransformValueComboIndex(this.TransformValue);		
		}

				
		private void mnuRemove_Click(object sender, System.EventArgs e)
		{
			string testName = lstActions.SelectedItems[0].SubItems[0].Text;

			if ( MessageBox.Show("Are you sure you want to remove the selected action?",AppLocation.ApplicationName,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes )
			{				
				_isEdit = false;

				// remove from view
				lstActions.Items.Remove(lstActions.SelectedItems[0]);
			}
		}

		#region Override Methods and Properties

		public override void Clear()
		{
			base.Clear ();

			lstActions.Items.Clear();
			this.txtTransformDescription.Text = "";
			this.txtHeaderName.Text = "";
		}

		public override void LoadTransformEditorValues(int requestIndex, ScriptingApplication scriptingData, WebTransform transform)
		{
			base.LoadTransformEditorValues (requestIndex, scriptingData, transform);

			this.Clear();
			lstActions.Items.Clear();

			HeaderTransform headerTransform = (HeaderTransform)base.WebTransform;

			// Load any tranform actions, if available
			if ( headerTransform.Headers.Length > 0 )
			{				

				foreach ( TransformAction action in headerTransform.Headers )
				{
					ListViewItem listItem = new ListViewItem();
					listItem.Text = action.Name;
					listItem.SubItems.Add(action.Description);
					listItem.Tag = action;

					lstActions.Items.Add(listItem);
				}
			}


			if ( _headerList.Count <= 0 )
			{
				// Load the header combo list.
				_headerList.AddRange(HeaderTransform.GetRestrictedHeaders);				

				foreach ( WebHeader header in base.SessionScripting.WebRequests[base.SelectedWebRequestIndex].RequestHttpSettings.AdditionalHeaders )
				{
					_headerList.Add(header.Name);
				}
			}

			WebRequest req = base.SessionScripting.WebRequests[base.SelectedWebRequestIndex];
			LoadHeaderList(_headerList);
			LoadFormValues(req);
			LoadCookieNames(req);
		}


		/// <summary>
		/// Gets the web transform.
		/// </summary>
		public override WebTransform WebTransform
		{
			get
			{				
				if ( base.WebTransform != null )
				{
					HeaderTransform transform = (HeaderTransform)base.WebTransform;
					transform.RemoveAllHeaderTransformActions();

					foreach ( ListViewItem item in lstActions.Items )
					{												
						transform.AddHeaderTransformAction((TransformAction)item.Tag);
					}
				}

				return base.WebTransform;
			}
		}

		#endregion


	}
}
