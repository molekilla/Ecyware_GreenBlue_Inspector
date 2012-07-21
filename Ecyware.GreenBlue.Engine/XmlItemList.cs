// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Xml;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Contains the logic for accesing a xml item/value list.
	/// </summary>
	public sealed class XmlItemList
	{
		private XmlItemList()
		{
		}

		/// <summary>
		/// Gets the value from the xml item/value list.
		/// </summary>
		/// <param name="name"> The item name.</param>
		/// <param name="fileName"> The xml file.</param>
		/// <returns> Returns a string containing the data.</returns>
		public static string GetValue(string name, string fileName)
		{
			XmlDocument document = new XmlDocument();

			try
			{
				document.Load(fileName);

				// get items
				XmlNode items = document.ChildNodes[1];

				// match node
				XmlNode matchNode = items.SelectSingleNode("item[@name=" + name + "]");

				if  ( matchNode == null )
				{
					return string.Empty;
				} 
				else 
				{
					return matchNode.InnerText;
				}
			}
			catch
			{
				throw;
			}
		}
	}
}
