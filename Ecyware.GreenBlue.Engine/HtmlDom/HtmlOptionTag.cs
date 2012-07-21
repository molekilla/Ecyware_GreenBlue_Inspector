// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003
using System;

namespace Ecyware.GreenBlue.Engine.HtmlDom
{
	/// <summary>
	/// Contains the properties for a option tag.
	/// </summary>
	[Serializable]
	public class HtmlOptionTag
	{
		string _id;
		string _value;
		string _text= string.Empty;
		string _key = string.Empty;
		bool _selected=false;

		/// <summary>
		/// Creates a new option tag.
		/// </summary>
		public HtmlOptionTag()
		{
		}

		public string Key
		{
			get
			{
				return _key;
			}
			set
			{
				_key = value;
			}
		}
		
		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		public string Id
		{
			get
			{
				return _id;
			}
			set
			{
				// if value is nothing, generate an id.
				// TODO: Create generator
				if ( value==String.Empty )
				{
					_id = this.GetHashCode().ToString();
				} 
				else 
				{
					_id = value;
				}
			}
		}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		public string Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
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
		/// Gets or sets the selected setting.
		/// </summary>
		public bool Selected
		{
			get
			{
				return _selected;
			}
			set
			{
				_selected = value;
			}
		}

	}
}
