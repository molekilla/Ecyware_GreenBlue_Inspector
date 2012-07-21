// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Security.Permissions;
using System.IO;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Ecyware.GreenBlue.Engine.HtmlDom;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Contains the UnitTestItem type.
	/// </summary>
	[Serializable]
	[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
	public class UnitTestItem : ISerializable
	{
		private Hashtable _userPostData = null;
		private HtmlFormTag _form = null;
		private TestCollection _tc = new TestCollection();
		private int _selectedTest = Int32.MinValue;

		/// <summary>
		/// Creates a new UnitTestItem.
		/// </summary>
		public UnitTestItem()
		{
		}

		/// <summary>
		/// Creates a new UnitTestItem.
		/// </summary>
		/// <param name="form"> The form that the tests will be applied.</param>
		/// <param name="tests"> The test collection.</param>
		public UnitTestItem(HtmlFormTag form, TestCollection tests)
		{
			this.Form = form;
			this.Tests = tests;
		}

		/// <summary>
		/// Gets or sets the form.
		/// </summary>
		public HtmlFormTag Form
		{
			get
			{
				return _form;
			}
			set
			{
				_form = value;
			}
		}

		/// <summary>
		/// Gets or sets the PostData
		/// </summary>
		public Hashtable PostData
		{
			get
			{
				return _userPostData;
			}
			set
			{
				_userPostData = value;
			}
		}

		/// <summary>
		/// Gets or sets the test collection.
		/// </summary>
		public TestCollection Tests
		{
			get
			{
				return _tc;
			}
			set
			{
				_tc = value;
			}
		}

		/// <summary>
		/// Gets or sets the selected test index.
		/// </summary>
		public int SelectedTestIndex
		{
			get
			{
				return _selectedTest;
			}
			set
			{
				_selectedTest = value;
			}
		}

		/// <summary>
		/// Clones the current object into a new UnitTestItem.
		/// </summary>
		/// <returns>A new UnitTestItem.</returns>
		public UnitTestItem Clone()
		{
			// new memory stream
			MemoryStream ms = new MemoryStream();
			// new BinaryFormatter
			BinaryFormatter bf = new BinaryFormatter(null,new StreamingContext(StreamingContextStates.Clone));
			// serialize
			bf.Serialize(ms,this);
			// go to beggining
			ms.Seek(0,SeekOrigin.Begin);
			// deserialize
			UnitTestItem retVal = (UnitTestItem)bf.Deserialize(ms);
			ms.Close();

			return retVal;
		}
		#region ISerializable Members

		/// <summary>
		/// ISerializable private constructor.
		/// </summary>
		/// <param name="s"> SerializationInfo. </param>
		/// <param name="context"> The StreamingContext.</param>
		private UnitTestItem(SerializationInfo s, StreamingContext context)
		{
			this.Form = (HtmlFormTag)s.GetValue("Form", typeof(HtmlFormTag));			
			this.PostData = (Hashtable)s.GetValue("PostData",typeof(Hashtable));
			this.Tests = (TestCollection)s.GetValue("Tests", typeof(TestCollection));
			this.SelectedTestIndex = s.GetInt32("SelectedTestIndex");
		}

		/// <summary>
		/// Serializes the object.
		/// </summary>
		/// <param name="info"> SerializationInfo.</param>
		/// <param name="context"> StreamingContext.</param>
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("Form", this.Form);
			info.AddValue("PostData", this.PostData);
			info.AddValue("Tests", this.Tests);
			info.AddValue("SelectedTestIndex", this.SelectedTestIndex);
		}

		#endregion
	}
}
