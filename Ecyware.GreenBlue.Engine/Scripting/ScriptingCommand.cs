using System;
using System.Reflection;
using System.IO;
using System.Net;
using System.Collections;
using System.Threading;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Engine.HtmlCommand;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms;
//using Microsoft.Vsa;

namespace Ecyware.GreenBlue.Engine.Scripting
{
	/// <summary>
	/// Delegates that signals the start of a request.
	/// </summary>
	public delegate void OnRequestStartEventHandler(object sender, RequestStartEndEventArgs e);

	/// <summary>
	/// Delegates that signals the end of a request.
	/// </summary>
	public delegate void OnRequestEndEventHandler(object sender, RequestStartEndEventArgs e);

	/// <summary>
	/// Delegate that outputs the results to HTML.
	/// </summary>
	public delegate void HtmlResultEventHandler(object sender, EventArgs e);

	/// <summary>
	/// Delegate that outputs the results to text.
	/// </summary>
	public delegate void TextResultEventHandler(object sender, EventArgs e);

	/// <summary>
	/// Summary description for ScriptingCommand.
	/// </summary>
	public class ScriptingCommand 
	{	
		/// <summary>
		/// Event that handles the request start.
		/// </summary>
		public event OnRequestStartEventHandler OnRequestStart;
		/// <summary>
		/// Event that handles the request end.
		/// </summary>
		public event OnRequestEndEventHandler OnRequestEnd;
		/// <summary>
		/// Event that handles the HTML Browser.
		/// </summary>
		public event HtmlResultEventHandler HtmlBrowserEvent;

		/// <summary>
		/// Event that handles the Text Browser.
		/// </summary>
		public event TextResultEventHandler TextBrowserEvent;

		/// <summary>
		/// Event that handles a session abortion.
		/// </summary>
		public event SessionAbortEventHandler SessionAbortedEvent;

		// CookieManager
		//private CookieManager cookieManager = new CookieManager();
		private ScriptingRequestClient _client;
		private HttpProxy _proxy;
		//private HttpProperties ClientProperties;
		private int _currentIndex = 0;
		private ScriptingApplication _sessionScripting;
		private bool _isRunning = false;

		/// <summary>
		/// Parser type.
		/// </summary>
		private HtmlParser parser = new HtmlParser();

//		/// Jscript Engine
//		/// 
//		Microsoft.Vsa.IVsaEngine _engine;
		static private TextWriter   _jsStream;

		/// <summary>
		/// Creates a new ScriptingCommand.
		/// </summary>
		public ScriptingCommand()
		{
			BaseHttpForm.ResetCookieManager();

//			_engine = new Microsoft.JScript.Vsa.VsaEngine();
//			_engine.RootMoniker = "gb://scriptingCommand/";
//			_engine.Site = this;
//			_engine.InitNew();
//			_engine.RootNamespace = "Ecyware.GreenBlue.Engine.Scripting";
//
//			_engine.GenerateDebugInfo = true;
//			IVsaItems items = _engine.Items;
//			IVsaReferenceItem  refItem;
//
//			// Add System.dll
//			refItem = (IVsaReferenceItem)items.CreateItem("system.dll",
//				VsaItemType.Reference,
//				VsaItemFlag.None);
//			refItem.AssemblyName = "system.dll";
//
//			// Add mscorlib.dll
//			refItem = (IVsaReferenceItem)items.CreateItem("mscorlib.dll",
//				VsaItemType.Reference,
//				VsaItemFlag.None);
//			refItem.AssemblyName = "mscorlib.dll";
//
//			// Add System.Windows.Forms.dll
//			refItem = (IVsaReferenceItem)items.CreateItem("WinForms",
//				VsaItemType.Reference,
//				VsaItemFlag.None);
//			refItem.AssemblyName = "System.Windows.Forms.dll";
//
//			// Add "Ecyware.GreenBlue.Configuration.dll"
//			refItem = (IVsaReferenceItem)items.CreateItem("Ecyware.GreenBlue.Configuration",
//				VsaItemType.Reference,
//				VsaItemFlag.None);
//			refItem.AssemblyName = Assembly.GetAssembly(typeof(Ecyware.GreenBlue.Configuration.ConfigurationSection)).Location;
//
//			// Add "System.Xml.dll"
//			refItem = (IVsaReferenceItem)items.CreateItem("System.Xml",
//				VsaItemType.Reference,
//				VsaItemFlag.None);
//			refItem.AssemblyName = "System.Xml.dll";
//
//			// Add "Ecyware.GreenBlue.Engine.dll"
//			refItem = (IVsaReferenceItem)items.CreateItem("Ecyware.GreenBlue.Engine",
//				VsaItemType.Reference,
//				VsaItemFlag.None);
//			refItem.AssemblyName = Assembly.GetAssembly(typeof(Ecyware.GreenBlue.Engine.Session)).Location;
//
//			string  assemName = Assembly.GetExecutingAssembly().Location;
//			refItem = (IVsaReferenceItem)items.CreateItem(assemName,
//				VsaItemType.Reference,
//				VsaItemFlag.None);
//			refItem.AssemblyName = assemName;
		}

