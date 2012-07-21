// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: July 2004
using System;
using Ecyware.GreenBlue.Engine.HtmlCommand;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Sniffer options contains the properties that apply to the user interface. Inherits from HtmlParserProperties.
	/// </summary>
	[Serializable]
	public class SnifferOptions : HtmlParserProperties
	{
		/// <summary>
		/// Creates a new SnifferOptions.
		/// </summary>
		public SnifferOptions()
		{
			this.RemoveDocumentType = true;
			this.RemoveScriptTags = true;
			this.RemoveStyleTags = false;
		}
	}
}
