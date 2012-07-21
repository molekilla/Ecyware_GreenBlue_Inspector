// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using System.Collections;
using Ecyware.GreenBlue.Engine.Scripting;
using Microsoft.ApplicationBlocks.ExceptionManagement;
using Ecyware.GreenBlue.Engine.Transforms.Designers;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for HeaderTransform.
	/// </summary>
	[Serializable]
	[WebTransformAttribute("Headers Transform", "input","Adds, updates or removes headers.")]
	[UITransformEditor(typeof(HeaderTransformDesigner))]
	public class HeaderTransform : WebTransform
	{
		private ArrayList _actions = new ArrayList(10);
		private static string[] _restrictedHeaders = new string[] {
			"Accept",
			"Content Length",
			"Content Type",
			"Character Set",
			"Content Encoding",
			"If Modified Since",
			"Keep Alive",
			"Location",
			"Media Type",
			"Referer",
			"Response Uri",
			"Send Chunked",
			"Status Code",
			"Status Description",
			"Transfer Encoding",
			"User Agent",
			"Version"};

		/// <summary>
		/// Creates a new HeaderTransform.
		/// </summary>
		public HeaderTransform()
		{
		}

		/// <summary>
		/// Gets a list of restricted headers.
		/// </summary>
		public static string[] GetRestrictedHeaders
		{
			get
			{
				return _restrictedHeaders;
			}
		}
		/// <summary>
		/// Gets or sets the collection of header transform actions.
		/// </summary>
		public TransformAction[] Headers
		{
			get
			{
				return (TransformAction[])_actions.ToArray(typeof(TransformAction));
			}
			set
			{
				if ( value != null )
					_actions.AddRange(value);
			}
		}

		/// <summary>
		/// Adds a header transform action.
		/// </summary>
		/// <param name="action"> The TransformAction type.</param>
		public void AddHeaderTransformAction(TransformAction action)
		{
			_actions.Add(action);
		}

		/// <summary>
		/// Removes the header transform action.
		/// </summary>
		/// <param name="index"> The index.</param>
		public void RemoveHeaderTransformAction(int index)
		{
			_actions.RemoveAt(index);
		}

		/// <summary>
		/// Removes all header tranforms action.
		/// </summary>
		public void RemoveAllHeaderTransformActions()
		{
			_actions.Clear();
		}

		/// <summary>
		/// Gets a header transform action.
		/// </summary>
		/// <param name="index"> The index.</param>
		/// <returns> A TransformAction type.</returns>
		public TransformAction GetHeaderTransformAction(int index)
		{
			return (TransformAction)_actions[index];
		}

		/// <summary>
		/// Gets the value from the web response.
		/// </summary>
		/// <param name="request"> The request name</param>
		/// <param name="name"> The header name.</param>
		/// <param name="value"> The header value.</param>
		/// <param name="headers"> The headers.</param>
		/// <returns> An object.</returns>
		public void SetHeaderValue(WebRequest request, string name, string value, System.Net.WebHeaderCollection headers)
		{	

			object result = string.Empty;

			switch ( name )
			{
				case "Accept":
					request.RequestHttpSettings.Accept = value;
					break;
				case "Content Length":				
					request.RequestHttpSettings.ContentLength = Convert.ToInt32(value);
					break;
				case "Content Type":
					request.RequestHttpSettings.ContentType = value;
					break;
				case "If Modified Since":					
					request.RequestHttpSettings.IfModifiedSince = DateTime.Parse(value);
					break;
				case "Keep Alive":
					if ( value.ToLower(System.Globalization.CultureInfo.InvariantCulture) == "true" )
					{
						request.RequestHttpSettings.KeepAlive = true;
					} 
					else 
					{
						request.RequestHttpSettings.KeepAlive = false;
					}
					break;
				case "Media Type":
					request.RequestHttpSettings.MediaType = value;					
					break;
				case "Referer":
					request.RequestHttpSettings.Referer = value;
					break;
				case "Send Chunked":
					if ( value.ToLower(System.Globalization.CultureInfo.InvariantCulture) == "true" )
					{
						request.RequestHttpSettings.SendChunked = true;
					} 
					else 
					{
						request.RequestHttpSettings.SendChunked = false;
					}
					break;
				case "Transfer Encoding":
					request.RequestHttpSettings.TransferEncoding = value;
					break;
				case "User Agent":
					request.RequestHttpSettings.UserAgent = value;
					break;
				default:
					headers[name] = value;
					break;
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


			try
			{
				System.Net.WebHeaderCollection headers = new System.Net.WebHeaderCollection();
				WebHeader.FillWebHeaderCollection(headers,request.RequestHttpSettings.AdditionalHeaders);

				foreach ( TransformAction transformAction in _actions )
				{
					// Add Header
					if ( transformAction is AddTransformAction )
					{
						AddTransformAction add = (AddTransformAction)transformAction;

						object result = add.Value.GetValue(response);

						if ( headers[add.Name] == null )
						{
							headers.Add(add.Name, Convert.ToString(result));
						}
					}

					// Update Header
					if ( transformAction is UpdateTransformAction )
					{
						UpdateTransformAction update = (UpdateTransformAction)transformAction;

						object result = update.Value.GetValue(response);
						SetHeaderValue(request, update.Name, Convert.ToString(result), headers);
					}

					// Remove Header
					if ( transformAction is RemoveTransformAction )
					{
						RemoveTransformAction remove = (RemoveTransformAction)transformAction;
					
						if ( headers[remove.Name] != null )
						{
							headers.Remove(remove.Name);
						}
					}
				}

				// update request
				request.RequestHttpSettings.AdditionalHeaders = WebHeader.ToArray(headers);
			}
			catch ( Exception ex )
			{				
				ExceptionManager.Publish(ex);
			}
		}

		/// <summary>
		/// Gets the arguments.
		/// </summary>
		/// <returns> An Argument type array.</returns>
		public override Argument[] GetArguments()
		{
			ArrayList arguments = new ArrayList();

			foreach ( TransformAction action in this.Headers )
			{
				TransformValue tvalue = null;

				if ( action is UpdateTransformAction  )
				{					
					tvalue = ((UpdateTransformAction)action).Value;
				}
				if ( action is AddTransformAction  )
				{					
					tvalue = ((AddTransformAction)action).Value;
				}
				if ( tvalue is DefaultTransformValue )
				{
					if ( ((DefaultTransformValue)tvalue).EnabledInputArgument )
					{
						Argument arg = new Argument();
						arg.Name = action.Name;
						arguments.Add(arg);
					}
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
