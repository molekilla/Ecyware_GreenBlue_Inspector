// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: July 2004
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using Ecyware.GreenBlue.Utils;using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Configuration;


namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Contains the definition for the BufferOverflowGeneratorDialog type.
	/// </summary>
	public class SqlTestDialog : System.Windows.Forms.Form
	{
		private InspectorConfiguration _inspectorConfig = null;
		private SortedList sqlValuesList = new SortedList();
		private string _selectedValue = string.Empty;

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.ComboBox cmbSqlTestValues;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new SqlTestDialog.
		/// </summary>
		public SqlTestDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Creates a new SqlTestDialog.
		/// </summary>
		/// <param name="inspectorConfig"> The InspectorConfiguration settings.</param>
		public SqlTestDialog(InspectorConfiguration inspectorConfig) : this()
		{
			_inspectorConfig = inspectorConfig;

			this.cmbSqlTestValues.DataSource = Converter.ConvertToArrayList(this.GetSqlValues());
		}
		/// <summary>
		/// Gets the sql injection values from file.
		/// </summary>
		/// <returns>A SortedList with the values.</returns>
		private SortedList GetSqlValues()
		{
			if ( this.sqlValuesList.Count == 0)
			{
				// get file path
				string filePath = AppLocation.CommonFolder + "\\" + _inspectorConfig.SqlSignatures;

				StreamReader reader = new StreamReader(filePath);
				XmlTextReader xmlReader = new XmlTextReader(reader);

				// SortedList
				int i=0;
				while (xmlReader.Read())
				{
					if ( CompareString.Compare(xmlReader.Name,"value") )
					{
						sqlValuesList.Add(i,xmlReader.ReadString());
					}

					i++;
				}

				xmlReader.Close();
			}

			return sqlValuesList;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SqlTestDialog));
			this.label1 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.cmbSqlTestValues = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 18);
			this.label1.TabIndex = 1;
			this.label1.Text = "SQL Injection:";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(236, 48);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 12;
			this.btnCancel.Text = "Cancel";
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.Location = new System.Drawing.Point(150, 48);
			this.btnSave.Name = "btnSave";
			this.btnSave.TabIndex = 11;
			this.btnSave.Text = "Set";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// cmbSqlTestValues
			// 
			this.cmbSqlTestValues.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSqlTestValues.DropDownWidth = 250;
			this.cmbSqlTestValues.Location = new System.Drawing.Point(96, 18);
			this.cmbSqlTestValues.Name = "cmbSqlTestValues";
			this.cmbSqlTestValues.Size = new System.Drawing.Size(216, 21);
			this.cmbSqlTestValues.TabIndex = 31;
			// 
			// SqlTestDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(318, 74);
			this.Controls.Add(this.cmbSqlTestValues);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SqlTestDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Sql Injection Test Dialog";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Gets or sets the selected value
		/// </summary>
		public string SelectedValue
		{
			get
			{
				return _selectedValue;
			}
			set
			{
				_selectedValue = value;
			}
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			this.SelectedValue = this.cmbSqlTestValues.Text;
		}
	}
}
