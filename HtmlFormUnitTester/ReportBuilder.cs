// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.IO;
using System.Collections;
using System.Net;
using System.Xml.Serialization;
using Ecyware.GreenBlue.WebUnitTestManager;
using Ecyware.GreenBlue.ReportEngine;
using Ecyware.GreenBlue.Protocols.Http;
using Ecyware.GreenBlue.Utils;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Engine.HtmlCommand;

namespace Ecyware.GreenBlue.WebUnitTestCommand
{
	/// <summary>
	/// Creates the application reports.
	/// </summary>
	public class ReportBuilder
	{
		private bool _saveHtml = false;
		private ArrayList hiddenHeaders = null;

		/// <summary>
		/// Creates a new ReportBuilder.
		/// </summary>
		public ReportBuilder()
		{
			hiddenHeaders = new ArrayList();
			hiddenHeaders.Add("host");
			hiddenHeaders.Add("connection");
			hiddenHeaders.Add("user-agent");
			hiddenHeaders.Add("status code");
			hiddenHeaders.Add("status description");
		}

		/// <summary>
		/// Creates a new ReportBuilder.
		/// </summary>
		/// <param name="saveHtml"> Saves the html as files in report temp folder.</param>
		public ReportBuilder(bool saveHtml)
		{
			this.CanSaveHtml = saveHtml;
		}

		/// <summary>
		/// Gets or sets the save as html file setting.
		/// </summary>
		public bool CanSaveHtml
		{
			get
			{
				return _saveHtml;
			}
			set
			{
				_saveHtml = value;
			}

		}


		/// <summary>
		/// Creates a new report from the response event args.
		/// </summary>
		/// <param name="response"> The response event args type.</param>
		/// <returns> A HtmlUnitTestReport type.</returns>
		public HtmlUnitTestReport BuildReport(ResponseEventArgs response)
		{
			HtmlUnitTestReport reportDataSet = new HtmlUnitTestReport();
			
			HtmlUnitTestReport.ResponseDocumentRow responseDocumentRow = AddReportDocumentRow(reportDataSet,response.Response);

			if ( response.Response.ErrorMessage.Length == 0 )
			{			

				if ( ((HttpState)response.State).TestSessionRequest != null )
				{
					// add request headers with additional http properties
					AddRequestHeaders(responseDocumentRow, reportDataSet, response.Response, ((HttpState)response.State).TestSessionRequest.RequestHttpSettings);
				} 
				else 
				{
					// add request headers
					AddRequestHeaders(responseDocumentRow, reportDataSet, response.Response);
				}
				AddResponseHeaders(responseDocumentRow, reportDataSet, response.Response);
				AddCookies(responseDocumentRow, reportDataSet, response.Response);

				HtmlUnitTestReport.TestItemRow testItemRow = AddTestItemRow(responseDocumentRow,reportDataSet,((HttpState)response.State).TestItem);
				if ( ((HttpState)response.State).TestItem != null )
				{
					AddFormTag(testItemRow,reportDataSet,((HttpState)response.State).TestItem);
					AddTest(testItemRow,reportDataSet,((HttpState)response.State).TestItem);
				}
			}

			return reportDataSet;
		}


		/// <summary>
		/// Creates a new report from the response event args and unit test result.
		/// </summary>
		/// <param name="response"> The response event args.</param>
		/// <param name="unitTestResult"> The unit test result.</param>
		/// <returns> A HtmlUnitTestReport type.</returns>
		public HtmlUnitTestReport BuildReport(ResponseEventArgs response, UnitTestResult unitTestResult)
		{
			HtmlUnitTestReport report = this.BuildReport(response);
			report.ResponseDocument[0].SeverityLevel = unitTestResult.SeverityLevel.ToString();
			report.ResponseDocument[0].SolutionId = unitTestResult.SolutionId.ToString();
			//report.ResponseDocument[0].ReferenceId = unitTestResult.SolutionId.ToString();

			return report;
		}

