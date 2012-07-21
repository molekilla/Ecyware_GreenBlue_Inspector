using System;
using System.Net;
using System.Threading;
using System.IO;
using System.Text;
using System.Security.Permissions;
using Microsoft.Win32;
using System.Collections;

using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Engine.HtmlCommand;

namespace Ecyware.GreenBlue.Engine.Scripting
{
	public delegate void ScriptingRequestClientResultHandler(object sender, ScriptingResponseArgs e);

	/// <summary>
	/// Contains the arguments for the internal scripting response.
	/// </summary>
	public class ScriptingResponseArgs : EventArgs
	{
		private HttpRequestResponseContext _context;

		/// <summary>
		/// Creates a new ScriptingResponseArgs.
		/// </summary>
		public ScriptingResponseArgs()
		{
		}

		/// <summary>
		/// Gets or sets the context.
		/// </summary>
		public HttpRequestResponseContext Context
		{
			get
			{
				return _context;
			}
			set
			{
				_context = value;
			}
		}
	}
	/// <summary>
	/// Summary description for ScriptingGetRequestClient.
	/// </summary>
	public class ScriptingRequestClient : BaseHttpForm
	{		
		HtmlParser parser = new HtmlParser();

		/// <summary>
		/// EndHttp ResponseCallbackDelegate
		/// </summary>
		public event ScriptingRequestClientResultHandler ScriptingRequestClientResult;

		/// <summary>
		/// Creates a new ScriptingRequestClient.
		/// </summary>
		public ScriptingRequestClient() : base()
		{
			SetServicePointManagerDefaults();
			//this.Timeout = 10*1000; // 10 seconds default timeout for posts.
		}

		/// <summary>
		/// Event call from Worker Process, sends ReponseEventArgs to ResponseCallbackDelegate on callee.
		/// </summary>
		/// <param name="command"> The IPipelineCommand.</param>
		private void RequestCallback(IPipelineCommand command)
		{
			ScriptingResponseArgs args = new ScriptingResponseArgs();			
			args.Context = ((ScriptingHttpState)command.HttpStateData).HttpRequestResponseContext;

			if ( args.Context.Request.WebResponse.ErrorMessage.Length == 0 )
			{
				// Saves Cookies
				_cookieManager.AddCookies(command.HttpResponseData.CookieCollection);
			}

			// Return response in a event
			ScriptingRequestClientResult(this, args);
		}

