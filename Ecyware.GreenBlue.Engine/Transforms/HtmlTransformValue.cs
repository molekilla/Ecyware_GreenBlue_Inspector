// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using Ecyware.GreenBlue.Engine.HtmlCommand;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for HtmlTransformValue.
	/// </summary>
	[Serializable]
	public class HtmlTransformValue  : TransformValue
	{
		//private HtmlLightParser _liteParser = new HtmlLightParser();
		private string _tag;
		private string _nameId;
		private int _index;
		private string _attName = string.Empty;
		private int _delimiterIndex;
		private string _delimiter;
		private bool _hasAttributeDelimiter = false;
		private static string[] _tags = new string[] {"A",
																	  "Input",
																	  "Textarea",
																	  "Div",
																	  "Script",
																	  "Option",
																	  "Body",
																	  "Link",
																	  "Pre",
																	  "Object",
																	  "Embed",
																	  "Span", "Meta"};
		/// <summary>
		/// Creates a new HtmlTransformValue.
		/// </summary>
		public HtmlTransformValue()
		{
		}


		/// <summary>
		/// Gets or sets the tag
		/// </summary>
		public string Tag
		{
			get
			{
				return _tag;
			}
			set
			{
				_tag = value;
			}
		}

		/// <summary>
		/// Gets or sets the attribute name.
		/// </summary>
		public string AttributeName
		{
			get
			{
				return _attName;
			}
			set
			{
				_attName = value;
			}
		}
		/// <summary>
		/// Gets or sets the name or id of the tag.
		/// </summary>
		public string TagNameId
		{
			get
			{
				return _nameId;
			}
			set
			{
				_nameId = value;
			}
		}

		/// <summary>
		/// Gets or sets the index.
		/// </summary>
		public int Index
		{
			get
			{
				return _index;
			}
			set
			{
				_index = value;
			}
		}

		/// <summary>
		/// If the attribute uses a delimiter.
		/// </summary>
		public bool HasAttributeDelimiter
		{
			get
			{
				return _hasAttributeDelimiter;
			}
			set
			{
				_hasAttributeDelimiter = value;
			}
		}

		/// <summary>
		/// Gets or sets the delimiter.
		/// </summary>
		public string Delimiter
		{
			get
			{
				return _delimiter;
			}
			set
			{
				_delimiter = value;
			}
		}

		/// <summary>
		/// Gets or sets the delimiter index.
		/// </summary>
		public int DelimiterIndex
		{
			get
			{
				return _delimiterIndex;
			}
			set
			{
				_delimiterIndex = value;
			}
		}

		/// <summary>
		/// Gets a list of tags.
		/// </summary>
		public static string[] GetTags
		{
			get
			{
				return _tags;
			}
		}


		/// <summary>
		/// Gets the value associated with the web response.
		/// </summary>
		/// <param name="response"> The WebResponse type.</param>
		/// <returns> The result to return.</returns>
		public override object GetValue(WebResponse response)
		{
			string htmlContent = response.HttpBody;
			NameObjectCollection tagCollection = HtmlLightParser.CreateHtmlElement(htmlContent, this.Tag);
			
			//string rawElement;
			HtmlLightParserElement liteElement = null;

			if ( TagNameId.Length > 0 )
			{
				liteElement = (HtmlLightParserElement)tagCollection[this.TagNameId];
			} 
			else 
			{
				// get by index
				liteElement = (HtmlLightParserElement)tagCollection[Tag];
			}

			if ( this.AttributeName.Length > 0 )
			{
				if ( HasAttributeDelimiter )
				{
					string value = liteElement.GetAttribute(Index, AttributeName);
					return HtmlLightParser.GetSubAttributeValue(value, this.Delimiter, this.DelimiterIndex);
				} 
				else 
				{
					// Get Attribute.
					return liteElement.GetAttribute(Index, AttributeName);
				}
			} 
			else 
			{
				return string.Empty;
			}
		}
	}
}

