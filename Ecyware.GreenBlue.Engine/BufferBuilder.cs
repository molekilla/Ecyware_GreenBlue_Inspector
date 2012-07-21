// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: November 2003
using System;
using System.Text;
using System.IO;
using System.Net;
using System.Collections;
using Ecyware.GreenBlue.Engine.HtmlCommand;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// BufferBuilder converts the data to collection types.
	/// </summary>
	public class BufferBuilder
	{

		/// <summary>
		/// Creates a new BufferBuilder.
		/// </summary>
		private BufferBuilder()
		{
		}

		/// <summary>
		/// Fills the Response Header Collection.
		/// </summary>
		/// <param name="resp"> The ResponseBuffer type.</param>
		/// <param name="request"> The HttpWebRequest type.</param>
		/// <param name="responseHeaders"> The Response WebHeaderCollection.</param>
		/// <param name="hwr"> The HttpWebResponse type.</param>
		/// <returns> An updated ResponseBuffer type containing the change.</returns>
		public static ResponseBuffer FillResponseHeader(ResponseBuffer resp,HttpWebRequest request, WebHeaderCollection responseHeaders, HttpWebResponse hwr)
		{			
			Hashtable coll = new Hashtable();

			coll.Add("Character Set",hwr.CharacterSet);
			coll.Add("Content Encoding",hwr.ContentEncoding);
			coll.Add("Last Modified",hwr.LastModified);
			coll.Add("Method",hwr.Method);
			coll.Add("Protocol Version",hwr.ProtocolVersion);
			coll.Add("Response Uri",hwr.ResponseUri);
			coll.Add("Server",hwr.Server);
			coll.Add("Status Code",hwr.StatusCode);
			coll.Add("Status Description",hwr.StatusDescription);
			coll.Add("Referer", request.Referer);

			for (int i = 0;i<responseHeaders.Count;i++)
			{
				if (!coll.ContainsKey(responseHeaders.GetKey(i)) )
				{					
					coll.Add(responseHeaders.GetKey(i), responseHeaders[i]);
				}
			}

			StringBuilder sb = new StringBuilder();

			sb.Append("---------------------");
			sb.Append("=RESPONSE HEADERS=");
			sb.Append("---------------------\r\n");
			foreach (DictionaryEntry de in coll)
			{
				sb.Append(de.Key);
				sb.Append(":");
				sb.Append(de.Value);
				sb.Append("\r\n");
			}

			resp.ResponseHeader = sb.ToString();
			resp.ResponseHeaderCollection = coll;
			return resp;
		}
		/// <summary>
		/// Fills The Request Header Collection.
		/// </summary>
		/// <param name="resp"> The ResponseBuffer type.</param>
		/// <param name="h"> The WebHeaderCollection type.</param>
		/// <param name="hwrq"> The HttpWebRequest type.</param>
		/// <returns> An updated ResponseBuffer type containing the change.</returns>
		public static ResponseBuffer FillRequestHeader(ResponseBuffer resp,WebHeaderCollection h,HttpWebRequest hwrq)
		{

			Hashtable coll = new Hashtable();

			coll.Add("Accept",hwrq.Accept);
			coll.Add("User Agent",hwrq.UserAgent);
			coll.Add("ContentType",hwrq.ContentType);
			coll.Add("Method",hwrq.Method);
			coll.Add("Pipelined",hwrq.Pipelined);
			coll.Add("Keep Alive",hwrq.KeepAlive);
			coll.Add("Request Uri",hwrq.RequestUri);
			coll.Add("Send Chunked",hwrq.SendChunked);
			coll.Add("Transfer Encoding",hwrq.TransferEncoding);

			for (int i = 0;i<h.Count;i++)
			{
				if ( !coll.ContainsKey(h.GetKey(i)) )
				{
					coll.Add(h.GetKey(i),h[i]);
				}
			}

			StringBuilder sb = new StringBuilder();
			sb.Append("---------------------");
			sb.Append("=REQUEST HEADERS=");
			sb.Append("---------------------\r\n");

			foreach (DictionaryEntry de in coll)
			{
				sb.Append(de.Key);
				sb.Append(":");
				sb.Append(de.Value);
				sb.Append("\r\n");
			}

			resp.RequestHeaderCollection = coll;
			resp.RequestHeader = sb.ToString();

			return resp;

		}
		/// <summary>
		/// Fills the HTTP Body.
		/// </summary>
		/// <param name="resp"> The ResponseBuffer type.</param>
		/// <param name="stm"> The Stream containing the HTTP Body.</param>
		/// <returns> An updated ResponseBuffer type containing the change.</returns>
		public static ResponseBuffer FillHttpBody(ResponseBuffer resp, Stream stm)
		{
			// used to build entire input
			StringBuilder buffer = new StringBuilder();

			// used on each read operation
			byte[] buf = new byte[8192];

			string tempString = null;
			int count = 0;

			do
			{
				if ( !stm.CanRead)
				{
					break;
				}

				// fill the buffer with data
				count = stm.Read(buf, 0, buf.Length);

				// make sure we read some data
				if (count != 0)
				{
					// translate from bytes to ASCII text
					tempString = Encoding.ASCII.GetString(buf, 0, count);

					// continue building the string
					buffer.Append(tempString);
				}
			}
			while (count > 0); // any more data to read?			         
			resp.HttpBody = buffer.ToString(); // writer.ToString();
			return resp;
		}

		/// <summary>
		/// Fills the cookie data collection.
		/// </summary>
		/// <param name="resp"> The ResponseBuffer type.</param>
		/// <param name="cookies"> Cookie collection. </param>
		/// <returns> An updated ResponseBuffer type containing the change.</returns>
		public static ResponseBuffer FillCookieData(ResponseBuffer resp, CookieCollection cookies)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("---------------------");
			sb.Append("=COOKIES=");
			sb.Append("---------------------\r\n");
			foreach (Cookie cky in cookies)
			{
					// text
					sb.Append(cky.Name);
					sb.Append(": ");
					sb.Append(cky.Value);
					sb.Append("\r\n");
			}


			resp.CookieCollection = cookies;
			resp.CookieData = sb.ToString();

			return resp;
		}
	}
}
