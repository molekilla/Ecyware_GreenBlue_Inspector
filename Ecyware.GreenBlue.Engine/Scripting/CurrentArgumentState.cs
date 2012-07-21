using System;

namespace Ecyware.GreenBlue.Engine.Scripting
{
	public class CurrentArgumentState
	{
		Argument _argument;
		int _webRequestIndex;
		int _argumentIndex;

		/// <summary>
		/// Creates a new CurrentArgumentState.
		/// </summary>
		public CurrentArgumentState()
		{
		}

		/// <summary>
		/// Gets or sets the web request index.
		/// </summary>
		public int WebRequestIndex
		{
			get
			{
				return _webRequestIndex;
			}
			set
			{
				_webRequestIndex = value;
			}
		}

		/// <summary>
		/// Gets or sets the argument index.
		/// </summary>
		public int ArgumentIndex
		{
			get
			{
				return _argumentIndex;
			}
			set
			{
				_argumentIndex = value;
			}
		}

		/// <summary>
		/// Gets or sets the argument.
		/// </summary>
		public Argument SelectedArgument
		{
			get
			{
				return _argument;
			}
			set
			{
				_argument = value;
			}
		}


	}
}
