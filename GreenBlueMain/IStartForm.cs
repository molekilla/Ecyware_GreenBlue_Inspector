// All rights reserved.
// Title: GreenBlue Project
// Author(s): Rogelio Morrell C.
// Date: November 2003
// Add additional authors here
using System;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// IStartForm Interface
	/// </summary>
	//public delegate void UpdateStatusBarEventHandler(object sender,EventArgs e);
	//public delegate void CancelViewEventHandler(object sender,EventArgs e);
	
	public enum FormViewTypes
	{
		Initial, Editing, Browsing
	}
	public interface IStartForm
	{
		
		/**
		bool CanClearTabs { get ; set; }
		bool IsUniqueForm {get;set;}
		string FormName { get; set; }
		string FormLabel { get; set; }
		void LoadInitialView(object[] parameters);
		void LoadEditingView(object[] parameters);
		void LoadBrowsingView(object[] parameters);
		*/
		//void ChangeStatusBarPanel(int panelIndex,string message);
		//event UpdateStatusBarEventHandler OnUpdateStatusBarEventHandler;
	}
}
