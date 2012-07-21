// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003
using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Ecyware.GreenBlue.Engine.HtmlDom
{
	/// <summary>
	/// Contains the properties for the script tag.
	/// </summary>
	[Serializable]
	public class HtmlScript
	{
		private string _lang = String.Empty;
		private string _text = String.Empty;
		private string _source = String.Empty;

		/// <summary>
		/// Creates a new script tag.
		/// </summary>
		public HtmlScript()
		{
		}

		/// <summary>
		/// Gets or sets the language.
		/// </summary>
		public string Language
		{
			get
			{
				return _lang;
			}
			set
			{
				_lang  =  value;
			}
		}

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		public string Text
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
		/// Gets or sets the source.
		/// </summary>
		public string Source
		{
			get
			{
				return _source;
			}
			set
			{
				_source = value;
			}
		}
	}
}
