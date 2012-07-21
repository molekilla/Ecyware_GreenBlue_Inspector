// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: June 2004 - July 2004
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;
using System.Xml;
using Ecyware.GreenBlue.Controls;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Engine.HtmlCommand;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms;
using Ecyware.GreenBlue.Utils;
using Compona;
using Compona.SourceCode;
using mshtml;

namespace Ecyware.GreenBlue.Controls.Scripting
{
	/// <summary>
	/// Summary description for TestRequestDialog.
	/// </summary>
	public class TestRequestDialog : System.Windows.Forms.Form
	{
		string _translatedDomXpath;
		ScriptingApplication _appClone;
		int _startingIndex;
		WebResponse currentWebResponse;
		HtmlQueryUtil queryUtil = new HtmlQueryUtil(false);
		HtmlParser parser = new HtmlParser();
		private delegate void SendElement(mshtml.IHTMLElement element);
		mshtml.IHTMLDocument2 htmlDocument = null;
		ScriptingCommand scriptingCommand = new ScriptingCommand();
		private delegate void SetEditorDocumentEventHandler(SyntaxDocument document);
		private string textValue = string.Empty;		
		string htmlSyntaxFile = string.Empty;
		Compona.SourceCode.Language language;

		private System.Windows.Forms.ImageList imgIcons;
		private System.Windows.Forms.GroupBox groupBox2;
		private Compona.SourceCode.SyntaxDocument syntaxDocument2;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.ProgressBar progress;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListView lvRequests;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TabControl tabDocuments;
		private System.Windows.Forms.TabPage tabPage5;
		private AxSHDocVw.AxWebBrowser webBrowser;
		private System.Windows.Forms.TabPage tabPage4;
		internal Compona.Windows.Forms.SyntaxBoxControl txtEditor;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.TreeView tvResponses;
		private System.Windows.Forms.TabControl tabPages;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ComboBox cmbRegEx;
		private System.Windows.Forms.ContextMenu mnuSourceHelpers;
		private System.Windows.Forms.MenuItem mnuXPathQueryDialog;
		private System.Windows.Forms.MenuItem mnuRegExQueryDialog;
		private System.Windows.Forms.MenuItem mnuXSLTDialog;
		private System.ComponentModel.IContainer components;
		

		/// <summary>
		/// Creates a new TestRequestDialog.
		/// </summary>
		public TestRequestDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			htmlSyntaxFile = Application.StartupPath + @"\XML.syn";
			language = Compona.SourceCode.Language.FromSyntaxFile(htmlSyntaxFile);
			syntaxDocument2.Parser.Init(language);

			ConnectEventHandlers();

			Navigate("about:blank");

			while (this.webBrowser.ReadyState != SHDocVw.tagREADYSTATE.READYSTATE_COMPLETE )
			{
				Application.DoEvents();
			}

			cmbRegEx.Items.Add(@"(?<header><(?i:input)[^>]*?)(/>|>(?<source>[\w|\t|\r|\W]*?)</(?i:input)>)");
			cmbRegEx.Items.Add(@"(?<header><(?i:script)[^>]*?)(/>|>(?<source>[\w|\t|\r|\W]*?)</(?i:script)>)");
			cmbRegEx.Items.Add(@"(?<header><(?i:td)[^>]*?)(/>|>(?<source>[\w|\t|\r|\W]*?)</(?i:td)>)");
			cmbRegEx.Items.Add(@"(?<header><(?i:span)[^>]*?)(/>|>(?<source>[\w|\t|\r|\W]*?)</(?i:span)>)");
			cmbRegEx.Items.Add(@"(?<name>(\w+))=(""|')(?<value>.*?)(""|')");
		}

		/// <summary>
		/// Web Browser GET Request.
		/// </summary>
		/// <param name="url"> URL to query.</param>
		public void Navigate(string url)
		{
			object flags = new Object();
			object targetFrame = new Object();
			object postData = new Object();
			object headers = new Object();
			
			this.webBrowser.Navigate(url,ref flags,ref targetFrame, ref postData, ref headers);
		}

		/// <summary>
		/// Connect the events to the scripting command.
		/// </summary>
		public void ConnectEventHandlers()
		{
			// Connect to client delegate.
			scriptingCommand.SessionAbortedEvent +=  new SessionAbortEventHandler(OnSessionAborted);
			scriptingCommand.OnRequestEnd += new OnRequestEndEventHandler(scriptingCommand_OnRequestEnd);
			scriptingCommand.OnRequestStart += new OnRequestStartEventHandler(scriptingCommand_OnRequestStart);
		}

