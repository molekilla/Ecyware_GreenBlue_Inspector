// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Net;
using System.Text;
using System.Collections;
using Ecyware.GreenBlue.Protocols.Http;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Engine.HtmlCommand;
using Ecyware.GreenBlue.WebUnitTestManager;
using Ecyware.GreenBlue.ReportEngine;


namespace Ecyware.GreenBlue.WebUnitTestCommand
{
	/// <summary>
	///  Contains logic for processing quick tests.
	/// </summary>
	public class SoftTestCommand : UnitTestCommand, IUnitTestCommand
	{
		private AsdeCommand asdeCommand = new AsdeCommand();
		private ReportBuilder rptBuilder = new ReportBuilder();
		public event DisplayProcessEventHandler DisplayProcessEvent;
		public event UnitTestSessionReportEventHandler CreateReportEvent;

		private bool _isRunning = false;
		private HttpProxy _proxy = null;
		private HttpProperties _httpProperties = null;

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
		public SoftTestCommand()
		{
			rptBuilder.CanSaveHtml = true;
		}
		/// <summary>
		/// Creates a new SoftTestCommand.
		/// </summary>
		/// <param name="url"> The url to apply the test.</param>
		/// <param name="form"> The form to apply the tests.</param>
		/// <param name="proxySettings"> The http proxy settings.</param>
		/// <param name="httpProperties"> The http settings.</param>
		/// <param name="sqlTest"> The sql test to use.</param>
		/// <param name="xssTest"> The xss test to use.</param>
		/// <param name="bufferLength"> The buffer overflow length to use.</param>
		public SoftTestCommand(Uri url,
			HtmlFormTag form,
			HttpProxy proxySettings,
			HttpProperties httpProperties,
			string sqlTest,
			string xssTest,
			int bufferLength) : this()
		{

			this._xssSignature = xssTest;
			this._sqlSignature = sqlTest;
			this._bufferLen = bufferLength;

			this.Url = url;
			this.FormTag = form;
			this.Proxy = proxySettings;
			this.ProtocolProperties = httpProperties;

//			updateElementNames.Add("__VIEWSTATE");
//			updateElementNames.Add("__EVENTTARGET");
		}
		#region Properties
		/// <summary>
		/// Gets or sets the url.
		/// </summary>
		private Uri Url
		{
			get
			{
				return _url;
			}
			set
			{
				_url = value;
			}
		}
		/// <summary>
		/// Gets or sets the HtmlFormTag.
		/// </summary>
		private HtmlFormTag FormTag
		{
			get
			{
				return _formTag;
			}
			set
			{
				_formTag = value;
			}
		}
		/// <summary>
		/// Gets or sets the buffer test check.
		/// </summary>
		public bool BufferTest
		{
			get
			{
				return _bufferTest;
			}
			set
			{
				_bufferTest = value;
			}
		}
		/// <summary>
		/// Gets or sets the XSS test check.
		/// </summary>
		public bool XssTest
		{
			get
			{
				return _xssTest;
			}
			set
			{
				_xssTest = value;
			}
		}
		/// <summary>
		/// Gets or sets the SQL Injection test check.
		/// </summary>
		public bool SqlTest
		{
			get
			{
				return _sqlTest;
			}
			set
			{
				_sqlTest = value;
			}
		}
		/// <summary>
		/// Gets or sets the HttpProperties.
		/// </summary>
		public HttpProperties ProtocolProperties
		{
			get
			{
				return _httpProperties;
			}
			set
			{
				_httpProperties = value;
			}
		}

		/// <summary>
		/// Gets or sets the HttpProxy.
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

