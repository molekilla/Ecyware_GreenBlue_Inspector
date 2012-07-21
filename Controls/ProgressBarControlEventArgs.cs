// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004.
using System;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Contains the ProgressBarControlEventArgs class.
	/// </summary>
	public sealed class ProgressBarControlEventArgs : EventArgs
	{
		private string _message = string.Empty;

		/// <summary>
		/// Creates a new ProgressBarControlEventArgs.
		/// </summary>
		public ProgressBarControlEventArgs()
		{
		}

		/// <summary>
		/// Creates a new ProgressBarControlEventArgs.
		/// </summary>
		/// <param name="message"> The text message.</param>
		public ProgressBarControlEventArgs(string message)
		{
			this.Message = message;
		}

		/// <summary>
		/// Gets or sets the text message.
		/// </summary>
		public string Message
		{
			get
			{
				return _message;
			}
			set			
			{
				_message = value;
			}

		}
	}
}
