// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: July 2004
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
	/// Contains the definition for the CommandContext type.
	/// </summary>
	public class CommandContext
	{
		private HttpProxy _proxy = null;
		private HttpProperties _httpProperties = null;

		/// <summary>
		/// Creates a new CommandContext.
		/// </summary>
		public CommandContext()
		{
		}

		/// <summary>
		/// Gets or sets the HttpProperties.
		/// </summary>
		public HttpProperties ProtocolProperties
		{
			get
			{
				return _httpProperties;
			}
			set
			{
				_httpProperties = value;
			}
		}

		/// <summary>
		/// Gets or sets the HttpProxy.
		/// </summary>
		public HttpProxy Proxy
		{
			get
			{
				return _proxy;
			}
			set
			{
				_proxy = value;
			}
		}

	}
}