		#region Processing Methods
		/// <summary>
		/// Runs the command.
		/// </summary>
		public void Run()
		{			
			this._isRunning = true;

			postRequest = new PostForm();
			getRequest = new GetForm();

			postRequest.EndHttp += new ResponseCallbackDelegate(httpResponse_EndHttp);
			getRequest.EndHttp += new ResponseCallbackDelegate(httpResponse_EndHttp);

			reports = new ArrayList();

			TestCollection tests = GetTests();

			UnitTestItem testItem = new UnitTestItem(FormTag, tests);

			int availableTests = tests.Count;
			bool lastItem = false;

			#region Run each test in UnitTestItem

				// run each test in Form
				foreach (DictionaryEntry de in tests)
				{
					Test test = (Test)de.Value;

					// apply test to form
					HtmlFormTag filledForm = ApplyTestToForm(test, FormTag.CloneTag());

					// set current test index
					testItem.SelectedTestIndex = testItem.Tests.IndexOfValue(test);

					// resolve uri
					string url = UriResolver.ResolveUrl(this.Url,filledForm.Action);

					// convert to array list
					// TODO: Send HTML Source for bypassing fields. Will be needed for ASP.NET testing.
					ArrayList al = parser.GetArrayList(filledForm);

					// set posted data
					StringBuilder postDataBuffer = new StringBuilder();

					postDataBuffer.Append("?");
					for (int k=0;k<al.Count;k++)
					{
						postDataBuffer.Append(al[k]);
						postDataBuffer.Append("&");
					}

					test.Arguments.PostData = postDataBuffer.ToString();

					// set last item flag
					if ( availableTests == 1)
					{
						lastItem = true;
					}

					CookieManager cookieManager = new CookieManager();
					CookieCollection cookies = cookieManager.GetCookies(new Uri(url));

					HttpState httpRequestState = new HttpState();
					httpRequestState.TestItem = testItem.Clone();
					httpRequestState.IsLastItem = lastItem;

					if ( filledForm.Method.ToLower(System.Globalization.CultureInfo.InvariantCulture) == "get" )
					{

						getRequest.StartAsyncHttpGet(
							url,
							this.ProtocolProperties,
							al,
							cookies,
							httpRequestState,
							false);

					} 
					else 
					{
						postRequest.StartAsyncHttpPost(
							url,
							this.ProtocolProperties,
							al,
							cookies,
							httpRequestState);
					}

					availableTests--;
				}
				#endregion
		}

		/// <summary>
		/// Creates the easy test for the command.
		/// </summary>
		/// <returns> A test collection</returns>
		private TestCollection GetTests()
		{
			TestCollection tests = new TestCollection();
			if ( BufferTest )
			{
				Test bufferTest = new Test();
				bufferTest.Name = "EasyBufferTest";
				bufferTest.TestType = UnitTestType.BufferOverflow;

				if ( this._bufferLen != 0 )
				{
					bufferTest.Arguments = new BufferOverflowTesterArgs(this._bufferLen);
				} 
				else 
				{
					bufferTest.Arguments = new BufferOverflowTesterArgs(300);
				}
				tests.Add(bufferTest.Name, bufferTest);
			}

			if ( XssTest )
			{
				Test xssTest = new Test();
				xssTest.Name = "EasyXSSTest";
				xssTest.TestType = UnitTestType.XSS;

				if ( this._xssSignature.Length > 0 )
				{
					xssTest.Arguments = new XssInjectionTesterArgs(this._xssSignature);
				}
				else 
				{
					xssTest.Arguments = new XssInjectionTesterArgs("<script>alert('XSS Test: You have been hacked!');</script>");
				}
				tests.Add(xssTest.Name, xssTest);
			}

			if ( SqlTest )
			{
				Test sqlTest = new Test();
				sqlTest.Name = "EasySQLTest";
				sqlTest.TestType = UnitTestType.SqlInjection;

				if ( this._sqlSignature.Length > 0 )
				{
					sqlTest.Arguments = new SqlInjectionTesterArgs(this._sqlSignature);
				} 
				else 
				{
					sqlTest.Arguments = new SqlInjectionTesterArgs("'1=1 --");
				}
				tests.Add(sqlTest.Name, sqlTest);
			}

			return tests;
		}

		#endregion

		#region Callback events from Http
		/// <summary>
		/// Returns the result from the unit test execution.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The ResponseEventArgs type.</param>
		private void httpResponse_EndHttp(object sender,ResponseEventArgs e)
		{
			if ( e != null)
			{
				this.DisplayProcessEvent(this, e);

				HttpState state = (HttpState)e.State;

				// check test result
				Test test = state.TestItem.Tests.GetByIndex(((HttpState)e.State).TestItem.SelectedTestIndex);
				UnitTestResult testResult = asdeCommand.CheckTestResult(e.Response, test);

				// add response to report
				HtmlUnitTestReport report = rptBuilder.BuildReport(e, testResult);
				reports.Add(report);

				if ( state.IsLastItem )
				{
					this._isRunning = false;

					// show report
					UnitTestSessionReportEventArgs args = new UnitTestSessionReportEventArgs();					
					args.Report = reports;
					this.CreateReportEvent(this,args);
				}
			}
		}
		#endregion
	}
}