// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003
using System;

namespace Ecyware.GreenBlue.Engine.HtmlDom
{
	/// <summary>
	/// Contains the properties for the textarea tag.
	/// </summary>
	[Serializable]
	public class HtmlTextAreaTag : HtmlTagBase
	{
		
		string _name;
		string _value;


		/// <summary>
		/// Creates a new textarea tag.
		/// </summary>
		public HtmlTextAreaTag()
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

	}
}
