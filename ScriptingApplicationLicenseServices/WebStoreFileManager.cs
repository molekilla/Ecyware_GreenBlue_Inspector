using System;
using System.IO;
using System.Text;
using Ecyware.GreenBlue.LicenseServices.Client;

namespace Ecyware.GreenBlue.LicenseServices
{
	/// <summary>
	/// Summary description for WebStoreFileManager.
	/// </summary>
	internal class WebStoreFileManager
	{
		public WebStoreFileManager()
		{
		}


		/// <summary>
		/// Loads the application from file.
		/// </summary>
		/// <param name="publisher"> The publisher username.</param>
		/// <param name="message"> The WebStoreRequestMessage message.</param>
		public static string LoadApplication(string publisher, WebStoreRequestMessage message)
		{
			// format is http://www.ecyware.com/WebStore/username/applicationID
			string parentPath = System.Web.HttpContext.Current.Server.MapPath("~/WebStore");
			string userStore = parentPath + "\\" + publisher;

			StringBuilder url = new StringBuilder();
			url.Append(parentPath);
			url.Append("\\");
			url.Append(publisher);
			url.Append("\\");
			url.Append(message.ApplicationID);
			url.Append(".gbscr");

			string file = url.ToString();
			string result = string.Empty;

			if ( File.Exists(file) )
			{
				FileInfo info = new FileInfo(file);
				FileStream fs = new FileStream(file, FileMode.Open,FileAccess.Read);
				byte[] data = new byte[info.Length];
				fs.Read(data,0,data.Length);
				fs.Flush();
				fs.Close();

				// Convert to base64
				result = Convert.ToBase64String(data, 0, data.Length);
			}

			return result;
		}

		/// <summary>
		/// Removes the application to file.
		/// </summary>
		/// <param name="publisher"> The publisher username.</param>
		/// <param name="message"> The WebStoreRequestMessage message.</param>
		public static void RemoveApplication(string publisher, WebStoreRequestMessage message)
		{
			// format is http://www.ecyware.com/WebStore/username/applicationID
			string parentPath = System.Web.HttpContext.Current.Server.MapPath("~/WebStore");
			string userStore = parentPath + "\\" + publisher;

			if ( !Directory.Exists(userStore) )
			{
				Directory.CreateDirectory(userStore);
			}

			StringBuilder url = new StringBuilder();
			url.Append(parentPath);
			url.Append("\\");
			url.Append(publisher);
			url.Append("\\");
			url.Append(message.ApplicationID);
			url.Append(".gbscr");

			if ( File.Exists(url.ToString()) )
			{
				File.Delete(url.ToString());
			}
		}

		/// <summary>
		/// Saves the application to file.
		/// </summary>
		/// <param name="publisher"> The publisher username.</param>
		/// <param name="message"> The WebStoreRequestMessage message.</param>
		public static void SaveApplication(string publisher, WebStoreRequestMessage message)
		{
			// format is http://www.ecyware.com/WebStore/username/applicationID
			string parentPath = System.Web.HttpContext.Current.Server.MapPath("~/WebStore");
			string userStore = parentPath + "\\" + publisher;

			if ( !Directory.Exists(userStore) )
			{
				Directory.CreateDirectory(userStore);
			}

			StringBuilder url = new StringBuilder();
			url.Append(parentPath);
			url.Append("\\");
			url.Append(publisher);
			url.Append("\\");
			url.Append(message.ApplicationID);
			url.Append(".gbscr");
						
			FileStream fs = new FileStream(url.ToString(), FileMode.Create,FileAccess.ReadWrite);
			byte[] data = Convert.FromBase64String(message.ApplicationData);
			fs.Write(data,0,data.Length);
			fs.Flush();
			fs.Close();
		}
	}
}
