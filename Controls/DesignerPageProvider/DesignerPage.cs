// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using Ecyware.GreenBlue.Configuration;

namespace Ecyware.GreenBlue.Controls.DesignerPageProvider
{
	/// <summary>
	/// Summary description for DesignerPage.
	/// </summary>
	public class DesignerPage : Provider
	{
		private string _name;

		/// <summary>
		/// Creates a new Designer Page.
		/// </summary>
		public DesignerPage()
		{
		}

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}
	}
}
