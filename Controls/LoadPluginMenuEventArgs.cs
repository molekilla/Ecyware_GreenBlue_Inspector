// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003

using System;
using Ecyware.GreenBlue.Controls;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Contains the LoadPlugunMenuEventArgs class.
	/// </summary>
	public sealed class LoadPluginMenuEventArgs:EventArgs
	{
		MenuRootHashtable _menuroot;

		/// <summary>
		/// Creates a new LoadPluginMenuEventArgs.
		/// </summary>
		public LoadPluginMenuEventArgs()
		{
		}

		/// <summary>
		/// Creates a new LoadPluginMenuEventArgs.
		/// </summary>
		/// <param name="menu"> The root menus.</param>
		public LoadPluginMenuEventArgs(MenuRootHashtable menu)
		{
			this.MenuRoot = menu;
		}		

		/// <summary>
		/// Gets or sets the root menus.
		/// </summary>
		public MenuRootHashtable MenuRoot
		{
			get
			{
				return _menuroot;
			}
			set
			{
				_menuroot = value;
			}
		}
	}
}
