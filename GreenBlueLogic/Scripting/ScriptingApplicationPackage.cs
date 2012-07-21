using System;
using System.IO;
using C1.C1Zip;
using System.Collections;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms;
using Ecyware.GreenBlue.Engine.HtmlDom;
using System.Reflection;
using System.Xml;


namespace Ecyware.GreenBlue.Protocols.Http.Scripting
{
	/// <summary>
	/// Summary description for ScriptingApplicationPackage.
	/// </summary>
	public class ScriptingApplicationPackage
	{
		ScriptingApplication _application;
		ScriptingApplicationArgs _arguments;

		/// <summary>
		/// Creates a new ScriptingApplicationPackage.
		/// </summary>
		public ScriptingApplicationPackage()
		{
		}

		/// <summary>
		/// Creates a new ScriptingApplicationPackage.
		/// </summary>
		/// <param name="fileName"> The application name.</param>
		public ScriptingApplicationPackage(string fileName)
		{
			OpenPackage(fileName);
		}

		#region Read Only properties
		/// <summary>
		/// Gets or sets the scripting application.
		/// </summary>
		public ScriptingApplication ScriptingApplication
		{
			get
			{
				return _application;
			}
		}

		/// <summary>
		/// Gets or sets the scripting application arguments.
		/// </summary>
		public ScriptingApplicationArgs ScriptingApplicationArguments
		{
			get
			{
				return _arguments;
			}
		}
		#endregion


		/// <summary>
		/// Reads the application id from the package.
		/// </summary>
		/// <param name="packageFile"> The package file.</param>
		/// <returns> An application id.</returns>
		public static string ReadApplicationID(string packageFile)
		{
			string result = string.Empty;
			C1ZipFile zipFile = new C1ZipFile();

			try
			{							
				zipFile.Open(packageFile);				

				// Application
				C1ZipEntry entry = zipFile.Entries[0];
				// Get entry name and convert to guid
				// if invalid GUID, throw exception
				string name = entry.FileName;

				Guid g = new Guid(name);
				result = name;
			}
			catch ( Exception ex )
			{
				System.Diagnostics.Debug.Write(ex.ToString());
			}
			finally
			{
				zipFile.Close();
			}

			return result;
		}

		/// <summary>
		/// Opens a package.
		/// </summary>
		/// <param name="fileName"> The package file name.</param>
		public void OpenPackage(string fileName)
		{
			C1ZipFile zipFile = new C1ZipFile();

			try
			{							
				zipFile.Open(fileName);

				// Application
				C1ZipEntry entry = zipFile.Entries[0];
				// Get entry name and convert to guid
				// if invalid GUID, throw exception
				string name = entry.FileName;
				string applicationXml = string.Empty;
				string applicationArgumentsXml = string.Empty;

				// Get Scripting Application Xml.
				Guid g = new Guid(name);
				StreamReader reader = new StreamReader(entry.OpenReader(),System.Text.Encoding.UTF8);
				applicationXml= reader.ReadToEnd();				
				reader.Close();

				if ( zipFile.Entries.Count > 1 )
				{
					// Arguments
					entry = zipFile.Entries[1];
					reader = new StreamReader(entry.OpenReader(),System.Text.Encoding.UTF8);
					applicationArgumentsXml = reader.ReadToEnd();

					if ( applicationArgumentsXml.ToLower() != "none" )
					{
						_arguments = ScriptingApplicationArgs.FromXml(applicationArgumentsXml);
					} 
					else 
					{
						_arguments = null;
					}

					reader.Close();
				}


				// Get Custom Transforms
				int start = 2;
				for ( int i=2;i<zipFile.Entries.Count;i++ )
				{					
					entry = zipFile.Entries[i];

					if ( entry.FileName == "CustomTransformsSeparator" )
					{
						start = i+1;
						break;
					}

					// Add File References to Application Startup
					string location = System.Windows.Forms.Application.StartupPath + Path.DirectorySeparatorChar + entry.FileName;
					if ( !File.Exists(location) )
					{
						zipFile.Entries.Extract(i, location);
					}	
					// Add WebTransform Entry in Configuration.
					// if exists don't add.
					RegisterWebTransform(location);
					ScriptingApplicationSerializer.UpdateSerializer();
				}

				// Generate scripting application.
				XmlDocument document = new XmlDocument();
				document.LoadXml(applicationXml);
				_application = ScriptingApplication.Decrypt(document);
			//	_application = ScriptingApplication.FromXml(applicationXml);

				for ( int i=start;i<zipFile.Entries.Count;i++ )
				{					
					entry = zipFile.Entries[i];

					// Add File References to Internet Cache
					string location = Utils.AppLocation.InternetTemp + Path.DirectorySeparatorChar + _application.Header.ApplicationID + Path.DirectorySeparatorChar + entry.FileName;
					if ( File.Exists(location) )
					{
						File.Delete(location);
					}
					zipFile.Entries.Extract(i, location);
				}			
			}
			catch
			{
				throw;
			}
			finally
			{
				zipFile.Close();
			}
		}


