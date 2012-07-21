// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: June 2004 - July 2004
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Ecyware.GreenBlue.Controls;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Engine.HtmlCommand;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.Scripting;

namespace Ecyware.GreenBlue.Controls.Scripting
{
	/// <summary>
	/// Contains the definition for the ScriptingMainPage control.
	/// </summary>
	public class WebRequestPage : BaseScriptingDataPage
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox txtRequestID;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtUrl;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox grpRequestHeaders;
		private Ecyware.GreenBlue.Controls.FlatPropertyGrid pgHeaders;
		private System.Windows.Forms.LinkLabel lnkEditHeaders;
		private System.Windows.Forms.CheckBox chkUsePostData;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Creates a new SessionRequestHeaderEditor
		/// </summary>
		public WebRequestPage()
		{
			// This call is required by the Windows.Forms Form Designer.
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(WebRequestPage));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lnkEditHeaders = new System.Windows.Forms.LinkLabel();
			this.txtRequestID = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtUrl = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.grpRequestHeaders = new System.Windows.Forms.GroupBox();
			this.pgHeaders = new Ecyware.GreenBlue.Controls.FlatPropertyGrid();
			this.chkUsePostData = new System.Windows.Forms.CheckBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.groupBox1.SuspendLayout();
			this.grpRequestHeaders.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.chkUsePostData);
			this.groupBox1.Controls.Add(this.lnkEditHeaders);
			this.groupBox1.Controls.Add(this.txtRequestID);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtUrl);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(600, 84);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Request Description";
			// 
			// lnkEditHeaders
			// 
			this.lnkEditHeaders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lnkEditHeaders.Location = new System.Drawing.Point(516, 49);
			this.lnkEditHeaders.Name = "lnkEditHeaders";
			this.lnkEditHeaders.Size = new System.Drawing.Size(72, 18);
			this.lnkEditHeaders.TabIndex = 8;
			this.lnkEditHeaders.TabStop = true;
			this.lnkEditHeaders.Text = "Edit Headers";
			this.lnkEditHeaders.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEditHeaders_LinkClicked);
			// 
			// txtRequestID
			// 
			this.txtRequestID.Location = new System.Drawing.Point(84, 48);
			this.txtRequestID.Name = "txtRequestID";
			this.txtRequestID.Size = new System.Drawing.Size(258, 20);
			this.txtRequestID.TabIndex = 7;
			this.txtRequestID.Text = "";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(12, 50);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(66, 18);
			this.label2.TabIndex = 2;
			this.label2.Text = "Request ID";
			// 
			// txtUrl
			// 
			this.txtUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtUrl.BackColor = System.Drawing.Color.White;
			this.txtUrl.Location = new System.Drawing.Point(48, 18);
			this.txtUrl.Name = "txtUrl";
			this.txtUrl.Size = new System.Drawing.Size(540, 20);
			this.txtUrl.TabIndex = 1;
			this.txtUrl.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(24, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "Url";
			// 
			// grpRequestHeaders
			// 
			this.grpRequestHeaders.Controls.Add(this.pgHeaders);
			this.grpRequestHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpRequestHeaders.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpRequestHeaders.Location = new System.Drawing.Point(0, 84);
			this.grpRequestHeaders.Name = "grpRequestHeaders";
			this.grpRequestHeaders.Size = new System.Drawing.Size(600, 316);
			this.grpRequestHeaders.TabIndex = 4;
			this.grpRequestHeaders.TabStop = false;
			this.grpRequestHeaders.Text = "Headers";
			// 
			// pgHeaders
			// 
			this.pgHeaders.CommandsVisibleIfAvailable = true;
			this.pgHeaders.Dock = System.Windows.Forms.DockStyle.Top;
			this.pgHeaders.LargeButtons = false;
			this.pgHeaders.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.pgHeaders.Location = new System.Drawing.Point(3, 16);
			this.pgHeaders.Name = "pgHeaders";
			this.pgHeaders.Size = new System.Drawing.Size(594, 296);
			this.pgHeaders.TabIndex = 9;
			this.pgHeaders.Text = "PropertyGrid";
			this.pgHeaders.ViewBackColor = System.Drawing.SystemColors.Window;
			this.pgHeaders.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// chkUsePostData
			// 
			this.chkUsePostData.Location = new System.Drawing.Point(354, 48);
			this.chkUsePostData.Name = "chkUsePostData";
			this.chkUsePostData.TabIndex = 9;
			this.chkUsePostData.Text = "Use Post Data";
			this.toolTip1.SetToolTip(this.chkUsePostData, "Set Post Data to true for use in SOAP or REST services");
			this.chkUsePostData.CheckedChanged += new System.EventHandler(this.chkUsePostData_CheckedChanged);
			// 
			// WebRequestPage
			// 
			this.Controls.Add(this.grpRequestHeaders);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "WebRequestPage";
			this.Size = new System.Drawing.Size(600, 400);
			this.groupBox1.ResumeLayout(false);
			this.grpRequestHeaders.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion


		/// <summary>
		/// Loads the request.
		/// </summary>
		/// <param name="index"> The selected web request index.</param>
		/// <param name="scripting"> The scripting application.</param>
		/// <param name="request"> The current web request.</param>
		public override void LoadRequest(int index, ScriptingApplication scripting ,Ecyware.GreenBlue.Engine.Scripting.WebRequest request)
		{
			if ( request is PostWebRequest )
			{
				chkUsePostData.Visible = true;
				chkUsePostData.Checked = ((PostWebRequest)request).UsePostData;
			} 
			else 
			{
				chkUsePostData.Visible = false;
			}

			base.LoadRequest (index, scripting, request);
			txtUrl.Text = request.Url;
			txtRequestID.Text = request.ID;
			this.LoadHttpProperties(request.RequestHttpSettings);
		}

		/// <summary>
		/// Gets or sets the WebRequest.
		/// </summary>
		public override Ecyware.GreenBlue.Engine.Scripting.WebRequest WebRequest
		{
			get
			{
				if ( base.WebRequest != null )
				{
					base.WebRequest.Url = this.txtUrl.Text;
					base.WebRequest.ID = this.txtRequestID.Text;
					UpdateHttpPropertiesFromPropertyGrid(base.WebRequest);
				}

				return base.WebRequest;
			}
		}

		
		/// <summary>
		/// Loads the http properties.
		/// </summary>
		/// <param name="httpProperties"> The HttpProperties type.</param>
		public void LoadHttpProperties(HttpProperties httpProperties)
		{
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

			// Additional Headers
			for (int i=0;i<httpProperties.AdditionalHeaders.Length;i++)
			{				
				PropertySpec property = new PropertySpec(httpProperties.AdditionalHeaders[i].Name,typeof(string),"Additional Headers");
				bag.Properties.Add(property);
				bag[httpProperties.AdditionalHeaders[i].Name] = httpProperties.AdditionalHeaders[i].Value;
			}
			// PropertySpec addonHeaders = new PropertySpec("Additional headers",typeof(string[]), category);
			// PropertySpec ntlmAuth = new PropertySpec("Windows Integrated Security", typeof(bool), category2);
			// accept.ConverterTypeName = "Ecyware.GreenBlue.Controls.HttpPropertiesWrapper";

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
		/// Updates the http properties.
		/// </summary>
		/// <returns> A HttpProperties.</returns>
		private void UpdateHttpPropertiesFromPropertyGrid(Ecyware.GreenBlue.Engine.Scripting.WebRequest request)
		{			
			HttpProperties settings = request.RequestHttpSettings;
			PropertyTable bag = (PropertyTable)pgHeaders.SelectedObject;
			settings.Accept = (string)bag["Accept"];

			if ( settings.AuthenticationSettings.UseBasicAuthentication )
			{
				settings.AuthenticationSettings = settings.AuthenticationSettings;
			} 
			else 
			{
				settings.AuthenticationSettings.UseNTLMAuthentication =settings.AuthenticationSettings.UseNTLMAuthentication;
			}

			settings.ContentLength = Convert.ToInt64(bag["Content Length"]);
			settings.ContentType = (string)bag["Content Type"];
			settings.IfModifiedSince = Convert.ToDateTime(bag["If Modified Since"]);
			settings.KeepAlive = (bool)bag["Keep Alive"];
			settings.MediaType = (string)bag["Media Type"];
			settings.Pipeline = (bool)bag["Pipeline"];
			settings.Referer = (string)bag["Referer"];
			settings.SendChunked = (bool)bag["Send Chunked"];
			settings.TransferEncoding = (string)bag["Transfer Encoding"];
			settings.UserAgent = (string)bag["User Agent"];			

			// Additional Headers
			for (int i=0;i<settings.AdditionalHeaders.Length;i++)
			{					
				// Update additional headers values.
				settings.SetWebHeader(settings.AdditionalHeaders[i].Name,(string)bag[settings.AdditionalHeaders[i].Name]);				
			}

			request.RequestHttpSettings = settings;

		}

		private void lnkEditHeaders_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			EditHeaderDialog dialog = new EditHeaderDialog();
			dialog.ClientSettings = base.WebRequest.RequestHttpSettings;

			if ( dialog.ShowDialog() == DialogResult.OK )
			{
				base.WebRequest.RequestHttpSettings = dialog.ClientSettings;
				LoadHttpProperties(base.WebRequest.RequestHttpSettings);
			}
		}

		private void chkUsePostData_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( base.WebRequest is PostWebRequest )
			{
				((PostWebRequest)base.WebRequest).UsePostData = chkUsePostData.Checked;
			}
//			if ( base.WebRequest is PutWebRequest )
//			{
//				((PutWebRequest)base.WebRequest).UsePostData = chkUsePostData.Checked;
//			}
//			if ( base.WebRequest is SoapHttpWebRequest )
//			{
//				((SoapHttpWebRequest)base.WebRequest).UsePostData = chkUsePostData.Checked;
//			}			
		}
	}
}
