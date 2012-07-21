using System;
using System.Net;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Summary description for BaseHttpState.
	/// </summary>
	public class BaseHttpState
	{
		private HttpWebRequest _httpRequest;
		private HttpWebResponse _httpResponse;	

		public BaseHttpState()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Gets or sets the HttpResponse.
		/// </summary>
		public HttpWebResponse HttpResponse
		{
			get
			{
				return _httpResponse;
			}
			set
			{
				_httpResponse = value;
			}
		}

		/// <summary>
		/// Gets or sets the HttpRequest.
		/// </summary>
		public HttpWebRequest HttpRequest
		{
			get
			{
				return _httpRequest;
			}
			set
			{
				_httpRequest = value;
			}
		}

	}
}