		/// <summary>
		/// Closes the command.
		/// </summary>
		public void Close()
		{
			_sessionScripting = null;
			parser = null;
		}
		/// <summary>
		/// Creates a new ScriptingCommand.
		/// </summary>
		/// <param name="sessionScripting"> The ScriptingData to load.</param>
		public ScriptingCommand(ScriptingApplication sessionScripting) : this()
		{
			_sessionScripting = sessionScripting;
		}

		#region Properties
		/// <summary>
		/// Gets or sets the if the command is running.
		/// </summary>
		public bool IsRunning
		{
			get
			{
				return _isRunning;
			}
			set
			{
				_isRunning = value;
			}
		}

		/// <summary>
		/// Gets or sets the proxy.
		/// </summary>
		public HttpProxy Proxy
		{
			get
			{
				return _proxy;
			}
			set
			{
				_proxy = value;
			}
		}

		/// <summary>
		/// Gets or sets the session scripting.
		/// </summary>
		public ScriptingApplication SessionScripting
		{
			get
			{
				return _sessionScripting;
			}
			set
			{
				_sessionScripting = value;
			}
		}
		#endregion
		/// <summary>
		/// Executes the session scripting.
		/// </summary>
		public void ExecuteScriptingCommand()
		{
			ExecuteRequest(_sessionScripting.WebRequests[_currentIndex],_currentIndex);
		}

		/// <summary>
		/// Resets the cookies.
		/// </summary>
		public void ResetCookies()
		{
			BaseHttpForm.ResetCookieManager();
		}

		/// <summary>
		/// Executes a session that executes until the end index and uses a scripting file. 
		/// </summary>
		/// <param name="application"> The scripting application.</param>
		/// <param name="endIndex"> The index where the last request executes.</param>
		/// <param name="scriptingFileLocation"> The scripting file location.</param>
//		public void ExecuteScriptedSession(ScriptingApplication application, int endIndex, string scriptingFileLocation)
//		{
//			_sessionScripting = application;
//			IVsaItems    items = _engine.Items;
//
//			// Load the script code
//			// NOTE: For VB, the name of the item, "Script", must match the name of the Module,
//			//       or adding global items won't work <sigh>
//			IVsaCodeItem codeItem = (IVsaCodeItem)items.CreateItem("Script",
//				VsaItemType.Code,
//				VsaItemFlag.None);
//
//			StreamReader reader = new StreamReader(scriptingFileLocation);
//			codeItem.SourceText = reader.ReadToEnd();
//			reader.Close();
//
//			// Add the global "This" item
//			IVsaGlobalItem  scommand = (IVsaGlobalItem)items.CreateItem("This",
//				VsaItemType.AppGlobal,
//				VsaItemFlag.None);
//			scommand.TypeString = "Ecyware.GreenBlue.Engine.Scripting.ScriptingCommand";
//
//			// Add the global "MyScriptingApplication" item
//			IVsaGlobalItem  sapplication = (IVsaGlobalItem)items.CreateItem("MyScriptingApplication",
//				VsaItemType.AppGlobal,
//				VsaItemFlag.None);
//			sapplication.TypeString = "Ecyware.GreenBlue.Engine.Scripting.ScriptingApplication";
//			
////			_engine.SetOption("print", true);
//
////			// Set the JS output stream once for this appdomain
////			if( _jsStream == null )
////			{
////				StreamWriter    writer = new StreamWriter(@"c:\jsout.txt");
////				writer.AutoFlush = true;
////				_jsStream = writer;
////				Microsoft.JScript.ScriptStream.Out = _jsStream;
////			}
//
//			try				
//			{
//				// Run the script
//				if ( _engine.Compile() )
//				{
//					_engine.Run();
//					ExecuteSessionUntilEnd(application, endIndex);
//				}
//			}
//			catch ( Exception ex )
//			{
//				System.Windows.Forms.MessageBox.Show(ex.ToString());
//			}
//		}
//
//

