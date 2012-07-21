using System;
using System.Data;
using System.Xml;
using System.Xml.XPath;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.IO;
using Microsoft.Web.Services2;
using Microsoft.Web.Services2.Attachments;
using Microsoft.Web.Services2.Dime;
using Microsoft.Web.Services2.Addressing;
using Microsoft.Web.Services2.Messaging;
using Microsoft.Web.Services2.Security;
using Microsoft.Web.Services2.Security.Tokens;
using Ecyware.GreenBlue.Configuration;
using EcyXmlEncryption = Ecyware.GreenBlue.Configuration.Encryption;
using Ecyware.GreenBlue.Configuration.XmlTypeSerializer;
using Ecyware.GreenBlue.Configuration.DigitalSignature;


namespace Ecyware.GreenBlue.LicenseServices.Client
{
	public delegate void MessageResultHandler(object sender, EventArgs message);
	public delegate void EmptyResultHandler();
	public delegate void ExceptionHandler(object sender, Exception ex);

	/// <summary>
	/// Summary description for LicenseServiceClient.
	/// </summary>
	public class LicenseServiceClient : SoapClient
	{		
		string currentSessionId;
		private Microsoft.Web.Services2.Security.Security _security;
		private MessageResultHandler _clientHandler;
		public event ExceptionHandler ExceptionEventHandler;

		/// <summary>
		/// Creates a new LicenseServiceClient.
		/// </summary>		
		public LicenseServiceClient()
		{
		}

		/// <summary>
		/// Creates a new LicenseServiceClient.
		/// </summary>
		/// <param name="er"> The EndpointReference type. </param>
		public LicenseServiceClient(EndpointReference er) : base(er)
		{
		}


		/// <summary>
		/// Gets or sets the WSE Security type.
		/// </summary>
		public Microsoft.Web.Services2.Security.Security Security
		{
			get
			{
				return _security;
			}
			set
			{
				_security = value;
			}
		}

		/// <summary>
		/// Gets the session id.
		/// </summary>
		public string SessionID
		{
			get
			{
				return this.currentSessionId;
			}
		}

		#region Encryption Security Methods
		/// <summary>
		/// Gets the RSA key.
		/// </summary>
		/// <returns> Returns a RSA key.</returns>
		private RSA ReadKey()
		{
			// Get the public key from instance
			Stream pvk 
				= this.GetType().Assembly.GetManifestResourceStream(this.GetType().Namespace + ".TransportPublic.pub");

			// Read in the key
			RSA key = new RSACryptoServiceProvider();
			using( StreamReader reader = new StreamReader(pvk) )
			{
				key.FromXmlString(reader.ReadLine());
			}

			return key;
		}

		/// <summary>
		/// Encrypts the account message.
		/// </summary>
		/// <param name="message"> The AccountMessage.</param>
		/// <returns> A encrypted XML String.</returns>
		private string EncryptAccountMessage(AccountMessage message)
		{
			try
			{
				AccountMessageSerializer serializer = new AccountMessageSerializer();
				XmlNode xmlNode = serializer.Serialize(message);
				XmlDocument document = new XmlDocument();
				document.AppendChild(document.ImportNode(xmlNode,true));
				
				// Encrypt xml
				EcyXmlEncryption.EncryptXml enc = new EcyXmlEncryption.EncryptXml(document);
				enc.AddKeyNameMapping("accountMessage", ReadKey());

				XmlElement el = (XmlElement)document.SelectSingleNode("/AccountMessage");
				EcyXmlEncryption.EncryptedData data = enc.Encrypt(el, "accountMessage");
				enc.ReplaceElement(el, data);

				return document.DocumentElement.OuterXml;
			}
			catch
			{
				throw;
			}
		}
		#endregion
		#region Scripting Application Signing
		/// <summary>
		/// Registers an Ecyware Scripting Application document.
		/// </summary>
		/// <param name="envelope"> The SoapEnvelope to process.</param>
		/// <returns> A SignScriptingApplicationResultMessage.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:registerScriptingApplication")]
		public RegisterApplicationResultMessage RegisterScriptingApplication(RegisterApplicationMessage message)
		{
			SoapEnvelope envelope = new SoapEnvelope();			
			envelope.Context.Security.Tokens.AddRange(_security.Tokens);
			envelope.SetBodyObject(message);
			
			return (RegisterApplicationResultMessage)base.SendRequestResponse("RegisterScriptingApplication",envelope).GetBodyObject(typeof(RegisterApplicationResultMessage)); 
		}

