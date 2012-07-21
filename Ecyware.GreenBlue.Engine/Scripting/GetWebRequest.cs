using System;

namespace Ecyware.GreenBlue.Engine.Scripting
{
	/// <summary>
	/// Summary description for GetWebRequest.
	/// </summary>
	[Serializable]
	public sealed class GetWebRequest : WebRequest
	{
		/// <summary>
		/// Creates a new GetWebRequest.
		/// </summary>
		public GetWebRequest() : base()
		{
			this.RequestType = HttpRequestType.GET;
		}

		public GetWebRequest(GetSessionRequest request) : this()
		{					
			AddCookies(request.RequestCookies);

			if ( request.Form != null )
				Form.ReadHtmlFormTag(request.Form);
	
			RequestHttpSettings = request.RequestHttpSettings;			
			Url = request.Url.ToString();
			ID =  GenerateID;
		}	
	}
}
