// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: October 2004
using System;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

namespace Ecyware.GreenBlue.Configuration
{
	/// <summary>
	/// Summary description for Provider.
	/// </summary>
	public class Provider
	{
		string _type;

		[XmlAttributeAttribute("type")]
		public string Type
		{
			get { return _type; }
			set { _type = value; }
		} 				
	}
}
