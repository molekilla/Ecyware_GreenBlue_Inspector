using System;
using System.Collections;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Engine.Transforms;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms.Designers;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for SecurityTransform.
	/// </summary>
	[Serializable]
	[WebTransformAttribute("Security Transform", "input","Creates exploit data for unit testing web applications.")]
	[UITransformEditor(typeof(SecurityTransformDesigner))]	
	public class SecurityTransform : WebTransform
	{
		private ArrayList _actions = new ArrayList();

		/// <summary>
		/// Creates a new SecurityTransform.
		/// </summary>
		public SecurityTransform()
		{
		}

		/// <summary>
		/// Gets or sets the security transform actions.
		/// </summary>
		public SecurityTransformAction[] SecurityTransformActions
		{
			get
			{
				return (SecurityTransformAction[])_actions.ToArray(typeof(SecurityTransformAction));
			}
			set
			{
				if ( value != null )
					_actions.AddRange(value);
			}
		}

		#region Security Transform Action helper methods
		/// <summary>
		/// Adds a security transform action.
		/// </summary>
		/// <param name="action"> The SecurityTransformAction type.</param>
		public void AddSecurityTransformAction(SecurityTransformAction action)
		{
			_actions.Add(action);
		}

		/// <summary>
		/// Removes the security transform action.
		/// </summary>
		/// <param name="index"> The index.</param>
		public void RemoveSecurityTransformAction(int index)
		{
			_actions.RemoveAt(index);
		}

		/// <summary>
		/// Removes all security transform actions.
		/// </summary>
		public void RemoveAllSecurityTransformActions()
		{
			_actions.Clear();
		}

		/// <summary>
		/// Gets a security transform action.
		/// </summary>
		/// <param name="index"> The index.</param>
		/// <returns> A SecurityTransformAction type.</returns>
		public SecurityTransformAction GetSecurityTransformAction(int index)
		{
			return (SecurityTransformAction)_actions[index];
		}

		#endregion


		public override void ApplyTransform(Ecyware.GreenBlue.Engine.Scripting.WebRequest request, Ecyware.GreenBlue.Engine.Scripting.WebResponse response)
		{
			base.ApplyTransform (request, response);

			foreach ( SecurityTransformAction action in SecurityTransformActions )
			{
				// TODO
				action.ApplySecurityTransformAction(request, response);
			}
		}

	}


}
