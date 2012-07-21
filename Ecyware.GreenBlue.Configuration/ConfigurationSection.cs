// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: October 2004
using System;
using System.Configuration;
using System.Xml;
using System.Reflection;
using Ecyware.GreenBlue.Configuration.XmlTypeSerializer;

namespace Ecyware.GreenBlue.Configuration
{
	/// <summary>
	/// Contains the code for implementing configuration sections in configuration files or XML files.
	/// </summary>
	public class ConfigurationSection : IConfigurationSectionHandler,IConfigurationSectionHandlerWriter
	{
		protected static Ecyware.GreenBlue.Configuration.XmlTypeSerializer.XmlTypeSerializer serializer = new XmlTypeSerializer.XmlTypeSerializer();           
            
		/// <summary>
		/// Creates a new ConfigurationSection.
		/// </summary>
		public ConfigurationSection()
		{
			ConfigurationHandlerAttribute at = (ConfigurationHandlerAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof (ConfigurationHandlerAttribute));
			if ( !serializer.HasCache(at.ConfigurationType.Name) )
			{                      
				serializer.XmlAttributeOverrideMappingEvent     += new XmlAttributeOverrideMappingHandler(ConfigurationSectionOverrideTypeMapping);                    
				serializer.AddSerializerCache(at.ConfigurationType,at.ConfigurationType.Name);
			}
		}

      

		#region IConfigurationSectionHandler Members

		/// <summary>
		/// Reads the configuration from a config file or XML.
		/// </summary>
		/// <param name="parent"> The parent object.</param>
		/// <param name="configContext"> The configuration context.</param>
		/// <param name="section"> The XmlNode section.</param>
		/// <returns> A serialized object.</returns>
		public virtual object Create(object parent, object configContext, XmlNode section)
		{           
			ConfigurationHandlerAttribute at = (ConfigurationHandlerAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof (ConfigurationHandlerAttribute));   
			object s = serializer.ReadXmlNode(at.ConfigurationType, section.FirstChild, at.ConfigurationType.Name);
			return s;
		}

		/// <summary>
		/// Creates the object from a XML, not configuration compatible.
		/// </summary>
		/// <param name="xmlString"> The Xml string.</param>
		/// <returns> A serialized object.</returns>
		public virtual object Create(string xmlString)
		{           
			ConfigurationHandlerAttribute at = (ConfigurationHandlerAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof (ConfigurationHandlerAttribute));   
			object s = serializer.ReadXmlString(at.ConfigurationType, xmlString, at.ConfigurationType.Name);
			return s;
		}
		#endregion

		#region IConfigurationSectionHandlerWriter Members
 
		/// <summary>
		/// Serializes the object.
		/// </summary>
		/// <param name="value"> The object to serialize.</param>
		/// <returns> Returns a XmlNode type.</returns>
		public virtual XmlNode Serialize(object value)
		{
			ConfigurationHandlerAttribute at = (ConfigurationHandlerAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof (ConfigurationHandlerAttribute));   
			XmlNode node = serializer.WriteXmlNode(at.ConfigurationType, value, at.ConfigurationType.Name, false);
			return node;            
		}

		/// <summary>
		/// Serializes the object.
		/// </summary>
		/// <param name="value"> The object to serialize.</param>
		/// <param name="useNamespaces"> Use namespaces.</param>
		/// <returns> Returns a XmlNode type.</returns>
		public virtual XmlNode Serialize(object value, bool useNamespaces)
		{
			ConfigurationHandlerAttribute at = (ConfigurationHandlerAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof (ConfigurationHandlerAttribute));   
			XmlNode node = serializer.WriteXmlNode(at.ConfigurationType, value, at.ConfigurationType.Name, useNamespaces);
			return node;            
		}
		#endregion

		/// <summary>
		/// Checks if the XML can deserialize.
		/// </summary>
		/// <param name="section"> The XML string.</param>
		/// <returns> Returns true if it can be deserialize, else false.</returns>
		public virtual bool CanDeserialize(string section)
		{
			ConfigurationHandlerAttribute at = (ConfigurationHandlerAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof (ConfigurationHandlerAttribute));
			return serializer.CanDeserialize(section, at.ConfigurationType.Name);
		}

		/// <summary>
		/// Saves the configuration.
		/// </summary>
		/// <param name="value"> The object to save.</param>
		/// <param name="sectionName"> The section name to save to.</param>
		/// <param name="fileName"> The file name of the configuration or XML. Leave empty to use default configuration.</param>
		public virtual void Save(object value, string sectionName, string fileName)
		{                 
			string file;
			if ( fileName.Length == 0 )
			{
				ConfigurationManagementSettings cms = new ConfigurationManagementSettings();
				file = cms.GetAppConfigFile();
			} 
			else 
			{
				file = fileName;
			}

			// Serialize
			XmlNode node = this.Serialize(value);

			// Write
			ConfigurationManagementSettings.WriteConfigNode(sectionName, node, file);
		}

            
		/// <summary>
		/// Loads a configuration section from a configuration file or XML.
		/// </summary>
		/// <param name="sectionName"> The section name to load.</param>
		/// <param name="fileName"> The file name of the configuration or XML. Leave empty to use default configuration.</param>
		public virtual object Load(string sectionName, string fileName)
		{
			string file;
			if ( fileName.Length == 0 )
			{
				ConfigurationManagementSettings cms = new ConfigurationManagementSettings();
				file = cms.GetAppConfigFile();
			} 
			else 
			{
				file = fileName;
			}

			// Read
			XmlNode sectionNode = ConfigurationManagementSettings.ReadConfigNode(sectionName, file);
			object result;

			// Serialize
			if ( sectionNode == null )
			{
				result = null;
			} 
			else 
			{                       
				result = this.Create(null, null, sectionNode);
			} 

			return result;
		}

 

		/// <summary>
		/// Contains the configuration section type mapping overrides.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The EventArgs arguments.</param>
		protected virtual void ConfigurationSectionOverrideTypeMapping(object sender, XmlAttributeOverrideMappingArgs e)
		{
		}
	}

}

