// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Windows.Forms;
using Ecyware.GreenBlue.Controls;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Contains the logic for the ApplyMenuRootSettingsEventArgs.
	/// </summary>
	public sealed class ApplyMenuRootSettingsEventArgs : EventArgs
	{
		MenuRootHashtable _menus=new MenuRootHashtable();

		/// <summary>
		/// Creates a new ApplyMenuRootSettingsEventArgs.
		/// </summary>
		public ApplyMenuRootSettingsEventArgs()
		{
		}

		/// <summary>
		/// Creates a new ApplyMenuRootSettingsEventArgs.
		/// </summary>
		/// <param name="rootShortcut"> The menu shortcut.</param>
		/// <param name="menu"> The root menus.</param>
		public ApplyMenuRootSettingsEventArgs(Shortcut rootShortcut,MenuRootHashtable menu)
		{
			this.MenuRootItems = menu;
		}		

		/// <summary>
		/// Gets or sets the root menus.
		/// </summary>
		public MenuRootHashtable MenuRootItems
		{
			get
			{
				return _menus;
			}
			set
			{
				_menus = value;
			}
		}
	}
}