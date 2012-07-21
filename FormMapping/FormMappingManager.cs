using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Ecyware.GreenBlue.HtmlDom;

namespace Ecyware.GreenBlue.FormMapping
{
	/// <summary>
	/// Summary description for FormMappingCommand.
	/// </summary>
	public sealed class FormMappingManager
	{
		string indexData = Application.UserAppDataPath + @"\formmap.dat";
		string formMapsFolder = Application.UserAppDataPath + @"\formmaps";
		SortedList formMapIndex = null;

		public FormMappingManager()
		{
			LoadFormMaps();
		}

		/// <summary>
		/// Loads the form mapping index in memory.
		/// </summary>
		public void LoadFormMaps()
		{
			// check if directory exists else create new
			if ( !Directory.Exists(formMapsFolder) )
			{
				Directory.CreateDirectory(formMapsFolder);
			}

			// load index data
			if ( File.Exists(indexData) )
			{
				try
				{
					FileStream stm = File.Open(indexData,FileMode.Open);
					BinaryFormatter bf = new BinaryFormatter();
					formMapIndex = (SortedList)bf.Deserialize(stm);
					stm.Close();
				}
				catch
				{
					throw;
				}
			} 
			else 
			{
				formMapIndex = new SortedList();
			}

		}

		
		#region Disk Serialization Methods
		private void AddFormMapping(Uri siteUri, FormMapData data)
		{
			string folderName = siteUri.Authority + siteUri.Port;
			string uriAndPort = siteUri.Authority + ":" + siteUri.Port;
			string dir = formMapsFolder + @"\" + folderName;

			if ( !Directory.Exists(dir) )
			{
				Directory.CreateDirectory(dir);
			}

			// search index data for site root existence
			FormMappingDiskInfo diskInfo = (FormMappingDiskInfo)formMapIndex[uriAndPort];

			// add new to disk
			if ( diskInfo != null )
			{
				// get form mapping from site url
				diskInfo = (FormMappingDiskInfo)formMapIndex[uriAndPort + siteUri.AbsolutePath];				

				// add file to disk
				string filePath = UpdateFormMapping(dir,siteUri.AbsolutePath,data);

				if ( diskInfo == null )
				{				
					diskInfo = new FormMappingDiskInfo(filePath);

					// add reference to Index data
					formMapIndex.Add(uriAndPort + siteUri.AbsolutePath, diskInfo);
				}
			} 
			else 
			{						
				// add file to disk
				string filePath = UpdateFormMapping(dir,siteUri.AbsolutePath,data);

				diskInfo = new FormMappingDiskInfo(filePath);

				// add reference to Index data
				formMapIndex.Add(uriAndPort + siteUri.AbsolutePath, diskInfo);

				// add root reference to Index data
				formMapIndex.Add(uriAndPort,new FormMappingDiskInfo(""));
			}

			UpdateIndexData();
		}
		
		// TODO: Check for errors while updating
		private void UpdateIndexData()
		{
			FileStream stm = File.Open(indexData,FileMode.Create);
			BinaryFormatter bf = new BinaryFormatter();
			bf.Serialize(stm, formMapIndex);
			stm.Close();
		}


		// TODO: Check for errors while loading
		private FormMapData OpenFormMapping(string path)
		{
			FileStream stm = null;
			FormMapData resp = null;
			if ( File.Exists(path) )
			{
				stm = File.Open(path,FileMode.Open);
				BinaryFormatter bf = new BinaryFormatter();
				resp = (FormMapData)bf.Deserialize(stm);
				stm.Close();
			}
			return resp;
		}
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

		// TODO: Check for errors while updating
		private string UpdateFormMapping(string dirPath,string absPath, FormMapData data)
		{
			string newPath = absPath;
			if ( !File.Exists(newPath) )
			{
				newPath = GetFilePath(dirPath, newPath);
			}

			FileStream stm = null;
			if ( File.Exists(newPath) )
			{
				stm = File.Open(newPath,FileMode.Create);
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

		#endregion

		public void SaveFormMapping(Uri uri, FormMappingDataRelationList relations, Hashtable postDataValues, HtmlFormTag form)
		{

			// create new formMapData
			FormMapData formMapData = new FormMapData();
			formMapData.FormTag = form;
			formMapData.PostData = postDataValues;
			formMapData.FormMappingRelations = relations;

			// Save .gbmap file.
			this.AddFormMapping(uri, formMapData);
		}

		public FormMapData GetFormMapData(Uri url)
		{
			string name = url.Authority + ":" + url.Port + url.AbsolutePath;
			
			FormMappingDiskInfo diskInfo = (FormMappingDiskInfo)formMapIndex[name];
			if ( diskInfo == null )
			{
				return null;
			} 
			else 
			{
				return this.OpenFormMapping(diskInfo.Path);
			}
		}

		public FormMapData SetRelationValues(FormMapData mapData)
		{
			int i=0;
			foreach ( FormMappingDataRelation relation in mapData.FormMappingRelations )
			{
				SetValue(mapData.FormTag[mapData.FormTag.Name][i],relation.CurrentValue);
				i++;
			}

			return mapData;
		}

		private void SetValue(HtmlTagBase tag, string value)
		{
			if (tag is HtmlInputTag)
			{
				HtmlInputTag input=(HtmlInputTag)tag;
				input.Value = value;
			}

			if (tag is HtmlButtonTag)
			{
				HtmlButtonTag button = (HtmlButtonTag)tag;
				button.Value = value;
			}

			if (tag is HtmlSelectTag)
			{
				HtmlSelectTag select = (HtmlSelectTag)tag;
				select.Value = value;
			}
					
			if (tag is HtmlTextAreaTag)
			{
				HtmlTextAreaTag textarea=(HtmlTextAreaTag)tag;
				textarea.Value = value;
			}		
		}
	}
}
