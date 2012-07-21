// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: August 2004
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// Contains the collection for the Post data.
/// </summary>
public class NameObjectCollection : NameObjectCollectionBase  
{

	private DictionaryEntry _de = new DictionaryEntry();

	// Creates an empty collection.
	public NameObjectCollection()  
	{
	}

	// Adds elements from an IDictionary into the new collection.
	public NameObjectCollection( IDictionary d, Boolean bReadOnly )  
	{
		foreach ( DictionaryEntry de in d )  
		{
			this.BaseAdd( (String) de.Key, de.Value );
		}
		this.IsReadOnly = bReadOnly;
	}

	// Gets a key-and-value pair (DictionaryEntry) using an index.
	public DictionaryEntry this[ int index ]  
	{
		get  
		{
			_de.Key = this.BaseGetKey(index);
			_de.Value = this.BaseGet(index);
			return( _de );
		}
	}

	// Gets or sets the value associated with the specified key.
	public object this[ String key ]  
	{
		get  
		{
			return ( this.BaseGet( key ) );
		}
		set  
		{
			this.BaseSet( key, value );
		}
	}

	// Gets a String array that contains all the keys in the collection.
	public String[] AllKeys  
	{
		get  
		{
			return( this.BaseGetAllKeys() );
		}
	}

	// Gets an Object array that contains all the values in the collection.
	public Array AllValues  
	{
		get  
		{
			return( this.BaseGetAllValues() );
		}
	}

	// Gets a String array that contains all the values in the collection.
	public String[] AllStringValues  
	{
		get  
		{
			return( (String[]) this.BaseGetAllValues( Type.GetType( "System.String" ) ) );
		}
	}

	// Gets a value indicating if the collection contains keys that are not null.
	public Boolean HasKeys  
	{
		get  
		{
			return( this.BaseHasKeys() );
		}
	}

	// Adds an entry to the collection.
	public void Add( String key, object value )  
	{
		this.BaseAdd( key, value );
	}


	// Removes an entry with the specified key from the collection.
	public void Remove( String key )  
	{
		this.BaseRemove( key );
	}

	// Removes an entry in the specified index from the collection.
	public void Remove( int index )  
	{
		this.BaseRemoveAt( index );
	}

	// Clears all the elements in the collection.
	public void Clear()  
	{
		this.BaseClear();
	}

	/// <summary>
	/// Clones the current object into a new PostDataCollection.
	/// </summary>
	/// <returns>A new HtmlFormTag.</returns>
//	public PostDataCollection Clone()
//	{
//		// new memory stream
//		MemoryStream ms = new MemoryStream();
//
//		// new BinaryFormatter
//		BinaryFormatter bf = new BinaryFormatter(null,
//			new StreamingContext(StreamingContextStates.Clone));
//			
//		// serialize
//		bf.Serialize(ms,this);
//			
//		// go to beggining
//		ms.Seek(0,SeekOrigin.Begin);
//			
//		// deserialize
//		PostDataCollection retVal = (PostDataCollection)bf.Deserialize(ms);
//		ms.Close();
//
//		return retVal;
//	}

}
