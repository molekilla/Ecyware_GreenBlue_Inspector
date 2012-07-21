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
	/// Summary description for FillXmlTransformDesigner.
	/// </summary>
	public class FillXmlTransformDesigner : UITransformEditor
	{
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
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colDescription;
		private System.Windows.Forms.ListView lstActions;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ContextMenu mnuActions;
		private System.Windows.Forms.MenuItem mnuEdit;
		private System.Windows.Forms.ErrorProvider requiredField;
		private System.Windows.Forms.TextBox txtLocation;
		private System.Windows.Forms.LinkLabel lnkLoadXPathDialog;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtXmlFieldName;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new FillXmlTransformDesigner.
		/// </summary>
		public FillXmlTransformDesigner()
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
			this.lnkLoadXPathDialog = new System.Windows.Forms.LinkLabel();
			this.txtLocation = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.txtTransformDescription = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.cmbTransformValue = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.requiredField = new System.Windows.Forms.ErrorProvider();
			this.label1 = new System.Windows.Forms.Label();
			this.txtXmlFieldName = new System.Windows.Forms.TextBox();
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
			this.groupBox1.Text = "Xml Fields";
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
			this.lstActions.Size = new System.Drawing.Size(474, 143);
			this.lstActions.TabIndex = 0;
			this.lstActions.View = System.Windows.Forms.View.Details;
			// 
			// colName
			// 
			this.colName.Text = "Xml Field Name";
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
			this.mnuRemove.Text = "Remove";
			this.mnuRemove.Visible = false;
			this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.lnkLoadXPathDialog);
			this.groupBox2.Controls.Add(this.txtLocation);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.txtXmlFieldName);
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
			this.groupBox2.Size = new System.Drawing.Size(480, 198);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Xml Fields Transform";
			// 
			// lnkLoadXPathDialog
			// 
			this.lnkLoadXPathDialog.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lnkLoadXPathDialog.Location = new System.Drawing.Point(324, 50);
			this.lnkLoadXPathDialog.Name = "lnkLoadXPathDialog";
			this.lnkLoadXPathDialog.Size = new System.Drawing.Size(30, 18);
			this.lnkLoadXPathDialog.TabIndex = 11;
			this.lnkLoadXPathDialog.TabStop = true;
			this.lnkLoadXPathDialog.Text = "[...]";
			this.lnkLoadXPathDialog.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkLoadXPathDialog_LinkClicked);
			// 
			// txtLocation
			// 
			this.txtLocation.Location = new System.Drawing.Point(144, 48);
			this.txtLocation.Name = "txtLocation";
			this.txtLocation.Size = new System.Drawing.Size(174, 20);
			this.txtLocation.TabIndex = 10;
			this.txtLocation.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 20);
			this.label2.TabIndex = 2;
			this.label2.Text = "Location";
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
			this.linkLabel1.Location = new System.Drawing.Point(324, 78);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(30, 18);
			this.linkLabel1.TabIndex = 6;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "[...]";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// txtTransformDescription
			// 
			this.txtTransformDescription.Location = new System.Drawing.Point(144, 102);
			this.txtTransformDescription.Multiline = true;
			this.txtTransformDescription.Name = "txtTransformDescription";
			this.txtTransformDescription.ReadOnly = true;
			this.txtTransformDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtTransformDescription.Size = new System.Drawing.Size(210, 84);
			this.txtTransformDescription.TabIndex = 8;
			this.txtTransformDescription.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 102);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(132, 24);
			this.label4.TabIndex = 7;
			this.label4.Text = "Value Description";
			// 
			// cmbTransformValue
			// 
			this.cmbTransformValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTransformValue.Location = new System.Drawing.Point(144, 72);
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
			// requiredField
			// 
			this.requiredField.ContainerControl = this;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Field Name";
			// 
			// txtXmlFieldName
			// 
			this.txtXmlFieldName.Location = new System.Drawing.Point(144, 24);
			this.txtXmlFieldName.Name = "txtXmlFieldName";
			this.txtXmlFieldName.Size = new System.Drawing.Size(210, 20);
			this.txtXmlFieldName.TabIndex = 1;
			this.txtXmlFieldName.Text = "";
			// 
			// FillXmlTransformDesigner
			// 
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "FillXmlTransformDesigner";
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
			if ( ValidateFields() )
			{
				UpdateXmlField();
			}
		}
		private bool ValidateFields()
		{
			bool result = true;
			if ( this.txtXmlFieldName.Text.Length == 0 )
			{
				requiredField.SetError(txtXmlFieldName, "Required field");
				result = false;
			} 
			else 
			{
				requiredField.SetError(txtXmlFieldName, "");
			}

			if ( this.txtTransformDescription.Text.Length == 0 )
			{
				requiredField.SetError(txtTransformDescription, "A value needs to be configured");
				result = false;
			} 
			else 
			{
				requiredField.SetError(txtTransformDescription, "");
			}


			return result;
		}

		private void UpdateXmlField()
		{
			FillXmlTransform transform = (FillXmlTransform)base.WebTransform;
			XmlElementField field = new XmlElementField();

			// Add Xml Element Field			
			field.Name = this.txtXmlFieldName.Text;
			field.Location = this.txtLocation.Text;
			field.TransformValue = TransformValue;
			field.Description = this.txtTransformDescription.Text;// + " to fill form field \"" + this.txtFormField.Text + "\"";

			if ( _isEdit )
			{
				ListViewItem updateItem = lstActions.Items[_editIndex];
				updateItem.Text = field.Name;
				updateItem.SubItems[1].Text = field.Description;
				updateItem.Tag = field;

				this.btnUpdate.Text = "Add";
				_isEdit = false;
			} 
			else 
			{
				// Add List View Item
				#region Add List View Item
				ListViewItem listItem = new ListViewItem();
				listItem.Text = field.Name;
				listItem.SubItems.Add(field.Description);
				listItem.Tag = field;

				lstActions.Items.Add(listItem);	
				#endregion
			}

				
			this.txtXmlFieldName.Text = "";
			this.txtTransformDescription.Text = "";
			this.txtLocation.Text = "";
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
				
		private void mnuRemove_Click(object sender, System.EventArgs e)
		{
			//string testName = lstActions.SelectedItems[0].SubItems[0].Text;

			if ( MessageBox.Show("Are you sure you want to remove the selected field?",AppLocation.ApplicationName,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes )
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
			this.txtXmlFieldName.Text = "";
			this.txtTransformDescription.Text = "";
			this.txtLocation.Text = "";
		}

		/// <summary>
		/// Updates the xml element.
		/// </summary>
		/// <param name="request"> The current web request.</param>
		private void UpdateXmlElement(WebRequest request)
		{
			if ( request.RequestType == HttpRequestType.POST )
			{
				request.UpdateXmlEnvelope(((PostWebRequest)request).PostData);
			}
			if ( request.RequestType == HttpRequestType.PUT )
			{
				request.UpdateXmlEnvelope(((PutWebRequest)request).PostData);
			}
		}
		public override void LoadTransformEditorValues(int requestIndex, ScriptingApplication scriptingData, WebTransform transform)
		{
			base.LoadTransformEditorValues (requestIndex, scriptingData, transform);
			UpdateXmlElement(scriptingData.WebRequests[requestIndex]);

			this.Clear();
			lstActions.Items.Clear();

			FillXmlTransform xmlFieldTransform = (FillXmlTransform)base.WebTransform;

			// Load any tranform actions, if available
			if ( xmlFieldTransform.XmlElementFields.Length > 0 )
			{				
				foreach ( XmlElementField field in xmlFieldTransform.XmlElementFields )
				{
					ListViewItem listItem = new ListViewItem();
					listItem.Text = field.Name;
					listItem.SubItems.Add(field.Description);
					listItem.Tag = field;

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

		private void mnuEdit_Click(object sender, System.EventArgs e)
		{
			XmlElementField field = (XmlElementField)this.lstActions.SelectedItems[0].Tag;
			
			this.btnUpdate.Text = "Update";
			_isEdit = true;
			_editIndex = this.lstActions.SelectedIndices[0];

			this.txtTransformDescription.Text = field.Description;
			this.txtXmlFieldName.Text = field.Name;		
			this.TransformValue = field.TransformValue;
			this.txtLocation.Text = field.Location;			
			
			this.cmbTransformValue.SelectedIndex = GetTransformValueComboIndex(this.TransformValue);
		}

		private void lnkLoadXPathDialog_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			Utils.XmlXpathDialog dialog = new Ecyware.GreenBlue.Utils.XmlXpathDialog();
			
			if ( base.SessionScripting.WebRequests[base.SelectedWebRequestIndex].XmlEnvelope != null )
			{
				dialog.XmlString = base.SessionScripting.WebRequests[base.SelectedWebRequestIndex].XmlEnvelope.OuterXml;
			
				if ( dialog.ShowDialog() == DialogResult.OK )
				{
					this.txtLocation.Text = dialog.XPathLocation;
				}
			} 
			else 
			{
				MessageBox.Show("The selected web request has no Xml data.", AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}

			dialog.Close();
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
					FillXmlTransform transform = (FillXmlTransform)base.WebTransform;
					transform.RemoveAllFields();

					foreach ( ListViewItem item in lstActions.Items )
					{												
						transform.AddXmlElementField((XmlElementField)item.Tag);
					}
				}

				return base.WebTransform;
			}
		}

		#endregion


	}
}
