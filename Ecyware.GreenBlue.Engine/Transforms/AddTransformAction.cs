using System;
using Ecyware.GreenBlue.Engine.Scripting;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for AddTransformAction.
	/// </summary>
	[Serializable]
	public class AddTransformAction : TransformAction
	{
		private TransformValue _value;

		/// <summary>
		/// Creates a new AddTransformAction.
		/// </summary>
		public AddTransformAction()
		{
		}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		public TransformValue Value
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

		public override object ApplyTransformAction(WebResponse response)
		{
			base.ApplyTransformAction(response);

			return Value.GetValue(response);
		}
	}
}
