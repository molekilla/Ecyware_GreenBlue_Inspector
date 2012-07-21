// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: June 2004 - July 2004
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Controls;
using Ecyware.GreenBlue.HtmlDom;
using Ecyware.GreenBlue.HtmlCommand;
using Ecyware.GreenBlue.WebUnitTestManager;
using Ecyware.GreenBlue.WebUnitTestCommand;
using Ecyware.GreenBlue.Protocols.Http;
using Ecyware.GreenBlue.Utils;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Contains the definition for SessionWebUnitTestItemEditor control.
	/// </summary>
	public class SessionWebUnitTestItemEditor : BaseSessionDesignerUserControl
	{
		private SessionRequest _sessionRequest = null;
		private InspectorConfiguration inspectorConfig = null;
		
		private SortedList dataTypesList  = new SortedList();
		private SortedList xssValuesList = new SortedList();
		private SortedList sqlValuesList = new SortedList();
		private System.Windows.Forms.GroupBox groupItems;
		private System.Windows.Forms.RadioButton rbPostQueryData;
		private System.Windows.Forms.RadioButton rbForm;
		private System.Windows.Forms.ListView lstTestManager;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colValue;
		private System.Windows.Forms.RadioButton rbBufferOverflow;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown numBufferLen;
		private System.Windows.Forms.RadioButton rbDataType;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox cmbDataType;
		private System.Windows.Forms.RadioButton rbSqlTest;
		private System.Windows.Forms.RadioButton rbXSSTest;
		private System.Windows.Forms.RadioButton rbPredefined;
		private System.Windows.Forms.ComboBox cmbSqlTestValues;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cmbXssTestValues;
		private System.Windows.Forms.Button btnAddTest;
		private System.Windows.Forms.TextBox txtTestName;
		private System.Windows.Forms.GroupBox grpTestTypes;
		private System.Windows.Forms.ContextMenu mnuTestManager;
		private System.Windows.Forms.MenuItem mnuEditTest;
		private System.Windows.Forms.MenuItem mnuRemoveTest;
		private System.Windows.Forms.Label lblSelectData;
		private System.Windows.Forms.Label lblTestName;
		private System.Windows.Forms.ColumnHeader colArguments;
		private System.Windows.Forms.Label lblPredefined;
		private System.Windows.Forms.RadioButton rbCookies;
		private System.Windows.Forms.ColumnHeader colTestDataType;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new Session Web Unit Test Item Editor.
		/// </summary>
		public SessionWebUnitTestItemEditor()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
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
			this.groupItems = new System.Windows.Forms.GroupBox();
			this.rbCookies = new System.Windows.Forms.RadioButton();
			this.btnAddTest = new System.Windows.Forms.Button();
			this.grpTestTypes = new System.Windows.Forms.GroupBox();
			this.lblPredefined = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.cmbXssTestValues = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.cmbSqlTestValues = new System.Windows.Forms.ComboBox();
			this.rbPredefined = new System.Windows.Forms.RadioButton();
			this.rbXSSTest = new System.Windows.Forms.RadioButton();
			this.rbSqlTest = new System.Windows.Forms.RadioButton();
			this.label4 = new System.Windows.Forms.Label();
			this.cmbDataType = new System.Windows.Forms.ComboBox();
			this.rbDataType = new System.Windows.Forms.RadioButton();
			this.label3 = new System.Windows.Forms.Label();
			this.numBufferLen = new System.Windows.Forms.NumericUpDown();
			this.rbBufferOverflow = new System.Windows.Forms.RadioButton();
			this.lstTestManager = new System.Windows.Forms.ListView();
			this.colName = new System.Windows.Forms.ColumnHeader();
			this.colValue = new System.Windows.Forms.ColumnHeader();
			this.colTestDataType = new System.Windows.Forms.ColumnHeader();
			this.colArguments = new System.Windows.Forms.ColumnHeader();
			this.mnuTestManager = new System.Windows.Forms.ContextMenu();
			this.mnuEditTest = new System.Windows.Forms.MenuItem();
			this.mnuRemoveTest = new System.Windows.Forms.MenuItem();
			this.txtTestName = new System.Windows.Forms.TextBox();
			this.lblTestName = new System.Windows.Forms.Label();
			this.rbForm = new System.Windows.Forms.RadioButton();
			this.rbPostQueryData = new System.Windows.Forms.RadioButton();
			this.lblSelectData = new System.Windows.Forms.Label();
			this.groupItems.SuspendLayout();
			this.grpTestTypes.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numBufferLen)).BeginInit();
			this.SuspendLayout();
			// 
			// groupItems
			// 
			this.groupItems.Controls.Add(this.rbCookies);
			this.groupItems.Controls.Add(this.btnAddTest);
			this.groupItems.Controls.Add(this.grpTestTypes);
			this.groupItems.Controls.Add(this.lstTestManager);
			this.groupItems.Controls.Add(this.txtTestName);
			this.groupItems.Controls.Add(this.lblTestName);
			this.groupItems.Controls.Add(this.rbForm);
			this.groupItems.Controls.Add(this.rbPostQueryData);
			this.groupItems.Controls.Add(this.lblSelectData);
			this.groupItems.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupItems.Location = new System.Drawing.Point(0, 0);
			this.groupItems.Name = "groupItems";
			this.groupItems.Size = new System.Drawing.Size(552, 414);
			this.groupItems.TabIndex = 0;
			this.groupItems.TabStop = false;
			this.groupItems.Text = "Web Unit Test Manager";
			// 
			// rbCookies
			// 
			this.rbCookies.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.rbCookies.Location = new System.Drawing.Point(282, 21);
			this.rbCookies.Name = "rbCookies";
			this.rbCookies.Size = new System.Drawing.Size(60, 24);
			this.rbCookies.TabIndex = 10;
			this.rbCookies.Text = "Cookies";
			this.rbCookies.CheckedChanged += new System.EventHandler(this.rbCookies_CheckedChanged);
			// 
			// btnAddTest
			// 
			this.btnAddTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnAddTest.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnAddTest.Location = new System.Drawing.Point(474, 18);
			this.btnAddTest.Name = "btnAddTest";
			this.btnAddTest.Size = new System.Drawing.Size(72, 23);
			this.btnAddTest.TabIndex = 9;
			this.btnAddTest.Text = "Add";
			this.btnAddTest.Click += new System.EventHandler(this.btnAddTest_Click);
			// 
			// grpTestTypes
			// 
			this.grpTestTypes.Controls.Add(this.lblPredefined);
			this.grpTestTypes.Controls.Add(this.label6);
			this.grpTestTypes.Controls.Add(this.cmbXssTestValues);
			this.grpTestTypes.Controls.Add(this.label5);
			this.grpTestTypes.Controls.Add(this.cmbSqlTestValues);
			this.grpTestTypes.Controls.Add(this.rbPredefined);
			this.grpTestTypes.Controls.Add(this.rbXSSTest);
			this.grpTestTypes.Controls.Add(this.rbSqlTest);
			this.grpTestTypes.Controls.Add(this.label4);
			this.grpTestTypes.Controls.Add(this.cmbDataType);
			this.grpTestTypes.Controls.Add(this.rbDataType);
			this.grpTestTypes.Controls.Add(this.label3);
			this.grpTestTypes.Controls.Add(this.numBufferLen);
			this.grpTestTypes.Controls.Add(this.rbBufferOverflow);
			this.grpTestTypes.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpTestTypes.Location = new System.Drawing.Point(6, 78);
			this.grpTestTypes.Name = "grpTestTypes";
			this.grpTestTypes.Size = new System.Drawing.Size(462, 186);
			this.grpTestTypes.TabIndex = 8;
			this.grpTestTypes.TabStop = false;
			this.grpTestTypes.Text = "Web Unit Test Types";
			// 
			// lblPredefined
			// 
			this.lblPredefined.Location = new System.Drawing.Point(144, 150);
			this.lblPredefined.Name = "lblPredefined";
			this.lblPredefined.Size = new System.Drawing.Size(306, 18);
			this.lblPredefined.TabIndex = 34;
			this.lblPredefined.Text = "This test uses the modified values in the test data.";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(144, 117);
			this.label6.Name = "label6";
			this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label6.Size = new System.Drawing.Size(90, 18);
			this.label6.TabIndex = 33;
			this.label6.Text = "Select signature";
			// 
			// cmbXssTestValues
			// 
			this.cmbXssTestValues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbXssTestValues.DropDownWidth = 250;
			this.cmbXssTestValues.Location = new System.Drawing.Point(240, 114);
			this.cmbXssTestValues.Name = "cmbXssTestValues";
			this.cmbXssTestValues.Size = new System.Drawing.Size(216, 21);
			this.cmbXssTestValues.TabIndex = 32;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(144, 87);
			this.label5.Name = "label5";
			this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label5.Size = new System.Drawing.Size(90, 18);
			this.label5.TabIndex = 31;
			this.label5.Text = "Select signature";
			// 
			// cmbSqlTestValues
			// 
			this.cmbSqlTestValues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSqlTestValues.DropDownWidth = 250;
			this.cmbSqlTestValues.Location = new System.Drawing.Point(240, 84);
			this.cmbSqlTestValues.Name = "cmbSqlTestValues";
			this.cmbSqlTestValues.Size = new System.Drawing.Size(216, 21);
			this.cmbSqlTestValues.TabIndex = 30;
			// 
			// rbPredefined
			// 
			this.rbPredefined.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.rbPredefined.Location = new System.Drawing.Point(18, 144);
			this.rbPredefined.Name = "rbPredefined";
			this.rbPredefined.Size = new System.Drawing.Size(108, 24);
			this.rbPredefined.TabIndex = 11;
			this.rbPredefined.Text = "Predefined";
			// 
			// rbXSSTest
			// 
			this.rbXSSTest.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.rbXSSTest.Location = new System.Drawing.Point(18, 114);
			this.rbXSSTest.Name = "rbXSSTest";
			this.rbXSSTest.Size = new System.Drawing.Size(108, 24);
			this.rbXSSTest.TabIndex = 10;
			this.rbXSSTest.Text = "Cross Site Scripting (XSS)";
			// 
			// rbSqlTest
			// 
			this.rbSqlTest.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.rbSqlTest.Location = new System.Drawing.Point(18, 84);
			this.rbSqlTest.Name = "rbSqlTest";
			this.rbSqlTest.TabIndex = 9;
			this.rbSqlTest.Text = "SQL Injection";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(144, 57);
			this.label4.Name = "label4";
			this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label4.Size = new System.Drawing.Size(78, 18);
			this.label4.TabIndex = 7;
			this.label4.Text = "Select type";
			// 
			// cmbDataType
			// 
			this.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbDataType.Location = new System.Drawing.Point(240, 56);
			this.cmbDataType.Name = "cmbDataType";
			this.cmbDataType.Size = new System.Drawing.Size(216, 21);
			this.cmbDataType.TabIndex = 8;
			// 
			// rbDataType
			// 
			this.rbDataType.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.rbDataType.Location = new System.Drawing.Point(18, 54);
			this.rbDataType.Name = "rbDataType";
			this.rbDataType.TabIndex = 6;
			this.rbDataType.Text = "Data Type";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(144, 27);
			this.label3.Name = "label3";
			this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label3.Size = new System.Drawing.Size(78, 18);
			this.label3.TabIndex = 4;
			this.label3.Text = "Buffer Length";
			// 
			// numBufferLen
			// 
			this.numBufferLen.Location = new System.Drawing.Point(240, 26);
			this.numBufferLen.Maximum = new System.Decimal(new int[] {
																		 1000000,
																		 0,
																		 0,
																		 0});
			this.numBufferLen.Minimum = new System.Decimal(new int[] {
																		 100,
																		 0,
																		 0,
																		 0});
			this.numBufferLen.Name = "numBufferLen";
			this.numBufferLen.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.numBufferLen.Size = new System.Drawing.Size(78, 20);
			this.numBufferLen.TabIndex = 5;
			this.numBufferLen.Value = new System.Decimal(new int[] {
																	   1000,
																	   0,
																	   0,
																	   0});
			// 
			// rbBufferOverflow
			// 
			this.rbBufferOverflow.Checked = true;
			this.rbBufferOverflow.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.rbBufferOverflow.Location = new System.Drawing.Point(18, 24);
			this.rbBufferOverflow.Name = "rbBufferOverflow";
			this.rbBufferOverflow.TabIndex = 0;
			this.rbBufferOverflow.TabStop = true;
			this.rbBufferOverflow.Text = "Buffer Overflow";
			// 
			// lstTestManager
			// 
			this.lstTestManager.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.lstTestManager.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							 this.colName,
																							 this.colValue,
																							 this.colTestDataType,
																							 this.colArguments});
			this.lstTestManager.ContextMenu = this.mnuTestManager;
			this.lstTestManager.FullRowSelect = true;
			this.lstTestManager.LabelEdit = true;
			this.lstTestManager.Location = new System.Drawing.Point(6, 270);
			this.lstTestManager.MultiSelect = false;
			this.lstTestManager.Name = "lstTestManager";
			this.lstTestManager.Size = new System.Drawing.Size(462, 132);
			this.lstTestManager.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lstTestManager.TabIndex = 7;
			this.lstTestManager.View = System.Windows.Forms.View.Details;
			// 
			// colName
			// 
			this.colName.Text = "Test Name";
			this.colName.Width = 130;
			// 
			// colValue
			// 
			this.colValue.Text = "Test Type";
			this.colValue.Width = 130;
			// 
			// colTestDataType
			// 
			this.colTestDataType.Text = "Test Data Type";
			this.colTestDataType.Width = 100;
			// 
			// colArguments
			// 
			this.colArguments.Text = "Arguments";
			this.colArguments.Width = 150;
			// 
			// mnuTestManager
			// 
			this.mnuTestManager.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						   this.mnuEditTest,
																						   this.mnuRemoveTest});
			this.mnuTestManager.Popup += new System.EventHandler(this.mnuTestManager_Popup);
			// 
			// mnuEditTest
			// 
			this.mnuEditTest.Index = 0;
			this.mnuEditTest.Text = "&Edit Test";
			this.mnuEditTest.Visible = false;
			this.mnuEditTest.Click += new System.EventHandler(this.mnuEditTest_Click);
			// 
			// mnuRemoveTest
			// 
			this.mnuRemoveTest.Index = 1;
			this.mnuRemoveTest.Text = "&Remove Test";
			this.mnuRemoveTest.Click += new System.EventHandler(this.mnuRemoveTest_Click);
			// 
			// txtTestName
			// 
			this.txtTestName.Location = new System.Drawing.Point(192, 48);
			this.txtTestName.Name = "txtTestName";
			this.txtTestName.Size = new System.Drawing.Size(252, 20);
			this.txtTestName.TabIndex = 5;
			this.txtTestName.Text = "";
			// 
			// lblTestName
			// 
			this.lblTestName.Location = new System.Drawing.Point(12, 49);
			this.lblTestName.Name = "lblTestName";
			this.lblTestName.Size = new System.Drawing.Size(168, 18);
			this.lblTestName.TabIndex = 4;
			this.lblTestName.Text = "Test Name";
			// 
			// rbForm
			// 
			this.rbForm.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.rbForm.Location = new System.Drawing.Point(372, 21);
			this.rbForm.Name = "rbForm";
			this.rbForm.Size = new System.Drawing.Size(60, 24);
			this.rbForm.TabIndex = 3;
			this.rbForm.Text = "Form";
			this.rbForm.CheckedChanged += new System.EventHandler(this.rbForm_CheckedChanged);
			// 
			// rbPostQueryData
			// 
			this.rbPostQueryData.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.rbPostQueryData.Location = new System.Drawing.Point(192, 21);
			this.rbPostQueryData.Name = "rbPostQueryData";
			this.rbPostQueryData.Size = new System.Drawing.Size(78, 24);
			this.rbPostQueryData.TabIndex = 2;
			this.rbPostQueryData.Text = "Post Data / Query";
			this.rbPostQueryData.CheckedChanged += new System.EventHandler(this.rbPostQueryData_CheckedChanged);
			// 
			// lblSelectData
			// 
			this.lblSelectData.Location = new System.Drawing.Point(12, 24);
			this.lblSelectData.Name = "lblSelectData";
			this.lblSelectData.Size = new System.Drawing.Size(168, 18);
			this.lblSelectData.TabIndex = 0;
			this.lblSelectData.Text = "Select the data to use for testing";
			// 
			// SessionWebUnitTestItemEditor
			// 
			this.Controls.Add(this.groupItems);
			this.Name = "SessionWebUnitTestItemEditor";
			this.Size = new System.Drawing.Size(552, 414);
			this.groupItems.ResumeLayout(false);
			this.grpTestTypes.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numBufferLen)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		#region Properties
		/// <summary>
		/// Gets or sets a session request.
		/// </summary>
		public SessionRequest WebSessionRequest
		{
			get
			{
				return _sessionRequest;
			}
			set
			{
				_sessionRequest = value;
				if ( _sessionRequest.WebUnitTest == null )
				{
					_sessionRequest.WebUnitTest = new UnitTestItem();
				}
			}
		}

		/// <summary>
		/// Gets the test collection for the current session request.
		/// </summary>
		public TestCollection MyTests
		{
			get
			{
				if ( _sessionRequest.WebUnitTest.Tests == null )
				{
					_sessionRequest.WebUnitTest.Tests = new TestCollection();

				} 
				
				return _sessionRequest.WebUnitTest.Tests;
			}
		}

		#endregion
		#region Lists
		/// <summary>
		/// Gets the XSS attacks signatures from file.
		/// </summary>
		/// <returns> A SortedList.</returns>
		private SortedList GetXssValues()
		{
			if ( this.xssValuesList.Count == 0)
			{
				// get file path
				string filePath = Utils.AppLocation.CommonFolder + "\\" + inspectorConfig.XssSignatures;

				StreamReader reader = new StreamReader(filePath);
				XmlTextReader xmlReader = new XmlTextReader(reader);

				// SortedList
				int i=0;
				while (xmlReader.Read())
				{
					if ( Utils.CompareString.Compare(xmlReader.Name,"value") )
					{
						xssValuesList.Add(i,xmlReader.ReadString());
					}

					i++;
				}

				xmlReader.Close();
			}

			return xssValuesList;
		}

		/// <summary>
		/// Gets Data types values.
		/// </summary>
		/// <returns> A SortedList.</returns>
		private SortedList GetDataTypes()
		{
			if ( this.dataTypesList.Count == 0 )
			{
				dataTypesList.Add(DataType.Numeric,"Numeric");
				dataTypesList.Add(DataType.Character,"Character");
				dataTypesList.Add(DataType.Null,"Null");
			}

			return dataTypesList;
		}

		/// <summary>
		/// Gets the SQL injection values from file.
		/// </summary>
		/// <returns> A SortedList.</returns>
		private SortedList GetSqlValues()
		{
			if ( this.sqlValuesList.Count == 0)
			{
				// get file path
				string filePath = Utils.AppLocation.CommonFolder + "\\" + inspectorConfig.SqlSignatures;

				StreamReader reader = new StreamReader(filePath);
				XmlTextReader xmlReader = new XmlTextReader(reader);

				// SortedList
				int i=0;
				while (xmlReader.Read())
				{
					if ( Utils.CompareString.Compare(xmlReader.Name,"value") )
					{
						sqlValuesList.Add(i,xmlReader.ReadString());
					}

					i++;
				}

				xmlReader.Close();
			}

			return sqlValuesList;
		}

		#endregion
		#region Methods

		/// <summary>
		/// Adds a safe test.
		/// </summary>
		private void AddSafeTest()
		{
			if ( !MyTests.ContainsKey("Safe Test") )
			{
				IHtmlFormUnitTestArgs args = null;
				Test test = new Test();

				test.TestType = UnitTestType.SafeTest;
				test.TestTypeName = "Safe Test";
				test.Name = "Safe Test";
				test.Arguments = args;

				// Add safe test
				MyTests.Add(test.Name, test);
			}
		}

		/// <summary>
		/// Loads the test manager environment.
		/// </summary>
		public void LoadTestManager()
		{
			// Clear items
			this.lstTestManager.Items.Clear();

			// load tests
			foreach ( DictionaryEntry de in MyTests )
			{
				Test test = (Test)de.Value;
				AddTestToList(test);
			}

			this.txtTestName.Text = "";
			this.rbBufferOverflow.Checked = true;

			if ( this.WebSessionRequest.RequestType == HttpRequestType.GET )
			{
				// load manager for a GET request
				// Can create tests for Cookies and Url.
				this.rbPostQueryData.Text = "Url";
				this.rbPostQueryData.Checked = true;				
				this.rbForm.Enabled = false;

				this.lblPredefined.Enabled = false;
				this.rbPredefined.Enabled = false;

				this.btnAddTest.Enabled = true;
				this.lblTestName.Enabled = true;
				this.txtTestName.Enabled = true;

				if ( this.WebSessionRequest.RequestCookies.Count == 0 )
				{
					this.rbPostQueryData.Enabled = false;
					this.rbCookies.Enabled = false;
				} else {
					this.rbPostQueryData.Enabled = true;
					this.rbCookies.Enabled = true;
				}
			} else if ( this.WebSessionRequest.RequestType == HttpRequestType.POST ) {
				// Load manager for POST request
				// Can create tests for Cookies, Post Data and Form Data.
				this.lblPredefined.Enabled = true;
				this.rbPredefined.Enabled = true;

				this.lblSelectData.Enabled = true;
				this.rbPostQueryData.Text = "Post Data";
				this.rbPostQueryData.Enabled = true;
				this.rbPostQueryData.Checked = false;
				this.rbForm.Checked = true;
				this.rbForm.Visible = true;
				this.rbForm.Enabled = true;

				this.btnAddTest.Enabled = true;
				this.lblTestName.Enabled = true;
				this.txtTestName.Enabled = true;

				this.grpTestTypes.Enabled = true;

				if ( this.WebSessionRequest.RequestCookies.Count == 0 )
				{
					this.rbCookies.Enabled = false;
				} 
				else 
				{
					this.rbCookies.Enabled = true;
				}
			}
		}

		/// <summary>
		/// Loads the test manager environment.
		/// </summary>
		/// <param name="config"> The InspectorConfiguration settings.</param>
		public void LoadTestManager(InspectorConfiguration config)
		{
			this.LoadTestManager();
			this.inspectorConfig = config;
			
			this.cmbDataType.DataSource = Converter.ConvertToArrayList(this.GetDataTypes());
			this.cmbSqlTestValues.DataSource = Converter.ConvertToArrayList(this.GetSqlValues());
			this.cmbXssTestValues.DataSource = Converter.ConvertToArrayList(this.GetXssValues());
		}


		/// <summary>
		/// Adds a test.
		/// </summary>
		private void AddTest()
		{
			if ( !MyTests.ContainsKey(txtTestName.Text) )
			{
				if ( txtTestName.Text.Length != 0 )
				{
					Test test = CreateWebUnitTest();
					MyTests.Add(test.Name,test);
					AddTestToList(test);
				} 
				else 
				{
					MessageBox.Show("Insert a valid name for the test.","Ecyware GreenBlue Inspector", MessageBoxButtons.OK, MessageBoxIcon.Information);
					this.txtTestName.Focus();
					this.txtTestName.SelectAll();
				}
			} else {
				MessageBox.Show("There is an existing test with the same name, please change the name for the test.","Ecyware GreenBlue Inspector", MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.txtTestName.Focus();
				this.txtTestName.SelectAll();
			}
		}


		#endregion
		#region Test Helpers
		/// <summary>
		/// Creates a test argument.
		/// </summary>
		private Test CreateWebUnitTest()
		{
			IHtmlFormUnitTestArgs args = null;
			Test test = new Test();
			test.UnitTestDataType  = GetUnitTestDataType();

			#region Buffer Overflow Settings
			if ( this.rbBufferOverflow.Checked )
			{
				args = new BufferOverflowTesterArgs();
				((BufferOverflowTesterArgs)args).BufferLength = (int)this.numBufferLen.Value;

				test.TestType = UnitTestType.BufferOverflow;
				test.TestTypeName = "Buffer Overflow Test";
				test.Name = this.txtTestName.Text;
				test.Arguments = args;					
			}
			#endregion

			#region Data Type Settings
			if ( this.rbDataType.Checked )
			{
				args = new DataTypesTesterArgs();
				((DataTypesTesterArgs)args).SelectedDataType = (DataType)dataTypesList.GetKey(dataTypesList.IndexOfValue(this.cmbDataType.Text));

				test.TestType= UnitTestType.DataTypes;
				test.TestTypeName = "Data Type Test";
				test.Name  = this.txtTestName.Text;
				test.Arguments = args;
			}
			#endregion

			#region SQL Injection Settings
			if ( this.rbSqlTest.Checked )
			{
				args = new SqlInjectionTesterArgs();
				((SqlInjectionTesterArgs)args).SqlValue = this.cmbSqlTestValues.Text;

				test.TestType = UnitTestType.SqlInjection;
				test.TestTypeName = "SQL Injection Test";
				test.Name = this.txtTestName.Text;
				test.Arguments = args;
			}
			#endregion

			#region XSS Settings
			if ( this.rbXSSTest.Checked )
			{
				args = new XssInjectionTesterArgs();
				((XssInjectionTesterArgs)args).XssValue = this.cmbXssTestValues.Text;

				test.TestType = UnitTestType.XSS;
				test.TestTypeName = "XSS Injection Test";
				test.Name = this.txtTestName.Text;
				test.Arguments = args;
			}
			#endregion

			#region Predefined Post Data Test
			if ( this.rbPredefined.Checked )
			{
				args = new PredefinedTesterArgs();
				PredefinedTesterArgs predefinedArgs = (PredefinedTesterArgs)args;
				
				if ( this.rbForm.Checked )
				{
					predefinedArgs.FormData = this.WebSessionRequest.Form;
				} 
				else 
				{
					// TODO: Change to PostDataCollection method.
					FormConverter formConverter = new FormConverter();
					predefinedArgs.UserPostData = formConverter.GetPostDataCollection(((PostSessionRequest)this.WebSessionRequest).PostData);
				}

				test.TestType = UnitTestType.Predefined;
				test.TestTypeName = "Predefined Test";
				test.Name = this.txtTestName.Text;
				test.Arguments = args;
			}
			#endregion

			return test;
		}


		/// <summary>
		/// Gets the UnitTestDataType.
		/// </summary>
		/// <returns> Returns the selected unit test data type.</returns>
		private UnitTestDataContainer GetUnitTestDataType()
		{
			UnitTestDataContainer unitTestDataType = UnitTestDataContainer.NoPostData;
			// ----------------------------------------------
			// Check data container type
			// ----------------------------------------------
			if ( rbForm.Checked )
			{
				unitTestDataType = UnitTestDataContainer.HtmlFormTag;			
			}

			if ( rbCookies.Checked )
			{
				unitTestDataType = UnitTestDataContainer.Cookies;
			} 
			
			if ( rbPostQueryData.Checked )
			{
				if ( this.rbPostQueryData.Text.ToLower() == "url" )
				{
					unitTestDataType = UnitTestDataContainer.NoPostData;
				} 
				else 
				{
					unitTestDataType= UnitTestDataContainer.PostDataHashtable;
				}
			}
			// ----------------------------------------------

			return unitTestDataType;
		}


		/// <summary>
		/// Adds a test to the list.
		/// </summary>
		/// <param name="test"> The test to add.</param>
		private void AddTestToList(Test test)
		{
			ListViewItem lv = new ListViewItem();

			string testType = test.TestTypeName;
			lv.Text = test.Name;
			lv.SubItems.Add(testType);

			// Test Data Type display
			switch ( test.UnitTestDataType )
			{
				case UnitTestDataContainer.Cookies:
					lv.SubItems.Add("Cookies");
					break;
				case UnitTestDataContainer.HtmlFormTag:
					lv.SubItems.Add("Form");
					break;
				case UnitTestDataContainer.PostDataHashtable:
					lv.SubItems.Add("Post Data");
					break;
				case UnitTestDataContainer.NoPostData:
					lv.SubItems.Add("Url");
					break;
			}

			// Arguments display
			if ( test.Arguments is BufferOverflowTesterArgs )
			{
				BufferOverflowTesterArgs args = (BufferOverflowTesterArgs)test.Arguments;
				lv.SubItems.Add("Buffer Length:" + args.BufferLength.ToString());
			}
			if ( test.Arguments is DataTypesTesterArgs )
			{
				DataTypesTesterArgs args = (DataTypesTesterArgs)test.Arguments;
				lv.SubItems.Add("Type:" + args.SelectedDataType.ToString() );
			}
			if ( test.Arguments is XssInjectionTesterArgs )
			{
				XssInjectionTesterArgs args = (XssInjectionTesterArgs)test.Arguments;
				lv.SubItems.Add(args.XssValue);
			}
			if ( test.Arguments is SqlInjectionTesterArgs )
			{
				SqlInjectionTesterArgs args = (SqlInjectionTesterArgs)test.Arguments;
				lv.SubItems.Add(args.SqlValue);
			}
						
			// add to list
			lstTestManager.Items.Add(lv);
		}
		#endregion
		#region Control Events
		private void rbCookies_CheckedChanged(object sender, System.EventArgs e)
		{
			this.rbPredefined.Enabled = false;
		}

		private void rbForm_CheckedChanged(object sender, System.EventArgs e)
		{
			this.rbPredefined.Enabled = true;
		}

		private void rbPostQueryData_CheckedChanged(object sender, System.EventArgs e)
		{
			this.rbPredefined.Enabled = true;
		}
		private void mnuTestManager_Popup(object sender, System.EventArgs e)
		{
			if ( lstTestManager.SelectedIndices.Count > 0 )
			{
				//mnuAddTest.Enabled = true;
				mnuEditTest.Enabled = true;
				mnuRemoveTest.Enabled = true;
			}

			if ( lstTestManager.SelectedIndices.Count == 0 )
			{
				//mnuAddTest.Enabled = true;
				mnuEditTest.Enabled = false;
				mnuRemoveTest.Enabled = false;
			}
			
		}

		private void mnuRemoveTest_Click(object sender, System.EventArgs e)
		{
			string testName = lstTestManager.SelectedItems[0].SubItems[0].Text;

			if ( testName != "Safe Test" )
			{
				if ( MessageBox.Show("Are you sure you want to remove the selected test?","Ecyware GreenBlue Inspector",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes )
				{				
				
					// remove from collection
					this.MyTests.Remove(testName);

					// remove from view
					lstTestManager.Items.Remove(lstTestManager.SelectedItems[0]);
				}
			}
		}

		private void btnAddTest_Click(object sender, System.EventArgs e)
		{
			AddTest();
		}
		#endregion
		public override void UpdateSessionRequestData()
		{
			//UpdateSessionRequestArgs updateArgs = new UpdateSessionRequestArgs();
		}

		private void mnuEditTest_Click(object sender, System.EventArgs e)
		{
		
		}



	}
}
