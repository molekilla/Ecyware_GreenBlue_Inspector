// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: November 2003
using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections;
using System.Text;
using System.Threading;
//using Ecyware.LicenseType.IPLimit;
using Ecyware.GreenBlue.Engine.HtmlCommand;

namespace Ecyware.GreenBlue.Engine
{

	/// <summary>
	/// Callback for session abortion.
	/// </summary>
	public delegate void SessionAbortEventHandler(object sender, SessionAbortEventArgs e);

	/// <summary>
	/// Displays the process execution.
	/// </summary>
	public delegate void DisplayProcessEventHandler(object sender, EventArgs e);

	/// <summary>
	/// Delegate that returns a ResponseBuffer type.
	/// </summary>
	public delegate void ResponseCallbackDelegate(object sender,ResponseEventArgs e);

	/// <summary>
	/// Delegate that handles the packet state data returned from worker process.
	/// </summary>
	internal delegate void ReturnPacket(PacketStateData packet);

	internal delegate void PipelineCommandResultEventHandler(IPipelineCommand command);

	/// <summary>
	/// BaseHttpForm is the base class for all HTTP related types.
	/// </summary>
	public class BaseHttpForm
	{
		// Continue Delegate
		protected HttpContinueDelegate getRedirectHeaders;
		
		string _continueHeader;
		int _timeout = 20*1000; // 20 seconds default timeout
		bool _parsingEnabled = false;
		private HttpProxy _proxySettings = null;
		private HttpProperties _properties = null;

		/// <summary>
		/// EndHttp ResponseCallbackDelegate
		/// </summary>
		public event ResponseCallbackDelegate EndHttp;

		// CookieManager
		protected static CookieManager _cookieManager = new CookieManager();

		// current http state
		protected HttpState httpRequestState;

		/// <summary>
		/// Creates a new BaseHttpForm.
		/// </summary>
		public BaseHttpForm()
		{
			// Starts a WorkerProcess if not started
			if ( WorkerProcess.StartWorkerProcess() )
			{
				// Start Worker Process
				//WorkerProcess.ReturnPacketEvent += new ReturnPacket(WorkerProcess_ReturnPacketEvent);
				WorkerProcess.PipelineCommandEvent +=new PipelineCommandResultEventHandler(WorkerProcess_PipelineCommandResultEvent);
			}
		}


		/// <summary>
		/// Resets the cookie manager.
		/// </summary>
		public static void ResetCookieManager()
		{
			_cookieManager = new CookieManager();
		}
		
		#region Not Pipeline Enabled Callback Code
		/// <summary>
		/// Callback method that returns the PacketStateData from WorkerProcess and dynamically invokes the delegate associated with the state.
		/// </summary>
		/// <param name="data"> The PacketStateData.</param>
		private void WorkerProcess_ReturnPacketEvent(PacketStateData data)
		{
			PacketStateData d = data;
			Delegate method = d.CallerMethod;
			object[] a = {data};

			try
			{
				method.DynamicInvoke(a);
			}
			catch ( Exception ex)
			{
				ExceptionHandler.RegisterException(ex);
			}
		}

		/// <summary>
		/// Event call from Worker Process, sends ReponseEventArgs to ResponseCallbackDelegate on callee.
		/// </summary>
		/// <param name="packet"> The PacketStateData.</param>
		private void ReturnResponseBuffer(PacketStateData packet)
		{
			//respBuffer.ContinueHeader = base.ContinueHeader;
			ResponseEventArgs args = new ResponseEventArgs();
			args.Response = packet.ResponseData;
			args.State = packet.HttpStateData;
			// args.TestItem = packet.HttpStateData.TestItem;
			// args.IsSessionLastItem = packet.HttpStateData.IsLastItem;

			// return response in a event
			EndHttp(this, args);
		}