		/// <summary>
		/// Callback method that sends the request to the WorkerProcess for further processing.
		/// It handles each request sequencially without blocking the asynchronous request.
		/// </summary>
		/// <param name="ar"> The IAsyncResult from async request.</param>
		protected void SessionScriptingCallback(IAsyncResult ar)
		{
			// Get HttpScriptingState
			ScriptingHttpState httpState = (ScriptingHttpState)ar.AsyncState;
			
			// new InspectorPipelineCommand
			SessionScriptingPipelineCommand scriptingPipeline = 
				new SessionScriptingPipelineCommand(httpState,				
				ProxySettings,				
				new PipelineCommandResultEventHandler(RequestCallback)
				);
			
			try
			{				
				HttpWebResponse response = (HttpWebResponse)httpState.HttpRequest.EndGetResponse(ar);
				
				scriptingPipeline.HttpStateData.HttpResponse = response;
			}
			catch (WebException wex)
			{
				// Handle web exceptions
				if ( wex.Status == WebExceptionStatus.ProtocolError )
				{
					scriptingPipeline.HttpStateData.HttpResponse = (HttpWebResponse)wex.Response;
				} 
				else 
				{
					//ExceptionHandler.RegisterException(wex);
					scriptingPipeline.ErrorMessage = wex.Message;
				}
			}
			catch (Exception ex)
			{
				// Handle unknown exceptions
				//ExceptionHandler.RegisterException(ex);
				scriptingPipeline.ErrorMessage = ex.Message;				
			}

			// enter lock and signaled event			
			Monitor.Enter( WorkerProcess.ReceiveList );			
			
			WorkerProcess.ReceiveList.Add(scriptingPipeline);			
			WorkerProcess.ReceiveEvent.Set();

			Monitor.Exit( WorkerProcess.ReceiveList );			
		}
		/// <summary>
		/// Updates the url to navigate for a GET request.
		/// </summary>
		/// <param name="formTag"> The HtmlFormTag type.</param>
		/// <param name="url"> The Url as a string.</param>
		/// <param name="multipartPost"> Is a multipart/form-data post. This flags skips the input=file.</param>
		/// <returns> The current or updated url.</returns>
		private string UpdateUrl(HtmlFormTag formTag, string url, bool multipartPost)
		{			
			if ( multipartPost )
			{
				foreach ( HtmlTagBaseList tagBaseList in formTag )
				{
					for (int i=0;i<tagBaseList.Count;i++)
					{
						if ( tagBaseList[i] is HtmlInputTag )
						{
							HtmlInputTag input = (HtmlInputTag)tagBaseList[i];
							if ( input.Type == HtmlInputType.File )
							{
								tagBaseList.RemoveAt(i);
							}
						}
					}
				}
			}

			string urlToNavigate = url;
			if ( formTag != null )
			{
				ArrayList values = parser.GetArrayList(formTag);
				if ( (new Uri(url)).Query.Length == 0 )
				{
					urlToNavigate = GetForm.AppendToUri(url, values);
				}
			}

			return urlToNavigate.TrimEnd('?');
		}
		/// <summary>
		/// Begins a new asynchronous HTTP Post request. This function is not thread safe.
		/// </summary>
		/// <param name="context"> The HttpRequestResponseContext type.</param>
		public void ExecutePostWebRequestFileUpload(HttpRequestResponseContext context)
		{
			PostWebRequest postWebRequest = (PostWebRequest)context.Request;

			HtmlFormTag formTag = postWebRequest.Form.WriteHtmlFormTag();
			UploadFileInfo[] fileInfoItems = UploadFileInfo.GetUploadFiles(formTag);

			HtmlParser parser = new HtmlParser();
			ScriptingHttpState httpRequestState = new ScriptingHttpState();
			httpRequestState.HttpRequestResponseContext = context;

			string uriString = context.Request.Url;

			if ( context.DecodeUrl )
			{
				uriString = EncodeDecode.UrlDecode(uriString);				
			}

			bool isException = false;
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
				IAsyncResult ar = httpRequestState.HttpRequest.BeginGetResponse(new AsyncCallback(SessionScriptingCallback),httpRequestState);

				// register a timeout
				ThreadPool.RegisterWaitForSingleObject(ar.AsyncWaitHandle, new WaitOrTimerCallback(BaseHttpForm.RequestTimeoutCallback), httpRequestState, this.GetTimeout(), true);

			}
			catch
			{
				isException = true;
				throw;
			}
			finally 
			{
				if (isException)
				{
					if (httpRequestState.HttpResponse != null)
					{
						httpRequestState.HttpResponse.Close();
					}
				}
			}

		}
		//		public static string UploadFileEx( string uploadfile, string url, 
		//			string fileFormName, string contenttype,NameValueCollection querystring, 
		//			CookieContainer cookies)
		//		{
		//			if( (fileFormName== null) ||
		//				(fileFormName.Length ==0))
		//			{
		//				fileFormName = "file";
		//			}
		//
		//			if( (contenttype== null) ||
		//				(contenttype.Length ==0))
		//			{
		//				contenttype = "application/octet-stream";
		//			}
		//
		//
		//			string postdata;
		//			postdata = "?";
		//			if (querystring!=null)
		//			{
		//				foreach(string key in querystring.Keys)
		//				{
		//					postdata+= key +"=" + querystring.Get(key)+"&";
		//				}
		//			}
		//			Uri uri = new Uri(url+postdata);
		//
		//
		//			string boundary = "----------" + DateTime.Now.Ticks.ToString("x");
		//			HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(uri);
		//			webrequest.CookieContainer = cookies;
		//			webrequest.ContentType = "multipart/form-data; boundary=" + boundary;
		//			webrequest.Method = "POST";
		//
		//
		//			// Build up the post message header
		//			StringBuilder sb = new StringBuilder();
		//			sb.Append("--");
		//			sb.Append(boundary);
		//			sb.Append("\r\n");
		//			sb.Append("Content-Disposition: form-data; name=\"");
		//			sb.Append(fileFormName);
		//			sb.Append("\"; filename=\"");
		//			sb.Append(Path.GetFileName(uploadfile));
		//			sb.Append("\"");
		//			sb.Append("\r\n");
		//			sb.Append("Content-Type: ");
		//			sb.Append(contenttype);
		//			sb.Append("\r\n");
		//			sb.Append("\r\n");            
		//
		//			string postHeader = sb.ToString();
		//			byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);
		//
		//			// Build the trailing boundary string as a byte array
		//			// ensuring the boundary appears on a line by itself
		//			byte[] boundaryBytes = 
		//				Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
		//
		//			FileStream fileStream = new FileStream(uploadfile, 
		//				FileMode.Open, FileAccess.Read);
		//			long length = postHeaderBytes.Length + fileStream.Length + 
		//				boundaryBytes.Length;
		//			webrequest.ContentLength = length;
		//
		//			Stream requestStream = webrequest.GetRequestStream();
		//
		//			// Write out our post header
		//			requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
		//
		//			// Write out the file contents
		//			byte[] buffer = new Byte[checked((uint)Math.Min(4096, 
		//				(int)fileStream.Length))];
		//			int bytesRead = 0;
		//			while ( (bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0 )
		//				requestStream.Write(buffer, 0, bytesRead);
		//
		//			// Write out the trailing boundary
		//			requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
		//			WebResponse responce = webrequest.GetResponse();
		//			Stream s = responce.GetResponseStream();
		//			StreamReader sr = new StreamReader(s);
		//
		//			return sr.ReadToEnd();
		//		}

