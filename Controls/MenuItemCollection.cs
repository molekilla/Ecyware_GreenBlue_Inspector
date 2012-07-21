// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Collections;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	///		Represents a strongly-typed collection of key-and-value pairs that are
	///		sorted by the keys and are accessible by key and by index.
	/// </summary>
	/// <seealso cref="System.Collections.SortedList"/>
	[Serializable]
	public class MenuItemCollection : IDictionary, ICloneable
	{
		#region Member Variables
		private const int DEFAULT_CAPACITY = 16;

		private string[] keys;
		private MenuItem[] values;
		private int count;
		[NonSerialized]
		private int version;
		private IComparer comparer;
		private KeyList keyList;
		private ValueList valueList;
		#endregion

		#region Constructors
		/// <summary>
		///		Initializes a new instance of the <see cref="MenuItemCollection"/> class that is empty, 
		///		has the default initial capacity and is sorted according to the <see cref="IComparable"/> interface 
		///		implemented by each key added to the <b>MenuItemCollection</b>.
		/// </summary>
		public MenuItemCollection()
		{
			keys = new string[DEFAULT_CAPACITY];
			values = new MenuItem[DEFAULT_CAPACITY];
			comparer = Comparer.Default;
		}

		/// <summary>
		///		Initializes a new instance of the <see cref="MenuItemCollection"/> class that is empty, 
		///		has the specified initial capacity and is sorted according to the <see cref="IComparable"/>
		///		interface implemented by each key added to the <b>MenuItemCollection</b>.
		/// </summary>
		/// <param name="capacity">The initial number of elements that the <see cref="MenuItemCollection"/> can contain.</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity"/> is less than zero.</exception>
		public MenuItemCollection(int capacity)
		{
			if (capacity < 0)
				throw new ArgumentOutOfRangeException("capacity", capacity, "Initial capacity cannot be less than zero.");

			keys = new string[capacity];
			values = new MenuItem[capacity];
			comparer = Comparer.Default;
		}

		/// <summary>
		///		Initializes a new instance of the <see cref="MenuItemCollection"/> class that is empty, 
		///		has the default initial capacity and is sorted according to the specified 
		///		<see cref="IComparer"/> interface.
		/// </summary>
		/// <param name="comparer">
		///		<para>The <see cref="IComparer"/> implementation to use when comparing keys.</para>
		///		<para>-or-</para>
		///		<para>A null reference, to use the <see cref="IComparable"/> implementation of each key.</para>
		/// </param>
		public MenuItemCollection(IComparer comparer) : this()
		{
			if (comparer != null)
				this.comparer = comparer;
		}

		/// <summary>
		///		Initializes a new instance of the <see cref="MenuItemCollection"/> class that is empty, 
		///		has the specified initial capacity and is sorted according to the specified 
		///		<see cref="IComparer"/> interface.
		/// </summary>
		/// <param name="comparer">
		///		<para>The <see cref="IComparer"/> implementation to use when comparing keys.</para>
		///		<para>-or-</para>
		///		<para>A null reference, to use the <see cref="IComparable"/> implementation of each key.</para>
		/// </param>
		/// <param name="capacity">The initial number of elements that the <see cref="MenuItemCollection"/> can contain.</param>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="capacity"/> is less than zero.</exception>
		public MenuItemCollection(IComparer comparer, int capacity) : this(capacity)
		{
			if (comparer != null)
				this.comparer = comparer;
		}

		/// <summary>
		///		Initializes a new instance of the <see cref="MenuItemCollection"/> class that contains 
		///		elements copied from the specified dictionary, has the same initial capacity as the 
		///		number of elements copied and is sorted according to the <see cref="IComparable"/> interface 
		///		implemented by each key.
		/// </summary>
		/// <param name="d">The <see cref="IDictionary"/> to copy to a new SortedList.</param>
		/// <exception cref="ArgumentNullException"><paramref name="d"/> is a null reference.</exception>
		/// <exception cref="InvalidCastException">
		///		<para>One or more elements in <paramref name="d"/> do not implement the
		///		<see cref="IComparable"/> interface.</para>
		///		<para>-or-</para>
		///		<para><pararef name="d"/> contains elements of a type not supported by <see cref="MenuItemCollection"/>.</para>
		///	</exception>
		public MenuItemCollection(IDictionary d) : this(d, null) {}

		/// <summary>
		///		Initializes a new instance of the <see cref="MenuItemCollection"/> class that contains 
		///		elements copied from the specified dictionary, has the same initial capacity as the 
		///		number of elements copied and is sorted according to the specified <see cref="IComparer"/> interface.
		/// </summary>
		/// <param name="d">The <see cref="IDictionary"/> to copy to a new SortedList.</param>
		/// <param name="comparer">
		///		<para>The <see cref="IComparer"/> implementation to use when comparing keys.</para>
		///		<para>-or-</para>
		///		<para>A null reference, to use the <see cref="IComparable"/> implementation of each key.</para>
		/// </param>
		/// <exception cref="ArgumentNullException"><paramref name="d"/> is a null reference.</exception>
		/// <exception cref="InvalidCastException">
		///		<para>One or more elements in <paramref name="d"/> do not implement the
		///		<see cref="IComparable"/> interface.</para>
		///		<para>-or-</para>
		///		<para><pararef name="d"/> contains elements of a type not supported by <see cref="MenuItemCollection"/>.</para>
		///	</exception>
		public MenuItemCollection(IDictionary d, IComparer comparer)
			: this(comparer, (d == null ? 0 : d.Count))
		{
			if (d == null)
				throw new ArgumentNullException("d", "The IDictionary cannot be null.");

			d.Keys.CopyTo(keys, 0);
			d.Values.CopyTo(values, 0);
			Array.Sort(this.keys, this.values, this.comparer);
			this.count = d.Count;
		}
		#endregion

		#region Properties
		/// <summary>
		///		Gets and sets the value associated with a specific key in the <b>MenuItemCollection</b>.
		/// </summary>
		/// <value>
		///		The <see cref="MenuItem"/> associated with <paramref name="key"/> in the <see cref="MenuItemCollection"/>,
		///		if <paramref name="key"/> is found; otherwise, a <see cref="NullReferenceException"/> is thrown.
		///	</value>
		/// <remarks>
		///		This property, unlike its equivalent in <see cref="System.Collections.SortedList"/>, does not return
		///		null when a key cannot be found. The reason for this is that the strongly-typed sorted list
		///		template this class is created from allows the value to be a value type (or struct), which cannot
		///		be set to null. Instead, a <see cref="NullReferenceException"/> is thrown when a key cannot be found.
		///		Callers must either catch this exception, or call the <see cref="MenuItemCollection.Contains"/> method first to
		///		see if the key exists.
		/// </remarks>
		/// <exception cref="ArgumentNullException">The <paramref name="key"/> is a null reference.</exception>
		/// <exception cref="ArgumentException">
		///		Either the <paramref name="key"/> or the <paramref name="value"/> are not of a type supported by the <see cref="MenuItemCollection"/>.
		/// </exception>
		/// <exception cref="NullReferenceException">
		///		<paramref name="key"/> is not found in the <see cref="MenuItemCollection"/>.
		/// </exception>
		object IDictionary.this[object key]
		{
			get
			{
				if (key == null)
					throw new ArgumentNullException("key", "The key cannot be null.");

				if (!(key is string))
					throw new ArgumentException("The key must be of type: " + typeof(string).FullName, "key");

				return this[(string)key];
			}
			set
			{
				if (key == null)
					throw new ArgumentNullException("key", "The key cannot be null.");

				if (!(key is string))
					throw new ArgumentException("The key must be of type: " + typeof(string).FullName, "key");

				if (!(value is MenuItem))
					throw new ArgumentException("The value must be of type: " + typeof(MenuItem).FullName, "value");

				this[(string)key] = (MenuItem)value;
			}
		}

		/// <summary>
		///		Gets and sets the value associated with a specific key in the <b>MenuItemCollection</b>.
		/// </summary>
		/// <value>
		///		The <see cref="MenuItem"/> associated with <paramref name="key"/> in the <see cref="MenuItemCollection"/>,
		///		if <paramref name="key"/> is found; otherwise, a <see cref="NullReferenceException"/> is thrown.
		///	</value>
		/// <remarks>
		///		<para>This property, unlike its equivalent in <see cref="System.Collections.SortedList"/>, does not return
		///		null when a key cannot be found. The reason for this is that the strongly-typed sorted list
		///		template this class is created from allows the value to be a value type (or struct), which cannot
		///		be set to null. Instead, a <see cref="NullReferenceException"/> is thrown when a key cannot be found.
		///		Callers must either catch this exception, or call the <see cref="MenuItemCollection.Contains"/> method first to
		///		see if the key exists.</para>
		/// </remarks>
		/// <exception cref="ArgumentNullException">The <paramref name="key"/> is a null reference.</exception>
		/// <exception cref="NullReferenceException">
		///		<paramref name="key"/> is not found in the <see cref="MenuItemCollection"/>.
		/// </exception>
		public virtual MenuItem this[string key]
		{
			get
			{
				int index = IndexOfKey(key);
				if (index >= 0)
					return values[index];

				throw new NullReferenceException("The specified key could not be found.");
			}
			set
			{
				if (Object.ReferenceEquals(key, null)) // avoids compiler error for null check on value type
					throw new ArgumentNullException("key", "The key cannot be null.");

				int index = Array.BinarySearch(keys, 0, count, key, comparer);
				if (index >= 0)
				{
					values[index] = value;
					version++;
					return;
				}

				Insert(~index, key, value);
			}
		}

		/// <summary>
		///		Gets or sets the capacity of the <b>MenuItemCollection</b>.
		/// </summary>
		/// <value>The number of elements that the <see cref="MenuItemCollection"/> can contain.</value>
		public virtual int Capacity
		{
			get
			{
				return keys.Length;
			}
			set
			{
				if (value < count)
					value = count;

				if (value != keys.Length)
				{
					if (value > 0)
					{
						string[] newKeys = new string[value];
						MenuItem[] newValues = new MenuItem[value];

						if (count > 0)
						{
							Array.Copy(keys, 0, newKeys, 0, count);
							Array.Copy(values, 0, newValues, 0, count);
						}
						
						keys = newKeys;
						values = newValues;
					}
					else
					{
						keys = new string[DEFAULT_CAPACITY];
						values = new MenuItem[DEFAULT_CAPACITY];
					}
				}
			}
		}

		/// <summary>
		///		Gets the number of elements contained in the <b>MenuItemCollection</b>.
		/// </summary>
		/// <value>The number of elements contained in the <see cref="MenuItemCollection"/>.</value>
		public virtual int Count
		{
			get
			{
				return this.count;
			}
		}

		/// <summary>
		///		Gets a value indicating whether the <b>MenuItemCollection</b> has a fixed size.
		/// </summary>
		/// <value>
		///		<b>true</b> if the <see cref="MenuItemCollection"/> has a fixed size; otherwise, <b>false</b>.
		///		The default is <b>false</b>.
		/// </value>
		public virtual bool IsFixedSize
		{
			get { return false; }
		}

		/// <summary>
		///		Gets a value indicating whether the <b>MenuItemCollection</b> is read-only.
		/// </summary>
		/// <value>
		///		<b>true</b> if the <see cref="MenuItemCollection"/> is read-only; otherwise, <b>false</b>.
		///		The default is <b>false</b>.
		/// </value>
		public virtual bool IsReadOnly
		{
			get	{ return false; }
		}

		/// <summary>
		///		Gets a value indicating whether access to the <b>MenuItemCollection</b> is
		///		synchronized (thread-safe).
		/// </summary>
		/// <value>
		///		<b>true</b> if access to the the <see cref="MenuItemCollection"/> is 
		///		synchronized (thread-safe); otherwise, <b>false</b>.
		///		The default is <b>false</b>.
		/// </value>
		public virtual bool IsSynchronized
		{
			get { return false; }
		}

		/// <summary>
		///		Gets the keys in the <b>MenuItemCollection</b>.
		/// </summary>
		/// <value>An <see cref="ICollection"/> containing the keys in the <see cref="MenuItemCollection"/>.</value>
		/// <remarks>
		///		<para>The <see cref="ICollection"/> is a read-only view of the keys in the 
		///		<see cref="MenuItemCollection"/>. Modifications made to the underlying <b>MenuItemCollection</b>
		///		are immediately reflected in the <b>ICollection</b>.</para>
		///		<para>The elements of the <b>ICollection</b> are sorted in the same order as the keys
		///		of the <b>MenuItemCollection</b>.</para>
		///		<para>Similar to <see cref="MenuItemCollection.GetKeyList"/>, but returns an <b>ICollection</b>
		///		instead of an <see cref="IList"/>.</para>
		/// </remarks>
		public virtual ICollection Keys
		{
			get
			{
				return GetKeyList();
			}
		}

		/// <summary>
		///		Gets an object that can be used to synchronize access to the <b>MenuItemCollection</b>.
		/// </summary>
		/// <value>An object that can be used to synchronize access to the <see cref="MenuItemCollection"/>.</value>
		public virtual object SyncRoot
		{
			get { return this; }
		}

		/// <summary>
		///		Gets the values in the <b>MenuItemCollection</b>.
		/// </summary>
		/// <value>An <see cref="ICollection"/> containing the values in the <see cref="MenuItemCollection"/>.</value>
		/// <remarks>
		///		<para>The <see cref="ICollection"/> is a read-only view of the values in the 
		///		<see cref="MenuItemCollection"/>. Modifications made to the underlying <b>MenuItemCollection</b>
		///		are immediately reflected in the <b>ICollection</b>.</para>
		///		<para>The elements of the <b>ICollection</b> are sorted in the same order as the values
		///		of the <b>MenuItemCollection</b>.</para>
		///		<para>Similar to <see cref="MenuItemCollection.GetValueList"/>, but returns an <b>ICollection</b>
		///		instead of an <see cref="IList"/>.</para>
		/// </remarks>
		public virtual ICollection Values
		{
			get
			{
				return GetValueList();
			}
		}
		#endregion

		#region Public Methods
		/// <summary>
		///		Adds an element with the specified key and value to the <b>MenuItemCollection</b>.
		/// </summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add.</param>
		/// <exception cref="ArgumentNullException">The <paramref name="key"/> is a null reference.</exception>
		/// <exception cref="ArgumentException">
		///		<para>An element with the specified <paramref name="key"/> already exists in the <see cref="MenuItemCollection"/>.</para>
		///		<para>-or-</para>
		///		<para>Either the <paramref name="key"/> or the <paramref name="value"/> are not of a type supported by the <see cref="MenuItemCollection"/>.</para>
		///		<para>-or-</para>
		///		<para>The <b>MenuItemCollection</b> is set to use the <see cref="IComparable"/> interface,
		///		and <paramref name="key"/> does not implement the <b>IComparable</b> interface.</para>
		/// </exception>
		/// <exception cref="InvalidOperationException">The comparer throws an exception.</exception>
		/// <exception cref="NotSupportedException">
		///		<para>The <see cref="MenuItemCollection"/> is read-only.</para>
		///		<para>-or-</para>
		///		<para>The <b>MenuItemCollection</b> has a fixed size.</para>
		/// </exception>
		void IDictionary.Add(object key, object value)
		{
			if (key == null)
				throw new ArgumentNullException("key", "The key cannot be null.");

			if (!(key is string))
				throw new ArgumentException("The key must be of type: " + typeof(string).FullName, "key");

			if (!(value is MenuItem))
				throw new ArgumentException("The value must be of type: " + typeof(MenuItem).FullName, "value");

			this.Add((string)key, (MenuItem)value);
		}

		/// <summary>
		///		Adds an element with the specified key and value to the <b>MenuItemCollection</b>.
		/// </summary>
		/// <param name="key">The key of the element to add.</param>
		/// <param name="value">The value of the element to add.</param>
		/// <exception cref="ArgumentNullException">The <paramref name="key"/> is a null reference.</exception>
		/// <exception cref="ArgumentException">
		///		<para>An element with the specified <paramref name="key"/> already exists in the <see cref="MenuItemCollection"/>.</para>
		///		<para>-or-</para>
		///		<para>The <b>MenuItemCollection</b> is set to use the <see cref="IComparable"/> interface,
		///		and <paramref name="key"/> does not implement the <b>IComparable</b> interface.</para>
		/// </exception>
		/// <exception cref="InvalidOperationException">The comparer throws an exception.</exception>
		/// <exception cref="NotSupportedException">
		///		<para>The <see cref="MenuItemCollection"/> is read-only.</para>
		///		<para>-or-</para>
		///		<para>The <b>MenuItemCollection</b> has a fixed size.</para>
		/// </exception>
		public virtual void Add(string key, MenuItem value)
		{
			if (Object.ReferenceEquals(key, null)) // avoids compiler error for null check on value type
				throw new ArgumentNullException("key", "The key cannot be null.");

			int index = Array.BinarySearch(keys, 0, count, key, comparer);

			if (index >= 0)
				throw new ArgumentException(String.Format("Item has already been added.  Key being added: \"{0}\".", key));
 
			Insert(~index, key, value);
		}

		/// <summary>
		///		Removes all elements from the <b>MenuItemCollection</b>.
		/// </summary>
		/// <exception cref="NotSupportedException">
		///		<para>The <see cref="MenuItemCollection"/> is read-only.</para>
		///		<para>-or-</para>
		///		<para>The <b>MenuItemCollection</b> has a fixed size.</para>
		/// </exception>
		public virtual void Clear()
		{
			keys = new string[DEFAULT_CAPACITY];
			values = new MenuItem[DEFAULT_CAPACITY];
			count = 0;
			version++;
		}

		/// <summary>
		///		Creates a shallow copy of the <b>MenuItemCollection</b>.
		/// </summary>
		/// <returns>A shallow copy of the <see cref="MenuItemCollection"/>.</returns>
		public virtual object Clone()
		{
			MenuItemCollection newList = new MenuItemCollection(count);
			Array.Copy(keys, 0, newList.keys, 0, count);
			Array.Copy(values, 0, newList.values, 0, count);
			newList.count = count;
			newList.version = version;
			newList.comparer = comparer;

			return newList;
		}

		/// <summary>
		///		Determines whether the <b>MenuItemCollection</b> contains a specific key.
		/// </summary>
		/// <param name="key">The key to locate in the <see cref="MenuItemCollection"/>.</param>
		/// <exception cref="ArgumentNullException">The <paramref name="key"/> is a null reference.</exception>
		/// <exception cref="ArgumentException">
		///		The <paramref name="key"/> is not of a type supported by the <see cref="MenuItemCollection"/>.
		/// </exception>
		/// <exception cref="InvalidOperationException">The comparer throws an exception.</exception>
		/// <returns>
		///		<b>true</b> if the <see cref="MenuItemCollection"/> contains an element with the specified key; 
		///		otherwise, <b>false</b>.
		///	</returns>
		bool IDictionary.Contains(object key)
		{
			if (key == null)
				throw new ArgumentNullException("key", "The key cannot be null.");

			if (!(key is string))
				throw new ArgumentException("The key must be of type: " + typeof(string).FullName, "key");

			return (IndexOfKey((string)key) >= 0);
		}

		/// <summary>
		///		Determines whether the <b>MenuItemCollection</b> contains a specific key.
		/// </summary>
		/// <param name="key">The key to locate in the <see cref="MenuItemCollection"/>.</param>
		/// <exception cref="ArgumentNullException">The <paramref name="key"/> is a null reference.</exception>
		/// <exception cref="InvalidOperationException">The comparer throws an exception.</exception>
		/// <returns>
		///		<b>true</b> if the <see cref="MenuItemCollection"/> contains an element with the specified 
		///		<paramref name="key"/>; otherwise, <b>false</b>.
		///	</returns>
		public virtual bool Contains(string key)
		{
			return (IndexOfKey(key) >= 0);
		}

		/// <summary>
		///		Determines whether the <b>MenuItemCollection</b> contains a specific key.
		/// </summary>
		/// <param name="key">The key to locate in the <see cref="MenuItemCollection"/>.</param>
		/// <exception cref="ArgumentNullException">The <paramref name="key"/> is a null reference.</exception>
		/// <exception cref="InvalidOperationException">The comparer throws an exception.</exception>
		/// <returns>
		///		<b>true</b> if the <see cref="MenuItemCollection"/> contains an element with the specified 
		///		<paramref name="key"/>; otherwise, <b>false</b>.
		///	</returns>
		public virtual bool ContainsKey(string key)
		{
			return (IndexOfKey(key) >= 0);
		}

		/// <summary>
		///		Determines whether the <b>MenuItemCollection</b> contains a specific value.
		/// </summary>
		/// <param name="value">The value to locate in the <see cref="MenuItemCollection"/>.</param>
		/// <returns>
		///		<b>true</b> if the <see cref="MenuItemCollection"/> contains an element with the specified 
		///		<paramref name="value"/>; otherwise, <b>false</b>.
		/// </returns>
		public virtual bool ContainsValue(MenuItem value)
		{
			return (IndexOfValue(value) >= 0);
		}

		/// <summary>
		///		Copies the <see cref="MenuItemCollection"/> elements to a one-dimensional <see cref="System.Array"/>
		///		instance at the specified index.
		/// </summary>
		/// <param name="array">
		///		The one-dimensional <see cref="System.Array"/> that is the destination of the
		///		<see cref="DictionaryEntry"/> objects copied from <see cref="MenuItemCollection"/>.
		///		The array must have zero-based indexing.
		/// </param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
		/// <exception cref="ArgumentNullException"><paramref name="array"/> is a null reference.</exception>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than zero.</exception>
		/// <exception cref="ArgumentException">
		///		<para><paramref name="array"/> is multidimensional.</para>
		///		<para>-or-</para>
		///		<para><paramref name="arrayIndex"/> is equal to or greater than the length of <paramref name="array"/>.</para>
		///		<para>-or-</para>
		///		<para>The number of elements in the source <see cref="MenuItemCollection"/> is greater than the 
		///		available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.</para>
		/// </exception>
		/// <exception cref="InvalidCastException">
		///		The type of the source <see cref="MenuItemCollection"/> cannot be cast automatically to the type
		///		of the destination <paramref name="array"/>.
		/// </exception>
		public virtual void CopyTo(Array array, int arrayIndex)
		{
			if (array == null)
				throw new ArgumentNullException("array", "The destination array cannot be null.");
			if (arrayIndex < 0)
				throw new ArgumentOutOfRangeException("arrayIndex", "Destination index cannot be less than zero.");
			if (array.Rank != 1)
				throw new ArgumentException("Multidimensional arrays are not supported.", "array");
			if (arrayIndex >= array.Length)
				throw new ArgumentException("Destination index cannot be greater than the size of the destination array.", "arrayIndex");
			if (count > (array.Length - arrayIndex))
				throw new ArgumentException("Not enough available space in the destination array.");

			for (int i=0; i < count; i++)
			{
				DictionaryEntry entry = new DictionaryEntry(keys[i], values[i]);
				array.SetValue(entry, arrayIndex + i);
			}
		}

		/// <summary>
		///		Gets the value at the specified index of the <b>MenuItemCollection</b>.
		/// </summary>
		/// <param name="index">The zero-based index of the value to get.</param>
		/// <returns>The value at the specified index of the <see cref="MenuItemCollection"/>.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		///		<paramref name="index"/> is outside the range of valid indices for the <see cref="MenuItemCollection"/>.
		/// </exception>
		public virtual MenuItem GetByIndex(int index)
		{
			if (index < 0 || index >= count)
				throw new ArgumentOutOfRangeException("index", index, "The index is outside the range of valid indices.");

			return values[index];
		}

		/// <summary>
		///		Returns an <see cref="IEnumerator"/> that can iterate through the <b>MenuItemCollection</b>.
		/// </summary>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return new SortedListEnumerator(this, 0, count, SortedListEnumerator.DictEntry);
		}

		/// <summary>
		///		Returns an <see cref="IDictionaryEnumerator"/> that can iterate through the
		///		<b>MenuItemCollection</b>.		
		/// </summary>
		/// <returns>An <see cref="IDictionaryEnumerator"/> for the <see cref="MenuItemCollection"/>.</returns>
		public virtual IDictionaryEnumerator GetEnumerator()
		{
			return new SortedListEnumerator(this, 0, count, SortedListEnumerator.DictEntry);
		}

		/// <summary>
		///		Gets the key at the specified index of the <b>MenuItemCollection</b>.
		/// </summary>
		/// <param name="index">The zero-based index of the key to get.</param>
		/// <returns>The key at the specified index of the <see cref="MenuItemCollection"/>.</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		///		<paramref name="index"/> is outside the range of valid indices for the <see cref="MenuItemCollection"/>.
		/// </exception>
		public virtual string GetKey(int index)
		{
			if (index < 0 || index >= count)
				throw new ArgumentOutOfRangeException("index", index, "The index is outside the range of valid indices.");

			return keys[index];
		}

		/// <summary>
		///		Gets the keys in the <b>MenuItemCollection</b>.
		/// </summary>
		/// <returns>An <see cref="IList"/> containing the keys in the <see cref="MenuItemCollection"/>.</returns>
		/// <remarks>
		///		<para>The returned <see cref="IList"/> is a read-only view of the keys in the 
		///		<see cref="MenuItemCollection"/>. Modifications made to the underlying <b>MenuItemCollection</b>
		///		are immediately reflected in the <b>IList</b>.</para>
		///		<para>The elements of the <b>IList</b> are sorted in the same order as the keys
		///		of the <b>MenuItemCollection</b>.</para>
		///		<para>Similar to <see cref="MenuItemCollection.Keys"/>, but returns an <b>IList</b>
		///		instead of an <see cref="ICollection"/>.</para>
		/// </remarks>
		public virtual IList GetKeyList()
		{
			if (keyList == null)
				keyList = new KeyList(this);

			return keyList;
		}

		/// <summary>
		///		Gets the values in the <b>MenuItemCollection</b>.
		/// </summary>
		/// <returns>An <see cref="IList"/> containing the values in the <see cref="MenuItemCollection"/>.</returns>
		/// <remarks>
		///		<para>The returned <see cref="IList"/> is a read-only view of the values in the 
		///		<see cref="MenuItemCollection"/>. Modifications made to the underlying <b>MenuItemCollection</b>
		///		are immediately reflected in the <b>IList</b>.</para>
		///		<para>The elements of the <b>IList</b> are sorted in the same order as the values
		///		of the <b>MenuItemCollection</b>.</para>
		///		<para>Similar to <see cref="MenuItemCollection.Values"/>, but returns an <b>IList</b>
		///		instead of an <see cref="ICollection"/>.</para>
		/// </remarks>
		public virtual IList GetValueList()
		{
			if (valueList == null)
				valueList = new ValueList(this);

			return valueList;
		}

		/// <summary>
		///		Returns the zero-based index of the specified key in the <b>MenuItemCollection</b>.
		/// </summary>
		/// <param name="key">The key to locate in the <see cref="MenuItemCollection"/>.</param>
		/// <returns>
		///		The zero-based index of <paramref name="key"/>, if <paramref name="key"/> is found in
		///		the <see cref="MenuItemCollection"/>; otherwise, -1.
		/// </returns>
		/// <exception cref="ArgumentNullException"><paramref name="key"/> is a null reference.</exception>
		/// <exception cref="InvalidOperationException">The comparer throws an exception.</exception>
		/// <remarks>
		///		<para>The elements of a <see cref="MenuItemCollection"/> are sorted by the keys either 
		///		according to a specific <see cref="IComparer"/> implementation specified when the 
		///		<see cref="MenuItemCollection"/> is created or according to the <see cref="IComparable"/> 
		///		implementation provided by the keys themselves.</para>
		///		<para>The index sequence is based on the sort sequence. When an element is added, 
		///		it is inserted into <see cref="MenuItemCollection"/> in the correct sort order, and the 
		///		indexing adjusts accordingly. When an element removed, the indexing also adjusts accordingly.
		///		Therefore, the index of a specific key-and-value pair might change as elements are added or 
		///		removed from the <see cref="MenuItemCollection"/>.</para>
		///		<para>This method uses a binary search algorithm; therefore, the average execution time is 
		///		proportional to Log2(<i>n</i>), where <i>n</i> is <see cref="MenuItemCollection.Count"/>.</para>
		/// </remarks>
		public virtual int IndexOfKey(string key)
		{
			if (Object.ReferenceEquals(key, null)) // avoids compiler error for null check on value type
				throw new ArgumentNullException("key", "The key cannot be null.");

			int index = Array.BinarySearch(keys, 0, count, key, comparer);

			return (index >= 0 ? index : -1);
		}

		/// <summary>
		///		Returns the zero-based index of the first occurrence of the specified value in
		///		the <b>MenuItemCollection</b>.
		/// </summary>
		/// <param name="value">The value to locate in the <see cref="MenuItemCollection"/>.</param>
		/// <returns>
		///		The zero-based index of <paramref name="value"/>, if <paramref name="value"/> is found in
		///		the <see cref="MenuItemCollection"/>; otherwise, -1.
		/// </returns>
		/// <remarks>
		///		<para>The index sequence is based on the sort sequence. When an element is added, 
		///		it is inserted into <see cref="MenuItemCollection"/> in the correct sort order, and 
		///		the indexing adjusts accordingly. When an element removed, the indexing also adjusts 
		///		accordingly. Therefore, the index of a specific key-and-value pair might change as 
		///		elements are added or removed from the <see cref="MenuItemCollection"/>.</para>
		///		<para>The values of the elements of the <see cref="MenuItemCollection"/> are compared to the 
		///		specified value using the Equals method.</para>
		///		<para>This method uses a linear search; therefore, the average execution time is 
		///		proportional to <see cref="MenuItemCollection.Count"/>.</para>
		/// </remarks>
		public virtual int IndexOfValue(MenuItem value)
		{
			return Array.IndexOf(values, value, 0, count);
		}

		/// <summary>
		///		Removes the element with the specified key from the <b>MenuItemCollection</b>.
		/// </summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="ArgumentException">
		///		The <paramref name="key"/> is not of a type supported by the <see cref="MenuItemCollection"/>.
		/// </exception>
		/// <exception cref="ArgumentNullException"><paramref name="key"/> is a null reference.</exception>
		/// <exception cref="NotSupportedException">
		///		<para>The <see cref="MenuItemCollection"/> is read-only.</para>
		///		<para>-or-</para>
		///		<para>The <b>MenuItemCollection</b> has a fixed size.</para>
		/// </exception>
		void IDictionary.Remove(object key)
		{
			if (key == null)
				throw new ArgumentNullException("key", "The key cannot be null.");

			if (!(key is string))
				throw new ArgumentException("The key must be of type: " + typeof(string).FullName, "key");

			Remove((string)key);
		}

		/// <summary>
		///		Removes the element with the specified key from the <b>MenuItemCollection</b>.
		/// </summary>
		/// <param name="key">The key of the element to remove.</param>
		/// <exception cref="ArgumentNullException"><paramref name="key"/> is a null reference.</exception>
		/// <exception cref="NotSupportedException">
		///		<para>The <see cref="MenuItemCollection"/> is read-only.</para>
		///		<para>-or-</para>
		///		<para>The <b>MenuItemCollection</b> has a fixed size.</para>
		/// </exception>
		/// <remarks>
		///		If the <see cref="MenuItemCollection"/> does not contain an element with the specified key,
		///		the <b>MenuItemCollection</b> remains unchanged. No exception is thrown.
		/// </remarks>
		public virtual void Remove(string key)
		{
			if (Object.ReferenceEquals(key, null)) // avoids compiler error for null check on value type
				throw new ArgumentNullException("key", "The key cannot be null.");

			int index = IndexOfKey(key);
			if (index >= 0)
				RemoveAt(index);
		}

		/// <summary>
		///		Removes the element at the specified index of the <b>MenuItemCollection</b>.
		/// </summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		///		<paramref name="index"/> is outside the range of valid indices for the <see cref="MenuItemCollection"/>.
		///	</exception>
		/// <exception cref="NotSupportedException">
		///		<para>The <see cref="MenuItemCollection"/> is read-only.</para>
		///		<para>-or-</para>
		///		<para>The <b>MenuItemCollection</b> has a fixed size.</para>
		/// </exception>
		/// <remarks>
		///		<para>The index sequence is based on the sort sequence. When an element is added, 
		///		it is inserted into <see cref="MenuItemCollection"/> in the correct sort order, and 
		///		the indexing adjusts accordingly. When an element removed, the indexing also adjusts 
		///		accordingly. Therefore, the index of a specific key-and-value pair might change as 
		///		elements are added or removed from the <see cref="MenuItemCollection"/>.</para>
		///		<para>In collections of contiguous elements, such as lists, the elements that
		///		follow the removed element move up to occupy the vacated spot. If the collection is
		///		indexed, the indices of the elements that are moved are also updated.</para>
		/// </remarks>
		public virtual void RemoveAt(int index)
		{
			if (index < 0 || index >= count)
				throw new ArgumentOutOfRangeException("index", index, "The index is outside the range of valid indices.");

			count--;
			if (index < count)
			{
				Array.Copy(keys, index + 1, keys, index, count - index);
				Array.Copy(values, index + 1, values, index, count - index);
			}

			// We can't set the deleted entries equal to null, because they might be value types.
			// Instead, we'll create empty single-element arrays of the right type and copy them 
			// over the entries we want to erase.
			string[] tempKey = new string[1];
			MenuItem[] tempVal = new MenuItem[1];
			Array.Copy(tempKey, 0, keys, count, 1);
			Array.Copy(tempVal, 0, values, count, 1);

			version++;
		}

		/// <summary>
		///		Replaces the value at a specific index in the <b>MenuItemCollection</b>.
		/// </summary>
		/// <param name="index">The zero-based index at which to save <paramref name="value"/>.</param>
		/// <param name="value">The <see cref="MenuItem"/> to save into the <see cref="MenuItemCollection"/>.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		///		<paramref name="index"/> is outside the range of valid indices for the <see cref="MenuItemCollection"/>.
		/// </exception>
		/// <remarks>
		///		<para>The index sequence is based on the sort sequence. When an element is added, 
		///		it is inserted into <see cref="MenuItemCollection"/> in the correct sort order, and 
		///		the indexing adjusts accordingly. When an element removed, the indexing also adjusts 
		///		accordingly. Therefore, the index of a specific key-and-value pair might change as 
		///		elements are added or removed from the <see cref="MenuItemCollection"/>.</para>
		/// </remarks>
		public virtual void SetByIndex(int index, MenuItem value)
		{
			if (index < 0 || index >= count)
				throw new ArgumentOutOfRangeException("index", index, "The index is outside the range of valid indices.");

			values[index] = value;
			version++;
		}

		/// <summary>
		///		Returns a synchronized (thread-safe) wrapper for the <b>MenuItemCollection</b>.
		/// </summary>
		/// <param name="list">The <see cref="MenuItemCollection"/> to synchronize.</param>
		/// <returns>A synchronized (thread-safe) wrapper for the <see cref="MenuItemCollection"/>.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="list"/> is a null reference.</exception>
		public static MenuItemCollection Synchronized(MenuItemCollection list)
		{
			if (list == null)
				throw new ArgumentNullException("list", "The list cannot be null.");

			return new SyncSortedList(list);
		}
		
		/// <summary>
		///		Sets the capacity to the actual number of elements in the <b>MenuItemCollection</b>.
		/// </summary>
		/// <exception cref="NotSupportedException">
		///		<para>The <see cref="MenuItemCollection"/> is read-only.</para>
		///		<para>-or-</para>
		///		<para>The <b>MenuItemCollection</b> has a fixed size.</para>
		/// </exception>
		public virtual void TrimToSize()
		{
			this.Capacity = count;
		}
		#endregion

		#region Private Methods
		private void Insert(int index, string key, MenuItem value)
		{
			if (count == keys.Length)
				EnsureCapacity(count + 1);

			if (index < count)
			{
				Array.Copy(keys, index, keys, index + 1, count - index);
				Array.Copy(values, index, values, index + 1, count - index);
			}

			keys[index] = key;
			values[index] = value;
			count++;
			version++;
		}

		private void EnsureCapacity(int min)
		{
			int newCapacity = ((keys.Length == 0) ? DEFAULT_CAPACITY : keys.Length * 2);
			if (newCapacity < min)
				newCapacity = min;

			this.Capacity = newCapacity;
		}
		#endregion

		#region Nested Class: SyncSortedList
		[Serializable]
			private class SyncSortedList : MenuItemCollection, IDictionary
		{
			private MenuItemCollection list;
			private object root;

			internal SyncSortedList(MenuItemCollection list)
			{
				this.list = list;
				this.root = list.SyncRoot;
			}

			public override int Capacity
			{
				get
				{
					lock (root)
						return list.Capacity;
				}
			}

			public override int Count
			{
				get
				{
					lock (root)
						return list.Count;
				}
			}

			public override bool IsFixedSize
			{
				get
				{
					return list.IsFixedSize;
				}
			}

			public override bool IsReadOnly
			{
				get
				{
					return list.IsReadOnly;
				}
			}

			public override bool IsSynchronized
			{
				get
				{
					return true;
				}
			}

			public override object SyncRoot
			{
				get
				{
					return root;
				}
			}

			object IDictionary.this[object key]
			{
				get
				{
					lock (root)
						return ((IDictionary)list)[key];
				}
				set
				{
					lock (root)
						((IDictionary)list)[key] = value;
				}
			}

			public override MenuItem this[string key]
			{
				get
				{
					lock (root)
						return list[key];
				}
				set
				{
					lock (root)
						list[key] = value;
				}
			}

			void IDictionary.Add(object key, object value)
			{
				lock (root)
					((IDictionary)list).Add(key, value);
			}

			public override void Add(string key, MenuItem value)
			{
				lock (root)
					list.Add(key, value);
			}

			public override void Clear()
			{
				lock (root)
					list.Clear();
			}

			public override object Clone()
			{
				lock (root)
					return list.Clone();
			}

			bool IDictionary.Contains(object key)
			{
				lock (root)
					return ((IDictionary)list).Contains(key);
			}

			public override bool Contains(string key)
			{
				lock (root)
					return list.Contains(key);
			}

			public override bool ContainsKey(string key)
			{
				lock (root)
					return list.ContainsKey(key);
			}

			public override bool ContainsValue(MenuItem value)
			{
				lock (root)
					return list.ContainsValue(value);
			}

			public override void CopyTo(Array array, int index)
			{
				lock (root)
					list.CopyTo(array, index);
			}

			public override MenuItem GetByIndex(int index)
			{
				lock (root)
					return list.GetByIndex(index);
			}

			public override IDictionaryEnumerator GetEnumerator()
			{
				lock (root)
					return list.GetEnumerator();
			}

			public override string GetKey(int index)
			{
				lock (root)
					return list.GetKey(index);
			}

			public override IList GetKeyList()
			{
				lock (root)
					return list.GetKeyList();
			}

			public override IList GetValueList()
			{
				lock (root)
					return list.GetValueList();
			}

			public override int IndexOfKey(string key)
			{
				lock (root)
					return list.IndexOfKey(key);
			}

			public override int IndexOfValue(MenuItem value)
			{
				lock (root)
					return list.IndexOfValue(value);
			}

			void IDictionary.Remove(object key)
			{
				lock (root)
					((IDictionary)list).Remove(key);
			}

			public override void Remove(string key)
			{
				lock (root)
					list.Remove(key);
			}

			public override void RemoveAt(int index)
			{
				lock (root)
					list.RemoveAt(index);
			}

			public override void SetByIndex(int index, MenuItem value)
			{
				lock (root)
					list.SetByIndex(index, value);
			}

			public override void TrimToSize()
			{
				lock (root)
					list.TrimToSize();
			}
		}
		#endregion

		#region Nested Class: SortedListEnumerator
		[Serializable]
			private class SortedListEnumerator : IDictionaryEnumerator, ICloneable
		{
			private MenuItemCollection list;
			private string key;
			private MenuItem value;
			private int index;
			private int startIndex;
			private int endIndex;
			private int version;
			private bool currentValid;
			private int returnType;

			internal const int Keys = 1;
			internal const int Values = 2;
			internal const int DictEntry = 3;

			internal SortedListEnumerator(MenuItemCollection list, int index, int count, int returnType)
			{
				this.list = list;
				this.index = index;
				this.startIndex = index;
				this.endIndex = index + count;
				this.version = list.version;
				this.returnType = returnType;
				this.currentValid = false;
			}

			public object Clone()
			{
				return this.MemberwiseClone();
			}

			object IDictionaryEnumerator.Key
			{
				get
				{
					CheckState();
					return key;
				}
			}

			public virtual string Key
			{
				get
				{
					CheckState();
					return key;
				}
			}

			public virtual DictionaryEntry Entry
			{
				get
				{
					CheckState();
					return new DictionaryEntry(key, value);
				}
			}

			public virtual object Current
			{
				get
				{
					CheckState();

					switch (returnType)
					{
						case Keys:
							return key;
						case Values:
							return value;
						case DictEntry:
						default:
							return new DictionaryEntry(key, value);
					}
				}
			}

			object IDictionaryEnumerator.Value
			{
				get
				{
					CheckState();
					return value;
				}
			}

			public virtual MenuItem Value
			{
				get
				{
					CheckState();
					return value;
				}
			}

			public virtual bool MoveNext()
			{
				if (version != list.version)
					throw new InvalidOperationException("The collection was modified - enumeration cannot continue.");
				
				if (index < endIndex)
				{
					key = list.keys[index];
					value = list.values[index];
					index++;
					currentValid = true;
					return true;
				}

				// We can't set the entries equal to null, because they might be value types.
				// Instead, we'll create empty single-element arrays of the right type and copy them 
				// over the entries we want to erase.
				string[] tempKey = new string[1];
				MenuItem[] tempVal = new MenuItem[1];
				key = tempKey[0];
				value = tempVal[0];
				currentValid = false;
				return false;
			}

			public virtual void Reset()
			{
				if (version != list.version)
					throw new InvalidOperationException("The collection was modified - enumeration cannot continue.");

				// We can't set the entries equal to null, because they might be value types.
				// Instead, we'll create empty single-element arrays of the right type and copy them 
				// over the entries we want to erase.
				string[] tempKey = new string[1];
				MenuItem[] tempVal = new MenuItem[1];
				key = tempKey[0];
				value = tempVal[0];
				currentValid = false;
				index = startIndex;
			}

			private void CheckState()
			{
				if (version != list.version)
					throw new InvalidOperationException("The collection was modified - enumeration cannot continue.");
				if (!currentValid)
					throw new InvalidOperationException("Enumeration either has not started or has already finished.");
			}
		}
		#endregion

		#region Nested Class: KeyList
		[Serializable]
			private class KeyList : IList
		{
			private MenuItemCollection list;

			internal KeyList(MenuItemCollection list)
			{
				this.list = list;
			}

			public virtual int Count
			{
				get
				{
					return list.Count;
				}
			}

			public virtual bool IsReadOnly
			{
				get { return true; }
			}

			public virtual bool IsFixedSize
			{
				get { return true; }
			}

			public virtual bool IsSynchronized
			{
				get { return list.IsSynchronized; }
			}

			public virtual object SyncRoot
			{
				get { return list.SyncRoot; }
			}

			public virtual int Add(object key)
			{
				throw new NotSupportedException("Cannot add to a read-only list.");
			}

			public virtual void Clear()
			{
				throw new NotSupportedException("Cannot clear a read-only list.");
			}

			bool IList.Contains(object key)
			{
				return ((IDictionary)list).Contains(key);
			}

			public virtual bool Contains(string key)
			{
				return list.Contains(key);
			}

			public virtual void CopyTo(Array array, int index)
			{
				if (array != null && array.Rank != 1)
																throw new ArgumentException("Multidimensional arrays are not supported.", "array");

				Array.Copy(list.keys, 0, array, index, list.Count);
			}

			public virtual void Insert(int index, object value)
			{
				throw new NotSupportedException("Cannot insert into a read-only list.");
			}

			object IList.this[int index]
			{
				get
				{
					return list.GetKey(index);
				}
				set
				{
					throw new NotSupportedException("Cannot modify a read-only list.");
				}
			}

			public virtual string this[int index]
			{
				get
				{
					return list.GetKey(index);
				}
				set
				{
					throw new NotSupportedException("Cannot modify a read-only list.");
				}
			}

			public virtual IEnumerator GetEnumerator()
			{
				return new SortedListEnumerator(list, 0, list.Count, SortedListEnumerator.Keys);
			}

			int IList.IndexOf(object key)
			{
				if (!(key is string))
					throw new ArgumentException("The key must be of type: " + typeof(string).FullName, "key");

				return list.IndexOfKey((string)key);
			}

			public virtual int IndexOf(string key)
			{
				return list.IndexOfKey(key);
			}

			public virtual void Remove(object key)
			{
				throw new NotSupportedException("Cannot modify a read-only list.");
			}

			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException("Cannot modify a read-only list.");
			}
		}
		#endregion

		#region Nested Class: ValueList
		[Serializable]
			private class ValueList : IList
		{
			private MenuItemCollection list;

			internal ValueList(MenuItemCollection list)
			{
				this.list = list;
			}

			public virtual int Count
			{
				get
				{
					return list.Count;
				}
			}

			public virtual bool IsReadOnly
			{
				get { return true; }
			}

			public virtual bool IsFixedSize
			{
				get { return true; }
			}

			public virtual bool IsSynchronized
			{
				get { return list.IsSynchronized; }
			}

			public virtual object SyncRoot
			{
				get { return list.SyncRoot; }
			}

			public virtual int Add(object key)
			{
				throw new NotSupportedException("Cannot add to a read-only list.");
			}

			public virtual void Clear()
			{
				throw new NotSupportedException("Cannot clear a read-only list.");
			}

			bool IList.Contains(object value)
			{
				if (!(value is MenuItem))
					throw new ArgumentException("The value must be of type: " + typeof(MenuItem).FullName, "value");

				return list.ContainsValue((MenuItem)value);
			}

			public virtual bool Contains(MenuItem value)
			{
				return list.ContainsValue(value);
			}

			public virtual void CopyTo(Array array, int index)
			{
				if (array != null && array.Rank != 1)
																throw new ArgumentException("Multidimensional arrays are not supported.", "array");

				Array.Copy(list.values, 0, array, index, list.Count);
			}

			public virtual void Insert(int index, object value)
			{
				throw new NotSupportedException("Cannot insert into a read-only list.");
			}

			object IList.this[int index]
			{
				get
				{
					return list.GetByIndex(index);
				}
				set
				{
					if (!(value is MenuItem))
						throw new ArgumentException("The value must be of type: " + typeof(MenuItem).FullName, "value");

					list.SetByIndex(index, (MenuItem)value);
				}
			}

			public virtual MenuItem this[int index]
			{
				get
				{
					return list.GetByIndex(index);
				}
				set
				{
					list.SetByIndex(index, value);
				}
			}

			public virtual IEnumerator GetEnumerator()
			{
				return new SortedListEnumerator(list, 0, list.Count, SortedListEnumerator.Values);
			}

			int IList.IndexOf(object value)
			{
				if (!(value is MenuItem))
					throw new ArgumentException("The value must be of type: " + typeof(MenuItem).FullName, "value");

				return list.IndexOfValue((MenuItem)value);
			}

			public virtual int IndexOf(MenuItem value)
			{
				return list.IndexOfValue(value);
			}

			public virtual void Remove(object key)
			{
				throw new NotSupportedException("Cannot modify a read-only list.");
			}

			public virtual void RemoveAt(int index)
			{
				throw new NotSupportedException("Cannot modify a read-only list.");
			}
		}
		#endregion
	}
}
