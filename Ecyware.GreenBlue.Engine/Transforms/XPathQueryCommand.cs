using System;
using System.Xml;
using System.Text;
using Ecyware.GreenBlue.Engine.HtmlCommand;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Defines the XmlNode properties that can be use.
	/// </summary>
	public enum XmlNodeProperty
	{
		InnerText,
		InnerXml,
		OuterXml,
		LocalName
	}

	/// <summary>
	/// Summary description for XPathQueryCommand.
	/// </summary>
	[Serializable]
	public class XPathQueryCommand : TransformValue
	{
		string _prefix;
		string _postfix;
		XmlNodeProperty _xmlNodeProperty = XmlNodeProperty.OuterXml;

		[NonSerialized()]
		HtmlParser parser = new HtmlParser(false, false);

		[NonSerialized()]
		HtmlQueryUtil queryUtil = new HtmlQueryUtil();

		string _query;
		string _xslt;

		/// <summary>
		/// Creates a new XPathQueryCommand.
		/// </summary>
		public XPathQueryCommand()
		{
		}

		/// <summary>
		/// Gets or sets the query.
		/// </summary>
		public string Query
		{
			get
			{
				return _query;
			}
			set
			{
				_query = value;
			}
		}

		/// <summary>
		/// Gets or sets the xslt template.
		/// </summary>
		public string XsltTemplate
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

		/// <summary>
		/// Gets or sets the XmlNode property to use.
		/// </summary>
		public XmlNodeProperty UseNodeProperty
		{
			get
			{
				return _xmlNodeProperty;
			}
			set
			{
				_xmlNodeProperty = value;
			}
		}

		/// <summary>
		/// Gets or sets the prefix.
		/// </summary>
		public string Prefix
		{
			get
			{
				return _prefix;
			}
			set
			{
				_prefix = value;
			}
		}

		/// <summary>
		/// Gets or sets the postfix.
		/// </summary>
		public string Postfix
		{
			get
			{
				return _postfix;
			}
			set
			{
				_postfix = value;
			}
		}


		/// <summary>
		/// Executes a XSLT template.
		/// </summary>
		/// <param name="xml"> The XML to process.</param>
		/// <returns> Returns the result from the xslt.</returns>
		public string ExecuteXslt(string xml)
		{
			XsltCommand xsltCommand = new XsltCommand();

			string result = string.Empty;

			try
			{
				result = xsltCommand.TransformFromData(xml, this.XsltTemplate);
			}
			catch
			{
				result = string.Empty;
			}

			return result;
		}

		/// <summary>
		/// Returns true if xml is valid, else false.
		/// </summary>
		/// <param name="xml"> The XML Text to test.</param>
		/// <returns> True if xml, else false.</returns>
		public bool IsXml(string xml)
		{
			try
			{
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(xml);
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Executes the XPath query.
		/// </summary>
		/// <param name="text"> The XML to process.</param>
		/// <returns> Returns the result from the xpath query.</returns>
		public string ExecuteQuery(string text)
		{	
			try
			{
				string xml = string.Empty;

				// Validate XML
				if ( IsXml(text) )
				{
					xml = text;
				} 
				else 
				{
					xml = parser.GetParsableString(text);
				}

				// Execute XPath
				XmlNodeList nodeList = queryUtil.GetNodes(xml, this.Query);

				StringBuilder cache = new StringBuilder();
				foreach ( XmlNode node in nodeList )
				{
					switch ( this.UseNodeProperty )
					{
						case XmlNodeProperty.InnerText:
							cache.Append(node.InnerText);
							break;
						case XmlNodeProperty.InnerXml:
							cache.Append(node.InnerXml);
							break;
						case XmlNodeProperty.LocalName:
							cache.Append(node.LocalName);
							break;
						case XmlNodeProperty.OuterXml:
							cache.Append(node.OuterXml);
							break;
					}
					cache.Append("\r\n");
				}

				string result = cache.ToString();

				if ( this.XsltTemplate.Length > 0 )
				{
					result = ExecuteXslt(result);
				}

				if ( Prefix.Length > 0 )
				{
					result = Prefix + result;
				}

				if ( Postfix.Length > 0 )
				{
					result = result + Postfix;
				}
			
				return result;
			}
			catch
			{
				return string.Empty;
			}


		}

		public override object GetValue(Ecyware.GreenBlue.Engine.Scripting.WebResponse response)
		{			
			string result = ExecuteQuery(response.HttpBody);
			result = ExecuteXslt(result);

			if ( Prefix.Length > 0 )
			{
				result = Prefix + result;
			}

			if ( Postfix.Length > 0 )
			{
				result = result + Postfix;
			}

			return result;
		}

	}
}