		/// <summary>
		/// Creates a sync ResponseBuffer.
		/// </summary>
		/// <param name="state"> The ScriptingHttpState type.</param>
		/// <returns> A ResponseBuffer.</returns>
		private ResponseBuffer CreateSyncResponse(ScriptingHttpState state)
		{
			HttpWebResponse r = (HttpWebResponse)state.HttpRequest.GetResponse();
			state.HttpResponse = r;
			Scripting.SessionScriptingPipelineCommand pipelineCommand = new SessionScriptingPipelineCommand(state, this.ProxySettings, null);
			pipelineCommand.ExecuteCommand();

			if (pipelineCommand.ErrorMessage.Length > 0)
			{
				return pipelineCommand.HttpResponseData;
			}
			else
			{
				return null;
			}
		}
		#region For Session Scripting
		/// <summary>
		/// Begins a new  HTTP Post request. This function is not thread safe.
		/// </summary>
		/// <param name="context"> The HttpRequestResponseContext type.</param>
		public ResponseBuffer ExecutePostWebRequestSync(HttpRequestResponseContext context)
		{
			ResponseBuffer result = null;
			HtmlParser parser = new HtmlParser();
			ArrayList values = parser.GetArrayList(context.Request.Form.WriteHtmlFormTag());

			ScriptingHttpState httpRequestState = new ScriptingHttpState();
			httpRequestState.HttpRequestResponseContext = context;

			string uriString = context.Request.Url;

			if (context.DecodeUrl)
			{
				uriString = EncodeDecode.UrlDecode(uriString);
			}

			bool isException = false;

			// create webrequest
			try
			{
				//this.ValidateIPAddress(new Uri(uriString));

				httpRequestState.HttpRequest = (HttpWebRequest)System.Net.WebRequest.Create(uriString);

				// Set HttpWebRequestProperties
				SetHttpWebRequestProperties(httpRequestState.HttpRequest, context.Request.RequestHttpSettings);

				// Apply proxy settings
				if (this.ProxySettings != null)
				{
					SetProxy(httpRequestState.HttpRequest, this.ProxySettings);
				}

				//httpRequestState.httpRequest.Referer = postUri;

				// Continue headers
				//hwr.ContinueDelegate = getRedirectHeaders;

				// Save cookies
				httpRequestState.HttpRequest.CookieContainer = new CookieContainer();
				if (context.Request.Cookies != null)
				{
					httpRequestState.HttpRequest.CookieContainer.Add(context.Request.GetCookieCollection());
				}

				byte[] data = null;
				if (values != null)
				{
					PostWebRequest postWebRequest = (PostWebRequest)context.Request;
					string postData = string.Empty;

					if (postWebRequest.UsePostData)
					{
						postData = postWebRequest.PostData;
						httpRequestState.HttpRequest.ContentType = context.Request.RequestHttpSettings.ContentType;
					}
					else
					{
						// transform to postdata and encode in bytes
						postData = GetPostData(values);
						httpRequestState.HttpRequest.ContentType = "application/x-www-form-urlencoded";
					}

					data = Encoding.UTF8.GetBytes(postData);

					// set properties
					//httpRequestState.HttpRequest.AllowWriteStreamBuffering = false;
					httpRequestState.HttpRequest.KeepAlive = true;
					//httpRequestState.HttpRequest.Timeout = 120000;
					httpRequestState.HttpRequest.Method = "POST";
					httpRequestState.HttpRequest.ContentLength = data.Length;

					// get request stream
					Stream stm = httpRequestState.HttpRequest.GetRequestStream();
					stm.Write(data, 0, data.Length);
					stm.Flush();
					stm.Close();
				}

				result = CreateSyncResponse(httpRequestState);

				// Get Response
				// IAsyncResult ar = httpRequestState.HttpRequest.BeginGetResponse(new AsyncCallback(SessionScriptingCallback), httpRequestState);

				// register a timeout
				// ThreadPool.RegisterWaitForSingleObject(ar.AsyncWaitHandle, new WaitOrTimerCallback(BaseHttpForm.RequestTimeoutCallback), httpRequestState, this.GetTimeout(), true);

			}
			catch
			{
				isException = true;
				throw;
			}
			finally
			{
				if (isException)
				{
					if (httpRequestState.HttpResponse != null)
					{
						httpRequestState.HttpResponse.Close();
					}
				}
			}

			return result;
		}
		/// <summary>
		/// Begins a new asynchronous HTTP Post request. This function is not thread safe.
		/// </summary>
		/// <param name="context"> The HttpRequestResponseContext type.</param>
		public void ExecutePostWebRequest(HttpRequestResponseContext context)
		{
			HtmlParser parser = new HtmlParser();
			ArrayList values = parser.GetArrayList(context.Request.Form.WriteHtmlFormTag());

			ScriptingHttpState httpRequestState = new ScriptingHttpState();
			httpRequestState.HttpRequestResponseContext = context;

			string uriString = context.Request.Url;

			if ( context.DecodeUrl )
			{
				uriString = EncodeDecode.UrlDecode(uriString);				
			}

			bool isException = false;

			// create webrequest
			try
			{
				//this.ValidateIPAddress(new Uri(uriString));

				httpRequestState.HttpRequest = (HttpWebRequest)System.Net.WebRequest.Create(uriString);

				// Set HttpWebRequestProperties
				SetHttpWebRequestProperties(httpRequestState.HttpRequest, context.Request.RequestHttpSettings);

				// Apply proxy settings
				if ( this.ProxySettings != null )
				{
					SetProxy(httpRequestState.HttpRequest,this.ProxySettings);
				}
				
				//httpRequestState.httpRequest.Referer = postUri;

				// Continue headers
				//hwr.ContinueDelegate = getRedirectHeaders;
				
				// Save cookies
				httpRequestState.HttpRequest.CookieContainer = new CookieContainer();
				if ( context.Request.Cookies != null )
				{										
					httpRequestState.HttpRequest.CookieContainer.Add(context.Request.GetCookieCollection());
				}

				byte[] data=null;
				if (values!=null)
				{
					PostWebRequest postWebRequest = (PostWebRequest)context.Request;
					string postData = string.Empty;

					if ( postWebRequest.UsePostData )
					{
						postData = postWebRequest.PostData;
						httpRequestState.HttpRequest.ContentType = context.Request.RequestHttpSettings.ContentType;
					} 
					else 
					{
						// transform to postdata and encode in bytes
						postData = GetPostData(values);
						httpRequestState.HttpRequest.ContentType = "application/x-www-form-urlencoded";
					}

					data = Encoding.UTF8.GetBytes(postData);

					// set properties
					//httpRequestState.HttpRequest.AllowWriteStreamBuffering = false;
					httpRequestState.HttpRequest.KeepAlive = true;
					//httpRequestState.HttpRequest.Timeout = 120000;
					httpRequestState.HttpRequest.Method = "POST";					
					httpRequestState.HttpRequest.ContentLength = data.Length;

					// get request stream
					Stream stm = httpRequestState.HttpRequest.GetRequestStream();
					stm.Write(data,0,data.Length);
					stm.Flush();
					stm.Close();			
				}

				// Get Response
				IAsyncResult ar = httpRequestState.HttpRequest.BeginGetResponse(new AsyncCallback(SessionScriptingCallback),httpRequestState);

				// register a timeout
				ThreadPool.RegisterWaitForSingleObject(ar.AsyncWaitHandle, new WaitOrTimerCallback(BaseHttpForm.RequestTimeoutCallback), httpRequestState, this.GetTimeout(), true);

			}
			catch
			{
				isException = true;
				throw;
			}
			finally 
			{
				if (isException)
				{
					if (httpRequestState.HttpResponse != null)
					{
						httpRequestState.HttpResponse.Close();
					}
				}
			}
		}

