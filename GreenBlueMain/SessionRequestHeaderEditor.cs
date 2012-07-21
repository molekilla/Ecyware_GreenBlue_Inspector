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
using Ecyware.GreenBlue.HtmlDom;
using Ecyware.GreenBlue.HtmlCommand;
using Ecyware.GreenBlue.WebUnitTestManager;
using Ecyware.GreenBlue.WebUnitTestCommand;
using Ecyware.GreenBlue.Protocols.Http;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Contains the definition for the SessionRequestHeaderEditor control.
	/// </summary>
	public class SessionRequestHeaderEditor : BaseSessionDesignerUserControl
	{
		private SessionRequest _sessionRequest = null;
		private ArrayList filterList = new ArrayList();
		internal HttpGetPageEventHandler HttpGetEvent;

		private System.Windows.Forms.GroupBox grpRequestHeaders;
		private System.Windows.Forms.GroupBox grpUrlInfo;
		private System.Windows.Forms.GroupBox grpResponseHeaders;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtUrl;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblStatusCode;
		private System.Windows.Forms.Label lblStatusDescription;
		private System.Windows.Forms.ListView lvRequestHeader;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colValue;
		private System.Windows.Forms.ListView lvResponseHeader;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.LinkLabel lnkNavigate;
		private System.Windows.Forms.CheckBox chkUpdateUrl;
		private System.Windows.Forms.LinkLabel lnkEditHeaders;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new SessionRequestHeaderEditor
		/// </summary>
		public SessionRequestHeaderEditor()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			filterList.Add("keep alive");
			filterList.Add("host");
			filterList.Add("connection");
			filterList.Add("pipelined");
			filterList.Add("send chunked");
			filterList.Add("content-type");
			filterList.Add("cookie");
			filterList.Add("user-agent");
		}
		/// <summary>
		/// Gets or sets a session request.
		/// </summary>
		public SessionRequest WebSessionRequest
		{
			get
			{
				return _sessionRequest;
			}
			set
			{
				_sessionRequest = value;
			}
		}

		/// <summary>
		/// Loads the header editor.
		/// </summary>
		public void LoadHeaderEditor()
		{
			if ( this.WebSessionRequest.RequestType == HttpRequestType.GET )
			{
				this.lnkNavigate.Visible = true;
			} else if ( this.WebSessionRequest.RequestType == HttpRequestType.POST ) {
				this.lnkNavigate.Visible = false;
			}
			this.txtUrl.Text = this.WebSessionRequest.Url.ToString();
			this.lblStatusCode.Text = this.WebSessionRequest.StatusCode.ToString();
			this.lblStatusDescription.Text = this.WebSessionRequest.StatusDescription;

			this.chkUpdateUrl.Checked = this.WebSessionRequest.UpdateSessionUrl;

			FillHeaders();
		}

		/// <summary>
		/// Fills the headers.
		/// </summary>
		public void FillHeaders()
		{
			if ( WebSessionRequest != null )
			{
				lvResponseHeader.Items.Clear();
				lvRequestHeader.Items.Clear();

				if ( WebSessionRequest.ResponseHeaders != null )
				{
					foreach (DictionaryEntry de in WebSessionRequest.ResponseHeaders)
					{
						ListViewItem lvi = new ListViewItem();
						lvResponseHeader.Items.Add(lvi);

						lvi.Text = (string)de.Key;
						lvi.SubItems.Add(Convert.ToString(de.Value));
					}
				}

				if ( WebSessionRequest.RequestHttpSettings != null )
				{
					UpdateRequestHeaderListView();
				} else {
					if ( WebSessionRequest.RequestHeaders != null )
					{
						foreach (DictionaryEntry de in WebSessionRequest.RequestHeaders)
						{
							string key = (string)de.Key;

							if ( !filterList.Contains(key.ToLower()) )
							{
								ListViewItem lvi = new ListViewItem();
								lvRequestHeader.Items.Add(lvi);
								lvi.Text = key;
								lvi.SubItems.Add(Convert.ToString(de.Value));
							}
						}
					}
				}
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.grpRequestHeaders = new System.Windows.Forms.GroupBox();
			this.lnkEditHeaders = new System.Windows.Forms.LinkLabel();
			this.lvRequestHeader = new System.Windows.Forms.ListView();
			this.colName = new System.Windows.Forms.ColumnHeader();
			this.colValue = new System.Windows.Forms.ColumnHeader();
			this.grpUrlInfo = new System.Windows.Forms.GroupBox();
			this.chkUpdateUrl = new System.Windows.Forms.CheckBox();
			this.lnkNavigate = new System.Windows.Forms.LinkLabel();
			this.lblStatusDescription = new System.Windows.Forms.Label();
			this.lblStatusCode = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtUrl = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.grpResponseHeaders = new System.Windows.Forms.GroupBox();
			this.lvResponseHeader = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.grpRequestHeaders.SuspendLayout();
			this.grpUrlInfo.SuspendLayout();
			this.grpResponseHeaders.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpRequestHeaders
			// 
			this.grpRequestHeaders.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grpRequestHeaders.Controls.Add(this.lnkEditHeaders);
			this.grpRequestHeaders.Controls.Add(this.lvRequestHeader);
			this.grpRequestHeaders.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpRequestHeaders.Location = new System.Drawing.Point(6, 114);
			this.grpRequestHeaders.Name = "grpRequestHeaders";
			this.grpRequestHeaders.Size = new System.Drawing.Size(588, 138);
			this.grpRequestHeaders.TabIndex = 0;
			this.grpRequestHeaders.TabStop = false;
			this.grpRequestHeaders.Text = "Request Headers";
			// 
			// lnkEditHeaders
			// 
			this.lnkEditHeaders.Location = new System.Drawing.Point(6, 18);
			this.lnkEditHeaders.Name = "lnkEditHeaders";
			this.lnkEditHeaders.Size = new System.Drawing.Size(78, 18);
			this.lnkEditHeaders.TabIndex = 2;
			this.lnkEditHeaders.TabStop = true;
			this.lnkEditHeaders.Text = "Edit Headers";
			this.lnkEditHeaders.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEditHeaders_LinkClicked);
			// 
			// lvRequestHeader
			// 
			this.lvRequestHeader.AllowColumnReorder = true;
			this.lvRequestHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lvRequestHeader.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							  this.colName,
																							  this.colValue});
			this.lvRequestHeader.FullRowSelect = true;
			this.lvRequestHeader.LabelEdit = true;
			this.lvRequestHeader.Location = new System.Drawing.Point(6, 36);
			this.lvRequestHeader.Name = "lvRequestHeader";
			this.lvRequestHeader.Size = new System.Drawing.Size(576, 96);
			this.lvRequestHeader.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvRequestHeader.TabIndex = 1;
			this.lvRequestHeader.View = System.Windows.Forms.View.Details;
			// 
			// colName
			// 
			this.colName.Text = "Name";
			this.colName.Width = 110;
			// 
			// colValue
			// 
			this.colValue.Text = "Value";
			this.colValue.Width = 350;
			// 
			// grpUrlInfo
			// 
			this.grpUrlInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grpUrlInfo.Controls.Add(this.chkUpdateUrl);
			this.grpUrlInfo.Controls.Add(this.lnkNavigate);
			this.grpUrlInfo.Controls.Add(this.lblStatusDescription);
			this.grpUrlInfo.Controls.Add(this.lblStatusCode);
			this.grpUrlInfo.Controls.Add(this.label3);
			this.grpUrlInfo.Controls.Add(this.label2);
			this.grpUrlInfo.Controls.Add(this.txtUrl);
			this.grpUrlInfo.Controls.Add(this.label1);
			this.grpUrlInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpUrlInfo.Location = new System.Drawing.Point(6, 6);
			this.grpUrlInfo.Name = "grpUrlInfo";
			this.grpUrlInfo.Size = new System.Drawing.Size(588, 100);
			this.grpUrlInfo.TabIndex = 1;
			this.grpUrlInfo.TabStop = false;
			this.grpUrlInfo.Text = "Session Request General information";
			// 
			// chkUpdateUrl
			// 
			this.chkUpdateUrl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkUpdateUrl.Location = new System.Drawing.Point(402, 66);
			this.chkUpdateUrl.Name = "chkUpdateUrl";
			this.chkUpdateUrl.Size = new System.Drawing.Size(168, 24);
			this.chkUpdateUrl.TabIndex = 7;
			this.chkUpdateUrl.Text = "Update url while running";
			this.chkUpdateUrl.Visible = false;
			this.chkUpdateUrl.CheckedChanged += new System.EventHandler(this.chkUpdateUrl_CheckedChanged);
			// 
			// lnkNavigate
			// 
			this.lnkNavigate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lnkNavigate.Location = new System.Drawing.Point(402, 48);
			this.lnkNavigate.Name = "lnkNavigate";
			this.lnkNavigate.Size = new System.Drawing.Size(144, 18);
			this.lnkNavigate.TabIndex = 6;
			this.lnkNavigate.TabStop = true;
			this.lnkNavigate.Text = "Click here to navigate to url";
			this.lnkNavigate.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkNavigate_LinkClicked);
			// 
			// lblStatusDescription
			// 
			this.lblStatusDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblStatusDescription.Location = new System.Drawing.Point(114, 72);
			this.lblStatusDescription.Name = "lblStatusDescription";
			this.lblStatusDescription.Size = new System.Drawing.Size(240, 18);
			this.lblStatusDescription.TabIndex = 5;
			this.lblStatusDescription.Text = "Status Code";
			// 
			// lblStatusCode
			// 
			this.lblStatusCode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblStatusCode.Location = new System.Drawing.Point(114, 48);
			this.lblStatusCode.Name = "lblStatusCode";
			this.lblStatusCode.Size = new System.Drawing.Size(102, 18);
			this.lblStatusCode.TabIndex = 4;
			this.lblStatusCode.Text = "Status Code";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(12, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 18);
			this.label3.TabIndex = 3;
			this.label3.Text = "Status Description";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(12, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(66, 18);
			this.label2.TabIndex = 2;
			this.label2.Text = "Status Code";
			// 
			// txtUrl
			// 
			this.txtUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtUrl.BackColor = System.Drawing.Color.White;
			this.txtUrl.Location = new System.Drawing.Point(48, 18);
			this.txtUrl.Name = "txtUrl";
			this.txtUrl.ReadOnly = true;
			this.txtUrl.Size = new System.Drawing.Size(528, 20);
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
			// grpResponseHeaders
			// 
			this.grpResponseHeaders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grpResponseHeaders.Controls.Add(this.lvResponseHeader);
			this.grpResponseHeaders.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpResponseHeaders.Location = new System.Drawing.Point(6, 258);
			this.grpResponseHeaders.Name = "grpResponseHeaders";
			this.grpResponseHeaders.Size = new System.Drawing.Size(588, 138);
			this.grpResponseHeaders.TabIndex = 2;
			this.grpResponseHeaders.TabStop = false;
			this.grpResponseHeaders.Text = "Response Headers";
			// 
			// lvResponseHeader
			// 
			this.lvResponseHeader.AllowColumnReorder = true;
			this.lvResponseHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lvResponseHeader.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							   this.columnHeader1,
																							   this.columnHeader2});
			this.lvResponseHeader.FullRowSelect = true;
			this.lvResponseHeader.LabelEdit = true;
			this.lvResponseHeader.Location = new System.Drawing.Point(6, 18);
			this.lvResponseHeader.Name = "lvResponseHeader";
			this.lvResponseHeader.Size = new System.Drawing.Size(576, 114);
			this.lvResponseHeader.Sorting = System.Windows.Forms.SortOrder.Ascending;
			this.lvResponseHeader.TabIndex = 2;
			this.lvResponseHeader.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Name";
			this.columnHeader1.Width = 110;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Value";
			this.columnHeader2.Width = 350;
			// 
			// SessionRequestHeaderEditor
			// 
			this.Controls.Add(this.grpResponseHeaders);
			this.Controls.Add(this.grpUrlInfo);
			this.Controls.Add(this.grpRequestHeaders);
			this.Name = "SessionRequestHeaderEditor";
			this.Size = new System.Drawing.Size(600, 400);
			this.grpRequestHeaders.ResumeLayout(false);
			this.grpUrlInfo.ResumeLayout(false);
			this.grpResponseHeaders.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		public override void UpdateSessionRequestData()
		{
			// do nothing yet.
		}

		private void chkUpdateUrl_CheckedChanged(object sender, System.EventArgs e)
		{
			this.WebSessionRequest.UpdateSessionUrl = this.chkUpdateUrl.Checked;
		}

		private void lnkEditHeaders_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			// display edit headers dialog box
			RequestHeaderDialog requestHeaderDialog = new RequestHeaderDialog();
			requestHeaderDialog.LoadHttpProperties(this.WebSessionRequest.RequestHttpSettings);

			if ( requestHeaderDialog.ShowDialog() == DialogResult.OK )
			{
				this.WebSessionRequest.RequestHttpSettings = requestHeaderDialog.GetHttpProperties();

				// update header list view
				UpdateRequestHeaderListView();
			}

			requestHeaderDialog.Close();
		}

		/// <summary>
		/// Updates the request header list view.
		/// </summary>
		private void UpdateRequestHeaderListView()
		{
			this.lvRequestHeader.Items.Clear();

			HttpProperties settings = this.WebSessionRequest.RequestHttpSettings;

			// accept
			ListViewItem lvi = new ListViewItem();
			lvRequestHeader.Items.Add(lvi);
			lvi.Text = "Accept";
			lvi.SubItems.Add(settings.Accept);

			// content length
			lvi = new ListViewItem();
			lvRequestHeader.Items.Add(lvi);
			lvi.Text = "Content Length";
			lvi.SubItems.Add(Convert.ToString(settings.ContentLength));

			// content type
			lvi = new ListViewItem();
			lvRequestHeader.Items.Add(lvi);
			lvi.Text = "Content Type";
			lvi.SubItems.Add(settings.ContentType);

			// if modified since
			lvi = new ListViewItem();
			lvRequestHeader.Items.Add(lvi);
			lvi.Text = "If Modified Since";
			lvi.SubItems.Add(Convert.ToString(settings.IfModifiedSince));

			// keep alive
			lvi = new ListViewItem();
			lvRequestHeader.Items.Add(lvi);
			lvi.Text = "Keep Alive";
			lvi.SubItems.Add(Convert.ToString(settings.KeepAlive));

			// media type
			lvi = new ListViewItem();
			lvRequestHeader.Items.Add(lvi);
			lvi.Text = "Media Type";
			lvi.SubItems.Add(settings.MediaType);

			// Pipeline
			lvi = new ListViewItem();
			lvRequestHeader.Items.Add(lvi);
			lvi.Text = "Pipeline";
			lvi.SubItems.Add(Convert.ToString(settings.Pipeline));

			// Referer
			lvi = new ListViewItem();
			lvRequestHeader.Items.Add(lvi);
			lvi.Text = "Referer";
			lvi.SubItems.Add(settings.Referer);

			// Send Chunked
			lvi = new ListViewItem();
			lvRequestHeader.Items.Add(lvi);
			lvi.Text = "Send Chunked";
			lvi.SubItems.Add(Convert.ToString(settings.SendChunked));

			// Transfer Encoding
			lvi = new ListViewItem();
			lvRequestHeader.Items.Add(lvi);
			lvi.Text = "Transfer Encoding";
			lvi.SubItems.Add(settings.TransferEncoding);

			// User agent
			lvi = new ListViewItem();
			lvRequestHeader.Items.Add(lvi);
			lvi.Text = "User Agent";
			lvi.SubItems.Add(settings.UserAgent);
		}

		private void lnkNavigate_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if ( this.WebSessionRequest.RequestType == HttpRequestType.GET )
			{
				RequestGetEventArgs args = new RequestGetEventArgs();
				args.Url = this.WebSessionRequest.Url.ToString();
				HttpGetEvent(this, args);
			} 
		}
	}
}
