
using System;
using System.Collections;
using Ecyware.GreenBlue.FormMapping;

namespace Ecyware.GreenBlue.FormMapping 
{
	#region Class FormMappingDataRelationList

	/// <summary>
	/// Implements a strongly typed collection of <see cref="FormMappingDataRelation"/> elements.
	/// </summary>
	/// <remarks>
	/// <b>FormMappingDataRelationList</b> provides an <see cref="ArrayList"/> 
	/// that is strongly typed for <see cref="FormMappingDataRelation"/> elements.
	/// </remarks>    

	[Serializable]
	public class FormMappingDataRelationList: 
		IFormMappingDataRelationList, IList, ICloneable 
	{
		#region Private Fields
            
		private const int _defaultCapacity = 16;

		private FormMappingDataRelation[] _array = null;
		private int _count = 0;

		[NonSerialized]
		private int _version = 0;
        
		#endregion
		#region Private Constructors
        
		private enum Tag { Default }

		private FormMappingDataRelationList(Tag tag) { }
        
		#endregion
		#region Public Constructors

		/// <overloads>
		/// Initializes a new instance of the <see cref="FormMappingDataRelationList"/> class.
		/// </overloads>
		/// <summary>
		/// Initializes a new instance of the <see cref="FormMappingDataRelationList"/> class
		/// that is empty and has the default initial capacity.
		/// </summary>
		/// <remarks>Please refer to <see cref="ArrayList()"/> for details.</remarks>    

		public FormMappingDataRelationList() 
		{
			this._array = new FormMappingDataRelation[_defaultCapacity];
		}
        
		/// <summary>
		/// Initializes a new instance of the <see cref="FormMappingDataRelationList"/> class
		/// that is empty and has the specified initial capacity.
		/// </summary>
		/// <param name="capacity">The number of elements that the new 
		/// <see cref="FormMappingDataRelationList"/> is initially capable of storing.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <paramref name="capacity"/> is less than zero.</exception>    
		/// <remarks>Please refer to <see cref="ArrayList(Int32)"/> for details.</remarks>    

		public FormMappingDataRelationList(int capacity) 
		{
			if (capacity < 0)
				throw new ArgumentOutOfRangeException("capacity", capacity, "Argument cannot be negative.");

			this._array = new FormMappingDataRelation[capacity];
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FormMappingDataRelationList"/> class
		/// that contains elements copied from the specified collection and
		/// that has the same initial capacity as the number of elements copied.
		/// </summary>
		/// <param name="collection">The <see cref="FormMappingDataRelationList"/> 
		/// whose elements are copied to the new collection.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="collection"/> is a null reference.</exception>        
		/// <remarks>Please refer to <see cref="ArrayList(ICollection)"/> for details.</remarks>    

		public FormMappingDataRelationList(FormMappingDataRelationList collection) 
		{
			if (collection == null)
				throw new ArgumentNullException("collection");

			this._array = new FormMappingDataRelation[collection.Count];
			AddRange(collection);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FormMappingDataRelationList"/> class
		/// that contains elements copied from the specified <see cref="FormMappingDataRelation"/>
		/// array and that has the same initial capacity as the number of elements copied.
		/// </summary>
		/// <param name="array">An <see cref="Array"/> of <see cref="FormMappingDataRelation"/> 
		/// elements that are copied to the new collection.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="array"/> is a null reference.</exception>        
		/// <remarks>Please refer to <see cref="ArrayList(ICollection)"/> for details.</remarks>    

		public FormMappingDataRelationList(FormMappingDataRelation[] array) 
		{
			if (array == null)
				throw new ArgumentNullException("array");

			this._array = new FormMappingDataRelation[array.Length];
			AddRange(array);
		}
        
		#endregion
		#region Public Properties
		#region Capacity

		/// <summary>
		/// Gets or sets the capacity of the <see cref="FormMappingDataRelationList"/>.
		/// </summary>
		/// <value>The number of elements that the 
		/// <see cref="FormMappingDataRelationList"/> can contain.</value>
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
					this._array = new FormMappingDataRelation[_defaultCapacity];
					return;
				}

				FormMappingDataRelation[] newArray = new FormMappingDataRelation[value];
				Array.Copy(this._array, newArray, this._count);
				this._array = newArray;
			}
		}