		/// <summary>
		/// Initiate the test.
		/// </summary>
		/// <param name="application"> The scripting application.</param>
		/// <param name="index"> The index.</param>
		public void TestRequestUntilIndex(ScriptingApplication application, int index)
		{			
			_appClone = application.Clone();
			_startingIndex = index;
			scriptingCommand.ExecuteSessionUntilEnd(application.Clone(), index);
		}


		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if ( syntaxDocument2 != null )
				{
					syntaxDocument2.Dispose();
				}
				if ( scriptingCommand != null )
				{
					scriptingCommand.Close();
				}
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		#region Editor Text Properties and Methods

		/// <summary>
		/// Gets or sets the Text Editor Text.
		/// </summary>
		internal string EditorText
		{
			get
			{
				return this.txtEditor.Document.Text;
			}
			set
			{
				syntaxDocument2.clear();

				// save value in temp
				textValue = value;

				// set wait message.				
				this.txtEditor.Document.Text = "Wait while document is being parsed...";
				//Application.DoEvents();		
				syntaxDocument2.Text = textValue;			

				// be sure to set parse all, this allows the parsing to be done in this method.
				//syntaxDocument2.ParseAll();
				//syntaxDocument2.UnFoldAll();
				txtEditor.Document = syntaxDocument2;
			}
		}


//		/// <summary>
//		/// Sets the text async.
//		/// </summary>
//		private void SetAsyncEditorText()
//		{
//			Compona.SourceCode.SyntaxDocument doc = new SyntaxDocument();
//			doc.Parser.Init(language);
//			doc.Text = textValue;			
//
//			// be sure to set parse all, this allows the parsing to be done in this method.
//			doc.ParseAll();
//			doc.UnFoldAll();
//
//			this.BeginInvoke(new SetEditorDocumentEventHandler(SetEditorDocument), new object[] {doc});
//		}
//
//		/// <summary>
//		/// Sets the editor document.
//		/// </summary>
//		/// <param name="document"> The SyntaxDocument to use.</param>
//		private void SetEditorDocument(SyntaxDocument document)
//		{
//			txtEditor.Document = document;
//		}