		/// <summary>
		/// Adds the request headers to the report.
		/// </summary>
		/// <param name="parentRow"> The ResponseDocumentRow.</param>
		/// <param name="report"> The HtmlUnitTestReport type.</param>
		/// <param name="responseBuffer"> The response buffer.</param>
		/// <param name="httpProperties"> The Http Properties type.</param>
		private void AddRequestHeaders(HtmlUnitTestReport.ResponseDocumentRow parentRow,HtmlUnitTestReport report, ResponseBuffer responseBuffer, HttpProperties httpProperties)
		{
			Ecyware.GreenBlue.ReportEngine.HtmlUnitTestReport.RequestHeaderRow reqHeaderRow = report.RequestHeader.NewRequestHeaderRow();

			reqHeaderRow.SetParentRow(parentRow);
			report.RequestHeader.AddRequestHeaderRow(reqHeaderRow);

			Ecyware.GreenBlue.ReportEngine.HtmlUnitTestReport.RequestItemsRow row = null;

			if ( httpProperties.Accept != null )
			{
				row = report.RequestItems.NewRequestItemsRow();					
				row.Name = "Accept";
				row.Value = httpProperties.Accept;
				row.SetParentRow(reqHeaderRow);
				report.RequestItems.AddRequestItemsRow(row);
			}

			row = report.RequestItems.NewRequestItemsRow();
			row.Name = "Content length";
			row.Value = httpProperties.ContentLength.ToString();
			row.SetParentRow(reqHeaderRow);
			report.RequestItems.AddRequestItemsRow(row);
			


			if ( httpProperties.ContentType != null )
			{
				row = report.RequestItems.NewRequestItemsRow();
				row.Name = "Content type";
				row.Value = httpProperties.ContentType;
				row.SetParentRow(reqHeaderRow);
				report.RequestItems.AddRequestItemsRow(row);
			}

			row = report.RequestItems.NewRequestItemsRow();
			row.Name = "If modified since";
			row.Value = httpProperties.IfModifiedSince.ToString();
			row.SetParentRow(reqHeaderRow);
			report.RequestItems.AddRequestItemsRow(row);
			

			row = report.RequestItems.NewRequestItemsRow();
			row.Name = "Keep alive";
			row.Value = httpProperties.KeepAlive.ToString();
			row.SetParentRow(reqHeaderRow);
			report.RequestItems.AddRequestItemsRow(row);
			

			if ( httpProperties.MediaType != null )
			{
				row = report.RequestItems.NewRequestItemsRow();
				row.Name = "Media type";
				row.Value = httpProperties.MediaType;
				row.SetParentRow(reqHeaderRow);
				report.RequestItems.AddRequestItemsRow(row);
			}

			row = report.RequestItems.NewRequestItemsRow();
			row.Name = "Pipeline";
			row.Value = httpProperties.Pipeline.ToString();
			row.SetParentRow(reqHeaderRow);
			report.RequestItems.AddRequestItemsRow(row);

			if ( httpProperties.Referer != null )
			{
				row = report.RequestItems.NewRequestItemsRow();
				row.Name = "Referer";
				row.Value = httpProperties.Referer;
				row.SetParentRow(reqHeaderRow);
				report.RequestItems.AddRequestItemsRow(row);
			}

			row = report.RequestItems.NewRequestItemsRow();
			row.Name = "Send chunked";
			row.Value = httpProperties.SendChunked.ToString();
			row.SetParentRow(reqHeaderRow);
			report.RequestItems.AddRequestItemsRow(row);

			if ( httpProperties.TransferEncoding != null )
			{
				row = report.RequestItems.NewRequestItemsRow();
				row.Name = "Transfer encoding";
				row.Value = httpProperties.TransferEncoding;
				row.SetParentRow(reqHeaderRow);
				report.RequestItems.AddRequestItemsRow(row);
			}

			if ( httpProperties.UserAgent != null )
			{
				row = report.RequestItems.NewRequestItemsRow();
				row.Name = "User agent";
				row.Value = httpProperties.UserAgent;

				row.SetParentRow(reqHeaderRow);
				report.RequestItems.AddRequestItemsRow(row);
			}
		
		}


