// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: October 2004
using System;
using System.IO;
using System.Xml;
using System.Configuration;
using System.Xml.Serialization;

namespace ConfigurationManager
{
	/// <summary>
	/// Summary description for Configuration.
	/// </summary>
	public abstract class Configuration
	{
		public Configuration()
		{
		}
		/// <summary>
		/// Loads the configuration.
		/// </summary>
		/// <returns> A Configuration type.</returns>
		protected static Configuration LoadConfiguration(Type instanceType,XmlNode section)
		{
			XmlSerializer ser = new XmlSerializer(instanceType);
			Configuration cfg = (Configuration)ser.Deserialize( new XmlNodeReader( section ) );

			return cfg;
		}

		/// <summary>
		/// Loads the configuration.
		/// </summary>
		/// <param name="instanceType"> The instance type.</param>
		/// <param name="section"> The section data.</param>
		/// <param name="types"> The overrided types for the schema.</param>
		/// <returns> A Configuration type.</returns>
		protected static Configuration LoadConfiguration(Type instanceType,XmlNode section, Type[] types)
		{
			XmlSerializer ser = new XmlSerializer(instanceType, GetXmlOverrides(instanceType,types));
			Configuration cfg = (Configuration)ser.Deserialize( new XmlNodeReader( section ) );

			return cfg;
		}


		protected static XmlNode SaveConfiguration(Type instanceType, object instance, Type[] types)
		{		
			XmlSerializer ser = new XmlSerializer(instanceType, GetXmlOverrides(instance.GetType(),types));
			
			// Serialize object to xml
			StringWriter sw = new StringWriter( System.Globalization.CultureInfo.CurrentUICulture );
			SkipSerializerNamespacesWriter writer = new SkipSerializerNamespacesWriter(sw);			
			ser.Serialize(writer, instance);
			writer.Flush();

			// Return as a XmlNode
			XmlDocument doc = new XmlDocument();
			doc.LoadXml( sw.ToString() );		

			return doc.DocumentElement;

		}

		private static XmlAttributeOverrides GetXmlOverrides(Type instanceType, Type[] types)
		{
			XmlAttributes attrs = new XmlAttributes();
			
			foreach ( Type t in types )
			{
				attrs.XmlArrayItems.Add( new XmlArrayItemAttribute(t) );
			}
			
			XmlAttributeOverrides attrOverrides = new XmlAttributeOverrides();
			attrOverrides.Add(instanceType,"Providers",attrs);

			return attrOverrides;
		}
	}
}
