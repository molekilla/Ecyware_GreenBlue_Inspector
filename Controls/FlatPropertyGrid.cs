// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004

using System;
using System.Windows.Forms;
using System.ComponentModel;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// A property grid just like the one in VS.NET.
	/// </summary>
	public class FlatPropertyGrid : PropertyGrid
	{
		/// <summary>
		/// Creates a new FlatPropertyGrid.
		/// </summary>
		public FlatPropertyGrid() : base()
		{
			base.DrawFlatToolbar = true;
		}
	}
}
