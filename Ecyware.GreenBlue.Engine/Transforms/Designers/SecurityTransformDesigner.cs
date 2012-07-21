using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms.Designers;
using Ecyware.GreenBlue.Engine.Transforms;
using C1.C1Zip;

namespace Ecyware.GreenBlue.Engine.Transforms.Designers
{
	/// <summary>
	/// Summary description for SecurityTransformDesigner.
	/// </summary>
	public class SecurityTransformDesigner : UITransformEditor
	{
		private Cursor tempCursor;
		private TestCollection _currentTests;
		private bool _isEdit = false;
		private int _editIndex = -1;
		private ArrayList _headerList = new ArrayList();

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ContextMenu mnuHeaderActions;
		private System.Windows.Forms.Button btnUpdate;
		private System.Windows.Forms.MenuItem mnuEdit;
		private System.Windows.Forms.MenuItem mnuRemove;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colDescription;
		private System.Windows.Forms.ListView lstActions;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox cmbStateData;
		private System.Windows.Forms.ComboBox cmbUnitTest;
		private System.Windows.Forms.TextBox txtUnitTestTemplate;
		private System.Windows.Forms.OpenFileDialog dlgOpenFile;
		private System.Windows.Forms.ErrorProvider requiredField;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new SecurityTransformDesigner.
		/// </summary>
		public SecurityTransformDesigner()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();			

