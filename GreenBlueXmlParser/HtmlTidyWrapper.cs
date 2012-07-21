// All rights reserved.
// Title: GreenBlue Project
// Author(s): Rogelio Morrell C.
// Date: November 2003
// Add additional authors here
using System;
using Tidy;

namespace Ecyware.GreenBlue.HtmlProcessor
{
	/// <summary>
	/// Summary description for HtmlTidyWrapper.
	/// </summary>
	public class HtmlTidyWrapper
	{
		public HtmlTidyWrapper()
		{
		}

		public string CorrectHtmlString(string data)
		{
			DocumentClass tidyDoc = new DocumentClass();

			SetOptions(tidyDoc);
			tidyDoc.ParseString(data);
			tidyDoc.CleanAndRepair();
			tidyDoc.SetOptBool(TidyOptionId.TidyForceOutput,1);
			string result = tidyDoc.SaveString();
			tidyDoc = null;
			return result;
		}

		private void SetOptions(DocumentClass doc)
		{
			doc.SetOptInt(Tidy.TidyOptionId.TidyIndentSpaces,4);
			doc.SetOptValue(Tidy.TidyOptionId.TidyIndentContent,"auto");
			doc.SetOptValue(Tidy.TidyOptionId.TidyIndentAttributes,"yes");
			doc.SetOptValue(Tidy.TidyOptionId.TidyMark,"no");
		}
	}
}
