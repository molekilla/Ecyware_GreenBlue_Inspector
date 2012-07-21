// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using Ecyware.GreenBlue.Engine.Scripting;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for UpdateTransformValue.
	/// </summary>
	[Serializable]
	public class UpdateTransformAction : TransformAction
	{
		private TransformValue _value;

		/// <summary>
		/// Creates a new UpdateTransformAction.
		/// </summary>
		public UpdateTransformAction()
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
