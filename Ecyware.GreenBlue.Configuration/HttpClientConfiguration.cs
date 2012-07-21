// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January-September 2004
using System;
using System.Xml;
using System.Collections;

namespace Ecyware.GreenBlue.Configuration.Inspector
{
	/// <summary>
	/// Contains the HttpClientConfiguration class.
	/// </summary>
	public class HttpClientConfiguration
	{
		private System.Net.SecurityProtocolType _securityProtocol = System.Net.SecurityProtocolType.Ssl3;
		private string _userAgent = string.Empty;
		private bool _keepAlive = false; // default

		/// <summary>
		/// Creates a new HttpClientConfiguration.
		/// </summary>
		public HttpClientConfiguration()
		{
		}

		/// <summary>
		/// Gets or sets the user agent.
		/// </summary>
		public string UserAgent
		{
			get
			{
				return _userAgent;
			}
			set
			{
				_userAgent = value;
			}
		}

		/// <summary>
		/// Gets or sets the security protocol.
		/// </summary>
		public System.Net.SecurityProtocolType SecurityProtocol
		{
			get
			{
				return _securityProtocol;
			}
			set
			{
				_securityProtocol = value;
			}
		}

		/// <summary>
		/// Gets or sets the keep alive setting.
		/// </summary>
		public bool KeepAlive
		{
			get
			{
				return _keepAlive;
			}
			set
			{
				_keepAlive = value;
			}
		}

		internal HttpClientConfiguration(HttpClientConfiguration config)
		{

			if ( config != null )
			{
				this._userAgent = config.UserAgent;
				this._keepAlive = config.KeepAlive;
				this._securityProtocol = config.SecurityProtocol;
			}
		}

		internal void LoadValuesFromConfiguration(XmlNode node)
		{
			XmlAttributeCollection items = node.Attributes;

			// set
			this._userAgent = items["userAgent"].Value;
			this._keepAlive = bool.Parse(items["keepAlive"].Value);

			if ( items["securityProtocol"].Value == "tls" )
			{
				this.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
			} 
			else 
			{
				this.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3;
			}
		}
	}
}
