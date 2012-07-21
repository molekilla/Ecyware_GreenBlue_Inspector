using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Configuration;
using Ecyware.GreenBlue.WebUnitTestManager;
using Ecyware.GreenBlue.Configuration.Inspector;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Summary description for UnitTestManagerEditForm.
	/// </summary>
	public class UnitTestManagerEditForm : System.Windows.Forms.Form
	{
		private InspectorConfiguration inspectorConfig = null;

		private SortedList dataTypesList  = new SortedList();
		private SortedList sqlValuesList = new SortedList();
		private SortedList xssValuesList = new SortedList();

		private string _testName = string.Empty;
		private string _testType = string.Empty;
		private UnitTestType _htmlFormTestType;
		private IHtmlFormUnitTestArgs _testArgs = null;

		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.NumericUpDown numBufferLen;
		private System.Windows.Forms.ComboBox cmbDataType;
		private System.Windows.Forms.TabPage tabBufferOverflow;
		private System.Windows.Forms.TabPage tabDataTypes;
		private System.Windows.Forms.TabPage tabSqlInjection;
		private System.Windows.Forms.TabPage tabXssInjection;
		private System.Windows.Forms.TabPage tabPredefinedTest;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cmbSqlTestValues;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txtBufferTestName;
		private System.Windows.Forms.TextBox txtDataTypeTestName;
		private System.Windows.Forms.TextBox txtSqlTestName;
		private System.Windows.Forms.TextBox txtXssTestName;
		private System.Windows.Forms.TextBox txtPredfinedTestName;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TabControl tabTests;
		private System.Windows.Forms.ComboBox cmbXssTestValues;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Unit Test Editor Form.
		/// </summary>
		public UnitTestManagerEditForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

		}

		/// <summary>
		/// Loads the unit test editor form.
		/// </summary>
		/// <param name="label"> Title to display in form.</param>
		/// <param name="config"> The Inspector Workspace configuration.</param>
		public UnitTestManagerEditForm(string label, InspectorConfiguration config) : this()
		{
			this.Text = label;
			this.inspectorConfig = config;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(UnitTestManagerEditForm));
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.tabTests = new System.Windows.Forms.TabControl();
			this.tabBufferOverflow = new System.Windows.Forms.TabPage();
			this.label2 = new System.Windows.Forms.Label();
			this.numBufferLen = new System.Windows.Forms.NumericUpDown();
			this.txtBufferTestName = new System.Windows.Forms.TextBox();
			this.tabDataTypes = new System.Windows.Forms.TabPage();
			this.txtDataTypeTestName = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.cmbDataType = new System.Windows.Forms.ComboBox();
			this.tabSqlInjection = new System.Windows.Forms.TabPage();
			this.txtSqlTestName = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.cmbSqlTestValues = new System.Windows.Forms.ComboBox();
			this.tabXssInjection = new System.Windows.Forms.TabPage();
			this.txtXssTestName = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.cmbXssTestValues = new System.Windows.Forms.ComboBox();
			this.tabPredefinedTest = new System.Windows.Forms.TabPage();
			this.txtPredfinedTestName = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.groupBox2.SuspendLayout();
			this.tabTests.SuspendLayout();
			this.tabBufferOverflow.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numBufferLen)).BeginInit();
			this.tabDataTypes.SuspendLayout();
			this.tabSqlInjection.SuspendLayout();
			this.tabXssInjection.SuspendLayout();
			this.tabPredefinedTest.SuspendLayout();
			this.SuspendLayout();
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 12);
			this.label1.Name = "label1";
			this.label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label1.Size = new System.Drawing.Size(48, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Name:";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnOK);
			this.groupBox2.Controls.Add(this.btnCancel);
			this.groupBox2.Controls.Add(this.tabTests);
			this.groupBox2.Location = new System.Drawing.Point(6, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(486, 154);
			this.groupBox2.TabIndex = 22;
			this.groupBox2.TabStop = false;
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(312, 126);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "&OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(396, 126);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "&Cancel";
			// 
			// tabTests
			// 
			this.tabTests.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tabTests.Controls.Add(this.tabBufferOverflow);
			this.tabTests.Controls.Add(this.tabDataTypes);
			this.tabTests.Controls.Add(this.tabSqlInjection);
			this.tabTests.Controls.Add(this.tabXssInjection);
			this.tabTests.Controls.Add(this.tabPredefinedTest);
			this.tabTests.Location = new System.Drawing.Point(6, 12);
			this.tabTests.Name = "tabTests";
			this.tabTests.SelectedIndex = 0;
			this.tabTests.Size = new System.Drawing.Size(468, 108);
			this.tabTests.TabIndex = 1;
			this.tabTests.SelectedIndexChanged += new System.EventHandler(this.tabTests_SelectedIndexChanged);
			// 
			// tabBufferOverflow
			// 
			this.tabBufferOverflow.Controls.Add(this.label2);
			this.tabBufferOverflow.Controls.Add(this.numBufferLen);
			this.tabBufferOverflow.Controls.Add(this.label1);
			this.tabBufferOverflow.Controls.Add(this.txtBufferTestName);
			this.tabBufferOverflow.Location = new System.Drawing.Point(4, 22);
			this.tabBufferOverflow.Name = "tabBufferOverflow";
			this.tabBufferOverflow.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.tabBufferOverflow.Size = new System.Drawing.Size(460, 82);
			this.tabBufferOverflow.TabIndex = 0;
			this.tabBufferOverflow.Text = "Buffer Overflow";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 36);
			this.label2.Name = "label2";
			this.label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label2.Size = new System.Drawing.Size(78, 18);
			this.label2.TabIndex = 1;
			this.label2.Text = "Buffer Length:";
			// 
			// numBufferLen
			// 
			this.numBufferLen.Location = new System.Drawing.Point(84, 34);
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
			this.numBufferLen.TabIndex = 3;
			this.numBufferLen.Value = new System.Decimal(new int[] {
																	   1000,
																	   0,
																	   0,
																	   0});
			// 
			// txtBufferTestName
			// 
			this.txtBufferTestName.Location = new System.Drawing.Point(84, 10);
			this.txtBufferTestName.Name = "txtBufferTestName";
			this.txtBufferTestName.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtBufferTestName.Size = new System.Drawing.Size(156, 20);
			this.txtBufferTestName.TabIndex = 2;
			this.txtBufferTestName.Text = "";
			// 
			// tabDataTypes
			// 
			this.tabDataTypes.Controls.Add(this.txtDataTypeTestName);
			this.tabDataTypes.Controls.Add(this.label3);
			this.tabDataTypes.Controls.Add(this.label4);
			this.tabDataTypes.Controls.Add(this.cmbDataType);
			this.tabDataTypes.Location = new System.Drawing.Point(4, 22);
			this.tabDataTypes.Name = "tabDataTypes";
			this.tabDataTypes.Size = new System.Drawing.Size(460, 82);
			this.tabDataTypes.TabIndex = 1;
			this.tabDataTypes.Text = "Data Types Test";
			// 
			// txtDataTypeTestName
			// 
			this.txtDataTypeTestName.Location = new System.Drawing.Point(84, 10);
			this.txtDataTypeTestName.Name = "txtDataTypeTestName";
			this.txtDataTypeTestName.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtDataTypeTestName.Size = new System.Drawing.Size(156, 20);
			this.txtDataTypeTestName.TabIndex = 1;
			this.txtDataTypeTestName.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(6, 36);
			this.label3.Name = "label3";
			this.label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label3.Size = new System.Drawing.Size(78, 18);
			this.label3.TabIndex = 2;
			this.label3.Text = "Data Type:";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(6, 12);
			this.label4.Name = "label4";
			this.label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label4.Size = new System.Drawing.Size(48, 18);
			this.label4.TabIndex = 0;
			this.label4.Text = "Name:";
			// 
			// cmbDataType
			// 
			this.cmbDataType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbDataType.Location = new System.Drawing.Point(84, 33);
			this.cmbDataType.Name = "cmbDataType";
			this.cmbDataType.Size = new System.Drawing.Size(156, 21);
			this.cmbDataType.TabIndex = 3;
			// 
			// tabSqlInjection
			// 
			this.tabSqlInjection.Controls.Add(this.txtSqlTestName);
			this.tabSqlInjection.Controls.Add(this.label5);
			this.tabSqlInjection.Controls.Add(this.label6);
			this.tabSqlInjection.Controls.Add(this.cmbSqlTestValues);
			this.tabSqlInjection.Location = new System.Drawing.Point(4, 22);
			this.tabSqlInjection.Name = "tabSqlInjection";
			this.tabSqlInjection.Size = new System.Drawing.Size(460, 82);
			this.tabSqlInjection.TabIndex = 2;
			this.tabSqlInjection.Text = "SQL Injection Test";
			// 
			// txtSqlTestName
			// 
			this.txtSqlTestName.Location = new System.Drawing.Point(84, 10);
			this.txtSqlTestName.Name = "txtSqlTestName";
			this.txtSqlTestName.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtSqlTestName.Size = new System.Drawing.Size(156, 20);
			this.txtSqlTestName.TabIndex = 32;
			this.txtSqlTestName.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6, 36);
			this.label5.Name = "label5";
			this.label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label5.Size = new System.Drawing.Size(78, 18);
			this.label5.TabIndex = 31;
			this.label5.Text = "SQLTests:";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(6, 12);
			this.label6.Name = "label6";
			this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label6.Size = new System.Drawing.Size(48, 18);
			this.label6.TabIndex = 30;
			this.label6.Text = "Name:";
			// 
			// cmbSqlTestValues
			// 
			this.cmbSqlTestValues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSqlTestValues.Location = new System.Drawing.Point(84, 33);
			this.cmbSqlTestValues.Name = "cmbSqlTestValues";
			this.cmbSqlTestValues.Size = new System.Drawing.Size(276, 21);
			this.cmbSqlTestValues.TabIndex = 29;
			// 
			// tabXssInjection
			// 
			this.tabXssInjection.Controls.Add(this.txtXssTestName);
			this.tabXssInjection.Controls.Add(this.label7);
			this.tabXssInjection.Controls.Add(this.label8);
			this.tabXssInjection.Controls.Add(this.cmbXssTestValues);
			this.tabXssInjection.Location = new System.Drawing.Point(4, 22);
			this.tabXssInjection.Name = "tabXssInjection";
			this.tabXssInjection.Size = new System.Drawing.Size(460, 82);
			this.tabXssInjection.TabIndex = 3;
			this.tabXssInjection.Text = "XSS Test";
			// 
			// txtXssTestName
			// 
			this.txtXssTestName.Location = new System.Drawing.Point(84, 10);
			this.txtXssTestName.Name = "txtXssTestName";
			this.txtXssTestName.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtXssTestName.Size = new System.Drawing.Size(156, 20);
			this.txtXssTestName.TabIndex = 36;
			this.txtXssTestName.Text = "";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(6, 36);
			this.label7.Name = "label7";
			this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label7.Size = new System.Drawing.Size(78, 18);
			this.label7.TabIndex = 35;
			this.label7.Text = "XSS Tests:";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(6, 12);
			this.label8.Name = "label8";
			this.label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label8.Size = new System.Drawing.Size(48, 18);
			this.label8.TabIndex = 34;
			this.label8.Text = "Name:";
			// 
			// cmbXssTestValues
			// 
			this.cmbXssTestValues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbXssTestValues.Location = new System.Drawing.Point(84, 33);
			this.cmbXssTestValues.Name = "cmbXssTestValues";
			this.cmbXssTestValues.Size = new System.Drawing.Size(276, 21);
			this.cmbXssTestValues.TabIndex = 33;
			// 
			// tabPredefinedTest
			// 
			this.tabPredefinedTest.Controls.Add(this.txtPredfinedTestName);
			this.tabPredefinedTest.Controls.Add(this.label9);
			this.tabPredefinedTest.Location = new System.Drawing.Point(4, 22);
			this.tabPredefinedTest.Name = "tabPredefinedTest";
			this.tabPredefinedTest.Size = new System.Drawing.Size(460, 82);
			this.tabPredefinedTest.TabIndex = 4;
			this.tabPredefinedTest.Text = "Predefined Test";
			// 
			// txtPredfinedTestName
			// 
			this.txtPredfinedTestName.Location = new System.Drawing.Point(84, 10);
			this.txtPredfinedTestName.Name = "txtPredfinedTestName";
			this.txtPredfinedTestName.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtPredfinedTestName.Size = new System.Drawing.Size(156, 20);
			this.txtPredfinedTestName.TabIndex = 38;
			this.txtPredfinedTestName.Text = "";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(6, 12);
			this.label9.Name = "label9";
			this.label9.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.label9.Size = new System.Drawing.Size(48, 18);
			this.label9.TabIndex = 37;
			this.label9.Text = "Name:";
			// 
			// UnitTestManagerEditForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(498, 158);
			this.Controls.Add(this.groupBox2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UnitTestManagerEditForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Load += new System.EventHandler(this.UnitTestManagerEditForm_Load);
			this.groupBox2.ResumeLayout(false);
			this.tabTests.ResumeLayout(false);
			this.tabBufferOverflow.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numBufferLen)).EndInit();
			this.tabDataTypes.ResumeLayout(false);
			this.tabSqlInjection.ResumeLayout(false);
			this.tabXssInjection.ResumeLayout(false);
			this.tabPredefinedTest.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void UnitTestManagerEditForm_Load(object sender, System.EventArgs e)
		{
			if ( this.TestArgs is BufferOverflowTesterArgs )
			{
				BufferOverflowTesterArgs args = (BufferOverflowTesterArgs)this.TestArgs;
				this.numBufferLen.Value = (decimal)args.BufferLength;
				this.txtBufferTestName.Text = this.TestName;
				this.tabTests.SelectedTab = this.tabBufferOverflow;
			}

			if ( this.TestArgs is SqlInjectionTesterArgs )
			{
				SqlInjectionTesterArgs args = (SqlInjectionTesterArgs)this.TestArgs;
				this.cmbSqlTestValues.Text = args.SqlValue;
				this.txtSqlTestName.Text = this.TestName;
				this.tabTests.SelectedTab = this.tabSqlInjection;
			}

			if ( this.TestArgs is XssInjectionTesterArgs )
			{
				XssInjectionTesterArgs args = (XssInjectionTesterArgs)this.TestArgs;
				this.cmbXssTestValues.Text = args.XssValue;
				this.txtXssTestName.Text = this.TestName;
				this.tabTests.SelectedTab = this.tabXssInjection;
			}

			if ( this.TestArgs is DataTypesTesterArgs )
			{
				DataTypesTesterArgs args = (DataTypesTesterArgs)this.TestArgs;
				this.cmbDataType.SelectedIndex = dataTypesList.IndexOfKey(args.SelectedDataType);
				this.txtDataTypeTestName.Text = this.TestName;
				this.tabTests.SelectedTab = this.tabDataTypes;
			}

			// Load combos
			this.cmbDataType.DataSource = ConvertToArrayList(this.GetDataTypes());
			this.cmbSqlTestValues.DataSource = ConvertToArrayList(this.GetSqlValues());
			this.cmbXssTestValues.DataSource = ConvertToArrayList(this.GetXssValues());

		}

		private ArrayList ConvertToArrayList(SortedList coll)
		{
			ArrayList al = new ArrayList();
			foreach (DictionaryEntry de in coll)
			{
				al.Add(de.Value);
			}
			
			return al;
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			if ( ValidateText() )
			{
				this.DialogResult = DialogResult.OK;
				CreateArgument();
			}
		}

		#region Lists
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
		/// Gets the xss attacks signatures from file.
		/// </summary>
		/// <returns></returns>
		private SortedList GetXssValues()
		{
			if ( this.xssValuesList.Count == 0)
			{
				// get file path
				string filePath = Application.CommonAppDataPath + "\\" + inspectorConfig.XssSignatures;

				StreamReader reader = new StreamReader(filePath);
				XmlTextReader xmlReader = new XmlTextReader(reader);

				// SortedList
				int i=0;
				while (xmlReader.Read())
				{
					if ( xmlReader.Name.ToLower(System.Globalization.CultureInfo.InvariantCulture)=="value")
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
		/// Gets the sql injection values from file.
		/// </summary>
		/// <returns></returns>
		private SortedList GetSqlValues()
		{
			if ( this.sqlValuesList.Count == 0)
			{
				// get file path
				string filePath = Application.UserAppDataPath + "\\" + inspectorConfig.SqlSignatures;

				StreamReader reader = new StreamReader(filePath);
				XmlTextReader xmlReader = new XmlTextReader(reader);

				// SortedList
				int i=0;
				while (xmlReader.Read())
				{
					if ( xmlReader.Name.ToLower(System.Globalization.CultureInfo.InvariantCulture)=="value")
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
		private void CreateArgument()
		{
			IHtmlFormUnitTestArgs args = null;

			switch ( this.tabTests.SelectedTab.Name )
			{
				case "tabBufferOverflow":
					args = new BufferOverflowTesterArgs();
					((BufferOverflowTesterArgs)args).BufferLength = (int)this.numBufferLen.Value;
					this.HtmlFormTestType = UnitTestType.BufferOverflow;
					this.TestType = "Buffer Overflow Test";
					this.TestName = this.txtBufferTestName.Text;
					this.TestArgs = args;					
					break;

				case "tabDataTypes":
					args = new DataTypesTesterArgs();
					((DataTypesTesterArgs)args).SelectedDataType = (DataType)dataTypesList.GetKey(dataTypesList.IndexOfValue(this.cmbDataType.Text));
					this.HtmlFormTestType = UnitTestType.DataTypes;
					this.TestType = "Data Type Test";
					this.TestName = this.txtDataTypeTestName.Text;
					this.TestArgs = args;
					break;

				case "tabSqlInjection":
					args = new SqlInjectionTesterArgs();
					((SqlInjectionTesterArgs)args).SqlValue = this.cmbSqlTestValues.Text;
					this.HtmlFormTestType = UnitTestType.SqlInjection;
					this.TestType = "SQL Injection Test";
					this.TestName = this.txtSqlTestName.Text;
					this.TestArgs = args;
					break;

				case "tabXssInjection":
					args = new XssInjectionTesterArgs();
					((XssInjectionTesterArgs)args).XssValue = this.cmbXssTestValues.Text;
					this.HtmlFormTestType = UnitTestType.XSS;
					this.TestType = "XSS Injection Test";
					this.TestName = this.txtXssTestName.Text;
					this.TestArgs = args;
					break;

				case "tabPredefinedTest":
					args = new PredefinedTesterArgs();
					this.HtmlFormTestType = UnitTestType.Predefined;
					this.TestType = "Predefined Test";
					this.TestName = this.txtPredfinedTestName.Text;
					this.TestArgs = args;
					break;
			}
		}

		private bool ValidateText()
		{
			TextBox textControl = null;
			
			switch ( this.tabTests.SelectedTab.Name )
			{
				case "tabBufferOverflow":
					textControl = this.txtBufferTestName;
					break;
				case "tabDataTypes":
					textControl = this.txtDataTypeTestName;
					break;
				case "tabSqlInjection":
					textControl = this.txtSqlTestName;
					break;
				case "tabXssInjection":
					textControl = this.txtXssTestName;
					break;
				case "tabPredefinedTest":
					textControl = this.txtPredfinedTestName;
					break;
			}
			
			if ( textControl.Text.Length == 0 )
			{
				this.errorProvider1.SetError(textControl,"The Name value is empty. Please set a new value.");
				textControl.Focus();
				//this.btnOK.Enabled=false;
				return false;
			} 
			else 
			{
				this.errorProvider1.SetError(textControl,"");
				//this.btnOK.Enabled=true;
				return true;
			}
		}

		#endregion

		private void tabTests_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}


		#region Properties

		/// <summary>
		/// Gets or sets the selected data type.
		/// </summary>
		public string SelectedDataTypeString
		{
			get
			{
				return (string)this.cmbDataType.SelectedValue;
			}
			set
			{
				int idx = this.cmbDataType.FindString(value,-1);
				this.cmbDataType.SelectedIndex = idx;
			}
		}
		/// <summary>
		/// Gets or sets the buffer length.
		/// </summary>
		public int BufferLength
		{
			get
			{
				return (int)this.numBufferLen.Value;
			}
			set
			{
				this.numBufferLen.Value = value;
			}
		}

		/// <summary>
		/// Gets or sets the arguments assign for a test.
		/// </summary>
		public IHtmlFormUnitTestArgs TestArgs
		{
			get
			{
				return _testArgs;
			}
			set
			{
				_testArgs = value;
			}
		}

		/// <summary>
		/// Gets or sets the test name.
		/// </summary>
		public string TestName
		{
			get
			{
				return _testName;
			}
			set
			{
				_testName = value;
			}
		}

		/// <summary>
		/// Gets or sets the test type.
		/// </summary>
		public string TestType
		{
			get
			{
				return _testType;
			}
			set
			{
				_testType = value;
			}
		}

		/// <summary>
		/// Gets or sets the HtmlFormTest enum type.
		/// </summary>
		public UnitTestType HtmlFormTestType
		{
			get
			{
				return _htmlFormTestType;
			}
			set
			{
				_htmlFormTestType = value;
			}
		}


//		public string OnSuccessString
//		{
//			get
//			{
//				return (string)cmbOnSuccess.SelectedValue;
//			}
//			set
//			{
//				int idx = ((ArrayList)cmbOnSuccess.DataSource).IndexOf(value);
//				cmbOnSuccess.SelectedIndex = idx;
//			}
//		}
//		public string OnErrorString
//		{
//			get
//			{
//				return (string)cmbOnError.SelectedValue;
//			}
//			set
//			{
//				int idx = ((ArrayList)cmbOnError.DataSource).IndexOf(value);
//				cmbOnError.SelectedIndex = idx;
//			}
//		}
		#endregion
	}
}
