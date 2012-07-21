// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003
using System;
using System.Collections;
using Ecyware.GreenBlue.Engine.HtmlDom ;

namespace Ecyware.GreenBlue.Engine.HtmlDom  
{
	#region Interface IHtmlTagBaseCollection

	/// <summary>
	/// Defines size, enumerators, and synchronization methods for strongly
	/// typed collections of <see cref="HtmlTagBase"/> elements.
	/// </summary>
	/// <remarks>
	/// <b>IHtmlTagBaseCollection</b> provides an <see cref="ICollection"/> 
	/// that is strongly typed for <see cref="HtmlTagBase"/> elements.
	/// </remarks>    

	public interface IHtmlTagBaseCollection 
	{
		#region Properties
		#region Count

		/// <summary>
		/// Gets the number of elements contained in the 
		/// <see cref="IHtmlTagBaseCollection"/>.
		/// </summary>
		/// <value>The number of elements contained in the 
		/// <see cref="IHtmlTagBaseCollection"/>.</value>
		/// <remarks>Please refer to <see cref="ICollection.Count"/> for details.</remarks>

		int Count { get; }
        
		#endregion
		#region IsSynchronized
        
		/// <summary>
		/// Gets a value indicating whether access to the 
		/// <see cref="IHtmlTagBaseCollection"/> is synchronized (thread-safe).
		/// </summary>
		/// <value><c>true</c> if access to the <see cref="IHtmlTagBaseCollection"/> is 
		/// synchronized (thread-safe); otherwise, <c>false</c>. The default is <c>false</c>.</value>
		/// <remarks>Please refer to <see cref="ICollection.IsSynchronized"/> for details.</remarks>

		bool IsSynchronized { get; }
        
		#endregion
		#region SyncRoot

		/// <summary>
		/// Gets an object that can be used to synchronize access 
		/// to the <see cref="IHtmlTagBaseCollection"/>.
		/// </summary>
		/// <value>An object that can be used to synchronize access 
		/// to the <see cref="IHtmlTagBaseCollection"/>.</value>
		/// <remarks>Please refer to <see cref="ICollection.SyncRoot"/> for details.</remarks>

		object SyncRoot { get; }

		#endregion
		#endregion
		#region Methods
		#region CopyTo

		/// <summary>
		/// Copies the entire <see cref="IHtmlTagBaseCollection"/> to a one-dimensional <see cref="Array"/>
		/// of <see cref="HtmlTagBase"/> elements, starting at the specified index of the target array.
		/// </summary>
		/// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the
		/// <see cref="HtmlTagBase"/> elements copied from the <see cref="IHtmlTagBaseCollection"/>. 
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
		/// The number of elements in the source <see cref="IHtmlTagBaseCollection"/> is greater 
		/// than the available space from <paramref name="arrayIndex"/> to the end of the destination 
		/// <paramref name="array"/>.</para></exception>
		/// <remarks>Please refer to <see cref="ICollection.CopyTo"/> for details.</remarks>

		void CopyTo(HtmlTagBase[] array, int arrayIndex);
        
		#endregion
		#region GetEnumerator

		/// <summary>
		/// Returns an <see cref="IHtmlTagBaseEnumerator"/> that can
		/// iterate through the <see cref="IHtmlTagBaseCollection"/>.
		/// </summary>
		/// <returns>An <see cref="IHtmlTagBaseEnumerator"/> 
		/// for the entire <see cref="IHtmlTagBaseCollection"/>.</returns>
		/// <remarks>Please refer to <see cref="IEnumerable.GetEnumerator"/> for details.</remarks>

		IHtmlTagBaseEnumerator GetEnumerator();

		#endregion
		#endregion
	}
    
	#endregion
	#region Interface IHtmlTagBaseList

	/// <summary>
	/// Represents a strongly typed collection of <see cref="HtmlTagBase"/> 
	/// objects that can be individually accessed by index.
	/// </summary>
	/// <remarks>
	/// <b>IHtmlTagBaseList</b> provides an <see cref="IList"/>
	/// that is strongly typed for <see cref="HtmlTagBase"/> elements.
	/// </remarks>    

