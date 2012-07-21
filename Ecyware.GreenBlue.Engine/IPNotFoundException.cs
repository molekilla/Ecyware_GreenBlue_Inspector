// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Exception throw when no IP found in license.
	/// </summary>
	public class IPNotFoundException : Exception
	{
		/// <summary>
		/// Creates a new IPNotFoundException.
		/// </summary>
		public IPNotFoundException() : base()
		{
			this.Source = "Ecyware.GreenBlue.Engine";
		}


		/// <summary>
		/// Gets the exception message.
		/// </summary>
		public override string Message
		{
			get
			{
				return "IP not found in license.";
			}
		}

	}
}
