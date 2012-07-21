// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: November 2003
using System;
using System.Windows.Forms;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// MenuRoot contains MenuItems to be loaded in GUI.
	/// </summary>
	public class MenuRoot
	{
		MenuItemCollection _menuItems;
		string _name;
		string _text;
		Shortcut _shortcut = Shortcut.None;
		bool _enabled = true;
		bool _visible = true;

		#region Constructors
		/// <summary>
		/// Creates a new MenuRoot.
		/// </summary>
		public MenuRoot()
		{
		}

		/// <summary>
		/// Creates a new MenuRoot.
		/// </summary>
		/// <param name="name"> The name of the menu.</param>
		/// <param name="text"> The text of the menu.</param>
		public MenuRoot(string name, string text)
		{
			this.Name=name;
			this.Text = text;
		}

		/// <summary>
		/// Creates a new MenuRoot.
		/// </summary>
		/// <param name="name"> The name of the menu.</param>
		/// <param name="text"> The text of the menu.</param>
		/// <param name="shortcut"> The shortcut of the menu.</param>
		public MenuRoot(string name, string text, Shortcut shortcut)
		{
			this.Name=name;
			this.Text = text;
			this.Shortcut=shortcut;
		}
		#endregion

		/// <summary>
		/// Gets or sets the shortcut.
		/// </summary>
		public Shortcut Shortcut
		{
			get
			{
				return _shortcut;
			}
			set
			{
				_shortcut = value;
			}
		}
		/// <summary>
		/// The name of the control.
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

		/// <summary>
		/// The text to display in menu.
		/// </summary>
		public string Text
		{
			get
			{
				return _text;
			}
			set
			{
				_text =value;
			}
		}

		/// <summary>
		/// Enables the menu on or off
		/// </summary>
		public bool Enabled
		{
			get
			{
				return _enabled;
			}
			set
			{
				_enabled = value;
			}
		}

		/// <summary>
		/// Displays the menu.
		/// </summary>
		public bool Visible
		{
			get
			{
				return _visible;
			}
			set
			{
				_visible = value;
			}
		}


		/// <summary>
		/// The MenuItem collection
		/// </summary>
		public MenuItemCollection MenuItems
		{
			get
			{
				return _menuItems;
			}
			set
			{
				_menuItems = value;
			}
		}

	}
}
