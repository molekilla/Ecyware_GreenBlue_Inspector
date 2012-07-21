// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: June 2004
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Text;
using System.Net;
using Ecyware.GreenBlue.Controls;
using Ecyware.GreenBlue.Protocols.Http;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.Engine.HtmlCommand;
using mshtml;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Contains the logic for transforming HTML DOM to Ecyware HTML DOM types.
	/// </summary>
	public class HtmlDomTransformation
	{
		/// <summary>
		/// Creates a new HtmlDomTransformation.
		/// </summary>
		private HtmlDomTransformation()
		{
		}
		/// <summary>
		/// Transform each form element to a HtmlFormTag.
		/// </summary>
		/// <param name="htmlDoc"> The HTML DOM Document to process.</param>
		public static HtmlFormTagCollection TransformFormElements(IHTMLDocument2 htmlDoc, Uri currentUri)
		{
			// For each form, add in temp FormTags Collection
			FormConverter converter = new FormConverter();

			HtmlFormTagCollection formCollection = new HtmlFormTagCollection(htmlDoc.forms.length);

			try
			{
				foreach ( HTMLFormElementClass formElement in htmlDoc.forms )
				{
					System.Windows.Forms.Application.DoEvents();

					// Convert to HTML Form Tag
					HtmlFormTag form = converter.ConvertToHtmlFormTag(formElement, currentUri);

					if ( !formCollection.ContainsKey(form.Name) )
					{
						formCollection.Add(form.Name, form);
					}
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.ToString());
				Microsoft.ApplicationBlocks.ExceptionManagement.ExceptionManager.Publish(ex);
			}

		return formCollection;
	}

		/// <summary>
		/// Transform anchor tags to HtmlAnchorTag.
		/// </summary>
		/// <param name="htmlDoc"> The HTML DOM Document to process.</param>
		/// <returns> A HtmlTagBaseList.</returns>
		public static HtmlTagBaseList TransformAnchorElements(IHTMLDocument2 htmlDoc)
		{
			HtmlTagBaseList list = new HtmlTagBaseList();

			foreach ( object obj in htmlDoc.links )
			{				
				if ( obj is IHTMLAnchorElement )
				{
					IHTMLAnchorElement a = (IHTMLAnchorElement)obj;				
					HtmlAnchorTag anchorTag = new HtmlAnchorTag();
					anchorTag.HRef = a.href;
					anchorTag.Host = a.host;
					anchorTag.Hostname = a.hostname;
					anchorTag.MimeType = a.mimeType;
					anchorTag.Pathname = a.pathname;
					anchorTag.Protocol = a.protocol;
					anchorTag.Query = a.search;
					list.Add(anchorTag);
				} 
//				else 
//				{
//					System.Windows.Forms.MessageBox.Show(((mshtml.IHTMLElement)obj).outerHTML);
//				}
			}

			return list;
		}


		/// <summary>
		/// Transform frame elements to HtmlLinkTag array.
		/// </summary>
		/// <param name="htmlDoc"> The HTML DOM Document to process.</param>
		/// <returns> A HtmlTagBaseList.</returns>
		public static HtmlTagBaseList TransformFrameElements(IHTMLDocument2 htmlDoc)
		{
			HtmlTagBaseList list = new HtmlTagBaseList();

			IHTMLElementCollection coll = (IHTMLElementCollection)htmlDoc.all.tags("frame");
			foreach ( object obj in coll )
			{				
				if ( obj is IHTMLFrameBase )
				{
					IHTMLFrameBase a = (IHTMLFrameBase)obj;
					
					HtmlLinkTag frame = new HtmlLinkTag();
					frame.HRef = a.src;					
					list.Add(frame);
				} 
			}

			return list;
		}

		/// <summary>
		/// Transform link tags to HtmlAnchorTag.
		/// </summary>
		/// <param name="htmlDoc"> The HTML DOM Document to process.</param>
		/// <returns> A HtmlTagBaseList.</returns>
		public static HtmlTagBaseList TransformLinksElements(IHTMLDocument2 htmlDoc)
		{
			HtmlTagBaseList list = new HtmlTagBaseList();
			IHTMLElementCollection coll = (IHTMLElementCollection)htmlDoc.all.tags("link");

			foreach ( object obj in coll )
			{
				if ( obj is IHTMLLinkElement )
				{
					IHTMLLinkElement link = (IHTMLLinkElement)obj;
					HtmlLinkTag linkTag = new HtmlLinkTag();
					linkTag.HRef = link.href;
					linkTag.MimeType = link.type;
					list.Add(linkTag);
				}
//				else 
//				{
//					System.Windows.Forms.MessageBox.Show(((mshtml.IHTMLElement)obj).outerHTML);
//				}
			}

			return list;
		}

	}
}
