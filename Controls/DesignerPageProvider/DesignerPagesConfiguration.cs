// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using Ecyware.GreenBlue.Configuration;
using System.Collections;
using System.Windows.Forms;

namespace Ecyware.GreenBlue.Controls.DesignerPageProvider
{
	/// <summary>
	/// Summary description for DesignerPages.
	/// </summary>
	public class DesignerPagesConfiguration
	{
		private ArrayList _pages = new ArrayList();

		/// <summary>
		/// Creates a DesignerPages type.
		/// </summary>
		public DesignerPagesConfiguration()
		{
		}

		/// <summary>
		/// Gets or sets the designer pages.
		/// </summary>
		public DesignerPage[] Pages
		{
			get
			{
				return (DesignerPage[])_pages.ToArray(typeof(DesignerPage));
			}
			set
			{
				if ( value != null )
					_pages.AddRange(value);
			}
		}
	}
}
