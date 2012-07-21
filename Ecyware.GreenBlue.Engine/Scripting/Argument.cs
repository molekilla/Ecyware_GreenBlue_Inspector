using System;
using System.ComponentModel;

namespace Ecyware.GreenBlue.Engine.Scripting
{
	/// <summary>
	/// Summary description for Argument.
	/// </summary>
	public class Argument
	{
		ArgumentProperty _property = new ArgumentProperty();
		string _name;
		string _value;

		/// <summary>
		/// Creates a new Argument.
		/// </summary>
		public Argument()
		{
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		[Browsable(false)]
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


		/// <summary>
		/// Gets or sets the design property.
		/// </summary>
		public ArgumentProperty DesignProperty
		{
			get
			{
				return _property;
			}
			set
			{
				_property = value;
			}
		}
	}
}