		#endregion
		#region Count

		/// <summary>
		/// Gets the number of elements contained in the <see cref="FormMappingDataRelationList"/>.
		/// </summary>
		/// <value>
		/// The number of elements contained in the <see cref="FormMappingDataRelationList"/>.
		/// </value>
		/// <remarks>Please refer to <see cref="ArrayList.Count"/> for details.</remarks>

		public virtual int Count 
		{
			get { return this._count; }
		}
        
		#endregion
		#region IsFixedSize

		/// <summary>
		/// Gets a value indicating whether the <see cref="FormMappingDataRelationList"/> has a fixed size.
		/// </summary>
		/// <value><c>true</c> if the <see cref="FormMappingDataRelationList"/> has a fixed size;
		/// otherwise, <c>false</c>. The default is <c>false</c>.</value>
		/// <remarks>Please refer to <see cref="ArrayList.IsFixedSize"/> for details.</remarks>

		public virtual bool IsFixedSize 
		{
			get { return false; }
		}

		#endregion
		#region IsReadOnly

		/// <summary>
		/// Gets a value indicating whether the <see cref="FormMappingDataRelationList"/> is read-only.
		/// </summary>
		/// <value><c>true</c> if the <see cref="FormMappingDataRelationList"/> is read-only;
		/// otherwise, <c>false</c>. The default is <c>false</c>.</value>
		/// <remarks>Please refer to <see cref="ArrayList.IsReadOnly"/> for details.</remarks>

		public virtual bool IsReadOnly 
		{
			get { return false; }
		}

		#endregion
		#region IsSynchronized

		/// <summary>
		/// Gets a value indicating whether access to the <see cref="FormMappingDataRelationList"/> 
		/// is synchronized (thread-safe).
		/// </summary>
		/// <value><c>true</c> if access to the <see cref="FormMappingDataRelationList"/> is 
		/// synchronized (thread-safe); otherwise, <c>false</c>. The default is <c>false</c>.</value>
		/// <remarks>Please refer to <see cref="ArrayList.IsSynchronized"/> for details.</remarks>

		public virtual bool IsSynchronized 
		{
			get { return false; }
		}

		#endregion
		#region Item

		/// <summary>
		/// Gets or sets the <see cref="FormMappingDataRelation"/> element at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the 
		/// <see cref="FormMappingDataRelation"/> element to get or set.</param>
		/// <value>
		/// The <see cref="FormMappingDataRelation"/> element at the specified <paramref name="index"/>.
		/// </value>    
		/// <exception cref="ArgumentOutOfRangeException">
		/// <para><paramref name="index"/> is less than zero.</para>
		/// <para>-or-</para>
		/// <para><paramref name="index"/> is equal to or greater than <see cref="Count"/>.</para>
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// The property is set and the <see cref="FormMappingDataRelationList"/> is read-only.</exception>
		/// <remarks>Please refer to <see cref="ArrayList.this"/> for details.</remarks>

		public virtual FormMappingDataRelation this[int index] 
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
		/// is set, this value must be compatible with <see cref="FormMappingDataRelation"/>.
		/// </value>    
		/// <exception cref="ArgumentOutOfRangeException">
		/// <para><paramref name="index"/> is less than zero.</para>
		/// <para>-or-</para>
		/// <para><paramref name="index"/> is equal to or greater than <see cref="Count"/>.</para>
		/// </exception>
		/// <exception cref="InvalidCastException">The property is set to a value
		/// that is not compatible with <see cref="FormMappingDataRelation"/>.</exception>
		/// <exception cref="NotSupportedException">
		/// The property is set and the <see cref="FormMappingDataRelationList"/> is read-only.</exception>
		/// <remarks>Please refer to <see cref="ArrayList.this"/> for details.</remarks>

