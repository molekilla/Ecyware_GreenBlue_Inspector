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

namespace Ecyware.GreenBlue.SessionScriptingDesigner
{
	/// <summary>
	/// Contains the definition for the BaseStartForm type.
	/// </summary>
	public class BaseStartForm : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Base Class for StartForm.
		/// </summary>
		public BaseStartForm()
		{
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
			}
			base.Dispose( disposing );
		}

		private void InitializeComponent()
		{
			// 
			// BaseStartForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(358, 266);
			this.Name = "BaseStartForm";

		}		
	}
}
