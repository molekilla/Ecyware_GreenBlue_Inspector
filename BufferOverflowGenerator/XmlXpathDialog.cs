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

namespace Ecyware.GreenBlue.Utils
{
	/// <summary>
	/// Summary description for XmlXpathDialog.
	/// </summary>
	public class XmlXpathDialog : System.Windows.Forms.Form
	{
		string _location;
		System.Xml.XmlNamespaceManager _namespaceCache;
		private string _xml;
		private string textValue = string.Empty;		
		string htmlSyntaxFile = string.Empty;
		Compona.SourceCode.Language language;
		private System.Windows.Forms.GroupBox grpHTTPCommands;
		private System.Windows.Forms.ComboBox cmbFilters;
		private System.Windows.Forms.Button cmdFilter;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		internal Compona.Windows.Forms.SyntaxBoxControl txtEditor;
		private Compona.SourceCode.SyntaxDocument syntaxDocument2;
		private System.Windows.Forms.Button btnSet;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.ComponentModel.IContainer components;

		public XmlXpathDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			htmlSyntaxFile = Application.StartupPath + @"\XML.syn";
			language = Compona.SourceCode.Language.FromSyntaxFile(htmlSyntaxFile);
			syntaxDocument2.Parser.Init(language);

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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(XmlXpathDialog));
			this.grpHTTPCommands = new System.Windows.Forms.GroupBox();
			this.btnSet = new System.Windows.Forms.Button();
			this.cmbFilters = new System.Windows.Forms.ComboBox();
			this.cmdFilter = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtEditor = new Compona.Windows.Forms.SyntaxBoxControl();
			this.syntaxDocument2 = new Compona.SourceCode.SyntaxDocument(this.components);
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
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
			this.grpHTTPCommands.Text = "XPath Filter";
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
			this.groupBox1.Controls.Add(this.txtEditor);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 42);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(610, 332);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Xml";
			// 
			// txtEditor
			// 
			this.txtEditor.ActiveView = Compona.Windows.Forms.SyntaxBox.ActiveView.BottomRight;
			this.txtEditor.AutoListPosition = null;
			this.txtEditor.AutoListSelectedText = "a123";
			this.txtEditor.AutoListVisible = false;
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
			this.txtEditor.Size = new System.Drawing.Size(604, 313);
			this.txtEditor.SmoothScroll = false;
			this.txtEditor.SplitviewH = -4;
			this.txtEditor.SplitviewV = -4;
			this.txtEditor.TabGuideColor = System.Drawing.Color.FromArgb(((System.Byte)(228)), ((System.Byte)(231)), ((System.Byte)(235)));
			this.txtEditor.TabIndex = 12;
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
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2});
			this.menuItem1.Text = "&File";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "&Close";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// XmlXpathDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(610, 374);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.grpHTTPCommands);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu1;
			this.Name = "XmlXpathDialog";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "XPath Query Dialog";
			this.grpHTTPCommands.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Query Methods
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
				EditorText = GetXmlString(html,cmbFilters.Text);			
			}
			catch (Exception ex)
			{
				EditorText = ex.ToString();
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
					return GetXmlString(this.EditorText,"/");
				} 
				else 
				{
					return GetXmlString(this.EditorText,cmbFilters.Text);
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
				if ( EditorText.Length > 0 )
				{
					ExecuteXPathFilter(EditorText);
				} 
				else 
				{
					ExecuteXPathFilter(XmlString);
				}
			}			
		}

//		/// <summary>
//		/// Queries the current html.
//		/// </summary>
//		/// <returns> Returns a XmlNodeList with the result.</returns>
//		public XmlNodeList GetXmlNodes()
//		{
//			try
//			{
//				return GetNodes(this.EditorText, this.cmbFilters.Text);
//			}
//			catch
//			{
//				throw;
//			}
//		}

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
				//syntaxDocument2.clear();

				// save value in temp
				syntaxDocument2.Text = value;			
				txtEditor.Document = syntaxDocument2;
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

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
	}
}
