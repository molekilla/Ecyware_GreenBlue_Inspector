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
	/// Delegates that handles the XML Type atttribute overrides mappings.
	/// </summary>
	public delegate void XmlAttributeOverrideMappingHandler(object sender, XmlAttributeOverrideMappingArgs e);

	/// <summary>
	/// Contains the XML Type Serializer.
	/// </summary>
	public class XmlTypeSerializer
	{
		//private string _namespaceName = string.Empty;
		private XmlSerializerNamespaces _namespaces;
		XmlSerializer ser = null;
		static Hashtable _cache = new Hashtable(20);
		//private bool _cached = false;
		public event XmlAttributeOverrideMappingHandler XmlAttributeOverrideMappingEvent;

		/// <summary>
		/// Creates a new XmlTypeSerializer.
		/// </summary>
		public XmlTypeSerializer()
		{
		}

		/// <summary>
		/// Creates a new XmlTypeSerializer.
		/// </summary>
		/// <param name="namespaces"> The XmlSerializerNamespaces type.</param>
		public XmlTypeSerializer(XmlSerializerNamespaces namespaces)
		{
			//_namespaceName = namespaceName;
			_namespaces = namespaces;
		}


		/// <summary>
		/// Gets or sets the namespaces.
		/// </summary>
		public XmlSerializerNamespaces Namespaces
		{
			get
			{
				return _namespaces;
			}
			set
			{
				_namespaces = value;
			}
		}
		/// <summary>
		/// Returns true if the cache name exists in the cache, else false.
		/// </summary>
		/// <param name="name"> The cache name.</param>
		/// <returns>Returns true if the cache name exists in the cache, else false.</returns>
		public bool HasCache(string name)
		{
			if ( _cache.ContainsKey(name) )
			{
				return true;
			} 
			else 
			{
				return false;
			}
		}

		/// <summary>
		/// Adds a type to the serializer cache.
		/// </summary>
		/// <param name="type"> The type to cache.</param>
		/// <param name="name"> The name of the cache.</param>
		public void AddSerializerCache(Type type,string name)
		{
			CreateSerializer(type);
			_cache.Add(name, ser);
		}


		/// <summary>
		/// Removes a serializer from the cache store.
		/// </summary>
		/// <param name="name"> The cache name.</param>
		public void RemoveSerializerCache(string name)
		{
			_cache.Remove(name);
		}

		private void CreateSerializer(Type type)
		{
			XmlAttributeOverrides attrs = new XmlAttributeOverrides();			

			if ( XmlAttributeOverrideMappingEvent != null )
			{
				XmlAttributeOverrideMappingArgs args = new XmlAttributeOverrideMappingArgs();
				args.Type = type;
				XmlAttributeOverrideMappingEvent(this, args);
				attrs = args.XmlMemberOverrides;

				ser = new XmlSerializer(type,attrs);
			} 
			else 
			{
				ser = new XmlSerializer(type);
			}
		}

		/// <summary>
		/// Checks if the XML can deserialize.
		/// </summary>
		/// <param name="section"> The XML string.</param>
		/// <param name="cacheName"> The cache name.</param>
		/// <returns> Returns true if it can be deserialize, else false.</returns>
		public bool CanDeserialize(string section, string cacheName)
		{
			try
			{
				if ( _cache[cacheName] != null )
				{
					ser = (XmlSerializer)_cache[cacheName];
					XmlTextReader reader = new XmlTextReader( new StringReader(section) );
					return ser.CanDeserialize(reader);
				} 
				else 
				{
					return false;
				}
			}
			catch
			{
				// ignore error, return false.
				return false;
			}
		}

		/// <summary>
		/// Serializes a type instance to a XML string.
		/// </summary>
		/// <param name="type"> The type.</param>
		/// <param name="instance"> The type instance.</param>
		/// <returns> A XML string.</returns>
		public string WriteXmlString(Type type, object instance)
		{		
			CreateSerializer(type);

			// Serialize object to xml
			StringWriter sw = new StringWriter( System.Globalization.CultureInfo.CurrentUICulture );
			XmlTextWriter writer = new XmlTextWriter(sw);			
			ser.Serialize(writer, instance);
			writer.Flush();

			return sw.ToString();
		}

		public string WriteXmlString(Type type, object instance, string cacheName)
		{
			if ( _cache[cacheName] != null )
			{
				ser = (XmlSerializer)_cache[cacheName];

				// Serialize object to xml
				StringWriter sw = new StringWriter( System.Globalization.CultureInfo.CurrentUICulture );
				XmlTextWriter writer = new XmlTextWriter(sw);			
				ser.Serialize(writer, instance);
				writer.Flush();

				return sw.ToString();
			} 
			else 
			{
				return string.Empty;
			}			
		}

		/// <summary>
		/// Writes a XML Node.
		/// </summary>
		/// <param name="type"> The instance type.</param>
		/// <param name="instance"> The instance.</param>
		/// <param name="cacheName"> The cache name.</param>
		/// <param name="useNamespaces"> Sets if the result has namespaces.</param>
		/// <returns> Returns a XmlNode.</returns>
		public XmlNode WriteXmlNode(Type type, object instance, string cacheName, bool useNamespaces)
		{
			if ( _cache[cacheName] != null )
			{
				ser = (XmlSerializer)_cache[cacheName];

				// Serialize object to xml
				StringWriter sw = new StringWriter( System.Globalization.CultureInfo.CurrentUICulture );
				
				XmlTextWriter writer = null;

				if ( !useNamespaces )
				{
					writer = new SkipSerializerNamespacesWriter(sw);					
				} 
				else 
				{					
					writer = new XmlTextWriter(sw);
				}
				
				ser.Serialize(writer, instance);
				writer.Flush();

				// Return as a XmlNode
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(sw.ToString());

				return doc.DocumentElement;
			} 
			else 
			{
				return null;
			}			
		}

		/// <summary>
		/// Writes a XML Node.
		/// </summary>
		/// <param name="type"> The instance type.</param>
		/// <param name="instance"> The instance.</param>
		/// <param name="cacheName"> The cache name.</param>
		/// <returns> Returns a XmlNode.</returns>
		public XmlNode WriteXmlNode(Type type, object instance, string cacheName)
		{
			if ( _cache[cacheName] != null )
			{
				ser = (XmlSerializer)_cache[cacheName];

				// Serialize object to xml
				StringWriter sw = new StringWriter( System.Globalization.CultureInfo.CurrentUICulture );
				XmlTextWriter writer = new XmlTextWriter(sw);			
				ser.Serialize(writer, instance);
				writer.Flush();

				// Return as a XmlNode
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(sw.ToString());

				return doc.DocumentElement;
			} 
			else 
			{
				return null;
			}			
		}

		/// <summary>
		/// Serializes a type instance to a XmlNode.
		/// </summary>
		/// <param name="type"> The type.</param>
		/// <param name="instance"> The type instance.</param>
		/// <returns> A XmlNode.</returns>
		public XmlNode WriteXmlNode(Type type, object instance)
		{
			CreateSerializer(type);
			
			// Serialize object to xml
			StringWriter sw = new StringWriter( System.Globalization.CultureInfo.CurrentUICulture );
			XmlTextWriter writer = new XmlTextWriter(sw);			
			ser.Serialize(writer, instance);
			writer.Flush();

			// Return as a XmlNode
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(sw.ToString());

			return doc.DocumentElement;
		}


		/// <summary>
		/// Deserializes a XML string to a type instance.
		/// </summary>
		/// <param name="type"> The type.</param>
		/// <param name="section"> The XML string.</param>
		/// <returns> A type instance.</returns>
		public object ReadXmlString(Type type, string section)
		{	
			CreateSerializer(type);

			//			XmlNodeReader reader = ;
			//			reader.MoveToContent();
			//			string r = reader.ReadOuterXml();
			//			reader = new XmlNodeReader( section );
			
			XmlTextReader reader = new XmlTextReader( new StringReader(section) );
			object cfg = ser.Deserialize(reader);

			return cfg;
		}

		public object ReadXmlString(Type type, string section, string cacheName)
		{
			if ( _cache[cacheName] != null )
			{
				ser = (XmlSerializer)_cache[cacheName];

				XmlTextReader reader = new XmlTextReader( new StringReader(section) );
				object cfg = ser.Deserialize(reader);

				return cfg;
			} 
			else 
			{
				return null;
			}	
		}


		public object ReadXmlNode(Type type, XmlNode section, string cacheName)
		{
			if ( _cache[cacheName] != null )
			{
				ser = (XmlSerializer)_cache[cacheName];
			
				object cfg = ser.Deserialize(new XmlNodeReader( section ));

				return cfg;
			} 
			else 
			{
				return null;
			}
		}

		/// <summary>
		/// Deserializes a XML string to a type instance.
		/// </summary>
		/// <param name="type"> The type.</param>
		/// <param name="section"> The XmlNode type.</param>
		/// <returns> A type instance.</returns>
		public object ReadXmlNode(Type type, XmlNode section)
		{
			CreateSerializer(type);

			//			XmlNodeReader reader = ;
			//			reader.MoveToContent();
			//			string r = reader.ReadOuterXml();
			//			reader = new XmlNodeReader( section );
			
			object cfg = ser.Deserialize(new XmlNodeReader( section ));

			return cfg;
		}
	}
}
