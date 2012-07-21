// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Net;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// HttpState contains data related to the callbacks.
	/// </summary>
	public class HttpState : BaseHttpState
	{
	
		private UnitTestItem _testItem = null;
		private UnitTestResult _testResult = null;
		private bool _lastItem = false;
		private byte[] postdata = null;
		private int _srid = -1;
		private int _ssrCurrentId = -1;

		private SessionRequest _testRequest = null;

		/// <summary>
		/// Creates a new HttpState.
		/// </summary>
		public HttpState()
		{
		}

		/// <summary>
		/// Gets or sets the PostData in bytes.
		/// </summary>
		public byte[] PostData
		{
			get
			{
				return postdata;
			}
			set
			{
				postdata = value;
			}
		}

		/// <summary>
		/// For internal use only. Gets or sets the current id of the safe session.
		/// </summary>
		public int SafeSessionRequestCurrentId
		{
			get
			{
				return _ssrCurrentId;
			}
			set
			{
				_ssrCurrentId = value;
			}
		}
		/// <summary>
		/// Gets or sets the UnitTestItem for the request.
		/// </summary>
		public UnitTestItem TestItem
		{
			get
			{
				return _testItem;
			}
			set
			{
				_testItem = value;
			}
		}


		/// <summary>
		/// Gets or sets the last item flag in a web unit test sequence.
		/// </summary>
		public bool IsLastItem
		{
			get
			{
				return _lastItem;
			}
			set
			{
				_lastItem = value;
			}
		}


		/// <summary>
		/// Gets or sets the test session request.
		/// </summary>
		public SessionRequest TestSessionRequest
		{
			get
			{
				return _testRequest;
			}
			set
			{
				_testRequest = value;
			}
		}


		/// <summary>
		/// Get current id for the session request.
		/// </summary>
		public int SessionRequestId
		{
			get
			{
				return _srid;
			}
			set
			{
				_srid = value;
			}
		}

		/// <summary>
		/// Gets or sets the Test Result.
		/// </summary>
		public UnitTestResult TestResult
		{
			get
			{
				return _testResult;
			}
			set
			{
				_testResult = value;
			}
		}
	}
}
