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
	/// Contains the logic for the ApplyToolbarSettingsEventArgs.
	/// </summary>
	public sealed class ApplyToolbarSettingsEventArgs:EventArgs
	{
		private ToolbarItem _item = null;

		/// <summary>
		/// Creates a new ApplyToolbarSettingsEventArgs.
		/// </summary>
		public ApplyToolbarSettingsEventArgs()
		{
		}

		/// <summary>
		/// Creates a new ApplyToolbarSettingsEventArgs.
		/// </summary>
		/// <param name="item"> The toolbar item to add.</param>
		public ApplyToolbarSettingsEventArgs(ToolbarItem item)
		{
			this.ToolbarCommand = item;
		}

		/// <summary>
		/// Gets or sets the toolbar command.
		/// </summary>
		public ToolbarItem ToolbarCommand
		{
			get
			{
				return _item;
			}
			set
			{
				_item = value;
			}
		}
	}
}
