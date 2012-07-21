using System;
using Ecyware.GreenBlue.Protocols.Http.Scripting;
using Ecyware.GreenBlue.Protocols.Http.Transforms.Designers;

namespace Ecyware.GreenBlue.Protocols.Http.Transforms
{
	/// <summary>
	/// Summary description for RequestUrlTransform.
	/// </summary>
	[UITransformEditor(typeof(RequestUrlTransformDesigner))]
	public class RequestUrlTransform : WebTransform
	{
		private UpdateTransformAction _changeUrl = new UpdateTransformAction();

		/// <summary>
		/// Creates a RequestUrlTransform.
		/// </summary>
		public RequestUrlTransform()
		{
		}

		/// <summary>
		/// Gets or sets the update transform to update the request url.
		/// </summary>
		public UpdateTransformAction ChangeRequestUrl
		{
			get
			{
				return _changeUrl;
			}
			set
			{
				_changeUrl = value;
			}
		}

		/// <summary>
		/// Applies the transform to the request.
		/// </summary>
		/// <param name="request"> The web request.</param>
		public override void ApplyTransform(WebRequest request)
		{
			base.ApplyTransform (request);

			// Get the result
			WebResponse response = request.WebResponse;

			// Apply TransformAction
			request.Url = (string)ChangeRequestUrl.ApplyTransformAction(response);			
		}
	}
}
