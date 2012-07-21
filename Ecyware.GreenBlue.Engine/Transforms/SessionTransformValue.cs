using System;
using System.Xml.Serialization;
using Ecyware.GreenBlue.Engine.Scripting;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for SessionTransformValue.
	/// </summary>
	[Serializable]
	public class SessionTransformValue : TransformValue
	{
		private string _name;

		/// <summary>
		/// Creates a new DefaultTransformValue.
		/// </summary>
		public SessionTransformValue()
		{
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


		public override object GetValue(WebResponse response)
		{
			return ScriptingApplication.Session[_name];
		}

	}
}