		#endregion
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(TestRequestDialog));
			this.imgIcons = new System.Windows.Forms.ImageList(this.components);
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.progress = new System.Windows.Forms.ProgressBar();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.syntaxDocument2 = new Compona.SourceCode.SyntaxDocument(this.components);
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lvRequests = new System.Windows.Forms.ListView();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.tabDocuments = new System.Windows.Forms.TabControl();
			this.tabPage5 = new System.Windows.Forms.TabPage();
			this.webBrowser = new AxSHDocVw.AxWebBrowser();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.txtEditor = new Compona.Windows.Forms.SyntaxBoxControl();
			this.mnuSourceHelpers = new System.Windows.Forms.ContextMenu();
			this.mnuXPathQueryDialog = new System.Windows.Forms.MenuItem();
			this.mnuRegExQueryDialog = new System.Windows.Forms.MenuItem();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.tvResponses = new System.Windows.Forms.TreeView();
			this.tabPages = new System.Windows.Forms.TabControl();
			this.button1 = new System.Windows.Forms.Button();
			this.cmbRegEx = new System.Windows.Forms.ComboBox();
			this.mnuXSLTDialog = new System.Windows.Forms.MenuItem();
			this.groupBox2.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.tabDocuments.SuspendLayout();
			this.tabPage5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.webBrowser)).BeginInit();
			this.tabPage4.SuspendLayout();
			this.tabPages.SuspendLayout();
			this.SuspendLayout();
			// 
			// imgIcons
			// 
			this.imgIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imgIcons.ImageSize = new System.Drawing.Size(16, 16);
			this.imgIcons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgIcons.ImageStream")));
			this.imgIcons.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.progress);
			this.groupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox2.Location = new System.Drawing.Point(6, 368);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(622, 42);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Progress";
			// 
			// progress
			// 
			this.progress.Dock = System.Windows.Forms.DockStyle.Fill;
			this.progress.Location = new System.Drawing.Point(3, 16);
			this.progress.Name = "progress";
			this.progress.Size = new System.Drawing.Size(616, 23);
			this.progress.TabIndex = 2;
			// 
			// btnStop
			// 
			this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStop.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnStop.Location = new System.Drawing.Point(464, 416);
			this.btnStop.Name = "btnStop";
			this.btnStop.TabIndex = 2;
			this.btnStop.Text = "&Stop";
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.Location = new System.Drawing.Point(550, 416);
			this.btnClose.Name = "btnClose";
			this.btnClose.TabIndex = 3;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// syntaxDocument2
			// 
			this.syntaxDocument2.Lines = new string[] {
														  ""};
			this.syntaxDocument2.MaxUndoBufferSize = 1000;
			this.syntaxDocument2.Modified = false;
			this.syntaxDocument2.UndoStep = 0;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(622, 330);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Request Progress";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lvRequests);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(622, 330);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Request Progress";
			// 
			// lvRequests
			// 
			this.lvRequests.AllowColumnReorder = true;
			this.lvRequests.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						 this.columnHeader4,
																						 this.columnHeader1,
																						 this.columnHeader3});
			this.lvRequests.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvRequests.FullRowSelect = true;
			this.lvRequests.LabelEdit = true;
			this.lvRequests.Location = new System.Drawing.Point(3, 16);
			this.lvRequests.MultiSelect = false;
			this.lvRequests.Name = "lvRequests";
			this.lvRequests.Size = new System.Drawing.Size(616, 311);
			this.lvRequests.SmallImageList = this.imgIcons;
			this.lvRequests.TabIndex = 4;
			this.lvRequests.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "ID";
			this.columnHeader4.Width = 100;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Method";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Description";
			this.columnHeader3.Width = 450;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.groupBox3);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(622, 330);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Responses";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.tabDocuments);
			this.groupBox3.Controls.Add(this.splitter1);
			this.groupBox3.Controls.Add(this.tvResponses);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox3.Location = new System.Drawing.Point(0, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(622, 330);
			this.groupBox3.TabIndex = 0;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Responses";
			// 
			// tabDocuments
			// 
			this.tabDocuments.Controls.Add(this.tabPage5);
			this.tabDocuments.Controls.Add(this.tabPage4);
			this.tabDocuments.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabDocuments.ItemSize = new System.Drawing.Size(42, 18);
			this.tabDocuments.Location = new System.Drawing.Point(201, 16);
			this.tabDocuments.Name = "tabDocuments";
			this.tabDocuments.SelectedIndex = 0;
			this.tabDocuments.Size = new System.Drawing.Size(418, 311);
			this.tabDocuments.TabIndex = 2;
			// 
			// tabPage5
			// 
			this.tabPage5.Controls.Add(this.webBrowser);
			this.tabPage5.Location = new System.Drawing.Point(4, 22);
			this.tabPage5.Name = "tabPage5";
			this.tabPage5.Size = new System.Drawing.Size(410, 285);
			this.tabPage5.TabIndex = 1;
			this.tabPage5.Text = "HTML";
			// 
			// webBrowser
			// 
			this.webBrowser.ContainingControl = this;
			this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.webBrowser.Enabled = true;
			this.webBrowser.Location = new System.Drawing.Point(0, 0);
			this.webBrowser.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("webBrowser.OcxState")));
			this.webBrowser.Size = new System.Drawing.Size(410, 285);
			this.webBrowser.TabIndex = 0;
			// 
			// tabPage4
			// 
			this.tabPage4.Controls.Add(this.txtEditor);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(410, 285);
			this.tabPage4.TabIndex = 0;
			this.tabPage4.Text = "Source";
			// 
			// txtEditor
			// 
			this.txtEditor.ActiveView = Compona.Windows.Forms.SyntaxBox.ActiveView.BottomRight;
			this.txtEditor.AutoListPosition = null;
			this.txtEditor.AutoListSelectedText = "a123";
			this.txtEditor.AutoListVisible = false;
			this.txtEditor.BackColor = System.Drawing.Color.White;
			this.txtEditor.ContextMenu = this.mnuSourceHelpers;
			this.txtEditor.CopyAsRTF = false;
			this.txtEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtEditor.FontName = "Courier new";
			this.txtEditor.Indent = Compona.Windows.Forms.SyntaxBox.IndentStyle.Smart;
			this.txtEditor.InfoTipCount = 1;
			this.txtEditor.InfoTipPosition = null;
			this.txtEditor.InfoTipSelectedIndex = 1;
			this.txtEditor.InfoTipVisible = false;
			this.txtEditor.Location = new System.Drawing.Point(0, 0);
			this.txtEditor.LockCursorUpdate = false;
			this.txtEditor.Name = "txtEditor";
			this.txtEditor.ShowTabGuides = true;
			this.txtEditor.Size = new System.Drawing.Size(410, 285);
			this.txtEditor.SmoothScroll = false;
			this.txtEditor.SplitviewH = -4;
			this.txtEditor.SplitviewV = -4;
			this.txtEditor.TabGuideColor = System.Drawing.Color.FromArgb(((System.Byte)(244)), ((System.Byte)(243)), ((System.Byte)(234)));
			this.txtEditor.TabIndex = 11;
			this.txtEditor.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
			// 
			// mnuSourceHelpers
			// 
			this.mnuSourceHelpers.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																							 this.mnuXPathQueryDialog,
																							 this.mnuRegExQueryDialog,
																							 this.mnuXSLTDialog});
			// 
			// mnuXPathQueryDialog
			// 
			this.mnuXPathQueryDialog.Index = 0;
			this.mnuXPathQueryDialog.Text = "&XPath Query Dialog";
			this.mnuXPathQueryDialog.Click += new System.EventHandler(this.mnuXPathQueryDialog_Click);
			// 
			// mnuRegExQueryDialog
			// 
			this.mnuRegExQueryDialog.Index = 1;
			this.mnuRegExQueryDialog.Text = "&RegEx Query Dialog";
			this.mnuRegExQueryDialog.Click += new System.EventHandler(this.mnuRegExQueryDialog_Click);
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(198, 16);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 311);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			// 
			// tvResponses
			// 
			this.tvResponses.Dock = System.Windows.Forms.DockStyle.Left;
			this.tvResponses.HideSelection = false;
			this.tvResponses.ImageIndex = 7;
			this.tvResponses.ImageList = this.imgIcons;
			this.tvResponses.Location = new System.Drawing.Point(3, 16);
			this.tvResponses.Name = "tvResponses";
			this.tvResponses.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
																					new System.Windows.Forms.TreeNode("Responses")});
			this.tvResponses.SelectedImageIndex = 7;
			this.tvResponses.Size = new System.Drawing.Size(195, 311);
			this.tvResponses.TabIndex = 0;
			this.tvResponses.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvResponses_AfterSelect);
			// 
			// tabPages
			// 
			this.tabPages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.tabPages.Controls.Add(this.tabPage1);
			this.tabPages.Controls.Add(this.tabPage2);
			this.tabPages.ItemSize = new System.Drawing.Size(96, 18);
			this.tabPages.Location = new System.Drawing.Point(0, 0);
			this.tabPages.Name = "tabPages";
			this.tabPages.SelectedIndex = 0;
			this.tabPages.Size = new System.Drawing.Size(630, 356);
			this.tabPages.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Location = new System.Drawing.Point(378, 416);
			this.button1.Name = "button1";
			this.button1.TabIndex = 4;
			this.button1.Text = "&Retry";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// cmbRegEx
			// 
			this.cmbRegEx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cmbRegEx.DropDownWidth = 318;
			this.cmbRegEx.ItemHeight = 13;
			this.cmbRegEx.Location = new System.Drawing.Point(150, 414);
			this.cmbRegEx.MaxDropDownItems = 10;
			this.cmbRegEx.Name = "cmbRegEx";
			this.cmbRegEx.Size = new System.Drawing.Size(102, 21);
			this.cmbRegEx.TabIndex = 14;
			this.cmbRegEx.Visible = false;
			// 
			// mnuXSLTDialog
			// 
			this.mnuXSLTDialog.Index = 2;
			this.mnuXSLTDialog.Text = "XSL&T Dialog";
			this.mnuXSLTDialog.Click += new System.EventHandler(this.mnuXSLTDialog_Click);
			// 
			// TestRequestDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(634, 448);
			this.Controls.Add(this.cmbRegEx);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.tabPages);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "TestRequestDialog";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Test Requests";
			this.groupBox2.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.tabDocuments.ResumeLayout(false);
			this.tabPage5.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.webBrowser)).EndInit();
			this.tabPage4.ResumeLayout(false);
			this.tabPages.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void OnSessionAborted(object sender, SessionAbortEventArgs e)
		{
			string message = e.ErrorMessage;

			ListViewItem item = new ListViewItem();			
			item.ImageIndex = 6;
			item.Text = "Session aborted";
			item.SubItems.Add("Error: "+ message);
			item.ForeColor = Color.Red;				
			lvRequests.Items.Add(item);

			// stop progress bar	
			progress.Value = 0;
		}


		private void StartProgressBar(int count, int index)
		{
			progress.Maximum = count * 100;
			progress.Value = index * 100;
		}

		private void MoveProgressBar(int index)
		{
			progress.Value = (index + 1) * 100;

			if ( progress.Value == progress.Maximum )
			{
				MessageBox.Show("Testing completed",AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void ViewHTML(string text)
		{
			try
			{
				mshtml.IHTMLDocument2 document = (mshtml.IHTMLDocument2)webBrowser.Document;
				//mshtml.IHTMLDocument2 document = (mshtml.IHTMLDocument2)htmlEditor.Document;
				//htmlEditor.LoadDocument(text);				
				document.write("");
				document.close();
				document.write(new object[] {text});			
//
//				mshtml.IHTMLElementCollection coll = (IHTMLElementCollection)document.all.tags("table");
//				foreach ( IHTMLElement el in coll )
//				{
//					if ( el is mshtml.IHTMLTable )
//					{
//						((mshtml.IHTMLTable)el).border = 1;
//					}
//				}
				if ( document != null )
				{					
					htmlDocument = document;
					mshtml.HTMLDocumentEvents2_Event iEvent 
						= (mshtml.HTMLDocumentEvents2_Event)htmlDocument;
					iEvent.onclick += new mshtml.HTMLDocumentEvents2_onclickEventHandler(iEvent_onclick);

					// On Error Event
					mshtml.HTMLWindowEvents2_Event windowEvents 
						= (mshtml.HTMLWindowEvents2_Event)htmlDocument.parentWindow;
					windowEvents.onerror += new HTMLWindowEvents2_onerrorEventHandler(windowEvents_onerror);
				}
			}
			catch 
			{
				// Ignore
			}
		}
		/// <summary>
		/// Add request progress.
		/// </summary>
		/// <param name="request"> The web request.</param>
		private void AddRequestProgress(WebRequest request)
		{
			ListViewItem item = new ListViewItem();			
			
			item.UseItemStyleForSubItems = true;

			// Main Request
			item.ImageIndex = 9;
			item.Text = request.ID;			
			item.SubItems.Add(request.RequestType.ToString());
			item.SubItems.Add(request.Url);
			item.ForeColor = Color.Green;
			lvRequests.Items.Add(item);

			// Add Transform Info
			foreach ( WebTransform transform in request.InputTransforms )
			{
				ListViewItem subitem = new ListViewItem();
				subitem.ImageIndex = 8;			
				subitem.Text = "";
				subitem.SubItems.Add("");
				subitem.SubItems.Add("Input Transform " + transform.Name + " applied.");
				subitem.ForeColor = Color.LightSeaGreen;

				lvRequests.Items.Add(subitem);
			}
			
			lvRequests.Refresh();
		}
		/// <summary>
		/// Adds a response node to the tree.
		/// </summary>
		/// <param name="request"> The web request.</param>
		private void AddResponseNode(WebRequest request)
		{
			TreeNode parent = tvResponses.Nodes[0];			

			TreeNode responseNode = new TreeNode(request.WebResponse.ResponseUri);
			responseNode.ImageIndex = 9;
			responseNode.SelectedImageIndex = 9;
			responseNode.Tag = request;

			responseNode.Nodes.Add("Request Information");
			responseNode.Nodes[0].ImageIndex = 1;
			responseNode.Nodes[0].SelectedImageIndex = 1;
			responseNode.Nodes.Add("Response Headers");
			responseNode.Nodes[1].ImageIndex = 1;
			responseNode.Nodes[1].SelectedImageIndex = 1;
			responseNode.Nodes.Add("Response Cookies");
			responseNode.Nodes[2].ImageIndex = 1;
			responseNode.Nodes[2].SelectedImageIndex = 1;
			responseNode.Nodes.Add("HTML Body");
			responseNode.Nodes[3].ImageIndex = 1;
			responseNode.Nodes[3].SelectedImageIndex = 1;
			parent.Nodes.Add(responseNode);
		}

		#region Scripting Command Events
		private void AddResponseNodeInvoker(object sender, RequestStartEndEventArgs e)
		{
			AddResponseNode(e.Request);

			MoveProgressBar(e.CurrentIndex);
		}

		private void AddRequestProgressInvoker(object sender, RequestStartEndEventArgs e)
		{
			AddRequestProgress(e.Request);

			if ( e.CurrentIndex == 0 )
			{
				StartProgressBar(e.RequestCount, e.CurrentIndex);
			}
		}
		private void scriptingCommand_OnRequestEnd(object sender, RequestStartEndEventArgs e)
		{
			// Add response to tree.
			Invoke(new OnRequestEndEventHandler(AddResponseNodeInvoker), new object[] {sender, e});
		}

		private void scriptingCommand_OnRequestStart(object sender, RequestStartEndEventArgs e)
		{
			// Add request to progress.
			Invoke(new OnRequestStartEventHandler(AddRequestProgressInvoker), new object[] {sender, e});
		}

		private void btnStop_Click(object sender, System.EventArgs e)
		{
			scriptingCommand.Stop();
		}

		#endregion

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			scriptingCommand.Reset();
			this.tvResponses.Nodes[0].Nodes.Clear();
			this.lvRequests.Items.Clear();
			this.EditorText = "";
			this.Hide();
		}

		private void tvResponses_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			StringBuilder text = new StringBuilder();
			string selectedNodeText = tvResponses.SelectedNode.Text;			
			currentWebResponse = null;

			switch ( selectedNodeText )
			{
				case "Request Information":
					WebRequest request = (WebRequest)tvResponses.SelectedNode.Parent.Tag;
					text.Append("-------------------------------\r\n");
					text.Append("Request Headers\r\n");
					text.Append("-------------------------------\r\n");
					ResponseResultTransform.AppendRequestHeaders(text, request);
					text.Append("-------------------------------\r\n");
					text.Append("Request Cookies\r\n");
					text.Append("-------------------------------\r\n");
					ResponseResultTransform.AppendCookies(text, request.Cookies);
					EditorText = text.ToString();
					ViewHTML(text.ToString());
					break;
				case "Response Headers":										
					text.Append("-------------------------------\r\n");
					text.Append("Response Headers\r\n");
					text.Append("-------------------------------\r\n");
					ResponseResultTransform.AppendResponseHeaders(text, ((WebRequest)tvResponses.SelectedNode.Parent.Tag).WebResponse);
					EditorText = text.ToString();
					ViewHTML(text.ToString());
					break;
				case "Response Cookies":
					text.Append("-------------------------------\r\n");
					text.Append("Response Cookies\r\n");
					text.Append("-------------------------------\r\n");
					ResponseResultTransform.AppendCookies(text, ((WebRequest)tvResponses.SelectedNode.Parent.Tag).WebResponse.Cookies);
					EditorText = text.ToString();
					ViewHTML(text.ToString());
					break;
				case "HTML Body":					
					currentWebResponse = ((WebRequest)tvResponses.SelectedNode.Parent.Tag).WebResponse;
					EditorText = currentWebResponse.HttpBody;
					ViewHTML(currentWebResponse.HttpBody);
					//panel1.Enabled = true;
					break;
			}
		}

		#region IE Events
		private void windowEvents_onerror(string description, string url, int line)
		{
			// ignore errors.
		}
		private bool iEvent_onclick(mshtml.IHTMLEventObj pEvtObj)
		{			
			Invoke(new SendElement(TranslateSelectedElement), new object[] {pEvtObj.srcElement});
			return false;
		}
		private void TranslateSelectedElement(mshtml.IHTMLElement element)
		{			
			StringBuilder text = new StringBuilder();
			AppendParent(text, element);
			_translatedDomXpath = text.ToString();
		}
		private void AppendParent(StringBuilder text, mshtml.IHTMLElement element)
		{
			if ( element != null )
			{
				AppendParent(text, element.parentElement);
				text.Append("/");
				text.Append(element.tagName.ToLower(System.Globalization.CultureInfo.InvariantCulture));
			}
		}
		#endregion

		private void cmbRegEx_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
		}


		#region Query Methods
