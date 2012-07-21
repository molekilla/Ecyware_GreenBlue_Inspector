// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: June 2004
using System;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Contains the interface for IPipelineCommand.
	/// </summary>
	public interface IPipelineCommand
	{
		ResponseBuffer HttpResponseData {get;}
		BaseHttpState HttpStateData {get;set;}
		Delegate CallbackMethod {get;set;}
		string ErrorMessage {get;set;}

		void ExecuteCommand();
	}
}
