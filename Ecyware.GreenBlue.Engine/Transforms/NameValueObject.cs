// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for NameValueObject.
	/// </summary>
	[Serializable]
	public class NameValueObject
	{
		private string _name;
		private string _value;
		/// <summary>
		/// Creates a name value object.
		/// </summary>
		public NameValueObject()
		{
		}

		/// <summary>
		/// Creates a name value object.
		/// </summary>
		public NameValueObject(string name, string value)
		{
			_name = name;
			_value = value;
		}

		/// <summary>
		/// Gets or sets the value.
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
	}
}
