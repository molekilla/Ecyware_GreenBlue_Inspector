// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;
using System.Xml;
using System.Collections;


namespace Ecyware.GreenBlue.Configuration.Inspector
{
	/// <summary>
	/// Contains the InspectorConfiguration class.
	/// </summary>
	public class InspectorConfiguration
	{
		private string _sqlTest = string.Empty;
		private string _xssTest = string.Empty;
		private int _bufferLen = 100;
		private string _breportTemplateName = string.Empty;
		private string _areportTemplateName = string.Empty;
		private string _sqlSignatures = string.Empty;
		private string _xssSignatures = string.Empty;
		private string _referenceDataFile = string.Empty;
		private string _solutionDataFile = string.Empty;

		#region Properties

		/// <summary>
		/// Gets or sets the references data xml file.
		/// </summary>
		public string ReferenceDataFile
		{
			get
			{
				return _referenceDataFile;
			}
			set
			{
				_referenceDataFile = value;
			}
		}

		/// <summary>
		/// Gets or sets the solution data xml file.
		/// </summary>
		public string SolutionDataFile
		{
			get
			{
				return _solutionDataFile;
			}
			set
			{
				_solutionDataFile = value;
			}
		}
		/// <summary>
		/// Gets or sets the XSS attack signature list.
		/// </summary>
		public string XssSignatures
		{
			get
			{
				return _xssSignatures;
			}
			set
			{
				_xssSignatures = value;
			}
		}
		/// <summary>
		/// Gets or sets the SQL Injection signature list.
		/// </summary>
		public string SqlSignatures
		{
			get
			{
				return _sqlSignatures;
			}
			set
			{
				_sqlSignatures = value;
			}
		}

		/// <summary>
		/// Gets or sets the the basic report template file.
		/// </summary>
		public string BasicReportTemplate
		{
			get
			{
				return _breportTemplateName;
			}
			set
			{
				_breportTemplateName = value;
			}
		}

		/// <summary>
		/// Gets or sets the the advanced report template file.
		/// </summary>
		public string AdvancedReportTemplate
		{
			get
			{
				return _areportTemplateName;
			}
			set
			{
				_areportTemplateName = value;
			}
		}


		/// <summary>
		/// Gets or sets the default SQL Injection test for the easy unit test action.
		/// </summary>
		public string DefaultSqlTest
		{
			get
			{
				return _sqlTest;
			}
			set
			{
				_sqlTest = value;
			}
		}

		/// <summary>
		/// Gets or sets the default XSS attack test for the easy unit test action.
		/// </summary>
		public string DefaultXssTest
		{
			get
			{
				return _xssTest;
			}
			set
			{
				_xssTest = value;
			}
		}
		/// <summary>
		/// Gets or sets the default length of the buffer overflow for the easy unit test action.
		/// </summary>
		public int DefaultBufferOverflowLength
		{
			get
			{
				return _bufferLen;
			}
			set
			{
				_bufferLen = value;
			}
		}
		#endregion

		internal InspectorConfiguration(InspectorConfiguration config)
		{

			if ( config != null )
			{
				this._referenceDataFile = config.ReferenceDataFile;
				this._solutionDataFile = config.SolutionDataFile;
				this._xssSignatures = config.XssSignatures;
				this._sqlSignatures = config.SqlSignatures;
				this._breportTemplateName = config.BasicReportTemplate;
				this._areportTemplateName = config.AdvancedReportTemplate;
				this._bufferLen  = config.DefaultBufferOverflowLength;
				this._sqlTest = config.DefaultSqlTest;
				this._xssTest = config.DefaultXssTest;
			}
		}

		internal void LoadValuesFromConfiguration(XmlNode node)
		{
			XmlAttributeCollection items = node.Attributes;

			// set
			try
			{
				this._bufferLen = Int32.Parse(items["defaultBufferOverflowLength"].Value);
			}
			catch
			{
				this._bufferLen = 100;
			}

			this._xssSignatures = items["xssSignatures"].Value;
			this._sqlSignatures = items["sqlSignatures"].Value;
			this._breportTemplateName = items["basicReportTemplate"].Value;
			this._areportTemplateName = items["advancedReportTemplate"].Value;
			this._sqlTest = items["defaultSqlTest"].Value;
			this._xssTest = items["defaultXssTest"].Value;
			this._referenceDataFile = items["referenceDataFile"].Value;
			this._solutionDataFile = items["solutionDataFile"].Value;
		}
	}
}
