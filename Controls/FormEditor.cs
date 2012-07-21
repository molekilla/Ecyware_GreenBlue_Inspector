// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2004

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Data;
using Ecyware.GreenBlue.Engine.HtmlDom;
using System.ComponentModel;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Allows manipulation of forms in a tree structure design.
	/// </summary>
	public sealed class FormEditor : TreeEditor
	{
		#region Constructors
		/// <summary>
		/// Creates a new FormEditor.
		/// </summary>
		public FormEditor() : base()
		{
			this.ItemHeight = 20;
			this.Scrollable = true;
		}

		/// <summary>
		/// Creates a new FormEditor.
		/// </summary>
		/// <param name="itemHeight"> The item height.</param>
		public FormEditor(int itemHeight) : base()
		{
			this.ItemHeight = itemHeight;
			this.Scrollable = true;
		}
		#endregion


		/// <summary>
		/// Clears the tree editor controls.
		/// </summary>
		public new void Clear()
		{
			base.Clear();
			this.Nodes.Clear();
			this.Controls.Clear();
		}

		/// <summary>
		/// Gets or sets the selected node.
		/// </summary>
		public new FormEditorNode SelectedNode
		{
			get
			{
				return (FormEditorNode)base.SelectedNode;
			}
			set
			{
				base.SelectedNode=value;
			}
		}


		#region " Add nodes"
		/// <summary>
		/// Adds a Form Node.
		/// </summary>
		/// <param name="node"> A FormEditorNode to add.</param>
		/// <param name="text"> The node text.</param>
		/// <param name="formtag"> The HtmFormTag.</param>
		public void AddFormNode(FormEditorNode node,string text, HtmlFormTag formtag)
		{			
			node.Text=text;
			node.BaseHtmlTag=formtag;
			this.Nodes.Add(node);
		}

		/// <summary>
		/// Adds a FormNode and returns the added node.
		/// </summary>
		/// <param name="text"> The node text.</param>
		/// <param name="formtag"> The HtmlFormTag.</param>
		/// <returns> Returns a FormEditorNode.</returns>
		public FormEditorNode AddFormNode(string text, HtmlFormTag formtag)
		{
			FormEditorNode node = new FormEditorNode();
			node.Text=text;
			node.BaseHtmlTag=formtag;
			this.Nodes.Add(node);

			return node;
		}

		/// <summary>
		/// Adds a Input node.
		/// </summary>
		/// <param name="root"> The root node.</param>
		/// <param name="text"> The node text.</param>
		/// <param name="inputTag"> The HtmlInputTag.</param>
		public void AddInput(FormEditorNode root, string text,HtmlInputTag inputTag)
		{
			FormEditorNode node = new FormEditorNode();

			// add it to root node
			root.Nodes.Add(node);

			node.Text=text;
			node.BaseHtmlTag = inputTag;
			
			TextBox[] textboxes;

			switch (inputTag.Type)
			{
				case HtmlInputType.Button:
					textboxes = GetTextBoxArray(1);
					textboxes[0].Name = "txtValue";
					textboxes[0].Text = inputTag.Value;
					node.AddTextControl("Value:",textboxes[0],new EventHandler(TextChangeValue));
					break;
				case HtmlInputType.Checkbox:
					textboxes = GetTextBoxArray(1);
					textboxes[0].Name = "txtValue";
					textboxes[0].Text = inputTag.Value;
					node.AddTextControl("Value:",textboxes[0],new EventHandler(TextChangeValue));
					break;
				case HtmlInputType.Text:
					textboxes = GetTextBoxArray(1);
					textboxes[0].Name = "txtValue";
					textboxes[0].Text = inputTag.Value;
					node.AddTextControl("Value:",textboxes[0],new EventHandler(TextChangeValue));
					break;
				case HtmlInputType.Submit:
					textboxes = GetTextBoxArray(1);
					textboxes[0].Name = "txtValue";
					textboxes[0].Text = inputTag.Value;
					node.AddTextControl("Value:",textboxes[0],new EventHandler(TextChangeValue));
					break;
				case HtmlInputType.Password:
					textboxes = GetTextBoxArray(1);
					textboxes[0].Name = "txtValue";
					textboxes[0].Text = inputTag.Value;
					node.AddTextControl("Value:",textboxes[0],new EventHandler(TextChangeValue));
					break;
				default:
					textboxes = GetTextBoxArray(1);
					textboxes[0].Name = "txtValue";
					textboxes[0].Text = inputTag.Value;
					node.AddTextControl("Value:",textboxes[0],new EventHandler(TextChangeValue));
					break;
			}		
		}

		/// <summary>
		/// Adds a TextArea node.
		/// </summary>
		/// <param name="root"> The root node.</param>
		/// <param name="text"> The node text.</param>
		/// <param name="tag"> The HtmlTextAreaTag.</param>
		public void AddTextArea(FormEditorNode root, string text,HtmlTextAreaTag tag)
		{
			FormEditorNode node = new FormEditorNode();

			// add it to root node
			root.Nodes.Add(node);

			node.Text=text;
			node.BaseHtmlTag = tag;
			
			TextBox[] textboxes;

			textboxes = GetTextBoxArray(1);
			textboxes[0].Size = new Size(300,15);
			textboxes[0].Name = "txtValue";
			textboxes[0].Text = tag.Value;
			node.AddTextControl("Value:",textboxes[0],new EventHandler(TextAreaChangeValue));

		}

		/// <summary>
		/// Adds a link node.
		/// </summary>
		/// <param name="root"> The root node.</param>
		/// <param name="text"> The node text.</param>
		/// <param name="tag"> The HtmlALinkTag.</param>
		public void AddALink(FormEditorNode root, string text,HtmlALinkTag tag)
		{
			FormEditorNode node = new FormEditorNode();

			// add it to root node
			root.Nodes.Add(node);
			node.BaseHtmlTag = tag;
			node.Text = text;

		}

		/// <summary>
		/// Adds a button node.
		/// </summary>
		/// <param name="text">The node text.</param>
		/// <param name="button"> The HtmlButtonTag.</param>
		public void AddButton(string text,HtmlButtonTag button)
		{
			FormEditorNode node = new FormEditorNode();
			node.Text=text;
			node.BaseHtmlTag = button;

			TextBox[] textboxes;
			textboxes = GetTextBoxArray(1);
			textboxes = GetTextBoxArray(1);
			textboxes[0].Name = "txtValue";
			textboxes[0].Text = button.Value;
			node.AddControl("Value:",textboxes[0]);
		}

		/// <summary>
		/// Adds a Select node.
		/// </summary>
		/// <param name="root"> The root node.</param>
		/// <param name="text"> The node text.</param>
		/// <param name="tag"> The HtmlSelectTag.</param>
		public void AddSelect(FormEditorNode root, string text,HtmlSelectTag tag)
		{
			FormEditorNode node = new FormEditorNode();

			// add it to root node
			root.Nodes.Add(node);

			node.Text=text;
			node.BaseHtmlTag = tag;


			if ( tag.Multiple )
			{
				ListBox lst = new ListBox();
				//lst.HorizontalScrollbar=true;
				//lst.DoubleClick+=new EventHandler(ListBox_DoubleClick);
				lst.Size=new Size(200,30);
				lst.BackColor=Color.WhiteSmoke;
				lst.ForeColor=Color.Blue;
				foreach (HtmlOptionTag opt in tag.Options)
				{
					lst.Items.Add(opt.Value);
				}
				//lst.Name="lstList";
				lst.SelectionMode=SelectionMode.MultiExtended;
				this.ItemHeight=25;

				node.AddListBoxControl("Values:",lst,new EventHandler(ListBox_DoubleClick),new EventHandler(ListBox_SelectedValueChanged));
			} 
			else 
			{			
				ListBox lst = new ListBox();
				//lst.DropDownStyle=ComboBoxStyle.DropDown;
				lst.Size=new Size(200,30);
				lst.BackColor=Color.WhiteSmoke;
				lst.ForeColor=Color.Blue;
				foreach ( HtmlOptionTag opt in tag.Options)
				{
					lst.Items.Add(opt.Value);
				}
				//lst.Name="cmbList";
				lst.SelectionMode=SelectionMode.One;
				this.ItemHeight=25;

				node.AddListBoxControl("Values:",lst,new EventHandler(ListBox_DoubleClick),new EventHandler(ListBox_SelectedValueChanged));
			}

		}

		#endregion

		/// <summary>
		/// Retrieves an ArrayList containing the options element from a HtmlOptionCollection.
		/// </summary>
		/// <param name="coll"> The HtmlOptionCollection.</param>
		/// <returns> An ArrayList containing the option values.</returns>
		private ArrayList GetList(HtmlOptionCollection coll)
		{
			ArrayList t = new ArrayList();
			foreach (DictionaryEntry de in coll)
			{
				t.Add(((HtmlOptionTag)de.Value).Value);
			}

			return t;
		}


		#region " Node Control events"
		/// <summary>
		/// Changes the text on a control.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The EventArgs object.</param>
		private void TextChangeValue(object sender, EventArgs e)
		{
			TextBox control = (TextBox)sender;
			FormEditorNode node = (FormEditorNode)control.Tag;
			HtmlInputTag tag = (HtmlInputTag)node.BaseHtmlTag;
			tag.Value = control.Text;
		}

		/// <summary>
		/// Changes the text in a textarea node.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The EventArgs object.</param>
		private void TextAreaChangeValue(object sender, EventArgs e)
		{
			TextBox control = (TextBox)sender;
			FormEditorNode node = (FormEditorNode)control.Tag;
			HtmlTextAreaTag tag=(HtmlTextAreaTag)node.BaseHtmlTag;
			tag.Value = control.Text;
		}

		/// <summary>
		/// Raises when a double click is made on a listbox.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The EventArgs object.</param>
		private void ListBox_DoubleClick(object sender, EventArgs e)
		{
			ListBoxInputForm formDialog = new ListBoxInputForm();
			ListBox lst = (ListBox)sender;
			FormEditorNode node = (FormEditorNode)lst.Tag;			
			
			// set current value
			formDialog.ListBoxValue = (string)lst.SelectedItem;

			if ( formDialog.ShowDialog()==DialogResult.OK )
			{
				string oldValue = (string)lst.SelectedItem;

				// save new value in listbox
				int temp = lst.SelectedIndex;
				lst.Items.Insert(temp,formDialog.ListBoxValue);
				lst.Items.RemoveAt(temp+1);
				lst.SelectedIndex=temp;
				
				// save in option tag
				HtmlSelectTag tag = (HtmlSelectTag)node.BaseHtmlTag;
				HtmlOptionTag option = tag.Options[lst.SelectedIndex];
				option.Value = formDialog.ListBoxValue;

				// if not multiple, save in Value
				if ( !tag.Multiple )
				{
					tag.Value = formDialog.ListBoxValue;
				}
			}			
		}

		/// <summary>
		/// Raises when a value is changed on a listbox.
		/// </summary>
		/// <param name="sender"> The sender object.</param>
		/// <param name="e"> The EventArgs object.</param>
		private void ListBox_SelectedValueChanged(object sender, EventArgs e)
		{
			ListBox control = (ListBox)sender;
			if ( control.SelectedIndex == -1 ) return;

			FormEditorNode node = (FormEditorNode)control.Tag;
			HtmlSelectTag tag=(HtmlSelectTag)node.BaseHtmlTag;

			// list box selected item
			string selection = (string)control.SelectedItem;

			// add selection to tag and option
			tag.Value = selection;
			HtmlOptionTag option = tag.Options[control.SelectedIndex];
			option.Selected = true;
		}

	#endregion
	}
}
