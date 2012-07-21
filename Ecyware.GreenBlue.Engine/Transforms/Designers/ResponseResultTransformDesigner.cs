using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms.Designers;
using Ecyware.GreenBlue.Engine.Transforms;


namespace Ecyware.GreenBlue.Engine.Transforms.Designers
{
	/// <summary>
	/// Contains the designer for the response result.
	/// </summary>
	public class ResponseResultTransformDesigner : UITransformEditor
	{
		private Transport _transport;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.LinkLabel lnkSelectTransport;
		private System.Windows.Forms.ComboBox cmbTransports;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chkUseSession;
		private System.Windows.Forms.TextBox txtSessionName;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new ResponseResultTransformDesigner.
		/// </summary>
		public ResponseResultTransformDesigner()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.cmbTransports.Items.Clear();
			this.cmbTransports.Items.AddRange(TransportValueDialogs);
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
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtSessionName = new System.Windows.Forms.TextBox();
			this.chkUseSession = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lnkSelectTransport = new System.Windows.Forms.LinkLabel();
			this.cmbTransports = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.groupBox1);
			this.groupBox2.Controls.Add(this.lnkSelectTransport);
			this.groupBox2.Controls.Add(this.cmbTransports);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(480, 210);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Results Transform";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.txtSessionName);
			this.groupBox1.Controls.Add(this.chkUseSession);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(12, 60);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(336, 138);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Session values";
			// 
			// txtSessionName
			// 
			this.txtSessionName.Enabled = false;
			this.txtSessionName.Location = new System.Drawing.Point(96, 54);
			this.txtSessionName.Name = "txtSessionName";
			this.txtSessionName.Size = new System.Drawing.Size(198, 20);
			this.txtSessionName.TabIndex = 2;
			this.txtSessionName.Text = "";
			// 
			// chkUseSession
			// 
			this.chkUseSession.Location = new System.Drawing.Point(12, 24);
			this.chkUseSession.Name = "chkUseSession";
			this.chkUseSession.Size = new System.Drawing.Size(138, 24);
			this.chkUseSession.TabIndex = 1;
			this.chkUseSession.Text = "Use Session Value";
			this.chkUseSession.CheckedChanged += new System.EventHandler(this.chkUseSession_CheckedChanged);
			// 
			// label1
			// 
			this.label1.Enabled = false;
			this.label1.Location = new System.Drawing.Point(12, 55);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Session Name";
			// 
			// lnkSelectTransport
			// 
			this.lnkSelectTransport.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lnkSelectTransport.Location = new System.Drawing.Point(318, 36);
			this.lnkSelectTransport.Name = "lnkSelectTransport";
			this.lnkSelectTransport.Size = new System.Drawing.Size(30, 18);
			this.lnkSelectTransport.TabIndex = 2;
			this.lnkSelectTransport.TabStop = true;
			this.lnkSelectTransport.Text = "[...]";
			this.lnkSelectTransport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkSelectTransport_LinkClicked);
			// 
			// cmbTransports
			// 
			this.cmbTransports.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTransports.Items.AddRange(new object[] {
															   "Smtp",
															   "Gmail",
															   "Database",
															   "Blogger",
															   "Session"});
			this.cmbTransports.Location = new System.Drawing.Point(138, 30);
			this.cmbTransports.Name = "cmbTransports";
			this.cmbTransports.Size = new System.Drawing.Size(175, 21);
			this.cmbTransports.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 30);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 20);
			this.label2.TabIndex = 0;
			this.label2.Text = "Transport";
			// 
			// ResponseResultTransformDesigner
			// 
			this.Controls.Add(this.groupBox2);
			this.Name = "ResponseResultTransformDesigner";
			this.Size = new System.Drawing.Size(480, 210);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Override Methods and Properties

		public override void Clear()
		{
			base.Clear ();

			//this.chkAppend.Checked = false;
		}

		public override void LoadTransformEditorValues(int requestIndex, ScriptingApplication scriptingData, WebTransform transform)
		{
			base.LoadTransformEditorValues (requestIndex, scriptingData, transform);

			ResponseResultTransform rtransform = (ResponseResultTransform)base.WebTransform;

			this.cmbTransports.SelectedIndex = GetTransportValueComboIndex(rtransform.Transport);

			if ( rtransform != null )
			{				
				this.chkUseSession.Checked = rtransform.UseSession;
				this.txtSessionName.Text = rtransform.SessionName;
				_transport = rtransform.Transport;
			}
		}

		private void lnkSelectTransport_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			_transport = this.ShowTransportValueDialog(this.cmbTransports.SelectedIndex, _transport);
		}

		private void chkUseSession_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( chkUseSession.Checked )
			{
				this.label1.Enabled = true;
				this.txtSessionName.Enabled = true;
			} 
			else 
			{
				this.label1.Enabled = false;
				this.txtSessionName.Enabled = false;
			}
		}

		/// <summary>
		/// Gets the web transform.
		/// </summary>
		public override WebTransform WebTransform
		{
			get
			{				
				if ( base.WebTransform != null )
				{
					ResponseResultTransform transform = (ResponseResultTransform)base.WebTransform;					
					transform.Transport = this._transport;
					transform.UseSession = this.chkUseSession.Checked;
					transform.SessionName = txtSessionName.Text;
				}

				return base.WebTransform;
			}
		}

		#endregion
	}
}
