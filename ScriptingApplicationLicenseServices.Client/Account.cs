using System;

namespace Ecyware.GreenBlue.LicenseServices.Client
{
	/// <summary>
	/// Summary description for AccountMessage.
	/// </summary>
	public class Account
	{		
		private DateTime _updated;
		private int _currentRegisteredApplication;
		private int _applicationLimit;
		private string _username = string.Empty;
		private string _password = string.Empty;
		private string _name = string.Empty;
		private string _email = string.Empty;
		private DateTime _created;

		/// <summary>
		/// Creates a new Account.
		/// </summary>
		public Account()
		{
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}

		/// <summary>
		/// Gets or sets the username.
		/// </summary>
		public string Username
		{
			get
			{
				return _username;
			}
			set
			{
				_username = value;
			}
		}

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				_password = value;
			}
		}

		/// <summary>
		/// Gets or sets the email.
		/// </summary>
		public string Email
		{
			get
			{
				return _email;
			}
			set
			{
				_email = value;
			}
		}

		/// <summary>
		/// Gets or sets the creation date.
		/// </summary>
		public DateTime Created
		{
			get
			{
				return _created;
			}
			set
			{
				_created = value;
			}
		}

		/// <summary>
		/// Gets or sets the updated datetime.
		/// </summary>
		public DateTime Updated
		{
			get
			{
				return _updated;
			}
			set
			{
				_updated = value;
			}
		}
		/// <summary>
		/// Gets or sets the application limit.
		/// </summary>
		public int ApplicationLimit
		{
			get
			{
				return _applicationLimit;
			}
			set
			{
				_applicationLimit = value;
			}
		}
		/// <summary>
		/// Gets or sets the current registered application count.
		/// </summary>
		public int RegisteredApplicationCount
		{
			get
			{
				return _currentRegisteredApplication;
			}
			set
			{
				_currentRegisteredApplication = value;
			}
		}

	}
}
