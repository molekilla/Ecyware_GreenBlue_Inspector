// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003
using System;
using System.Collections;

namespace Ecyware.GreenBlue.Engine.HtmlDom
{
	public class HtmlFormTagCollection : IDictionary, ICollection, IEnumerable, ICloneable
	{
		protected Hashtable innerHash;
		
		#region "Constructors"
		public  HtmlFormTagCollection()
		{
			innerHash = new Hashtable();
		}
		
		public HtmlFormTagCollection(HtmlFormTagCollection original)
		{
			innerHash = new Hashtable (original.innerHash);
		}
		
		public HtmlFormTagCollection(IDictionary dictionary)
		{
			innerHash = new Hashtable (dictionary);
		}
		
		public HtmlFormTagCollection(int capacity)
		{
			innerHash = new Hashtable(capacity);
		}
		
		public HtmlFormTagCollection(IDictionary dictionary, float loadFactor)
		{
			innerHash = new Hashtable(dictionary, loadFactor);
		}
		
		public HtmlFormTagCollection(IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable (codeProvider, comparer);
		}
		
		public HtmlFormTagCollection(int capacity, int loadFactor)
		{
			innerHash = new Hashtable(capacity, loadFactor);
		}
		
		public HtmlFormTagCollection(IDictionary dictionary, IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable (dictionary, codeProvider, comparer);
		}
		
		public HtmlFormTagCollection(int capacity, IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable (capacity, codeProvider, comparer);
		}
		
		public HtmlFormTagCollection(IDictionary dictionary, float loadFactor, IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable (dictionary, loadFactor, codeProvider, comparer);
		}
		
		public HtmlFormTagCollection(int capacity, float loadFactor, IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable (capacity, loadFactor, codeProvider, comparer);
		}
		#endregion

		#region Implementation of IDictionary
		public HtmlFormTagCollectionEnumerator GetEnumerator()
		{
			return new HtmlFormTagCollectionEnumerator(this);
		}
        	
		System.Collections.IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new HtmlFormTagCollectionEnumerator(this);
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
		
		public void Add(string key, HtmlFormTag value)
		{
			innerHash.Add (key, value);
		}
		
		void IDictionary.Add(object key, object value)
		{
			Add ((string)key, (HtmlFormTag)value);
		}
		
		public bool IsReadOnly
		{
			get
			{
				return innerHash.IsReadOnly;
			}
		}
		
		public HtmlFormTag this[string key]
		{
			get
			{
				return (HtmlFormTag) innerHash[key];
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
				this[(string)key] = (HtmlFormTag)value;
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
		public HtmlFormTagCollection Clone()
		{
			HtmlFormTagCollection clone = new HtmlFormTagCollection();
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
		
		public bool ContainsValue (HtmlFormTag value)
		{
			return innerHash.ContainsValue(value);
		}
		
		public static HtmlFormTagCollection Synchronized(HtmlFormTagCollection nonSync)
		{
			HtmlFormTagCollection sync = new HtmlFormTagCollection();
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
	
	public class HtmlFormTagCollectionEnumerator : IDictionaryEnumerator
	{
		private IDictionaryEnumerator innerEnumerator;
		
		internal HtmlFormTagCollectionEnumerator (HtmlFormTagCollection enumerable)
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
		
		public HtmlFormTag Value
		{
			get
			{
				return (HtmlFormTag)innerEnumerator.Value;
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