		/// <summary>
		/// Executes a session that executes until the end index.
		/// </summary>
		/// <param name="application"> The scripting application.</param>
		/// <param name="endIndex"> The index where the last request executes.</param>
		public void ExecuteSessionUntilEnd(ScriptingApplication application, int endIndex)
		{
			ScriptingApplication temp = application.Clone();
			temp.ClearWebRequests();			

			// if endIndex is 0, we want to include it.
			for ( int i=0;i<=endIndex;i++ )
			{
				// add to new scripting data.
				temp.AddWebRequest(application.WebRequests[i]);
			}

			// Replace Scripting Data with current.
			_sessionScripting = temp;

			// Start from zero.
			IsRunning = true;
			ExecuteRequest(temp.WebRequests[0],0);
		}

//		/// <summary>
//		/// Executes a web request and returns the response.
//		/// </summary>
//		/// <param name="application"> The scripting application.</param>
//		/// <param name="endIndex"> The index where the last request executes.</param>
//		public void ExecuteRequestResponse(ScriptingApplication application, int request)
//		{
//
//		}

		/// <summary>
		/// Executes the next request.
		/// </summary>
		private void ExecuteNextRequest()
		{			
			_currentIndex++;			

			if ( _sessionScripting.WebRequests.Length > _currentIndex )
			{
				ExecuteRequest(_sessionScripting.WebRequests[_currentIndex], _currentIndex);
			} 
			else 
			{
				_currentIndex = 0;
			}
		}

		#region Transformations


		/// <summary>
		/// Apply input transforms
		/// </summary>
		/// <param name="request"> The web request.</param>
		public void ApplyInputTransforms(WebRequest request)
		{
			foreach ( WebTransform transform in request.InputTransforms )
			{
				if ( transform.IsActive )
				{
					if ( _currentIndex - 1 < 0 )
					{
						transform.ApplyTransform(request, null);
					} 
					else 
					{
						WebResponse previousResponse = _sessionScripting.WebRequests[_currentIndex-1].WebResponse;
						transform.ApplyTransform(request, previousResponse);
					}
				}
			}
		}