		/// <summary>
		/// Callback method that sends the request to the WorkerProcess for further processing.
		/// It handles each request sequencially without blocking the asynchronous request.
		/// </summary>
		/// <param name="ar"> The IAsyncResult from async request.</param>
		protected void FastResponseCallback2(IAsyncResult ar)
		{
			HttpState httpState = (HttpState)ar.AsyncState;
			PacketStateData packet =  new PacketStateData();
			
			try
			{				
				HttpWebResponse response = (HttpWebResponse)httpState.HttpRequest.EndGetResponse(ar);
				
				// Save response , httpState in packet
				packet.HttpStateData = httpState;
				packet.WebResponse = response;
				packet.CallerMethod = new ReturnPacket(ReturnResponseBuffer);
				packet.ClientSettings = this.ClientSettings;
				packet.ProxySettings = this.ProxySettings;

			}
			catch (WebException wex)
			{
				if ( wex.Status == WebExceptionStatus.ProtocolError )
				{
					packet.HttpStateData = httpState;
					packet.WebResponse = (HttpWebResponse)wex.Response;
					packet.CallerMethod = new ReturnPacket(ReturnResponseBuffer);
					packet.ClientSettings = this.ClientSettings;
					packet.ProxySettings = this.ProxySettings;
				} 
				else 
				{
					ExceptionHandler.RegisterException(wex);
					packet.HttpStateData = httpState;
					packet.WebResponse = null;
					packet.ErrorMessage = wex.Message;
					packet.CallerMethod = new ReturnPacket(ReturnResponseBuffer);
					packet.ClientSettings = this.ClientSettings;
					packet.ProxySettings = this.ProxySettings;
				}
			}
			catch (Exception ex)
			{
				ExceptionHandler.RegisterException(ex);
				packet.HttpStateData = httpState;
				packet.WebResponse = null;
				packet.ErrorMessage = ex.Message;
				packet.CallerMethod = new ReturnPacket(ReturnResponseBuffer);
				packet.ClientSettings = this.ClientSettings;
				packet.ProxySettings = this.ProxySettings;
			}

			// enter lock and signaled event
			Monitor.Enter( WorkerProcess.ReceiveList );
			
			WorkerProcess.ReceiveList.Add(packet);
			WorkerProcess.ReceiveEvent.Set();

			Monitor.Exit( WorkerProcess.ReceiveList );

		}

		#endregion
		#region Pipeline Enabled Code Callback Code
		/// <summary>
		/// Callback method that returns the IPipelineCommand from WorkerProcess and dynamically invokes the delegate associated with the state.
		/// </summary>
		/// <param name="command"> The IPipelineCommand.</param>
		private void WorkerProcess_PipelineCommandResultEvent(IPipelineCommand command)
		{			
			Delegate method = command.CallbackMethod;
			object[] a = {command};

			try
			{
				method.DynamicInvoke(a);
			}
			catch ( Exception ex)
			{
				ExceptionHandler.RegisterException(ex);
			}
		}
		/// <summary>
		/// Event call from Worker Process, sends ReponseEventArgs to ResponseCallbackDelegate on callee.
		/// </summary>
		/// <param name="command"> The IPipelineCommand.</param>
		private void PipelineCallback(IPipelineCommand command)
		{
			ResponseEventArgs args = new ResponseEventArgs();
			args.Response = command.HttpResponseData;
			args.State = command.HttpStateData;

			if ( command.ErrorMessage.Length == 0 )
			{
				// Saves cookies
				_cookieManager.AddCookies(command.HttpResponseData.CookieCollection);
			}

			// return response in a event
			EndHttp(this, args);
		}

		/// <summary>
		/// Callback method that sends the request to the WorkerProcess for further processing.
		/// It handles each request sequencially without blocking the asynchronous request.
		/// </summary>
		/// <param name="ar"> The IAsyncResult from async request.</param>
		protected void FastResponseCallback(IAsyncResult ar)
		{
			//
			// TODO: Rename to InspectorCallback and try to move the command closer to the first caller.
			//

			// Get http state
			HttpState httpState = (HttpState)ar.AsyncState;
			
			// new InspectorPipelineCommand
			InspectorPipelineCommand inspectorPipelineCommand = 
				new InspectorPipelineCommand(
				ClientSettings,
				ProxySettings,
				httpState,
				new PipelineCommandResultEventHandler(PipelineCallback)
				);
			
			try
			{				
				HttpWebResponse response = (HttpWebResponse)httpState.HttpRequest.EndGetResponse(ar);
				
				inspectorPipelineCommand.HttpStateData.HttpResponse = response;
			}
			catch (WebException wex)
			{
				// Handle web exceptions
				if ( wex.Status == WebExceptionStatus.ProtocolError )
				{
					inspectorPipelineCommand.HttpStateData.HttpResponse = (HttpWebResponse)wex.Response;
				} 
				else 
				{
					ExceptionHandler.RegisterException(wex);
					inspectorPipelineCommand.ErrorMessage = wex.Message;
				}
			}
			catch (Exception ex)
			{
				// Handle unknown exceptions
				ExceptionHandler.RegisterException(ex);
				inspectorPipelineCommand.ErrorMessage = ex.Message;
			}

			// enter lock and signaled event
			System.Diagnostics.Debug.Write("Before Enter - Set Signal.\r\n");
			Monitor.Enter( WorkerProcess.ReceiveList );
			System.Diagnostics.Debug.Write("After Enter - Set Signal.\r\n");
			
			WorkerProcess.ReceiveList.Add(inspectorPipelineCommand);
			System.Diagnostics.Debug.Write("Packet Set Signal.\r\n");
			WorkerProcess.ReceiveEvent.Set();

			Monitor.Exit( WorkerProcess.ReceiveList );
			System.Diagnostics.Debug.Write("After Exit - Set Signal.\r\n");
		}

