// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using Ecyware.GreenBlue.Configuration;
using System.Collections;
namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for WebTransformConfiguration.
	/// </summary>
	public class WebTransformConfiguration
	{
		private ArrayList _providers = new ArrayList();

		/// <summary>
		/// Creates a WebTransformConfiguration type.
		/// </summary>
		public WebTransformConfiguration()
		{
		}

		/// <summary>
		/// Clears the transform proviers.
		/// </summary>
		public void ClearTransformProviders()
		{
			_providers.Clear();
		}

		/// <summary>
		/// Gets or sets transform providers.
		/// </summary>
		public TransformProvider[] Transforms
		{
			get
			{
				return (TransformProvider[])_providers.ToArray(typeof(TransformProvider));
			}
			set
			{
				if ( value != null )
					_providers.AddRange(value);
			}
		}
	}
}
