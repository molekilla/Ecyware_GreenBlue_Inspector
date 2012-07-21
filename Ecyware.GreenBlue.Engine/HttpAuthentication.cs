// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003
using System;
using System.Xml.Serialization;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Handles the authentication use in HTTP requests.
	/// </summary>
	[Serializable]
	public class HttpAuthentication
	{
		private bool _ntlm = true;
		private bool _basic = false;
		private string _username = String.Empty;
		private string _password = String.Empty;
		private string _domain = String.Empty;

		/// <summary>
		/// Creates a new HttpAuthentication.
		/// </summary>
		public HttpAuthentication()
		{
		}

		/// <summary>
		/// Creates a new HttpAuthentication.
		/// </summary>
		/// <param name="username"> Username.</param>
		/// <param name="password"> Password.</param>
		public HttpAuthentication(string username, string password)
		{
			this.UseBasicAuthentication=true;
			this.Username=username;
			this.Password=password;
		}
		/// <summary>
		/// Creates a new HttpAuthentication.
		/// </summary>
		/// <param name="username"> Username.</param>
		/// <param name="password"> Password.</param>
		/// <param name="domain"> Domain name.</param>
		public HttpAuthentication(string username, string password,string domain)
		{
			this.UseBasicAuthentication=true;
			this.Username=username;
			this.Password=password;
			this.Domain=domain;
		}

		/// <summary>
		/// Gets or sets the domain.
		/// </summary>
		[XmlElement("domain")]
		public string Domain
			{
				get
				{
					return _domain;
				}
				set
				{
					_domain = value;
				}
			}

		/// <summary>
		/// Gets or sets the username.		
		/// </summary>
		[XmlElement("username")]
		public string Username
		{
			get
			{
				return _username;
			}
			set
			{
				_username = value;
			}
		}

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		[XmlElement("password")]
		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				_password = value;
			}
		}

		/// <summary>
		/// Gets or sets if Basic Authentication is going to be used. If UseNTMLAuthentication is already set to true, then it is set to false.
		/// </summary>
		[XmlElement("useBasicAuthentication")]
		public bool UseBasicAuthentication
		{
			get
			{
				return _basic;
			}
			set
			{
				if ( value==true )
				{
					_ntlm = false;
					_basic = value;
				} 
				else 
				{
					_basic = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets if NTLM Authentication is going to be used. If UseBasicAuthentication is already set to true, then it is set to false.
		/// </summary>
		[XmlElement("useNTLM")]
		public bool UseNTLMAuthentication
		{
			get
			{
				return _ntlm;
			}
			set
			{
				if ( value==true )
				{
					_ntlm = value;
					_basic = false;
				} 
				else 
				{
					_ntlm = value;
				}
			}
		}
	}
}
