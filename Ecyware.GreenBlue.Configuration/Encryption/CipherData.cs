// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: October 2004
using System;
using System.Text;

namespace Ecyware.GreenBlue.Configuration.Encryption
{
	/// <summary>
	/// Summary description for CipherData.
	/// </summary>
	public class CipherData
	{
		private string _cipherValue;		

		/// <summary>
		/// Creates a new CipherData.
		/// </summary>
		public CipherData()
		{
		}

		/// <summary>
		/// Creates a new CipherData.
		/// </summary>
		/// <param name="encryptedKey"> The encrypted key in bytes.</param>
		public CipherData(byte[] encryptedKey)
		{
			//ASCIIEncoding textConverter = new ASCIIEncoding();
			_cipherValue = Convert.ToBase64String(encryptedKey);
		}


		/// <summary>
		/// Gets or sets the cipher value.
		/// </summary>
		public string CipherValue
		{
			get
			{
				return _cipherValue;
			}
			set
			{
				_cipherValue = value;
			}

		}
	}
}
