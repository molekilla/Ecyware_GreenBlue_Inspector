// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003

using System;
using Ecyware.GreenBlue.Engine.HtmlDom;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Contains the RequestPostEventArgs class.
	/// </summary>
	public class RequestPostEventArgs : EventArgs
	{
		/// <summary>
		/// Creates a new RequestPostEventArgs.
		/// </summary>
		public RequestPostEventArgs()
		{
		}

		private Uri _currentUri;
		private HtmlFormTag _form;
		private string _method;
		private InspectorAction _action;
		private byte[] _postData = null;


		/// <summary>
		/// Gets or sets the inspector action.
		/// </summary>
		public InspectorAction InspectorRequestAction
		{
			get
			{
				return _action;
			}
			set
			{
				_action = value;
			}
		}

		/// <summary>
		/// Gets or sets the method.
		/// </summary>
		public string Method
		{
			get
			{
				return _method;
			}
			set
			{
				_method = value;
			}
		}

		/// <summary>
		/// Gets or sets the HtmlFormTag.
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
		public byte[] PostData
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

		public Uri CurrentUri
		{
			get
			{
				return _currentUri;
			}
			set
			{
				_currentUri = value;
			}
		}
	}
}
