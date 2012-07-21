// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Collections;
using Ecyware.GreenBlue.Engine.HtmlDom;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Contains the PredefinedTesterArgs type.
	/// </summary>
	[Serializable]
	public class PredefinedTesterArgs : EventArgs, IHtmlFormUnitTestArgs
	{
		private string _postData = string.Empty;
		private HtmlFormTag _formData = null;
		private PostDataCollection _userPostData = null;

		/// <summary>
		/// Creates a new PredefinedTesterArgs.
		/// </summary>
		public PredefinedTesterArgs()
		{
		}

		// TODO: Im thinking using Encoding as a property

		/// <summary>
		/// Gets or sets the user post data.
		/// </summary>
		public PostDataCollection UserPostData
		{
			get
			{
				return _userPostData;
			}
			set
			{
				_userPostData = value;
			}
		}

		/// <summary>
		/// Gets or sets the form tag.
		/// </summary>
		public HtmlFormTag FormData
		{
			get
			{
				return _formData;
			}
			set
			{
				_formData = value;
			}
		}
		#region IHtmlFormUnitTestArgs Members

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

		#endregion
	}
}
