using System;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Summary description for FormMappingEventArgs.
	/// </summary>
	public class FormMappingEventArgs : EventArgs
	{
		private string _postData = string.Empty;
		private int _formCount = 0;
		private Uri _siteUri = null;

		public FormMappingEventArgs()
		{
		}

		public Uri SiteUri
		{
			get
			{
				return _siteUri;
			}
			set
			{
				_siteUri = value;
			}
			
		}
		public string PostData
		{
			get
			{
				return _postData;
			}
			set
			{
				_postData = value;
			}
		}
		public int FormCount
		{
			get
			{
				return _formCount;
			}
			set
			{
				_formCount = value;
			}
		}
	}
}
