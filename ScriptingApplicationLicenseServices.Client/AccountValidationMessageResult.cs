using System;

namespace Ecyware.GreenBlue.LicenseServices.Client
{
	/// <summary>
	/// Summary description for AccountValidationMessageResult.
	/// </summary>
	public class AccountValidationMessageResult : ServiceContext
	{
		private bool _validated = false;

		/// <summary>
		/// Creates a new AccountValidationMessageResult.
		/// </summary>
		public AccountValidationMessageResult()
		{
		}

		/// <summary>
		/// Gets or sets if the account is validated.
		/// </summary>
		public bool Validated
		{
			get
			{
				return _validated;
			}
			set
			{
				_validated = value;
			}
		}
	}
}
