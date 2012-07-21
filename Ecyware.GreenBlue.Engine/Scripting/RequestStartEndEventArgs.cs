using System;

namespace Ecyware.GreenBlue.Engine.Scripting
{
	/// <summary>
	/// Summary description for RequestStartEndEventArgs.
	/// </summary>
	public class RequestStartEndEventArgs : EventArgs
	{
		WebRequest _request;
		int _currentIndex = 0;
		int _requestCount = 0;

		/// <summary>
		/// Creates a new RequestStartEndEventArgs.
		/// </summary>
		public RequestStartEndEventArgs()
		{
		}

		/// <summary>
		/// Gets or sets the current request.
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

		/// <summary>
		/// Gets or sets the request count.
		/// </summary>
		public int RequestCount
		{
			get
			{
				return _requestCount;
			}
			set
			{
				_requestCount = value;
			}
		}

		/// <summary>
		/// Gets or sets the current index.
		/// </summary>
		public int CurrentIndex
		{
			get
			{
				return _currentIndex;
			}
			set
			{
				_currentIndex = value;
			}
		}
	}
}
