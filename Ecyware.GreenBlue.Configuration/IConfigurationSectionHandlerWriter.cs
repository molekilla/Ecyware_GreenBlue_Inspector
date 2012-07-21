// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: October 2004
using System;
using System.ComponentModel;
using System.Configuration;
using System.Xml;
using System.Runtime.InteropServices;

namespace Ecyware.GreenBlue.Configuration
{
	/// <summary>
	/// Implemented by custom section handlers in order to allow a writeable implementation
	/// </summary>
	[ComVisible(false)]
	public interface IConfigurationSectionHandlerWriter
		: IConfigurationSectionHandler
	{	
		/// <summary>
		/// This method converts the public fields and read/write properties of an object into XML.
		/// </summary>
		XmlNode Serialize( object value );
	}
}
