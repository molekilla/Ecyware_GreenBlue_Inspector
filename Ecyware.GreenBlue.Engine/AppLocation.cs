// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Security.Permissions;
using System.IO;
using Microsoft.Win32;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Use for file and directory location.
	/// </summary>
	public sealed class AppLocation
	{
		private AppLocation()
		{
		}

		/// <summary>
		/// Gets the MIME Type of the file.
		/// </summary>
		/// <param name="filePath"> The file path.</param>
		/// <returns> Returns a string representing the MIME type.</returns>
		public static string GetMIMEType(string filePath)
		{
			RegistryPermission regPerm = new RegistryPermission(RegistryPermissionAccess.Read,"\\\\HKEY_CLASSES_ROOT");
			RegistryKey classesRoot = Registry.ClassesRoot;
			FileInfo fi = new FileInfo(filePath);
			string dotExt = fi.Extension.ToUpper();
			RegistryKey typeKey = classesRoot.OpenSubKey("MIME\\Database\\Content Type");			
		
			string result = string.Empty;

			foreach ( string keyname in typeKey.GetSubKeyNames() )
			{
				RegistryKey curKey = classesRoot.OpenSubKey("MIME\\Database\\Content Type\\" + keyname);
							
				if ( Convert.ToString(curKey.GetValue("Extension")).ToUpper() == dotExt )
				{
					result = keyname;
				}
			}

			return result;
		}

		/// <summary>
		/// Get Common Folder.
		/// </summary>
		public static string CommonFolder
		{
			get
			{
				return Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles) + "\\Ecyware Solutions\\Ecyware GreenBlue Services Designer";
			}
		}

		/// <summary>
		/// Get the Internet Temp Folder.
		/// </summary>
		public static string InternetTemp
		{
			get
			{
				return Environment.GetFolderPath(Environment.SpecialFolder.InternetCache) + "\\Ecyware";
			}
		}


		/// <summary>
		/// Gets the application name.
		/// </summary>
		public static string ApplicationName
		{
			get
			{
				return "Ecyware GreenBlue Services Designer";
			}
		}
		/// <summary>
		/// Gets the document folder.
		/// </summary>
		public static string DocumentFolder
		{
			get
			{
				return Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\My GreenBlue Scripting Applications";
			}
		}
	}
}
