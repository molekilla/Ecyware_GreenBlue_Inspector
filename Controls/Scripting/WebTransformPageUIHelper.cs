// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using System.Windows.Forms;
using System.Collections;
using Ecyware.GreenBlue.Engine.Transforms;
using Ecyware.GreenBlue.Engine.Transforms.Designers;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Configuration;

namespace Ecyware.GreenBlue.Controls.Scripting
{
	public class TransformProviderMenuItem : System.Windows.Forms.MenuItem
	{
		private TransformProvider provider;

		/// <summary>
		/// Creates a new TransformProviderMenu.
		/// </summary>
		public TransformProviderMenuItem() : base()
		{
		}

		/// <summary>
		/// Gets or sets the transform provider.
		/// </summary>
		public TransformProvider TransformProvider
		{
			get
			{
				return provider;
			}
			set
			{
				provider = value;
			}
		}

	}
	/// <summary>
	/// Summary description for WebTransformPageUIHelper.
	/// </summary>
	public class WebTransformPageUIHelper
	{
		static string configurationSection = "WebTransforms";
		//static Hashtable _transformEditors = null;
		//static ArrayList _controls = null;

		private WebTransformPageUIHelper()
		{
		}



		/// <summary>
		/// Loads the transform providers.
		/// </summary>
		/// <param name="menu"> The context menu.</param>
		/// <param name="onclick"> The onclick eventhandler.</param>
		public static void LoadInputTransformProviders(ContextMenu menu, EventHandler onclick)
		{
			// Get Configuration
			WebTransformConfiguration config = (WebTransformConfiguration)ConfigManager.Read(configurationSection, false);
			
			System.Windows.Forms.MenuItem parent = new System.Windows.Forms.MenuItem("Add Web Transforms");
			menu.MenuItems.Add(parent);

			foreach ( TransformProvider provider in config.Transforms )
			{
				// Get WebTransformAttribute
				LoadWebTransformAttribute(provider);

				if ( provider.TransformType.ToLower(System.Globalization.CultureInfo.InvariantCulture) == "input" )
				{
					TransformProviderMenuItem menuTransform = new TransformProviderMenuItem();		
					menuTransform.Text = provider.Name;
					menuTransform.TransformProvider = provider;
					menuTransform.Click += onclick;
					parent.MenuItems.Add(menuTransform);
				}
			}			
		}

		/// <summary>
		/// Loads the web transform attribute using the transform provider type.
		/// </summary>
		/// <param name="provider"> A TransformProvider type.</param>
		/// <returns> An updated transform provider.</returns>
		public static void LoadWebTransformAttribute(TransformProvider provider)
		{
			// Get WebTransformAttribute
			Type type = Type.GetType(provider.Type);
			WebTransformAttribute attribute = (WebTransformAttribute)Attribute.GetCustomAttribute(type, typeof (WebTransformAttribute));

			provider.Name = attribute.Name;
			provider.TransformType = attribute.TransformProviderType;
		}



		/// <summary>
		/// Checks if the typename is an output transform.
		/// </summary>
		/// <param name="typeName"> The type name.</param>
		/// <returns> Returns true if transform is output, else false.</returns>
		public static bool IsOutputTransform(string typeName)
		{
			// Get Configuration
			WebTransformConfiguration config = (WebTransformConfiguration)ConfigManager.Read(configurationSection, true);
			bool result = false;
			
			foreach ( TransformProvider provider in config.Transforms )
			{
				// Get WebTransformAttribute
				LoadWebTransformAttribute(provider);

				if ( provider.Type.IndexOf(typeName) > -1 )
				{
					if ( provider.TransformType.ToLower(System.Globalization.CultureInfo.InvariantCulture) == "output" )
					{
						result = true;
					}
				}
			}

			return result;
		}

		/// <summary>
		/// Loads the transform providers.
		/// </summary>
		/// <param name="menu"> The context menu.</param>
		/// <param name="onclick"> The onclick eventhandler.</param>
		public static void LoadOutputTransformProviders(ContextMenu menu, EventHandler onclick)
		{
			// Get Configuration
			WebTransformConfiguration config = (WebTransformConfiguration)ConfigManager.Read(configurationSection, false);
			
			System.Windows.Forms.MenuItem parent = new System.Windows.Forms.MenuItem("Add Web Transforms");
			menu.MenuItems.Add(parent);

			foreach ( TransformProvider provider in config.Transforms )
			{
				// Get WebTransformAttribute
				LoadWebTransformAttribute(provider);

				if ( provider.TransformType.ToLower(System.Globalization.CultureInfo.InvariantCulture) == "output" )
				{
					TransformProviderMenuItem menuTransform = new TransformProviderMenuItem();		
					menuTransform.Text = provider.Name;
					menuTransform.TransformProvider = provider;
					menuTransform.Click += onclick;
					parent.MenuItems.Add(menuTransform);
				}
			}			
		}


		/// <summary>
		/// Load the web transforms.
		/// </summary>
		/// <param name="treeNode"> The tree view.</param>
		/// <param name="transforms"> The web transforms to load.</param>
		public static void LoadTransforms(TreeNode treeNode, WebTransform[] transforms)
		{
			treeNode.Nodes.Clear();
			foreach ( WebTransform transform in transforms )
			{
				TreeNode node = new TreeNode(transform.Name);
				node.Tag = transform;
				treeNode.Nodes.Add(node);
			}
		}


		/// <summary>
		/// Gets the web transforms from a tree view.
		/// </summary>
		/// <param name="tv"> The tree view.</param>
		/// <returns> A WebTransform array.</returns>
		public static WebTransform[] GetTransforms(TreeView tv)
		{
			WebTransform[] transforms = null;


			if ( tv.Nodes[0].Nodes.Count > 0 )
			{
				if ( tv.Nodes[0].Nodes[0].Tag == null )
				{
					// Is a Dummy Node, return empty
					transforms = new WebTransform[0];
				} 
				else 
				{
					int count = tv.Nodes[0].Nodes.Count;
					transforms = new WebTransform[count];
					for (int i=0;i<count;i++)
					{				
						TreeNode node = tv.Nodes[0].Nodes[i];
						WebTransform transform = (WebTransform)node.Tag;
						transforms[i] = transform;
					}
				}
			}

			
			return transforms;
		}

		/// <summary>
		/// Shows the current control.
		/// </summary>
		/// <param name="container"> The control container.</param>
		/// <param name="control"> The current selected control type.</param>
		public static void ShowTransformEditorControl(Panel container, UITransformEditor control)
		{
			control.Show();

			for (int i=0;i<container.Controls.Count;i++ )
			{
				if ( !(container.Controls[i].GetType() == control.GetType() ) )
				{
					container.Controls[i].Hide();
				}
			}
		}

		/// <summary>
		/// Gets the editor control.
		/// </summary>
		/// <param name="container"> The control container.</param>
		/// <param name="editorType"> The type of the editor.</param>
		/// <returns></returns>
		public static UITransformEditor LookupTransformEditorControl(Panel container, Type editorType)
		{

			UITransformEditor page = null;

			for (int i=0;i<container.Controls.Count;i++ )
			{
				if ( container.Controls[i].GetType() ==  editorType )
				{
					// Update Page Changes.
					page = (UITransformEditor)container.Controls[i];
					break;
				}
			}

			return page;
		}

	}
}
