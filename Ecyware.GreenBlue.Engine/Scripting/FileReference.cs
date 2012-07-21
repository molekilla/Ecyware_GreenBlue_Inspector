using System;

namespace Ecyware.GreenBlue.Engine.Scripting
{
	/// <summary>
	/// Summary description for FileReference.
	/// </summary>
	[Serializable]
	public class FileReference
	{
		private string _fileName;

		/// <summary>
		/// Creates a new FileReference.
		/// </summary>
		public FileReference()
		{
		}

		/// <summary>
		/// Creates a new FileReference.
		/// </summary>
		/// <param name="fileName"> The file name.</param>
		public FileReference(string fileName)
		{
			_fileName = fileName;
		}

		/// <summary>
		/// Gets or sets the file name.
		/// </summary>
		public string FileName
		{
			get
			{
				return _fileName;
			}
			set
			{
				_fileName = value;
			}
		}
	}
}
