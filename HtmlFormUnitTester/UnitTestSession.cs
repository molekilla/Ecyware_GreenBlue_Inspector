using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Ecyware.GreenBlue.Protocols.Http;
using Ecyware.GreenBlue.WebUnitTestManager;

namespace Ecyware.GreenBlue.WebUnitTestCommand
{
	/// <summary>
	/// Contains the logic for a unit test session.
	/// </summary>
	[Serializable]
	public class UnitTestSession
	{
		private UnitTestFormCollection _unitTestForms = new UnitTestFormCollection();
		private Uri _uri=null;
		private ResponseBuffer _siteData = null;

		/// <summary>
		/// Create a new UnitTestSesion.
		/// </summary>
		public UnitTestSession()
		{
		}

		/// <summary>
		/// Create a new UnitTestSesion.
		/// </summary>
		/// <param name="stm"> The serialized UnitTestSession stream.</param>
		public UnitTestSession(Stream stm)
		{
			UnitTestSession uts = OpenUnitTestSession(stm);
			this.SessionData = uts.SessionData;
			this.SessionUri = uts.SessionUri;
			this.UnitTestForms = uts.UnitTestForms;
		}

		/// <summary>
		/// Gets or sets the ResponseBuffer data.
		/// </summary>
		public ResponseBuffer SessionData
		{
			get
			{
				return _siteData;
			}
			set
			{
				_siteData = value;
			}
		}
		/// <summary>
		/// Gets or sets the session uri.
		/// </summary>
		public Uri SessionUri
		{
			get
			{
				return _uri;
			}
			set
			{
				_uri = value;
			}
		}

		/// <summary>
		/// Gets or sets the unit test form collection.
		/// </summary>
		public UnitTestFormCollection UnitTestForms
		{
			get
			{
				return _unitTestForms;
			}
			set
			{
				_unitTestForms = value;
			}
		}


		#region Methods
		public int AvailableTests()
		{
			int availableTests=0;
			// get tests count
			for (int i=0;i<this.UnitTestForms.Count;i++)
			{
				UnitTestItem testItem = this.UnitTestForms.GetByIndex(i);
				availableTests += testItem.Tests.Count;
			}

			return availableTests;
		}
		public void SaveUnitTestSession(Stream stream)
		{
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(stream,this);
			stream.Close();
		}

		public static UnitTestSession OpenUnitTestSession(Stream stream)
		{
			BinaryFormatter bf = new BinaryFormatter();
			UnitTestSession session = (UnitTestSession)bf.Deserialize(stream);
			stream.Close();

			return session;
		}
		#endregion
	}
}
