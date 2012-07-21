// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Ecyware.GreenBlue.Controls
{
	public delegate void UpdateSessionEventHandler(object sender, UpdateSessionEventArgs e);
	public delegate void UpdateSessionRequestEventHandler(object sender, UpdateSessionRequestEventArgs e);
	public delegate void HttpGetPageEventHandler(object sender, RequestGetEventArgs e);

	/// <summary>
	/// Contains the BaseSessionUserControl class.
	/// </summary>
	public class BaseSessionDesignerUserControl  : UserControl
	{		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new BaseSessionDesignerUserControl.
		/// </summary>
		public BaseSessionDesignerUserControl()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}


		/// <summary>
		/// Updates the session request data.
		/// </summary>
		public virtual void UpdateSessionRequestData(){}

		/// <summary>
		/// Updates the session data.
		/// </summary>
		public virtual void UpdateSessionData(){}
	}
}
