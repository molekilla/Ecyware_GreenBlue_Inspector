// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004

using System;
using System.Text;
using mshtml;
using System.Collections;
using Ecyware.GreenBlue.Engine.HtmlDom;

namespace Ecyware.GreenBlue.Engine.HtmlCommand
{
	/// <summary>
	/// Contains logic for converting a MSHTML Form Element to a GB HtmlFormTag.
	/// </summary>
	public class FormConverter
	{
		/// <summary>
		/// Creates a new FormConverter.
		/// </summary>
		public FormConverter()
		{
		}

		/// <summary>
		/// Adds the end of line (/0).
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public string AddEndLine(string value)
		{
			if ( value.Length > 1 )
			{
				System.Diagnostics.Debug.WriteLine("Contains end of line:" + value.EndsWith("\0"));
				string end = value.Substring(value.Length - 1,1);
				char lineEnd = Char.Parse(end);
				if ( lineEnd.CompareTo('\0') > 0 )
				{
					// Add
					value += "\0";
				}
			}

			return value;
		}

		#region Form Convertion
		/// <summary>
		/// Converts a HTMLFormElementClass to a GB HtmlFormTag.
		/// </summary>
		/// <param name="formElement"> The HTMLFormElementClass to convert.</param>
		/// <param name="currentUri"> The current uri.</param>
		/// <returns> A HtmlFormTag.</returns>
		public HtmlFormTag ConvertToHtmlFormTag(HTMLFormElementClass formElement, Uri currentUri)
		{
			IHTMLElement el = (IHTMLElement)formElement.elements;
			ArrayList elements = new ArrayList();
			// Converts the element tag to an array list
			elements = ParseTags(elements,(IHTMLElementCollection)el.children);

			HtmlFormTag formTag = new HtmlFormTag();

			#region Set Form Properties
			if ( formElement.action == null )
			{
				formTag.Action = currentUri.Scheme + "://" + currentUri.Authority + currentUri.AbsolutePath;
			} 
			else 
			{
				formTag.Action = formElement.action;
			}

			formTag.Class = formElement.className;

			if ( formElement.method == null )
			{
				formTag.Method = "GET";
			} else {
				formTag.Method = formElement.method;
			}
			if ( formElement.encoding == null )
			{
				formTag.Enctype = "";
			} 
			else 
			{
				formTag.Enctype = formElement.encoding;
			}
			
			// formTag.FormIndex = formElement.
			formTag.Id = formElement.id;

			if ( formElement.name != null )
			{
				formTag.Name = formElement.name;
			}
			else 
			{
				if ( formElement.id != null )
				{
					formTag.Name = formElement.id;
				} 
				else 
				{
					formTag.Name = formElement.uniqueID;
					formTag.Id = formElement.uniqueID;
				}
			}
			formTag.OnSubmit = string.Empty;
			formTag.Style = string.Empty;
			formTag.Title = formElement.title;
			#endregion
			#region Add Tags
			foreach (object obj in elements)
			{
				// The last check is the most significant type

				#region Button Element
				if (obj is mshtml.HTMLInputButtonElementClass)
				{
					HtmlButtonTag button = CreateHtmlButtonTag((HTMLInputButtonElementClass)obj);
					
					if ( formTag.ContainsKey(button.Name) )
					{
						// just add the value
						HtmlTagBaseList array = ((HtmlTagBaseList)formTag[button.Name]);
						array.Add(button);
					} 
					else 
					{
						HtmlTagBaseList list = new HtmlTagBaseList();
						list.Add(button);
						formTag.Add(button.Name,list);
					}
					continue;
				}
				#endregion

				#region Select Element
				if (obj is mshtml.HTMLSelectElementClass)
				{
					HtmlSelectTag select = CreateHtmlSelectTag((HTMLSelectElementClass)obj);

					if ( formTag.ContainsKey(select.Name) )
					{
						// just add the value
						HtmlTagBaseList array = ((HtmlTagBaseList)formTag[select.Name]);
						array.Add(select);
					} 
					else 
					{
						HtmlTagBaseList list = new HtmlTagBaseList();
						list.Add(select);
						formTag.Add(select.Name,list);
					}
					continue;
				}
				#endregion

				#region Textarea Element
				if (obj is mshtml.HTMLTextAreaElementClass)
				{
					HtmlTextAreaTag textarea=CreateHtmlTextAreaTag((HTMLTextAreaElementClass)obj);
					if ( formTag.ContainsKey(textarea.Name) )
					{
						// just add the value
						HtmlTagBaseList array = ((HtmlTagBaseList)formTag[textarea.Name]);
						array.Add(textarea);
					} 
					else 
					{
						HtmlTagBaseList list = new HtmlTagBaseList();
						list.Add(textarea);
						formTag.Add(textarea.Name,list);
					}
					continue;
				}
				#endregion

				#region Input Element
				if (obj is mshtml.HTMLInputElementClass)
				{
					HtmlInputTag input = CreateHtmlInputTag((HTMLInputElementClass)obj);
					if ( formTag.ContainsKey(input.Name) )
					{
						// just add the value
						HtmlTagBaseList array = ((HtmlTagBaseList)formTag[input.Name]);
						array.Add(input);
					} 
					else 
					{
						HtmlTagBaseList list = new HtmlTagBaseList();
						list.Add(input);
						formTag.Add(input.Name,list);
					}
					continue;
				}
				#endregion
					
			}
			#endregion

			return formTag;
		}


