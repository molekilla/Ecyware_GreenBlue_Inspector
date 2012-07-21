// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Net;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// PacketStateData contains the state data that is used between the BaseHttpForm and WorkerProcess.
	/// </summary>
	internal class PacketStateData
	{
		private string _message=String.Empty;
		private HttpState _state = null;
		private HttpWebResponse _response = null;
		private Delegate _callerMethod=null;
		private ResponseBuffer _responseBuffer = null;
		private HttpProperties _clientSettings = null;
		private HttpProxy _proxySettings = null;

		/// <summary>
		/// Creates a new PacketStateData.
		/// </summary>
		public PacketStateData()
		{
		}

		/// <summary>
		/// PacketStateData Constructor.
		/// </summary>
		/// <param name="state"> The HttpState associated with the request.</param>
		/// <param name="response"> The HttpWebResponse.</param>
		public PacketStateData(HttpState state,HttpWebResponse response)
		{
			this.HttpStateData = state;
			this.WebResponse = response;
		}

		/// <summary>
		/// PacketStateData Constructor.
		/// </summary>
		/// <param name="state"> The HttpState associated with the request.</param>
		/// <param name="response"> The HttpWebResponse.</param>
		/// <param name="message"> The error message.</param>
		public PacketStateData(HttpState state,HttpWebResponse response,string message)
		{
			this.HttpStateData = state;
			this.WebResponse = response;
			this.ErrorMessage = message;
		}

		/// <summary>
		/// Gets or sets the HttpStateData.
		/// </summary>
		public HttpState HttpStateData
		{
			get
			{
				return _state;
			}
			set
			{
				_state = value;
			}
		}

		/// <summary>
		/// Gets or sets the HttpWebResponse.
		/// </summary>
		public HttpWebResponse WebResponse
		{
			get
			{
				return _response;
			}
			set
			{
				_response = value;
			}
		}

		/// <summary>
		/// Gets or sets the error message.
		/// </summary>
		public string ErrorMessage
		{
			get
			{
				return _message;
			}
			set
			{
				_message = value;
			}
		}

		/// <summary>
		/// Gets or sets the caller method.
		/// </summary>
		public Delegate CallerMethod
		{
			get
			{
				return _callerMethod;
			}
			set
			{
				_callerMethod = value;
			}
		}

		/// <summary>
		/// Gets or sets the ResponseBuffer data.
		/// </summary>
		public ResponseBuffer ResponseData
		{
			get
			{
				return _responseBuffer;
			}
			set
			{
				_responseBuffer = value;
			}
		}

		/// <summary>
		/// Gets or sets the HTTP client settings.
		/// </summary>
		public HttpProperties ClientSettings
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
		/// Gets or sets the Proxy settings.
		/// </summary>
		public HttpProxy ProxySettings
		{
			get
			{
				return _proxySettings;
			}
			set
			{
				_proxySettings = value;
			}
		}

	}
}
