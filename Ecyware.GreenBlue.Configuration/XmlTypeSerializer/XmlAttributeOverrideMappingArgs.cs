// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: October 2004
using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Configuration;
using System.Xml.Serialization;
using System.Reflection;


namespace Ecyware.GreenBlue.Configuration.XmlTypeSerializer
{
	/// <summary>
	/// Summary description for XmlAttributeOverrideMappingArgs.
	/// </summary>
	public class XmlAttributeOverrideMappingArgs
	{
		private Type _type;
		private XmlAttributes _xmlat = new XmlAttributes();
		private XmlAttributeOverrides _overrides = new XmlAttributeOverrides();

		/// <summary>
		/// Creates a new XmlAttributeOverrideMappingArgs.
		/// </summary>
		public XmlAttributeOverrideMappingArgs()
		{
		}

		/// <summary>
		/// Gets or sets the type of the instance.
		/// </summary>
		public Type Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
			}
		}

		/// <summary>
		/// Gets or sets the XmlAttributeOverrides type.
		/// </summary>
		public XmlAttributeOverrides XmlMemberOverrides
		{
			get
			{
				return _overrides;
			}
			set
			{
				_overrides = value;
			}
		}

		/// <summary>
		/// Gets or sets the XmlAttributes type.
		/// </summary>
		public XmlAttributes XmlAttribute
		{
			get
			{
				return _xmlat;
			}
			set
			{
				_xmlat = value;
			}
		}

		public XmlAttributes NewXmlAttribute
		{
			get
			{
				return (new XmlAttributes());
			}
		}
	}
}
