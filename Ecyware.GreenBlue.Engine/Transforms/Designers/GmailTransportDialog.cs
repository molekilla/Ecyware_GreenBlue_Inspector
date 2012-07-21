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
	/// Summary description for GmailTransportDialog.
	/// </summary>
	public class GmailTransportDialog : System.Windows.Forms.Form
	{
		private Transport _transport;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtSubject;
		private System.Windows.Forms.TextBox txtBcc;
		private System.Windows.Forms.TextBox txtCc;
		private System.Windows.Forms.TextBox txtTo;
		private System.Windows.Forms.TextBox txtFrom;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.RadioButton rbText;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.RadioButton rbHTML;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label10;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new SmtpTransportDialog.
		/// </summary>
		public GmailTransportDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			this.txtPassword.PasswordChar = '\u25cf';
			//cmbFormat.Items.Add(System.Web.Mail.MailFormat.Html.ToString());
			//cmbFormat.Items.Add(System.Web.Mail.MailFormat.Text.ToString());
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(GmailTransportDialog));
			this.label4 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtSubject = new System.Windows.Forms.TextBox();
			this.txtBcc = new System.Windows.Forms.TextBox();
			this.txtCc = new System.Windows.Forms.TextBox();
			this.txtTo = new System.Windows.Forms.TextBox();
			this.txtFrom = new System.Windows.Forms.TextBox();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.rbText = new System.Windows.Forms.RadioButton();
			this.label6 = new System.Windows.Forms.Label();
			this.rbHTML = new System.Windows.Forms.RadioButton();
			this.label5 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 6);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(372, 23);
			this.label4.TabIndex = 0;
			this.label4.Text = "Configure GMail Transport";
			// 
			// btnOK
			// 
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(120, 294);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 11;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(204, 294);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 12;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// txtSubject
			// 
			this.txtSubject.Location = new System.Drawing.Point(120, 144);
			this.txtSubject.Name = "txtSubject";
			this.txtSubject.Size = new System.Drawing.Size(156, 20);
			this.txtSubject.TabIndex = 36;
			this.txtSubject.Text = "";
			// 
			// txtBcc
			// 
			this.txtBcc.Location = new System.Drawing.Point(120, 114);
			this.txtBcc.Name = "txtBcc";
			this.txtBcc.Size = new System.Drawing.Size(156, 20);
			this.txtBcc.TabIndex = 35;
			this.txtBcc.Text = "";
			// 
			// txtCc
			// 
			this.txtCc.Location = new System.Drawing.Point(120, 84);
			this.txtCc.Name = "txtCc";
			this.txtCc.Size = new System.Drawing.Size(156, 20);
			this.txtCc.TabIndex = 34;
			this.txtCc.Text = "";
			// 
			// txtTo
			// 
			this.txtTo.Location = new System.Drawing.Point(120, 60);
			this.txtTo.Name = "txtTo";
			this.txtTo.Size = new System.Drawing.Size(156, 20);
			this.txtTo.TabIndex = 33;
			this.txtTo.Text = "";
			// 
			// txtFrom
			// 
			this.txtFrom.Location = new System.Drawing.Point(120, 30);
			this.txtFrom.Name = "txtFrom";
			this.txtFrom.Size = new System.Drawing.Size(156, 20);
			this.txtFrom.TabIndex = 32;
			this.txtFrom.Text = "";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(120, 258);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new System.Drawing.Size(156, 20);
			this.txtPassword.TabIndex = 31;
			this.txtPassword.Text = "";
			// 
			// txtUsername
			// 
			this.txtUsername.Location = new System.Drawing.Point(120, 228);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(156, 20);
			this.txtUsername.TabIndex = 30;
			this.txtUsername.Text = "";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(18, 258);
			this.label9.Name = "label9";
			this.label9.TabIndex = 29;
			this.label9.Text = "Password";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(18, 228);
			this.label8.Name = "label8";
			this.label8.TabIndex = 28;
			this.label8.Text = "Username";
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.Location = new System.Drawing.Point(18, 204);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(160, 16);
			this.label7.TabIndex = 27;
			this.label7.Text = "GMail User Credentials";
			// 
			// rbText
			// 
			this.rbText.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.rbText.Location = new System.Drawing.Point(186, 168);
			this.rbText.Name = "rbText";
			this.rbText.Size = new System.Drawing.Size(48, 24);
			this.rbText.TabIndex = 26;
			this.rbText.Text = "Text";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(18, 168);
			this.label6.Name = "label6";
			this.label6.TabIndex = 25;
			this.label6.Text = "Body format";
			// 
			// rbHTML
			// 
			this.rbHTML.Checked = true;
			this.rbHTML.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.rbHTML.Location = new System.Drawing.Point(120, 168);
			this.rbHTML.Name = "rbHTML";
			this.rbHTML.Size = new System.Drawing.Size(64, 24);
			this.rbHTML.TabIndex = 24;
			this.rbHTML.TabStop = true;
			this.rbHTML.Text = "HTML";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(18, 144);
			this.label5.Name = "label5";
			this.label5.TabIndex = 23;
			this.label5.Text = "Subject";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(18, 114);
			this.label1.Name = "label1";
			this.label1.TabIndex = 22;
			this.label1.Text = "Bcc";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(18, 84);
			this.label3.Name = "label3";
			this.label3.TabIndex = 21;
			this.label3.Text = "Cc";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(18, 60);
			this.label2.Name = "label2";
			this.label2.TabIndex = 20;
			this.label2.Text = "To";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(18, 30);
			this.label10.Name = "label10";
			this.label10.TabIndex = 19;
			this.label10.Text = "From";
			// 
			// GmailTransportDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(342, 328);
			this.Controls.Add(this.txtSubject);
			this.Controls.Add(this.txtBcc);
			this.Controls.Add(this.txtCc);
			this.Controls.Add(this.txtTo);
			this.Controls.Add(this.txtFrom);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.txtUsername);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.rbText);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.rbHTML);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label4);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "GmailTransportDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "GMail Transport Dialog";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.SmtpTransportDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			GmailTransport transport = new GmailTransport();
			transport.Bcc =  this.txtBcc.Text;
			transport.Cc = this.txtCc.Text;
			if ( this.rbHTML.Checked )
			{
				transport.Format = System.Web.Mail.MailFormat.Html;
			}
			if ( this.rbText.Checked )
			{
				transport.Format = System.Web.Mail.MailFormat.Text;
			}

			transport.From = this.txtFrom.Text;
			transport.Password = this.txtPassword.Text;
			transport.Username  = this.txtUsername.Text;
			transport.To = this.txtTo.Text;
			transport.Subject = this.txtSubject.Text;

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
				if ( this.Transport is GmailTransport )
				{
					GmailTransport t = (GmailTransport)this.Transport;


					this.txtBcc.Text = t.Bcc;
					this.txtCc.Text = t.Cc;
					if ( t.Format ==  System.Web.Mail.MailFormat.Html )
					{
						this.rbHTML.Checked = true;
					}
					if ( t.Format ==  System.Web.Mail.MailFormat.Text )
					{
						this.rbText.Checked = true;
					}
					this.txtFrom.Text = t.From;
					this.txtPassword.Text = t.Password;
					this.txtUsername.Text = t.Username;
					this.txtTo.Text = t.To;
					this.txtSubject.Text = t.Subject;
				}
			}
		}


	}
}
