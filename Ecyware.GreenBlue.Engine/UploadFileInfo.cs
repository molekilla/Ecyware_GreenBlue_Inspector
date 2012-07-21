using System;
using System.Collections;
using System.IO;
using Ecyware.GreenBlue.Engine.HtmlDom;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Summary description for UploadFileInfo.
	/// </summary>
	public class UploadFileInfo
	{
		string _fileName;
		string _formFieldName;
		string _contentType;

		/// <summary>
		/// Creates a new UploadFileInfo.
		/// </summary>
		public UploadFileInfo()
		{
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

		/// <summary>
		/// Gets or sets the form field name.
		/// </summary>
		public string FormFieldName
		{
			get
			{
				return _formFieldName;
			}
			set
			{
				_formFieldName = value;
			}
		}

		/// <summary>
		/// Gets or sets the content type.
		/// </summary>
		public string ContentType
		{
			get
			{
				return _contentType;
			}
			set
			{
				_contentType = value;
			}
		}


		/// <summary>
		/// Upload the file infos.
		/// </summary>
		/// <param name="formTag"> The form tag to get the UploadFileInfo items.</param>
		/// <returns> An UploadFileInfo array.</returns>
		public static UploadFileInfo[] GetUploadFiles(HtmlFormTag formTag)
		{
			ArrayList list = new ArrayList();
			foreach ( HtmlTagBaseList tagBaseList in formTag.AllValues )
			{
				foreach ( HtmlTagBase tag in tagBaseList )
				{
					if ( tag is HtmlInputTag )
					{
						HtmlInputTag input = (HtmlInputTag)tag;

						if ( input.Type == HtmlInputType.File )
						{
							UploadFileInfo fileInfo = new UploadFileInfo();

							// get file name
							fileInfo.FormFieldName = input.Name;
							if ( input.Value == null )
							{
								input.Value = string.Empty;
							}

							fileInfo.FileName = input.Value.Trim('"').Trim('\0').Trim();

							if ( fileInfo.FileName.Length > 0 )
							{
								fileInfo.ContentType = AppLocation.GetMIMEType(fileInfo.FileName);
								list.Add(fileInfo);
							}
						}
					}
				}
			}

			return (UploadFileInfo[])list.ToArray(typeof(UploadFileInfo));
		}
	}
}
