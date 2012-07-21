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
	public class RequestGetEventArgs : EventArgs
	{
		private string _url = String.Empty;
		private InspectorAction _action;
		private HtmlFormTag _form = null;

		/// <summary>
		/// Creates a new RequestGetEventArgs.
		/// </summary>
		public RequestGetEventArgs()
		{
		}		

		/// <summary>
		/// Creates a new RequestGetEventArgs.
		/// </summary>
		/// <param name="url"> The url value.</param>
		public RequestGetEventArgs(string url)
		{
			_url = url;
		}	

		/// <summary>
		/// Gets or sets the inspector action enum.
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
	}
}
