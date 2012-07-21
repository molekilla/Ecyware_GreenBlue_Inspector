using System;
using System.Xml.Serialization;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Contains the wrapper class for a web header.
	/// </summary>
	public class WebHeader
	{
		private string _name;
		private string _val;

		/// <summary>
		/// Creates a new WebHeader.
		/// </summary>
		public WebHeader()
		{
		}

		/// <summary>
		/// Creates a new WebHeader.
		/// </summary>
		/// <param name="name"> The web header name.</param>
		/// <param name="value"> The web header value.</param>
		public WebHeader(string name, string value)
		{
			_name = name;
			_val = value;
		}


		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		[XmlAttributeAttribute("name")]
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		[XmlAttributeAttribute("value")]
		public string Value
		{
			get
			{
				return _val;
			}
			set
			{
				_val = value;
			}
		}


		/// <summary>
		/// Fills the web header collection.
		/// </summary>
		/// <param name="headers"> The web header collection to fill.</param>
		/// <param name="values"> The web header array.</param>
		public static void FillWebHeaderCollection(System.Net.WebHeaderCollection headers, WebHeader[] values)
		{
			for (int i=0;i<values.Length;i++)
			{
				if ( headers[values[i].Name] != null )
				{
					headers[values[i].Name] = values[i].Value;
				} 
				else 
				{
					headers.Add(values[i].Name, values[i].Value);
				}
			}
		}

		/// <summary>
		/// Converts the WebHeaderCollection to a WebHeader array.
		/// </summary>
		/// <param name="headers"> The WebHeaderCollection to convert.</param>
		public static WebHeader[] ToArray(System.Net.WebHeaderCollection headers)
		{
			WebHeader[] array = new WebHeader[headers.Count];

			for (int i=0;i<headers.Count;i++)
			{
				string name = headers.GetKey(i);
				string val = headers[name];
				array[i] = new WebHeader(name, val);
			}

			return array;
		}
	}
}