		/// <summary>
		/// Begins a new asynchronous SOAP HTTP request. This function is not thread safe.
		/// </summary>
		/// <param name="context"> The HttpRequestResponseContext type.</param>
		public void ExecuteSoapHttpWebRequest(HttpRequestResponseContext context)
		{
			HtmlParser parser = new HtmlParser();
			ScriptingHttpState httpRequestState = new ScriptingHttpState();
			httpRequestState.HttpRequestResponseContext = context;

			string uriString = context.Request.Url;

			if ( context.DecodeUrl )
			{
				uriString = EncodeDecode.UrlDecode(uriString);				
			}

			bool isException = false;
			// create webrequest
			try
			{
				//this.ValidateIPAddress(new Uri(uriString));
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

				byte[] data=null;
				SoapHttpWebRequest soapWebRequest = (SoapHttpWebRequest)context.Request;
				if ( soapWebRequest.XmlEnvelope != null )
				{
					data = Encoding.UTF8.GetBytes(soapWebRequest.XmlEnvelope.OuterXml);

					// set properties
					//httpRequestState.HttpRequest.KeepAlive = true;
					//httpRequestState.HttpRequest.Timeout = 10000;
					httpRequestState.HttpRequest.Method = "POST";
					httpRequestState.HttpRequest.ContentType = context.Request.RequestHttpSettings.ContentType;
					httpRequestState.HttpRequest.ContentLength = data.Length;

					// get request stream
					Stream stm = httpRequestState.HttpRequest.GetRequestStream();
					stm.Write(data,0,data.Length);
					stm.Flush();
					stm.Close();			
				}

				// Get Response
				IAsyncResult ar = httpRequestState.HttpRequest.BeginGetResponse(new AsyncCallback(SessionScriptingCallback),httpRequestState);

				// register a timeout
				ThreadPool.RegisterWaitForSingleObject(ar.AsyncWaitHandle, new WaitOrTimerCallback(BaseHttpForm.RequestTimeoutCallback), httpRequestState, this.GetTimeout(), true);

			}
			catch
			{
				isException = true;
				throw;
			}
			finally 
			{
				if (isException)
				{
					if (httpRequestState.HttpResponse != null)
					{
						httpRequestState.HttpResponse.Close();
					}
				}
			}
		}

