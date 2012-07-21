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
using System.Collections;

namespace Ecyware.GreenBlue.Engine.HtmlCommand
{
	/// <summary>
	/// The HtmlQueryUtil class contains logic for querying HTML.
	/// </summary>
	public class HtmlQueryUtil
	{

		private bool _docCaching;
		private XmlDocument cache=null;
		private XmlNamespaceManager namespaceCache=null;

		/// <summary>
		/// Creates a new HtmlQueryUtil.
		/// </summary>
		public HtmlQueryUtil()
		{
		}

		/// <summary>
		/// Creates a new HtmlQueryUtil.
		/// </summary>
		/// <param name="cachedDocument"> Sets the caching flag.</param>
		public HtmlQueryUtil(bool cachedDocument)
		{
			this.AllowDocumentCaching = cachedDocument;
		}

		/// <summary>
		/// Gets or sets the caching setting for the document.
		/// </summary>
		public bool AllowDocumentCaching
		{
			get
			{
				return _docCaching;
			}
			set
			{
				_docCaching = value;
			}
		}

		/// <summary>
		/// Gets a XmlNodeList from the queried HTML.
		/// </summary>
		/// <param name="data"> The HTML content to query.</param>
		/// <param name="query"> The XPath Query.</param>
		/// <returns> A XmlNodeList.</returns>
		public XmlNodeList GetNodes(string data,string query)
		{			
			
			HtmlParser parser = new HtmlParser();

			try
			{
				XmlDocument doc;

				if ( this.AllowDocumentCaching )
				{
					if ( cache == null )
					{
						//Load the file.
						doc = new XmlDocument(); 
						doc.Load(new StringReader(data));
						this.cache=doc;
					} 
					else 
					{
						doc = this.cache;
					}
				} 
				else 
				{
					//Load the file.
					doc = new XmlDocument(); 
					doc.Load(new StringReader(data));
				}
				

				if ( this.namespaceCache==null)
				{
					// create prefix<->namespace mappings (if any) 
					XmlNamespaceManager  nsMgr = new XmlNamespaceManager(doc.NameTable);

					// resolve namespaces before loading document
					Hashtable values = HtmlParser.ResolveNamespaces(new XmlTextReader(new StringReader(data)));

					foreach (DictionaryEntry de in values)
					{
						nsMgr.AddNamespace((string)de.Key,(string)de.Value);
					}

					this.namespaceCache = nsMgr;
				}

				//Query the document 
				XmlNodeList nodes = doc.SelectNodes(query, this.namespaceCache); 

				return nodes;

			}
			catch
			{
				throw;
			}

		}


		/// <summary>
		/// Gets a XML string from the queried HTML.
		/// </summary>
		/// <param name="data"> The HTML content to query.</param>
		/// <param name="query"> The XPath Query.</param>
		/// <returns> A XML string.</returns>
		public string GetXmlString(string data,string query)
		{			
			
			StringBuilder sb = new StringBuilder();
			HtmlParser parser = new HtmlParser();

			try
			{
				XmlDocument doc;

				if ( this.AllowDocumentCaching )
				{
					if ( cache==null )
					{
						//Load the file.
						doc = new XmlDocument(); 
						doc.Load(new StringReader(data));
						this.cache=doc;
					} 
					else 
					{
						doc=this.cache;
					}
				} else {
					//Load the file.
					doc = new XmlDocument(); 
					doc.Load(new StringReader(data));
				}
				

				if ( this.namespaceCache==null)
				{
					// create prefix<->namespace mappings (if any) 
					XmlNamespaceManager  nsMgr = new XmlNamespaceManager(doc.NameTable);

					// resolve namespaces before loading document
					Hashtable values = HtmlParser.ResolveNamespaces(new XmlTextReader(new StringReader(data)));

					foreach (DictionaryEntry de in values)
					{
						nsMgr.AddNamespace((string)de.Key,(string)de.Value);
					}

					this.namespaceCache = nsMgr;
				}

				// Query the document 
				XmlNodeList nodes = doc.SelectNodes(query, this.namespaceCache); 

				// print output 
				foreach(XmlNode node in nodes)
				{
					sb.Append(node.OuterXml + "\n\n");
				}

				return sb.ToString();

			}
			catch
			{
				throw;
			}
		}

	}
}
