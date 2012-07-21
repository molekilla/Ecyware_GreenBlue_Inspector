using System;
using System.Threading;
using Ecyware.GreenBlue.Configuration;

namespace Ecyware.GreenBlue.LicenseServices
{
	/// <summary>
	/// Summary description for AccountManager.
	/// </summary>	
	public class AccountManager
	{
		static Timer autoSavingTimer;
		static AccountDatabase _accountDatabase = null;

		/// <summary>
		/// Creates a new AccountManager.
		/// </summary>
		static AccountManager()
		{
		}

		/// <summary>
		/// Gets or sets the account database.
		/// </summary>
		public static AccountDatabase AccountDatabase
		{
			get
			{
				return _accountDatabase;
			}
			set
			{
				_accountDatabase = value;
			}
		}

		/// <summary>
		/// Loads te account database.
		/// </summary>
		public static void LoadAccountDatabase()
		{
			if ( _accountDatabase == null )
			{
				AccountDatabaseConfigurationHandler databaseManager = new AccountDatabaseConfigurationHandler();			

				if ( System.Web.HttpContext.Current == null )
				{
					_accountDatabase = (AccountDatabase)databaseManager.Load("Accounts", "AccountDatabase.xml");
				} 
				else 
				{
					_accountDatabase = (AccountDatabase)databaseManager.Load("Accounts", System.Web.HttpContext.Current.Server.MapPath("users/AccountDatabase.xml"));
				}
				_accountDatabase.LoadAccountStore();
				StartStoreAutoSaving();
			}
		}

		#region User Store Auto Saving
		/// <summary>
		/// Starts the store auto saving.
		/// </summary>
		public static void StartStoreAutoSaving()
		{
			// save store to disk every hour.
			TimeSpan dueTime = new TimeSpan(0,0,0,60,0);

			if ( System.Web.HttpContext.Current == null )
			{
				autoSavingTimer = new Timer(new TimerCallback(SaveAccountDatabase), null, dueTime, dueTime);
			} 
			else 
			{
				// Timer timer = new Timer(new TimerCallback(SaveAccountDatabase), null, dueTime, dueTime);
				// System.Web.HttpRuntime.Cache.Insert("SalsTimer", timer);				

				AccountDatabaseCacheDependencyState state = new AccountDatabaseCacheDependencyState();
				state.CurrentAccountCount =  AccountManager.AccountDatabase.Accounts.Length;
				state.AccountDatabasePath = System.Web.HttpContext.Current.Server.MapPath("users/AccountDatabase.xml");
				AccountDatabaseCacheDependency dep = new AccountDatabaseCacheDependency("AccountDBUpdate", state, 120);
                
				// Create the cache entry
				CacheHelper.Insert("AccountDBUpdate", state, dep);
			}
		}

		/// <summary>
		/// Stops the store auto saving.
		/// </summary>
		public static void StopStoreAutoSaving()
		{
			if ( autoSavingTimer != null )
				autoSavingTimer.Dispose();
		}

		/// <summary>
		/// Saves the database.
		/// </summary>
		/// <param name="state"> The object state.</param>
		public static void SaveAccountDatabase(object state)
		{
			AccountDatabaseConfigurationHandler accountDatabaseHandler = new AccountDatabaseConfigurationHandler();

			string file;

			if ( state == null )
			{
				file = "AccountDatabase.xml";
			} 
			else 
			{
				file = (string)state;
			}

			accountDatabaseHandler.Save(_accountDatabase, "Accounts", file);
		}


		#endregion
	}
}
