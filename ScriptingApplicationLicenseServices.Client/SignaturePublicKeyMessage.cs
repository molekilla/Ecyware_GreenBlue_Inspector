using System;

namespace Ecyware.GreenBlue.LicenseServices.Client
{
	/// <summary>
	/// Summary description for SignaturePublicKeyMessage.
	/// </summary>
	public class SignaturePublicKeyMessage : ServiceContext
	{
		string _pub = string.Empty;

		/// <summary>
		/// Creates a new SignaturePublicKeyMessage.
		/// </summary>
		public SignaturePublicKeyMessage()
		{
		}

		/// <summary>
		/// Gets or sets the public key.
		/// </summary>
		public string PublicKeyString
		{
			get
			{
				return _pub;
			}
			set
			{
				_pub = value;
			}
		}
	}
}
