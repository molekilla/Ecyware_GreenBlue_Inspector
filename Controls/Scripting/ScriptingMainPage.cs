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
using System.Threading;
using Ecyware.GreenBlue.Controls;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Engine.HtmlCommand;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.Scripting;
using Compona;
using Compona.SourceCode;

namespace Ecyware.GreenBlue.Controls.Scripting
{
	/// <summary>
	/// Contains the definition for the ScriptingMainPage control.
	/// </summary>
	public class ScriptingMainPage : BaseScriptingDataPage
	{
		ScriptingApplicationSerializer serializer = new ScriptingApplicationSerializer();
		private delegate void SetEditorDocumentEventHandler(SyntaxDocument document);
		private string textValue = string.Empty;
		string htmlSyntaxFile = string.Empty;
		Compona.SourceCode.Language language;

		private FindReplaceForm findReplace;
		private System.Windows.Forms.GroupBox grpUrlInfo;
		internal Compona.Windows.Forms.SyntaxBoxControl txtEditor;
		private Compona.SourceCode.SyntaxDocument syntaxDocument2;
		private System.Windows.Forms.ToolBarButton tbArguments;
		private System.Windows.Forms.ImageList icons;
		private System.Windows.Forms.ToolBarButton tbSaveApplication;
		private System.Windows.Forms.ToolBarButton tbTestApplication;
		private System.Windows.Forms.ToolBar toolBar;
		private System.Windows.Forms.ToolBarButton tbExport;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Creates a new SessionRequestHeaderEditor
		/// </summary>
		public ScriptingMainPage()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			findReplace = new FindReplaceForm(txtEditor);
			htmlSyntaxFile = Application.StartupPath + @"\XML.syn";
			language = Compona.SourceCode.Language.FromSyntaxFile(htmlSyntaxFile);
			syntaxDocument2.Parser.Init(language);
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
				Application.DoEvents();		
				syntaxDocument2.Text = textValue;			

				// be sure to set parse all, this allows the parsing to be done in this method.
				syntaxDocument2.ParseAll();
				syntaxDocument2.UnFoldAll();
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

		/// <summary>
		/// Displays the Find Dialog.
		/// </summary>
		internal void ShowFindDialog()
		{
			findReplace.ShowFind();
		}

		/// <summary>
		/// Displays the Replace Dialog.
		/// </summary>
		internal void ShowReplaceDialog()
		{
			findReplace.ShowReplace();
		}

		/// <summary>
		/// Shows the scripting data as xml.
		/// </summary>
		/// <param name="scriptingData"> The ScriptingData type.</param>
		public void ShowScriptingDataXml(ScriptingApplication scriptingData)
		{			
			this.EditorText = scriptingData.ToXml();
		}

