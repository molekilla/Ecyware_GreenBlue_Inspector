// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Net;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Engine.HtmlCommand;


namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Contains the methods that are called in the WorkerProcess queue. This class is internal and all the methods are static.
	/// </summary>
	internal class HttpPipeline
	{
		private static Hashtable RegExpList = null;

		/// <summary>
		/// Loads scripts with SRC attribute and adds it to the Script Collection in ResponseBuffer.
		/// </summary>
		/// <param name="requestUrl"> The top url.</param>
		/// <param name="response"> The ResponseBuffer.</param>
		/// <param name="clientSettings"> The http settings.</param>
		/// <returns> An updated ResponseBuffer.</returns>
		internal static ResponseBuffer LoadScriptsFromSrc(Uri requestUrl,ResponseBuffer response,HttpProperties clientSettings)
		{
			GetForm http = new GetForm();
			HtmlScriptCollection scripts = response.Scripts;

			for (int i=0;i<scripts.Count;i++)
			{
				if ( scripts[i].Source.Length!=0 )
				{
					string src = scripts[i].Source;
					string requestData = string.Empty;
			
					requestData = UriResolver.ResolveUrl(requestUrl,src);

					// add User Agent
					clientSettings.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0)";
					
					try
					{
						// load
						ResponseBuffer result = http.StartSyncHttpGet(requestData,clientSettings,null,response.CookieCollection);

						scripts[i].Text = result.HttpBody;
					}
					catch (Exception ex)
					{
						// TODO: Log this errors as SCRIPT loading failed error
						System.Diagnostics.Debug.Write(ex.Message);
					}
				}
			}

			return response;
		}

		/// <summary>
		/// Parses the scripts tags found in buffer.
		/// </summary>
		/// <param name="buffer"> The ResponseBuffer.</param>
		/// <returns> An updated ResponseBuffer.</returns>
		internal static ResponseBuffer ParseScriptTags(ResponseBuffer buffer)
		{
			Regex getScripts = null;
			Regex getAttributes = null;

			if ( RegExpList == null )
			{
				// Remove Scripts regex
				RegexOptions options = RegexOptions.None;
				getScripts = new Regex(@"(?<header><(?i:script)[^>]*?)(/>|>(?<source>[\w|\t|\r|\W]*?)</(?i:script)>)",options);
				getAttributes = new Regex(@"(?<name>(\w+))=(""|')(?<value>.*?)(""|')",options);

				// add to list
				RegExpList = new Hashtable();
				RegExpList.Add("ScriptQuery",getScripts);
				RegExpList.Add("AttributesQuery",getAttributes);
			}
			else 
			{
				getScripts = (Regex)RegExpList["ScriptQuery"];
				getAttributes = (Regex)RegExpList["AttributesQuery"];
			}
			
			// Get matches
			MatchCollection matches = getScripts.Matches(buffer.HttpBody);
			
			for(int i=0;i<matches.Count;i++)
			{
				HtmlScript scriptTag = new HtmlScript();
				string scriptHeader = matches[i].Groups["header"].Value;
				string scriptSource = matches[i].Groups["source"].Value;

				scriptTag.Text = scriptSource;

				// get attributes
				MatchCollection attributes = getAttributes.Matches(scriptHeader);
				foreach (Match m in attributes)
				{
					string name = m.Groups["name"].Value;
					string result = m.Groups["value"].Value;

					if ( name.ToLower() == "language" )
					{
						scriptTag.Language=result.Trim();
					}
					if ( name.ToLower() == "src" )
					{
						scriptTag.Source = result.Trim();
					}
				}				
				buffer.Scripts.Add(scriptTag);
			}

			return buffer;
		}


		/// <summary>
		/// Fills the ResponseBuffer.
		/// </summary>
		/// <param name="response"> The HttpWebResponse type.</param>
		/// <param name="hwr"> The HttpWebRequest type.</param>
		/// <param name="clientSettings"> The http settings.</param>
		/// <param name="httpState"> The Http state.</param>
		/// <returns> A ResponseBuffer type.</returns>
		internal static ResponseBuffer FillResponseBuffer(HttpWebResponse response, HttpWebRequest hwr, HttpProperties clientSettings, HttpState httpState)
		{
			ResponseBuffer respBuffer = null;
			 
			if ( CompareString.Compare(hwr.Method,"get") )
			{
				respBuffer = new ResponseBuffer(HttpRequestType.GET);
			} 
			else 
			{
				respBuffer = new ResponseBuffer(HttpRequestType.POST);
			}

			respBuffer.StatusCode = (int)response.StatusCode;
			respBuffer.StatusDescription=response.StatusDescription;
			
			if ( response.ProtocolVersion == HttpVersion.Version11  )
			{
				respBuffer.Version="1.1";
			} 
			else 
			{
				respBuffer.Version="1.0";
			}

			// Request Header Collection	
			BufferBuilder.FillRequestHeader(respBuffer,hwr.Headers,hwr);
			
			// Header Collection
			BufferBuilder.FillResponseHeader(respBuffer,hwr,response.Headers,response);

			// Cookie collection
			response.Cookies = hwr.CookieContainer.GetCookies(hwr.RequestUri);
			BufferBuilder.FillCookieData(respBuffer,response.Cookies);
			
			Stream stm = response.GetResponseStream();			
			BufferBuilder.FillHttpBody(respBuffer,stm);
			stm.Close();

			if ( response!=null )
			{
				response.Close();
			}

			return respBuffer;
		}

		/// <summary>
		/// Fills the error message and returns the ResponseBuffer.
		/// </summary>
		/// <param name="errorMessage"> An error message explaining the cause of error.</param>
		/// <param name="httpState"> The HTTP State</param>
		/// <returns> A ResponseBuffer type.</returns>
		internal static ResponseBuffer FillErrorBuffer(string errorMessage, HttpState httpState)
		{
			//StringBuilder textStream = new StringBuilder();
			ResponseBuffer respBuffer = new ResponseBuffer();

			respBuffer.ErrorMessage = errorMessage;
			return respBuffer;
		}

	}
}
