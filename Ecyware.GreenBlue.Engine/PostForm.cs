// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004 - January 2005
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Collections;
using System.Collections.Specialized;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Engine.HtmlCommand;


namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// PostForm contains HTTP POST Method logic.
	/// </summary>
	public class PostForm : BaseHttpForm
	{

		/// <summary>
		/// PostForm Constructor.
		/// </summary>
		public PostForm() : base()
		{
			SetServicePointManagerDefaults();
			//this.Timeout = 10*1000; // 10 seconds default timeout for posts.
		}

		#region Async Methods
		/// <summary>
		/// Begins a new asynchronous HTTP Post request. This function is not thread safe.
		/// </summary>
		/// <param name="postUri"> The Url string.</param>
		/// <param name="properties"> The HttpProperties to set for the request.</param>
		/// <param name="values"> The data to post.</param>
		public void StartAsyncHttpPost(string postUri,HttpProperties properties,ArrayList values)
		{
			if ( values != null ) 
			{
				StartAsyncHttpPost(postUri,properties,values,null);
			}
		}



		/// <summary>
		/// Begins a new asynchronous HTTP Post request. This function is not thread safe.
		/// </summary>
		/// <param name="context"> The HttpRequestResponseContext type.</param>
		public void StartAsyncHttpPostFileUpload(Ecyware.GreenBlue.Engine.Scripting.HttpRequestResponseContext context)
		{
			Ecyware.GreenBlue.Engine.Scripting.PostWebRequest postWebRequest = (Ecyware.GreenBlue.Engine.Scripting.PostWebRequest)context.Request;
			HtmlFormTag formTag = postWebRequest.Form.WriteHtmlFormTag();
			UploadFileInfo[] fileInfoItems = UploadFileInfo.GetUploadFiles(formTag);
			
			HtmlParser parser = new HtmlParser();
			httpRequestState = new HttpState();

			string uriString = context.Request.Url;

			if ( context.DecodeUrl )
			{
				uriString = EncodeDecode.UrlDecode(uriString);				
			}

			// create webrequest
			try
			{
				httpRequestState.HttpRequest = (HttpWebRequest)System.Net.WebRequest.Create(uriString);

				// Set HttpWebRequestProperties
				SetHttpWebRequestProperties(httpRequestState.HttpRequest, context.Request.RequestHttpSettings);

				// Apply proxy settings
				if ( this.ProxySettings != null )
				{
					SetProxy(httpRequestState.HttpRequest,this.ProxySettings);
				}			
				
				// Save cookies
				httpRequestState.HttpRequest.CookieContainer = new CookieContainer();
				if ( context.Request.Cookies != null )
				{										
					httpRequestState.HttpRequest.CookieContainer.Add(context.Request.GetCookieCollection());
				}

				#region Post Data
				string boundary  = "--------------" + DateTime.Now.Ticks.ToString("x");

				byte[] data = null;							
				// Build the trailing boundary string as a byte array
				// ensuring the boundary appears on a line by itself
				byte[] boundaryBytes = null;					

				long sumFileLength = 0;
				foreach ( UploadFileInfo uploadFile in fileInfoItems )
				{					
					FileInfo fileInformation = new FileInfo(uploadFile.FileName);
					sumFileLength += fileInformation.Length;
				}

				string postdata = GenerateMimeFormData(boundary, formTag, fileInfoItems);
				data = Encoding.UTF8.GetBytes(postdata);

				long length;

				if ( fileInfoItems.Length == 0 )
				{
					boundaryBytes = Encoding.ASCII.GetBytes("--" + boundary + "--\r\n\0");
					length = data.Length + sumFileLength + 
						boundaryBytes.Length;
				} 
				else 
				{
					boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
					length = data.Length + sumFileLength + 
						boundaryBytes.Length;
				}

				// set properties
				//httpRequestState.HttpRequest.AllowWriteStreamBuffering = false;
				httpRequestState.HttpRequest.ServicePoint.Expect100Continue = false;
				httpRequestState.HttpRequest.KeepAlive = true;
				httpRequestState.HttpRequest.Timeout = 120000;
				httpRequestState.HttpRequest.Method = "POST";
				httpRequestState.HttpRequest.ContentType = "multipart/form-data; boundary=" + boundary;
				httpRequestState.HttpRequest.ContentLength = length;
				httpRequestState.HttpRequest.Headers.Add("Cache-Control","no-cache");

				// get request stream and write header
				Stream stm = httpRequestState.HttpRequest.GetRequestStream();
				stm.Write(data,0,data.Length);

				if ( fileInfoItems.Length > 0 )
				{
					foreach ( UploadFileInfo uploadFile in fileInfoItems )
					{
						UploadFile(uploadFile, boundaryBytes, stm);
					}										
				}
				stm.Write(boundaryBytes, 0, boundaryBytes.Length);
				stm.Flush();
				stm.Close();
				#endregion

				// Get Response
				IAsyncResult ar = httpRequestState.HttpRequest.BeginGetResponse(new AsyncCallback(FastResponseCallback),httpRequestState);

				// register a timeout
				ThreadPool.RegisterWaitForSingleObject(ar.AsyncWaitHandle, new WaitOrTimerCallback(BaseHttpForm.RequestTimeoutCallback), httpRequestState,this.GetTimeout(), true);

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
		/// Begins a new asynchronous HTTP Post request. This function is not thread safe.
		/// </summary>
		/// <param name="uriString"> The URL string.</param>
		/// <param name="clientSettings"> The client settings.</param>
		/// <param name="form"> The HTML Form tag.</param>
		/// <param name="cookies"> The cookies collection.</param>
		/// <param name="decodeUrl"> Decodes the url in Url and Html decoding.</param>
		public void StartAsyncHttpPost(string uriString, HttpProperties clientSettings, HtmlFormTag form, CookieCollection cookies, bool decodeUrl)
		{
			HtmlParser parser = new HtmlParser();
			ArrayList values = parser.GetArrayList(form);

			httpRequestState = new HttpState();

			if ( decodeUrl )
			{
				uriString = EncodeDecode.UrlDecode(uriString);
				uriString = EncodeDecode.HtmlDecode(uriString);
			}

			// create webrequest
			try
			{
				//this.ValidateIPAddress(new Uri(uriString));

				httpRequestState.HttpRequest = (HttpWebRequest)WebRequest.Create(uriString);

				// Set HttpWebRequestProperties
				SetHttpWebRequestProperties(httpRequestState.HttpRequest, clientSettings);

				// Apply proxy settings
				if ( this.ProxySettings != null )
				{
					SetProxy(httpRequestState.HttpRequest,this.ProxySettings);
				}
				
				//httpRequestState.httpRequest.Referer = postUri;

				// Continue headers
				//hwr.ContinueDelegate = getRedirectHeaders;
				
				// save cookies
				httpRequestState.HttpRequest.CookieContainer=new CookieContainer();

				if ( cookies!=null )
				{
					httpRequestState.HttpRequest.CookieContainer.Add(cookies);
				}

				byte[] data=null;
				if (values!=null)
				{
					// transform to postdata and encode in bytes
					string postData = GetPostData(values);
					data = Encoding.UTF8.GetBytes(postData);

					// set properties
					httpRequestState.HttpRequest.Method="POST";
					httpRequestState.HttpRequest.ContentType="application/x-www-form-urlencoded";
					httpRequestState.HttpRequest.ContentLength=data.Length;

					// get request stream
					Stream stm = httpRequestState.HttpRequest.GetRequestStream();
					stm.Write(data,0,data.Length);
					stm.Flush();
					stm.Close();			
				}

				// Get Response
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
		/// Begins a new asynchronous HTTP Post request. This function is not thread safe.
		/// </summary>
		/// <param name="postUri"> The Url string.</param>
		/// <param name="properties"> The HttpProperties to set for the request.</param>
		/// <param name="values"> The data to post.</param>
		/// <param name="cookies"> The cookie data to set for the request.</param>
		public void StartAsyncHttpPost(string postUri,HttpProperties properties,ArrayList values, CookieCollection cookies)
		{
			httpRequestState = new HttpState();

			postUri = EncodeDecode.UrlDecode(postUri);
			postUri = EncodeDecode.HtmlDecode(postUri);

			// create webrequest
			try
			{
				//this.ValidateIPAddress(new Uri(postUri));

				httpRequestState.HttpRequest = (HttpWebRequest)WebRequest.Create(postUri);

				// Set HttpWebRequestProperties
				SetHttpWebRequestProperties(httpRequestState.HttpRequest, properties);

				// Apply proxy settings
				if ( this.ProxySettings != null )
				{
					SetProxy(httpRequestState.HttpRequest,this.ProxySettings);
				}
				
				//httpRequestState.httpRequest.Referer = postUri;

				// Continue headers
				//hwr.ContinueDelegate = getRedirectHeaders;
				
				// save cookies
				httpRequestState.HttpRequest.CookieContainer=new CookieContainer();

				if ( cookies!=null )
				{
					httpRequestState.HttpRequest.CookieContainer.Add(cookies);
				}

				byte[] data=null;
				if (values!=null)
				{
					// transform to postdata and encode in bytes
					string postData = GetPostData(values);
					data = Encoding.UTF8.GetBytes(postData);

					// set properties
					httpRequestState.HttpRequest.Method="POST";
					httpRequestState.HttpRequest.ContentType="application/x-www-form-urlencoded";
					httpRequestState.HttpRequest.ContentLength=data.Length;

					// get request stream
					Stream stm = httpRequestState.HttpRequest.GetRequestStream();
					stm.Write(data,0,data.Length);
					stm.Flush();
					stm.Close();			
				}

				// Get Response
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
		/// Begins a new asynchronous HTTP Post request. This function is not thread safe.
		/// </summary>
		/// <param name="postUri"> The Url string.</param>
		/// <param name="properties"> The HttpProperties to set for the request.</param>
		/// <param name="values"> The data to post.</param>
		/// <param name="cookies"> The cookie data to set for the request.</param>
		/// <param name="httpRequestState"> The HttpState.</param>		
		public void StartAsyncHttpPost(string postUri,HttpProperties properties,ArrayList values, CookieCollection cookies, HttpState httpRequestState)
		{
			//			httpRequestState = new HttpState();
			//			httpRequestState.TestItem = unitTestItem;
			//			httpRequestState.IsLastItem = lastItem;

			postUri = EncodeDecode.UrlDecode(postUri);
			postUri = EncodeDecode.HtmlDecode(postUri);

			// create webrequest
			try
			{
				//this.ValidateIPAddress(new Uri(postUri));

				httpRequestState.HttpRequest = (HttpWebRequest)WebRequest.Create(postUri);

				// Set HttpWebRequestProperties
				SetHttpWebRequestProperties(httpRequestState.HttpRequest, properties);

				// Apply proxy settings
				if ( this.ProxySettings != null )
				{
					SetProxy(httpRequestState.HttpRequest,this.ProxySettings);
				}

				//httpRequestState.httpRequest.Referer = postUri;

				// Continue headers
				//hwr.ContinueDelegate = getRedirectHeaders;
				
				// save cookies
				httpRequestState.HttpRequest.CookieContainer=new CookieContainer();

				if ( cookies!=null )
				{
					httpRequestState.HttpRequest.CookieContainer.Add(cookies);
				}

				byte[] data=null;
				if (values!=null)
				{
					// transform to postdata and encode in bytes
					string postData = GetPostData(values);
					data = Encoding.UTF8.GetBytes(postData);

					// set properties
					httpRequestState.HttpRequest.Method="POST";
					httpRequestState.HttpRequest.ContentType="application/x-www-form-urlencoded";
					httpRequestState.HttpRequest.ContentLength=data.Length;

					// get request stream
					// TODO: async, but in our example we ain't posting files
					Stream stm = httpRequestState.HttpRequest.GetRequestStream();
					stm.Write(data,0,data.Length);
					stm.Flush();
					stm.Close();
				}
			
				// Get Response
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

		#region Sync methods
		/// <summary>
		/// Starts a new HTTP Post request.
		/// </summary>
		/// <param name="postUri"> The Url string.</param>
		/// <param name="properties"> The HttpProperties to set for the request.</param>
		/// <param name="values"> The data to post.</param>
		/// <returns> A ResponseBuffer type.</returns>
		public ResponseBuffer StartSyncHttpPost(string postUri,HttpProperties properties,ArrayList values)
		{
			HttpWebResponse response=null;
			ResponseBuffer respBuffer=new ResponseBuffer();
			httpRequestState = new HttpState();
			
			postUri = EncodeDecode.UrlDecode(postUri);
			postUri = EncodeDecode.HtmlDecode(postUri);

			// create webrequest
			try
			{
				//this.ValidateIPAddress(new Uri(postUri));

				httpRequestState.HttpRequest = (HttpWebRequest)WebRequest.Create(postUri);
				
				// Set HttpWebRequestProperties
				SetHttpWebRequestProperties(httpRequestState.HttpRequest, properties);

				// Apply proxy settings
				if ( this.ProxySettings != null )
				{
					SetProxy(httpRequestState.HttpRequest,this.ProxySettings);
				}
				
				//httpRequestState.httpRequest.Referer = postUri;

				// transform to postdata and encode in bytes
				byte[] data=null;
				if (values!=null)
				{
					// transform to postdata and encode in bytes
					string postData = GetPostData(values);
					data=Encoding.UTF8.GetBytes(postData);
				}

				// set properties
				httpRequestState.HttpRequest.Method="POST";
				httpRequestState.HttpRequest.ContentType="application/x-www-form-urlencoded";
				httpRequestState.HttpRequest.ContentLength=data.Length;

				// get request stream
				Stream stm = httpRequestState.HttpRequest.GetRequestStream();
				stm.Write(data,0,data.Length);
				stm.Close();

				//hwr.ContinueDelegate=getRedirectHeaders;
				HttpWebResponse httpResponse = (HttpWebResponse)httpRequestState.HttpRequest.GetResponse();
				httpRequestState.HttpResponse = httpResponse;
				
				// get ResponseBuffer
				respBuffer = HttpPipeline.FillResponseBuffer(httpResponse,
					httpRequestState.HttpRequest,
					properties, 
					httpRequestState);				
			}
			catch (ProtocolViolationException p)
			{
				if ( response!=null )
				{
					response.Close();
				}
				respBuffer.ErrorMessage = "Protocol Exception:" + p.Message;
				return respBuffer;
			}
			catch (WebException w)
			{
				StringBuilder s = new StringBuilder();
				s.Append("Error message:");
				s.Append(w.Message);
				s.Append("\r\nStatus Code:");
				s.Append(((HttpWebResponse)w.Response).StatusCode);
				s.Append("\r\nStatus Description:");
				s.Append(((HttpWebResponse)w.Response).StatusDescription);

				if ( response!=null )
				{
					response.Close();
				}
				respBuffer.ErrorMessage = s.ToString();
				return  respBuffer;
			}

			// response here
			return respBuffer;
		}

		/// <summary>
		/// Starts a new HTTP Post request.
		/// </summary>
		/// <param name="postUri"> The Url string.</param>
		/// <param name="properties"> The HttpProperties to set for the request.</param>
		/// <param name="values"> The data to post.</param>
		/// <param name="cookies"> The cookie data to set for the request.</param>
		/// <returns> A ResponseBuffer type.</returns>
		public ResponseBuffer StartSyncHttpPost(string postUri,HttpProperties properties,ArrayList values, CookieCollection cookies)
		{
			HttpWebResponse response=null;
			ResponseBuffer respBuffer=new ResponseBuffer();
			httpRequestState = new HttpState();

			postUri = EncodeDecode.UrlDecode(postUri);
			postUri = EncodeDecode.HtmlDecode(postUri);

			// create webrequest
			try
			{
				//this.ValidateIPAddress(new Uri(postUri));

				httpRequestState.HttpRequest = (HttpWebRequest)WebRequest.Create(postUri);
				
				// Set HttpWebRequestProperties
				SetHttpWebRequestProperties(httpRequestState.HttpRequest, properties);

				// Apply proxy settings
				if ( this.ProxySettings != null )
				{
					SetProxy(httpRequestState.HttpRequest,this.ProxySettings);
				}

				byte[] data=null;
				if (values!=null)
				{
					// transform to postdata and encode in bytes
					string postData = GetPostData(values);
					data=Encoding.UTF8.GetBytes(postData);
				}

				//httpRequestState.httpRequest.Referer = postUri;

				// set properties
				httpRequestState.HttpRequest.Method="POST";
				httpRequestState.HttpRequest.ContentType="application/x-www-form-urlencoded";
				httpRequestState.HttpRequest.ContentLength=data.Length;

				// get request stream
				Stream stm = httpRequestState.HttpRequest.GetRequestStream();
				stm.Write(data,0,data.Length);
				stm.Close();

				//hwr.ContinueDelegate=getRedirectHeaders;

				// save cookies
				httpRequestState.HttpRequest.CookieContainer=new CookieContainer();
				if ( cookies!=null )
				{
					httpRequestState.HttpRequest.CookieContainer.Add(cookies);
				}

				HttpWebResponse httpResponse = (HttpWebResponse)httpRequestState.HttpRequest.GetResponse();
				httpRequestState.HttpResponse = httpResponse;
				
				// get ResponseBuffer
				respBuffer = HttpPipeline.FillResponseBuffer(httpResponse,
					httpRequestState.HttpRequest,
					properties, 
					httpRequestState);
			}
			catch (ProtocolViolationException p)
			{
				if ( response!=null )
				{
					response.Close();
				}
				respBuffer.ErrorMessage = "Protocol Exception:" + p.Message;
				return respBuffer;
			}
			catch (WebException w)
			{
				StringBuilder s = new StringBuilder();
				s.Append("Error message:");
				s.Append(w.Message);
				s.Append("\r\nStatus Code:");
				s.Append(((HttpWebResponse)w.Response).StatusCode);
				s.Append("\r\nStatus Description:");
				s.Append(((HttpWebResponse)w.Response).StatusDescription);

				if ( response!=null )
				{
					response.Close();
				}
				respBuffer.ErrorMessage = s.ToString();
				return  respBuffer;
			}

			// response here
			return respBuffer;
		}

		#endregion

		#region Utils
		/// <summary>
		/// Creates the post data as a string.
		/// </summary>
		/// <param name="al"> The post data parameters.</param>
		/// <returns> A string containing the post data.</returns>
		public string GetPostData(ArrayList al)
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

			return buffer.ToString();
		}

		/// <summary>
		/// Creates the post data as a string.
		/// </summary>
		/// <param name="values"> The post data parameters.</param>
		/// <returns> A string containing the post data.</returns>
		public string GetPostData(Hashtable values)
		{
			StringBuilder buffer = new StringBuilder();

			int i=0;
			foreach (DictionaryEntry de in values)
			{
				if (i>0)
				{
					buffer.Append("&");
				}
				buffer.AppendFormat("{0}={1}",de.Key,(string)de.Value);
				i++;
			}

			return buffer.ToString();
		}
		#endregion
	}
}