		/// <summary>
		/// Opens a package from a web instance.
		/// </summary>
		/// <param name="fileName"> The package file name.</param>
		public void OpenPackageWeb(string fileName)
		{
			C1ZipFile zipFile = new C1ZipFile();
			//  package = new ScriptingApplicationPackage();

			try
			{							
				zipFile.Open(fileName);

				// Application
				C1ZipEntry entry = zipFile.Entries[0];
				// Get entry name and convert to guid
				// if invalid GUID, throw exception
				string name = entry.FileName;
				string applicationXml = string.Empty;
				string applicationArgumentsXml = string.Empty;

				// Get Scripting Application Xml.
				Guid g = new Guid(name);
				StreamReader reader = new StreamReader(entry.OpenReader(),System.Text.Encoding.UTF8);
				applicationXml= reader.ReadToEnd();				
				reader.Close();

				if ( zipFile.Entries.Count > 1 )
				{
					// Arguments
					entry = zipFile.Entries[1];
					reader = new StreamReader(entry.OpenReader(),System.Text.Encoding.UTF8);
					applicationArgumentsXml = reader.ReadToEnd();

					if ( applicationArgumentsXml.ToLower() != "none" )
					{
						_arguments = ScriptingApplicationArgs.FromXml(applicationArgumentsXml);
					} 
					else 
					{
						_arguments = null;
					}

					reader.Close();
				}


				// Get Custom Transforms
				int start = 2;
				for ( int i=2;i<zipFile.Entries.Count;i++ )
				{					
					entry = zipFile.Entries[i];

					if ( entry.FileName == "CustomTransformsSeparator" )
					{
						start = i+1;
						break;
					}

					// Add File References to Application Startup
					// string location = System.Windows.Forms.Application.StartupPath + Path.DirectorySeparatorChar + entry.FileName;
//					if ( !File.Exists(location) )
//					{
//						zipFile.Entries.Extract(i, location);
//					}	
					// Add WebTransform Entry in Configuration.
					// if exists don't add.
					// RegisterWebTransform(location);
					// ScriptingApplicationSerializer.UpdateSerializer();
				}

				// Generate scripting application.
				XmlDocument document = new XmlDocument();
				document.LoadXml(applicationXml);
				_application = ScriptingApplication.Decrypt(document);
				//	_application = ScriptingApplication.FromXml(applicationXml);
//
//				for ( int i=start;i<zipFile.Entries.Count;i++ )
//				{					
//					entry = zipFile.Entries[i];
//
//					// Add File References to Internet Cache
//					string location = Utils.AppLocation.InternetTemp + Path.DirectorySeparatorChar + _application.Header.ApplicationID + Path.DirectorySeparatorChar + entry.FileName;
//					if ( File.Exists(location) )
//					{
//						File.Delete(location);
//					}
//					zipFile.Entries.Extract(i, location);
//				}			
			}
			catch
			{
				throw;
			}
			finally
			{
				zipFile.Close();
			}
		}


