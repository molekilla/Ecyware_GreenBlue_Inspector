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
	/// Summary description for RegExQueryCommandDialog.
	/// </summary>
	public class RegExQueryCommandDialog : System.Windows.Forms.Form
	{
		private TransformValue _tvalue;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.TextBox txtRegEx;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtPostfix;
		private System.Windows.Forms.TextBox txtPrefix;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox chkApplyGroup;
		private System.Windows.Forms.NumericUpDown numGroupMatchTo;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown numGroupMatchFrom;
		private System.Windows.Forms.NumericUpDown numMatchTo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numMatchFrom;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox txtGroup;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new RegExQueryCommandDialog.
		/// </summary>
		public RegExQueryCommandDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}


		/// <summary>
		/// Gets a description.
		/// </summary>
		public string Description
		{
			get
			{
				return "Uses a Regular Expression: " + this.txtRegEx.Text;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(RegExQueryCommandDialog));
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtRegEx = new System.Windows.Forms.TextBox();
			this.txtPostfix = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtPrefix = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.chkApplyGroup = new System.Windows.Forms.CheckBox();
			this.numGroupMatchTo = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.numGroupMatchFrom = new System.Windows.Forms.NumericUpDown();
			this.numMatchTo = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.numMatchFrom = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.txtGroup = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.numGroupMatchTo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numGroupMatchFrom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numMatchTo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numMatchFrom)).BeginInit();
			this.SuspendLayout();
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 42);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(114, 18);
			this.label2.TabIndex = 1;
			this.label2.Text = "Regular Expression";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 12);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(372, 23);
			this.label4.TabIndex = 0;
			this.label4.Text = "Sets a regular expression";
			// 
			// btnOK
			// 
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(252, 312);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(336, 312);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// txtRegEx
			// 
			this.txtRegEx.Location = new System.Drawing.Point(126, 42);
			this.txtRegEx.Multiline = true;
			this.txtRegEx.Name = "txtRegEx";
			this.txtRegEx.Size = new System.Drawing.Size(264, 54);
			this.txtRegEx.TabIndex = 2;
			this.txtRegEx.Text = "";
			// 
			// txtPostfix
			// 
			this.txtPostfix.Location = new System.Drawing.Point(126, 124);
			this.txtPostfix.Name = "txtPostfix";
			this.txtPostfix.Size = new System.Drawing.Size(264, 20);
			this.txtPostfix.TabIndex = 14;
			this.txtPostfix.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(12, 126);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 18);
			this.label5.TabIndex = 13;
			this.label5.Text = "Postfix to add";
			// 
			// txtPrefix
			// 
			this.txtPrefix.Location = new System.Drawing.Point(126, 100);
			this.txtPrefix.Name = "txtPrefix";
			this.txtPrefix.Size = new System.Drawing.Size(264, 20);
			this.txtPrefix.TabIndex = 12;
			this.txtPrefix.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 102);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 18);
			this.label3.TabIndex = 11;
			this.label3.Text = "Prefix to add";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(24, 186);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(100, 12);
			this.label9.TabIndex = 46;
			this.label9.Text = "From Match Index";
			// 
			// chkApplyGroup
			// 
			this.chkApplyGroup.Location = new System.Drawing.Point(18, 270);
			this.chkApplyGroup.Name = "chkApplyGroup";
			this.chkApplyGroup.Size = new System.Drawing.Size(114, 24);
			this.chkApplyGroup.TabIndex = 45;
			this.chkApplyGroup.Text = "Apply Group from";
			// 
			// numGroupMatchTo
			// 
			this.numGroupMatchTo.Location = new System.Drawing.Point(204, 272);
			this.numGroupMatchTo.Maximum = new System.Decimal(new int[] {
																			1000,
																			0,
																			0,
																			0});
			this.numGroupMatchTo.Minimum = new System.Decimal(new int[] {
																			1,
																			0,
																			0,
																			-2147483648});
			this.numGroupMatchTo.Name = "numGroupMatchTo";
			this.numGroupMatchTo.Size = new System.Drawing.Size(42, 20);
			this.numGroupMatchTo.TabIndex = 44;
			this.numGroupMatchTo.Value = new System.Decimal(new int[] {
																		  1,
																		  0,
																		  0,
																		  -2147483648});
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(180, 276);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(18, 12);
			this.label8.TabIndex = 43;
			this.label8.Text = "to";
			// 
			// numGroupMatchFrom
			// 
			this.numGroupMatchFrom.Location = new System.Drawing.Point(132, 272);
			this.numGroupMatchFrom.Maximum = new System.Decimal(new int[] {
																			  1000,
																			  0,
																			  0,
																			  0});
			this.numGroupMatchFrom.Minimum = new System.Decimal(new int[] {
																			  1,
																			  0,
																			  0,
																			  -2147483648});
			this.numGroupMatchFrom.Name = "numGroupMatchFrom";
			this.numGroupMatchFrom.Size = new System.Drawing.Size(42, 20);
			this.numGroupMatchFrom.TabIndex = 42;
			this.numGroupMatchFrom.Value = new System.Decimal(new int[] {
																			1,
																			0,
																			0,
																			-2147483648});
			// 
			// numMatchTo
			// 
			this.numMatchTo.Location = new System.Drawing.Point(204, 182);
			this.numMatchTo.Maximum = new System.Decimal(new int[] {
																	   1000,
																	   0,
																	   0,
																	   0});
			this.numMatchTo.Minimum = new System.Decimal(new int[] {
																	   1,
																	   0,
																	   0,
																	   -2147483648});
			this.numMatchTo.Name = "numMatchTo";
			this.numMatchTo.Size = new System.Drawing.Size(42, 20);
			this.numMatchTo.TabIndex = 41;
			this.numMatchTo.Value = new System.Decimal(new int[] {
																	 1,
																	 0,
																	 0,
																	 -2147483648});
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(180, 186);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(18, 12);
			this.label1.TabIndex = 40;
			this.label1.Text = "to";
			// 
			// numMatchFrom
			// 
			this.numMatchFrom.Location = new System.Drawing.Point(132, 182);
			this.numMatchFrom.Maximum = new System.Decimal(new int[] {
																		 1000,
																		 0,
																		 0,
																		 0});
			this.numMatchFrom.Minimum = new System.Decimal(new int[] {
																		 1,
																		 0,
																		 0,
																		 -2147483648});
			this.numMatchFrom.Name = "numMatchFrom";
			this.numMatchFrom.Size = new System.Drawing.Size(42, 20);
			this.numMatchFrom.TabIndex = 39;
			this.numMatchFrom.Value = new System.Decimal(new int[] {
																	   1,
																	   0,
																	   0,
																	   -2147483648});
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(12, 216);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 18);
			this.label7.TabIndex = 38;
			this.label7.Text = "Group Options";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(12, 156);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 18);
			this.label6.TabIndex = 37;
			this.label6.Text = "Match Options";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(24, 241);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(72, 18);
			this.label10.TabIndex = 36;
			this.label10.Text = "Use Group";
			// 
			// txtGroup
			// 
			this.txtGroup.Location = new System.Drawing.Point(96, 240);
			this.txtGroup.Name = "txtGroup";
			this.txtGroup.Size = new System.Drawing.Size(150, 20);
			this.txtGroup.TabIndex = 35;
			this.txtGroup.Text = "";
			// 
			// RegExQueryCommandDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(414, 358);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.chkApplyGroup);
			this.Controls.Add(this.numGroupMatchTo);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.numGroupMatchFrom);
			this.Controls.Add(this.numMatchTo);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.numMatchFrom);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.txtGroup);
			this.Controls.Add(this.txtPostfix);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.txtPrefix);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.txtRegEx);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RegExQueryCommandDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Regular Expression Command Value Dialog";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.numGroupMatchTo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numGroupMatchFrom)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numMatchTo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numMatchFrom)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			RegExQueryCommand tvalue = new RegExQueryCommand();
			tvalue.Expression = this.txtRegEx.Text;			
			tvalue.Postfix = this.txtPostfix.Text;
			tvalue.Prefix = this.txtPrefix.Text;
			tvalue.ApplyGroupCapture = this.chkApplyGroup.Checked;
			tvalue.GroupCaptureFromIndex = (int)this.numGroupMatchFrom.Value;
			tvalue.GroupCaptureToIndex = (int)this.numGroupMatchTo.Value;
			tvalue.GroupName = this.txtGroup.Text;
			tvalue.MatchFromIndex = (int)this.numMatchFrom.Value;
			tvalue.MatchToIndex = (int)this.numMatchTo.Value;

			_tvalue = tvalue;
			DialogResult = DialogResult.OK;
		}

		/// <summary>
		/// Loads the transform values.
		/// </summary>
		public void LoadTransformValue()
		{
			if ( this.TransformValue != null )
			{
				if ( this.TransformValue is RegExQueryCommand )
				{
					RegExQueryCommand cmd = ((RegExQueryCommand)_tvalue);
					txtRegEx.Text = cmd.Expression;
					txtPrefix.Text = cmd.Prefix;
					txtPostfix.Text = cmd.Postfix;

					chkApplyGroup.Checked = cmd.ApplyGroupCapture;
					numGroupMatchFrom.Value = cmd.GroupCaptureFromIndex;
					numGroupMatchTo.Value = cmd.GroupCaptureToIndex;
					txtGroup.Text = cmd.GroupName;
					numMatchFrom.Value = cmd.MatchFromIndex;
					numMatchTo.Value = cmd.MatchToIndex;
				}
			}
		}

		/// <summary>
		/// Gets the transform value.
		/// </summary>
		public TransformValue TransformValue
		{
			get
			{
				return _tvalue;
			}

			set
			{
				_tvalue = value;
			}
		}
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}


	}
}
