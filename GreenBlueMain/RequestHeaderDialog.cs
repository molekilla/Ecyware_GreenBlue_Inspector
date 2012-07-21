using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Ecyware.GreenBlue.Controls;
using Ecyware.GreenBlue.Protocols.Http;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.Scripting;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Summary description for RequestHeaderDialog.
	/// </summary>
	public class RequestHeaderDialog : System.Windows.Forms.Form
	{
		private Ecyware.GreenBlue.Controls.FlatPropertyGrid pgHeaders;
		private HttpProperties _requestHeaders = null;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnSave;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new RequestHeaderDialog.
		/// </summary>
		public RequestHeaderDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Gets or sets the request headers.
		/// </summary>
		public HttpProperties RequestHeaders
		{
			get
			{
				return _requestHeaders;
			}
			set
			{
				_requestHeaders = value;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(RequestHeaderDialog));
			this.pgHeaders = new Ecyware.GreenBlue.Controls.FlatPropertyGrid();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// pgHeaders
			// 
			this.pgHeaders.CommandsVisibleIfAvailable = true;
			this.pgHeaders.LargeButtons = false;
			this.pgHeaders.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.pgHeaders.Location = new System.Drawing.Point(0, 0);
			this.pgHeaders.Name = "pgHeaders";
			this.pgHeaders.Size = new System.Drawing.Size(396, 228);
			this.pgHeaders.TabIndex = 8;
			this.pgHeaders.Text = "PropertyGrid";
			this.pgHeaders.ToolbarVisible = false;
			this.pgHeaders.ViewBackColor = System.Drawing.SystemColors.Window;
			this.pgHeaders.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(316, 240);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 10;
			this.btnCancel.Text = "Cancel";
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.Location = new System.Drawing.Point(235, 240);
			this.btnSave.Name = "btnSave";
			this.btnSave.TabIndex = 9;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// RequestHeaderDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(392, 266);
			this.Controls.Add(this.pgHeaders);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnSave);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RequestHeaderDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Request Header Collection Dialog";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Loads the http properties.
		/// </summary>
		/// <param name="httpProperties"> The HttpProperties type.</param>
		public void LoadHttpProperties(HttpProperties httpProperties)
		{
			this.RequestHeaders = httpProperties;

			PropertyTable bag = new PropertyTable();			
			string category = "Request Headers";
			// string category2 = "Authentication";

			//PropertySpec item = new PropertySpec("Headers",typeof(HttpPropertiesWrapper),category,"Request Headers");
			PropertySpec accept = new PropertySpec("Accept", typeof(string),category);
			PropertySpec contentLength = new PropertySpec("Content Length",typeof(string), category);
			PropertySpec contentType = new PropertySpec("Content Type",typeof(string), category);
			PropertySpec ifModifiedSince = new PropertySpec("If Modified Since",typeof(DateTime), category);
			PropertySpec keepAlive = new PropertySpec("Keep Alive",typeof(bool), category);
			PropertySpec mediaType = new PropertySpec("Media Type",typeof(string), category);
			PropertySpec pipeline = new PropertySpec("Pipeline",typeof(bool), category);
			PropertySpec referer = new PropertySpec("Referer",typeof(string), category);
			PropertySpec sendChunked = new PropertySpec("Send Chunked",typeof(bool), category);
			PropertySpec transferEncoding = new PropertySpec("Transfer Encoding",typeof(string), category);			
			PropertySpec userAgent = new PropertySpec("User Agent",typeof(string), category);
			PropertySpec timeout = new PropertySpec("Timeout",typeof(int), category);

			// PropertySpec addonHeaders = new PropertySpec("Additional headers",typeof(string[]), category);
			// PropertySpec ntlmAuth = new PropertySpec("Windows Integrated Security", typeof(bool), category2);
			// accept.ConverterTypeName = "Ecyware.GreenBlue.Controls.HttpPropertiesWrapper";

			bag.Properties.AddRange(new PropertySpec[] {accept,
														   contentLength, 
														   contentType, 
														   ifModifiedSince,
														   keepAlive,
														   mediaType,
														   pipeline,
														   referer,
														   sendChunked,
														   transferEncoding,
														   userAgent, timeout});

			bag[accept.Name] = httpProperties.Accept;
			bag[contentLength.Name] = httpProperties.ContentLength;
			bag[contentType.Name] = httpProperties.ContentType;
			bag[ifModifiedSince.Name] = httpProperties.IfModifiedSince;
			bag[keepAlive.Name] = httpProperties.KeepAlive;
			bag[mediaType.Name] = httpProperties.MediaType;
			bag[pipeline.Name] = httpProperties.Pipeline;
			bag[referer.Name] = httpProperties.Referer;
			bag[sendChunked.Name] = httpProperties.SendChunked;
			bag[transferEncoding.Name] = httpProperties.TransferEncoding;
			bag[userAgent.Name] = httpProperties.UserAgent;
			bag[timeout.Name] = httpProperties.Timeout;
			//bag[addonHeaders.Name] = new string[0];
			// bag[ntlmAuth.Name] = httpProperties.AuthenticationSettings.UseNTLMAuthentication;

			this.pgHeaders.SelectedObject = bag;

		}

		/// <summary>
		/// Returns the http properties from the property grid.
		/// </summary>
		/// <returns> A HttpProperties.</returns>
		public HttpProperties GetHttpProperties()
		{
			HttpProperties props = new HttpProperties();
			PropertyTable bag = (PropertyTable)this.pgHeaders.SelectedObject;
			props.Accept = (string)bag["Accept"];

			if ( this.RequestHeaders.AuthenticationSettings.UseBasicAuthentication )
			{
				props.AuthenticationSettings = this.RequestHeaders.AuthenticationSettings;
			} 
			else 
			{
				props.AuthenticationSettings.UseNTLMAuthentication = this.RequestHeaders.AuthenticationSettings.UseNTLMAuthentication;
			}

			props.ContentLength = Convert.ToInt64(bag["Content Length"]);
			props.ContentType = (string)bag["Content Type"];
			props.IfModifiedSince = Convert.ToDateTime(bag["If Modified Since"]);
			props.KeepAlive = (bool)bag["Keep Alive"];
			props.MediaType = (string)bag["Media Type"];
			props.Pipeline = (bool)bag["Pipeline"];
			props.Referer = (string)bag["Referer"];
			props.SendChunked = (bool)bag["Send Chunked"];
			props.TransferEncoding = (string)bag["Transfer Encoding"];
			props.UserAgent = (string)bag["User Agent"];
			props.Timeout = (int)bag["Timeout"];

			return props;

		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			//this.RequestHeaders = this.GetHttpProperties();
		}
	}
}