		/// <summary>
		/// Registers the web transform in the system.
		/// </summary>
		/// <param name="location"> The assembly location.</param>
		private void RegisterWebTransform(string location)
		{
			Assembly assm = Assembly.LoadFile(location);
			Type[] types = WebTransform.LoadWebTransformsFromAssembly(assm);

			string configurationSection = "WebTransforms";

			WebTransformConfiguration transforms = (WebTransformConfiguration)ConfigManager.Read(configurationSection,false);			
			ArrayList list = new ArrayList();
			list.AddRange(transforms.Transforms);
			transforms.ClearTransformProviders();
			
			ArrayList matchList = new ArrayList();
			foreach ( TransformProvider provider in list )
			{
				Type t = Type.GetType(provider.Type);
				matchList.Add(t);
			}

			foreach ( Type type in types )
			{				
				if ( !matchList.Contains(type) )
				{
					TransformProvider provider = new TransformProvider();
					provider.Type = type.AssemblyQualifiedName;
					list.Add(provider);
				}
			}
			
			transforms.Transforms = (TransformProvider[])list.ToArray(typeof(TransformProvider));

			// Save Configuration
			ConfigManager.Write(configurationSection, transforms);
		}

		/// <summary>
		/// Creates a new ScriptingApplicationPackage.
		/// </summary>
		/// <param name="application"> The scripting application.</param>
		/// <param name="arguments"> The application arguments.</param>
		/// <param name="filePath"> The output file path.</param>
		/// <param name="doEncrypt"> Sets the encrypttion setting for a scripting application.</param>
		public static void CreatePackage(ScriptingApplication application, ScriptingApplicationArgs arguments, string filePath, bool doEncrypt)
		{
			try
			{
				if ( application.Header.ApplicationID == null )
				{
					application.Header.ApplicationID = System.Guid.NewGuid().ToString();
				}

				// Get all FileReference elements.
				FileReference[] fileReferences = application.GetFileReferences();
				string applicationPath = System.Windows.Forms.Application.UserAppDataPath;

				string temp = applicationPath + "/temp/";

				if ( !Directory.Exists(temp) )
				{
					Directory.CreateDirectory(temp);
				}

				C1.C1Zip.C1ZipFile zipFile = new C1.C1Zip.C1ZipFile();				
				zipFile.Create(filePath);			
				zipFile.Open(filePath);

				string argumentsFileName = application.Header.ApplicationID + "-applicationArguments.xml";

				FileReference argumentsFileReference = new FileReference(argumentsFileName);
				application.Header.ScriptingApplicationArgumentsReference = argumentsFileReference;

				// Add Scripting Application
				string xml = string.Empty;
				if ( doEncrypt )
				{
					xml = application.Encrypt();
				} 
				else 
				{
					xml = application.ToXml();
				}

				MemoryStream mem = new MemoryStream(System.Text.Encoding.UTF8.GetByteCount(xml));
				StreamWriter writer =  new StreamWriter(mem, System.Text.Encoding.UTF8);
				writer.Write(xml);
				writer.Flush();
				mem.Position = 0;
				zipFile.Entries.Add(writer.BaseStream, application.Header.ApplicationID);
				writer.Close();

				string argsXml = "None";
				if ( arguments != null )
				{
					// Add Scripting Application Arguments
					argsXml = arguments.ToXml();
				}
				mem = new MemoryStream(System.Text.Encoding.UTF8.GetByteCount(argsXml));
				writer =  new StreamWriter(mem, System.Text.Encoding.UTF8);
				writer.Write(argsXml);
				writer.Flush();
				mem.Position = 0;
				zipFile.Entries.Add(writer.BaseStream, argumentsFileName);
				writer.Close();

				FileReference[] customTransforms = application.GetCustomTransforms();

				// add custom transforms
				foreach ( FileReference reference in customTransforms )
				{
					if ( File.Exists(reference.FileName) )
					{
						FileInfo fileInfo = new FileInfo(reference.FileName);
						zipFile.Entries.Add(reference.FileName, fileInfo.Name);
					}
				}

				MemoryStream stream = new MemoryStream();
				stream.Write(new byte[] {0}, 0, 1);
				zipFile.Entries.Add(stream, "CustomTransformsSeparator");
				stream.Close();
								
				// add file references that are not custom transforms
				foreach ( FileReference reference in fileReferences )
				{					
					if ( File.Exists(reference.FileName) )
					{
						FileInfo fileInfo = new FileInfo(reference.FileName);
						zipFile.Entries.Add(reference.FileName, fileInfo.Name);
					}
				}
				
				zipFile.Close();
			} 
			catch
			{
				throw;
			}
		}

	}
}