	public interface 
		IHtmlTagBaseList: IHtmlTagBaseCollection 
	{
		#region Properties
		#region IsFixedSize

		/// <summary>
		/// Gets a value indicating whether the <see cref="IHtmlTagBaseList"/> has a fixed size.
		/// </summary>
		/// <value><c>true</c> if the <see cref="IHtmlTagBaseList"/> has a fixed size;
		/// otherwise, <c>false</c>. The default is <c>false</c>.</value>
		/// <remarks>Please refer to <see cref="IList.IsFixedSize"/> for details.</remarks>

		bool IsFixedSize { get; }
        
		#endregion
		#region IsReadOnly

		/// <summary>
		/// Gets a value indicating whether the <see cref="IHtmlTagBaseList"/> is read-only.
		/// </summary>
		/// <value><c>true</c> if the <see cref="IHtmlTagBaseList"/> is read-only;
		/// otherwise, <c>false</c>. The default is <c>false</c>.</value>
		/// <remarks>Please refer to <see cref="IList.IsReadOnly"/> for details.</remarks>

		bool IsReadOnly { get; }
        
		#endregion
		#region Item

		/// <summary>
		/// Gets or sets the <see cref="HtmlTagBase"/> element at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the 
		/// <see cref="HtmlTagBase"/> element to get or set.</param>
		/// <value>
		/// The <see cref="HtmlTagBase"/> element at the specified <paramref name="index"/>.
		/// </value>    
		/// <exception cref="ArgumentOutOfRangeException">
		/// <para><paramref name="index"/> is less than zero.</para>
		/// <para>-or-</para>
		/// <para><paramref name="index"/> is equal to or greater than 
		/// <see cref="IHtmlTagBaseCollection.Count"/>.</para>
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// The property is set and the <see cref="IHtmlTagBaseList"/> is read-only.</exception>
		/// <remarks>Please refer to <see cref="IList.this"/> for details.</remarks>

		HtmlTagBase this[int index] { get; set; }

		#endregion
		#endregion
		#region Methods
		#region Add

		/// <summary>
		/// Adds a <see cref="HtmlTagBase"/> to the end 
		/// of the <see cref="IHtmlTagBaseList"/>.
		/// </summary>
		/// <param name="value">The <see cref="HtmlTagBase"/> object 
		/// to be added to the end of the <see cref="IHtmlTagBaseList"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <returns>The <see cref="IHtmlTagBaseList"/> index at which
		/// the <paramref name="value"/> has been added.</returns>
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="IHtmlTagBaseList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>IHtmlTagBaseList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="IList.Add"/> for details.</remarks>

		int Add(HtmlTagBase value);
        
		#endregion
		#region Clear
        
		/// <summary>
		/// Removes all elements from the <see cref="IHtmlTagBaseList"/>.
		/// </summary>
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="IHtmlTagBaseList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>IHtmlTagBaseList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="IList.Clear"/> for details.</remarks>

		void Clear();
        
		#endregion
		#region Contains
        
		/// <summary>
		/// Determines whether the <see cref="IHtmlTagBaseList"/>
		/// contains the specified <see cref="HtmlTagBase"/> element.
		/// </summary>
		/// <param name="value">The <see cref="HtmlTagBase"/> object
		/// to locate in the <see cref="IHtmlTagBaseList"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <returns><c>true</c> if <paramref name="value"/> is found in the 
		/// <see cref="IHtmlTagBaseList"/>; otherwise, <c>false</c>.</returns>
		/// <remarks>Please refer to <see cref="IList.Contains"/> for details.</remarks>

		bool Contains(HtmlTagBase value);
        
		#endregion
		#region IndexOf

		/// <summary>
		/// Returns the zero-based index of the first occurrence of the specified 
		/// <see cref="HtmlTagBase"/> in the <see cref="IHtmlTagBaseList"/>.
		/// </summary>
		/// <param name="value">The <see cref="HtmlTagBase"/> object 
		/// to locate in the <see cref="IHtmlTagBaseList"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <returns>
		/// The zero-based index of the first occurrence of <paramref name="value"/> 
		/// in the <see cref="IHtmlTagBaseList"/>, if found; otherwise, -1.
		/// </returns>
		/// <remarks>Please refer to <see cref="IList.IndexOf"/> for details.</remarks>

		int IndexOf(HtmlTagBase value);
        
		#endregion
		#region Insert

		/// <summary>
		/// Inserts a <see cref="HtmlTagBase"/> element into the 
		/// <see cref="IHtmlTagBaseList"/> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which 
		/// <paramref name="value"/> should be inserted.</param>
		/// <param name="value">The <see cref="HtmlTagBase"/> object
		/// to insert into the <see cref="IHtmlTagBaseList"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <exception cref="ArgumentOutOfRangeException">
		/// <para><paramref name="index"/> is less than zero.</para>
		/// <para>-or-</para>
		/// <para><paramref name="index"/> is greater than 
		/// <see cref="IHtmlTagBaseCollection.Count"/>.</para>
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="IHtmlTagBaseList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>IHtmlTagBaseList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="IList.Insert"/> for details.</remarks>

		void Insert(int index, HtmlTagBase value);
        
		#endregion
		#region Remove

		/// <summary>
		/// Removes the first occurrence of the specified <see cref="HtmlTagBase"/>
		/// from the <see cref="IHtmlTagBaseList"/>.
		/// </summary>
		/// <param name="value">The <see cref="HtmlTagBase"/> object
		/// to remove from the <see cref="IHtmlTagBaseList"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="IHtmlTagBaseList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>IHtmlTagBaseList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="IList.Remove"/> for details.</remarks>

		void Remove(HtmlTagBase value);
        
		#endregion
		#region RemoveAt

		/// <summary>
		/// Removes the element at the specified index of the 
		/// <see cref="IHtmlTagBaseList"/>.
		/// </summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <para><paramref name="index"/> is less than zero.</para>
		/// <para>-or-</para>
		/// <para><paramref name="index"/> is equal to or greater than 
		/// <see cref="IHtmlTagBaseCollection.Count"/>.</para>
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="IHtmlTagBaseList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>IHtmlTagBaseList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="IList.RemoveAt"/> for details.</remarks>

		void RemoveAt(int index);

		#endregion
		#endregion
	}
    
