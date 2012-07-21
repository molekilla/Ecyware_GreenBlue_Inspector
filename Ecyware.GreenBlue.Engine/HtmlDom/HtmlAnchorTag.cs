// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: June 2004
using System;

namespace Ecyware.GreenBlue.Engine.HtmlDom
{
	/// <summary>
	/// Contains the properties for a link tag.
	/// </summary>
	[Serializable]
	public class HtmlAnchorTag : HtmlTagBase
	{

		string _pathName = string.Empty;
		string _mimeType = string.Empty;
		string _host = string.Empty;
		string _hostName = string.Empty;
		string _query = string.Empty;
		string _protocol = string.Empty;
		string _href = string.Empty;

		/// <summary>
		/// Creates a new anchor tag.
		/// </summary>
		public HtmlAnchorTag()
		{
		}

		/// <summary>
		/// Gets or sets the link URL reference.
		/// </summary>
		public string HRef
		{
			get
			{
				return _href;
			}
			set
			{
				_href = value;
			}
		}


		/// <summary>
		/// Gets or sets the protocol.
		/// </summary>
		public string Protocol
		{
			get
			{
				return _protocol;
			}
			set
			{
				_protocol = value;
			}
		}
		/// <summary>
		/// Gets or sets the query string.
		/// </summary>
		public string Query
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
		/// <summary>
		/// Gets or sets the pathname.
		/// </summary>
		public string Pathname
		{
			get
			{
				return _pathName;
			}
			set
			{
				_pathName = value;
			}
		}
		/// <summary>
		/// Gets or sets the MimeType
		/// </summary>
		public string MimeType
		{
			get
			{
				return _mimeType;
			}
			set
			{
				_mimeType = value;
			}
		}
		/// <summary>
		/// Gets or sets the host.
		/// </summary>
		public string Host
		{
			get
			{
				return _host;
			}
			set
			{
				_host = value;
			}
		}

		/// <summary>
		/// Gets or sets the hostname.
		/// </summary>
		public string Hostname
		{
			get
			{
				return _hostName;
			}
			set
			{
				_hostName = value;
			}
		}

	}
}
