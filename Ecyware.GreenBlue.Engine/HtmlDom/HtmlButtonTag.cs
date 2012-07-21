// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003
using System;

namespace Ecyware.GreenBlue.Engine.HtmlDom
{
	public enum HtmlButtonType
	{
		Button,
		Reset,
		Submit
	}
	/// <summary>
	/// Contains the properties for a button tag.
	/// </summary>
	[Serializable]
	public class HtmlButtonTag:HtmlTagBase
	{
		string _name;
		string _value;
		HtmlButtonType _type;

		/// <summary>
		/// Creates a new button tag.
		/// </summary>
		public HtmlButtonTag()
		{			
		}

		/// <summary>
		/// Gets or sets the button type.
		/// </summary>
		public HtmlButtonType Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type=value;
			}
		}
		/// <summary>
		/// Gets or sets the button value.
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
		/// Gets or sets the button name.
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


	}
}
