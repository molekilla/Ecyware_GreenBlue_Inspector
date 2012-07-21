// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003
using System;
using System.Windows.Forms;
using System.Drawing;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Contains the TreeEditorNode class.
	/// </summary>
	public class TreeEditorNode : TreeNode
	{
		Control _nodecontrol=null;
		Label _label=null;

		/// <summary>
		/// Creates a new TreeEditorNode.
		/// </summary>
		public TreeEditorNode() : base()
		{
		}

		/// <summary>
		/// Gets or sets the LabelControl.
		/// </summary>
		public Label LabelControl
		{
			get
			{
				return _label;
			}
			set
			{
				_label = value;
			}

		}

		/// <summary>
		/// Gets or sets the control.
		/// </summary>
		public Control NodeControl
		{
			get
			{
				return _nodecontrol;
			}
			set
			{
				_nodecontrol=value;
			}
		}

	}

}
