// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Ecyware.GreenBlue.Engine.HtmlDom
{

	/// <summary>
	/// Contains the properties for HtmlTagBase.
	/// </summary>
	[Serializable]
	public class HtmlTagBase
		//: HtmlTagBaseCollection, ISerializable
	{		
		string _id;
		string _title;
		string _style;
		string _onclick;
		string _class;

		/// <summary>
		/// Creates a new HtmlTagBase
		/// </summary>
		public HtmlTagBase()
		{
		}

//		private HtmlTagBase(SerializationInfo info, StreamingContext context) : base(info, context)
//		{
//			this._id = info.GetString("Id");
//			this._title = info.GetString("Title");
//			this._style = info.GetString("Style");
//			this._onclick = info.GetString("OnClick");
//			this._class = info.GetInt32("Class");	
//		}

		/// <summary>
		/// Gets or sets the id.
		/// </summary>
		public string Id
		{
			get
			{
				return _id;
			}
			set
			{
				_id =  value;
			}
		}


		/// <summary>
		/// Gets or sets the style.
		/// </summary>
		public string Style
		{
			get
			{
				return _style;
			}
			set
			{
				_style = value;
			}
		}


		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		public string Title
		{
			get
			{
				return _title;
			}
			set
			{
				_title = value;
			}
		}

		/// <summary>
		/// Gets or sets the class.
		/// </summary>
		public string Class
		{
			get
			{
				return _class;
			}
			set
			{
				_class = value;
			}
		}

		/// <summary>
		/// Gets or sets the on click event name.
		/// </summary>
		public string OnClick
		{
			get
			{
				return _onclick;
			}
			set
			{
				_onclick=value;
			}
		}
		#region ISerializable Members

//		public void GetObjectData(SerializationInfo info, StreamingContext context)
//		{
//			// TODO:  Add HtmlTagBase.GetObjectData implementation
//		}

		#endregion
	}
}
