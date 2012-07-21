using System;
using System.Xml.Serialization;
using Ecyware.GreenBlue.Engine.Scripting;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for DefaultTransformValue.
	/// </summary>
	[Serializable]
	public class DefaultTransformValue : TransformValue
	{
		private string _value;
		private bool _inputArgumentEnabled = false;

		/// <summary>
		/// Creates a new DefaultTransformValue.
		/// </summary>
		public DefaultTransformValue()
		{
		}

		/// <summary>
		/// Creates a new DefaultTransformValue.
		/// </summary>
		public DefaultTransformValue(bool enabledArgument)
		{
			_inputArgumentEnabled = enabledArgument;
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
		/// Gets or sets if the field is enabled for input argument.
		/// </summary>
		public bool EnabledInputArgument
		{
			get
			{
				return _inputArgumentEnabled;
			}
			set
			{
				_inputArgumentEnabled = value;
			}
		}

		public override object GetValue(WebResponse response)
		{
			return _value;
		}

	}
}
