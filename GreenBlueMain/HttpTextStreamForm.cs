// All rights reserved.
// Title: GreenBlue Project
// Author(s): Rogelio Morrell C.
// Date: November 2003
// Add additional authors here
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using Ecyware.GreenBlue.GreenBlueGUI;
using Ecyware.GreenBlue.HtmlDom;
using Ecyware.GreenBlue.Protocols.Http;
using Ecyware.GreenBlue.HtmlProcessor;
using Ecyware.GreenBlue.Controls;

namespace Ecyware.GreenBlue.GreenBlueMain
{



	/// <summary>
	/// Summary description for HttpTextStreamForm.
	/// </summary>
	public class HttpTextStreamForm : BasePluginForm
	{
		internal event InspectorStartRequestEventHandler StartEvent;
		internal event InspectorCancelRequestEventHandler CancelEvent;

		internal System.Windows.Forms.RichTextBox txtHTTPStream;
		internal System.Windows.Forms.ComboBox cmbUrl;
		private System.Windows.Forms.GroupBox grpHTTPCommands;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Button cmdGo;
		private System.Windows.Forms.Label label1;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public HttpTextStreamForm()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

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
			this.cmbUrl = new System.Windows.Forms.ComboBox();
			this.txtHTTPStream = new System.Windows.Forms.RichTextBox();
			this.grpHTTPCommands = new System.Windows.Forms.GroupBox();
			this.btnStop = new System.Windows.Forms.Button();
			this.cmdGo = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.grpHTTPCommands.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmbUrl
			// 
			this.cmbUrl.Location = new System.Drawing.Point(60, 21);
			this.cmbUrl.Name = "cmbUrl";
			this.cmbUrl.Size = new System.Drawing.Size(402, 21);
			this.cmbUrl.TabIndex = 0;
			this.cmbUrl.Text = "http://www.ecyware.com";
			this.cmbUrl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUrl_KeyPress);
			this.cmbUrl.SelectedIndexChanged += new System.EventHandler(this.cmdGo_Click);
			// 
			// txtHTTPStream
			// 
			this.txtHTTPStream.AcceptsTab = true;
			this.txtHTTPStream.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtHTTPStream.AutoWordSelection = true;
			this.txtHTTPStream.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtHTTPStream.Location = new System.Drawing.Point(0, 54);
			this.txtHTTPStream.Name = "txtHTTPStream";
			this.txtHTTPStream.Size = new System.Drawing.Size(600, 312);
			this.txtHTTPStream.TabIndex = 7;
			this.txtHTTPStream.Text = "";
			this.txtHTTPStream.WordWrap = false;
			// 
			// grpHTTPCommands
			// 
			this.grpHTTPCommands.Controls.Add(this.btnStop);
			this.grpHTTPCommands.Controls.Add(this.cmdGo);
			this.grpHTTPCommands.Controls.Add(this.cmbUrl);
			this.grpHTTPCommands.Controls.Add(this.label1);
			this.grpHTTPCommands.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpHTTPCommands.Location = new System.Drawing.Point(0, 0);
			this.grpHTTPCommands.Name = "grpHTTPCommands";
			this.grpHTTPCommands.Size = new System.Drawing.Size(600, 54);
			this.grpHTTPCommands.TabIndex = 6;
			this.grpHTTPCommands.TabStop = false;
			this.grpHTTPCommands.Text = "Navigate";
			// 
			// btnStop
			// 
			this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnStop.Location = new System.Drawing.Point(534, 18);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(60, 24);
			this.btnStop.TabIndex = 8;
			this.btnStop.Text = "Stop";
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// cmdGo
			// 
			this.cmdGo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdGo.Location = new System.Drawing.Point(468, 18);
			this.cmdGo.Name = "cmdGo";
			this.cmdGo.Size = new System.Drawing.Size(60, 24);
			this.cmdGo.TabIndex = 5;
			this.cmdGo.Text = "Go";
			this.cmdGo.Click += new System.EventHandler(this.cmdGo_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Go to url:";
			// 
			// HttpTextStreamForm
			// 
			this.Controls.Add(this.grpHTTPCommands);
			this.Controls.Add(this.txtHTTPStream);
			this.Name = "HttpTextStreamForm";
			this.Size = new System.Drawing.Size(600, 366);
			this.grpHTTPCommands.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region UI events





		private void cmdGo_Click(object sender, System.EventArgs e)
		{
			GoUrl();
		}

		private void txtUrl_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ( e.KeyChar==(char)13 )
			{
				GoUrl();
				e.Handled=true;
			}
		}
		#endregion
		#region http requests methods
		/// <summary>
		/// Go to url and process http request.
		/// </summary>
		private void GoUrl()
		{
			// Add item to combo list
			if ( cmbUrl.Items.IndexOf(cmbUrl.Text) == -1 )
			{
				this.cmbUrl.Items.Add(cmbUrl.Text);
			}

			RequestGetEventArgs args = new RequestGetEventArgs();
			args.Url = this.cmbUrl.Text;
			StartEvent(this,args);
			//GetHttpRequest();
		}


		#endregion

		private void btnStop_Click(object sender, System.EventArgs e)
		{
			CancelEvent(this, new EventArgs());
		}

		#region Get parents properties
		private GBInspectorWorkspace GetExtendedWebSniffer
		{
			get
			{
				return (GBInspectorWorkspace)this.Parent.Parent.Parent;
			}
		}


#endregion

	}
}
