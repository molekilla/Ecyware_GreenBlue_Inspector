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
	/// Contains the logic for the session document.
	/// </summary>
	[Serializable]
	[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
	public class SessionDocument : ISerializable
	{
		private Session _safeSession = null;
		private Session _testSession = null;

		/// <summary>
		/// Creates a new SessionDocument.
		/// </summary>
		public SessionDocument()
		{
		}

		/// <summary>
		/// Creates a new SessionDocument.
		/// </summary>
		/// <param name="safeSession"> The safe session object.</param>
		/// <param name="testSession"> The test session object.</param>
		public SessionDocument(Session safeSession, Session testSession )
		{
			this._safeSession = safeSession;
			this._testSession = testSession;
		}

		/// <summary>
		/// ISerializable private constructor.
		/// </summary>
		/// <param name="s"> SerializationInfo. </param>
		/// <param name="context"> The StreamingContext.</param>
		private SessionDocument(SerializationInfo s, StreamingContext context)
		{
			this.SafeSession = (Session)s.GetValue("SafeSession", typeof(Session));
			this.TestSession = (Session)s.GetValue("TestSession", typeof(Session));
		}

		/// <summary>
		/// Serializes the object.
		/// </summary>
		/// <param name="info"> SerializationInfo.</param>
		/// <param name="context"> StreamingContext.</param>
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("SafeSession", this.SafeSession);
			info.AddValue("TestSession", this.TestSession);
		}

		/// <summary>
		/// Gets or sets the safe session.
		/// </summary>
		public Session SafeSession
		{
			get
			{
				return _safeSession;
			}
			set
			{
				_safeSession = value;
			}
		}

		/// <summary>
		/// Gets or sets the test session.
		/// </summary>
		public Session TestSession
		{
			get
			{
				return _testSession;
			}
			set
			{
				_testSession = value;
			}
		}

		/// <summary>
		/// Saves the current session to a stream.
		/// </summary>
		/// <param name="stream"> The stream to save the current session.</param>
		public void SaveSessionDocument(Stream stream)
		{
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(stream, this);
			stream.Close();
		}

		/// <summary>
		/// Opens a serialize session from a stream.
		/// </summary>
		/// <param name="stream"> The stream to load the session.</param>
		/// <returns> A session.</returns>
		public static SessionDocument OpenSessionDocument(Stream stream)
		{
			BinaryFormatter bf = new BinaryFormatter();
			SessionDocument doc = (SessionDocument)bf.Deserialize(stream);
			stream.Close();

			return doc;
		}


	}
}
