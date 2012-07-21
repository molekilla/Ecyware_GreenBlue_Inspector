// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: Mars 2003
using System;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Contains the ToolbarItem class.
	/// </summary>
	public class ToolbarItem : MenuItem
	{
		//private MenuItemCollection _submenu = null;
		private bool _isDropDown = false;
		private EventHandler _dropDownDelegate = null;

		/// <summary>
		/// Creates a new ToolbarItem.
		/// </summary>
		public ToolbarItem()
		{
		}

		/// <summary>
		/// Gets or sets the drop down delegate.
		/// </summary>
		public EventHandler DropDownDelegate
		{
			get
			{
				return _dropDownDelegate;
			}
			set
			{
				_dropDownDelegate = value;
			}
		}


		/// <summary>
		/// Gets or sets the drop down value.
		/// </summary>
		public bool IsDropDown
		{
			get
			{
				return _isDropDown;
			}
			set
			{
				_isDropDown = value;
			}
		}
	}
}
