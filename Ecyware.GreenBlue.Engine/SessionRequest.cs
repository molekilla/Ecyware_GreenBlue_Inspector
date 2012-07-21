// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Security.Permissions;
using System.IO;
using System.Collections;
using System.Net;
using System.Runtime.Serialization;
using Ecyware.GreenBlue.Engine.HtmlDom;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Provides logic for the base session request type.
	/// </summary>
	[Serializable]
	[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
	public class SessionRequest : ISerializable
	{
		private HttpRequestType _requestType = HttpRequestType.GET;
		private CookieCollection _cookies = null;
		private HtmlFormTag _form = null;
		private Uri _uri= null;
		private UnitTestItem _webUnitTest = new UnitTestItem();

		private Hashtable _requestHeaders = null;
		private Hashtable _responseHeaders = null;
		private int _statusCode = 0;
		private string _statusDescription = string.Empty;
		private bool _updateSessionUrl = false;
		private HttpProperties _requestHttpSettings = null;

		/// <summary>
		/// Creates a new SessionRequest.
		/// </summary>
		public SessionRequest()
		{
		}
		/// <summary>
		/// ISerializable private constructor.
		/// </summary>
		/// <param name="s"> SerializationInfo. </param>
		/// <param name="context"> The StreamingContext.</param>
		private SessionRequest(SerializationInfo s, StreamingContext context)
		{
			this.ResponseHeaders = (Hashtable)s.GetValue("ResponseHeaders",typeof(Hashtable));
			this.RequestHeaders = (Hashtable)s.GetValue("RequestHeaders",typeof(Hashtable));
			this.StatusDescription = s.GetString("StatusDescription");
			this.StatusCode = s.GetInt32("StatusCode");
			this.Form = (HtmlFormTag)s.GetValue("Form",typeof(HtmlFormTag));
			this.RequestCookies = (System.Net.CookieCollection)s.GetValue("RequestCookies",typeof(System.Net.CookieCollection));
			this.RequestType = (HttpRequestType)s.GetValue("RequestType",typeof(HttpRequestType));
			this.Url = (Uri)s.GetValue("Url", typeof(Uri));
			this.WebUnitTest = (UnitTestItem)s.GetValue("WebUnitTest",typeof(UnitTestItem));
			try
			{
				this.UpdateSessionUrl = s.GetBoolean("UpdateSessionUrl");
				this.RequestHttpSettings = (HttpProperties)s.GetValue("RequestHttpSettings", typeof(HttpProperties));
			}
			catch
			{
				// do nothing
			}
		}

		/// <summary>
		/// Serializes the object.
		/// </summary>
		/// <param name="info"> SerializationInfo.</param>
		/// <param name="context"> StreamingContext.</param>
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("ResponseHeaders", this.ResponseHeaders);
			info.AddValue("RequestHeaders", this.RequestHeaders );
			info.AddValue("StatusDescription",this.StatusDescription );
			info.AddValue("StatusCode", this.StatusCode);
			info.AddValue("Form", this.Form);
			info.AddValue("RequestCookies",this.RequestCookies);
			info.AddValue("RequestType", this.RequestType);
			info.AddValue("Url", this.Url);
			info.AddValue("WebUnitTest", this.WebUnitTest);
			try
			{
				info.AddValue("UpdateSessionUrl", this.UpdateSessionUrl);
				info.AddValue("RequestHttpSettings", this.RequestHttpSettings);
			}
			catch
			{
				// do nothing
			}
		}

		#region Properties

		/// <summary>
		/// Gets or sets the update session url setting.
		/// </summary>
		public bool UpdateSessionUrl
		{
			get
			{
				return this._updateSessionUrl;
			}
			set
			{
				_updateSessionUrl = value;
			}
		}

		/// <summary>
		/// Gets or sets the http settings.
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
		/// Gets or sets the response headers.
		/// </summary>
		public Hashtable ResponseHeaders
		{
			get
			{
				return _responseHeaders;
			}
			set
			{
				_responseHeaders = value;
			}
		}
		/// <summary>
		/// Gets or sets the request headers.
		/// </summary>
		public Hashtable RequestHeaders
		{
			get
			{
				return _requestHeaders;
			}
			set
			{
				_requestHeaders = value;
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
				_statusDescription = value;
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
				_statusCode = value;
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
		public Uri Url
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
		/// Gets or sets the request cookies.
		/// </summary>
		public CookieCollection RequestCookies
		{
			get
			{
				return _cookies;
			}
			set
			{
				_cookies = value;
			}
		}

		/// <summary>
		/// Gets or sets the form.
		/// </summary>
		public HtmlFormTag Form
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
		/// Gets or sets the web unit test for a session request.
		/// </summary>
		public UnitTestItem WebUnitTest
		{
			get
			{
				return _webUnitTest;
			}
			set
			{
				_webUnitTest = value;
			}
		}
		#endregion
	}
}
