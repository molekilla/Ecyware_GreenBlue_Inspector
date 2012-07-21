using System;

namespace Ecyware.GreenBlue.LicenseServices.Client
{
	/// <summary>
	/// Summary description for RegisterApplicationResultMessage.
	/// </summary>
	public class RegisterApplicationResultMessage : ServiceContext
	{
		string _payload;
		bool _registered = false;
		string _message;
		string _newApplicationID = string.Empty;


		/// <summary>
		/// Creates a new SignScriptingApplicationResultMessage.
		/// </summary>
		public RegisterApplicationResultMessage()
		{
		}

		/// <summary>
		/// Gets or sets the message.
		/// </summary>
		public string Message
		{
			get
			{
				return _message;
			}
			set
			{
				_message = value;
			}
		}

		/// <summary>
		/// Gets or sets if the application is registered.
		/// </summary>
		public bool IsApplicationRegistered
		{
			get
			{
				return _registered;
			}
			set
			{
				_registered = value;
			}
		}

		/// <summary>
		/// Gets or sets the XML for the signed scripting application.
		/// </summary>
		public string SignedScriptingApplicationXml
		{
			get
			{
				return _payload;
			}
			set
			{
				_payload = value;
			}
		}

		/// <summary>
		/// Gets or sets the new application id.
		/// </summary>
		public string NewApplicationID
		{
			get
			{
				return _newApplicationID;
			}
			set
			{
				_newApplicationID = value;
			}
		}
	}
}
