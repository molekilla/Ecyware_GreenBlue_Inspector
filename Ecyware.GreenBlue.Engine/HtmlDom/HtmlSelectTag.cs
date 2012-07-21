// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003
using System;
using System.Collections;

namespace Ecyware.GreenBlue.Engine.HtmlDom
{
	/// <summary>
	/// Contains the properties for a select tag.
	/// </summary>
	[Serializable]
	public class HtmlSelectTag:HtmlTagBase
	{

		string _name;
		string _value;
		bool _multiple=false;
		HtmlOptionCollection _options = new HtmlOptionCollection();

		/// <summary>
		/// Creates a new HtmlSelectTag.
		/// </summary>
		public HtmlSelectTag()
		{			
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
		/// Gets or sets the name.
		/// </summary>
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}


		/// <summary>
		/// Gets or sets the multiple setting.
		/// </summary>
		public bool Multiple
		{
			get
			{
				return _multiple;
			}
			set
			{
				_multiple=value;
			}
		}

		public void AddOptionTag(HtmlOptionTag tag)
		{
			_options.Add(tag.Key, tag);
		}

		/// <summary>
		/// Updates an HtmlOptionTag value.
		/// </summary>
		/// <param name="index"> The index.</param>
		/// <param name="value"> The updated value.</param>
		public void UpdateOptionValue(int index, string value)
		{
			_options.GetByIndex(index).Value = value;
		}
		/// <summary>
		/// Gets or sets the option collection.
		/// </summary>
		public HtmlOptionTag[] Options
		{
			get
			{
				ArrayList l = new ArrayList(_options.Values);
				return (HtmlOptionTag[])l.ToArray(typeof(HtmlOptionTag));
			}
			set
			{
				foreach ( HtmlOptionTag t in value )
				{
					_options.Add(t.Key, t);
				}				
			}
		}
	}
}
