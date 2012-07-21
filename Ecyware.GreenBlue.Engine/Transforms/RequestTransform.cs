// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using System.Collections;
using System.Reflection;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms.Designers;

namespace Ecyware.GreenBlue.Engine.Transforms
{

	/// <summary>
	/// Summary description for RequestTransform.
	/// </summary>
	[Serializable]
	[WebTransformAttribute("Request Transform", "input","Updates the properties related to the request.")]
	[UITransformEditor(typeof(RequestTransformDesigner))]
	public class RequestTransform : WebTransform
	{
		private string _requestField;
		private UpdateTransformAction _updateTransformAction = new UpdateTransformAction();

		/// <summary>
		/// Creates a new RequestTransform.
		/// </summary>
		public RequestTransform()
		{
		}

		/// <summary>
		/// Gets or sets the request field name.
		/// </summary>
		public string RequestFieldName
		{
			get
			{
				return _requestField;
			}
			set
			{
				_requestField = value;
			}
		}
		/// <summary>
		/// Gets or sets the update transform to update the request url.
		/// </summary>
		public UpdateTransformAction UpdateTransformAction
		{
			get
			{
				return _updateTransformAction;
			}
			set
			{
				_updateTransformAction = value;
			}
		}

		/// <summary>
		/// Applies the transform to the request.
		/// </summary>		
		/// <param name="request"> The web request.</param>
		/// <param name="response"> The web response.</param>
		public override void ApplyTransform(WebRequest request, WebResponse response)
		{
			base.ApplyTransform (request, response);

			// Apply TransformAction
			object value = _updateTransformAction.ApplyTransformAction(response);

			switch ( this.RequestFieldName )
			{
				case "ChangeUrlHostname":
					string hostname =  Convert.ToString(value);
					Uri ur = new Uri(request.Url);
					Uri temp = new Uri(ur.Scheme + "://" + hostname + "/");
					Uri temp1 = new Uri(temp, ur.AbsolutePath);
					request.Url = temp1.ToString();
					break;
				case "ChangeUrlPath":
					string path =  Convert.ToString(value).TrimStart('/');
					Uri _ur = new Uri(request.Url);
					Uri _temp = new Uri(_ur.Scheme + "://" + _ur.Host + "/" + path);
					request.Url = _temp.ToString();
					break;									
				case "Username":
					request.RequestHttpSettings.UseBasicAuthentication = true;
					request.RequestHttpSettings.Username = Convert.ToString(value);
					break;
				case "Password":
					request.RequestHttpSettings.UseBasicAuthentication = true;
					request.RequestHttpSettings.Password = Convert.ToString(value);
					break;
				default:
					Type requestType = request.GetType();				
					PropertyInfo pi = requestType.GetProperty(RequestFieldName);

					if ( pi != null )
					{
						pi.SetValue(request,value,null);
					}

					break;
			}
		}

		/// <summary>
		/// Gets the arguments.
		/// </summary>
		/// <returns> An Argument type array.</returns>
		public override Argument[] GetArguments()
		{
			ArrayList arguments = new ArrayList();

			if ( this.UpdateTransformAction.Value is DefaultTransformValue )
			{
				if ( ((DefaultTransformValue)UpdateTransformAction.Value).EnabledInputArgument )
				{
					Argument arg = new Argument();
					arg.Name = UpdateTransformAction.Name;
					arguments.Add(arg);
				}
			}

			if ( arguments.Count == 0 )
			{
				return null;
			} 
			else 
			{
				return (Argument[])arguments.ToArray(typeof(Argument));
			}
		}
	}
}
