// All rights reserved.
// Title: GreenBlue Project
// Author(s): Rogelio Morrell C.
// Date: February 2003
// Add additional authors here
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Ecyware.GreenBlue.Protocols.Http;
using System.Net;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Summary description for CookieData.
	/// </summary>
	/// 

	public sealed class CookieTree : TreeView
	{
		SortedList cookieIndex = null;

		private int _iconSiteIndex;
		private int _iconNodeIndex;
		#region Icon Properties
		public int IconSiteIndex
		{
			get
			{
				return _iconSiteIndex;
			}
			set
			{
				_iconSiteIndex = value;
			}
		}
		public int IconNodeIndex
		{
			get
			{
				return _iconNodeIndex;
			}
			set
			{
				_iconNodeIndex = value;
			}
		}
		#endregion
		public CookieTree() : base()
		{
		}

		/// <summary>
		/// Loads the tree.
		/// </summary>
		private void LoadTree(CookieContainer cookieContainer)
		{
			// For each Cookie Collection in Cookie Store
			foreach ( cookie in cookieContainer.)
			{
				string key = de.Key.ToString();
				HttpCookieCollection cookieColl = (HttpCookieCollection)de.Value;

				TreeNode newNode = new TreeNode();
				newNode.Text = key;

				// add values to newNode
				foreach ( DictionaryEntry dd in cookieColl )
				{
					TreeNode childNode = new TreeNode();
					HttpCookie cookie = (HttpCookie)dd.Value;
					childNode.Text = cookie.Name;
					newNode.Nodes.Add(childNode);
				}

				// add new node to root
				this.Nodes.Add(newNode);
			}
		}

		#region Main Methods
		/// <summary>
		/// Get the Cookie Index as a Hashtable.
		/// </summary>
		/// <returns> A Hashtable.</returns>
//		public Hashtable GetCookieIndexReference()
//		{	
//			CookieDiskInfo diskInfo = null;
//			Hashtable cookies = new Hashtable();
//
//			foreach ( DictionaryEntry de in cookieIndex )
//			{
//				// key
//				string key = de.Key.ToString();
//				if ( key.EndsWith("/") )
//				{					
//					// Get Disk Info
//					diskInfo = (CookieDiskInfo)de.Value;
//
//					try
//					{
//						// Open cookie data and add to array
//						cookies.Add(key.TrimEnd('/'),OpenCookieData(diskInfo.Path));
//					}
//					catch
//					{ 
//						// Could not load CookieData
//						cookies.Add(key.TrimEnd('/'),null);
//					}
//				}
//				
//			}
//
//			return cookies;
//		}

		/// <summary>
		/// Gets a HttpCookieCollection from a url.
		/// </summary>
		/// <param name="url"> A web site uri.</param>
		/// <returns> A HttpCookieCollection.</returns>
//		public HttpCookieCollection GetCookieHashtable(Uri url)
//		{
//			// check segments
//			HttpCookieCollection cookies = new HttpCookieCollection(21);
//
//			string checkDomain = string.Empty;
//			string[] domainParts = url.Authority.Split('.');
//
//			// reverse loop
//			for (int i=(domainParts.Length-1);i>0;i--)
//			{
//				checkDomain = domainParts[i] + "." + checkDomain;
//
//				// skip root domain
//				if ( ( domainParts.Length-1 ) != i)
//				{
//					// check domain in Cookie Index
//					if ( cookieIndex.ContainsKey(checkDomain.TrimEnd('.') + "/") ) 
//					{
//						CookieDiskInfo diskInfo = (CookieDiskInfo)cookieIndex[checkDomain.TrimEnd('.') + "/"];
//
//						// load Cookie Data
//						HttpCookieCollection temp = OpenCookieData(diskInfo.Path);
//						foreach ( DictionaryEntry de in temp )
//						{
//							HttpCookie tempCookie = (HttpCookie)de.Value;
//							cookies.Add(tempCookie.Name, tempCookie);
//						}
//					}
//				}			
//			}
//
//			return cookies;
//
//			/*CookieDiskInfo diskInfo = null;
//			string uriAndPort = url.Authority;
//
//			if ( cookieIndex.ContainsKey(uriAndPort + "/") ) 
//			{
//				diskInfo = (CookieDiskInfo)cookieIndex[uriAndPort + "/"];
//				return OpenCookieData(diskInfo.Path);
//			} else {
//				return null;
//			}
//			*/
//			
//		}

		private string GetFilePath(string directory, string path)
		{
			string newPath = String.Empty;
			if ( path == "/" )
			{
				newPath = directory + @"\sitecookie.gbc";
			} 
			else 
			{
				newPath = directory + path.Replace(@"/",@"\");
				string[] s = newPath.Split('\\');

				string fileName = s[s.Length-1].Split('.')[0] + ".gbi";
				StringBuilder sb = new StringBuilder();
				for (int i=0;i<(s.Length-1);i++)
				{
					sb.Append(s[i]);
					sb.Append("\\");
				}
				sb.Append(fileName);
				newPath = sb.ToString();
			}

			return newPath;
		}
				
		/// <summary>
		/// Opens a cookie from path.
		/// </summary>
		/// <param name="path"> Path that contains the cookie data.</param>
		/// <returns> A Cookie Collection.</returns>
//		private HttpCookieCollection OpenCookieData(string path)
//		{
//			FileStream stm = null;
//			HttpCookieCollection result = new HttpCookieCollection();
//			if ( File.Exists(path) )
//			{
//				try
//				{
//					stm = File.Open(path,FileMode.Open);
//					BinaryFormatter bf = new BinaryFormatter();
//					result = (HttpCookieCollection)bf.Deserialize(stm);
//					stm.Close();
//				}
//				catch
//				{
//					throw;
//				}
//
//			}
//			return result;
//		}


		/// <summary>
		/// Adds a cookie to cookie store.
		/// </summary>
		/// <param name="siteUri"> Web Site uri. </param>
		/// <param name="cookies"> Cookie collection.</param>
//		public void AddCookies(Uri siteUri, CookieCollection cookies)
//		{
//
//			// Add Cookies to CookieContainer
//
//
//			// Since we are not using children in this type
//			// of index, all nodes are parent and use / instead of the default
//			// value from AbsolutePath from Uri.
//
//			string folderName = siteUri.Authority;
//			//string cookieFile = siteUri.Authority + "/";
//			//string dir = cookiesFolder + @"\" + folderName;
//
//			CookieCollection coll = null;
//			CookieDiskInfo diskInfo = null;
//
//			// Add each cookie using the Domain, like IE cookie management
//			foreach ( DictionaryEntry de in cookies)
//			{
//				Cookie cookie = (Cookie)de.Value;
//
//				// Check for previous cookie
//				diskInfo = (CookieDiskInfo)cookieIndex[cookie.Domain];
//
//				// if found, check that name and path are equals
//				if ( diskInfo != null )
//				{
//					#region Update Cookie
//					// opens an existing disk info file
//					coll = this.OpenCookieData(diskInfo.Path);
//
//					//TODO: remember we can only add 20 cookies by domain
//					
//					// check name
//					if ( coll.ContainsKey(cookie.Name) )
//					{
//						// check path
//						if ( coll[cookie.Name].Path == cookie.Path )
//						{
//							// Update cookie
//							coll[cookie.Name] = cookie;
//						}
//					}
//
//					// update cookie data
//					string dirName = cookie.Domain;
//					string fileName = UpdateCookieFile(dirName,"/",coll);
//					#endregion
//				} 
//				else 
//				{
//					#region Add Cookie	
//					coll = new CookieCollection(21);
//					coll.Add(cookie.Name, cookie);
//					
//					string dirName = cookie.Domain;
//					if ( !Directory.Exists(dirName) )
//					{
//						Directory.CreateDirectory(dirName);
//					}
//
//					// update cookie data
//					string filePath = UpdateCookieFile(dirName,"/",coll);
//
//					// Add Disk Info to Cookie Index
//					diskInfo = new CookieDiskInfo(filePath);
//
//					// add cookie reference to Index data
//					cookieIndex.Add(dirName + "/",diskInfo);
//	
//					// add root folder reference to Index data
//					cookieIndex.Add(dirName,new CookieDiskInfo(""));
//
//					UpdateIndexData();
//					#endregion
//				}
//				
//			}
//
////			// search index data for Cookie Domain existence
////			CookieDiskInfo domainIndex = (CookieDiskInfo)cookieIndex[uriAndPort];
////
////			// add / update new cookie to disk
////			if ( domainIndex != null )
////			{			
////				// get Cookie Data
////				CookieDiskInfo cookieInfo = (CookieDiskInfo)cookieIndex[uriAndPort + "/"];
////
////				// Update tree node
////				UpdateCookieTreeNode(uriAndPort, cookies);
////
////				// update file to disk
////				string filePath = UpdateCookieFile(dir,"/",cookies);
////
////				// if cookieinfo null, then we add reference to Cookie Index
////				if ( cookieInfo == null )
////				{
////					CookieDiskInfo newCookieInfo = new CookieDiskInfo(filePath);
////
////					// add reference to Index data
////					cookieIndex.Add(uriAndPort + "/",newCookieInfo);
////				}
////
////			} 
////			else 
////			{
////				// add tree node
////				AddCookieTreeNode(uriAndPort, cookies);
////
////				// add as root node						
////				// add file to disk
////				string filePath = UpdateCookieFile(dir,"/",cookies);
////
////				CookieDiskInfo cookieInfo = new CookieDiskInfo(filePath);
////
////				// add cookie reference to Index data
////				cookieIndex.Add(uriAndPort + "/",cookieInfo);
////
////				// add root folder reference to Index data
////				cookieIndex.Add(uriAndPort,new CookieDiskInfo(""));
////			}
//
//			//UpdateIndexData();
//		}

//		private void AddCookieTreeNode(string key, HttpCookieCollection cookies)
//		{
//			TreeNode newNode = new TreeNode();
//			newNode.Text = key;
//
//			foreach ( DictionaryEntry dd in cookies )
//			{
//				TreeNode childNode = new TreeNode();
//				HttpCookie cookie = (HttpCookie)dd.Value;
//				childNode.Text = cookie.Name;
//				newNode.Nodes.Add(childNode);
//			}
//
//			this.Nodes.Add(newNode);
//		}
//
//		private void UpdateCookieTreeNode(string key, HttpCookieCollection cookies)
//		{
//			TreeNode newNode = null;
//
//			// get node
//			foreach ( TreeNode node in this.Nodes )
//			{
//				if ( node.Text == key )
//				{
//					newNode = node;
//					break;
//				}
//			}
//			
//			// clear children
//			newNode.Nodes.Clear();
//
//			foreach ( DictionaryEntry dd in cookies )
//			{
//				TreeNode childNode = new TreeNode();
//				Cookie cookie = (Cookie)dd.Value;
//				childNode.Text = cookie.Name;
//				newNode.Nodes.Add(childNode);
//			}
//
//			//this.Nodes.Add(newNode);
//		}


		#endregion
		#region Update Methods
		// TODO: Check for errors while updating
		private string UpdateCookieFile(string dirPath,string absPath, CookieCollection data)
		{
			string newPath = absPath;
			if ( !File.Exists(newPath) )
			{
				newPath = GetFilePath(dirPath, newPath);
			}

			FileStream stm = null;
			if ( File.Exists(newPath) )
			{
				File.Delete(newPath);
				stm = File.Open(newPath,FileMode.CreateNew);
			} 
			else 
			{
				// create any directory inside path
				string[] dirs = absPath.Split('/');
				string newDir = dirPath + "\\";
				for (int i=1;i<dirs.Length;i++)
				{
					if (i==(dirs.Length-1))
						break; // exit for loop

					newDir += dirs[i] + "\\";
					if ( !Directory.Exists(newDir) )
					{
						Directory.CreateDirectory(newDir);
					}
				}
				stm = File.Open(newPath,FileMode.CreateNew);
			}

			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(stm,data);
			stm.Close();

			return newPath;
		}
		// TODO: Check for errors while updating
		private void UpdateIndexData()
		{
			FileStream stm = File.Open(indexData,FileMode.Create);
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(stm,cookieIndex);
			stm.Close();
		}

		#endregion
	}
}
