using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Data;
using System.Runtime.InteropServices;
using mshtml;
using System.Reflection;
using Ecyware.GreenBlue.HtmlDom;

namespace Ecyware.GreenBlue.HtmlProcessor
{

	/// <summary>
	/// Summary description for HtmlScriptParser.
	/// </summary>
	public sealed class HtmlScriptParser
	{
		// HTML Document
		private IHTMLDocument2 internalDocument = null;
		private int _timeout = 10;
		private HtmlParser _htmlParser = new HtmlParser();

		public HtmlScriptParser()
		{
		}
		// The default COM Interop method
		[DispId(0)]
		public void DefaultMethod()
		{
			HTMLWindow2 win = (HTMLWindow2) internalDocument.parentWindow;
			System.Diagnostics.Debug.Write("Object: " + win.@event.srcElement + ", Type: " + win.@event.type);
		}

		#region Load document from url
		public IHTMLDocument2 GetDocumentWithData(HtmlScriptCollection scripts,string data)
		{
			// new class
			HTMLDocumentClass oDoc = new HTMLDocumentClass();

			// create class interface instances
			IHTMLDocument2 iDoc2a = (IHTMLDocument2)oDoc; 
			IHTMLDocument4 iDoc4 = (IHTMLDocument4)oDoc; 

			// This is the key ingredient - have to put some HTML 
			// in the DOM before using it, even though we're not 
			// accessing the DOM. 
			iDoc2a.write("<html></html>"); 
			iDoc2a.close(); 
			
			iDoc2a.parentWindow.onerror = this;

			Regex removeScripts = (Regex)_htmlParser.GetRegExpParserScripts["RemoveScripts"];
			MatchCollection matches = removeScripts.Matches(data);

			StringBuilder dataBuffer = new StringBuilder(data);
			scripts = CommentPopups(scripts);

			// parse html
			for (int i=0;i<matches.Count;i++)
			{
				HtmlScript scriptTag = scripts[i];

				Match m = matches[i];
				//dataBuffer.Remove(m.Index,m.Length);
				
				StringBuilder newScript = new StringBuilder();

				newScript.Append("<script");
				if ( scriptTag.Language.Length != 0 )
				{
					newScript.AppendFormat(" language=\"{0}\"",scriptTag.Language);
				}

				newScript.Append(">");
				newScript.Append(scriptTag.Text);
				newScript.Append("</script>");

				//dataBuffer.Insert(m.Index,newScript.ToString());
				dataBuffer.Replace(m.Value,newScript.ToString());
			}

			// write data
			iDoc2a.write(dataBuffer.ToString());
			iDoc2a.close();			

			return iDoc2a;
		}

		public IHTMLDocument2 GetDocument(string url)
		{
			// new class
			HTMLDocumentClass oDoc = new HTMLDocumentClass();

			// create class interface instances
			IHTMLDocument2 iDoc2a = (IHTMLDocument2)oDoc; 
			IHTMLDocument4 iDoc4 = (IHTMLDocument4)oDoc; 

			// This is the key ingredient - have to put some HTML 
			// in the DOM before using it, even though we're not 
			// accessing the DOM. 
			iDoc2a.write("<html></html>"); 
			iDoc2a.close(); 
			
			IHTMLDocument2 internalDocument = iDoc4.createDocumentFromUrl(url, "null");
			internalDocument.parentWindow.onerror = this;

			// wait for loading, timeout added if something happens
			DateTime timeout = DateTime.Now.AddSeconds(_timeout);
			while (internalDocument.readyState != "complete") 
			{
				System.Windows.Forms.Application.DoEvents(); 

				if ( DateTime.Now.CompareTo(timeout) > 0 )
				{
					break;
				}
			}

			return internalDocument;
		}

		public IHTMLDocument2 GetDocument(string url, int timeOut)
		{
			// new class
			HTMLDocument oDoc = new HTMLDocument(); 
			// create class interface instances
			IHTMLDocument2 iDoc2a = (IHTMLDocument2)oDoc; 
			IHTMLDocument4 iDoc4 = (IHTMLDocument4)oDoc; 

			// This is the key ingredient - have to put some HTML 
			// in the DOM before using it, even though we're not 
			// accessing the DOM. 
			iDoc2a.write("<html></html>"); 
			iDoc2a.close(); 

			IHTMLDocument2 internalDocument = iDoc4.createDocumentFromUrl(url, "null"); 

			// wait for loading, timeout added if something happens
			DateTime timeout = DateTime.Now.AddSeconds(timeOut);
			while (internalDocument.readyState != "complete") 
			{
				System.Windows.Forms.Application.DoEvents(); 

				if ( DateTime.Now.CompareTo(timeout) > 0 )
				{
					break;
				}
			}

			return internalDocument;
		}
		
