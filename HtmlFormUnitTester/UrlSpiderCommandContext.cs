// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: July 2004
using System;
using System.Net;
using System.Text;
using System.Collections;
using Ecyware.GreenBlue.HtmlDom;
using Ecyware.GreenBlue.Protocols.Http;
using Ecyware.GreenBlue.HtmlCommand;
using Ecyware.GreenBlue.WebUnitTestManager;
using Ecyware.GreenBlue.ReportEngine;


namespace Ecyware.GreenBlue.WebUnitTestCommand
{
	/// <summary>
	/// Creates a new UrlSpiderCommandContext type.
	/// </summary>
	public class UrlSpiderCommandContext : CommandContext
	{
		TestCollection _unitTestCollection;
		/// <summary>
		/// Creates a new UrlSpiderCommandContext.
		/// </summary>
		public UrlSpiderCommandContext()
		{
		}

		/// <summary>
		/// Gets or sets the test collection.
		/// </summary>
		public TestCollection UnitTestCollection
		{
			get
			{
				return _unitTestCollection;
			}
			set
			{
				_unitTestCollection = value;
			}

		}
	}
}