		object IList.this[int index] 
		{
			get { return this[index]; }
			set { this[index] = (FormMappingDataRelation) value; }
		}

		#endregion
		#region SyncRoot

		/// <summary>
		/// Gets an object that can be used to synchronize 
		/// access to the <see cref="FormMappingDataRelationList"/>.
		/// </summary>
		/// <value>An object that can be used to synchronize 
		/// access to the <see cref="FormMappingDataRelationList"/>.
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
		/// Adds a <see cref="FormMappingDataRelation"/> to the end of the <see cref="FormMappingDataRelationList"/>.
		/// </summary>
		/// <param name="value">The <see cref="FormMappingDataRelation"/> object 
		/// to be added to the end of the <see cref="FormMappingDataRelationList"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <returns>The <see cref="FormMappingDataRelationList"/> index at which the 
		/// <paramref name="value"/> has been added.</returns>
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="FormMappingDataRelationList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>FormMappingDataRelationList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="ArrayList.Add"/> for details.</remarks>

		public virtual int Add(FormMappingDataRelation value) 
		{
			if (this._count == this._array.Length)
				EnsureCapacity(this._count + 1);

			++this._version;
			this._array[this._count] = value;
			return this._count++;
		}
        
		/// <summary>
		/// Adds an <see cref="Object"/> to the end of the <see cref="FormMappingDataRelationList"/>.
		/// </summary>
		/// <param name="value">
		/// The object to be added to the end of the <see cref="FormMappingDataRelationList"/>.
		/// This argument must be compatible with <see cref="FormMappingDataRelation"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <returns>The <see cref="FormMappingDataRelationList"/> index at which the 
		/// <paramref name="value"/> has been added.</returns>
		/// <exception cref="InvalidCastException"><paramref name="value"/> 
		/// is not compatible with <see cref="FormMappingDataRelation"/>.</exception>    
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="FormMappingDataRelationList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>FormMappingDataRelationList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="ArrayList.Add"/> for details.</remarks>

		int IList.Add(object value) 
		{
			return Add((FormMappingDataRelation) value);
		}

		#endregion
		#region AddRange

		/// <overloads>
		/// Adds a range of elements to the end of the <see cref="FormMappingDataRelationList"/>.
		/// </overloads>
		/// <summary>
		/// Adds the elements of another collection to the end of the <see cref="FormMappingDataRelationList"/>.
		/// </summary>
		/// <param name="collection">The <see cref="FormMappingDataRelationList"/> whose elements 
		/// should be added to the end of the current collection.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="collection"/> is a null reference.</exception>    
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="FormMappingDataRelationList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>FormMappingDataRelationList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="ArrayList.AddRange"/> for details.</remarks>

		public virtual void AddRange(FormMappingDataRelationList collection) 
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
		/// Adds the elements of a <see cref="FormMappingDataRelation"/> array 
		/// to the end of the <see cref="FormMappingDataRelationList"/>.
		/// </summary>
		/// <param name="array">An <see cref="Array"/> of <see cref="FormMappingDataRelation"/> elements
		/// that should be added to the end of the <see cref="FormMappingDataRelationList"/>.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="array"/> is a null reference.</exception>    
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="FormMappingDataRelationList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>FormMappingDataRelationList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="ArrayList.AddRange"/> for details.</remarks>

		public virtual void AddRange(FormMappingDataRelation[] array) 
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
		#region Clear

		/// <summary>
		/// Removes all elements from the <see cref="FormMappingDataRelationList"/>.
		/// </summary>
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="FormMappingDataRelationList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>FormMappingDataRelationList</b> has a fixed size.</para></exception>    
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
		/// Creates a shallow copy of the <see cref="FormMappingDataRelationList"/>.
		/// </summary>
		/// <returns>A shallow copy of the <see cref="FormMappingDataRelationList"/>.</returns>
		/// <remarks>Please refer to <see cref="ArrayList.Clone"/> for details.</remarks>

