// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: April 2005
using System;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Configuration.XmlTypeSerializer;
using Ecyware.GreenBlue.Engine.Transforms;
namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Summary description for ScriptingApplicationSerializer.
	/// </summary>
	[ConfigurationHandler(typeof(SecureLoginCredentials))]
	public sealed class SecureLoginCredentialsSerializer : ConfigurationSection
	{
		public SecureLoginCredentialsSerializer()
		{
		}
	}
}
