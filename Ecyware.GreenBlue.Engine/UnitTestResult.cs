// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004
using System;

namespace Ecyware.GreenBlue.Engine
{
	public enum UnitTestSeverity
	{
		None = 0,
		Low,
		Medium,
		High
	}

	/// <summary>
	/// Contains the unit test result use for the ASDE Pattern Evaluation.
	/// </summary>
	public class UnitTestResult
	{
		private UnitTestSeverity _severityLevel = UnitTestSeverity.None;
		
		private int _solutionId = -1;
		private int _moreInfoId = -1;

		/// <summary>
		/// Creates a new UnitTestResult.
		/// </summary>
		public UnitTestResult()
		{
		}

		/// <summary>
		/// Creates a new UnitTestResult.
		/// </summary>
		/// <param name="severity"> The severity level.</param>
		/// <param name="solutionId"> The solution id.</param>
		/// <param name="moreInfoId"> The more information id.</param>
		public UnitTestResult(UnitTestSeverity severity, int solutionId, int moreInfoId)
		{
			this.SeverityLevel = severity;
			this.SolutionId = solutionId;
			this.MoreInformationId = moreInfoId;
		}


		/// <summary>
		/// Gets or sets the addiotional information id.
		/// </summary>
		private int MoreInformationId
		{
			get
			{
				return _moreInfoId;
			}
			set
			{
				_moreInfoId = value;
			}
		}
		/// <summary>
		/// Gets or sets the solution id.
		/// </summary>
		public int SolutionId
		{
			get
			{
				return _solutionId;
			}
			set
			{
				_solutionId = value;
			}
		}

		/// <summary>
		/// Gets or sets the severity level of the test result.
		/// </summary>
		public UnitTestSeverity SeverityLevel
		{
			get
			{
				return _severityLevel;
			}
			set
			{
				_severityLevel = value;
			}
		}
	}
}
