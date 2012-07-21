// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;

namespace Ecyware.GreenBlue.Engine.HtmlCommand
{
	/// <summary>
	/// Contains the parser properties.
	/// </summary>
	public class HtmlParserProperties
	{
		private bool _removeDoctype=true;
		private bool _removeScripts = true;
		private bool _removeStyle = false;


		#region Constructors
		/// <summary>
		/// Creates a new HtmlParserProperties.
		/// </summary>
		public HtmlParserProperties()
		{
		}

		/// <summary>
		/// Creates a new HtmlParserProperties.
		/// </summary>
		/// <param name="removeScripts"> Sets the remove script tags flag.</param>
		/// <param name="removeStyle"> Sets the remove style tags flag.</param>
		public HtmlParserProperties(bool removeScripts, bool removeStyle)
		{
			this.RemoveScriptTags=removeScripts;
			this.RemoveStyleTags=removeStyle;
		}


		/// <summary>
		/// Creates a new HtmlParserProperties.
		/// </summary>
		/// <param name="removeScripts"> Sets the remove script tags flag.</param>
		/// <param name="removeStyle"> Sets the remove style tags flag.</param>
		/// <param name="removeDoctype"> Sets the remove doctype flag.</param>
		public HtmlParserProperties(bool removeScripts, bool removeStyle, bool removeDoctype)
		{
			this.RemoveScriptTags=removeScripts;
			this.RemoveStyleTags=removeStyle;
			this.RemoveDocumentType=removeDoctype;
		}
		#endregion

		/// <summary>
		/// Gets or sets the remove document type.
		/// </summary>
		public bool  RemoveDocumentType
		{
			get
			{
				return _removeDoctype;
			}
			set
			{
				_removeDoctype = value;
			}
		}

		/// <summary>
		/// Gets or sets the remove script tags.
		/// </summary>
		public bool RemoveScriptTags
		{
			get
			{
				return _removeScripts;
			}
			set
			{
				_removeScripts = value;
			}
		}

		/// <summary>
		/// Gets or sets the remove style tags.
		/// </summary>
		public bool RemoveStyleTags
		{
			get
			{
				return _removeStyle;
			}
			set
			{
				_removeStyle = value;
			}
		}
	}
}
