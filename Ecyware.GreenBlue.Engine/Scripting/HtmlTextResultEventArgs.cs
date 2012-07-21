using System;

namespace Ecyware.GreenBlue.Engine.Scripting
{
	/// <summary>
	/// Summary description for HtmlBrowserArgs.
	/// </summary>
	public class HtmlTextResultEventArgs : EventArgs
	{
		private WebRequest _request;
		private bool _append;
		private string _text;
		/// <summary>
		/// Creates a new HtmlTextResultEventArgs.
		/// </summary>
		public HtmlTextResultEventArgs()
		{
		}

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		public string HtmlText
		{
			get
			{
				return _text;
			}
			set
			{
				_text = value;
			}
		}
		/// <summary>
		/// Gets or sets the append.
		/// </summary>
		public bool Append
		{
			get
			{
				return _append;
			}
			set
			{
				_append = value;
			}
		}


		/// <summary>
		/// Gets or sets the web request.
		/// </summary>
		public WebRequest Request
		{
			get
			{
				return _request;
			}
			set
			{
				_request = value;
			}
		}
	}
}
