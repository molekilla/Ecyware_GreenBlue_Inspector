using System;
using Ecyware.GreenBlue.Configuration;

namespace Ecyware.GreenBlue.LicenseServices.Client
{
	/// <summary>
	/// Summary description for AccountMessageSerializer.
	/// </summary>
	[ConfigurationHandler(typeof(AccountMessage))]
	public class AccountMessageSerializer : ConfigurationSection
	{
		/// <summary>
		/// Creates a new AccountMessageSerializer.
		/// </summary>
		public AccountMessageSerializer()
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
