using System;
using System.Collections;
using System.Collections.Specialized;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Summary description for ReverseStringComparer.
	/// </summary>
	public class ReverseStringComparer : IComparer
	{
		public ReverseStringComparer()
		{
		}
		#region IComparer Members

		public int Compare(object x, object y)
		{
			return -String.Compare(x.ToString(),y.ToString());
		}

		#endregion
	}
}
