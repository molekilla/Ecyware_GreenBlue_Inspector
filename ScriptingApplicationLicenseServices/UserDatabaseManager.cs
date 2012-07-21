using System;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using Microsoft.ApplicationBlocks.Data;
using Ecyware.GreenBlue.LicenseServices.Client;

namespace Ecyware.GreenBlue.LicenseServices
{
	/// <summary>
	/// Summary description for UserDatabaseManager.
	/// </summary>
	internal class UserDatabaseManager
	{
		string _connectionString;

		/// <summary>
		/// Creates a new UserDatabaseManager.
		/// </summary>
		public UserDatabaseManager()
		{
		}

		/// <summary>
		/// Creates a new UserDatabaseManager.
		/// </summary>
		public UserDatabaseManager(string connectionString)
		{
			_connectionString = connectionString;
		}

		#region Web Store

		/// <summary>
		/// Rates the application.
		/// </summary>
		/// <param name="message"> A WebStoreRequestMessage type.</param>
		public void RateApplication(WebStoreRequestMessage message)
		{
			SqlParameter[] param = new SqlParameter[5];

			param[0] = new SqlParameter("@ApplicationID", SqlDbType.UniqueIdentifier);
			param[0].Value = new Guid(message.ApplicationID);

			param[1] = new SqlParameter("@Rating", SqlDbType.Int);
			param[1].Value = message.Rating;

			int affectedRows = SqlHelper.ExecuteNonQuery(_connectionString,CommandType.StoredProcedure, "RateApplication",param);
		}

		/// <summary>
		/// Gets the search web store view.
		/// </summary>
		/// <param name="message"> The web store view message.</param>
		/// <returns> The web store view result.</returns>
		public WebStoreViewResultMessage SearchWebStoreView(WebStoreViewMessage message)
		{
			// SearchWebStoreViewByKeyword
			// SearchWebStoreViewByPublisher
			SqlConnection connection = null;
			WebStoreViewResultMessage result = new WebStoreViewResultMessage();

			try
			{
				// Create connection string				
				connection = new SqlConnection(_connectionString);
				connection.Open();

				WebStore webStore = new WebStore();

				if ( message.UsePaging )
				{		
					string sp = string.Empty;
					
					if ( message.SearchByType == WebStoreViewMessage.SearchType.ByKeyword )
					{
						sp = "SearchWebStoreViewByKeyword";
					}
					if ( message.SearchByType == WebStoreViewMessage.SearchType.ByPublisher )
					{
						sp = "SearchWebStoreViewByPublisher";
					}

					webStore.EnforceConstraints = false;

					SqlHelper.FillDataset(connection,
						sp,
						webStore,
						new string[] {webStore.WebStoreApplications.TableName},
						new object[] {message.SearchValue});

					webStore.EnforceConstraints = true;
				}			
					
				result.WebStoreView = webStore;
			}
			catch
			{
				throw;
			}
			finally
			{
				if ( connection.State != ConnectionState.Closed )
				{
					connection.Close();
				}
			}

			return result;

		}

		/// <summary>
		/// Gets the web store view.
		/// </summary>
		/// <param name="message"> The web store view message.</param>
		/// <returns> The web store view result.</returns>
		public WebStoreViewResultMessage GetWebStoreView(WebStoreViewMessage message)
		{
			SqlConnection connection = null;
			WebStoreViewResultMessage result = new WebStoreViewResultMessage();

			try
			{
				// Create connection string				
				connection = new SqlConnection(_connectionString);
				connection.Open();

				WebStore webStore = new WebStore();

				if ( message.UsePaging )
				{		
					webStore.EnforceConstraints = false;

					SqlHelper.FillDataset(connection,
						"GetWebStoreView",
						webStore,
						new string[] {webStore.WebStoreApplications.TableName},
						new object[] {message.StartFromIndex});

					webStore.EnforceConstraints = true;
				}			
					
				result.WebStoreView = webStore;
			}
			catch
			{
				throw;
			}
			finally
			{
				if ( connection.State != ConnectionState.Closed )
				{
					connection.Close();
				}
			}

			return result;
		}
		/// <summary>
		/// Gets the application from the web store.
		/// </summary>
		/// <param name="message"> The WebStoreRequestMessage type.</param>
		public string GetApplication(WebStoreRequestMessage message)
		{
			Guid appid = new Guid(message.ApplicationID);

			// Get Application Info
			DataSet application = SqlHelper.ExecuteDataset(_connectionString,"GetApplication", appid);
			DataTable table = application.Tables[0];
			string publisher = Convert.ToString(table.Rows[0]["Publisher"]);

			// Load Application from file
			string applicationData = WebStoreFileManager.LoadApplication(publisher, message);			
			SqlHelper.ExecuteNonQuery(_connectionString, "UpdateApplicationDownloadCount", appid);

			return applicationData;
		}
		/// <summary>
		/// Adds the application.
		/// </summary>
		/// <param name="username"> The username.</param>
		/// <param name="message"> The WebStoreRequestMessage message.</param>
		public void AddApplicationToWebStore(string username, WebStoreRequestMessage message)
		{
			SqlParameter[] param = new SqlParameter[5];

			param[0] = new SqlParameter("@ApplicationID", SqlDbType.UniqueIdentifier);
			param[0].Value = new Guid(message.ApplicationID);

			param[1] = new SqlParameter("@Description", SqlDbType.VarChar, 500);
			param[1].Value = message.Description;

			param[2] = new SqlParameter("@Keywords", SqlDbType.VarChar, 300);
			param[2].Value = message.Keywords;

			param[3] = new SqlParameter("@Publisher", SqlDbType.VarChar, 20);
			param[3].Value = username;

			param[4] = new SqlParameter("@ApplicationName", SqlDbType.VarChar, 50);
			param[4].Value = message.ApplicationName;

			int affectedRows = SqlHelper.ExecuteNonQuery(_connectionString,CommandType.StoredProcedure, "AddApplicationToWebStore",param);

			if ( affectedRows > 0 )
			{
				// Add File
				WebStoreFileManager.SaveApplication(username,message);
			}
		}

