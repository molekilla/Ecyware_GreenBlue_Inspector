using System;
using System.Collections;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Configuration.Encryption;
using Ecyware.GreenBlue.Configuration.XmlTypeSerializer;
using Ecyware.GreenBlue.Engine.Transforms;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;
using System.IO;

namespace Ecyware.GreenBlue.Engine.Scripting
{
	/// <summary>
	/// Summary description for ScriptingApplicationArguments.
	/// </summary>
	[XmlRoot(Namespace = "http://schemas.ecyware.com/2005/03/Ecyware-GreenBlue-ScriptingApplicationArgs", 
		 ElementName = "ScriptingApplicationArgs", 
		 IsNullable=true)]
	public class ScriptingApplicationArgs
	{
		ScriptingApplicationArgsSerializer serializer = new ScriptingApplicationArgsSerializer();
		private ArrayList _list = new ArrayList();
		string section = "ScriptingApplicationArgs";

		/// <summary>
		/// Creates a new ScriptingApplicationArguments.
		/// </summary>
		public ScriptingApplicationArgs()
		{
		}

//		/// <summary>
//		/// Fills the scripting application arguments.
//		/// </summary>
//		/// <param name="args"> The arguments to fill.</param>
//		/// <returns> The updated ScriptingApplicationArgs.</returns>
//		public static ScriptingApplicationArgs FillScriptingApplicationArgs(ScriptingApplicationArgs args)
//		{
//			ScriptingApplicationArgumentForm form = new ScriptingApplicationArgumentForm();
//			form.ScriptingApplicationArgs = args;
//			form.ShowDialog();
//
//			return args;
//		}

		/// <summary>
		/// Gets or sets the web request arguments.
		/// </summary>
		public WebRequestArgs[] WebRequestArguments
		{
			get
			{
				return (WebRequestArgs[])_list.ToArray(typeof(WebRequestArgs));
			}
			set
			{	
				if ( value != null )
					_list.AddRange(value);
			}
		}

		#region Helpers

		/// <summary>
		/// Clears all the web requests args.
		/// </summary>
		public void ClearWebRequestArgs()
		{
			_list.Clear();
		}


		/// <summary>
		/// Removes a web request args by index.
		/// </summary>
		/// <param name="index"> The web request args index.</param>
		public void RemoveWebRequestArgs(int index)
		{
			_list.RemoveAt(index);
		}

		/// <summary>
		/// Adds a WebRequestArgs.
		/// </summary>
		/// <param name="request"> The request to add.</param>
		public void AddWebRequestArgs(WebRequestArgs request)
		{
			_list.Add(request);
		}

		/// <summary>
		/// Inserts a WebRequestArgs.
		/// </summary>
		/// <param name="index"> The index to insert at.</param>
		/// <param name="request"> The request args to add.</param>
		public void InsertWebRequestArgs(int index, WebRequestArgs request)
		{
			_list.Insert(index, request);
		}

		/// <summary>
		/// Updates the web request args.
		/// </summary>
		/// <param name="index"> The request args index.</param>
		/// <param name="request"> The web request args type.</param>
		public void UpdateWebRequestArgs(int index, WebRequestArgs request)
		{
			if ( index > -1 )
			{
				if ( request != null )
				{
					// Update request index.
					_list[index] = request;
				}
			}
		}
		#endregion

		/// <summary>
		/// Loads an existing ScriptingApplicationArgs.
		/// </summary>
		/// <param name="fileName"> The file name.</param>
		public void Load(string fileName)
		{		
			ScriptingApplicationArgs args = (ScriptingApplicationArgs)serializer.Load(section, fileName);
			this.WebRequestArguments = args.WebRequestArguments;			
		}

		/// <summary>
		/// Loads a file reference.
		/// </summary>
		/// <param name="fileReference"> The file reference to load.</param>
		public void LoadFileReference(FileReference fileReference)
		{
			string fileName = fileReference.FileName;
			string xml = string.Empty;

			using ( FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read) )
			{
				using ( StreamReader reader = new StreamReader(fileStream) )
				{
					xml = reader.ReadToEnd();				
				}
			}

			if ( xml.Length > 0 )
			{
				bool valid = serializer.CanDeserialize(xml);
				if ( valid )
				{
					Load(fileName);
				}
			}
			
		}

		/// <summary>
		/// Saves a ScriptingApplicationArgs.
		/// </summary>
		/// <param name="fileName"> The file name.</param>
		public void Save(string fileName)
		{
			// Write
			XmlNode node = serializer.Serialize(this, true);

			// Write			
			XmlDocument document = new XmlDocument();
			XmlNode imported = document.ImportNode(node,true);
			document.AppendChild(imported);
			document.Save(fileName);
		}

		/// <summary>
		/// Returns a ScriptingApplicationArgs.
		/// </summary>
		/// <param name="xml"> The xml to load.</param>
		/// <returns> A ScriptingApplicationArgs type.</returns>
		public static ScriptingApplicationArgs FromXml(string xml)
		{
			ScriptingApplicationArgsSerializer serializer = new ScriptingApplicationArgsSerializer();
			return (ScriptingApplicationArgs)serializer.Create(xml);
		}

		/// <summary>
		/// Returns the scripting application arguments as XML.
		/// </summary>
		/// <returns> Returns the scripting application arguments as a XML string.</returns>
		public string ToXml()
		{
			// Write
			XmlNode node = serializer.Serialize(this, true);

			XmlNodeReader reader = new XmlNodeReader(node);

			StringWriter str = new StringWriter();
			XmlTextWriter writer = new XmlTextWriter(str);
			writer.Formatting = System.Xml.Formatting.Indented;
			writer.Indentation = 4;
			writer.WriteNode(reader, false);

			string result = str.ToString();

			writer.Close();
			str.Close();
			
			return result;
		}

		/// <summary>
		/// Converts the scripting application to a XmlDocument.
		/// </summary>
		/// <returns> A XmlDocument.</returns>
		public XmlDocument ToXmlDocument()
		{
			// Write
			XmlNode node = serializer.Serialize(this, true);

			XmlDocument document = new XmlDocument();
			document.AppendChild(document.ImportNode(node, true));

			return document;
		}

	}
}
