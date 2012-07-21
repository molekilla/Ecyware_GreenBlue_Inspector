using System;
using System.Collections;

namespace Ecyware.GreenBlue.Engine.HtmlDom
{
	/// <summary>
	/// Summary description for HtmlTagListXml.
	/// </summary>
	public class HtmlTagListXml
	{
		ArrayList _tags = new ArrayList();
		private string _name;

		public HtmlTagListXml()
		{
		}

		public HtmlTagListXml(string key, HtmlTagBaseList list)
		{
			this.Name = key;

			foreach ( HtmlTagBase tag in list )
			{
				_tags.Add(tag);
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
				_name = value;
			}
		}

		public HtmlTagBase[] Tags
		{
			get
			{
				return (HtmlTagBase[])_tags.ToArray(typeof(HtmlTagBase));
			}
			set
			{
				_tags.AddRange(value);
			}
		}


		public void AddHtmlTag(HtmlTagBase tag)
		{
			_tags.Add(tag);
		}
	}
}