//		private void ExecuteRegExFilter(string html)
//		{			
//			Application.DoEvents();
//
//			this.cmbXPath.Enabled = false;
//			this.cmbRegEx.Enabled = false;
//
//			// Add item to combo list
//			if ( cmbRegEx.Items.IndexOf(cmbRegEx.Text) == -1 )
//			{
//				this.cmbRegEx.Items.Add(cmbRegEx.Text);
//			}
//
//			Regex r = new Regex(cmbRegEx.Text, RegexOptions.None);
//
//			if ( r.IsMatch(html) )
//			{
//				MatchCollection matches = r.Matches(html);
//
//				StringBuilder text = new StringBuilder();
//				text.Append("Matches found:\r\n\r\n");
//
//				foreach ( Match m in matches )
//				{					
//					text.Append(m.Value);
//				}
//
//				this.EditorText = text.ToString();
//
//				if ( chkUpdateBrowser.Checked )
//				{
//					ViewHTML(EditorText);
//				}
//			} 
//			else 
//			{
//				this.EditorText = "No regular expression matches found.";
//			}
//
//			this.cmbXPath.Enabled = true;
//			this.cmbRegEx.Enabled = true;
//		}

//		private void ExecuteXPathFilter(string html)
//		{
//			Application.DoEvents();
//			this.cmbXPath.Enabled = false;
//			this.cmbRegEx.Enabled = false;
//
//			// Add item to combo list
//			if ( cmbXPath.Items.IndexOf(cmbXPath.Text) == -1 )
//			{
//				this.cmbXPath.Items.Add(cmbXPath.Text);
//			}
//			try
//			{
//				EditorText = queryUtil.GetXmlString(html,cmbXPath.Text);
//				
//				if ( chkUpdateBrowser.Checked )
//				{
//					ViewHTML(EditorText);
//				}
//			}
//			catch (Exception ex)
//			{
//				EditorText = ex.ToString();
//			}
//
//			this.cmbXPath.Enabled = true;
//			this.cmbRegEx.Enabled = true;
//
//		}



		/// <summary>
		/// ParseHTML.
		/// </summary>
		private string ParseHTML()
		{
			parser.ParserProperties.RemoveScriptTags = false;
			
			try
			{
				// load XmlView
				return parser.GetParsableString(this.EditorText);				
			}
			catch (Exception ex)
			{
				this.EditorText = ex.ToString();
				return String.Empty;
			}
		}
		#endregion

		private void button1_Click(object sender, System.EventArgs e)
		{			
			this.lvRequests.Items.Clear();
			this.tvResponses.Nodes[0].Nodes.Clear();
			this.EditorText = "";
			scriptingCommand.ResetCookies();
			scriptingCommand.ExecuteSessionUntilEnd(_appClone.Clone(), _startingIndex);
		}

		private void mnuXPathQueryDialog_Click(object sender, System.EventArgs e)
		{
			XmlXpathDialog dialog = new XmlXpathDialog();
			bool displayDialog = false;
			
			if ( currentWebResponse != null )
			{
				if ( currentWebResponse.GetHtmlXml.Length > 0 )
				{
					dialog.XmlString = currentWebResponse.GetHtmlXml;
					dialog.XPathLocation = _translatedDomXpath;
					displayDialog = true;
					dialog.Show();
				} 
				else 
				{
					string s = ParseHTML();
					if ( s != string.Empty )
					{
						dialog.XmlString = s;
						displayDialog = true;
						dialog.XPathLocation = _translatedDomXpath;
						dialog.Show();
					} 
				}				
			}			

			if ( displayDialog == false )
			{
				MessageBox.Show("The HTML cannot be transformed to XML. Use the RegEx Query dialog instead.", AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void mnuRegExQueryDialog_Click(object sender, System.EventArgs e)
		{
			RegExDesignerDialog dialog = new RegExDesignerDialog();
			bool displayDialog = false;
			
			if ( currentWebResponse != null )
			{
				if ( currentWebResponse.HttpBody.Length > 0 )
				{
					dialog.TextContent = currentWebResponse.HttpBody;
					//dialog.RegExQuery = _regex;
					displayDialog = true;
					dialog.Show();
				}
			}			

			if ( displayDialog == false )
			{
				MessageBox.Show("No HTTP Body data found.", AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void mnuXSLTDialog_Click(object sender, System.EventArgs e)
		{
			XsltDialog dialog = new XsltDialog();
			bool displayDialog = false;
			
			if ( currentWebResponse != null )
			{
				if ( currentWebResponse.HttpBody.Length > 0 )
				{
					dialog.XmlString = currentWebResponse.HttpBody;
					displayDialog = true;
					dialog.Show();
				}
			}			

			if ( displayDialog == false )
			{
				MessageBox.Show("No HTTP Body data found.", AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}		
		}
	}
}
