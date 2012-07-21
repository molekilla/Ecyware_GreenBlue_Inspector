// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: November 2003 - July 2004
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Text;
using Ecyware.GreenBlue.Utils;using Ecyware.GreenBlue.Engine;


namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// EncodeDecodeUtil is use for encoding and decoding of data.
	/// </summary>
	public class EncodeDecodeUtil : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rbEncode;
		private System.Windows.Forms.RadioButton rbDecode;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ComboBox cmbFormat;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtInput;
		private System.Windows.Forms.TextBox txtOutput;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new EncodeDecode Utility.
		/// </summary>
		public EncodeDecodeUtil()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			LoadTypeCombo();
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
			this.label1 = new System.Windows.Forms.Label();
			this.txtOutput = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.cmbFormat = new System.Windows.Forms.ComboBox();
			this.rbDecode = new System.Windows.Forms.RadioButton();
			this.rbEncode = new System.Windows.Forms.RadioButton();
			this.txtInput = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtOutput);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.cmbFormat);
			this.groupBox1.Controls.Add(this.rbDecode);
			this.groupBox1.Controls.Add(this.rbEncode);
			this.groupBox1.Controls.Add(this.txtInput);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(438, 300);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Encode Decode Utility";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 162);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(186, 12);
			this.label1.TabIndex = 6;
			this.label1.Text = "Result";
			// 
			// txtOutput
			// 
			this.txtOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtOutput.Location = new System.Drawing.Point(6, 180);
			this.txtOutput.Multiline = true;
			this.txtOutput.Name = "txtOutput";
			this.txtOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtOutput.Size = new System.Drawing.Size(426, 114);
			this.txtOutput.TabIndex = 5;
			this.txtOutput.Text = "";
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Location = new System.Drawing.Point(360, 18);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(66, 24);
			this.button1.TabIndex = 4;
			this.button1.Text = "Process";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// cmbFormat
			// 
			this.cmbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbFormat.Location = new System.Drawing.Point(180, 20);
			this.cmbFormat.Name = "cmbFormat";
			this.cmbFormat.Size = new System.Drawing.Size(168, 21);
			this.cmbFormat.TabIndex = 3;
			// 
			// rbDecode
			// 
			this.rbDecode.Location = new System.Drawing.Point(90, 24);
			this.rbDecode.Name = "rbDecode";
			this.rbDecode.Size = new System.Drawing.Size(84, 18);
			this.rbDecode.TabIndex = 2;
			this.rbDecode.Text = "Decode";
			// 
			// rbEncode
			// 
			this.rbEncode.Checked = true;
			this.rbEncode.Location = new System.Drawing.Point(6, 24);
			this.rbEncode.Name = "rbEncode";
			this.rbEncode.Size = new System.Drawing.Size(84, 18);
			this.rbEncode.TabIndex = 1;
			this.rbEncode.TabStop = true;
			this.rbEncode.Text = "Encode";
			// 
			// txtInput
			// 
			this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtInput.Location = new System.Drawing.Point(6, 48);
			this.txtInput.Multiline = true;
			this.txtInput.Name = "txtInput";
			this.txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtInput.Size = new System.Drawing.Size(426, 108);
			this.txtInput.TabIndex = 0;
			this.txtInput.Text = "";
			// 
			// EncodeDecodeUtil
			// 
			this.Controls.Add(this.groupBox1);
			this.DockPadding.All = 3;
			this.Name = "EncodeDecodeUtil";
			this.Size = new System.Drawing.Size(444, 306);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region UI Events
		private void button1_Click(object sender, System.EventArgs e)
		{
			try
			{
				string selection=((DataRowView)this.cmbFormat.SelectedValue)["Value"].ToString();
				if ( rbEncode.Checked )
				{
					ProcessEncode(selection);
				}

				if ( rbDecode.Checked )
				{
					ProcessDecode(selection);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message,AppLocation.ApplicationName,MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
//			catch ( DecodeException ex)
//			{
//				MessageBox.Show("An error ocurred while decoding.");
//			}
//			catch ( EncodeException ex)
//			{
//				MessageBox.Show("An error ocurred while encoding.");
//			}
		}
		#endregion

		#region Methods
		/// <summary>
		/// Loads the encode and decode types.
		/// </summary>
		private void LoadTypeCombo()
		{
			DataTable list=new DataTable("list");
			list.Columns.Add("Name");
			list.Columns.Add("Value");

			DataRow r = list.NewRow();
			r["Name"]="Base64";
			r["value"]="Base64".ToUpper();
			list.Rows.Add(r);
			
			r = list.NewRow();
			r["Name"]="HtmlEncode";
			r["value"]="HtmlEncode".ToUpper();
			list.Rows.Add(r);

			r = list.NewRow();
			r["Name"]="Hex";
			r["value"]="Hex".ToUpper();
			list.Rows.Add(r);

			r = list.NewRow();
			r["Name"]="UrlEncode";
			r["value"]="UrlEncode".ToUpper();
			list.Rows.Add(r);
			
			cmbFormat.DataSource=list;
			cmbFormat.DisplayMember="Name";
		}
		/// <summary>
		/// Process a decode request.
		/// </summary>
		/// <param name="type"> The type to decode to.</param>
		private void ProcessDecode(string type)
		{

			string data = this.txtInput.Text;
			string r = String.Empty;

			switch (type)
			{
				case "BASE64":
					r = Encoding.UTF8.GetString(EncodeDecode.Base64Decode(data));
					break;
				case "HTMLENCODE":
					r = EncodeDecode.HtmlDecode(data);
					break;
				case "URLENCODE":
					r = EncodeDecode.UrlDecode(data);
					break;
				case "HEX":
					r = EncodeDecode.GetHexDecode(data);
					break;
			}

			txtOutput.Text=r;

		}

		/// <summary>
		/// Process a encode request.
		/// </summary>
		/// <param name="type"> The type to encode to.</param>
		private void ProcessEncode(string type)
		{

			string data = this.txtInput.Text;
			string r = String.Empty;

			switch (type)
			{
				case "BASE64":
					r = EncodeDecode.Base64Encode(Encoding.UTF8.GetBytes(data));
					break;
				case "HTMLENCODE":
					r = EncodeDecode.HtmlEncode(data);
					break;
				case "URLENCODE":
					r = EncodeDecode.UrlEncode(data);
					break;
				case "HEX":
					r = EncodeDecode.GetHexEncode(data);
					break;
			}

			txtOutput.Text=r;

		}
		#endregion
	}
}
