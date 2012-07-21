// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: November 2003 - July 2004
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Ecyware.GreenBlue.Controls;
using Ecyware.GreenBlue.HtmlDom;
using Ecyware.GreenBlue.Protocols.Http;
using Ecyware.GreenBlue.HtmlCommand;
using Compona;
using Compona.SourceCode;

namespace Ecyware.GreenBlue.SessionScriptingDesigner
{
	/// <summary>
	/// Contains the definition for the TextEditorForm type.
	/// </summary>
	public class TextEditorForm : BasePluginForm
	{	
		private delegate void SetEditorDocumentEventHandler(SyntaxDocument document);
		private string textValue = string.Empty;
		private bool _enabledParsing = true;
		private Cursor tempCursor;
		string htmlSyntaxFile = string.Empty;
		Compona.SourceCode.Language language;

		private FindReplaceForm findReplace;
		//internal event InspectorStartRequestEventHandler StartEvent;		
		private Compona.SourceCode.SyntaxDocument syntaxDocument2;
		internal Compona.Windows.Forms.SyntaxBoxControl txtEditor;
		private System.Windows.Forms.PrintDialog printDialog;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Creates a new text editor form.
		/// </summary>
		public TextEditorForm()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			findReplace = new FindReplaceForm(this.txtEditor);
			htmlSyntaxFile = Application.StartupPath + @"\ASP.syn";
			language = Compona.SourceCode.Language.FromSyntaxFile(htmlSyntaxFile);
			//this.txtEditor.Document.Parser.Init(language);
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

		#region Editor Text Properties and Methods

		/// <summary>
		/// Enables the rich text parsing.
		/// </summary>
		internal bool EnabledRichTextParsing
		{
			get
			{
				return _enabledParsing;
			}
			set
			{
				_enabledParsing = value;
			}
		}

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

				if ( EnabledRichTextParsing )
				{
					// set wait message.				
					this.txtEditor.Document.Text = "Wait while document is being parsed...";

					// set thread
					Thread t = new Thread(new ThreadStart(SetAsyncEditorText));

					// set priority, very important else it will block in Normal
					t.Priority = ThreadPriority.BelowNormal;
					t.Start();

					// TODO: Add progress bar for init ... end parsing
				} 
				else 
				{
					Compona.SourceCode.SyntaxDocument doc = new SyntaxDocument();					
					doc.Text = textValue;
					this.txtEditor.Document = doc;
				}
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
			doc.UnFoldAll();

			this.BeginInvoke(new SetEditorDocumentEventHandler(SetEditorDocument), new object[] {doc});
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
		/// Print document.
		/// </summary>
		internal void PrintDocument()
		{
			//Print a document
			Compona.SourceCode.SourceCodePrintDocument pd;
			pd = new Compona.SourceCode.SourceCodePrintDocument (this.txtEditor.Document);

			printDialog.Document = pd;
			if (printDialog.ShowDialog() == DialogResult.OK)
				pd.Print();   
		}

		/// <summary>
		/// Saves html.
		/// </summary>
		internal void SaveHtml()
		{
			// send this to disk
			System.IO.Stream stream=null;

			saveFileDialog.InitialDirectory = Application.UserAppDataPath;
			saveFileDialog.RestoreDirectory = false;
			saveFileDialog.Filter = "HTML files (*.htm)|*.htm";
			saveFileDialog.Title = "Save HTML";

			if ( saveFileDialog.ShowDialog() == DialogResult.OK )
			{
				Application.DoEvents();
				tempCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;

				// file
				stream = saveFileDialog.OpenFile();
				if ( stream!=null )
				{
					try
					{
						StreamWriter writer = new StreamWriter(stream,System.Text.Encoding.Default);
						writer.Write(this.EditorText);
						writer.Flush();
						writer.Close();
					}
					catch ( Exception ex )
					{
						Utils.ExceptionHandler.RegisterException(ex);
						MessageBox.Show("Error while saving the html code.", "Ecyware GreenBlue Inspector", MessageBoxButtons.OK,MessageBoxIcon.Error);
					}
				}
			}

			if (stream != null)
			{
				Cursor.Current = tempCursor;
				stream.Close();
			}
		}
		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.txtEditor = new Compona.Windows.Forms.SyntaxBoxControl();
			this.syntaxDocument2 = new Compona.SourceCode.SyntaxDocument(this.components);
			this.printDialog = new System.Windows.Forms.PrintDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.SuspendLayout();
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
			this.txtEditor.Document = this.syntaxDocument2;
			this.txtEditor.FontName = "Courier new";
			this.txtEditor.InfoTipCount = 1;
			this.txtEditor.InfoTipPosition = null;
			this.txtEditor.InfoTipSelectedIndex = 1;
			this.txtEditor.InfoTipVisible = false;
			this.txtEditor.Location = new System.Drawing.Point(0, 0);
			this.txtEditor.LockCursorUpdate = false;
			this.txtEditor.Name = "txtEditor";
			this.txtEditor.Size = new System.Drawing.Size(600, 276);
			this.txtEditor.SmoothScroll = false;
			this.txtEditor.SplitView = false;
			this.txtEditor.SplitviewH = -4;
			this.txtEditor.SplitviewV = -4;
			this.txtEditor.TabGuideColor = System.Drawing.Color.FromArgb(((System.Byte)(244)), ((System.Byte)(243)), ((System.Byte)(234)));
			this.txtEditor.TabIndex = 8;
			this.txtEditor.Text = "syntaxBoxControl1";
			this.txtEditor.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
			this.txtEditor.WordMouseDown += new Compona.Windows.Forms.SyntaxBox.WordMouseHandler(this.txtEditor_WordMouseDown);
			// 
			// syntaxDocument2
			// 
			this.syntaxDocument2.Lines = new string[] {
														  ""};
			this.syntaxDocument2.MaxUndoBufferSize = 1000;
			this.syntaxDocument2.Modified = false;
			this.syntaxDocument2.UndoStep = 0;
			// 
			// printDialog
			// 
			this.printDialog.AllowSelection = true;
			// 
			// TextEditorForm
			// 
			this.Controls.Add(this.txtEditor);
			this.Name = "TextEditorForm";
			this.Size = new System.Drawing.Size(600, 276);
			this.ResumeLayout(false);

		}
		#endregion
		#region UI events
		private void txtEditor_WordMouseDown(object sender, ref Compona.Windows.Forms.SyntaxBox.WordMouseEventArgs e)
		{
//			string url=e.Word.Text;
//
//			RequestGetEventArgs args = new RequestGetEventArgs();
//			args.Url = url;
//			StartEvent(this,args);	
		}
		#endregion
	}
}
