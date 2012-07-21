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
	/// Summary description for BloggerTransportDialog.
	/// </summary>
	public class BloggerTransportDialog : System.Windows.Forms.Form
	{
		private Transport _transport;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtAppName;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtUserName;
		private System.Windows.Forms.TextBox txtEndpoint;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox txtCategory;
		private System.Windows.Forms.TextBox txtTitle;
		private System.Windows.Forms.TextBox txtSelectedIndex;
		private System.Windows.Forms.TextBox txtUrl;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new BloggerTransportDialog.
		/// </summary>
		public BloggerTransportDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.txtPassword.PasswordChar = '\u25cf';
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BloggerTransportDialog));
			this.label4 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtUrl = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtAppName = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtUserName = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtEndpoint = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtCategory = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.txtSelectedIndex = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 12);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(372, 23);
			this.label4.TabIndex = 0;
			this.label4.Text = "Configure Blogger Transport";
			// 
			// btnOK
			// 
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(150, 276);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 11;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(234, 276);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 12;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtUrl);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtAppName);
			this.groupBox1.Location = new System.Drawing.Point(24, 36);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(558, 78);
			this.groupBox1.TabIndex = 13;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Generator Settings";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 18);
			this.label2.TabIndex = 6;
			this.label2.Text = "Url Name";
			// 
			// txtUrl
			// 
			this.txtUrl.Location = new System.Drawing.Point(114, 48);
			this.txtUrl.Name = "txtUrl";
			this.txtUrl.Size = new System.Drawing.Size(204, 20);
			this.txtUrl.TabIndex = 5;
			this.txtUrl.Text = "http://www.ecyware.com";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 18);
			this.label1.TabIndex = 4;
			this.label1.Text = "Application Name";
			// 
			// txtAppName
			// 
			this.txtAppName.Location = new System.Drawing.Point(114, 23);
			this.txtAppName.Name = "txtAppName";
			this.txtAppName.Size = new System.Drawing.Size(204, 20);
			this.txtAppName.TabIndex = 3;
			this.txtAppName.Text = "Ecyware GreenBlue Services";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.txtPassword);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.txtUserName);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.txtEndpoint);
			this.groupBox2.Location = new System.Drawing.Point(24, 120);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(558, 72);
			this.groupBox2.TabIndex = 14;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Endpoint Settings";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(332, 44);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(72, 18);
			this.label6.TabIndex = 12;
			this.label6.Text = "Password";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(408, 43);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(144, 20);
			this.txtPassword.TabIndex = 11;
			this.txtPassword.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 44);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 18);
			this.label3.TabIndex = 10;
			this.label3.Text = "Username";
			// 
			// txtUserName
			// 
			this.txtUserName.Location = new System.Drawing.Point(116, 43);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.Size = new System.Drawing.Size(164, 20);
			this.txtUserName.TabIndex = 9;
			this.txtUserName.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(16, 20);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(96, 18);
			this.label5.TabIndex = 8;
			this.label5.Text = "Endpoint";
			// 
			// txtEndpoint
			// 
			this.txtEndpoint.Location = new System.Drawing.Point(116, 20);
			this.txtEndpoint.Name = "txtEndpoint";
			this.txtEndpoint.Size = new System.Drawing.Size(164, 20);
			this.txtEndpoint.TabIndex = 7;
			this.txtEndpoint.Text = "https://www.blogger.com/atom/";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.label7);
			this.groupBox3.Controls.Add(this.txtCategory);
			this.groupBox3.Controls.Add(this.label8);
			this.groupBox3.Controls.Add(this.txtTitle);
			this.groupBox3.Controls.Add(this.label9);
			this.groupBox3.Controls.Add(this.txtSelectedIndex);
			this.groupBox3.Location = new System.Drawing.Point(24, 198);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(558, 72);
			this.groupBox3.TabIndex = 15;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Blog Message Settings";
			this.groupBox3.Enter += new System.EventHandler(this.groupBox3_Enter);
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(332, 44);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(72, 18);
			this.label7.TabIndex = 18;
			this.label7.Text = "Category";
			// 
			// txtCategory
			// 
			this.txtCategory.Location = new System.Drawing.Point(408, 44);
			this.txtCategory.Name = "txtCategory";
			this.txtCategory.Size = new System.Drawing.Size(144, 20);
			this.txtCategory.TabIndex = 17;
			this.txtCategory.Text = "http://www.ecyware.com";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(16, 44);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(96, 18);
			this.label8.TabIndex = 16;
			this.label8.Text = "Title";
			// 
			// txtTitle
			// 
			this.txtTitle.Location = new System.Drawing.Point(140, 44);
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.Size = new System.Drawing.Size(176, 20);
			this.txtTitle.TabIndex = 15;
			this.txtTitle.Text = "Test";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(16, 20);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(116, 18);
			this.label9.TabIndex = 14;
			this.label9.Text = "Selected Blog Index";
			// 
			// txtSelectedIndex
			// 
			this.txtSelectedIndex.Location = new System.Drawing.Point(140, 20);
			this.txtSelectedIndex.Name = "txtSelectedIndex";
			this.txtSelectedIndex.Size = new System.Drawing.Size(24, 20);
			this.txtSelectedIndex.TabIndex = 13;
			this.txtSelectedIndex.Text = "0";
			// 
			// BloggerTransportDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(588, 316);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label4);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BloggerTransportDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Blogger Transport Dialog";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.SmtpTransportDialog_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			BloggerTransport transport = new BloggerTransport();
			transport.ApplicationName = this.txtAppName.Text;
			transport.Category = this.txtCategory.Text;
			transport.Endpoint = this.txtEndpoint.Text;
			transport.Password = this.txtPassword.Text;			
			transport.SelectedIndex = this.txtSelectedIndex.Text;
			transport.Title = this.txtTitle.Text;
			transport.Url  = this.txtUrl.Text;
			transport.UserName = this.txtUserName.Text;


			_transport = transport;
			DialogResult = DialogResult.OK;
		}


		/// <summary>
		/// Gets the transform value.
		/// </summary>
		public Transport Transport
		{
			get
			{
				return _transport;
			}

			set
			{
				_transport = value;
			}
		}
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void SmtpTransportDialog_Load(object sender, System.EventArgs e)
		{
			if ( this.Transport != null )
			{
				if ( this.Transport is BloggerTransport )
				{
					BloggerTransport transport = (BloggerTransport)this.Transport;

					this.txtAppName.Text = transport.ApplicationName;
					this.txtCategory.Text = transport.Category;
					this.txtEndpoint.Text = transport.Endpoint;
					this.txtPassword.Text = transport.Password;			
					this.txtSelectedIndex.Text = transport.SelectedIndex;
					this.txtTitle.Text = transport.Title;
					this.txtUrl.Text = transport.Url;
					this.txtUserName.Text = transport.UserName;
				}

			}
		}

		private void groupBox3_Enter(object sender, System.EventArgs e)
		{
		
		}


	}
}
