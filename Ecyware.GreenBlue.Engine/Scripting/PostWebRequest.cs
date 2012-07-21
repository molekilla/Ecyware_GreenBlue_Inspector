using System;

namespace Ecyware.GreenBlue.Engine.Scripting
{
	/// <summary>
	/// Summary description for PostWebRequest.
	/// </summary>
	[Serializable]
	public sealed class PostWebRequest : WebRequest
	{
		private bool _usePostData = false;
		private string _postData = string.Empty;

		/// <summary>
		/// Creates a new PostWebRequest.
		/// </summary>
		public PostWebRequest() : base()
		{
			this.RequestType = HttpRequestType.POST;
		}

		public PostWebRequest(PostSessionRequest request) : this()
		{					
			AddCookies(request.RequestCookies);
			if ( request.Form != null )
				Form.ReadHtmlFormTag(request.Form);
					
			RequestHttpSettings = request.RequestHttpSettings;			
			Url = request.Url.ToString();
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
