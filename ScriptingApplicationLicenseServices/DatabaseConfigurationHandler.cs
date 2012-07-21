using System;
using System.Xml;
using System.Security.Cryptography;
using System.IO;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Configuration.Encryption;
using Ecyware.GreenBlue.Configuration.XmlTypeSerializer;
using Ecyware.GreenBlue.LicenseServices.Client;

namespace Ecyware.GreenBlue.LicenseServices
{
	/// <summary>
	/// Summary description for DatabaseConfigurationHandler.
	/// </summary>
	[ConfigurationHandler(typeof(DatabaseConfiguration))]
	public class DatabaseConfigurationHandler : ConfigurationSection
	{
		public DatabaseConfigurationHandler()
		{
		}
	}
}
