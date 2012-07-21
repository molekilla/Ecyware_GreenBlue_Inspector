// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003
using System;

namespace Ecyware.GreenBlue.Engine.HtmlDom
{
	/// <summary>
	/// Contains the properties for a link tag.
	/// </summary>
	[Serializable]
	public class HtmlALinkTag : HtmlTagBase
	{

		string _text="";
		string _href="";

		/// <summary>
		/// Creates a new link tag.
		/// </summary>
		public HtmlALinkTag()
		{
		}

		/// <summary>
		/// Gets or sets the link URL reference.
		/// </summary>
		public string HRef
		{
			get
			{
				return _href;
			}
			set
			{
				_href = value;
			}
		}

		/// <summary>
		/// Gets or sets the text reference.
		/// </summary>
		public string Text
		{
			get
			{
				return _text;
			}
			set
			{
				_text = value;
			}
		}
	}
}
