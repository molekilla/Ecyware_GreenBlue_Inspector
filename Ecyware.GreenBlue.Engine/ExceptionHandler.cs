// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using Microsoft.ApplicationBlocks.ExceptionManagement;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Contains the exception handler that registers exceptions in the Event Log.
	/// </summary>
	public sealed class ExceptionHandler
	{
		private ExceptionHandler()
		{
		}

		/// <summary>
		/// Register an exception in the event log.
		/// </summary>
		/// <param name="error"> The error to register.</param>
		/// <returns> A string with the error message.</returns>
		public static string RegisterException(Exception error)
		{
			try
			{
				// register error
				ExceptionManager.Publish(error);

				return error.Message;
			}
			catch (Exception ex)
			{
				return ex.Message;
			}
		}
	}
}
