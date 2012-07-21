// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for RequestReferenceTransformValue.
	/// </summary>
	[Serializable]
	public class HeaderTransformValue : TransformValue
	{
		private string _from = string.Empty;
		private string _name;
		/// <summary>
		/// Creates a new HeaderTransformValue.
		/// </summary>
		public HeaderTransformValue()
		{
		}		

		/// <summary>
		/// Gets or sets the header name.
		/// </summary>
		public string HeaderName
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}

		/// <summary>
		/// Gets the value from the web response.
		/// </summary>
		/// <param name="response"> The web response.</param>
		/// <returns> An object.</returns>
		public override object GetValue(WebResponse response)
		{	
			object result = string.Empty;
			switch ( _name )
			{
			    case "Accept":
					result = response.ResponseHttpSettings.Accept;
					break;
				case "ContentLength":				
					result = response.ResponseHttpSettings.ContentLength;
					break;
				case "ContentType":
					result =  response.ResponseHttpSettings.ContentType;
					break;
				case "CharacterSet":
					result = response.CharacterSet;
					break;
				case "ContentEncoding":
					result = response.ContentEncoding;
					break;
				case "IfModifiedSince":					
					result =  response.ResponseHttpSettings.IfModifiedSince;
					break;
				case "KeepAlive":
					result =  response.ResponseHttpSettings.KeepAlive;
					break;
				case "MediaType":
					result =  response.ResponseHttpSettings.MediaType;					
					break;
				case "Referer":
					result = response.ResponseHttpSettings.Referer;
					break;
				case "SendChunked":
					result = response.ResponseHttpSettings.SendChunked;
					break;
				case "TransferEncoding":
					result = response.ResponseHttpSettings.TransferEncoding;
					break;
				case "UserAgent":
					result =  response.ResponseHttpSettings.UserAgent;
					break;
				case "ResponseUri":
					result = response.ResponseUri;
					break;
				case "StatusCode":
					result = response.StatusCode;
					break;
				case "StatusDescription":
					result = response.StatusDescription;
					break;
				case "Version":
					result = response.Version;
					break;
				default:
					try
					{
						System.Net.WebHeaderCollection headers = new System.Net.WebHeaderCollection();
						WebHeader.FillWebHeaderCollection(headers, response.ResponseHttpSettings.AdditionalHeaders);			
						result =  headers[_name];
					}
					catch ( Exception ex )
					{						
						ExceptionManager.Publish(ex);
					}
					break;
			}

			return result;
		}

	}
}