		/// <summary>
		/// Begins a new asynchronous HTTP Put request. This function is not thread safe.
		/// </summary>
		/// <param name="context"> The HttpRequestResponseContext type.</param>
		public void ExecutePutWebRequest(HttpRequestResponseContext context)
		{
			HtmlParser parser = new HtmlParser();
			//ArrayList values = parser.GetArrayList(context.Request.Form.WriteHtmlFormTag());

			ScriptingHttpState httpRequestState = new ScriptingHttpState();
			httpRequestState.HttpRequestResponseContext = context;

			string uriString = context.Request.Url;

			if ( context.DecodeUrl )
			{
				uriString = EncodeDecode.UrlDecode(uriString);				
			}

			bool isException = false;
			// create webrequest
			try
			{
				//this.ValidateIPAddress(new Uri(uriString));

				httpRequestState.HttpRequest = (HttpWebRequest)System.Net.WebRequest.Create(uriString);

				// Set HttpWebRequestProperties
				SetHttpWebRequestProperties(httpRequestState.HttpRequest, context.Request.RequestHttpSettings);

				// Apply proxy settings
				if ( this.ProxySettings != null )
				{
					SetProxy(httpRequestState.HttpRequest,this.ProxySettings);
				}
				
				//httpRequestState.httpRequest.Referer = postUri;

				// Continue headers
				//hwr.ContinueDelegate = getRedirectHeaders;
				
				// Save cookies
				httpRequestState.HttpRequest.CookieContainer = new CookieContainer();
				if ( context.Request.Cookies != null )
				{										
					httpRequestState.HttpRequest.CookieContainer.Add(context.Request.GetCookieCollection());
				}

				byte[] data=null;
				PutWebRequest putWebRequest = (PutWebRequest)context.Request;
				if ( putWebRequest.PostData.Length > 0 )
				{
					data = Encoding.UTF8.GetBytes(putWebRequest.PostData);

					// set properties
					//httpRequestState.HttpRequest.AllowWriteStreamBuffering = false;
					//httpRequestState.HttpRequest.KeepAlive = true;
					//httpRequestState.HttpRequest.Timeout = 5000;
					httpRequestState.HttpRequest.Method = "PUT";
					httpRequestState.HttpRequest.ContentType = context.Request.RequestHttpSettings.ContentType;
					httpRequestState.HttpRequest.ContentLength = data.Length;

					// get request stream
					Stream stm = httpRequestState.HttpRequest.GetRequestStream();
					stm.Write(data,0,data.Length);
					stm.Flush();
					stm.Close();			
				}

				// Get Response
				IAsyncResult ar = httpRequestState.HttpRequest.BeginGetResponse(new AsyncCallback(SessionScriptingCallback),httpRequestState);

				// register a timeout
				ThreadPool.RegisterWaitForSingleObject(ar.AsyncWaitHandle, new WaitOrTimerCallback(BaseHttpForm.RequestTimeoutCallback), httpRequestState, this.GetTimeout(), true);

			}
			catch
			{
				isException = true;
				throw;
			}
			finally 
			{
				if (isException)
				{
					if (httpRequestState.HttpResponse != null)
					{
						httpRequestState.HttpResponse.Close();
					}
				}
			}
		}
		
