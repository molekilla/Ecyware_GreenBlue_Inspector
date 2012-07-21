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
	/// Contains the definition for the SessionInfoForm control.
	/// </summary>
	public class SessionInfoForm : BaseSessionDesignerUserControl
	{
		internal UpdateSessionEventHandler UpdateSessionEvent;

		private Session _session = null;

		private System.Windows.Forms.GroupBox grpUrlInfo;
		private System.Windows.Forms.Label lblSessionCreated;
		private System.Windows.Forms.CheckBox chkAllowSafeRequestBacktracking;
		private System.Windows.Forms.CheckBox chkUpdateCookies;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new SessionRequestHeaderEditor
		/// </summary>
		public SessionInfoForm()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

		}
		/// <summary>
		/// Gets or sets a session request.
		/// </summary>
		public Session SelectedSession
		{
			get
			{
				return _session;
			}
			set
			{
				_session = value;
			}
		}

		/// <summary>
		/// Sets the session settings.
		/// </summary>
		public void SetSessionSettings()
		{
			this.lblSessionCreated.Text = "Session Created: " + this.SelectedSession.SessionDate.ToString();
			this.chkAllowSafeRequestBacktracking.Checked = this.SelectedSession.AllowSafeRequestBacktracking;
			this.chkUpdateCookies.Checked = this.SelectedSession.IsCookieUpdatable;
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
			this.grpUrlInfo = new System.Windows.Forms.GroupBox();
			this.lblSessionCreated = new System.Windows.Forms.Label();
			this.chkAllowSafeRequestBacktracking = new System.Windows.Forms.CheckBox();
			this.chkUpdateCookies = new System.Windows.Forms.CheckBox();
			this.grpUrlInfo.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpUrlInfo
			// 
			this.grpUrlInfo.Controls.Add(this.lblSessionCreated);
			this.grpUrlInfo.Controls.Add(this.chkAllowSafeRequestBacktracking);
			this.grpUrlInfo.Controls.Add(this.chkUpdateCookies);
			this.grpUrlInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpUrlInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpUrlInfo.Location = new System.Drawing.Point(0, 0);
			this.grpUrlInfo.Name = "grpUrlInfo";
			this.grpUrlInfo.Size = new System.Drawing.Size(600, 400);
			this.grpUrlInfo.TabIndex = 1;
			this.grpUrlInfo.TabStop = false;
			this.grpUrlInfo.Text = "Session General information";
			// 
			// lblSessionCreated
			// 
			this.lblSessionCreated.Location = new System.Drawing.Point(24, 24);
			this.lblSessionCreated.Name = "lblSessionCreated";
			this.lblSessionCreated.Size = new System.Drawing.Size(252, 18);
			this.lblSessionCreated.TabIndex = 9;
			this.lblSessionCreated.Text = "Session Created:";
			// 
			// chkAllowSafeRequestBacktracking
			// 
			this.chkAllowSafeRequestBacktracking.Location = new System.Drawing.Point(36, 78);
			this.chkAllowSafeRequestBacktracking.Name = "chkAllowSafeRequestBacktracking";
			this.chkAllowSafeRequestBacktracking.Size = new System.Drawing.Size(192, 24);
			this.chkAllowSafeRequestBacktracking.TabIndex = 8;
			this.chkAllowSafeRequestBacktracking.Text = "Allow safe request backtracking";
			this.chkAllowSafeRequestBacktracking.CheckedChanged += new System.EventHandler(this.chkAllowSafeRequestBacktracking_CheckedChanged);
			// 
			// chkUpdateCookies
			// 
			this.chkUpdateCookies.Location = new System.Drawing.Point(36, 54);
			this.chkUpdateCookies.Name = "chkUpdateCookies";
			this.chkUpdateCookies.Size = new System.Drawing.Size(192, 24);
			this.chkUpdateCookies.TabIndex = 7;
			this.chkUpdateCookies.Text = "Always update session cookies";
			this.chkUpdateCookies.CheckedChanged += new System.EventHandler(this.chkUpdateCookies_CheckedChanged);
			// 
			// SessionInfoForm
			// 
			this.Controls.Add(this.grpUrlInfo);
			this.Name = "SessionInfoForm";
			this.Size = new System.Drawing.Size(600, 400);
			this.grpUrlInfo.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// Updates the session data.
		/// </summary>
		public override void UpdateSessionData()
		{
			UpdateSessionEventArgs args = new UpdateSessionEventArgs();
			args.WebSession = this.SelectedSession;
			this.UpdateSessionEvent(this, args);
		}

		private void chkUpdateCookies_CheckedChanged(object sender, System.EventArgs e)
		{
			this.SelectedSession.IsCookieUpdatable = this.chkUpdateCookies.Checked;
		}

		private void chkAllowSafeRequestBacktracking_CheckedChanged(object sender, System.EventArgs e)
		{
			this.SelectedSession.AllowSafeRequestBacktracking = this.chkAllowSafeRequestBacktracking.Checked;
		}
	}
}
