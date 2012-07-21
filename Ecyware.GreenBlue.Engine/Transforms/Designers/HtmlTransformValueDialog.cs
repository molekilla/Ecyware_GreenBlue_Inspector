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
	/// Summary description for HtmlTransformValueDialog.
	/// </summary>
	public class HtmlTransformValueDialog : System.Windows.Forms.Form
	{
		//private ArrayList _requestFields = new ArrayList();
		private TransformValue _tvalue;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtAttributeName;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.NumericUpDown numIndex;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cmbTags;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox chkHasAttributeDelimiter;
		private System.Windows.Forms.TextBox txtDelimiter;
		private System.Windows.Forms.NumericUpDown numDelimiterIndex;
		private System.Windows.Forms.Label lblDelimiterIndex;
		private System.Windows.Forms.Label lblDelimiter;
		private System.Windows.Forms.ComboBox cmbNameID;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new HtmlTransformValueDialog.
		/// </summary>
		public HtmlTransformValueDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			#region Request Fields
			if ( cmbTags.Items.Count == 0 )
			{
				//_requestFields.AddRange(HtmlTransformValue.GetTags);
				cmbTags.Items.AddRange(HtmlTransformValue.GetTags);
			}
			#endregion

			if ( this.TransformValue != null )
			{
				HtmlTransformValue transform = (HtmlTransformValue)this.TransformValue;
				this.txtDelimiter.Text = transform.Delimiter;
				this.numDelimiterIndex.Value  = transform.DelimiterIndex;
				this.txtAttributeName.Text = transform.AttributeName;
				this.chkHasAttributeDelimiter.Checked = transform.HasAttributeDelimiter;
				this.numIndex.Value = transform.Index;
				this.cmbTags.SelectedText = transform.Tag;
				this.cmbNameID.SelectedText = transform.TagNameId;
			}
		}

		/// <summary>
		/// Gets a description.
		/// </summary>
		public string Description
		{
			get
			{
				HtmlTransformValue tv = (HtmlTransformValue)this.TransformValue;
				return "Uses a HTML tag \"" + tv.Tag + "\" with id or name " + tv.TagNameId + " or tag index " + tv.Index;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(HtmlTransformValueDialog));
			this.label4 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.cmbNameID = new System.Windows.Forms.ComboBox();
			this.txtAttributeName = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.numIndex = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cmbTags = new System.Windows.Forms.ComboBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.lblDelimiter = new System.Windows.Forms.Label();
			this.numDelimiterIndex = new System.Windows.Forms.NumericUpDown();
			this.lblDelimiterIndex = new System.Windows.Forms.Label();
			this.txtDelimiter = new System.Windows.Forms.TextBox();
			this.chkHasAttributeDelimiter = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numIndex)).BeginInit();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numDelimiterIndex)).BeginInit();
			this.SuspendLayout();
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 12);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(372, 23);
			this.label4.TabIndex = 0;
			this.label4.Text = "Set a HTML value to map.";
			// 
			// btnOK
			// 
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(216, 270);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(300, 270);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.cmbNameID);
			this.groupBox1.Controls.Add(this.txtAttributeName);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.numIndex);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.cmbTags);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(6, 36);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(372, 126);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "HTML Tag Element Options";
			// 
			// cmbNameID
			// 
			this.cmbNameID.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbNameID.Location = new System.Drawing.Point(150, 45);
			this.cmbNameID.Name = "cmbNameID";
			this.cmbNameID.Size = new System.Drawing.Size(174, 21);
			this.cmbNameID.TabIndex = 3;
			// 
			// txtAttributeName
			// 
			this.txtAttributeName.Location = new System.Drawing.Point(150, 96);
			this.txtAttributeName.Name = "txtAttributeName";
			this.txtAttributeName.Size = new System.Drawing.Size(174, 20);
			this.txtAttributeName.TabIndex = 7;
			this.txtAttributeName.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(18, 96);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(120, 18);
			this.label5.TabIndex = 6;
			this.label5.Text = "Tag attribute";
			// 
			// numIndex
			// 
			this.numIndex.Location = new System.Drawing.Point(150, 72);
			this.numIndex.Name = "numIndex";
			this.numIndex.Size = new System.Drawing.Size(60, 20);
			this.numIndex.TabIndex = 5;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(18, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 22);
			this.label3.TabIndex = 4;
			this.label3.Text = "Tag index";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(18, 48);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 18);
			this.label1.TabIndex = 2;
			this.label1.Text = "Select id or name";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(18, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(120, 18);
			this.label2.TabIndex = 0;
			this.label2.Text = "Create lookup for tag";
			// 
			// cmbTags
			// 
			this.cmbTags.Location = new System.Drawing.Point(150, 18);
			this.cmbTags.Name = "cmbTags";
			this.cmbTags.Size = new System.Drawing.Size(174, 21);
			this.cmbTags.TabIndex = 1;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.lblDelimiter);
			this.groupBox2.Controls.Add(this.numDelimiterIndex);
			this.groupBox2.Controls.Add(this.lblDelimiterIndex);
			this.groupBox2.Controls.Add(this.txtDelimiter);
			this.groupBox2.Controls.Add(this.chkHasAttributeDelimiter);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(6, 162);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(372, 96);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Sub Attribute Options";
			// 
			// lblDelimiter
			// 
			this.lblDelimiter.Enabled = false;
			this.lblDelimiter.Location = new System.Drawing.Point(18, 48);
			this.lblDelimiter.Name = "lblDelimiter";
			this.lblDelimiter.Size = new System.Drawing.Size(120, 18);
			this.lblDelimiter.TabIndex = 1;
			this.lblDelimiter.Text = "Delimiter";
			// 
			// numDelimiterIndex
			// 
			this.numDelimiterIndex.Enabled = false;
			this.numDelimiterIndex.Location = new System.Drawing.Point(150, 70);
			this.numDelimiterIndex.Name = "numDelimiterIndex";
			this.numDelimiterIndex.Size = new System.Drawing.Size(60, 20);
			this.numDelimiterIndex.TabIndex = 4;
			// 
			// lblDelimiterIndex
			// 
			this.lblDelimiterIndex.Enabled = false;
			this.lblDelimiterIndex.Location = new System.Drawing.Point(18, 72);
			this.lblDelimiterIndex.Name = "lblDelimiterIndex";
			this.lblDelimiterIndex.Size = new System.Drawing.Size(120, 18);
			this.lblDelimiterIndex.TabIndex = 3;
			this.lblDelimiterIndex.Text = "Index";
			// 
			// txtDelimiter
			// 
			this.txtDelimiter.Enabled = false;
			this.txtDelimiter.Location = new System.Drawing.Point(150, 46);
			this.txtDelimiter.Name = "txtDelimiter";
			this.txtDelimiter.Size = new System.Drawing.Size(60, 20);
			this.txtDelimiter.TabIndex = 2;
			this.txtDelimiter.Text = "";
			// 
			// chkHasAttributeDelimiter
			// 
			this.chkHasAttributeDelimiter.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.chkHasAttributeDelimiter.Location = new System.Drawing.Point(18, 18);
			this.chkHasAttributeDelimiter.Name = "chkHasAttributeDelimiter";
			this.chkHasAttributeDelimiter.Size = new System.Drawing.Size(318, 24);
			this.chkHasAttributeDelimiter.TabIndex = 0;
			this.chkHasAttributeDelimiter.Text = "Has sub attribute";
			this.chkHasAttributeDelimiter.CheckedChanged += new System.EventHandler(this.chkHasAttributeDelimiter_CheckedChanged);
			// 
			// HtmlTransformValueDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(382, 304);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label4);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "HtmlTransformValueDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "HTML Value Dialog";
			this.TopMost = true;
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numIndex)).EndInit();
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numDelimiterIndex)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			HtmlTransformValue tvalue = new HtmlTransformValue();
			tvalue.Tag = (string)this.cmbTags.Text;
			tvalue.TagNameId = Convert.ToString(this.cmbNameID.SelectedItem);
			tvalue.Index = Convert.ToInt32(this.numIndex.Value);
			tvalue.AttributeName = this.txtAttributeName.Text;

			if ( this.chkHasAttributeDelimiter.Checked )
			{
				tvalue.HasAttributeDelimiter = chkHasAttributeDelimiter.Checked;
				tvalue.Delimiter = txtDelimiter.Text;
				tvalue.DelimiterIndex = Convert.ToInt32(numDelimiterIndex.Value);
			}

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
				if ( this.TransformValue is HtmlTransformValue  )
				{
					HtmlTransformValue vl = ((HtmlTransformValue)_tvalue);

					cmbTags.Text = vl.Tag;
					this.cmbNameID.SelectedText = vl.TagNameId;
					numIndex.Value = vl.Index;

					this.txtAttributeName.Text = vl.AttributeName;

					if ( vl.HasAttributeDelimiter)
					{
						chkHasAttributeDelimiter.Checked = vl.HasAttributeDelimiter;
						txtDelimiter.Text = vl.Delimiter;
						numDelimiterIndex.Value = vl.DelimiterIndex;
					}
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

		/// <summary>
		/// Loads the form id's list.
		/// </summary>
		/// <param name="names"></param>
		public void LoadForm(ArrayList names)
		{
			this.cmbNameID.Items.Clear();
			// Load names.
			this.cmbNameID.Items.AddRange((string[])names.ToArray(typeof(string)));
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void chkHasAttributeDelimiter_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( this.chkHasAttributeDelimiter.Checked )
			{
				this.lblDelimiterIndex.Enabled = true;
				this.lblDelimiter.Enabled = true;
				this.numDelimiterIndex.Enabled = true;
				this.txtDelimiter.Enabled = true;
			} 
			else 
			{
				this.lblDelimiterIndex.Enabled = false;
				this.lblDelimiter.Enabled = false;
				this.numDelimiterIndex.Enabled = false;
				this.txtDelimiter.Enabled = false;
			}
		}




	}
}
