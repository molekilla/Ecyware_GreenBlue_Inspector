// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: October 2004
using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;


namespace Ecyware.GreenBlue.Configuration
{
	/// <summary>
	/// Contains the InspectorConfiguration class.
	/// </summary>
	[Serializable]	
	[XmlRoot(ElementName="inspectorConfiguration", IsNullable=true)]
	public class InspectorConfiguration : Configuration
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
		private bool _richTextParsing = true;

		#region Properties

		/// <summary>
		/// Enables the rich text parsing.
		/// </summary>
		[XmlElement("enabledRichTextParsing")]
		public bool EnabledRichTextParsing
		{
			get
			{
				return _richTextParsing;
			}
			set
			{
				_richTextParsing = value;
			}
		}

		/// <summary>
		/// Gets or sets the references data xml file.
		/// </summary>
		[XmlElement("referenceDataFile")]
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
		[XmlElement("solutionDataFile")]
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
		[XmlElement("xssSignatures")]
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
		[XmlElement("sqlSignatures")]
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
		[XmlElement("basicReportTemplate")]
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
		[XmlElement("advancedReportTemplate")]
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
		[XmlElement("defaultSqlTest")]
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
		[XmlElement("defaultXssTest")]
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
		[XmlElement("defaultBufferOverflowLength")]
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

		#region Strong Type LoadConfiguration and SaveConfiguration
		public static XmlNode SaveConfiguration(object instance, Type[] types)
		{
			return InspectorConfiguration.SaveConfiguration(typeof(InspectorConfiguration),instance,string.Empty, types);
		}
		public static InspectorConfiguration LoadConfiguration(XmlNode section)
		{
			return (InspectorConfiguration)InspectorConfiguration.LoadConfiguration(typeof(InspectorConfiguration),section);
		}

		public static InspectorConfiguration LoadConfiguration(XmlNode section, Type[] types)
		{
			return (InspectorConfiguration)InspectorConfiguration.LoadConfiguration(typeof(InspectorConfiguration), section,String.Empty, types);
		}
		#endregion
	}
}
