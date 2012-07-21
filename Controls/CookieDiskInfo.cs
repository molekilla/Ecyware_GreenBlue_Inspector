using System;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Contains a definition for a disk based cookie info.
	/// </summary>
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
}
