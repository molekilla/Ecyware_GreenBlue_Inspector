// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Security.Permissions;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Contains logic for a user web session.
	/// </summary>
	[Serializable]
	[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
	public class Session : ISerializable
	{
		private SessionRequestList _requests = new SessionRequestList(24);
		private DateTime _date = DateTime.Now;
		private bool _safeRequestBacktracking = true;
		private bool _isCookieUpdatable = true;

		/// <summary>
		/// Creates a new Session object.
		/// </summary>
		public Session()
		{
		}

		/// <summary>
		/// ISerializable private constructor.
		/// </summary>
		/// <param name="s"> SerializationInfo. </param>
		/// <param name="context"> The StreamingContext.</param>
		private Session(SerializationInfo s, StreamingContext context)
		{
			this.SessionDate = s.GetDateTime("SessionDate");
			this.SessionRequests = (SessionRequestList)s.GetValue("SessionRequests", typeof(SessionRequestList));

			try
			{
				this.IsCookieUpdatable = s.GetBoolean("IsCookieUpdatable");
				this.AllowSafeRequestBacktracking = s.GetBoolean("AllowSafeRequestBacktracking");
			}
			catch
			{
				// ignore
			}
		}

		/// <summary>
		/// Serializes the object.
		/// </summary>
		/// <param name="info"> SerializationInfo.</param>
		/// <param name="context"> StreamingContext.</param>
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("SessionDate", this.SessionDate);
			info.AddValue("SessionRequests", this.SessionRequests);

			try
			{
				info.AddValue("IsCookieUpdatable", this.IsCookieUpdatable);
				info.AddValue("AllowSafeRequestBacktracking", this.AllowSafeRequestBacktracking);
			}
			catch
			{
			}
		}

		/// <summary>
		/// Gets or sets the value for applying safe request backtracking.
		/// </summary>
		public bool AllowSafeRequestBacktracking
		{
			get
			{
				return _safeRequestBacktracking;
			}
			set
			{
				_safeRequestBacktracking = value;
			}
		}
		/// <summary>
		/// Gets or sets whether to update cookies.
		/// </summary>
		public bool IsCookieUpdatable
		{
			get
			{
				return _isCookieUpdatable;
			}
			set
			{
				_isCookieUpdatable = value;
			}
		}
		/// <summary>
		/// Gets or sets the session datetime.
		/// </summary>
		public DateTime SessionDate
		{
			get
			{
				return _date;
			}
			set
			{
				_date = value;
			}
		}

		/// <summary>
		/// Gets or sets the session requests.
		/// </summary>
		public SessionRequestList SessionRequests
		{
			get
			{
				return _requests;
			}
			set
			{
				_requests = value;
			}
		}

		#region Methods
		/// <summary>
		/// Saves the current session to a stream.
		/// </summary>
		/// <param name="stream"> The stream to save the current session.</param>
		public void SaveWebSession(Stream stream)
		{
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(stream,this);
			stream.Close();
		}

		/// <summary>
		/// Opens a serialize session from a stream.
		/// </summary>
		/// <param name="stream"> The stream to load the session.</param>
		/// <returns> A session.</returns>
		public static Session OpenWebSession(Stream stream)
		{
			BinaryFormatter bf = new BinaryFormatter();
			Session session = (Session)bf.Deserialize(stream);
			stream.Close();

			return session;
		}

		/// <summary>
		/// Clones the current object into a new Session.
		/// </summary>
		/// <returns>A Session type.</returns>
		public Session CloneSession()
		{
			// new memory stream
			MemoryStream ms = new MemoryStream();
			// new BinaryFormatter
			BinaryFormatter bf = new BinaryFormatter(null,new StreamingContext(StreamingContextStates.Clone));
			// serialize
			bf.Serialize(ms, this);
			// go to beggining
			ms.Seek(0, SeekOrigin.Begin);
			// deserialize
			Session retVal = (Session)bf.Deserialize(ms);
			ms.Close();

			return retVal;
		}

		#endregion
	}
}
