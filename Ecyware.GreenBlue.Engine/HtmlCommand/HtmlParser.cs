// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using Sgml;
using System.Collections;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using Ecyware.GreenBlue.Engine.HtmlDom;


namespace Ecyware.GreenBlue.Engine.HtmlCommand
{	
	/// <summary>
	/// Contains logic for parsing HTML.
	/// </summary>
	public class HtmlParser
	{
		private delegate HtmlFormTagCollection LoadFormDelegate(string data);

		HtmlParserProperties _properties = new HtmlParserProperties();
		private XPathDocument _cache = null;
		private XmlNamespaceManager _namespaceCache = null;

		// begin / end Async Call
		private LoadFormDelegate _loadForm = null;

		// regex cache
		Hashtable regex = new Hashtable();

		#region Constructors
		/// <summary>
		/// Creates a new HtmlParser object.
		/// </summary>
		public HtmlParser()
		{
			// cache regex
			// Remove Scripts regex
			RegexOptions options = RegexOptions.None;
			Regex removeScripts = new Regex(@"(?<header><(?i:script)[^>]*?)(/>|>(?<source>[\w|\t|\r|\W]*?)</(?i:script)>)",options);
			Regex removeStyles = new Regex(@"<(?i:style)[^>]*?(/>|>[\w|\t|\r|\W]*?</(?i:style)>)",options);
			Regex getMetaTag = new Regex(@"(?i:<meta).+>", options);
			Regex getAttributes = new Regex(@"(?<name>(\w+))=(""|')(?<value>.*?)(""|')", options);

			regex.Add("RemoveScripts",removeScripts);
			regex.Add("RemoveStyles",removeStyles);
			regex.Add("GetMetaTag", getMetaTag);
			regex.Add("GetAttributes", getAttributes);

			_loadForm = new LoadFormDelegate(LoadForm);
		}

		/// <summary>
		/// Creates a new HtmlParser object.
		/// </summary>
		/// <param name="properties"> The Html Parser properties to apply.</param>
		public HtmlParser(HtmlParserProperties properties) : this()
		{
			this.ParserProperties=properties;
		}

		/// <summary>
		/// Creates a new HtmlParser object.
		/// </summary>
		/// <param name="removeScripts"> Removes SCRIPT tags.</param>
		/// <param name="removeStyle"> Removes STYLE tags.</param>
		public HtmlParser(bool removeScripts, bool removeStyle)  : this()
		{
			this.ParserProperties.RemoveScriptTags=removeScripts;
			this.ParserProperties.RemoveStyleTags=removeStyle;
		}
		#endregion
		#region Properties

		/// <summary>
		/// Gets or sets the regular expressions to parse scripts.
		/// </summary>
		internal Hashtable GetRegExpParserScripts
		{
			get
			{
				return regex;
			}
		}

		/// <summary>
		/// Gets or sets the NamespaceCache.
		/// </summary>
		private XmlNamespaceManager NamespaceCache
		{
			get
			{
				return _namespaceCache;
			}
			set
			{
				_namespaceCache = value;
			}
		}

		/// <summary>
		/// Gets or sets the DocumentCache.
		/// </summary>
		public XPathDocument DocumentCache
		{
			get
			{
				return _cache;
			}
			set
			{
				_cache = value;
			}
		}

		/// <summary>
		/// Gets or sets the parser properties.
		/// </summary>
		public HtmlParserProperties ParserProperties
		{
			get
			{
				return _properties;
			}
			set
			{
				_properties = value;
			}
		}
		#endregion
		#region Parsing methods
		/// <summary>
		/// Parse a HTML to XML and returns a string, if error occurs returns an exception.
		/// </summary>
		/// <remarks> Use this method when you want to catch a parsing error.</remarks>
		/// <param name="html"> HTML string to parse.</param>
		/// <returns>A string with the parsed value.</returns>	
		public string GetParsableString(string html)
		{
			html = PreProcessHtml(html);
			SgmlReader reader = new SgmlReader();

			// set SgmlReader values
			reader.DocType = "HTML";
			
			// lower case all
			reader.InputStream = new StringReader(html);

			// write to xml
			StringWriter sw = new StringWriter();
			XmlTextWriter w = new XmlTextWriter(sw);

			w.Formatting = Formatting.Indented;

			try
			{
				while (reader.Read()) 
				{
					if ( (reader.NodeType != XmlNodeType.DocumentType) && (this.ParserProperties.RemoveDocumentType) )
					{
						if ( reader.NodeType != XmlNodeType.Whitespace )
						{
							// Write entire reader to xml
							w.WriteNode(reader, true);
						}
					}					
				}

				return PostProcessHtml(sw.ToString());
			}
			catch
			{
				throw;
			}
			finally 
			{

				reader.Close();
				w.Close();
			}
			

		}


		/// <summary>
		/// Removes a tag using the starting tag and end tag.
		/// </summary>
		/// <param name="html"> The HTML source to parse.</param>
		/// <param name="tagName"> The tag name.</param>
		/// <param name="startTag"> The start tag.</param>
		/// <param name="endTag"> The end tag.</param>
		/// <returns> A string with the cleaned HTML.</returns>
		private string RemoveStartEndTags(string html,string tagName,string startTag,string endTag)
		{
			int startIndex=0;

			// be sure tagName is replace with a lower version

			while (startIndex>-1)
			{
				// clean startTag and endTag if inside the tag
				int i=html.IndexOf(tagName,startIndex);

				if ( i==-1 )
				{
					// exit
					return html;
				}

				int foundStart=-1;
				int foundEnd=-1;
				int j = (int)System.Math.Round(0.1*html.Length);
				bool eos=false;
				StringBuilder sb;

				while ( foundEnd==-1 )
				{	
					// verify end of string
					if ( ((i+j) > html.Length) || ( (foundStart+j)>html.Length ) )	
					{
						eos=true;
						break;
					}
					if ( foundStart==-1 )
					{
						foundStart=html.IndexOf(startTag.ToLower(),i,j);
					} 
					else 
					{
						foundEnd=html.IndexOf(endTag.ToLower(),foundStart,j);
					}
					j=j+j;
					if ( foundEnd!=-1 )
					{
						break;
					}
				}
			
				if ( !eos )
				{
					// remove comments
					sb=new StringBuilder(html);
					sb.Remove(foundEnd,endTag.Length);
					sb.Remove(foundStart,startTag.Length);
				} else {
					return html;
				}

				html=sb.ToString();
				startIndex=i+1;
			}

			return html;

		}


