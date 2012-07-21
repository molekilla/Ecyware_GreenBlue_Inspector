// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003
using System;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Contains the BufferOverflowTesterArgs type.
	/// </summary>
	[Serializable]
	public class BufferOverflowTesterArgs : EventArgs, IHtmlFormUnitTestArgs
	{
		private string _postData = string.Empty;
		private int _bl = 100;

		/// <summary>
		/// Creates a new BufferOverflowTesterArgs.
		/// </summary>
		public BufferOverflowTesterArgs()
		{
		}

		/// <summary>
		/// Creates a new BufferOverflowTesterArgs.
		/// </summary>
		/// <param name="bufferLength"> The buffer length.</param>
		public BufferOverflowTesterArgs(int bufferLength)
		{
			this.BufferLength = bufferLength;
		}

		/// <summary>
		/// Gets or sets the buffer length.
		/// </summary>
		public int BufferLength
		{
			get
			{
				return _bl;
			}
			set
			{
				_bl = value;
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
