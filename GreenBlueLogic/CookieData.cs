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

namespace Ecyware.GreenBlue.Protocols.Http
{
	/// <summary>
	/// Summary description for CookieData.
	/// </summary>
	/// 
	[Serializable]
	public class CookieDiskInfo
	{
		public string Path=String.Empty;
		
		public CookieDiskInfo()
		{
		}
		public CookieDiskInfo(string path)
		{
			this.Path = path;
		}
	}

	public class CookieData
	{
		string indexData = Application.UserAppDataPath + @"\cookies.dat";
		string cookiesFolder = Application.UserAppDataPath + @"\cookies";

		SortedList cookieIndex = null;

		public CookieData()
		{
			LoadCookieIndex();
		}

		/// <summary>
		/// Loads the cookie index in memory.
		/// </summary>
		private void LoadCookieIndex()
		{
			// check if directory exists else create new
			if ( !Directory.Exists(cookiesFolder) )
			{
				Directory.CreateDirectory(cookiesFolder);
			}

			// load index data
			if ( File.Exists(indexData) )
			{
				try
				{
					FileStream stm = File.Open(indexData,FileMode.Open);
					BinaryFormatter bf = new BinaryFormatter();
					cookieIndex = (SortedList)bf.Deserialize(stm);
					stm.Close();
				}
				catch
				{
					throw;
				}
			} 
			else 
			{
				cookieIndex = new SortedList();
			}

		}

		#region Main Methods
		public Hashtable GetCookieIndexReference()
		{	
			CookieDiskInfo diskInfo = null;
			Hashtable cookies = new Hashtable();

			foreach ( DictionaryEntry de in cookieIndex )
			{
				// key
				string key = de.Key.ToString();
				if ( key.EndsWith("/") )
				{					
					// Get Disk Info
					diskInfo = (CookieDiskInfo)de.Value;

					try
					{
						// Open cookie data and add to array
						cookies.Add(key.TrimEnd('/'),OpenCookieData(diskInfo.Path));
					}
					catch
					{ 
						// Could not load CookieData
						cookies.Add(key.TrimEnd('/'),null);
					}
				}
				
			}

			return cookies;
		}
		public HttpCookieCollection GetCookieHashtable(Uri url)
		{
			CookieDiskInfo diskInfo = null;
			string uriAndPort = url.Authority;

			if ( cookieIndex.ContainsKey(uriAndPort + "/") ) 
			{
				diskInfo = (CookieDiskInfo)cookieIndex[uriAndPort + "/"];
				return OpenCookieData(diskInfo.Path);
			} else {
				return null;
			}
			
		}


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

		
		
		private HttpCookieCollection OpenCookieData(string path)
		{
			FileStream stm = null;
			HttpCookieCollection result = new HttpCookieCollection();
			if ( File.Exists(path) )
			{
				try
				{
					stm = File.Open(path,FileMode.Open);
					BinaryFormatter bf = new BinaryFormatter();
					result = (HttpCookieCollection)bf.Deserialize(stm);
					stm.Close();
				}
				catch
				{
					throw;
				}

			}
			return result;
		}

		// TODO: Check for errors while updating
		private string UpdateCookieFile(string dirPath,string absPath, HttpCookieCollection data)
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

		public void AddCookie(Uri siteUri,HttpCookieCollection cookies)
		{
			// Since we are not using children in this type
			// of index, all nodes are parent and use / instead of the default
			// value from AbsolutePath from Uri.

			string folderName = siteUri.Authority;// + siteUri.Port;
			string uriAndPort = siteUri.Authority;// + ":" + siteUri.Port;
			string dir = cookiesFolder + @"\" + folderName;

			if ( !Directory.Exists(dir) )
			{
				Directory.CreateDirectory(dir);
			}

			// search index data for root existence
			CookieDiskInfo domainIndex = (CookieDiskInfo)cookieIndex[uriAndPort];

			// add / update new cookie to disk
			if ( domainIndex != null )
			{			
				// get current cookie index
				CookieDiskInfo cookieInfo = (CookieDiskInfo)cookieIndex[uriAndPort + "/"];

				// add/update file to disk
				string filePath = UpdateCookieFile(dir,"/",cookies);

				if ( cookieInfo == null )
				{
					CookieDiskInfo newCookieInfo = new CookieDiskInfo(filePath);

					// add reference to Index data
					cookieIndex.Add(uriAndPort + "/",newCookieInfo);
				}

			} 
			else 
			{
				// add as root node						
				// add file to disk
				string filePath = UpdateCookieFile(dir,"/",cookies);

				CookieDiskInfo cookieInfo = new CookieDiskInfo(filePath);

				// add cookie reference to Index data
				cookieIndex.Add(uriAndPort + "/",cookieInfo);

				// add root folder reference to Index data
				cookieIndex.Add(uriAndPort,new CookieDiskInfo(""));
			}

			UpdateIndexData();
		}

		#endregion
	}
}
