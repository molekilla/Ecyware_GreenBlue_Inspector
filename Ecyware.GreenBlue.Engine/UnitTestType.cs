// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Contains the Unit Test types.
	/// </summary>
	public enum UnitTestType
	{
		/// <summary>
		/// Specifies the Buffer Overflow test.
		/// </summary>
		BufferOverflow,
		/// <summary>
		/// Specifies the Data Types test (integer, string, null string).
		/// </summary>
		DataTypes,
		/// <summary>
		/// Specifies the SQL Injection test.
		/// </summary>
		SqlInjection,
		/// <summary>
		/// Specifies a predefined filled HtmlFormTag test.
		/// </summary>
		Predefined,
		/// <summary>
		/// Specifies a Cross Site Scripting (XSS) test.
		/// </summary>
		XSS,
		/// <summary>
		/// Specifies a safe test, use internally.
		/// </summary>
		SafeTest
	}
}