	#endregion
	#region Interface IHtmlTagBaseEnumerator

	/// <summary>
	/// Supports type-safe iteration over a collection that 
	/// contains <see cref="HtmlTagBase"/> elements.
	/// </summary>
	/// <remarks>
	/// <b>IHtmlTagBaseEnumerator</b> provides an <see cref="IEnumerator"/> 
	/// that is strongly typed for <see cref="HtmlTagBase"/> elements.
	/// </remarks>    
        
	public interface IHtmlTagBaseEnumerator 
	{
		#region Properties
		#region Current

		/// <summary>
		/// Gets the current <see cref="HtmlTagBase"/> element in the collection.
		/// </summary>
		/// <value>The current <see cref="HtmlTagBase"/> element in the collection.</value>
		/// <exception cref="InvalidOperationException">The enumerator is positioned 
		/// before the first element of the collection or after the last element.</exception>    
		/// <remarks>Please refer to <see cref="IEnumerator.Current"/> for details.</remarks>    

		HtmlTagBase Current { get; }
        
		#endregion
		#endregion
		#region Methods
		#region MoveNext

		/// <summary>
		/// Advances the enumerator to the next element of the collection.
		/// </summary>
		/// <returns><c>true</c> if the enumerator was successfully advanced to the next element; 
		/// <c>false</c> if the enumerator has passed the end of the collection.</returns>
		/// <exception cref="InvalidOperationException">
		/// The collection was modified after the enumerator was created.</exception>
		/// <remarks>Please refer to <see cref="IEnumerator.MoveNext"/> for details.</remarks>    

		bool MoveNext();
        
		#endregion
		#region Reset

		/// <summary>
		/// Sets the enumerator to its initial position, 
		/// which is before the first element in the collection.
		/// </summary>
		/// <exception cref="InvalidOperationException">
		/// The collection was modified after the enumerator was created.</exception>
		/// <remarks>Please refer to <see cref="IEnumerator.Reset"/> for details.</remarks>    

		void Reset();
        
		#endregion
		#endregion
	}

	#endregion
}
