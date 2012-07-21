// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Ecyware.GreenBlue.Protocols.Http;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Engine.HtmlCommand;
using Ecyware.GreenBlue.WebUnitTestManager;

namespace Ecyware.GreenBlue.WebUnitTestCommand
{
	/// <summary>
	/// Contains the Attack Signature Detection Engine (ASDE) logic.
	/// </summary>
	public class AsdeCommand
	{
		private Test _test = null;
		private ResponseBuffer _response = null;

		/// <summary>
		/// Creates a new ASDE Command.
		/// </summary>
		public AsdeCommand()
		{
		}

		/// <summary>
		/// Creates a new ASDE Command.
		/// </summary>
		/// <param name="httpResponse"> The response buffer type.</param>
		/// <param name="test"> The test type.</param>
		public AsdeCommand(ResponseBuffer httpResponse, Test test)
		{
			this.HttpResponseBuffer = httpResponse;
			this.TestToEvaluate = test;
		}


		/// <summary>
		/// Gets or sets the http response buffer.
		/// </summary>
		public ResponseBuffer HttpResponseBuffer
		{
			get
			{
				return _response;
			}
			set
			{
				_response = value;
			}
		}

		/// <summary>
		/// Gets or sets the test to evaluate.
		/// </summary>
		public Test TestToEvaluate
		{
			get
			{
				return _test;
			}
			set
			{
				_test = value;
			}
		}

		#region ASDE Logic

		/// <summary>
		/// Checks for the unit test result.
		/// </summary>
		/// <returns> Returns a UnitTestResult.</returns>
		public UnitTestResult CheckTestResult()
		{
			UnitTestResult testResult = new UnitTestResult();
			UnitTestSeverity statusCodeSL = UnitTestSeverity.Low;
			bool isSignatureFound = false;

			// check first the StatusCode result
			switch ( this.HttpResponseBuffer.StatusCode )
			{
				case (int)HttpStatusCode.InternalServerError:
					statusCodeSL = UnitTestSeverity.High;
					break;
				case (int)HttpStatusCode.Found:
					statusCodeSL = UnitTestSeverity.Low;
					break;
				default:
					statusCodeSL = UnitTestSeverity.Low;
					break;
			}

			Uri responseUri = (Uri)this.HttpResponseBuffer.ResponseHeaderCollection["Response Uri"];

			// if SL is Low, then check Html Source
			//if ( testResult.SeverityLevel == UnitTestSeverity.Low )
			//{				
				// checks only bo, xss and sql
				switch ( this.TestToEvaluate.UnitTestDataType )
				{
					case UnitTestDataContainer.HtmlFormTag:
						isSignatureFound = ParseDataForSignature(this.HttpResponseBuffer.HttpBody, this.TestToEvaluate.UnitTestDataType);
						break;
					case UnitTestDataContainer.PostDataHashtable:
						isSignatureFound = ParseDataForSignature(this.HttpResponseBuffer.HttpBody, this.TestToEvaluate.UnitTestDataType);
						break;
					case UnitTestDataContainer.NoPostData:
						isSignatureFound = ParseDataForSignature(responseUri.ToString(), this.TestToEvaluate.UnitTestDataType);
						break;
					case UnitTestDataContainer.Cookies:
						isSignatureFound = ParseDataForSignature(this.HttpResponseBuffer.CookieData, this.TestToEvaluate.UnitTestDataType);
						break;
				}

				#region Evaluate by test type
				// check which type of test
				switch ( this.TestToEvaluate.TestType )
				{
					case UnitTestType.BufferOverflow:
						if ( statusCodeSL == UnitTestSeverity.High )
						{
							// 3 Success
							testResult.SeverityLevel = UnitTestSeverity.High;
							testResult.SolutionId = 100;
						} 							
						break;
					case UnitTestType.DataTypes:
						if ( statusCodeSL == UnitTestSeverity.High )
						{
							// 3 Success
							testResult.SeverityLevel = UnitTestSeverity.High;
							testResult.SolutionId = 101;
						}
						break;
					case UnitTestType.SqlInjection:
						if ( statusCodeSL == UnitTestSeverity.High )
						{
							// 3 Success
							testResult.SeverityLevel = UnitTestSeverity.High;
							testResult.SolutionId = 102;
						} 
						else 
						{
							if ( isSignatureFound )
							{
								statusCodeSL = UnitTestSeverity.Medium;

								// 2 medium
								testResult.SeverityLevel = UnitTestSeverity.Medium;
								testResult.SolutionId = 103;
							}
						}
						break;
					case UnitTestType.XSS:
						if ( statusCodeSL == UnitTestSeverity.High )
						{
							// 3 Success
							testResult.SeverityLevel = UnitTestSeverity.High;
							testResult.SolutionId = 104;
						} 
						else 
						{
							if ( isSignatureFound )
							{
								statusCodeSL = UnitTestSeverity.High;

								// 3 always high
								testResult.SeverityLevel = UnitTestSeverity.High;
								testResult.SolutionId = 105;
							}
						}
						break;
				}
				#endregion
			//}

			if ( statusCodeSL == UnitTestSeverity.Low )
			{
				testResult.SeverityLevel = statusCodeSL;
				testResult.SolutionId = 200;
			}


			return testResult;
		}

