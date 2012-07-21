// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Text;
using System.IO;
using System.Xml;
using System.Net;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Ecyware.GreenBlue.Engine.HtmlDom;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Contains a subset of properties from the HttpWebResponse and HttpWebRequest types.
	/// </summary>
	[Serializable]
	public class ResponseBuffer
	{
		private string _responseHeader=String.Empty;
		private string _requestHeader=String.Empty;
		private string _continueHeader=String.Empty;
		private string _httpBody=String.Empty;
		private string _errorMessage=String.Empty;
		private string _cookieData=String.Empty;
		private HtmlScriptCollection _scripts = new HtmlScriptCollection();

		private Hashtable _responseHeaderCollection;
		private Hashtable _requestHeaderCollection;
		private Hashtable _continueHeaderCollection;
		private CookieCollection _cookieCollection;
		private string _parseHtml = String.Empty;
		private HttpRequestType _method; 

		string _version;
		int _statusCode;
		string _statusDescription;

		#region Constructors
		/// <summary>
		/// Creates a new ResponseBuffer.
		/// </summary>
		public ResponseBuffer()
		{
		}
		/// <summary>
		/// Creates a new ResponseBuffer.
		/// </summary>
		/// <param name="requestType"> The request type of the response buffer.</param>
		public ResponseBuffer(HttpRequestType requestType)
		{
			this.RequestType = requestType;
		}
		#endregion

		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		public string Version
		{
			get
			{
				return _version;
			}
			set
			{
				_version=value;
			}
		}

		/// <summary>
		/// Gets or sets the status description.
		/// </summary>
		public string StatusDescription
		{
			get
			{
				return _statusDescription;
			}
			set
			{
				_statusDescription=value;
			}
		}

		/// <summary>
		/// Gets or sets the status code.
		/// </summary>
		public int StatusCode
		{
			get
			{
				return _statusCode;
			}
			set
			{
				_statusCode=value;
			}
		}

		/// <summary>
		/// Gets or sets the Request type.
		/// </summary>
		public HttpRequestType RequestType
		{
			get
			{
				return _method;
			}
			set
			{
				_method=value;
			}
		}
		/// <summary>
		/// Gets or sets the parsed html.
		/// </summary>
		public string GetHtmlXml
		{
			get
			{
				return _parseHtml;
			}
			set
			{
				_parseHtml = value;
			}
		}

		/// <summary>
		/// Gets or sets the cookie data.
		/// </summary>
		public string CookieData
		{
			get
			{
				return _cookieData;
			}
			set
			{
				_cookieData = value;
			}
		}

		/// <summary>
		/// Gets or sets the cookie collection.
		/// </summary>
		public CookieCollection CookieCollection
		{
			get
			{
				return _cookieCollection;
			}
			set
			{
				_cookieCollection = value;
			}
		}

		/// <summary>
		/// Gets or sets the response header collection.
		/// </summary>
		public Hashtable ResponseHeaderCollection
		{
			get
			{
				return _responseHeaderCollection;
			}
			set
			{
				_responseHeaderCollection = value;
			}
		}

		/// <summary>
		/// Gets or sets the response header string.
		/// </summary>
		public String ResponseHeader
		{
			get
			{
				return _responseHeader;
			}
			set
			{
				_responseHeader = value;
			}
		}

		/// <summary>
		/// Gets or sets the request header string
		/// </summary>
		public string RequestHeader
		{
			get
			{
				return _requestHeader;
			}
			set
			{
				_requestHeader = value;
			}
		}

		/// <summary>
		/// Gets or sets the request header collection
		/// </summary>
		public Hashtable RequestHeaderCollection
		{
			get
			{
				return _requestHeaderCollection;
			}
			set
			{
				_requestHeaderCollection = value;
			}
		}

		/// <summary>
		/// Gets or sets the HttpBody
		/// </summary>
		public string HttpBody
		{
			get
			{
				return _httpBody;
			}
			set
			{
				_httpBody = value;
			}
		}

		/// <summary>
		/// Gets or sets the continue header string.
		/// </summary>
		public string ContinueHeader
		{
			get
			{
				return _continueHeader;
			}
			set
			{
				_continueHeader = value;
			}
		}

		/// <summary>
		/// Gets or sets the continue header collection.
		/// </summary>
		public Hashtable ContinueHeaderCollection
		{
			get
			{
				return _continueHeaderCollection;
			}
			set
			{
				_continueHeaderCollection = value;
			}
		}

		/// <summary>
		/// Gets or sets the internal error message.
		/// </summary>
		public string ErrorMessage
		{
			get
			{
				return _errorMessage;
			}
			set
			{
				_errorMessage = value;
			}
		}


		/// <summary>
		/// Gets or sets the script collection.
		/// </summary>
		public HtmlScriptCollection Scripts
		{
			get
			{
				return _scripts;
			}
			set
			{
				_scripts = value;
			}
		}

		#region Clone Method
		/// <summary>
		/// Clones the current object into a new ResponseBuffer.
		/// </summary>
		/// <returns>A new ResponseBuffer.</returns>
		public ResponseBuffer Clone()
		{
			// new memory stream
			MemoryStream ms = new MemoryStream();
			// new BinaryFormatter
			BinaryFormatter bf = new BinaryFormatter(null,new StreamingContext(StreamingContextStates.Clone));
			// serialize
			bf.Serialize(ms,this);
			// go to beggining
			ms.Seek(0,SeekOrigin.Begin);
			// deserialize
			ResponseBuffer retVal = (ResponseBuffer)bf.Deserialize(ms);
			ms.Close();

			return retVal;
		}
		#endregion
	}
}