			if ( this.cmbStateData.Items.Count == 0 )
			{
				cmbStateData.Items.Add(RequestStateDataType.Url.ToString());
				cmbStateData.Items.Add(RequestStateDataType.Cookies.ToString());
				cmbStateData.Items.Add(RequestStateDataType.Form.ToString());
				cmbStateData.Items.Add(RequestStateDataType.PostData.ToString());
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
			this.label5 = new System.Windows.Forms.Label();
			this.txtUnitTestTemplate = new System.Windows.Forms.TextBox();
			this.cmbUnitTest = new System.Windows.Forms.ComboBox();
			this.btnUpdate = new System.Windows.Forms.Button();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbStateData = new System.Windows.Forms.ComboBox();
			this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
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
			this.groupBox1.Location = new System.Drawing.Point(0, 108);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(480, 252);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Security Transform Actions";
			// 
			// lstActions
			// 
			this.lstActions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						 this.colName,
																						 this.colDescription});
			this.lstActions.ContextMenu = this.mnuHeaderActions;
			this.lstActions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstActions.FullRowSelect = true;
			this.lstActions.LabelEdit = true;
			this.lstActions.Location = new System.Drawing.Point(3, 16);
			this.lstActions.MultiSelect = false;
			this.lstActions.Name = "lstActions";
			this.lstActions.Size = new System.Drawing.Size(474, 233);
			this.lstActions.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lstActions.TabIndex = 0;
			this.lstActions.View = System.Windows.Forms.View.Details;
			// 
			// colName
			// 
			this.colName.Text = "Action for Http State";
			this.colName.Width = 220;
			// 
			// colDescription
			// 
			this.colDescription.Text = "Test Value";
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
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.txtUnitTestTemplate);
			this.groupBox2.Controls.Add(this.cmbUnitTest);
			this.groupBox2.Controls.Add(this.btnUpdate);
			this.groupBox2.Controls.Add(this.linkLabel1);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.cmbStateData);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(480, 108);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Security Transform";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(12, 72);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(114, 18);
			this.label5.TabIndex = 5;
			this.label5.Text = "Unit Test";
			// 
			// txtUnitTestTemplate
			// 
			this.txtUnitTestTemplate.Location = new System.Drawing.Point(132, 48);
			this.txtUnitTestTemplate.Name = "txtUnitTestTemplate";
			this.txtUnitTestTemplate.ReadOnly = true;
			this.txtUnitTestTemplate.Size = new System.Drawing.Size(210, 20);
			this.txtUnitTestTemplate.TabIndex = 3;
			this.txtUnitTestTemplate.Text = "";
			// 
			// cmbUnitTest
			// 
			this.cmbUnitTest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbUnitTest.Location = new System.Drawing.Point(132, 72);
			this.cmbUnitTest.Name = "cmbUnitTest";
			this.cmbUnitTest.Size = new System.Drawing.Size(210, 21);
			this.cmbUnitTest.TabIndex = 6;
			this.cmbUnitTest.SelectionChangeCommitted += new System.EventHandler(this.cmbUnitTest_SelectionChangeCommitted);
			// 
			// btnUpdate
			// 
			this.btnUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUpdate.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnUpdate.Location = new System.Drawing.Point(396, 24);
			this.btnUpdate.Name = "btnUpdate";
			this.btnUpdate.TabIndex = 9;
			this.btnUpdate.Text = "Add";
			this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
			// 
			// linkLabel1
			// 
			this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.linkLabel1.Location = new System.Drawing.Point(348, 54);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(30, 18);
			this.linkLabel1.TabIndex = 4;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "[...]";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(114, 18);
			this.label3.TabIndex = 2;
			this.label3.Text = "Unit Test Template";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 20);
			this.label2.TabIndex = 0;
			this.label2.Text = "Http State Data";
			// 
			// cmbStateData
			// 
			this.cmbStateData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbStateData.Location = new System.Drawing.Point(132, 24);
			this.cmbStateData.Name = "cmbStateData";
			this.cmbStateData.Size = new System.Drawing.Size(210, 21);
			this.cmbStateData.TabIndex = 1;
			// 
			// requiredField
			// 
			this.requiredField.ContainerControl = this;
			// 
			// SecurityTransformDesigner
			// 
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "SecurityTransformDesigner";
			this.Size = new System.Drawing.Size(480, 366);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			// txtTransformDescription.Text = ShowTransformValueDialog(this.cmbTransformValue.SelectedIndex);
			OpenTestTemplate();
		}

		#region Methods
		private void LoadUnitTests(TestCollection tests)
		{
			// Clear items
			this.cmbUnitTest.Items.Clear();

			// load tests
			foreach ( DictionaryEntry de in tests )
			{
				this.cmbUnitTest.Items.Add(de.Key);
			}

			_currentTests = tests;
		}

		private void OpenTestTemplate()
		{
			// send this to disk
			System.IO.Stream stream = null;

			dlgOpenFile.CheckFileExists = true;
			dlgOpenFile.InitialDirectory = Application.UserAppDataPath;
			dlgOpenFile.RestoreDirectory = true;
			dlgOpenFile.Filter = "Web Unit Test Template Files (*.gbtt)|*.gbtt";			 
			dlgOpenFile.Title = "Open Web Unit Test Template";

			if ( dlgOpenFile.ShowDialog() == DialogResult.OK )
			{
				Application.DoEvents();
				tempCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;

				// file
				stream = dlgOpenFile.OpenFile();
				if ( stream != null )
				{
					try
					{						
						this.txtUnitTestTemplate.Text = dlgOpenFile.FileName;
						TestCollection tests = OpenTemplate(stream);
						LoadUnitTests(tests);
					}
					catch ( Exception ex )
					{
						MessageBox.Show(ex.Message,AppLocation.ApplicationName,MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}

			if (stream != null)
			{
				Cursor.Current = tempCursor;
				stream.Close();
			}
		}

		/// <summary>
		/// Opens a serialize test collection from a stream.
		/// </summary>
		/// <param name="stream"> The stream to load the TestCollection.</param>
		/// <returns> A TestCollection.</returns>
		public TestCollection OpenTemplate(Stream stream)
		{
			// Decompressed zip
			C1ZStreamReader openZip = new C1ZStreamReader(stream);

			// Desearialize
			BinaryFormatter bf = new BinaryFormatter();
			TestCollection tests = (TestCollection)bf.Deserialize(openZip);
			stream.Close();

			return tests;
		}
		#endregion

		private void btnUpdate_Click(object sender, System.EventArgs e)
		{
			if ( ValidateFields() )
			{
				UpdateSecurityTransform();
			}
		}
		private bool ValidateFields()
		{
			bool result = true;

			if ( this.cmbStateData.SelectedIndex == -1 )
			{
				requiredField.SetError(cmbStateData, "Required field");
				result = false;
			} 
			else 
			{
				requiredField.SetError(cmbStateData, "");
			}

			if ( this.cmbUnitTest.SelectedIndex == -1 )
			{
				requiredField.SetError(cmbUnitTest, "Required field");
				result = false;
			} 
			else 
			{
				requiredField.SetError(cmbUnitTest, "");
			}

//			if ( this.txtTransformDescription.Text.Length == 0 )
//			{
//				requiredField.SetError(txtTransformDescription, "A value needs to be configured");
//				result = false;
//			} 
//			else 
//			{
//				requiredField.SetError(txtTransformDescription, "");
//			}


			return result;
		}

		/// <summary>
		/// Creates a new SecurityTransformValue.
		/// </summary>
		/// <returns> A new SecurityTransformValue based on a Test type.</returns>
		private SecurityTransformValue CreateTestValue()
		{
			SecurityTransformValue transformValue = new SecurityTransformValue();
			Test selectedTest = _currentTests[(string)this.cmbUnitTest.SelectedItem];

			transformValue.Name = selectedTest.Name;
			transformValue.TestType = selectedTest.TestType.ToString();

			switch ( selectedTest.TestType )
			{
				case UnitTestType.BufferOverflow:
					BufferOverflowGenerator generator = new Ecyware.GreenBlue.Engine.BufferOverflowGenerator();
					transformValue.Value = generator.GenerateStringBuffer(((BufferOverflowTesterArgs)selectedTest.Arguments).BufferLength);
					break;
				case UnitTestType.DataTypes:
					transformValue.Value = DataTypeTestHelper.GetDataTypeTestValue(((DataTypesTesterArgs)selectedTest.Arguments).SelectedDataType);
					break;
				case UnitTestType.SqlInjection:
					transformValue.Value = ((SqlInjectionTesterArgs)selectedTest.Arguments).SqlValue;
					break;
				case UnitTestType.XSS:
					transformValue.Value = ((XssInjectionTesterArgs)selectedTest.Arguments).XssValue;
					break;
			}

			return transformValue;
		}
		private void UpdateSecurityTransform()
		{
			SecurityTransform transform = (SecurityTransform)base.WebTransform;
			SecurityTransformAction transformAction = new SecurityTransformAction();

			// Add Security Transform Action
			transformAction.RequestStateDataType = (RequestStateDataType)Enum.Parse(typeof(RequestStateDataType), (string)this.cmbStateData.SelectedItem);	
			transformAction.TestValue = CreateTestValue();
			transformAction.Name = "Test";

			if ( _isEdit )
			{
				ListViewItem updateItem = lstActions.Items[_editIndex];
				updateItem.Text = transformAction.RequestStateDataType.ToString();
				updateItem.SubItems[1].Text  = transformAction.TestValue.Value;
				updateItem.Tag = transformAction;

				this.btnUpdate.Text = "Add";
				_isEdit = false;
			} 
			else 
			{
				// Add List View Item
				#region Add List View Item
				ListViewItem listItem = new ListViewItem();
				listItem.Text = transformAction.RequestStateDataType.ToString();
				listItem.SubItems.Add(transformAction.TestValue.Value);
				listItem.Tag = transformAction;

				lstActions.Items.Add(listItem);	
				#endregion
			}

			// TODO: Clean
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
			SecurityTransformAction transformAction = (SecurityTransformAction)this.lstActions.SelectedItems[0].Tag;
			
			this.btnUpdate.Text = "Update";
			_isEdit = true;
			_editIndex = this.lstActions.SelectedIndices[0];

			int i = this.cmbStateData.FindString(transformAction.RequestStateDataType.ToString());
			cmbStateData.SelectedIndex = i;
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

//			lstActions.Items.Clear();
//			this.txtTransformDescription.Text = "";
//			this.txtHeaderName.Text = "";
		}

		public override void LoadTransformEditorValues(int requestIndex, ScriptingApplication scriptingData, WebTransform transform)
		{
			base.LoadTransformEditorValues (requestIndex, scriptingData, transform);

			this.Clear();
			lstActions.Items.Clear();

			SecurityTransform securityTransform = (SecurityTransform)base.WebTransform;

			// Load any tranform actions, if available
			if ( securityTransform.SecurityTransformActions.Length > 0 )
			{				

				foreach ( SecurityTransformAction action in securityTransform.SecurityTransformActions )
				{
					ListViewItem listItem = new ListViewItem();
					listItem.Text = action.RequestStateDataType.ToString();
					listItem.SubItems.Add(action.TestValue.Value);
					listItem.Tag = action;

					lstActions.Items.Add(listItem);
				}
			}


//			if ( _headerList.Count <= 0 )
//			{
//				// Load the header combo list.
//				_headerList.AddRange(HeaderTransform.GetRestrictedHeaders);				
//
//				foreach ( WebHeader header in base.SessionScripting.WebRequests[base.SelectedWebRequestIndex].RequestHttpSettings.AdditionalHeaders )
//				{
//					_headerList.Add(header.Name);
//				}
//			}

			WebRequest req = base.SessionScripting.WebRequests[base.SelectedWebRequestIndex];
			//LoadHeaderList(_headerList);
			//LoadFormValues(req);
			//LoadCookieNames(req);
		}

		private void cmbUnitTest_SelectionChangeCommitted(object sender, System.EventArgs e)
		{
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
					SecurityTransform transform = (SecurityTransform)base.WebTransform;
					transform.RemoveAllSecurityTransformActions();

					foreach ( ListViewItem item in lstActions.Items )
					{												
						transform.AddSecurityTransformAction((SecurityTransformAction)item.Tag);
					}
				}

				return base.WebTransform;
			}
		}

		#endregion


	}
}
