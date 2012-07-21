// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: June 2004
using System;

namespace Ecyware.GreenBlue.Engine
{
	// wrong, each one of this are the filters.
	internal delegate void FillHeadersEventHandler(object sender, EventArgs e);
	internal delegate void FillCookiesEventHandler(object sender, EventArgs e);
	internal delegate void FillHttpBodyEventHandler(object sender, EventArgs e);
	internal delegate void ParseScriptsEventHandler(object sender, EventArgs e);
	internal delegate void LoadScriptSourceEventHandler(object sender, EventArgs e);
	internal delegate void SetErrorMessageEventHandler(object sender, EventArgs e);
	internal delegate void ExecuteWebTransformsEventHandler(object sender, EventArgs e);

	/// <summary>
	/// Contains the implementation for the inspector pipeline application.
	/// </summary>
	public class InspectorPipelineApplication : PipelineApplication
	{
		internal event FillHeadersEventHandler FillHeadersEvent;
		internal event FillCookiesEventHandler FillCookiesEvent;
		internal event FillHttpBodyEventHandler FillHttpBodyEvent;
		internal event ParseScriptsEventHandler ParseScriptsEvent;
		internal event LoadScriptSourceEventHandler LoadScriptSourceEvent;
		internal event SetErrorMessageEventHandler SetErrorMessageEvent;
		internal event ExecuteWebTransformsEventHandler ExecuteWebTransformsEvent;

		/// <summary>
		/// Creates a new InspectorPipelineApplication.
		/// </summary>
		public InspectorPipelineApplication()
		{
			this.ResponseData = new ResponseBuffer();
		}

		/// <summary>
		/// Executes the pipeline.
		/// </summary>
		public override void Execute()
		{
			if ( ErrorMessage.Length == 0 )
			{
				// Exception catching necessary.
				try
				{
					if ( FillHeadersEvent != null )
						FillHeadersEvent(this, new EventArgs());

					if ( FillCookiesEvent != null )
						FillCookiesEvent(this, new EventArgs());

					if ( FillHttpBodyEvent != null )
						FillHttpBodyEvent(this, new EventArgs());

					if ( ParseScriptsEvent != null )
						ParseScriptsEvent(this, new EventArgs());

					if ( LoadScriptSourceEvent != null )
						LoadScriptSourceEvent(this, new EventArgs());

					if ( ExecuteWebTransformsEvent != null )
						ExecuteWebTransformsEvent(this, new EventArgs());
					
					if ( PipelineCompleteEvent != null )
						PipelineCompleteEvent(this, new EventArgs());
				}
				catch ( System.Net.WebException wex )
				{
					this.ResponseData.ErrorMessage = wex.Message;

					if ( SetErrorMessageEvent != null )
						SetErrorMessageEvent(this, new EventArgs());
				}	
				catch ( Exception ex )
				{
					if ( PipelineErrorEvent != null )
						PipelineErrorEvent(this, ex);

					//ExceptionHandler.RegisterException(ex);
				}
			} 
			else 
			{		
				this.ResponseData.ErrorMessage = ErrorMessage;

				if ( SetErrorMessageEvent != null )
					SetErrorMessageEvent(this, new EventArgs());
			}
		}
	}
}
