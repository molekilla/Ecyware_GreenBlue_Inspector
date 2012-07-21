using System;
using System.Xml;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Configuration.XmlTypeSerializer;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Summary description for HistoryIndexConfigurationHandler.
	/// </summary>
	[ConfigurationHandler(typeof(HistoryIndex))]
	public class HistoryIndexConfigurationHandler : ConfigurationSection
	{
		public HistoryIndexConfigurationHandler()
		{
		}

		public override object Load(string sectionName, string fileName)
		{
			XmlDocument document = new XmlDocument();
			document.Load(fileName);

			// add EncryptedData to new document
			XmlNode node = document.DocumentElement;
			HistoryIndex index = null;

			if ( index == null )
			{
				if ( CanDeserialize(node.OuterXml) )
				{
					index = (HistoryIndex)this.Create(node.OuterXml);
				}
			}			

			return index;
		}

		public override void Save(object value, string sectionName, string fileName)
		{
			// Write
			XmlNode node = Serialize(value, true);

			// Write			
			XmlDocument document = new XmlDocument();
			XmlNode imported = document.ImportNode(node,true);
			document.AppendChild(imported);
			document.Save(fileName);
		}

	}
}
