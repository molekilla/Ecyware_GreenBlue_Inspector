using System;
using System.Collections;

namespace Ecyware.GreenBlue.Engine.HtmlCommand
{
	/// <summary>
	/// Summary description for HtmlLightParserElement.
	/// </summary>
	public class HtmlLightParserElement
	{
		private ArrayList _elements = new ArrayList();

		/// <summary>
		/// Creates a new HtmlLightParserElement.
		/// </summary>
		public HtmlLightParserElement()
		{
		}

		/// <summary>
		/// Creates a new HtmlLightParserElement
		/// </summary>
		/// <param name="element"> The HTML Element.</param>
		public HtmlLightParserElement(string element)
		{
			Add(element);
		}

		/// <summary>
		/// Gets or sets the elements.
		/// </summary>
		public string[] Elements
		{
			get
			{
				return (string[])_elements.ToArray(typeof(string));
			}
			set
			{
				if ( value != null )
					_elements.AddRange(value);
			}
		}

		/// <summary>
		/// Creates a Html element collection for the current element.
		/// </summary>
		/// <param name="index"> The index of the element.</param>
		/// <param name="tagName"> The tag name.</param>
		/// <returns> A NameObjectCollection type.</returns>
		public NameObjectCollection CreateHtmlElement(int index, string tagName)
		{
			string element = this.GetElement(index);
			return HtmlLightParser.CreateHtmlElement(element, tagName);
		}

		/// <summary>
		/// Gets the attribute.
		/// </summary>
		/// <param name="elementValue"> The element value.</param>
		/// <param name="attributeName"> The attribute name.</param>
		/// <returns> A string value.</returns>
		public string GetAttribute(int index, string attributeName)
		{
			string element = this.GetElement(index);
			return HtmlLightParser.GetAttribute(element, attributeName);
		}



		/// <summary>
		/// Adds a HTML Element.
		/// </summary>
		/// <param name="element"> The HTML element.</param>
		public void Add(string element)
		{
			_elements.Add(element);
		}

		/// <summary>
		/// Determines whether an element is in the collection.
		/// </summary>
		/// <param name="element"> The element to search.</param>
		/// <returns> Returns true if found, else false.</returns>
		public bool Contains(string element)
		{
			return _elements.Contains(element);
		}

		/// <summary>
		/// Clear elements.
		/// </summary>
		public void Clear()
		{
			_elements.Clear();
		}

		/// <summary>
		/// Get element by index.
		/// </summary>
		/// <param name="index"> The index.</param>
		/// <returns> A HTML Element.</returns>
		public string GetElement(int index)
		{
			return (string)_elements[index];
		}

	}
}
