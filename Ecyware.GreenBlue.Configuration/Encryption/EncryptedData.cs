// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: October 2004
using System;
using System.Collections;
using System.Xml.Serialization;

namespace Ecyware.GreenBlue.Configuration.Encryption
{
	/// <summary>
	/// Summary description for EncryptedData.
	/// </summary>
	[XmlType(TypeName="EncryptedData", Namespace="http://www.w3.org/2001/04/xmlenc#")]
	public class EncryptedData
	{
		KeyInfoEncryptedKey _keyInfo;
		EncryptionMethod _encMethod;
		CipherData _cipherData;

		/// <summary>
		/// Creates a new EncryptedData.
		/// </summary>
		public EncryptedData()
		{
		}

		/// <summary>
		/// Gets the type.
		/// </summary>
		[XmlAttribute("Type")]
		public string Type = "http://www.w3.org/2001/04/xmlenc#Element";


		/// <summary>
		/// Gets or sets the encryption method.
		/// </summary>	
		[XmlElement]
		public EncryptionMethod EncryptionMethod
		{
			get
			{
				return _encMethod;
			}
			set
			{
				_encMethod = value;
			}
		}

		/// <summary>
		/// Gets or sets the cipher data.
		/// </summary>
		[XmlElement]
		public CipherData CipherData
		{
			get
			{
				return _cipherData;
			}
			set
			{
				_cipherData = value;
			}
		}

		/// <summary>
		/// Adds a KeyInfo.
		/// </summary>
		/// <param name="encryptedKey"></param>
		public void AddKeyInfoClause(KeyInfoEncryptedKey encryptedKey)
		{
			_keyInfo = encryptedKey;
		}


		/// <summary>
		/// Gets or sets the KeyInfo collection.
		/// </summary>
		[XmlElement(ElementName="KeyInfo", Namespace="http://www.w3.org/2000/09/xmldsig#KeyInfoEncryptedKey")]
		public KeyInfoEncryptedKey KeyInfo
		{
			get
			{
				return _keyInfo;
			}
			set
			{
				_keyInfo = value;
			}
		}
	}
}