		/// <summary>
		/// Adds the request headers to the report.
		/// </summary>
		/// <param name="parentRow"> The ResponseDocumentRow.</param>
		/// <param name="report"> The HtmlUnitTestReport type.</param>
		/// <param name="responseBuffer"> The response buffer.</param>
		private void AddRequestHeaders(HtmlUnitTestReport.ResponseDocumentRow parentRow,HtmlUnitTestReport report, ResponseBuffer responseBuffer)
		{
			Ecyware.GreenBlue.ReportEngine.HtmlUnitTestReport.RequestHeaderRow reqHeaderRow = report.RequestHeader.NewRequestHeaderRow();

			reqHeaderRow.SetParentRow(parentRow);
			report.RequestHeader.AddRequestHeaderRow(reqHeaderRow);

			foreach (DictionaryEntry de in responseBuffer.RequestHeaderCollection)
			{								

				Ecyware.GreenBlue.ReportEngine.HtmlUnitTestReport.RequestItemsRow row = report.RequestItems.NewRequestItemsRow();
				
				string name = (string)de.Key;

				if ( !hiddenHeaders.Contains(name.ToLower()) )
				{
					if ( de.Value is Uri )
					{
						row.Name = name;
						row.Value = ((Uri)de.Value).ToString();
					} 
					else 
					{
						row.Name = name;
						if ( de.Value == null )
						{
							row.Value = String.Empty;
						} 
						else 
						{
							row.Value = de.Value.ToString();
						}
					}

					row.SetParentRow(reqHeaderRow);
					report.RequestItems.AddRequestItemsRow(row);
				}
			}
		}


		/// <summary>
		/// Adds the response headers to the report.
		/// </summary>
		/// <param name="parentRow"> The ResponseDocumentRow.</param>
		/// <param name="report"> The HtmlUnitTestReport type.</param>
		/// <param name="responseBuffer"> The response buffer.</param>
		private void AddResponseHeaders(HtmlUnitTestReport.ResponseDocumentRow parentRow,HtmlUnitTestReport report, ResponseBuffer responseBuffer)
		{
			Ecyware.GreenBlue.ReportEngine.HtmlUnitTestReport.ResponseHeaderRow respHeaderRow = report.ResponseHeader.NewResponseHeaderRow();
			respHeaderRow.SetParentRow(parentRow);
			report.ResponseHeader.AddResponseHeaderRow(respHeaderRow);

			foreach (DictionaryEntry de in responseBuffer.ResponseHeaderCollection)
			{				
				Ecyware.GreenBlue.ReportEngine.HtmlUnitTestReport.ResponseItemsRow row = report.ResponseItems.NewResponseItemsRow();

				string name = (string)de.Key;
				if ( !hiddenHeaders.Contains(name.ToLower()) )
				{
					if ( de.Value is Uri )
					{
						row.Name = name;
						row.Value = ((Uri)de.Value).ToString();
					} 
					else 
					{
						row.Name = name;
						if ( de.Value == null )
						{
							row.Value = String.Empty;
						} 
						else 
						{
							row.Value = de.Value.ToString();
						}

					}

					row.SetParentRow(respHeaderRow);
					report.ResponseItems.AddResponseItemsRow(row);
				}
			}


		}