		/// <summary>
		/// Set any delegate needed for the transforms.
		/// </summary>
		/// <param name="request"> The web request.</param>
		public void ConfigureOutputTransforms(WebRequest request)
		{
			HtmlResultEventHandler htmlViewer = HtmlBrowserEvent;
			TextResultEventHandler textViewer = TextBrowserEvent;
			Delegate[] callbacks = new Delegate[] {htmlViewer, textViewer};			

			foreach ( WebTransform transform in request.OutputTransforms )
			{
				if ( transform.SupportsCallbacks )
				{
					transform.AddTransformCallbacks(callbacks);
				}
			}
		}
		#endregion
		#region HTTP Methods
		/// <summary>
		/// Executes a request.
		/// </summary>
		/// <param name="request"> The WebRequest to execute.</param>
		/// <param name="index"> The request index.</param>
		private void ExecuteRequest(WebRequest request, int index)
		{
			InitializeHttpCommands();

			// Get Cookies			
			CookieCollection cookies = null;
			cookies = _client.CookieManager.GetCookies(new Uri(request.Url));
			request.ClearCookies();
			request.AddCookies(cookies);

			// Apply Input Transforms.
			ApplyInputTransforms(request);

			if ( OnRequestStart != null )
			{
				RequestStartEndEventArgs args = new RequestStartEndEventArgs();
				args.CurrentIndex = index;
				args.RequestCount = _sessionScripting.WebRequests.Length;
				args.Request = request;

				OnRequestStart(this, args);
			}

			switch ( request.RequestType )
			{
				case HttpRequestType.GET:
					ExecuteGetRequest((GetWebRequest)request, index);
					break;
				case HttpRequestType.DELETE:
					ExecuteDeleteRequest((DeleteWebRequest)request, index);
					break;
				case HttpRequestType.PUT:
					ExecutePutRequest((PutWebRequest)request, index);
					break;
				case HttpRequestType.POST:
					ExecutePostRequest((PostWebRequest)request, index);
					break;
				case HttpRequestType.SOAPHTTP:
					ExecuteSoapHttpRequest((SoapHttpWebRequest)request, index);
					break;
			}
		}


		/// <summary>
		/// Initializes the HTTP Commands.
		/// </summary>
		private void InitializeHttpCommands()
		{
			_client = new ScriptingRequestClient();	
			_client.ScriptingRequestClientResult += new ScriptingRequestClientResultHandler(ScriptingRequestClientResult);
		}

		/// <summary>
		/// Executes a POST request.
		/// </summary>
		/// <param name="request"> Post request to execute.</param>
		/// <param name="index"> The request index.</param>
		private void ExecutePostRequest(PostWebRequest request, int index)
		{					
			try
			{
				ConfigureOutputTransforms(request);

				_client.ProxySettings = this.Proxy;

				if ( request.Form.Enctype == null )
				{
					_client.ExecutePostWebRequest(new HttpRequestResponseContext(request, index, false));
				} 
				else 
				{
					if ( request.Form.Enctype.ToLower(System.Globalization.CultureInfo.InvariantCulture) == "multipart/form-data" )
					{
						_client.ExecutePostWebRequestFileUpload(new HttpRequestResponseContext(request, index, false));
					} 
					else 
					{
						_client.ExecutePostWebRequest(new HttpRequestResponseContext(request, index, false));
					}
				}
			}
			catch ( Exception ex )
			{
				AbortSessionRun(ex.ToString());
			}
		}


		/// <summary>
		/// Executes a SOAP over HTTP request.
		/// </summary>
		/// <param name="request"> Soap request to execute.</param>
		/// <param name="index"> The request index.</param>
		private void ExecuteSoapHttpRequest(SoapHttpWebRequest request, int index)
		{
			try
			{
				ConfigureOutputTransforms(request);				

				_client.ProxySettings = this.Proxy;
				_client.ExecuteSoapHttpWebRequest(new HttpRequestResponseContext(request,index, false));
			}
			catch ( Exception ex )
			{
				AbortSessionRun(ex.ToString());
			}
		}

		/// <summary>
		/// Executes a PUT request.
		/// </summary>
		/// <param name="request"> Delete request to execute.</param>
		/// <param name="index"> The request index.</param>
		private void ExecutePutRequest(PutWebRequest request, int index)
		{
			try
			{
				ConfigureOutputTransforms(request);				

				_client.ProxySettings = this.Proxy;
				_client.ExecutePutWebRequest(new HttpRequestResponseContext(request,index, false));
			}
			catch ( Exception ex )
			{
				AbortSessionRun(ex.ToString());
			}
		}

		/// <summary>
		/// Executes a DELETE request.
		/// </summary>
		/// <param name="request"> Delete request to execute.</param>
		/// <param name="index"> The request index.</param>
		private void ExecuteDeleteRequest(DeleteWebRequest request, int index)
		{
			try
			{
				ConfigureOutputTransforms(request);				

				_client.ProxySettings = this.Proxy;
				_client.ExecuteDeleteWebRequest(new HttpRequestResponseContext(request,index, false));
			}
			catch ( Exception ex )
			{
				AbortSessionRun(ex.ToString());
			}
		}

