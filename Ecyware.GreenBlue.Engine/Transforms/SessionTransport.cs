// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2005
using System;
using System.Text;
using System.Reflection;
using System.Collections;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms;
using System.Data;
using System.Data.Odbc;
using Ecyware.GreenBlue.Engine.Transforms.Designers;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for SessionTransport.
	/// </summary>
	[Serializable]
	public class SessionTransport : Transport
	{
		DefaultTransformValue _sessionName = new DefaultTransformValue(true);

		public SessionTransport()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Gets or sets the sql query.
		/// </summary>
		public DefaultTransformValue SessionName
		{
			get
			{
				return _sessionName;
			}
			set
			{
				_sessionName = value;
			}
		}


		public override Argument[] GetArguments()
		{
		
			ArrayList arguments = new ArrayList();

			Argument arg = new Argument();
			arg.Name = "SessionTransport.SessionName";
			arguments.Add(arg);

			if ( arguments.Count == 0 )
			{
				return null;
			} 
			else 
			{
				return (Argument[])arguments.ToArray(typeof(Argument));
			}
		}

		public override void Send(string[] payload)
		{
			string result = payload[0];

			ScriptingApplication.Session[_sessionName.Value] = payload;

		}
	}
}
