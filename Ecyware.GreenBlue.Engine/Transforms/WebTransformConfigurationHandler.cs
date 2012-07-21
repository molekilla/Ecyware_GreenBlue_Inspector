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

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for WebTransformConfigurationHandler.
	/// </summary>
	public class WebTransformConfigurationHandler : IConfigurationSectionHandler, IConfigurationSectionHandlerWriter
	{
		private static XmlTypeSerializer ser = null;

		/// <summary>
		/// Creates a new DesignerPageSettingsHandler.
		/// </summary>
		public WebTransformConfigurationHandler()
		{
			if ( ser == null )
			{
				ser = new XmlTypeSerializer();
				ser.XmlAttributeOverrideMappingEvent += new XmlAttributeOverrideMappingHandler(ser_XmlAttributeOverrideMappingEvent);
				ser.AddSerializerCache(typeof(WebTransformConfiguration), "WebTransformConfiguration");
			}
		}

		#region IConfigurationSectionHandler Members

		public object Create(object parent, object configContext, XmlNode section)
		{			
			return ser.ReadXmlNode(typeof(WebTransformConfiguration), section.FirstChild, "WebTransformConfiguration");
			
		}
		#endregion

		#region IConfigurationSectionHandlerWriter Members

		public XmlNode Serialize(object value)
		{
			return ser.WriteXmlNode(typeof(WebTransformConfiguration), value, "WebTransformConfiguration");
		}

		#endregion

		private void ser_XmlAttributeOverrideMappingEvent(object sender, XmlAttributeOverrideMappingArgs e)
		{			
			// Add Array Items for Pages in DesignerPages
			//XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(DesignerPage), "DesignerPage",true);			
			//e.XmlMemberOverrides.Add(typeof(DesignerPagesConfiguration), "Pages", e.XmlAttribute);
			XmlTypeSerializerHelper.AddAttributeMemberMapping(e, typeof(TransformProvider), "Name", "name");
			XmlTypeSerializerHelper.AddAttributeMemberMapping(e, typeof(TransformProvider), "Type", "type");
			XmlTypeSerializerHelper.AddAttributeMemberMapping(e, typeof(TransformProvider), "TransformType", "transformType");

			e.XmlAttribute = e.NewXmlAttribute;
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(TransformProvider), "Transform", true);
			e.XmlMemberOverrides.Add(typeof(WebTransformConfiguration), "Transforms", e.XmlAttribute);
		}
	}
}
