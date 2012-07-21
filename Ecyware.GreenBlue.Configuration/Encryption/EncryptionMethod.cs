// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: October 2004
using System;
using System.Xml.Serialization;

namespace Ecyware.GreenBlue.Configuration.Encryption
{
	/// <summary>
	/// Summary description for EncryptionMethod.
	/// </summary>
	public class EncryptionMethod
	{
		private string _alg;

		/// <summary>
		/// Gets or sets the EncryptionMethod.
		/// </summary>
		public EncryptionMethod()
		{
		}

		/// <summary>
		/// Gets or sets the EncryptionMethod.
		/// </summary>
		/// <param name="algorithm"> The algorithm used.</param>
		public EncryptionMethod(string algorithm)
		{
			_alg = algorithm;
		}

		/// <summary>
		/// Gets or sets the algorithm.
		/// </summary>
		[XmlAttribute]
		public string Algorithm
		{
			get
			{
				return _alg;
			}
			set
			{
				_alg = value;
			}
			
		}
	}
}
