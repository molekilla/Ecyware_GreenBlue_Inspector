// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;

namespace Ecyware.GreenBlue.Engine
{

	/// <summary>
	/// Contains the SessionCommandProcessEventArgs type.
	/// </summary>
	public class SessionCommandProcessEventArgs : EventArgs
	{
		private string _message = string.Empty;
		private string _detail = string.Empty;
		private SessionProcessType _processType = SessionProcessType.SafeRequest;

		/// <summary>
		/// Creates a new SessionCommandProcessEventArgs.
		/// </summary>
		public SessionCommandProcessEventArgs()
		{
		}

		/// <summary>
		/// Creates a new SessionCommandProcessEventArgs.
		/// </summary>
		/// <param name="message"> The process event message.</param>
		public SessionCommandProcessEventArgs(string message)
		{
			this.Message = message;
		}

		/// <summary>
		/// Gets or sets the process type.
		/// </summary>
		public SessionProcessType ProcessType
		{
			get
			{
				return _processType;
			}
			set
			{
				_processType = value;
			}
		}

		/// <summary>
		/// Gets or sets the detail.
		/// </summary>
		public string Detail
		{
			get
			{
				return _detail;
			}
			set
			{
				_detail = value;
			}
		}

		/// <summary>
		/// Gets or sets the message.
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
