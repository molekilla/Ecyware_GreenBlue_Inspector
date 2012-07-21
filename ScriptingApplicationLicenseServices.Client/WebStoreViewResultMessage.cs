// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: March 2005
using System;

namespace Ecyware.GreenBlue.LicenseServices.Client
{
	/// <summary>
	/// Summary description for WebStoreViewResultMessage.
	/// </summary>
	public class WebStoreViewResultMessage : ServiceContext
	{
		WebStore _webStore = null;

		/// <summary>
		/// Creates a new WebStoreViewResultMessage.
		/// </summary>
		public WebStoreViewResultMessage()
		{
		}
	
		/// <summary>
		/// Gets or sets the web store view.
		/// </summary>
		public WebStore WebStoreView
		{
			get
			{
				return _webStore;
			}
			set
			{
				_webStore = value;
			}
		}
	}
}
