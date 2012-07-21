// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: October 2004
using System;
using System.Xml.Serialization;
using System.Security.Cryptography.Xml;

namespace Ecyware.GreenBlue.Configuration.Encryption
{
	/// <summary>
	/// Summary description for KeyInfoEncryptedKey.
	/// </summary>	
	public class KeyInfoEncryptedKey
	{
		private EncryptedKey _encryptedKey;

		/// <summary>
		/// Creates a new KeyInfoEncryptedKey.
		/// </summary>
		public KeyInfoEncryptedKey()
		{
		}


		/// <summary>
		/// Creates a new KeyInfoEncryptedKey.
		/// </summary>
		/// <param name="encryptedKey"> The encrypted key.</param>
		public KeyInfoEncryptedKey(EncryptedKey encryptedKey)
		{
			_encryptedKey = encryptedKey;
		}


		/// <summary>
		/// Gets or sets the encrypted key.
		/// </summary>
		[XmlElement(Namespace="http://www.w3.org/2001/04/xmlenc#")]
		public EncryptedKey EncryptedKey
		{
			get
			{
				return _encryptedKey;
			}
			set
			{
				_encryptedKey = value;
			}
		}
	}
}
