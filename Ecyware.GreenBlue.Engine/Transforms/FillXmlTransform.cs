// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using System.Collections;
using System.Xml;
using System.IO;
using System.Xml.XPath;
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
	[WebTransformAttribute("Xml Fields Transform", "input","Creates a collection of fields for a XML schema or API Specification.")]
	[UITransformEditor(typeof(FillXmlTransformDesigner))]
	public class FillXmlTransform : WebTransform
	{
		//private string _name;
		private ArrayList _fields = new ArrayList();

		/// <summary>
		/// Creates a new FillXmlTransform.
		/// </summary>
		public FillXmlTransform()
		{
		}


		/// <summary>
		/// Gets or sets the Xml Element fields.
		/// </summary>
		public XmlElementField[] XmlElementFields
		{
			get
			{
				return (XmlElementField[])_fields.ToArray(typeof(XmlElementField));
			}
			set
			{
				if ( value != null )
					_fields.AddRange(value);
			}
		}

		/// <summary>
		/// Adds a xml element field.
		/// </summary>
		/// <param name="field"> The XmlElementField type.</param>
		public void AddXmlElementField(XmlElementField field)
		{
			_fields.Add(field);
		}

		/// <summary>
		/// Removes the xml element field.
		/// </summary>
		/// <param name="index"> The index.</param>
		public void RemoveXmlElementField(int index)
		{
			_fields.RemoveAt(index);
		}

		/// <summary>
		/// Removes all form fields.
		/// </summary>
		public void RemoveAllFields()
		{
			_fields.Clear();
		}

		/// <summary>
		/// Gets a form field.
		/// </summary>
		/// <param name="index"> The index.</param>
		/// <returns> A XmlElementField type.</returns>
		public XmlElementField GetXmlElementField(int index)
		{
			return (XmlElementField)_fields[index];
		}

		public override void ApplyTransform(WebRequest request, WebResponse response)
		{
			base.ApplyTransform (request, response);

			if ( request.XmlEnvelope != null )
			{
				XmlDocument document = new XmlDocument();
				XmlNode n = document.ImportNode(request.XmlEnvelope,true);
				document.AppendChild(n);
					
				XmlNamespaceManager nsmgr = new XmlNamespaceManager(document.NameTable);										
				nsmgr = HtmlParser.ResolveNamespaces(new StringReader(request.XmlEnvelope.OuterXml),nsmgr);

				foreach ( XmlElementField field in XmlElementFields )
				{
					// Get Xml Element Location
					XmlNode selectedNode = document.SelectSingleNode(field.Location, nsmgr);
					
					// Generate TransformValue
					string result = Convert.ToString(field.TransformValue.GetValue(response));

					// Set value
					if ( selectedNode.NodeType == XmlNodeType.Element )
					{
						selectedNode.InnerText = result;
					}
					if ( selectedNode.NodeType == XmlNodeType.Attribute )
					{
						selectedNode.Value = result;
					}
				}

				if ( request.RequestType == HttpRequestType.PUT )
				{
					((PutWebRequest)request).PostData = document.DocumentElement.OuterXml;
				}
				if ( request.RequestType == HttpRequestType.POST )
				{
					((PostWebRequest)request).PostData = document.DocumentElement.OuterXml;
				}
				if ( request.RequestType == HttpRequestType.SOAPHTTP )
				{
					request.XmlEnvelope = document.DocumentElement;
				}
			}
		}

		/// <summary>
		/// Gets the arguments.
		/// </summary>
		/// <returns> An Argument type array.</returns>
		public override Argument[] GetArguments()
		{
			ArrayList arguments = new ArrayList();

			foreach ( XmlElementField field in this.XmlElementFields )
			{
				if ( field.TransformValue is DefaultTransformValue )
				{
					if ( ((DefaultTransformValue)field.TransformValue).EnabledInputArgument )
					{
						Argument arg = new Argument();
						arg.Name = field.Name;
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
