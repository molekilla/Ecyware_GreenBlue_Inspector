using System;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.LicenseServices.Client;

namespace Ecyware.GreenBlue.LicenseServices
{
	/// <summary>
	/// Summary description for AccountSerializer.
	/// </summary>
	[ConfigurationHandler(typeof(Account))]
	public class AccountSerializer : ConfigurationSection
	{
		/// <summary>
		/// Creates a new AccountSerializer.
		/// </summary>
		public AccountSerializer()
		{
		}

		public override void Save(object value, string sectionName, string fileName)
		{
			base.Save (value, sectionName, fileName);
		}

		public override object Load(string sectionName, string fileName)
		{
			return base.Load (sectionName, fileName);
		}
	}
}
