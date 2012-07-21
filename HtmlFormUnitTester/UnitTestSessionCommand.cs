using System;
using System.Net;
using System.Text;
using System.Collections;
using Ecyware.GreenBlue.HtmlDom;
using Ecyware.GreenBlue.Protocols.Http;
using Ecyware.GreenBlue.HtmlProcessor;
using Ecyware.GreenBlue.WebUnitTestManager;
using Ecyware.GreenBlue.ReportEngine;


namespace Ecyware.GreenBlue.WebUnitTestCommand
{
	/// <summary>
	/// Summary description for UnitTestSessionProcessor.
	/// </summary>
	public class UnitTestSessionProcessor
	{
		public event DisplayProcessEventHandler DisplayProcessEvent;
		public event UnitTestSessionReportEventHandler CreateReportEvent;

		private UnitTestSession _uts = null;
		private HttpProxy _proxy = null;
		private HttpProperties _httpProperties = null;

		private PostForm postRequest = null;
		private GetForm getRequest = null;
		private HtmlParser parser = new HtmlParser();
		private ArrayList reports = null;

		public UnitTestSessionProcessor(UnitTestSession uts, HttpProxy proxySettings,HttpProperties httpProperties)
		{
			this.CurrentUnitTestSession = uts;
			this.Proxy = proxySettings;
			this.ProtocolProperties = httpProperties;
		}
		#region Properties
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
		public UnitTestSession CurrentUnitTestSession
		{
			get
			{
				return _uts;
			}
			set
			{
				_uts = value;
			}
		}

		#endregion

		#region Processing Methods
		// TODO: Still have to check this function, if is working as intended
		public void Run()
		{
			postRequest = new PostForm();
			getRequest = new GetForm();

			postRequest.EndHttp += new ResponseCallbackDelegate(httpResponse_EndHttp);
			getRequest.EndHttp += new ResponseCallbackDelegate(httpResponse_EndHttp);

			reports = new ArrayList();
			UnitTestSession session = this.CurrentUnitTestSession;			
			int availableTests = session.AvailableTests();
			bool lastItem = false;

			// get tests count
			for (int i=0;i<session.UnitTestForms.Count;i++)
			{
				UnitTestItem testItem = session.UnitTestForms.GetByIndex(i);
				HtmlFormTag form = testItem.Form;

				#region Run each test in UnitTestItem
				// run each test in Form
				foreach (DictionaryEntry de in testItem.Tests)
				{
					Test test = (Test)de.Value;

					// apply test to form
					HtmlFormTag filledForm = ApplyTestToForm(test,form.CloneTag());

					// set current test index
					testItem.SelectedTestIndex = testItem.Tests.IndexOfValue(test);

					// get reponse uri
					Uri uri = (Uri)this.CurrentUnitTestSession.SessionData.ResponseHeaderCollection["Response Uri"];

					// resolve uri
					string url = UriResolver.ResolveUrl(uri,filledForm.Action);

					// process special fields
					// filledForm = parser.ProcessSpecialFields(filledForm);

					// convert to array list
					ArrayList al = parser.ConvertToArrayList(filledForm);

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

					if ( filledForm.Method.ToLower(System.Globalization.CultureInfo.InvariantCulture) == "get" )
					{
						getRequest.StartAsyncHttpGet(
							url,
							this.ProtocolProperties,
							al,
							cookies,
							testItem.Clone(),
							lastItem);

					} else {
						postRequest.StartAsyncHttpPost(
							url,
							this.ProtocolProperties,
							al,
							cookies,
							testItem.Clone(),
							lastItem);
					}

					availableTests--;
				}
				#endregion
			}
		}

		private HtmlFormTag ApplyTestToForm(Test t, HtmlFormTag f)
		{
			UnitTester tester = new UnitTester(t.Arguments);
			return tester.BuildUnitTest(t.TestType,f);
		}
		#endregion

		#region Callback events from Http
		private void httpResponse_EndHttp(object sender,ResponseEventArgs e)
		{
			if ( e == null) return;
			// TODO: Use Invoke
				this.DisplayProcessEvent(this, e);

				// add response to report
				ReportBuilder rptBuilder = new ReportBuilder();
				HtmlUnitTestReport report = rptBuilder.BuildReport(e);
				reports.Add(report);

				if ( e.IsSessionLastItem )
				{
					// show report
					UnitTestSessionReportEventArgs args = new UnitTestSessionReportEventArgs();					
					args.Report = reports;
					this.CreateReportEvent(this,args);
				}
		}
		#endregion

//		/// <summary>
//		/// Resolves uri from an ACTION value.
//		/// </summary>
//		/// <param name="uri"> Uri type.</param>
//		/// <param name="urlString"> Url string from ACTION.</param>
//		/// <returns></returns>
//		// TODO: Change ResolveUri Logic
//		private string ResolveUri(Uri uri, string urlString)
//		{
//			UriBuilder ub=null;
//
//			if ( (urlString.Length > 6) && (urlString.Substring(0,4)=="http") )
//			{
//				ub=new UriBuilder(urlString);
//
//				// TODO: Show a message form asking for input
//			} 
//			else 
//			{
//				ub = new UriBuilder(uri);
//
//				string s=String.Empty;
//				// segments - 1
//				for (int i=0;i<uri.Segments.Length-1;i++)
//				{
//					s+=uri.Segments[i];
//				}
//
//				// concanate with authority + base path
//				if ( urlString.Substring(0,1)==@"/" )
//				{
//					//urlString = urlString.Substring(1,urlString.Length-1);
//					ub.Path=urlString;
//				} 
//				else 
//				{
//					ub.Query="";
//					ub.Path= s + urlString;
//				}
//				
//			}
//
//			return ub.ToString();
//		}

	}
}
