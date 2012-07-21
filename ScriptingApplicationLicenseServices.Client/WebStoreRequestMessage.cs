// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: March 2005
using System;

namespace Ecyware.GreenBlue.LicenseServices.Client
{
	/// <summary>
	/// Summary description for WebStoreRequestMessage.
	/// </summary>
	public class WebStoreRequestMessage : ServiceContext
	{
		string _description;
		string _applicationID;
		string _keywords;
		string _applicationName;
		string _data;
		string _useWebStore;
		int _rating;

		/// <summary>
		/// Creates a new WebStoreRequestMessage.
		/// </summary>
		public WebStoreRequestMessage()
		{
		}

		/// <summary>
		/// Gets or sets the scripting application header.
		/// </summary>
		public string ApplicationID
		{
			get
			{
				return _applicationID;
			}
			set
			{
				_applicationID = value;
			}
		}

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		public string Description
		{
			get
			{
				return _description;
			}
			set
			{
				_description = value;
			}
		}

		/// <summary>
		/// Gets or sets the keywords.
		/// </summary>
		public string Keywords
		{
			get
			{
				return _keywords;
			}
			set
			{
				_keywords = value;
			}
		}

		/// <summary>
		/// Gets or sets the application name.
		/// </summary>
		public string ApplicationName
		{
			get
			{
				return _applicationName;
			}
			set
			{
				_applicationName = value;
			}
		}

		/// <summary>
		/// Gets or sets the application payload data.
		/// </summary>
		public string ApplicationData
		{
			get
			{
				return _data;
			}
			set
			{
				_data = value;
			}
		}

		/// <summary>
		/// Gets or sets the web store.
		/// </summary>
		public string UseWebStore
		{
			get
			{
				return _useWebStore;
			}
			set
			{
				_useWebStore = value;
			}
		}

		/// <summary>
		/// Gets or sets the rating.
		/// </summary>
		public int Rating
		{
			get
			{
				return _rating;
			}
			set
			{
				_rating = value;
			}
		}
	}
}