		/// <summary>
		/// Creates a HtmlSelectTag from a HTMLSelectElementClass.
		/// </summary>
		/// <param name="selectElement"> The HTMLSelectElementClass to convert.</param>
		/// <returns> A HtmlSelectTag</returns>
		private HtmlSelectTag CreateHtmlSelectTag(HTMLSelectElementClass selectElement)
		{
			HtmlSelectTag tag = new HtmlSelectTag();

			tag.Class = selectElement.className;
			tag.Id = selectElement.id;
			if ( selectElement.name != null )
			{
				tag.Name = selectElement.name;
			}
			else 
			{
				if ( selectElement.id != null )
				{
					tag.Name = selectElement.id;
				} 
				else 
				{
					tag.Name = selectElement.uniqueID;
					tag.Id = selectElement.uniqueID;
				}
			}
			// TODO: parse from innerHtml
			// tag.OnClick = 
			// tag.Style=currentNode.GetAttribute("style",currentNode.NamespaceURI);
			tag.Title = selectElement.title;

			if ( selectElement.multiple )
			{
				tag.Multiple=true;
			}
			
			tag.Value = selectElement.value;

			// tag.Options = new HtmlOptionCollection();
			//object noll = null;

			IHTMLElementCollection options = (IHTMLElementCollection)selectElement.children;
			
			// while ( options.MoveNext() )
			foreach ( object obj in options )
			{				
				if ( obj is HTMLOptionElementClass )
				{
					HTMLOptionElementClass option = (HTMLOptionElementClass)obj; 
					
					HtmlOptionTag optionTag = new HtmlOptionTag();
					optionTag.Id = option.id;
					if ( option.selected )
					{
						optionTag.Selected=true;
					}

					if ( option.text == null )
					{
						optionTag.Text = string.Empty;
					} 
					else 
					{
						optionTag.Text = option.text;
					}

					if ( option.value == null )
					{
						optionTag.Value = string.Empty;
					} 
					else 
					{
						optionTag.Value = option.value;
					}

					optionTag.Key = "Option " + tag.Options.Length.ToString();
					tag.AddOptionTag(optionTag);
				}
			}

			return tag;

		}


		/// <summary>
		/// Creates a HtmlTextAreaTag from a HTMLTextAreaElementClass.
		/// </summary>
		/// <param name="textareaElement"> The HTMLTextAreaElementClass to convert.</param>
		/// <returns> A HtmlTextAreaTag.</returns>
		private HtmlTextAreaTag CreateHtmlTextAreaTag(HTMLTextAreaElementClass textareaElement)
		{
			HtmlTextAreaTag tag = new HtmlTextAreaTag();

			tag.Class = textareaElement.className;
			tag.Id = textareaElement.id;
			if ( textareaElement.name != null )
			{
				tag.Name = textareaElement.name;
			}
			else 
			{
				if ( textareaElement.id != null )
				{
					tag.Name = textareaElement.id;
				} 
				else 
				{
					tag.Name = textareaElement.uniqueID;
					tag.Id = textareaElement.uniqueID;
				}
			}
			// tag.OnClick = currentNode.GetAttribute("onclick",currentNode.NamespaceURI);
			// tag.Style = currentNode.GetAttribute("style",currentNode.NamespaceURI);
			tag.Title = textareaElement.title;
			tag.Value = textareaElement.value;


			return tag;

		}

