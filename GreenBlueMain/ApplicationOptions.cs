using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Ecyware.GreenBlue.Controls;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Protocols.Http;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.Scripting;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Summary description for ApplicationOptions.
	/// </summary>
	public class ApplicationOptions : System.Windows.Forms.Form
	{
		private PropertyTable _propertyTable;
		private InspectorConfiguration _appSettings;
		private HttpProperties _clientSettings;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnApply;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown bufferLength;
		private System.Windows.Forms.TextBox txtSQLTest;
		private System.Windows.Forms.TextBox txtXSSTest;
		private System.Windows.Forms.TextBox txtBasicReport;
		private System.Windows.Forms.TextBox txtAdvancedReport;
		private System.Windows.Forms.CheckBox chkEnabledRichTextParsing;
		private System.Windows.Forms.LinkLabel lnkEditSqlSignatures;
		private System.Windows.Forms.LinkLabel lnkEditXssSignatures;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox txtUserAgent;
		private System.Windows.Forms.RadioButton rbSsl;
		private System.Windows.Forms.RadioButton rbTls;
		private System.Windows.Forms.CheckBox chkPipeline;
		private System.Windows.Forms.CheckBox chkAllowAutoRedirects;
		private System.Windows.Forms.TextBox txtContentType;
		private System.Windows.Forms.TextBox txtMediaType;
		private System.Windows.Forms.CheckBox chkKeepAlive;
		private System.Windows.Forms.CheckBox chkAllowWriteStreamBuffering;
		private System.Windows.Forms.NumericUpDown maxAutoRedirects;
		private System.Windows.Forms.ContextMenu mnuAddonHeaders;
		private System.Windows.Forms.GroupBox groupBox6;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox txtHeaderName;
		private System.Windows.Forms.Button btnAddHeader;
		private Ecyware.GreenBlue.Controls.FlatPropertyGrid pgHeaders;
		private System.Windows.Forms.MenuItem mnuRemoveHeader;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new ApplicationOptions.
		/// </summary>
		public ApplicationOptions()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			_propertyTable = new PropertyTable();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ApplicationOptions));
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.chkEnabledRichTextParsing = new System.Windows.Forms.CheckBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.lnkEditXssSignatures = new System.Windows.Forms.LinkLabel();
			this.lnkEditSqlSignatures = new System.Windows.Forms.LinkLabel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtBasicReport = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtAdvancedReport = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtXSSTest = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtSQLTest = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.bufferLength = new System.Windows.Forms.NumericUpDown();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.groupBox6 = new System.Windows.Forms.GroupBox();
			this.label12 = new System.Windows.Forms.Label();
			this.txtHeaderName = new System.Windows.Forms.TextBox();
			this.btnAddHeader = new System.Windows.Forms.Button();
			this.pgHeaders = new Ecyware.GreenBlue.Controls.FlatPropertyGrid();
			this.mnuAddonHeaders = new System.Windows.Forms.ContextMenu();
			this.mnuRemoveHeader = new System.Windows.Forms.MenuItem();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.txtMediaType = new System.Windows.Forms.TextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.txtContentType = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.maxAutoRedirects = new System.Windows.Forms.NumericUpDown();
			this.chkAllowAutoRedirects = new System.Windows.Forms.CheckBox();
			this.chkAllowWriteStreamBuffering = new System.Windows.Forms.CheckBox();
			this.chkKeepAlive = new System.Windows.Forms.CheckBox();
			this.chkPipeline = new System.Windows.Forms.CheckBox();
			this.rbTls = new System.Windows.Forms.RadioButton();
			this.rbSsl = new System.Windows.Forms.RadioButton();
			this.label7 = new System.Windows.Forms.Label();
			this.txtUserAgent = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnApply = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.tabControl.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.bufferLength)).BeginInit();
			this.tabPage2.SuspendLayout();
			this.groupBox6.SuspendLayout();
			this.groupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.maxAutoRedirects)).BeginInit();
			this.SuspendLayout();
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tabPage1);
			this.tabControl.Controls.Add(this.tabPage2);
			this.tabControl.Location = new System.Drawing.Point(6, 6);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(510, 426);
			this.tabControl.TabIndex = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.groupBox4);
			this.tabPage1.Controls.Add(this.groupBox3);
			this.tabPage1.Controls.Add(this.groupBox2);
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(502, 400);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Application";
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.chkEnabledRichTextParsing);
			this.groupBox4.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox4.Location = new System.Drawing.Point(6, 318);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(492, 78);
			this.groupBox4.TabIndex = 3;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "HTTP Browser";
			// 
			// chkEnabledRichTextParsing
			// 
			this.chkEnabledRichTextParsing.Location = new System.Drawing.Point(18, 30);
			this.chkEnabledRichTextParsing.Name = "chkEnabledRichTextParsing";
			this.chkEnabledRichTextParsing.Size = new System.Drawing.Size(192, 24);
			this.chkEnabledRichTextParsing.TabIndex = 0;
			this.chkEnabledRichTextParsing.Text = "Enabled rich text parsing";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.lnkEditXssSignatures);
			this.groupBox3.Controls.Add(this.lnkEditSqlSignatures);
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(6, 216);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(492, 100);
			this.groupBox3.TabIndex = 2;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Signatures Files";
			// 
			// lnkEditXssSignatures
			// 
			this.lnkEditXssSignatures.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lnkEditXssSignatures.Location = new System.Drawing.Point(18, 60);
			this.lnkEditXssSignatures.Name = "lnkEditXssSignatures";
			this.lnkEditXssSignatures.Size = new System.Drawing.Size(198, 18);
			this.lnkEditXssSignatures.TabIndex = 1;
			this.lnkEditXssSignatures.TabStop = true;
			this.lnkEditXssSignatures.Text = "Edit Cross Site-Scripting Signatures...";
			this.lnkEditXssSignatures.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEditXssSignatures_LinkClicked);
			// 
			// lnkEditSqlSignatures
			// 
			this.lnkEditSqlSignatures.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lnkEditSqlSignatures.Location = new System.Drawing.Point(18, 24);
			this.lnkEditSqlSignatures.Name = "lnkEditSqlSignatures";
			this.lnkEditSqlSignatures.Size = new System.Drawing.Size(162, 18);
			this.lnkEditSqlSignatures.TabIndex = 0;
			this.lnkEditSqlSignatures.TabStop = true;
			this.lnkEditSqlSignatures.Text = "Edit SQL Injection Signatures...";
			this.lnkEditSqlSignatures.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEditSqlSignatures_LinkClicked);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.txtBasicReport);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.txtAdvancedReport);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(6, 114);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(492, 96);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Report Templates";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(12, 18);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(468, 23);
			this.label6.TabIndex = 10;
			this.label6.Text = "The report templates are located in the application path. Only file names are val" +
				"id entries.";
			// 
			// txtBasicReport
			// 
			this.txtBasicReport.Location = new System.Drawing.Point(180, 66);
			this.txtBasicReport.Name = "txtBasicReport";
			this.txtBasicReport.Size = new System.Drawing.Size(300, 20);
			this.txtBasicReport.TabIndex = 9;
			this.txtBasicReport.Text = "";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(12, 66);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(138, 18);
			this.label4.TabIndex = 8;
			this.label4.Text = "Basic Report File Name";
			// 
			// txtAdvancedReport
			// 
			this.txtAdvancedReport.Location = new System.Drawing.Point(180, 42);
			this.txtAdvancedReport.Name = "txtAdvancedReport";
			this.txtAdvancedReport.Size = new System.Drawing.Size(300, 20);
			this.txtAdvancedReport.TabIndex = 7;
			this.txtAdvancedReport.Text = "";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(12, 42);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(150, 18);
			this.label5.TabIndex = 6;
			this.label5.Text = "Advanced Report File Name";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.txtXSSTest);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.txtSQLTest);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.bufferLength);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(6, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(492, 100);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Default Quick Unit Tests";
			// 
			// txtXSSTest
			// 
			this.txtXSSTest.Location = new System.Drawing.Point(156, 72);
			this.txtXSSTest.Name = "txtXSSTest";
			this.txtXSSTest.Size = new System.Drawing.Size(324, 20);
			this.txtXSSTest.TabIndex = 5;
			this.txtXSSTest.Text = "";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(12, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(138, 18);
			this.label3.TabIndex = 4;
			this.label3.Text = "Cross Site-Scripting Test";
			// 
			// txtSQLTest
			// 
			this.txtSQLTest.Location = new System.Drawing.Point(156, 48);
			this.txtSQLTest.Name = "txtSQLTest";
			this.txtSQLTest.Size = new System.Drawing.Size(324, 20);
			this.txtSQLTest.TabIndex = 3;
			this.txtSQLTest.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 18);
			this.label2.TabIndex = 2;
			this.label2.Text = "SQL Injection Test";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(120, 18);
			this.label1.TabIndex = 1;
			this.label1.Text = "Buffer Overflow Length";
			// 
			// bufferLength
			// 
			this.bufferLength.Location = new System.Drawing.Point(156, 24);
			this.bufferLength.Name = "bufferLength";
			this.bufferLength.Size = new System.Drawing.Size(84, 20);
			this.bufferLength.TabIndex = 0;
			this.bufferLength.Value = new System.Decimal(new int[] {
																	   1,
																	   0,
																	   0,
																	   0});
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.groupBox6);
			this.tabPage2.Controls.Add(this.groupBox5);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(502, 400);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "HTTP Client";
			// 
			// groupBox6
			// 
			this.groupBox6.Controls.Add(this.label12);
			this.groupBox6.Controls.Add(this.txtHeaderName);
			this.groupBox6.Controls.Add(this.btnAddHeader);
			this.groupBox6.Controls.Add(this.pgHeaders);
			this.groupBox6.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox6.Location = new System.Drawing.Point(6, 222);
			this.groupBox6.Name = "groupBox6";
			this.groupBox6.Size = new System.Drawing.Size(492, 174);
			this.groupBox6.TabIndex = 18;
			this.groupBox6.TabStop = false;
			this.groupBox6.Text = "Additional Headers";
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(12, 24);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(78, 18);
			this.label12.TabIndex = 21;
			this.label12.Text = "Header Name";
			// 
			// txtHeaderName
			// 
			this.txtHeaderName.Location = new System.Drawing.Point(108, 24);
			this.txtHeaderName.Name = "txtHeaderName";
			this.txtHeaderName.Size = new System.Drawing.Size(270, 20);
			this.txtHeaderName.TabIndex = 20;
			this.txtHeaderName.Text = "";
			// 
			// btnAddHeader
			// 
			this.btnAddHeader.Location = new System.Drawing.Point(396, 23);
			this.btnAddHeader.Name = "btnAddHeader";
			this.btnAddHeader.TabIndex = 19;
			this.btnAddHeader.Text = "Add Header";
			this.btnAddHeader.Click += new System.EventHandler(this.btnAddHeader_Click);
			// 
			// pgHeaders
			// 
			this.pgHeaders.CommandsVisibleIfAvailable = true;
			this.pgHeaders.ContextMenu = this.mnuAddonHeaders;
			this.pgHeaders.HelpVisible = false;
			this.pgHeaders.LargeButtons = false;
			this.pgHeaders.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.pgHeaders.Location = new System.Drawing.Point(6, 54);
			this.pgHeaders.Name = "pgHeaders";
			this.pgHeaders.Size = new System.Drawing.Size(468, 114);
			this.pgHeaders.TabIndex = 18;
			this.pgHeaders.Text = "flatPropertyGrid1";
			this.pgHeaders.ToolbarVisible = false;
			this.pgHeaders.ViewBackColor = System.Drawing.SystemColors.Window;
			this.pgHeaders.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// mnuAddonHeaders
			// 
			this.mnuAddonHeaders.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							this.mnuRemoveHeader});
			// 
			// mnuRemoveHeader
			// 
			this.mnuRemoveHeader.Index = 0;
			this.mnuRemoveHeader.Text = "&Remove Header";
			this.mnuRemoveHeader.Click += new System.EventHandler(this.mnuRemoveHeader_Click);
			// 
			// groupBox5
			// 
			this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox5.Controls.Add(this.txtMediaType);
			this.groupBox5.Controls.Add(this.label11);
			this.groupBox5.Controls.Add(this.txtContentType);
			this.groupBox5.Controls.Add(this.label10);
			this.groupBox5.Controls.Add(this.maxAutoRedirects);
			this.groupBox5.Controls.Add(this.chkAllowAutoRedirects);
			this.groupBox5.Controls.Add(this.chkAllowWriteStreamBuffering);
			this.groupBox5.Controls.Add(this.chkKeepAlive);
			this.groupBox5.Controls.Add(this.chkPipeline);
			this.groupBox5.Controls.Add(this.rbTls);
			this.groupBox5.Controls.Add(this.rbSsl);
			this.groupBox5.Controls.Add(this.label7);
			this.groupBox5.Controls.Add(this.txtUserAgent);
			this.groupBox5.Controls.Add(this.label8);
			this.groupBox5.Controls.Add(this.label9);
			this.groupBox5.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox5.Location = new System.Drawing.Point(5, 6);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(492, 210);
			this.groupBox5.TabIndex = 1;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Request Headers";
			// 
			// txtMediaType
			// 
			this.txtMediaType.Location = new System.Drawing.Point(156, 96);
			this.txtMediaType.Name = "txtMediaType";
			this.txtMediaType.Size = new System.Drawing.Size(324, 20);
			this.txtMediaType.TabIndex = 16;
			this.txtMediaType.Text = "";
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(12, 96);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(100, 18);
			this.label11.TabIndex = 15;
			this.label11.Text = "Media Type";
			// 
			// txtContentType
			// 
			this.txtContentType.Location = new System.Drawing.Point(156, 72);
			this.txtContentType.Name = "txtContentType";
			this.txtContentType.Size = new System.Drawing.Size(324, 20);
			this.txtContentType.TabIndex = 14;
			this.txtContentType.Text = "";
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(12, 72);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(100, 18);
			this.label10.TabIndex = 13;
			this.label10.Text = "Content Type";
			// 
			// maxAutoRedirects
			// 
			this.maxAutoRedirects.Enabled = false;
			this.maxAutoRedirects.Location = new System.Drawing.Point(294, 150);
			this.maxAutoRedirects.Name = "maxAutoRedirects";
			this.maxAutoRedirects.TabIndex = 12;
			// 
			// chkAllowAutoRedirects
			// 
			this.chkAllowAutoRedirects.Location = new System.Drawing.Point(12, 150);
			this.chkAllowAutoRedirects.Name = "chkAllowAutoRedirects";
			this.chkAllowAutoRedirects.Size = new System.Drawing.Size(132, 24);
			this.chkAllowAutoRedirects.TabIndex = 11;
			this.chkAllowAutoRedirects.Text = "Allow Auto Redirects";
			this.chkAllowAutoRedirects.CheckedChanged += new System.EventHandler(this.chkAllowAutoRedirects_CheckedChanged);
			// 
			// chkAllowWriteStreamBuffering
			// 
			this.chkAllowWriteStreamBuffering.Location = new System.Drawing.Point(156, 180);
			this.chkAllowWriteStreamBuffering.Name = "chkAllowWriteStreamBuffering";
			this.chkAllowWriteStreamBuffering.Size = new System.Drawing.Size(180, 24);
			this.chkAllowWriteStreamBuffering.TabIndex = 10;
			this.chkAllowWriteStreamBuffering.Text = "Allow Write Stream Buffering";
			// 
			// chkKeepAlive
			// 
			this.chkKeepAlive.Location = new System.Drawing.Point(12, 180);
			this.chkKeepAlive.Name = "chkKeepAlive";
			this.chkKeepAlive.TabIndex = 9;
			this.chkKeepAlive.Text = "Keep Alive";
			// 
			// chkPipeline
			// 
			this.chkPipeline.Location = new System.Drawing.Point(12, 120);
			this.chkPipeline.Name = "chkPipeline";
			this.chkPipeline.TabIndex = 8;
			this.chkPipeline.Text = "Pipeline";
			// 
			// rbTls
			// 
			this.rbTls.Location = new System.Drawing.Point(276, 24);
			this.rbTls.Name = "rbTls";
			this.rbTls.TabIndex = 7;
			this.rbTls.Text = "TLS";
			// 
			// rbSsl
			// 
			this.rbSsl.Location = new System.Drawing.Point(156, 24);
			this.rbSsl.Name = "rbSsl";
			this.rbSsl.TabIndex = 6;
			this.rbSsl.Text = "SSL3";
			// 
			// label7
			// 
			this.label7.Enabled = false;
			this.label7.Location = new System.Drawing.Point(156, 150);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(138, 24);
			this.label7.TabIndex = 4;
			this.label7.Text = "Maximum Auto Redirects";
			// 
			// txtUserAgent
			// 
			this.txtUserAgent.Location = new System.Drawing.Point(156, 48);
			this.txtUserAgent.Name = "txtUserAgent";
			this.txtUserAgent.Size = new System.Drawing.Size(324, 20);
			this.txtUserAgent.TabIndex = 3;
			this.txtUserAgent.Text = "";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(12, 49);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100, 18);
			this.label8.TabIndex = 2;
			this.label8.Text = "User Agent";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(12, 26);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(120, 18);
			this.label9.TabIndex = 1;
			this.label9.Text = "Security Protocol";
			// 
			// btnCancel
			// 
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(354, 438);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnApply
			// 
			this.btnApply.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnApply.Location = new System.Drawing.Point(438, 438);
			this.btnApply.Name = "btnApply";
			this.btnApply.TabIndex = 2;
			this.btnApply.Text = "Apply";
			this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
			// 
			// btnOK
			// 
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnOK.Location = new System.Drawing.Point(270, 438);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "OK";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// ApplicationOptions
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(520, 470);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnApply);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.tabControl);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ApplicationOptions";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Options";
			this.Load += new System.EventHandler(this.ApplicationOptions_Load);
			this.tabControl.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox4.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.bufferLength)).EndInit();
			this.tabPage2.ResumeLayout(false);
			this.groupBox6.ResumeLayout(false);
			this.groupBox5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.maxAutoRedirects)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void btnApply_Click(object sender, System.EventArgs e)
		{
			ApplySettings();
		}

		/// <summary>
		/// Applies the settings
		/// </summary>
		public void ApplySettings()
		{
			// Set application settings
			_appSettings.AdvancedReportTemplate = this.txtAdvancedReport.Text;
			_appSettings.BasicReportTemplate = this.txtBasicReport.Text;
			_appSettings.DefaultBufferOverflowLength = Convert.ToInt32(this.bufferLength.Value);
			_appSettings.DefaultSqlTest = this.txtSQLTest.Text;
			_appSettings.DefaultXssTest = this.txtXSSTest.Text;
			_appSettings.EnabledRichTextParsing = this.chkEnabledRichTextParsing.Checked;

			// Set client settings
			if ( rbSsl.Checked )
			{
				_clientSettings.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3;
			}

			if ( rbTls.Checked )
			{
				_clientSettings.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
			}

			_clientSettings.UserAgent = txtUserAgent.Text;
			_clientSettings.ContentType = txtContentType.Text;
			_clientSettings.MediaType = txtMediaType.Text;
			_clientSettings.Pipeline = chkPipeline.Checked;
			_clientSettings.AllowAutoRedirects =  chkAllowAutoRedirects.Checked;
			_clientSettings.MaximumAutoRedirects = Convert.ToInt32(maxAutoRedirects.Value);
			_clientSettings.KeepAlive = chkKeepAlive.Checked;
			_clientSettings.AllowWriteStreamBuffering = chkAllowWriteStreamBuffering.Checked;
								
			// Additional Headers
			foreach ( PropertySpec p in _propertyTable.Properties )
			{					
				// Update additional headers values.
				_clientSettings.SetWebHeader(p.Name,(string)_propertyTable[p.Name]);
			}

			ConfigManager.Write("greenBlue/httpClient",_clientSettings);
			ConfigManager.Write("greenBlue/inspector",_appSettings);
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			ApplySettings();
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void ApplicationOptions_Load(object sender, System.EventArgs e)
		{
			// Load options
			this.txtAdvancedReport.Text = ApplicationSettings.AdvancedReportTemplate;
			this.txtBasicReport.Text = ApplicationSettings.BasicReportTemplate;
			this.txtSQLTest.Text = ApplicationSettings.DefaultSqlTest;
			this.txtXSSTest.Text = ApplicationSettings.DefaultXssTest;
			this.chkEnabledRichTextParsing.Checked = ApplicationSettings.EnabledRichTextParsing;			
			this.bufferLength.Value = ApplicationSettings.DefaultBufferOverflowLength;

			txtUserAgent.Text = ClientSettings.UserAgent;
			txtContentType.Text = ClientSettings.ContentType;
			txtMediaType.Text = ClientSettings.MediaType;
			chkPipeline.Checked = ClientSettings.Pipeline;
			chkAllowAutoRedirects.Checked = ClientSettings.AllowAutoRedirects;
			maxAutoRedirects.Value = ClientSettings.MaximumAutoRedirects;
			chkKeepAlive.Checked = ClientSettings.KeepAlive;
			chkAllowWriteStreamBuffering.Checked = ClientSettings.AllowWriteStreamBuffering;

			if ( ClientSettings.SecurityProtocol == System.Net.SecurityProtocolType.Ssl3 )
			{
				rbSsl.Checked = true;
				rbTls.Checked = false;
			} 
			else 
			{
				rbTls.Checked = true;
				rbSsl.Checked = false;
			}
			// Additional Headers
			for (int i=0;i<ClientSettings.AdditionalHeaders.Length;i++)
			{				
				PropertySpec property = new PropertySpec(ClientSettings.AdditionalHeaders[i].Name,typeof(string),"Additional Headers");
				_propertyTable.Properties.Add(property);
				_propertyTable[ClientSettings.AdditionalHeaders[i].Name] = ClientSettings.AdditionalHeaders[i].Value;
			}

			this.pgHeaders.SelectedObject = _propertyTable;
		}

		private void chkAllowAutoRedirects_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( chkAllowAutoRedirects.Checked )
			{
				this.label7.Enabled = true;
				this.maxAutoRedirects.Enabled = true;
			} 
			else 
			{
				this.label7.Enabled = false;
				this.maxAutoRedirects.Enabled = false;
			}
		}

		private void groupBox6_Enter(object sender, System.EventArgs e)
		{
		
		}

		private void btnAddHeader_Click(object sender, System.EventArgs e)
		{
			AddNewHeader();
		}

		/// <summary>
		/// Adds a new header.
		/// </summary>
		private void AddNewHeader()
		{
			string name = txtHeaderName.Text;

			if ( _propertyTable[name] != null )
			{
				MessageBox.Show("There is an existing header, please insert a new header name.", AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			} 
			else 
			{
				PropertySpec property = new PropertySpec(name,typeof(string),"Additional Headers");
				_propertyTable.Properties.Add(property);
				_propertyTable[name] = string.Empty;

				this.pgHeaders.SelectedObject = _propertyTable;
			}
		}

		private void mnuRemoveHeader_Click(object sender, System.EventArgs e)
		{
			_propertyTable.Properties.Remove(this.pgHeaders.SelectedGridItem.Label);
			this.pgHeaders.SelectedObject = _propertyTable;
		}

		private void lnkEditSqlSignatures_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			LoadSignatureEditor(_appSettings.SqlSignatures);
		}

		private void lnkEditXssSignatures_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			LoadSignatureEditor(_appSettings.XssSignatures);
		}

		private void LoadSignatureEditor(string filePath)
		{
			filePath = AppLocation.CommonFolder + System.IO.Path.DirectorySeparatorChar + filePath;

			XmlSignaturesEditor editor = new XmlSignaturesEditor(filePath);
			editor.ShowDialog();
		}

		/// <summary>
		/// Gets or sets the client settings.
		/// </summary>
		public HttpProperties ClientSettings
		{
			get
			{
				return _clientSettings;
			}
			set
			{
				_clientSettings = value;
			}
		}

		/// <summary>
		/// Gets or sets the application settings.
		/// </summary>
		public InspectorConfiguration ApplicationSettings
		{
			get
			{
				return _appSettings;
			}
			set
			{
				_appSettings = value;
			}
		}
	}
}
