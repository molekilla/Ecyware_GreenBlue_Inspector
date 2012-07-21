using System;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;
using System.IO;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Configuration.Encryption;


namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Summary description for SecureLoginCredentials.
	/// </summary>
	public class SecureLoginCredentials
	{
		private static string location = System.Windows.Forms.Application.UserAppDataPath + "\\gbs_secl_encc.dat";
		SecureLoginCredentialsSerializer serializer = new SecureLoginCredentialsSerializer();
		string _username;
		string _password;

		/// <summary>
		/// Creates a new SecureLoginCredentials.
		/// </summary>
		public SecureLoginCredentials()
		{
		}


		/// <summary>
		/// Creates a new SecureLoginCredentials.
		/// </summary>
		/// <param name="username"> The username.</param>
		/// <param name="password"> The password.</param>
		public SecureLoginCredentials(string username, string password)
		{
			_username = username;
			_password = password;
		}

		/// <summary>
		/// Returns the secure login credentials as XML.
		/// </summary>
		/// <returns> Returns the secure login credentials as XML.</returns>
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

//		/// <summary>
//		/// Saves a ScriptingData.
//		/// </summary>
//		public void Save()
//		{
//			// Write
//			XmlNode node = serializer.Serialize(this, true);
//
//			// Write			
//			XmlDocument document = new XmlDocument();
//			XmlNode imported = document.ImportNode(node,true);
//			document.AppendChild(imported);
//			document.Save(location);
//		}

		/// <summary>
		/// Removes any existing secure login credentials.
		/// </summary>
		public static void Remove()
		{
			if ( File.Exists(location) )
			{
				File.Delete(location);
			}
		}

		/// <summary>
		/// Returns true if the login cred exists, else false.
		/// </summary>
		/// <returns>Returns true if the login cred exists, else false.</returns>
		public static bool Exists()
		{
			return File.Exists(location);
		}
		/// <summary>
		/// Saves the scripting application.
		/// </summary>
		public void Save()
		{
			try
			{				
				XmlDocument document = new XmlDocument();
				document.LoadXml(this.ToXml());
				
				// Encrypt xml
				EncryptXml enc = new EncryptXml(document);
				enc.AddKeyNameMapping("slcreds", enc.CreateMachineStoreKey("Ecyware.SecLogCreds"));

				XmlElement el = (XmlElement)document.FirstChild;
				EncryptedData data = enc.Encrypt(el, "slcreds");
				enc.ReplaceElement(el, data);

				document.Save(location);
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Decrypts a XmlDocument representing a secure login credentials.
		/// </summary>
		/// <param name="encryptedDocument"> A XmlDocument type.</param>
		/// <returns> A decrypted secure login credentials.</returns>
		public static SecureLoginCredentials Load()
		{
			XmlDocument encryptedDocument = new XmlDocument();
			encryptedDocument.Load(location);

			// add to new document
			XmlNode node = encryptedDocument.SelectSingleNode("//EncryptedData");
			SecureLoginCredentials sec = null;
			SecureLoginCredentialsSerializer serializer = new SecureLoginCredentialsSerializer();

			if ( node != null )
			{
				XmlDocument document = new XmlDocument();
				document.AppendChild(document.ImportNode(node,true));

				// decrypt
				EncryptXml decrypt = new EncryptXml(document);
				decrypt.AddKeyNameMapping("slcreds", decrypt.GetMachineStoreKey("Ecyware.SecLogCreds"));
				decrypt.DecryptDocument();
				
				sec = (SecureLoginCredentials)serializer.Create(document.DocumentElement.OuterXml);
			}

			return sec;
		}

		/// <summary>
		/// Gets or sets the username.
		/// </summary>
		public string Username
		{
			get
			{
				return _username;
			}
			set
			{
				_username = value;
			}
		}

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				_password = value;
			}
		}
	}
}