		/// <summary>
		/// Creates a HtmlButtonTag from a HTMLInputElementClass.
		/// </summary>
		/// <param name="buttonElement"> The HTMLInputElementClass to convert.</param>
		/// <returns> A HtmlButtonTag.</returns>
		private HtmlButtonTag CreateHtmlButtonTag(HTMLInputButtonElementClass buttonElement)
		{
			HtmlButtonTag tag = new HtmlButtonTag();

			tag.Class = buttonElement.className;
			tag.Id = buttonElement.id;
			if ( buttonElement.name != null )
			{
				tag.Name = buttonElement.name;
			}
			else 
			{
				if ( buttonElement.id != null )
				{
					tag.Name = buttonElement.id;
				} 
				else 
				{
					tag.Name = String.Empty;
					tag.Id = String.Empty;
				}
			}
			//tag.OnClick = currentNode.GetAttribute("onclick",currentNode.NamespaceURI);
			//tag.Style = currentNode.GetAttribute("style",currentNode.NamespaceURI);
			tag.Title = buttonElement.title;

			switch ( buttonElement.type )
			{
				case "button":
					tag.Type=HtmlButtonType.Button;
					break;
				case "reset":
					tag.Type=HtmlButtonType.Reset;
					break;
				case "submit":
					tag.Type=HtmlButtonType.Submit;
					break;
			}

			tag.Value = buttonElement.value;

			return tag;

		}


		/// <summary>
		/// Creates a HtmlInputTag from a HTMLInputElementClass.
		/// </summary>
		/// <param name="inputElement"> The HTMLInputElementClass to convert.</param>
		/// <returns> A HtmlInputTag.</returns>
		private HtmlInputTag CreateHtmlInputTag(HTMLInputElementClass inputElement)
		{
			HtmlInputTag input = new HtmlInputTag();

			input.Checked = inputElement.@checked.ToString();
			input.Class = inputElement.className;
			input.Id = inputElement.id;
			input.MaxLength = inputElement.maxLength.ToString();
			if ( inputElement.name != null )
			{
				input.Name = inputElement.name;
			}
			else 
			{
				if ( inputElement.id != null )
				{
					input.Name = inputElement.id;
				} 
				else 
				{
					input.Name = inputElement.uniqueID;
					input.Id = inputElement.uniqueID;
				}
			}
			// input.OnClick = currentNode.GetAttribute("onclick",currentNode.NamespaceURI);
			input.ReadOnly = inputElement.readOnly.ToString();
			//input.Style = currentNode.GetAttribute("style",currentNode.NamespaceURI);
			input.Title = inputElement.title;
			input.Value = inputElement.value;

			switch ( inputElement.type )
			{
				case "button":
					input.Type=HtmlInputType.Button;
					break;
				case "checkbox":
					input.Type=HtmlInputType.Checkbox;
					break;
				case "file":
					input.Type=HtmlInputType.File;
					break;
				case "hidden":
					input.Type=HtmlInputType.Hidden;
					break;
				case "image":
					input.Type=HtmlInputType.Image;
					break;
				case "password":
					input.Type=HtmlInputType.Password;
					break;
				case "radio":
					input.Type=HtmlInputType.Radio;
					break;
				case "reset":
					input.Type=HtmlInputType.Reset;
					break;
				case "submit":
					input.Type=HtmlInputType.Submit;
					break;
				case "text":
					input.Type=HtmlInputType.Text;
					break;
				default:
					input.Type=HtmlInputType.Text;
					break;
			}

			return input;
		}


