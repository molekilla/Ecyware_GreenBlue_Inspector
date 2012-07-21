using System;

namespace Ecyware.GreenBlue.HtmlProcessor
{
	/// <summary>
	/// Summary description for UriParser.
	/// </summary>
	public class UriParser
	{
		public UriParser()
		{
		}
		/// <summary>
		/// Converts the post data string to a Hashtable.
		/// </summary>
		/// <param name="data"> The post data string.</param>
		/// <param name="separator"> The main separator for the string.</param>
		/// <param name="nameValueSeparator"> The name value pair separator.</param>
		/// <returns> A Hashtable representation of the post data.</returns>
		public Hashtable ConvertQueryString(string data, string separator, string nameValueSeparator)
		{
			// PostData
			Hashtable postData = new Hashtable();
			string[] nameValueArray = data.Split(separator.ToCharArray());

			
			#region Parse with name value separator tag
			int i = 0;
			foreach ( string s in nameValueArray)
			{
				// parses using both separators
				if ( nameValueSeparator.Length > 0 )
				{
					string[] values = s.Split(nameValueSeparator.ToCharArray());
					string name = EncodeDecode.UrlDecode(values[0]);

					if ( postData.ContainsKey(name) )
					{
						ArrayList list = (ArrayList)postData[name];
						list.Add(values[1]);
					} 
					else 
					{
						if ( values.Length == 2 )
						{
							ArrayList list = new ArrayList(1);
							list.Add(values[1]);
							postData.Add(name, list);
						}
					}
				} 
				else 
				{
					// simple separator parsing
					ArrayList list = new ArrayList(1);
					list.Add(EncodeDecode.UrlDecode(s));
					postData.Add("Value " + i.ToString(), list);
				}
				i++;
			}
			#endregion

			return postData;
		}


		/// <summary>
		/// Converts the query string to an string.
		/// </summary>
		/// <param name="data"></param>
		/// <param name="separator"></param>
		/// <param name="nameValueSeparator"></param>
		/// <returns></returns>
		public string ConvertQueryHashtable(Hashtable data, string separator, string nameValueSeparator)
		{
			// QueryString
			StringBuilder queryString = new StringBuilder();
			
			foreach ( DictionaryEntry de in data )
			{
				ArrayList itemValues = (ArrayList)de.Value;
				string key = (string)de.Key;

				if ( nameValueSeparator.Length == 0 )
				{
					//queryString.Append(key);
					queryString.Append(separator);
					queryString.Append(itemValues[0]);
				} 
				else 
				{
					foreach ( string s in itemValues )
					{
						queryString.Append(key);
						queryString.Append(nameValueSeparator);
						queryString.Append(s);
						queryString.Append(separator);
					}
				}
			}

			return queryString.ToString();
		}

	}
}
