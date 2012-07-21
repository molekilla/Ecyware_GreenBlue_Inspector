// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: July 2004
using System;
using System.Net;
using System.Collections;
using Ecyware.GreenBlue.Engine;
//using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.HtmlDom;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// The session request types available for updating.
	/// </summary>
	public enum UpdateSessionRequestType
	{
		Cookies,
		QueryString,
		PostData,
		Form,
		Unselected
	}
	/// <summary>
	/// Contains the definition for UpdateSessionRequestArgs type.
	/// </summary>
	public class UpdateSessionRequestEventArgs : EventArgs
	{

		private string _postData = string.Empty;
		private string _queryString = string.Empty;
		private HtmlFormTag _form = null;
		private CookieCollection _cookies = null;

		private UpdateSessionRequestType _updateType = UpdateSessionRequestType.Unselected;

		/// <summary>
		/// Creates a new UpdateSessionRequestArgs.
		/// </summary>
		public UpdateSessionRequestEventArgs()
		{
		}

		/// <summary>
		/// Gets or sets the session request update type.
		/// </summary>
		public UpdateSessionRequestType UpdateType
		{
			get
			{
				return _updateType;
			}
			set
			{
				_updateType = value;
			}
		}
		/// <summary>
		/// Gets or sets the cookies.
		/// </summary>
		public CookieCollection Cookies
		{
			get
			{
				return _cookies;
			}
			set
			{
				_cookies = value;
			}
		}

		/// <summary>
		/// Gets or sets the form.
		/// </summary>
		public HtmlFormTag Form
		{
			get
			{
				return _form;
			}
			set
			{
				_form = value;
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

		/// <summary>
		/// Gets or sets the query string.
		/// </summary>
		public string QueryString
		{
			get
			{
				return _queryString;
			}
			set
			{
				_queryString = value;
			}
		}
	}
}
