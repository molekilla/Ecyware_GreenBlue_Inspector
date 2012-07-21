using System;
using System.Net;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Engine.Transforms;

namespace Ecyware.GreenBlue.Engine.Scripting
{
	/// <summary>
	/// Contains the WebRequest.
	/// </summary>
	[Serializable]
	public abstract class WebRequest
	{
		private System.Xml.XmlNode _xmlElement = null;
		private WebResponse _result = new WebResponse();
		private HttpProperties _requestHttpSettings = new HttpProperties();
		private ArrayList _inputTransforms = new ArrayList();
		private ArrayList _outputTransforms = new ArrayList();
		private HtmlFormTagXml _form = new HtmlFormTagXml();
		private Cookies _cookies = new Cookies();
		private HttpRequestType _requestType = HttpRequestType.GET;
//		private int _statusCode = 0;
//		private string _statusDescription = string.Empty;		
		private string _uri;
		private string _name;
		private static Random rnd = new Random();
		

		/// <summary>
		/// Creates a new WebRequest.
		/// </summary>
		public WebRequest()
		{
		}

		#region Properties

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		public string ID
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}

		/// <summary>
		/// Gets or sets the RequestType.
		/// </summary>
		public HttpRequestType RequestType
		{
			get
			{
				return _requestType;
			}
			set
			{
				_requestType = value;
			}
		}

		/// <summary>
		/// Gets or sets the url.
		/// </summary>
		public string Url
		{
			get
			{
				return _uri;
			}
			set
			{
				_uri = value;
			}
		}

