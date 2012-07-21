// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Net;
using System.Xml;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Maintains the cookie store for GB Inspector.
	/// </summary>
	public class CookieManager
	{
		/// <summary>
		/// The current cookie container store.
		/// </summary>
		private CookieContainer manager = new CookieContainer();

		/// <summary>
		/// The file where the cookie container is saved.
		/// </summary>
		string managerDiskData;

		/// <summary>
		/// CookieManager Constructor.
		/// </summary>
		public CookieManager()
		{
			if ( System.Web.HttpContext.Current == null )
			{
				managerDiskData = System.Windows.Forms.Application.UserAppDataPath + @"\cookies.dat";
			} 
			else 
			{
				managerDiskData = System.Web.HttpContext.Current.Server.MapPath("cookies.txt");
			}
		}


		/// <summary>
		/// Adds a cookie collection and saves it. If cookie collection is null, it doesn't add it to the manager.
		/// </summary>
		/// <param name="cookies"> The CookieCollection to add.</param>
		public void AddCookies(CookieCollection cookies)
		{
			if ( cookies != null )
			{
				manager.Add(cookies);

//				if ( cookies.Count > 0 )
//					this.SaveManagerState();
			}
		}

		/// <summary>
		/// Gets the cookie collection by uri.
		/// </summary>
		/// <param name="url"> The uri.</param>
		/// <returns> Returns the cookie collection if found, else null.</returns>
		public CookieCollection GetCookies(Uri url)
		{
			return manager.GetCookies(url);
		}

		/// <summary>
		/// Saves the cookie container to disk.
		/// </summary>
		public void SaveManagerState()
		{
			FileStream stm = File.Open(managerDiskData, FileMode.Create);
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(stm, manager);
			stm.Close();
		}

		/// <summary>
		/// Loads the cookie container.
		/// </summary>
		public void OpenManagerState()
		{
			if ( File.Exists(managerDiskData) )
			{
				FileStream stm = File.Open(managerDiskData, FileMode.Open);
				BinaryFormatter bf = new BinaryFormatter();
				manager = (CookieContainer)bf.Deserialize(stm);
				stm.Close();
			}
		}

		/// <summary>
		/// Checks that the cookie store is not empty.
		/// </summary>
		/// <returns> Returns true if no cookie is in store, else false.</returns>
		public bool IsManagerEmpty()
		{
			if ( manager.Count == 0 )
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
