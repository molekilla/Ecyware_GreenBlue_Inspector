using System;
using System.Collections;
using Ecyware.GreenBlue.Engine.HtmlDom;

namespace Ecyware.GreenBlue.Engine.Scripting
{
	/// <summary>
	/// Contains the WebResponse.
	/// </summary>
	[Serializable]
	public class WebResponse
	{
		private string _httpBody = String.Empty;
		private string _errorMessage = String.Empty;
		private ArrayList _scripts = new ArrayList();
		private Cookies _cookies = new Cookies();
		private string _parseHtml = String.Empty;
		private string _responseUri;
		private string _characterSet;
		private string _contentEncoding;
		private HttpProperties _clientSettings = new HttpProperties();
		
		string _version;
		int _statusCode;
		string _statusDescription;

		#region Constructors
		/// <summary>
		/// Creates a new ResponseBuffer.
		/// </summary>
		public WebResponse()
		{
		}
		#endregion

		/// <summary>
		/// Gets or sets the response uri.
		/// </summary>
		public string ResponseUri
		{
			get
			{
				return _responseUri;
			}
			set
			{
				_responseUri = value;
			}
		}
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
		/// Gets or sets the content encoding.
		/// </summary>
		public string ContentEncoding
		{
			get
			{
				return _contentEncoding;
			}
			set
			{
				_contentEncoding = value;
			}
		}

		/// <summary>
		/// Gets or sets the character set.
		/// </summary>
		public string CharacterSet
		{
			get
			{
				return _characterSet;
			}
			set
			{
				_characterSet = value;
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
		/// Gets or sets the request cookies.
		/// </summary>
		public Cookie[] Cookies
		{
			get
			{
				return _cookies.GetCookies();
			}
			set
			{
				if ( value != null )
					_cookies.CookieList().AddRange(value);
			}
		}

		/// <summary>
		/// Gets or sets the response HTTP client settings.
		/// </summary>
		public HttpProperties ResponseHttpSettings
		{
			get
			{
				return _clientSettings;
			}
			set
			{
				_clientSettings = value;
			}
		}

		/// <summary>
		/// Gets or sets the script collection.
		/// </summary>
		public HtmlScript[] Scripts
		{
			get
			{
				return (HtmlScript[])_scripts.ToArray(typeof(HtmlScript));
			}
			set
			{
				_scripts.AddRange(value);
			}
		}

	}
}