		/// <summary>
		/// Registers an Ecyware Scripting Application async.
		/// </summary>
		/// <param name="message"> The RegisterApplicationMessage type.</param>
		/// <param name="callback"> The message callback.</param>
		/// <param name="state"> The object state.</param>
		/// <returns> An IAsyncResult.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:registerScriptingApplication")]
		public IAsyncResult BeginRegisterScriptingApplication(RegisterApplicationMessage message, MessageResultHandler callback, object state)
		{
			SoapEnvelope envelope = new SoapEnvelope();
			envelope.Context.Security.Tokens.AddRange(_security.Tokens);
			envelope.SetBodyObject(message);
								
			_clientHandler = callback;
			return base.BeginSendRequestResponse("RegisterScriptingApplication",envelope,new AsyncCallback(EndMessageResultHandler),typeof(RegisterApplicationResultMessage));
		}
		#endregion
		#region Verify Application Signature
		/// <summary>
		/// Gets the signature public key.
		/// </summary>
		private SignaturePublicKeyMessage GetScriptingApplicationPublicKey()
		{
			SoapEnvelope envelope = new SoapEnvelope();			
			envelope.Context.Addressing.Action = new Action("urn:ecyware:greenblue:licenseServices:getScriptingApplicationPublicKey");
			envelope.Context.Security.Tokens.AddRange(_security.Tokens);
			ServiceContext message = new ServiceContext();
			message.SessionID = this.currentSessionId;
			envelope.SetBodyObject(message);
			return (SignaturePublicKeyMessage)base.SendRequestResponse("GetScriptingApplicationPublicKey",envelope).GetBodyObject(typeof(SignaturePublicKeyMessage));
		}

		/// <summary>
		/// Verifies the scripting application.
		/// </summary>
		/// <param name="applicationStream"> The scripting application stream.</param>
		/// <returns></returns>
		public bool VerifyApplicationSignature(Stream applicationStream)
		{
			SignaturePublicKeyMessage message = GetScriptingApplicationPublicKey();

			// read in the public key
			RSA publicKey = new RSACryptoServiceProvider();
			publicKey.FromXmlString(message.PublicKeyString);			

			XmlTextReader xmlReader = new XmlTextReader(applicationStream);

			return DigitalSignatureVerifier.VerifyDigitalSignature(xmlReader, publicKey);
		}

		#endregion
		#region Account Methods
		/// <summary>
		/// Gets the session id.
		/// </summary>
		/// <returns> A ServiceContext type.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:getSessionID")]
		public void GetSessionID()
		{
			SoapEnvelope envelope = new SoapEnvelope();			
			envelope.Context.Security.Tokens.AddRange(_security.Tokens);
			ServiceContext context = (ServiceContext)base.SendRequestResponse("GetSessionID",envelope).GetBodyObject(typeof(ServiceContext));
			this.currentSessionId = context.SessionID;
		}

		/// <summary>
		/// Gets the user details.
		/// </summary>
		/// <returns> An AccountMessage type.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:getUserDetails")]
		public AccountMessage GetUserDetails()
		{			
			SoapEnvelope envelope = new SoapEnvelope();			
			envelope.Context.Security.Tokens.AddRange(_security.Tokens);
			ServiceContext message = new ServiceContext();
			message.SessionID = this.currentSessionId;
			envelope.SetBodyObject(message);
			return (AccountMessage)base.SendRequestResponse("GetUserDetails",envelope).GetBodyObject(typeof(AccountMessage));
		}

