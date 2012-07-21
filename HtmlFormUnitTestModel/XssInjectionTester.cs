// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: February 2004
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
	/// Contains the XssInjectionTester type.
	/// </summary>
	internal class XssInjectionTester : HtmlFormUnitTestBase, IHtmlFormUnitTest
	{
		private string _xssValue = string.Empty;

		/// <summary>
		/// Creates a new XssInjectionTester.
		/// </summary>
		/// <param name="args"> The xss injection tester arguments.</param>
		public XssInjectionTester(XssInjectionTesterArgs args)
		{
			this.UnitTestName = "XssInjectionTester";
			this.XssValue = args.XssValue;
		}

		/// <summary>
		/// Gets or sets the xss value.
		/// </summary>
		public string XssValue
		{
			get
			{
				return _xssValue;
			}
			set
			{
				_xssValue = value;
			}
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
			string buffer = this.XssValue;

			// copy url
			Uri temp = new Uri(url.ToString());

			UriGenerator generator = new UriGenerator();

			try
			{
				temp = generator.GenerateUri(uriType, temp, buffer);
				return temp;
			}
			catch (Exception ex)
			{
				ExceptionHandler.RegisterException(ex);
				return url;
			}
		}
		/// <summary>
		/// Fills the cookie collection with tests.
		/// </summary>
		/// <param name="cookies"> The cookie collection.</param>
		/// <returns> The updated cookie collection.</returns>
		public CookieCollection FillCookies(CookieCollection cookies)
		{
			string buffer = this.XssValue;
			CookieCollection tempCookies = new CookieCollection();

			foreach ( Cookie cky in cookies )
			{
				Cookie temp = new Cookie(cky.Name,"");
				temp.Comment = buffer;
				temp.CommentUri = cky.CommentUri;
				temp.Domain = cky.Domain;
				temp.Expired = cky.Expired;
				temp.Expires = cky.Expires;
				//temp.Name = temp.Name;
				temp.Path = cky.Path;
				temp.Port = cky.Port;
				temp.Secure = cky.Secure;
				temp.Value = buffer;
				temp.Version = cky.Version;

				tempCookies.Add(temp);
			}

			return tempCookies;
		}
		/// <summary>
		/// Fills the post data with tests.
		/// </summary>
		/// <param name="postData"> The post data hastable.</param>
		/// <returns> The updated post data hashtable.</returns>
		public PostDataCollection FillPostData(PostDataCollection postData)	
		{
			string buffer = this.XssValue;

			//ArrayList keys = new ArrayList(postData.Keys);

			for (int i=0;i<postData.Keys.Count;i++)
			{
				ArrayList values = postData[postData.Keys[i]];

				for (int j=0;j<values.Count;j++)
				{
					values[j] = buffer;
				}
			}

			return postData;
		}
		/// <summary>
		/// Fills the form tag with tests.
		/// </summary>
		/// <param name="form"> The HtmlFormTag.</param>
		/// <returns> The updated HtmlFormTag.</returns>
		public HtmlFormTag FillForm(HtmlFormTag form)
		{

			string buffer = this.XssValue;

			for (int i=0;i<form.Count;i++)
			{
				HtmlTagBaseList controlArray = (HtmlTagBaseList)((DictionaryEntry)form[i]).Value;

				#region inner foreach loop
				foreach (HtmlTagBase tag in controlArray)
				{
					if (tag is HtmlInputTag)
					{
						HtmlInputTag input=(HtmlInputTag)tag;
						input.Value = buffer;
					}
					if (tag is HtmlButtonTag)
					{
						HtmlButtonTag button = (HtmlButtonTag)tag;
						button.Value=buffer;
					}
					if (tag is HtmlSelectTag)
					{
						HtmlSelectTag select = (HtmlSelectTag)tag;
						if  ( select.Multiple )
						{
							foreach ( HtmlOptionTag opt in select.Options )
							{
								// HtmlOptionTag opt = tag;
								if ( opt.Selected ) 
								{
									opt.Value=buffer;
								}
							}
						} 
						else 
						{							
							select.Value = buffer;
						}
					}
					
					if (tag is HtmlTextAreaTag)
					{
						HtmlTextAreaTag textarea=(HtmlTextAreaTag)tag;
						textarea.Value=buffer;
					}
				}
				#endregion
			}

			return form;
		}

		#endregion
	}
}
