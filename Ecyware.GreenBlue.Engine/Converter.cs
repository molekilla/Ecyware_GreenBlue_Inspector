// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Collections;
namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Contains logic for converting a sorted list to an arraylist.
	/// </summary>
	public sealed class Converter
	{
		private Converter()
		{
		}

		/// <summary>
		/// Converts a sorted list collection to an array list.
		/// </summary>
		/// <param name="coll"> The SortedList to convert.</param>
		/// <returns> An ArrayList.</returns>
		public static ArrayList ConvertToArrayList(SortedList coll)
		{
			ArrayList al = new ArrayList();
			foreach (DictionaryEntry de in coll)
			{
				al.Add(de.Value);
			}
			
			return al;
		}
	}
}
