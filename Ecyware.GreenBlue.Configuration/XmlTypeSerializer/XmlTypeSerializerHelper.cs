// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: October 2004
using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;


namespace Ecyware.GreenBlue.Configuration.XmlTypeSerializer
{
	/// <summary>
	/// Summary description for XmlTypeSerializerHelper.
	/// </summary>
	public class XmlTypeSerializerHelper
	{	
		private XmlTypeSerializerHelper()
		{			
			//			at.XmlAnyAttribute;
			//			at.XmlAnyElements;
			//			at.XmlArray;
			//			at.XmlArrayItems;
			//			at.XmlAttribute;
			//			at.XmlChoiceIdentifier;
			//			at.XmlDefaultValue;
			//			at.XmlElements;
			//			at.XmlEnum;
			//			at.XmlIgnore;
			//			at.Xmlns;
			//			at.XmlRoot;
			//			at.XmlText;
			//			at.XmlType;
		}


		public static void AddMemberMapping(XmlAttributeOverrideMappingArgs mappings, string member)
		{
			mappings.XmlMemberOverrides.Add(mappings.Type, member, mappings.XmlAttribute);
		}

		public static void AddMapping(XmlAttributeOverrideMappingArgs mappings)
		{
			mappings.XmlMemberOverrides.Add(mappings.Type, mappings.XmlAttribute);
		}

		private static XmlArrayItemAttribute AddArrayItemMemberMappingInternal(Type type, string elementName, bool isNullable)
		{
			XmlArrayItemAttribute arrayItem = new XmlArrayItemAttribute();
			arrayItem.ElementName = elementName;
			arrayItem.IsNullable = isNullable;
			arrayItem.Type = type;
			return arrayItem;
		}

		public static void AddArrayItemMemberMapping(XmlAttributes at, Type type)
		{			
			at.XmlArrayItems.Add(new XmlArrayItemAttribute(type));
		}

		public static void AddArrayItemMemberMapping(XmlAttributes at, Type type, string elementName, bool isNullable)
		{
			XmlArrayItemAttribute arrayItem = AddArrayItemMemberMappingInternal(type, elementName, isNullable);
			at.XmlArrayItems.Add(arrayItem);
		}

		public static void AddArrayItemMemberMapping(XmlAttributes at, Type type, string elementName, bool isNullable, string elementNamespace, bool isQualified)
		{
			XmlArrayItemAttribute arrayItem = AddArrayItemMemberMappingInternal(type, elementName, isNullable);
			arrayItem.Namespace = elementNamespace;

			if ( isQualified )
			{
				arrayItem.Form = XmlSchemaForm.Qualified;
			} 
			else 
			{
				arrayItem.Form = XmlSchemaForm.Unqualified;
			}
			at.XmlArrayItems.Add(arrayItem);
		}

		public static void AddArrayItemMemberMapping(XmlAttributes at, Type type, string elementName, bool isNullable, string elementNamespace, bool isQualified, int nestingLevel)
		{
			XmlArrayItemAttribute arrayItem = AddArrayItemMemberMappingInternal(type, elementName, isNullable);
			if ( isQualified )
			{
				arrayItem.Form = XmlSchemaForm.Qualified;
			} 
			else 
			{
				arrayItem.Form = XmlSchemaForm.Unqualified;
			}
			arrayItem.NestingLevel = nestingLevel;	
			at.XmlArrayItems.Add(arrayItem);		
		}


		public static void AddElementMemberMapping(XmlAttributes at, Type type, string elementName)
		{
			at.XmlElements.Add( new XmlElementAttribute(elementName,type) );
		}

		public static void AddElementMemberMapping(XmlAttributes at, Type type, string elementName, string elementNamespace)
		{
			XmlElementAttribute el = new XmlElementAttribute(elementName,type);
			el.Namespace = elementNamespace;
			at.XmlElements.Add( el );
		}

		public static void AddAttributeMemberMapping(XmlAttributeOverrideMappingArgs mappings, Type type, string member, string attributeName)
		{
			XmlAttributes at = new XmlAttributes();
			XmlAttributeAttribute att = new XmlAttributeAttribute(attributeName);
			at.XmlAttribute = att;
			mappings.XmlMemberOverrides.Add(type, member, at);
		}

		public static void AddAttributeMemberMapping(XmlAttributeOverrideMappingArgs mappings, Type type, string member, string attributeName, string attributeNamespace)
		{
			XmlAttributes at = new XmlAttributes();
			XmlAttributeAttribute att = new XmlAttributeAttribute(attributeName);			
			att.Namespace = attributeNamespace;
			at.XmlAttribute = att;
			mappings.XmlMemberOverrides.Add(type, member, at);
		}

		public static void AddTypeMemberMapping(XmlAttributeOverrideMappingArgs mappings, Type type, string typeName)
		{
			XmlAttributes at = new XmlAttributes();
			XmlTypeAttribute att = new XmlTypeAttribute(typeName);
			at.XmlType = att;
			mappings.XmlMemberOverrides.Add(type, at);
		}

		public static void AddTypeMemberMapping(XmlAttributeOverrideMappingArgs mappings, Type type, string typeName, string namespaces)
		{
			XmlAttributes at = new XmlAttributes();
			XmlTypeAttribute att = new XmlTypeAttribute(typeName);
			att.Namespace = namespaces;
			at.XmlType = att;
			mappings.XmlMemberOverrides.Add(type, at);
		}
	}
}
