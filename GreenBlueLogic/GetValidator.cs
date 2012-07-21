// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Collections;
using Ecyware.LicenseType.IPLimit;

namespace Ecyware.GreenBlue.Protocols.Http
{
	/// <summary>
	/// Contains the ip validation logic.
	/// </summary>
	public sealed class GetIPValidator
	{
		private static readonly IPValidator instance = new IPValidator();
		private static ArrayList _hosts = new ArrayList();
   
		private GetIPValidator(){}

		/// <summary>
		/// Get the instance of a IPValidator.
		/// </summary>
		public static IPValidator Instance
		{
			get 
			{
				return instance; 
			}
		}

		/// <summary>
		/// Adds a url host to cache.
		/// </summary>
		/// <param name="urlHost"> Url host to add.</param>
		public static void AddToCache(string urlHost)
		{
			if ( !_hosts.Contains(urlHost) )
				_hosts.Add(urlHost);
		}

		/// <summary>
		/// Checks the cache
		/// </summary>
		/// <param name="urlHost"> The url host to check.</param>
		/// <returns> Returns true if found, else false.</returns>
		public static bool CheckCache(string urlHost)
		{
			if ( _hosts.Contains(urlHost) )
			{
				return true;
			} 
			else 
			{
				return false;
			}
		}
	}
}
