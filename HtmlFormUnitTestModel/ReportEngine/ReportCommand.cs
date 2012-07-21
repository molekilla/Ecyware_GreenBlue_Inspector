// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Configuration;
using System.Windows.Forms;
using System.Security.Policy;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.HtmlDom;
using Ecyware.GreenBlue.WebUnitTestManager;
using Ecyware.GreenBlue.Engine.HtmlCommand;

namespace Ecyware.GreenBlue.ReportEngine
{
	/// <summary>
	/// Contains the report command logic.
	/// </summary>
	public class ReportCommand
	{
		/// <summary>
		/// Creates a new ReportCommand.
		/// </summary>
		public ReportCommand()
		{
		}

		/// <summary>
		/// Gets the solution description.
		/// </summary>
		/// <param name="id"> The solution id.</param>
		/// <param name="solutionDataFilePath"> The file path to the solution xml file.</param>
		/// <returns> A string with the solution description.</returns>
		private string GetSolutionDescription(string id, string solutionDataFilePath)
		{
			if ( id.Length > 0 )
			{
				return XmlItemList.GetValue(id, solutionDataFilePath);
			} 
			else 
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// Gets the reference description.
		/// </summary>
		/// <param name="id"> The solution id.</param>
		/// <param name="referenceDataFilePath"> The file path to the reference xml file.</param>
		/// <returns> A string with the reference description.</returns>
		private string GetReferenceDescription(string id, string referenceDataFilePath)
		{
			if ( id.Length > 0 )
			{
				return XmlItemList.GetValue(id, referenceDataFilePath);
			} 
			else 
			{
				return string.Empty;
			}

		}

		/// <summary>
		/// Creates the html report.
		/// </summary>
		/// <param name="report"> The report type.</param>
		/// <param name="reportTemplateFileName"> The name of the report template.</param>
		/// <param name="solutionDataFile"> The solution data file.</param>
		/// <param name="referenceDataFile"> The reference data file.</param>
		/// <returns> The html string.</returns>
		public string CreateHtmlReport(HtmlUnitTestReport report, string reportTemplateFileName, string solutionDataFile, string referenceDataFile)
		{
			string stylesheet;

			try
			{
				stylesheet = AppLocation.CommonFolder + "\\" + reportTemplateFileName;
				
				string solutionId = report.ResponseDocument[0].SolutionId;
				
				// Clone report
				HtmlUnitTestReport tempReport = (HtmlUnitTestReport)report.Copy();

				// Replace SolutionId with description
				tempReport.ResponseDocument[0].SolutionId = GetSolutionDescription(solutionId, AppLocation.CommonFolder + "\\" + solutionDataFile);

				// Replace ReferenceId with reference description
				tempReport.ResponseDocument[0].ReferenceId = GetReferenceDescription(solutionId, AppLocation.CommonFolder + "\\" + referenceDataFile);
				
				string htmlReport =  this.BuildHtmlReport(tempReport, stylesheet);
				return htmlReport;
			}
			catch
			{
				throw;
			}

		}

		/// <summary>
		/// Creates a report in html using a XSLT Template.
		/// </summary>
		/// <param name="dataset"> The HtmlUnitTestReport to load.</param>
		/// <param name="stylesheet"> The xslt stylesheet to use.</param>
		/// <returns> A string containing the html report.</returns>
		private string BuildHtmlReport(DataSet dataset, string stylesheet)
		{
			// new XmlDocument, adds a xml using GetXml()
			XmlDocument root = new XmlDocument();
			root.LoadXml(dataset.GetXml());
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
			catch (Exception ex)
			{
				ExceptionHandler.RegisterException(ex);
				throw ex;
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
		/// Serialize to xml.
		/// </summary>
		/// <param name="ds"> The HtmlUnitTestReport.</param>
		/// <param name="file"> The stream to serialize the xml.</param>
		public void SerializeToXml(HtmlUnitTestReport ds, Stream file)
		{
			//ds.AcceptChanges();
			// Save file to disc
			ds.WriteXml(file,XmlWriteMode.IgnoreSchema);
		}

		/// <summary>
		/// Serialize to schema.
		/// </summary>
		/// <param name="ds"> The HtmlUnitTestReport.</param>
		/// <param name="file"> The stream to serialize the schema.</param>
		public void SerializeToSchema(HtmlUnitTestReport ds, Stream file)
		{
			//ds.AcceptChanges();
			// Save file to disc
			ds.WriteXmlSchema(file);
		}		
	}
}
