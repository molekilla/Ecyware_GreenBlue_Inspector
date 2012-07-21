using System;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Protocols.Http;
using Ecyware.GreenBlue.Protocols.Http.Scripting;
using Ecyware.GreenBlue.Protocols.Http.Transforms;
using Ecyware.GreenBlue.Protocols.Http.Transforms.Designers;
using System.Text;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace GmailSmtp
{
	/// <summary>
	/// Summary description for GMailSmtpTransformDesigner.
	/// </summary>
	public class GMailSmtpTransformDesigner : UITransformEditor
	{		
		private bool _isEdit = false;
		private int _editIndex = -1;
		private string _description;
		private ArrayList _headerList = new ArrayList();

		private System.Windows.Forms.GroupBox grpMain;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.RadioButton rbHTML;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.RadioButton rbText;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.TextBox txtFrom;
		private System.Windows.Forms.TextBox txtTo;
		private System.Windows.Forms.TextBox txtCc;
		private System.Windows.Forms.TextBox txtBcc;
		private System.Windows.Forms.TextBox txtSubject;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ComboBox cmbTransformValue;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ListView lvValues;
		private System.Windows.Forms.Button cmdAdd;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.ContextMenu mnuActions;
		private System.Windows.Forms.MenuItem mnuEdit;
		private System.Windows.Forms.MenuItem mnuRemove;
		private System.Windows.Forms.MenuItem mnuUp;
		private System.Windows.Forms.MenuItem mnuDown;
		private System.Windows.Forms.MenuItem menuItem1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public GMailSmtpTransformDesigner()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();


			this.txtPassword.PasswordChar = '\u25cf';

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
			this.grpMain = new System.Windows.Forms.GroupBox();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.cmdAdd = new System.Windows.Forms.Button();
			this.lvValues = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.mnuActions = new System.Windows.Forms.ContextMenu();
			this.mnuEdit = new System.Windows.Forms.MenuItem();
			this.mnuRemove = new System.Windows.Forms.MenuItem();
			this.label11 = new System.Windows.Forms.Label();
			this.cmbTransformValue = new System.Windows.Forms.ComboBox();
			this.label10 = new System.Windows.Forms.Label();
			this.txtSubject = new System.Windows.Forms.TextBox();
			this.txtBcc = new System.Windows.Forms.TextBox();
			this.txtCc = new System.Windows.Forms.TextBox();
			this.txtTo = new System.Windows.Forms.TextBox();
			this.txtFrom = new System.Windows.Forms.TextBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.rbText = new System.Windows.Forms.RadioButton();
			this.label6 = new System.Windows.Forms.Label();
			this.rbHTML = new System.Windows.Forms.RadioButton();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.mnuUp = new System.Windows.Forms.MenuItem();
			this.mnuDown = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.grpMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpMain
			// 
			this.grpMain.Controls.Add(this.linkLabel1);
			this.grpMain.Controls.Add(this.cmdAdd);
			this.grpMain.Controls.Add(this.lvValues);
			this.grpMain.Controls.Add(this.label11);
			this.grpMain.Controls.Add(this.cmbTransformValue);
			this.grpMain.Controls.Add(this.label10);
			this.grpMain.Controls.Add(this.txtSubject);
			this.grpMain.Controls.Add(this.txtBcc);
			this.grpMain.Controls.Add(this.txtCc);
			this.grpMain.Controls.Add(this.txtTo);
			this.grpMain.Controls.Add(this.txtFrom);
			this.grpMain.Controls.Add(this.btnSave);
			this.grpMain.Controls.Add(this.txtPassword);
			this.grpMain.Controls.Add(this.txtUsername);
			this.grpMain.Controls.Add(this.label9);
			this.grpMain.Controls.Add(this.label8);
			this.grpMain.Controls.Add(this.label7);
			this.grpMain.Controls.Add(this.rbText);
			this.grpMain.Controls.Add(this.label6);
			this.grpMain.Controls.Add(this.rbHTML);
			this.grpMain.Controls.Add(this.label5);
			this.grpMain.Controls.Add(this.label4);
			this.grpMain.Controls.Add(this.label3);
			this.grpMain.Controls.Add(this.label2);
			this.grpMain.Controls.Add(this.label1);
			this.grpMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpMain.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpMain.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grpMain.Location = new System.Drawing.Point(0, 0);
			this.grpMain.Name = "grpMain";
			this.grpMain.Size = new System.Drawing.Size(476, 440);
			this.grpMain.TabIndex = 0;
			this.grpMain.TabStop = false;
			this.grpMain.Text = "GMail Smtp Settings";
			// 
			// linkLabel1
			// 
			this.linkLabel1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.linkLabel1.Location = new System.Drawing.Point(284, 306);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(24, 16);
			this.linkLabel1.TabIndex = 24;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "[...]";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// cmdAdd
			// 
			this.cmdAdd.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdAdd.Location = new System.Drawing.Point(376, 304);
			this.cmdAdd.Name = "cmdAdd";
			this.cmdAdd.TabIndex = 23;
			this.cmdAdd.Text = "&Add";
			this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
			// 
			// lvValues
			// 
			this.lvValues.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lvValues.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																					   this.columnHeader1});
			this.lvValues.ContextMenu = this.mnuActions;
			this.lvValues.Location = new System.Drawing.Point(16, 332);
			this.lvValues.Name = "lvValues";
			this.lvValues.Size = new System.Drawing.Size(444, 100);
			this.lvValues.TabIndex = 22;
			this.lvValues.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Description";
			this.columnHeader1.Width = 250;
			// 
			// mnuActions
			// 
			this.mnuActions.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.mnuUp,
																					   this.mnuDown,
																					   this.menuItem1,
																					   this.mnuEdit,
																					   this.mnuRemove});
			this.mnuActions.Popup += new System.EventHandler(this.mnuActions_Popup);
			// 
			// mnuEdit
			// 
			this.mnuEdit.Index = 3;
			this.mnuEdit.Text = "&Edit";
			this.mnuEdit.Click += new System.EventHandler(this.mnuEdit_Click);
			// 
			// mnuRemove
			// 
			this.mnuRemove.Index = 4;
			this.mnuRemove.Text = "&Remove";
			this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(16, 308);
			this.label11.Name = "label11";
			this.label11.TabIndex = 21;
			this.label11.Text = "Value";
			// 
			// cmbTransformValue
			// 
			this.cmbTransformValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTransformValue.Location = new System.Drawing.Point(120, 304);
			this.cmbTransformValue.Name = "cmbTransformValue";
			this.cmbTransformValue.Size = new System.Drawing.Size(156, 21);
			this.cmbTransformValue.TabIndex = 20;
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label10.Location = new System.Drawing.Point(16, 284);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(160, 16);
			this.label10.TabIndex = 19;
			this.label10.Text = "Transform Values";
			// 
			// txtSubject
			// 
			this.txtSubject.Location = new System.Drawing.Point(120, 137);
			this.txtSubject.Name = "txtSubject";
			this.txtSubject.Size = new System.Drawing.Size(156, 20);
			this.txtSubject.TabIndex = 18;
			this.txtSubject.Text = "";
			// 
			// txtBcc
			// 
			this.txtBcc.Location = new System.Drawing.Point(120, 109);
			this.txtBcc.Name = "txtBcc";
			this.txtBcc.Size = new System.Drawing.Size(156, 20);
			this.txtBcc.TabIndex = 17;
			this.txtBcc.Text = "";
			// 
			// txtCc
			// 
			this.txtCc.Location = new System.Drawing.Point(120, 81);
			this.txtCc.Name = "txtCc";
			this.txtCc.Size = new System.Drawing.Size(156, 20);
			this.txtCc.TabIndex = 16;
			this.txtCc.Text = "";
			// 
			// txtTo
			// 
			this.txtTo.Location = new System.Drawing.Point(120, 53);
			this.txtTo.Name = "txtTo";
			this.txtTo.Size = new System.Drawing.Size(156, 20);
			this.txtTo.TabIndex = 15;
			this.txtTo.Text = "";
			// 
			// txtFrom
			// 
			this.txtFrom.Location = new System.Drawing.Point(120, 25);
			this.txtFrom.Name = "txtFrom";
			this.txtFrom.Size = new System.Drawing.Size(156, 20);
			this.txtFrom.TabIndex = 14;
			this.txtFrom.Text = "";
			// 
			// btnSave
			// 
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.Location = new System.Drawing.Point(376, 24);
			this.btnSave.Name = "btnSave";
			this.btnSave.TabIndex = 13;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(120, 252);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new System.Drawing.Size(156, 20);
			this.txtPassword.TabIndex = 12;
			this.txtPassword.Text = "";
			// 
			// txtUsername
			// 
			this.txtUsername.Location = new System.Drawing.Point(120, 224);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(156, 20);
			this.txtUsername.TabIndex = 11;
			this.txtUsername.Text = "";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(16, 252);
			this.label9.Name = "label9";
			this.label9.TabIndex = 10;
			this.label9.Text = "Password";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(16, 224);
			this.label8.Name = "label8";
			this.label8.TabIndex = 9;
			this.label8.Text = "Username";
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.Location = new System.Drawing.Point(16, 200);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(160, 16);
			this.label7.TabIndex = 8;
			this.label7.Text = "GMail User Credentials";
			// 
			// rbText
			// 
			this.rbText.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.rbText.Location = new System.Drawing.Point(184, 164);
			this.rbText.Name = "rbText";
			this.rbText.Size = new System.Drawing.Size(48, 24);
			this.rbText.TabIndex = 7;
			this.rbText.Text = "Text";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(16, 165);
			this.label6.Name = "label6";
			this.label6.TabIndex = 6;
			this.label6.Text = "Body format";
			// 
			// rbHTML
			// 
			this.rbHTML.Checked = true;
			this.rbHTML.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.rbHTML.Location = new System.Drawing.Point(120, 164);
			this.rbHTML.Name = "rbHTML";
			this.rbHTML.Size = new System.Drawing.Size(64, 24);
			this.rbHTML.TabIndex = 5;
			this.rbHTML.TabStop = true;
			this.rbHTML.Text = "HTML";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 136);
			this.label5.Name = "label5";
			this.label5.TabIndex = 4;
			this.label5.Text = "Subject";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(16, 108);
			this.label4.Name = "label4";
			this.label4.TabIndex = 3;
			this.label4.Text = "Bcc";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 80);
			this.label3.Name = "label3";
			this.label3.TabIndex = 2;
			this.label3.Text = "Cc";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 52);
			this.label2.Name = "label2";
			this.label2.TabIndex = 1;
			this.label2.Text = "To";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.TabIndex = 0;
			this.label1.Text = "From";
			// 
			// mnuUp
			// 
			this.mnuUp.Index = 0;
			this.mnuUp.Text = "Move &Up";
			this.mnuUp.Click += new System.EventHandler(this.mnuUp_Click);
			// 
			// mnuDown
			// 
			this.mnuDown.Index = 1;
			this.mnuDown.Text = "Move &Down";
			this.mnuDown.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 2;
			this.menuItem1.Text = "-";
			// 
			// GMailSmtpTransformDesigner
			// 
			this.Controls.Add(this.grpMain);
			this.Name = "GMailSmtpTransformDesigner";
			this.Size = new System.Drawing.Size(476, 440);
			this.grpMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public override void LoadTransformEditorValues(int requestIndex, ScriptingApplication scriptingData, WebTransform transform)
		{
			base.LoadTransformEditorValues (requestIndex, scriptingData, transform);

			this.Clear();
			lvValues.Items.Clear();

			GmailSmtpTransform gmtransform = (GmailSmtpTransform)transform;

			if ( gmtransform != null )
			{
				this.txtBcc.Text = gmtransform.Bcc;
				gmtransform.Cc = this.txtCc.Text;
				if ( gmtransform.Format ==  System.Web.Mail.MailFormat.Html )
				{
					this.rbHTML.Checked = true;
				}
				if ( gmtransform.Format ==  System.Web.Mail.MailFormat.Text )
				{
					this.rbText.Checked = true;
				}
				this.txtFrom.Text = gmtransform.From;
				this.txtPassword.Text = gmtransform.Password;
				this.txtUsername.Text = gmtransform.Username;
				this.txtTo.Text = gmtransform.To;
				this.txtSubject.Text = gmtransform.Subject;
			}

			// Load any tranform value, if available
			if ( gmtransform.QueryCommandActions.Length > 0 )
			{				
				foreach ( QueryCommandAction action in gmtransform.QueryCommandActions )
				{
					ListViewItem listItem = new ListViewItem();
					listItem.Text = action.Description;
					listItem.Tag = action;

					lvValues.Items.Add(listItem);
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

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			SaveTransform();
		}

		private GmailSmtpTransform SaveTransform()
		{
			GmailSmtpTransform transform = new GmailSmtpTransform();
			transform.Bcc =  this.txtBcc.Text;
			transform.Cc = this.txtCc.Text;
			if ( this.rbHTML.Checked )
			{
				transform.Format = System.Web.Mail.MailFormat.Html;
			}
			if ( this.rbText.Checked )
			{
				transform.Format = System.Web.Mail.MailFormat.Text;
			}

			transform.From = this.txtFrom.Text;
			transform.Password = this.txtPassword.Text;
			transform.Username = this.txtUsername.Text;
			transform.To = this.txtTo.Text;
			transform.Subject = this.txtSubject.Text;

			foreach ( ListViewItem item in lvValues.Items )
			{												
				transform.AddQueryCommandAction((QueryCommandAction)item.Tag);
			}

			return transform;
		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			_description = ShowTransformValueDialog(this.cmbTransformValue.SelectedIndex);
		}

		private void cmdAdd_Click(object sender, System.EventArgs e)
		{
			UpdateTransformValue();
		}
		private void UpdateTransformValue()
		{
			GmailSmtpTransform transform = (GmailSmtpTransform)base.WebTransform;

			QueryCommandAction action = new QueryCommandAction();
			action.Description = _description;
			action.Value = this.TransformValue;
			action.Name = string.Empty;

			if ( _isEdit )
			{
				ListViewItem updateItem = lvValues.Items[_editIndex];
				updateItem.Text = action.Description;
				updateItem.Tag = action;

				this.cmdAdd.Text = "&Add";

				_isEdit = false;
			} 
			else 
			{
				// Add List View Item
				#region Add List View Item
				ListViewItem listItem = new ListViewItem();
				listItem.Text = action.Description;
				listItem.Tag = action;

				lvValues.Items.Add(listItem);				
				#endregion
			}

			_description = "";
		}

		private void mnuActions_Popup(object sender, System.EventArgs e)
		{
			if ( this.lvValues.Items.Count > 0 )
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
			if ( MessageBox.Show("Are you sure you want to remove the selected query command?",Ecyware.GreenBlue.Utils.AppLocation.ApplicationName,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes )
			{				
				_isEdit = false;

				// remove from view
				lvValues.Items.Remove(lvValues.SelectedItems[0]);
			}		
		}

		public override void Clear()
		{
			base.Clear ();

			lvValues.Items.Clear();			
		}

		private void mnuEdit_Click(object sender, System.EventArgs e)
		{
			QueryCommandAction action = (QueryCommandAction)this.lvValues.SelectedItems[0].Tag;
			
			this.cmdAdd.Text = "&Update";
			_isEdit = true;
			_editIndex = this.lvValues.SelectedIndices[0];

			_description = action.Description;
			this.TransformValue = action.Value;
			
			this.cmbTransformValue.SelectedIndex = GetTransformValueComboIndex(this.TransformValue);		
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			ListViewItem clone = (ListViewItem)this.lvValues.SelectedItems[0].Clone();

			// Get index
			int index = this.lvValues.SelectedItems[0].Index;

			// Remove 
			this.lvValues.SelectedItems[0].Remove();

			// Move Down
			if ( (index + 1) <= lvValues.Items.Count )
			{
				lvValues.Items.Insert(index + 1, clone);
			}		
		}

		private void mnuUp_Click(object sender, System.EventArgs e)
		{
			ListViewItem clone = (ListViewItem)this.lvValues.SelectedItems[0].Clone();

			// Get index
			int index = this.lvValues.SelectedItems[0].Index;

			// Remove 
			this.lvValues.SelectedItems[0].Remove();

			// Move up
			if ( (index - 1) >= 0 )
			{
				lvValues.Items.Insert(index - 1, clone);
			}
		}


		
		public override WebTransform WebTransform
		{
			get
			{
				return SaveTransform();
			}
		}


	}
}
