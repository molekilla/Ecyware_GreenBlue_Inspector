// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: July 2004
using System;
using System.Net;
using System.Text;
using System.Collections;
using Ecyware.GreenBlue.HtmlDom;
using Ecyware.GreenBlue.Protocols.Http;
using Ecyware.GreenBlue.HtmlCommand;
using Ecyware.GreenBlue.WebUnitTestManager;
using Ecyware.GreenBlue.ReportEngine;


namespace Ecyware.GreenBlue.WebUnitTestCommand
{
	/// <summary>
	///  Contains logic for processing URL spider tests.
	/// </summary>
	public class UrlSpiderTestCommand : UnitTestCommand, IUnitTestCommand
	{
		private AsdeCommand asdeCommand = new AsdeCommand();
		private ReportBuilder rptBuilder = new ReportBuilder();
		public event DisplayProcessEventHandler DisplayProcessEvent;
		public event UnitTestSessionReportEventHandler CreateReportEvent;

		private bool _isRunning = false;
		private UrlSpiderCommandContext _context;


		private PostForm postRequest = null;
		private GetForm getRequest = null;
		private HtmlParser parser = new HtmlParser();
		private ArrayList reports = null;
		
		private int _bufferLen = 0;
		private string _sqlSignature = string.Empty;
		private string _xssSignature = string.Empty;
		private bool _xssTest = false;
		private bool _sqlTest = false;
		private bool _bufferTest = false;
		private HtmlFormTag _formTag;
		private Uri _url;

		// Hashtable for element names to update in form
		//private StringCollection updateElementNames = new StringCollection();

		/// <summary>
		/// Creates a new SoftTestCommand.
		/// </summary>
		public UrlSpiderTestCommand()
		{
			rptBuilder.CanSaveHtml = true;
		}
		/// <summary>
		/// Creates a new UrlSpiderTestCommand.
		/// </summary>
		/// <param name="context"> The context type.</param>
		public UrlSpiderTestCommand(UrlSpiderCommandContext context) : this()
		{
			this.Context = context;
		}
		#region Properties

		/// <summary>
		/// Gets or sets the context.
		/// </summary>
		private UrlSpiderCommandContext Context
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

		/// <summary>
		/// Returns if the session is currently executing the run.
		/// </summary>
		public bool IsRunning
		{
			get
			{
				return _isRunning;
			}
		}
		#endregion
		#region Helpers
		/// <summary>
		/// Get Async get request.
		/// </summary>
		/// <param name="getUrl"> The url.</param>
		/// <param name="values"> The get values.</param>
		/// <param name="cookies"> The cookies.</param>
		/// <param name="state"> The http state.</param>
		private void StartGetRequest(GetForm httpCommand, string getUrl, ArrayList values, CookieCollection cookies, HttpProperties settings, HttpState state)
		{
			try
			{
				httpCommand.StartAsyncHttpGet(
					getUrl,
					settings,
					values,
					cookies,
					state,
					false);
			}
			catch (Exception ex)
			{
				// register and show
				Utils.ExceptionHandler.RegisterException(ex);
				
				AbortSessionRun(ex.Message);
			}
		}

		#endregion
		#region Methods
		/// <summary>
		/// Executes the session.
		/// </summary>
		public void Run()
		{
			this._isRunning = true;

			reports = new ArrayList();

			unitTestPostRequest = new PostForm();
			unitTestGetRequest = new GetForm();

			// Add proxy settings
			unitTestPostRequest.ProxySettings = this.Context.Proxy;
			unitTestGetRequest.ProxySettings = this.Context.Proxy;

			// http settings
			unitTestPostRequest.ClientSettings = this.Context.ProtocolProperties;
			unitTestGetRequest.ClientSettings = this.Context.ProtocolProperties;
	
			// set events for unit test requests
			unitTestPostRequest.EndHttp += new ResponseCallbackDelegate(UnitTestResult_EndHttp);
			unitTestGetRequest.EndHttp += new ResponseCallbackDelegate(UnitTestResult_EndHttp);
					
			HttpState state = new HttpState();

			// the test sessin request of the safe request.
			state.TestSessionRequest = this.TestSession.SessionRequests[0];
			state.SessionRequestId = 0;
			state.IsLastItem = true;

			ExecuteNextSafeRequest(state);
		}

