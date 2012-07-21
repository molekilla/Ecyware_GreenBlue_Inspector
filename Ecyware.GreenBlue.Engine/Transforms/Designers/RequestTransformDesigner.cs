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
	/// Summary description for UserControl1.
	/// </summary>
	public class RequestTransformDesigner : UITransformEditor
	{
		private ArrayList _headerList = new ArrayList();
		private ArrayList _requestFields = new ArrayList();
		
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.TextBox txtTransformDescription;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox cmbTransformValue;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmbRequestField;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new HeaderTransformDesigner.
		/// </summary>
		public RequestTransformDesigner()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			if ( cmbTransformValue.Items.Count == 0 )
			{
				cmbTransformValue.Items.AddRange(TransformValueDialogs);
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
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.cmbRequestField = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.txtTransformDescription = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.cmbTransformValue = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.cmbRequestField);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.linkLabel1);
			this.groupBox2.Controls.Add(this.txtTransformDescription);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.cmbTransformValue);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(480, 210);
			this.groupBox2.TabIndex = 0;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Request Transform";
			// 
			// cmbRequestField
			// 
			this.cmbRequestField.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbRequestField.Items.AddRange(new object[] {
																 "Use a header value",
																 "Use a default value"});
			this.cmbRequestField.Location = new System.Drawing.Point(168, 21);
			this.cmbRequestField.Name = "cmbRequestField";
			this.cmbRequestField.Size = new System.Drawing.Size(175, 21);
			this.cmbRequestField.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(132, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Apply Action";
			// 
			// linkLabel1
			// 
			this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.linkLabel1.Location = new System.Drawing.Point(348, 54);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(30, 18);
			this.linkLabel1.TabIndex = 4;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "[...]";
			this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			// 
			// txtTransformDescription
			// 
			this.txtTransformDescription.Location = new System.Drawing.Point(167, 78);
			this.txtTransformDescription.Multiline = true;
			this.txtTransformDescription.Name = "txtTransformDescription";
			this.txtTransformDescription.ReadOnly = true;
			this.txtTransformDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtTransformDescription.Size = new System.Drawing.Size(210, 90);
			this.txtTransformDescription.TabIndex = 6;
			this.txtTransformDescription.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 78);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(150, 24);
			this.label4.TabIndex = 5;
			this.label4.Text = "Value Description";
			// 
			// cmbTransformValue
			// 
			this.cmbTransformValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbTransformValue.Location = new System.Drawing.Point(167, 48);
			this.cmbTransformValue.Name = "cmbTransformValue";
			this.cmbTransformValue.Size = new System.Drawing.Size(175, 21);
			this.cmbTransformValue.TabIndex = 3;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(132, 18);
			this.label3.TabIndex = 2;
			this.label3.Text = "Value";
			// 
			// RequestTransformDesigner
			// 
			this.Controls.Add(this.groupBox2);
			this.Name = "RequestTransformDesigner";
			this.Size = new System.Drawing.Size(480, 210);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Override Methods and Properties

		public override void Clear()
		{
			base.Clear ();

			this.txtTransformDescription.Text = "";
		}

		public override void LoadTransformEditorValues(int requestIndex, ScriptingApplication scriptingData, WebTransform transform)
		{
			base.LoadTransformEditorValues (requestIndex, scriptingData, transform);

			this.Clear();
			RequestTransform requestTransform = (RequestTransform)base.WebTransform;

			TransformValue = requestTransform.UpdateTransformAction.Value;			
			txtTransformDescription.Text = requestTransform.UpdateTransformAction.Description;

			#region Request Fields
			if ( _requestFields.Count <= 0 )
			{				
				_requestFields.Add(new NameValueObject("Change Complete Url", "Url"));
				_requestFields.Add(new NameValueObject("Change Url Hostname", "ChangeUrlHostname"));
				_requestFields.Add(new NameValueObject("Change Url Path", "ChangeUrlPath"));								
				_requestFields.Add(new NameValueObject("Set Request ID", "ID"));
				_requestFields.Add(new NameValueObject("Set Basic Authentication Username", "Username"));
				_requestFields.Add(new NameValueObject("Set Basic Authentication Password", "Password"));

				this.cmbRequestField.DataSource = _requestFields;
				this.cmbRequestField.DisplayMember = "Name";
				this.cmbRequestField.ValueMember = "Value";
			}
			#endregion

			if ( requestTransform.RequestFieldName != null )
			{
				cmbRequestField.SelectedValue = requestTransform.RequestFieldName;
			}

			this.cmbTransformValue.SelectedIndex = this.GetTransformValueComboIndex(TransformValue);

			#region Headers Dialog
			if ( _headerList.Count <= 0 )
			{
				// Load the header combo list.
				_headerList.AddRange(HeaderTransform.GetRestrictedHeaders);

				foreach ( WebHeader header in base.SessionScripting.WebRequests[base.SelectedWebRequestIndex].RequestHttpSettings.AdditionalHeaders )
				{
					_headerList.Add(header.Name);
				}
			}

			WebRequest req = base.SessionScripting.WebRequests[base.SelectedWebRequestIndex];
			LoadHeaderList(_headerList);
			LoadFormValues(req);
			LoadCookieNames(req);
			#endregion
		}

		private void linkLabel1_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			txtTransformDescription.Text = ShowTransformValueDialog(this.cmbTransformValue.SelectedIndex);
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
					RequestTransform transform = (RequestTransform)base.WebTransform;
					
					UpdateTransformAction update = new UpdateTransformAction();					
					update.Name = (string)this.cmbRequestField.SelectedValue;
					update.Value = TransformValue;
					update.Description = txtTransformDescription.Text;
					transform.RequestFieldName = (string)this.cmbRequestField.SelectedValue;
					transform.UpdateTransformAction = update;
				}

				return base.WebTransform;
			}
		}

		#endregion
	}
}
