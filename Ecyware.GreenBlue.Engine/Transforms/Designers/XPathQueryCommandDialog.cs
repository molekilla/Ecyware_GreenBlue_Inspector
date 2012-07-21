using System;
using System.Drawing;
using System.Collections;
using System.Reflection;
using System.ComponentModel;
using System.Windows.Forms;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms.Designers;
using Ecyware.GreenBlue.Engine.Transforms;


namespace Ecyware.GreenBlue.Engine.Transforms.Designers
{
	/// <summary>
	/// Summary description for XPathQueryCommandDialog.
	/// </summary>
	public class XPathQueryCommandDialog : System.Windows.Forms.Form
	{
		private TransformValue _tvalue;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtQuery;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmbXmlNodeProperty;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtPrefix;
		private System.Windows.Forms.TextBox txtPostfix;
		private System.Windows.Forms.TextBox txtXSLT;
		private System.Windows.Forms.Label label6;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new HeaderTransformValueDialog.
		/// </summary>
		public XPathQueryCommandDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			if ( this.cmbXmlNodeProperty.Items.Count == 0 )
			{
				cmbXmlNodeProperty.Items.Add(XmlNodeProperty.InnerText.ToString());
				cmbXmlNodeProperty.Items.Add(XmlNodeProperty.InnerXml.ToString());
				cmbXmlNodeProperty.Items.Add(XmlNodeProperty.OuterXml.ToString());
				cmbXmlNodeProperty.Items.Add(XmlNodeProperty.LocalName.ToString());
			}
		}


		/// <summary>
		/// Gets a description.
		/// </summary>
		public string Description
		{
			get
			{
				return "Uses a XPath query: " + this.txtQuery.Text;
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(XPathQueryCommandDialog));
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtQuery = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.cmbXmlNodeProperty = new System.Windows.Forms.ComboBox();
			this.txtPrefix = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtPostfix = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtXSLT = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(18, 42);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 18);
			this.label2.TabIndex = 1;
			this.label2.Text = "XPath Query";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 12);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(372, 23);
			this.label4.TabIndex = 0;
			this.label4.Text = "Sets a XPath Query";
			// 
			// btnOK
			// 
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(240, 258);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(324, 258);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// txtQuery
			// 
			this.txtQuery.Location = new System.Drawing.Point(138, 42);
			this.txtQuery.Name = "txtQuery";
			this.txtQuery.Size = new System.Drawing.Size(264, 20);
			this.txtQuery.TabIndex = 2;
			this.txtQuery.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(18, 72);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 18);
			this.label1.TabIndex = 5;
			this.label1.Text = "Use XmlNode property";
			// 
			// cmbXmlNodeProperty
			// 
			this.cmbXmlNodeProperty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbXmlNodeProperty.Location = new System.Drawing.Point(138, 69);
			this.cmbXmlNodeProperty.Name = "cmbXmlNodeProperty";
			this.cmbXmlNodeProperty.Size = new System.Drawing.Size(264, 21);
			this.cmbXmlNodeProperty.TabIndex = 6;
			// 
			// txtPrefix
			// 
			this.txtPrefix.Location = new System.Drawing.Point(138, 94);
			this.txtPrefix.Name = "txtPrefix";
			this.txtPrefix.Size = new System.Drawing.Size(264, 20);
			this.txtPrefix.TabIndex = 8;
			this.txtPrefix.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(18, 96);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 18);
			this.label3.TabIndex = 7;
			this.label3.Text = "Prefix to add";
			// 
			// txtPostfix
			// 
			this.txtPostfix.Location = new System.Drawing.Point(138, 118);
			this.txtPostfix.Name = "txtPostfix";
			this.txtPostfix.Size = new System.Drawing.Size(264, 20);
			this.txtPostfix.TabIndex = 10;
			this.txtPostfix.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(18, 120);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 18);
			this.label5.TabIndex = 9;
			this.label5.Text = "Postfix to add";
			// 
			// txtXSLT
			// 
			this.txtXSLT.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtXSLT.Location = new System.Drawing.Point(138, 144);
			this.txtXSLT.Multiline = true;
			this.txtXSLT.Name = "txtXSLT";
			this.txtXSLT.Size = new System.Drawing.Size(264, 96);
			this.txtXSLT.TabIndex = 11;
			this.txtXSLT.Text = "";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(18, 144);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 18);
			this.label6.TabIndex = 12;
			this.label6.Text = "XSLT";
			// 
			// XPathQueryCommandDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(420, 292);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.txtXSLT);
			this.Controls.Add(this.txtPostfix);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtPrefix);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.cmbXmlNodeProperty);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtQuery);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "XPathQueryCommandDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "XPath Query Dialog";
			this.TopMost = true;
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			XPathQueryCommand tvalue = new XPathQueryCommand();
			
			tvalue.Query = this.txtQuery.Text;			
			tvalue.UseNodeProperty = (XmlNodeProperty)Enum.Parse(typeof(XmlNodeProperty), (string)this.cmbXmlNodeProperty.SelectedItem);
			tvalue.Postfix = this.txtPostfix.Text;
			tvalue.Prefix = this.txtPrefix.Text;
			tvalue.XsltTemplate = this.txtXSLT.Text;

			_tvalue = tvalue;
			DialogResult = DialogResult.OK;
		}
		/// <summary>
		/// Loads the transform values.
		/// </summary>
		public void LoadTransformValue()
		{
			if ( this.TransformValue != null )
			{
				if ( this.TransformValue is XPathQueryCommand )
				{
					XPathQueryCommand cmd = ((XPathQueryCommand)_tvalue);

					this.txtQuery.Text = cmd.Query;
					this.txtPostfix.Text = cmd.Postfix;
					this.txtPrefix.Text = cmd.Prefix;
					this.txtXSLT.Text = cmd.XsltTemplate;

					int i = this.cmbXmlNodeProperty.FindString(cmd.UseNodeProperty.ToString());
					this.cmbXmlNodeProperty.SelectedIndex = i;
				}
			}
		}

		/// <summary>
		/// Gets the transform value.
		/// </summary>
		public TransformValue TransformValue
		{
			get
			{
				return _tvalue;
			}

			set
			{
				_tvalue = value;
			}
		}
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}


	}
}
