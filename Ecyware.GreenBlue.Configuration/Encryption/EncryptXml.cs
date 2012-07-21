// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: October 2004
using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace Ecyware.GreenBlue.Configuration.Encryption
{
	/// <summary>
	/// Summary description for EncryptXml.
	/// </summary>
	public class EncryptXml
	{
		public static string XmlEncRSA1_5Url = "http://www.w3.org/2001/04/xmlenc#rsa-1_5";
		public static string XmlEncAES256Url = "http://www.w3.org/2001/04/xmlenc#aes256-cbc";
		private XmlDocument _document;

		// private RSA _key;
		private Hashtable _keyMappings = new Hashtable();

		/// <summary>
		/// Creates a new EncryptXml.
		/// </summary>
		public EncryptXml()
		{
		}

		/// <summary>
		/// Creates a new EncryptXml.
		/// </summary>
		/// <param name="document"> The XmlDocument type.</param>
		public EncryptXml(XmlDocument document)
		{
			_document = document;
		}

		/// <summary>
		/// Loads a XmlNode.
		/// </summary>
		/// <param name="node"> The node to load to the document.</param>
		/// <remarks> This replaces the existing document, if any.</remarks>
		public void LoadXmlNode(XmlNode node)
		{
			_document = new XmlDocument();
			XmlNode n =_document.ImportNode(node, true);
			_document.AppendChild(n);
		}

		/// <summary>
		/// Reads the document element. Use this when using LoadXmlNode.
		/// </summary>
		/// <returns> A XmlNode.</returns>
		/// <remarks> Use this when using LoadXmlNode.</remarks>
		public XmlNode ReadXmlNode()
		{
			return _document.DocumentElement;
		}


		/// <summary>
		/// Gets or sets the XmlDocument.
		/// </summary>
		public XmlDocument Document
		{
			get
			{
				return _document;
			}
			set
			{
				_document = value;
			}
		}


		/// <summary>
		/// Creates a new key using the machine store.
		/// </summary>
		/// <param name="keyName"> The key name.</param>
		/// <returns>A RSA key.</returns>
		public RSA CreateMachineStoreKey(string keyName)
		{
			// creates the CspParameters object and sets the key container name used to store the RSA key pair
			CspParameters cp = new CspParameters(1);
			cp.Flags = CspProviderFlags.UseMachineKeyStore;
			cp.KeyContainerName = keyName;
			cp.KeyNumber = 2;			
			
			// instantiates the rsa instance accessing the key container MyKeyContainerName
			// RSACryptoServiceProvider key = new RSACryptoServiceProvider(cp);
			RSACryptoServiceProvider.UseMachineKeyStore = true;

			return new RSACryptoServiceProvider(cp);
		}
		/// <summary>
		/// Gets a existing key from the machine store.
		/// </summary>
		/// <param name="keyName"> The key name.</param>
		/// <returns>A RSA key.</returns>
		public RSA GetMachineStoreKey(string keyName)
		{
			// creates the CspParameters object and sets the key container name used to store the RSA key pair
			CspParameters cp = new CspParameters(1);
			cp.Flags = CspProviderFlags.UseMachineKeyStore;
			cp.KeyContainerName = keyName;
			cp.KeyNumber = 2;			
			
			// instantiates the rsa instance accessing the key container MyKeyContainerName
			//RSACryptoServiceProvider key = new RSACryptoServiceProvider(cp);
			RSACryptoServiceProvider.UseMachineKeyStore = true;

			return new RSACryptoServiceProvider(cp);
		}

		/// <summary>
		/// Adds a key mapping.
		/// </summary>
		/// <param name="keyName"> The key name.</param>
		/// <param name="keys"> The keys.</param>
		public void AddKeyNameMapping(string keyName, RSA keys)
		{			
			// instantiates the rsa instance accessing the key container MyKeyContainerName
			_keyMappings.Add(keyName, keys);
		}


		/// <summary>
		/// Reads a key from a file.
		/// </summary>
		/// <param name="stream"> The file stream.</param>
		/// <returns> Returns a RSA key.</returns>
		public static RSA ReadRSAKey(Stream keyStream)
		{
			// read in the key
			RSA key = new RSACryptoServiceProvider();
			using(StreamReader reader = new StreamReader(keyStream))
			{
				key.FromXmlString(reader.ReadLine());
			}

			return key;
		}

		/// <summary>
		/// Encryps the XML element.
		/// </summary>
		/// <param name="element"> The element to encrypt.</param>
		/// <param name="keyName"> The key name.</param>
		/// <returns> A EncryptedData type.</returns>
		public EncryptedData Encrypt(XmlElement element, string keyName)
		{
			// Get Key from key mappings
			RSA key = (RSA)_keyMappings[keyName];
			
			// Import key
			RSACryptoServiceProvider crypto = new RSACryptoServiceProvider();
			RSAParameters param = new RSAParameters();
			
			crypto.ImportParameters(key.ExportParameters(false));

			// Create session key
			RijndaelManaged sessionKey = new RijndaelManaged();
			sessionKey.KeySize = 256;
			
			// Encrypt the symmetric key and IV (session key encryption).
			byte[] encryptedSymmetricKey = crypto.Encrypt(sessionKey.Key, false);
			//byte[] encryptedSymmetricIV = crypto.Encrypt(sessionKey.IV, false);

			// Create a new EncryptedKey
			EncryptedKey ek = new EncryptedKey();
			ek.CipherData = new CipherData(encryptedSymmetricKey);
			ek.EncryptionMethod = new EncryptionMethod(EncryptXml.XmlEncRSA1_5Url);

			// set up a key info clause for the key that was used to encrypt the session key 
//			KeyInfoName keyName = new KeyInfoName(); 
//			keyName.Value = keyName;
			// TODO: KeyInfo.AddClause.
			ek.KeyInfo = new KeyInfo(keyName);

			byte[] encryptedData = EncryptData(element, sessionKey);

			// create the encrypted data 
			EncryptedData ed = new EncryptedData();
			ed.CipherData = new CipherData(encryptedData);
			//ed.Type = EncryptedXml.XmlEncElementUrl;
			ed.EncryptionMethod = new EncryptionMethod(EncryptXml.XmlEncAES256Url);
			ed.AddKeyInfoClause(new KeyInfoEncryptedKey(ek)); 

			return ed;
		}


		private byte[] DecryptSessionKey(EncryptedKey encryptedKey)
		{
			// Get RSA Key
			RSA key = (RSA)_keyMappings[encryptedKey.KeyInfo.KeyName];

			// Import key
			CspParameters CSPParam = new CspParameters();
			CSPParam.Flags = CspProviderFlags.UseMachineKeyStore;
			RSACryptoServiceProvider crypto = new RSACryptoServiceProvider(CSPParam);
			crypto.ImportParameters(key.ExportParameters(true));

			// Create session key
			RijndaelManaged sessionKey = new RijndaelManaged();
			sessionKey.KeySize = 256;
			
			// Encrypt the symmetric key and IV (session key encryption).
			byte[] symmetricKey = crypto.Decrypt(Convert.FromBase64String(encryptedKey.CipherData.CipherValue), false);			

			return symmetricKey;
		}

		public void DecryptDocument()
		{
			// find all EncryptedData
			XmlNodeList encryptedDataList = _document.SelectNodes("//EncryptedData");

			foreach ( XmlNode node in encryptedDataList )
			{
				EncryptedData data = Deserialize(node);
				
				// lookup
				if ( data.EncryptionMethod.Algorithm == EncryptXml.XmlEncAES256Url )
				{
					// Get the session key
					byte[] sessionKey = DecryptSessionKey(data.KeyInfo.EncryptedKey);

					// Decrypt element
					XmlElement element = DecryptData(Convert.FromBase64String(data.CipherData.CipherValue), sessionKey);

					// Replace
					XmlNode newNode = _document.ImportNode(element,true);
					node.ParentNode.ReplaceChild(newNode, node);					
				} else {
					throw new Exception("EncryptionMethod Algorithm not found or valid.");
				}
			}
		}

		/// <summary>
		/// Replaces the current element.
		/// </summary>
		/// <param name="element"> The original element unencrypted.</param>
		/// <param name="encryptedElement"> The EncryptedData type.</param>
		public void ReplaceElement(XmlElement element, EncryptedData encryptedElement)
		{
			XmlNode encryptedNode = Serialize(encryptedElement);
			XmlNode newNode = _document.ImportNode(encryptedNode, true);
			element.ParentNode.ReplaceChild(newNode, element);
		}


		private EncryptedData Deserialize(XmlNode encryptedDataNode)
		{
			XmlSerializer ser = new XmlSerializer(typeof(EncryptedData));
			EncryptedData ed = (EncryptedData)ser.Deserialize(new XmlNodeReader(encryptedDataNode));
			return ed;			
		}

		private XmlNode Serialize(EncryptedData encryptedElement)
		{
			XmlSerializer ser = new XmlSerializer(typeof(EncryptedData));
			
			// Serialize object to xml
			StringWriter sw = new StringWriter( System.Globalization.CultureInfo.CurrentUICulture );
			ser.Serialize(sw, encryptedElement);
			sw.Flush();

			// Convert to a XmlNode
			XmlDocument doc = new XmlDocument();
			doc.LoadXml( sw.ToString() );	
			
			return doc.DocumentElement;
		}

		/// <summary>
		/// Encrypts the data.
		/// </summary>
		/// <param name="element"> The element to encrypt.</param>
		/// <param name="sessionKey"> The session key.</param>
		/// <returns></returns>
		private byte[] EncryptData(XmlElement element, RijndaelManaged sessionKey)
		{
			UnicodeEncoding textConverter = new UnicodeEncoding();

			// Encrypt Data
			ICryptoTransform encryptor = sessionKey.CreateEncryptor();

			//Encrypt the data.
			MemoryStream msEncrypt = new MemoryStream();
			CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

			// Convert the data to a byte array.
			byte[] toEncrypt = textConverter.GetBytes(element.OuterXml);

			// Write the IV unencrypted
			csEncrypt.Write(sessionKey.IV, 0, sessionKey.IV.Length);

			// Write all data to the crypto stream and flush it.
			csEncrypt.Write(toEncrypt, 0, toEncrypt.Length);
			csEncrypt.FlushFinalBlock();			

			// Get encrypted array of bytes.
			byte[] encrypted = msEncrypt.ToArray();

			msEncrypt.Close();
			csEncrypt.Close();

			return encrypted;
		}

		private XmlElement DecryptData(byte[] data, byte[] sessionKey )
		{
			UnicodeEncoding textConverter = new UnicodeEncoding();

			// Create session key
			RijndaelManaged key = new RijndaelManaged();
			key.KeySize = 256;

			// Get unencrypted IV from data
			byte[] iv = new byte[key.IV.Length];
			MemoryStream msDecrypt = new MemoryStream(data);
			msDecrypt.Read(iv, 0, iv.Length);
			
			ICryptoTransform desencryptor = key.CreateDecryptor(sessionKey, iv);

			//Decrypt the data.			
			CryptoStream csDecrypt = new CryptoStream(msDecrypt, desencryptor, CryptoStreamMode.Read);
			
			byte[] fromEncrypt = new byte[data.Length - iv.Length];

			// Read the data out of the crypto stream
			csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);
			
			string text = textConverter.GetString(fromEncrypt);

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(text);

			msDecrypt.Close();
			csDecrypt.Close();

			return (XmlElement)doc.DocumentElement;
		}
	}
}
