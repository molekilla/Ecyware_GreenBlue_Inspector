// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: October 2004
using System;
using System.Data;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.Web.Services2.Security;
using Microsoft.Web.Services2.Security.Tokens;

namespace Ecyware.GreenBlue.LicenseServices.Client
{
	/// <summary>
	/// Summary description for QueryContext.
	/// </summary>
	[Serializable]
	public class ServiceContext
	{		
		private string _sessionID = string.Empty;
		
		/// <summary>
		/// Creates a new QueryContext.
		/// </summary>
		public ServiceContext()
		{	
		}

		/// <summary>
		/// Gets or sets the session id.
		/// </summary>
		public string SessionID
		{
			get
			{
				return _sessionID;
			}
			set
			{
				_sessionID = value;
			}
		}

	}
}
