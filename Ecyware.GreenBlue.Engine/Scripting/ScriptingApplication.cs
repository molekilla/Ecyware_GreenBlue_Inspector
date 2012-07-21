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
	public class Script
	{
		/// <summary>
		/// Creates a new Script.
		/// </summary>
		public Script()
		{
		}

		string _name = string.Empty;
		bool _isFileSource = false;
		string _source = string.Empty;
		string _type = string.Empty;

		[XmlAttribute]
		public string Type
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


		[XmlAttribute]
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}

		[XmlAttribute]
		public bool IsFileSource
		{
			get
			{
				return _isFileSource;
			}
			set
			{
				_isFileSource = value;
			}
		}

		public string Source
		{
			get
			{
				return _source;
			}
			set
			{
				_source = value;
			}
		}


	}

	public class Header
	{
		string _applicationID;
		FileReference _argumentDefinition = new FileReference();

		/// <summary>
		/// Creates a new Header.
		/// </summary>
		public Header()
		{
		}

		/// <summary>
		/// Gets or sets the application id.
		/// </summary>
		public string ApplicationID
		{
			get
			{
				return _applicationID;
			}
			set
			{
				_applicationID = value;
			}
		}

		/// <summary>
		/// Gets or sets the scripting application arguments file.
		/// </summary>
		public FileReference ScriptingApplicationArgumentsReference
		{
			get
			{
				return _argumentDefinition;
			}
			set
			{
				_argumentDefinition = value;
			}
		}


	}
	/// <summary>
	/// Summary description for ScriptingData.
	/// </summary>
	[XmlRoot(Namespace = "http://schemas.ecyware.com/2005/01/Ecyware-GreenBlue-ScriptingApplication", 
		 ElementName = "ScriptingApplication", 
		 IsNullable=true)]
	public class ScriptingApplication
	{
		ScriptingApplicationSerializer serializer = new ScriptingApplicationSerializer();
		private Script _script = new Script();
		private Header _header = new Header();
		private ArrayList _list = new ArrayList();
		private ArrayList _plugins = new ArrayList();
		string section = "ScriptingApplication";
		//private static InternalSession _session = new InternalSession();

		/// <summary>
		/// Creates a new ScriptingData.
		/// </summary>
		public ScriptingApplication()
		{						
		}




		#region Methods
		/// <summary>
		/// Updates a ScriptingApplicationArgs.
		/// </summary>
		/// <returns>A ScriptingApplicationArgs type.</returns>
		public ScriptingApplicationArgs UpdateArgumentDefinition(ScriptingApplicationArgs existingApplicationArgs)
		{
			ScriptingApplicationArgs saArguments = new ScriptingApplicationArgs();

			if ( this.WebRequests.Length > 0 )
			{			
				
				Hashtable lookupTable = new Hashtable();
				foreach ( WebRequestArgs wbr in existingApplicationArgs.WebRequestArguments )
				{
					foreach ( Argument a in wbr.Arguments )
					{
						lookupTable.Add(a.Name, a);
					}
				}

				int i = 0;
				for (int j=0;j<this.WebRequests.Length;j++)
				{
					WebRequest request = this.WebRequests[j];

					if ( request.InputTransforms.Length > 0 ||  request.OutputTransforms.Length > 0)
					{
						// Create WebRequestArgs, if values are enabled.
						WebRequestArgs webRequestArgs = new WebRequestArgs();
						webRequestArgs.WebRequestIndex = j;
												
						foreach ( WebTransform t in request.InputTransforms )
						{
							Argument[] args = t.GetArguments();

							if ( args != null )
							{								
								foreach ( Argument newArgument in args )
								{
									if ( lookupTable.ContainsKey(newArgument.Name) )
									{
										newArgument.DesignProperty = ((Argument)lookupTable[newArgument.Name]).DesignProperty;
									}
								}
								webRequestArgs.AddArguments(args);
							}
							i++;
						}

						i = 0;
						foreach ( WebTransform t in request.OutputTransforms )
						{
							Argument[] args = t.GetArguments();

							if ( args != null )
							{								
								foreach ( Argument newArgument in args )
								{
									if ( lookupTable.ContainsKey(newArgument.Name) )
									{
										newArgument.DesignProperty = ((Argument)lookupTable[newArgument.Name]).DesignProperty;
									}
								}
								webRequestArgs.AddArguments(args);
							}
							i++;
						}


						// Add WebRequestArgs to ScriptingApplicationArgs.
						saArguments.AddWebRequestArgs(webRequestArgs);
					}
				}
			}

			return saArguments;
		}

		/// <summary>
		/// Creates a ScriptingApplicationArgs.
		/// </summary>
		/// <returns>A ScriptingApplicationArgs type.</returns>
		public ScriptingApplicationArgs CreateArgumentDefinition()
		{
			ScriptingApplicationArgs saArguments = new ScriptingApplicationArgs();

			if ( this.WebRequests.Length > 0 )
			{				
				for (int i=0;i<this.WebRequests.Length;i++)
				{
					WebRequest request = this.WebRequests[i];

					if ( request.InputTransforms.Length > 0 )
					{
						// Create WebRequestArgs, if values are enabled.
						WebRequestArgs webRequestArgs = new WebRequestArgs();
						webRequestArgs.WebRequestIndex = i;

						foreach ( WebTransform t in request.InputTransforms )
						{
							Argument[] args = t.GetArguments();

							if ( args != null )
							{
								webRequestArgs.AddArguments(args);
							}
						}

						// Add WebRequestArgs to ScriptingApplicationArgs.
						saArguments.AddWebRequestArgs(webRequestArgs);
					}
				}
			}

			return saArguments;
		}

		/// <summary>
		/// Get file references.
		/// </summary>
		/// <returns> An array of FileReference.</returns>
		public FileReference[] GetFileReferences()
		{
			XmlDocument document = this.ToXmlDocument();

			XmlNamespaceManager manager = new XmlNamespaceManager(document.NameTable);
			manager.AddNamespace("scr", "http://schemas.ecyware.com/2005/01/Ecyware-GreenBlue-ScriptingApplication");
			manager.AddNamespace("xsd","http://www.w3.org/2001/XMLSchema");
			manager.AddNamespace("xsi","http://www.w3.org/2001/XMLSchema-instance");

			string query = "//scr:FileReference";

			XmlNodeList fileReferences = 
				document.SelectNodes(query, manager);

			ArrayList list = new ArrayList();
			foreach ( XmlNode node in fileReferences )
			{
				XmlNode value = node.SelectSingleNode("scr:FileName");
				string fileName = value.InnerText;
				FileReference fileRef = new FileReference(fileName);
				list.Add(fileRef);
			}

			return (FileReference[])list.ToArray(typeof(FileReference));
		}

		/// <summary>
		/// Loads a ScriptingApplicationArgs.
		/// </summary>
		/// <returns>A ScriptingApplicationArgs type.</returns>
		public void LoadArgumentDefinition(ScriptingApplicationArgs arguments)
		{
			XmlDocument document = this.ToXmlDocument();

			XmlNamespaceManager manager = new XmlNamespaceManager(document.NameTable);
			manager.AddNamespace("scr", "http://schemas.ecyware.com/2005/01/Ecyware-GreenBlue-ScriptingApplication");
			manager.AddNamespace("xsd","http://www.w3.org/2001/XMLSchema");
			manager.AddNamespace("xsi","http://www.w3.org/2001/XMLSchema-instance");

			if ( this.WebRequests.Length > 0 )
			{
				int webRequestIndex = 1;
				int webRequestArgsIndex = 0;
				foreach ( WebRequest request in WebRequests )
				{
					if ( request.InputTransforms.Length > 0 || request.OutputTransforms.Length > 0 )
					{						
						WebRequestArgs webRequestArgs = arguments.WebRequestArguments[webRequestArgsIndex];

						string query = "//scr:WebRequests/*[" + webRequestIndex + "]//*[scr:EnabledInputArgument='true']";

						XmlNodeList defaultTransformValues = 
							document.SelectNodes(query, manager);

						if ( defaultTransformValues.Count > 0 )
						{
							int i = 0;
							foreach ( XmlNode defaultTransformValue in defaultTransformValues )
							{
								XmlNode value =  defaultTransformValue.SelectSingleNode("scr:Value",manager);
								value.InnerText =  webRequestArgs.Arguments[i].Value;
								i++;
							}
						}
						
						webRequestArgsIndex++;
					}
				
					webRequestIndex++;
				}

				// update input transforms
				ScriptingApplication scrapp = (ScriptingApplication)serializer.Create(document.DocumentElement.OuterXml);
				this._list.Clear();
				this.WebRequests = scrapp.WebRequests;
			}
		}


		/// <summary>
		/// Saves a ScriptingData.
		/// </summary>
		/// <param name="fileName"> The file name.</param>
		public void Save(string fileName)
		{
			if ( this.Header.ApplicationID == null )
			{
				this.Header.ApplicationID = System.Guid.NewGuid().ToString();
			}

			// Write
			XmlNode node = serializer.Serialize(this, true);

			// Write			
			XmlDocument document = new XmlDocument();
			XmlNode imported = document.ImportNode(node,true);
			document.AppendChild(imported);
			document.Save(fileName);
		}
		/// <summary>
		/// Encrypts the scripting application.
		/// </summary>
		/// <returns> A encrypted XML string.</returns>
		public string Encrypt()
		{
			try
			{				
				XmlDocument document = new XmlDocument();
				document.LoadXml(this.ToXml());
				
				// Encrypt xml
				EncryptXml enc = new EncryptXml(document);
				enc.AddKeyNameMapping("scriptingApplication", enc.CreateMachineStoreKey("Ecyware.ScrAppEncryption"));

				XmlElement el = (XmlElement)document.FirstChild;
				EncryptedData data = enc.Encrypt(el, "scriptingApplication");
				enc.ReplaceElement(el, data);

				return document.DocumentElement.OuterXml;
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Decrypts a XmlDocument representing a scripting application.
		/// </summary>
		/// <param name="encryptedDocument"> A XmlDocument type.</param>
		/// <returns> A decrypted Scripting Application.</returns>
		public static ScriptingApplication Decrypt(XmlDocument encryptedDocument)
		{
			// add to new document
			XmlNode node = encryptedDocument.SelectSingleNode("//EncryptedData");
			ScriptingApplication scrapp = null;
			ScriptingApplicationSerializer serializer = new ScriptingApplicationSerializer();

			if ( node != null )
			{
				XmlDocument document = new XmlDocument();
				document.AppendChild(document.ImportNode(node,true));

				// decrypt
				EncryptXml decrypt = new EncryptXml(document);
				decrypt.AddKeyNameMapping("scriptingApplication", decrypt.GetMachineStoreKey("Ecyware.ScrAppEncryption"));
				decrypt.DecryptDocument();
				
				scrapp = (ScriptingApplication)serializer.Create(document.DocumentElement.OuterXml);
			} 
			else 
			{
				scrapp = (ScriptingApplication)serializer.Create(encryptedDocument.DocumentElement.OuterXml);
			}

			return scrapp;
		}

		/// <summary>
		/// Returns the scripting application as XML.
		/// </summary>
		/// <returns> Returns the scripting application as a XML string.</returns>
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

		/// <summary>
		/// Returns a ScriptingApplication.
		/// </summary>
		/// <param name="xml"> The xml to load.</param>
		/// <returns> A ScriptingApplication type.</returns>
		public static ScriptingApplication FromXml(string xml)
		{
			ScriptingApplicationSerializer serializer = new ScriptingApplicationSerializer();
			return (ScriptingApplication)serializer.Create(xml);
		}

		/// <summary>
		/// Loads an existing ScriptingData.
		/// </summary>
		/// <param name="fileName"></param>
		public void Load(string fileName)
		{					
			ScriptingApplication sd = (ScriptingApplication)serializer.Load(section, fileName);
			this.Script = sd.Script;
			this.Header = sd.Header;
			this.WebRequests = sd.WebRequests;			
		}
		#endregion
		#region Properties

//		public static InternalSession Session
//		{
//			get
//			{
//				return _session;
//			}
//			set
//			{
//				_session = value;
//			}
//		}


		/// <summary>
		/// Gets or sets the header.
		/// </summary>
		public Header Header
		{
			get
			{
				return _header;
			}
			set
			{
				_header = value;
			}
		}

		/// <summary>
		/// Gets or sets the script.
		/// </summary>
		public Script Script
		{
			get
			{
				return _script;
			}
			set
			{
				_script = value;
			}
		}

		/// <summary>
		/// Gets or sets the session requests.
		/// </summary>
		public WebRequest[] WebRequests
		{
			get
			{
				return (WebRequest[])_list.ToArray(typeof(WebRequest));
			}
			set
			{	
				if ( value != null )
				{
					_list.AddRange(value);
				}
			}
		}

		/// <summary>
		/// Gets or sets the custom transforms.
		/// </summary>
		[XmlIgnore]
		public FileReference[] CustomTransforms
		{
			get
			{
				return (FileReference[])_plugins.ToArray(typeof(FileReference));
			}
			set
			{	
				if ( value != null )
					_plugins.AddRange(value);
			}
		}
		#endregion
		#region Helpers

		/// <summary>
		/// Clones the object.
		/// </summary>
		/// <returns> A duplicate of the ScriptingData.</returns>
		public ScriptingApplication Clone()
		{
			return (ScriptingApplication)ScriptingApplicationSerializer.Clone(this);
		}

		/// <summary>
		/// Clears all the web requests.
		/// </summary>
		public void ClearWebRequests()
		{
			_list.Clear();
		}

		/// <summary>
		/// Adds a WebResponse.
		/// </summary>
		/// <param name="index"> The web request index.</param>
		/// <param name="response"> The web response.</param>
		public void AddWebResponse(int index,WebResponse response)
		{
			((WebRequest)_list[index]).WebResponse = response;
		}

		/// <summary>
		/// Removes a web request by index.
		/// </summary>
		/// <param name="index"> The web request index.</param>
		public void RemoveWebRequest(int index)
		{
			_list.RemoveAt(index);
		}

		/// <summary>
		/// Removes a custom transform.
		/// </summary>
		/// <param name="index"> The plugin index.</param>
		public void RemoveCustomTransform(int index)
		{
			_plugins.RemoveAt(index);
		}

		/// <summary>
		/// Adds a WebRequest.
		/// </summary>
		/// <param name="request"> The request to add.</param>
		public void AddWebRequest(WebRequest request)
		{
			_list.Add(request);
		}

		/// <summary>
		/// Adds a custom transform.
		/// </summary>
		/// <param name="plugin"> The plugin to add.</param>
		public void AddCustomTransform(FileReference plugin)
		{
			_plugins.Add(plugin);
		}

		/// <summary>
		/// Inserts a WebRequest.
		/// </summary>
		/// <param name="index"> The index to insert at.</param>
		/// <param name="request"> The request to add.</param>
		public void InsertWebRequest(int index, WebRequest request)
		{
			_list.Insert(index, request);
		}

		/// <summary>
		/// Inserts a custom transform.
		/// </summary>
		/// <param name="index"> The index to insert at.</param>
		/// <param name="plugin"> The plugin to add.</param>
		public void InsertCustomTransform(int index, FileReference plugin)
		{
			_plugins.Insert(index, plugin);
		}

		/// <summary>
		/// Updates the web request.
		/// </summary>
		/// <param name="index"> The request index.</param>
		/// <param name="request"> The web request type.</param>
		public void UpdateWebRequest(int index, WebRequest request)
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

		/// <summary>
		/// Gets the custom transforms.
		/// </summary>
		/// <returns> A FileReference array.</returns>
		public FileReference[] GetCustomTransforms()
		{
			foreach ( WebRequest request in this.WebRequests )
			{
				ArrayList transforms = new ArrayList();
				
				if ( request.InputTransforms.Length > 0 )
				{
					transforms.AddRange(request.InputTransforms);
				}
				if ( request.OutputTransforms.Length > 0 )
				{
					transforms.AddRange(request.OutputTransforms);
				}

				foreach ( WebTransform transform in transforms )
				{
					string location = transform.GetType().Assembly.Location;
					
					FileInfo fileInfo = new FileInfo(location);
					if ( !(fileInfo.Name.IndexOf("Ecyware.GreenBlue.Engine") > -1) )
					{
						// is custom
						this.AddCustomTransform(new FileReference(location));
					}
				}
			}

			return this.CustomTransforms;
		}
		#endregion
	}
}
