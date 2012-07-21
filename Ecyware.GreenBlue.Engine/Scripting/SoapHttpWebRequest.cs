using System;

namespace Ecyware.GreenBlue.Engine.Scripting
{
	/// <summary>
	/// Summary description for SoapHttpWebRequest.
	/// </summary>
	[Serializable]
	public sealed class SoapHttpWebRequest : WebRequest
	{
		/// <summary>
		/// Creates a new SoapHttpWebRequest.
		/// </summary>
		public SoapHttpWebRequest() : base()
		{
			this.RequestType = HttpRequestType.SOAPHTTP;
			this.RequestHttpSettings.ContentType = "text/xml";
			ID =  GenerateID;
		}
	}
}
