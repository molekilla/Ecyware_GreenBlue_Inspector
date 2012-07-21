// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Net;
using System.Text;
using System.Collections;
using Ecyware.GreenBlue.Protocols.Http;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Engine.HtmlCommand;
using Ecyware.GreenBlue.WebUnitTestManager;
using Ecyware.GreenBlue.ReportEngine;

namespace Ecyware.GreenBlue.WebUnitTestCommand
{
	/// <summary>
	/// Handles the session report processing.
	/// </summary>
	public delegate void UnitTestSessionReportEventHandler(object sender, UnitTestSessionReportEventArgs e);

	/// <summary>
	/// Contains the interface for IUnitTestCommand.
	/// </summary>
	public interface IUnitTestCommand
	{
		event DisplayProcessEventHandler DisplayProcessEvent;
		event UnitTestSessionReportEventHandler CreateReportEvent;
		void Run();
		bool IsRunning
		{
			get;
		}
	}

	/// <summary>
	/// Contains the abstract class UnitTestCommand.
	/// </summary>
	public abstract class UnitTestCommand
	{
		/// <summary>
		/// Stops the execution.
		/// </summary>
		public virtual void Stop()
		{
		}

		/// <summary>
		/// Applies the test to a form.
		/// </summary>
		/// <param name="test"> The test to apply.</param>
		/// <param name="formTag"> The form.</param>
		/// <returns> A form with the new values.</returns>
		protected HtmlFormTag ApplyTestToForm(Test test, HtmlFormTag formTag)
		{
			UnitTester tester = new UnitTester(test.Arguments);
			return tester.BuildUnitTestForm(test.TestType, formTag);
		}

		/// <summary>
		/// Applies the test to cookie collection.
		/// </summary>
		/// <param name="test"> The test to apply.</param>
		/// <param name="cookies"> The cookie collection.</param>
		/// <returns> A cookie collection with new values.</returns>
		protected CookieCollection ApplyTestToCookies(Test test, CookieCollection cookies)
		{
			UnitTester tester = new UnitTester(test.Arguments);
			return tester.BuildUnitTestCookies(test.TestType, cookies);
		}

		/// <summary>
		/// Applies the test to url.
		/// </summary>
		/// <param name="test"> The test to apply.</param>
		/// <param name="webServerUriType"> The web server uri type.</param>
		/// <param name="url"> The url.</param>
		/// <returns> A new url.</returns>
		protected Uri ApplyTestToUrl(Test test, WebServerUriType webServerUriType, Uri url)
		{
			UnitTester tester = new UnitTester(test.Arguments);
			return tester.BuildUnitTestGetRequest(test.TestType, webServerUriType, url);
		}

		/// <summary>
		/// Applies the test to a form.
		/// </summary>
		/// <param name="test"> The test to apply.</param>
		/// <param name="postData"> The post data collection.</param>
		/// <returns> A form with the new values.</returns>
		protected PostDataCollection ApplyTestToPostData(Test test, PostDataCollection postData)
		{
			UnitTester tester = new UnitTester(test.Arguments);
			return tester.BuildUnitTestPostData(test.TestType, postData);
		}
	}
}
