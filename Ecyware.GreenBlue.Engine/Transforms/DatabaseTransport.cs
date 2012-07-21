// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2005
using System;
using System.Text;
using System.Reflection;
using System.Collections;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms;
using System.Data;
using System.Data.Odbc;
using Ecyware.GreenBlue.Engine.Transforms.Designers;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for DatabaseTransport.
	/// </summary>
	[Serializable]
	public class DatabaseTransport : Transport
	{
		DefaultTransformValue _connectionString = new DefaultTransformValue(true);
		DefaultTransformValue _query = new DefaultTransformValue(true);

		public DatabaseTransport()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Gets or sets the ConnectionString.
		/// </summary>
		public DefaultTransformValue ConnectionString
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

		/// <summary>
		/// Gets or sets the sql query.
		/// </summary>
		public DefaultTransformValue Query
		{
			get
			{
				return _query;
			}
			set
			{
				_query = value;
			}
		}


		public override Argument[] GetArguments()
		{
		
			ArrayList arguments = new ArrayList();

			Argument arg = new Argument();
			arg.Name = "DatabaseTransport.ConnectionString";
			arguments.Add(arg);

			arg = new Argument();
			arguments.Add("DatabaseTransport.Query");
			arguments.Add(arg);

			if ( arguments.Count == 0 )
			{
				return null;
			} 
			else 
			{
				return (Argument[])arguments.ToArray(typeof(Argument));
			}
		}


		public override void Send(string[] payload)
		{
			string[] values = payload;

			OdbcConnection odbc = new OdbcConnection(this.ConnectionString.Value);
			
			// Mail sending
			try 
			{
				odbc.Open();
				string executeQuery = string.Empty;
				
				if ( values.Length > 0 )
				{
					executeQuery = String.Format(_query.Value, values);
				} 
				else 
				{
					executeQuery = _query.Value;
				}

				OdbcCommand command = new OdbcCommand(executeQuery,odbc);
				command.ExecuteNonQuery();				
			} 
			catch
			{
				throw;
				// nothing
			} 
			finally
			{
				if ( odbc.State != ConnectionState.Closed )
				{
					odbc.Close();
				}
			}

		}
	}
}
