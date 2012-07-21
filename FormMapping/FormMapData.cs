using System;
using System.Collections;
using Ecyware.GreenBlue.HtmlDom;

namespace Ecyware.GreenBlue.FormMapping
{
	/// <summary>
	/// Summary description for FormMapData.
	/// </summary>
	[Serializable]
	public class FormMapData
	{
		private HtmlFormTag _form = null;
		private Hashtable _postData = null;
		private FormMappingDataRelationList _relations = null;

		public FormMapData()
		{
		}

		public HtmlFormTag FormTag
		{
			get
			{
				return _form;
			}
			set
			{
				_form = value;
			}
		}

		public Hashtable PostData
		{
			get
			{
				return _postData;
			}
			set
			{
				_postData = value;
			}
		}

		public FormMappingDataRelationList FormMappingRelations
		{
			get
			{
				return _relations;
			}
			set
			{
				_relations = value;
			}
		}


	}
}
