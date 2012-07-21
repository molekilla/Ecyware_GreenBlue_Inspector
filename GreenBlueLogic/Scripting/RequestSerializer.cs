// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using System.Xml;
using System.Configuration;
using Ecyware.GreenBlue.HtmlDom;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Configuration.XmlTypeSerializer;
using Ecyware.GreenBlue.Protocols.Http.Transforms;

namespace Ecyware.GreenBlue.Protocols.Http.Scripting
{
	/// <summary>
	/// Summary description for SessionRequestSerializer.
	/// </summary>
	public class RequestSerializer : IConfigurationSectionHandler, IConfigurationSectionHandlerWriter
	{
		private static XmlTypeSerializer ser = null;
		// private static WebTransformConfiguration transformConfig;

		public RequestSerializer()
		{
			if ( ser == null )
			{
				ser = new XmlTypeSerializer();
				ser.XmlAttributeOverrideMappingEvent += new XmlAttributeOverrideMappingHandler(ser_XmlAttributeOverrideMappingEvent);
				ser.AddSerializerCache(typeof(WebRequest), "WebRequest");
			}
		}

		/// <summary>
		/// Returns a clone of the object.
		/// </summary>
		/// <param name="request"> The session scripting.</param>
		/// <returns> A clone of the object.</returns>
		public static object Clone(WebRequest request)
		{
			if ( ser == null )
			{
				RequestSerializer _ser = new RequestSerializer();
			}

			XmlNode node = ser.WriteXmlNode(typeof(WebRequest), request, "WebRequest");
			XmlNode clone = node.Clone();
			return ser.ReadXmlNode(typeof(WebRequest), clone, "WebRequest");
		}

		/// <summary>
		/// Checks if the XML can deserialize.
		/// </summary>
		/// <param name="section"> The XML string.</param>
		/// <returns> Returns true if it can be deserialize, else false.</returns>
		public static bool CanDeserialize(string section)
		{
			return ser.CanDeserialize(typeof(WebRequest), section, "WebRequest");
		}
		#region IConfigurationSectionHandler Members

		public object Create(object parent, object configContext, XmlNode section)
		{			
			return ser.ReadXmlNode(typeof(WebRequest), section, "WebRequest");
			
		}

		public object Create(string section)
		{
			return ser.ReadXmlString(typeof(WebRequest), section, "WebRequest");
		}
		#endregion

		#region IConfigurationSectionHandlerWriter Members

		public XmlNode Serialize(object value)
		{
			return ser.WriteXmlNode(typeof(WebRequest), value, "WebRequest");
		}

		#endregion

