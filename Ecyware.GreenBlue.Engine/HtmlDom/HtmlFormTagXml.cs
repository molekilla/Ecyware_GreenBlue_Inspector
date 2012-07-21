using System;
using System.Collections;


namespace Ecyware.GreenBlue.Engine.HtmlDom
{
	/// <summary>
	/// Summary description for HtmlFormTagXml.
	/// </summary>
	public class HtmlFormTagXml
	{
		string _action;
		string _method = "get";
		string _name;
		string _enctype;
		string _onsubmit;
		int _formIndex;
		private ArrayList _elements = new ArrayList();

		/// <summary>
		/// Creates a new HtmlFormTagXml.
		/// </summary>
		public HtmlFormTagXml()
		{
		}

		/// <summary>
		/// Reads a HtmlFormTag and reads the values to the current object.
		/// </summary>
		/// <param name="form"></param>
		public void ReadHtmlFormTag(Ecyware.GreenBlue.Engine.HtmlDom.HtmlFormTag form)
		{
			this.Action = form.Action;
			this.Enctype = form.Enctype;
			this.FormIndex = form.FormIndex;
			this.Method = form.Method;
			this.Name = form.Name;
			this.OnSubmit = form.OnSubmit;
			
			_elements.Clear();

			// load elements
			for (int i = 0;i<form.AllKeys.Length;i++)
			{
				string key = form.AllKeys[i];
				HtmlTagBaseList list = (HtmlTagBaseList)form[key];

				HtmlTagListXml listXml = new HtmlTagListXml(key, list);
				_elements.Add(listXml);
			}
		}

		/// <summary>
		/// Writes the HtmlFormTagXml to a HtmlFormTag.
		/// </summary>
		/// <returns>A new cloned HtmlFormTag.</returns>
		public HtmlFormTag WriteHtmlFormTag()
		{
			HtmlFormTag form = new HtmlFormTag();
			form.Action = this.Action;
			form.Enctype = this.Enctype;
			form.FormIndex = this.FormIndex;
			form.Method = this.Method;
			form.Name = this.Name;
			form.OnSubmit = this.OnSubmit;
			
			
			foreach ( HtmlTagListXml listXml in Elements )
			{
				HtmlTagBaseList list = new HtmlTagBaseList();

				list.AddRange(listXml.Tags);
				form.Add(listXml.Name, list);
			}

			return form;
		}

		#region Properties

		/// <summary>
		/// Gets or sets the form index.
		/// </summary>
		public int FormIndex
		{
			get
			{
				return _formIndex;
			}
			set
			{
				_formIndex = value;
			}
		}

		/// <summary>
		/// Gets or sets the on submit event name.
		/// </summary>
		public string OnSubmit
		{
			get
			{
				return _onsubmit;
			}
			set
			{
				_onsubmit = value;
			}
		}

		/// <summary>
		/// Gets or sets the action.
		/// </summary>
		public string Action
		{
			get
			{
				return _action;
			}
			set
			{
				_action = value;
			}
		}

		/// <summary>
		/// Gets or sets the method.
		/// </summary>
		public string Method
		{
			get
			{
				return _method;
			}
			set
			{
				_method = value;
			}
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
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
		/// Gets or sets the encoding type.
		/// </summary>
		public string Enctype
		{
			get
			{
				return _enctype;
			}
			set
			{
				_enctype = value;
			}
		}
		#endregion


		public HtmlTagListXml[] Elements
		{
			get
			{
				return (HtmlTagListXml[])_elements.ToArray(typeof(HtmlTagListXml));
			}
			set
			{
				if ( value != null )
				_elements.AddRange(value);
			}			
		}
	}
}
