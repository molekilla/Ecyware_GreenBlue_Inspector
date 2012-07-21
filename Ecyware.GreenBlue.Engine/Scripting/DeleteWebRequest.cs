using System;

namespace Ecyware.GreenBlue.Engine.Scripting
{
	/// <summary>
	/// Summary description for DeleteWebRequest.
	/// </summary>
	[Serializable]
	public sealed class DeleteWebRequest : WebRequest
	{
		/// <summary>
		/// Creates a new DeleteWebRequest.
		/// </summary>
		public DeleteWebRequest() : base()
		{
			this.RequestType = HttpRequestType.DELETE;
			this.RequestHttpSettings.ContentType = "text/xml";
			ID =  GenerateID;
		}	
	}
}
