using System;
using System.Collections;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms;
using Ecyware.GreenBlue.Engine.Transforms.Designers;

namespace Ecyware.GreenBlue.Controls.Scripting
{
	/// <summary>
	/// Summary description for UITransformEditorManager.
	/// </summary>
	public class UITransformEditorManager
	{
		private static ArrayList _controls;

		public UITransformEditorManager()
		{
		}

		/// <summary>
		/// Get transforms editors.
		/// </summary>
		/// <returns> A UITransformEditor array.</returns>
		public UITransformEditor[] LoadTransformEditors()
		{
			if ( _controls == null )
			{	
				_controls = new ArrayList();
				// Get Configuration
				WebTransformConfiguration config = (WebTransformConfiguration)ConfigManager.Read("WebTransforms", true);

				Type[] _transforms = new Type[config.Transforms.Length];
				for (int j=0;j<_transforms.Length;j++)
				{
					_transforms[j] = Type.GetType(config.Transforms[j].Type);

					if ( _transforms[j] == null )
					{
						throw new ArgumentNullException("Type in TransformProvider", "The type in TransformProvider " + config.Transforms[j].Name + " cannot be found.");
					}
				}

				for (int i=0;i<_transforms.Length;i++)
				{				
					UITransformEditor t = GetTransformEditor(_transforms[i]);
					_controls.Add(t);
				}
			}

			return (UITransformEditor[])_controls.ToArray(typeof(UITransformEditor));
		}

		/// <summary>
		/// Gets the transform editor.
		/// </summary>
		/// <param name="transform"> The web transform.</param>
		/// <returns> A UserControl.</returns>
		public UITransformEditor GetTransformEditor(Type transform)
		{
			UITransformEditorAttribute attribute = (UITransformEditorAttribute)Attribute.GetCustomAttribute(transform, typeof (UITransformEditorAttribute));
			return attribute.CreateUITransformEditor();
		}

		/// <summary>
		/// Gets the transform editor type.
		/// </summary>
		/// <param name="transform"> The web transform.</param>
		/// <returns></returns>
		public Type GetTransformEditorType(Type transform)
		{
			UITransformEditorAttribute attribute = (UITransformEditorAttribute)Attribute.GetCustomAttribute(transform, typeof (UITransformEditorAttribute));
			return attribute.UITransformEditor;
		}
	}
}
