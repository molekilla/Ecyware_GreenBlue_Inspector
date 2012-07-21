// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Collections;
using Ecyware.GreenBlue.Engine;

namespace Ecyware.GreenBlue.Engine 
{
	#region Class SessionRequestList

	/// <summary>
	/// Implements a strongly typed collection of <see cref="SessionRequest"/> elements.
	/// </summary>
	/// <remarks>
	/// <b>SessionRequestList</b> provides an <see cref="ArrayList"/> 
	/// that is strongly typed for <see cref="SessionRequest"/> elements.
	/// </remarks>    

	[Serializable]
	public class SessionRequestList: 
		ISessionRequestList, IList, ICloneable 
	{
		#region Private Fields
            
		private const int _defaultCapacity = 16;

		private SessionRequest[] _array = null;
		private int _count = 0;

		[NonSerialized]
		private int _version = 0;
        
		#endregion
		#region Private Constructors
        
		private enum Tag { Default }

		private SessionRequestList(Tag tag) { }
        
		#endregion
		#region Public Constructors

		/// <overloads>
		/// Initializes a new instance of the <see cref="SessionRequestList"/> class.
		/// </overloads>
		/// <summary>
		/// Initializes a new instance of the <see cref="SessionRequestList"/> class
		/// that is empty and has the default initial capacity.
		/// </summary>
		/// <remarks>Please refer to <see cref="ArrayList()"/> for details.</remarks>    

		public SessionRequestList() 
		{
			this._array = new SessionRequest[_defaultCapacity];
		}
        
		/// <summary>
		/// Initializes a new instance of the <see cref="SessionRequestList"/> class
		/// that is empty and has the specified initial capacity.
		/// </summary>
		/// <param name="capacity">The number of elements that the new 
		/// <see cref="SessionRequestList"/> is initially capable of storing.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="capacity"/> is less than zero.</exception>    
		/// <remarks>Please refer to <see cref="ArrayList(Int32)"/> for details.</remarks>    

		public SessionRequestList(int capacity) 
		{
			if (capacity < 0)
				throw new ArgumentOutOfRangeException("capacity", capacity, "Argument cannot be negative.");

			this._array = new SessionRequest[capacity];
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SessionRequestList"/> class
		/// that contains elements copied from the specified collection and
		/// that has the same initial capacity as the number of elements copied.
		/// </summary>
		/// <param name="collection">The <see cref="SessionRequestList"/> 
		/// whose elements are copied to the new collection.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="collection"/> is a null reference.</exception>        
		/// <remarks>Please refer to <see cref="ArrayList(ICollection)"/> for details.</remarks>    

		public SessionRequestList(SessionRequestList collection) 
		{
			if (collection == null)
				throw new ArgumentNullException("collection");

			this._array = new SessionRequest[collection.Count];
			AddRange(collection);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SessionRequestList"/> class
		/// that contains elements copied from the specified <see cref="SessionRequest"/>
		/// array and that has the same initial capacity as the number of elements copied.
		/// </summary>
		/// <param name="array">An <see cref="Array"/> of <see cref="SessionRequest"/> 
		/// elements that are copied to the new collection.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="array"/> is a null reference.</exception>        
		/// <remarks>Please refer to <see cref="ArrayList(ICollection)"/> for details.</remarks>    

		public SessionRequestList(SessionRequest[] array) 
		{
			if (array == null)
				throw new ArgumentNullException("array");

			this._array = new SessionRequest[array.Length];
			AddRange(array);
		}
        
		#endregion
		#region Public Properties
		#region Capacity

		/// <summary>
		/// Gets or sets the capacity of the <see cref="SessionRequestList"/>.
		/// </summary>
		/// <value>The number of elements that the 
		/// <see cref="SessionRequestList"/> can contain.</value>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <b>Capacity</b> is set to a value that is less than <see cref="Count"/>.</exception>
		/// <remarks>Please refer to <see cref="ArrayList.Capacity"/> for details.</remarks>

		public virtual int Capacity 
		{
			get { return this._array.Length; }
			set 
			{
				if (value == this._array.Length) return;
                
				if (value < this._count)
					throw new ArgumentOutOfRangeException("Capacity", value, "Value cannot be less than Count.");

				if (value == 0) 
				{
					this._array = new SessionRequest[_defaultCapacity];
					return;
				}

				SessionRequest[] newArray = new SessionRequest[value];
				Array.Copy(this._array, newArray, this._count);
				this._array = newArray;
			}
		}

		#endregion
		#region Count

		/// <summary>
		/// Gets the number of elements contained in the <see cref="SessionRequestList"/>.
		/// </summary>
		/// <value>
		/// The number of elements contained in the <see cref="SessionRequestList"/>.
		/// </value>
		/// <remarks>Please refer to <see cref="ArrayList.Count"/> for details.</remarks>

		public virtual int Count 
		{
			get { return this._count; }
		}
        
		#endregion
		#region IsFixedSize

		/// <summary>
		/// Gets a value indicating whether the <see cref="SessionRequestList"/> has a fixed size.
		/// </summary>
		/// <value><c>true</c> if the <see cref="SessionRequestList"/> has a fixed size;
		/// otherwise, <c>false</c>. The default is <c>false</c>.</value>
		/// <remarks>Please refer to <see cref="ArrayList.IsFixedSize"/> for details.</remarks>

		public virtual bool IsFixedSize 
		{
			get { return false; }
		}

		#endregion
		#region IsReadOnly

		/// <summary>
		/// Gets a value indicating whether the <see cref="SessionRequestList"/> is read-only.
		/// </summary>
		/// <value><c>true</c> if the <see cref="SessionRequestList"/> is read-only;
		/// otherwise, <c>false</c>. The default is <c>false</c>.</value>
		/// <remarks>Please refer to <see cref="ArrayList.IsReadOnly"/> for details.</remarks>

		public virtual bool IsReadOnly 
		{
			get { return false; }
		}

		#endregion
		#region IsSynchronized

		/// <summary>
		/// Gets a value indicating whether access to the <see cref="SessionRequestList"/> 
		/// is synchronized (thread-safe).
		/// </summary>
		/// <value><c>true</c> if access to the <see cref="SessionRequestList"/> is 
		/// synchronized (thread-safe); otherwise, <c>false</c>. The default is <c>false</c>.</value>
		/// <remarks>Please refer to <see cref="ArrayList.IsSynchronized"/> for details.</remarks>

		public virtual bool IsSynchronized 
		{
			get { return false; }
		}

		#endregion
		#region Item

		/// <summary>
		/// Gets or sets the <see cref="SessionRequest"/> element at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the 
		/// <see cref="SessionRequest"/> element to get or set.</param>
		/// <value>
		/// The <see cref="SessionRequest"/> element at the specified <paramref name="index"/>.
		/// </value>    
		/// <exception cref="ArgumentOutOfRangeException">
		/// <para><paramref name="index"/> is less than zero.</para>
		/// <para>-or-</para>
		/// <para><paramref name="index"/> is equal to or greater than <see cref="Count"/>.</para>
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// The property is set and the <see cref="SessionRequestList"/> is read-only.</exception>
		/// <remarks>Please refer to <see cref="ArrayList.this"/> for details.</remarks>

		public virtual SessionRequest this[int index] 
		{
			get 
			{
				ValidateIndex(index);
				return this._array[index]; 
			}
			set 
			{
				ValidateIndex(index);
				++this._version; 
				this._array[index] = value; 
			}
		}

		/// <summary>
		/// Gets or sets the element at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the element to get or set.</param>
		/// <value>
		/// The element at the specified <paramref name="index"/>. When the property
		/// is set, this value must be compatible with <see cref="SessionRequest"/>.
		/// </value>    
		/// <exception cref="ArgumentOutOfRangeException">
		/// <para><paramref name="index"/> is less than zero.</para>
		/// <para>-or-</para>
		/// <para><paramref name="index"/> is equal to or greater than <see cref="Count"/>.</para>
		/// </exception>
		/// <exception cref="InvalidCastException">The property is set to a value
		/// that is not compatible with <see cref="SessionRequest"/>.</exception>
		/// <exception cref="NotSupportedException">
		/// The property is set and the <see cref="SessionRequestList"/> is read-only.</exception>
		/// <remarks>Please refer to <see cref="ArrayList.this"/> for details.</remarks>

		object IList.this[int index] 
		{
			get { return this[index]; }
			set { this[index] = (SessionRequest) value; }
		}

		#endregion
		#region SyncRoot

		/// <summary>
		/// Gets an object that can be used to synchronize 
		/// access to the <see cref="SessionRequestList"/>.
		/// </summary>
		/// <value>An object that can be used to synchronize 
		/// access to the <see cref="SessionRequestList"/>.
		/// </value>
		/// <remarks>Please refer to <see cref="ArrayList.SyncRoot"/> for details.</remarks>

		public virtual object SyncRoot 
		{
			get { return this; }
		}

		#endregion
		#endregion
		#region Public Methods
		#region Add    

		/// <summary>
		/// Adds a <see cref="SessionRequest"/> to the end of the <see cref="SessionRequestList"/>.
		/// </summary>
		/// <param name="value">The <see cref="SessionRequest"/> object 
		/// to be added to the end of the <see cref="SessionRequestList"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <returns>The <see cref="SessionRequestList"/> index at which the 
		/// <paramref name="value"/> has been added.</returns>
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="SessionRequestList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>SessionRequestList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="ArrayList.Add"/> for details.</remarks>

		public virtual int Add(SessionRequest value) 
		{
			if (this._count == this._array.Length)
				EnsureCapacity(this._count + 1);

			++this._version;
			this._array[this._count] = value;
			return this._count++;
		}
        
		/// <summary>
		/// Adds an <see cref="Object"/> to the end of the <see cref="SessionRequestList"/>.
		/// </summary>
		/// <param name="value">
		/// The object to be added to the end of the <see cref="SessionRequestList"/>.
		/// This argument must be compatible with <see cref="SessionRequest"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <returns>The <see cref="SessionRequestList"/> index at which the 
		/// <paramref name="value"/> has been added.</returns>
		/// <exception cref="InvalidCastException"><paramref name="value"/> 
		/// is not compatible with <see cref="SessionRequest"/>.</exception>    
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="SessionRequestList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>SessionRequestList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="ArrayList.Add"/> for details.</remarks>

		int IList.Add(object value) 
		{
			return Add((SessionRequest) value);
		}

		#endregion
		#region AddRange

		/// <overloads>
		/// Adds a range of elements to the end of the <see cref="SessionRequestList"/>.
		/// </overloads>
		/// <summary>
		/// Adds the elements of another collection to the end of the <see cref="SessionRequestList"/>.
		/// </summary>
		/// <param name="collection">The <see cref="SessionRequestList"/> whose elements 
		/// should be added to the end of the current collection.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="collection"/> is a null reference.</exception>    
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="SessionRequestList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>SessionRequestList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="ArrayList.AddRange"/> for details.</remarks>

		public virtual void AddRange(SessionRequestList collection) 
		{
			if (collection == null)
				throw new ArgumentNullException("collection");

			if (collection.Count == 0) return;
			if (this._count + collection.Count > this._array.Length)
				EnsureCapacity(this._count + collection.Count);

			++this._version;
			Array.Copy(collection._array, 0, this._array, this._count, collection.Count);
			this._count += collection.Count;
		}

		/// <summary>
		/// Adds the elements of a <see cref="SessionRequest"/> array 
		/// to the end of the <see cref="SessionRequestList"/>.
		/// </summary>
		/// <param name="array">An <see cref="Array"/> of <see cref="SessionRequest"/> elements
		/// that should be added to the end of the <see cref="SessionRequestList"/>.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="array"/> is a null reference.</exception>    
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="SessionRequestList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>SessionRequestList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="ArrayList.AddRange"/> for details.</remarks>

		public virtual void AddRange(SessionRequest[] array) 
		{
			if (array == null)
				throw new ArgumentNullException("array");

			if (array.Length == 0) return;
			if (this._count + array.Length > this._array.Length)
				EnsureCapacity(this._count + array.Length);

			++this._version;
			Array.Copy(array, 0, this._array, this._count, array.Length);
			this._count += array.Length;
		}
        
		#endregion
		#region BinarySearch
            
		/// <summary>
		/// Searches the entire sorted <see cref="SessionRequestList"/> for an 
		/// <see cref="SessionRequest"/> element using the default comparer 
		/// and returns the zero-based index of the element.
		/// </summary>
		/// <param name="value">The <see cref="SessionRequest"/> object
		/// to locate in the <see cref="SessionRequestList"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <returns>The zero-based index of <paramref name="value"/> in the sorted 
		/// <see cref="SessionRequestList"/>, if <paramref name="value"/> is found; 
		/// otherwise, a negative number, which is the bitwise complement of the index 
		/// of the next element that is larger than <paramref name="value"/> or, if there 
		/// is no larger element, the bitwise complement of <see cref="Count"/>.</returns>
		/// <exception cref="InvalidOperationException">
		/// Neither <paramref name="value"/> nor the elements of the <see cref="SessionRequestList"/> 
		/// implement the <see cref="IComparable"/> interface.</exception>
		/// <remarks>Please refer to <see cref="ArrayList.BinarySearch"/> for details.</remarks>

		public virtual int BinarySearch(SessionRequest value) 
		{
			return Array.BinarySearch(this._array, 0, this._count, value);
		}    
            
		#endregion    
		#region Clear

		/// <summary>
		/// Removes all elements from the <see cref="SessionRequestList"/>.
		/// </summary>
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="SessionRequestList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>SessionRequestList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="ArrayList.Clear"/> for details.</remarks>

		public virtual void Clear() 
		{
			if (this._count == 0) return;

			++this._version;
			Array.Clear(this._array, 0, this._count);
			this._count = 0;
		}
        
		#endregion
		#region Clone

		/// <summary>
		/// Creates a shallow copy of the <see cref="SessionRequestList"/>.
		/// </summary>
		/// <returns>A shallow copy of the <see cref="SessionRequestList"/>.</returns>
		/// <remarks>Please refer to <see cref="ArrayList.Clone"/> for details.</remarks>

		public virtual object Clone() 
		{
			SessionRequestList collection = new SessionRequestList(this._count);
            
			Array.Copy(this._array, 0, collection._array, 0, this._count);
			collection._count = this._count;
			collection._version = this._version;

			return collection;
		}

		#endregion
		#region Contains

		/// <summary>
		/// Determines whether the <see cref="SessionRequestList"/>
		/// contains the specified <see cref="SessionRequest"/> element.
		/// </summary>
		/// <param name="value">The <see cref="SessionRequest"/> object
		/// to locate in the <see cref="SessionRequestList"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <returns><c>true</c> if <paramref name="value"/> is found in the 
		/// <see cref="SessionRequestList"/>; otherwise, <c>false</c>.</returns>
		/// <remarks>Please refer to <see cref="ArrayList.Contains"/> for details.</remarks>

		public virtual bool Contains(SessionRequest value) 
		{
			return (IndexOf(value) >= 0);
		}

		/// <summary>
		/// Determines whether the <see cref="SessionRequestList"/> contains the specified element.
		/// </summary>
		/// <param name="value">The object to locate in the <see cref="SessionRequestList"/>.
		/// This argument must be compatible with <see cref="SessionRequest"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <returns><c>true</c> if <paramref name="value"/> is found in the 
		/// <see cref="SessionRequestList"/>; otherwise, <c>false</c>.</returns>
		/// <exception cref="InvalidCastException"><paramref name="value"/> 
		/// is not compatible with <see cref="SessionRequest"/>.</exception>    
		/// <remarks>Please refer to <see cref="ArrayList.Contains"/> for details.</remarks>

		bool IList.Contains(object value) 
		{
			return Contains((SessionRequest) value);
		}

		#endregion
		#region CopyTo

		/// <overloads>
		/// Copies the <see cref="SessionRequestList"/> or a portion of it to a one-dimensional array.
		/// </overloads>
		/// <summary>
		/// Copies the entire <see cref="SessionRequestList"/> to a one-dimensional <see cref="Array"/>
		/// of <see cref="SessionRequest"/> elements, starting at the beginning of the target array.
		/// </summary>
		/// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the
		/// <see cref="SessionRequest"/> elements copied from the <see cref="SessionRequestList"/>.
		/// The <b>Array</b> must have zero-based indexing.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="array"/> is a null reference.</exception>    
		/// <exception cref="ArgumentException">
		/// The number of elements in the source <see cref="SessionRequestList"/> is greater 
		/// than the available space in the destination <paramref name="array"/>.</exception>
		/// <remarks>Please refer to <see cref="ArrayList.CopyTo"/> for details.</remarks>

		public virtual void CopyTo(SessionRequest[] array) 
		{
			CheckTargetArray(array, 0);
			Array.Copy(this._array, array, this._count); 
		}
        
		/// <summary>
		/// Copies the entire <see cref="SessionRequestList"/> to a one-dimensional <see cref="Array"/>
		/// of <see cref="SessionRequest"/> elements, starting at the specified index of the target array.
		/// </summary>
		/// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the
		/// <see cref="SessionRequest"/> elements copied from the <see cref="SessionRequestList"/>.
		/// The <b>Array</b> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array"/> 
		/// at which copying begins.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="array"/> is a null reference.</exception>    
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="arrayIndex"/> is less than zero.</exception>    
		/// <exception cref="ArgumentException"><para>
		/// <paramref name="arrayIndex"/> is equal to or greater than the length of <paramref name="array"/>.
		/// </para><para>-or-</para><para>
		/// The number of elements in the source <see cref="SessionRequestList"/> is greater than the
		/// available space from <paramref name="arrayIndex"/> to the end of the destination 
		/// <paramref name="array"/>.</para></exception>
		/// <remarks>Please refer to <see cref="ArrayList.CopyTo"/> for details.</remarks>

		public virtual void CopyTo(SessionRequest[] array, int arrayIndex) 
		{
			CheckTargetArray(array, arrayIndex);
			Array.Copy(this._array, 0, array, arrayIndex, this._count); 
		}

		/// <summary>
		/// Copies the entire <see cref="SessionRequestList"/> to a one-dimensional <see cref="Array"/>,
		/// starting at the specified index of the target array.
		/// </summary>
		/// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the
		/// <see cref="SessionRequest"/> elements copied from the <see cref="SessionRequestList"/>.
		/// The <b>Array</b> must have zero-based indexing.</param>
		/// <param name="arrayIndex">The zero-based index in <paramref name="array"/> 
		/// at which copying begins.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="array"/> is a null reference.</exception>    
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="arrayIndex"/> is less than zero.</exception>    
		/// <exception cref="ArgumentException"><para>
		/// <paramref name="array"/> is multidimensional.    
		/// </para><para>-or-</para><para>
		/// <paramref name="arrayIndex"/> is equal to or greater than the length of <paramref name="array"/>.
		/// </para><para>-or-</para><para>
		/// The number of elements in the source <see cref="SessionRequestList"/> is greater than the
		/// available space from <paramref name="arrayIndex"/> to the end of the destination 
		/// <paramref name="array"/>.</para></exception>
		/// <exception cref="InvalidCastException">
		/// The <see cref="SessionRequest"/> type cannot be cast automatically 
		/// to the type of the destination <paramref name="array"/>.</exception>    
		/// <remarks>Please refer to <see cref="ArrayList.CopyTo"/> for details.</remarks>

		void ICollection.CopyTo(Array array, int arrayIndex) 
		{
			CheckTargetArray(array, arrayIndex);
			CopyTo((SessionRequest[]) array, arrayIndex);
		}

		#endregion
		#region GetEnumerator

		/// <summary>
		/// Returns an <see cref="ISessionRequestEnumerator"/> that can
		/// iterate through the <see cref="SessionRequestList"/>.
		/// </summary>
		/// <returns>An <see cref="ISessionRequestEnumerator"/> 
		/// for the entire <see cref="SessionRequestList"/>.</returns>
		/// <remarks>Please refer to <see cref="ArrayList.GetEnumerator"/> for details.</remarks>

		public virtual ISessionRequestEnumerator GetEnumerator() 
		{
			return new Enumerator(this);
		}

		/// <summary>
		/// Returns an <see cref="IEnumerator"/> that can
		/// iterate through the <see cref="SessionRequestList"/>.
		/// </summary>
		/// <returns>An <see cref="IEnumerator"/>
		/// for the entire <see cref="SessionRequestList"/>.</returns>
		/// <remarks>Please refer to <see cref="ArrayList.GetEnumerator"/> for details.</remarks>

		IEnumerator IEnumerable.GetEnumerator() 
		{
			return (IEnumerator) GetEnumerator();
		}

		#endregion
		#region IndexOf

		/// <summary>
		/// Returns the zero-based index of the first occurrence of the specified 
		/// <see cref="SessionRequest"/> in the <see cref="SessionRequestList"/>.
		/// </summary>
		/// <param name="value">The <see cref="SessionRequest"/> object 
		/// to locate in the <see cref="SessionRequestList"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <returns>
		/// The zero-based index of the first occurrence of <paramref name="value"/> 
		/// in the <see cref="SessionRequestList"/>, if found; otherwise, -1.
		/// </returns>
		/// <remarks>Please refer to <see cref="ArrayList.IndexOf"/> for details.</remarks>

		public virtual int IndexOf(SessionRequest value) 
		{
			return Array.IndexOf(this._array, value, 0, this._count);
		}

		/// <summary>
		/// Returns the zero-based index of the first occurrence of the specified 
		/// <see cref="Object"/> in the <see cref="SessionRequestList"/>.
		/// </summary>
		/// <param name="value">The object to locate in the <see cref="SessionRequestList"/>.
		/// This argument must be compatible with <see cref="SessionRequest"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <returns>
		/// The zero-based index of the first occurrence of <paramref name="value"/> 
		/// in the <see cref="SessionRequestList"/>, if found; otherwise, -1.
		/// </returns>
		/// <exception cref="InvalidCastException"><paramref name="value"/>
		/// is not compatible with <see cref="SessionRequest"/>.</exception>    
		/// <remarks>Please refer to <see cref="ArrayList.IndexOf"/> for details.</remarks>

		int IList.IndexOf(object value) 
		{
			return IndexOf((SessionRequest) value);
		}

		#endregion
		#region Insert

		/// <summary>
		/// Inserts a <see cref="SessionRequest"/> element into the 
		/// <see cref="SessionRequestList"/> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which <paramref name="value"/> 
		/// should be inserted.</param>
		/// <param name="value">The <see cref="SessionRequest"/> object
		/// to insert into the <see cref="SessionRequestList"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <exception cref="ArgumentOutOfRangeException">
		/// <para><paramref name="index"/> is less than zero.</para>
		/// <para>-or-</para>
		/// <para><paramref name="index"/> is greater than <see cref="Count"/>.</para>
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="SessionRequestList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>SessionRequestList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="ArrayList.Insert"/> for details.</remarks>

		public virtual void Insert(int index, SessionRequest value) 
		{
			if (index < 0)
				throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");
			if (index > this._count)
				throw new ArgumentOutOfRangeException("index", index, "Argument cannot exceed Count.");
            
			if (this._count == this._array.Length)
				EnsureCapacity(this._count + 1);

			++this._version;
			if (index < this._count)
				Array.Copy(this._array, index, this._array, index + 1, this._count - index);

			this._array[index] = value;
			++this._count;
		}

		/// <summary>
		/// Inserts an element into the <see cref="SessionRequestList"/> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which <paramref name="value"/> 
		/// should be inserted.</param>
		/// <param name="value">The object to insert into the <see cref="SessionRequestList"/>.
		/// This argument must be compatible with <see cref="SessionRequest"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <exception cref="ArgumentOutOfRangeException">
		/// <para><paramref name="index"/> is less than zero.</para>
		/// <para>-or-</para>
		/// <para><paramref name="index"/> is greater than <see cref="Count"/>.</para>
		/// </exception>
		/// <exception cref="InvalidCastException"><paramref name="value"/>
		/// is not compatible with <see cref="SessionRequest"/>.</exception>    
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="SessionRequestList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>SessionRequestList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="ArrayList.Insert"/> for details.</remarks>

		void IList.Insert(int index, object value) 
		{
			Insert(index, (SessionRequest) value);
		}

		#endregion
		#region ReadOnly

		/// <summary>
		/// Returns a read-only wrapper for the specified <see cref="SessionRequestList"/>.
		/// </summary>
		/// <param name="collection">The <see cref="SessionRequestList"/> to wrap.</param>    
		/// <returns>A read-only wrapper around <paramref name="collection"/>.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="collection"/> is a null reference.</exception>    
		/// <remarks>Please refer to <see cref="ArrayList.ReadOnly"/> for details.</remarks>

		public static SessionRequestList ReadOnly(SessionRequestList collection) 
		{
			if (collection == null)
				throw new ArgumentNullException("collection");

			return new ReadOnlyList(collection);
		}

		#endregion
		#region Remove

		/// <summary>
		/// Removes the first occurrence of the specified <see cref="SessionRequest"/>
		/// from the <see cref="SessionRequestList"/>.
		/// </summary>
		/// <param name="value">The <see cref="SessionRequest"/> object
		/// to remove from the <see cref="SessionRequestList"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="SessionRequestList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>SessionRequestList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="ArrayList.Remove"/> for details.</remarks>

		public virtual void Remove(SessionRequest value) 
		{
			int index = IndexOf(value);
			if (index >= 0) RemoveAt(index);
		}

		/// <summary>
		/// Removes the first occurrence of the specified <see cref="Object"/>
		/// from the <see cref="SessionRequestList"/>.
		/// </summary>
		/// <param name="value">The object to remove from the <see cref="SessionRequestList"/>.
		/// This argument must be compatible with <see cref="SessionRequest"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <exception cref="InvalidCastException"><paramref name="value"/>
		/// is not compatible with <see cref="SessionRequest"/>.</exception>    
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="SessionRequestList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>SessionRequestList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="ArrayList.Remove"/> for details.</remarks>

		void IList.Remove(object value) 
		{
			Remove((SessionRequest) value);
		}

		#endregion
		#region RemoveAt

		/// <summary>
		/// Removes the element at the specified index of the <see cref="SessionRequestList"/>.
		/// </summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <para><paramref name="index"/> is less than zero.</para>
		/// <para>-or-</para>
		/// <para><paramref name="index"/> is equal to or greater than <see cref="Count"/>.</para>
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="SessionRequestList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>SessionRequestList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="ArrayList.RemoveAt"/> for details.</remarks>

		public virtual void RemoveAt(int index) 
		{
			ValidateIndex(index);
            
			++this._version;
			if (index < --this._count)
				Array.Copy(this._array, index + 1, this._array, index, this._count - index);
            
			this._array[this._count] = null;
		}

		#endregion
		#region RemoveRange
        
		/// <summary>
		/// Removes a range of elements from the <see cref="SessionRequestList"/>.
		/// </summary>
		/// <param name="index">The zero-based starting index of the range
		/// of elements to remove.</param>
		/// <param name="count">The number of elements to remove.</param>
		/// <exception cref="ArgumentException">
		/// <paramref name="index"/> and <paramref name="count"/> do not denote a
		/// valid range of elements in the <see cref="SessionRequestList"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <para><paramref name="index"/> is less than zero.</para>
		/// <para>-or-</para>
		/// <para><paramref name="count"/> is less than zero.</para>
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="SessionRequestList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>SessionRequestList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="ArrayList.RemoveRange"/> for details.</remarks>

		public virtual void RemoveRange(int index, int count) 
		{
			if (index < 0)
				throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");
			if (count < 0)    
				throw new ArgumentOutOfRangeException("count", count, "Argument cannot be negative.");
			if (index + count > this._count)
				throw new ArgumentException("Arguments denote invalid range of elements.");
                
			if (count == 0) return;

			++this._version;
			this._count -= count;

			if (index < this._count)
				Array.Copy(this._array, index + count, this._array, index, this._count - index);

			Array.Clear(this._array, this._count, count);
		}

		#endregion
		#region Sort

		/// <summary>
		/// Sorts the elements in the entire <see cref="SessionRequestList"/>
		/// using the <see cref="IComparable"/> implementation of each element.
		/// </summary>
		/// <exception cref="NotSupportedException">
		/// The <see cref="SessionRequestList"/> is read-only.</exception>
		/// <remarks>Please refer to <see cref="ArrayList.Sort"/> for details.</remarks>

		public virtual void Sort() 
		{
			if (this._count <= 1) return;
			++this._version;
			Array.Sort(this._array, 0, this._count);
		}
        
		#endregion
		#region Synchronized

		/// <summary>
		/// Returns a synchronized (thread-safe) wrapper 
		/// for the specified <see cref="SessionRequestList"/>.
		/// </summary>
		/// <param name="collection">The <see cref="SessionRequestList"/> to synchronize.</param>    
		/// <returns>
		/// A synchronized (thread-safe) wrapper around <paramref name="collection"/>.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="collection"/> is a null reference.</exception>    
		/// <remarks>Please refer to <see cref="ArrayList.Synchronized"/> for details.</remarks>

		public static SessionRequestList Synchronized(SessionRequestList collection) 
		{
			if (collection == null)
				throw new ArgumentNullException("collection");

			return new SyncList(collection);
		}

		#endregion
		#region ToArray

		/// <summary>
		/// Copies the elements of the <see cref="SessionRequestList"/> to a new
		/// <see cref="Array"/> of <see cref="SessionRequest"/> elements.
		/// </summary>
		/// <returns>A one-dimensional <see cref="Array"/> of <see cref="SessionRequest"/> 
		/// elements containing copies of the elements of the <see cref="SessionRequestList"/>.</returns>
		/// <remarks>Please refer to <see cref="ArrayList.ToArray"/> for details.</remarks>

		public virtual SessionRequest[] ToArray() 
		{
			SessionRequest[] array = new SessionRequest[this._count];
			Array.Copy(this._array, array, this._count);
			return array;
		}
        
		#endregion
		#region TrimToSize

		/// <summary>
		/// Sets the capacity to the actual number of elements in the <see cref="SessionRequestList"/>.
		/// </summary>
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="SessionRequestList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>SessionRequestList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="ArrayList.TrimToSize"/> for details.</remarks>

		public virtual void TrimToSize() 
		{
			Capacity = this._count;
		}

		#endregion
		#endregion
		#region Private Methods

		private void CheckEnumIndex(int index) 
		{
			if (index < 0 || index >= this._count)
				throw new InvalidOperationException("Enumerator is not on a collection element.");
		}

		private void CheckEnumVersion(int version) 
		{
			if (version != this._version)
				throw new InvalidOperationException("Enumerator invalidated by modification to collection.");
		}

		private void CheckTargetArray(Array array, int arrayIndex) 
		{
			if (array == null)
				throw new ArgumentNullException("array");
			if (array.Rank > 1)
				throw new ArgumentException("Argument cannot be multidimensional.", "array");
			if (arrayIndex < 0)
				throw new ArgumentOutOfRangeException("arrayIndex", arrayIndex, "Argument cannot be negative.");
			if (arrayIndex >= array.Length)
				throw new ArgumentException("Argument must be less than array length.", "arrayIndex");
			if (this._count > array.Length - arrayIndex)
				throw new ArgumentException("Argument section must be large enough for collection.", "array");
		}

		private void EnsureCapacity(int minimum) 
		{
			int newCapacity = (this._array.Length == 0 ? 
			_defaultCapacity : this._array.Length * 2);

			if (newCapacity < minimum) newCapacity = minimum;
			Capacity = newCapacity;
		}

		private void ValidateIndex(int index) 
		{
			if (index < 0)
				throw new ArgumentOutOfRangeException("index", index, "Argument cannot be negative.");
			if (index >= this._count)
				throw new ArgumentOutOfRangeException("index", index, "Argument must be less than Count.");
		}

		#endregion
		#region Class Enumerator

		[Serializable]
			private sealed class Enumerator: 
			ISessionRequestEnumerator, IEnumerator 
		{
            
			private readonly SessionRequestList _collection;
			private readonly int _version;
			private int _index;
            
			internal Enumerator(SessionRequestList collection) 
			{
				this._collection = collection;
				this._version = collection._version;
				this._index = -1;
			}
            
			public SessionRequest Current 
			{
				get 
				{ 
					this._collection.CheckEnumIndex(this._index);
					return this._collection[this._index]; 
				}
			}
            
			object IEnumerator.Current 
			{
				get { return Current; }
			}
            
			public bool MoveNext() 
			{
				this._collection.CheckEnumVersion(this._version);
				return (++this._index < this._collection.Count);
			}

			public void Reset() 
			{
				this._collection.CheckEnumVersion(this._version);
				this._index = -1;
			}

		}
        
		#endregion
		#region Class ReadOnlyList

		[Serializable]
			private sealed class ReadOnlyList: SessionRequestList 
		{
            
			private SessionRequestList _collection;

			internal ReadOnlyList(SessionRequestList collection): 
				base(Tag.Default) 
			{
				this._collection = collection;
			}
            
			#region Public Properties
            
			public override int Capacity 
			{
				get { return this._collection.Capacity; }                
				set { throw new NotSupportedException("Read-only collections cannot be modified."); }
			}

			public override int Count 
			{
				get { return this._collection.Count; }
			}

			public override bool IsFixedSize 
			{
				get { return true; }
			}

			public override bool IsReadOnly 
			{
				get { return true; }
			}

			public override bool IsSynchronized 
			{
				get { return this._collection.IsSynchronized; }
			}

			public override SessionRequest this[int index] 
			{
				get { return this._collection[index]; }
				set { throw new NotSupportedException("Read-only collections cannot be modified."); }
			}

			public override object SyncRoot 
			{
				get { return this._collection.SyncRoot; }
			}

			#endregion
			#region Public Methods

			public override int Add(SessionRequest value) 
			{
				throw new NotSupportedException("Read-only collections cannot be modified.");
			}
            
			public override void AddRange(SessionRequestList collection) 
			{
				throw new NotSupportedException("Read-only collections cannot be modified.");
			}

			public override void AddRange(SessionRequest[] array) 
			{
				throw new NotSupportedException("Read-only collections cannot be modified.");
			}
            
			public override int BinarySearch(SessionRequest value) 
			{
				return this._collection.BinarySearch(value);
			}

			public override void Clear() 
			{
				throw new NotSupportedException("Read-only collections cannot be modified.");
			}

			public override object Clone() 
			{
				return new ReadOnlyList((SessionRequestList) this._collection.Clone());
			}

			public override bool Contains(SessionRequest value) 
			{
				return this._collection.Contains(value);
			}

			public override void CopyTo(SessionRequest[] array) 
			{
				this._collection.CopyTo(array);
			}

			public override void CopyTo(SessionRequest[] array, int arrayIndex) 
			{
				this._collection.CopyTo(array, arrayIndex);
			}
            
			public override ISessionRequestEnumerator GetEnumerator() 
			{
				return this._collection.GetEnumerator();
			}

			public override int IndexOf(SessionRequest value) 
			{
				return this._collection.IndexOf(value);
			}

			public override void Insert(int index, SessionRequest value) 
			{
				throw new NotSupportedException("Read-only collections cannot be modified.");
			}

			public override void Remove(SessionRequest value) 
			{           
				throw new NotSupportedException("Read-only collections cannot be modified.");
			}

			public override void RemoveAt(int index) 
			{
				throw new NotSupportedException("Read-only collections cannot be modified.");
			}
            
			public override void RemoveRange(int index, int count) 
			{
				throw new NotSupportedException("Read-only collections cannot be modified.");
			}
            
			public override void Sort() 
			{
				throw new NotSupportedException("Read-only collections cannot be modified.");
			}
            
			public override SessionRequest[] ToArray() 
			{
				return this._collection.ToArray();
			}

			public override void TrimToSize() 
			{
				throw new NotSupportedException("Read-only collections cannot be modified.");
			}
            
			#endregion
		}

		#endregion
		#region Class SyncList

		[Serializable]
			private sealed class SyncList: SessionRequestList 
		{
            
			private SessionRequestList _collection;
			private object _root;

			internal SyncList(SessionRequestList collection): 
				base(Tag.Default) 
			{

				this._root = collection.SyncRoot;
				this._collection = collection;
			}
            
			#region Public Properties
                
			public override int Capacity 
			{
				get { lock (this._root) return this._collection.Capacity; }                
				set { lock (this._root) this._collection.Capacity = value; }
			}

			public override int Count 
			{
				get { lock (this._root) return this._collection.Count; }
			}

			public override bool IsFixedSize 
			{
				get { return this._collection.IsFixedSize; }
			}

			public override bool IsReadOnly 
			{
				get { return this._collection.IsReadOnly; }
			}

			public override bool IsSynchronized 
			{
				get { return true; }
			}

			public override SessionRequest this[int index] 
			{
				get { lock (this._root) return this._collection[index]; }
				set { lock (this._root) this._collection[index] = value;  }
			}

			public override object SyncRoot 
			{
				get { return this._root; }
			}

			#endregion
			#region Public Methods

			public override int Add(SessionRequest value) 
			{
				lock (this._root) return this._collection.Add(value);
			}
            
			public override void AddRange(SessionRequestList collection) 
			{
				lock (this._root) this._collection.AddRange(collection);
			}

			public override void AddRange(SessionRequest[] array) 
			{
				lock (this._root) this._collection.AddRange(array);
			}

			public override int BinarySearch(SessionRequest value) 
			{
				lock (this._root) return this._collection.BinarySearch(value);
			}

			public override void Clear() 
			{
				lock (this._root) this._collection.Clear();
			}
            
			public override object Clone() 
			{
				lock (this._root) 
					return new SyncList((SessionRequestList) this._collection.Clone());
			}

			public override bool Contains(SessionRequest value) 
			{
				lock (this._root) return this._collection.Contains(value);
			}

			public override void CopyTo(SessionRequest[] array) 
			{
				lock (this._root) this._collection.CopyTo(array);
			}

			public override void CopyTo(SessionRequest[] array, int arrayIndex) 
			{
				lock (this._root) this._collection.CopyTo(array, arrayIndex);
			}
            
			public override ISessionRequestEnumerator GetEnumerator() 
			{
				lock (this._root) return this._collection.GetEnumerator();
			}

			public override int IndexOf(SessionRequest value) 
			{
				lock (this._root) return this._collection.IndexOf(value);
			}

			public override void Insert(int index, SessionRequest value) 
			{
				lock (this._root) this._collection.Insert(index, value);
			}

			public override void Remove(SessionRequest value) 
			{           
				lock (this._root) this._collection.Remove(value);
			}

			public override void RemoveAt(int index) 
			{
				lock (this._root) this._collection.RemoveAt(index);
			}
            
			public override void RemoveRange(int index, int count) 
			{
				lock (this._root) this._collection.RemoveRange(index, count);
			}
            
			public override void Sort() 
			{
				lock (this._root) this._collection.Sort();
			}
            
			public override SessionRequest[] ToArray() 
			{
				lock (this._root) return this._collection.ToArray();
			}

			public override void TrimToSize() 
			{
				lock (this._root) this._collection.TrimToSize();
			}
            
			#endregion
		}
        
		#endregion
	}

	#endregion
}
