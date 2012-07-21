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
	/// Summary description for CookiesTransformDesigner.
	/// </summary>
	public class CookiesTransformDesigner : UITransformEditor
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
		private System.Windows.Forms.ComboBox cmbTransformValue;
		private System.Windows.Forms.TextBox txtTransformDescription;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.MenuItem mnuRemove;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ContextMenu mnuActions;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colDescription;
		private System.Windows.Forms.ListView lstActions;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox cmbTransformAction;
		private System.Windows.Forms.DateTimePicker dtExpires;
		private System.Windows.Forms.TextBox txtPort;
		private System.Windows.Forms.TextBox txtDomain;
		private System.Windows.Forms.TextBox txtPath;
		private System.Windows.Forms.TextBox txtCookieName;
		private System.Windows.Forms.MenuItem mnuEdit;
		private System.Windows.Forms.ErrorProvider requiredField;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new CookiesTransformDesigner.
		/// </summary>
		public CookiesTransformDesigner()
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
			this.mnuActions = new System.Windows.Forms.ContextMenu();
			this.mnuEdit = new System.Windows.Forms.MenuItem();
			this.mnuRemove = new System.Windows.Forms.MenuItem();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label8 = new System.Windows.Forms.Label();
			this.cmbTransformAction = new System.Windows.Forms.ComboBox();
			this.dtExpires = new System.Windows.Forms.DateTimePicker();
			this.label7 = new System.Windows.Forms.Label();
			this.txtPort = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtDomain = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtPath = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txtCookieName = new System.Windows.Forms.TextBox();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.txtTransformDescription = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.cmbTransformValue = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
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
			this.groupBox1.Location = new System.Drawing.Point(0, 264);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(480, 144);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Cookies";
			// 
			// lstActions
			// 
			this.lstActions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						 this.colName,
																						 this.colDescription});
			this.lstActions.ContextMenu = this.mnuActions;
			this.lstActions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstActions.FullRowSelect = true;
			this.lstActions.Location = new System.Drawing.Point(3, 16);
			this.lstActions.MultiSelect = false;
			this.lstActions.Name = "lstActions";
			this.lstActions.Size = new System.Drawing.Size(474, 125);
			this.lstActions.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lstActions.TabIndex = 0;
			this.lstActions.View = System.Windows.Forms.View.Details;
			// 
			// colName
			// 
			this.colName.Text = "Cookie Name";
			this.colName.Width = 120;
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
			this.mnuRemove.Text = "&Remove";
			this.mnuRemove.Visible = false;
			this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.cmbTransformAction);
			this.groupBox2.Controls.Add(this.dtExpires);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.txtPort);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.txtDomain);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.txtPath);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.txtCookieName);
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
			this.groupBox2.Size = new System.Drawing.Size(480, 264);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Cookies Transform";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(12, 144);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100, 20);
			this.label8.TabIndex = 10;
			this.label8.Text = "Action";
			// 
			// cmbTransformAction
			// 
			this.cmbTransformAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTransformAction.Items.AddRange(new object[] {
																	"Add",
																	"Update",
																	"Remove"});
			this.cmbTransformAction.Location = new System.Drawing.Point(168, 144);
			this.cmbTransformAction.Name = "cmbTransformAction";
			this.cmbTransformAction.Size = new System.Drawing.Size(174, 21);
			this.cmbTransformAction.TabIndex = 11;
			// 
			// dtExpires
			// 
			this.dtExpires.Format = System.Windows.Forms.DateTimePickerFormat.Short;
			this.dtExpires.Location = new System.Drawing.Point(168, 120);
			this.dtExpires.Name = "dtExpires";
			this.dtExpires.Size = new System.Drawing.Size(174, 20);
			this.dtExpires.TabIndex = 9;
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(12, 120);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 20);
			this.label7.TabIndex = 8;
			this.label7.Text = "Expires";
			// 
			// txtPort
			// 
			this.txtPort.Location = new System.Drawing.Point(168, 96);
			this.txtPort.Name = "txtPort";
			this.txtPort.Size = new System.Drawing.Size(174, 20);
			this.txtPort.TabIndex = 7;
			this.txtPort.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(12, 96);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 20);
			this.label6.TabIndex = 6;
			this.label6.Text = "Port";
			// 
			// txtDomain
			// 
			this.txtDomain.Location = new System.Drawing.Point(168, 72);
			this.txtDomain.Name = "txtDomain";
			this.txtDomain.Size = new System.Drawing.Size(174, 20);
			this.txtDomain.TabIndex = 5;
			this.txtDomain.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(12, 72);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 20);
			this.label5.TabIndex = 4;
			this.label5.Text = "Domain";
			// 
			// txtPath
			// 
			this.txtPath.Location = new System.Drawing.Point(168, 48);
			this.txtPath.Name = "txtPath";
			this.txtPath.Size = new System.Drawing.Size(174, 20);
			this.txtPath.TabIndex = 3;
			this.txtPath.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 20);
			this.label2.TabIndex = 2;
			this.label2.Text = "Path";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Cookie Name";
			// 
			// txtCookieName
			// 
			this.txtCookieName.Location = new System.Drawing.Point(168, 24);
			this.txtCookieName.Name = "txtCookieName";
			this.txtCookieName.Size = new System.Drawing.Size(174, 20);
			this.txtCookieName.TabIndex = 1;
			this.txtCookieName.Text = "";
			// 
			// btnUpdate
			// 
			this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnUpdate.Location = new System.Drawing.Point(396, 18);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.TabIndex = 17;
			this.btnUpdate.Text = "Add";
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// linkLabel1
			// 
			this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.linkLabel1.Location = new System.Drawing.Point(348, 174);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(30, 18);
			this.linkLabel1.TabIndex = 14;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "[...]";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// txtTransformDescription
			// 
			this.txtTransformDescription.Location = new System.Drawing.Point(167, 192);
			this.txtTransformDescription.Multiline = true;
			this.txtTransformDescription.Name = "txtTransformDescription";
			this.txtTransformDescription.ReadOnly = true;
			this.txtTransformDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtTransformDescription.Size = new System.Drawing.Size(210, 66);
			this.txtTransformDescription.TabIndex = 16;
			this.txtTransformDescription.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 192);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(150, 24);
			this.label4.TabIndex = 15;
			this.label4.Text = "Value Description";
			// 
			// cmbTransformValue
			// 
			this.cmbTransformValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTransformValue.Location = new System.Drawing.Point(167, 168);
			this.cmbTransformValue.Name = "cmbTransformValue";
			this.cmbTransformValue.Size = new System.Drawing.Size(174, 21);
			this.cmbTransformValue.TabIndex = 13;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 168);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(132, 18);
			this.label3.TabIndex = 12;
			this.label3.Text = "Value";
			// 
			// requiredField
			// 
			this.requiredField.ContainerControl = this;
			// 
			// CookiesTransformDesigner
			// 
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "CookiesTransformDesigner";
			this.Size = new System.Drawing.Size(480, 414);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			this.txtTransformDescription.Text = ShowTransformValueDialog(this.cmbTransformValue.SelectedIndex);
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
			if ( this.txtCookieName.Text.Length == 0 )
			{
				requiredField.SetError(txtCookieName, "Required field");
				result = false;
			} 
			else 
			{
				requiredField.SetError(txtCookieName, "");
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
			CookiesTransform transform = (CookiesTransform)base.WebTransform;
			TransformAction action = null;
			Cookie cookie = null;
			
			switch ( this.cmbTransformAction.SelectedIndex )
			{
				case 0:
					// Add TransformAction and cookie too.
					AddTransformAction a = new AddTransformAction();
					a.Name = this.txtCookieName.Text;
					a.Value = TransformValue;
					action = a;

					if ( _hasDescription )
					{
						a.Description = txtTransformDescription.Text;// + " with an add transform action";
					}

					// Add dummy cookie.
					cookie = new Cookie();
					cookie.Domain = txtDomain.Text;
					cookie.Name = txtCookieName.Text;
					cookie.Path = txtPath.Text;
					cookie.Port = txtPort.Text;
					cookie.Expires = this.dtExpires.Value;
					break;
				case 1:
					// Update
					UpdateTransformAction u = new UpdateTransformAction();
					u.Name = this.txtCookieName.Text;
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
					r.Name = this.txtCookieName.Text;						
					action = r;
					r.Description = "Removes a cookie";
					break;
			}

			if ( _isEdit )
			{
				ListViewItem updateItem = lstActions.Items[_editIndex];
				updateItem.Text = txtCookieName.Text;
				if ( action.Description != null )
				{
					updateItem.SubItems[1].Text = action.Description;
				} 
				else 
				{
					action.Description = updateItem.SubItems[1].Text;
				}

				updateItem.Tag = new CookieListItemArgs(cookie, action);

				btnUpdate.Text = "Add";
				_isEdit = false;
			} 
			else 
			{
				// Add List View Item
				#region Add List View Item
				ListViewItem listItem = new ListViewItem();
				listItem.Text = txtCookieName.Text;
				listItem.SubItems.Add(action.Description);
				listItem.Tag = new CookieListItemArgs(cookie, action);
				lstActions.Items.Add(listItem);				
				#endregion
			}

			this.txtCookieName.Text = String.Empty;
			this.txtDomain.Text  = String.Empty;
			this.txtPath.Text  = String.Empty;
			this.txtPort.Text  = String.Empty;
			this.txtTransformDescription.Text  = String.Empty;
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
			CookieListItemArgs args = (CookieListItemArgs)this.lstActions.SelectedItems[0].Tag;
			
			btnUpdate.Text = "Update";
			_isEdit = true;
			_editIndex = this.lstActions.SelectedIndices[0];
			
			if ( args.Cookie != null )
			{
				//this.txtCookieName.Text = args.Cookie.Name;
				this.txtDomain.Text = args.Cookie.Domain;
				this.txtPath.Text = args.Cookie.Path;
				this.txtPort.Text = args.Cookie.Port;
				this.dtExpires.Value = args.Cookie.Expires;

				this.TransformValue = ((AddTransformAction)args.TransformAction).Value;
				this.cmbTransformAction.SelectedIndex = 0;
			}

			this.txtTransformDescription.Text = args.TransformAction.Description;
			this.txtCookieName.Text = args.TransformAction.Name;

			if ( args.TransformAction is RemoveTransformAction )
			{
				this.txtDomain.Text = string.Empty;
				this.txtPath.Text = string.Empty;
				this.txtPort.Text = string.Empty;				
				this.cmbTransformAction.SelectedIndex = 2;
				this.TransformValue = new EmptyTransformValue();
			}

			if ( args.TransformAction is UpdateTransformAction )
			{
				this.txtDomain.Text = string.Empty;
				this.txtPath.Text = string.Empty;
				this.txtPort.Text = string.Empty;
				this.TransformValue = ((UpdateTransformAction)args.TransformAction).Value;
				this.cmbTransformAction.SelectedIndex = 1;
			}

			this.cmbTransformValue.SelectedIndex = GetTransformValueComboIndex(this.TransformValue);

		}

		private void mnuRemove_Click(object sender, System.EventArgs e)
		{
			//string testName = lstActions.SelectedItems[0].SubItems[0].Text;

			if ( MessageBox.Show("Are you sure you want to remove the selected transform action?",AppLocation.ApplicationName,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes )
			{				
				// remove from view
				lstActions.Items.Remove(lstActions.SelectedItems[0]);
				_isEdit = false;
			}
		}

		#region Override Methods and Properties

		public override void Clear()
		{
			base.Clear ();

			lstActions.Items.Clear();
			this.txtCookieName.Text = String.Empty;
			this.txtDomain.Text  = String.Empty;
			this.txtPath.Text  = String.Empty;
			this.txtPort.Text  = String.Empty;
			this.txtTransformDescription.Text  = String.Empty;
		}

		public override void LoadTransformEditorValues(int requestIndex, ScriptingApplication scriptingData, WebTransform transform)
		{
			base.LoadTransformEditorValues (requestIndex, scriptingData, transform);

			this.Clear();
			lstActions.Items.Clear();

			CookiesTransform tvalue = (CookiesTransform)base.WebTransform;

			// Load any tranform actions, if available
			if ( tvalue.CookieTransformActions.Length > 0 )
			{				
				int i = 0;
				foreach ( TransformAction ta in  tvalue.CookieTransformActions )
				{
					ListViewItem listItem = new ListViewItem();
					listItem.Text = ta.Name;
					listItem.SubItems.Add(ta.Description);

					if ( ta is AddTransformAction )
					{
						Cookie ck = tvalue.GetCookie(ta.Name);
						listItem.Tag = new CookieListItemArgs(ck, ta);
					} 
					else 
					{
						listItem.Tag = new CookieListItemArgs(null, ta);
					}					

					lstActions.Items.Add(listItem);
					i++;
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
					CookiesTransform transform = (CookiesTransform)base.WebTransform;
					transform.RemoveAllCookies();
					transform.RemoveAllCookieTransformActions();

					foreach ( ListViewItem item in lstActions.Items )
					{												
						CookieListItemArgs args = (CookieListItemArgs)item.Tag;
						if ( args.Cookie != null )
						{
							transform.AddCookie(args.Cookie);
						}

						transform.AddCookieTransformAction(args.TransformAction);						
					}
				}

				return base.WebTransform;
			}
		}

		#endregion


	}

	internal class CookieListItemArgs
	{
		public CookieListItemArgs()
		{
		}

		public CookieListItemArgs(Cookie ck, TransformAction ta)
		{
			Cookie = ck;
			TransformAction = ta;
		}

		public Cookie Cookie;
		public TransformAction TransformAction;

	}
}
