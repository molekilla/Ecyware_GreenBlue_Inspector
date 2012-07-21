// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004 - July 2004
using System;
using mshtml;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Contains the definition for the FormConvertionArgs.
	/// </summary>
	public class FormConvertionArgs : EventArgs
	{
		private mshtml.HTMLFormElementClass _formElement = null;
		private string _postData = string.Empty;
		private Uri _siteUri = null;

		/// <summary>
		/// Creates a new FormConvertionArgs.
		/// </summary>
		public FormConvertionArgs()
		{
		}

		/// <summary>
		/// Gets or sets a HtmlFormElementClass.
		/// </summary>
		public HTMLFormElementClass FormElement
		{
			get
			{
				return _formElement;
			}
			set
			{
				_formElement = value;
			}
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
	}
}
