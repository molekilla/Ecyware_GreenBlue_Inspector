// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: May 2004

using System;
using System.Net;
using System.Windows.Forms;
using System.ComponentModel;
using Ecyware.GreenBlue.Engine;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Wraps a HttpProperties type converter.
	/// </summary>
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[DefaultProperty("UserAgent")]
	public class HttpPropertiesWrapper : ExpandableObjectConverter
	{
		private HttpProperties _httpProperties = null;

		/// <summary>
		/// Creates a new HttpPropertiesWrapper.
		/// </summary>
		public HttpPropertiesWrapper()
		{
		}

		/// <summary>
		/// Creates a new HttpPropertiesWrapper.
		/// </summary>
		/// <param name="httpProperties"> The HttpProperties object to wrap.</param>
		public HttpPropertiesWrapper(HttpProperties httpProperties)
		{
			_httpProperties = httpProperties;
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
			if ( destinationType == typeof(string) && value is HttpPropertiesWrapper )
			{
				HttpPropertiesWrapper properties = (HttpPropertiesWrapper)value;
				return properties.UserAgent;
			}
			return base.ConvertTo (context, culture, value, destinationType);
		}

		#endregion

		#region Properties
		/// <summary>
		/// Gets the HttpProperties object.
		/// </summary>
		/// <returns> A HttpProperties.</returns>
		[Browsable(false)]
		public HttpProperties GetHttpProperties()
		{
			return _httpProperties;
		}

		/// <summary>
		/// Gets or sets the Accept header.
		/// </summary>
		[Description("Gets or sets the Accept header.")]
		public string Accept
		{
			get
			{
				return this._httpProperties.Accept;
			}
			set
			{
				this._httpProperties.Accept = value;
			}
		}


		/// <summary>
		/// Gets or sets the HTTP Authentication Settings.
		/// </summary>
		[Description("Gets or sets the HTTP authentication settings.")]
		public HttpAuthentication AuthenticationSettings
		{
			get
			{
				return this._httpProperties.AuthenticationSettings;
			}
			set
			{
				this.AuthenticationSettings = value;
			}
		}


		/// <summary>
		/// Gets or sets the content length header.
		/// </summary>
		[Description("Gets or sets the Content-Length header.")]
		public long ContentLength
		{
			get
			{
				return this._httpProperties.ContentLength;
			}
			set
			{
				this._httpProperties.ContentLength = value;
			}
		}


		/// <summary>
		/// Gets or sets the content type header.
		/// </summary>
		[Description("Gets or sets the Content-type header.")]
		public string ContentType
		{
			get
			{
				return this._httpProperties.ContentType;
			}
			set
			{
				this._httpProperties.ContentType = value;
			}
		}


		[Description("Gets or sets the If-Modified-Since header.")]
		public DateTime IfModifiedSince
		{
			get
			{
				return this._httpProperties.IfModifiedSince;
			}
			set
			{
				this._httpProperties.IfModifiedSince = value;
			}
		}


		/// <summary>
		/// Gets or sets the media type.
		/// </summary>
		[Description("Gets or sets the media type.")]
		public string MediaType
		{
			get
			{
				return this._httpProperties.MediaType;
			}
			set
			{
				this._httpProperties.MediaType = value;
			}
		}


		/// <summary>
		/// Gets or sets the referer header.
		/// </summary>
		[Description("Gets or sets the referer header.")]
		public string Referer
		{
			get
			{
				return this._httpProperties.Referer;
			}
			set
			{
				this._httpProperties.Referer = value;
			}
		}


		/// <summary>
		/// Gets or sets the transfer encoding.
		/// </summary>
		[Description("Gets or sets the Transfer Encoding.")]
		public string TransferEncoding
		{
			get
			{
				return this._httpProperties.TransferEncoding;
			}
			set
			{
				this._httpProperties.TransferEncoding = value;
			}
		}


		/// <summary>
		/// Gets or sets the keep alive.
		/// </summary>
		[Description("Gets or sets the keep alive setting.")]
		public bool KeepAlive
		{
			get
			{
				return this._httpProperties.KeepAlive;
			}
			set
			{
				this._httpProperties.KeepAlive = value;
			}
		}


		/// <summary>
		/// Gets or sets the pipeline setting.
		/// </summary>
		[Description("Gets or sets the pipeline setting.")]
		public bool Pipeline
		{
			get
			{
				return this._httpProperties.Pipeline;
			}
			set
			{
				this._httpProperties.Pipeline = value;
			}
		}


		/// <summary>
		/// Gets or sets the send chunked setting.
		/// </summary>
		[Description("Gets or sets the send chunked setting.")]
		public bool SendChunked
		{
			get
			{
				return this._httpProperties.SendChunked;
			}
			set
			{
				this._httpProperties.SendChunked = value;
			}
		}


		/// <summary>
		/// Gets or sets the UserAgent setting.
		/// </summary>
		[Description("Gets or sets the user agent setting.")]
		public string UserAgent
		{
			get
			{
				return this._httpProperties.UserAgent;
			}
			set
			{
				this._httpProperties.UserAgent = value;
			}
		}
		#endregion
	}
}
