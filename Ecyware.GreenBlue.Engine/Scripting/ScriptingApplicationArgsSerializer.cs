// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: March 2005
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
	/// Summary description for ScriptingApplicationArgsSerializer.
	/// </summary>
	[ConfigurationHandler(typeof(ScriptingApplicationArgs))]
	public sealed class ScriptingApplicationArgsSerializer : ConfigurationSection
	{
		/// <summary>
		/// Creates a new ScriptingApplicationArgsSerializer.
		/// </summary>
		public ScriptingApplicationArgsSerializer()
		{
		}

		public override object Load(string sectionName, string fileName)
		{
			XmlDocument document = new XmlDocument();
			document.Load(fileName);

			XmlNode node = document.DocumentElement;
			ScriptingApplicationArgs args = null;

			if ( node != null )
			{
				if ( CanDeserialize(node.OuterXml) )
				{
					args = (ScriptingApplicationArgs)this.Create(node.OuterXml);
				}
			}			

			return args;
		}

		/// <summary>
		/// Returns a clone of the object.
		/// </summary>
		/// <param name="argsDefinition"> The ScriptingApplicationArgs to clone.</param>
		/// <returns> A clone of the object.</returns>
		public static object Clone(ScriptingApplicationArgs argsDefinition)
		{
			if ( serializer == null )
			{
				// Initiate serializer.
				ScriptingApplicationArgsSerializer init = new ScriptingApplicationArgsSerializer();
			}

			XmlNode node = serializer.WriteXmlNode(typeof(ScriptingApplicationArgs), argsDefinition, "ScriptingApplicationArgs");
			XmlNode clone = node.Clone();
			return serializer.ReadXmlNode(typeof(ScriptingApplicationArgs), clone, "ScriptingApplicationArgs");
		}
	}
}
