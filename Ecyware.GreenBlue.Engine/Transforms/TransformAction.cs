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
	/// Summary description for TransformAction.
	/// </summary>
	[Serializable]
	public abstract class TransformAction
	{
		private string _name;
		private string _description;

		/// <summary>
		/// Creates a new TransformAction.
		/// </summary>
		public TransformAction()
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

		/// <summary>
		/// Applies the transform action.
		/// </summary>
		/// <param name="response"> The web response.</param>
		/// <returns> An object with the value.</returns>
		public virtual object ApplyTransformAction(WebResponse response)
		{
			return null;
		}
	}
}
