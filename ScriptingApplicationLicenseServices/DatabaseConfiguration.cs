using System;


namespace Ecyware.GreenBlue.LicenseServices
{
	/// <summary>
	/// Summary description for DatabaseConfiguration.
	/// </summary>
	public class DatabaseConfiguration
	{
		private string _connectionString;

		/// <summary>
		/// Creates a new DatabaseConfiguration.
		/// </summary>
		public DatabaseConfiguration()
		{
		}

		/// <summary>
		/// Gets or sets the connection string.
		/// </summary>
		public string ConnectionString
		{
			get
			{
				return _connectionString;
			}
			set
			{
				_connectionString = value;
			}
		}
	}
}
