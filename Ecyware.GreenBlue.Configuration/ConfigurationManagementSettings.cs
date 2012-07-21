// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: October 2004
using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace Ecyware.GreenBlue.Configuration
{
	/// <summary>
	/// Summary description for ConfigurationManagementSettings.
	/// </summary>
	[ReflectionPermissionAttribute(SecurityAction.Demand, Unrestricted=true)]	
	internal class ConfigurationManagementSettings
	{
		Hashtable _handlers = null;
		//private string _appConfig = string.Empty;

		/// <summary>
		/// Creates the ConfigurationManagementSettings.
		/// </summary>
		public ConfigurationManagementSettings()
		{
			_handlers = new Hashtable(5);			
			
		}

		public Hashtable SectionHandlers
		{
			get
			{
				return _handlers;
			}
		}

		/// <summary>
		/// Loads the section handlers.
		/// </summary>
		/// <param name="node"> The XPathNavigator node.</param>
		public void LoadSectionHandlers(XPathNavigator node)
		{
			XPathNodeIterator children = node.SelectChildren(XPathNodeType.Element);

			while ( children.MoveNext() )
			{
				XPathNavigator currentNode = children.Current;

				string name = currentNode.GetAttribute("name",currentNode.NamespaceURI);

				if ( currentNode.LocalName == "section" )
				{
					string type = currentNode.GetAttribute("type",currentNode.NamespaceURI);
					_handlers.Add(name,Type.GetType(type));
				}
				if ( currentNode.LocalName == "sectionGroup" )
				{
					// get sections
					LoadSectionHandlers(name, currentNode);
				}
			}
		}


		/// <summary>
		/// Loads the section handlers.
		/// </summary>
		/// <param name="parentLocalName"> The parent localname.</param>
		/// <param name="node"> The XPathNavigator node.</param>
		private void LoadSectionHandlers(string parentLocalName, XPathNavigator node)
		{
			XPathNodeIterator children = node.SelectChildren(XPathNodeType.Element);

			while ( children.MoveNext() )
			{
				XPathNavigator currentNode = children.Current;

				string name = currentNode.GetAttribute("name",currentNode.NamespaceURI);

				if ( currentNode.LocalName == "section" )
				{
					string type = currentNode.GetAttribute("type", currentNode.NamespaceURI);
					_handlers.Add(parentLocalName + "/" + name, Type.GetType(type));
				}
			}
		}

		//		private object InstantiateType(string typeName)
		//		{
		//			// Create object
		//			Type t = Type.GetType(typeName);
		//			Type[] param = new Type[0];
		//			ConstructorInfo ci = t.GetConstructor(param);
		//			
		//			// Create a new type
		//			return ci.Invoke(new object[]{});
		//		}


		
		/// <summary>
		/// Get the app config file.
		/// </summary>
		/// <returns> The first app config found in the list.</returns>
		public string GetAppConfigFile()
		{			
			string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;			

			string[] files = Directory.GetFiles(baseDirectory,"*.config");

			string appConfigFile = null;

			if ( files.Length > 0 )
			{
				appConfigFile = files[0];
			}

			return appConfigFile;
		}

		/// <summary>
		/// Get the app config file.
		/// </summary>
		/// <returns> The first app config found in the list.</returns>
		public string GetAppConfigFile(string assemblyCallerLocation)
		{			
			DirectoryInfo info = Directory.GetParent(assemblyCallerLocation);
			
			FileInfo[] files = info.GetFiles("*.config");
			string appConfigFile = null;

			if ( files.Length > 0 )
			{
				appConfigFile = files[0].FullName;
			}

			return appConfigFile;
		}

		#region Static Methods
		/// <summary>
		/// Reads the configuation node.
		/// </summary>
		/// <param name="section"> The section handler name.</param>
		/// <param name="configFile"> The configuration file.</param>
		/// <returns> A XmlNode with the data.</returns>
		public static XmlNode ReadConfigNode(string section, string configFile)
		{			
			XmlDocument document = new XmlDocument();
			using ( FileStream fs = new FileStream(configFile,FileMode.Open, FileAccess.Read) )
			{
				document.Load(fs);
			}
			XmlNode sectionNode = document.SelectSingleNode("/configuration/" + section);
			
			if ( sectionNode == null )
				sectionNode = document.SelectSingleNode("/" + section);

			return sectionNode;
		}

		/// <summary>
		/// Writes the configuration node.
		/// </summary>
		/// <param name="section"> The section handler name.</param>
		/// <param name="node"> The XmlNode data.</param>
		/// <param name="configFile"> The configuration file.</param>
		public static void WriteConfigNode(string section, XmlNode node, string configFile)
		{
			XmlDocument document = new XmlDocument();
			
			if ( File.Exists(configFile) )
			{
				FileStream readFile = new FileStream(configFile, FileMode.Open,FileAccess.Read);
				document.Load(readFile);			

				XmlNode sectionNode = document.SelectSingleNode("/configuration/" + section);

				if ( sectionNode != null )
				{
					sectionNode.RemoveAll();
				}
				else 
				{
					sectionNode = document.CreateElement(section);
					XmlNode parent = document.SelectSingleNode("/configuration");
					parent.AppendChild(sectionNode);
				}

				// imports the new node to the document
				XmlNode cloneNode = document.ImportNode(node, true);
				sectionNode.AppendChild(cloneNode);

				readFile.Close();
			}

			XmlTextWriter writer = new XmlTextWriter(configFile, null);
			writer.Formatting = Formatting.Indented;
			//writer.Namespaces = false;
			document.WriteTo(writer);
			writer.Flush();
			writer.Close();
		}

		/// <summary>
		/// Writes the configuration node.
		/// </summary>
		/// <param name="section"> The section handler name.</param>
		/// <param name="node"> The XmlNode data.</param>
		/// <param name="configFile"> The configuration file.</param>
		/// <param name="supportsNamespaces"> Set to true if it supports namespaces.</param>
		internal static void WriteConfigNode(string section, XmlNode node, string configFile, bool supportsNamespaces)
		{
			XmlDocument document = new XmlDocument();
			
			if ( File.Exists(configFile) )
			{
				FileStream readFile = new FileStream(configFile, FileMode.Open,FileAccess.Read);
				document.Load(readFile);			

				XmlNode sectionNode = document.SelectSingleNode("/configuration/" + section);

				if ( sectionNode != null )
				{
					sectionNode.RemoveAll();
				}
				else 
				{
					sectionNode = document.CreateElement(section);
					XmlNode parent = document.SelectSingleNode("/configuration");
					parent.AppendChild(sectionNode);
				}

				// imports the new node to the document
				XmlNode cloneNode = document.ImportNode(node, true);
				sectionNode.AppendChild(cloneNode);

				readFile.Close();
			
			} 

			XmlTextWriter writer = null;

			if ( !supportsNamespaces )
			{
				writer = new SkipSerializerNamespacesWriter(configFile, null);					
			} 
			else 
			{					
				writer = new XmlTextWriter(configFile, null);
			}

			writer.Formatting = Formatting.Indented;
			writer.Namespaces = supportsNamespaces;
			document.WriteTo(writer);
			writer.Flush();
			writer.Close();
		}
		#endregion
	}

}
