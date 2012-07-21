// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: March 2005
using System;

namespace Ecyware.GreenBlue.LicenseServices.Client
{
	/// <summary>
	/// Summary description for WebStoreViewMessage.
	/// </summary>
	public class WebStoreViewMessage : ServiceContext
	{
		public enum SearchType
		{
			ByPublisher,
			ByKeyword
		}

		SearchType _searchType = SearchType.ByKeyword;
		string _searchValue = string.Empty;
		private int _pageSize = 10;
		private int _startFromIndex = 0;
		private bool _usePaging = false;

		/// <summary>
		/// Creates a new WebStoreViewMessage.
		/// </summary>
		public WebStoreViewMessage()
		{			
		}

		/// <summary>
		/// Gets or sets the search value.
		/// </summary>
		public string SearchValue
		{
			get
			{
				return _searchValue;
			}
			set
			{
				_searchValue = value;
			}
		}

		/// <summary>
		/// Gets or sets the search type.
		/// </summary>
		public SearchType SearchByType
		{
			get
			{
				return _searchType;
			}
			set
			{
				_searchType = value;
			}
		}

		/// <summary>
		/// Gets or sets the start from index.
		/// </summary>
		public int StartFromIndex
		{
			get
			{
				return _startFromIndex;
			}
			set
			{
				_startFromIndex = value;
			}
		}
		
		/// <summary>
		/// Gets or sets the use paging setting.
		/// </summary>
		public bool UsePaging
		{
			get
			{
				return _usePaging;
			}
			set
			{
				_usePaging = value;
			}
		}
		
		/// <summary>
		/// Gets or sets the page size.
		/// </summary>
		public int PageSize
		{
			get
			{
				return _pageSize;
			}
			set
			{
				_pageSize = value;
			}
		}
	}
}
