using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using Ecyware.GreenBlue.Controls;
using Ecyware.GreenBlue.Engine;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Summary description for ProxyDialog.
	/// </summary>
	public class ProxyDialog : System.Windows.Forms.Form
	{
		private string username = string.Empty;
		private string password = string.Empty;
		private string domain = string.Empty;
		private string proxyUri = string.Empty;

		private HttpProxy _proxy = null;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtProxyUrl;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtUsername;
		private System.Windows.Forms.TextBox txtDomain;
		private System.Windows.Forms.TextBox txtPassword;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.CheckBox chkBypassLocal;
		private System.Windows.Forms.CheckBox chkUseProxy;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private bool _isSet = false;
		/// <summary>
		/// Creates a new proxy dialog.
		/// </summary>
		public ProxyDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Creates a new proxy dialog.
		/// </summary>
		/// <param name="proxySettings"> The proxy settings.</param>
		public ProxyDialog(HttpProxy proxySettings) : this()
		{
			this.ProxySettings = proxySettings;

			if ( this.ProxySettings != null )
			{
				this.txtProxyUrl.Text = proxySettings.ProxyUri;
				this.chkBypassLocal.Checked = proxySettings.BypassOnLocal;
			}
		}

		/// <summary>
		/// Gets or sets the proxy settings.
		/// </summary>
		public HttpProxy ProxySettings
		{
			get
			{
				return _proxy;
			}
			set
			{
				_proxy = value;
			}
		}

		/// <summary>
		/// Sets the proxy settings.
		/// </summary>
		private void SetProxy()
		{
			if ( this.txtProxyUrl.Text.Length > 0 )
			{
				proxyUri = this.txtProxyUrl.Text;

				this.ProxySettings = new HttpProxy();
				this.ProxySettings.ProxyUri = proxyUri;

				this.ProxySettings.BypassOnLocal = this.chkBypassLocal.Checked;

				if ( (username != String.Empty) || (password != String.Empty) || (domain != String.Empty) )
				{
					this.ProxySettings.SetProxyAuthentication(username,password,domain);
				} 
				else 
				{		
					if ( (username != String.Empty) || (password != String.Empty) )
					{
						this.ProxySettings.SetProxyAuthentication(username,password);
					} 
					else 
					{
						// public proxy settings
						this.ProxySettings.SetPublicProxy();
					}
				}
			} 
			else
			{
				if ( this.ProxySettings!=null )
					this.ProxySettings = null;
			}
		}

		private bool ValidateProxyUri()
		{
			try
			{
				Uri url = new Uri(this.txtProxyUrl.Text);
				return true;
			}
			catch
			{
				return false;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ProxyDialog));
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.txtProxyUrl = new System.Windows.Forms.TextBox();
			this.chkBypassLocal = new System.Windows.Forms.CheckBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.txtDomain = new System.Windows.Forms.TextBox();
			this.txtUsername = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.chkUseProxy = new System.Windows.Forms.CheckBox();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(234, 198);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.Location = new System.Drawing.Point(150, 198);
			this.btnSave.Name = "btnSave";
			this.btnSave.TabIndex = 5;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.button1_Click);
			// 
			// txtProxyUrl
			// 
			this.txtProxyUrl.Enabled = false;
			this.txtProxyUrl.Location = new System.Drawing.Point(96, 36);
			this.txtProxyUrl.Name = "txtProxyUrl";
			this.txtProxyUrl.Size = new System.Drawing.Size(192, 20);
			this.txtProxyUrl.TabIndex = 2;
			this.txtProxyUrl.Text = "";
			// 
			// chkBypassLocal
			// 
			this.chkBypassLocal.Enabled = false;
			this.chkBypassLocal.Location = new System.Drawing.Point(96, 60);
			this.chkBypassLocal.Name = "chkBypassLocal";
			this.chkBypassLocal.Size = new System.Drawing.Size(216, 18);
			this.chkBypassLocal.TabIndex = 3;
			this.chkBypassLocal.Text = "Bypass local addresses";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtPassword);
			this.groupBox1.Controls.Add(this.txtDomain);
			this.groupBox1.Controls.Add(this.txtUsername);
			this.groupBox1.Enabled = false;
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(12, 78);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(300, 108);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Authentication";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 78);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(84, 18);
			this.label4.TabIndex = 4;
			this.label4.Text = "Domain:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 54);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(84, 18);
			this.label3.TabIndex = 2;
			this.label3.Text = "Password:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(84, 18);
			this.label2.TabIndex = 0;
			this.label2.Text = "Username:";
			// 
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(102, 52);
			this.txtPassword.MaxLength = 200;
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '*';
			this.txtPassword.Size = new System.Drawing.Size(186, 20);
			this.txtPassword.TabIndex = 3;
			this.txtPassword.Text = "";
			// 
			// txtDomain
			// 
			this.txtDomain.Location = new System.Drawing.Point(102, 76);
			this.txtDomain.MaxLength = 200;
			this.txtDomain.Name = "txtDomain";
			this.txtDomain.Size = new System.Drawing.Size(186, 20);
			this.txtDomain.TabIndex = 5;
			this.txtDomain.Text = "";
			// 
			// txtUsername
			// 
			this.txtUsername.Location = new System.Drawing.Point(102, 28);
			this.txtUsername.MaxLength = 200;
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(186, 20);
			this.txtUsername.TabIndex = 1;
			this.txtUsername.Text = "";
			// 
			// label1
			// 
			this.label1.Enabled = false;
			this.label1.Location = new System.Drawing.Point(12, 36);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 18);
			this.label1.TabIndex = 1;
			this.label1.Text = "Proxy address:";
			// 
			// chkUseProxy
			// 
			this.chkUseProxy.Location = new System.Drawing.Point(18, 6);
			this.chkUseProxy.Name = "chkUseProxy";
			this.chkUseProxy.Size = new System.Drawing.Size(288, 24);
			this.chkUseProxy.TabIndex = 0;
			this.chkUseProxy.Text = "Use Proxy";
			this.chkUseProxy.CheckedChanged += new System.EventHandler(this.chkUseProxy_CheckedChanged);
			// 
			// ProxyDialog
			// 
			this.AutoScale = false;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(318, 226);
			this.Controls.Add(this.chkUseProxy);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.chkBypassLocal);
			this.Controls.Add(this.txtProxyUrl);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ProxyDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Proxy Settings";
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{
			if ( this.IsProxySettingSet )
			{
				if ( ValidateProxyUri() )
				{
					SetProxy();
					this.Hide();
				} 
				else 
				{
					MessageBox.Show("Please enter a valid proxy address with the format http://<address>", AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
		}

		private void chkUseProxy_CheckedChanged(object sender, System.EventArgs e)
		{
			bool enabled = chkUseProxy.Checked;

			this.label1.Enabled = enabled;
			this.groupBox1.Enabled = enabled;
			this.txtProxyUrl.Enabled = enabled;
			this.chkBypassLocal.Enabled = enabled;

			IsProxySettingSet = enabled;
		}

		/// <summary>
		/// Gets or sets if the proxy settings has been set.
		/// </summary>
		public bool IsProxySettingSet 
		{
			get
			{
				return _isSet;
			}
			set
			{
				_isSet = value;
			}
		}
	}
}
