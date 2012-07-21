using System;
using System.Collections;

namespace Ecyware.GreenBlue.Engine.Scripting
{
	/// <summary>
	/// Summary description for WebRequestArgs.
	/// </summary>
	public class WebRequestArgs
	{
		private int _index = -1;
		private ArrayList _list = new ArrayList();

		/// <summary>
		/// Creates a new WebRequestArgs.
		/// </summary>
		public WebRequestArgs()
		{
		}

		/// <summary>
		/// Gets or sets the web request index.
		/// </summary>
		public int WebRequestIndex
		{
			get
			{
				return _index;
			}
			set
			{
				_index = value;
			}
		}

		/// <summary>
		/// Gets or sets the argument.
		/// </summary>
		public Argument[] Arguments
		{
			get
			{
				return (Argument[])_list.ToArray(typeof(Argument));
			}
			set
			{	
				if ( value != null )
					_list.AddRange(value);
			}
		}

		#region Helpers

		/// <summary>
		/// Clears all the web requests args.
		/// </summary>
		public void ClearArguments()
		{
			_list.Clear();
		}


		/// <summary>
		/// Removes an argument by index.
		/// </summary>
		/// <param name="index"> The argument index.</param>
		public void RemoveArgument(int index)
		{
			_list.RemoveAt(index);
		}

		/// <summary>
		/// Adds an argument.
		/// </summary>
		/// <param name="argument"> The argument to add.</param>
		public void AddArgument(Argument argument)
		{
			_list.Add(argument);
		}

		/// <summary>
		/// Adds an argument array.
		/// </summary>
		/// <param name="argument"> The argument to add.</param>
		public void AddArguments(Argument[] argument)
		{
			_list.AddRange(argument);
		}

		/// <summary>
		/// Inserts an argument.
		/// </summary>
		/// <param name="index"> The index to insert at.</param>
		/// <param name="argument"> The argument to add.</param>
		public void InsertArgument(int index, Argument argument)
		{
			_list.Insert(index, argument);
		}

		/// <summary>
		/// Updates the argument.
		/// </summary>
		/// <param name="index"> The request args index.</param>
		/// <param name="argument"> The argument type.</param>
		public void UpdateArgument(int index, Argument argument)
		{
			if ( index > -1 )
			{
				if ( argument != null )
				{
					// Update request index.
					_list[index] = argument;
				}
			}
		}
		#endregion
	}
}
