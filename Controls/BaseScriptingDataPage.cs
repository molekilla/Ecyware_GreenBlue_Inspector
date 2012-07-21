// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.Scripting;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Contains the BaseSessionUserControl class.
	/// </summary>
	public class BaseScriptingDataPage  : UserControl
	{
		private ScriptingApplication _scripting;
		private int _requestIndex;
		private Icon _icon;
		private string _caption;
		private WebRequest _request;		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new BaseSessionDesignerUserControl.
		/// </summary>
		public BaseScriptingDataPage()
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

		[Description("Gets or sets the icon.")]
		public Icon Icon
		{
			get
			{
				return _icon;
			}
			set
			{
				_icon = value;
			}
		}
		/// <summary>
		/// Gets or sets the caption.
		/// </summary>
		[Description("Gets or sets the caption for the page.")]
		public string Caption
		{
			get
			{
				return _caption;
			}
			set
			{
				_caption = value;
			}
		}

		/// <summary>
		/// Gets the WebRequest.
		/// </summary>
		[Browsable(false)]
		public virtual WebRequest WebRequest
		{
			get
			{
				return _request;
			}
		}
		/// <summary>
		/// Gets the ScriptingData.
		/// </summary>
		[Browsable(false)]
		public virtual ScriptingApplication SessionScripting
		{
			get
			{
				return _scripting;
			}
		}

		/// <summary>
		/// Gets the selected web request index.
		/// </summary>
		[Browsable(false)]
		public virtual int SelectedWebRequestIndex
		{
			get
			{
				return _requestIndex;
			}
		}
		/// <summary>
		/// Loads a WebRequest.
		/// </summary>
		/// <param name="request"> Loads a WebRequest.</param>
		public virtual void LoadRequest(int requestIndex, ScriptingApplication sessionScripting, WebRequest request)
		{

			_requestIndex = requestIndex;
			_scripting = sessionScripting;
			_request = request;
		}
	}
}
