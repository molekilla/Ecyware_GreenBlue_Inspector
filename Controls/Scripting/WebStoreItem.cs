using System;
//using Ecyware.GreenBlue.LicenseServices.Client;

namespace Ecyware.GreenBlue.Controls.Scripting
{
	/// <summary>
	/// Summary description for WebStoreItem.
	/// </summary>
	public class WebStoreItem
	{
		string _description;
		DateTime _createDate;
		DateTime _updateDate;
		string _publisher;
		Guid _applicationID;
		decimal _rating;
		int _downloads;
		int _userRatingCount;
		string _applicationName;

		/// <summary>
		/// Creates a web store item.
		/// </summary>
		public WebStoreItem()
		{
		}

		/// <summary>
		/// Gets or sets the application id.
		/// </summary>
		public Guid ApplicationID
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
		/// Gets or sets the rating.
		/// </summary>
		public decimal Rating
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

		/// <summary>
		/// Gets or sets the downloads.
		/// </summary>
		public int Downloads
		{
			get
			{
				return _downloads;
			}
			set
			{
				_downloads = value;
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
		/// Gets or sets the user rating count.
		/// </summary>
		public int UserRatingCount
		{
			get
			{
				return _userRatingCount;
			}
			set
			{
				_userRatingCount = value;
			}
		}

		/// <summary>
		/// Gets or sets the publisher.
		/// </summary>
		public string Publisher
		{
			get
			{
				return _publisher;
			}
			set
			{
				_publisher = value;
			}
		}

		/// <summary>
		/// Gets or sets the date when the application was uploaded.
		/// </summary>
		public DateTime CreateDate
		{
			get
			{
				return _createDate;
			}
			set
			{
				_createDate = value;
			}
		}

		/// <summary>
		/// Gets or sets the date when the application was updated.
		/// </summary>
		public DateTime UpdateDate
		{
			get
			{
				return _updateDate;
			}
			set
			{
				_updateDate = value;
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
	}
}
