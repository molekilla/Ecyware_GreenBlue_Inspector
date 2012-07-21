using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Xml;
using Ecyware.GreenBlue.Controls;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Summary description for HtmlXmlTreeForm.
	/// </summary>
	public class HtmlXmlTreeForm : BasePluginForm
	{
		private System.Windows.Forms.TreeView tvXmlNodes;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Loads a HtmlXmlTreeForm.
		/// </summary>
		public HtmlXmlTreeForm()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

		}
		/// <summary>
		/// Loads a HtmlXmlTreeForm with nodes.
		/// </summary>
		/// <param name="nodes"> String nodes.</param>
		public HtmlXmlTreeForm(string nodes) :this()
		{
			LoadXmlTree(nodes);
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.tvXmlNodes = new System.Windows.Forms.TreeView();
			this.SuspendLayout();
			// 
			// tvXmlNodes
			// 
			this.tvXmlNodes.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvXmlNodes.ImageIndex = -1;
			this.tvXmlNodes.Location = new System.Drawing.Point(0, 0);
			this.tvXmlNodes.Name = "tvXmlNodes";
			this.tvXmlNodes.SelectedImageIndex = -1;
			this.tvXmlNodes.Size = new System.Drawing.Size(456, 288);
			this.tvXmlNodes.TabIndex = 0;
			// 
			// HtmlXmlTreeForm
			// 
			this.Controls.Add(this.tvXmlNodes);
			this.Name = "HtmlXmlTreeForm";
			this.Size = new System.Drawing.Size(456, 288);
			this.ResumeLayout(false);

		}
		#endregion

		private void LoadXmlTree(string nodes)
		{
			System.IO.StringReader s = new System.IO.StringReader(nodes);
			XmlTextReader reader = new XmlTextReader(s);

			TreeNode node = null;
			TreeNode elementParent = null;
			bool bEmptyTag = false;

			tvXmlNodes.BeginUpdate();

			while(reader.Read())
			{
				switch(reader.NodeType)
				{
					case XmlNodeType.Attribute:
						continue;
					case XmlNodeType.CDATA:
					case XmlNodeType.Comment:
						node = new TreeNode(reader.NodeType.ToString());
						node.Nodes.Add(reader.Value);
						if (elementParent == null)
							break;
						elementParent.Nodes.Add(node);
						continue;
					case XmlNodeType.Document:
						continue;
					case XmlNodeType.DocumentFragment:
						continue;
					case XmlNodeType.Element:
						node = new TreeNode("<" + reader.Name + ">");
						// Must be done here because of the attribute tags!
						bEmptyTag = reader.IsEmptyElement;
						while(reader.MoveToNextAttribute())
						{
							//TreeNode attNode = new TreeNode("Attribute");
							//attNode.Nodes.Add(reader.Name + "='" + reader.Value + "'");
							//node.Nodes.Add(attNode);
							string attr=" " + reader.Name + "='" + reader.Value + "'";
							node.Text=node.Text.Insert(node.Text.Length-1,attr);
						}
						if (elementParent == null)
						{
							elementParent = node;
							break;
						}
						elementParent.Nodes.Add(node);
						if (!bEmptyTag)
							elementParent = node;
						continue;
					case XmlNodeType.EndEntity:
						continue;
					case XmlNodeType.EndElement:
						elementParent = elementParent.Parent;
						continue;
					case XmlNodeType.Entity:
						continue;
					case XmlNodeType.EntityReference:
						continue;
					case XmlNodeType.None:
						continue;
					case XmlNodeType.Notation:
						continue;
					case XmlNodeType.Text:
						elementParent.Nodes.Add(reader.Value);
						continue;
					case XmlNodeType.SignificantWhitespace:
					case XmlNodeType.Whitespace:
						continue;
					case XmlNodeType.DocumentType:
					case XmlNodeType.ProcessingInstruction:
					case XmlNodeType.XmlDeclaration:
						node = new TreeNode(reader.NodeType.ToString());
						node.Nodes.Add(reader.Name + " - " + reader.Value);
						break;
					default:
						break;
				}
				tvXmlNodes.Nodes.Add(node);
			}

			tvXmlNodes.EndUpdate();
			reader.Close();
		}
	}
}
