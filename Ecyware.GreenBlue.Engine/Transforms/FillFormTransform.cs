// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using System.Collections;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Engine.HtmlCommand;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms.Designers;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for FillFormTransform.
	/// </summary>	
	[Serializable]
	[WebTransformAttribute("Form Fields Transform", "input","Creates a collection of fields for HTML Form tag.")]
	[UITransformEditor(typeof(FillFormTransformDesigner))]
	public class FillFormTransform : WebTransform
	{
		//private string _name;
		//private int _index;
		private ArrayList _formFields = new ArrayList();

		/// <summary>
		/// Creates a new FillFormTransform.
		/// </summary>
		public FillFormTransform()
		{
		}


		/// <summary>
		/// Gets or sets the form fields.
		/// </summary>
		public FormField[] FormFields
		{
			get
			{
				return (FormField[])_formFields.ToArray(typeof(FormField));
			}
			set
			{
				if ( value != null )
					_formFields.AddRange(value);
			}
		}

//		/// <summary>
//		/// Gets or sets if the form fields are enabled for Windows Form Generation.
//		/// </summary>
//		public bool IsWindowsFormEnabled
//		{
//			get
//			{
//				return _isWindowsFormEnabled;
//			}
//			set
//			{
//				_isWindowsFormEnabled = value;
//			}
//		}

		/// <summary>
		/// Adds a header transform action.
		/// </summary>
		/// <param name="formField"> The FormField type.</param>
		public void AddFormField(FormField formField)
		{
			_formFields.Add(formField);
		}

		/// <summary>
		/// Removes the form field.
		/// </summary>
		/// <param name="index"> The index.</param>
		public void RemoveFormField(int index)
		{
			_formFields.RemoveAt(index);
		}

		/// <summary>
		/// Removes all form fields.
		/// </summary>
		public void RemoveAllFormFields()
		{
			_formFields.Clear();
		}

		/// <summary>
		/// Gets a form field.
		/// </summary>
		/// <param name="index"> The index.</param>
		/// <returns> A TransformAction type.</returns>
		public FormField GetFormField(int index)
		{
			return (FormField)_formFields[index];
		}

		public override void ApplyTransform(WebRequest request, WebResponse response)
		{
			base.ApplyTransform (request, response);

			if ( request.Form != null )
			{
				HtmlFormTag formTag = request.Form.WriteHtmlFormTag();

				#region Fill Form
				foreach ( FormField formField in FormFields )
				{
					string result = Convert.ToString(formField.TransformValue.GetValue(response));

					HtmlTagBaseList tagList = formTag[formField.FieldName];
					HtmlTagBase tagBase = tagList[formField.Index];

					if ( tagBase is HtmlInputTag )
					{
						((HtmlInputTag)tagBase).Value = result;
					}
					if ( tagBase is HtmlButtonTag)
					{
						HtmlButtonTag button = (HtmlButtonTag)tagBase;
						button.Value = result;
					}
					if ( tagBase is HtmlSelectTag )
					{
						HtmlSelectTag select = (HtmlSelectTag)tagBase;
						if  ( select.Multiple )
						{
							foreach ( HtmlOptionTag opt in select.Options )
							{
								//HtmlOptionTag opt = tag;
								if ( opt.Selected ) 
								{
									opt.Value = result;
								}
							}
						} 
						else 
						{							
							select.Value = result;
						}
					}
					if  ( tagBase is HtmlTextAreaTag )
					{
						((HtmlTextAreaTag)tagBase).Value = result;
					}					
				}
				#endregion

				// Update request
				request.Form.ReadHtmlFormTag(formTag);
			}
		}

		/// <summary>
		/// Gets the arguments.
		/// </summary>
		/// <returns> An Argument type array.</returns>
		public override Argument[] GetArguments()
		{
			ArrayList arguments = new ArrayList();

			foreach ( FormField formField in this.FormFields )
			{
				if ( formField.TransformValue is DefaultTransformValue )
				{
					if ( ((DefaultTransformValue)formField.TransformValue).EnabledInputArgument )
					{
						Argument arg = new Argument();
						arg.Name = formField.FieldName;
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
