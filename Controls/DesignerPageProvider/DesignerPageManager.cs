// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using System.Collections;
using System.Reflection;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Configuration.XmlTypeSerializer;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace Ecyware.GreenBlue.Controls.DesignerPageProvider
{

	/// <summary>
	/// Contains the designer page manager.
	/// </summary>
	public class DesignerPageManager
	{
		private DesignerPagesConfiguration _pages;

		/// <summary>
		/// Creates a new DesignerPageManager.
		/// </summary>
		public DesignerPageManager()
		{			
		}

		/// <summary>
		/// Gets the designer pages.
		/// </summary>
		protected DesignerPagesConfiguration DesignerPages
		{
			get
			{
				return _pages;
			}
			set
			{
				_pages = value;
			}
		}
		/// <summary>
		/// Loas the designer pages.
		/// </summary>
		/// <param name="configurationSection"></param>
		/// <returns></returns>
		protected UserControl[] LoadDesignerPages(string configurationSection)
		{
			ArrayList controls = new ArrayList();

			// Get Configuration
			DesignerPagesConfiguration designerPages = (DesignerPagesConfiguration)ConfigManager.Read(configurationSection, true);
			_pages = designerPages;

			foreach ( DesignerPage page in designerPages.Pages )
			{
				// Load Types
				Type type = Type.GetType( page.Type );

				// Insert the type into the cache
				Type[] paramTypes = new Type[0];				
				ConstructorInfo cinfo = type.GetConstructor(paramTypes);
			
				// Load control
				object[] paramArray = new object[0];
				UserControl control = (UserControl)cinfo.Invoke(paramArray);
				
				controls.Add(control);				
			}

			return (UserControl[])controls.ToArray(typeof(UserControl));
		}

	}
}
