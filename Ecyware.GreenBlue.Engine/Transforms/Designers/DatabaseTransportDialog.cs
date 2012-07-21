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
	/// Summary description for DatabaseTransportDialog.
	/// </summary>
	public class DatabaseTransportDialog : System.Windows.Forms.Form
	{
		private Transport _transport;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtConnectionString;
		private System.Windows.Forms.TextBox txtQuery;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new DatabaseTransportDialog.
		/// </summary>
		public DatabaseTransportDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DatabaseTransportDialog));
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtConnectionString = new System.Windows.Forms.TextBox();
			this.txtQuery = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(18, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 18);
			this.label2.TabIndex = 1;
			this.label2.Text = "Connection String";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 12);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(372, 23);
			this.label4.TabIndex = 0;
			this.label4.Text = "Configure Database Transport";
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
			// txtConnectionString
			// 
			this.txtConnectionString.Location = new System.Drawing.Point(120, 48);
			this.txtConnectionString.Name = "txtConnectionString";
			this.txtConnectionString.Size = new System.Drawing.Size(402, 20);
			this.txtConnectionString.TabIndex = 2;
			this.txtConnectionString.Text = "";
			// 
			// txtQuery
			// 
			this.txtQuery.Location = new System.Drawing.Point(120, 72);
			this.txtQuery.Multiline = true;
			this.txtQuery.Name = "txtQuery";
			this.txtQuery.Size = new System.Drawing.Size(402, 102);
			this.txtQuery.TabIndex = 4;
			this.txtQuery.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(18, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 18);
			this.label3.TabIndex = 3;
			this.label3.Text = "Query";
			// 
			// DatabaseTransportDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(534, 226);
			this.Controls.Add(this.txtQuery);
			this.Controls.Add(this.txtConnectionString);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DatabaseTransportDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Database Transport Dialog";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.SmtpTransportDialog_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			DatabaseTransport transport = new DatabaseTransport();
			transport.ConnectionString.Value = this.txtConnectionString.Text;
			transport.Query.Value = this.txtQuery.Text;

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
				if ( this.Transport is DatabaseTransport )
				{
					DatabaseTransport t = (DatabaseTransport)this.Transport;
					this.txtConnectionString.Text = t.ConnectionString.Value;
					this.txtQuery.Text = t.Query.Value;
				}				
			}
		}


	}
}
