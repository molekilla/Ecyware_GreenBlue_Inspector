// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: June 2004
using System;
using System.Net;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Raises when a pipeline command completes.
	/// </summary>
	public delegate void PipelineCompleteEventHandler(object sender, EventArgs e);

	/// <summary>
	/// Raises when a pipeline command throws an exception.
	/// </summary>
	public delegate void PipelineErrorEventHandler(object sender, Exception e);

	/// <summary>
	/// Contains the base class for implementing pipelines.
	/// </summary>
	public abstract class PipelineApplication
	{
		private string _message = String.Empty;
		private ResponseBuffer _responseBuffer = null;
		private HttpProperties _clientSettings = null;
		private HttpProxy _proxySettings = null;
		
		public PipelineCompleteEventHandler PipelineCompleteEvent;
		public PipelineErrorEventHandler PipelineErrorEvent;

		/// <summary>
		/// Creates a new PipelineApplication.
		/// </summary>
		public PipelineApplication()
		{
		}

		#region Properties
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
		#endregion

		/// <summary>
		/// Executes the pipeline.
		/// </summary>
		public abstract void Execute();
	}
}
