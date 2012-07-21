using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using Ecyware.GreenBlue.Engine.Transforms;
using Ecyware.GreenBlue.Engine;

namespace Ecyware.GreenBlue.Controls.Scripting
{
	/// <summary>
	/// Summary description for SaveApplicationDialog.
	/// </summary>
	public class SetRequestTypeDialog : System.Windows.Forms.Form
	{

		HttpRequestType _selectedRequestType = HttpRequestType.GET;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ComboBox combo;
		private System.Windows.Forms.TextBox txtUrl;
		private System.Windows.Forms.TextBox txtContentType;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new SaveApplicationDialog.
		/// </summary>
		public SetRequestTypeDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			ArrayList items = new ArrayList();
			
			// Load combo box.		
			items.Add(new NameValueObject("Delete",HttpRequestType.DELETE.ToString()));
			items.Add(new NameValueObject("Get",HttpRequestType.GET.ToString()));
			//items.Add(new NameValueObject("Head",HttpRequestType.HEAD.ToString()));
			//items.Add(new NameValueObject("Options",HttpRequestType.OPTIONS.ToString()));
			items.Add(new NameValueObject("Post",HttpRequestType.POST.ToString()));
			items.Add(new NameValueObject("Put",HttpRequestType.PUT.ToString()));
			//items.Add(new NameValueObject("Trace",HttpRequestType.TRACE.ToString()));
			items.Add(new NameValueObject("Soap over Http",HttpRequestType.SOAPHTTP.ToString()));

			combo.DataSource = items;
			combo.DisplayMember = "Name";
			combo.ValueMember = "Value";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SetRequestTypeDialog));
			this.label1 = new System.Windows.Forms.Label();
			this.btnSave = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.combo = new System.Windows.Forms.ComboBox();
			this.txtUrl = new System.Windows.Forms.TextBox();
			this.txtContentType = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(126, 18);
			this.label1.TabIndex = 11;
			this.label1.Text = "HTTP Request Type";
			// 
			// btnSave
			// 
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.Location = new System.Drawing.Point(198, 114);
			this.btnSave.Name = "btnSave";
			this.btnSave.TabIndex = 9;
			this.btnSave.Text = "&OK";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Location = new System.Drawing.Point(282, 114);
			this.button1.Name = "button1";
			this.button1.TabIndex = 12;
			this.button1.Text = "&Cancel";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// combo
			// 
			this.combo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.combo.Location = new System.Drawing.Point(156, 23);
			this.combo.Name = "combo";
			this.combo.Size = new System.Drawing.Size(228, 21);
			this.combo.TabIndex = 13;
			// 
			// txtUrl
			// 
			this.txtUrl.Location = new System.Drawing.Point(156, 47);
			this.txtUrl.Name = "txtUrl";
			this.txtUrl.Size = new System.Drawing.Size(228, 20);
			this.txtUrl.TabIndex = 14;
			this.txtUrl.Text = "";
			// 
			// txtContentType
			// 
			this.txtContentType.Location = new System.Drawing.Point(156, 71);
			this.txtContentType.Name = "txtContentType";
			this.txtContentType.Size = new System.Drawing.Size(228, 20);
			this.txtContentType.TabIndex = 15;
			this.txtContentType.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(126, 18);
			this.label2.TabIndex = 16;
			this.label2.Text = "Url";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(24, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(126, 18);
			this.label3.TabIndex = 17;
			this.label3.Text = "Content Type";
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// SetRequestTypeDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(432, 154);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtContentType);
			this.Controls.Add(this.txtUrl);
			this.Controls.Add(this.combo);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnSave);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SetRequestTypeDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Insert New Web Request";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			if ( this.txtUrl.Text.Length == 0 )
			{
				this.errorProvider1.SetError(txtUrl, "A url is required.");
			} 
			else 
			{
				_selectedRequestType = (HttpRequestType)Enum.Parse(typeof(HttpRequestType),(string)combo.SelectedValue);
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		/// <summary>
		/// Gets the selected request type.
		/// </summary>
		public HttpRequestType SelectedHttpRequestType
		{
			get
			{
				return _selectedRequestType;
			}
		}

		/// <summary>
		/// Gets the url.
		/// </summary>
		public string Url
		{
			get
			{
				return this.txtUrl.Text;
			}
		}

		/// <summary>
		/// Gets the content type.
		/// </summary>
		public string ContentType
		{
			get
			{
				return this.txtContentType.Text;
			}
		}
	}
}
