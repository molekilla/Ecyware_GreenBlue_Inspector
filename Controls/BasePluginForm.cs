// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using Ecyware.GreenBlue.Controls;
//using Xheo.Licensing;


namespace Ecyware.GreenBlue.Controls
{

	/// <summary>
	/// Delegate that is used to apply settings for toolbar items.
	/// </summary>
	public delegate void ApplyToolbarSettingsEventHandler(object sender, ApplyToolbarSettingsEventArgs e);

	/// <summary>
	/// Delegate that is used to apply settings for menu items.
	/// </summary>
	public delegate void ApplyMenuSettingsEventHandler(object sender, ApplyMenuSettingsEventArgs e);

	/// <summary>
	/// Delegate that is used to apply settings for menu root items.
	/// </summary>
	public delegate void ApplyMenuRootSettingsEventHandler(object sender, ApplyMenuRootSettingsEventArgs e);

	/// <summary>
	/// Delegate that is used to load the menus in the start form.
	/// </summary>
	public delegate void LoadPluginMenusEventHandler(object sender, LoadPluginMenuEventArgs e);

	/// <summary>
	/// Delegates that is used to set the statusbar properties
	/// </summary>
	public delegate void ChangeStatusBarEventHandler(object sender, ChangeStatusBarEventArgs e );

	/// <summary>
	/// Delegates that is used to start the progress bar.
	/// </summary>
	public delegate void StartProgressBarEventHandler(object sender, ProgressBarControlEventArgs e);

	/// <summary>
	/// Delegates that is used to stop the progress bar.
	/// </summary>
	public delegate void StopProgressBarEventHandler(object sender, ProgressBarControlEventArgs e);

	/// <summary>
	/// Delegates that is use to exit the application.
	/// </summary>
	public delegate void GBInspectorExitEventHandler(object sender, EventArgs e);

	/// <summary>
	/// BasePluginForms is the user control base for all the plugins / workspaces.
	/// </summary>
	//[ LicenseProvider( typeof( Xheo.Licensing.ExtendedLicenseProvider ) ) ]
	public class BasePluginForm : System.Windows.Forms.UserControl
	{
		//private License _license = null;
		
		bool _isUnique=false;
		MenuItemCollection _pluginMenus = null;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// BasePluginForm Constructor.
		/// </summary>
		public BasePluginForm()
		{
			//_license = LicenseManager.Validate( typeof( BasePluginForm ), this );
		}


		/// <summary>
		/// Gets or sets if the plugin can only be instantiated once in the workspace.
		/// </summary>
		public bool IsUnique
		{
			get
			{
				return _isUnique;
			}
			set
			{
				_isUnique = value;
			}
		}

		/// <summary>
		/// Gets or sets the plugin menus.
		/// </summary>
		public MenuItemCollection PluginMenus
		{
			get
			{
				return _pluginMenus;
			}
			set
			{
				_pluginMenus = value;
			}
		}

		/// <summary>
		/// Closes the plugins.
		/// </summary>
		public virtual void Close()
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

	/// <summary>
	/// Implements the workspace plugin interface.
	/// </summary>
	public interface IWorkspacePlugin
	{		

		/// <summary>
		/// Raises plugin loading event. This event has to be defined inside the Load event.
		/// </summary>
		event LoadPluginMenusEventHandler LoadPluginMenusEvent;

		/// <summary>
		/// Raises the start progress event.
		/// </summary>
		event StartProgressBarEventHandler StartProgressBarEvent;

		/// <summary>
		/// Raises the stop progress event.
		/// </summary>
		event StopProgressBarEventHandler StopProgressBarEvent;

		/// <summary>
		/// Raises the change status bar event.
		/// </summary>
		event ChangeStatusBarEventHandler ChangeStatusBarPanelEvent;

		/// <summary>
		/// Raises the apply menut settings.
		/// </summary>
		event ApplyMenuSettingsEventHandler ApplyMenuSettingsEvent;

		/// <summary>
		/// Raises the apply menu root settings.
		/// </summary>
		event ApplyMenuRootSettingsEventHandler ApplyMenuRootSettingsEvent;

		//event UnloadWorkspaceEventHandler UnloadWorkspaceEvent;
		//event LoadWorkspaceEventHandler LoadWorkspaceEvent;
		//event ShowWorkspaceMenuToolbarItemsEventHandler ShowWorkspaceMenuToolbarItemsEvent;
		//event HideWorkspaceMenuToolbarItemsEventHandler HideWorkspaceMenuToolbarItemsEvent;

		/// <summary>
		/// Hides the workspace menu items.
		/// </summary>
		void HideWorkspaceMenuToolbarItems();

		/// <summary>
		/// Shows the workspace menu items.
		/// </summary>
		void ShowWorkspaceMenuToolbarItems();
	}
}
