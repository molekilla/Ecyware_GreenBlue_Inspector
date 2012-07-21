using System;
using Ecyware.GreenBlue.Engine.Scripting;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for ClientSettingsTransformValue.
	/// </summary>
	[Serializable]
	public class ClientSettingsTransformValue : TransformValue
	{
		private string _name;
		/// <summary>
		/// Creates a ClientSettingsTransformValue.
		/// </summary>
		public ClientSettingsTransformValue()
		{
		}


		/// <summary>
		/// Gets or sets the field name.
		/// </summary>
		public string FieldName
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}

		/// <summary>
		/// Gets the value from the web response.
		/// </summary>
		/// <param name="response"> The web response.</param>
		/// <returns> An object.</returns>
		public override object GetValue(WebResponse response)
		{	
			Type clientSettingsType = typeof(HttpProperties);
			object value = clientSettingsType.GetProperty(_name).GetValue(response.ResponseHttpSettings, null);
			return value;
		}

	}
}
