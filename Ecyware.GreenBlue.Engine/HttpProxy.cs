// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Net;


namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Contains the HTTP Proxy settings.
	/// </summary>
	public class HttpProxy
	{
		private string[] bypassList=null;
		private string _proxyUri=String.Empty;
		private ICredentials _credentials=null;
		private HttpAuthentication _auth = new HttpAuthentication();
		private bool _bypassOnLocal = false;

		#region Constructors
		/// <summary>
		/// Creates a new HttpProxy.
		/// </summary>
		public HttpProxy()
		{
		}

		/// <summary>
		/// Creates a new HttpProxy.
		/// </summary>
		/// <param name="proxy"> Proxy Uri.</param>
		public HttpProxy(string proxy)
		{
			this.ProxyUri=proxy;
		}

		/// <summary>
		/// Creates a new HttpProxy.
		/// </summary>
		/// <param name="proxy"> Proxy Uri.</param>
		/// <param name="bypasslist"> Urls to bypass.</param>
		public HttpProxy(string proxy, string[] bypasslist)
		{
			this.ProxyUri=proxy;
			this.BypassList=bypasslist;
		}

		/// <summary>
		/// Creates a new HttpProxy.
		/// </summary>
		/// <param name="proxy"> Proxy url.</param>
		/// <param name="bypasslist"> Urls to bypass.</param>
		/// <param name="creds"> The current connection credentials.</param>
		public HttpProxy(string proxy, string[] bypasslist, ICredentials creds)
		{
			this.ProxyUri=proxy;
			this.BypassList=bypasslist;
			this.Credentials=creds;
		}

		#endregion

		/// <summary>
		/// Gets or sets if the proxy bypasses local resources.
		/// </summary>
		public bool BypassOnLocal
		{
			get
			{
				return _bypassOnLocal;
			}
			set
			{
				_bypassOnLocal = value;
			}
		}

		/// <summary>
		/// Gets or sets the connection credentials.
		/// </summary>
		public ICredentials Credentials
		{
			get
			{
				return _credentials;
			}
			set
			{
				if ( _credentials != value )
				{
					_credentials=value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the proxy uri.
		/// </summary>
		public string ProxyUri
		{
			get
			{
				return _proxyUri;
			}
			set
			{
				_proxyUri = value;
			}
		}

		/// <summary>
		/// Gets or sets the urls to bypass.
		/// </summary>
		public string[] BypassList
		{
			get
			{
				return bypassList;
			}
			set
			{
				bypassList = value;
			}
		}

		/// <summary>
		/// Gets or sets the HttpAuthentication.
		/// </summary>
		public HttpAuthentication AuthenticationSettings
		{
			get
			{
				return _auth;
			}
			set
			{
				_auth=value;
			}
		}

		#region Methods
		/// <summary>
		/// Sets the proxy authentication.
		/// </summary>
		/// <param name="username"> Username.</param>
		/// <param name="password"> Password.</param>
		public void SetProxyAuthentication(string username, string password)
		{
			this.AuthenticationSettings.UseBasicAuthentication=true;
			this.AuthenticationSettings.Username=username;
			this.AuthenticationSettings.Password=password;
			this.Credentials=new NetworkCredential(username,password);
		}

		/// <summary>
		/// Sets the proxy authentication.
		/// </summary>
		/// <param name="username"> Username.</param>
		/// <param name="password"> Password.</param>
		/// <param name="domain"> Domain.</param>
		public void SetProxyAuthentication(string username, string password, string domain)
		{
			this.AuthenticationSettings.UseBasicAuthentication=true;
			this.AuthenticationSettings.Username=username;
			this.AuthenticationSettings.Password=password;
			this.AuthenticationSettings.Domain=domain;
			this.Credentials=new NetworkCredential(username,password,domain);
		}

		/// <summary>
		/// Sets default credentials.
		/// </summary>
		public void SetDefaultCredentials()
		{
			this.AuthenticationSettings.UseNTLMAuthentication = true;
			this.Credentials = CredentialCache.DefaultCredentials;
		}

		/// <summary>
		/// Sets a public proxy setting.
		/// </summary>
		public void SetPublicProxy()
		{
			this.Credentials = null;
		}
		#endregion
	}
}
