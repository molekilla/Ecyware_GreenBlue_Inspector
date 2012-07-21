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
	/// Summary description for CookiesTransform.
	/// </summary>
	[Serializable]
	[WebTransformAttribute("Cookies Transform", "input","Adds, updates or removes cookies.")]
	[UITransformEditor(typeof(CookiesTransformDesigner))]
	public class CookiesTransform : WebTransform
	{
		private ArrayList _actions = new ArrayList(10);
		private ArrayList _cookies = new ArrayList(10);

		/// <summary>
		/// Creates a new CookiesTransform.
		/// </summary>
		public CookiesTransform()
		{
		}

		/// <summary>
		/// Gets or sets the collection of cookies transform actions.
		/// </summary>
		public TransformAction[] CookieTransformActions
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
		/// Gets or sets the cookies.
		/// </summary>
		public Cookie[] Cookies
		{
			get
			{
				return (Cookie[])_cookies.ToArray(typeof(Cookie));
			}
			set
			{
				if ( value != null )
					_cookies.AddRange(value);
			}
		}

		/// <summary>
		/// Adds a cookie.
		/// </summary>
		/// <param name="cookie"> The cookie to add.</param>
		public void AddCookie(Cookie cookie)
		{
			_cookies.Add(cookie);
		}

		/// <summary>
		/// Removes a cookie.
		/// </summary>
		/// <param name="index"> The cookie index.</param>
		public void RemoveCookie(int index)
		{
			_cookies.RemoveAt(index);
		}

		/// <summary>
		/// Gets a cookie.
		/// </summary>
		/// <param name="name"> The cookie index.</param>
		/// <returns> A cookie.</returns>
		public Cookie GetCookie(string name)
		{
			Cookie result = null;
			foreach ( Cookie c in _cookies )
			{
				if ( c.Name == name )
				{
					result = c;
					break;
				}
			}

			return result;
		}


		/// <summary>
		/// Remove all cookies.
		/// </summary>
		public void RemoveAllCookies()
		{
			_cookies.Clear();
		}

		/// <summary>
		/// Adds a cookie transform action.
		/// </summary>
		/// <param name="action"> The TransformAction type.</param>
		public void AddCookieTransformAction(TransformAction action)
		{
			_actions.Add(action);
		}

		/// <summary>
		/// Removes the cookie transform action.
		/// </summary>
		/// <param name="index"> The index.</param>
		public void RemoveCookieTransformAction(int index)
		{
			_actions.RemoveAt(index);
		}

		/// <summary>
		/// Removes all cookie tranforms action.
		/// </summary>
		public void RemoveAllCookieTransformActions()
		{
			_actions.Clear();
		}

		/// <summary>
		/// Gets a cookie transform action.
		/// </summary>
		/// <param name="index"> The index.</param>
		/// <returns> A TransformAction type.</returns>
		public TransformAction GetCookieTransformAction(int index)
		{
			return (TransformAction)_actions[index];
		}


		/// <summary>
		/// Applies the transform to the request.
		/// </summary>
		/// <param name="request"> The web request.</param>
		/// <param name="response"> The web response.</param>
		public override void ApplyTransform(WebRequest request, WebResponse response)
		{
			base.ApplyTransform (request, response);

			Hashtable cookieTable = new Hashtable();
			foreach ( Ecyware.GreenBlue.Engine.Scripting.Cookie cookie in request.Cookies )
			{
				cookieTable.Add(cookie.Name, cookie);
			}

			try
			{
				int i = 0;
				foreach ( TransformAction transformAction in _actions )
				{
					// Add Cookie
					if ( transformAction is AddTransformAction )
					{
						AddTransformAction add = (AddTransformAction)transformAction;

						object result = add.Value.GetValue(response);

						if ( result != null )
						{
							// add cookie
							Cookie ck = GetCookie(add.Name);
							ck.Value = result.ToString();

							if ( !cookieTable.ContainsKey(add.Name) )
							{
								cookieTable.Add(add.Name, ck);
							}
						}
					}

					// Update Cookie
					if ( transformAction is UpdateTransformAction )
					{
						UpdateTransformAction update = (UpdateTransformAction)transformAction;

						object result = update.Value.GetValue(response);


						if ( cookieTable[update.Name] != null )
						{
							// Update cookie						
							Cookie ck = (Cookie)cookieTable[update.Name];
							ck.Value = result.ToString();
							cookieTable[update.Name] = ck;
						}
					}

					// Remove Cookie
					if ( transformAction is RemoveTransformAction )
					{
						RemoveTransformAction remove = (RemoveTransformAction)transformAction;

						if ( cookieTable.ContainsKey(remove.Name) )
						{
							cookieTable.Remove(remove.Name);
						}
					}

					i++;
				}

				ArrayList cks = new ArrayList();
				foreach ( DictionaryEntry de in cookieTable )
				{
					cks.Add(de.Value);
				}
						
				// update request
				request.Cookies = (Ecyware.GreenBlue.Engine.Scripting.Cookie[])cks.ToArray(typeof(Ecyware.GreenBlue.Engine.Scripting.Cookie));
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

			foreach ( TransformAction action in this.CookieTransformActions )
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
