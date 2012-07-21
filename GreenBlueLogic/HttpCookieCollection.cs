using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Ecyware.GreenBlue.Protocols.Http
{
	[Serializable]
	public class HttpCookieCollection : IDictionary, ICollection, IEnumerable, ICloneable
	{
		protected Hashtable innerHash;
		
		#region "Constructors"
		public  HttpCookieCollection()
		{
			innerHash = new Hashtable();
		}
		
		public HttpCookieCollection(HttpCookieCollection original)
		{
			innerHash = new Hashtable (original.innerHash);
		}
		
		public HttpCookieCollection(IDictionary dictionary)
		{
			innerHash = new Hashtable (dictionary);
		}
		
		public HttpCookieCollection(int capacity)
		{
			innerHash = new Hashtable(capacity);
		}
		
		public HttpCookieCollection(IDictionary dictionary, float loadFactor)
		{
			innerHash = new Hashtable(dictionary, loadFactor);
		}
		
		public HttpCookieCollection(IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable (codeProvider, comparer);
		}
		
		public HttpCookieCollection(int capacity, int loadFactor)
		{
			innerHash = new Hashtable(capacity, loadFactor);
		}
		
		public HttpCookieCollection(IDictionary dictionary, IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable (dictionary, codeProvider, comparer);
		}
		
		public HttpCookieCollection(int capacity, IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable (capacity, codeProvider, comparer);
		}
		
		public HttpCookieCollection(IDictionary dictionary, float loadFactor, IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable (dictionary, loadFactor, codeProvider, comparer);
		}
		
		public HttpCookieCollection(int capacity, float loadFactor, IHashCodeProvider codeProvider, IComparer comparer)
		{
			innerHash = new Hashtable (capacity, loadFactor, codeProvider, comparer);
		}
		#endregion

		#region Implementation of IDictionary
		public HttpCookieCollectionEnumerator GetEnumerator()
		{
			return new HttpCookieCollectionEnumerator(this);
		}
        	
		System.Collections.IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			return new HttpCookieCollectionEnumerator(this);
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
		
		public void Add(string key, HttpCookie value)
		{
			innerHash.Add (key, value);
		}
		
		void IDictionary.Add(object key, object value)
		{
			Add ((string)key, (HttpCookie)value);
		}
		
		public bool IsReadOnly
		{
			get
			{
				return innerHash.IsReadOnly;
			}
		}
		
		public HttpCookie this[string key]
		{
			get
			{
				return (HttpCookie) innerHash[key];
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
				this[(string)key] = (HttpCookie)value;
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
		public HttpCookieCollection Clone()
		{
			HttpCookieCollection clone = new HttpCookieCollection();
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
		
		public bool ContainsValue (HttpCookie value)
		{
			return innerHash.ContainsValue(value);
		}
		
		public static HttpCookieCollection Synchronized(HttpCookieCollection nonSync)
		{
			HttpCookieCollection sync = new HttpCookieCollection();
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
	
	public class HttpCookieCollectionEnumerator : IDictionaryEnumerator
	{
		private IDictionaryEnumerator innerEnumerator;
		
		internal HttpCookieCollectionEnumerator (HttpCookieCollection enumerable)
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
		
		public HttpCookie Value
		{
			get
			{
				return (HttpCookie)innerEnumerator.Value;
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
