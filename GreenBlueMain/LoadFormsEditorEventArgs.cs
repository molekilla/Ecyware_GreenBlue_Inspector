// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: June 2004
using System;
using Ecyware.GreenBlue.Engine.HtmlDom;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Summary description for LoadFormsEditorEventArgs.
	/// </summary>
	public class LoadFormsEditorEventArgs : EventArgs
	{
		private byte[] _postData = null;
		private Uri _uri;
		private HtmlFormTagCollection _forms = null;

		/// <summary>
		/// Creates a new LoadFormsEditorEventArgs.
		/// </summary>
		/// <param name="postData"> Post data in bytes.</param>
		/// <param name="forms"> Form collection.</param>
		public LoadFormsEditorEventArgs(byte[] postData, HtmlFormTagCollection forms)
		{
			this.PostData = postData;
			this.FormCollection = forms;
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

		/// <summary>
		/// Gets or sets the site uri.
		/// </summary>
		public Uri SiteUri
		{
			get
			{
				return _uri;
			}
			set
			{
				_uri = value;
			}
		}
		/// <summary>
		/// Gets or sets the form collection.
		/// </summary>
		public HtmlFormTagCollection FormCollection
		{
			get
			{
				return _forms;
			}
			set
			{
				_forms = value;
			}
		}
	}
}
