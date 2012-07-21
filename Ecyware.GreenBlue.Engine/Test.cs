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
	/// Contains the data container types.
	/// </summary>
	[Serializable]	
	public enum UnitTestDataContainer
	{
		/// <summary>
		/// A HtmlFormTag.
		/// </summary>
		HtmlFormTag,
		/// <summary>
		/// A hashtable with the post data.
		/// </summary>
		PostDataHashtable,
		/// <summary>
		/// Represents a url.
		/// </summary>
		NoPostData,
		/// <summary>
		/// Cookies
		/// </summary>
		Cookies
	}
	/// <summary>
	/// Contains the Test type.
	/// </summary>
	[Serializable]
	[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
	public class Test
	{
		private UnitTestType _name;
		private string _testTypeName = string.Empty;
		private IHtmlFormUnitTestArgs _args = null;
		private UnitTestDataContainer _dataContainer = UnitTestDataContainer.HtmlFormTag;

		public string _testname = String.Empty;

		/// <summary>
		/// Creates a new Test.
		/// </summary>
		public Test()
		{
		}

		/// <summary>
		/// Gets or sets the test name.
		/// </summary>
		public string Name
		{
			get
			{
				return _testname;
			}
			set
			{
				_testname = value;
			}
		}

		/// <summary>
		/// Gets or sets the UnitTestDataContainer to use.
		/// </summary>
		public UnitTestDataContainer UnitTestDataType
		{
			get
			{
				return _dataContainer;
			}
			set
			{
				_dataContainer = value;
			}
		}


		/// <summary>
		/// Gets or sets the html form test type.
		/// </summary>
		public UnitTestType TestType
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}

		/// <summary>
		/// Gets or sets the test type name.
		/// </summary>
		public string TestTypeName
		{
			get
			{
				return _testTypeName;
			}
			set
			{
				_testTypeName = value;
			}
		}

		/// <summary>
		/// Gets or sets the arguments associated with the test.
		/// </summary>
		public IHtmlFormUnitTestArgs Arguments
		{
			get
			{
				return _args;
			}
			set
			{
				_args = value;
			}
		}
	}
}
