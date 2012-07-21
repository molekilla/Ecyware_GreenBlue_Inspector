// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: February 2003
using System;
using Ecyware.GreenBlue.Engine.HtmlDom;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Contains the RequestPostEventArgs class.
	/// </summary>
	public sealed class RequestCompleteEventArgs : EventArgs
	{
		private string _url = String.Empty;
		private int _formCount = 0;

		/// <summary>
		/// Creates a new RequestCompleteEventArgs.
		/// </summary>
		public RequestCompleteEventArgs()
		{
		}

		/// <summary>
		/// Gets or sets the uri.
		/// </summary>
		public string Url
		{
			get
			{
				return _url;
			}
			set
			{
				_url = value;
			}

		}

		/// <summary>
		/// Gets or sets the form count.
		/// </summary>
		public int FormCount
		{
			get
			{
				return _formCount;
			}
			set
			{
				_formCount = value;
			}
		}
	}
}
