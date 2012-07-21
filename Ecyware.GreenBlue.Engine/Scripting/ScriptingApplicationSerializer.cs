// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Configuration.XmlTypeSerializer;
using Ecyware.GreenBlue.Engine.Transforms;

namespace Ecyware.GreenBlue.Engine.Scripting
{
	/// <summary>
	/// Summary description for ScriptingApplicationSerializer.
	/// </summary>
	[ConfigurationHandler(typeof(ScriptingApplication))]
	public sealed class ScriptingApplicationSerializer : ConfigurationSection
	{
		NameValueCollection inputTransforms = new NameValueCollection();
		NameValueCollection outputTransforms = new NameValueCollection();

		/// <summary>
		/// Creates a new ScriptingApplicationSerializer.
		/// </summary>
		public ScriptingApplicationSerializer() : base()
		{
		}

		/// <summary>
		/// Loads the transform definition.
		/// </summary>
		private void LoadTransformDefinition()
		{
			// Get Configuration
			WebTransformConfiguration config = (WebTransformConfiguration)ConfigManager.Read("WebTransforms", false);

			foreach ( TransformProvider transformProvider in config.Transforms )
			{
				// Get WebTransformAttribute
				Type type = Type.GetType(transformProvider.Type);
				WebTransformAttribute attribute = (WebTransformAttribute)Attribute.GetCustomAttribute(type, typeof (WebTransformAttribute));

				transformProvider.Name = attribute.Name;
				transformProvider.TransformType = attribute.TransformProviderType;

				string[] typeName = transformProvider.Type.Split(new char[] {','})[0].Split(new char[] {'.'});
				string className = typeName[typeName.Length - 1];

				if ( transformProvider.TransformType.ToLower(System.Globalization.CultureInfo.InvariantCulture) == "input" )
				{
					inputTransforms.Add(transformProvider.Type,className);
				}

				if ( transformProvider.TransformType.ToLower(System.Globalization.CultureInfo.InvariantCulture) == "output" )
				{
					outputTransforms.Add(transformProvider.Type,className);
				}
				
			}
		}

		public override object Load(string sectionName, string fileName)
		{
			XmlDocument document = new XmlDocument();
			document.Load(fileName);

			// add EncryptedData to new document
			XmlNode node = document.DocumentElement;
			ScriptingApplication application = null;

			if ( node != null )
			{
				if ( CanDeserialize(node.OuterXml) )
				{
					application = (ScriptingApplication)this.Create(node.OuterXml);
				}
			}			

			return application;
		}

		public override void Save(object value, string sectionName, string fileName)
		{
			base.Save (value, sectionName, fileName);
		}

		/// <summary>
		/// Returns a clone of the object.
		/// </summary>
		/// <param name="sessionScripting"> The session scripting.</param>
		/// <returns> A clone of the object.</returns>
		public static object Clone(ScriptingApplication sessionScripting)
		{
			if ( serializer == null )
			{
				// Initiate serializer.
				ScriptingApplicationSerializer init = new ScriptingApplicationSerializer();
			}

			XmlNode node = serializer.WriteXmlNode(typeof(ScriptingApplication), sessionScripting, "ScriptingApplication");
			XmlNode clone = node.Clone();
			return serializer.ReadXmlNode(typeof(ScriptingApplication), clone, "ScriptingApplication");
		}

