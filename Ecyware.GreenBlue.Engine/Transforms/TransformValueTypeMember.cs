// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for TransformValueTypeMember.
	/// </summary>
	[Serializable]
	public class TransformValueTypeMember
	{
		private string _memberName;
		private string _collName;

		/// <summary>
		/// Creates a new TransformValueTypeMember.
		/// </summary>
		public TransformValueTypeMember()
		{
		}

		/// <summary>
		/// Gets or sets the member name.
		/// </summary>
		public string MemberName
		{
			get
			{
				return _memberName;
			}
			set
			{
				_memberName = value;
			}
		}

		/// <summary>
		/// Gets or sets the collection member name.
		/// </summary>
		public string CollectionMemberName
		{
			get
			{
				return _collName;
			}
			set
			{
				_collName = value;
			}
		}
	}
}
