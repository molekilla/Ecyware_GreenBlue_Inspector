using System;
using System.Data;
using System.IO;
using System.Security.Cryptography;
using System.Xml;
using System.Web.Services.Protocols;
using System.Collections;
using Microsoft.Web.Services2;
using Microsoft.Web.Services2.Addressing;
using Microsoft.Web.Services2.Messaging;
using Microsoft.Web.Services2.Security;
using Microsoft.Web.Services2.Security.Tokens;
using Ecyware.GreenBlue.Configuration;


namespace Ecyware.GreenBlue.LicenseServices
{
	/// <summary>
	/// Summary description for LicenseServicesAuthenticationManager.
	/// </summary>
	public class LicenseServicesAuthenticationManager : UsernameTokenManager
	{	
		/// <summary>
		/// Authenticates the token.
		/// </summary>
		/// <param name="token"> The UsernameToken to authenticate.</param>
		/// <returns> Returns the user's password.</returns>
		protected override string AuthenticateToken(UsernameToken token)
		{	
			string result = string.Empty;
	
			if ( token.Id == "LicenseToken" )
			{				
				// Login user
				if ( ValidateUsernameToken(token) )
				{
					result = token.Password;
				} 
				else 
				{
					throw new SoapException(
						"Missing security token", 
						SoapException.ClientFaultCode);
				}
			}

			return result;
		}

		/// <summary>
		/// Creates a hash for the password.
		/// </summary>
		/// <param name="sessionId"> The current session id.</param>
		/// <param name="password"> The password.</param>
		/// <returns></returns>
		private string HashPassword(string sessionId, string password)
		{
			SHA1CryptoServiceProvider hashProvider 
				= new SHA1CryptoServiceProvider();

			string salt = Convert.ToBase64String(hashProvider.ComputeHash(System.Text.Encoding.UTF8.GetBytes(sessionId)));
			byte[] hash = hashProvider.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password + salt));
			return Convert.ToBase64String(hash);
		}

		private bool ValidateUsernameToken(UsernameToken token)
		{						
			DatabaseConfigurationHandler databaseConfigManager = new DatabaseConfigurationHandler();
			DatabaseConfiguration databaseConfiguration = (DatabaseConfiguration)databaseConfigManager.Load("serviceDatabaseConfiguration",string.Empty);

			UserDatabaseManager userDatabase = new UserDatabaseManager();
			string password = userDatabase.GetPasswordToken(databaseConfiguration.ConnectionString,token.Username);

			if ( password.Length == 0 )
			{
				return false;
			} 
			else 
			{
				if ( HashPassword(token.Username, password) == token.Password )
				{
					return true;
				} 
				else 
				{
					return false;
				}
			}
		}
	}
}
