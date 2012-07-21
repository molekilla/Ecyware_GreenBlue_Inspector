// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;

namespace Ecyware.GreenBlue.Configuration.Inspector
{
	/// <summary>
	/// Contains properties for locating the configuration sections.
	/// </summary>
	public sealed class ConfigurationSettingsLocator
	{

		private ConfigurationSettingsLocator()
		{
		}
		/// <summary>
		/// Gets the application configuration settings.
		/// </summary>
		public static InspectorConfiguration InspectorSettings
		{
			get
			{
				return (InspectorConfiguration)System.Configuration.ConfigurationSettings.GetConfig("inspector/inspectorSettings");
			}
		}

		/// <summary>
		/// Gets the HTTP Client Configuration settings.
		/// </summary>
		public static HttpClientConfiguration HttpClientSettings
		{
			get
			{
				return (HttpClientConfiguration)System.Configuration.ConfigurationSettings.GetConfig("inspector/httpClientSettings");
			}
		}
	}
}
