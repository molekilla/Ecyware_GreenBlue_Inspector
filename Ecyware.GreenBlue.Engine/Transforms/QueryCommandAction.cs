using System;
using Ecyware.GreenBlue.Engine.Scripting;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for QueryCommandAction.
	/// </summary>
	[Serializable]
	public class QueryCommandAction : TransformAction
	{
		private TransformValue _value;

		/// <summary>
		/// Creates a new QueryCommandAction.
		/// </summary>
		public QueryCommandAction()
		{
		}

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		public TransformValue Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
			}
		}

		public string ApplyQueryCommandAction(string input)
		{
			string s = string.Empty;

			if ( Value is RegExQueryCommand )
			{
				s = ((RegExQueryCommand)Value).ExecuteQuery(input);
			}

			if ( Value is XPathQueryCommand )
			{
				s = ((XPathQueryCommand)Value).ExecuteQuery(input);
			}

			return s;
		}
		public override object ApplyTransformAction(WebResponse response)
		{
			base.ApplyTransformAction(response);

			return Value.GetValue(response);
		}
	}
}