		public virtual object Clone() 
		{
			FormMappingDataRelationList collection = new FormMappingDataRelationList(this._count);
            
			Array.Copy(this._array, 0, collection._array, 0, this._count);
			collection._count = this._count;
			collection._version = this._version;

			return collection;
		}

		#endregion
		#region Contains

		/// <summary>
		/// Determines whether the <see cref="FormMappingDataRelationList"/>
		/// contains the specified <see cref="FormMappingDataRelation"/> element.
		/// </summary>
		/// <param name="value">The <see cref="FormMappingDataRelation"/> object
		/// to locate in the <see cref="FormMappingDataRelationList"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <returns><c>true</c> if <paramref name="value"/> is found in the 
		/// <see cref="FormMappingDataRelationList"/>; otherwise, <c>false</c>.</returns>
		/// <remarks>Please refer to <see cref="ArrayList.Contains"/> for details.</remarks>

		public virtual bool Contains(FormMappingDataRelation value) 
		{
			return (IndexOf(value) >= 0);
		}

		/// <summary>
		/// Determines whether the <see cref="FormMappingDataRelationList"/> contains the specified element.
		/// </summary>
		/// <param name="value">The object to locate in the <see cref="FormMappingDataRelationList"/>.
		/// This argument must be compatible with <see cref="FormMappingDataRelation"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <returns><c>true</c> if <paramref name="value"/> is found in the 
		/// <see cref="FormMappingDataRelationList"/>; otherwise, <c>false</c>.</returns>
		/// <exception cref="InvalidCastException"><paramref name="value"/> 
		/// is not compatible with <see cref="FormMappingDataRelation"/>.</exception>    
		/// <remarks>Please refer to <see cref="ArrayList.Contains"/> for details.</remarks>

		bool IList.Contains(object value) 
		{
			return Contains((FormMappingDataRelation) value);
		}

		#endregion
		#region CopyTo

		/// <overloads>
		/// Copies the <see cref="FormMappingDataRelationList"/> or a portion of it to a one-dimensional array.
		/// </overloads>
		/// <summary>
		/// Copies the entire <see cref="FormMappingDataRelationList"/> to a one-dimensional <see cref="Array"/>
		/// of <see cref="FormMappingDataRelation"/> elements, starting at the beginning of the target array.
		/// </summary>
		/// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the
		/// <see cref="FormMappingDataRelation"/> elements copied from the <see cref="FormMappingDataRelationList"/>.
		/// The <b>Array</b> must have zero-based indexing.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="array"/> is a null reference.</exception>    
		/// <exception cref="ArgumentException">
		/// The number of elements in the source <see cref="FormMappingDataRelationList"/> is greater 
		/// than the available space in the destination <paramref name="array"/>.</exception>
		/// <remarks>Please refer to <see cref="ArrayList.CopyTo"/> for details.</remarks>

		public virtual void CopyTo(FormMappingDataRelation[] array) 
		{
			CheckTargetArray(array, 0);
			Array.Copy(this._array, array, this._count); 
		}
        
		/// <summary>
		/// Copies the entire <see cref="FormMappingDataRelationList"/> to a one-dimensional <see cref="Array"/>
		/// of <see cref="FormMappingDataRelation"/> elements, starting at the specified index of the target array.
		/// </summary>
		/// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the
		/// <see cref="FormMappingDataRelation"/> elements copied from the <see cref="FormMappingDataRelationList"/>.
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
		/// The number of elements in the source <see cref="FormMappingDataRelationList"/> is greater than the
		/// available space from <paramref name="arrayIndex"/> to the end of the destination 
		/// <paramref name="array"/>.</para></exception>
		/// <remarks>Please refer to <see cref="ArrayList.CopyTo"/> for details.</remarks>