		/// <summary>
		/// Recursively parses the element tag collection to an ArrayList.
		/// </summary>
		/// <param name="list"> The list where the items are added.</param>
		/// <param name="coll"> The element collection.</param>
		/// <returns> An ArrayList with the parsed tags.</returns>
		private ArrayList ParseTags(ArrayList list, IHTMLElementCollection coll)
		{
			foreach (object obj in coll)
			{
				if ( 
					(obj is mshtml.HTMLInputButtonElementClass)
					|| 
					(obj is mshtml.HTMLSelectElementClass)
					||
					(obj is mshtml.HTMLTextAreaElementClass)
					||
					(obj is mshtml.HTMLInputElementClass)
					)
				{
					// Add to ArrayList
					list.Add(obj);
					continue;
				} else {
					// else get items and loop again
					IHTMLElementCollection children = (IHTMLElementCollection)((IHTMLElement)obj).children;
					if ( children.length > 0 )
					{
						ParseTags(list, children);
					}
				}							
			}

			return list;
		}
		#endregion
		#region Form Filling
		/// <summary>
		/// Converts the post data string to a PostDataCollection.
		/// </summary>
		/// <param name="data"> The post data string.</param>
		/// <returns> A PostDataCollection representation of the post data.</returns>
		public PostDataCollection GetPostDataCollection(string data)
		{			
			if ( data.EndsWith("&") )
				data = data.TrimEnd('&');

			// PostData
			PostDataCollection postData = new PostDataCollection();
			string[] nameValueArray = data.Split('&');

			for ( int i=0;i<nameValueArray.Length;i++ )
			{
				string pair = nameValueArray[i];
				string[] keyValuePair = pair.Split('=');				
				string name = EncodeDecode.UrlDecode(keyValuePair[0]);

				if ( pair.CompareTo(name) == 0 )
				{
					//  this is not post data
					break;
				}

				// If last item, check \0 else add \0.
				if ( i == (nameValueArray.Length-1) )
				{
					keyValuePair[1] = AddEndLine(keyValuePair[1]);
				}
				#region Add to PostDataCollection
				if ( postData[name] != null )
				{
					ArrayList list = postData[name];

					// The value is add 'as is'
					list.Add(keyValuePair[1]);

					// Debug
					System.Diagnostics.Debug.Write("ConvertPostDataString: Name= " + name + " Value= " + keyValuePair[1] + "\r\n");
				} 
				else 
				{
					if ( keyValuePair.Length == 2 )
					{
						ArrayList list = new ArrayList();

						// The value is add 'as is'
						list.Add(keyValuePair[1]);
						postData.Add(name, list);

						// Debug
						System.Diagnostics.Debug.Write("ConvertPostDataString: Name= " + name + " Value= " + keyValuePair[1] + "\r\n");
						
					}
				}		
				#endregion


			}

			return postData;
		}


		/// <summary>
		/// Converts the PostDataCollection to a post data string.
		/// </summary>
		/// <param name="data"> The post data hashtable.</param>
		/// <returns> A string representation of the post data.</returns>
		public string GetString(PostDataCollection data)
		{
			StringBuilder postdata = new StringBuilder();
			for (int i = 0;i<data.Count;i++ )
			{
				string key = data.Keys[i];
				ArrayList values = data[key];

				foreach ( string s in values )
				{
					postdata.Append(key);
					postdata.Append("=");
					postdata.Append(s);
					postdata.Append("&");
				}
			}

			return postdata.ToString();
		}


		/// <summary>
		/// Converts the PostDataCollection to a post data ArrayList.
		/// </summary>
		/// <param name="data"> The post data collection.</param>
		/// <returns> An ArrayList representation of the post data.</returns>
		public ArrayList GetArrayList(PostDataCollection data)
		{
			ArrayList list = new ArrayList();
			for (int i = 0;i<data.Count;i++ )
			{
				string key = data.Keys[i];
				ArrayList values = data[key];

				foreach ( string s in data )
				{
					list.Add(key + "=" + s);
				}
			}

			return list;
		}


