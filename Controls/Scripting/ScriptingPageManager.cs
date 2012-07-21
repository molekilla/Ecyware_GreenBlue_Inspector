using System;
using System.Windows.Forms;
using System.Collections;
using Ecyware.GreenBlue.Controls;
using Ecyware.GreenBlue.Controls.DesignerPageProvider;

namespace Ecyware.GreenBlue.Controls.Scripting
{
	/// <summary>
	/// Summary description for ScriptingPageManager.
	/// </summary>
	public class ScriptingPageManager : DesignerPageManager
	{
		private static Hashtable _cache = new Hashtable(5);
		
		/// <summary>
		/// Creates a new ScriptingPageManager.
		/// </summary>
		public ScriptingPageManager()
		{
		}

		/// <summary>
		/// Loads the pages for a designer.
		/// </summary>
		/// <param name="configurationSection"> The configuration section.</param>
		/// <returns> A BaseScriptingDataPage array.</returns>
		public new Ecyware.GreenBlue.Controls.BaseScriptingDataPage[] LoadDesignerPages(string configurationSection)
		{			
			if ( !_cache.ContainsKey(configurationSection) )
			{				
				ArrayList list = new ArrayList();

				UserControl[] controls = base.LoadDesignerPages (configurationSection);

				for (int i=0;i<controls.Length;i++)
				{
					BaseScriptingDataPage page = (BaseScriptingDataPage)controls[i];
					page.Caption = base.DesignerPages.Pages[i].Name;
					list.Add(page);
				}

				_cache.Add(configurationSection, (BaseScriptingDataPage[])list.ToArray(typeof(BaseScriptingDataPage)) );
			}	

			return(BaseScriptingDataPage[])_cache[configurationSection];
		}

	}
}