		/// <summary>
		/// Executes the next safe request in order.
		/// </summary>
		/// <param name="state"> The HTTP state.</param>
		private void ExecuteNextSafeRequest(HttpState state)
		{
			// Get safe session and execute
			Session safeSession = this.SafeSession;
			int k = state.SessionRequestId;

			// http settings
			HttpProperties httpSettings = null;
			if ( safeSession.SessionRequests[k].RequestHttpSettings == null )
			{
				httpSettings = safeSessionPostRequest.ClientSettings;
			} 
			else 
			{
				httpSettings = safeSession.SessionRequests[k].RequestHttpSettings;
			}

			if ( safeSession.SessionRequests[k].RequestType == HttpRequestType.GET )
			{
				GetSessionRequest sessionRequest = (GetSessionRequest)safeSession.SessionRequests[k];

				SessionCommandProcessEventArgs args = new SessionCommandProcessEventArgs("Requesting " + sessionRequest.Url.ToString());
				args.ProcessType = SessionProcessType.SafeRequest;
				this.DisplaySessionProcessEvent(this, args);

				// Request, we only use url, http settings and cookies, because
				// IE doesn't returns and arraylist of values for GET requests.
				StartGetRequest(safeSessionGetRequest,
					sessionRequest.Url.ToString(),
					null,
					sessionRequest.RequestCookies,
					httpSettings,
					state);
			}
		}


