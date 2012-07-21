// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: October 2004
using System;
using System.Xml.Serialization;

namespace Ecyware.GreenBlue.Configuration.Encryption
{
	/// <summary>
	/// Summary description for KeyInfo.
	/// </summary>	
	public class KeyInfo
	{
		string _keyName;

		/// <summary>
		/// Creates a new KeyInfo.
		/// </summary>
		public KeyInfo()
		{
		}

		/// <summary>
		/// Creates a new KeyInfo.
		/// </summary>
		/// <param name="keyName"> The key name.</param>
		public KeyInfo(string keyName)
		{
			_keyName = keyName;
		}

		/// <summary>
		/// Gets or sets the key name.
		/// </summary>
		[XmlElement]
		public string KeyName
		{
			get
			{
				return _keyName;
			}
			set
			{
				_keyName = value;
			}
		}
	}
}
