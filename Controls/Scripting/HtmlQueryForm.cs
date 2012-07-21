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
using System.Windows.Forms;
using System.Xml;
using System.Threading;
using Ecyware.GreenBlue.HtmlCommand;
using Ecyware.GreenBlue.Controls;
using Compona;
using Compona.SourceCode;

namespace Ecyware.GreenBlue.Controls.Scripting
{
	/// <summary>
	/// Summary description for TextViewer.
	/// </summary>
	public class HtmlQueryForm : BasePluginForm
	{
		private delegate void SetEditorDocumentEventHandler(SyntaxDocument document);
		Compona.SourceCode.Language language;
		private string textValue = string.Empty;
		HtmlQueryUtil queryUtil = new HtmlQueryUtil(true);
		private System.Windows.Forms.GroupBox grpHTTPCommands;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cmbFilters;
		private System.Windows.Forms.Button cmdFilter;
		internal Compona.Windows.Forms.SyntaxBoxControl txtEditor;
		private Compona.SourceCode.SyntaxDocument syntaxDocument1;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Creates a HtmlQueryForm.
		/// </summary>
		public HtmlQueryForm()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			string htmlSyntaxFile = Application.StartupPath + @"\XML.syn";
			language = Compona.SourceCode.Language.FromSyntaxFile(htmlSyntaxFile);
			this.txtEditor.Document.Parser.Init(language);
		}


		/// <summary>
		/// Creates a HtmlQueryForm with data.
		/// </summary>
		/// <param name="html"> Html text.</param>
		public HtmlQueryForm(string html) : this()
		{
			this.EditorText = html;
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
				if( this.txtEditor != null )
				{
					txtEditor.Dispose();
					txtEditor = null;
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
			this.grpHTTPCommands = new System.Windows.Forms.GroupBox();
			this.cmbFilters = new System.Windows.Forms.ComboBox();
			this.cmdFilter = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.txtEditor = new Compona.Windows.Forms.SyntaxBoxControl();
			this.syntaxDocument1 = new Compona.SourceCode.SyntaxDocument(this.components);
			this.grpHTTPCommands.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpHTTPCommands
			// 
			this.grpHTTPCommands.Controls.Add(this.cmbFilters);
			this.grpHTTPCommands.Controls.Add(this.cmdFilter);
			this.grpHTTPCommands.Controls.Add(this.label1);
			this.grpHTTPCommands.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpHTTPCommands.Location = new System.Drawing.Point(0, 0);
			this.grpHTTPCommands.Name = "grpHTTPCommands";
			this.grpHTTPCommands.Size = new System.Drawing.Size(600, 42);
			this.grpHTTPCommands.TabIndex = 9;
			this.grpHTTPCommands.TabStop = false;
			this.grpHTTPCommands.Text = "XPath Filter";
			// 
			// cmbFilters
			// 
			this.cmbFilters.ItemHeight = 13;
			this.cmbFilters.Location = new System.Drawing.Point(66, 12);
			this.cmbFilters.MaxDropDownItems = 10;
			this.cmbFilters.Name = "cmbFilters";
			this.cmbFilters.Size = new System.Drawing.Size(462, 21);
			this.cmbFilters.TabIndex = 6;
			this.cmbFilters.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbFilters_KeyPress);
			// 
			// cmdFilter
			// 
			this.cmdFilter.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdFilter.Location = new System.Drawing.Point(534, 12);
			this.cmdFilter.Name = "cmdFilter";
			this.cmdFilter.Size = new System.Drawing.Size(60, 24);
			this.cmdFilter.TabIndex = 5;
			this.cmdFilter.Text = "Filter";
			this.cmdFilter.Click += new System.EventHandler(this.cmdFilter_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Query:";
			// 
			// txtEditor
			// 
			this.txtEditor.ActiveView = Compona.Windows.Forms.SyntaxBox.ActiveView.BottomRight;
			this.txtEditor.AutoListPosition = null;
			this.txtEditor.AutoListSelectedText = "a123";
			this.txtEditor.AutoListVisible = false;
			this.txtEditor.BackColor = System.Drawing.Color.White;
			this.txtEditor.CopyAsRTF = false;
			this.txtEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtEditor.Document = this.syntaxDocument1;
			this.txtEditor.FontName = "Courier new";
			this.txtEditor.InfoTipCount = 1;
			this.txtEditor.InfoTipPosition = null;
			this.txtEditor.InfoTipSelectedIndex = 1;
			this.txtEditor.InfoTipVisible = false;
			this.txtEditor.Location = new System.Drawing.Point(0, 42);
			this.txtEditor.LockCursorUpdate = false;
			this.txtEditor.Name = "txtEditor";
			this.txtEditor.Size = new System.Drawing.Size(600, 246);
			this.txtEditor.SmoothScroll = false;
			this.txtEditor.SplitView = false;
			this.txtEditor.SplitviewH = -4;
			this.txtEditor.SplitviewV = -4;
			this.txtEditor.TabGuideColor = System.Drawing.Color.FromArgb(((System.Byte)(244)), ((System.Byte)(243)), ((System.Byte)(234)));
			this.txtEditor.TabIndex = 10;
			this.txtEditor.Text = "syntaxBoxControl1";
			this.txtEditor.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
			// 
			// syntaxDocument1
			// 
			this.syntaxDocument1.Lines = new string[] {
														  ""};
			this.syntaxDocument1.MaxUndoBufferSize = 1000;
			this.syntaxDocument1.Modified = false;
			this.syntaxDocument1.UndoStep = 0;
			// 
			// HtmlQueryForm
			// 
			this.Controls.Add(this.txtEditor);
			this.Controls.Add(this.grpHTTPCommands);
			this.Name = "HtmlQueryForm";
			this.Size = new System.Drawing.Size(600, 288);
			this.Load += new System.EventHandler(this.TextViewer_Load);
			this.grpHTTPCommands.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

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
				syntaxDocument1.clear();
				// save value in temp
				textValue = value;
				// set wait message.
				this.txtEditor.Document.Text = "Wait while document is being parsed...";
				// set thread
				Thread t = new Thread(new ThreadStart(SetAsyncEditorText));
				// set priority, very important else it will block in Normal
				t.Priority = ThreadPriority.BelowNormal;
				t.Start();
				// TODO: Add progress bar for init ... end parsing
			}
		}

