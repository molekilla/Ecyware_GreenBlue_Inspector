// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue
// Author: Rogelio Morrell C.
// Date: November 2004, January 2005
using System;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections;

namespace Ecyware.GreenBlue.Engine.HtmlCommand
{
	/// <summary>
	/// Summary description for HtmlLightParser.
	/// </summary>
	public class HtmlLightParser
	{
		// regex cache
		static Hashtable regex = new Hashtable();		


		/// <summary>
		/// Creates a new HtmlLightParser.
		/// </summary>
		static HtmlLightParser()
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
		}

		/// <summary>
		/// Gets the META tag redirect url if any.
		/// </summary>
		/// <param name="htmlContent"> The HTML content to parse.</param>
		/// <returns> Returns empty if no meta tag url found, else a url segment.</returns>
		public static string GetMetaRedirectUrlString(string htmlContent)
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
		/// Gets the subattribute value.
		/// </summary>
		/// <param name="attributeValue"> The attribute value.</param>
		/// <param name="delimiter"> The delimiter.</param>
		/// <param name="index"> The index.</param>
		/// <returns> A string value.</returns>
		public static string GetSubAttributeValue(string attributeValue, string delimiter, int index)
		{
			// split with delimiter
			string[] content = attributeValue.Split(delimiter.ToCharArray());

			int i = content[index].IndexOf("=");
			if (  i > 0 )
			{
				// contains = 
				// split and return value.
				string s = content[index].Substring(i+1);
				return s;
			} 
			else 
			{
				return content[index];
			}			
		}

		/// <summary>
		/// Creates the Html element collection.
		/// </summary>
		/// <param name="htmlContent"> The HTML Content.</param>
		/// <param name="tagName"> The tag name.</param>
		/// <returns> A string value.</returns>
		public static NameObjectCollection CreateHtmlElement(string htmlContent, string tagName)
		{
			NameObjectCollection collection = new NameObjectCollection();

			string elementValue = string.Empty;

			StringBuilder rex = new StringBuilder();
			rex.Append(@"(?<header><(?i:");
			rex.Append(tagName);
			rex.Append(@")[^>]*?)(>|>(?<source>[\w|\t|\r|\W]*?)</(?i:");
			rex.Append(tagName);
			rex.Append(@")>)");

			Regex getElements = new Regex(rex.ToString(), RegexOptions.None);
			Regex getAttributes = (Regex)regex["GetAttributes"];

			// Get elements matches
			MatchCollection matches = getElements.Matches(htmlContent);
			
			// Example:
			// <input name='Username' ...
			// <input name='Username' ...
			// <input name='Password' ...

			// for each element
			for( int i=0;i<matches.Count;i++ )
			{
				string element = matches[i].Value;

				#region Search for element with name
				// get attributes
				MatchCollection attributes = getAttributes.Matches(element);

				int j=0;
				foreach (Match m in attributes)
				{
					string name = m.Groups["name"].Value;
					string value = m.Groups["value"].Value.ToLower(System.Globalization.CultureInfo.InvariantCulture);
					name = name.ToLower(System.Globalization.CultureInfo.InvariantCulture);

					// Create Item in collection
					if ( name == "name" )
					{
						if ( collection[value] != null )
						{
							HtmlLightParserElement liteElement = (HtmlLightParserElement)collection[value];
							if ( !liteElement.Contains(element) )
							{
								liteElement.Add(element);
							}
						} 
						else 
						{							
							collection.Add(value, new HtmlLightParserElement(element));
						}
					} 
					if ( name == "id" )
					{
						if ( collection[value] != null )
						{
							HtmlLightParserElement liteElement = (HtmlLightParserElement)collection[value];
							if ( !liteElement.Contains(element) )
							{
								liteElement.Add(element);
							}
						} 
						else 
						{							
							collection.Add(value, new HtmlLightParserElement(element));
						}
					} 
					if ( collection[tagName] != null )
					{
						HtmlLightParserElement liteElement = (HtmlLightParserElement)collection[tagName];
						if ( !liteElement.Contains(element) )
						{
							liteElement.Add(element);
						}
					} 
					else 
					{		
						collection.Add(tagName, new HtmlLightParserElement(element));
					}

					j++;
				}
				#endregion
			}

			return collection;
		}


		/// <summary>
		/// Gets the attribute.
		/// </summary>
		/// <param name="elementValue"> The element value.</param>
		/// <param name="attributeName"> The attribute name.</param>
		/// <returns> A string value.</returns>
		public static string GetAttribute(string elementValue, string attributeName)
		{
			Regex getAttributes = (Regex)regex["GetAttributes"];
			string valueResult = string.Empty;

			// get attributes
			MatchCollection attributes = getAttributes.Matches(elementValue);

			foreach (Match m in attributes)
			{
				string attName = m.Groups["name"].Value.ToLower(System.Globalization.CultureInfo.InvariantCulture);

				// if match, then get value
				if ( attName == attributeName )
				{					
					valueResult = m.Groups["value"].Value;
					break;
				}
			}

			return valueResult;
		}
		/// <summary>
		/// Get the element value by type and name attribute value.
		/// </summary>
		/// <param name="elementType"> The element type.</param>
		/// <param name="name"> The name value.</param>
		/// <param name="htmlContent"> The html content to parse.</param>
		/// <returns> A value from a Form Element.</returns>
//		public string GetFormElementValue(string elementType, string name, string htmlContent)
//		{
//			string elementValue = string.Empty;
//
//			StringBuilder rex = new StringBuilder();
//			rex.Append(@"(?<header><(?i:");
//			rex.Append(elementType.ToLower());
//			rex.Append(@")[^>]*?)(>|>(?<source>[\w|\t|\r|\W]*?)</(?i:");
//			rex.Append(elementType.ToLower());
//			rex.Append(@")>)");
//
//			Regex getElements = new Regex(rex.ToString(), RegexOptions.None);
//			Regex getAttributes = (Regex)regex["GetAttributes"];
//
//			// Get elements matches
//			MatchCollection matches = getElements.Matches(htmlContent);
//			
//			// for each meta
//			for( int i=0;i<matches.Count;i++ )
//			{
//				string element = matches[i].Value;
//
//				#region Search for element with name
//				// get attributes
//				MatchCollection attributes = getAttributes.Matches(element);
//
//				foreach (Match m in attributes)
//				{
//					// string name = m.Groups["name"].Value;
//					string attributeName = m.Groups["value"].Value;
//
//					// if match, then get value
//					if ( attributeName == name )
//					{
//						// element found, get value now
//						foreach (Match mm in attributes)
//						{
//							attributeName = mm.Groups["name"].Value;
//							string result = mm.Groups["value"].Value;
//
//							// if match, then get value
//							if ( attributeName.ToLower() == "value" )
//							{
//								// value found
//								elementValue = result;
//								break;
//							}
//						}
//						break;
//					}
//				}
//				#endregion
//			}
//
//			return elementValue;
//		}
//		
		
		/// <summary>
		/// Gets the ACTION url by absolute url.
		/// </summary>
		/// <param name="url"> The absolute url.</param>
		/// <param name="htmlContent"> The parsed HTML content.</param>
		/// <returns> A string with the form action.</returns>
//		public string GetFormActionByAbsoluteUrl(string url, string htmlContent)
//		{
//			string result = string.Empty;
//
//			int startAction = htmlContent.ToLower().IndexOf(url);			
//
//			if ( startAction > -1 )
//			{
//				int endTag = htmlContent.IndexOf("\"", startAction);
//				result = htmlContent.Substring(startAction,(endTag-startAction));
//			}
//
//			return result;
//		}
	}
}
