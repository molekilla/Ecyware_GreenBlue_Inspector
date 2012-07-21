// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Text;
using System.Web;
using System.Globalization;


namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Contains the encoding and decoding methods.
	/// </summary>
	public sealed class EncodeDecode
	{
		private EncodeDecode()
		{
		}

		/// <summary>
		/// Encodes the data with HTML encoding.
		/// </summary>
		/// <param name="data"> The data to encode.</param>
		/// <returns> A string with the applied encoding.</returns>
		public static string HtmlEncode(string data)
		{
			return HttpUtility.HtmlEncode(data);
		}

		/// <summary>
		/// Decodes the HTML encoded data.
		/// </summary>
		/// <param name="data"> The data to decode.</param>
		/// <returns> A string with the applied decoding.</returns>
		public static string HtmlDecode(string data)
		{
			return HttpUtility.HtmlDecode(data);
		}

		/// <summary>
		/// Encodes the data with Url encoding.
		/// </summary>
		/// <param name="data"> The data to encode.</param>
		/// <returns> A string with the applied encoding.</returns>
		public static string UrlEncode(string data)
		{
			return HttpUtility.UrlEncode(data);
		}

		/// <summary>
		/// Decodes the Url encoded data.
		/// </summary>
		/// <param name="data"> The data to decode.</param>
		/// <returns> A string with the applied decoding.</returns>
		public static string UrlDecode(string data)
		{
			return HttpUtility.UrlDecode(data);
		}

		/// <summary>
		/// Encodes the Base64 data.
		/// </summary>
		/// <param name="data"> The data to encode.</param>
		/// <returns> A string with the applied encoding.</returns>
		public static string Base64Encode(byte[] data)
		{
			return Convert.ToBase64String(data);
		}

		/// <summary>
		/// Decodes the Base64 data.
		/// </summary>
		/// <param name="data"> The data to decode</param>
		/// <returns> A string with the applied decoding.</returns>
		public static byte[] Base64Decode(string data)
		{
			return Convert.FromBase64String(data);
		}

		/// <summary>
		/// Converts the string data to byte array.
		/// </summary>
		/// <param name="data"> The string to convert.</param>
		/// <returns> A byte array.</returns>
		public static byte[] GetBytes(string data)
		{
			return Encoding.UTF8.GetBytes(data);
		}

		/// <summary>
		/// Converts the byte array to string array.
		/// </summary>
		/// <param name="data"> The byte array to convert.</param>
		/// <returns> A string.</returns>
		public static string GetString(byte[] data)
		{
			return Encoding.UTF8.GetString(data);
		}

		/// <summary>
		/// Encodes a string to Hex format.
		/// </summary>
		/// <param name="s"> A string to convert.</param>
		/// <returns> Returns a string.</returns>
		public static string GetHexEncode(string s)
		{
			int temp = 0;
			if (s.Length > 32767)
			{ 
				throw new ArithmeticException("Length out of range.");
			}
			else
			{
				StringBuilder cadena = new StringBuilder();
				foreach (char c in s)
				{
					temp = System.Text.Encoding.GetEncoding("Windows-1252").GetBytes(c.ToString())[0];

					if ( temp <= 15 )
						cadena.Append("0" + temp.ToString("X"));
					else
						cadena.Append(temp.ToString("X"));
				};
				return cadena.ToString();
			}
		}

		/// <summary>
		/// Decodes a string from Hex format.
		/// </summary>
		/// <param name="s"> A string.</param>
		/// <returns> Returns a string.</returns>
		public static string GetHexDecode(string s)
		{
			StringBuilder cadena = new StringBuilder();
			for (int i=0; i < s.Length;i=i+2)
			{
				//cadena.Append(ConvertToString( Convert.ToInt32("0x" + s.Substring(i,2),16) ) );
				cadena.Append( ConvertToString(s.Substring(i,2)) );
			}
			return cadena.Replace("\0","").ToString();
		}

		private static string ConvertToString(string hex)
		{
			return System.Text.Encoding.GetEncoding("Windows-1252").GetString( new byte[] {HexToByte(hex)} );
		}

		/// <summary>
		/// Converts 1 or 2 character string into equivalant byte value
		/// </summary>
		/// <param name="hex">1 or 2 character string</param>
		/// <returns>byte</returns>
		private static byte HexToByte(string hex)
		{
			if (hex.Length > 2 || hex.Length <= 0)
				throw new ArgumentException("hex must be 1 or 2 characters in length");
			byte newByte = byte.Parse(hex, System.Globalization.NumberStyles.HexNumber);
			return newByte;
		}

}
}
