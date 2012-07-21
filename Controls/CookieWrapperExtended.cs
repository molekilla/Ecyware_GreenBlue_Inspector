// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using System.Net;
using System.Windows.Forms;
using System.ComponentModel;
namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Wraps a cookie type converter.
	/// </summary>
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[DefaultProperty("Value")]
	public class CookieWrapperExtended : ExpandableObjectConverter
	{
		private Ecyware.GreenBlue.Engine.Scripting.Cookie _cookie = null;
		/// <summary>
		/// Creates a new CookieWrapper.
		/// </summary>
		public CookieWrapperExtended()
		{
		}

		/// <summary>
		/// Creates a new CookieWrapper.
		/// </summary>
		/// <param name="cookie"> The cookie to wrap.</param>
		public CookieWrapperExtended(Ecyware.GreenBlue.Engine.Scripting.Cookie cookie)
		{
			_cookie = cookie;
		}

		#region Overriden Methods
		/// <summary>
		/// Converts the given value object to the specified type.
		/// </summary>
		/// <param name="context">An ITypeDescriptorContext.</param>
		/// <param name="culture"> A CultureInfo.</param>
		/// <param name="value"> The object value.</param>
		/// <param name="destinationType"> The value to convert to.</param>
		/// <returns> The converted object.</returns>
		public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
		{
			if ( destinationType == typeof(string) && value is CookieWrapperExtended )
			{
				CookieWrapperExtended ckyWrapper = (CookieWrapperExtended)value;
				return ckyWrapper.Value;
			}
			return base.ConvertTo (context, culture, value, destinationType);
		}

		#endregion
		#region Properties

		/// <summary>
		/// Gets a cookie.
		/// </summary>
		/// <returns> A cookie type.</returns>
		[Browsable(false)]
		public Ecyware.GreenBlue.Engine.Scripting.Cookie GetCookie()
		{
			return _cookie;
		}

		/// <summary>
		/// Gets or sets a cookie name.
		/// </summary>
		[Browsable(false)]
		public string Name
		{
			get
			{
				return _cookie.Name;
			}
			set
			{
				_cookie.Name = value;
			}
		}


		/// <summary>
		/// Gets or sets a comment.
		/// </summary>
		[Description("Gets or sets a comment for the cookie.")]
		public string Comment
		{
			get
			{
				return _cookie.Comment;
			}
			set
			{
				_cookie.Comment = value;
			}
		}

		/// <summary>
		/// Gets or sets a comment uri.
		/// </summary>
		[Description("Gets or sets a comment uri for the cookie.")]
		public string CommentUri
		{
			get
			{
				return _cookie.CommentUri;
			}
			set
			{
				_cookie.Comment = value;
			}
		}

		/// <summary>
		/// Gets or sets the discard flag.
		/// </summary>
		[Description("Gets or sets the discard flag set by the server.")]
		public bool Discard
		{
			get
			{
				return _cookie.Discard;
			}
			set
			{
				_cookie.Discard = value;
			}
		}

		/// <summary>
		/// Gets or sets the domain.
		/// </summary>
		[Description("Gets or sets the URIs for which the cookie is valid.")]
		public string Domain
		{
			get
			{
				return _cookie.Domain;
			}
			set
			{
				_cookie.Domain = value;
			}
		}

		/// <summary>
		/// Gets or sets the expired flag.
		/// </summary>
		[Description("Gets or sets the current state for the cookie.")]
		public bool Expired
		{
			get
			{
				return _cookie.Expired;
			}
			set
			{
				_cookie.Expired = value;
			}
		}

		/// <summary>
		/// Gets or sets the expirs datetime.
		/// </summary>
		[Description("Gets or sets the expiration date and time for the cookie.")]
		public DateTime Expires
		{
			get
			{
				return _cookie.Expires;
			}
			set
			{
				_cookie.Expires = value;
			}
		}

		/// <summary>
		/// Gets or sets the cookie path.
		/// </summary>
		[Description("Gets or sets the URIs to which the cookie applies.")]
		public string Path
		{
			get
			{
				return _cookie.Path;
			}
			set
			{
				_cookie.Path = value;
			}
		}

		/// <summary>
		/// Gets or sets the port.
		/// </summary>
		[Description("Gets or sets a list of TCP ports that the cookie applies to.")]
		public string Port
		{
			get
			{
				return _cookie.Port;
			}
			set
			{
				_cookie.Port = value;
			}
		}

		/// <summary>
		/// Gets or sets the secure level.
		/// </summary>
		[Description("Gets or sets the security level of a cookie.")]
		public bool Secure
		{
			get
			{
				return _cookie.Secure;
			}
			set
			{
				_cookie.Secure = value;
			}
		}

		/// <summary>
		/// Gets the cookie timestamp.
		/// </summary>
		[Description("Gets the time when the cookie was issued")]
		public DateTime Timestamp
		{
			get
			{
				return _cookie.TimeStamp;
			}
		}

		/// <summary>
		/// Gets or sets the cookie value.
		/// </summary>
		//[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Gets or sets the cookie value.")]
		public string Value
		{
			get
			{
				return _cookie.Value;
			}
			set
			{
				_cookie.Value = value;
			}
		}

		/// <summary>
		/// Gets or sets the version.
		/// </summary>
		[Description("Gets or sets the version of HTTP state maintenance.")]
		public int Version
		{
			get
			{
				return _cookie.Version;
			}
			set
			{
				_cookie.Version = value;
			}
		}
		#endregion

	}
}
