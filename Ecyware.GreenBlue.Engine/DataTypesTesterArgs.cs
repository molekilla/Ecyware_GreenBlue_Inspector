// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003
using System;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Contains the DataTypesTesterArgs type.
	/// </summary>
	[Serializable]
	public class DataTypesTesterArgs : EventArgs, IHtmlFormUnitTestArgs
	{
		private string _postData = string.Empty;
		private DataType _selected=DataType.Null;

		/// <summary>
		/// Creates a new DataTypesTesterArgs.
		/// </summary>
		public DataTypesTesterArgs()
		{
		}

		/// <summary>
		/// Gets or sets the SelectedDataType.
		/// </summary>
		public DataType SelectedDataType
		{
			get
			{
				return _selected;
			}
			set
			{
				_selected = value;
			}
		}

		#region IHtmlFormUnitTestArgs Members

		/// <summary>
		/// Gets or sets the PostData.
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
