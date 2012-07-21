using System;

namespace Ecyware.GreenBlue.FormMapping
{
	[Serializable]
	public class FormMappingDiskInfo
	{
		public string Path=String.Empty;
		
		public FormMappingDiskInfo()
		{
		}
		public FormMappingDiskInfo(string path)
		{
			this.Path = path;
		}
	}
}
