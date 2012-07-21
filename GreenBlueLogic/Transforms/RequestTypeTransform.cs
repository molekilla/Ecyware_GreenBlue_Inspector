using System;
using Ecyware.GreenBlue.Protocols.Http.Transforms.Designers;

namespace Ecyware.GreenBlue.Protocols.Http.Transforms
{
	/// <summary>
	/// Summary description for RequestTypeTransform.
	/// </summary>
	[UITransformEditor(typeof(RequestTypeTransformDesigner))]
	public class RequestTypeTransform : WebTransform
	{
		private UpdateTransformAction _changeRequestType;

		/// <summary>
		/// Creates a RequestTypeTransform.
		/// </summary>
		public RequestTypeTransform()
		{
		}

		/// <summary>
		/// Gets or sets the update transform to update the request type.
		/// </summary>
		public UpdateTransformAction ChangeRequestType
		{
			get
			{
				return _changeRequestType;
			}
			set
			{
				_changeRequestType = value;
			}
		}
	}
}
