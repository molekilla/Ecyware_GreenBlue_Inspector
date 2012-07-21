// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Xml;
using System.Configuration;
using Ecyware.GreenBlue.Configuration;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Contains the HttpClientSectionHandler class.
	/// </summary>
	public class HttpClientSectionHandler : IConfigurationSectionHandler, IConfigurationSectionHandlerWriter
	{
		/// <summary>
		/// Creates a new HttpSectionHandler.
		/// </summary>
		public HttpClientSectionHandler()
		{
		}

		#region IConfigurationSectionHandler Members

		public object Create(object parent, object configContext, XmlNode section)
		{
			HttpProperties cfg = HttpProperties.LoadConfiguration(section.SelectSingleNode("httpProperties"));
			return cfg;
		}
		#endregion

		#region IConfigurationSectionHandlerWriter Members

		public XmlNode Serialize(object value)
		{
			return HttpProperties.SaveConfiguration(value, null);
		}

		#endregion
	}
}
