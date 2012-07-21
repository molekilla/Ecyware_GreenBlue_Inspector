using System;

namespace Ecyware.GreenBlue.Configuration
{
	/// <summary>
	/// Summary description for ConfigurationHandlerAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class ConfigurationHandlerAttribute : Attribute
	{
		private Type _configType;

		/// <summary>
		/// Creates a new ConfigurationHandlerAttribute.
		/// </summary>
		/// <param name="configurationType"> The type of the configuration.</param>
		public ConfigurationHandlerAttribute(Type configurationType)
		{
			_configType = configurationType;
		}

		/// <summary>
		/// Gets or sets the configuration type.
		/// </summary>
		public Type ConfigurationType
		{
			get
			{
				return _configType;
			}
			set
			{
				_configType = value;
			}
		}
	}
}
