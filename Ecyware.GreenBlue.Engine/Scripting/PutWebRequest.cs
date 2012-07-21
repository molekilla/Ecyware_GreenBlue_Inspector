using System;

namespace Ecyware.GreenBlue.Engine.Scripting
{
	/// <summary>
	/// Summary description for PutWebRequest.
	/// </summary>
	[Serializable]
	public sealed class PutWebRequest : WebRequest
	{
		private bool _usePostData = true;
		private string _postData = string.Empty;

		/// <summary>
		/// Creates a new PutWebRequest.
		/// </summary>
		public PutWebRequest() : base()
		{
			this.RequestType = HttpRequestType.PUT;
			this.RequestHttpSettings.ContentType = "text/xml";
			ID =  GenerateID;
		}

		/// <summary>
		/// Gets or sets the use postdata flag.
		/// </summary>
		public bool UsePostData
		{
			get
			{
				return _usePostData;
			}
			set
			{
				_usePostData = value;
			}
		}
		/// <summary>
		/// Gets or sets the post data.
		/// </summary>
		public string PostData
		{
			get
			{
				return _postData;
			}
			set
			{
				_postData = value;
			}
		}
	}
}
