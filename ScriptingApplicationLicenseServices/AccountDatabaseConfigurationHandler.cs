using System;
using System.Xml;
using System.Security.Cryptography;
using System.IO;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Configuration.Encryption;
using Ecyware.GreenBlue.Configuration.XmlTypeSerializer;
using Ecyware.GreenBlue.LicenseServices.Client;

namespace Ecyware.GreenBlue.LicenseServices
{
	/// <summary>
	/// Summary description for AccountDatabaseConfigurationHandler.
	/// </summary>
	[ConfigurationHandler(typeof(AccountDatabase))]
	public class AccountDatabaseConfigurationHandler : ConfigurationSection
	{
		/// <summary>
		/// Creates a new AccountDatabaseConfigurationHandler.
		/// </summary>
		public AccountDatabaseConfigurationHandler()
		{
		}

		/// <summary>
		/// Gets the RSA key.
		/// </summary>
		/// <returns> Returns a RSA key.</returns>
		private RSA ReadServerEncryptionKey()
		{
			// Get the public key from instance
			Stream pvk 
				= this.GetType().Assembly.GetManifestResourceStream(this.GetType().Namespace + ".ServerEncryptionDigitalSign.pvk");

			// Read in the key
			RSA key = new RSACryptoServiceProvider();
			using( StreamReader reader = new StreamReader(pvk) )
			{
				key.FromXmlString(reader.ReadLine());
			}

			return key;
		}


		public override void Save(object value, string sectionName, string fileName)
		{			
			XmlNode xmlNode = this.Serialize(value);
			XmlDocument document = new XmlDocument();
			document.AppendChild(document.ImportNode(xmlNode,true));
				
			// Encrypt xml
			EncryptXml enc = new EncryptXml(document);
			enc.AddKeyNameMapping("db", ReadServerEncryptionKey());

			XmlNodeList list = document.SelectNodes("//Password");

			foreach ( XmlNode n in list )
			{
				XmlElement el = (XmlElement)n;
				EncryptedData data = enc.Encrypt(el, "db");
				enc.ReplaceElement(el, data);
			}

			XmlTextWriter writer = new XmlTextWriter(fileName, null);
			writer.Formatting = Formatting.Indented;
			document.WriteTo(writer);
			writer.Flush();
			writer.Close();					
		}

		public override object Load(string sectionName, string fileName)
		{
			XmlDocument document = new XmlDocument();
			document.Load(fileName);

			// add EncryptedData to new document
			XmlNode node = document.SelectSingleNode("//EncryptedData");

			if ( node != null )
			{
				// decrypt
				EncryptXml decrypt = new EncryptXml(document);
				decrypt.AddKeyNameMapping("db", ReadServerEncryptionKey());
				decrypt.DecryptDocument();

				// create object (desearilize)
				AccountMessageSerializer serializer = new AccountMessageSerializer();
				AccountDatabase database = (AccountDatabase)Create(document);

				return database;
			}
			else 
			{
				return base.Load (sectionName, fileName);
			}
		}
	}
}

