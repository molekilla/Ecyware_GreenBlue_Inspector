// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: July 2004
using System;
using Ecyware.GreenBlue.Engine;
// using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.HtmlDom;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Contains the definition for the UpdateSessionEventArgs type.
	/// </summary>
	public class UpdateSessionEventArgs
	{
		private Session _session;

		/// <summary>
		/// Creates a new UpdateSessionEventArgs.
		/// </summary>
		public UpdateSessionEventArgs()
		{
		}

		/// <summary>
		/// Gets or sets the session.
		/// </summary>
		public Session WebSession
		{
			get
			{
				return _session;
			}
			set
			{
				_session = value;
			}
		}
	}
}
