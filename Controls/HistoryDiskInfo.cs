// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Collections;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Contains a definition for a serializable based history info.
	/// </summary>
	[Serializable]
	public class HistoryDiskInfo
	{
		public enum NodeType
		{
			Parent,
			Child
		}

		SortedList list = new SortedList();
		public NodeType Type = NodeType.Child;
		public string Url = string.Empty;
		//public string Path = String.Empty;
		//public int Index = -1;

		/// <summary>
		/// Creates a new HistoryDiskInfo.
		/// </summary>
		public HistoryDiskInfo()
		{
		}

		/// <summary>
		/// Creates a new HistoryDiskInfo.
		/// </summary>
		/// <param name="path"> The file path of the history file.</param>
		/// <param name="index"> The index.</param>
//		public HistoryDiskInfo(string path, int index)
//		{
//			this.Path = path;
//			this.Index = index;
//		}

		public void AddHistoryDiskInfo(string url, HistoryDiskInfo diskInfo)
		{
			list.Add(url, diskInfo);
		}

		public bool ContainsNodeKey(string key)
		{
			return list.ContainsKey(key);
		}

		/// <summary>
		/// Gets the sorted list.
		/// </summary>
		/// <returns></returns>
		public SortedList GetSortedList()
		{
			return list;
		}

		/// <summary>
		/// Gets or sets the history disk info list.
		/// </summary>
		public HistoryDiskInfo[] HistoryDiskInfoList
		{
			get
			{
				HistoryDiskInfo[] d = (HistoryDiskInfo[])(new ArrayList(list.Values)).ToArray(typeof(HistoryDiskInfo));
				return d;
			}
			set
			{
				if ( value != null )
				{
					foreach ( HistoryDiskInfo item in value )
					{
						list.Add(item.Url, item);
					}
				}
			}
		}
	}
}
