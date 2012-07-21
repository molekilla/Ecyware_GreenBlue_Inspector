using System;

namespace Ecyware.GreenBlue.Engine.Scripting
{
	[Serializable]
	public class Cookie
	{
		int _version = 1;
		bool _expired;
		DateTime _expires;
		DateTime _timeStamp;
		string _port;
		bool _secure;
		string _path;
		string _name;
		string _value;
		string _domain;
		string _comment;
		string _commentUri;
		bool _discard;


		public Cookie()
		{
		}

		public bool Discard
		{
			get{ return _discard; }
			set{ _discard = value; }
		}


		public string Comment
		{
			get{ return _comment; }
			set{ _comment = value; }
		}

		public string CommentUri
		{
			get{ return _commentUri; }
			set{ _commentUri = value; }
		}


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