		/// <summary>
		/// Gets the MIME Type of the file.
		/// </summary>
		/// <param name="filePath"> The file path.</param>
		/// <returns> Returns a string representing the MIME type.</returns>
		//		public string GetMIMEType(string filePath)
		//		{
		//			RegistryPermission regPerm = new RegistryPermission(RegistryPermissionAccess.Read,"\\\\HKEY_CLASSES_ROOT");
		//			RegistryKey classesRoot = Registry.ClassesRoot;
		//			FileInfo fi = new FileInfo(filePath);
		//			string dotExt = fi.Extension.ToUpper();
		//			RegistryKey typeKey = classesRoot.OpenSubKey("MIME\\Database\\Content Type");			
		//		
		//			string result = "application/octet-stream";
		//
		//			foreach ( string keyname in typeKey.GetSubKeyNames() )
		//			{
		//				RegistryKey curKey = classesRoot.OpenSubKey("MIME\\Database\\Content Type\\" + keyname);
		//							
		//				if ( Convert.ToString(curKey.GetValue("Extension")).ToUpper() == dotExt )
		//				{
		//					result = keyname;
		//				}
		//			}
		//
		//			return result;
		//		}
		/// <summary>
		/// Begins a new HTTP Get request. This function is not thread safe.
		/// </summary>
		/// <param name="context"> The HttpRequestResponseContext context.</param>
		public ResponseBuffer ExecuteGetWebRequestSync(HttpRequestResponseContext context)
		{
			// string uri = context.Request.Url;
			ResponseBuffer result = null;
			ScriptingHttpState httpRequestState = new ScriptingHttpState();

			// Update Url if form exists			
			context.Request.Url = UpdateUrl(context.Request.Form.WriteHtmlFormTag(), context.Request.Url, false);

			string uri = context.Request.Url;
			httpRequestState.HttpRequestResponseContext = context;

			if (context.DecodeUrl)
			{
				uri = EncodeDecode.UrlDecode(uri);
			}

			bool isException = false;
			try
			{
				//this.ValidateIPAddress(new Uri(uri));

				httpRequestState.HttpRequest = (HttpWebRequest)System.Net.WebRequest.Create(uri);

				// Set HttpWebRequestProperties
				SetHttpWebRequestProperties(httpRequestState.HttpRequest, context.Request.RequestHttpSettings);

				// Apply proxy settings
				if (ProxySettings != null)
				{
					SetProxy(httpRequestState.HttpRequest, ProxySettings);
				}

				// Continue headers
				//hwr.ContinueDelegate=getRedirectHeaders;

				// Save cookies
				httpRequestState.HttpRequest.CookieContainer = new CookieContainer();
				if (context.Request.Cookies != null)
				{
					httpRequestState.HttpRequest.CookieContainer.Add(context.Request.GetCookieCollection());
				}

				result = CreateSyncResponse(httpRequestState);

				// Get Response
				// IAsyncResult ar = httpRequestState.HttpRequest.BeginGetResponse(new AsyncCallback(SessionScriptingCallback), httpRequestState);

				// register a timeout
				// ThreadPool.RegisterWaitForSingleObject(ar.AsyncWaitHandle, new WaitOrTimerCallback(BaseHttpForm.RequestTimeoutCallback), httpRequestState, this.GetTimeout(), true);

			}
			catch
			{
				isException = true;
				throw;
			}
			finally
			{
				if (isException)
				{
					if (httpRequestState.HttpResponse != null)
					{
						httpRequestState.HttpResponse.Close();
					}
				}
			}

			return result;
		}

