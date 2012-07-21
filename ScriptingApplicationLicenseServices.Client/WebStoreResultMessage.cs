// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: March 2005
using System;

namespace Ecyware.GreenBlue.LicenseServices.Client
{
	/// <summary>
	/// Summary description for WebStoreResultMessage.
	/// </summary>
	public class WebStoreResultMessage : ServiceContext
	{
		string _payload;
		bool _registered = false;
		string _message;
		//string _newApplicationID = string.Empty;


		/// <summary>
		/// Creates a new WebStoreResultMessage.
		/// </summary>
		public WebStoreResultMessage()
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
		/// Gets or sets the application data.
		/// </summary>
		public string ApplicationData
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

//		/// <summary>
//		/// Gets or sets the new application id.
//		/// </summary>
//		public string NewApplicationID
//		{
//			get
//			{
//				return _newApplicationID;
//			}
//			set
//			{
//				_newApplicationID = value;
//			}
//		}
	}
}
