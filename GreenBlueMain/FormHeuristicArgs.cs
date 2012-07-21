// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004 - July 2004
using System;
using Ecyware.GreenBlue.Engine.HtmlDom;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Contatins the definition for the FormConvertionArgs.
	/// </summary>
	public class FormHeuristicArgs : EventArgs
	{
		private string _postData = string.Empty;
		private Uri _siteUri = null;
		private HtmlFormTag _formTag = null;

		/// <summary>
		/// Creates a new FormHeuristicArgs
		/// </summary>
		public FormHeuristicArgs()
		{
		}
		/// <summary>
		/// Gets or sets the site uri.
		/// </summary>
		public Uri SiteUri
		{
			get
			{
				return _siteUri;
			}
			set
			{
				_siteUri = value;
			}
			
		}
		/// <summary>
		/// Gets or sets the post data.
		/// </summary>
		public string PostData
		{
			get
			{
				return _postData;
			}
			set
			{
				_postData = value;
			}
		}

		/// <summary>
		/// Gets or sets a HtmlFormTag.
		/// </summary>
		public HtmlFormTag FormTag
		{
			get
			{
				return _formTag;
			}
			set
			{
				_formTag = value;
			}
		}
	}
}