		/// <summary>
		/// Adds the cookies to the report.
		/// </summary>
		/// <param name="parentRow"> The ResponseDocumentRow.</param>
		/// <param name="report"> The HtmlUnitTestReport type.</param>
		/// <param name="responseBuffer"> The response buffer.</param>
		private void AddCookies(HtmlUnitTestReport.ResponseDocumentRow parentRow,HtmlUnitTestReport report, ResponseBuffer responseBuffer)
		{
			foreach (Cookie cookie in responseBuffer.CookieCollection)
			{
//				HttpCookie cookie = (HttpCookie)de.Value;
				Ecyware.GreenBlue.ReportEngine.HtmlUnitTestReport.CookiesRow row = report.Cookies.NewCookiesRow();
				
				row.Domain = cookie.Domain;
				row.Name = cookie.Name;
				row.Path = cookie.Path;
				row.Value = cookie.Value;

				row.SetParentRow(parentRow);
				report.Cookies.AddCookiesRow(row);
			}
		}

		/// <summary>
		/// Adds the report document row to a report.
		/// </summary>
		/// <param name="report"> The HtmlUnitTestReport type.</param>
		/// <param name="responseBuffer"> The response buffer.</param>
		/// <returns> A HtmlUnitTestReport type.</returns>
		private HtmlUnitTestReport.ResponseDocumentRow AddReportDocumentRow(HtmlUnitTestReport report, ResponseBuffer responseBuffer)
		{
			Ecyware.GreenBlue.ReportEngine.HtmlUnitTestReport.ResponseDocumentRow row = report.ResponseDocument.NewResponseDocumentRow();

			row.Date = DateTime.Now;
			row.ErrorMessage = responseBuffer.ErrorMessage;

			if ( this.CanSaveHtml )
			{
				// save html
				row.HtmlResponse = SaveHtml(responseBuffer.HttpBody);
				row.IsHtmlResponseFile = true;
			} 
			else 
			{
				row.HtmlResponse = responseBuffer.HttpBody;
				row.IsHtmlResponseFile = false;
			}

			row.RequestType = responseBuffer.RequestType.ToString();
			row.StatusCode = responseBuffer.StatusCode.ToString();
			row.StatusDescription = responseBuffer.StatusDescription;
			row.Version = responseBuffer.Version;

			report.ResponseDocument.AddResponseDocumentRow(row);

			return row;
		}

		/// <summary>
		/// Adds the tests to the report.
		/// </summary>
		/// <param name="parentRow"> The TestItemRow.</param>
		/// <param name="report"> The HtmlUnitTestReport type.</param>
		/// <param name="unitTestItem"> The UnitTestItem type.</param>
		private void AddTest(HtmlUnitTestReport.TestItemRow parentRow, HtmlUnitTestReport report, UnitTestItem unitTestItem)
		{		
			HtmlUnitTestReport.TestsRow row = report.Tests.NewTestsRow();
			
			Test t = unitTestItem.Tests.GetByIndex(unitTestItem.SelectedTestIndex);

			if ( t.Arguments is BufferOverflowTesterArgs )
			{
				row.BufferLength = ((BufferOverflowTesterArgs)t.Arguments).BufferLength;
			}

			if ( t.Arguments is DataTypesTesterArgs )
			{
				Ecyware.GreenBlue.Engine.DataType dt = ((DataTypesTesterArgs)t.Arguments).SelectedDataType;

				switch ( dt )
				{
					case Ecyware.GreenBlue.Engine.DataType.Character:
						row.DataTypeTest = "Character";
						break;
					case Ecyware.GreenBlue.Engine.DataType.Null:
						row.DataTypeTest = "Null String";
						break;
					case Ecyware.GreenBlue.Engine.DataType.Numeric:
						row.DataTypeTest = "Numeric";
						break;
				}					
			}				

			if ( t.Arguments is SqlInjectionTesterArgs )
			{
				string testValue = ((SqlInjectionTesterArgs)t.Arguments).SqlValue;
				row.TestValue = testValue;
			}

			if ( t.Arguments is XssInjectionTesterArgs )
			{
				string testValue = ((XssInjectionTesterArgs)t.Arguments).XssValue;
				row.TestValue = testValue;
			}				

				
			row.PostData = t.Arguments.PostData;
			row.Name = t.Name;			

			switch ( t.UnitTestDataType )
			{
				case UnitTestDataContainer.Cookies:
					row.PostDataContainer = "Cookies";
					break;
				case UnitTestDataContainer.HtmlFormTag:
					row.PostDataContainer = "Form";
					break;
				case UnitTestDataContainer.NoPostData:
					row.PostDataContainer = "Url";
					break;
				case UnitTestDataContainer.PostDataHashtable:
					row.PostDataContainer = "Post Data";
					break;
			}			

			switch ( t.TestType )
			{
				case UnitTestType.BufferOverflow:
					row.TestType = "Buffer overflow";
					break;
				case UnitTestType.DataTypes:
					row.TestType = "Data type";
					break;
				case UnitTestType.Predefined:
					row.TestType = "Predefined";
					break;
				case UnitTestType.SafeTest:
					row.TestType = "Safe Test";
					break;
				case UnitTestType.SqlInjection:
					row.TestType = "SQL Injection";
					break;
				case UnitTestType.XSS:
					row.TestType = "XSS";
					break;
			}
			row.SetParentRow(parentRow);

			report.Tests.AddTestsRow(row);
		}


