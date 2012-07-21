using System;
using System.Data;
using System.Net;
using System.IO;
using System.Web;
using System.Configuration;

namespace Ecyware.GreenBlue.LicenseServices
{
	public class AccountDatabaseCacheDependencyState
	{
		private int _count;
		private string _path;

		/// <summary>
		/// Creates a new AccountDatabaseCacheDependencyState.
		/// </summary>
		public AccountDatabaseCacheDependencyState()
		{
		}

		/// <summary>
		/// Gets or sets the account count.
		/// </summary>
		public int CurrentAccountCount
		{
			get
			{
				return _count;
			}
			set
			{
				_count = value;
			}
		}
		/// <summary>
		/// Gets or sets the AccountDatabasePath.
		/// </summary>
		public string AccountDatabasePath
		{
			get
			{
				return _path;
			}
			set
			{
				_path = value;
			}
		}

	}

	/// <summary>
	/// Create a cache dependency for the AccountDatabase
	/// </summary>
	public class AccountDatabaseCacheDependency : CacheDependency
	{
		protected AccountDatabaseCacheDependencyState _state;

		public AccountDatabaseCacheDependency(string key,AccountDatabaseCacheDependencyState state, int pollTime) : base(key, pollTime)
		{
			_state = state;
			//Polling = pollTime;
		}

		protected override bool HasChanged()
		{
			// Make access to the Web service 
			// string response = GetBooksInfo();

			// GetBooksInfo reads all the data and compares that to the cached snapshot.
			// If you have access to the Web Service, you might want to use a separate,
			// ad-hoc method to check for changes. For example, a method that simply
			// returns a Boolean value.

			// Compare data  
			// bool hasChanged = (response != (string) HttpRuntime.Cache[DependentStorageKey]);

			bool hasChanged = false;
			if ( HttpRuntime.Cache[DependentStorageKey] is AccountDatabaseCacheDependencyState )
			{
				AccountDatabaseCacheDependencyState state = (AccountDatabaseCacheDependencyState)HttpRuntime.Cache[DependentStorageKey];
			 				
				if ( AccountManager.AccountDatabase.Accounts.Length != state.CurrentAccountCount )
				{
					// update
					// hasChanged = true;
					AccountManager.SaveAccountDatabase(state.AccountDatabasePath);
				}			
			}
			return hasChanged;
		}

	}
}
