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
using System.Web.Caching;
 

namespace Ecyware.GreenBlue.Configuration
{

	/// <summary>
	/// Summary description for ConfigManager.
	/// </summary>
	public sealed class ConfigManager
	{
		private static Hashtable _internalCache;
		private static string _appConfigFile;
		private static Hashtable _handlers;
		private static bool _isInitialized = false;

		static ConfigManager()
		{				
			_internalCache = new Hashtable(10);
			ConfigurationManagementSettings cms = new ConfigurationManagementSettings();
			_appConfigFile = cms.GetAppConfigFile();
			Initialize();
			_isInitialized = true;
		}

		private static void Initialize()
		{
			try
			{												
				FileStream stream = new FileStream(_appConfigFile,FileMode.Open, FileAccess.Read);
				
				// open config file
				XPathDocument doc = new XPathDocument(new XmlTextReader(stream));
				XPathNavigator navigator = doc.CreateNavigator();

				// Nodes
				XPathExpression expr = navigator.Compile("configuration/configSections");
				XPathNodeIterator nodes = navigator.Select(expr);

				if ( nodes.Count > 0 )
				{
					nodes.MoveNext();

					// Load Section Handlers
					ConfigurationManagementSettings settings = new ConfigurationManagementSettings();
					settings.LoadSectionHandlers(nodes.Current);
					_handlers = settings.SectionHandlers;
				}

				stream.Close();
			}
			catch ( Exception ex )
			{
				throw new ConfigurationException("Configuration settings error.",ex);
			}
		}

		/// <summary>
		/// Overrides the loaded section handlers.
		/// </summary>
		/// <param name="handlers"> A Hashtable containing the handlers by name , type.</param>
		/// <example>
		/// Hashtable handlers = new Hashtable();
		/// handlers.Add("gsi.epower",Type.GetType(type));
		/// ConfigManager.SetSectionHandlersOverrides(handlers);
		/// </example>
		public static void SetSectionHandlersOverrides(Hashtable handlers)
		{
			// Load Section Handlers
			foreach ( DictionaryEntry de in handlers )
			{
				if ( !_handlers.ContainsKey(de.Key) )
				{
					_handlers.Add(de.Key, de.Value);
				}
			}
		}

		/// <summary>
		/// Overrides the default configuration file path.
		/// </summary>
		/// <param name="configurationFilePath"> The file path to the configuration file.</param>
		public static void SetConfigurationFilePathOverrides(string configurationFilePath)
		{
			_appConfigFile = configurationFilePath;
		}


		/// <summary>
		/// Reads a section from the config.
		/// </summary>
		/// <param name="sectionName"> The configuration section name.</param>
		/// <param name="caller"> The type calling this method. This type is use for config file location.</param>
		/// <param name="useCache"> Sets the ConfigManager to save the configuration object in the cache store.</param>
		/// <returns> The configuration object.</returns>		
		public static object Read(string sectionName, Type caller, bool useCache)
		{						
			ConfigurationManagementSettings cms = new ConfigurationManagementSettings();
			_appConfigFile = cms.GetAppConfigFile(caller.Assembly.Location);			
			Initialize();
			_isInitialized = true;

			if ( useCache )
			{				
				// Cache
				if ( _internalCache[sectionName] == null )
				{
					_internalCache.Add(sectionName,Read(sectionName));
				}

				return _internalCache[sectionName];
			} 
			else 
			{
				// no cache
				return Read(sectionName);
			}
		}

		/// <summary>
		/// Reads a section from the config.
		/// </summary>
		/// <param name="sectionName"> The configuration section name.</param>
		/// <param name="caller"> The type calling this method. This type is use for config file location.</param>
		/// <param name="useCache"> Sets the ConfigManager to save the configuration object in the cache store.</param>
		/// <returns> The configuration object.</returns>		
		public static object Read(string sectionName, bool useCache)
		{						
			if ( useCache )
			{				
				// Cache
				if ( _internalCache[sectionName] == null )
				{
					_internalCache.Add(sectionName,Read(sectionName));
				}

				return _internalCache[sectionName];
			} 
			else 
			{
				// no cache
				return Read(sectionName);
			}
		}

		/// <summary>
		/// Reads a section from the config.
		/// </summary>
		/// <param name="sectionName"> The configuration section name.</param>
		/// <returns> The configuration object.</returns>
		private static object Read(string sectionName)
		{
			if ( !_isInitialized )
				throw new ConfigurationException("The configuration manager has not loaded correctly.");

			object result = null;

			if ( IsValidSection(sectionName) )
			{
				// Get Section Handler
				IConfigurationSectionHandler sectionHandler = CreateSectionHandler(sectionName);
				
				// Read
				XmlNode sectionNode = ConfigurationManagementSettings.ReadConfigNode(sectionName, _appConfigFile);

				if ( sectionNode == null )
				{
					result = null;
				} 
				else 
				{
					// Serialize
					result = ((IConfigurationSectionHandler)sectionHandler).Create(null,null,sectionNode);
				}
			}

			return result;
		}


		/// <summary>
		/// Reads a section from the config.
		/// </summary>
		/// <param name="sectionName"> The configuration section name.</param>
		/// <returns> A XmlNode type.</returns>
		public static XmlNode ReadXmlNode(string sectionName)
		{
			if ( !_isInitialized )
				throw new ConfigurationException("The configuration manager has not loaded correctly.");

			XmlNode result = null;

			if ( IsValidSection(sectionName) )
			{
				// Get Section Handler
				IConfigurationSectionHandler sectionHandler = CreateSectionHandler(sectionName);
				
				// Read
				result = ConfigurationManagementSettings.ReadConfigNode(sectionName, _appConfigFile);

				// Serialize
				// result = ((IConfigurationSectionHandler)sectionHandler).Create(null,null,sectionNode);
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

		/// <summary>
		/// Writes the data to a XmlNode.
		/// </summary>
		/// <param name="sectionName"> The configuration section name.</param>
		/// <param name="value"> The value to write.</param>
		public static XmlNode WriteXmlNode(string sectionName, object value)
		{
			XmlNode node = null;

			if ( !_isInitialized )
				throw new ConfigurationException("The configuration manager has not loaded correctly.");

			if ( IsValidSection(sectionName) )
			{
				// Get Section Handler
				IConfigurationSectionHandler sectionHandler = CreateSectionHandler(sectionName);
				
				if ( sectionHandler is IConfigurationSectionHandlerWriter )
				{
					// Serialize
					node = ((IConfigurationSectionHandlerWriter)sectionHandler).Serialize(value);

					// Write
					//ConfigurationManagementSettings.WriteConfigNode(sectionName, node, _appConfigFile);
				} 
				else 
				{
					// throw new TypeException
					throw new TypeLoadException("The type is not a IConfigurationSectionHandlerWriter type.");
				}
			}

			return node;
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
