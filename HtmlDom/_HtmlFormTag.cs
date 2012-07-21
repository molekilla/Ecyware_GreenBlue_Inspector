// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003

using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Ecyware.GreenBlue.HtmlDom
{
	[Serializable]
	public class HtmlFormTag : HtmlTagBase, IDictionary, ICollection, IEnumerable, ICloneable
	{
		protected Hashtable innerHash;
		string _action;
		string _method = "get";
		string _name;
		string _enctype;
		string _onsubmit;
		int _formIndex;


		#region Constructors
		/// <summary>
		/// Creates a new HtmlFormTag.
		/// </summary>
		public  HtmlFormTag()
		{
			innerHash = new Hashtable();
		}

		/// <summary>
		/// Creates a new HtmlFormTag.
		/// </summary>
		/// <param name="original"> The original form tag.</param>
		public HtmlFormTag(HtmlFormTag original)
		{
			innerHash = new Hashtable (original.innerHash);
		}

		/// <summary>
		/// Creates a new HtmlFormTag.
		/// </summary>
		/// <param name="dictionary"> The dictionary to use for the inner list.</param>
		public HtmlFormTag(IDictionary dictionary)
		{
			innerHash = new Hashtable (dictionary);
		}

		/// <summary>
		/// Creates a new HtmlFormTag.
		/// </summary>
		/// <param name="capacity"> The capacity of the inner list.</param>
		public HtmlFormTag(int capacity)
		{
			innerHash = new Hashtable(capacity);
		}

		public HtmlFormTag(IDictionary dictionary, float loadFactor)
		{
			innerHash = new Hashtable(dictionary, loadFactor);
		}

		public HtmlFormTag(IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable (codeProvider, comparer);
		}

		public HtmlFormTag(int capacity, int loadFactor)
		{
			innerHash = new Hashtable(capacity, loadFactor);
		}

		public HtmlFormTag(IDictionary dictionary, IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable (dictionary, codeProvider, comparer);
		}

		public HtmlFormTag(int capacity, IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable (capacity, codeProvider, comparer);
		}

		public HtmlFormTag(IDictionary dictionary, float loadFactor, IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable (dictionary, loadFactor, codeProvider, comparer);
		}

		public HtmlFormTag(int capacity, float loadFactor, IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable (capacity, loadFactor, codeProvider, comparer);
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
		/// Clones the current object into a new HtmlFormTag
		/// </summary>
		/// <returns>A new HtmlFormTag.</returns>
		public HtmlFormTag CloneTag()
		{
			// new memory stream
			MemoryStream ms = new MemoryStream();

			// new BinaryFormatter
			BinaryFormatter bf = new BinaryFormatter(null,new StreamingContext(StreamingContextStates.Clone));
			
			// serialize
			bf.Serialize(ms,this);
			
			// go to beggining
			ms.Seek(0,SeekOrigin.Begin);
			
			// deserialize
			HtmlFormTag retVal = (HtmlFormTag)bf.Deserialize(ms);
			ms.Close();

			return retVal;
		}
		#region Implementation of IDictionary
		public HtmlFormTagEnumerator GetEnumerator()
		{
			return new HtmlFormTagEnumerator(this);
		}
    
		System.Collections.IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new HtmlFormTagEnumerator(this);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Remove(string key)
		{
			innerHash.Remove (key);
		}

		void IDictionary.Remove(object key)
		{
			Remove ((string)key);
		}

		public bool Contains(string key)
		{
			return innerHash.Contains(key);
		}

		bool IDictionary.Contains(object key)
		{
			return Contains((string)key);
		}

		public void Clear()
		{
			innerHash.Clear();		
		}

		public void Add(string key, HtmlTagBaseList value)
		{
			innerHash.Add (key, value);
		}

		void IDictionary.Add(object key, object value)
		{
			Add ((string)key, (HtmlTagBaseList)value);
		}

		public bool IsReadOnly
		{
			get
			{
				return innerHash.IsReadOnly;
			}
		}

		public HtmlTagBaseList this[string key]
		{
			get
			{
				return (HtmlTagBaseList) innerHash[key];
			}
			set
			{
				innerHash[key] = value;
			}
		}

		object IDictionary.this[object key]
		{
			get
			{
				return this[(string)key];
			}
			set
			{
				this[(string)key] = (HtmlTagBaseList)value;
			}
		}
    
		public System.Collections.ICollection Values
		{
			get
			{
				return innerHash.Values;
			}
		}

		public System.Collections.ICollection Keys
		{
			get
			{
				return innerHash.Keys;
			}
		}

		public bool IsFixedSize
		{
			get
			{
				return innerHash.IsFixedSize;
			}
		}
		#endregion

		#region Implementation of ICollection
		public void CopyTo(System.Array array, int index)
		{
			innerHash.CopyTo (array, index);
		}

		public bool IsSynchronized
		{
			get
			{
				return innerHash.IsSynchronized;
			}
		}

		public int Count
		{
			get
			{
				return innerHash.Count;
			}
		}

		public object SyncRoot
		{
			get
			{
				return innerHash.SyncRoot;
			}
		}
		#endregion

		#region Implementation of ICloneable
		public HtmlFormTag Clone()
		{
			HtmlFormTag clone = new HtmlFormTag();
			clone.innerHash = (Hashtable) innerHash.Clone();
	
			return clone;
		}

		object ICloneable.Clone()
		{
			return Clone();
		}
		#endregion

		#region "HashTable Methods"
		public bool ContainsKey (string key)
		{
			return innerHash.ContainsKey(key);
		}

		public bool ContainsValue (HtmlTagBaseList value)
		{
			return innerHash.ContainsValue(value);
		}

		public static HtmlFormTag Synchronized(HtmlFormTag nonSync)
		{
			HtmlFormTag sync = new HtmlFormTag();
			sync.innerHash = Hashtable.Synchronized(nonSync.innerHash);

			return sync;
		}
		#endregion

		internal Hashtable InnerHash
		{
			get
			{
				return innerHash;
			}
		}
	}

	public class HtmlFormTagEnumerator : IDictionaryEnumerator
	{
		private IDictionaryEnumerator innerEnumerator;

		internal HtmlFormTagEnumerator (HtmlFormTag enumerable)
		{
			innerEnumerator = enumerable.InnerHash.GetEnumerator();
		}

		#region Implementation of IDictionaryEnumerator
		public string Key
		{
			get
			{
				return (string)innerEnumerator.Key;
			}
		}

		object IDictionaryEnumerator.Key
		{
			get
			{
				return Key;
			}
		}

		public HtmlTagBaseList Value
		{
			get
			{
				return (HtmlTagBaseList)innerEnumerator.Value;
			}
		}

		object IDictionaryEnumerator.Value
		{
			get
			{
				return Value;
			}
		}

		public System.Collections.DictionaryEntry Entry
		{
			get
			{
				return innerEnumerator.Entry;
			}
		}
		#endregion

		#region Implementation of IEnumerator
		public void Reset()
		{
			innerEnumerator.Reset();
		}

		public bool MoveNext()
		{
			return innerEnumerator.MoveNext();
		}

		public object Current
		{
			get
			{
				return innerEnumerator.Current;
			}
		}
		#endregion
	}
}
