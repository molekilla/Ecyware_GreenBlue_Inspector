// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: March 2005
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
using System.IO;
using Compona;
using Compona.SourceCode;
using Ecyware.GreenBlue.Utils;

namespace Ecyware.GreenBlue.Utils
{
	/// <summary>
	/// Summary description for XsltDialog.
	/// </summary>
	public class XsltDialog : System.Windows.Forms.Form
	{
		Cursor tempCursor;
		string _location;
		XsltCommand xsltCommand = new XsltCommand();
		System.Xml.XmlNamespaceManager _namespaceCache;
		private string _xml;
		private string _xslt;
		private string textValue = string.Empty;		
		string htmlSyntaxFile = string.Empty;
		Compona.SourceCode.Language language;
		private System.Windows.Forms.GroupBox grpHTTPCommands;
		private System.Windows.Forms.ComboBox cmbFilters;
		private System.Windows.Forms.Button cmdFilter;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnSet;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem mnuXslt;
		private System.Windows.Forms.MenuItem mnuClose;
		private System.Windows.Forms.Splitter splitter1;
		internal Compona.Windows.Forms.SyntaxBoxControl txtXML;
		internal Compona.Windows.Forms.SyntaxBoxControl txtXSLT;
		private Compona.SourceCode.SyntaxDocument synXml;
		private Compona.SourceCode.SyntaxDocument synXslt;
		private System.Windows.Forms.MenuItem mnuExecute;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.OpenFileDialog dlgOpenFile;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Creates a new XsltDialog.
		/// </summary>
		public XsltDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			htmlSyntaxFile = Application.StartupPath + @"\XML.syn";
			language = Compona.SourceCode.Language.FromSyntaxFile(htmlSyntaxFile);
			synXml.Parser.Init(language);
			synXslt.Parser.Init(language);

			StringBuilder defaultTemplate = new StringBuilder();			
			defaultTemplate.Append("<!-- Author: Ecyware  -->");
			defaultTemplate.Append("\r\n");
			defaultTemplate.Append("<!-- Description: Default Test Template -->");
			defaultTemplate.Append("\r\n");
			defaultTemplate.Append("<!-- Note: Load an existing XSLT Template from File > Open XSLT or use this template. -->");
			defaultTemplate.Append("\r\n");
			defaultTemplate.Append("<!-- Note: We recommend always using UTF-8 for best support. -->");
			defaultTemplate.Append("\r\n");
			defaultTemplate.Append("<!-- Note: For XML, XPath and XSLT documentation, visit http://msdn.microsoft.com/ -->");
			defaultTemplate.Append("\r\n");
			defaultTemplate.Append("<xsl:stylesheet xmlns:xsl=\"http://www.w3.org/1999/XSL/Transform\" version=\"1.0\">");
			defaultTemplate.Append("\r\n");
			defaultTemplate.Append("<xsl:output method=\"xml\" version=\"1.0\" indent=\"yes\" encoding=\"UTF-8\" omit-xml-declaration=\"yes\"/>");
			defaultTemplate.Append("\r\n");
			defaultTemplate.Append("<xsl:template match=\"/\">");
			defaultTemplate.Append("\r\n");
			defaultTemplate.Append("<xsl:if test=\"rss\">");
			defaultTemplate.Append("\r\n");
			defaultTemplate.Append("<xsl:apply-templates select=\"rss\" />");
			defaultTemplate.Append("\r\n");
			defaultTemplate.Append("</xsl:if>");
			defaultTemplate.Append("\r\n");
			defaultTemplate.Append("</xsl:template>");
			defaultTemplate.Append("\r\n");
			defaultTemplate.Append("<!-- RSS Template -->");
			defaultTemplate.Append("\r\n");
			defaultTemplate.Append("<xsl:template name=\"Reseller\" match=\"rss\">");
			defaultTemplate.Append("\r\n");
			defaultTemplate.Append("<div>");
			defaultTemplate.Append("\r\n");
			defaultTemplate.Append("<xsl:for-each select=\"channel/item\">");
			defaultTemplate.Append("\r\n");
			defaultTemplate.Append("<br/><span>Titel:<xsl:value-of select=\"./description\" /></span>");
			defaultTemplate.Append("\r\n");
			defaultTemplate.Append("</xsl:for-each>");
			defaultTemplate.Append("\r\n");
			defaultTemplate.Append("</div>");
			defaultTemplate.Append("\r\n");
			defaultTemplate.Append("</xsl:template>");
			defaultTemplate.Append("\r\n");
			defaultTemplate.Append("</xsl:stylesheet>");
			defaultTemplate.Append("\r\n");

