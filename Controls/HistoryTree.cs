// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using Ecyware.GreenBlue.Engine;
using System.ComponentModel;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Represents the history tree for the sites visited.
	/// </summary>
	public sealed class HistoryTree : TreeView
	{
		/// <summary>
		/// The index data filepath.
		/// </summary>
		string indexData = Application.UserAppDataPath + @"\index.dat";
		/// <summary>
		/// The directory where the history files are saved.
		/// </summary>
		string recentSitesDir = Application.UserAppDataPath + @"\recent sites";

		/// <summary>
		/// The recent sites SortedList type.
		/// </summary>
		SortedList sitesIndex = null;
		private bool _useCache=false;

		private HistoryIndexConfigurationHandler serializer;
		private int _iconSiteIndex;
		private int _iconNodeIndex;

		#region Constructors
		/// <summary>
		/// HistoryTree Constructor.
		/// </summary>
		public HistoryTree() : base()
		{
			serializer = new HistoryIndexConfigurationHandler();
		}

		/// <summary>
		/// Creates a new HistoryTree with a cache setting. 
		/// </summary>
		/// <param name="cacheDisk"> If true, the files are store into disk, else the recent sites are store in memory.</param>
		public HistoryTree(bool cacheDisk) : this()
		{
			this.Sorted = true;
			_useCache = cacheDisk;

			if ( _useCache )
			{
				LoadHistoryTree();
			}
		}

		/// <summary>
		/// Creates a new HistoryTree and sets the cache, imagelist and image indices properties.
		/// </summary>
		/// <param name="cacheDisk"> If true, the files are store into disk, else the recent sites are store in memory.</param>
		/// <param name="imageList"> The ImageList containing the images.</param>
		/// <param name="siteImageIndex"> The site image index.</param>
		/// <param name="nodeImageIndex"> The node image index.</param>
		public HistoryTree(bool cacheDisk,ImageList imageList, int siteImageIndex, int nodeImageIndex):this()
		{
			this.Sorted = true;
			_useCache = cacheDisk;

			this.IconNodeIndex = nodeImageIndex;
			this.IconSiteIndex = siteImageIndex;
			this.ImageList = imageList;			

			if ( _useCache )
			{
				LoadHistoryTree();
			}
		}

		#endregion

		/// <summary>
		/// Loads the history tree in memory.
		/// </summary>
		public void LoadHistoryTree()
		{
			_useCache = true;
			// check if directory exists else create new
//			if ( !Directory.Exists(recentSitesDir) )
//			{
//				Directory.CreateDirectory(recentSitesDir);
//			}
			// load index data
			if ( File.Exists(indexData) )
			{
				try
				{
					sitesIndex = ((HistoryIndex)serializer.Load("HistoryIndex", indexData)).GetSortedList();
					LoadTree(sitesIndex);
				}
				catch
				{
					throw;
				}
			} 
			else 
			{
				sitesIndex = new SortedList();
			}

		}

		#region Disk serialization methods
		/// <summary>
		/// Gets a ResponseBuffer by HistoryTreeNode.
		/// </summary>
		/// <param name="node"> A HistoryTreeNode to lookup.</param>
		/// <returns> A ResponseBuffer type.</returns>
//		public ResponseBuffer GetHttpSiteData(HistoryTreeNode node)
//		{
//			string uriAndPort = string.Empty;
//
//			if ( node.Url.Port == 80 )
//			{
//				uriAndPort = node.Url.Authority + ":" + node.Url.Port;
//			} 
//			else 
//			{
//				uriAndPort = node.Url.Authority;
//			}
//
//			if ( _useCache )
//			{
//				HistoryDiskInfo diskInfo = (HistoryDiskInfo)sitesIndex[uriAndPort + node.Url.AbsolutePath];
//				return this.OpenResponseBuffer(diskInfo.Path);
//			} 
//			else 
//			{
//				return node.HttpSiteData;
//			}
//		}

		/// <summary>
		/// Loads the tree.
		/// </summary>
		/// <param name="sitesIdx"> The sites index list to load.</param>
		private void LoadTree(SortedList sitesIdx)
		{
			// Root nodes are simple like
			// asp.net, msdn.microsoft.com, www.mysite.com
			// they don't include /
			// add node to tree
			HistoryTreeNode rootNode=null;
			int i = 0;

			while ( i < sitesIndex.Count )
			{
				string key = (string)sitesIndex.GetKey(i);
				HistoryDiskInfo hdi = (HistoryDiskInfo)sitesIndex[key];

				if ( hdi.Type == HistoryDiskInfo.NodeType.Parent )
				{
					// is a root node
					rootNode = new HistoryTreeNode(key,new Uri("http://" + key),null);
					rootNode.ImageIndex = this.IconSiteIndex;
					rootNode.SelectedImageIndex = this.IconSiteIndex;
					this.Nodes.Add(rootNode);

					foreach ( HistoryDiskInfo item in hdi.HistoryDiskInfoList )
					{
						// is a child node					
						Uri u = new Uri("http://" + item.Url);
						HistoryTreeNode newNode = new HistoryTreeNode(u.AbsolutePath,u,null);
						newNode.ImageIndex = this.IconNodeIndex;
						newNode.SelectedImageIndex = this.IconNodeIndex;
						rootNode.Nodes.Add(newNode);
					}
				}

				i++;
			}

		}

		/// <summary>
		/// Gets a filepath.
		/// </summary>
		/// <param name="directory"> The site directory.</param>
		/// <param name="path"> The current path.</param>
		/// <returns> A string with the filepath.</returns>
		private string GetFilePath(string directory, string path)
		{
			string newPath = String.Empty;
			if ( path == "/" )
			{
				newPath = directory + @"\0_.gbi";			
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
		/// Opens a ResponseBuffer.
		/// </summary>
		/// <param name="path"> The path where the serialize ResponseBuffer is saved.</param>
		/// <returns> Returns a ResponseBuffer, else returns null.</returns>
//		private ResponseBuffer OpenResponseBuffer(string path)
//		{
//			FileStream stm = null;
//			ResponseBuffer resp = null;
//			if ( File.Exists(path) )
//			{
//				stm = File.Open(path,FileMode.Open);
//				BinaryFormatter bf = new BinaryFormatter();
//				resp = (ResponseBuffer)bf.Deserialize(stm);
//				stm.Close();
//			}
//			return resp;
//		}
		
		/// <summary>
		/// Updates the site data.
		/// </summary>
		/// <param name="dirPath"> The directory where the ResponseBuffer resides.</param>
		/// <param name="absPath"> The absolute path.</param>
		/// <param name="data"> The current ResponseBuffer.</param>
		/// <returns> A string with the new path.</returns>
//		private string UpdateRecentSite(string dirPath,string absPath, ResponseBuffer data)
//		{
//			string newPath = absPath;
//			if ( !File.Exists(newPath) )
//			{
//				newPath = GetFilePath(dirPath, newPath);
//			}
//
//			FileStream stm = null;
//			if ( File.Exists(newPath) )
//			{
//				stm = File.Open(newPath,FileMode.Create);
//			} 
//			else 
//			{
//				// create any directory inside path
//				string[] dirs = absPath.Split('/');
//				string newDir = dirPath + "\\";
//				for (int i=1;i<dirs.Length;i++)
//				{
//					if (i==(dirs.Length-1))
//						break; // exit for loop
//
//					newDir += dirs[i] + "\\";
//					if ( !Directory.Exists(newDir) )
//					{
//						Directory.CreateDirectory(newDir);
//					}
//				}
//				stm = File.Open(newPath,FileMode.CreateNew);
//			}
//
//			// Save Response Buffer
//			BinaryFormatter bf = new BinaryFormatter();
//			bf.Serialize(stm,data);
//			stm.Close();
//
//			return newPath;
//		}

		/// <summary>
		/// Updates the index data.
		/// </summary>
		private void UpdateIndexData()
		{
			HistoryIndex index = new HistoryIndex();
			HistoryDiskInfo[] d = (HistoryDiskInfo[])(new ArrayList(sitesIndex.Values)).ToArray(typeof(HistoryDiskInfo));
			index.HistoryDiskInfoList = d;
			serializer.Save(index, "HistoryIndex", indexData);			
		}

		/// <summary>
		/// Adds a HistoryDiskInfo.
		/// </summary>
		/// <param name="siteUri"> The site uri.</param>
		/// <param name="siteData"> The ResponseBuffer data.</param>
		public void AddHistoryNodeDisk(Uri siteUri, ResponseBuffer siteData)
		{
			this.SuspendLayout();

			//string folderName = string.Empty;
			string uriAndPort = string.Empty;

			if ( siteUri.Port == 80 )
			{
				//folderName = siteUri.Authority + siteUri.Port;
				uriAndPort = siteUri.Authority + ":" + siteUri.Port;
			} 
			else 
			{
				//folderName = siteUri.Authority.Replace(":","");
				uriAndPort = siteUri.Authority;
			}
//			string dir = recentSitesDir + @"\" + folderName;
//
//			if ( !Directory.Exists(dir) )
//			{
//				Directory.CreateDirectory(dir);
//			}

			// search index data for site root existence
			HistoryDiskInfo diskInfo = (HistoryDiskInfo)sitesIndex[uriAndPort];
			int index = sitesIndex.IndexOfKey(uriAndPort);

			// add new to disk
			if ( diskInfo != null )
			{				
				HistoryTreeNode node = (HistoryTreeNode)this.Nodes[index];				
				
				// add node to tree
				HistoryTreeNode tn = new HistoryTreeNode(siteUri.AbsolutePath,siteUri,null);
				tn.ImageIndex = this.IconNodeIndex;
				tn.SelectedImageIndex = this.IconNodeIndex;

				//int i = node.Nodes.IndexOf(tn);
				HistoryDiskInfo hdi = new HistoryDiskInfo();
				hdi.Url = uriAndPort + siteUri.AbsolutePath;

				if ( !diskInfo.ContainsNodeKey(hdi.Url) )
				{
					node.Nodes.Add(tn);
					diskInfo.AddHistoryDiskInfo(hdi.Url, hdi);
				}
			} 
			else 
			{
				// add as root node
				HistoryTreeNode tn = new HistoryTreeNode(uriAndPort,siteUri,null);
				tn.ImageIndex = this.IconSiteIndex;
				tn.SelectedImageIndex = this.IconSiteIndex;
							
				// add file to disk
				//string filePath = UpdateRecentSite(dir,siteUri.AbsolutePath,siteData);

				// add node to tree
				HistoryTreeNode newNode = new HistoryTreeNode(siteUri.AbsolutePath,siteUri,null);
				newNode.ImageIndex = this.IconNodeIndex;
				newNode.SelectedImageIndex = this.IconNodeIndex;

				tn.Nodes.Add(newNode);

				int i = tn.Nodes.IndexOf(newNode);
				HistoryDiskInfo child = new HistoryDiskInfo();
				child.Url = uriAndPort + siteUri.AbsolutePath;

				// add reference to Index data
				//sitesIndex.Add(uriAndPort + siteUri.AbsolutePath,hdi);

				this.Nodes.Add(tn);

				// add root reference to Index data
				HistoryDiskInfo parent = new HistoryDiskInfo();
				parent.Url = uriAndPort;
				parent.Type = HistoryDiskInfo.NodeType.Parent;
				parent.AddHistoryDiskInfo(child.Url, child);
				sitesIndex.Add(uriAndPort, parent);
			}

			UpdateIndexData();
			this.ResumeLayout();
		}

		#endregion
		#region Icon Properties
		/// <summary>
		/// Gets or sets the IconSiteIndex.
		/// </summary>
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

		/// <summary>
		/// Gets or sets the IconNodeIndex.
		/// </summary>
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

	}
}
