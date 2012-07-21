// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;

namespace Ecyware.GreenBlue.Engine
{
	public class ResponseEventArgs : EventArgs
	{
		private ResponseBuffer _response = null;
		private BaseHttpState _state = null;


		/// <summary>
		/// Creates a new ResponseEventArgs.
		/// </summary>
		public ResponseEventArgs()
		{
		}

		/// <summary>
		/// Gets or sets the ResponseBuffer.
		/// </summary>
		public ResponseBuffer Response
		{
			get
			{
				return _response;
			}
			set
			{
				_response  = value;
			}
		}

		/// <summary>
		/// Gets or sets the HTTP state.
		/// </summary>
		public BaseHttpState State
		{
			get
			{
				return _state;
			}
			set
			{
				_state = value;
			}
		}
	}

}
