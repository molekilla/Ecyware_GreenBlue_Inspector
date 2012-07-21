// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: February 2004
using System;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Contains the XssInjectionTesterArgs type.
	/// </summary>
	[Serializable]
	public class XssInjectionTesterArgs : EventArgs, IHtmlFormUnitTestArgs
	{
		private string _postData = string.Empty;

		/// <summary>
		/// Creates a new XssInjectionTesterArgs.
		/// </summary>
		public XssInjectionTesterArgs()
		{
		}

		/// <summary>
		/// Creates a new XSSInjectionTesterArgs.
		/// </summary>
		/// <param name="value"> the value to use.</param>
		public XssInjectionTesterArgs(string value)
		{
			this.XssValue = value;
		}

		private string _value=string.Empty;

		/// <summary>
		/// Gets or sets the cross site scripting value.
		/// </summary>
		public string XssValue
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

		#region IHtmlFormUnitTestArgs Members

		/// <summary>
		/// Gets or sets the post data for the current test.
		/// </summary>
		public string PostData
		{
			get
			{
				return _postData;
			}
			set
			{
				_postData = value;
			}
		}

		#endregion
	}
}
