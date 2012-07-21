// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: November 2003-September 2004
using System;
using System.Net;
using System.IO;
using System.Security.Permissions;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Ecyware.GreenBlue.Configuration;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Contains the properties for the http protocol.
	/// </summary>
	[Serializable]	
	[XmlRoot(ElementName="httpProperties", IsNullable=true)]
	public class HttpProperties : Ecyware.GreenBlue.Configuration.Configuration
	{
		private int _timeout = 20000;
		private System.Net.SecurityProtocolType _securityProtocol = System.Net.SecurityProtocolType.Ssl3;
		private string _accept = null;
		private long _contentLen = -1;
		private string _contentType = null;
		private DateTime _ifModifiedSince = DateTime.MinValue;
		private bool _autoredirects = false;
		private string _referer = null;
		private string _transferEncoding = null;
		//private NameValueCollection _addonHeaders = new NameValueCollection();

		// max redirections
		private int _maxredirects = 50;

		// always false
		private bool _writeStreamBuffer = false;
		/// <summary>
		/// User agent default is empty
		/// </summary>
		private string _agent = String.Empty;
		private bool _pipelined = true;
		private bool _chunked = false;
		private string _mediaType = null;

		/// <summary>
		/// Keep alive default is true
		/// </summary>
		private bool _keepalive = true;

		private WebHeaderCollection _additionalHeaders;

		/// <summary>
		/// new authentication
		/// </summary>
		private HttpAuthentication _auth = new HttpAuthentication();

		/// <summary>
		/// Creates a new HttpProperties.
		/// </summary>
		public HttpProperties()
		{
			_additionalHeaders = new WebHeaderCollection();
			_additionalHeaders.Add("Language","en-us");
		}

		//		/// <summary>
		//		/// ISerializable private constructor.
		//		/// </summary>
		//		/// <param name="s"> SerializationInfo. </param>
		//		/// <param name="context"> The StreamingContext.</param>
		//		private HttpProperties(SerializationInfo s, StreamingContext context)
		//		{
		//			this.AllowAutoRedirects = s.GetBoolean("AllowAutoRedirects");
		//			this.AllowWriteStreamBuffering = s.GetBoolean("AllowWriteStreamBuffering");
		//			this.AuthenticationSettings = (HttpAuthentication)s.GetValue("AuthenticationSettings", typeof(HttpAuthentication));
		//			this.KeepAlive = s.GetBoolean("KeepAlive");
		//			this.MaximumAutoRedirects = s.GetInt32("MaximumAutoRedirects");
		//			this.Pipeline = s.GetBoolean("Pipeline");
		//			this.SendChunked = s.GetBoolean("SendChunked");
		//			this.UserAgent = s.GetString("UserAgent");
		//			this.Accept = s.GetString("Accept");
		//			this.ContentLength = Convert.ToInt64(s.GetValue("ContentLength", typeof(long)));
		//			this.ContentType = s.GetString("ContentType");
		//			this.IfModifiedSince = s.GetDateTime("IfModifiedSince");
		//			this.MediaType = s.GetString("MediaType");
		//			this.Referer = s.GetString("Referer");
		//			this.TransferEncoding = s.GetString("TransferEncoding");
		//
		//			try
		//			{
		//				this.SecurityProtocol = (System.Net.SecurityProtocolType)s.GetValue("SecurityProtocol", typeof(System.Net.SecurityProtocolType));
		//			}
		//			catch
		//			{
		//				// ignore
		//			}
		//		}
		//
		//		/// <summary>
		//		/// Serializes the object.
		//		/// </summary>
		//		/// <param name="info"> SerializationInfo.</param>
		//		/// <param name="context"> StreamingContext.</param>
		//		public void GetObjectData(SerializationInfo info, StreamingContext context)
		//		{
		//			info.AddValue("AllowAutoRedirects", this.AllowAutoRedirects);
		//			info.AddValue("AllowWriteStreamBuffering", this.AllowWriteStreamBuffering);
		//			info.AddValue("AuthenticationSettings", this.AuthenticationSettings);
		//			info.AddValue("KeepAlive", this.KeepAlive);
		//			info.AddValue("MaximumAutoRedirects", this.MaximumAutoRedirects);
		//			info.AddValue("Pipeline", this.Pipeline);
		//			info.AddValue("SendChunked", this.SendChunked);
		//			info.AddValue("UserAgent", this.UserAgent);
		//			info.AddValue("Accept", this.Accept);
		//			info.AddValue("ContentLength", this.ContentLength);
		//			info.AddValue("ContentType", this.ContentType);
		//			info.AddValue("IfModifiedSince", this.IfModifiedSince);
		//			info.AddValue("MediaType", this.MediaType);
		//			info.AddValue("Referer", this.Referer);
		//			info.AddValue("TransferEncoding", this.TransferEncoding);
		//
		//			try
		//			{
		//				info.AddValue("SecurityProtocol", this.SecurityProtocol);
		//			}
		//			catch
		//			{
		//			}
		//		}
		//

		#region Properties
		/// <summary>
		/// Gets or sets the Accept header.
		/// </summary>
		[XmlElement("accept")]
		public string Accept
		{
			get
			{
				return _accept;
			}
			set
			{
				_accept = value;
			}
		}


		/// <summary>
		/// Gets or sets the Content-Length header.
		/// </summary>
		[XmlElement("contentLength")]
		public long ContentLength
		{
			get
			{
				return _contentLen;
			}
			set
			{
				_contentLen = value;
			}
		}

		/// <summary>
		/// Gets or sets the Content-type header.
		/// </summary>
		[XmlElement("contentType")]
		public string ContentType
		{
			get
			{
				return _contentType;
			}
			set
			{
				_contentType = value;
			}
		}

		/// <summary>
		/// Gets or sets the If-Modified-Since header.
		/// </summary>
		[XmlElement("ifModifiedSince")]
		public DateTime IfModifiedSince
		{
			get
			{
				return this._ifModifiedSince;
			}
			set
			{
				_ifModifiedSince = value;
			}
		}

		/// <summary>
		/// Gets or sets the media type.
		/// </summary>
		[XmlElement("mediaType")]
		public string MediaType
		{
			get
			{
				return _mediaType;
			}
			set
			{
				_mediaType = value;
			}
		}

		/// <summary>
		/// Gets or sets the referer header.
		/// </summary>
		[XmlElement("referer")]
		public string Referer
		{
			get
			{
				return _referer;
			}
			set
			{
				_referer = value;
			}
		}


		/// <summary>
		/// Gets or sets the Security Protocol type
		/// </summary>
		[XmlElement("securityProtocol")]
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
		/// Gets or sets the Transfer Encoding.
		/// </summary>
		[XmlElement("transferEncoding")]
		public string TransferEncoding
		{
			get
			{
				return _transferEncoding;
			}
			set
			{
				_transferEncoding = value;
			}
		}

		/// <summary>
		/// Gets or sets the HttpAuthentication type.
		/// </summary>
		[XmlElement("authenticationSettings")]
		public HttpAuthentication AuthenticationSettings
		{
			get
			{
				return _auth;
			}
			set
			{
				_auth = value;
			}
		}


		/// <summary>
		/// Gets or sets the HttpAuthentication Username.
		/// </summary>
		[XmlIgnore]
		public string Username
		{
			get
			{
				if ( _auth != null )
				{
					return _auth.Username;
				} 
				else 
				{
					return string.Empty;
				}
			}
			set
			{
				if ( _auth != null )
				{
					_auth.Username = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the HttpAuthentication Password.
		/// </summary>
		[XmlIgnore]
		public string Password
		{
			get
			{
				if ( _auth != null )
				{
					return _auth.Password;
				} 
				else 
				{
					return string.Empty;
				}
			}
			set
			{
				if ( _auth != null )
				{
					_auth.Password = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the HttpAuthentication Use Basic Authentication.
		/// </summary>
		[XmlIgnore]
		public bool UseBasicAuthentication
		{
			get
			{
				if ( _auth != null )
				{
					return _auth.UseBasicAuthentication;
				} 
				else 
				{
					return false;
				}
			}
			set
			{
				if ( _auth != null )
				{
					_auth.UseBasicAuthentication = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the Pipeline setting.
		/// </summary>
		[XmlElement("pipeline")]
		public bool Pipeline
		{
			get{return _pipelined;}
			set{_pipelined=value;}
		}

		/// <summary>
		/// Gets or sets the KeepAlive setting.
		/// </summary>
		[XmlElement("keepAlive")]
		public bool KeepAlive
		{
			get{return _keepalive;}
			set{_keepalive=value;}
		}

		/// <summary>
		/// Gets or sets the SendChunked setting.
		/// </summary>
		[XmlElement("sendChunked")]
		public bool SendChunked
		{
			get{return _chunked;}
			set{_chunked=value;}
		}

		/// <summary>
		/// Gets or sets the UserAgent setting.
		/// </summary>
		[XmlElement("userAgent")]
		public string UserAgent
		{
			get{return _agent;}
			set{_agent=value;}
		}

		/// <summary>
		/// Gets or sets AllowWriteStreamBuffering setting.
		/// </summary>
		[XmlElement("allowWriteStreamBuffering")]
		public bool AllowWriteStreamBuffering
		{
			get
			{
				return _writeStreamBuffer;
			}
			set
			{
				_writeStreamBuffer=value;
			}
		}

		/// <summary>
		/// Gets or sets the AllowAutoRedirects setting.
		/// </summary>
		[XmlElement("allowAutoRedirects")]
		public bool AllowAutoRedirects
		{
			get{return _autoredirects;}
			set{_autoredirects=value;}
		}

		/// <summary>
		/// Gets or sets the MaximumAutoRedirects setting.
		/// </summary>
		[XmlElement("maximumAutoRedirects")]
		public int MaximumAutoRedirects
		{
			get{return _maxredirects;}
			set{_maxredirects=value;}
		}

		/// <summary>
		/// Gets or sets the timeout setting.
		/// </summary>
		[XmlElement("timeout")]
		public int Timeout
		{
			get { return _timeout; }
			set { _timeout = value; }
		}

		/// <summary>
		/// Gets or sets the providers.
		/// </summary>
		[XmlArray("additionalHeaders")]
		[XmlArrayItem(IsNullable=false,ElementName="header")]
		public WebHeader[] AdditionalHeaders
		{
			get
			{
				return WebHeader.ToArray(_additionalHeaders);
			}
			set
			{	
				WebHeader.FillWebHeaderCollection(_additionalHeaders,value);
			}
		}

		#endregion

		/// <summary>
		/// Sets the web header. If it doesn't exists, the item is added.
		/// </summary>
		/// <param name="name"> The web header name.</param>
		/// <param name="value"> The web header value.</param>
		public void SetWebHeader(string name, string value)
		{
			if ( _additionalHeaders[name] == null )
			{
				_additionalHeaders.Add(name, value);
			} 
			else 
			{
				_additionalHeaders[name] = value;
			}
		}

		/// <summary>
		/// Gets the web header.
		/// </summary>
		/// <param name="name"> The web header name.</param>
		/// <returns> Returns a string value.</returns>
		public string GetWebHeader(string name)
		{
			return _additionalHeaders[name];
		}
		/// <summary>
		/// Clones the current object into a new HttpProperties.
		/// </summary>
		/// <returns>A HttpProperties type.</returns>
		public HttpProperties Clone()
		{
			// new memory stream
			MemoryStream ms = new MemoryStream();
			// new BinaryFormatter
			BinaryFormatter bf = new BinaryFormatter(null,new StreamingContext(StreamingContextStates.Clone));
			// serialize
			bf.Serialize(ms, this);
			// go to beggining
			ms.Seek(0, SeekOrigin.Begin);
			// deserialize
			HttpProperties retVal = (HttpProperties)bf.Deserialize(ms);
			ms.Close();

			return retVal;
		}

		#region Strong Type LoadConfiguration and SaveConfiguration
		public static XmlNode SaveConfiguration(object instance, Type[] types)
		{
			return HttpProperties.SaveConfiguration(typeof(HttpProperties),instance,"", types);
		}
		public static HttpProperties LoadConfiguration(XmlNode section)
		{
			return (HttpProperties)HttpProperties.LoadConfiguration(typeof(HttpProperties), section);
		}

		public static HttpProperties LoadConfiguration(XmlNode section, Type[] types)
		{
			return (HttpProperties)HttpProperties.LoadConfiguration(typeof(HttpProperties), section,String.Empty, types);
		}
		#endregion
	}
}