		/// <summary>
		/// Executes a GET request.
		/// </summary>
		/// <param name="request"> Get request to execute.</param>
		/// <param name="index"> The request index.</param>
		private void ExecuteGetRequest(GetWebRequest request, int index)
		{
			try
			{
				ConfigureOutputTransforms(request);				

				_client.ProxySettings = this.Proxy;
				_client.ExecuteGetWebRequest(new HttpRequestResponseContext(request,index, false));
			}
			catch ( Exception ex )
			{
				AbortSessionRun(ex.ToString());
			}
		}

		#endregion
		/// <summary>
		/// The request client result.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The event arguments.</param>
		private void ScriptingRequestClientResult(object sender, ScriptingResponseArgs e)
		{
			WebRequest request = e.Context.Request;

			if ( e.Context.Request.WebResponse.ErrorMessage.Length > 0 )
			{
				AbortSessionRun(e.Context.Request.WebResponse.ErrorMessage);
				_currentIndex = 0;
				return;
			}

			// Update Scripting Data
			_sessionScripting.WebRequests[e.Context.RequestIndex] = request;
			
			if ( OnRequestEnd != null )
			{
				RequestStartEndEventArgs args = new RequestStartEndEventArgs();
				args.CurrentIndex = e.Context.RequestIndex;
				args.RequestCount = _sessionScripting.WebRequests.Length;
				args.Request = request;

				OnRequestEnd(this, args);
			}

			// Execute Next Request
			if ( this.IsRunning )
			{
				ExecuteNextRequest();
			} 
			else 
			{
				_currentIndex = 0;
			}
		}

		#region Abort Methods
		public void Reset()
		{
			_isRunning = false;
			_currentIndex = 0;
		}
		/// <summary>
		/// Aborts the session and updates the pending reports.
		/// </summary>
		/// <param name="message"> The message to show.</param>
		private void AbortSessionRun(string message)
		{
			_isRunning = false;
			_currentIndex = 0;

			SessionAbortEventArgs sargs = new SessionAbortEventArgs();
			sargs.ErrorMessage = message;

			if ( SessionAbortedEvent != null )
				SessionAbortedEvent(this, sargs);
		}

		/// <summary>
		/// Stops the session.
		/// </summary>
		public void Stop()
		{
			_isRunning = false;

			SessionAbortEventArgs sargs = new SessionAbortEventArgs();
			sargs.ErrorMessage = "Scripting Application aborted by user.";
			SessionAbortedEvent(this, sargs);
		}

		#endregion

//		#region IVsaSite Members
//
//		public object GetEventSourceInstance(string itemName, string eventSourceName)
//		{
//			// TODO:  Add ScriptingCommand.GetEventSourceInstance implementation
//			return null;
//		}
//
//		public object GetGlobalInstance(string name)
//		{
//			switch( name ) 
//			{
//				case "This":
//					return this;
//				case "MyScriptingApplication":
//					return this.SessionScripting;
//				default:
//					return null;
//			}
//		}
//
//		public void Notify(string notify, object info)
//		{
//			// TODO:  Add ScriptingCommand.Notify implementation
//		}
//
//		public bool OnCompilerError(IVsaError e)
//		{
//			System.Windows.Forms.MessageBox.Show(String.Format("Error of severity {0} on line {1}: {2}", e.Severity, e.Line, e.Description));
//			return true; // Continue to report errors
//		}
//
//		public void GetCompiledState(out byte[] pe, out byte[] debugInfo)
//		{
//			System.Diagnostics.Trace.WriteLine("IVsaSite.GetCompiledState()");
//			pe = debugInfo = null;
//		}
//
//		#endregion

		// IDisposable
//		void IDisposable.Dispose()
//		{
//			// Close the engine
//			if( _engine != null ) _engine.Close();
//			_jsStream.Close();
//		}
	}
}
