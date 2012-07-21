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
	/// Contains the logic for executing unit tests.
	/// </summary>
	public class UnitTester
	{
		private IHtmlFormUnitTestArgs _args;

		/// <summary>
		/// Creates a new UnitTester.
		/// </summary>
		/// <param name="args"> The IHtmlFormUnitTestArgs.</param>
		public UnitTester(IHtmlFormUnitTestArgs args)
		{
			this.Arguments = args;
		}

		/// <summary>
		/// Gets or sets the arguments.
		/// </summary>
		private IHtmlFormUnitTestArgs Arguments
		{
			get
			{
				return _args;
			}
			set
			{
				_args = value;
			}
		}

		/// <summary>
		/// Builds a unit test for a post data ArrayList.
		/// </summary>
		/// <param name="testType"> The test type to create.</param>
		/// <param name="postData"> The post data values to edit.</param>
		/// <returns> An edited post data ArrayList with applied test.</returns>
		public PostDataCollection BuildUnitTestPostData(UnitTestType testType, PostDataCollection postData)
		{
			PostDataCollection ret=null;
			IHtmlFormUnitTest tester;

			// Call FillPostData
			switch (testType)
			{
				case UnitTestType.BufferOverflow:
					tester = new BufferOverflowTester((BufferOverflowTesterArgs)this.Arguments);
					ret = tester.FillPostData(postData);
					break;
				case UnitTestType.DataTypes:
					tester = new DataTypesTester((DataTypesTesterArgs)this.Arguments);
					ret = tester.FillPostData(postData);
					break;
				case UnitTestType.Predefined:
					tester = new PredefinedTester(((PredefinedTesterArgs)this.Arguments));					
					ret = tester.FillPostData(postData);
					break;
				case UnitTestType.SqlInjection:
					tester = new SqlInjectionTester((SqlInjectionTesterArgs)this.Arguments);
					ret = tester.FillPostData(postData);
					break;
				case UnitTestType.XSS:
					tester = new XssInjectionTester((XssInjectionTesterArgs)this.Arguments);
					ret = tester.FillPostData(postData);
					break;
			}

			return ret;
		}

		/// <summary>
		/// Builds a unit test for a form.
		/// </summary>
		/// <param name="testType"> The test type to create.</param>
		/// <param name="form"> The form to edit.</param>
		/// <returns> An edited form tag with applied test.</returns>
		public HtmlFormTag BuildUnitTestForm(UnitTestType testType, HtmlFormTag form)
		{
			HtmlFormTag ret=null;
			IHtmlFormUnitTest tester;
			// Call FillForm
			switch (testType)
			{
				case UnitTestType.BufferOverflow:
					tester = new BufferOverflowTester((BufferOverflowTesterArgs)this.Arguments);
					ret = tester.FillForm(form);
					break;
				case UnitTestType.DataTypes:
					tester = new DataTypesTester((DataTypesTesterArgs)this.Arguments);
					ret = tester.FillForm(form);
					break;
				case UnitTestType.Predefined:
					tester = new PredefinedTester(((PredefinedTesterArgs)this.Arguments));					
					ret = tester.FillForm(form);
					break;
				case UnitTestType.SqlInjection:
					tester = new SqlInjectionTester((SqlInjectionTesterArgs)this.Arguments);
					ret = tester.FillForm(form);
					break;
				case UnitTestType.XSS:
					tester = new XssInjectionTester((XssInjectionTesterArgs)this.Arguments);
					ret = tester.FillForm(form);
					break;
			}

			return ret;

		}

		/// <summary>
		/// Builds a unit test for a uri.
		/// </summary>
		/// <param name="testType"> The test type</param>
		/// <param name="webServerUriType"> The web server url type.</param>
		/// <param name="url"> The uri data.</param>
		/// <returns> An edited cookie collection.</returns>
		public Uri BuildUnitTestGetRequest(UnitTestType testType, WebServerUriType webServerUriType, Uri url)
		{
			Uri result = null;
			IHtmlFormUnitTest tester = null;

			// Call FillForm
			switch (testType)
			{
				case UnitTestType.BufferOverflow:
					tester = new BufferOverflowTester((BufferOverflowTesterArgs)this.Arguments);
					break;
				case UnitTestType.DataTypes:
					tester = new DataTypesTester((DataTypesTesterArgs)this.Arguments);
					break;
				case UnitTestType.SqlInjection:
					tester = new SqlInjectionTester((SqlInjectionTesterArgs)this.Arguments);
					break;
				case UnitTestType.XSS:
					tester = new XssInjectionTester((XssInjectionTesterArgs)this.Arguments);
					break;
			}
			
			if ( tester != null )
				result = tester.FillUri(url,webServerUriType);

			return result;
		}

		/// <summary>
		/// Builds a unit test for a cookie collection.
		/// </summary>
		/// <param name="testType"> The test type.</param>
		/// <param name="cookies"> The cookie collection.</param>
		/// <returns> An edited cookie collection.</returns>
		public CookieCollection BuildUnitTestCookies(UnitTestType testType, CookieCollection cookies)
		{
			CookieCollection changedCookies = null;
			IHtmlFormUnitTest tester = null;

			// Call FillForm
			switch (testType)
			{
				case UnitTestType.BufferOverflow:
					tester = new BufferOverflowTester((BufferOverflowTesterArgs)this.Arguments);
					break;
				case UnitTestType.DataTypes:
					tester = new DataTypesTester((DataTypesTesterArgs)this.Arguments);
					break;
				case UnitTestType.SqlInjection:
					tester = new SqlInjectionTester((SqlInjectionTesterArgs)this.Arguments);
					break;
				case UnitTestType.XSS:
					tester = new XssInjectionTester((XssInjectionTesterArgs)this.Arguments);
					break;
			}
			
			if ( tester != null )
				changedCookies = tester.FillCookies(cookies);

			return changedCookies;
		}
	}
}
