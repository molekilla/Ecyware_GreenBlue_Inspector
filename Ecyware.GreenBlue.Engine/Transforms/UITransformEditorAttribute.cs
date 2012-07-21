using System;
using System.Reflection;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms.Designers;
using Ecyware.GreenBlue.Engine.Transforms;


namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for UITransformEditorAttribute.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class)]
	public class UITransformEditorAttribute : Attribute
	{
		private Type _editor;

		/// <summary>
		/// Creates a new UITransformEditorAttribute.
		/// </summary>
		public UITransformEditorAttribute(Type editor)
		{
			_editor = editor;
		}

		/// <summary>
		/// Gets or sets the ui transform editor.
		/// </summary>
		public Type UITransformEditor
		{
			get
			{
				return _editor;
			}
			set
			{
				_editor = value;
			}
		}

		/// <summary>
		/// Creates a UI Transform Editor.
		/// </summary>
		/// <returns> A UITransformEditor.</returns>
		public UITransformEditor CreateUITransformEditor()
		{
			if ( _editor != null )
			{
				// Create Transform.
				ConstructorInfo ci = _editor.GetConstructor(System.Type.EmptyTypes);
				UITransformEditor control = (UITransformEditor)ci.Invoke(new object[] {});
				return control;
			} 
			else 
			{
				return null;
			}
		}
	}
}
