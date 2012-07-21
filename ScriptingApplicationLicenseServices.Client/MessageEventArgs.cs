using System;

namespace Ecyware.GreenBlue.LicenseServices.Client
{
	/// <summary>
	/// Summary description for MessageEventArgs.
	/// </summary>
	public class MessageEventArgs : EventArgs
	{
		private ServiceContext _context;

		/// <summary>
		/// Creates a new MessageEventArgs.
		/// </summary>
		public MessageEventArgs()
		{
		}

		/// <summary>
		/// Gets or sets the service context.
		/// </summary>
		public ServiceContext Message
		{
			get
			{
				return _context;
			}
			set
			{
				_context = value;
			}
		}
	}
}
