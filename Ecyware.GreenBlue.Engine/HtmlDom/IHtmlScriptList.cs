// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003
using System;
using System.Collections;
using Ecyware.GreenBlue.Engine.HtmlDom ;

namespace Ecyware.GreenBlue.Engine.HtmlDom  
{
	#region Interface IHtmlScriptCollection

	/// <summary>
	/// Defines size, enumerators, and synchronization methods for strongly
	/// typed collections of <see cref="HtmlScript"/> elements.
	/// </summary>
	/// <remarks>
	/// <b>IHtmlScriptCollection</b> provides an <see cref="ICollection"/> 
	/// that is strongly typed for <see cref="HtmlScript"/> elements.
	/// </remarks>    

	public interface IHtmlScriptCollection 
	{
		#region Properties
		#region Count

		/// <summary>
		/// Gets the number of elements contained in the 
		/// <see cref="IHtmlScriptCollection"/>.
		/// </summary>
		/// <value>The number of elements contained in the 
		/// <see cref="IHtmlScriptCollection"/>.</value>
		/// <remarks>Please refer to <see cref="ICollection.Count"/> for details.</remarks>

		int Count { get; }
        
		#endregion
		#region IsSynchronized
        
		/// <summary>
		/// Gets a value indicating whether access to the 
		/// <see cref="IHtmlScriptCollection"/> is synchronized (thread-safe).
		/// </summary>
		/// <value><c>true</c> if access to the <see cref="IHtmlScriptCollection"/> is 
		/// synchronized (thread-safe); otherwise, <c>false</c>. The default is <c>false</c>.</value>
		/// <remarks>Please refer to <see cref="ICollection.IsSynchronized"/> for details.</remarks>

		bool IsSynchronized { get; }
        
		#endregion
		#region SyncRoot

		/// <summary>
		/// Gets an object that can be used to synchronize access 
		/// to the <see cref="IHtmlScriptCollection"/>.
		/// </summary>
		/// <value>An object that can be used to synchronize access 
		/// to the <see cref="IHtmlScriptCollection"/>.</value>
		/// <remarks>Please refer to <see cref="ICollection.SyncRoot"/> for details.</remarks>

		object SyncRoot { get; }

		#endregion
		#endregion
		#region Methods
		#region CopyTo

		/// <summary>
		/// Copies the entire <see cref="IHtmlScriptCollection"/> to a one-dimensional <see cref="Array"/>
		/// of <see cref="HtmlScript"/> elements, starting at the specified index of the target array.
		/// </summary>
		/// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the
		/// <see cref="HtmlScript"/> elements copied from the <see cref="IHtmlScriptCollection"/>. 
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
		/// The number of elements in the source <see cref="IHtmlScriptCollection"/> is greater 
		/// than the available space from <paramref name="arrayIndex"/> to the end of the destination 
		/// <paramref name="array"/>.</para></exception>
		/// <remarks>Please refer to <see cref="ICollection.CopyTo"/> for details.</remarks>

		void CopyTo(HtmlScript[] array, int arrayIndex);
        
		#endregion
		#region GetEnumerator

		/// <summary>
		/// Returns an <see cref="IHtmlScriptEnumerator"/> that can
		/// iterate through the <see cref="IHtmlScriptCollection"/>.
		/// </summary>
		/// <returns>An <see cref="IHtmlScriptEnumerator"/> 
		/// for the entire <see cref="IHtmlScriptCollection"/>.</returns>
		/// <remarks>Please refer to <see cref="IEnumerable.GetEnumerator"/> for details.</remarks>

		IHtmlScriptEnumerator GetEnumerator();

		#endregion
		#endregion
	}
    
	#endregion
	#region Interface IHtmlScriptList

	/// <summary>
	/// Represents a strongly typed collection of <see cref="HtmlScript"/> 
	/// objects that can be individually accessed by index.
	/// </summary>
	/// <remarks>
	/// <b>IHtmlScriptList</b> provides an <see cref="IList"/>
	/// that is strongly typed for <see cref="HtmlScript"/> elements.
	/// </remarks>    

