// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: August 2004
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using System.IO;

namespace Ecyware.GreenBlue.Engine.HtmlDom
{
	/// <summary>
	/// Contains the collection for the Post data.
	/// </summary>
	[Serializable]
	//[SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter=true)]
	public class HtmlFormTag : HtmlTagBase, IEnumerable
	{
		protected HtmlTagBaseCollection innerCollection;
		string _action;
		string _method = "get";
		string _name;
		string _enctype;
		string _onsubmit;
		int _formIndex;

		/// <summary>
		/// Creates a new HtmlFormTag.
		/// </summary>
		public HtmlFormTag()
		{
			innerCollection = new HtmlTagBaseCollection();
		}

		#region Methods
		/// <summary>
		/// Gets the item count.
		/// </summary>
		public int Count
		{
			get
			{
				return innerCollection.Count;
			}
		}
		// Gets a key-and-value pair (DictionaryEntry) using an index.
		public DictionaryEntry this[ int index ]  
		{
			get  
			{				
				return innerCollection[index];
			}
		}

		// Gets or sets the value associated with the specified key.
		public HtmlTagBaseList this[ String key ]  
		{
			get  
			{
				return innerCollection[key];
			}
			set  
			{
				innerCollection[key] = value;
			}
		}

		// Gets a String array that contains all the keys in the collection.
		public String[] AllKeys  
		{
			get  
			{
				return( innerCollection.AllKeys );
			}
		}

		// Gets an Object array that contains all the values in the collection.
		public Array AllValues  
		{
			get  
			{
				return( innerCollection.AllValues );
			}
		}

		// Gets a String array that contains all the values in the collection.
		public String[] AllStringValues  
		{
			get  
			{
				return innerCollection.AllStringValues ;
			}
		}

		// Gets a value indicating if the collection contains keys that are not null.
		public Boolean HasKeys  
		{
			get  
			{
				return( innerCollection.HasKeys );
			}
		}

		// Adds an entry to the collection.
		public void Add( String key, HtmlTagBaseList value )  
		{
			innerCollection.Add( key, value );
		}

		// Removes an entry with the specified key from the collection.
		public void Remove( String key )  
		{
			innerCollection.Remove( key );
		}

		// Removes an entry in the specified index from the collection.
		public void Remove( int index )  
		{
			innerCollection.Remove( index );
		}

		// Clears all the elements in the collection.
		public void Clear()  
		{
			innerCollection.Clear();
		}

		public bool ContainsKey(string key)
		{
			if ( innerCollection[key] == null )
			{
				return false;
			} 
			else 
			{
				return true;
			}
		}
		#endregion
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
		/// <summary>
		/// Clones the current object into a new HtmlFormTag.
		/// </summary>
		/// <returns>A new HtmlFormTag.</returns>
		public HtmlFormTag CloneTag()
		{
			// new memory stream
			MemoryStream ms = new MemoryStream();

			// new BinaryFormatter
			BinaryFormatter bf = new BinaryFormatter(null,
				new StreamingContext(StreamingContextStates.Clone));
			
			// serialize
			bf.Serialize(ms,this);
			
			// go to beggining
			ms.Seek(0,SeekOrigin.Begin);
			
			// deserialize
			HtmlFormTag retVal = (HtmlFormTag)bf.Deserialize(ms);
			ms.Close();

			return retVal;
		}

		#region IEnumerable Members

		/// <summary>
		/// Returns an IEnumerator that can iterate through the collection.
		/// </summary>
		/// <returns></returns>
		public IEnumerator GetEnumerator()
		{
			return innerCollection.GetEnumerator();
		}

		#endregion
	}
}