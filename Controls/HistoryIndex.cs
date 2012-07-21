using System;
using System.Collections;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Summary description for HistoryIndex.
	/// </summary>
	public class HistoryIndex
	{
		SortedList list = new SortedList();

		public HistoryIndex()
		{
		}

		public void AddHistoryDiskInfo(string url, HistoryDiskInfo diskInfo)
		{
			list.Add(url, diskInfo);
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