		/// <summary>
		/// Removes the application.
		/// </summary>
		/// <param name="username"> The username.</param>
		/// <param name="message"> The WebStoreRequestMessage message.</param>
		public void RemoveScriptingApplication(string username, WebStoreRequestMessage message)
		{
			SqlParameter[] param = new SqlParameter[2];

			param[0] = new SqlParameter("@ApplicationID", SqlDbType.UniqueIdentifier);
			param[0].Value = new Guid(message.ApplicationID);

			param[1] = new SqlParameter("@Publisher", SqlDbType.VarChar, 20);
			param[1].Value = username;

			int affectedRows = SqlHelper.ExecuteNonQuery(_connectionString,CommandType.StoredProcedure, "RemoveApplication",param);

			if ( affectedRows > 0 )
			{
				// Add File
				WebStoreFileManager.RemoveApplication(username,message);
			}
		}

		/// <summary>
		/// Updates the application.
		/// </summary>
		/// <param name="username"> The username.</param>
		/// <param name="message"> The WebStoreRequestMessage message.</param>
		public void UpdateApplicationToWebStore(string username, WebStoreRequestMessage message)
		{
			SqlParameter[] param = new SqlParameter[5];

			param[0] = new SqlParameter("@ApplicationID", SqlDbType.UniqueIdentifier);
			param[0].Value = new Guid(message.ApplicationID);

			param[1] = new SqlParameter("@Description", SqlDbType.VarChar, 500);
			param[1].Value = message.Description;

			param[2] = new SqlParameter("@Keywords", SqlDbType.VarChar, 300);
			param[2].Value = message.Keywords;

			param[3] = new SqlParameter("@Publisher", SqlDbType.VarChar, 20);
			param[3].Value = username;

			param[4] = new SqlParameter("@ApplicationName", SqlDbType.VarChar, 50);
			param[4].Value = message.ApplicationName;

			int affectedRows = SqlHelper.ExecuteNonQuery(_connectionString,CommandType.StoredProcedure, "UpdateApplicationToWebStore",param);

			if ( affectedRows > 0 )
			{
				// Add File
				WebStoreFileManager.SaveApplication(username,message);
			}
		}

		#endregion
		/// <summary>
		/// Creates a new account.
		/// </summary>
		/// <param name="user"> The account type.</param>
		public void CreateAccount(Account user)
		{
			SqlHelper.ExecuteNonQuery(_connectionString, "InsertUser",
				user.Username, user.Name, user.Password, user.Email, user.Created, user.Created);
		}

		/// <summary>
		/// Updates the account.
		/// </summary>
		/// <param name="user"> The account type.</param>
		public void UpdateAccount(Account user)
		{
			SqlHelper.ExecuteNonQuery(_connectionString, "UpdateUser", 
				user.Username, user.Name, user.Email, user.Password);
		}

		/// <summary>
		/// Gets the account.
		/// </summary>
		/// <param name="username"> The username to lookup.</param>
		public Account GetAccount(string username)
		{
			DataSet accountDataSet = SqlHelper.ExecuteDataset(_connectionString, "GetUser", username);
			Account account = null;

			if ( accountDataSet.Tables.Count > 0 )
			{ 
				if ( accountDataSet.Tables[0].Rows.Count > 0 )
				{
					// Create Account object
					account = new Account();
					account.Username = Convert.ToString(accountDataSet.Tables[0].Rows[0]["Username"]);
					account.Name = Convert.ToString(accountDataSet.Tables[0].Rows[0]["Name"]);
					//account = accountDataSet.Tables[0].Rows[0]["Password"];
					account.Email = Convert.ToString(accountDataSet.Tables[0].Rows[0]["Email"]);
					account.Created = Convert.ToDateTime(accountDataSet.Tables[0].Rows[0]["Created"]);
					account.Updated = Convert.ToDateTime(accountDataSet.Tables[0].Rows[0]["Updated"]);
				}
			}

			return account;
		}

