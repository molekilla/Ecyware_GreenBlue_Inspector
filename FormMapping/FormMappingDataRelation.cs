using System;

namespace Ecyware.GreenBlue.FormMapping
{
	/// <summary>
	/// Summary description for FormMappingDataRelation.
	/// </summary>
	[Serializable]
	public class FormMappingDataRelation
	{
		private string _currentValue = String.Empty;
		private string _fieldName = String.Empty;
		private FormMappingDataTransformation _formMappingCopyType;

		public FormMappingDataRelation()
		{
		}

		public FormMappingDataRelation(string fieldName, FormMappingDataTransformation formMappingCopyType, string currentValue)
		{
			this.FieldName = fieldName;
			this.FormMappingCopyType = formMappingCopyType;
			this.CurrentValue = currentValue;
		}

		public string FieldName
		{
			get
			{
				return _fieldName;
			}
			set
			{
				_fieldName = value;
			}
		}

		public FormMappingDataTransformation FormMappingCopyType
		{
			get
			{
				return _formMappingCopyType;
			}
			set
			{
				_formMappingCopyType = value;
			}
		}

		public string CurrentValue
		{
			get
			{
				return _currentValue;
			}
			set
			{
				_currentValue = value;
			}
		}
	}
	
}