		/// <summary>
		/// Executes the next safe request in order. Uses session cookies instead of updating them.
		/// </summary>
		/// <param name="state"> The HTTP state.</param>
		/// <param name="responseBufferData"> The ResponseBuffer.</param>
		private void ExecuteNextSafeRequest(HttpState state, ResponseBuffer responseBufferData)
		{
			
			CookieCollection cookies = null;

			// Get safe session and execute
			Session safeSession = this.SafeSession;
			int k = state.SessionRequestId;

			// http settings
			HttpProperties httpSettings = null;
			if ( safeSession.SessionRequests[k].RequestHttpSettings == null )
			{
				httpSettings = safeSessionPostRequest.ClientSettings;
			} 
			else 
			{
				httpSettings = safeSession.SessionRequests[k].RequestHttpSettings;
			}

			if ( safeSession.SessionRequests[k].RequestType == HttpRequestType.GET )
			{
				
				GetSessionRequest sessionRequest = (GetSessionRequest)safeSession.SessionRequests[k];

				//				// Location is found in Hashtable
				//				if ( responseBufferData.ResponseHeaderCollection.ContainsKey("Location") )
				//				{
				//					#region Redirect
				//					// Location is not empty
				//					if ( ((string)responseBufferData.ResponseHeaderCollection["Location"])!=String.Empty )
				//					{	
				//						string location = (string)responseBufferData.ResponseHeaderCollection["Location"];
				//						Uri url = (Uri)responseBufferData.ResponseHeaderCollection["Response Uri"];
				//
				//						// Get redirect uri
				//						string redirectUri = UriResolver.ResolveUrl(url,location);
				//
				//						if ( this.SafeSession.IsCookieUpdatable )
				//						{
				//							// get cookies from cookie manager
				//							cookies = cookieManager.GetCookies(new Uri(redirectUri));
				//						} 
				//						else 
				//						{
				//							// else use saved cookies
				//							cookies = sessionRequest.RequestCookies;
				//						}
				//
				//						this.ApplyUrlRedirection(state, cookies,httpSettings, redirectUri);
				//					}
				//					#endregion
				//				} 
				//				else 
				//				{
				#region Get Request
				// get cookies.
				if ( this.SafeSession.IsCookieUpdatable )
				{
					// get cookies from cookie manager
					cookies = cookieManager.GetCookies(sessionRequest.Url);
				} 
				else 
				{
					// else use saved cookies
					cookies = sessionRequest.RequestCookies;
				}

				this.DisplaySessionProcessEvent(this, new SessionCommandProcessEventArgs("Requesting " + sessionRequest.Url.ToString()));

				// Request, we only use url, http settings and cookies, because
				// IE doesn't returns and arraylist of values for GET requests.
				this.StartGetRequest(safeSessionGetRequest,
					sessionRequest.Url.ToString(),
					null,
					cookies,
					httpSettings,
					state);
				#endregion
				//}				
			}
			else 
			{
				#region Post Request
				PostSessionRequest sessionRequest = (PostSessionRequest)safeSession.SessionRequests[k];

				// Post url 
				string postUrl = String.Empty;
			
				#region Update post url
				//if ( this.TestSession.SessionRequests[k].UpdateSessionUrl )
				//{
				Uri postUri = null;

				// if response headers null, use from response buffer
				if ( sessionRequest.ResponseHeaders == null )
				{
					postUri = (Uri)responseBufferData.ResponseHeaderCollection["Response Uri"];
				} 
				else 
				{
					// if it has uri
					if ( sessionRequest.ResponseHeaders.ContainsKey("Response Uri") )
					{
						if ( sessionRequest.ResponseHeaders["Response Uri"] != null )
						{
							postUri = (Uri)sessionRequest.ResponseHeaders["Response Uri"];
						}
					}		
				}	

				string s = string.Empty;

				if ( postUri.Query.Length > 0 )
				{
					s = postUri.AbsoluteUri.Replace(postUri.Query,"");
				} 
				else 
				{
					s = postUri.AbsoluteUri;
				}
				
				string action = parser.GetFormActionByAbsoluteUrl(s, responseBufferData.HttpBody);
						
				if ( action == "" )
				{
					postUrl = s;
				} 
				else 
				{
					// Resolve url
					postUrl = UriResolver.ResolveUrl(postUri,action);
				}
				//}
				#endregion
	
				// get cookies.
				if ( this.SafeSession.IsCookieUpdatable )
				{
					// get cookies from cookie manager
					cookies = cookieManager.GetCookies(new Uri(postUrl));
				} 
				else 
				{
					// else use saved cookies
					cookies = sessionRequest.RequestCookies;
				}				

				SessionCommandProcessEventArgs args = new SessionCommandProcessEventArgs("Requesting " + postUrl);

				// Check form for updates session values
				// Convert form
				ArrayList listValues = parser.ConvertToArrayList(sessionRequest.Form, responseBufferData.HttpBody);

				// Display posted values
				string posted = ConvertToPostDataString(listValues);
				args.Detail = "Posted data: " + posted;
				args.ProcessType = SessionProcessType.SafeRequest;
				this.DisplaySessionProcessEvent(this, args);

				// Request post
				this.StartPostRequest(safeSessionPostRequest,
					postUrl, 
					listValues,
					cookies, 
					httpSettings,
					state);

				#endregion
			}
		}


		/// <summary>
		/// Redirects to url.
		/// </summary>
		/// <param name="state"> The HTTP State.</param>
		/// <param name="cookies"> The cookie collection.</param>
		/// <param name="settings"> The HTTP Settings.</param>
		/// <param name="url"> The url to redirect to.</param>
		private void ApplyUrlRedirection(HttpState state, CookieCollection cookies,HttpProperties settings, string url)
		{	
			this.DisplaySessionProcessEvent(this, new SessionCommandProcessEventArgs("Requesting " + url));

			// Request, we only use url, http settings and cookies, because
			// IE doesn't returns and arraylist of values for GET requests.
			this.StartGetRequest(safeSessionGetRequest,url,null, cookies, settings, state);
		}


