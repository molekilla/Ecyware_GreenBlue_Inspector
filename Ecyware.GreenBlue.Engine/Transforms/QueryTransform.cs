// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using System.Text;
using System.Collections;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms;
using Ecyware.GreenBlue.Engine.Transforms.Designers;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for QueryTransform.
	/// </summary>
	[Serializable]
	[WebTransformAttribute("Query Transform", "output","Creates queries of the response using regular expression or XPath queries.")]
	[UITransformEditor(typeof(QueryTransformDesigner))]
	public class QueryTransform : WebTransform
	{
		private ArrayList _commands = new ArrayList(10);
		Transport _transport;

		/// <summary>
		/// Creates a new QueryTransform.
		/// </summary>
		public QueryTransform()
		{
		}

		/// <summary>
		/// Gets or sets the query command actions.
		/// </summary>
		public QueryCommandAction[] QueryCommandActions
		{
			get
			{
				return (QueryCommandAction[])_commands.ToArray(typeof(QueryCommandAction));
			}
			set
			{
				if ( value != null )
					_commands.AddRange(value);
			}
		}

		/// <summary>
		/// Adds a query command action.
		/// </summary>
		/// <param name="action"> The QueryCommandAction type.</param>
		public void AddQueryCommandAction(QueryCommandAction action)
		{
			_commands.Add(action);
		}

		/// <summary>
		/// Removes the query command value.
		/// </summary>
		/// <param name="index"> The index.</param>
		public void RemoveQueryCommandAction(int index)
		{
			_commands.RemoveAt(index);
		}

		/// <summary>
		/// Removes all query command actions.
		/// </summary>
		public void RemoveAllQueryCommandActions()
		{
			_commands.Clear();
		}

		/// <summary>
		/// Gets a query command action.
		/// </summary>
		/// <param name="index"> The index.</param>
		/// <returns> A QueryCommandAction type.</returns>
		public QueryCommandAction GetQueryCommandAction(int index)
		{
			return (QueryCommandAction)_commands[index];
		}

		/// <summary>
		/// Gets or sets the transport
		/// </summary>
		public Transport Transport
		{
			get
			{
				return _transport;
			}
			set
			{
				_transport = value;
			}
		}

		public override Argument[] GetArguments()
		{
			if ( Transport.GetArguments() != null )
			{
				return Transport.GetArguments();
			} 
			else 
			{
				return null;
			}
		}

		public override void ApplyTransform(WebRequest request, WebResponse response)
		{
			base.ApplyTransform (request, response);
			string result = response.HttpBody;

			// Apply transform value and append each value.
			foreach ( QueryCommandAction action in this.QueryCommandActions )
			{
				if (( action.Value is XPathQueryCommand ) || ( action.Value is RegExQueryCommand ))
				{
					result = action.ApplyQueryCommandAction(result);
				}
			}

			if ( Transport != null )
			{
				// Send message to transport.
				Transport.Send(new string[] {result});
			}
		}

	}
}