		protected override void ConfigurationSectionOverrideTypeMapping(object sender, XmlAttributeOverrideMappingArgs e)
		{
			LoadTransformDefinition();

			// Add Array Items for Tags in Ecyware.GreenBlue.Engine.HtmlDom.HtmlTagListXml
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(Ecyware.GreenBlue.Engine.HtmlDom.HtmlALinkTag),"HtmlAnchorLink", true);			
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(Ecyware.GreenBlue.Engine.HtmlDom.HtmlAnchorTag),"HtmlAnchor", true);
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(Ecyware.GreenBlue.Engine.HtmlDom.HtmlButtonTag),"HtmlButton", true);
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(Ecyware.GreenBlue.Engine.HtmlDom.HtmlInputTag),"HtmlInput", true);
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(Ecyware.GreenBlue.Engine.HtmlDom.HtmlTextAreaTag),"HtmlTextArea", true);
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(Ecyware.GreenBlue.Engine.HtmlDom.HtmlLinkTag),"HtmlLink", true);
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(Ecyware.GreenBlue.Engine.HtmlDom.HtmlSelectTag),"HtmlSelect", true);

			e.XmlMemberOverrides.Add(typeof(Ecyware.GreenBlue.Engine.HtmlDom.HtmlTagListXml), "Tags", e.XmlAttribute);
			e.XmlAttribute = e.NewXmlAttribute;


			// Add Array Items for WebRequests in ScriptingData
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(GetWebRequest), "GetSessionRequest",true);
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(PostWebRequest), "PostSessionRequest",true);
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(PutWebRequest), "PutSessionRequest",true);
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(DeleteWebRequest), "DeleteSessionRequest",true);
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(SoapHttpWebRequest), "SoapHttpSessionRequest",true);

			e.XmlMemberOverrides.Add(typeof(ScriptingApplication), "WebRequests", e.XmlAttribute);
			
			//			Attributes
			XmlTypeSerializerHelper.AddAttributeMemberMapping(e, typeof(Ecyware.GreenBlue.Engine.HtmlDom.HtmlFormTagXml), "FormIndex", "index");
			XmlTypeSerializerHelper.AddAttributeMemberMapping(e, typeof(Ecyware.GreenBlue.Engine.HtmlDom.HtmlFormTagXml), "Name", "name");	
			XmlTypeSerializerHelper.AddAttributeMemberMapping(e, typeof(Ecyware.GreenBlue.Engine.HtmlDom.HtmlFormTagXml), "Method", "method");
			XmlTypeSerializerHelper.AddAttributeMemberMapping(e, typeof(Ecyware.GreenBlue.Engine.HtmlDom.HtmlFormTagXml), "Action", "action");
			XmlTypeSerializerHelper.AddAttributeMemberMapping(e, typeof(Ecyware.GreenBlue.Engine.Scripting.WebRequest), "ID", "id");
			XmlTypeSerializerHelper.AddAttributeMemberMapping(e, typeof(Ecyware.GreenBlue.Engine.Scripting.Cookie), "Name", "name");
			XmlTypeSerializerHelper.AddAttributeMemberMapping(e, typeof(Ecyware.GreenBlue.Engine.Scripting.Header), "ApplicationID", "appid");
			
			e.XmlAttribute = e.NewXmlAttribute;
			XmlTypeSerializerHelper.AddElementMemberMapping(e.XmlAttribute, typeof(Ecyware.GreenBlue.Engine.HtmlDom.HtmlTagListXml), "Field");
			e.XmlMemberOverrides.Add(typeof(Ecyware.GreenBlue.Engine.HtmlDom.HtmlFormTagXml), "Elements", e.XmlAttribute);

			e.XmlAttribute = e.NewXmlAttribute;
			XmlTypeSerializerHelper.AddAttributeMemberMapping(e, typeof(Ecyware.GreenBlue.Engine.HtmlDom.HtmlTagListXml), "Name", "name");
			XmlTypeSerializerHelper.AddAttributeMemberMapping(e, typeof(Ecyware.GreenBlue.Engine.HtmlDom.HtmlTagBase), "Name", "name");
			XmlTypeSerializerHelper.AddAttributeMemberMapping(e, typeof(Ecyware.GreenBlue.Engine.HtmlDom.HtmlTagBase), "Type", "type");
			e.XmlAttribute = e.NewXmlAttribute;


			// Add Array Items for InputTransforms and OutputTransforms in WebRequest
			for ( int i=0;i<inputTransforms.Count;i++ )
			{
				// TODO: Needs to be qualified
				XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, Type.GetType(inputTransforms.GetKey(i)),inputTransforms[i],true);
			}

			e.XmlMemberOverrides.Add(typeof(WebRequest), "InputTransforms", e.XmlAttribute);
			e.XmlAttribute = e.NewXmlAttribute;

			for ( int i=0;i<outputTransforms.Count;i++ )
			{
				// TODO: Needs to be qualified
				XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, Type.GetType(outputTransforms.GetKey(i)),outputTransforms[i],true);
			}
			e.XmlMemberOverrides.Add(typeof(WebRequest), "OutputTransforms", e.XmlAttribute);
			
			e.XmlAttribute = e.NewXmlAttribute;

			// Add Array Items for TransformValue for each TransformAction except RemoveTransformAction
			e.XmlAttribute = e.NewXmlAttribute;
			XmlTypeSerializerHelper.AddElementMemberMapping(e.XmlAttribute, typeof(HeaderTransformValue), "HeaderTransformValue");
			XmlTypeSerializerHelper.AddElementMemberMapping(e.XmlAttribute, typeof(ClientSettingsTransformValue), "ClientSettingsTransformValue");
			XmlTypeSerializerHelper.AddElementMemberMapping(e.XmlAttribute, typeof(DefaultTransformValue), "DefaultTransformValue");
			XmlTypeSerializerHelper.AddElementMemberMapping(e.XmlAttribute, typeof(HtmlTransformValue), "HtmlTransformValue");
			XmlTypeSerializerHelper.AddElementMemberMapping(e.XmlAttribute, typeof(CookieTransformValue), "CookieTransformValue");
			XmlTypeSerializerHelper.AddElementMemberMapping(e.XmlAttribute, typeof(XPathQueryCommand), "XPathQueryCommand");
			XmlTypeSerializerHelper.AddElementMemberMapping(e.XmlAttribute, typeof(RegExQueryCommand), "RegExQueryCommand");
			
			e.XmlMemberOverrides.Add(typeof(AddTransformAction),"Value", e.XmlAttribute);
			e.XmlMemberOverrides.Add(typeof(UpdateTransformAction),"Value", e.XmlAttribute);
			e.XmlMemberOverrides.Add(typeof(FormField),"TransformValue", e.XmlAttribute);
			e.XmlMemberOverrides.Add(typeof(XmlElementField),"TransformValue", e.XmlAttribute);
			e.XmlMemberOverrides.Add(typeof(QueryCommandAction),"Value", e.XmlAttribute);

			// Add elements for Transport
			e.XmlAttribute = e.NewXmlAttribute;
			XmlTypeSerializerHelper.AddElementMemberMapping(e.XmlAttribute, typeof(SmtpTransport), "SmtpTransport");
			XmlTypeSerializerHelper.AddElementMemberMapping(e.XmlAttribute, typeof(GmailTransport), "GmailTransport");
			XmlTypeSerializerHelper.AddElementMemberMapping(e.XmlAttribute, typeof(BloggerTransport), "BloggerTransport");
			XmlTypeSerializerHelper.AddElementMemberMapping(e.XmlAttribute, typeof(DatabaseTransport), "DatabaseTransport");
			//XmlTypeSerializerHelper.AddElementMemberMapping(e.XmlAttribute, typeof(SessionTransport), "SessionTransport");

			for ( int i=0;i<outputTransforms.Count;i++ )
			{
				e.XmlMemberOverrides.Add(Type.GetType(outputTransforms.GetKey(i)),"Transport", e.XmlAttribute);
			}			

			// Add Array Items for Headers to HeaderTransformAction in HeaderTransform
			e.XmlAttribute = e.NewXmlAttribute;			
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(AddTransformAction), "AddTransformAction",true);
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(UpdateTransformAction), "UpdateTransformAction",true);
			XmlTypeSerializerHelper.AddArrayItemMemberMapping(e.XmlAttribute, typeof(RemoveTransformAction), "RemoveTransformAction",true);

			e.XmlMemberOverrides.Add(typeof(HeaderTransform), "Headers", e.XmlAttribute);
		}		

		/// <summary>
		/// Updates the serializer.
		/// </summary>
		public static void UpdateSerializer()
		{
			if ( serializer.HasCache("ScriptingApplication") )
			{
				serializer.RemoveSerializerCache("ScriptingApplication");
				serializer = new XmlTypeSerializer();
				
				// update
				// Initiate serializer.
				ScriptingApplicationSerializer init = new ScriptingApplicationSerializer();
			}
		}
	}
}
