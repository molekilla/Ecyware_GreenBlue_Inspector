using System;
using System.Collections;
using System.IO;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Security;
using System.Security.Policy;
using System.Security.Permissions;

namespace Ecyware.GreenBlue.Engine
{
	/// <summary>
	/// Summary description for XsltCommand.
	/// </summary>
	public class XsltCommand
	{
		/// <summary>
		/// Creates a new XsltCommand.
		/// </summary>
		public XsltCommand()
		{
		}

		/// <summary>
		/// Transform data.
		/// </summary>
		/// <param name="xmlData"> The xml data.</param>
		/// <param name="template"> The template.</param>
		/// <param name="arguments"> Arguments use in the template.</param>
		/// <returns> A string containing the html report.</returns>
		public string TransformFromFile(string xmlData, string template, Hashtable arguments)
		{
			// new XmlDocument, adds a xml using GetXml()
			XmlDocument root = new XmlDocument();
			root.Load(xmlData);
			XPathNavigator nav = root.CreateNavigator();

			// xml resolver
			XmlUrlResolver resolver = new XmlUrlResolver();
			resolver.Credentials=System.Net.CredentialCache.DefaultCredentials;

			//evidence
			Evidence ev = XmlSecureResolver.CreateEvidenceForUrl(template);

			StringWriter output = null;
			XmlTextReader reader = null;
			try
			{

				// XmlReader
				StreamReader stm = new StreamReader(template,System.Text.Encoding.Default);
				reader = new XmlTextReader(stm);
				XslTransform xslt = new XslTransform();

				output = new StringWriter();
		
				// build argument list
				XsltArgumentList xsltArgs = new XsltArgumentList();
				foreach ( DictionaryEntry de in arguments )
				{
					string name  = (string)de.Key;					
					xsltArgs.AddParam(name,"", de.Value);
				}

				// load
				xslt.Load(reader, resolver, ev);

				// transform
				xslt.Transform(nav,xsltArgs,output,resolver);

				return output.ToString();
			}
			catch
			{
				throw;
			}
			finally
			{
				if ( output != null )
					output.Close();

				if ( reader != null )
					reader.Close();
			}

		}


		/// <summary>
		/// Transform data.
		/// </summary>
		/// <param name="xmlFile"> The xml file to load.</param>
		/// <param name="stylesheet"> The style sheet file to load.</param>
		/// <returns> A string containing the html report.</returns>
		public string TransformFromFile(string xmlFile, string stylesheet)
		{
			// new XmlDocument, adds a xml using GetXml()
			XmlDocument root = new XmlDocument();
			root.Load(xmlFile);
			XPathNavigator nav = root.CreateNavigator();

			// xml resolver
			XmlUrlResolver resolver = new XmlUrlResolver();
			resolver.Credentials=System.Net.CredentialCache.DefaultCredentials;

			//evidence
			Evidence ev = XmlSecureResolver.CreateEvidenceForUrl(stylesheet);

			StringWriter output = null;
			XmlTextReader reader = null;
			try
			{

				// XmlReader
				StreamReader stm = new StreamReader(stylesheet,System.Text.Encoding.Default);
				reader = new XmlTextReader(stm);
				XslTransform xslt = new XslTransform();

				output = new StringWriter();
		
				// load
				xslt.Load(reader, resolver, ev);

				// transform
				xslt.Transform(nav,null,output,resolver);

				return output.ToString();
			}
			catch
			{
				throw;
			}
			finally
			{
				if ( output != null )
					output.Close();

				if ( reader != null )
					reader.Close();
			}

		}


		/// <summary>
		/// Transform data.
		/// </summary>
		/// <param name="xmlData"> The xml data to load.</param>
		/// <param name="stylesheet"> The style sheet data to load.</param>
		/// <returns> A string containing the html report.</returns>
		public string TransformFromData(string xmlData, string stylesheet)
		{
			// new XmlDocument, adds a xml using GetXml()
			XmlDocument root = new XmlDocument();
			root.LoadXml(xmlData);
			XPathNavigator nav = root.CreateNavigator();

			// xml resolver
			XmlUrlResolver resolver = new XmlUrlResolver();
			resolver.Credentials=System.Net.CredentialCache.DefaultCredentials;

			// evidence
			//Evidence ev = XmlSecureResolver.CreateEvidenceForUrl(stylesheet);

			StringWriter output = null;
			XmlTextReader reader = null;
			try
			{

				// XmlReader
				//StreamReader stm = new StreamReader(st,System.Text.Encoding.Default);
				StringReader sreader = new StringReader(stylesheet);				
				reader = new XmlTextReader(sreader);
				XslTransform xslt = new XslTransform();

				output = new StringWriter();
		
				// load
				xslt.Load(reader, resolver, null);

				// transform
				xslt.Transform(nav,null,output,resolver);

				return output.ToString();
			}
			catch
			{
				throw;
			}
			finally
			{
				if ( output != null )
					output.Close();

				if ( reader != null )
					reader.Close();
			}

		}

	}
}