		public virtual void CopyTo(FormMappingDataRelation[] array, int arrayIndex) 
		{
			CheckTargetArray(array, arrayIndex);
			Array.Copy(this._array, 0, array, arrayIndex, this._count); 
		}

		/// <summary>
		/// Copies the entire <see cref="FormMappingDataRelationList"/> to a one-dimensional <see cref="Array"/>,
		/// starting at the specified index of the target array.
		/// </summary>
		/// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the
		/// <see cref="FormMappingDataRelation"/> elements copied from the <see cref="FormMappingDataRelationList"/>.
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
		/// The number of elements in the source <see cref="FormMappingDataRelationList"/> is greater than the
		/// available space from <paramref name="arrayIndex"/> to the end of the destination 
		/// <paramref name="array"/>.</para></exception>
		/// <exception cref="InvalidCastException">
		/// The <see cref="FormMappingDataRelation"/> type cannot be cast automatically 
		/// to the type of the destination <paramref name="array"/>.</exception>    
		/// <remarks>Please refer to <see cref="ArrayList.CopyTo"/> for details.</remarks>

		void ICollection.CopyTo(Array array, int arrayIndex) 
		{
			CheckTargetArray(array, arrayIndex);
			CopyTo((FormMappingDataRelation[]) array, arrayIndex);
		}

		#endregion
		#region GetEnumerator

		/// <summary>
		/// Returns an <see cref="IFormMappingDataRelationEnumerator"/> that can
		/// iterate through the <see cref="FormMappingDataRelationList"/>.
		/// </summary>
		/// <returns>An <see cref="IFormMappingDataRelationEnumerator"/> 
		/// for the entire <see cref="FormMappingDataRelationList"/>.</returns>
		/// <remarks>Please refer to <see cref="ArrayList.GetEnumerator"/> for details.</remarks>

		public virtual IFormMappingDataRelationEnumerator GetEnumerator() 
		{
			return new Enumerator(this);
		}

		/// <summary>
		/// Returns an <see cref="IEnumerator"/> that can
		/// iterate through the <see cref="FormMappingDataRelationList"/>.
		/// </summary>
		/// <returns>An <see cref="IEnumerator"/>
		/// for the entire <see cref="FormMappingDataRelationList"/>.</returns>
		/// <remarks>Please refer to <see cref="ArrayList.GetEnumerator"/> for details.</remarks>

		IEnumerator IEnumerable.GetEnumerator() 
		{
			return (IEnumerator) GetEnumerator();
		}

		#endregion
		#region IndexOf

		/// <summary>
		/// Returns the zero-based index of the first occurrence of the specified 
		/// <see cref="FormMappingDataRelation"/> in the <see cref="FormMappingDataRelationList"/>.
		/// </summary>
		/// <param name="value">The <see cref="FormMappingDataRelation"/> object 
		/// to locate in the <see cref="FormMappingDataRelationList"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <returns>
		/// The zero-based index of the first occurrence of <paramref name="value"/> 
		/// in the <see cref="FormMappingDataRelationList"/>, if found; otherwise, -1.
		/// </returns>
		/// <remarks>Please refer to <see cref="ArrayList.IndexOf"/> for details.</remarks>

		public virtual int IndexOf(FormMappingDataRelation value) 
		{

			if ((object) value == null) 
			{
				for (int i = 0; i < this._count; i++)
					if ((object) this._array[i] == null)
						return i;

				return -1;
			}
        
			for (int i = 0; i < this._count; i++)
				if (value.Equals(this._array[i]))
					return i;

			return -1;
		}

