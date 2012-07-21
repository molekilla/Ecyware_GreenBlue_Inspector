// All rights reserved.
// Title: GreenBlue Project
// Author(s): Rogelio Morrell C.
// Date: December 2003
// Add additional authors here
using System;

namespace Ecyware.GreenBlue.WebUnitTestManager
{
	/// <summary>
	/// Summary description for DataTypesTesterArgs.
	/// </summary>
	[Serializable]
	public class SafeTesterArgs : EventArgs, IHtmlFormUnitTestArgs
	{
		private string _postData = string.Empty;

		public SafeTesterArgs()
		{
		}

		#region IHtmlFormUnitTestArgs Members

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
