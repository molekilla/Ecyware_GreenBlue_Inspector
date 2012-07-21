using System;

namespace Ecyware.GreenBlue.LicenseServices.Client
{
	/// <summary>
	/// Summary description for RegisterApplicationMessage.
	/// </summary>
	public class RegisterApplicationMessage : ServiceContext
	{
		string _applicationID;
		string _encryptedXml;

		/// <summary>
		/// Creates a new RegisterApplicationMessage.
		/// </summary>
		public RegisterApplicationMessage()
		{
		}

		/// <summary>
		/// Gets or sets the scripting application header.
		/// </summary>
		public string ApplicationID
		{
			get
			{
				return _applicationID;
			}
			set
			{
				_applicationID = value;
			}
		}

		/// <summary>
		/// Gets or sets the encrypted scripting application xml.
		/// </summary>
		public string EncryptedScriptingApplicationXml
		{
			get
			{
				return _encryptedXml;
			}
			set
			{
				_encryptedXml = value;
			}
		}
	}
}
