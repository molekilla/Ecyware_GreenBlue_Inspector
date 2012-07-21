// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using Ecyware.GreenBlue.Configuration;
using System.Xml.Serialization;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for TransformProvider.
	/// </summary>
	public class TransformProvider : Provider
	{
		private string _name;
		private string _tt;

		/// <summary>
		/// Creates a new TransformProvider.
		/// </summary>
		public TransformProvider()
		{
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		[XmlIgnore]
		public string Name
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

		[XmlIgnore]
		public string TransformType
		{
			get
			{
				return _tt;
			}
			set
			{
				_tt = value;
			}
		}
	}
}
