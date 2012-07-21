// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for CookieTransformValue.
	/// </summary>
	[Serializable]
	public class CookieTransformValue : TransformValue
	{
		private string _name;
		/// <summary>
		/// Creates a new CookieTransformValue.
		/// </summary>
		public CookieTransformValue()
		{
		}		

		/// <summary>
		/// Gets or sets the cookie name.
		/// </summary>
		public string CookieName
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
		/// Gets the value from the web response.
		/// </summary>
		/// <param name="response"> The web response.</param>
		/// <returns> An object.</returns>
		public override object GetValue(WebResponse response)
		{	
			string result = string.Empty;

			foreach ( Ecyware.GreenBlue.Engine.Scripting.Cookie cookie in response.Cookies )
			{
				if ( cookie.Name.CompareTo(this.CookieName)  == 0 )
				{
					result = cookie.Value;
					break;
				}
			}

			return result;
		}

	}
}
