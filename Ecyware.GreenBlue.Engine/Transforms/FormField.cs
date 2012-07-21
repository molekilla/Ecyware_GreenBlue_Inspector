// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using Ecyware.GreenBlue.Engine.Scripting;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for FormField.
	/// </summary>
	[Serializable]
	public class FormField
	{
		private string _description;
		private string _name;
		private int _index;
		private TransformValue _tvalue;


		/// <summary>
		/// Creates a new FormField.
		/// </summary>
		public FormField()
		{
		}

		/// <summary>
		/// Gets or sets the field name.
		/// </summary>
		public string FieldName
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
		/// Gets or sets the index.
		/// </summary>
		public int Index
		{
			get
			{
				return _index;
			}
			set
			{
				_index = value;
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
