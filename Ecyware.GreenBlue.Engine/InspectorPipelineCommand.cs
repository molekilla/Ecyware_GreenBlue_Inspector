// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: June 2004
using System;
using System.Net;
using System.IO;

namespace Ecyware.GreenBlue.Engine
{
	//TODO: Change the inspectorpipelinecommand constructor to accept a Context type like.
	/// <summary>
	/// Contains the definition for the InspectorPipelineCommand.
	/// </summary>
	public class InspectorPipelineCommand : IPipelineCommand
	{
		InspectorPipelineApplication inspectorPipeline = new InspectorPipelineApplication();
		Delegate _callback;
		BaseHttpState _httpState;
		ResponseBuffer _responseBuffer;

		/// <summary>
		/// Creates a new InspectorPipelineCommand.
		/// </summary>
		/// <param name="clientSettings"> The HTTP Properties.</param>
		/// <param name="proxySettings"> The HTTP Proxy settings.</param>
		/// <param name="httpState"> The HTTP State.</param>
		/// <param name="callback"> The callback method.</param>
		public InspectorPipelineCommand(HttpProperties clientSettings, HttpProxy proxySettings, HttpState httpState, Delegate callback)
		{
			Initialize();

			inspectorPipeline.ClientSettings = clientSettings;			
			inspectorPipeline.ProxySettings = proxySettings;
			HttpStateData = httpState;
			CallbackMethod = callback;
		}

		private void Initialize()
		{
			inspectorPipeline.FillHeadersEvent += new FillHeadersEventHandler(InspectorPipeline_FillHeaders);
			inspectorPipeline.FillCookiesEvent += new FillCookiesEventHandler(InspectorPipeline_FillCookies);
			inspectorPipeline.FillHttpBodyEvent += new FillHttpBodyEventHandler(InspectorPipeline_FillHttpBody);
			inspectorPipeline.PipelineCompleteEvent += new PipelineCompleteEventHandler(PipelineCommandCompleted);
			inspectorPipeline.ParseScriptsEvent += new ParseScriptsEventHandler(InspectorPipeline_ParseScripts);
			inspectorPipeline.LoadScriptSourceEvent += new LoadScriptSourceEventHandler(InspectorPipeline_LoadScriptSource);
			//inspectorPipeline.SetErrorMessageEvent += new SetErrorMessageEventHandler(ErrorHandler);
		}

		private void InspectorPipeline_LoadScriptSource(object sender, EventArgs e)
		{
			// -- Load Scripts from source --
			inspectorPipeline.ResponseData = HttpPipeline.LoadScriptsFromSrc(
				(Uri)inspectorPipeline.ResponseData.RequestHeaderCollection["Request Uri"],
				inspectorPipeline.ResponseData,
				inspectorPipeline.ClientSettings);
		}

		private void InspectorPipeline_ParseScripts(object sender, EventArgs e)
		{
			// -- Parse Scripts --
			inspectorPipeline.ResponseData = HttpPipeline.ParseScriptTags(inspectorPipeline.ResponseData);
		}
		private void PipelineCommandCompleted(object sender, EventArgs e)
		{
			_responseBuffer = inspectorPipeline.ResponseData;
		}

		private void InspectorPipeline_FillHttpBody(object sender, EventArgs e)
		{
			Stream stm = HttpStateData.HttpResponse.GetResponseStream();			
			BufferBuilder.FillHttpBody(inspectorPipeline.ResponseData, stm);
			stm.Close();

			if ( HttpStateData.HttpResponse != null )
			{
				HttpStateData.HttpResponse.Close();
			}
		}
		private void InspectorPipeline_FillCookies(object sender, EventArgs e)
		{
			// Cookie collection
			HttpStateData.HttpResponse.Cookies = HttpStateData.HttpRequest.CookieContainer.GetCookies(HttpStateData.HttpRequest.RequestUri);
			BufferBuilder.FillCookieData(inspectorPipeline.ResponseData, HttpStateData.HttpResponse.Cookies);
		}
		private void InspectorPipeline_FillHeaders(object sender, EventArgs e)
		{
			ResponseBuffer respBuffer;
			HttpWebRequest webRequest = HttpStateData.HttpRequest;
			HttpWebResponse webResponse = HttpStateData.HttpResponse;
			 
			if ( CompareString.Compare(webRequest.Method,"get") )
			{
				respBuffer = new ResponseBuffer(HttpRequestType.GET);
			} 
			else 
			{
				respBuffer = new ResponseBuffer(HttpRequestType.POST);
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

			inspectorPipeline.ResponseData = respBuffer;
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
