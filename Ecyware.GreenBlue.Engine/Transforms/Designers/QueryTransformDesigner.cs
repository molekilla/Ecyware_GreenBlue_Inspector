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
	/// Summary description for QueryTransformDesigner.
	/// </summary>
	public class QueryTransformDesigner : UITransformEditor
	{
		private bool _isEdit = false;
		private int _editIndex = -1;
		private Transport _transport;
		private ArrayList _headerList = new ArrayList();

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox cmbTransformValue;
		private System.Windows.Forms.TextBox txtTransformDescription;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.MenuItem mnuRemove;
		private System.Windows.Forms.ColumnHeader colDescription;
		private System.Windows.Forms.ListView lstActions;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ContextMenu mnuActions;
		private System.Windows.Forms.LinkLabel lnkSelectTransport;
		private System.Windows.Forms.ComboBox cmbTransports;
		private System.Windows.Forms.MenuItem mnuEdit;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new QueryTransformDesigner.
		/// </summary>
		public QueryTransformDesigner()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
		
			if ( cmbTransformValue.Items.Count == 0 )
			{
				cmbTransformValue.Items.AddRange(TransformValueDialogs);
			}

			this.cmbTransports.Items.Clear();
			this.cmbTransports.Items.AddRange(TransportValueDialogs);
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
			this.colDescription = new System.Windows.Forms.ColumnHeader();
			this.mnuActions = new System.Windows.Forms.ContextMenu();
			this.mnuEdit = new System.Windows.Forms.MenuItem();
			this.mnuRemove = new System.Windows.Forms.MenuItem();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.lnkSelectTransport = new System.Windows.Forms.LinkLabel();
			this.cmbTransports = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.txtTransformDescription = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.cmbTransformValue = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
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
			this.groupBox1.Location = new System.Drawing.Point(0, 156);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(480, 204);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Query Commands";
			// 
			// lstActions
			// 
			this.lstActions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						 this.colDescription});
			this.lstActions.ContextMenu = this.mnuActions;
			this.lstActions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstActions.FullRowSelect = true;
			this.lstActions.Location = new System.Drawing.Point(3, 16);
			this.lstActions.MultiSelect = false;
			this.lstActions.Name = "lstActions";
			this.lstActions.Size = new System.Drawing.Size(474, 185);
			this.lstActions.TabIndex = 0;
			this.lstActions.View = System.Windows.Forms.View.Details;
			// 
			// colDescription
			// 
			this.colDescription.Text = "Description";
			this.colDescription.Width = 350;
			// 
			// mnuActions
			// 
			this.mnuActions.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.mnuEdit,
																					   this.mnuRemove});
			this.mnuActions.Popup += new System.EventHandler(this.mnuHeaderActions_Popup);
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
			this.mnuRemove.Text = "Remove";
			this.mnuRemove.Visible = false;
			this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.lnkSelectTransport);
			this.groupBox2.Controls.Add(this.cmbTransports);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.btnUpdate);
			this.groupBox2.Controls.Add(this.linkLabel1);
			this.groupBox2.Controls.Add(this.txtTransformDescription);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.cmbTransformValue);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(480, 156);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Query Transform";
			// 
			// lnkSelectTransport
			// 
			this.lnkSelectTransport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lnkSelectTransport.Location = new System.Drawing.Point(348, 30);
			this.lnkSelectTransport.Name = "lnkSelectTransport";
			this.lnkSelectTransport.Size = new System.Drawing.Size(30, 18);
			this.lnkSelectTransport.TabIndex = 2;
			this.lnkSelectTransport.TabStop = true;
			this.lnkSelectTransport.Text = "[...]";
			this.lnkSelectTransport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSelectTransport_LinkClicked);
			// 
			// cmbTransports
			// 
			this.cmbTransports.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTransports.Items.AddRange(new object[] {
															   "Smtp",
															   "Gmail",
															   "Database",
															   "Blogger",
															   "Session"});
			this.cmbTransports.Location = new System.Drawing.Point(168, 24);
			this.cmbTransports.Name = "cmbTransports";
			this.cmbTransports.Size = new System.Drawing.Size(175, 21);
			this.cmbTransports.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 20);
			this.label2.TabIndex = 0;
			this.label2.Text = "Transport";
			// 
			// btnUpdate
			// 
			this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnUpdate.Location = new System.Drawing.Point(396, 18);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.TabIndex = 8;
			this.btnUpdate.Text = "Add";
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// linkLabel1
			// 
			this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.linkLabel1.Location = new System.Drawing.Point(348, 54);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(30, 18);
			this.linkLabel1.TabIndex = 5;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "[...]";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// txtTransformDescription
			// 
			this.txtTransformDescription.Location = new System.Drawing.Point(167, 72);
			this.txtTransformDescription.Multiline = true;
			this.txtTransformDescription.Name = "txtTransformDescription";
			this.txtTransformDescription.ReadOnly = true;
			this.txtTransformDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtTransformDescription.Size = new System.Drawing.Size(210, 72);
			this.txtTransformDescription.TabIndex = 7;
			this.txtTransformDescription.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 72);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(150, 24);
			this.label4.TabIndex = 6;
			this.label4.Text = "Value Description";
			// 
			// cmbTransformValue
			// 
			this.cmbTransformValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTransformValue.Location = new System.Drawing.Point(167, 48);
			this.cmbTransformValue.Name = "cmbTransformValue";
			this.cmbTransformValue.Size = new System.Drawing.Size(175, 21);
			this.cmbTransformValue.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(132, 18);
			this.label3.TabIndex = 3;
			this.label3.Text = "Value";
			// 
			// QueryTransformDesigner
			// 
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "QueryTransformDesigner";
			this.Size = new System.Drawing.Size(480, 366);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.txtTransformDescription.Text = ShowTransformValueDialog(this.cmbTransformValue.SelectedIndex);
		}

		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			UpdateTransformValue();
		}

		private void UpdateTransformValue()
		{
			QueryTransform transform = (QueryTransform)base.WebTransform;
			transform.Transport = _transport;

			QueryCommandAction action = new QueryCommandAction();
			action.Description = this.txtTransformDescription.Text;
			action.Value = this.TransformValue;
			action.Name = string.Empty;

			if ( _isEdit )
			{
				ListViewItem updateItem = lstActions.Items[_editIndex];
				updateItem.Text = action.Description;
				updateItem.Tag = action;

				this.btnUpdate.Text = "Add";
				_isEdit = false;
			} 
			else 
			{
				// Add List View Item
				#region Add List View Item
				ListViewItem listItem = new ListViewItem();
				listItem.Text = action.Description;
				listItem.Tag = action;

				lstActions.Items.Add(listItem);				
				#endregion
			}

			this.txtTransformDescription.Text = "";
		}


		private void mnuHeaderActions_Popup(object sender, System.EventArgs e)
		{
			if ( this.lstActions.Items.Count > 0 )
			{
				this.mnuEdit.Visible = true;
				this.mnuRemove.Visible = true;
			} 
			else 
			{
				this.mnuEdit.Visible = false;
				this.mnuRemove.Visible = false;
			}
		}
				
		private void mnuRemove_Click(object sender, System.EventArgs e)
		{
			//string testName = lstActions.SelectedItems[0].SubItems[0].Text;

			if ( MessageBox.Show("Are you sure you want to remove the selected query command?",AppLocation.ApplicationName,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes )
			{				
				_isEdit = false;

				// remove from view
				lstActions.Items.Remove(lstActions.SelectedItems[0]);
			}
		}

		private void lnkSelectTransport_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			_transport = this.ShowTransportValueDialog(this.cmbTransports.SelectedIndex, _transport);
		}

		private void mnuEdit_Click(object sender, System.EventArgs e)
		{
			QueryCommandAction action = (QueryCommandAction)this.lstActions.SelectedItems[0].Tag;
			
			this.btnUpdate.Text = "Update";
			_isEdit = true;
			_editIndex = this.lstActions.SelectedIndices[0];

			this.txtTransformDescription.Text = action.Description;
			this.TransformValue = action.Value;
			
			this.cmbTransformValue.SelectedIndex = GetTransformValueComboIndex(this.TransformValue);
		}

		#region Override Methods and Properties

		public override void Clear()
		{
			base.Clear ();

			lstActions.Items.Clear();
			this.txtTransformDescription.Text = "";
		}

		public override void LoadTransformEditorValues(int requestIndex, ScriptingApplication scriptingData, WebTransform transform)
		{
			base.LoadTransformEditorValues (requestIndex, scriptingData, transform);

			this.Clear();
			lstActions.Items.Clear();

			QueryTransform queryTransform = (QueryTransform)base.WebTransform;

			this.cmbTransports.SelectedIndex = GetTransportValueComboIndex(queryTransform.Transport);

			if ( queryTransform.Transport != null )
			{
				_transport = queryTransform.Transport;
			}

			// Load any tranform value, if available
			if ( queryTransform.QueryCommandActions.Length > 0 )
			{				
				foreach ( QueryCommandAction action in queryTransform.QueryCommandActions )
				{
					ListViewItem listItem = new ListViewItem();
					listItem.Text = action.Description;
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
			
			//this.cmbTransformValue.SelectedIndex = this.GetTransformValueComboIndex(queryTransform);

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
					QueryTransform transform = (QueryTransform)base.WebTransform;
					transform.RemoveAllQueryCommandActions();

					foreach ( ListViewItem item in lstActions.Items )
					{												
						transform.AddQueryCommandAction((QueryCommandAction)item.Tag);
					}

					transform.Transport = _transport;
				}

				return base.WebTransform;
			}
		}

		#endregion


	}
}
