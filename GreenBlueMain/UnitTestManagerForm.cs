using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;
using Ecyware.GreenBlue.WebUnitTestManager;
using Ecyware.GreenBlue.WebUnitTestCommand;
using Ecyware.GreenBlue.HtmlDom;
using Ecyware.GreenBlue.Protocols.Http;
using Ecyware.GreenBlue.Utils;
using Ecyware.GreenBlue.Configuration.Inspector;


namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Summary description for UnitTestManagerForm.
	/// </summary>
	public class UnitTestManagerForm : System.Windows.Forms.Form
	{
		private InspectorConfiguration inspectorConfig = null;
		private UnitTestSession _session = null;
		private ResponseBuffer _sessionData = null;
		private HtmlFormTag _formTag = null;
		private TestCollection _tests = new TestCollection();
		private SortedList onErrorList = new SortedList();
		private SortedList onSuccessList = new SortedList();


		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblUrl;
		private System.Windows.Forms.Label lblFormName;
		private System.Windows.Forms.ContextMenu mnuTestManager;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem mnuAddTest;
		private System.Windows.Forms.MenuItem mnuEditTest;
		private System.Windows.Forms.MenuItem mnuRemoveTest;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colValue;
		private System.Windows.Forms.ListView lstTestManager;
		private System.Windows.Forms.MenuItem mnuExit;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtSelectedUnitTestInformation;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Form that manages unit tests for a form.
		/// </summary>
		/// <param name="form"></param>
		/// <param name="sessionData"></param>
		public UnitTestManagerForm(HtmlFormTag form, ResponseBuffer sessionData, InspectorConfiguration config)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.inspectorConfig = config;
			this.CurrentForm = form;
			this.lblFormName.Text = form.Name;
			this.lblUrl.Text = ((Uri)sessionData.RequestHeaderCollection["Request Uri"]).AbsoluteUri;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UnitTestManagerForm));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblFormName = new System.Windows.Forms.Label();
			this.lblUrl = new System.Windows.Forms.Label();
			this.mnuTestManager = new System.Windows.Forms.ContextMenu();
			this.mnuAddTest = new System.Windows.Forms.MenuItem();
			this.mnuEditTest = new System.Windows.Forms.MenuItem();
			this.mnuRemoveTest = new System.Windows.Forms.MenuItem();
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.mnuExit = new System.Windows.Forms.MenuItem();
			this.lstTestManager = new System.Windows.Forms.ListView();
			this.colName = new System.Windows.Forms.ColumnHeader();
			this.colValue = new System.Windows.Forms.ColumnHeader();
			this.txtSelectedUnitTestInformation = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(24, 18);
			this.label1.TabIndex = 2;
			this.label1.Text = "Url:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(336, 6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(84, 18);
			this.label2.TabIndex = 3;
			this.label2.Text = "Selected Form:";
			// 
			// lblFormName
			// 
			this.lblFormName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblFormName.Location = new System.Drawing.Point(426, 6);
			this.lblFormName.Name = "lblFormName";
			this.lblFormName.Size = new System.Drawing.Size(138, 18);
			this.lblFormName.TabIndex = 4;
			this.lblFormName.Text = "Form1";
			// 
			// lblUrl
			// 
			this.lblUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblUrl.Location = new System.Drawing.Point(36, 6);
			this.lblUrl.Name = "lblUrl";
			this.lblUrl.Size = new System.Drawing.Size(294, 18);
			this.lblUrl.TabIndex = 5;
			this.lblUrl.Text = "www.test.com";
			// 
			// mnuTestManager
			// 
			this.mnuTestManager.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						   this.mnuAddTest,
																						   this.mnuEditTest,
																						   this.mnuRemoveTest});
			this.mnuTestManager.Popup += new System.EventHandler(this.mnuTestManager_Popup);
			// 
			// mnuAddTest
			// 
			this.mnuAddTest.Index = 0;
			this.mnuAddTest.Text = "&Add Test";
			this.mnuAddTest.Click += new System.EventHandler(this.mnuAddTest_Click);
			// 
			// mnuEditTest
			// 
			this.mnuEditTest.Index = 1;
			this.mnuEditTest.Text = "&Edit Test";
			this.mnuEditTest.Click += new System.EventHandler(this.mnuEditTest_Click);
			// 
			// mnuRemoveTest
			// 
			this.mnuRemoveTest.Index = 2;
			this.mnuRemoveTest.Text = "&Remove Test";
			this.mnuRemoveTest.Click += new System.EventHandler(this.mnuRemoveTest_Click);
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
																					  this.mnuExit});
			this.menuItem1.Text = "&Actions";
			// 
			// mnuExit
			// 
			this.mnuExit.Index = 0;
			this.mnuExit.Text = "Save and &Exit";
			this.mnuExit.Click += new System.EventHandler(this.mnuExit_Click);
			// 
			// lstTestManager
			// 
			this.lstTestManager.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							 this.colName,
																							 this.colValue});
			this.lstTestManager.ContextMenu = this.mnuTestManager;
			this.lstTestManager.FullRowSelect = true;
			this.lstTestManager.GridLines = true;
			this.lstTestManager.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lstTestManager.LabelEdit = true;
			this.lstTestManager.Location = new System.Drawing.Point(6, 30);
			this.lstTestManager.MultiSelect = false;
			this.lstTestManager.Name = "lstTestManager";
			this.lstTestManager.Size = new System.Drawing.Size(564, 144);
			this.lstTestManager.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lstTestManager.TabIndex = 6;
			this.lstTestManager.View = System.Windows.Forms.View.Details;
			this.lstTestManager.Click += new System.EventHandler(this.lstTestManager_Click);
			// 
			// colName
			// 
			this.colName.Text = "Name";
			this.colName.Width = 170;
			// 
			// colValue
			// 
			this.colValue.Text = "Type";
			this.colValue.Width = 200;
			// 
			// txtSelectedUnitTestInformation
			// 
			this.txtSelectedUnitTestInformation.BackColor = System.Drawing.Color.White;
			this.txtSelectedUnitTestInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.txtSelectedUnitTestInformation.Location = new System.Drawing.Point(6, 198);
			this.txtSelectedUnitTestInformation.Multiline = true;
			this.txtSelectedUnitTestInformation.Name = "txtSelectedUnitTestInformation";
			this.txtSelectedUnitTestInformation.ReadOnly = true;
			this.txtSelectedUnitTestInformation.Size = new System.Drawing.Size(564, 54);
			this.txtSelectedUnitTestInformation.TabIndex = 7;
			this.txtSelectedUnitTestInformation.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(6, 180);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(336, 18);
			this.label4.TabIndex = 8;
			this.label4.Text = "Selected Unit Test Information";
			// 
			// UnitTestManagerForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(574, 259);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.txtSelectedUnitTestInformation);
			this.Controls.Add(this.lstTestManager);
			this.Controls.Add(this.lblUrl);
			this.Controls.Add(this.lblFormName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.Menu = this.mainMenu1;
			this.MinimizeBox = false;
			this.Name = "UnitTestManagerForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Unit Test Manager";
			this.Load += new System.EventHandler(this.UnitTestManagerForm_Load);
			this.ResumeLayout(false);

		}
		#endregion
		#region Properties
		/// <summary>
		/// Gets or sets the current unit test session.
		/// </summary>
		public UnitTestSession CurrentUnitTestSession
		{
			get
			{
				return _session;
			}
			set
			{
				_session = value;
			}
		}
		/// <summary>
		/// Get or sets the current ResponseBuffer.
		/// </summary>
		public ResponseBuffer CurrentSessionData
		{
			get
			{
				return _sessionData;
			}
			set
			{
				_sessionData = value;
			}
		}
		/// <summary>
		/// Gets or sets the current HtmlFormTag.
		/// </summary>
		public HtmlFormTag CurrentForm
		{
			get
			{
				return _formTag;
			}
			set
			{
				_formTag = value;
			}
		}

		private TestCollection FormTests
		{
			get
			{
				return _tests;
			}
			set
			{
				_tests = value;
			}
		}
		#endregion
		#region Methods
		private void SaveUnitTests()
		{
			string key = "uti_" + this.CurrentForm.Name;
			UnitTestItem uti = new UnitTestItem(this.CurrentForm,this.FormTests);

			if ( !this.CurrentUnitTestSession.UnitTestForms.ContainsKey(key) )
			{
				this.CurrentUnitTestSession.UnitTestForms.Add(key,uti);
			} 
			else 
			{
				// remove and then add current
				this.CurrentUnitTestSession.UnitTestForms.Remove(key);

				this.CurrentUnitTestSession.UnitTestForms.Add(key,uti);
			}
		}

		private void AddOtherInformation(Test test)
		{
			switch ( test.TestType )
			{
				case HtmlFormTest.BufferOverflow:
					BufferOverflowGenerator gen = new BufferOverflowGenerator();
					string buffer = gen.GenerateStringBuffer(((BufferOverflowTesterArgs)test.Arguments).BufferLength);
					this.txtSelectedUnitTestInformation.Text = "Buffer example: " + buffer.Substring(0,((BufferOverflowTesterArgs)test.Arguments).BufferLength);
					break;
				case HtmlFormTest.DataTypes:
					DataTypesTesterArgs args = (DataTypesTesterArgs)test.Arguments;

					if ( args.SelectedDataType == DataType.Character )
					{
						this.txtSelectedUnitTestInformation.Text = "Tests a forms for character type exploits in each form value.";
					}
					if ( args.SelectedDataType == DataType.Null )
					{
						this.txtSelectedUnitTestInformation.Text = "Tests a form for null values exploits in each form value.";
					}
					if ( args.SelectedDataType == DataType.Numeric )
					{
						this.txtSelectedUnitTestInformation.Text = "Tests a form for numeric type exploits in each form value.";
					}
					break;
				case HtmlFormTest.Predefined:
					Ecyware.GreenBlue.HtmlProcessor.HtmlParser parser = new Ecyware.GreenBlue.HtmlProcessor.HtmlParser();
					ArrayList al = parser.ConvertToArrayList(((PredefinedTesterArgs)test.Arguments).FormData);
					StringBuilder data = new StringBuilder();
					data.Append("Predefined test data to post: ?");
					for (int i=0;i<al.Count;i++)
					{
						data.Append(al[i]);
						data.Append("&");
					}

					this.txtSelectedUnitTestInformation.Text = data.ToString();
					break;
				case HtmlFormTest.SqlInjection:
					this.txtSelectedUnitTestInformation.Text = "SQL Injection test value: " + ((SqlInjectionTesterArgs)test.Arguments).SqlValue;
					break;
				case HtmlFormTest.XSS:
					this.txtSelectedUnitTestInformation.Text = "XSS Injection test value: " + ((XssInjectionTesterArgs)test.Arguments).XssValue;
					break;
			}
		}

		private void UpdateTest(UnitTestManagerEditForm editForm)
		{
			if ( _tests.ContainsKey(editForm.TestName) )
			{
				// update
				Test test = this.FormTests[editForm.TestName];
				test.TestType = editForm.HtmlFormTestType;
				test.Arguments = editForm.TestArgs;

				if ( test.TestType == HtmlFormTest.Predefined )
				{
					((PredefinedTesterArgs)test.Arguments).FormData = this.CurrentForm;
				}
				test.TestTypeName = editForm.TestType;
			} 
			else 
			{
				// add 
				Test test = new Test();
				test.Name=editForm.TestName;
				test.TestType = editForm.HtmlFormTestType;
				test.Arguments = editForm.TestArgs;
				
				if ( test.TestType == HtmlFormTest.Predefined )
				{
					((PredefinedTesterArgs)test.Arguments).FormData = this.CurrentForm;
				}

				test.TestTypeName = editForm.TestType;
			
				this.FormTests.Add(test.Name,test);
			}
		}

		#endregion
		#region Menus
		private void mnuExit_Click(object sender, System.EventArgs e)
		{
			SaveUnitTests();
			this.Close();
		}

		private void mnuSaveFormTests_Click(object sender, System.EventArgs e)
		{	
			//MessageBox.Show("Web Unit Test Session Saved!","GreenBlue Message");
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

		private void mnuAddTest_Click(object sender, System.EventArgs e)
		{
			string caption = "Add Test";
			UnitTestManagerEditForm editingDialog = new UnitTestManagerEditForm(caption,this.inspectorConfig);

			if ( editingDialog.ShowDialog() == DialogResult.OK )
			{

				this.UpdateTest(editingDialog);
				
				ListViewItem item = new ListViewItem();
				item.Text=editingDialog.TestName;
				item.SubItems.Add(editingDialog.TestType);
				AddOtherInformation(_tests[editingDialog.TestName]);

				lstTestManager.Items.Add(item);
			}

			editingDialog.Close();
		}

		private void mnuEditTest_Click(object sender, System.EventArgs e)
		{
			string caption = "Edit Test";
			UnitTestManagerEditForm editingDialog = new UnitTestManagerEditForm(caption, inspectorConfig);

			editingDialog.TestName = lstTestManager.SelectedItems[0].SubItems[0].Text;		
			editingDialog.TestType = lstTestManager.SelectedItems[0].SubItems[1].Text;

			// populate other values
			editingDialog.TestArgs = this.FormTests.GetByIndex(lstTestManager.SelectedIndices[0]).Arguments;

			if ( editingDialog.ShowDialog() == DialogResult.OK )
			{
				this.UpdateTest(editingDialog);
			
				ListViewItem item = lstTestManager.SelectedItems[0];				
				item.SubItems.Clear();
				item.Text = editingDialog.TestName;
				item.SubItems.Add(editingDialog.TestType);

				AddOtherInformation(_tests[editingDialog.TestName]);
			}

			editingDialog.Close();
		
		}

		private void mnuRemoveTest_Click(object sender, System.EventArgs e)
		{

			if ( MessageBox.Show("Are you sure you want to remove the selected item?","GreenBlue Message",MessageBoxButtons.YesNo) == DialogResult.Yes )
			{
				string testName = lstTestManager.SelectedItems[0].SubItems[0].Text;
				
				// remove from collection
				this.FormTests.Remove(testName);

				// remove from view
				lstTestManager.Items.Remove(lstTestManager.SelectedItems[0]);
			}
		
		}

		#endregion
		#region Obsolete! UnitTestManagerEditForm array lists
		private SortedList GetOnSuccessCollection()
		{
			if ( this.onSuccessList.Count == 0 )
			{
				onSuccessList.Add(TestOnSuccessAction.ContinueAndSave,"Continue and save response");
				onSuccessList.Add(TestOnSuccessAction.ContinueNoSave,"Continue without saving");
				onSuccessList.Add(TestOnSuccessAction.End,"End");
			}

			return onSuccessList;
		}

		private SortedList GetOnErrorCollection()
		{
			if ( this.onErrorList.Count == 0 )
			{
				onErrorList.Add(TestOnErrorAction.SaveAndEnd,"End and save response");
				onErrorList.Add(TestOnErrorAction.SaveAndContinue,"Save and continue to next unit test");
				onErrorList.Add(TestOnErrorAction.EndNoSave,"End without saving");
			}

			return onErrorList;
		}

		private SortedList GetTypes()
		{
			if ( this.onSuccessList.Count == 0 )
			{
				onSuccessList.Add(TestOnSuccessAction.ContinueAndSave,"Continue and save response");
				onSuccessList.Add(TestOnSuccessAction.ContinueNoSave,"Continue without saving");
				onSuccessList.Add(TestOnSuccessAction.End,"End");
			}

			return onSuccessList;
		}
		#endregion

		private void UnitTestManagerForm_Load(object sender, System.EventArgs e)
		{
			if ( this.CurrentUnitTestSession.UnitTestForms.Count > 0 )
			{
				if ( this.CurrentUnitTestSession.UnitTestForms.ContainsKey("uti_" + this.CurrentForm.Name) )
				{
					// load tests
					TestCollection tests = this.CurrentUnitTestSession.UnitTestForms["uti_" + this.CurrentForm.Name].Tests;
					this.FormTests = tests;

					for (int i=0;i<this.FormTests.Count;i++)
					{
						Test t = FormTests.GetByIndex(i);
						ListViewItem lv = new ListViewItem();

						string testType = t.TestTypeName;
						//string errorString = (string)this.GetOnErrorCollection()[t.OnErrorValue];
						//string successString = (string)this.GetOnSuccessCollection()[t.OnSuccessValue];
						lv.Text = t.Name;
						lv.SubItems.Add(testType);
						
						// TODO: Populate
						lstTestManager.Items.Add(lv);
					}
				}
			}
		}

		private void lstTestManager_Click(object sender, System.EventArgs e)
		{
			string testName = lstTestManager.SelectedItems[0].SubItems[0].Text;

			// get test
			Test test = _tests[testName];

			AddOtherInformation(test);
		}


	}
}