		/// <summary>
		/// Begins a new asynchronous HTTP Get request. This function is not thread safe.
		/// </summary>
		/// <param name="context"> The HttpRequestResponseContext context.</param>
		public void ExecuteGetWebRequest(HttpRequestResponseContext context)
		{
			// string uri = context.Request.Url;

			ScriptingHttpState httpRequestState = new ScriptingHttpState();			

			// Update Url if form exists			
			context.Request.Url = UpdateUrl(context.Request.Form.WriteHtmlFormTag(), context.Request.Url, false);

			string uri = context.Request.Url;
			httpRequestState.HttpRequestResponseContext = context;

			if ( context.DecodeUrl )
			{
				uri = EncodeDecode.UrlDecode(uri);				
			}

			bool isException = false;
			try
			{
				//this.ValidateIPAddress(new Uri(uri));

				httpRequestState.HttpRequest = (HttpWebRequest)System.Net.WebRequest.Create(uri);

				// Set HttpWebRequestProperties
				SetHttpWebRequestProperties(httpRequestState.HttpRequest, context.Request.RequestHttpSettings);
				
				// Apply proxy settings
				if ( ProxySettings != null )
				{
					SetProxy(httpRequestState.HttpRequest, ProxySettings);
				}

				// Continue headers
				//hwr.ContinueDelegate=getRedirectHeaders;

				// Save cookies
				httpRequestState.HttpRequest.CookieContainer = new CookieContainer();
				if ( context.Request.Cookies != null )
				{										
					httpRequestState.HttpRequest.CookieContainer.Add(context.Request.GetCookieCollection());
				}

				// Begin requesting...
				IAsyncResult ar = httpRequestState.HttpRequest.BeginGetResponse(new AsyncCallback(SessionScriptingCallback),httpRequestState);

				// register a timeout
				ThreadPool.RegisterWaitForSingleObject(ar.AsyncWaitHandle, new WaitOrTimerCallback(BaseHttpForm.RequestTimeoutCallback), httpRequestState, this.GetTimeout(), true);
			}
			catch
			{
				isException = true;
				throw;
			}
			finally 
			{
				if (isException)
				{
					if (httpRequestState.HttpResponse != null)
					{
						httpRequestState.HttpResponse.Close();
					}
				}
			}

		}


