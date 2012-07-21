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
	/// Summary description for SmtpTransportDialog.
	/// </summary>
	public class SmtpTransportDialog : System.Windows.Forms.Form
	{
		private Transport _transport;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtFrom;
		private System.Windows.Forms.TextBox txtSubject;
		private System.Windows.Forms.TextBox txtTo;
		private System.Windows.Forms.TextBox txtServer;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cmbFormat;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new SmtpTransportDialog.
		/// </summary>
		public SmtpTransportDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			cmbFormat.Items.Add(System.Web.Mail.MailFormat.Html.ToString());
			cmbFormat.Items.Add(System.Web.Mail.MailFormat.Text.ToString());
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SmtpTransportDialog));
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtFrom = new System.Windows.Forms.TextBox();
			this.txtSubject = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtTo = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtServer = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.cmbFormat = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(18, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 18);
			this.label2.TabIndex = 1;
			this.label2.Text = "From";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 12);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(372, 23);
			this.label4.TabIndex = 0;
			this.label4.Text = "Configure SMTP Transport";
			// 
			// btnOK
			// 
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(162, 186);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 11;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(246, 186);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 12;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// txtFrom
			// 
			this.txtFrom.Location = new System.Drawing.Point(120, 48);
			this.txtFrom.Name = "txtFrom";
			this.txtFrom.Size = new System.Drawing.Size(204, 20);
			this.txtFrom.TabIndex = 2;
			this.txtFrom.Text = "";
			// 
			// txtSubject
			// 
			this.txtSubject.Location = new System.Drawing.Point(120, 96);
			this.txtSubject.Name = "txtSubject";
			this.txtSubject.Size = new System.Drawing.Size(204, 20);
			this.txtSubject.TabIndex = 6;
			this.txtSubject.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(18, 96);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 18);
			this.label1.TabIndex = 5;
			this.label1.Text = "Subject";
			// 
			// txtTo
			// 
			this.txtTo.Location = new System.Drawing.Point(120, 72);
			this.txtTo.Name = "txtTo";
			this.txtTo.Size = new System.Drawing.Size(204, 20);
			this.txtTo.TabIndex = 4;
			this.txtTo.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(18, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 18);
			this.label3.TabIndex = 3;
			this.label3.Text = "To";
			// 
			// txtServer
			// 
			this.txtServer.Location = new System.Drawing.Point(120, 144);
			this.txtServer.Name = "txtServer";
			this.txtServer.Size = new System.Drawing.Size(204, 20);
			this.txtServer.TabIndex = 10;
			this.txtServer.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(18, 144);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 18);
			this.label5.TabIndex = 9;
			this.label5.Text = "SMTP Server";
			// 
			// cmbFormat
			// 
			this.cmbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbFormat.Location = new System.Drawing.Point(120, 120);
			this.cmbFormat.Name = "cmbFormat";
			this.cmbFormat.Size = new System.Drawing.Size(204, 21);
			this.cmbFormat.TabIndex = 8;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(18, 120);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 18);
			this.label6.TabIndex = 7;
			this.label6.Text = "Message Format";
			// 
			// SmtpTransportDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(382, 226);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.cmbFormat);
			this.Controls.Add(this.txtServer);
			this.Controls.Add(this.txtTo);
			this.Controls.Add(this.txtSubject);
			this.Controls.Add(this.txtFrom);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SmtpTransportDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "SMTP Transport Dialog";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.SmtpTransportDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			SmtpTransport transport = new SmtpTransport();
			transport.From = this.txtFrom.Text;
			transport.ServerUrl = this.txtServer.Text;
			transport.Subject = this.txtSubject.Text;
			transport.To = this.txtTo.Text;			
			if ( this.cmbFormat.Text.Length == 0 )
			{
				transport.MessageFormat = System.Web.Mail.MailFormat.Html;
			} 
			else 
			{
				transport.MessageFormat =  (System.Web.Mail.MailFormat)Enum.Parse(typeof(System.Web.Mail.MailFormat),Convert.ToString(this.cmbFormat.Text));
			}

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
				if ( this.Transport is SmtpTransport )
				{
					SmtpTransport t = (SmtpTransport)this.Transport;

					if  ( t.MessageFormat == System.Web.Mail.MailFormat.Html )
					{
						this.cmbFormat.SelectedIndex = 0;
					} 
					else
					{
						this.cmbFormat.SelectedIndex = 1;
					}

					this.txtTo.Text = t.To;
					this.txtSubject.Text = t.Subject;
					this.txtServer.Text = t.ServerUrl;
					this.txtFrom.Text = t.From;
				}
			}
		}


	}
}