		/// <summary>
		/// Loads the current scripting application.
		/// </summary>
		/// <returns> A ScriptingApplication type.</returns>
		public ScriptingApplication LoadScriptingApplication()
		{
			if ( serializer.CanDeserialize(this.EditorText) )
			{
				return ScriptingApplication.FromXml(this.EditorText);
			} else {
				return null;
			}
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				this.syntaxDocument2.Dispose();
				txtEditor.Dispose();

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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ScriptingMainPage));
			this.grpUrlInfo = new System.Windows.Forms.GroupBox();
			this.txtEditor = new Compona.Windows.Forms.SyntaxBoxControl();
			this.syntaxDocument2 = new Compona.SourceCode.SyntaxDocument(this.components);
			this.toolBar = new System.Windows.Forms.ToolBar();
			this.tbSaveApplication = new System.Windows.Forms.ToolBarButton();
			this.tbArguments = new System.Windows.Forms.ToolBarButton();
			this.tbTestApplication = new System.Windows.Forms.ToolBarButton();
			this.tbExport = new System.Windows.Forms.ToolBarButton();
			this.icons = new System.Windows.Forms.ImageList(this.components);
			this.grpUrlInfo.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpUrlInfo
			// 
			this.grpUrlInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.grpUrlInfo.Controls.Add(this.txtEditor);
			this.grpUrlInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpUrlInfo.Location = new System.Drawing.Point(0, 54);
			this.grpUrlInfo.Name = "grpUrlInfo";
			this.grpUrlInfo.Size = new System.Drawing.Size(600, 348);
			this.grpUrlInfo.TabIndex = 1;
			this.grpUrlInfo.TabStop = false;
			this.grpUrlInfo.Text = "Scripting Application XML Editor";
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
			this.txtEditor.FontName = "Courier new";
			this.txtEditor.Indent = Compona.Windows.Forms.SyntaxBox.IndentStyle.Smart;
			this.txtEditor.InfoTipCount = 1;
			this.txtEditor.InfoTipPosition = null;
			this.txtEditor.InfoTipSelectedIndex = 1;
			this.txtEditor.InfoTipVisible = false;
			this.txtEditor.Location = new System.Drawing.Point(3, 16);
			this.txtEditor.LockCursorUpdate = false;
			this.txtEditor.Name = "txtEditor";
			this.txtEditor.ShowTabGuides = true;
			this.txtEditor.Size = new System.Drawing.Size(594, 329);
			this.txtEditor.SmoothScroll = false;
			this.txtEditor.SplitviewH = -4;
			this.txtEditor.SplitviewV = -4;
			this.txtEditor.TabGuideColor = System.Drawing.Color.FromArgb(((System.Byte)(244)), ((System.Byte)(243)), ((System.Byte)(234)));
			this.txtEditor.TabIndex = 9;
			this.txtEditor.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
			// 
			// syntaxDocument2
			// 
			this.syntaxDocument2.Lines = new string[] {
														  ""};
			this.syntaxDocument2.MaxUndoBufferSize = 1000;
			this.syntaxDocument2.Modified = false;
			this.syntaxDocument2.UndoStep = 0;
			// 
			// toolBar
			// 
			this.toolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																					   this.tbSaveApplication,
																					   this.tbArguments,
																					   this.tbTestApplication,
																					   this.tbExport});
			this.toolBar.DropDownArrows = true;
			this.toolBar.ImageList = this.icons;
			this.toolBar.Location = new System.Drawing.Point(0, 0);
			this.toolBar.Name = "toolBar";
			this.toolBar.ShowToolTips = true;
			this.toolBar.Size = new System.Drawing.Size(600, 50);
			this.toolBar.TabIndex = 2;
			this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
			// 
			// tbSaveApplication
			// 
			this.tbSaveApplication.ImageIndex = 0;
			this.tbSaveApplication.Text = "Save Application";
			this.tbSaveApplication.ToolTipText = "Save Application";
			// 
			// tbArguments
			// 
			this.tbArguments.ImageIndex = 1;
			this.tbArguments.Text = "Arguments Designer";
			this.tbArguments.Visible = false;
			// 
			// tbTestApplication
			// 
			this.tbTestApplication.ImageIndex = 2;
			this.tbTestApplication.Text = "Run Application";
			this.tbTestApplication.ToolTipText = "Run Application";
			this.tbTestApplication.Visible = false;
			// 
			// tbExport
			// 
			this.tbExport.ImageIndex = 0;
			this.tbExport.Text = "Export XML";
			this.tbExport.ToolTipText = "Export XML";
			// 
			// icons
			// 
			this.icons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.icons.ImageSize = new System.Drawing.Size(24, 24);
			this.icons.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("icons.ImageStream")));
			this.icons.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// ScriptingMainPage
			// 
			this.Controls.Add(this.toolBar);
			this.Controls.Add(this.grpUrlInfo);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ScriptingMainPage";
			this.Size = new System.Drawing.Size(600, 400);
			this.grpUrlInfo.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			ScriptingDataDesigner designer = (ScriptingDataDesigner)this.Parent.Parent;

			if ( e.Button == tbSaveApplication )
			{
				if ( this.Parent.Parent is ScriptingDataDesigner )
				{
					designer.SaveApplication();
				}
			}

			if ( e.Button == tbArguments )
			{
				if ( this.Parent.Parent is ScriptingDataDesigner )
				{
					designer.ShowScriptingArgumentsDesigner();
				}
			}

			if ( e.Button == tbTestApplication )
			{
				designer.RunApplication();
			}

			if ( e.Button == tbExport )
			{
				designer.ExportApplicationToXml();
			}
		}
	}
}
