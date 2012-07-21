using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.IO;
using System.Text;
using Microsoft.ApplicationBlocks.Data;
using Microsoft.Web.Services2;
using Microsoft.Web.Services2.Attachments;
using Microsoft.Web.Services2.Dime;
using Microsoft.Web.Services2.Messaging;
using Microsoft.Web.Services2.Security;
using Microsoft.Web.Services2.Security.Tokens;
using System.Security.Cryptography;
using Ecyware.GreenBlue.Configuration;
using EcyXmlEncryption = Ecyware.GreenBlue.Configuration.Encryption;
using Ecyware.GreenBlue.Configuration.XmlTypeSerializer;
using Ecyware.GreenBlue.LicenseServices.Client;

namespace Ecyware.GreenBlue.LicenseServices
{
	/// <summary>
	/// Contains the definition for the ePower query service.
	/// </summary>
	public class LicenseServices : SoapService
	{		
		UserDatabaseManager userDatabaseManager;
		DatabaseConfiguration _tempConfig;
		XmlSignature signer = new XmlSignature();
		private string publicKeyString = string.Empty;
		private RNGCryptoServiceProvider _sessionGenerator = new RNGCryptoServiceProvider();		

		/// <summary>
		/// Creates a new QueryService.
		/// </summary>
		public LicenseServices()
		{						
			// Load the account database.
			userDatabaseManager = new UserDatabaseManager(GetDatabaseConfiguration.ConnectionString);
		}

		/// <summary>
		/// Gets the connection string.
		/// </summary>
		public DatabaseConfiguration GetDatabaseConfiguration
		{
			get
			{
				if  ( _tempConfig == null )
				{
					DatabaseConfigurationHandler databaseConfig = new DatabaseConfigurationHandler();
					_tempConfig = (DatabaseConfiguration)databaseConfig.Load("serviceDatabaseConfiguration",string.Empty);
				}

				return _tempConfig;
			}
		}

		#region RSA Keys
		/// <summary>
		/// Gets the public key for the server encryption / signature key.
		/// </summary>
		private string GetPublicKey
		{
			get
			{
				return ReadServerEncryptionKey().ToXmlString(false);
			}
		}
		/// <summary>
		/// Gets the transport RSA key.
		/// </summary>
		/// <returns> Returns a RSA key.</returns>
		private RSA ReadTransportKey()
		{
			// Get the public key from instance
			Stream pvk 
				= this.GetType().Assembly.GetManifestResourceStream(this.GetType().Namespace + ".TransportPrivate.pvk");

			// Read in the key
			CspParameters CSPParam = new CspParameters();
			CSPParam.Flags = CspProviderFlags.UseMachineKeyStore;
			RSA key = new RSACryptoServiceProvider(CSPParam);

			using( StreamReader reader = new StreamReader(pvk) )
			{
				key.FromXmlString(reader.ReadLine());
			}

			return key;
		}

		/// <summary>
		/// Gets the RSA key.
		/// </summary>
		/// <returns> Returns a RSA key.</returns>
		private RSA ReadServerEncryptionKey()
		{
			// Get the public key from instance
			Stream pvk 
				= this.GetType().Assembly.GetManifestResourceStream(this.GetType().Namespace + ".ServerEncryptionDigitalSign.pvk");

			// Read in the key
			CspParameters CSPParam = new CspParameters();
			CSPParam.Flags = CspProviderFlags.UseMachineKeyStore;
			RSA key = new RSACryptoServiceProvider(CSPParam);
			using( StreamReader reader = new StreamReader(pvk) )
			{
				key.FromXmlString(reader.ReadLine());
			}

			return key;
		}
		#endregion

		/// <summary>
		/// Handles the SoapEnvelopes.
		/// </summary>
		/// <param name="envelope"> The SoapEnvelope.</param>
		protected override void Receive(SoapEnvelope envelope)
		{
			XmlDocument document;
			AccountMessage message;
			AccountMessageSerializer serializer = new AccountMessageSerializer();

			switch ( envelope.Context.Addressing.Action.Value )
			{
				case "urn:ecyware:greenblue:licenseServices:createAccount":
					document = DecryptBodyObject(envelope);
					
					// create object (desearilize)					
					message = (AccountMessage)serializer.Create(document.DocumentElement.OuterXml);

					// Replace body and send to base class.
					envelope.SetBodyObject(message);
					base.Receive(envelope);
					break;
				case "urn:ecyware:greenblue:licenseServices:updateAccount":
					document = DecryptBodyObject(envelope);
					
					// create object (desearilize)					
					message = (AccountMessage)serializer.Create(document.DocumentElement.OuterXml);

					// Replace body and send to base class.
					envelope.SetBodyObject(message);
					base.Receive(envelope);
					break;
				default:
					base.Receive(envelope);
					break;
			}
		}