		/// <summary>
		/// Gets the user details async.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="callback"></param>
		/// <param name="state"></param>
		/// <returns></returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:getUserDetails")]
		public IAsyncResult BeginGetUserDetails(MessageResultHandler callback, object state)
		{
			SoapEnvelope envelope = new SoapEnvelope();
			envelope.Context.Security.Tokens.AddRange(_security.Tokens);
			ServiceContext message = new ServiceContext();
			message.SessionID = this.currentSessionId;
			envelope.SetBodyObject(message);
								
			_clientHandler = callback;
			return base.BeginSendRequestResponse("GetUserDetails",envelope,new AsyncCallback(EndMessageResultHandler),typeof(AccountMessage));
		}

		/// <summary>
		/// Creates a new account.
		/// </summary>
		/// <param name="message"> A new AccountMessage.</param>
		/// <returns> An AccountMessage type.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:createAccount")]
		public AccountMessage CreateAccount(AccountMessage message)
		{
			message.SessionID = this.currentSessionId;

			SoapEnvelope envelope = new SoapEnvelope();			
			envelope.Context.Security.Tokens.AddRange(_security.Tokens);
			envelope.Body.InnerXml = EncryptAccountMessage(message);
			// envelope.SetBodyObject(message);
			return (AccountMessage)base.SendRequestResponse("CreateAccount",envelope).GetBodyObject(typeof(AccountMessage)); 			
		}

		/// <summary>
		/// Updates the account.
		/// </summary>
		/// <param name="message"> An existing AccountMessage.</param>
		/// <returns> An AccountMessage type.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:updateAccount")]
		public AccountMessage UpdateAccount(AccountMessage message)
		{
			message.SessionID = this.currentSessionId;

			SoapEnvelope envelope = new SoapEnvelope();			
			envelope.Context.Security.Tokens.AddRange(_security.Tokens);
			envelope.Body.InnerXml = EncryptAccountMessage(message);			
			return (AccountMessage)base.SendRequestResponse("UpdateAccount",envelope).GetBodyObject(typeof(AccountMessage)); 			
		}

		/// <summary>
		/// Creates a new account async.
		/// </summary>
		/// <param name="message"></param>
		/// <param name="callback"></param>
		/// <param name="state"></param>
		/// <returns></returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:createAccount")]
		public IAsyncResult BeginCreateAccount(AccountMessage message, MessageResultHandler callback, object state)
		{
			message.SessionID = this.currentSessionId;

			SoapEnvelope envelope = new SoapEnvelope();
			envelope.Context.Security.Tokens.AddRange(_security.Tokens);
			envelope.Body.InnerXml = EncryptAccountMessage(message);
			// envelope.SetBodyObject(message);
								
			_clientHandler = callback;
			return base.BeginSendRequestResponse("CreateAccount",envelope,new AsyncCallback(EndMessageResultHandler),typeof(AccountMessage));
		}
		#endregion
		#region Web Store
		/// <summary>
		/// Gets the web store view.
		/// </summary>
		/// <param name="message"> The WebStoreViewMessage type.</param>
		/// <returns> A WebStoreViewResultMessage type.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:getWebStoreView")]
		public WebStoreViewResultMessage GetWebStoreView(WebStoreViewMessage message)
		{
			SoapEnvelope envelope = new SoapEnvelope();			
			envelope.Context.Security.Tokens.AddRange(_security.Tokens);
			envelope.SetBodyObject(message);
			
			return (WebStoreViewResultMessage)base.SendRequestResponse("GetWebStoreView",envelope).GetBodyObject(typeof(WebStoreViewResultMessage)); 
		}

		/// <summary>
		/// Gets the web store view async.
		/// </summary>
		/// <param name="message"> The WebStoreViewMessage type.</param>
		/// <param name="callback"> The message callback.</param>
		/// <param name="state"> The object state.</param>
		/// <returns> An IAsyncResult.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:getWebStoreView")]
		public IAsyncResult BeginGetWebStoreView(WebStoreViewMessage message, MessageResultHandler callback, object state)
		{
			SoapEnvelope envelope = new SoapEnvelope();
			envelope.Context.Security.Tokens.AddRange(_security.Tokens);
			envelope.SetBodyObject(message);
								
			_clientHandler = callback;
			return base.BeginSendRequestResponse("GetWebStoreView",envelope,new AsyncCallback(EndMessageResultHandler),typeof(WebStoreViewResultMessage));
		}
		/// <summary>
		/// Searchs the web store view.
		/// </summary>
		/// <param name="message"> The WebStoreViewMessage type.</param>
		/// <returns> A WebStoreViewResultMessage type.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:searchWebStoreView")]
		public WebStoreViewResultMessage SearchWebStoreView(WebStoreViewMessage message)
		{
			SoapEnvelope envelope = new SoapEnvelope();			
			envelope.Context.Security.Tokens.AddRange(_security.Tokens);
			envelope.SetBodyObject(message);
			
			return (WebStoreViewResultMessage)base.SendRequestResponse("SearchWebStoreView",envelope).GetBodyObject(typeof(WebStoreViewResultMessage)); 
		}