		/// <summary>
		/// Returns the zero-based index of the first occurrence of the specified 
		/// <see cref="Object"/> in the <see cref="FormMappingDataRelationList"/>.
		/// </summary>
		/// <param name="value">The object to locate in the <see cref="FormMappingDataRelationList"/>.
		/// This argument must be compatible with <see cref="FormMappingDataRelation"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <returns>
		/// The zero-based index of the first occurrence of <paramref name="value"/> 
		/// in the <see cref="FormMappingDataRelationList"/>, if found; otherwise, -1.
		/// </returns>
		/// <exception cref="InvalidCastException"><paramref name="value"/>
		/// is not compatible with <see cref="FormMappingDataRelation"/>.</exception>    
		/// <remarks>Please refer to <see cref="ArrayList.IndexOf"/> for details.</remarks>

		int IList.IndexOf(object value) 
		{
			return IndexOf((FormMappingDataRelation) value);
		}

		#endregion
		#region Insert

		/// <summary>
		/// Inserts a <see cref="FormMappingDataRelation"/> element into the 
		/// <see cref="FormMappingDataRelationList"/> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which <paramref name="value"/> 
		/// should be inserted.</param>
		/// <param name="value">The <see cref="FormMappingDataRelation"/> object
		/// to insert into the <see cref="FormMappingDataRelationList"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <exception cref="ArgumentOutOfRangeException">
		/// <para><paramref name="index"/> is less than zero.</para>
		/// <para>-or-</para>
		/// <para><paramref name="index"/> is greater than <see cref="Count"/>.</para>
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="FormMappingDataRelationList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>FormMappingDataRelationList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="ArrayList.Insert"/> for details.</remarks>

		public virtual void Insert(int index, FormMappingDataRelation value) 
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
		/// Inserts an element into the <see cref="FormMappingDataRelationList"/> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which <paramref name="value"/> 
		/// should be inserted.</param>
		/// <param name="value">The object to insert into the <see cref="FormMappingDataRelationList"/>.
		/// This argument must be compatible with <see cref="FormMappingDataRelation"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <exception cref="ArgumentOutOfRangeException">
		/// <para><paramref name="index"/> is less than zero.</para>
		/// <para>-or-</para>
		/// <para><paramref name="index"/> is greater than <see cref="Count"/>.</para>
		/// </exception>
		/// <exception cref="InvalidCastException"><paramref name="value"/>
		/// is not compatible with <see cref="FormMappingDataRelation"/>.</exception>    
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="FormMappingDataRelationList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>FormMappingDataRelationList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="ArrayList.Insert"/> for details.</remarks>

		void IList.Insert(int index, object value) 
		{
			Insert(index, (FormMappingDataRelation) value);
		}

		#endregion
		#region ReadOnly

		/// <summary>
		/// Returns a read-only wrapper for the specified <see cref="FormMappingDataRelationList"/>.
		/// </summary>
		/// <param name="collection">The <see cref="FormMappingDataRelationList"/> to wrap.</param>    
		/// <returns>A read-only wrapper around <paramref name="collection"/>.</returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="collection"/> is a null reference.</exception>    
		/// <remarks>Please refer to <see cref="ArrayList.ReadOnly"/> for details.</remarks>

		public static FormMappingDataRelationList ReadOnly(FormMappingDataRelationList collection) 
		{
			if (collection == null)
				throw new ArgumentNullException("collection");

			return new ReadOnlyList(collection);
		}

		#endregion
		#region Remove

		/// <summary>
		/// Removes the first occurrence of the specified <see cref="FormMappingDataRelation"/>
		/// from the <see cref="FormMappingDataRelationList"/>.
		/// </summary>
		/// <param name="value">The <see cref="FormMappingDataRelation"/> object
		/// to remove from the <see cref="FormMappingDataRelationList"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="FormMappingDataRelationList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>FormMappingDataRelationList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="ArrayList.Remove"/> for details.</remarks>

		public virtual void Remove(FormMappingDataRelation value) 
		{
			int index = IndexOf(value);
			if (index >= 0) RemoveAt(index);
		}