		#region Encryption and Signature Methods
		/// <summary>
		/// Decrypts the body object.
		/// </summary>
		/// <param name="envelope"> The SoapEnvelope.</param>
		/// <returns> The XmlDocument with the decrypted Soap Body object.</returns>
		private XmlDocument DecryptBodyObject(SoapEnvelope envelope)
		{
			// add to new document
			XmlNode node = envelope.SelectSingleNode("//EncryptedData");
			XmlDocument document = new XmlDocument();
			document.AppendChild(document.ImportNode(node,true));

			// decrypt
			EcyXmlEncryption.EncryptXml decrypt = new EcyXmlEncryption.EncryptXml(document);
			decrypt.AddKeyNameMapping("accountMessage", ReadTransportKey());
			decrypt.DecryptDocument();

			return document;
		}

		/// <summary>
		/// Signs the Soap Body object.
		/// </summary>
		/// <param name="envelope"> The SoapEnvelope.</param>
		/// <returns> A XmlDocument with Xml Digital Signature applied.</returns>
		private XmlDocument SignScriptingApplication(string encryptedScriptingApplication)
		{
			//string bodyXml = envelope.Body.InnerXml;
			XmlDocument document = new XmlDocument();
			document.LoadXml(encryptedScriptingApplication);
			XmlDocument signedDocument = signer.SignXmlDocument(document, this.ReadServerEncryptionKey());

			return signedDocument;
		}

		/// <summary>
		/// Validates that the application is registered.
		/// </summary>
		/// <param name="envelope"> The SoapEnvelope to process.</param>
		/// <returns> A RegisterApplicationResultMessage.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:isRegisteredApplication")]
		public RegisterApplicationResultMessage IsRegisteredApplication(RegisterApplicationMessage message)
		{
			UsernameToken token = SecurityHelper.GetLicenseToken(RequestSoapContext.Current);
			
			RegisterApplicationResultMessage result = new RegisterApplicationResultMessage();

			if ( userDatabaseManager.RegisteredApplicationExists(token.Username, message.ApplicationID) )
			{
				result.Message = "Application exists.";
				result.IsApplicationRegistered = true;
			} 
			else 
			{
				result.Message = "Application needs to be registered.";
				result.IsApplicationRegistered = false;
			}

			return result;
		}

		/// <summary>
		/// Registers an Ecyware Scripting Application document.
		/// </summary>
		/// <param name="message"> The SoapEnvelope to process.</param>
		/// <returns> A RegisterApplicationResultMessage.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:registerScriptingApplication")]
		public RegisterApplicationResultMessage RegisterScriptingApplication(RegisterApplicationMessage message)
		{
			UsernameToken token = SecurityHelper.GetLicenseToken(RequestSoapContext.Current);
			
			RegisterApplicationResultMessage result = new RegisterApplicationResultMessage();

			// Validate license (application limit)
			if ( userDatabaseManager.ValidateApplicationLicenseLimit(token.Username) )
			{
				// Register application in database.
				bool inserted = userDatabaseManager.RegisterApplication(token.Username, message.ApplicationID);

				if ( inserted )
				{
					// Sign application.
					XmlDocument signedDocument = SignScriptingApplication(message.EncryptedScriptingApplicationXml);
					result.SignedScriptingApplicationXml = signedDocument.OuterXml;
					result.Message = "Application signed succesfully.";
					result.IsApplicationRegistered = true;
				} 
				else 
				{
					result.IsApplicationRegistered = false;
					result.Message = "Verify that the application id is valid and try saving again the document.";
				}
			} 
			else 
			{
				result.IsApplicationRegistered = false;
				result.Message = "Your application license limit doesn't allow more applications to be registered. Please purchase additional licenses.";
			}

			return result;
		}

		/// <summary>
		/// Rates the application.
		/// </summary>
		/// <param name="message"> The WebStoreRequestMessage type.</param>
		/// <returns>A WebStoreResultMessage type.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:rateApplication")]
		public WebStoreResultMessage RateApplication(WebStoreRequestMessage message)
		{
			UsernameToken token = SecurityHelper.GetLicenseToken(RequestSoapContext.Current);

			WebStoreResultMessage result = new WebStoreResultMessage();
			result.Message = "Rating complete";
			userDatabaseManager.RateApplication(message);

			return result;
		}

