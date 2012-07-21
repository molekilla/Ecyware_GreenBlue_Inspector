using System;
using System.IO;
using System.Reflection;
using System.Collections;
using System.Security.Permissions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for WebTransform.
	/// </summary>
	[Serializable]
	public abstract class WebTransform
	{
		private Delegate[] _callbacks;
		static Random _rnd = new Random();
		private bool _isActive = true;
		private string _name = string.Empty;
		private bool _supportsCallback = false;

		/// <summary>
		/// Creates a new WebTransform.
		/// </summary>
		public WebTransform()
		{
				//[IsolatedStorageFilePermissionAttribute(SecurityAction.Demand, Unrestricted=true)]
			FileIOPermission filePerm = new FileIOPermission(PermissionState.None);
			RegistryPermission regPerm = new RegistryPermission(PermissionState.None);

			filePerm.Demand();
			regPerm.Demand();
		}

		/// <summary>
		/// Gets or sets the callbacks.
		/// </summary>
		public bool SupportsCallbacks
		{
			get
			{
				return _supportsCallback;
			}
			set
			{
				_supportsCallback = value;
			}
		}
		

		/// <summary>
		/// Adds the transforms callback.
		/// </summary>
		/// <param name="callback"> The delegate object.</param>
		public void AddTransformCallbacks(Delegate[] callback)
		{
			_callbacks = callback;
		}

		/// <summary>
		/// Gets the transforms callback.
		/// </summary>
		/// <returns> Returns a delegate.</returns>
		public Delegate[] GetTransformCallbacks()
		{
			return _callbacks;
		}

		/// <summary>
		/// Gets the delegate to use.
		/// </summary>
		/// <param name="delegateType"> The delegate type.</param>
		/// <returns> Returns a delegate.</returns>
		public Delegate GetDelegateType(Type delegateType)
		{
			Delegate useDelegate = null;

			foreach ( Delegate del in _callbacks )
			{
				if ( delegateType  == del.GetType() )
				{
					useDelegate = del;
					break;
				}
			}

			return useDelegate;
		}

		/// <summary>
		/// Gets or sets if the transform is active.
		/// </summary>
		public bool IsActive
		{
			get
			{
				return _isActive;
			}
			set
			{
				_isActive = value;
			}
			
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
		/// Gets a generate id.
		/// </summary>
		public static string GenerateID
		{
			get
			{								
				double i = _rnd.Next();
				return i.ToString();
			}
		}

		/// <summary>
		/// Applies the current transform to a web request.
		/// </summary>
		public virtual void ApplyTransform(WebRequest request, WebResponse response)
		{
			ValidateLicense();
		}

		/// <summary>
		/// Validates the license.
		/// </summary>
		public virtual void ValidateLicense()
		{
		}
		/// <summary>
		/// Gets the arguments for the current transform.
		/// </summary>
		/// <returns> An Argument type array.</returns>
		public virtual Argument[] GetArguments()
		{
			return null;
		}

		/// <summary>
		/// Clones the current object.
		/// </summary>
		/// <returns>A WebTransform type.</returns>
		public WebTransform Clone()
		{
			// new memory stream
			MemoryStream ms = new MemoryStream();
			// new BinaryFormatter
			BinaryFormatter bf = new BinaryFormatter(null,new StreamingContext(StreamingContextStates.Clone));
			// serialize
			bf.Serialize(ms, this);
			// go to beggining
			ms.Seek(0, SeekOrigin.Begin);
			// deserialize
			WebTransform retVal = (WebTransform)bf.Deserialize(ms);
			ms.Close();

			return retVal;
		}


		/// <summary>
		/// Loads the web transforms types from an assembly.
		/// </summary>
		/// <param name="assm"> The assembly to search.</param>
		/// <returns> A type array of web transforms types..</returns>
		public static Type[] LoadWebTransformsFromAssembly(Assembly assm)
		{			
			Type[] types = assm.GetTypes();

			//int count = 0;
			//bool foundTypes = false;
			ArrayList transforms = new ArrayList();

			foreach ( Type t in types )
			{
				if ( t.IsSubclassOf(typeof(Ecyware.GreenBlue.Engine.Transforms.WebTransform)) )
				{
					//foundTypes = true;
					//count++;
					transforms.Add(t);
				}
			}

			return (Type[])transforms.ToArray(typeof(Type));
		}
	}
}