		/// <summary>
		/// Gets the meta tag url from http content.
		/// </summary>
		/// <param name="httpResponse"> The response buffer.</param>
		/// <param name="url"> The response uri.</param>
		/// <returns> An url found in the META tag.</returns>
		private Uri GetMetaTagUrl(ResponseBuffer httpResponse, Uri url)
		{
			// check if html content has any META tag.
			string metaUrl = parser.GetMetaRedirectUrlString(httpResponse.HttpBody);
			Uri newUrl = null;

			if ( metaUrl.Length > 0 )
			{
				if ( metaUrl.ToLower().StartsWith("http") )
				{
					try
					{
						newUrl = new Uri(metaUrl);
					}
					catch
					{								
						newUrl = url;
					}
				} 
				else 
				{
					// make relative
					newUrl = new Uri(UriResolver.ResolveUrl(url, metaUrl));
				}
			}

			return newUrl;
		}

		/// <summary>
		/// Returns the avaiable tests in a session.
		/// </summary>
		/// <returns> The test count for a session.</returns>
		public int AvailableTests()
		{
			int availableTests=0;
			// get tests count
			for (int i=0;i<this.TestSession.SessionRequests.Count;i++)
			{
				SessionRequest sr = this.TestSession.SessionRequests[i];
				availableTests += sr.WebUnitTest.Tests.Count;
			}

			return availableTests;
		}

		/// <summary>
		/// Apply the test requests for a session request.
		/// </summary>
		/// <param name="sessionRequest"> The session request.</param>
		/// <param name="result"> The response buffer result from the safe session.</param>
		private void ApplyRequestTests(SessionRequest sessionRequest, ResponseBuffer result)
		{			
			UnitTestItem unitTestItem = sessionRequest.WebUnitTest;
			unitTestItem.Form = sessionRequest.Form;

			CookieCollection cookies = null;

			//int availableTests = this.AvailableTests();
			//bool lastItem = false;
			string requestUrl = sessionRequest.Url.ToString();
			
			#region Run each test in SessionRequest WebUnitTestItem

			// run each test in Form
			foreach (DictionaryEntry de in unitTestItem.Tests)
			{				
				Test test = (Test)de.Value;
				ArrayList values = new ArrayList();

				// get cookies
				cookies = cookieManager.GetCookies(sessionRequest.Url);

				// set current test index
				unitTestItem.SelectedTestIndex = unitTestItem.Tests.IndexOfValue(test);

				// create SessionCommandProcessEventArgs
				SessionCommandProcessEventArgs args = new SessionCommandProcessEventArgs("Applying test '" + test.Name + "' to " + sessionRequest.Url.ToString());
				args.ProcessType = SessionProcessType.TestRequest;

				#region Apply Test
				// --------------------------------------------------------------------------------
				// Process data			
				// Html Form Tag
				if ( test.UnitTestDataType == UnitTestDataContainer.HtmlFormTag )
				{
					// is a form tag
					// apply test to form
					HtmlFormTag filledForm = ApplyTestToForm(test, sessionRequest.Form.CloneTag());					
					values = parser.ConvertToArrayList(filledForm, result.HttpBody, updateElementNames);
				} 
				// Post Data Hashtable
				if ( test.UnitTestDataType == UnitTestDataContainer.PostDataHashtable )
				{
					string postdata = ((PostSessionRequest)sessionRequest).PostData;

					// convert post data to hashtable
					FormConverter converter = new FormConverter();
					Hashtable postDataHash = converter.ConvertPostDataString(postdata);

					// Applies test to post data hashtable
					Hashtable filledPostData = ApplyTestToPostData(test, (Hashtable)postDataHash.Clone());
					values = converter.ConvertPostDataArrayList(filledPostData);
				}
				// Cookies
				if ( test.UnitTestDataType == UnitTestDataContainer.Cookies )
				{
					cookies = ApplyTestToCookies(test, cookies);
				}
				// Url
				if( test.UnitTestDataType == UnitTestDataContainer.NoPostData )
				{
					// a url test
					requestUrl = ApplyTestToUrl(test, WebServerUriType.Normal,sessionRequest.Url).ToString();
				}
				// -----------------------------------------------------------------------------------
				#endregion

				if ( (test.UnitTestDataType == UnitTestDataContainer.HtmlFormTag ) || ( test.UnitTestDataType == UnitTestDataContainer.PostDataHashtable ) )
				{
					// Set post data for report
					test.Arguments.PostData = ConvertToPostDataString(values);
					args.Detail = "Posted Data:" + test.Arguments.PostData;
				}
				if ( test.UnitTestDataType == UnitTestDataContainer.NoPostData )
				{
					args.Detail = "Url query:" + requestUrl;
				}
				if ( test.UnitTestDataType == UnitTestDataContainer.Cookies )
				{
					StringBuilder cookieQuery = new StringBuilder();

					foreach ( Cookie cky in cookies )
					{
						cookieQuery.Append("Name:" + cky.Name);
						cookieQuery.Append(", ");
						cookieQuery.Append("Value:" + cky.Value);
						cookieQuery.Append(";");
					}

					args.Detail = "Cookie:" + cookieQuery.ToString();
				}
																					   
				//				// set last item flag
				//				if ( availableTests == 1)
				//				{
				//					lastItem = true;
				//				}
				
				// display the current processing
				this.DisplaySessionProcessEvent(this,args);

				// clone test item and set last item value
				HttpState httpRequestState = new HttpState();
				httpRequestState.TestItem = unitTestItem.Clone();
				//httpRequestState.IsLastItem = lastItem;
				
				
				// http settings
				HttpProperties httpSettings = null;
				if ( sessionRequest.RequestHttpSettings == null )
				{
					httpSettings = unitTestGetRequest.ClientSettings;
				} 
				else 
				{
					httpSettings = sessionRequest.RequestHttpSettings;
				}

				if ( sessionRequest.RequestType == HttpRequestType.GET )
				{
					// get request
					this.StartGetRequest(unitTestGetRequest,
						requestUrl,
						null,
						cookies,
						httpSettings,
						httpRequestState);
				} 
				else 
				{
					// post request
					this.StartPostRequest(unitTestPostRequest,
						requestUrl,
						values,
						cookies,
						httpSettings,
						httpRequestState);
				}

				//availableTests--;
			}
			#endregion

		}