		#endregion
		public IHTMLDocument2 HTMLDocument
		{
			get
			{
				return internalDocument;
			}
			set
			{
				internalDocument = value;
			}
		}
		#region Call Script functions
		public void ExecuteScript(IHTMLDocument2 document,string code, string language)
		{
			if ( code.Substring(0,2) != @"//" )
			{
				code=code.Replace("return","");
				code=code.Replace("((","(");
				code=code.Replace("))",")");

				if ( code.StartsWith("(") )
				{
					code = code.Substring(1);
				}
				try
				{
					object o = document.parentWindow.execScript(code,language);
				}
				catch
				{
					// do nothing
				}
			}
		}
		public void ExecuteFormSubmit(IHTMLDocument2 document, int formIndex)
		{
			this.ExecuteScript(document,"document.forms[" + formIndex + "].submit();","javascript");
		}
		public void InvokeScriptFunction(IHTMLDocument2 doc,string member) 
		{ 
			// Assumes a using statement in the file:
			// using System.Reflection;
			//Type t = script.GetType(); 		
			object script = doc.Script;
			Type t = script.GetType(); 
			t.InvokeMember(member, BindingFlags.InvokeMethod,null,script,null); 
		}
		public void InvokeScriptFunction(IHTMLDocument2 doc,string member, params object[] args)
		{ 
			// Assumes a using statement in the file:
			// using System.Reflection;			
			object script = doc.Script;
			Type t = script.GetType(); 
			t.InvokeMember(member, BindingFlags.InvokeMethod,null,script,args); 
		}

		#endregion
		#region Parse scripts from any alert or window.open
		public HtmlScriptCollection CommentPopups(HtmlScriptCollection scripts)
		{
			foreach (HtmlScript script in scripts)
			{
				string parsedString = string.Empty;
				// get Text value and send for parsing
				string scriptString = script.Text;
				parsedString = scriptString.Replace("alert(","//alert(");
				parsedString = parsedString.Replace("window.open(","0;//window.open(");
				

				// change submit for action, as a dummy
				parsedString = parsedString.Replace(".submit();",".action=\"dummy.asp\";");

				parsedString = parsedString.Replace(".focus();",".align=\"top\";");
				parsedString = parsedString.Replace(".select();",".align=\"top\";");

				script.Text = parsedString;
			}

			return scripts;
		}
		#endregion
		#region Set values from HtmlFormTag to DOM
		public void SetFormValue(IHTMLDocument2 doc, string id, string name,int formIndex, int index, string newValue)
		{
			IHTMLFormElement form = null;
			//object noll = null;
	
			// Get Form by index
			//form = (IHTMLFormElement)doc.forms.item(noll,formIndex);
			form = GetFormDocument(doc,formIndex);

			// Get elements
			IHTMLElement items = (IHTMLElement)form.elements;
			IHTMLElementCollection children = (IHTMLElementCollection)items.children;

			IHTMLElement item = null;

			try
			{
				// get using named item
				object temp = ((IHTMLElementCollection3)children).namedItem(name);

				try
				{
					// try IHTMLElement
					// check again if null
					if ( temp == null )
					{
						item = FindFormItem(children,name,index);

						if ( item == null )
						{
							// find by input
							item = FindFormItem((IHTMLElementCollection)form.tags("input"),name,index);
						}

						if ( item == null )
						{
							// find by textarea
							item = FindFormItem((IHTMLElementCollection)form.tags("textarea"),name,index);
						}

						if ( item == null )
						{
							// find by select
							item = FindFormItem((IHTMLElementCollection)form.tags("select"),name,index);
						}

						if ( item == null )
						{
							// find by button
							item = FindFormItem((IHTMLElementCollection)form.tags("button"),name,index);
						}
					} 
					else
					{
						item = (IHTMLElement)temp;
					}
				}
				catch
				{
					// check again
					if ( temp == null )
					{
						item = FindFormItem(children,name,index);
					} 
					else 
					{
						// then is IHTMLElementCollection
						item = FindItemFromCollection(temp,index);
					}
				}
			}
			catch
			{
				throw;
			}

			SetValueFromType(item, newValue);
		}
		private void SetValueFromType(IHTMLElement item, string newValue)
		{
			object noll = null;

			if ( item is HTMLSelectElementClass )
			{
				int selIndex = ((HTMLSelectElement)item).selectedIndex;
				HTMLOptionElementClass option = (HTMLOptionElementClass)((HTMLSelectElement)item).item(noll,selIndex);

				option.text = newValue;				
			}

			if ( item is HTMLInputElementClass )
			{
				((HTMLInputElementClass)item).value = newValue;
			}

			if ( item is HTMLTextAreaElementClass )
			{
				((HTMLTextAreaElementClass)item).value = newValue;
			}

			if ( item is HTMLButtonElementClass )
			{
				((HTMLButtonElementClass)item).value = newValue;
			}
		}
		#endregion
		#region Get values from DOM or FORM
		public string GetValue(IHTMLDocument2 doc, string id, string name, string tagName, int index)
		{

			if ( tagName == "a" )
				return string.Empty;

			IHTMLElement item = null;
			IHTMLElementCollection3 coll = (IHTMLElementCollection3)doc.all.tags(tagName);

			if ( id.Length > 0 )
			{

				item = (IHTMLElement)coll.namedItem(id);

				if ( item == null ) 
					item = (IHTMLElement)coll.namedItem(name);
			}

			try
			{

				string ret=String.Empty;

				if ( item is HTMLAnchorElement )
				{
					ret = ((HTMLAnchorElement)item).href;
					return ret;
				}

				if ( item is HTMLInputElement )
				{
					ret = ((HTMLInputElement)item).value;
					return ret;
				}

				if ( item is HTMLTextAreaElement )
				{
					ret = ((HTMLTextAreaElement)item).value;
					return ret;
				}

				if ( item is HTMLButtonElement )
				{
					ret = ((HTMLButtonElement)item).value;
					return ret;
				}

				if ( item is HTMLSelectElement )
				{
					ret = ((HTMLSelectElement)item).value;
					return ret;
				}

				return ret;
			}
			catch
			{
				return String.Empty;
			}

		}