		/// <summary>
		/// Gets the web store view.
		/// </summary>
		/// <param name="message"> The WebStoreViewMessage type.</param>
		/// <returns>A WebStoreViewMessage.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:getWebStoreView")]
		public WebStoreViewResultMessage GetWebStoreView(WebStoreViewMessage message)
		{
			UsernameToken token = SecurityHelper.GetLicenseToken(RequestSoapContext.Current);

			WebStoreViewResultMessage result = new WebStoreViewResultMessage();
			result = userDatabaseManager.GetWebStoreView(message);

			return result;
		}

		/// <summary>
		/// Gets the web store view.
		/// </summary>
		/// <param name="message"> The WebStoreViewMessage type.</param>
		/// <returns>A WebStoreViewMessage.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:searchWebStoreView")]
		public WebStoreViewResultMessage SearchWebStoreView(WebStoreViewMessage message)
		{
			UsernameToken token = SecurityHelper.GetLicenseToken(RequestSoapContext.Current);

			WebStoreViewResultMessage result = new WebStoreViewResultMessage();
			result = userDatabaseManager.SearchWebStoreView(message);

			return result;
		}

		/// <summary>
		/// Downloads a scripting application.
		/// </summary>
		/// <param name="message"> A WebStoreRequestMessage type.</param>
		/// <returns> A WebStoreResultMessage.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:downloadApplication")]
		public WebStoreResultMessage DownloadApplication(WebStoreRequestMessage message)
		{
			UsernameToken token = SecurityHelper.GetLicenseToken(RequestSoapContext.Current);

			WebStoreResultMessage result = new WebStoreResultMessage();

			bool exists = userDatabaseManager.RegisteredApplicationExists(token.Username, message.ApplicationID);
						
			if ( exists )
			{
				// Update to web store db
				string applicationData = userDatabaseManager.GetApplication(message);

				result.Message = "Application downloaded.";
				result.IsApplicationRegistered = true;
				result.ApplicationData = applicationData;
			} 
			else 
			{
				// Send message warning.
				result.IsApplicationRegistered = false;
				result.Message = "This application cannot be found in the system.";
			}

			return result;
		}


		/// <summary>
		/// Removes a scripting application to the web store.
		/// </summary>
		/// <param name="message"> The SoapEnvelope to process.</param>
		/// <returns> A WebStoreResultMessage.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:removeScriptingApplication")]
		public WebStoreResultMessage RemoveScriptingApplication(WebStoreRequestMessage message)
		{
			UsernameToken token = SecurityHelper.GetLicenseToken(RequestSoapContext.Current);
			WebStoreResultMessage result = new WebStoreResultMessage();

			bool exists = userDatabaseManager.RegisteredApplicationExists(token.Username, message.ApplicationID);
			if ( exists )
			{
				userDatabaseManager.RemoveScriptingApplication(token.Username, message);
				result.Message = "Scripting application removed.";
			} 
			else 
			{
				result.Message = "Scripting application not found.";
			}

			return result;
		}

		/// <summary>
		/// Adds a scripting application to the web store.
		/// </summary>
		/// <param name="message"> The SoapEnvelope to process.</param>
		/// <returns> A WebStoreResultMessage.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:updateScriptingApplicationToWebStore")]
		public WebStoreResultMessage UpdateScriptingApplicationToWebStore(WebStoreRequestMessage message)
		{
			UsernameToken token = SecurityHelper.GetLicenseToken(RequestSoapContext.Current);

			WebStoreResultMessage result = new WebStoreResultMessage();

			// Validate license (application limit)
			if ( userDatabaseManager.ValidateApplicationLicenseLimit(token.Username) )
			{
				bool exists = userDatabaseManager.RegisteredApplicationExists(token.Username, message.ApplicationID);
				if ( exists )
				{
					// Update to web store db
					userDatabaseManager.UpdateApplicationToWebStore(token.Username, message);

					result.Message = "Application registered and saved into web store.";
					result.IsApplicationRegistered = true;
				} 
				else 
				{
					#region Add Application
					// Register application in database.
					bool inserted = userDatabaseManager.RegisterApplication(token.Username, message.ApplicationID);

					if ( inserted )
					{
						// Registered succesfully, continue adding the application to web store.
						// Add to web store db
						userDatabaseManager.AddApplicationToWebStore(token.Username, message);

						result.Message = "Application registered and saved into web store.";
						result.IsApplicationRegistered = true;
					} 
					else 
					{
						// Send message warning.
						result.IsApplicationRegistered = false;
						result.Message = "Either the application id is invalid or the application already exists on web store.";
					}
					#endregion
				}
			} 
			else 
			{
				// Send message warning.
				result.IsApplicationRegistered = false;
				result.Message = "Your application license limit doesn't allow more applications to be registered. Please purchase additional licenses.";
			}

			return result;

		}

