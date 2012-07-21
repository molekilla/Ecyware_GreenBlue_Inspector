// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003
using System;
using System.Drawing;
using System.Windows.Forms;
using Ecyware.GreenBlue.Engine.HtmlDom;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Represents a node in a FormEditor Tree.
	/// </summary>
	public sealed class FormEditorNode : TreeEditorNode
	{

		private HtmlTagBase _baseHtmlTag;
		/// <summary>
		/// Creates a new FormEditorNode.
		/// </summary>
		public FormEditorNode() : base()
		{
		}

		#region Properties

		/// <summary>
		/// Gets or sets the BaseHtmlTag.
		/// </summary>
		public HtmlTagBase BaseHtmlTag
		{
			get
			{
				return _baseHtmlTag;
			}
			set
			{
				_baseHtmlTag = value;
			}
		}
		#endregion
		#region Methods
		/// <summary>
		/// Adds a control.
		/// </summary>
		/// <param name="label"> The control label value.</param>
		/// <param name="control"> The control to add.</param>
		public void AddControl(string label, Control control)
		{
			FormEditorNode newNode = new FormEditorNode();
			newNode.Text = label; 
			newNode.NodeControl = control;

			Label l = new Label();
			l.Size = new Size(control.Width, control.Height);
			l.Text=control.Text;
			newNode.LabelControl=l;

			this.Nodes.Add(newNode);
		}

		/// <summary>
		/// Adds a textbox control.
		/// </summary>
		/// <param name="label"> The control label value.</param>
		/// <param name="control"> The control to add.</param>
		/// <param name="eventHandler"> The event handler to attach.</param>
		internal void AddTextControl(string label, TextBox control,EventHandler eventHandler)
		{
			FormEditorNode newNode = new FormEditorNode();
			newNode.Text = label;
			control.TextChanged += eventHandler;
			control.Tag = this;
			newNode.NodeControl = control;

			Label l = new Label();
			l.Size = new Size(control.Width, control.Height);
			l.Text = control.Text;
			newNode.LabelControl=l;

			this.Nodes.Add(newNode);
		}

		/// <summary>
		/// Adds a ListBox control.
		/// </summary>
		/// <param name="label"> The control label value.</param>
		/// <param name="control"> The control to add.</param>
		/// <param name="doubleClickHandler"> The double click event handler to attach.</param>
		/// <param name="selectValueChangedHandler"> The selected value event handler to attach.</param>
		internal void AddListBoxControl(string label, ListBox control,EventHandler doubleClickHandler, EventHandler selectValueChangedHandler)
		{
			FormEditorNode newNode = new FormEditorNode();
			newNode.Text=label;
			control.DoubleClick += doubleClickHandler;
			control.SelectedValueChanged+=selectValueChangedHandler;
			control.Tag = this;
			newNode.NodeControl=control;

			Label l = new Label();
			l.Size = new Size(control.Width, control.Height);
			if ( control.SelectedItems.Count > 0 )
			{
				l.Text = (string)control.SelectedValue;
			} 
			else 
			{				 
				l.Text = ((HtmlSelectTag)this.BaseHtmlTag).Value;
				control.SelectedValue = l.Text;
			}
			newNode.LabelControl=l;

			this.Nodes.Add(newNode);
		}

		/// <summary>
		/// Adds a ComboBox control.
		/// </summary>
		/// <param name="label"> The control label value.</param>
		/// <param name="control"> The control to add.</param>
		/// <param name="eventHandler"> The event handler to attach.</param>
		internal void AddComboBoxControl(string label, ComboBox control,EventHandler eventHandler)
		{
			FormEditorNode newNode = new FormEditorNode();
			newNode.Text=label;
			control.SelectedIndexChanged+=eventHandler;
			control.Tag = this;
			newNode.NodeControl=control;

			Label l = new Label();
			l.Size = new Size(control.Width, control.Height);
			l.Text = (string)control.SelectedValue;
			newNode.LabelControl=l;

			this.Nodes.Add(newNode);
		}

		#endregion
	}
}