		/// <summary>
		/// Searchs the web store view async.
		/// </summary>
		/// <param name="message"> The WebStoreViewMessage type.</param>
		/// <param name="callback"> The message callback.</param>
		/// <param name="state"> The object state.</param>
		/// <returns> An IAsyncResult.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:searchWebStoreView")]
		public IAsyncResult BeginSearchWebStoreView(WebStoreViewMessage message, MessageResultHandler callback, object state)
		{
			SoapEnvelope envelope = new SoapEnvelope();
			envelope.Context.Security.Tokens.AddRange(_security.Tokens);
			envelope.SetBodyObject(message);
								
			_clientHandler = callback;
			return base.BeginSendRequestResponse("SearchWebStoreView",envelope,new AsyncCallback(EndMessageResultHandler),typeof(WebStoreViewResultMessage));
		}
		/// <summary>
		/// Downloads a scripting application.
		/// </summary>
		/// <param name="message"> The WebStoreRequestMessage type.</param>
		/// <returns> A WebStoreResultMessage type.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:downloadApplication")]
		public WebStoreResultMessage DownloadApplication(WebStoreRequestMessage message)
		{
			SoapEnvelope envelope = new SoapEnvelope();			
			envelope.Context.Security.Tokens.AddRange(_security.Tokens);
			envelope.SetBodyObject(message);
			
			return (WebStoreResultMessage)base.SendRequestResponse("DownloadApplication",envelope).GetBodyObject(typeof(WebStoreResultMessage)); 
		}

		/// <summary>
		/// Downloads a scripting application async.
		/// </summary>
		/// <param name="message"> The WebStoreRequestMessage type.</param>
		/// <param name="callback"> The message callback.</param>
		/// <param name="state"> The object state.</param>
		/// <returns> An IAsyncResult.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:downloadApplication")]
		public IAsyncResult BeginDownloadApplication(WebStoreRequestMessage message, MessageResultHandler callback, object state)
		{
			SoapEnvelope envelope = new SoapEnvelope();
			envelope.Context.Security.Tokens.AddRange(_security.Tokens);
			envelope.SetBodyObject(message);
								
			_clientHandler = callback;
			return base.BeginSendRequestResponse("DownloadApplication",envelope,new AsyncCallback(EndMessageResultHandler),typeof(WebStoreResultMessage));
		}
		/// <summary>
		/// Updates an Ecyware Scripting Application Package.
		/// </summary>
		/// <param name="message"> The WebStoreRequestMessage.</param>
		/// <returns> A WebStoreResultMessage.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:updateScriptingApplicationToWebStore")]
		public WebStoreResultMessage UpdateScriptingApplicationToWebStore(WebStoreRequestMessage message)
		{
			SoapEnvelope envelope = new SoapEnvelope();			
			envelope.Context.Security.Tokens.AddRange(_security.Tokens);
			envelope.SetBodyObject(message);
			
			return (WebStoreResultMessage)base.SendRequestResponse("UpdateScriptingApplicationToWebStore",envelope).GetBodyObject(typeof(WebStoreResultMessage)); 
		}

