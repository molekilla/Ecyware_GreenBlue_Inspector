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
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Compona;
using Compona.SourceCode;

namespace Ecyware.GreenBlue.Controls.Scripting
{
	/// <summary>
	/// Contains the definition for the PostDataPage control.
	/// </summary>
	public class PostDataPage : BaseScriptingDataPage
	{
		//ScriptingApplicationSerializer serializer = new ScriptingApplicationSerializer();
		//private delegate void SetEditorDocumentEventHandler(SyntaxDocument document);
		private string textValue = string.Empty;
		string htmlSyntaxFile = string.Empty;
		Compona.SourceCode.Language language;

		private FindReplaceForm findReplace;
		private System.Windows.Forms.GroupBox grpUrlInfo;
		internal Compona.Windows.Forms.SyntaxBoxControl txtEditor;
		private Compona.SourceCode.SyntaxDocument syntaxDocument2;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Creates a new PostDataPage
		/// </summary>
		public PostDataPage()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			findReplace = new FindReplaceForm(txtEditor);
			htmlSyntaxFile = Application.StartupPath + @"\XML.syn";
			language = Compona.SourceCode.Language.FromSyntaxFile(htmlSyntaxFile);
			syntaxDocument2.Parser.Init(language);
			txtEditor.Document = syntaxDocument2;
			syntaxDocument2.ParseAll();
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
				try
				{
					syntaxDocument2.clear();

					// save value in temp
					textValue = value;

					// set wait message.				
					syntaxDocument2.Text = textValue;			

					// be sure to set parse all, this allows the parsing to be done in this method.
					txtEditor.Document = syntaxDocument2;
				}
				catch ( Exception ex )
				{
					MessageBox.Show(ex.ToString());
				}
			}
		}


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
		/// Pretty parse the xml.
		/// </summary>
		/// <param name="node"> A XmlNode to parse.</param>
		/// <returns> A formatted xml.</returns>
		private string IndentFormatXml(XmlNode node)
		{
			XmlNodeReader reader = new XmlNodeReader(node);

			StringWriter str = new StringWriter();
			XmlTextWriter writer = new XmlTextWriter(str);
			writer.Formatting = System.Xml.Formatting.Indented;
			writer.Indentation = 4;
			writer.WriteNode(reader, false);

			string result = str.ToString();

			writer.Close();
			str.Close();
			
			return result;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
//				if ( syntaxDocument2 != null )
//				{
//					syntaxDocument2.Dispose();
//				}
//				if ( txtEditor != null )
//				{
//					txtEditor.Dispose();
//				}
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(PostDataPage));
			this.grpUrlInfo = new System.Windows.Forms.GroupBox();
			this.txtEditor = new Compona.Windows.Forms.SyntaxBoxControl();
			this.syntaxDocument2 = new Compona.SourceCode.SyntaxDocument(this.components);
			this.grpUrlInfo.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpUrlInfo
			// 
			this.grpUrlInfo.Controls.Add(this.txtEditor);
			this.grpUrlInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grpUrlInfo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpUrlInfo.Location = new System.Drawing.Point(0, 0);
			this.grpUrlInfo.Name = "grpUrlInfo";
			this.grpUrlInfo.Size = new System.Drawing.Size(600, 400);
			this.grpUrlInfo.TabIndex = 1;
			this.grpUrlInfo.TabStop = false;
			this.grpUrlInfo.Text = "Post Data Editor";
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
			this.txtEditor.Size = new System.Drawing.Size(594, 381);
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
			// PostDataPage
			// 
			this.Controls.Add(this.grpUrlInfo);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PostDataPage";
			this.Size = new System.Drawing.Size(600, 400);
			this.grpUrlInfo.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

//		private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
//		{
//			if ( e.Button == tbSaveApplication )
//			{
//				if ( this.Parent.Parent is ScriptingDataDesigner )
//				{
//					ScriptingDataDesigner designer = (ScriptingDataDesigner)this.Parent.Parent;
//					designer.SaveApplication();
//				}
//			}
//
//			if ( e.Button == tbArguments )
//			{
//				if ( this.Parent.Parent is ScriptingDataDesigner )
//				{
//					ScriptingDataDesigner designer = (ScriptingDataDesigner)this.Parent.Parent;
//					designer.ShowScriptingArgumentsDesigner();
//				}
//			}
//		}

		/// <summary>
		/// Loads the request.
		/// </summary>
		/// <param name="index"> The current request index.</param>
		/// <param name="scripting"> The scripting application.</param>
		/// <param name="request"> The current web request.</param>
		public override void LoadRequest(int index, ScriptingApplication scripting ,Ecyware.GreenBlue.Engine.Scripting.WebRequest request)
		{
			base.LoadRequest (index, scripting, request);

			this.EditorText = "";
			string postData = string.Empty;
			if ( request.RequestType == HttpRequestType.POST )
			{
				postData = ((PostWebRequest)request).PostData;
				request.UpdateXmlEnvelope(postData);
			}
			if ( request.RequestType == HttpRequestType.PUT )
			{
				postData = ((PutWebRequest)request).PostData;
				request.UpdateXmlEnvelope(postData);
			}

			if ( request.XmlEnvelope != null )
			{
				this.EditorText = this.IndentFormatXml(request.XmlEnvelope);
			} 
		}

		/// <summary>
		/// Gets or sets the WebRequest.
		/// </summary>
		public override Ecyware.GreenBlue.Engine.Scripting.WebRequest WebRequest
		{
			get
			{
				if ( base.WebRequest != null )
				{
					//if ( this.EditorText.Length > 0 )
					//{
						if ( base.WebRequest.RequestType == HttpRequestType.POST )
						{
							((PostWebRequest)base.WebRequest).UsePostData = true;
						}
						UpdatePostData(base.WebRequest);
					//}
				}

				return base.WebRequest;
			}
		}

		/// <summary>
		/// Updates the current postdata.
		/// </summary>
		/// <param name="request"></param>
		private void UpdatePostData(WebRequest request)
		{
			if ( request.RequestType == HttpRequestType.POST )
			{
				((PostWebRequest)request).PostData = this.EditorText;
			}
			if ( request.RequestType == HttpRequestType.PUT )
			{
				((PutWebRequest)request).PostData = this.EditorText;				
			}
			request.UpdateXmlEnvelope(this.EditorText);
		}

		/// <summary>
		/// Updates the xml element.
		/// </summary>
//		private void UpdateXmlElement(WebRequest request)
//		{
//			MemoryStream stream = new MemoryStream();
//			byte[] data = System.Text.Encoding.UTF8.GetBytes(this.EditorText);
//			stream.Write(data,0, data.Length);
//			stream.Position = 0;
//			
//			XmlTextReader reader = null;
//			try
//			{
//				reader = new XmlTextReader(stream);
//			}
//			catch
//			{
//				// no xml
//				reader = null;
//			}
//
//			if ( reader != null )
//			{
//				XmlDocument document = new XmlDocument();
//				document.Load(reader);
//				request.XmlEnvelope = document.DocumentElement;
//			} 
//			else 
//			{
//				request.XmlEnvelope = null;
//			}
//
//			stream.Close();
//		}
	}
}
