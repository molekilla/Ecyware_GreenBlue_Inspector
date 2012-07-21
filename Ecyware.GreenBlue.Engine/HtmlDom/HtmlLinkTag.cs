// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: June 2004
using System;
namespace Ecyware.GreenBlue.Engine.HtmlDom
{
	/// <summary>
	/// Contains the properties for a HtmlLinkTag.
	/// </summary>
	public class HtmlLinkTag : HtmlTagBase
	{
		string _href = string.Empty;
		string _mimeType = string.Empty;

		/// <summary>
		/// Creates a new HtmlLinkTag.
		/// </summary>
		public HtmlLinkTag()
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
		/// Gets or sets the MimeType
		/// </summary>
		public string MimeType
		{
			get
			{
				return _mimeType;
			}
			set
			{
				_mimeType = value;
			}
		}
	}
}
