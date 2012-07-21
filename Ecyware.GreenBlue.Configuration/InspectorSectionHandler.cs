// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Xml;
using System.Configuration;

namespace Ecyware.GreenBlue.Configuration
{
	/// <summary>
	/// Contains the InspectorSectionHandler class.
	/// </summary>
	internal class InspectorSectionHandler : IConfigurationSectionHandler, IConfigurationSectionHandlerWriter
	{
		#region IConfigurationSectionHandler Members

		public object Create(object parent, object configContext, XmlNode section)
		{
			InspectorConfiguration cfg = InspectorConfiguration.LoadConfiguration(section.SelectSingleNode("inspectorConfiguration"));
			return cfg;
		}
		#endregion

		#region IConfigurationSectionHandlerWriter Members

		public XmlNode Serialize(object value)
		{
			return InspectorConfiguration.SaveConfiguration(value,null);
//			XmlSerializer ser = new XmlSerializer(value.GetType());
//			
//			// Serialize object to xml
//			StringWriter sw = new StringWriter( System.Globalization.CultureInfo.CurrentUICulture );
//			XmlTextWriter writer = new XmlTextWriter( sw );			
//			ser.Serialize( writer, value );
//			writer.Flush();
//
//			// Return as a XmlNode
//			XmlDocument doc = new XmlDocument();
//			doc.LoadXml( writer.ToString() );
//			return doc.DocumentElement;
		}

		#endregion
	}
}