		/// <summary>
		/// Convert to post data string.
		/// </summary>
		/// <param name="al"> The ArrayList to convert.</param>
		/// <returns> A string with the post data.</returns>
		private string ConvertToPostDataString(ArrayList al)
		{
			// set posted data
			StringBuilder postDataBuffer = new StringBuilder();

			postDataBuffer.Append("?");
			for (int k=0;k<al.Count;k++)
			{
				postDataBuffer.Append(al[k]);
				postDataBuffer.Append("&");
			}

			return postDataBuffer.ToString();
		}
		
		#endregion
		#region Callback events for UnitTestResult and SafeSessionResult
		/// <summary>
		/// Returns the result from the unit test requests.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The ResponseEventArgs.</param>
		private void UnitTestResult_EndHttp(object sender, ResponseEventArgs e)
		{
			if ( IsRunning )
			{
				try
				{
					// Check test result
					Test test = e.State.TestItem.Tests.GetByIndex(e.State.TestItem.SelectedTestIndex);
					UnitTestResult testResult = asdeCommand.CheckTestResult(e.Response, test);

					// Create SessionCommandProcessEventArgs
					SessionCommandProcessEventArgs args = new SessionCommandProcessEventArgs("Test Result for '" + test.Name + "'");
					args.ProcessType = SessionProcessType.TestResultOk;
					args.Detail = "Severity Level: " + testResult.SeverityLevel.ToString();
					this.DisplaySessionProcessEvent(this, args);

					// Add response to report
					HtmlUnitTestReport report = rptBuilder.BuildReport(e, testResult);
					reports.Add(report);
				}
				catch (Exception ex)
				{
					Utils.ExceptionHandler.RegisterException(ex);
					AbortSessionRun(ex.Message);
				}
			} 
			else 
			{
				unitTestGetRequest.SafeAbortRequest();
				unitTestPostRequest.SafeAbortRequest();
			}
		}


