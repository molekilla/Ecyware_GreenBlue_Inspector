// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Text;
using System.Collections;
using System.Net;
using System.Threading;
using System.IO;
using Ecyware.GreenBlue.Engine.HtmlCommand;

namespace Ecyware.GreenBlue.Engine
{

	/// <summary>
	/// Contains HTTP GET Method logic.
	/// </summary>
	public class GetForm : BaseHttpForm
	{
		/// <summary>
		/// Creates a new GetForm.
		/// </summary>
		public GetForm() : base()
		{
			SetServicePointManagerDefaults();
		}
		#region Async Methods
		/// <summary>
		/// Begins a new asynchronous HTTP Get request.\r\nThis function is not thread safe.
		/// </summary>
		/// <param name="uri"> An uri to request.</param>
		public void StartAsyncHttpGet(string uri)
		{			
			httpRequestState = new HttpState();
			
			uri = EncodeDecode.UrlDecode(uri);
			uri = EncodeDecode.HtmlDecode(uri);

			try
			{
				//this.ValidateIPAddress(new Uri(uri));
				// Set HttpWebRequestProperties
				//SetHttpWebRequestProperties(hwr, properties);

				httpRequestState.HttpRequest = (HttpWebRequest)WebRequest.Create(uri);

				// Continue headers
				//hwr.ContinueDelegate=getRedirectHeaders;

				// new CookieContainer
				httpRequestState.HttpRequest.CookieContainer = new CookieContainer();
				//httpRequestState.httpRequest.Referer = uri;

				IAsyncResult ar = httpRequestState.HttpRequest.BeginGetResponse(new AsyncCallback(FastResponseCallback),httpRequestState);

				// register a timeout
				ThreadPool.RegisterWaitForSingleObject(ar.AsyncWaitHandle, new WaitOrTimerCallback(BaseHttpForm.RequestTimeoutCallback), httpRequestState, this.GetTimeout(), true);
			}
			catch
			{
				throw;
			}
			finally 
			{
				if ( httpRequestState.HttpResponse != null )
				{
					httpRequestState.HttpResponse.Close();
				}
			}
		}


		/// <summary>
		/// Begins a new asynchronous HTTP Get request.
		/// </summary>
		/// <param name="uri"> An uri to request.</param>
		/// <param name="properties"> The HttpProperties to set.</param>
		public void StartAsyncHttpGet(string uri, HttpProperties properties)
		{
			httpRequestState = new HttpState();
			
			uri = EncodeDecode.UrlDecode(uri);
			uri = EncodeDecode.HtmlDecode(uri);

			try
			{
				//this.ValidateIPAddress(new Uri(uri));

				httpRequestState.HttpRequest = (HttpWebRequest)WebRequest.Create(uri);

				// Apply settings
				SetHttpWebRequestProperties(httpRequestState.HttpRequest, properties);

				// Apply proxy settings
				if ( this.ProxySettings != null )
				{
					SetProxy(httpRequestState.HttpRequest,this.ProxySettings);
				}

				// Continue headers
				//hwr.ContinueDelegate=getRedirectHeaders;

				// new CookieContainer
				httpRequestState.HttpRequest.CookieContainer = new CookieContainer();
				//httpRequestState.httpRequest.Referer = uri;

				IAsyncResult ar = httpRequestState.HttpRequest.BeginGetResponse(new AsyncCallback(FastResponseCallback),httpRequestState);

				// register a timeout
				ThreadPool.RegisterWaitForSingleObject(ar.AsyncWaitHandle, new WaitOrTimerCallback(BaseHttpForm.RequestTimeoutCallback), httpRequestState, this.GetTimeout(), true);				
			}
			catch
			{
				throw;
			}
			finally 
			{
				httpRequestState.HttpResponse.Close();
			}

		}