			this.XsltText = defaultTemplate.ToString();
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(XsltDialog));
			this.grpHTTPCommands = new System.Windows.Forms.GroupBox();
			this.btnSet = new System.Windows.Forms.Button();
			this.cmbFilters = new System.Windows.Forms.ComboBox();
			this.cmdFilter = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtXSLT = new Compona.Windows.Forms.SyntaxBoxControl();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.txtXML = new Compona.Windows.Forms.SyntaxBoxControl();
			this.synXml = new Compona.SourceCode.SyntaxDocument(this.components);
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.mnuXslt = new System.Windows.Forms.MenuItem();
			this.mnuExecute = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.mnuClose = new System.Windows.Forms.MenuItem();
			this.synXslt = new Compona.SourceCode.SyntaxDocument(this.components);
			this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
			this.grpHTTPCommands.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpHTTPCommands
			// 
			this.grpHTTPCommands.Controls.Add(this.btnSet);
			this.grpHTTPCommands.Controls.Add(this.cmbFilters);
			this.grpHTTPCommands.Controls.Add(this.cmdFilter);
			this.grpHTTPCommands.Controls.Add(this.label1);
			this.grpHTTPCommands.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpHTTPCommands.Location = new System.Drawing.Point(0, 0);
			this.grpHTTPCommands.Name = "grpHTTPCommands";
			this.grpHTTPCommands.Size = new System.Drawing.Size(610, 42);
			this.grpHTTPCommands.TabIndex = 10;
			this.grpHTTPCommands.TabStop = false;
			this.grpHTTPCommands.Text = "XPath Filter for XML";
			// 
			// btnSet
			// 
			this.btnSet.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSet.Location = new System.Drawing.Point(534, 12);
			this.btnSet.Name = "btnSet";
			this.btnSet.Size = new System.Drawing.Size(60, 24);
			this.btnSet.TabIndex = 7;
			this.btnSet.Text = "&Set";
			this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
			// 
			// cmbFilters
			// 
			this.cmbFilters.ItemHeight = 13;
			this.cmbFilters.Location = new System.Drawing.Point(66, 17);
			this.cmbFilters.MaxDropDownItems = 10;
			this.cmbFilters.Name = "cmbFilters";
			this.cmbFilters.Size = new System.Drawing.Size(390, 21);
			this.cmbFilters.TabIndex = 6;
			this.cmbFilters.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbXPath_KeyPress);
			// 
			// cmdFilter
			// 
			this.cmdFilter.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdFilter.Location = new System.Drawing.Point(462, 12);
			this.cmdFilter.Name = "cmdFilter";
			this.cmdFilter.Size = new System.Drawing.Size(60, 24);
			this.cmdFilter.TabIndex = 5;
			this.cmdFilter.Text = "&OK";
			this.cmdFilter.Click += new System.EventHandler(this.cmdFilter_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Query";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtXSLT);
			this.groupBox1.Controls.Add(this.splitter1);
			this.groupBox1.Controls.Add(this.txtXML);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 42);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(610, 332);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "XML / XSLT";
			// 
			// txtXSLT
			// 
			this.txtXSLT.ActiveView = Compona.Windows.Forms.SyntaxBox.ActiveView.BottomRight;
			this.txtXSLT.AutoListPosition = null;
			this.txtXSLT.AutoListSelectedText = "a123";
			this.txtXSLT.AutoListVisible = false;
			this.txtXSLT.CopyAsRTF = false;
			this.txtXSLT.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtXSLT.FontName = "Courier new";
			this.txtXSLT.Indent = Compona.Windows.Forms.SyntaxBox.IndentStyle.Smart;
			this.txtXSLT.InfoTipCount = 1;
			this.txtXSLT.InfoTipPosition = null;
			this.txtXSLT.InfoTipSelectedIndex = 1;
			this.txtXSLT.InfoTipVisible = false;
			this.txtXSLT.Location = new System.Drawing.Point(3, 151);
			this.txtXSLT.LockCursorUpdate = false;
			this.txtXSLT.Name = "txtXSLT";
			this.txtXSLT.ShowTabGuides = true;
			this.txtXSLT.Size = new System.Drawing.Size(604, 178);
			this.txtXSLT.SmoothScroll = false;
			this.txtXSLT.SplitviewH = -4;
			this.txtXSLT.SplitviewV = -4;
			this.txtXSLT.TabGuideColor = System.Drawing.Color.FromArgb(((System.Byte)(228)), ((System.Byte)(231)), ((System.Byte)(235)));
			this.txtXSLT.TabIndex = 14;
			this.txtXSLT.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitter1.Location = new System.Drawing.Point(3, 148);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(604, 3);
			this.splitter1.TabIndex = 13;
			this.splitter1.TabStop = false;
			// 
			// txtXML
			// 
			this.txtXML.ActiveView = Compona.Windows.Forms.SyntaxBox.ActiveView.BottomRight;
			this.txtXML.AutoListPosition = null;
			this.txtXML.AutoListSelectedText = "a123";
			this.txtXML.AutoListVisible = false;
			this.txtXML.CopyAsRTF = false;
			this.txtXML.Dock = System.Windows.Forms.DockStyle.Top;
			this.txtXML.FontName = "Courier new";
			this.txtXML.Indent = Compona.Windows.Forms.SyntaxBox.IndentStyle.Smart;
			this.txtXML.InfoTipCount = 1;
			this.txtXML.InfoTipPosition = null;
			this.txtXML.InfoTipSelectedIndex = 1;
			this.txtXML.InfoTipVisible = false;
			this.txtXML.Location = new System.Drawing.Point(3, 16);
			this.txtXML.LockCursorUpdate = false;
			this.txtXML.Name = "txtXML";
			this.txtXML.ShowTabGuides = true;
			this.txtXML.Size = new System.Drawing.Size(604, 132);
			this.txtXML.SmoothScroll = false;
			this.txtXML.SplitviewH = -4;
			this.txtXML.SplitviewV = -4;
			this.txtXML.TabGuideColor = System.Drawing.Color.FromArgb(((System.Byte)(228)), ((System.Byte)(231)), ((System.Byte)(235)));
			this.txtXML.TabIndex = 12;
			this.txtXML.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
			// 
			// synXml
			// 
			this.synXml.Lines = new string[] {
												 ""};
			this.synXml.MaxUndoBufferSize = 1000;
			this.synXml.Modified = false;
			this.synXml.UndoStep = 0;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.mnuXslt,
																					  this.mnuExecute,
																					  this.menuItem3,
																					  this.mnuClose});
			this.menuItem1.Text = "&File";
			// 
			// mnuXslt
			// 
			this.mnuXslt.Index = 0;
			this.mnuXslt.Text = "&Open XSLT...";
			this.mnuXslt.Click += new System.EventHandler(this.mnuXslt_Click);
			// 
			// mnuExecute
			// 
			this.mnuExecute.Index = 1;
			this.mnuExecute.Text = "&Execute XSLT Transform";
			this.mnuExecute.Click += new System.EventHandler(this.mnuExecute_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 2;
			this.menuItem3.Text = "-";
			// 
			// mnuClose
			// 
			this.mnuClose.Index = 3;
			this.mnuClose.Text = "&Close";
			this.mnuClose.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// synXslt
			// 
			this.synXslt.Lines = new string[] {
												  ""};
			this.synXslt.MaxUndoBufferSize = 1000;
			this.synXslt.Modified = false;
			this.synXslt.UndoStep = 0;
			// 
			// XsltDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(610, 374);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.grpHTTPCommands);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu1;
			this.Name = "XsltDialog";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "XSLT Dialog";
			this.grpHTTPCommands.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Query Methods
		/// <summary>
		/// Executes the XPath query.
		/// </summary>
		/// <param name="html"> The HTML text content.</param>
		private void ExecuteXPathFilter(string html)
		{
			Application.DoEvents();
			this.cmbFilters.Enabled = false;


			// Add item to combo list
			if ( cmbFilters.Items.IndexOf(cmbFilters.Text) == -1 )
			{
				this.cmbFilters.Items.Add(cmbFilters.Text);
			}
			try
			{
				XmlText = GetXmlString(html,cmbFilters.Text);			
			}
			catch (Exception ex)
			{
				XmlText = ex.ToString();
			}

			this.cmbFilters.Enabled = true;

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
					return GetXmlString(this.XmlText,"/");
				} 
				else 
				{
					return GetXmlString(this.XmlText,cmbFilters.Text);
				}
			}
			catch
			{
				throw;
			}
		}

		private void btnSet_Click(object sender, System.EventArgs e)
		{
			this.XPathLocation = this.cmbFilters.Text;
			this.DialogResult = DialogResult.OK;
		}

		private void cmdFilter_Click(object sender, System.EventArgs e)
		{
			if ( XmlString != null )
			{
				if ( XmlText.Length > 0 )
				{
					ExecuteXPathFilter(XmlText);
				} 
				else 
				{
					ExecuteXPathFilter(XmlString);
				}
			}			
		}


		#endregion
		#region Editor Text Properties and Methods

		/// <summary>
		/// Gets or sets the Xstl Editor Text.
		/// </summary>
		internal string XsltText
		{
			get
			{
				return this.txtXSLT.Document.Text;
			}
			set
			{
				// save value in temp
				synXslt.Text = value;			
				txtXSLT.Document = synXslt;
			}
		}

		/// <summary>
		/// Gets or sets the Xml Editor Text.
		/// </summary>
		internal string XmlText
		{
			get
			{
				return this.txtXML.Document.Text;
			}
			set
			{
				// save value in temp
				synXml.Text = value;			
				txtXML.Document = synXml;
			}
		}


		#endregion
		private void cmbXPath_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ( e.KeyChar==(char)13 )
			{
				if ( XmlString != null )
				{
					if ( XmlString.Length > 0 )
					{
						ExecuteXPathFilter(XmlString);
					}
					e.Handled=true;
				}
			}	
		}

		/// <summary>
		/// Gets or sets the xpath location.
		/// </summary>
		public string XPathLocation
		{
			get
			{
				return _location;
			}
			set
			{
				_location = value;
			}
		}
		/// <summary>
		/// Gets or sets the xml string.
		/// </summary>
		public string XmlString
		{
			get
			{
				return _xml;
			}
			set
			{
				_xml = value;
			}
		}

		/// <summary>
		/// Gets or sets the xml string.
		/// </summary>
		public string XsltString
		{
			get
			{
				return _xslt;
			}
			set
			{
				_xslt = value;
			}
		}
		#region Resolve Namespaces methods
		/// <summary>
		/// Checks for namespaces found and adds them to a Hashtable.
		/// </summary>
		/// <param name="reader"> A StringReader representing the html source.</param>
		/// <returns> A Hashtable with the namespaces.</returns>
		public static Hashtable ResolveNamespaces(StringReader reader)
		{
			XmlTextReader xml = new XmlTextReader(reader);
			return ResolveNamespaces(xml);
		}
		/// <summary>
		/// Checks for namespaces found and adds them to a Hashtable.
		/// </summary>
		/// <param name="reader"> A XmlTextReader representing the html source.</param>
		/// <returns> A Hashtable with the namespaces.</returns>
		public static Hashtable ResolveNamespaces(XmlTextReader reader)
		{
			Hashtable namespaces = new Hashtable();

			try
			{
				reader.Normalization=true;
				while ( reader.Read() )
				{
					if ( reader.NodeType == XmlNodeType.Element )
					{
						while ( reader.MoveToNextAttribute() )
						{
							if ( (reader.Name.ToLower().StartsWith("xmlns:")) || (reader.Name.ToLower().StartsWith("xml:")) )
							{
								Char[] ch = new Char[] {':'};
								string prefix = reader.Name.Split(ch)[1];
								namespaces.Add(prefix,reader.Value);
							}
							if ( reader.Name.ToLower() == "xmlns" )
							{
								// default namespace
								namespaces.Add("",reader.Value);
							}
						}

						break;
					}
				}
				return namespaces;
			}
			catch
			{
				throw;
			}

		}

		/// <summary>
		/// Resolves the namespaces for a XmlDocument.
		/// </summary>
		/// <param name="reader"> A StringReader containing the xml data.</param>
		/// <param name="namespaceManager"> A XmlNamespaceManager from an existing document.</param>
		/// <remarks> To create a XmlNamespaceManager for a document, use XmlNamespaceManager  nsMgr = new XmlNamespaceManager(document.NameTable).</remarks>
		/// <returns> An updated XmlNamespaceManager</returns>
		public static XmlNamespaceManager ResolveNamespaces(StringReader reader, XmlNamespaceManager namespaceManager)
		{
			// resolve namespaces before loading document
			Hashtable values = ResolveNamespaces(new XmlTextReader(reader));

			foreach ( DictionaryEntry de in values )
			{
				namespaceManager.AddNamespace((string)de.Key,(string)de.Value);
			}

			return namespaceManager;
		}
		#endregion
		#region XPath Query
		/// <summary>
		/// Gets a XML string from the queried HTML.
		/// </summary>
		/// <param name="data"> The HTML content to query.</param>
		/// <param name="query"> The XPath Query.</param>
		/// <returns> A XML string.</returns>
		public string GetXmlString(string data,string query)
		{			
			
			StringBuilder sb = new StringBuilder();

			try
			{
				XmlDocument doc;

				//Load the file.
				doc = new XmlDocument(); 
				doc.Load(new StringReader(data));			

				if ( _namespaceCache == null)
				{
					// create prefix<->namespace mappings (if any) 
					XmlNamespaceManager  nsMgr = new XmlNamespaceManager(doc.NameTable);

					// resolve namespaces before loading document
					Hashtable values = ResolveNamespaces(new XmlTextReader(new StringReader(data)));

					foreach (DictionaryEntry de in values)
					{
						nsMgr.AddNamespace((string)de.Key,(string)de.Value);
					}

					_namespaceCache = nsMgr;
				}

				// Query the document 
				XmlNodeList nodes = doc.SelectNodes(query, _namespaceCache); 

				// print output 
				foreach(XmlNode node in nodes)
				{
					sb.Append(IndentFormatXml(node) + "\n\n");
				}

				return sb.ToString();

			}
			catch
			{
				throw;
			}
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

		#endregion
		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void mnuExecute_Click(object sender, System.EventArgs e)
		{
			ExecuteTransform();
		}

		private void ExecuteTransform()
		{		
			string result = string.Empty;
			XsltTransformResult dialog = new XsltTransformResult();

			try
			{
				result = xsltCommand.TransformFromData(this.XmlText, this.XsltText);				
			}
			catch ( Exception ex )
			{
				result = ex.ToString();
			}

			dialog.SetText(result);
			dialog.ShowDialog();
		}

		private void mnuXslt_Click(object sender, System.EventArgs e)
		{
			OpenXsltFile();
		}

		/// <summary>
		/// Opens a XSLT file.
		/// </summary>
		public void OpenXsltFile()
		{
			dlgOpenFile.CheckFileExists = true;
			dlgOpenFile.InitialDirectory = Application.UserAppDataPath;
			dlgOpenFile.RestoreDirectory = true;
			dlgOpenFile.Filter = "XSLT Transform File (*.xslt)|*.xslt|All files (*.*)|*.*";
			dlgOpenFile.Title = "Open XSLT";

			if ( dlgOpenFile.ShowDialog() == DialogResult.OK )
			{
				Application.DoEvents();
				tempCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;

				try
				{
					Stream stream = dlgOpenFile.OpenFile();
					using ( StreamReader reader = new StreamReader(stream) )
					{
						this.XsltText = reader.ReadToEnd();
					}
				}
				catch ( Exception ex )
				{
					MessageBox.Show(ex.ToString(),"Ecyware GreenBlue Services", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			Cursor.Current = tempCursor;
		}

	}
}