		/// <summary>
		/// Gets the scripting application public key.
		/// </summary>
		/// <returns> A SignaturePublicKeyMessage type.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:getScriptingApplicationPublicKey")]
		public SignaturePublicKeyMessage GetScriptingApplicationPublicKey()
		{
			UsernameToken token = SecurityHelper.GetLicenseToken(RequestSoapContext.Current);
			
			SignaturePublicKeyMessage message = new SignaturePublicKeyMessage();
			message.PublicKeyString = GetPublicKey;

			return message;
		}
		#endregion
		#region Application Limit and Registration		
		#endregion
		#region Service Helper Methods
		/// <summary>
		/// Gets the session id.
		/// </summary>
		/// <returns> Returns a session id.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:getSessionID")]
		public ServiceContext GetSessionID()
		{
			byte[] filler = new byte[100];
			_sessionGenerator.GetNonZeroBytes(filler);
			
			ServiceContext message = new ServiceContext();
			message.SessionID = Convert.ToBase64String(filler);

			return message;
		}
		/// <summary>
		/// Gets the signature public key.
		/// </summary>
		/// <returns> Returns a SignaturePublicKeyMessage.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:getSignaturePublicKey")]
		public SignaturePublicKeyMessage GetSignaturePublicKey()
		{
						UsernameToken token = SecurityHelper.GetLicenseToken(RequestSoapContext.Current);
			SignaturePublicKeyMessage result =  new SignaturePublicKeyMessage();

			if ( publicKeyString.Length == 0 )
			{
				this.publicKeyString = this.ReadServerEncryptionKey().ToXmlString(false);
			}

			result.PublicKeyString = this.publicKeyString;

			return result;
		}

		#endregion
		#region Account methods		


		// TODO: Place code, this is also encrypted.
		// public AccountMessage RemoveAccount(AccountMessage)

		/// <summary>
		/// Updates the account.
		/// </summary>
		/// <param name="message"> The account message.</param>
		/// <returns> Returns an AccountMessage.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:updateAccount")]
		public AccountMessage UpdateAccount(AccountMessage message)
		{
			UsernameToken token = SecurityHelper.GetLicenseToken(RequestSoapContext.Current);

			message.AccountExists = false;

			// Updates the account
			if ( userDatabaseManager.UserExists(message.CurrentAccount) )
			{
				userDatabaseManager.UpdateAccount(message.CurrentAccount);
				message.AccountExists = true;
			}

			// send result
			AccountMessage newMessage = new AccountMessage();
			newMessage.CurrentAccount.Email = message.CurrentAccount.Email;
			newMessage.CurrentAccount.Name = message.CurrentAccount.Name;
			newMessage.CurrentAccount.Username = message.CurrentAccount.Username;
			newMessage.SessionID = message.SessionID;
			newMessage.AccountExists = message.AccountExists;

			return newMessage;
		}

		/// <summary>
		/// Creates a new account.
		/// </summary>
		/// <param name="message"> The account message.</param>
		/// <returns> Returns an AccountMessage.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:createAccount")]
		public AccountMessage CreateAccount(AccountMessage message)
		{
			// Creates a new account
			if ( userDatabaseManager.UserExists(message.CurrentAccount) )
			{
				// it already exists
				message.AccountExists = true;
			} else {
				// add
				message.CurrentAccount.Created = DateTime.Now;
				message.AccountExists = false;
				userDatabaseManager.CreateAccount(message.CurrentAccount);
			}

			// send result
			AccountMessage newMessage = new AccountMessage();
			newMessage.CurrentAccount.Email = message.CurrentAccount.Email;
			newMessage.CurrentAccount.Name = message.CurrentAccount.Name;
			newMessage.CurrentAccount.Username = message.CurrentAccount.Username;
			newMessage.SessionID = message.SessionID;
			newMessage.AccountExists = message.AccountExists;

			return newMessage;
		}


		/// <summary>
		/// Gets the user details.
		/// </summary>
		/// <returns> Returns a the account user details.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:getUserDetails")]
		public AccountMessage GetUserDetails(ServiceContext message)
		{
			UsernameToken token = SecurityHelper.GetLicenseToken(RequestSoapContext.Current);
			Account account = userDatabaseManager.GetAccount(token.Username);

			AccountMessage result;
			if ( account != null )
			{
				 result = new AccountMessage(account);
			} 
			else 
			{
				result = new AccountMessage();
				result.CurrentAccount = new Account();
				result.CurrentAccount.Email = token.Username + token.Password;
				result.AccountExists = false;
			}

			return result;
		}
		#endregion

	}
}
