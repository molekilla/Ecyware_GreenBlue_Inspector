using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
namespace Ecyware.GreenBlue.Engine.HtmlDom
{
	/// <summary>
	/// Contains the definition for the HtmlTagBaseCollection.
	/// </summary>
	[Serializable]
	public class HtmlTagBaseCollection : NameObjectCollectionBase, ISerializable
	{
		private DictionaryEntry _de = new DictionaryEntry();

		/// <summary>
		/// Creates a new HtmlTagBaseCollection.
		/// </summary>
		public HtmlTagBaseCollection()
		{
		}

		/// <summary>
		/// Creates a new HtmlTagBaseCollection.
		/// </summary>
		/// <param name="info"> The serialization info.</param>
		/// <param name="context"> The streaming context.</param>
		public HtmlTagBaseCollection(SerializationInfo info, StreamingContext context)
		{
			//this._de = (Session)s.GetValue("SafeSession", typeof(Session));
			System.Runtime.Serialization.SerializationInfoEnumerator 
				infoItems = info.GetEnumerator();
			while(infoItems.MoveNext()) 
			{
				//if ( !infoItems.Name.StartsWith("Ecyware.GreenBlue.Engine.HtmlDom.") )
					base.BaseAdd(infoItems.Name, infoItems.Value);
			}

		}

		// Adds elements from an IDictionary into the new collection.
		public HtmlTagBaseCollection( IDictionary d, Boolean bReadOnly )  
		{
			foreach ( DictionaryEntry de in d )  
			{
				this.BaseAdd( (String) de.Key, de.Value );
			}
			this.IsReadOnly = bReadOnly;
		}

		#region Methods
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
		public HtmlTagBaseList this[ String key ]  
		{
			get  
			{
				return( (HtmlTagBaseList)this.BaseGet( key ) );
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
		public void Add( String key, HtmlTagBaseList value )  
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

		public bool ContainsKey(string key)
		{
			if ( this[key] == null )
			{
				return false;
			} 
			else 
			{
				return true;
			}
		}
		#endregion

		#region ISerializable Members

		/// <summary>
		/// Gets the serialization info to add data.
		/// </summary>
		/// <param name="info"> The serialization info.</param>
		/// <param name="context"> The streaming context.</param>
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			foreach(string name in base.BaseGetAllKeys()) 
			{
				try 
				{
					//if ( !infoItems.Name.StartsWith("Ecyware.GreenBlue.Engine.HtmlDom.") )
						info.AddValue(name, base.BaseGet(name));
				} 
				catch(Exception){}
			}

		}

		#endregion
	}
}
