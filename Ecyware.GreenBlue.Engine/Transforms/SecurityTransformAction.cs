using System;
using System.Collections;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Engine.Transforms;
using Ecyware.GreenBlue.Engine.Scripting;


namespace Ecyware.GreenBlue.Engine.Transforms
{
	public enum RequestStateDataType
	{
		Form,
		Cookies,
		PostData,
		Url
	}

	/// <summary>
	/// Contains the transform action for a security transform.
	/// </summary>
	[Serializable]
	public class SecurityTransformAction : TransformAction
	{
		RequestStateDataType _requestStateData;
		SecurityTransformValue _testValue;
		/// <summary>
		/// Creates a new SecurityTransformAction.
		/// </summary>
		public SecurityTransformAction()
		{
		}

		#region Properties
		/// <summary>
		/// Gets or sets the request state data type.
		/// </summary>
		public RequestStateDataType RequestStateDataType
		{
			get
			{
				return _requestStateData;
			}
			set
			{
				_requestStateData = value;
			}
		}

		/// <summary>
		/// Gets or sets the test value.
		/// </summary>
		public SecurityTransformValue TestValue
		{
			get
			{
				return _testValue;
			}
			set
			{
				_testValue = value;
			}
		}
		#endregion

		
		#region Methods
		private HtmlFormTag FillFormField(WebRequest request, HtmlFormTag form, string test)
		{
			foreach ( HtmlTagListXml list in request.Form.Elements )
			{
				HtmlTagBaseList tagList = form[list.Name];
				#region Fill Form				
				foreach ( HtmlTagBase tagBase in tagList )
				{
					if ( tagBase is HtmlInputTag )
					{
						((HtmlInputTag)tagBase).Value = test;
					}
					if ( tagBase is HtmlButtonTag)
					{
						HtmlButtonTag button = (HtmlButtonTag)tagBase;
						button.Value = test;
					}
					if ( tagBase is HtmlSelectTag )
					{
						HtmlSelectTag select = (HtmlSelectTag)tagBase;
						if  ( select.Multiple )
						{
							foreach ( HtmlOptionTag opt in select.Options )
							{
								if ( opt.Selected ) 
								{
									opt.Value = test;
								}
							}
						} 
						else 
						{							
							select.Value = test;
						}
					}
					if  ( tagBase is HtmlTextAreaTag )
					{
						((HtmlTextAreaTag)tagBase).Value = test;
					}			
				}
				#endregion
			}
			return form;
		}

		/// <summary>
		/// Applies the form test.
		/// </summary>
		/// <param name="request"> The WebRequest type.</param>
		protected virtual void ApplyFormTest(WebRequest request)
		{
			if ( request.Form != null )
			{
				// Create HtmlFormTag
				HtmlFormTag formTag = request.Form.WriteHtmlFormTag();

				// Generate value.
				string testValue = this.TestValue.Value;

				FillFormField(request, formTag, testValue);

				// Update request
				request.Form.ReadHtmlFormTag(formTag);
			}
		}		
		#endregion

		protected virtual void ApplyCustomTest(WebRequest request, SecurityTransformAction action)
		{
		}

		public void ApplySecurityTransformAction(WebRequest request, WebResponse response)
		{
			switch ( this.RequestStateDataType )
			{
				case  RequestStateDataType.Form:
					ApplyFormTest(request);
					break;
				case  RequestStateDataType.Cookies:
					//ApplyCookiesTest();
					break;
				case  RequestStateDataType.PostData:
					//ApplyPostDataTest();
					break;
				case  RequestStateDataType.Url:
					//ApplyUrlTest();
					break;
			}
		}
	}

}
