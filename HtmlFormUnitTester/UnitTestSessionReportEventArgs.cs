// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Collections;
using Ecyware.GreenBlue.Protocols.Http;

namespace Ecyware.GreenBlue.WebUnitTestCommand
{
	/// <summary>
	/// Contains the UnitTestSessionProcessEventArgs type.
	/// </summary>
	public class UnitTestSessionReportEventArgs : EventArgs
	{
		private ArrayList _report = null;

		/// <summary>
		/// Creates a new UnitTestSessionProcessEventArgs.
		/// </summary>
		public UnitTestSessionReportEventArgs()
		{
		}

		/// <summary>
		/// Gets or sets the report list.
		/// </summary>
		public ArrayList Report
		{
			get
			{
				return _report;
			}
			set
			{
				_report = value;
			}
		}
	}
}
