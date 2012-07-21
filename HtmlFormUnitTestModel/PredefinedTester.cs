// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Collections;
using System.Net;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.WebUnitTestManager;
using Ecyware.GreenBlue.Engine.HtmlCommand;
namespace Ecyware.GreenBlue.WebUnitTestManager
{
	/// <summary>
	/// Contains the PredefinedTester type.
	/// </summary>
	public class PredefinedTester : HtmlFormUnitTestBase, IHtmlFormUnitTest
	{
		PredefinedTesterArgs arguments = null;

		/// <summary>
		/// Creates a new PredefinedTester.
		/// </summary>
		/// <param name="args"> The predefined tester arguments.</param>
		public PredefinedTester(PredefinedTesterArgs args)
		{
			arguments = args;
		}

		#region IHtmlFormUnitTest Members
		/// <summary>
		/// Fills the uri with tests.
		/// </summary>
		/// <param name="url"> The url.</param>
		/// <param name="uriType"> The WebServerUriType type.</param>
		/// <returns> The updated uri.</returns>
		public Uri FillUri(Uri url, WebServerUriType uriType)
		{
			return url;
		}

		/// <summary>
		/// Fills the cookie collection with tests.
		/// </summary>
		/// <param name="cookies"> The cookie collection.</param>
		/// <returns> The updated cookie collection.</returns>
		public CookieCollection FillCookies(CookieCollection cookies)
		{
			return cookies;
		}
		/// <summary>
		/// Fills the post data with tests.
		/// </summary>
		/// <param name="postData"> The post data hastable.</param>
		/// <returns> The updated post data hashtable.</returns>
		public PostDataCollection FillPostData(PostDataCollection postData)
		{
			// ignore pattern and send PostData from FormData
			return arguments.UserPostData;
		}

		/// <summary>
		/// Fills the form tag with tests.
		/// </summary>
		/// <param name="formTag"> The HtmlFormTag.</param>
		/// <returns> The updated HtmlFormTag.</returns>
		public HtmlFormTag FillForm(HtmlFormTag formTag)
		{
			// ignore pattern and send HtmlFormTag from FormData
			return arguments.FormData;
		}


		#endregion
	}
}
