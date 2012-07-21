// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: June 2004
using System;
using Ecyware.GreenBlue.Engine.HtmlDom;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Summary description for LoadLinksEventArgs.
	/// </summary>
	public class LoadLinksEventArgs : EventArgs
	{
		private HtmlTagBaseList _anchors = null;
		private HtmlTagBaseList _links = null;
		private HtmlTagBaseList _frames = null;

		/// <summary>
		/// Creates a new LoadFormsEditorEventArgs.
		/// </summary>
		public LoadLinksEventArgs()
		{
		}

		/// <summary>
		/// Gets or sets the anchors collection.
		/// </summary>
		public HtmlTagBaseList Anchors
		{
			get
			{
				return _anchors;
			}
			set
			{
				_anchors = value;
			}
		}
		/// <summary>
		/// Gets or sets the links collection.
		/// </summary>
		public HtmlTagBaseList Links
		{
			get
			{
				return _links;
			}
			set
			{
				_links = value;
			}
		}

		/// <summary>
		/// Gets or sets the frames collection.
		/// </summary>
		public HtmlTagBaseList Frames
		{
			get
			{
				return _frames;
			}
			set
			{
				_frames = value;
			}
		}
	}
}
