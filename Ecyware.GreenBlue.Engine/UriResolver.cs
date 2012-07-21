// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Contains logic for resolving urls.
	/// </summary>
	public class UriResolver
	{
		/// <summary>
		/// Creates a new UriResolver.
		/// </summary>
		private UriResolver()
		{
		}

		/// <summary>
		/// Resolves the url for http and https uri's.
		/// </summary>
		/// <param name="uri"> The origini uri.</param>
		/// <param name="segment"> Any segment that belongs to the uri.</param>
		/// <returns> Returns an absolute url as a string.</returns>
		public static string ResolveUrl(Uri uri, string segment)
		{
			string checkFirstChar = segment.Substring(0,1).ToLower();
			string result = String.Empty;

			if ( checkFirstChar == "/" )
			{
				//segment = segment.Substring(1);
				//requestData = requestUrl.AbsoluteUri + src;
				Uri parseUri = new Uri(uri ,segment);
				//string diff = uri.MakeRelative(parseUri);
				result = parseUri.ToString();
			}
			else
			{
				if ( checkFirstChar == "h" )
				{
					// check if is http
					if ( segment.Substring(0,5).ToLower() == "http:" )
					{
						result = segment;
					} 
					else 
					{
						// check if is https
						if ( segment.Substring(0,6).ToLower() == "https:" )
						{
							result = segment;
						} 
					}
				}
				else 
				{
					Uri parseUri = new Uri(uri,segment);
					result = parseUri.ToString();
				}		
			}
	
			return result;
		}
	}
}
