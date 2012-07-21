// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: May 2004

using System;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// The available states of a InspectorWorkspace instance.
	/// </summary>
	public enum InspectorAction
	{
		Idle,
		FormMappingRequest,
		UserPost,
		UserGet,
		WebBrowserPost,
		WebBrowserGet,
		InspectorRedirection
	}
}