		/// <summary>
		/// Returns the result from the safe requests.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The ResponseEventArgs.</param>
		private void SafeSessionResult_EndHttp(object sender, ResponseEventArgs e)
		{			

			// if is not running, then exit ...
			if ( this.IsRunning )
			{
				// on return, run each test
				SessionRequest testRequest = e.State.TestSessionRequest;
			
				// add cookies to cookie manager
				cookieManager.AddCookies(e.Response.CookieCollection);

				Uri responseUri = (Uri)e.Response.ResponseHeaderCollection["Response Uri"];

				//			// Update Session Headers
				//			if ( ( testRequest.Url.ToString() != responseUri.ToString() ) && ( responseUri != null ) )
				//			{
				//				// Updates the Test Url with the response uri from the safe session
				//				testRequest.Url = responseUri;				
				//			}

				try
				{
					#region Apply Tests and execute safe requests
					if ( testRequest.WebUnitTest != null )
					{
						if ( testRequest.WebUnitTest.Tests != null )
						{
							#region Apply tests
							if ( ( testRequest.WebUnitTest.Tests.Count > 0 ) )
							{
								// matchId > currentRestart
								// apply tests
								ApplyRequestTests(testRequest, e.Response);
							}
							#endregion

							// add safe test report
							// HtmlUnitTestReport report = rptBuilder.BuildReport(e);
							// reports.Add(report);

							HttpState state = new HttpState();			
							state.SessionRequestId = e.State.SessionRequestId + 1;

							// if equal, then we stop to request and enabled the report.
							if ( state.SessionRequestId == this.SafeSession.SessionRequests.Count )
							{
								// end
								// show report
								UnitTestSessionReportEventArgs args = new UnitTestSessionReportEventArgs();					
								args.Report = reports;
								this.CreateReportEvent(this, args);
							} 
							else 
							{
								// continue
								state.TestSessionRequest = this.TestSession.SessionRequests[state.SessionRequestId];
								ResponseBuffer inputResponse = e.Response;

								// call safe request backtracking else call next safe request directly
								if ( this.SafeSession.AllowSafeRequestBacktracking )
								{							
									this.ExecuteNextSafeRequestById(state, inputResponse, state.SessionRequestId);
								} 
								else 
								{
									this.ExecuteNextSafeRequest(state, inputResponse);
								}
							}
						}					
					}
					#endregion
				}
				catch ( WebException web)
				{
					Utils.ExceptionHandler.RegisterException(web);

					AbortSessionRun(web.Message);
				}
				catch (Exception ex)
				{
					Utils.ExceptionHandler.RegisterException(ex);

					AbortSessionRun(ex.Message);
				}
			} 
			else 
			{
				// abort requests
				this.safeSessionGetRequest.SafeAbortRequest();
				this.safeSessionPostRequest.SafeAbortRequest();
			}
		}
		#endregion
		#region Abort Methods
		/// <summary>
		/// Aborts the session and updates the pending reports.
		/// </summary>
		/// <param name="message"> The message to show.</param>
		private void AbortSessionRun(string message)
		{
			this._isRunning = false;

			if ( reports.Count > 0 )
			{
				// show report
				UnitTestSessionReportEventArgs args = new UnitTestSessionReportEventArgs();					
				args.Report = reports;
				this.CreateReportEvent(this, args);
			}

			SessionAbortEventArgs sargs = new SessionAbortEventArgs();
			sargs.ErrorMessage = message;
			this.SessionAbortedEvent(this, sargs);
		}

		/// <summary>
		/// Stops the session.
		/// </summary>
		public override void Stop()
		{
			base.Stop();

			this._isRunning = false;

			SessionAbortEventArgs sargs = new SessionAbortEventArgs();
			sargs.ErrorMessage = "Session Run aborted by user.";
			this.SessionAbortedEvent(this, sargs);
		}

		#endregion
	}
}