using System;
using System.Net;
using System.Collections;
using System.IO;
using Ecyware.GreenBlue.Engine.HtmlCommand;
using Ecyware.GreenBlue.Engine.Transforms;

namespace Ecyware.GreenBlue.Engine.Scripting
{
	/// <summary>
	/// Summary description for SessionScriptingPipelineCommand.
	/// </summary>
	public class SessionScriptingPipelineCommand : IPipelineCommand
	{
		/// <summary>
		/// Pipeline Application.
		/// </summary>
		InspectorPipelineApplication inspectorPipeline = new InspectorPipelineApplication();
		/// <summary>
		/// Callback.
		/// </summary>
		Delegate _callback;
		/// <summary>
		/// Http State, in this case ScriptingHttpState.
		/// </summary>
		BaseHttpState _httpState;

		/// <summary>
		/// The response buffer.
		/// </summary>
		ResponseBuffer _responseBuffer;

		HtmlParser htmlParser = new HtmlParser(false, true);

		/// <summary>
		/// Creates a new SessionScriptingPipelineCommand.
		/// </summary>
		public SessionScriptingPipelineCommand(ScriptingHttpState state, HttpProxy proxy, Delegate callback)
		{
			Initialize();

			inspectorPipeline.ClientSettings = state.HttpRequestResponseContext.Request.RequestHttpSettings;			
			inspectorPipeline.ProxySettings = proxy;
			HttpStateData = state;
			CallbackMethod = callback;
		}

		/// <summary>
		/// Initialize the command.
		/// </summary>
		private void Initialize()
		{
			inspectorPipeline.FillHeadersEvent += new FillHeadersEventHandler(InspectorPipeline_FillHeaders);
			inspectorPipeline.FillCookiesEvent += new FillCookiesEventHandler(InspectorPipeline_FillCookies);
			inspectorPipeline.FillHttpBodyEvent += new FillHttpBodyEventHandler(InspectorPipeline_FillHttpBody);
			inspectorPipeline.PipelineCompleteEvent += new PipelineCompleteEventHandler(PipelineCommandCompleted);
			inspectorPipeline.ParseScriptsEvent += new ParseScriptsEventHandler(InspectorPipeline_ParseScripts);
			inspectorPipeline.LoadScriptSourceEvent += new LoadScriptSourceEventHandler(InspectorPipeline_LoadScriptSource);
			inspectorPipeline.ExecuteWebTransformsEvent += new ExecuteWebTransformsEventHandler(inspectorPipeline_ExecuteWebTransformsEvent);
			inspectorPipeline.SetErrorMessageEvent += new SetErrorMessageEventHandler(inspectorPipeline_SetErrorMessageEvent);
			inspectorPipeline.PipelineErrorEvent += new PipelineErrorEventHandler(inspectorPipeLineError);
		}

		private void inspectorPipeLineError(object sender, Exception ex)
		{
			this.HttpStateData.HttpResponse.Close();

			// Get the context
			HttpRequestResponseContext context = ((ScriptingHttpState)HttpStateData).HttpRequestResponseContext;

			// Create a web response
			context.Request.WebResponse = new WebResponse();
			context.Request.WebResponse.ErrorMessage = inspectorPipeline.ResponseData.ErrorMessage;

			// Notify all output tranforms.
			foreach (WebTransform transform in context.Request.OutputTransforms)
			{
				if (transform.IsActive)
				{
					transform.ApplyTransform(context.Request, context.Request.WebResponse);
				}
			}
		}

		/// <summary>
		/// Raise when the pipeline command is completed.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The event arguments.</param>
		private void PipelineCommandCompleted(object sender, EventArgs e)
		{
			// Signal completion of pipe.
			this.HttpStateData.HttpResponse.Close();
		}

