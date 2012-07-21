// All rights reserved.
// Title: GreenBlue Project
// Author(s): Rogelio Morrell C.
// Date: December 2003
// Add additional authors here
using System;

namespace Ecyware.GreenBlue.Protocols.Http
{
	/// <summary>
	/// Summary description for HttpCookie.
	/// </summary>
	[Serializable]
	public class HttpCookie
	{
		public HttpCookie()
		{
		}

		int _version=Int32.MinValue;
		bool _expired;
		DateTime _expires;
		DateTime _timeStamp;
		string _port;
		bool _secure;
		string _path;
		string _name;
		string _value;
		string _domain;


		public bool Expired
		{
			get
			{
				return _expired;
			}
			set
			{
				_expired = value;
			}
		}
		public DateTime Expires
		{
			get
			{
				return _expires;
			}
			set
			{
				_expires = value;
			}
		}
		public string Path
		{
			get
			{
				return _path;
			}
			set
			{
				_path=value;
			}
		}
		public string Name
		{
			get
			{
				return _name;	
			}
			set
			{
				_name=value;
			}
		}
		public string Domain
		{
			get
			{
				return _domain;
			}
			set
			{
				_domain=value;
			}
		}
		public bool Secure
		{
			get
			{
				return _secure;
			}
			set
			{
				_secure = value;
			}
		}
		public string Port
		{
			get
			{
				return _port;
			}
			set
			{
				_port = value;
			}
		}
		public DateTime TimeStamp
		{
			get
			{
				return _timeStamp;
			}
			set
			{
				_timeStamp = value;
			}
		}
		public string Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value=value;
			}
		}
		public int Version
		{
			get
			{
				return _version;
			}
			set
			{
				_version = value;
			}
		}
	}
}
