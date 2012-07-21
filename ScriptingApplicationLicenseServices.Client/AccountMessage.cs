using System;

namespace Ecyware.GreenBlue.LicenseServices.Client
{
	/// <summary>
	/// Summary description for AccountMessage.
	/// </summary>
	public class AccountMessage : ServiceContext
	{
		Account _account = new Account();
		bool _accountExists;

		/// <summary>
		/// Creates a new AccountMessage.
		/// </summary>
		public AccountMessage()
		{
		}

		/// <summary>
		/// Creates a new AccountMessage.
		/// </summary>
		/// <param name="account"> The Account type.</param>
		public AccountMessage(Account account)
		{
			_account = account;
		}

		/// <summary>
		/// Gets or sets the current account.
		/// </summary>
		public Account CurrentAccount
		{
			get
			{
				return _account;
			}
			set
			{
				_account = value;
			}
		}

		/// <summary>
		/// Gets or sets a boolean value if the account exists.
		/// </summary>
		public bool AccountExists
		{
			get
			{
				return _accountExists;
			}
			set
			{
				_accountExists = value;
			}
		}
	}
}