		/// <summary>
		/// Gets the password for the user.
		/// </summary>
		/// <param name="username"> The username to validate.</param>
		/// <returns> Returns true if password is validated, else false.</returns>
		public string GetPasswordToken(string connectionString, string username)
		{
			SqlParameter[] param = new SqlParameter[1];
			param[0] = new SqlParameter("@Username", SqlDbType.VarChar,20);
			param[0].Value = username;
			SqlDataReader reader = SqlHelper.ExecuteReader(connectionString,CommandType.StoredProcedure, "GetPassword",param);

			string password = string.Empty;

			if ( reader.Read() )
			{
				password = reader.GetString(0);
			} 

			reader.Close();

			return password;
		}

		/// <summary>
		/// Returns true if the user exists, else false.
		/// </summary>
		/// <param name="account"> The account to lookup.</param>
		/// <returns> True if found, else false.</returns>
		public bool UserExists(Account account)
		{
			SqlParameter[] param = new SqlParameter[2];

			param[0] = new SqlParameter("@Username", SqlDbType.VarChar,20);
			param[0].Value = account.Username;

			param[1] = new SqlParameter("@Found", SqlDbType.Int);
			param[1].Direction = ParameterDirection.ReturnValue;

			SqlHelper.ExecuteNonQuery(_connectionString,CommandType.StoredProcedure, "IsUserAvailable",param);

			int count = (int)param[1].Value;
			if ( count == 1 )
			{
				return true;
			} 
			else 
			{
				return false;
			}
		}

		#region License Methods
		/// <summary>
		/// Validates the application license limit.
		/// </summary>
		/// <param name="username"> The user name.</param>
		/// <returns> Returns true if valid, else false.</returns>
		public bool ValidateApplicationLicenseLimit(string username)
		{
			SqlParameter[] param = new SqlParameter[2];

			param[0] = new SqlParameter("@Username", SqlDbType.VarChar, 20);
			param[0].Value = username;

			param[1] = new SqlParameter("@Counter", SqlDbType.Int);
			param[1].Direction = ParameterDirection.ReturnValue;

			SqlHelper.ExecuteNonQuery(_connectionString,CommandType.StoredProcedure, "GetRegisteredApplicationCount",param);
			int count = (int)param[1].Value;
			int limit = (int)SqlHelper.ExecuteScalar(_connectionString, "GetApplicationLimitLicense", username);

			if ( count < limit )
			{
				// is valid
				return true;
			} 
			else 
			{
				return false;
			}
		}

		/// <summary>
		/// Validates that the application registered exists.
		/// </summary>
		/// <param name="username"> The username.</param>
		/// <param name="applicationID"> The application id.</param>
		/// <returns> Returns true if the application exists, else false.</returns>
		public bool RegisteredApplicationExists(string username, string applicationID)
		{
			SqlParameter[] param = new SqlParameter[3];

			param[0] = new SqlParameter("@RegisteredApplicationID", SqlDbType.UniqueIdentifier);
			param[0].Value = new Guid(applicationID);

			param[1] = new SqlParameter("@Username", SqlDbType.VarChar, 20);
			param[1].Value = username;

			param[2] = new SqlParameter("@Exists", SqlDbType.Bit);
			param[2].Direction = ParameterDirection.ReturnValue;

			SqlHelper.ExecuteNonQuery(_connectionString,CommandType.StoredProcedure, "RegisteredApplicationExists",param);

			bool exists = true;
			if ( Int32.Parse(param[2].Value.ToString()) == 0 )
			{
				exists = false;
			}
			return exists;
		}

		/// <summary>
		/// Registers a scripting application.
		/// </summary>
		/// <param name="username"> The username.</param>
		/// <param name="applicationID"> The application id.</param>
		/// <returns> Returns false if the the application already exists or has an invalid GUID, else true.</returns>
		public bool RegisterApplication(string username, string applicationID)
		{
			bool registered = false;

			if ( !RegisteredApplicationExists(username, applicationID) )
			{

				SqlParameter[] param = new SqlParameter[3];

				param[0] = new SqlParameter("@RegisteredApplicationID", SqlDbType.UniqueIdentifier);
				param[0].Value = new Guid(applicationID);

				param[1] = new SqlParameter("@Username", SqlDbType.VarChar, 20);
				param[1].Value = username;

				param[2] = new SqlParameter("@Inserted", SqlDbType.Bit);
				param[2].Direction = ParameterDirection.ReturnValue;

				SqlHelper.ExecuteNonQuery(_connectionString,CommandType.StoredProcedure, "AddRegisteredApplication",param);

				if ( Convert.ToBoolean(param[2].Value) )
				{
					registered = true;
				}
			}
		
			return registered;
		}
		#endregion
	}
}
