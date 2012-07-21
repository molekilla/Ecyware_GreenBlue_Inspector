using System;

namespace Ecyware.GreenBlue.Protocols.Http.Transforms
{
	/// <summary>
	/// Summary description for ResultReferenceTransformValue.
	/// </summary>
	public class ResultReferenceTransformValue : TransformValue
	{
		private string _from = string.Empty;
		private TransformValueTypeMember _member;

		/// <summary>
		/// Creates a new ResultReferenceTransformValue.
		/// </summary>
		public ResultReferenceTransformValue()
		{
		}		

		/// <summary>
		/// Gets or sets the web request name to query.
		/// </summary>
		public string WebResultName
		{
			get
			{
				return _from;
			}
			set
			{
				_from = value;
			}
		}


		/// <summary>
		/// Gets or sets the member.
		/// </summary>
		public TransformValueTypeMember Member
		{
			get
			{
				return _member;
			}
			set
			{
				_member = value;
			}
		}
	}
}
