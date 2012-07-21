using System;
using System.Collections;

namespace Ecyware.GreenBlue.Engine.Scripting
{
	/// <summary>
	/// Summary description for InternalSession.
	/// </summary>
	public class InternalSession
	{
		private Hashtable _unSyncSession = new Hashtable();
		private Hashtable _syncSession;

		public InternalSession()
		{
			_syncSession = Hashtable.Synchronized(_unSyncSession);
		}

		/// <summary>
		/// Gets or sets the session.
		/// </summary>
		public object this[string key]
		{		
			get
			{
				if ( _syncSession.ContainsKey(key) )
				{
					return _syncSession[key];
				}
				else 
				{
					return null;
				}
			}
			set
			{
				if ( _syncSession.ContainsKey(key) )
				{
					_syncSession[key] = value;
				} 
				else 
				{
					_syncSession.Add(key, value);
				}
			}
		}
	}
}
