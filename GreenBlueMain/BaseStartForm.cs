// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003-July 2004
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
//using Xheo.Licensing;

namespace Ecyware.GreenBlue.GreenBlueMain
{

	/// <summary>
	/// Contains the definition for the BaseStartForm type.
	/// </summary>
	//[ LicenseProvider( typeof( Xheo.Licensing.ExtendedLicenseProvider ) ) ]
	public class BaseStartForm : System.Windows.Forms.Form
	{
		//private License _license	= null;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Base Class for StartForm.
		/// </summary>
		public BaseStartForm()
		{
			//_license = LicenseManager.Validate( typeof( BaseStartForm ), this );
		}

		/// <summary>
		/// Adds a workspace.
		/// </summary>
		/// <param name="control"> UserControl to add.</param>
		/// <param name="name"> Name.</param>
		public virtual void AddWorkspace(UserControl control,string name)
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
//				if( _license != null )
//				{
//					_license.Dispose();
//					_license = null;
//				}
			}
			base.Dispose( disposing );
		}
	}
}