		public IHTMLFormElement GetFormDocument(IHTMLDocument2 doc, int formIndex)
		{
			int i=0;
			foreach (IHTMLFormElement form in doc.forms)
			{
				if ( i == formIndex)
				{
					return form;
				}
				i++;
			}

			return null;
		}

		public string GetFormValue(IHTMLDocument2 doc, string id, string name,int formIndex, int index)
		{

			IHTMLFormElement form = null;
			//object noll = null;
	
			//form = (IHTMLFormElement)doc.forms.item(noll,formIndex);
			form = GetFormDocument(doc,formIndex);

			IHTMLElement items = (IHTMLElement)form.elements;
			IHTMLElementCollection children = (IHTMLElementCollection)items.children;

			IHTMLElement item = null;

			try
			{
				// get using named item
				object temp = ((IHTMLElementCollection3)children).namedItem(name);

				try
				{
					// try IHTMLElement
					// check again if null
					if ( temp == null )
					{
						item = FindFormItem(children,name,index);

						if ( item == null )
						{
							// find by input
							item = FindFormItem((IHTMLElementCollection)form.tags("input"),name,index);
						}

						if ( item == null )
						{
							// find by textarea
							item = FindFormItem((IHTMLElementCollection)form.tags("textarea"),name,index);
						}

						if ( item == null )
						{
							// find by select
							item = FindFormItem((IHTMLElementCollection)form.tags("select"),name,index);
						}

						if ( item == null )
						{
							// find by button
							item = FindFormItem((IHTMLElementCollection)form.tags("button"),name,index);
						}
					} 
					else
					{
						item = (IHTMLElement)temp;
					}
				}
				catch
				{
					// check again
					if ( temp == null )
					{
						item = FindFormItem(children,name,index);
					} 
					else 
					{
						// then is IHTMLElementCollection
						item = FindItemFromCollection(temp,index);
					}
				}
			}
			catch
			{
				throw;
			}

			return GetValueFromType(item);
		}

		private IHTMLElement FindFormItem(IHTMLElementCollection elements, string name, int index)
		{
			IHTMLElement retVal = null;

			// loop
			foreach (IHTMLElement el in elements)
			{
				if ( (el is HTMLInputElementClass) || (el is HTMLButtonElementClass) || (el is HTMLSelectElementClass) || (el is HTMLTextAreaElementClass) )
				{
					int itemCount = ((IHTMLElementCollection)el.children).length;

					if ( itemCount > 0 )
					{
						FindItemFromCollection(el,index);
					} 
					else 
					{
						IHTMLElement el2 = (IHTMLElement)el;
						if ( name == (string)el2.getAttribute("name",0) )
						{
							retVal = (IHTMLElement)el2;
							break;
						}					
					}
				} 
				else 
				{
					// get children
					//retVal = FindFormItem((IHTMLElementCollection)el.children,name,index);
				}
			}

			return retVal;
		}