		/// <summary>
		/// Sets the text async.
		/// </summary>
		private void SetAsyncEditorText()
		{
			Compona.SourceCode.SyntaxDocument doc = new SyntaxDocument();
			doc.Parser.Init(language);
			doc.Text = textValue;

			// be sure to set parse all, this allows the parsing to be done in this method.
			doc.ParseAll(true);

			this.Invoke(new SetEditorDocumentEventHandler(SetEditorDocument), new object[] {doc});
		}
		/// <summary>
		/// Sets the editor document.
		/// </summary>
		/// <param name="document"> The SyntaxDocument to use.</param>
		private void SetEditorDocument(SyntaxDocument document)
		{
			this.txtEditor.Document = document;
		}

		#endregion

		private void cmdFilter_Click(object sender, System.EventArgs e)
		{
			ExecuteFilter();
		}

		/// <summary>
		/// Queries the current html.
		/// </summary>
		/// <returns> Returns a string with the result.</returns>
		public string GetXmlString()
		{
			try
			{
				if ( cmbFilters.Text=="")
				{					
					return queryUtil.GetXmlString(this.EditorText,"/");
				} else {
					return queryUtil.GetXmlString(this.EditorText,cmbFilters.Text);
				}
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Queries the current html.
		/// </summary>
		/// <returns> Returns a XmlNodeList with the result.</returns>
		public XmlNodeList GetXmlNodes()
		{
			try
			{
				return queryUtil.GetNodes(this.EditorText,cmbFilters.Text);
			}
			catch
			{
				throw;
			}
		}
		private void ExecuteFilter()
		{
			Application.DoEvents();
			//this.txtEditor.Document.Text = "Wait while document is being parsed...";
			this.cmbFilters.Enabled=false;
			this.cmdFilter.Enabled=false;

			// Add item to combo list
			if ( cmbFilters.Items.IndexOf(cmbFilters.Text) == -1 )
			{
				this.cmbFilters.Items.Add(cmbFilters.Text);
			}
			try
			{
				this.EditorText = queryUtil.GetXmlString(this.EditorText,cmbFilters.Text);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message,"Ecyware GreenBlue Inspector",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}

			this.cmbFilters.Enabled=true;
			this.cmdFilter.Enabled=true;

		}

		private void TextViewer_Load(object sender, System.EventArgs e)
		{
			this.cmbFilters.Items.Add("/html/body");
			this.cmbFilters.Items.Add("//form");
			this.cmbFilters.Items.Add("//input");

		}

		private void cmbFilters_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
				if ( e.KeyChar==(char)13 )
				{
					ExecuteFilter();
					e.Handled=true;
				}					
		}
	}
}
