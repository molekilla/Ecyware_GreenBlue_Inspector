// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Text;
using System.Collections;
using Ecyware.GreenBlue.Engine.HtmlDom;

namespace Ecyware.GreenBlue.Engine.HtmlCommand
{
	/// <summary>
	/// Contains logic for lookup and match of a post data structure and the corresponding form.
	/// </summary>
	public class FormHeuristic
	{
		/// <summary>
		/// Creates a new FormHeuristic type.
		/// </summary>
		public FormHeuristic()
		{
		}

		public string FilterPostDataMultiPart(string postData)
		{
			StringBuilder append = new StringBuilder();

			string[] s = postData.Split('\r');			

			bool isContentReady = false;
			int contentReadyLock = 0;

			foreach ( string part in s )
			{
				string element = part.Trim();

				if ( element.IndexOf("filename=") > -1 )
				{
					append.Append("&");

					// get name
					string[] names = element.Split(';');

					// get name
					string name = names[1].Split('=')[1];
					string value = names[2].Split('=')[1].Trim('"').Trim('\0').Trim();
					append.Append(name.Trim('"'));
					append.Append("=");
					append.Append(EncodeDecode.UrlEncode(value));
					append.Append("&");
				} 
				else 
				{
					if ( element.IndexOf("Content-Disposition") > -1 )
					{
						append.Append("&");

						// get name
						string[] names = element.Split(';');

						if ( names.Length > 2 )
						{
							// is a filename
						} 
						else 
						{
							// get name
							string name = names[1].Split('=')[1];
							append.Append(name.Trim('"'));
							append.Append("=");
						}

						isContentReady = true;
						contentReadyLock = 0;
					} 
					else 
					{   
						contentReadyLock++;

						if ( contentReadyLock == 3 )
						{
							contentReadyLock = 0;
							isContentReady = false;
						}

						if ( isContentReady )
						{
							if ( element.Length > 0 && element.IndexOf("Content-Disposition") == -1 )
							{
								// get value
								append.Append(EncodeDecode.UrlEncode(element));
								isContentReady = false;
								contentReadyLock = 0;
							}
						} 
					}
				}
			}


			return append.ToString().TrimStart('&');
		}
		/// <summary>
		/// Matches the post data to a form in a form collection.
		/// </summary>
		/// <param name="formCollection"> The form collection.</param>
		/// <param name="data"> The post data string.</param>
		/// <returns> A HtmlFormTag.</returns>
		public HtmlFormTag MatchPostDataToForm(HtmlFormTagCollection formCollection, string data)
		{
			FormConverter converter = new FormConverter();
			PostDataCollection postDataItems = converter.GetPostDataCollection(data);

			int matchByFieldName = 0;

			HtmlFormTagCollection formMatchByCount =  new HtmlFormTagCollection();

			#region First Match By Count
			foreach (DictionaryEntry de in formCollection)
			{
				HtmlFormTag form = (HtmlFormTag)de.Value;				
				
				// if match count is unique then use that form
				if ( postDataItems.Count == form.Count )
				{
					formMatchByCount.Add(form.Name, form);
				}
			}
			#endregion

			HtmlFormTag foundFormTag = null;
			if ( formMatchByCount.Count == 0 )
			{
				#region Then Match by Field Names, select the one with the matching more fields

				int weight = 0;
				string currentForm = string.Empty;
				Hashtable formKeysFound = new Hashtable(formCollection.Count);				

				foreach ( DictionaryEntry de in formCollection )
				{
					HtmlFormTag form = (HtmlFormTag)de.Value;					
					ArrayList keysToMaintain = new ArrayList();

					// if match by field name is unique then use that form
					for (int i=0;i<postDataItems.Count;i++ )
					{
						string name = postDataItems.Keys[i];

						// all values has to be found, else exit
						if ( form.ContainsKey(name) )
						{
							keysToMaintain.Add(name);
							matchByFieldName++;
						}
					}

					// form keys to maintain
					formKeysFound.Add(form.Name, keysToMaintain);

					// Example: 31 Forms Items, but match only 29 equals 2
					if ( matchByFieldName > 0 )
					{ 
						if ( (matchByFieldName - form.Count) < weight )
						{
							weight = matchByFieldName - form.Count;
							currentForm = form.Name;
						}						
						matchByFieldName = 0;
					}
				}

				// select form and remove not needed items
				foundFormTag = formCollection[currentForm];

				if ( foundFormTag != null )
				{
					ArrayList itemKeys = (ArrayList)formKeysFound[currentForm];
		
					// remove other fields from FormTag
					HtmlFormTag clone = foundFormTag.CloneTag();
					foundFormTag.Clear();

					for (int j=0;j<itemKeys.Count;j++)
					{
						string itemName = (string)itemKeys[j];
						foundFormTag.Add(itemName, clone[itemName]);
					}
				}
				#endregion
			}
			else 
			{
				#region Then Match by Field Names				
				foreach ( DictionaryEntry de in formMatchByCount )
				{
					HtmlFormTag form = (HtmlFormTag)de.Value;
					Hashtable formKeysFound = new Hashtable(formCollection.Count);
					ArrayList keysToMaintain = new ArrayList();

					// if match by field name is unique then use that form
					for (int i=0;i<postDataItems.Count;i++ )
					{
						string name = postDataItems.Keys[i];

						// all values has to be found, else exit
						if ( form.ContainsKey(name) )
						{
							keysToMaintain.Add(name);
							matchByFieldName++;
						} 
						else 
						{
							matchByFieldName = -1;
							break;
						}
					}

					// form keys to maintain
					formKeysFound.Add(form.Name, keysToMaintain);

					if ( matchByFieldName != -1 )
					{
						// found, return first occurence no matter what
						foundFormTag = form;

						ArrayList itemKeys = (ArrayList)formKeysFound[foundFormTag.Name];
		
						// remove other fields from FormTag
						HtmlFormTag clone = foundFormTag.CloneTag();
						foundFormTag.Clear();

						for (int j=0;j<itemKeys.Count;j++)
						{
							string itemName = (string)itemKeys[j];
							foundFormTag.Add(itemName, clone[itemName]);
						}
					}
				}
				#endregion
			}

			return foundFormTag;
		}


		/// <summary>
		/// Normalizes the form tag. For use with the multipart/form-data.
		/// </summary>
		public static HtmlFormTag NormalizeFormTag(HtmlFormTag formTag, string data)
		{
			FormConverter converter = new FormConverter();
			PostDataCollection postDataItems = converter.GetPostDataCollection(data);
			// removes extra tags.

			foreach ( string name in postDataItems.Keys )
			{
				if ( formTag.ContainsKey(name) )
				{
					HtmlTagBaseList list = formTag[name];

					if ( list.Count > 1 )
					{
						for ( int i=1;i<(list.Count+1);i++ )
						{
							list.Remove(list[i]);
						}
					}
				} 
				else
				{
					formTag.Remove(name);
				}
			}

			return formTag;
		}
	}
}
