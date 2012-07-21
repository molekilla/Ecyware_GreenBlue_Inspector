// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: October 2004
using System;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

namespace Ecyware.GreenBlue.Configuration
{
	/// <summary>
	/// Summary description for SimpleConfiguration.
	/// </summary>
	[Serializable]	
	[XmlRoot(ElementName="SimpleConfig", IsNullable=true)]
	public class SimpleConfiguration : Configuration
	{
		private ArrayList _prov = null;

		/// <summary>
		/// Creates a new SimpleConfiguration.
		/// </summary>
		public SimpleConfiguration()
		{
		}

		/// <summary>
		/// Gets or sets the providers.
		/// </summary>
		[XmlArrayItem(IsNullable=false)]
		public Provider[] Providers
		{
			get
			{
				return (Provider[])_prov.ToArray(typeof(Provider));
			}
			set
			{	
				_prov = new ArrayList();
				_prov.AddRange(value);
			}
		}

		#region Strong Type LoadConfiguration and SaveConfiguration
		public static XmlNode SaveConfiguration(object instance, Type[] types)
		{
			return SimpleConfiguration.SaveConfiguration(typeof(SimpleConfiguration),instance, "Provider",types);
		}
		public static SimpleConfiguration LoadConfiguration(XmlNode section)
		{
			return (SimpleConfiguration)SimpleConfiguration.LoadConfiguration(typeof(SimpleConfiguration), section);
		}

		public static SimpleConfiguration LoadConfiguration(XmlNode section, Type[] types)
		{
			return (SimpleConfiguration)SimpleConfiguration.LoadConfiguration(typeof(SimpleConfiguration), section,"Provider", types);
		}
		#endregion
	}
}
