// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Xml;
using System.Configuration;

namespace Ecyware.GreenBlue.Configuration.Inspector
{
	/// <summary>
	/// Contains the InspectorSectionHandler class.
	/// </summary>
	internal class InspectorSectionHandler : IConfigurationSectionHandler
	{
		#region Implementation of IConfigurationSectionHandler
		/// <summary>
		/// Creates a new configuration section handler.
		/// </summary>
		/// <param name="parent"> The parent object.</param>
		/// <param name="configContext"> The configuration context.</param>
		/// <param name="section"> The section handler.</param>
		/// <returns> An object with the configuration.</returns>
		public object Create(object parent, object configContext, System.Xml.XmlNode section)
		{
			InspectorConfiguration config = new InspectorConfiguration((InspectorConfiguration)parent);
			config.LoadValuesFromConfiguration(section);

			return config;
		}
		#endregion
	}
}
