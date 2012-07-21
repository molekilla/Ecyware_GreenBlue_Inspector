using System;
using Ecyware.GreenBlue.LicenseServices.Client;
using System.Data;
using System.IO;
using System.Web.Services.Protocols;
using Microsoft.Web.Services2;
using Microsoft.Web.Services2.Addressing;
using Microsoft.Web.Services2.Security.Tokens;
using Microsoft.Web.Services2.Security;
using Microsoft.Web.Services2.Messaging;
using System.Security.Cryptography;

namespace Ecyware.GreenBlue.Utils
{
	/// <summary>
	/// Summary description for ServicesProxy.
	/// </summary>
	public class ServicesProxy
	{		
		static LicenseServiceClient client = null;
		public static bool IsConnected = false;
	//	static string _hashedPassword = string.Empty;
		/// <summary>
		/// Gets or sets the username.
		/// </summary>
		public static string Username = string.Empty;

		/// <summary>
		/// Creates a new client proxy.
		/// </summary>
		private ServicesProxy()
		{	
		}

		/// <summary>
		/// Adds the user token.
		/// </summary>
		/// <param name="username"> The username.</param>
		/// <param name="password"> The user password.</param>
		public static void AddUserToken(string username, string password)
		{
			Username = username;
			string hashedPassword = HashPassword(username, password);
			UsernameToken token = new UsernameToken(username, hashedPassword, PasswordOption.SendPlainText);
			token.Id = "LicenseToken";

			if ( client.Security != null )
			{
				client.Security = new Security();
			}

			client.Security.Tokens.Add(token);
		}


		/// <summary>
		/// Converts the file to a Base64 string.
		/// </summary>
		/// <param name="file"> The file to convert.</param>
		/// <returns> The Base64 string.</returns>
		public static string ReadFileToBase64String(string file)
		{
			FileInfo fi = new FileInfo(file);
			int len = (int)fi.Length;

			byte[] data = new byte[len];
			FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
			fs.Read(data, 0, len);
			fs.Close();			

			return Convert.ToBase64String(data,0, data.Length);			
		}

//		public static bool UseCurrentCredential()
//		{
//			if ( username.Length > 0 && hashedPassword.Length > 0 )
//			{
//				UsernameToken token = new UsernameToken(username, hashedPassword, PasswordOption.SendPlainText);
//				token.Id = "LicenseToken";
//
//				if ( client.Security != null )
//				{
//					client.Security = new Security();
//				}
//
//				client.Security.Tokens.Add(token);
//			}
//		}
		/// <summary>
		/// Creates a hash for the password.
		/// </summary>
		/// <param name="username"> The username.</param>
		/// <param name="password"> The password.</param>
		/// <returns> A hashed password.</returns>
		public static string HashPassword(string username, string password)
		{
			SHA1CryptoServiceProvider hashProvider 
				= new SHA1CryptoServiceProvider();

			string salt = Convert.ToBase64String(hashProvider.ComputeHash(System.Text.Encoding.UTF8.GetBytes(username)));
			byte[] hash = hashProvider.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password + salt));
			return Convert.ToBase64String(hash);
		}


		/// <summary>
		/// Gets the proxy client.
		/// </summary>
		/// <returns> A LicenseServicesClient</returns>
		public static LicenseServiceClient GetClientProxy()
		{
			if ( client == null )
			{
				string url = string.Empty;
				if ( System.Configuration.ConfigurationSettings.AppSettings["LicenseServicesUrl"] != null )
				{
					url = System.Configuration.ConfigurationSettings.AppSettings["LicenseServicesUrl"];
				}

				if ( url.Length > 0 )
				{
					EndpointReference requestEndpoint = new EndpointReference(new Uri(url));



					// Client
					client = new LicenseServiceClient(requestEndpoint);

					SoapHttpOutputChannel channel = (SoapHttpOutputChannel)client.Channel;
					channel.Options.Credentials = System.Net.CredentialCache.DefaultCredentials;
					channel.Options.Proxy = System.Net.GlobalProxySelection.Select;

					Security security = new Security();
					client.Security = security;
					client.GetSessionID();
				} 
				else 
				{
					throw new ArgumentNullException("System.Configuration.ConfigurationSettings.AppSettings[\"LicenseServicesUrl\"]","No service url found in the configuration file.");
				}
			}

			return client;
		}

		/// <summary>
		/// Registers the exception event handler.
		/// </summary>
		/// <param name="handler"></param>
		public static void RegisterExceptionEventHandler(Ecyware.GreenBlue.LicenseServices.Client.ExceptionHandler handler)
		{			
			client.ExceptionEventHandler += handler;
		}
	}
}