		private IHTMLElement FindItemFromCollection(object item, int index)
		{
			IHTMLElementCollection items = (IHTMLElementCollection)item;
			IHTMLElement retVal = null;

			int k=0;
			foreach (IHTMLElement x in items)
			{
				// get by index
				//object none = null;
				if ( k == index)
				{
					retVal = x;
					break;
				}
				k++;
			}

			return retVal;
		}
		private string GetValueFromType(IHTMLElement item)
		{
			string ret=String.Empty;
			object noll = null;

			if ( item is HTMLSelectElementClass )
			{
				int selIndex = ((HTMLSelectElement)item).selectedIndex;
				HTMLOptionElementClass option = (HTMLOptionElementClass)((HTMLSelectElement)item).item(noll,selIndex);

				ret = option.text;
				return ret;
			}

			if ( item is HTMLInputElementClass )
			{
				ret = ((HTMLInputElementClass)item).value;
				return ret;
			}

			if ( item is HTMLTextAreaElementClass )
			{
				ret = ((HTMLTextAreaElementClass)item).value;
				return ret;
			}

			if ( item is HTMLButtonElementClass )
			{
				ret = ((HTMLButtonElementClass)item).value;
				return ret;
			}

			return ret;
		}
		#endregion
		private void SetFormValues(HtmlFormTag form)
		{
			foreach (DictionaryEntry dd in form)
			{
				FormEditorNode child = new FormEditorNode();
				HtmlTagBaseList controlArray = (HtmlTagBaseList)dd.Value;
				int controlIndex = 0;

				#region inner loop
				foreach (HtmlTagBase tag in controlArray)
				{
					//					// A tags
					//					if (tag is HtmlALinkTag)
					//					{
					//						HtmlALinkTag a=(HtmlALinkTag)tag;
					//						scriptParser.etValue(this.HTMLDocument,a.Id,a.Id,"a",controlIndex,)
					//						a.Text = ;
					//					}

					// Input tags
					if (tag is HtmlInputTag)
					{
						HtmlInputTag input=(HtmlInputTag)tag;
						scriptParser.SetFormValue(this.HTMLDocument,input.Id,input.Name,form.FormIndex,controlIndex,input.Value);
					}

					// Button tags
					if (tag is HtmlButtonTag)
					{
						HtmlButtonTag button = (HtmlButtonTag)tag;						
						scriptParser.SetFormValue(this.HTMLDocument,button.Id,button.Name,form.FormIndex,controlIndex,button.Value);
					}

					// Select tags
					if (tag is HtmlSelectTag)
					{
						HtmlSelectTag select = (HtmlSelectTag)tag;
						scriptParser.SetFormValue(this.HTMLDocument,select.Id,select.Name,form.FormIndex,controlIndex,select.Value);

					}
					
					// Textarea tags
					if (tag is HtmlTextAreaTag)
					{
						HtmlTextAreaTag textarea=(HtmlTextAreaTag)tag;
						scriptParser.SetFormValue(this.HTMLDocument,textarea.Id,textarea.Name,form.FormIndex,controlIndex,textarea.Value);

					}

					controlIndex++;
				}
				#endregion
			}
		}
		private void GetFormValues(HtmlFormTag form)
		{
			foreach (DictionaryEntry dd in form)
			{
				FormEditorNode child = new FormEditorNode();
				HtmlTagBaseList controlArray = (HtmlTagBaseList)dd.Value;
				int controlIndex = 0;

				#region inner loop
				foreach (HtmlTagBase tag in controlArray)
				{
					// A tags
					if (tag is HtmlALinkTag)
					{
						HtmlALinkTag a=(HtmlALinkTag)tag;
						a.Text = scriptParser.GetValue(this.HTMLDocument,a.Id,a.Id,"a",controlIndex);
					}

					// Input tags
					if (tag is HtmlInputTag)
					{
						HtmlInputTag input=(HtmlInputTag)tag;
						input.Value = scriptParser.GetFormValue(this.HTMLDocument,input.Id,input.Name,form.FormIndex,controlIndex);
					}

					// Button tags
					if (tag is HtmlButtonTag)
					{
						HtmlButtonTag button = (HtmlButtonTag)tag;
						button.Value = scriptParser.GetFormValue(this.HTMLDocument,button.Id,button.Name,form.FormIndex,controlIndex);
					}

					// Select tags
					if (tag is HtmlSelectTag)
					{
						HtmlSelectTag select = (HtmlSelectTag)tag;
						select.Value = scriptParser.GetFormValue(this.HTMLDocument,select.Id,select.Name,form.FormIndex,controlIndex);

					}
					
					// Textarea tags
					if (tag is HtmlTextAreaTag)
					{
						HtmlTextAreaTag textarea=(HtmlTextAreaTag)tag;
						textarea.Value = scriptParser.GetFormValue(this.HTMLDocument,textarea.Id,textarea.Name,form.FormIndex,controlIndex);

					}

					controlIndex++;
				}
				#endregion
			}
		}
	}
}