		/// <summary>
		/// Creates the web response for the current web request.
		/// </summary>
		private void CreateWebResponse()
		{
			// Get the context
			HttpRequestResponseContext context = ((ScriptingHttpState)HttpStateData).HttpRequestResponseContext;

			// Create a web response
			context.Request.WebResponse = new WebResponse();

			Cookies cookies = new Cookies();
			cookies.AddCookies(HttpResponseData.CookieCollection);

			// Fill the web response
			context.Request.WebResponse.Cookies = cookies.GetCookies();

			context.Request.WebResponse.ErrorMessage = string.Empty;
			context.Request.WebResponse.GetHtmlXml = HttpResponseData.GetHtmlXml;
			context.Request.WebResponse.HttpBody = HttpResponseData.HttpBody;
			
			context.Request.WebResponse.ResponseHttpSettings = context.Request.RequestHttpSettings.Clone();						

			ArrayList headers = new ArrayList();
			//int i = 0;
			foreach ( DictionaryEntry de in HttpResponseData.ResponseHeaderCollection )
			{
				string name = Convert.ToString(de.Key);
				switch ( name.ToLower(System.Globalization.CultureInfo.InvariantCulture) )
				{

					case "accept":						
						break;
					case "connection":
						break;
					case "content-length":
						context.Request.WebResponse.ResponseHttpSettings.ContentLength = HttpStateData.HttpResponse.ContentLength;
						break;
					case "content-type":
						context.Request.WebResponse.ResponseHttpSettings.ContentType = HttpStateData.HttpResponse.ContentType;
						break;
					case "character set":
						context.Request.WebResponse.CharacterSet = HttpStateData.HttpResponse.CharacterSet;
						break;
					case "content encoding":
						context.Request.WebResponse.ContentEncoding = HttpStateData.HttpResponse.ContentEncoding;
						break;
					case "date":
						break;
					case "expect":
						break;
					case "host":
						break;
					case "if-modified-since":
						context.Request.WebResponse.ResponseHttpSettings.IfModifiedSince = HttpStateData.HttpResponse.LastModified;
						break;
					case "last modified":
						context.Request.WebResponse.ResponseHttpSettings.IfModifiedSince = HttpStateData.HttpResponse.LastModified;
						break;
					case "protocol version":
						context.Request.WebResponse.Version = HttpResponseData.Version;
						break;
					case "range":
						break;
					case "referer":
						break;
					case "response uri":
						context.Request.WebResponse.ResponseUri = HttpStateData.HttpResponse.ResponseUri.ToString();						
						break;
					case "status":
						break;
					case "status code":
						context.Request.WebResponse.StatusCode = HttpResponseData.StatusCode;
						break;
					case "status description":
						context.Request.WebResponse.StatusDescription = HttpStateData.HttpResponse.StatusDescription;
						break;
					case "transfer-encoding":
						break;
					case "user-agent":												
						break;
					default:
						WebHeader header = new WebHeader();
						header.Name = name;
						header.Value = Convert.ToString(de.Value);					
						headers.Add(header);
						break;
				}
			}

			context.Request.WebResponse.ResponseHttpSettings.AdditionalHeaders = (WebHeader[])headers.ToArray(typeof(WebHeader));			
			context.Request.WebResponse.Scripts = HttpResponseData.Scripts.ToArray();						
		}

		/// <summary>
		/// Load the script source.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The event arguments.</param>
		private void InspectorPipeline_LoadScriptSource(object sender, EventArgs e)
		{
			// -- Load Scripts from source --
			//			this.HttpResponseData = HttpPipeline.LoadScriptsFromSrc(
			//				((ScriptingHttpState)this.HttpStateData).HttpRequest.RequestUri,
			//				this.HttpResponseData,
			//				inspectorPipeline.ClientSettings);
		}

		/// <summary>
		/// Parse the scripts.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The event arguments.</param>
		private void InspectorPipeline_ParseScripts(object sender, EventArgs e)
		{
			// -- Parse Scripts --
			this.HttpResponseData = HttpPipeline.ParseScriptTags(this.HttpResponseData);
		}
		/// <summary>
		/// Fills the http body.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The event arguments.</param>
		private void InspectorPipeline_FillHttpBody(object sender, EventArgs e)
		{
			Stream stm = HttpStateData.HttpResponse.GetResponseStream();			
			BufferBuilder.FillHttpBody(this.HttpResponseData, stm);
			stm.Close();

			if ( HttpStateData.HttpResponse != null )
			{
				HttpStateData.HttpResponse.Close();
			}

			try
			{
				// Parse to XML.
				this.HttpResponseData.GetHtmlXml = htmlParser.GetParsableString(this.HttpResponseData.HttpBody);
			}
			catch
			{
				this.HttpResponseData.GetHtmlXml = string.Empty;
			}			
		}

		/// <summary>
		/// Fills the cookies in the response buffer.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The event arguments.</param>
		private void InspectorPipeline_FillCookies(object sender, EventArgs e)
		{
			// Cookie collection
			HttpStateData.HttpResponse.Cookies = HttpStateData.HttpRequest.CookieContainer.GetCookies(HttpStateData.HttpRequest.RequestUri);
			BufferBuilder.FillCookieData(this.HttpResponseData, HttpStateData.HttpResponse.Cookies);
		}

