// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using System.Xml.Serialization;
using Ecyware.GreenBlue.Engine.Scripting;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for TransformValue.
	/// </summary>
	[Serializable]
	public abstract class TransformValue
	{
		/// <summary>
		/// Creates a new TransformValue.
		/// </summary>
		public TransformValue()
		{
		}

		/// <summary>
		/// Generates the value.
		/// </summary>
		/// <param name="response"> The web response.</param>
		/// <returns> An object with the value.</returns>
		public virtual object GetValue(WebResponse response)
		{
			return null;
		}
	}


	public class EmptyTransformValue : TransformValue
	{
		/// <summary>
		/// Creates a new EmptyTransformValue.
		/// </summary>
		public EmptyTransformValue()
		{
		}
	}
}
