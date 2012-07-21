using System;
using System.Collections;
using System.Threading;
using Microsoft.Web.Services2.Security;
using Microsoft.Web.Services2.Security.Tokens;
using Ecyware.GreenBlue.LicenseServices.Client;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace Ecyware.GreenBlue.LicenseServices
{
	/// <summary>
	/// Summary description for UserManagerStore.
	/// </summary>	
	public class AccountDatabase
	{
		Hashtable _userstore = new Hashtable(100);
		Hashtable _syncUserstore;
		ArrayList _accounts = new ArrayList();

		/// <summary>
		/// Creates a new UserManagerStore.
		/// </summary>
		public AccountDatabase()
		{
		}

		#region Properties
		/// <summary>
		/// Gets or sets the accounts.
		/// </summary>
		public Account[] Accounts
		{
			get
			{
				return (Account[])_accounts.ToArray(typeof(Account));
			}
			set
			{
				if ( value != null )
				{
					_accounts.AddRange(value);
				}
			}
		}

		#endregion
		#region Methods
		/// <summary>
		/// Removes an account.
		/// </summary>
		/// <param name="userAccount"> The account to remove.</param>
		/// <returns> Returns true if removed, else false.</returns>
		public bool RemoveAccount(Account userAccount)
		{
			if ( _syncUserstore[userAccount.Username] != null )
			{
				Account removeAccount = (Account)_syncUserstore[userAccount.Username];

				// remove
				_syncUserstore.Remove(userAccount.Username);
				_accounts.Remove(removeAccount);
				return true;
			} 
			else 
			{
				// not found.
				return false;
			}
		}

		/// <summary>
		/// Adds a new account.
		/// </summary>
		/// <param name="userAccount"> The account to add.</param>
		/// <returns> Returns true to add, else false.</returns>
		public bool AddAccount(Account userAccount)
		{
			if ( _syncUserstore[userAccount.Username] == null )
			{
				// add
				_syncUserstore.Add(userAccount.Username, userAccount);
				_accounts.Add(userAccount);
				return true;
			} 
			else 
			{
				// already exists, change username.
				return false;
			}
		}


		/// <summary>
		/// Gets an account.
		/// </summary>
		/// <param name="username"> The account username.</param>
		/// <returns> Returns an Account object, else null.</returns>
		public Account GetAccount(string username)
		{
			if ( _syncUserstore[username] != null )
			{
				Account existing = (Account)_syncUserstore[username];

				return existing;
			} 
			else 
			{
				return null;
			}
		}
		/// <summary>
		/// Loads the account store.
		/// </summary>
		public void LoadAccountStore()
		{		
			if ( _syncUserstore == null )
			{
				foreach ( Account account in _accounts )
				{
					_userstore.Add(account.Username, account);
				}

				_syncUserstore = Hashtable.Synchronized(_userstore);
			}
		}

		/// <summary>
		/// Validates the account.
		/// </summary>
		/// <param name="token"> The account to validate.</param>
		/// <returns> Returns true if password is validated, else false.</returns>
		public string GetPasswordToken(UsernameToken token)
		{
			if ( _syncUserstore[token.Username] != null )
			{
				Account existing = (Account)_syncUserstore[token.Username];

				return existing.Password;
			} 
			else 
			{
				return string.Empty;
			}
		}

//		public static bool PasswordMatches(string secret, string hash)
//		{
//			//byte[] nonceBytes = Convert.FromBase64String(nonce);
//			//string newHash = ComputePasswordDigest(nonceBytes, created, secret);
//			if ( secret == hash )
//				return true;
//			return false;
//		}

//		public static bool PasswordMatches(string secret, string nonce, string created, string pwDigest)
//		{
//			byte[] nonceBytes = Convert.FromBase64String(nonce);
//			string newHash = ComputePasswordDigest(nonceBytes, created, secret);
//			if ( newHash == pwDigest )
//				return true;
//			return false;
//		}
//
//		public static string ComputePasswordDigest(byte[] nonce, string created, string secret)
//		{
//			//Digest = Base64(SHA-1(Nonce + Created + Password)) 
//			if ((nonce == null) || (nonce.Length == 0))
//				throw new ArgumentNullException("nonce");
//			if (secret == null)
//				throw new ArgumentNullException("secret");
//
//			byte[] b1 = Encoding.UTF8.GetBytes(created); //XmlConvert.ToString(created, "yyyy-MM-ddTHH:mm:ssZ"));
//			byte[] b2 = Encoding.UTF8.GetBytes(secret);
//			byte[] b3 = new byte[nonce.Length + b1.Length + b2.Length];
//			Array.Copy(nonce, b3, nonce.Length);
//			Array.Copy(b1, 0, b3, nonce.Length, b1.Length);
//			Array.Copy(b2, 0, b3, (int) (nonce.Length + b1.Length), b2.Length);
//			return Convert.ToBase64String(SHA1.Create().ComputeHash(b3));
//		}
//

		#endregion

	}
}