		/// <summary>
		/// Begins a new asynchronous HTTP Get request. This function is not thread safe.
		/// </summary>
		/// <param name="uri"> An uri to request.</param>
		/// <param name="properties"> The HttpProperties to set for the request.</param>
		/// <param name="values"> The values to include for a GET request.</param>
		/// <param name="cookies"> The cookies to set for the request.</param>
		/// <param name="doDecode"> Specify if the url is url and html decoded.</param>
		public void StartAsyncHttpGet(string uri, HttpProperties properties, ArrayList values, CookieCollection cookies, bool doDecode)
		{
			httpRequestState = new HttpState();

			if ( doDecode )
			{
				uri = EncodeDecode.UrlDecode(uri);
				uri = EncodeDecode.HtmlDecode(uri);
			}

			try
			{
				//this.ValidateIPAddress(new Uri(uri));

				if ( values != null )
				{
					uri = AppendToUri(uri,values);
				}

				httpRequestState.HttpRequest = (HttpWebRequest)WebRequest.Create(uri);

				// Set HttpWebRequestProperties
				SetHttpWebRequestProperties(httpRequestState.HttpRequest, properties);

				// Apply proxy settings
				if ( this.ProxySettings != null )
				{
					SetProxy(httpRequestState.HttpRequest,this.ProxySettings);
				}
				// Continue headers
				//hwr.ContinueDelegate=getRedirectHeaders;

				// save cookies
				httpRequestState.HttpRequest.CookieContainer=new CookieContainer();
				if ( cookies!=null )
				{
					httpRequestState.HttpRequest.CookieContainer.Add(cookies);
				}

				//httpRequestState.httpRequest.Referer = uri;
				IAsyncResult ar = httpRequestState.HttpRequest.BeginGetResponse(new AsyncCallback(FastResponseCallback),httpRequestState);

				// register a timeout
				ThreadPool.RegisterWaitForSingleObject(ar.AsyncWaitHandle, new WaitOrTimerCallback(BaseHttpForm.RequestTimeoutCallback), httpRequestState, this.GetTimeout(), true);
			}
			catch
			{
				throw;
			}
			finally 
			{
				if ( httpRequestState.HttpResponse != null )
				{
					httpRequestState.HttpResponse.Close();
				}
			}

		}


		/// <summary>
		/// Begins a new asynchronous HTTP Get request. This function is not thread safe.
		/// </summary>
		/// <param name="uri"> An uri to request.</param>
		/// <param name="properties"> The HttpProperties to set for the request.</param>
		/// <param name="values"> The values to include for a GET request.</param>
		/// <param name="cookies"> The cookies to set for the request.</param>
		/// <param name="httpRequestState"> Sets the Http State type.</param>
		/// <param name="doDecode"> Decodes the uri with Url and Html.</param>
		public void StartAsyncHttpGet(string uri, HttpProperties properties, ArrayList values, CookieCollection cookies, HttpState httpRequestState, bool doDecode)
		{
			if ( doDecode )
			{
				uri = EncodeDecode.UrlDecode(uri);
				uri = EncodeDecode.HtmlDecode(uri);
			}

			try
			{
				//this.ValidateIPAddress(new Uri(uri));

				if ( values != null )
				{
					uri = AppendToUri(uri,values);
				}

				httpRequestState.HttpRequest = (HttpWebRequest)WebRequest.Create(uri);

				// Set HttpWebRequestProperties
				SetHttpWebRequestProperties(httpRequestState.HttpRequest, properties);

				// Apply proxy settings
				if ( this.ProxySettings != null )
				{
					SetProxy(httpRequestState.HttpRequest,this.ProxySettings);
				}
				// Continue headers
				//hwr.ContinueDelegate=getRedirectHeaders;

				// save cookies
				httpRequestState.HttpRequest.CookieContainer=new CookieContainer();
				if ( cookies!=null )
				{
					httpRequestState.HttpRequest.CookieContainer.Add(cookies);
				}
				
				//httpRequestState.httpRequest.Referer = uri;
				IAsyncResult ar = httpRequestState.HttpRequest.BeginGetResponse(new AsyncCallback(FastResponseCallback),httpRequestState);

				// register a timeout
				ThreadPool.RegisterWaitForSingleObject(ar.AsyncWaitHandle, new WaitOrTimerCallback(BaseHttpForm.RequestTimeoutCallback), httpRequestState, this.GetTimeout(), true);
			}
			catch
			{
				throw;
			}
			finally 
			{
				if ( httpRequestState.HttpResponse != null )
				{
					httpRequestState.HttpResponse.Close();
				}
			}

		}