		/// <summary>
		/// Gets or sets the HTTP client settings.
		/// </summary>
		public HttpProperties RequestHttpSettings
		{
			get
			{
				return this._requestHttpSettings;
			}
			set
			{
				_requestHttpSettings = value;
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
		/// Gets or sets the form.
		/// </summary>
		public HtmlFormTagXml Form
		{
			get
			{
				return _form;
			}
			set
			{
				_form = value;
			}
		}

		/// <summary>
		/// Gets or sets the web response.
		/// </summary>
		public WebResponse WebResponse
		{
			get
			{
				return _result;
			}
			set
			{
				_result = value;
			}
		}

		/// <summary>
		/// Gets or sets the xml envelope.
		/// </summary>	
		public System.Xml.XmlNode XmlEnvelope
		{
			get
			{
				return _xmlElement;
			}
			set
			{
				_xmlElement = value;
			}
		}
		#endregion

		#region Helpers
		/// <summary>
		/// Updates the xml element.
		/// </summary>
		public void UpdateXmlEnvelope(string postData)
		{
			XmlTextReader reader = null;
			MemoryStream stream = null;
			if ( postData.Length > 0 )
			{				
				stream = new MemoryStream();
				byte[] data = System.Text.Encoding.UTF8.GetBytes(postData);
				stream.Write(data,0, data.Length);
				stream.Position = 0;
							
				try
				{
					reader = new XmlTextReader(stream);
				}
				catch
				{
					// no xml
					reader = null;
				}
			}

			if ( reader != null )
			{
				XmlDocument document = new XmlDocument();
				document.Load(reader);
				XmlEnvelope = document.DocumentElement;
			} 
			else 
			{
				XmlEnvelope = null;
			}		

			if ( stream != null )
			{
				stream.Close();
			}
		}
		/// <summary>
		/// Creates a new web request.
		/// </summary>
		/// <param name="httpRequestType"> The HTTP request type.</param>
		/// <param name="url"> The url.</param>
		/// <returns> A new web request.</returns>
		public static WebRequest Create(HttpRequestType httpRequestType, string url)
		{
			WebRequest request = null;

			switch ( httpRequestType )
			{
				case HttpRequestType.GET:
					request = new GetWebRequest();
					request.Url = url;
					break;
				case HttpRequestType.POST:
					request = new PostWebRequest();
					request.Url = url;
					break;
				case HttpRequestType.PUT:
					request = new PutWebRequest();
					request.Url = url;
					break;
				case HttpRequestType.DELETE:
					request = new DeleteWebRequest();
					request.Url = url;
					break;
				case HttpRequestType.SOAPHTTP:
					request = new SoapHttpWebRequest();
					request.Url = url;
					break;
			}

			return request;
		}
		/// <summary>
		/// Clears the form.
		/// </summary>
		public void ClearForm()
		{
			_form = new HtmlFormTagXml();
		}


		/// <summary>
		/// Clears the cookies.
		/// </summary>
		public void ClearCookies()
		{
			_cookies.CookieList().Clear();
		}

		public void AddCookies(System.Net.CookieCollection cookies )
		{
			_cookies.AddCookies(cookies);
		}

		public void AddCookie(System.Net.Cookie cookie)
		{
			_cookies.AddCookie(cookie);
		}

		public System.Net.CookieCollection GetCookieCollection()
		{
			System.Net.CookieCollection cookies = new CookieCollection();
			foreach ( Cookie cookie in _cookies.GetCookies() )
			{
				System.Net.Cookie cky = new System.Net.Cookie();
				if ( cookie.CommentUri != null )
					cky.CommentUri = new Uri(cookie.CommentUri);
				cky.Domain = cookie.Domain;
				cky.Discard = cookie.Discard;
				cky.Expired = cookie.Expired;
				cky.Expires = cookie.Expires;
				cky.Name = cookie.Name;
				cky.Path = cookie.Path;
				cky.Port = cookie.Port;
				cky.Secure = cookie.Secure;
				// cky.TimeStamp = cookie.TimeStamp;
				cky.Value = cookie.Value;
				cky.Version = cookie.Version;

				cookies.Add(cky);
			}

			return cookies;
		}

		/// <summary>
		/// Adds a new input transform.
		/// </summary>
		/// <param name="inputTransform"> The input transform to add.</param>
		public void AddInputTransform(WebTransform inputTransform)
		{
			_inputTransforms.Add(inputTransform);
		}
		/// <summary>
		/// Adds a new output transform.
		/// </summary>
		/// <param name="outputTransform"> The output transform to add.</param>
		public void AddOutputTransform(WebTransform outputTransform)
		{
			_outputTransforms.Add(outputTransform);
		}

		/// <summary>
		/// Removes a input transform.
		/// </summary>
		/// <param name="inputTransform"> The input transform.</param>
		public void RemoveInputTransform(WebTransform inputTransform)
		{
			if ( _inputTransforms.IndexOf(inputTransform) > -1 )
			{
				_inputTransforms.Remove(inputTransform);
			}
		}

		/// <summary>
		/// Removes a output transform.
		/// </summary>
		/// <param name="outputTransform"> The output transform.</param>
		public void RemoveOutputTransform(WebTransform outputTransform)
		{
			if ( _outputTransforms.IndexOf(outputTransform) > -1 )
			{
				_outputTransforms.Remove(outputTransform);
			}
		}
		/// <summary>
		/// Generates an ID.
		/// </summary>
		protected string GenerateID
		{
			get
			{								
				double i = rnd.Next();
				return i.ToString();
			}
		}
		#endregion


		/// <summary>
		/// Gets or sets the input transforms.
		/// </summary>
		public WebTransform[] InputTransforms
		{
			get
			{
				return (WebTransform[])_inputTransforms.ToArray(typeof(WebTransform));
			}
			set
			{
				if ( value != null )
				{
					_inputTransforms.Clear();
					_inputTransforms.AddRange(value);
				}
			}
		}	
	
		/// <summary>
		/// Gets or sets the output transforms.
		/// </summary>
		public WebTransform[] OutputTransforms
		{
			get
			{
				return (WebTransform[])_outputTransforms.ToArray(typeof(WebTransform));
			}
			set
			{
				if ( value != null )
				{
					_outputTransforms.Clear();
					_outputTransforms.AddRange(value);
				}
			}
		}		
	}
}
