using System;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for Transport.
	/// </summary>
	[Serializable]
	public abstract class Transport
	{
		public Transport()
		{
		}

		public virtual Argument[] GetArguments()
		{
			return null;
		}

		public virtual void Send(string[] args)
		{
		}
	}
}
