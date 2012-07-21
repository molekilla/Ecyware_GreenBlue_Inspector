using System;
using System.Net;


namespace Ecyware.GreenBlue.Engine.Scripting
{
	/// <summary>
	/// Summary description for ScriptingHttpState.
	/// </summary>
	public class ScriptingHttpState : HttpState
	{
		private HttpRequestResponseContext _context;

		/// <summary>
		/// Creates a new HttpScriptingState.
		/// </summary>
		public ScriptingHttpState()
		{
		}

		public HttpRequestResponseContext HttpRequestResponseContext
		{
			get
			{
				return _context;
			}
			set
			{
				_context = value;
			}
		}
	}
}
