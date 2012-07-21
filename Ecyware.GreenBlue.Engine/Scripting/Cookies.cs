using System;
using System.Net;
using System.Collections;

namespace Ecyware.GreenBlue.Engine.Scripting
{
	/// <summary>
	/// Summary description for Cookie.
	/// </summary>
	public class Cookies
	{
		private ArrayList _cookies = new ArrayList(); 

		/// <summary>
		/// Creates a new Cookies.
		/// </summary>
		public Cookies()
		{
		}

		public Cookie[] GetCookies()
		{
			return (Cookie[])_cookies.ToArray(typeof(Cookie));
		}

		public ArrayList CookieList()
		{
			return _cookies;
		}
		/// <summary>
		/// Creates a new Cookies.
		/// </summary>
		/// <param name="cookie"> A .NET Cookie.</param>
		public Cookies(System.Net.Cookie cookie)
		{
			AddCookie(cookie);
		}

		/// <summary>
		/// Creates a new Cookies.
		/// </summary>
		/// <param name="cookies"> A CookieCollection.</param>
		public Cookies(System.Net.CookieCollection cookies)
		{
			AddCookies(cookies);
		}

		/// <summary>
		/// Adds a CookieCollection.
		/// </summary>
		/// <param name="cookies"></param>
		public void AddCookies(CookieCollection cookies)
		{
			foreach ( System.Net.Cookie ck in cookies )
			{
				AddCookie(ck);
			}
		}

		/// <summary>
		/// Adds a .NET Cookie.
		/// </summary>
		/// <param name="cookie"></param>
		public void AddCookie(System.Net.Cookie cookie)
		{
			Ecyware.GreenBlue.Engine.Scripting.Cookie cky = new Cookie();
			cky.Comment = cookie.Comment;

			if ( cookie.CommentUri != null )
				cky.CommentUri = cookie.CommentUri.ToString();

			cky.Domain = cookie.Domain;
			cky.Discard = cookie.Discard;
			cky.Expired = cookie.Expired;
			cky.Expires = cookie.Expires;
			cky.Name = cookie.Name;
			cky.Path = cookie.Path;
			cky.Port = cookie.Port;
			cky.Secure = cookie.Secure;
			cky.TimeStamp = cookie.TimeStamp;
			cky.Value = cookie.Value;
			cky.Version = cookie.Version;

			_cookies.Add(cky);
		}
	}
}
