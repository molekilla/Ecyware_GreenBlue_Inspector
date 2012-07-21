// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;

namespace Ecyware.GreenBlue.Engine
{
	public enum SessionProcessType
	{
		SafeRequest,
		TestRequest,
		Aborted,
		TestResultOk,
		TestResultError
	}

	/// <summary>
	/// Contains the SessionAbortEventArgs type.
	/// </summary>
	public class SessionAbortEventArgs : SessionCommandProcessEventArgs
	{
		private string _message = string.Empty;

		/// <summary>
		/// Creates a new SessionAbortEventArgs.
		/// </summary>
		public SessionAbortEventArgs() : base()
		{
			this.ProcessType = SessionProcessType.Aborted;
		}

		/// <summary>
		/// Gets or sets the error message.
		/// </summary>
		public string ErrorMessage
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
