using System;

namespace Ecyware.GreenBlue.Engine.Scripting
{
	/// <summary>
	/// Summary description for HttpRequestContext.
	/// </summary>
	public class HttpRequestResponseContext
	{
		bool _decodeUrl;
		WebRequest _request;
		int _index;

		/// <summary>
		/// Creates a HttpRequestContext.
		/// </summary>
		public HttpRequestResponseContext()
		{
		}

		/// <summary>
		/// Creates a HttpRequestContext.
		/// </summary>
		/// <param name="request"></param>
		/// <param name="requestIndex"></param>
		/// <param name="decodeUrl"></param>
		public HttpRequestResponseContext(WebRequest request, int requestIndex, bool decodeUrl)
		{
			_request = request;
			_decodeUrl = decodeUrl;
			_index = requestIndex;
		}

		/// <summary>
		/// Gets or sets to decode the url.
		/// </summary>
		public bool DecodeUrl
		{
			get
			{
				return _decodeUrl;
			}
			set
			{
				_decodeUrl = value;
			}
		}
		/// <summary>
		/// Gets or sets the request.
		/// </summary>
		public WebRequest Request
		{
			get
			{
				return _request;
			}
			set
			{
				_request = value;
			}
		}

		/// <summary>
		/// Gets or sets the request index.
		/// </summary>
		public int RequestIndex
		{
			get
			{
				return _index;
			}
			set
			{
				_index = value;
			}
		}
	}
}
