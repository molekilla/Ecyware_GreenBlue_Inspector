// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using System.Text;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms;
using Ecyware.GreenBlue.Engine.Transforms.Designers;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Contains the results format.
	/// </summary>
	public enum DisplayRequestResultFormat
	{
		Text,
		Html
	}

	/// <summary>
	/// Summary description for ResponseResultTransform.
	/// </summary>
	[Serializable]
	[WebTransformAttribute("Response Result Transform", "output","Sends the response result as HTML or TEXT using a transport provider.")]
	[UITransformEditor(typeof(ResponseResultTransformDesigner))]
	public class ResponseResultTransform : WebTransform
	{
		Transport _transport;
		private bool _append = true;
		private DisplayRequestResultFormat _format;
		private bool _useSession;
		private string _sessionName;

		/// <summary>
		/// Creates a new ResponseResultTransform.
		/// </summary>
		public ResponseResultTransform()
		{
		}
		
		/// <summary>
		/// Gets or sets the response result format.
		/// </summary>
		public DisplayRequestResultFormat ResponseResultFormat
		{
			get
			{
				return _format;
			}
			set
			{
				_format = value;
			}
		}


		/// <summary>
		/// Gets or sets if the displaying appends or not the results.
		/// </summary>
		public bool Append
		{
			get
			{
				return _append;
			}
			set
			{
				_append = value;
			}
		}

		public bool UseSession
		{
			get
			{
				return _useSession;
			}

			set
			{
				_useSession = value;
			}
		}

		public string SessionName
		{
			get
			{
				return _sessionName;
			}

			set
			{
				_sessionName = value;
			}
		}

		/// <summary>
		/// Append cookies.
		/// </summary>
		/// <param name="text"> The StringBuilder.</param>
		/// <param name="cookies"> The cookies.</param>
		public static void AppendCookies(StringBuilder text, Cookie[] cookies)
		{			
			foreach ( Cookie ck in cookies )
			{
				text.AppendFormat("Name: {0}, Value: {1}, Path: {2}, Domain: {3} \r\n Port: {4}, Secure: {5}, Timestamp: {6}, Version: {7}\r\n", ck.Name,
					ck.Value, ck.Path, ck.Domain, ck.Port , ck.Secure, ck.TimeStamp, ck.Version);
			}
		}

		/// <summary>
		/// Appens the request headers.
		/// </summary>
		/// <param name="text"> The StringBuilder.</param>
		/// <param name="request"> The web request.</param>
		public static void AppendRequestHeaders(StringBuilder text, WebRequest request)
		{
			text.AppendFormat("{0}: {1}\r\n", 
				"Accept",
				request.RequestHttpSettings.Accept);
			text.AppendFormat("{0}: {1}\r\n",
				"Content Length",
				request.RequestHttpSettings.ContentLength);
			text.AppendFormat("{0}: {1}\r\n",
				"Content Type",
				request.RequestHttpSettings.ContentType);
			text.AppendFormat("{0}: {1}\r\n",
				"If Modified Since",
				request.RequestHttpSettings.IfModifiedSince);
			text.AppendFormat("{0}: {1}\r\n",
				"Keep Alive",
				request.RequestHttpSettings.KeepAlive);
			text.AppendFormat("{0}: {1}\r\n",
				"Referer",
				request.RequestHttpSettings.Referer);
			text.AppendFormat("{0}: {1}\r\n",
				"Send Chunked",
				request.RequestHttpSettings.SendChunked);
			text.AppendFormat("{0}: {1}\r\n",
				"Transfer Encoding",
				request.RequestHttpSettings.TransferEncoding);
			text.AppendFormat("{0}: {1}\r\n",
				"User Agent",
				request.RequestHttpSettings.UserAgent);
			foreach ( WebHeader header in request.RequestHttpSettings.AdditionalHeaders )
			{
				text.AppendFormat("{0}: {1}\r\n", header.Name, header.Value);
			}
		}

		/// <summary>
		/// Appends the response headers.
		/// </summary>
		/// <param name="text"> The StringBuilder.</param>
		/// <param name="response"> The web response.</param>
		public static void AppendResponseHeaders(StringBuilder text, WebResponse response)
		{
			text.AppendFormat("{0}: {1}\r\n",
				"Accept",
				response.ResponseHttpSettings.Accept);
			text.AppendFormat("{0}: {1}\r\n",
				"Character Set",
				response.CharacterSet);
			text.AppendFormat("{0}: {1}\r\n",
				"Content Encoding",
				response.ContentEncoding);
			text.AppendFormat("{0}: {1}\r\n",
				"Content Length",
				response.ResponseHttpSettings.ContentLength);
			text.AppendFormat("{0}: {1}\r\n",
				"Content Type",
				response.ResponseHttpSettings.ContentType);
			text.AppendFormat("{0}: {1}\r\n",
				"If Modified Since",
				response.ResponseHttpSettings.IfModifiedSince);
			text.AppendFormat("{0}: {1}\r\n",
				"Keep Alive",
				response.ResponseHttpSettings.KeepAlive);
			text.AppendFormat("{0}: {1}\r\n",
				"Referer",
				response.ResponseHttpSettings.Referer);
			text.AppendFormat("{0}: {1}\r\n",
				"Send Chunked",
				response.ResponseHttpSettings.SendChunked);
			text.AppendFormat("{0}: {1}\r\n",
				"Transfer Encoding",
				response.ResponseHttpSettings.TransferEncoding);
			text.AppendFormat("{0}: {1}\r\n",
				"User Agent",
				response.ResponseHttpSettings.UserAgent);
			foreach ( WebHeader header in response.ResponseHttpSettings.AdditionalHeaders )
			{
				text.AppendFormat("{0}: {1}\r\n", header.Name, header.Value);
			}
		}

		/// <summary>
		/// Gets or sets the transport
		/// </summary>
		public Transport Transport
		{
			get
			{
				return _transport;
			}
			set
			{
				_transport = value;
			}
		}

		public override Argument[] GetArguments()
		{
			if ( Transport.GetArguments() != null )
			{
				return Transport.GetArguments();
			} 
			else 
			{
				return null;
			}
		}

		public override void ApplyTransform(Ecyware.GreenBlue.Engine.Scripting.WebRequest request, Ecyware.GreenBlue.Engine.Scripting.WebResponse response)
		{
			base.ApplyTransform (request, response);

			//if ( this.SupportsCallbacks )
			//{
			//Delegate[] callbacks =  this.GetTransformCallbacks();
			//Delegate useDelegate = null;
			HtmlTextResultEventArgs args = new HtmlTextResultEventArgs();
			StringBuilder text = new StringBuilder();

			// Client Request
			text.Append(request.RequestType.ToString());
			text.Append(" ");
			text.Append(request.Url);
			text.Append(" ");
			text.Append("HTTP/1.1");
			text.Append("\r\n\r\n");

			// Request Headers
			text.Append("-----------------------------------------\r\n");
			text.Append("Request Headers\r\n");
			text.Append("-----------------------------------------\r\n");

			// Append request headers
			AppendRequestHeaders(text, request);

			// Cookies
			text.Append("-----------------------------------------\r\n");
			text.Append("Request Cookies\r\n");
			text.Append("-----------------------------------------\r\n");				
			AppendCookies(text, request.Cookies);

			// Server Response
			text.Append("HTTP/");
			text.Append(request.WebResponse.Version);
			text.Append(" ");
			text.Append(request.WebResponse.StatusCode.ToString());
			text.Append(" ");
			text.Append(request.WebResponse.StatusDescription);
			text.Append("\r\n\r\n");
				
			// Response Headers
			text.Append("-----------------------------------------\r\n");
			text.Append("Response Headers\r\n");
			text.Append("-----------------------------------------\r\n");				

			AppendResponseHeaders(text, request.WebResponse);
				
			// Cookies
			text.Append("-----------------------------------------\r\n");
			text.Append("Cookies\r\n");
			text.Append("-----------------------------------------\r\n");
			AppendCookies(text, request.WebResponse.Cookies);

			// HTTP
			text.Append("-----------------------------------------\r\n");
			text.Append("HTML\r\n");
			text.Append("-----------------------------------------\r\n");
			text.Append(request.WebResponse.HttpBody + "\r\n");

			if  ( Transport != null )
			{
				// Send message to transport.
				Transport.Send(new string[] {text.ToString()});
			}
			
			//}
		}

	}
}
