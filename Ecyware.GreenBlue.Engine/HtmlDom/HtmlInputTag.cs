// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003
using System;

namespace Ecyware.GreenBlue.Engine.HtmlDom
{

	/// <summary>
	/// Represents the HTML input types.
	/// </summary>
	public enum HtmlInputType
	{
		Button,
		Checkbox,
		File,
		Hidden,
		Image,
		Password,
		Radio,
		Reset,
		Submit,
		Text
	}
	/// <summary>
	/// Contains the properties for a input tag.
	/// </summary>
	[Serializable]
	public class HtmlInputTag : HtmlTagBase
	{

		string _name;
		string _value;
		string _checked;
		string _maxlength;
		string _readonly;
		HtmlInputType _type;

		/// <summary>
		/// Creates a new HtmlInputTag.
		/// </summary>
		public HtmlInputTag()
		{			
		}

		/// <summary>
		/// Gets or sets the HtmlInputType.
		/// </summary>
		public HtmlInputType Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
			}
		}

		/// <summary>
		/// Gets or sets the input tag value.
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
		/// Gets or sets the input tag name.
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
		/// Gets or sets the checked value.
		/// </summary>
		public string Checked
		{
			get
			{
				return _checked;
			}
			set
			{
				_checked = value;
			}
		}

		/// <summary>
		/// Gets or sets the maxlenght value.
		/// </summary>
		public string MaxLength
		{
			get
			{
				return _maxlength;
			}
			set
			{
				_maxlength=value;
			}
		}
		/// <summary>
		/// Gets or sets the read only value.
		/// </summary>
		public string ReadOnly
		{
			get
			{
				return _readonly;
			}
			set
			{
				_readonly = value;
			}
		}
	}
}
