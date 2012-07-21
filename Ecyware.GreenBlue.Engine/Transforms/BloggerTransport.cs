// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2005
using Atomizer;
using System;
using System.Text;
using System.Reflection;
using System.Collections;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms;
using System.Data;
using System.Data.Odbc;
using Ecyware.GreenBlue.Engine.Transforms.Designers;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for DatabaseTransport.
	/// </summary>
	[Serializable]
	public class BloggerTransport : Transport
	{
		string _username = string.Empty;
		string _password = string.Empty;
		string _endpoint = string.Empty;
		string _title = string.Empty;
		string _url = string.Empty;
		string _app = string.Empty;
		string _category = string.Empty;
		string _selectedIndex = string.Empty;

		public BloggerTransport()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Gets or sets the UserName.
		/// </summary>
		public string UserName
		{
			get
			{
				return _username;
			}
			set
			{
				_username = value;
			}
		}

		/// <summary>
		/// Gets or sets the Password.
		/// </summary>
		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				_password = value;
			}
		}

		/// <summary>
		/// Gets or sets the Url.
		/// </summary>
		public string Url
		{
			get
			{
				return _url;
			}
			set
			{
				_url = value;
			}
		}


		/// <summary>
		/// Gets or sets the Endpoint.
		/// </summary>
		public string Endpoint
		{
			get
			{
				return _endpoint;
			}
			set
			{
				_endpoint = value;
			}
		}

		/// <summary>
		/// Gets or sets the ApplicationName.
		/// </summary>
		public string ApplicationName
		{
			get
			{
				return _app;
			}
			set
			{
				_app = value;
			}
		}

		/// <summary>
		/// Gets or sets the Title.
		/// </summary>
		public string Title
		{
			get
			{
				return _title;
			}
			set
			{
				_title = value;
			}
		}

		/// <summary>
		/// Gets or sets the Category.
		/// </summary>
		public string Category
		{
			get
			{
				return _category;
			}
			set
			{
				_category = value;
			}
		}

		/// <summary>
		/// Gets or sets the SelectedIndex.
		/// </summary>
		public string SelectedIndex
		{
			get
			{
				return _selectedIndex;
			}
			set
			{
				_selectedIndex = value;
			}
		}
//
//		public override Argument[] GetArguments()
//		{
//		
//			ArrayList arguments = new ArrayList();
//
//			Argument arg = new Argument();
//			arg.Name = "BloggerTransport.UserName";
//			arguments.Add(arg);
//
//			arg = new Argument();
//			arguments.Add("BloggerTransport.Password");
//			arguments.Add(arg);
//
//			arg = new Argument();
//			arguments.Add("BloggerTransport.Url");
//			arguments.Add(arg);
//
//			arg = new Argument();
//			arguments.Add("BloggerTransport.Endpoint");
//			arguments.Add(arg);
//
//			arg = new Argument();
//			arguments.Add("BloggerTransport.ApplicationName");
//			arguments.Add(arg);
//
//			arg = new Argument();
//			arguments.Add("BloggerTransport.Title");
//			arguments.Add(arg);
//
//			arg = new Argument();
//			arguments.Add("BloggerTransport.Category");
//			arguments.Add(arg);
//
//			arg = new Argument();
//			arguments.Add("BloggerTransport.SelectedIndex");
//			arguments.Add(arg);
//
//			if ( arguments.Count == 0 )
//			{
//				return null;
//			} 
//			else 
//			{
//				return (Argument[])arguments.ToArray(typeof(Argument));
//			}
//		}


		public override void Send(string[] payload)
		{
			string[] values = payload;
			
			// Mail sending
			try 
			{

				generatorType generator = new generatorType();
				generator.url = _url;
				generator.Value = _app;
				generator.version = "1.0";

				Atom atom = 
					Atom.Create(new Uri(_endpoint), generator, _username, _password);

				//get the user's services from the endpoint, these correspond to blogs
				service[] services = atom.GetServices();			
				service selectedService = services[0];

				// Get service index
				if ( _selectedIndex != string.Empty )
				{
					selectedService = services[Int32.Parse(_selectedIndex)];
				}

				// post
				atom.PostBlogEntry(selectedService.postURL, _title, values[0], _category);
			} 
			catch
			{
				throw;
				// nothing
			} 
			finally
			{
			}

		}
	}
}