	public interface 
		IHtmlScriptList: IHtmlScriptCollection 
	{
		#region Properties
		#region IsFixedSize

		/// <summary>
		/// Gets a value indicating whether the <see cref="IHtmlScriptList"/> has a fixed size.
		/// </summary>
		/// <value><c>true</c> if the <see cref="IHtmlScriptList"/> has a fixed size;
		/// otherwise, <c>false</c>. The default is <c>false</c>.</value>
		/// <remarks>Please refer to <see cref="IList.IsFixedSize"/> for details.</remarks>

		bool IsFixedSize { get; }
        
		#endregion
		#region IsReadOnly

		/// <summary>
		/// Gets a value indicating whether the <see cref="IHtmlScriptList"/> is read-only.
		/// </summary>
		/// <value><c>true</c> if the <see cref="IHtmlScriptList"/> is read-only;
		/// otherwise, <c>false</c>. The default is <c>false</c>.</value>
		/// <remarks>Please refer to <see cref="IList.IsReadOnly"/> for details.</remarks>

		bool IsReadOnly { get; }
        
		#endregion
		#region Item

		/// <summary>
		/// Gets or sets the <see cref="HtmlScript"/> element at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index of the 
		/// <see cref="HtmlScript"/> element to get or set.</param>
		/// <value>
		/// The <see cref="HtmlScript"/> element at the specified <paramref name="index"/>.
		/// </value>    
		/// <exception cref="ArgumentOutOfRangeException">
		/// <para><paramref name="index"/> is less than zero.</para>
		/// <para>-or-</para>
		/// <para><paramref name="index"/> is equal to or greater than 
		/// <see cref="IHtmlScriptCollection.Count"/>.</para>
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// The property is set and the <see cref="IHtmlScriptList"/> is read-only.</exception>
		/// <remarks>Please refer to <see cref="IList.this"/> for details.</remarks>

		HtmlScript this[int index] { get; set; }

		#endregion
		#endregion
		#region Methods
		#region Add

		/// <summary>
		/// Adds a <see cref="HtmlScript"/> to the end 
		/// of the <see cref="IHtmlScriptList"/>.
		/// </summary>
		/// <param name="value">The <see cref="HtmlScript"/> object 
		/// to be added to the end of the <see cref="IHtmlScriptList"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <returns>The <see cref="IHtmlScriptList"/> index at which
		/// the <paramref name="value"/> has been added.</returns>
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="IHtmlScriptList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>IHtmlScriptList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="IList.Add"/> for details.</remarks>

		int Add(HtmlScript value);
        
		#endregion
		#region Clear
        
		/// <summary>
		/// Removes all elements from the <see cref="IHtmlScriptList"/>.
		/// </summary>
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="IHtmlScriptList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>IHtmlScriptList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="IList.Clear"/> for details.</remarks>

		void Clear();
        
		#endregion
		#region Contains
        
		/// <summary>
		/// Determines whether the <see cref="IHtmlScriptList"/>
		/// contains the specified <see cref="HtmlScript"/> element.
		/// </summary>
		/// <param name="value">The <see cref="HtmlScript"/> object
		/// to locate in the <see cref="IHtmlScriptList"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <returns><c>true</c> if <paramref name="value"/> is found in the 
		/// <see cref="IHtmlScriptList"/>; otherwise, <c>false</c>.</returns>
		/// <remarks>Please refer to <see cref="IList.Contains"/> for details.</remarks>

		bool Contains(HtmlScript value);
        
		#endregion
		#region IndexOf

		/// <summary>
		/// Returns the zero-based index of the first occurrence of the specified 
		/// <see cref="HtmlScript"/> in the <see cref="IHtmlScriptList"/>.
		/// </summary>
		/// <param name="value">The <see cref="HtmlScript"/> object 
		/// to locate in the <see cref="IHtmlScriptList"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <returns>
		/// The zero-based index of the first occurrence of <paramref name="value"/> 
		/// in the <see cref="IHtmlScriptList"/>, if found; otherwise, -1.
		/// </returns>
		/// <remarks>Please refer to <see cref="IList.IndexOf"/> for details.</remarks>

		int IndexOf(HtmlScript value);
        
		#endregion
		#region Insert

		/// <summary>
		/// Inserts a <see cref="HtmlScript"/> element into the 
		/// <see cref="IHtmlScriptList"/> at the specified index.
		/// </summary>
		/// <param name="index">The zero-based index at which 
		/// <paramref name="value"/> should be inserted.</param>
		/// <param name="value">The <see cref="HtmlScript"/> object
		/// to insert into the <see cref="IHtmlScriptList"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <exception cref="ArgumentOutOfRangeException">
		/// <para><paramref name="index"/> is less than zero.</para>
		/// <para>-or-</para>
		/// <para><paramref name="index"/> is greater than 
		/// <see cref="IHtmlScriptCollection.Count"/>.</para>
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="IHtmlScriptList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>IHtmlScriptList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="IList.Insert"/> for details.</remarks>

		void Insert(int index, HtmlScript value);
        
		#endregion
		#region Remove

		/// <summary>
		/// Removes the first occurrence of the specified <see cref="HtmlScript"/>
		/// from the <see cref="IHtmlScriptList"/>.
		/// </summary>
		/// <param name="value">The <see cref="HtmlScript"/> object
		/// to remove from the <see cref="IHtmlScriptList"/>.
		/// This argument can be a null reference.
		/// </param>    
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="IHtmlScriptList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>IHtmlScriptList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="IList.Remove"/> for details.</remarks>

		void Remove(HtmlScript value);
        
		#endregion
		#region RemoveAt

		/// <summary>
		/// Removes the element at the specified index of the 
		/// <see cref="IHtmlScriptList"/>.
		/// </summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		/// <exception cref="ArgumentOutOfRangeException">
		/// <para><paramref name="index"/> is less than zero.</para>
		/// <para>-or-</para>
		/// <para><paramref name="index"/> is equal to or greater than 
		/// <see cref="IHtmlScriptCollection.Count"/>.</para>
		/// </exception>
		/// <exception cref="NotSupportedException">
		/// <para>The <see cref="IHtmlScriptList"/> is read-only.</para>
		/// <para>-or-</para>
		/// <para>The <b>IHtmlScriptList</b> has a fixed size.</para></exception>    
		/// <remarks>Please refer to <see cref="IList.RemoveAt"/> for details.</remarks>

		void RemoveAt(int index);

		#endregion
		#endregion
	}
    
	#endregion
	#region Interface IHtmlScriptEnumerator

	/// <summary>
	/// Supports type-safe iteration over a collection that 
	/// contains <see cref="HtmlScript"/> elements.
	/// </summary>
	/// <remarks>
	/// <b>IHtmlScriptEnumerator</b> provides an <see cref="IEnumerator"/> 
	/// that is strongly typed for <see cref="HtmlScript"/> elements.
	/// </remarks>    
        
	public interface IHtmlScriptEnumerator 
	{
		#region Properties
		#region Current

		/// <summary>
		/// Gets the current <see cref="HtmlScript"/> element in the collection.
		/// </summary>
		/// <value>The current <see cref="HtmlScript"/> element in the collection.</value>
		/// <exception cref="InvalidOperationException">The enumerator is positioned 
		/// before the first element of the collection or after the last element.</exception>    
		/// <remarks>Please refer to <see cref="IEnumerator.Current"/> for details.</remarks>    

		HtmlScript Current { get; }
        
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
