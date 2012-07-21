// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: November 2003
using System;
using System.Windows.Forms;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// A MenuItem type that is used to load menus in the GUI.
	/// </summary>	
	public class MenuItem
	{

		string _name = "";
		string _text = "";
		bool _enabled=true;
		bool _visible=true;
		bool _delimiter=false;
		bool _toggle = false;
		int _imageIndex = -1;
		Shortcut _shortcut = Shortcut.None;

		EventHandler _clickDelegate=null;
		EventHandler _checkedChangedDelegate = null;

		#region Constructors
		/// <summary>
		/// Creates a new MenuItem.
		/// </summary>
		public MenuItem()
		{
		}

		/// <summary>
		/// Creates a new MenuItem.
		/// </summary>
		/// <param name="name"> The name of the menu.</param>
		/// <param name="text"> The text of the menu.</param>
		public MenuItem(string name, string text)
		{
			this.Name=name;
			this.Text = text;
		}

		/// <summary>
		/// Creates a new MenuItem.
		/// </summary>
		/// <param name="name"> The name of the menu.</param>
		/// <param name="text"> The text of the menu.</param>
		/// <param name="enabled"> The enabled setting.</param>
		public MenuItem(string name, string text, bool enabled)
		{
			this.Name=name;
			this.Text = text;
			this.Enabled=enabled;
		}

		/// <summary>
		/// Creates a new MenuItem.
		/// </summary>
		/// <param name="name"> The name of the menu.</param>
		/// <param name="text"> The text of the menu.</param>
		/// <param name="enabled"> The enabled setting.</param>
		/// <param name="visible"> The visible setting.</param>
		public MenuItem(string name, string text, bool enabled,bool visible)
		{
			this.Name=name;
			this.Text = text;
			this.Enabled=enabled;
			this.Visible=visible;
		}

		/// <summary>
		/// Creates a new MenuItem.
		/// </summary>
		/// <param name="name"> The name of the menu.</param>
		/// <param name="text"> The text of the menu.</param>
		/// <param name="enabled"> The enabled setting.</param>
		/// <param name="visible"> The visible setting.</param>
		/// <param name="handler"> The click event handler.</param>
		public MenuItem(string name, string text, bool enabled,bool visible,EventHandler handler)
		{
			this.Name=name;
			this.Text = text;
			this.Enabled=enabled;
			this.Visible=visible;
			this.ClickDelegate=handler;
		}

		/// <summary>
		/// Creates a new MenuItem.
		/// </summary>
		/// <param name="name"> The name of the menu.</param>
		/// <param name="text"> The text of the menu.</param>
		/// <param name="enabled"> The enabled setting.</param>
		/// <param name="visible"> The visible setting.</param>
		/// <param name="delimiter"> The delimiter.</param>
		/// <param name="handler"> The click event handler.</param>
		public MenuItem(string name, string text, bool enabled,bool visible,bool delimiter,EventHandler handler)
		{
			this.Name=name;
			this.Text = text;
			this.Enabled=enabled;
			this.Visible=visible;
			this.Delimiter=delimiter;
			this.ClickDelegate=handler;
		}

		/// <summary>
		/// Creates a new MenuItem.
		/// </summary>
		/// <param name="name"> The name of the menu.</param>
		/// <param name="text"> The text of the menu.</param>
		/// <param name="handler"> The click event handler.</param>
		public MenuItem(string name, string text,EventHandler handler)
		{
			this.Name=name;
			this.Text = text;
			this.ClickDelegate=handler;
		}

		#endregion

		/// <summary>
		/// Gets or sets the Click delegate.
		/// </summary>
		public EventHandler ClickDelegate
		{
			get
			{
				return _clickDelegate;
			}
			set
			{
				_clickDelegate = value;
			}
		}

		/// <summary>
		/// Gets or sets the image index.
		/// </summary>
		public int ImageIndex
		{
			get
			{
				return _imageIndex;
			}
			set
			{
				_imageIndex = value;
			}
		}
		/// <summary>
		/// Gets or sets the CheckedChanged delegate.
		/// </summary>
		public EventHandler CheckedChangedDelegate
		{
			get
			{
				return _checkedChangedDelegate;
			}
			set
			{
				_checkedChangedDelegate = value;
			}
		}

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
		/// Gets or sets the toggle value.
		/// </summary>
		public bool Toggle
		{
			get
			{
				return _toggle;
			}
			set
			{
				_toggle = value;
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
		/// The delimiter for the menu.
		/// </summary>
		public bool Delimiter
		{
			get
			{
				return _delimiter;
			}
			set
			{
				_delimiter = value;
			}
		}

	}
}
