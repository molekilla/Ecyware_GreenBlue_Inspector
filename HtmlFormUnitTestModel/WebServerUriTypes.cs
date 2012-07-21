// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;

namespace Ecyware.GreenBlue.WebUnitTestManager
{
	/// <summary>
	/// Contains the different types of web server uri types.
	/// </summary>
	public enum WebServerUriType
	{
		/// <summary>
		/// Uri type for servers that use url translation.
		/// </summary>
		UrlTranslation,
		/// <summary>
		/// Uri type for servers that use default settings.
		/// </summary>
		Normal,
		/// <summary>
		/// Uri type for servers that use comma delimited urls.
		/// </summary>
		CommaDelimited,
		/// <summary>
		/// Uri type for servers that use pipe delimited urls.
		/// </summary>
		PipeDelimited,
		/// <summary>
		/// Uri type for servers that use semicolon delimited urls.
		/// </summary>
		SemicolonDelimited,
		/// <summary>
		/// Uri type for servers that use colon delimited urls.
		/// </summary>
		ColonDelimited,
		/// <summary>
		/// Uri type for servers that use tilde delimited urls.
		/// </summary>
		TildeDelimited,
		/// <summary>
		/// Uri type for servers that use question mark delimited urls.
		/// </summary>
		QuestionSignLimited
	}
}
