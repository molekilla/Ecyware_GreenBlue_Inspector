// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: October 2004
using System;
using System.IO;
using System.Xml;
using System.Configuration;
using System.Xml.Serialization;

namespace Ecyware.GreenBlue.Configuration
{
	/// <summary>
	/// Summary description for SimpleConfigurationHandler.
	/// </summary>
	public class SimpleSectionHandler : IConfigurationSectionHandler, IConfigurationSectionHandlerWriter
	{
		public SimpleSectionHandler()
		{
		}
	
		#region IConfigurationSectionHandler Members

		public object Create(object parent, object configContext, XmlNode section)
		{
			SimpleConfiguration cfg = SimpleConfiguration.LoadConfiguration(section.SelectSingleNode("SimpleConfig"));
			return cfg;
		}
		#endregion

		#region IConfigurationSectionHandlerWriter Members

		public XmlNode Serialize(object value)
		{
			XmlSerializer ser = new XmlSerializer(value.GetType());
			
			// Serialize object to xml
			StringWriter sw = new StringWriter( System.Globalization.CultureInfo.CurrentUICulture );
			XmlTextWriter writer = new XmlTextWriter( sw );			
			ser.Serialize( writer, value );
			writer.Flush();

			// Return as a XmlNode
			XmlDocument doc = new XmlDocument();
			doc.LoadXml( writer.ToString() );
			return doc.DocumentElement;
		}

		#endregion
	}
}