		/// <summary>
		/// Removes any tabs
		/// </summary>
		/// <param name="html">HTML string to parse.</param>
		/// <returns>A string.</returns>
		private string RemoveTabs(string html)
		{
			//html = html.ToLower();
			html = html.Replace("\t","");
			return html;
		}

		/// <summary>
		/// Post process the html.
		/// </summary>
		/// <param name="html"> The HTML source to parse.</param>
		/// <returns> A string with the cleaned HTML.</returns>
		private string PostProcessHtml(string html)
		{
			html = PostNormalizeNamespaces(html);
			html=html.Replace("selected=\"\"","selected=\"true\"");
			html=html.Replace("multiple=\"\"","multiple=\"true\"");
			/*
			RegexOptions options = RegexOptions.None;
			Regex regex = new Regex(@"(?s:",)", options);
			string result = regex.Replace(html, @"""");
			*/
			return html;
		}

		/// <summary>
		/// Pre parse any existing namespaces found in the HTML.
		/// </summary>
		/// <param name="html"> The HTML source to parse.</param>
		/// <returns> A string.</returns>
		private string PreNormalizeNamespaces(string html)
		{
			int i = 0;
			i=html.IndexOf("xmlns=",i);
			
			if ( i == -1 )
			{
				return html;
			}
			
			// normalize
			StringBuilder sb = new StringBuilder(html);
			string newValue = "xmlns:default=";
			sb.Replace("xmlns=",newValue,i,newValue.Length);
			html = sb.ToString();

			return html;
		}

		/// <summary>
		/// Post parse any existing namespaces found in the HTML.
		/// </summary>
		/// <param name="html"> The HTML source to parse.</param>
		/// <returns> A string.</returns>
		private string PostNormalizeNamespaces(string html)
		{
			int i = 0;
			i=html.IndexOf("xmlns:",i);
			
			if ( i == -1 )
			{
				return html;
			}
			
			while (i>-1)
			{
				int namespaceFound = html.IndexOf("=\"\"",i);

				// heuristic
				if ( (namespaceFound - i) > 50 )
				{
					i=-1;
					break;
				}
				if ( namespaceFound != -1 )
				{
					// normalize
					StringBuilder sb = new StringBuilder(html);
					string newValue = "=\"urn:tempuri.org\"";
					sb.Replace("=\"\"",newValue,namespaceFound,newValue.Length);
					html = sb.ToString();
				}				
				i = namespaceFound;
			}

			return html;
		}


		/// <summary>
		/// Pre parses the HTML.
		/// </summary>
		/// <param name="html"> The HTML source to parse.</param>
		/// <returns> A string.</returns>
		private string PreProcessHtml(string html)
		{
			// NOTE: Removed in final version
			// HtmlTidyWrapper tidy = new HtmlTidyWrapper();
			// html = tidy.CorrectHtmlString(html);

			html = RemoveTabs(html);

			html = PreNormalizeNamespaces(html);

			if ( this.ParserProperties.RemoveScriptTags )
			{
				Regex r = (Regex)regex["RemoveScripts"];
				html = r.Replace(html,@"");
			}

			if ( this.ParserProperties.RemoveStyleTags )
			{
				Regex r = (Regex)regex["RemoveStyles"];
				html = r.Replace(html,@"");
			}

			html = RemoveStartEndTags(html,"<style","<!--","-->");
			html = RemoveStartEndTags(html,"<script","<!--","-->");
			html = RemoveStartEndTags(html,"<script","<![CDATA[","]]>");

			return html;
		}
#endregion
		#region Form Validation and Form HTML Parsing Helpers
		/// <summary>
		/// Validates that the data is a valid XPathDocument.
		/// </summary>
		/// <remarks> CheckDocument tries to parse HTML code. In case an exception is found related to namespace resolving, it will try to add them to the HTML.
		/// If the namespace resolving cannot be handle, it throws the exception.</remarks>
		/// <param name="sr"> The HTML source to validate.</param>
		/// <returns> An XPathDocument of the html source.</returns>
		private XPathDocument CheckDocument(string sr)
		{
			bool done=false;
			string temp = sr;
			XPathDocument doc=null;

			while (!done)
			{				
				try
				{
					// load XPathDocument and create navigator
					doc = new XPathDocument(new StringReader(temp));
					done = true;
				}
				catch (XmlException xmlEx)
				{
					if ( xmlEx.Message.IndexOf("namespace") > -1 )
					{
						if ( xmlEx.Message.IndexOf("processing instructions") > -1)
						{
							throw xmlEx;
						}

						// get namespaces
						Match m = Regex.Match(xmlEx.Message,"'(?<namespace>.*?)'");

						// get html header
						Match mm = Regex.Match(temp,"<(?i:html)>?");
						temp = temp.Insert(mm.Index+mm.Length-1," xmlns:" +m.Groups["namespace"].Value + "=\"" + "urn:temp-" + m.Groups["namespace"].Value + "\"");

					} else {

						throw xmlEx;

					}
		
				}
			}

			return doc;
		}
		/// <summary>
		/// Verifies that HTML Document has forms.
		/// </summary>
		/// <param name="html"> The parsed HTML string.</param>
		/// <returns> Returns true if HTML Document contains forms, else false.</returns>
		private bool HasForms(string parsedHtml)
		{
			bool returnValue = false;
			StringReader sr = new StringReader(parsedHtml);

			try
			{
				Hashtable values = ResolveNamespaces(sr);

				XPathDocument doc = CheckDocument(parsedHtml);

				this.DocumentCache = doc;
				XPathNavigator nav = doc.CreateNavigator();				

				// Add namespaces
				XmlNamespaceManager xnm = new XmlNamespaceManager(nav.NameTable);
				
				foreach (DictionaryEntry de in values)
				{
					xnm.AddNamespace((string)de.Key,(string)de.Value);
				}

				this.NamespaceCache = xnm;

				XPathExpression expr = nav.Compile("//form");
				expr.SetContext(xnm);

				// select all form elements
				XPathNodeIterator nodes = nav.Select(expr);

				if ( nodes.Count>0 )
				{
					returnValue = true;

				} else {
					// try regex
				}
			}
			catch
			{
				// System.Diagnostics.Debug.Write(parsedHtml);
				throw;
			}
			finally
			{
				if ( sr != null )
				{
					sr.Close();
				}
			}

			return returnValue;
		}


