// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.WebUnitTestManager;
using Ecyware.GreenBlue.Engine.HtmlCommand;
namespace Ecyware.GreenBlue.WebUnitTestManager
{
	/// <summary>
	/// Contains the logic for generating an uri test according to the web server type.
	/// </summary>
	public class UriGenerator
	{
		public UriGenerator()
		{
		}

		public Uri GenerateUri(WebServerUriType type, Uri url, string buffer)
		{
			Uri result = null;

			// get last segment and save the rest segments in a builder
			StringBuilder builder = new StringBuilder();
			string lastSegment = string.Empty;

			// remove scheme
			string scheme = url.Scheme;
			string tempUrl = url.ToString();
			string[] segments = tempUrl.Replace(scheme + "://", "").Split('/');

			// append scheme
			segments[0] = scheme + "://" + segments[0];

			for (int i=0;i<segments.Length;i++)
			{
				if ( i == (segments.Length-1) )
				{
					if ( segments[i].Length == 0 )
					{
						lastSegment = "/";
					} 
					else 
					{
						// last segment
						lastSegment = segments[i];
					}
				} 
				else 
				{
					if ( segments[i].Length == 0 )
					{
						builder.Append("/");
					} 
					else 
					{
						// save segments
						builder.Append(segments[i] + "/");
					}
				}
			}

			if ( lastSegment != "/" )
			{
				switch (type)
				{
					case WebServerUriType.Normal:
						result = GenerateNormalUriTest(lastSegment, builder.ToString(), buffer);
						break;
				}
			} 
			else 
			{
				result = url;
			}

			return result;
		}


		private Uri GenerateNormalUriTest(string lastSegment, string urlSegments, string buffer)
		{
			StringBuilder writer = new StringBuilder();

			// ?value=1&value2=&
			// append &
			if ( !lastSegment.EndsWith("&") )
				lastSegment += "&";

			string[] firstParse = lastSegment.Split('&');

			//writer.Append("/");

			foreach ( string pair in firstParse )
			{
				string[] secondParse = pair.Split('=');
				for (int i=0;i<secondParse.Length;i++)
				{
					// mod
					if ( i % 2 == 0 )
					{
						// is pair
						writer.Append(secondParse[i]);
					} else {
						writer.Append("=" + EncodeDecode.UrlEncode(buffer) + "&");
					}
				}
			}

			Uri result = new Uri(urlSegments + writer.ToString());

			return result;
		}
	}
}