		/// <summary>
		/// Converts the post data string to a Hashtable.
		/// </summary>
		/// <param name="data"> The post data string.</param>
		/// <param name="separator"> The main separator for the string.</param>
		/// <param name="nameValueSeparator"> The name value pair separator.</param>
		/// <returns> A Hashtable representation of the query string.</returns>
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
		/// Converts a Hashtable containing a query string representation to string.
		/// </summary>
		/// <param name="data"> The Hashtable data.</param>
		/// <param name="separator"> The separatos.</param>
		/// <param name="nameValueSeparator"> The name value pair separator.</param>
		/// <returns> A string representation of the query string.</returns>
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
				} else {
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



//		/// <summary>
//		/// Converts the hashtable to a post data ArrayList.
//		/// </summary>
//		/// <param name="data"> The post data hashtable.</param>
//		/// <returns> An ArrayList representation of the post data.</returns>
//		public ArrayList ConvertPostDataArrayList(Hashtable data)
//		{
//
//			ArrayList list = new ArrayList();
//			foreach ( DictionaryEntry de in data)
//			{
//				string key = (string)de.Key;
//
//				foreach ( string s in ((ArrayList)de.Value) )
//				{
//					list.Add(key + "=" + s);
//				}
//			}
//
//			return list;
//		}


		/// <summary>
		/// Adds post data values for a form tag.
		/// </summary>
		/// <param name="formTag"> The HtmlFormTag.</param>
		/// <param name="postDataString"> The post data as a string.</param>
		/// <returns> An updated HtmlFormTag with the post data values.</returns>
		public HtmlFormTag AddPostDataValues(HtmlFormTag formTag, string postDataString)
		{						
			PostDataCollection postDataItems = GetPostDataCollection(postDataString);
			
			for (int j=0;j<formTag.Count;j++)
			{
				HtmlTagBaseList elementList =(HtmlTagBaseList)((DictionaryEntry)formTag[j]).Value;

				// foreach tag in elementList
				for (int i=0;i<elementList.Count;i++)
				{
					HtmlTagBase tagBase = elementList[i];

					SetTagValue(postDataItems,tagBase,i);
				}
			}

			return formTag;
		}

		
		
		/// <summary>
		/// Sets the post data value to an element tag.
		/// </summary>
		/// <param name="postData"> The post data collection.</param>
		/// <param name="tag"> The current tag base.</param>
		/// <param name="index"> The index of the element.</param>
		private void SetTagValue(PostDataCollection postData, HtmlTagBase tag, int index)
		{
			//
			// Note: The input value has to UrlDecoded.
			// 

			// Input Tag
			if (tag is HtmlInputTag)
			{
				HtmlInputTag input=(HtmlInputTag)tag;				

				if ( postData[input.Name] != null )
				{
					ArrayList values = postData[input.Name];
					switch ( input.Type )
					{
						case HtmlInputType.Radio:
							if ( input.Checked.ToLower() == "true" )
								if ( values.Count == 1 )
								{
									input.Value = EncodeDecode.UrlDecode((string)values[0]);
								} 

								if ( values.Count > 1 )
								{
									input.Value = EncodeDecode.UrlDecode((string)values[index]);
								}								
							break;
						default:
							input.Value = EncodeDecode.UrlDecode((string)values[index]);
							break;
					}
				}
			}
	
			// Button Tag
			if (tag is HtmlButtonTag)
			{
				HtmlButtonTag button = (HtmlButtonTag)tag;

				if ( postData[button.Name] != null )
				{
					ArrayList values = postData[button.Name];
					button.Value = EncodeDecode.UrlDecode((string)values[index]);
				}
			}
	
			// Select Tag
			if (tag is HtmlSelectTag)
			{
				HtmlSelectTag select = (HtmlSelectTag)tag;

				if ( postData[select.Name] != null )
				{
					ArrayList values = postData[select.Name];
					select.Value = EncodeDecode.UrlDecode((string)values[index]);
				}	
			}
						
			// Textarea Tag
			if (tag is HtmlTextAreaTag)
			{
				HtmlTextAreaTag textarea=(HtmlTextAreaTag)tag;

				if ( postData[textarea.Name] != null )
				{
					ArrayList values = postData[textarea.Name];
					textarea.Value = EncodeDecode.UrlDecode((string)values[index]);
				}	
			}

		}

//		/// <summary>
//		/// Converts the post data string to a Hashtable.
//		/// </summary>
//		/// <param name="data"> The post data string.</param>
//		/// <returns> A Hashtable representation of the post data.</returns>
//		private Hashtable ConvertPostDataStringHashtable(string data)
//		{
//			// PostData
//			Hashtable postData = new Hashtable();
//			string[] nameValueArray = data.Split('&');
//			foreach ( string s in nameValueArray)
//			{
//				string[] values = s.Split('=');
//
//				string name = EncodeDecode.UrlDecode(values[0]);
//				if ( postData.ContainsKey(name) )
//				{
//					ArrayList list = (ArrayList)postData[name];
//					list.Add(values[1]);
//				} 
//				else 
//				{
//					if ( values.Length == 2 )
//					{
//						ArrayList list = new ArrayList();
//						list.Add(values[1]);
//						postData.Add(name, list);
//					}
//				}				
//			}
//
//			return postData;
//		}
		#endregion
	}
}