		private void ser_XmlAttributeOverrideMappingEvent(object sender, XmlAttributeOverrideMappingArgs e)
		{			
			// SessionApplication Namespace
			// http://schemas.ecyware.com/2005/01/Ecyware-GreenBlue-ScriptingApplication
			string namesp = "http://schemas.ecyware.com/2005/01/Ecyware-GreenBlue-ScriptingApplication";
			XmlTypeSerializerHelper.AddTypeMemberMapping(e, typeof(Ecyware.GreenBlue.Protocols.Http.Scripting.ScriptingApplication),"ScriptingApplication",namesp);

			e.XmlAttribute = e.NewXmlAttribute;

			// Add Array Items for Tags in Ecyware.GreenBlue.HtmlDom.HtmlTagListXml
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(Ecyware.GreenBlue.HtmlDom.HtmlALinkTag),"HtmlAnchorLink", true);			
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(Ecyware.GreenBlue.HtmlDom.HtmlAnchorTag),"HtmlAnchor", true);
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(Ecyware.GreenBlue.HtmlDom.HtmlButtonTag),"HtmlButton", true);
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(Ecyware.GreenBlue.HtmlDom.HtmlInputTag),"HtmlInput", true);
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(Ecyware.GreenBlue.HtmlDom.HtmlTextAreaTag),"HtmlTextArea", true);
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(Ecyware.GreenBlue.HtmlDom.HtmlLinkTag),"HtmlLink", true);
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(Ecyware.GreenBlue.HtmlDom.HtmlSelectTag),"HtmlSelect", true);			

			e.XmlMemberOverrides.Add(typeof(Ecyware.GreenBlue.HtmlDom.HtmlTagListXml), "Tags", e.XmlAttribute);
			e.XmlAttribute = e.NewXmlAttribute;


			// Add Element member mapping for WebRequest
			XmlTypeSerializerHelper.AddElementMemberMapping(e.XmlAttribute, typeof(GetWebRequest), "GetSessionRequest");
			XmlTypeSerializerHelper.AddElementMemberMapping(e.XmlAttribute, typeof(PostWebRequest), "PostSessionRequest");
			e.XmlAttribute.XmlRoot =  new System.Xml.Serialization.XmlRootAttribute();
			e.XmlAttribute.XmlRoot.ElementName = "WebRequest";

			e.XmlMemberOverrides.Add(typeof(WebRequest), e.XmlAttribute);
			
			//			Attributes
			XmlTypeSerializerHelper.AddAttributeMemberMapping(e, typeof(Ecyware.GreenBlue.HtmlDom.HtmlFormTagXml), "FormIndex", "index");
			XmlTypeSerializerHelper.AddAttributeMemberMapping(e, typeof(Ecyware.GreenBlue.HtmlDom.HtmlFormTagXml), "Name", "name");	
			XmlTypeSerializerHelper.AddAttributeMemberMapping(e, typeof(Ecyware.GreenBlue.HtmlDom.HtmlFormTagXml), "Method", "method");
			XmlTypeSerializerHelper.AddAttributeMemberMapping(e, typeof(Ecyware.GreenBlue.HtmlDom.HtmlFormTagXml), "Action", "action");
			XmlTypeSerializerHelper.AddAttributeMemberMapping(e, typeof(Ecyware.GreenBlue.Protocols.Http.Scripting.WebRequest), "ID", "id");
			XmlTypeSerializerHelper.AddAttributeMemberMapping(e, typeof(Ecyware.GreenBlue.Protocols.Http.Scripting.Cookie), "Name", "name");
			
			e.XmlAttribute = e.NewXmlAttribute;
			XmlTypeSerializerHelper.AddElementMemberMapping(e.XmlAttribute, typeof(Ecyware.GreenBlue.HtmlDom.HtmlTagListXml), "Field");
			e.XmlMemberOverrides.Add(typeof(Ecyware.GreenBlue.HtmlDom.HtmlFormTagXml), "Elements", e.XmlAttribute);

			e.XmlAttribute = e.NewXmlAttribute;
			XmlTypeSerializerHelper.AddAttributeMemberMapping(e, typeof(Ecyware.GreenBlue.HtmlDom.HtmlTagListXml), "Name", "name");
			XmlTypeSerializerHelper.AddAttributeMemberMapping(e, typeof(Ecyware.GreenBlue.HtmlDom.HtmlTagBase), "Name", "name");
			XmlTypeSerializerHelper.AddAttributeMemberMapping(e, typeof(Ecyware.GreenBlue.HtmlDom.HtmlTagBase), "Type", "type");


			e.XmlAttribute = e.NewXmlAttribute;

			//			foreach ( TransformProvider transformProvider in transformConfig.Transforms )
			//			{
			//				string[] typeName = transformProvider.Type.Split(new char[] {','})[0].Split(new char[] {','});
			//				string className = typeName[typeName.Length - 1];
			//				XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, Type.GetType(transformProvider.Type), className, true);
			//			}
			// Add Array Items for InputTransforms and OutputTransforms in WebRequest
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(HeaderTransform), "HeaderTransform",true);
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(RequestTransform), "RequestTransform",true);
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(ResponseResultTransform), "ResponseResultTransform",true);
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(FillFormTransform), "FillFormTransform",true);

			e.XmlMemberOverrides.Add(typeof(WebRequest), "InputTransforms", e.XmlAttribute);
			e.XmlMemberOverrides.Add(typeof(WebRequest), "OutputTransforms", e.XmlAttribute);


			e.XmlAttribute = e.NewXmlAttribute;

			// Add Array Items for TransformValue for each TransformAction except RemoveTransformAction
			e.XmlAttribute = e.NewXmlAttribute;
			XmlTypeSerializerHelper.AddElementMemberMapping(e.XmlAttribute, typeof(HeaderTransformValue), "HeaderTransformValue");
			XmlTypeSerializerHelper.AddElementMemberMapping(e.XmlAttribute, typeof(ClientSettingsTransformValue), "ClientSettingsTransformValue");
			XmlTypeSerializerHelper.AddElementMemberMapping(e.XmlAttribute, typeof(DefaultTransformValue), "DefaultTransformValue");
			XmlTypeSerializerHelper.AddElementMemberMapping(e.XmlAttribute, typeof(HtmlTransformValue), "HtmlTransformValue");
			
			e.XmlMemberOverrides.Add(typeof(AddTransformAction),"Value", e.XmlAttribute);
			e.XmlMemberOverrides.Add(typeof(UpdateTransformAction),"Value", e.XmlAttribute);
			e.XmlMemberOverrides.Add(typeof(FormField),"TransformValue", e.XmlAttribute);


			// Add Array Items for Headers to HeaderTransformAction in HeaderTransform
			e.XmlAttribute = e.NewXmlAttribute;			
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(AddTransformAction), "AddTransformAction",true);
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(UpdateTransformAction), "UpdateTransformAction",true);
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(RemoveTransformAction), "RemoveTransformAction",true);

			e.XmlMemberOverrides.Add(typeof(HeaderTransform), "Headers", e.XmlAttribute);

			//e.XmlMemberOverrides.Add(typeof(HeaderTransform), "Headers", e.XmlAttribute);

		}
	}
}