		/// <summary>
		/// Gets the META tag redirect url if any.
		/// </summary>
		/// <param name="htmlContent"> The HTML content to parse.</param>
		/// <returns> Returns empty if no meta tag url found, else a url segment.</returns>
		public string GetMetaRedirectUrlString(string htmlContent)
		{
			string url = String.Empty;

			Regex metaTagResolver = (Regex)regex["GetMetaTag"];
			Regex getAttributes = (Regex)regex["GetAttributes"];

			// Get matches
			MatchCollection matches = metaTagResolver.Matches(htmlContent);
			
			// for each meta
			for( int i=0;i<matches.Count;i++ )
			{
				string meta = matches[i].Value;

				#region Search url in attributes
				// get attributes
				MatchCollection attributes = getAttributes.Matches(meta);

				foreach (Match m in attributes)
				{
					string name = m.Groups["name"].Value;
					string result = m.Groups["value"].Value;

					if ( name.ToLower() == "content" )
					{
						if ( ( result.ToLower().IndexOf("url") > -1 ) && (result.ToLower().IndexOf(";") > -1 ) )
						{
							// split with ;
							string[] content = result.Split(';');

							// get url
							url = (content[1].Split('='))[1];
							break;
						}
					}
				}
				#endregion
			}

			return url;
		}