		#endregion
		#region Properties
		/// <summary>
		/// Gets the Cookie Manager.
		/// </summary>
		public CookieManager CookieManager
		{
			get
			{
				return _cookieManager;
			}
		}
		/// <summary>
		/// Gets or sets the http settings for the obejct.
		/// </summary>
		public HttpProperties ClientSettings
		{
			get
			{
				return _properties;
			}
			set
			{
				_properties = value;
			}
		}

		/// <summary>
		/// Gets or sets the proxy settings.
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

		/// <summary>
		/// Gets or sets if automatic parsing is done after request completion.
		/// </summary>
		public bool ParseHttpBody
		{
			get
			{
				return _parsingEnabled;
			}
			set
			{
				_parsingEnabled=value;
			}
		}

		/// <summary>
		/// Gets or sets the Continue Header
		/// </summary>
		protected string ContinueHeader
		{
			get
			{
				return _continueHeader;
			}
			set
			{
				_continueHeader = value;
			}
		}

		public int GetTimeout()
		{
			return this.ClientSettings.Timeout;
		}
		///// <summary>
		///// Gets or sets the timeout.
		///// </summary>
		//public int Timeout
		//{
		//    get
		//    {
		//        return _timeout;
		//    }
		//    set
		//    {
		//        _timeout = value;
		//    }
		//}
		#endregion
		#region Methods