		/// <summary>
		/// Parses the data and looks for the signature. If match found returns true, else false.
		/// </summary>
		/// <returns> If match found, returns true, else false.</returns>
		public bool ParseDataForSignature(string data, UnitTestDataContainer dataContainerType)
		{
			string signature = string.Empty;
			bool result = false;

//			// get arguments
//			// check if buffer was written to html
//			if ( this.TestToEvaluate.Arguments is BufferOverflowTesterArgs )
//			{
//				signature = ((BufferOverflowTesterArgs)this.TestToEvaluate.Arguments).PostData;
//				string[] splitValues = signature.Split('=');
//				// second value
//				signature = splitValues[1];
//			}
			// check if xss was written to html
			if ( this.TestToEvaluate.Arguments is XssInjectionTesterArgs )
			{
				signature = ((XssInjectionTesterArgs)this.TestToEvaluate.Arguments).XssValue;
			}
			// check if sql was written to html
			if ( this.TestToEvaluate.Arguments is SqlInjectionTesterArgs )
			{
				signature = ((SqlInjectionTesterArgs)this.TestToEvaluate.Arguments).SqlValue;
			}

			if ( signature.Length > 0 )
			{
				//StringBuilder regexQuery = null;

				// evaluate only url
				if ( dataContainerType == UnitTestDataContainer.NoPostData )
				{
					signature = EncodeDecode.UrlEncode(signature);
					data = EncodeDecode.UrlDecode(data);
					data = EncodeDecode.UrlEncode(data);
				} else {
					// convert signature to a RegEx string
					//regexQuery = new StringBuilder(signature);
				}

				//RegexOptions options = RegexOptions.IgnoreCase;
				//Regex matchSignature = new Regex(regexQuery.ToString(),options);
			
				//MatchCollection matches = matchSignature.Matches(data);

				if ( data.IndexOf(signature) > 0 )
				{
					// match found
					result = true;
				} 
				else 
				{
					// match not found
					result = false;
				}
			} 
			else 
			{
				result = false;
			}

			return result;
		}

		/// <summary>
		/// Checks for the unit test result.
		/// </summary>
		/// <param name="httpResponse"> The response buffer.</param>
		/// <param name="test"> The test.</param>
		/// <returns> A UnitTestResult.</returns>
		public UnitTestResult CheckTestResult(ResponseBuffer httpResponse, Test test)
		{
			this.HttpResponseBuffer = httpResponse;
			this.TestToEvaluate = test;

			return CheckTestResult();
		}
		#endregion
	}
}
