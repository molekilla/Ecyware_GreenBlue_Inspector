using System;
using System.Reflection;


namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for WebTransformAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class WebTransformAttribute : Attribute
	{
		private string _name;
		private string _type;
		private string _description;

		/// <summary>
		/// Creates a new WebTransformAttribute.
		/// </summary>
		/// <param name="name"> The name of the web transform.</param>
		/// <param name="type"> The transform type.</param>
		public WebTransformAttribute(string name, string type)
		{
			_name = name;
			_type = type;
		}

		/// <summary>
		/// Creates a new WebTransformAttribute.
		/// </summary>
		/// <param name="name"> The name of the web transform.</param>
		/// <param name="type"> The transform type.</param>
		/// <param name="description"> The web transform description.</param>
		public WebTransformAttribute(string name, string type, string description)
		{
			_name = name;
			_type = type;
			_description = description;
		}

		/// <summary>
		/// Gets or sets the web transform name.
		/// </summary>
		public string Name
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
		/// Gets or sets the transform provider type.
		/// </summary>
		public string TransformProviderType
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
			}
		}

		/// <summary>
		/// Gets or sets the description.
		/// </summary>
		public string Description
		{
			get
			{
				return _description;
			}
			set
			{
				_description = value;
			}
		}
	}
}