		/// <summary>
		/// Uploads a file.
		/// </summary>
		/// <param name="fileInfo"> The UploadFileInfo item.</param>
		/// <param name="boundaryBytes"> The boundary as bytes.</param>
		/// <param name="currentStream"> The current stream.</param>
		protected void UploadFile(UploadFileInfo fileInfo, byte[] boundaryBytes, Stream currentStream)
		{
			// Open file
			FileStream fileStream = new FileStream(fileInfo.FileName, 
				FileMode.Open, FileAccess.Read);

			// write out file
			byte[] buffer = new Byte[checked((uint)Math.Min(4096, 
				(int)fileStream.Length))];

			int bytesRead = 0;
			while ( (bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0 )
			{
				// Begin Write
				//currentStream.BeginWrite(data, 0, data.Length, requestState.WriteCallback, requestState);
				currentStream.Write(buffer, 0, bytesRead);
			}

			// Write out the trailing boundary
			// currentStream.Write(boundaryBytes, 0, boundaryBytes.Length);
			fileStream.Close();
		}
		/// <summary>
		/// Generates a MIME multipart/form-data header.
		/// </summary>
		/// <param name="boundary"> The boundary.</param>
		/// <param name="formTag"> The HtmlFormTag value.</param>
		/// <param name="fileItems"> The UploadFileInfo items.</param>
		/// <returns> A MIME multipart/form-data header string.</returns>
		protected string GenerateMimeFormData(string boundary, Ecyware.GreenBlue.Engine.HtmlDom.HtmlFormTag formTag, UploadFileInfo[] fileItems)
		{

			// create hashtable for upload file infos.
			Hashtable files = new Hashtable();
			foreach ( UploadFileInfo fileInfo in fileItems )
			{
				files.Add(fileInfo.FormFieldName, fileInfo);
			}

			HtmlParser parser = new HtmlParser();
			ArrayList values = parser.GetArrayList(formTag);

			StringBuilder mimeData = new StringBuilder();

			// Create form fields
			foreach ( string nameValuePair in values )
			{
				string name = EncodeDecode.UrlDecode(nameValuePair.Split('=')[0]);
				string value = EncodeDecode.UrlDecode(nameValuePair.Split('=')[1].Trim().Trim('\0')).Trim('\0');

				// skip keys from UploadFileItems.
				if ( files.ContainsKey(name) )
				{
					UploadFileInfo fileInfo = (UploadFileInfo)files[name];
					//-----------------------------7d52903b1507ec
					//\r\n
					//Content-Disposition: form-data; name=\"oFile1\"; filename=\"C:\\images\\GB Splash Screen.psd\"
					//\r\n
					//Content-Type: application/octet-stream
					//\r\n
					//\r\n
					mimeData.Append("--");
					mimeData.Append(boundary);
					mimeData.Append("\r\n");
					mimeData.AppendFormat("Content-Disposition: form-data; name=\"{0}\"",name);
					mimeData.AppendFormat("; filename=\"{0}\"",fileInfo.FileName);
					mimeData.Append("\r\n");
					mimeData.Append("Content-Type: ");
					mimeData.Append(fileInfo.ContentType);
					mimeData.Append("\r\n");
					mimeData.Append("\r\n");
				} 
				else 
				{				
					// Example
					//-----------------------------7d52903b1507ec
					//\r\n
					//Content-Disposition: form-data;name="Hello23"
					//\r\n
					//\r\n
					//Hello
					//\r\n

					mimeData.Append("--");
					mimeData.Append(boundary);
					mimeData.Append("\r\n");
					mimeData.AppendFormat("Content-Disposition: form-data; name=\"{0}\"",name);
					mimeData.Append("\r\n\r\n");
					mimeData.Append(value);
					mimeData.Append("\r\n");
				}
			}

			//mimeData.Append("\0");

			return mimeData.ToString();
		}

		/// <summary>
		/// Receives the continue header from delegate and sets it to ContinueHeader property.
		/// </summary>
		/// <param name="statusCode"> The status code</param>
		/// <param name="header"> The header collection.</param>
		protected void GetRedirectHeaders(int statusCode, WebHeaderCollection header)
		{
			StringBuilder textStream = new StringBuilder();

			for (int i = 0;i<header.Count;i++)
			{
				textStream.Append(header.GetKey(i));
				textStream.Append(": ");
				textStream.Append(header.Get(i));
				textStream.Append("\r\n");
			}

			ContinueHeader= textStream.ToString();
		}

		/// <summary>
		/// Sets the Proxy
		/// </summary>
		/// <param name="hwr"> The HttpWebRequest type.</param>
		/// <param name="proxySettings"> The HttpProxy settings type.</param>
		protected void SetProxy(HttpWebRequest hwr, HttpProxy proxySettings)
		{
			if ( proxySettings.Credentials == null )
			{
				hwr.Proxy = new WebProxy(proxySettings.ProxyUri,proxySettings.BypassOnLocal);
			} 
			else 
			{
				hwr.Proxy = new WebProxy(proxySettings.ProxyUri,proxySettings.BypassOnLocal, proxySettings.BypassList, proxySettings.Credentials);
			}
		}
		/// <summary>
		/// Sets the HttpWebRequest properties.
		/// </summary>
		/// <param name="hwr"> The HttpWebRequest type.</param>
		/// <param name="properties"> The HttpProperties type that contains the values to set.</param>
		protected void SetHttpWebRequestProperties(HttpWebRequest hwr, HttpProperties properties)
		{
			// SSL/TLS Certificates Policy Settings			
			ServicePointManager.CertificatePolicy = new SSLCertificatePolicy();			

			// Security Protocol Settings
			ServicePointManager.SecurityProtocol = properties.SecurityProtocol;			

			this.ClientSettings = properties;

			hwr.AllowAutoRedirect = properties.AllowAutoRedirects;
			hwr.MaximumAutomaticRedirections = properties.MaximumAutoRedirects;
			hwr.AllowWriteStreamBuffering = properties.AllowWriteStreamBuffering;
			hwr.UserAgent = properties.UserAgent;
			hwr.Pipelined = properties.Pipeline;
			hwr.SendChunked = properties.SendChunked;
			hwr.KeepAlive = properties.KeepAlive;
			hwr.Accept = properties.Accept;

			if ( properties.ContentLength > -1 )
			{
				hwr.ContentLength = properties.ContentLength;
			}

			hwr.ContentType = properties.ContentType;
			hwr.IfModifiedSince = properties.IfModifiedSince;
			hwr.MediaType = properties.MediaType;
			hwr.TransferEncoding = properties.TransferEncoding;
			hwr.Referer= properties.Referer;

			foreach ( WebHeader wh in properties.AdditionalHeaders )
			{
				hwr.Headers.Add(wh.Name, wh.Value);
			}
			

			// authentication
			HttpAuthentication auth = properties.AuthenticationSettings;

			// set Windows NTLM security
			if ( auth.UseNTLMAuthentication )
			{				
				hwr.Credentials = CredentialCache.DefaultCredentials;
				hwr.UnsafeAuthenticatedConnectionSharing = true;

			} 
			else 
			{
				// use basic authentication
				if ( auth.UseBasicAuthentication )
				{
					if ( auth.Domain != String.Empty )
					{
						hwr.Credentials=new NetworkCredential(auth.Username,auth.Password,auth.Domain);
					} 
					else 
					{
						hwr.Credentials=new NetworkCredential(auth.Username,auth.Password);
					}
				}
			}
		}

		/// <summary>
		/// Sets the service point manager defaults settings.
		/// Defaults are:
		/// DefaultConnectionLimit = 10,
		/// MaxServicePointIdleTime = 2 minutes,
		/// MaxServicePoints = 20 and a dummy SSL Certificate Policy.
		/// </summary>
		protected void SetServicePointManagerDefaults()
		{
			ServicePointManager.DefaultConnectionLimit = 10;
			ServicePointManager.MaxServicePointIdleTime = 120000;
			ServicePointManager.MaxServicePoints = 15;

			ServicePointManager.CheckCertificateRevocationList = true;
		}
		#endregion
		#region Timeout and abort methods
		/// <summary>
		/// Timeout callback method.
		/// </summary>
		/// <param name="state"> Object state.</param>
		/// <param name="timeout"> True if timout limit exceeded, else false.</param>
		public static void RequestTimeoutCallback(object state, bool timeout)
		{
			try
			{
				if ( timeout == true )
				{
					HttpState requestState = state as HttpState;

					if ( (requestState != null) && (requestState.HttpRequest != null) )
					{
						requestState.HttpRequest.Abort();
					}
				}
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Aborts the current request.
		/// </summary>
		public void AbortRequest()
		{
			try
			{
				if ( (httpRequestState != null ) && (httpRequestState.HttpRequest != null) )
				{
					httpRequestState.HttpRequest.Abort();
				}
			}
			catch
			{
				throw;
			}
		}

		/// <summary>
		/// Aborts the current request witout throwing an exception.
		/// </summary>
		public void SafeAbortRequest()
		{
			try
			{
				if ( httpRequestState != null )
				{
					httpRequestState.HttpRequest.Abort();
				}
			}
			catch
			{
				// ignore
			}
		}
		#endregion	
		#region License Validation
		/// <summary>
		/// Checks that the validate ip event is set, else throw error.
		/// </summary>
		//		protected void ValidateIPAddress(Uri url)
		//		{
		//			bool found = false;
		//
		//			if ( !GetIPValidator.Instance.IsUnlimited )
		//			{
		//				#region Validate IP in license
		//				try
		//				{
		//					// check cache
		//					if ( GetIPValidator.CheckCache(url.Host) )
		//					{
		//						found = true;
		//					} 
		//					else 
		//					{
		//						IPHostEntry ipHost = Dns.Resolve(url.Host);
		//						foreach ( IPAddress ip in ipHost.AddressList )
		//						{
		//							if ( ip.AddressFamily.Equals(System.Net.Sockets.AddressFamily.InterNetwork) )
		//							{
		//								found = found | GetIPValidator.Instance.CheckIP(ip.ToString());
		//							}
		//						}
		//					}
		//
		//					if ( !found )
		//					{
		//						throw new IPNotFoundException();
		//					} 
		//					else 
		//					{
		//						// add to cache
		//						GetIPValidator.AddToCache(url.Host);
		//					}
		//				}
		//				catch ( SocketException dnsException )
		//				{
		//					Utils.ExceptionHandler.RegisterException(dnsException);
		//					throw dnsException;
		//				}
		//				catch (Exception ex)
		//				{
		//					Utils.ExceptionHandler.RegisterException(ex);
		//					throw ex;
		//				}
		//				#endregion
		//			}
		//		}
		#endregion
	}
}