		/// <summary>
		/// Removes an Ecyware Scripting Application Package.
		/// </summary>
		/// <param name="message"> The WebStoreRequestMessage.</param>
		/// <returns> A WebStoreResultMessage.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:removeScriptingApplication")]
		public WebStoreResultMessage RemoveScriptingApplication(WebStoreRequestMessage message)
		{
			SoapEnvelope envelope = new SoapEnvelope();			
			envelope.Context.Security.Tokens.AddRange(_security.Tokens);
			envelope.SetBodyObject(message);
			
			return (WebStoreResultMessage)base.SendRequestResponse("RemoveScriptingApplication",envelope).GetBodyObject(typeof(WebStoreResultMessage)); 
		}

		/// <summary>
		/// Removes an Ecyware Scripting Application async.
		/// </summary>
		/// <param name="message"> The WebStoreRequestMessage type.</param>
		/// <param name="callback"> The message callback.</param>
		/// <param name="state"> The object state.</param>
		/// <returns> An IAsyncResult.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:removeScriptingApplication")]
		public IAsyncResult BeginRemoveScriptingApplication(WebStoreRequestMessage message, MessageResultHandler callback, object state)
		{
			SoapEnvelope envelope = new SoapEnvelope();
			envelope.Context.Security.Tokens.AddRange(_security.Tokens);
			envelope.SetBodyObject(message);
								
			_clientHandler = callback;
			return base.BeginSendRequestResponse("RemoveScriptingApplication",envelope,new AsyncCallback(EndMessageResultHandler),typeof(WebStoreResultMessage));
		}

		/// <summary>
		/// Updates an Ecyware Scripting Application async.
		/// </summary>
		/// <param name="message"> The WebStoreRequestMessage type.</param>
		/// <param name="callback"> The message callback.</param>
		/// <param name="state"> The object state.</param>
		/// <returns> An IAsyncResult.</returns>
		[SoapMethod("urn:ecyware:greenblue:licenseServices:updateScriptingApplicationToWebStore")]
		public IAsyncResult BeginUpdateScriptingApplicationToWebStore(WebStoreRequestMessage message, MessageResultHandler callback, object state)
		{
			SoapEnvelope envelope = new SoapEnvelope();
			envelope.Context.Security.Tokens.AddRange(_security.Tokens);
			envelope.SetBodyObject(message);
								
			_clientHandler = callback;
			return base.BeginSendRequestResponse("UpdateScriptingApplicationToWebStore",envelope,new AsyncCallback(EndMessageResultHandler),typeof(WebStoreResultMessage));
		}

//		/// <summary>
//		/// Adds an Ecyware Scripting Application async.
//		/// </summary>
//		/// <param name="message"> The WebStoreRequestMessage type.</param>
//		/// <param name="callback"> The message callback.</param>
//		/// <param name="state"> The object state.</param>
//		/// <returns> An IAsyncResult.</returns>
//		[SoapMethod("urn:ecyware:greenblue:licenseServices:addScriptingApplicationToWebStore")]
//		public IAsyncResult BeginAddScriptingApplicationToWebStore(WebStoreRequestMessage message, MessageResultHandler callback, object state)
//		{
//			SoapEnvelope envelope = new SoapEnvelope();
//			envelope.Context.Security.Tokens.AddRange(_security.Tokens);
//			envelope.SetBodyObject(message);
//								
//			_clientHandler = callback;
//			return base.BeginSendRequestResponse("AddScriptingApplicationToWebStore",envelope,new AsyncCallback(EndMessageResultHandler),typeof(WebStoreResultMessage));
//		}

		#endregion

		/// <summary>
		/// Returns after async callback to BeginGetXXX is complete.
		/// </summary>
		/// <param name="result"> The IAsyncResult.</param>
		/// <returns> A ServiceContext.</returns>
		private void EndMessageResultHandler(IAsyncResult result)
		{
			try
			{
				SoapEnvelope envelope = base.EndSendRequestResponse(result);				
				object resultMessage = envelope.GetBodyObject((Type)result.AsyncState);

				MessageEventArgs args = new MessageEventArgs();
				args.Message = (ServiceContext)resultMessage;

				// callback delegate.
				if ( _clientHandler != null )
					_clientHandler(this, args);
			}
			catch (Exception ex)
			{
				if ( ExceptionEventHandler != null )
					ExceptionEventHandler(this, ex);
			}
		}
	}
}
