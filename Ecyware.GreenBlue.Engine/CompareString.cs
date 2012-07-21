// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Globalization;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Contains logic for comparing strings.
	/// </summary>
	public sealed class CompareString
	{
		/// <summary>
		/// Creates a new CompareString.
		/// </summary>
		private CompareString()
		{
		}

		/// <summary>
		/// Compare a string with another.
		/// </summary>
		/// <param name="originalValue"> The original value.</param>
		/// <param name="compareTo"> The value to compare against.</param>
		/// <returns> Returns true if equal, else false.</returns>
		public static bool Compare(string originalValue, string compareTo)
		{
			CompareInfo compare = System.Threading.Thread.CurrentThread.CurrentCulture.CompareInfo;

			if ( compare.Compare(originalValue, compareTo,CompareOptions.IgnoreCase) == 0 )
			{
				// are equal
				return true;
			} else {
				return false;
			}
		}
	}
}
