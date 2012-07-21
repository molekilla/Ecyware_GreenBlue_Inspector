// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Contains the SqlInjectionTesterArgs type.
	/// </summary>
	[Serializable]
	public class SqlInjectionTesterArgs : EventArgs, IHtmlFormUnitTestArgs
	{
		private string _postData = string.Empty;

		/// <summary>
		/// Creates a new SqlInjectionTesterArgs.
		/// </summary>
		public SqlInjectionTesterArgs()
		{
		}

		/// <summary>
		/// Creates a new SqlInjectionTesterArgs.
		/// </summary>
		/// <param name="value"> The SQL Injection value.</param>
		public SqlInjectionTesterArgs(string value)
		{
			this.SqlValue = value;
		}
		private string _value=string.Empty;

		/// <summary>
		/// Gets or sets the SQL Injection value.
		/// </summary>
		public string SqlValue
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
		/// Gets or sets the post data.
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
