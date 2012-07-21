// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: October 2004
using System;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Reflection;
using System.Xml;
using System.Xml.XPath;
 

namespace ConfigurationManager
{
	/// <summary>
	/// Summary description for ConfigManager.
	/// </summary>
	public sealed class ConfigManager
	{
		private static string _appConfigFile;
		private static Hashtable _handlers;
		private static bool _isInitialized = false;

		static ConfigManager()
		{
			Initialize();
			_isInitialized = true;
		}

		private static void Initialize()
		{
			try
			{
				_appConfigFile = ConfigurationManagementSettings.GetAppConfigFile();

				FileStream stream = new FileStream(_appConfigFile,FileMode.Open, FileAccess.Read);
				
				// open config file
				XPathDocument doc = new XPathDocument(new XmlTextReader(stream));
				XPathNavigator navigator = doc.CreateNavigator();

				// Nodes
				XPathExpression expr = navigator.Compile("configuration/configSections");
				XPathNodeIterator nodes = navigator.Select(expr);
				nodes.MoveNext();

				ConfigurationManagementSettings settings = new ConfigurationManagementSettings();
				settings.LoadSectionHandlers(nodes.Current);
				_handlers = settings.SectionHandlers;

				stream.Close();
			}
			catch ( Exception ex )
			{
				throw new ConfigurationException("Configuration settings error.",ex);
			}
		}

		/// <summary>
		/// Reads a section from the config.
		/// </summary>
		/// <param name="sectionName"> The configuration section name.</param>
		/// <returns></returns>
		public static object Read(string sectionName)
		{
			if ( !_isInitialized )
				throw new ConfigurationException("The configuration manager has not loaded correctly.");

			object result = null;

			if ( IsValidSection(sectionName) )
			{
				// Get Section Handler
				IConfigurationSectionHandler sectionHandler = CreateSectionHandler(sectionName);
				
				// Write
				XmlNode sectionNode = ConfigurationManagementSettings.ReadConfigNode(sectionName, _appConfigFile);

				// Serialize
				result = ((IConfigurationSectionHandler)sectionHandler).Create(null,null,sectionNode);
			}

			return result;
		}

		/// <summary>
		/// Writes the data to the config.
		/// </summary>
		/// <param name="sectionName"> The configuration section name.</param>
		/// <param name="value"> The value to write.</param>
		public static void Write(string sectionName, object value)
		{
			if ( !_isInitialized )
				throw new ConfigurationException("The configuration manager has not loaded correctly.");

			if ( IsValidSection(sectionName) )
			{
				// Get Section Handler
				IConfigurationSectionHandler sectionHandler = CreateSectionHandler(sectionName);
				
				if ( sectionHandler is IConfigurationSectionHandlerWriter )
				{
					// Serialize
					XmlNode node = ((IConfigurationSectionHandlerWriter)sectionHandler).Serialize(value);

					// Write
					ConfigurationManagementSettings.WriteConfigNode(sectionName, node, _appConfigFile);
				} 
				else 
				{
					// throw new TypeException
					throw new TypeLoadException("The type is not a IConfigurationSectionHandlerWriter type.");
				}
			}
		}

		private static IConfigurationSectionHandler CreateSectionHandler(string sectionName)
		{
			Type t = (Type)_handlers[sectionName];
			Type[] param = new Type[0];
			ConstructorInfo ci = t.GetConstructor(param);
			
			// Create a new object and return
			return (IConfigurationSectionHandler)ci.Invoke(new object[]{});
		}

		public static bool IsValidSection(string sectionName)
		{			
			bool valid = false;

			if ( _handlers[sectionName] != null )
			{
				valid = true;
			} 
			else 
			{
				valid = false;
			}

			return valid;
										
		}
	}
}