		/// <summary>
		/// Fills the headers in the response buffer.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The event arguments.</param>
		private void InspectorPipeline_FillHeaders(object sender, EventArgs e)
		{
			ResponseBuffer respBuffer = null;
			HttpWebRequest webRequest = HttpStateData.HttpRequest;
			HttpWebResponse webResponse = HttpStateData.HttpResponse;
			 
			if ( CompareString.Compare(webRequest.Method,"get") )
			{
				respBuffer = new ResponseBuffer(HttpRequestType.GET);
			} 
			else if ( CompareString.Compare(webRequest.Method,"post") )
			{
				respBuffer = new ResponseBuffer(HttpRequestType.POST);
			}
			else if ( CompareString.Compare(webRequest.Method,"put") )
			{
				respBuffer = new ResponseBuffer(HttpRequestType.PUT);
			}
			else if ( CompareString.Compare(webRequest.Method,"head") )
			{
				respBuffer = new ResponseBuffer(HttpRequestType.HEAD);
			}
			else if ( CompareString.Compare(webRequest.Method,"delete") )
			{
				respBuffer = new ResponseBuffer(HttpRequestType.DELETE);
			}
			else if ( CompareString.Compare(webRequest.Method,"trace") )
			{
				respBuffer = new ResponseBuffer(HttpRequestType.TRACE);
			}
			else if ( CompareString.Compare(webRequest.Method,"options") )
			{
				respBuffer = new ResponseBuffer(HttpRequestType.OPTIONS);
			}

			respBuffer.StatusCode = (int)webResponse.StatusCode;
			respBuffer.StatusDescription = webResponse.StatusDescription;
			
			if ( webResponse.ProtocolVersion == HttpVersion.Version11  )
			{
				respBuffer.Version="1.1";
			} 
			else 
			{
				respBuffer.Version="1.0";
			}

			// Request Header Collection	
			BufferBuilder.FillRequestHeader(respBuffer,webRequest.Headers,webRequest);
			
			// Header Collection
			BufferBuilder.FillResponseHeader(respBuffer,webRequest,webResponse.Headers,webResponse);

			this.HttpResponseData = respBuffer;
		}

		/// <summary>
		/// Sets the error message.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The event arguments.</param>
		private void inspectorPipeline_SetErrorMessageEvent(object sender, EventArgs e)
		{
			// Get the context
			HttpRequestResponseContext context = ((ScriptingHttpState)HttpStateData).HttpRequestResponseContext;

			// Create a web response
			context.Request.WebResponse = new WebResponse();
			context.Request.WebResponse.ErrorMessage = inspectorPipeline.ResponseData.ErrorMessage;

			// Notify all output tranforms.
			foreach ( WebTransform transform in context.Request.OutputTransforms )
			{
				if ( transform.IsActive )
				{
					transform.ApplyTransform(context.Request, context.Request.WebResponse);
				}
			}
		}

		/// <summary>
		/// Executes the web transforms.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The event arguments.</param>
		private void inspectorPipeline_ExecuteWebTransformsEvent(object sender, EventArgs e)
		{
			CreateWebResponse();
			HttpRequestResponseContext context = ((ScriptingHttpState)HttpStateData).HttpRequestResponseContext;

			WebRequest request = ((ScriptingHttpState)this.HttpStateData).HttpRequestResponseContext.Request;

			foreach ( WebTransform transform in request.OutputTransforms )
			{
				if ( transform.IsActive )
				{
					transform.ApplyTransform(request, context.Request.WebResponse);
				}
			}
		}
		#region IPipelineCommand Members
		/// <summary>
		/// The execute command.
		/// </summary>
		public void ExecuteCommand()
		{
			this.inspectorPipeline.Execute();
		}
		/// <summary>
		/// Gets or sets the error message.
		/// </summary>
		public string ErrorMessage
		{
			get
			{
				if ( inspectorPipeline != null )
				{
					return inspectorPipeline.ErrorMessage;
				} 
				else 
				{
					return String.Empty;
				}
			}
			set
			{
				if ( inspectorPipeline != null )
				{
					inspectorPipeline.ErrorMessage = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the response buffer.
		/// </summary>
		public ResponseBuffer HttpResponseData
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
		/// Gets or sets HTTP State Data.
		/// </summary>
		public BaseHttpState HttpStateData
		{
			get
			{				
				return _httpState;
			}
			set
			{
				_httpState = value;
			}
		}

		/// <summary>
		/// Gets or sets the callback delegate.
		/// </summary>
		public Delegate CallbackMethod
		{
			get
			{				
				return _callback;
			}
			set
			{
				_callback = value;
			}
		}


		#endregion


	}
}
