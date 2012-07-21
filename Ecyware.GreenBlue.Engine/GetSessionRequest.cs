// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Security.Permissions;
using System.Collections;
using System.Runtime.Serialization;
using Ecyware.GreenBlue.Engine.HtmlDom;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Contains logic for GET Session Requests.
	/// </summary>
	[Serializable]
	[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
	public sealed class GetSessionRequest : SessionRequest, ISerializable
	{
		private string _query = String.Empty;		
		
		/// <summary>
		/// Creates a new GetSessionRequest.
		/// </summary>
		public GetSessionRequest() : base()
		{
			this.RequestType = HttpRequestType.GET;
		}
	
		/// <summary>
		/// ISerializable private constructor.
		/// </summary>
		/// <param name="s"> SerializationInfo. </param>
		/// <param name="context"> The StreamingContext.</param>
		private GetSessionRequest(SerializationInfo s, StreamingContext context)
		{
			this.QueryString = s.GetString("QueryString");
			this.ResponseHeaders = (Hashtable)s.GetValue("ResponseHeaders",typeof(Hashtable));
			this.RequestHeaders = (Hashtable)s.GetValue("RequestHeaders",typeof(Hashtable));
			this.StatusDescription = s.GetString("StatusDescription");
			this.StatusCode = s.GetInt32("StatusCode");
			this.Form = (HtmlFormTag)s.GetValue("Form",typeof(HtmlFormTag));
			this.RequestCookies = (System.Net.CookieCollection)s.GetValue("RequestCookies",typeof(System.Net.CookieCollection));
			this.RequestType = (HttpRequestType)s.GetValue("RequestType",typeof(HttpRequestType));
			this.Url = (Uri)s.GetValue("Url", typeof(Uri));
			this.WebUnitTest = (UnitTestItem)s.GetValue("WebUnitTest",typeof(UnitTestItem));
			try
			{
				this.UpdateSessionUrl = s.GetBoolean("UpdateSessionUrl");
				this.RequestHttpSettings = (HttpProperties)s.GetValue("RequestHttpSettings", typeof(HttpProperties));
			}
			catch
			{
				// do nothing
			}
		}

		/// <summary>
		/// Serializes the object.
		/// </summary>
		/// <param name="info"> SerializationInfo.</param>
		/// <param name="context"> StreamingContext.</param>
		public new void GetObjectData(SerializationInfo info, StreamingContext context)
		{			
			base.GetObjectData(info, context);
			info.AddValue("QueryString", this.QueryString);
//			info.AddValue("Form", this.Form);
//			info.AddValue("RequestCookies",this.RequestCookies);
//			info.AddValue("RequestType", this.RequestType);
//			info.AddValue("Url", this.Url);
//			info.AddValue("WebUnitTest", this.WebUnitTest);
//
//			info.AddValue("ResponseHeaders", this.ResponseHeaders);
//			info.AddValue("RequestHeaders", this.RequestHeaders );
//			info.AddValue("StatusDescription",this.StatusDescription );
//			info.AddValue("StatusCode", this.StatusCode);
		}

		/// <summary>
		/// Gets or sets the url query string.
		/// </summary>
		public string QueryString
		{
			get
			{
				return _query;
			}
			set
			{
				_query = value;
			}
		}

	}
}