		/// <summary>
		/// Get the element value by type and name attribute value.
		/// </summary>
		/// <param name="elementType"> The element type.</param>
		/// <param name="name"> The name value.</param>
		/// <param name="htmlContent"> The html content to parse.</param>
		/// <returns> A value from a Form Element.</returns>
		public string GetFormElementValue(string elementType, string name, string htmlContent)
		{
			string elementValue = string.Empty;

			StringBuilder rex = new StringBuilder();
			rex.Append(@"(?<header><(?i:");
			rex.Append(elementType.ToLower());
			rex.Append(@")[^>]*?)(>|>(?<source>[\w|\t|\r|\W]*?)</(?i:");
			rex.Append(elementType.ToLower());
			rex.Append(@")>)");

			Regex getElements = new Regex(rex.ToString(), RegexOptions.None);
			Regex getAttributes = (Regex)regex["GetAttributes"];

			// Get elements matches
			MatchCollection matches = getElements.Matches(htmlContent);
			
			// for each meta
			for( int i=0;i<matches.Count;i++ )
			{
				string element = matches[i].Value;

				#region Search for element with name
				// get attributes
				MatchCollection attributes = getAttributes.Matches(element);

				foreach (Match m in attributes)
				{
					// string name = m.Groups["name"].Value;
					string attributeName = m.Groups["value"].Value;

					// if match, then get value
					if ( attributeName == name )
					{
						// element found, get value now
						foreach (Match mm in attributes)
						{
							attributeName = mm.Groups["name"].Value;
							string result = mm.Groups["value"].Value;

							// if match, then get value
							if ( attributeName.ToLower() == "value" )
							{
								// value found
								elementValue = result;
								break;
							}
						}
						break;
					}
				}
				#endregion
			}

			return elementValue;
		}
		
		
		/// <summary>
		/// Gets the ACTION url by absolute url.
		/// </summary>
		/// <param name="url"> The absolute url.</param>
		/// <param name="htmlContent"> The parsed HTML content.</param>
		/// <returns> A string with the form action.</returns>
		public string GetFormActionByAbsoluteUrl(string url, string htmlContent)
		{
			string result = string.Empty;

			int startAction = htmlContent.ToLower().IndexOf(url);			

			if ( startAction > -1 )
			{
				int endTag = htmlContent.IndexOf("\"", startAction);
				result = htmlContent.Substring(startAction,(endTag-startAction));
			}

			return result;
		}
		#endregion
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
				reader.Normalization = true;
				while ( reader.Read() )
				{
					//System.Windows.Forms.Application.DoEvents();

					if ( reader.NodeType == XmlNodeType.Element )
					{
						while ( reader.MoveToNextAttribute() )
						{
							if ( (reader.Name.ToLower(System.Globalization.CultureInfo.InvariantCulture).StartsWith("xmlns:")) || (reader.Name.ToLower().StartsWith("xml:")) )
							{
								Char[] ch = new Char[] {':'};
								string prefix = reader.Name.Split(ch)[1];
								namespaces.Add(prefix,reader.Value);
							}
							if ( reader.Name.ToLower(System.Globalization.CultureInfo.InvariantCulture) == "xmlns" )
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

		/// <summary>
		/// Resolves the namespaces for a XmlDocument.
		/// </summary>
		/// <param name="reader"> A XmlTextReader containing the xml data.</param>
		/// <param name="namespaceManager"> A XmlNamespaceManager from an existing document.</param>
		/// <remarks> To create a XmlNamespaceManager for a document, use XmlNamespaceManager  nsMgr = new XmlNamespaceManager(document.NameTable).</remarks>
		/// <returns> An updated XmlNamespaceManager</returns>
		public static XmlNamespaceManager ResolveNamespaces(XmlTextReader reader, XmlNamespaceManager namespaceManager)
		{
			// resolve namespaces before loading document
			Hashtable values = ResolveNamespaces(reader);

			foreach ( DictionaryEntry de in values )
			{
				namespaceManager.AddNamespace((string)de.Key,(string)de.Value);
			}

			return namespaceManager;
		}
		#endregion
		#region Form generation

		/// <summary>
		/// Begins the loading of forms into a HtmlFormTagCollection.
		/// </summary>
		/// <param name="html"> The parsed HTML content.</param>
		/// <param name="callback"> The AsyncCallback delegate.</param>
		/// <param name="state"> The callback state.</param>
		/// <returns> An IAsyncResult.</returns>
		public IAsyncResult BeginLoadForm(string html,AsyncCallback callback,object state)
		{
			return _loadForm.BeginInvoke(html,callback,state);
		}

		/// <summary>
		/// Callback method for BeginLoadForm.
		/// </summary>
		/// <param name="asyncResult"> The IAsyncResult.</param>
		/// <returns> A HtmlFormTagCollection</returns>
		public HtmlFormTagCollection EndLoadFrom(IAsyncResult asyncResult)
		{
			return _loadForm.EndInvoke(asyncResult);
		}
		/// <summary>
		/// Loads the forms into a HtmlFormTagCollection.
		/// </summary>
		/// <param name="html"> The parsed HTML content.</param>
		/// <returns> Returns a HtmlFormTagCollection with the forms contained in the HTML.</returns>
		public HtmlFormTagCollection LoadForm(string html)
		{
			HtmlFormTagCollection forms;
			try
			{
				forms = new HtmlFormTagCollection();
				XPathDocument doc;

				if ( HasForms(html) )
				{
					doc = this.DocumentCache;

					XPathNavigator nav = doc.CreateNavigator();
					#region "Set namespaces"
					if ( this.NamespaceCache == null)
					{
						//create prefix<->namespace mappings (if any) 
						XmlNamespaceManager nsMgr = new XmlNamespaceManager(nav.NameTable);

						// resolve namespaces before loading document
						Hashtable values = ResolveNamespaces(new XmlTextReader(new StringReader(html)));

						foreach (DictionaryEntry de in values)
						{
							nsMgr.AddNamespace((string)de.Key,(string)de.Value);
						}

						this.NamespaceCache = nsMgr;
					}
					#endregion
					int i=0;
					XPathExpression expr = nav.Compile("//form");
					expr.SetContext(this.NamespaceCache);
					// select all form elements
					XPathNodeIterator nodes = nav.Select(expr);

					#region Build Form Tag
					while (nodes.MoveNext()) 
					{
						HtmlFormTag f = new HtmlFormTag();

						f.FormIndex = i;
						f.Id=nodes.Current.GetAttribute("id",nodes.Current.NamespaceURI);
						f.Style=nodes.Current.GetAttribute("style",nodes.Current.NamespaceURI);
						f.Enctype=nodes.Current.GetAttribute("enctype",nodes.Current.NamespaceURI);
						f.Class=nodes.Current.GetAttribute("class",nodes.Current.NamespaceURI);
						f.Name=nodes.Current.GetAttribute("name",nodes.Current.NamespaceURI);
						f.Action=nodes.Current.GetAttribute("action",nodes.Current.NamespaceURI);
						f.Method=nodes.Current.GetAttribute("method",nodes.Current.NamespaceURI);
						f.Id=nodes.Current.GetAttribute("id",nodes.Current.NamespaceURI);
						f.OnSubmit=nodes.Current.GetAttribute("onsubmit",nodes.Current.NamespaceURI);

						if (f.Action.Length == 0 )
						{
							// add dummy action
							f.Action = "dummyAction";
						}

						if ( f.Method.Length==0 )
						{
							f.Method="get";
						}

						if ( f.Id!=String.Empty )
						{
							f.Name = f.Id;
						}
						if ( forms.ContainsKey(f.Name) )
						{
							forms.Add("_" + f.Name,f);
						} 
						else 
						{
							if ( f.Name!=String.Empty )
							{
								forms.Add(f.Name,f);
							}
							if (f.Id==String.Empty && f.Name == String.Empty )
							{
								f.Id = "Form " + i;
								f.Name = "Form " + i;
								forms.Add(f.Name,f);
							}
						}

						#region " Loop thru descendants from Form"
						// Select descendants, childs
						XPathNodeIterator items = nodes.Current.SelectDescendants(XPathNodeType.Element,true);
					
						int autoId = 0;

						while ( items.MoveNext() )
						{
						
							// if exists, add to same HtmlTagBaseList type
							// else create new
							string name = items.Current.GetAttribute("name",items.Current.NamespaceURI);
							string id = items.Current.GetAttribute("id",items.Current.NamespaceURI);
							string onclick = items.Current.GetAttribute("onclick",items.Current.NamespaceURI);

							if ( onclick == String.Empty )
							{
								onclick = items.Current.GetAttribute("onClick",items.Current.NamespaceURI);
							}

							// prioritize name use
							// else use id
							if ( name == String.Empty )
							{
								name = id;
								// if no id, generate one
								if ( name == String.Empty )
								{
									id = autoId.ToString();
									name = id;
								}
							}
				
							switch ( items.Current.Name )
							{							
								case "div":
									if ( onclick != String.Empty )
									{
										AddCommonTag(f,onclick,id,name);
									}
									break;
								case "span":
									if ( onclick != String.Empty )
									{
										AddCommonTag(f,onclick,id,name);
									}
									break;
								case "a":
									HtmlALinkTag a = CreateLinkTag(items.Current);
									if ( f.ContainsKey(name) )
									{
										// just add the value
										HtmlTagBaseList array = ((HtmlTagBaseList)f[name]);
										array.Add(a);
									} 
									else 
									{
										HtmlTagBaseList list = new HtmlTagBaseList();
										list.Add(a);
										f.Add("a" + autoId.ToString(),list);
									}
									break;
								case "input":
									// if exists, add to same HtmlTagBaseList type
									// else create new
									// verify by name and type
									HtmlInputTag input = FillInputTag(items.Current);
									input.Name = name;

									if ( f.ContainsKey(name) )
									{
										// just add the value
										HtmlTagBaseList array = ((HtmlTagBaseList)f[name]);
										array.Add(input);
									} 
									else 
									{
										HtmlTagBaseList list = new HtmlTagBaseList();
										//HtmlInputTag input = FillInputTag(items.Current);
										list.Add(input);
										f.Add(input.Name,list);
									}
									break;
								case "button":
									// if exists, add to same HtmlTagBaseList type
									// else create new
									// verify by name
									HtmlButtonTag button = FillButtonTag(items.Current);
									button.Name = name;

									if ( f.ContainsKey(name) )
									{
										// just add the value
										HtmlTagBaseList array = ((HtmlTagBaseList)f[name]);
										//HtmlButtonTag button = FillButtonTag(items.Current);
										array.Add(button);
									} 
									else 
									{
										HtmlTagBaseList buttonList = new HtmlTagBaseList();
										buttonList.Add(button);
										f.Add(button.Name,buttonList);
									}
								
									break;
								case "select":
									// if exists, add to same HtmlTagBaseList type
									// else create new
									// verify by name
									HtmlSelectTag select = CreateSelectTag(items.Current);
									select.Name = name;

									if ( f.ContainsKey(name) )
									{
										HtmlTagBaseList array = ((HtmlTagBaseList)f[name]);									
										array.Add(select);
									} 
									else 
									{
										HtmlTagBaseList selectList = new HtmlTagBaseList();
										//HtmlSelectTag select = FillSelectTag(items.Current);
										selectList.Add(select);
										f.Add(select.Name,selectList);
									}
									break;
								case "textarea":
									// if exists, add to same HtmlTagBaseList type
									// else create new
									// verify by name
									HtmlTextAreaTag textarea = FillTextAreaTag(items.Current);
									textarea.Name = name;

									if ( f.ContainsKey(name) )
									{
										HtmlTagBaseList array = ((HtmlTagBaseList)f[name]);
										//HtmlTextAreaTag textarea = FillTextAreaTag(items.Current);
										array.Add(textarea);
									} 
									else 
									{
										HtmlTagBaseList textAreaList = new HtmlTagBaseList();
										textAreaList.Add(textarea);
										f.Add(textarea.Name,textAreaList);
									}
									break;
							}

							// increase
							autoId++;
						}
						i++;
						#endregion
					}
					#endregion
				}
			}
			catch
			{
				throw;
			}

			return forms;

		}

		/// <summary>
		/// Adds a common tag.
		/// </summary>
		/// <param name="form"> The HtmlFormTag.</param>
		/// <param name="click"> The click event source code.</param>
		/// <param name="id"> The element id.</param>
		/// <param name="name"> The element name.</param>
		private void AddCommonTag(HtmlFormTag form,string click,string id, string name)
		{
			//add
			HtmlTagBase tag = new HtmlTagBase();
			tag.Id = id;
			tag.OnClick = click;

			if ( form.ContainsKey(name) )
			{
				// just add the value
				HtmlTagBaseList array = ((HtmlTagBaseList)form[name]);										
				array.Add(tag);
			} 
			else 
			{
				HtmlTagBaseList list = new HtmlTagBaseList();
				list.Add(tag);
				form.Add(tag.Id,list);
			}
		}


		/// <summary>
		/// Creates a link tag.
		/// </summary>
		/// <param name="currentNode"> The XPathNavigator node.</param>
		/// <returns> A HtmlALinkTag.</returns>
		private HtmlALinkTag CreateLinkTag(XPathNavigator currentNode)
		{
			HtmlALinkTag tag = new HtmlALinkTag();

			tag.Class=currentNode.GetAttribute("class",currentNode.NamespaceURI);
			tag.Id=currentNode.GetAttribute("id",currentNode.NamespaceURI);
			tag.OnClick=currentNode.GetAttribute("onclick",currentNode.NamespaceURI);
			tag.Style=currentNode.GetAttribute("style",currentNode.NamespaceURI);
			tag.Title=currentNode.GetAttribute("title",currentNode.NamespaceURI);
			tag.HRef=currentNode.GetAttribute("href",currentNode.NamespaceURI);

			if ( tag.OnClick.Length == 0 )
			{
				tag.OnClick = currentNode.GetAttribute("onClick",currentNode.NamespaceURI);
			}

			return tag;
		}

		/// <summary>
		/// Creates a select tag.
		/// </summary>
		/// <param name="currentNode"> The XPathNavigator node.</param>
		/// <returns> A HtmlSelectTag.</returns>
		private HtmlSelectTag CreateSelectTag(XPathNavigator currentNode)
		{
			HtmlSelectTag tag = new HtmlSelectTag();

			tag.Class=currentNode.GetAttribute("class",currentNode.NamespaceURI);
			tag.Id=currentNode.GetAttribute("id",currentNode.NamespaceURI);
			tag.Name=currentNode.GetAttribute("name",currentNode.NamespaceURI);
			tag.OnClick=currentNode.GetAttribute("onclick",currentNode.NamespaceURI);
			tag.Style=currentNode.GetAttribute("style",currentNode.NamespaceURI);
			tag.Title=currentNode.GetAttribute("title",currentNode.NamespaceURI);

			if ( tag.OnClick.Length == 0 )
			{
				tag.OnClick = currentNode.GetAttribute("onClick",currentNode.NamespaceURI);
			}

			if ( currentNode.GetAttribute("multiple",currentNode.NamespaceURI)=="true" )
			{
				tag.Multiple=true;
			}
			
			tag.Value=currentNode.GetAttribute("value",currentNode.NamespaceURI);

			// fill options nodes
			XPathNodeIterator options = currentNode.SelectChildren(XPathNodeType.Element);

			//tag.Options = new HtmlOptionCollection();

			int i=1;
			while ( options.MoveNext() )
			{
				HtmlOptionTag option = new HtmlOptionTag();
				option.Id=options.Current.GetAttribute("id",options.Current.NamespaceURI);
				if ( options.Current.GetAttribute("selected",options.Current.NamespaceURI)=="true" )
				{
					option.Selected=true;
				}
				option.Text=options.Current.Value;
				option.Value=options.Current.GetAttribute("value",options.Current.NamespaceURI);

				option.Key = "Option " + i;
				tag.AddOptionTag(option);
				i++;
			}

			return tag;

		}

		/// <summary>
		/// Creates a textarea tag.
		/// </summary>
		/// <param name="currentNode"> The XPathNavigator node.</param>
		/// <returns> A HtmlTextAreaTag.</returns>
		private HtmlTextAreaTag FillTextAreaTag(XPathNavigator currentNode)
		{
			HtmlTextAreaTag tag = new HtmlTextAreaTag();

			tag.Class=currentNode.GetAttribute("class",currentNode.NamespaceURI);
			tag.Id=currentNode.GetAttribute("id",currentNode.NamespaceURI);
			tag.Name=currentNode.GetAttribute("name",currentNode.NamespaceURI);
			tag.OnClick=currentNode.GetAttribute("onclick",currentNode.NamespaceURI);
			tag.Style=currentNode.GetAttribute("style",currentNode.NamespaceURI);
			tag.Title=currentNode.GetAttribute("title",currentNode.NamespaceURI);
			tag.Value=currentNode.Value;

			if ( tag.OnClick.Length == 0 )
			{
				tag.OnClick = currentNode.GetAttribute("onClick",currentNode.NamespaceURI);
			}

			return tag;

		}

		/// <summary>
		/// Creates a button tag.
		/// </summary>
		/// <param name="currentNode"> The XPathNavigator node.</param>
		/// <returns> A HtmlButtonTag.</returns>
		private HtmlButtonTag FillButtonTag(XPathNavigator currentNode)
		{
			HtmlButtonTag tag = new HtmlButtonTag();

			tag.Class=currentNode.GetAttribute("class",currentNode.NamespaceURI);
			tag.Id=currentNode.GetAttribute("id",currentNode.NamespaceURI);
			tag.Name=currentNode.GetAttribute("name",currentNode.NamespaceURI);
			tag.OnClick=currentNode.GetAttribute("onclick",currentNode.NamespaceURI);
			tag.Style=currentNode.GetAttribute("style",currentNode.NamespaceURI);
			tag.Title=currentNode.GetAttribute("title",currentNode.NamespaceURI);

			if ( tag.OnClick.Length == 0 )
			{
				tag.OnClick = currentNode.GetAttribute("onClick",currentNode.NamespaceURI);
			}

			switch (currentNode.GetAttribute("type",currentNode.NamespaceURI))
			{
				case "button":
					tag.Type=HtmlButtonType.Button;
					break;
				case "reset":
					tag.Type=HtmlButtonType.Reset;
					break;
				case "submit":
					tag.Type=HtmlButtonType.Submit;
					break;
			}
			tag.Value = currentNode.GetAttribute("value",currentNode.NamespaceURI);

			return tag;

		}


		/// <summary>
		/// Creates a input tag.
		/// </summary>
		/// <param name="currentNode"> The XPathNavigator node.</param>
		/// <returns> A HtmlInputTag.</returns>
		private HtmlInputTag FillInputTag(XPathNavigator currentNode)
		{
			HtmlInputTag input = new HtmlInputTag();

			input.Checked=currentNode.GetAttribute("checked",currentNode.NamespaceURI);
			input.Class=currentNode.GetAttribute("class",currentNode.NamespaceURI);
			input.Id=currentNode.GetAttribute("id",currentNode.NamespaceURI);
			input.MaxLength=currentNode.GetAttribute("maxlength",currentNode.NamespaceURI);
			input.Name=currentNode.GetAttribute("name",currentNode.NamespaceURI);
			input.OnClick=currentNode.GetAttribute("onclick",currentNode.NamespaceURI);
			input.ReadOnly=currentNode.GetAttribute("readonly",currentNode.NamespaceURI);
			input.Style=currentNode.GetAttribute("style",currentNode.NamespaceURI);
			input.Title=currentNode.GetAttribute("title",currentNode.NamespaceURI);
			input.Value=currentNode.GetAttribute("value",currentNode.NamespaceURI);

			if ( input.OnClick.Length == 0 )
			{
				input.OnClick = currentNode.GetAttribute("onClick",currentNode.NamespaceURI);
			}

			switch (currentNode.GetAttribute("type",currentNode.NamespaceURI))
			{
				case "button":
					input.Type=HtmlInputType.Button;
					break;
				case "checkbox":
					input.Type=HtmlInputType.Checkbox;
					break;
				case "file":
					input.Type=HtmlInputType.File;
					break;
				case "hidden":
					input.Type=HtmlInputType.Hidden;
					break;
				case "image":
					input.Type=HtmlInputType.Image;
					break;
				case "password":
					input.Type=HtmlInputType.Password;
					break;
				case "radio":
					input.Type=HtmlInputType.Radio;
					break;
				case "reset":
					input.Type=HtmlInputType.Reset;
					break;
				case "submit":
					input.Type=HtmlInputType.Submit;
					break;
				case "text":
					input.Type=HtmlInputType.Text;
					break;
				default:
					input.Type=HtmlInputType.Text;
					break;
			}

			return input;
		}

		#endregion
		#region Additional HtmlForm logic
		/// <summary>
		/// Process a HtmlFormTag radio fields.
		/// </summary>
		/// <param name="form"> The HtmlFormTag.</param>
		/// <returns> An updated HtmlFormTag.</returns>
		[Obsolete]
		private HtmlFormTag ProcessSpecialFields(HtmlFormTag form)
		{
			try
			{
				ArrayList al=new ArrayList();
				for (int j=0;j<form.Count;j++)
				{
					HtmlTagBaseList elementList = (HtmlTagBaseList)((DictionaryEntry)form[j]).Value;

					// foreach tag in elementList
					for (int i=0;i<elementList.Count;i++)
					{
						HtmlTagBase tagBase = elementList[i];

						#region Remove Node Heuristics
						if ( tagBase is HtmlInputTag)
						{
							HtmlInputTag b =(HtmlInputTag)tagBase;

							if ( b.Type == HtmlInputType.Radio )
							{
								if ( b.Value.Length == 0 )
								{
									// remove this one
									al.Add(tagBase);
								}
							}
						
							if ( b.Type == HtmlInputType.Checkbox )
							{
								if ( b.Value.Length==0 )
								{
									// remove this one
									al.Add(tagBase);
								}
							}
						}
						#endregion
					}
					// remove nodes
					for (int k=0;k<al.Count;k++)
					{
						elementList.Remove((HtmlTagBase)al[k]);
					}
				}

				return form;
			}
			catch
			{
				throw;
			}
		}
		#endregion
		#region Converter
		public string AddEndLine(string value)
		{
			if ( value == null )
			{
				value = string.Empty;
			} 
			else 
			{
				if ( value.Length > 1 )
				{
					string end = value.Substring(value.Length - 1,1);
					char lineEnd = Char.Parse(end);
					if ( lineEnd.CompareTo('\0') > 0 )
					{
						// Add
						value += "\0";
					}
				}
			}

			return value;
		}

		/// <summary>
		/// Converts the HtmlFormTag to string.
		/// </summary>
		/// <param name="form"> The html form tag.</param>
		/// <returns> A post data string.</returns>
		public string GetString(HtmlFormTag form)
		{
			ArrayList list = GetArrayList(form);
			StringBuilder buffer = new StringBuilder();	
			for (int i=0;i<list.Count;i++)
			{
				if (i>0)
				{
					buffer.Append("&");
				}
				buffer.Append(list[i]);
			}

			return buffer.ToString();
		}

		/// <summary>
		/// Converts the HtmlFormTag to a ArrayList.
		/// </summary>
		/// <param name="form"> HtmlFormTag to convert.</param>
		/// <returns> An ArrayList type.</returns>
		public ArrayList GetArrayList(HtmlFormTag form)
		{
			try
			{
				ArrayList al = new ArrayList();
				for (int j=0;j<form.Count;j++)
				{
					HtmlTagBaseList controlArray = (HtmlTagBaseList)((DictionaryEntry)form[j]).Value;

					bool last = false;
					if ( j == (form.Count - 1) )
						last = true;

					string s = string.Empty;
					for ( int i=0;i<controlArray.Count;i++ )
					{
						bool addEndLine = false;
						if ( i == (controlArray.Count - 1) && last )							
						{
							addEndLine = true;
						}
						HtmlTagBase tag = controlArray[i];

						if ( tag is HtmlButtonTag )
							AddButtonPostData((HtmlButtonTag)tag, al, addEndLine);

						if ( tag is HtmlInputTag)
							AddInputPostData((HtmlInputTag)tag, al, addEndLine);

						if ( tag is HtmlTextAreaTag )
							AddTextAreaPostData((HtmlTextAreaTag)tag,al, addEndLine);
						
						if ( tag is HtmlSelectTag )
							AddSelectPostData((HtmlSelectTag)tag,al, addEndLine);						
					}
				}

				return al;
			}
			catch
			{
				throw;
			}
		}


		/// <summary>
		/// Converts the HtmlFormTag to an ArrayList and checks for updated values in HTML content.
		/// </summary>
		/// <param name="form"> HtmlFormTag to convert.</param>
		/// <returns> A Hashtable type.</returns>
		public ArrayList GetArrayList(HtmlFormTag form, string html)
		{
			try
			{
				ArrayList al = new ArrayList();
				for (int j=0;j<form.Count;j++)
				{
					HtmlTagBaseList controlArray = (HtmlTagBaseList)((DictionaryEntry)form[j]).Value;

					bool last = false;
					if ( j == (form.Count - 1) )
						last = true;

					string s = string.Empty;
					for ( int i=0;i<controlArray.Count;i++ )
					{
						bool addEndLine = false;
						if ( i == (controlArray.Count - 1) && last )							
						{
							addEndLine = true;
						}

						HtmlTagBase tag = controlArray[i];

						if ( tag is HtmlButtonTag )
							AddButtonPostData((HtmlButtonTag)tag, al, addEndLine);

						if ( tag is HtmlInputTag)
						{
							HtmlInputTag input = (HtmlInputTag)tag;
							
							// get new value if any
							string newValue = GetFormElementValue("input",input.Name, html);

							if  ( ( newValue.Length > 0 ) && ( input.Value.Length > 0 ) )
							{
								input.Value = newValue;
							}

							AddInputPostData(input, al, addEndLine);
						}
												
						if ( tag is HtmlTextAreaTag )
							AddTextAreaPostData((HtmlTextAreaTag)tag,al, addEndLine);
						
						if ( tag is HtmlSelectTag )
							AddSelectPostData((HtmlSelectTag)tag,al, addEndLine);
					}
				}

				return al;
			}
			catch
			{
				throw;
			}
		}


		/// <summary>
		/// Converts the HtmlFormTag to an ArrayList and checks for updated values in HTML content.
		/// </summary>
		/// <param name="form"> HtmlFormTag to convert.</param>
		/// <param name="elementNames"> The list of elements to update.</param>
		/// <returns> A Hashtable type.</returns>
		public ArrayList GetArrayList(HtmlFormTag form, string html, StringCollection elementNames)
		{
			try
			{
				ArrayList al = new ArrayList();
				for (int j=0;j<form.Count;j++)
				{
					HtmlTagBaseList controlArray = (HtmlTagBaseList)((DictionaryEntry)form[j]).Value;

					bool last = false;
					if ( j == (form.Count - 1) )
						last = true;

					string s = string.Empty;
					for ( int i=0;i<controlArray.Count;i++ )
					{
						bool addEndLine = false;
						if ( i == (controlArray.Count - 1) && last )							
						{
							addEndLine = true;
						}

						HtmlTagBase tag = controlArray[i];

						if ( tag is HtmlButtonTag )
							AddButtonPostData((HtmlButtonTag)tag, al, addEndLine);

						if ( tag is HtmlInputTag)
						{
							HtmlInputTag input = (HtmlInputTag)tag;
							
							// if contains the element name
							if ( elementNames.Contains(input.Name) )
							{
								// get new value if any
								string newValue = GetFormElementValue("input",input.Name, html);

								if  ( ( newValue.Length > 0 ) )
								{
									input.Value = newValue;
								}
							}

							AddInputPostData(input, al, addEndLine);
						}

						if ( tag is HtmlTextAreaTag )
							AddTextAreaPostData((HtmlTextAreaTag)tag,al, addEndLine);

						if ( tag is HtmlSelectTag )
							AddSelectPostData((HtmlSelectTag)tag,al, addEndLine);
					}
				}

				return al;
			}
			catch
			{
				throw;
			}
		}


		/// <summary>
		/// Adds the button post data string.
		/// </summary>
		/// <param name="buttonTag"> The button tag.</param>
		/// <param name="list"> The arraylist to append tag.</param>
		/// <param name="addEndLine"> The add end line boolean value.</param>
		public void AddButtonPostData(HtmlButtonTag buttonTag, ArrayList list, bool addEndLine)
		{
			if ( addEndLine )
			{
				buttonTag.Value = AddEndLine(buttonTag.Value);
			}

			AddButtonPostData(buttonTag, list);
		}
		/// <summary>
		/// Adds the button post data string.
		/// </summary>
		/// <param name="tag"> The button tag.</param>
		/// <param name="list"> The arraylist to append tag.</param>
		public void AddButtonPostData(HtmlButtonTag buttonTag, ArrayList list)
		{						
			// no Name, so we cant send it to server
			if ( !buttonTag.Name.StartsWith("ms__id") )
			{	
				string s = EncodeDecode.UrlEncode(buttonTag.Name) + "=" 
					+ EncodeDecode.UrlEncode(buttonTag.Value);

				//name and value
				list.Add(s);
			}			
		}

		/// <summary>
		/// Adds the input post data string.
		/// </summary>
		/// <param name="input"> The input tag.</param>
		/// <param name="list"> The arraylist to append tag.</param>
		/// <param name="addEndLine"> The add end line boolean value.</param>
		public void AddInputPostData(HtmlInputTag input, ArrayList list, bool addEndLine)
		{
			if ( addEndLine )
			{
				input.Value = AddEndLine(input.Value);
			}

			AddInputPostData(input, list);
		}

		/// <summary>
		/// Adds the input post data string.
		/// </summary>
		/// <param name="input"> The input tag.</param>
		/// <param name="list"> The arraylist to append tag.</param>
		public void AddInputPostData(HtmlInputTag input, ArrayList list)
		{						
			// no Name, so we cant send it to server
			if ( !input.Name.StartsWith("ms__id") )
			{
				string s = EncodeDecode.UrlEncode(input.Name) + "=" + EncodeDecode.UrlEncode(input.Value);
			
				switch ( input.Type )
				{
					case HtmlInputType.Radio:
						if ( input.Checked.ToLower() == "true" )
						{
							//name and value
							list.Add(s);
						}
						break;
					case HtmlInputType.Image:
						// do nothing
						break;
					default:
						//name and value
						list.Add(s);
						break;
				}
			}
		}

		/// <summary>
		/// Adds the textarea tag post data string.
		/// </summary>
		/// <param name="textarea"> The textarea tag.</param>
		/// <param name="list"> The arraylist to append tag.</param>
		/// <param name="addEndLine"> The add end line boolean value.</param>
		public void AddTextAreaPostData(HtmlTextAreaTag textarea, ArrayList list, bool addEndLine)
		{
			if ( addEndLine )
			{
				textarea.Value = AddEndLine(textarea.Value);
			}

			AddTextAreaPostData(textarea, list);
		}
		/// <summary>
		/// Adds the textarea tag post data string.
		/// </summary>
		/// <param name="textarea"> The textarea tag.</param>
		/// <param name="list"> The arraylist to append tag.</param>
		public void AddTextAreaPostData(HtmlTextAreaTag textarea, ArrayList list)
		{						
			// no Name, so we cant send it to server
			if ( !textarea.Name.StartsWith("ms__id") )
			{
				string s = EncodeDecode.UrlEncode(textarea.Name) + "=" + EncodeDecode.UrlEncode(textarea.Value);

				//name and value
				list.Add(s);
			}
		}
		/// <summary>
		/// Adds the select tag post data string.
		/// </summary>
		/// <param name="select"> The select tag.</param>
		/// <param name="list"> The arraylist to append tag.</param>
		/// <param name="addEndLine"> The add end line boolean value.</param>
		public void AddSelectPostData(HtmlSelectTag select, ArrayList list, bool addEndLine)
		{
			if ( addEndLine )
			{
				select.Value = AddEndLine(select.Value);
			}

			AddSelectPostData(select, list);
		}
		/// <summary>
		/// Adds the select tag post data string.
		/// </summary>
		/// <param name="select"> The select tag.</param>
		/// <param name="list"> The arraylist to append tag.</param>
		public void AddSelectPostData(HtmlSelectTag select, ArrayList list)
		{			
			// no Name, so we cant send it to server
			if ( !select.Name.StartsWith("ms__id") )
			{
				string s;
				if  ( select.Multiple )
				{
					foreach ( HtmlOptionTag tag in select.Options )
					{
						HtmlOptionTag opt = tag;
						if ( opt.Selected ) 
						{
							s = EncodeDecode.UrlEncode(select.Name) + "=" + EncodeDecode.UrlEncode(opt.Value);
							//name and value
							list.Add(s);
						}
					}
				} 
				else 
				{							
					s = EncodeDecode.UrlEncode(select.Name) + "=" + EncodeDecode.UrlEncode(select.Value);
					//name and value
					list.Add(s);
				}
			}
		}
		#endregion
	}


}
