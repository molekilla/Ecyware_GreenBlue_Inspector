// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: October 2004
using System;
using System.Xml.Serialization;

namespace Ecyware.GreenBlue.Configuration.Encryption
{
	/// <summary>
	/// Summary description for EncryptKey.
	/// </summary>	
	public class EncryptedKey
	{
		private EncryptionMethod _encMethod;
		private KeyInfo _keyInfo;
		private CipherData _cipherData;

		/// <summary>
		/// Creates a new EncryptedKey.
		/// </summary>
		public EncryptedKey()
		{
		}

		/// <summary>
		/// Gets or sets the EncryptionMethod.
		/// </summary>
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
		/// Gets or sets the KeyInfo.
		/// </summary>
		[XmlElement(Namespace="http://www.w3.org/2000/09/xmldsig#KeyInfo")]
		public KeyInfo KeyInfo
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

		/// <summary>
		/// Gets or sets the CipherData.
		/// </summary>
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
	}
}
