// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003
using System;
using System.Net;
using System.Collections;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.WebUnitTestManager;
using Ecyware.GreenBlue.Engine.HtmlCommand;
namespace Ecyware.GreenBlue.WebUnitTestManager
{
	/// <summary>
	/// Contains the base class for the unit tester implementations.
	/// </summary>
	public class HtmlFormUnitTestBase
	{
		private string _name;

		/// <summary>
		/// Creates a new HtmlFormUnitTestBase.
		/// </summary>
		public HtmlFormUnitTestBase()
		{
		}

		/// <summary>
		/// Gets or sets the UnitTestName.
		/// </summary>
		public string UnitTestName
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}
	}

	/// <summary>
	/// Contains the interface for implementing a unit tester.
	/// </summary>
	interface IHtmlFormUnitTest
	{
		/// <summary>
		/// Fills the uri with tests.
		/// </summary>
		/// <param name="url"> The url.</param>
		/// <param name="uriType"> The WebServerUriType type.</param>
		/// <returns> The updated uri.</returns>
		Uri FillUri(Uri url, WebServerUriType uriType);
		/// <summary>
		/// Fills the cookie collection with tests.
		/// </summary>
		/// <param name="cookies"> The cookie collection.</param>
		/// <returns> The updated cookie collection.</returns>
		HtmlFormTag FillForm(HtmlFormTag formTag);
		/// <summary>
		/// Fills the post data with tests.
		/// </summary>
		/// <param name="postData"> The post data hastable.</param>
		/// <returns> The updated post data hashtable.</returns>
		PostDataCollection FillPostData(PostDataCollection postData);
		/// <summary>
		/// Fills the form tag with tests.
		/// </summary>
		/// <param name="formTag"> The HtmlFormTag.</param>
		/// <returns> The updated HtmlFormTag.</returns>
		CookieCollection FillCookies(CookieCollection cookies);
	}
}
