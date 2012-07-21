// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003
using System;
using System.Collections;
using System.Net;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.WebUnitTestManager;
using Ecyware.GreenBlue.Engine.HtmlCommand;
namespace Ecyware.GreenBlue.WebUnitTestManager
{

	/// <summary>
	/// Contains the DataTypesTester type.
	/// </summary>
	internal class DataTypesTester : HtmlFormUnitTestBase, IHtmlFormUnitTest
	{
		private DataType _selectedDataType;

		/// <summary>
		/// Creates a new DataTypesTester.
		/// </summary>
		/// <param name="args"> The data types tester arguments.</param>
		public DataTypesTester(DataTypesTesterArgs args)
		{
			this.UnitTestName = "DataTypesTester";
			this.SelectedDataType = args.SelectedDataType;
		}

		/// <summary>
		/// Gets or sets the selected data type.
		/// </summary>
		public DataType SelectedDataType
		{
			get
			{
				return _selectedDataType;
			}
			set
			{
				_selectedDataType = value;
			}
		}
		#region IHtmlFormUnitTest Members
		/// <summary>
		/// Fills the uri with tests.
		/// </summary>
		/// <param name="url"> The url.</param>
		/// <param name="uriType"> The WebServerUriType type.</param>
		/// <returns> The updated uri.</returns>
		public Uri FillUri(Uri url, WebServerUriType uriType)
		{		
			string buffer = String.Empty;

			switch ( this.SelectedDataType )
			{
				case DataType.Character:
					buffer = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
					break;
				case DataType.Numeric:
					buffer = "0123456789";
					break;
				case DataType.Null:
					buffer = "";
					break;
			}

			// copy url
			Uri temp = new Uri(url.ToString());

			UriGenerator generator = new UriGenerator();

			try
			{
				temp = generator.GenerateUri(uriType, temp, buffer);
				return temp;
			}
			catch (Exception ex)
			{
				ExceptionHandler.RegisterException(ex);
				return url;
			}
		}

		/// <summary>
		/// Fills the cookie collection with tests.
		/// </summary>
		/// <param name="cookies"> The cookie collection.</param>
		/// <returns> The updated cookie collection.</returns>
		public CookieCollection FillCookies(CookieCollection cookies)
		{
			string buffer = String.Empty;

			switch ( this.SelectedDataType )
			{
				case DataType.Character:
					buffer = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
					break;
				case DataType.Numeric:
					buffer = "0123456789";
					break;
				case DataType.Null:
					buffer = "";
					break;
			}


			CookieCollection tempCookies = new CookieCollection();

			foreach ( Cookie cky in cookies )
			{
				Cookie temp = new Cookie(cky.Name,"");
				temp.Comment = buffer;
				temp.CommentUri = cky.CommentUri;
				temp.Domain = cky.Domain;
				temp.Expired = cky.Expired;
				temp.Expires = cky.Expires;
				//temp.Name = temp.Name;
				temp.Path = cky.Path;
				temp.Port = cky.Port;
				temp.Secure = cky.Secure;
				temp.Value = buffer;
				temp.Version = cky.Version;

				tempCookies.Add(temp);
			}

			return tempCookies;
		}
		/// <summary>
		/// Fills the post data with tests.
		/// </summary>
		/// <param name="postData"> The post data hastable.</param>
		/// <returns> The updated post data hashtable.</returns>
		public PostDataCollection FillPostData(PostDataCollection postData)
		{
			string buffer = String.Empty;

			switch ( this.SelectedDataType )
			{
				case DataType.Character:
					buffer = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
					break;
				case DataType.Numeric:
					buffer = "0123456789";
					break;
				case DataType.Null:
					buffer = "";
					break;
			}

			//ArrayList keys = new ArrayList(postData.Keys);

			for (int i=0;i<postData.Keys.Count;i++)
			{
				ArrayList values = postData[postData.Keys[i]];

				for (int j=0;j<values.Count;j++)
				{
					values[j] = buffer;
				}
			}

			return postData;
		}

		/// <summary>
		/// Fills the form tag with tests.
		/// </summary>
		/// <param name="formTag"> The HtmlFormTag.</param>
		/// <returns> The updated HtmlFormTag.</returns>
		public HtmlFormTag FillForm(HtmlFormTag formTag)
		{

			string buffer = String.Empty;

			switch ( this.SelectedDataType )
			{
				case DataType.Character:
					buffer = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
					break;
				case DataType.Numeric:
					buffer = "0123456789";
					break;
				case DataType.Null:
					buffer = "";
					break;
			}

			for (int i=0;i<formTag.Count;i++)
			{
				HtmlTagBaseList controlArray = (HtmlTagBaseList)((DictionaryEntry)formTag[i]).Value;

				#region inner foreach loop
				foreach (HtmlTagBase tag in controlArray)
				{
					if (tag is HtmlInputTag)
					{
						HtmlInputTag input=(HtmlInputTag)tag;
						input.Value = buffer;
					}
					if (tag is HtmlButtonTag)
					{
						HtmlButtonTag button = (HtmlButtonTag)tag;
						button.Value=buffer;
					}
					if (tag is HtmlSelectTag)
					{
						HtmlSelectTag select = (HtmlSelectTag)tag;
						if  ( select.Multiple )
						{
							foreach (HtmlOptionTag opt in select.Options )
							{
								//HtmlOptionTag opt = tag;
								if ( opt.Selected ) 
								{
									opt.Value=buffer;
								}
							}
						} 
						else 
						{							
							select.Value = buffer;
						}
					}
					
					if (tag is HtmlTextAreaTag)
					{
						HtmlTextAreaTag textarea=(HtmlTextAreaTag)tag;
						textarea.Value=buffer;
					}
				}
				#endregion
			}

			return formTag;
		}

		#endregion
	}
}
