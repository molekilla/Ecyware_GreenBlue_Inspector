using System;

namespace Ecyware.GreenBlue.Engine
{
	public enum DataType
	{
		Numeric,
		Character,
		Null
	}

	/// <summary>
	/// Summary description for DataTypeTestHelper.
	/// </summary>
	public class DataTypeTestHelper
	{
		/// <summary>
		/// Creates a new DataTypeTestHelper.
		/// </summary>
		public DataTypeTestHelper()
		{
		}

		/// <summary>
		/// Gets the value for a specific data type.
		/// </summary>
		/// <param name="dataType"> The DataType enum selected.</param>
		/// <returns> Returns a string value containing the test value.</returns>
		public static string GetDataTypeTestValue(DataType dataType)
		{
			string buffer = string.Empty;

			switch ( dataType )
			{
				case DataType.Character:
					buffer = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
					break;
				case DataType.Numeric:
					buffer = "0123456789";
					break;
				case DataType.Null:
					buffer = "";
					break;
			}

			return buffer;
		}
	}
}