		/// <summary>
		/// Removes the first occurrence of the specified <see cref="Object"/>
		/// from the <see cref="FormMappingDataRelationList"/>.
		/// </summary>
		/// <param name="value">The object to remove from the <see cref="FormMappingDataRelationList"/>.
		/// This argument must be compatible with <see cref="FormMappingDataRelation"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <exception cref="InvalidCastException"><paramref name="value"/>
		/// is not compatible with <see cref="FormMappingDataRelation"/>.</exception>    
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="FormMappingDataRelationList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>FormMappingDataRelationList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="ArrayList.Remove"/> for details.</remarks>

		void IList.Remove(object value) 
		{
			Remove((FormMappingDataRelation) value);
		}

		#endregion
		#region RemoveAt

		/// <summary>
		/// Removes the element at the specified index of the <see cref="FormMappingDataRelationList"/>.
		/// </summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <para><paramref name="index"/> is less than zero.</para>
		/// <para>-or-</para>
		/// <para><paramref name="index"/> is equal to or greater than <see cref="Count"/>.</para>
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="FormMappingDataRelationList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>FormMappingDataRelationList</b> has a fixed size.</para></exception>    
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
		/// Removes a range of elements from the <see cref="FormMappingDataRelationList"/>.
		/// </summary>
		/// <param name="index">The zero-based starting index of the range
		/// of elements to remove.</param>
		/// <param name="count">The number of elements to remove.</param>
		/// <exception cref="ArgumentException">
		/// <paramref name="index"/> and <paramref name="count"/> do not denote a
		/// valid range of elements in the <see cref="FormMappingDataRelationList"/>.</exception>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <para><paramref name="index"/> is less than zero.</para>
		/// <para>-or-</para>
		/// <para><paramref name="count"/> is less than zero.</para>
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="FormMappingDataRelationList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>FormMappingDataRelationList</b> has a fixed size.</para></exception>    
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
		/// Sorts the elements in the entire <see cref="FormMappingDataRelationList"/>
		/// using the <see cref="IComparable"/> implementation of each element.
		/// </summary>
		/// <exception cref="NotSupportedException">
		/// The <see cref="FormMappingDataRelationList"/> is read-only.</exception>
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
		/// for the specified <see cref="FormMappingDataRelationList"/>.
		/// </summary>
		/// <param name="collection">The <see cref="FormMappingDataRelationList"/> to synchronize.</param>    
		/// <returns>
		/// A synchronized (thread-safe) wrapper around <paramref name="collection"/>.
		/// </returns>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="collection"/> is a null reference.</exception>    
		/// <remarks>Please refer to <see cref="ArrayList.Synchronized"/> for details.</remarks>

		public static FormMappingDataRelationList Synchronized(FormMappingDataRelationList collection) 
		{
			if (collection == null)
				throw new ArgumentNullException("collection");

			return new SyncList(collection);
		}

		#endregion
		#region ToArray

		/// <summary>
		/// Copies the elements of the <see cref="FormMappingDataRelationList"/> to a new
		/// <see cref="Array"/> of <see cref="FormMappingDataRelation"/> elements.
		/// </summary>
		/// <returns>A one-dimensional <see cref="Array"/> of <see cref="FormMappingDataRelation"/> 
		/// elements containing copies of the elements of the <see cref="FormMappingDataRelationList"/>.</returns>
		/// <remarks>Please refer to <see cref="ArrayList.ToArray"/> for details.</remarks>

		public virtual FormMappingDataRelation[] ToArray() 
		{
			FormMappingDataRelation[] array = new FormMappingDataRelation[this._count];
			Array.Copy(this._array, array, this._count);
			return array;
		}
        
		#endregion
		#region TrimToSize

