using System;
using System.Xml.Serialization;
using Ecyware.GreenBlue.Engine.Scripting;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for SecurityTransformValue.
	/// </summary>
	[Serializable]
	public class SecurityTransformValue : TransformValue
	{
		private string _name;
		private string _testType;
		private string _value;

		/// <summary>
		/// Creates a new SecurityTransformValue.
		/// </summary>
		public SecurityTransformValue()
		{
		}

		/// <summary>
		/// Gets or sets the name for the security test.
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
		/// Gets or sets the test type.
		/// </summary>
		public string TestType
		{
			get
			{
				return _testType;
			}
			set
			{
				_testType = value;
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

		public override object GetValue(WebResponse response)
		{
			return _value;
		}

	}
}
