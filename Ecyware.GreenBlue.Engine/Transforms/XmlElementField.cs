// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using Ecyware.GreenBlue.Engine.Scripting;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for XmlElementField.
	/// </summary>
	[Serializable]
	public class XmlElementField
	{
		private string _description;
		private string _location;
		private int _index;
		private string _name;
		private TransformValue _tvalue;

		/// <summary>
		/// Creates a new XmlElementField.
		/// </summary>
		public XmlElementField()
		{
		}

		/// <summary>
		/// Gets or sets the name of the element.
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
		/// Gets or sets a XPath query for the element location.
		/// </summary>
		public string Location
		{
			get
			{
				return _location;
			}
			set
			{
				_location = value;
			}
		}


		/// <summary>
		/// Gets or sets the transform value.
		/// </summary>
		public TransformValue TransformValue
		{
			get
			{
				return _tvalue;
			}
			set
			{
				_tvalue = value;
			}
		}

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		public string Description
		{
			get
			{
				return _description;
			}
			set
			{
				_description = value;
			}
		}
	}
}