		/// <summary>
		/// Begins a new asynchronous HTTP Delete request. This function is not thread safe.
		/// </summary>
		/// <param name="context"> The HttpRequestResponseContext context.</param>
		public void ExecuteDeleteWebRequest(HttpRequestResponseContext context)
		{
			// string uri = context.Request.Url;

			ScriptingHttpState httpRequestState = new ScriptingHttpState();			

			// Update Url if form exists			
			context.Request.Url = UpdateUrl(context.Request.Form.WriteHtmlFormTag(), context.Request.Url, false);

			string uri = context.Request.Url;
			httpRequestState.HttpRequestResponseContext = context;

			if ( context.DecodeUrl )
			{
				uri = EncodeDecode.UrlDecode(uri);				
			}

			bool isException = false;
			try
			{
				//this.ValidateIPAddress(new Uri(uri));

				httpRequestState.HttpRequest = (HttpWebRequest)System.Net.WebRequest.Create(uri);

				// Set HttpWebRequestProperties
				SetHttpWebRequestProperties(httpRequestState.HttpRequest, context.Request.RequestHttpSettings);
				
				// Apply proxy settings
				if ( ProxySettings != null )
				{
					SetProxy(httpRequestState.HttpRequest, ProxySettings);
				}

				// Continue headers
				//hwr.ContinueDelegate=getRedirectHeaders;

				// Save cookies
				httpRequestState.HttpRequest.CookieContainer = new CookieContainer();
				if ( context.Request.Cookies != null )
				{										
					httpRequestState.HttpRequest.CookieContainer.Add(context.Request.GetCookieCollection());
				}

				// Begin requesting...
				IAsyncResult ar = httpRequestState.HttpRequest.BeginGetResponse(new AsyncCallback(SessionScriptingCallback),httpRequestState);

				// register a timeout
				ThreadPool.RegisterWaitForSingleObject(ar.AsyncWaitHandle, new WaitOrTimerCallback(BaseHttpForm.RequestTimeoutCallback), httpRequestState, this.GetTimeout(), true);
			}
			catch
			{
				isException = true;
				throw;
			}
			finally 
			{
				if (isException)
				{
					if (httpRequestState.HttpResponse != null)
					{
						httpRequestState.HttpResponse.Close();
					}
				}
			}

		}

		#endregion

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
	}
}