		#endregion
		#region Sync Methods
		/// <summary>
		/// Begins a new synchronous HTTP Get request. This function is not thread safe.
		/// </summary>
		/// <param name="uri"> An uri to request.</param>
		/// <param name="properties"> The HttpProperties to set for the request.</param>
		/// <param name="values"> The values to include for a GET request.</param>
		/// <param name="cookies"> The cookies to set for the request.</param>
		public ResponseBuffer StartSyncHttpGet(string uri, HttpProperties properties, ArrayList values, CookieCollection cookies)
		{
			httpRequestState = new HttpState();

			uri = EncodeDecode.UrlDecode(uri);
			uri = EncodeDecode.HtmlDecode(uri);

			try
			{
				//this.ValidateIPAddress(new Uri(uri));

				if ( values != null )
					uri = AppendToUri(uri,values);

				httpRequestState.HttpRequest = (HttpWebRequest)WebRequest.Create(uri);

				// Set HttpWebRequestProperties
				SetHttpWebRequestProperties(httpRequestState.HttpRequest, properties);

				// Apply proxy settings
				if ( this.ProxySettings != null )
				{
					SetProxy(httpRequestState.HttpRequest,this.ProxySettings);
				}

				// save cookies
				httpRequestState.HttpRequest.CookieContainer=new CookieContainer();
				if ( cookies!=null )
				{
					httpRequestState.HttpRequest.CookieContainer.Add(cookies);
				}

				HttpWebResponse httpResponse = (HttpWebResponse)httpRequestState.HttpRequest.GetResponse();
				httpRequestState.HttpResponse = httpResponse;
				
				// get ResponseBuffer
				ResponseBuffer responseBuffer = HttpPipeline.FillResponseBuffer(httpResponse,
					httpRequestState.HttpRequest,
					properties, 
					httpRequestState);

				return responseBuffer;
			}
			catch
			{
				throw;
			}
			finally 
			{
				if ( httpRequestState.HttpResponse != null )
				{
					httpRequestState.HttpResponse.Close();
				}
			}

		}


		/// <summary>
		/// Begins a new asynchronous HTTP Get request. This function is not thread safe.
		/// </summary>
		/// <param name="uri"> An uri to request.</param>
		/// <param name="properties"> The HttpProperties to set for the request.</param>
		/// <param name="values"> The values to include for a GET request.</param>
		/// <param name="cookies"> The cookies to set for the request.</param>
		/// <param name="httpRequestState"> The HttpState.</param>
		public ResponseBuffer StartSyncHttpGet(string uri, HttpProperties properties, ArrayList values, CookieCollection cookies, HttpState httpRequestState)
		{
			uri = EncodeDecode.UrlDecode(uri);
			uri = EncodeDecode.HtmlDecode(uri);

			try
			{
				//this.ValidateIPAddress(new Uri(uri));

				if ( values != null )
					uri = AppendToUri(uri,values);

				httpRequestState.HttpRequest = (HttpWebRequest)WebRequest.Create(uri);

				// Set HttpWebRequestProperties
				SetHttpWebRequestProperties(httpRequestState.HttpRequest, properties);

				// Apply proxy settings
				if ( this.ProxySettings != null )
				{
					SetProxy(httpRequestState.HttpRequest,this.ProxySettings);
				}

				// save cookies
				httpRequestState.HttpRequest.CookieContainer=new CookieContainer();
				if ( cookies!=null )
				{
					httpRequestState.HttpRequest.CookieContainer.Add(cookies);
				}

				HttpWebResponse httpResponse = (HttpWebResponse)httpRequestState.HttpRequest.GetResponse();
				httpRequestState.HttpResponse = httpResponse;
				
				// get ResponseBuffer
				ResponseBuffer responseBuffer = HttpPipeline.FillResponseBuffer(httpResponse,
					httpRequestState.HttpRequest,
					properties, 
					httpRequestState);				

				return responseBuffer;
			}
			catch
			{
				throw;
			}
			finally 
			{
				if ( httpRequestState.HttpResponse != null )
				{
					httpRequestState.HttpResponse.Close();
				}
			}

		}

		#endregion
		#region Utils

		/// <summary>
		/// Appends query string to uri.
		/// </summary>
		/// <param name="uri"> The uri.</param>
		/// <param name="al"> The ArrayList containing the parameters.</param>
		/// <returns> The new uri.</returns>
		public static string AppendToUri(string uri,ArrayList al)
		{
			StringBuilder buffer = new StringBuilder();

			
			for (int i=0;i<al.Count;i++)
			{
				if (i>0)
				{
					buffer.Append("&");
				}
				buffer.Append(al[i]);
			}

			return uri + "?" + buffer.ToString();
		}
		/// <summary>
		/// Appends query string to uri.
		/// </summary>
		/// <param name="uri"> The uri.</param>
		/// <param name="values"> The hashtable containing the parameters.</param>
		/// <returns> The new uri.</returns>
		private string AppendToUri(string uri,Hashtable values)
		{
			StringBuilder buffer = new StringBuilder();

			int i=0;
			foreach (DictionaryEntry de in values)
			{
				if (i>0)
				{
					buffer.Append("&");
				}
				buffer.AppendFormat("{0}={1}",de.Key,EncodeDecode.UrlEncode((string)de.Value));
				i++;
			}

			return uri + "?" + buffer.ToString();
		}
		#endregion
	}
}
