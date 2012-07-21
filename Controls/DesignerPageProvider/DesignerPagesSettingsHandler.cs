// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using System.Xml;
using System.Configuration;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Configuration.XmlTypeSerializer;

namespace Ecyware.GreenBlue.Controls.DesignerPageProvider
{
	/// <summary>
	/// Summary description for DesignerPagesSettingsHandler.
	/// </summary>
	public class DesignerPagesSettingsHandler : IConfigurationSectionHandler, IConfigurationSectionHandlerWriter
	{
		private static XmlTypeSerializer ser = null;

		/// <summary>
		/// Creates a new DesignerPageSettingsHandler.
		/// </summary>
		public DesignerPagesSettingsHandler()
		{
			if ( ser == null )
			{
				ser = new XmlTypeSerializer();
				ser.XmlAttributeOverrideMappingEvent += new XmlAttributeOverrideMappingHandler(ser_XmlAttributeOverrideMappingEvent);
				ser.AddSerializerCache(typeof(DesignerPagesConfiguration), "DesignerPagesConfiguration");
			}
		}

		#region IConfigurationSectionHandler Members

		public object Create(object parent, object configContext, XmlNode section)
		{			
			return ser.ReadXmlNode(typeof(DesignerPagesConfiguration), section.FirstChild, "DesignerPagesConfiguration");
			
		}
		#endregion

		#region IConfigurationSectionHandlerWriter Members

		public XmlNode Serialize(object value)
		{
			return ser.WriteXmlNode(typeof(DesignerPagesConfiguration), value, "DesignerPagesConfiguration");
		}

		#endregion

		private void ser_XmlAttributeOverrideMappingEvent(object sender, XmlAttributeOverrideMappingArgs e)
		{			
			// Add Array Items for Pages in DesignerPages
			//XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(DesignerPage), "DesignerPage",true);			
			//e.XmlMemberOverrides.Add(typeof(DesignerPagesConfiguration), "Pages", e.XmlAttribute);
			XmlTypeSerializerHelper.AddAttributeMemberMapping(e, typeof(DesignerPage), "Name", "name");
			XmlTypeSerializerHelper.AddAttributeMemberMapping(e, typeof(DesignerPage), "Type", "type");
		}
	}
}