		/// <summary>
		/// Sets the capacity to the actual number of elements in the <see cref="FormMappingDataRelationList"/>.
		/// </summary>
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="FormMappingDataRelationList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>FormMappingDataRelationList</b> has a fixed size.</para></exception>    
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
			IFormMappingDataRelationEnumerator, IEnumerator 
		{
            
			private readonly FormMappingDataRelationList _collection;
			private readonly int _version;
			private int _index;
            
			internal Enumerator(FormMappingDataRelationList collection) 
			{
				this._collection = collection;
				this._version = collection._version;
				this._index = -1;
			}
            
			public FormMappingDataRelation Current 
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
			private sealed class ReadOnlyList: FormMappingDataRelationList 
		{
            
			private FormMappingDataRelationList _collection;

			internal ReadOnlyList(FormMappingDataRelationList collection): 
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

			public override FormMappingDataRelation this[int index] 
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

			public override int Add(FormMappingDataRelation value) 
			{
				throw new NotSupportedException("Read-only collections cannot be modified.");
			}
            
			public override void AddRange(FormMappingDataRelationList collection) 
			{
				throw new NotSupportedException("Read-only collections cannot be modified.");
			}

			public override void AddRange(FormMappingDataRelation[] array) 
			{
				throw new NotSupportedException("Read-only collections cannot be modified.");
			}           

			public override void Clear() 
			{
				throw new NotSupportedException("Read-only collections cannot be modified.");
			}

			public override object Clone() 
			{
				return new ReadOnlyList((FormMappingDataRelationList) this._collection.Clone());
			}

			public override bool Contains(FormMappingDataRelation value) 
			{
				return this._collection.Contains(value);
			}

			public override void CopyTo(FormMappingDataRelation[] array) 
			{
				this._collection.CopyTo(array);
			}

			public override void CopyTo(FormMappingDataRelation[] array, int arrayIndex) 
			{
				this._collection.CopyTo(array, arrayIndex);
			}
            
			public override IFormMappingDataRelationEnumerator GetEnumerator() 
			{
				return this._collection.GetEnumerator();
			}

			public override int IndexOf(FormMappingDataRelation value) 
			{
				return this._collection.IndexOf(value);
			}

			public override void Insert(int index, FormMappingDataRelation value) 
			{
				throw new NotSupportedException("Read-only collections cannot be modified.");
			}

			public override void Remove(FormMappingDataRelation value) 
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
            
			public override FormMappingDataRelation[] ToArray() 
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
			private sealed class SyncList: FormMappingDataRelationList 
		{
            
			private FormMappingDataRelationList _collection;
			private object _root;

			internal SyncList(FormMappingDataRelationList collection): 
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

			public override FormMappingDataRelation this[int index] 
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

			public override int Add(FormMappingDataRelation value) 
			{
				lock (this._root) return this._collection.Add(value);
			}
            
			public override void AddRange(FormMappingDataRelationList collection) 
			{
				lock (this._root) this._collection.AddRange(collection);
			}

			public override void AddRange(FormMappingDataRelation[] array) 
			{
				lock (this._root) this._collection.AddRange(array);
			}

			public override void Clear() 
			{
				lock (this._root) this._collection.Clear();
			}
            
			public override object Clone() 
			{
				lock (this._root) 
					return new SyncList((FormMappingDataRelationList) this._collection.Clone());
			}

			public override bool Contains(FormMappingDataRelation value) 
			{
				lock (this._root) return this._collection.Contains(value);
			}

			public override void CopyTo(FormMappingDataRelation[] array) 
			{
				lock (this._root) this._collection.CopyTo(array);
			}

			public override void CopyTo(FormMappingDataRelation[] array, int arrayIndex) 
			{
				lock (this._root) this._collection.CopyTo(array, arrayIndex);
			}
            
			public override IFormMappingDataRelationEnumerator GetEnumerator() 
			{
				lock (this._root) return this._collection.GetEnumerator();
			}

			public override int IndexOf(FormMappingDataRelation value) 
			{
				lock (this._root) return this._collection.IndexOf(value);
			}

			public override void Insert(int index, FormMappingDataRelation value) 
			{
				lock (this._root) this._collection.Insert(index, value);
			}

			public override void Remove(FormMappingDataRelation value) 
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
            
			public override FormMappingDataRelation[] ToArray() 
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
