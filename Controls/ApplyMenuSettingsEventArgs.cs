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
	/// Contains the logic for the ApplyMenuSettingsEventArgs.
	/// </summary>
	public sealed class ApplyMenuSettingsEventArgs : EventArgs
	{
		MenuItemCollection _menus=new MenuItemCollection();

		/// <summary>
		/// Creates a new ApplyMenuSettingsEventArgs.
		/// </summary>
		public ApplyMenuSettingsEventArgs()
		{
		}

		/// <summary>
		/// Creates a new ApplyMenuSettingsEventArgs.
		/// </summary>
		/// <param name="rootShortcut"> The menu shortcut.</param>
		/// <param name="menu"> The menu items.</param>
		public ApplyMenuSettingsEventArgs(Shortcut rootShortcut,MenuItemCollection menu)
		{
			this.MenuItems = menu;
		}

		Shortcut _s;

		/// <summary>
		/// Gets or sets the shortcut.
		/// </summary>
		public Shortcut RootShortcut
		{
			get
			{
				return _s;
			}
			set
			{
				_s = value;
			}
		}

		/// <summary>
		/// Gets or sets the menu items.
		/// </summary>
		public MenuItemCollection MenuItems
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