		/// <summary>
		/// Adds a HtmlFormTag to a HtmlUnitTestReport.
		/// </summary>
		/// <param name="parentRow"> The TestItemRow.</param>
		/// <param name="report"> The HtmlUnitTestReport type.</param>
		/// <param name="unitTestItem"> The UnitTestItem type.</param>
		private void AddFormTag(HtmlUnitTestReport.TestItemRow parentRow,HtmlUnitTestReport report,UnitTestItem unitTestItem)
		{
			HtmlUnitTestReport.FormRow row = report.Form.NewFormRow();
			

			if ( unitTestItem.Form != null )
			{
				row.Action = unitTestItem.Form.Action;
				row.Enctype = unitTestItem.Form.Enctype;
				row.Method = unitTestItem.Form.Method;
				row.Name = unitTestItem.Form.Name;
				row.SetParentRow(parentRow);

				report.Form.AddFormRow(row);
			}
		}

		
		/// <summary>
		/// Adds a UnitTestItem to a HtmlUnitTestReport.
		/// </summary>
		/// <param name="parentRow"> The TestItemRow.</param>
		/// <param name="report"> The HtmlUnitTestReport type.</param>
		/// <param name="unitTestItem"> The UnitTestItem type.</param>
		/// <returns> A TestItemRow type.</returns>
		private HtmlUnitTestReport.TestItemRow AddTestItemRow(HtmlUnitTestReport.ResponseDocumentRow parentRow,HtmlUnitTestReport report, UnitTestItem unitTestItem)
		{
			HtmlUnitTestReport.TestItemRow row = report.TestItem.NewTestItemRow();

			row.SetParentRow(parentRow);

			report.TestItem.AddTestItemRow(row);

			return row;
		}


		/// <summary>
		/// Saves the html content response.
		/// </summary>
		/// <param name="html"> The html data to save.</param>
		/// <returns> A string containing the file path to the html resource.</returns>
		private string SaveHtml(string html)
		{
			BufferOverflowGenerator bufferGen = new BufferOverflowGenerator();
			
			string fileName = bufferGen.GenerateStringBuffer(10).Replace("\\","mm").Replace("/","nn").Replace("=","M");
			
			// check dir
			if ( !Directory.Exists(System.Windows.Forms.Application.UserAppDataPath + "\\temphtml") )
			{
				Directory.CreateDirectory(System.Windows.Forms.Application.UserAppDataPath + "\\temphtml");
			}			

			string path = System.Windows.Forms.Application.UserAppDataPath + "\\temphtml\\" + fileName + ".htm";

			StreamWriter writer = new StreamWriter(path,true);
			writer.Write(html);
			writer.Flush();
			writer.Close();

			return fileName + ".htm";
		}
	}
}
