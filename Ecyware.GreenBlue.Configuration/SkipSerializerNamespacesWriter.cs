// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: October 2004
using System;
using System.Xml;
using System.Text;
using System.IO;

namespace Ecyware.GreenBlue.Configuration
{
	/// <summary>
	/// Summary description for SkipSerializerNamespacesReader.
	/// </summary>
	public class SkipSerializerNamespacesWriter : XmlTextWriter
	{
		
		public SkipSerializerNamespacesWriter( TextWriter w ) : base( w ) {}
		public SkipSerializerNamespacesWriter( Stream w, Encoding encoding ) : base( w, encoding ) {}
		public SkipSerializerNamespacesWriter( string filename, Encoding encoding ) : base( filename, encoding ) {}

		bool _skip = false;

		public override void WriteStartAttribute( string prefix, string localName, string ns )
		{
			// Omits XSD and XSI declarations.
			if ( prefix == "xmlns" && ( localName == "xsd" || localName == "xsi" ) ) 
			{
				_skip = true;
				return;
			}
			base.WriteStartAttribute( prefix, localName, ns );
		}

		public override void WriteString( string text )
		{
			if ( _skip ) return;
			base.WriteString( text );
		}

		public override void WriteEndAttribute()
		{
			if ( _skip )
			{
				// Reset the flag, so we keep writing.
				_skip = false;
				return;
			}
			base.WriteEndAttribute();
		}
	}
}
