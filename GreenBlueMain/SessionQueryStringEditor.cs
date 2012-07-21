// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: June 2004 - July 2004
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Text;
using Ecyware.GreenBlue.Controls;
using Ecyware.GreenBlue.HtmlCommand;
using Ecyware.GreenBlue.HtmlDom;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Contains the definition for the SessionQueryStringEditor control.
	/// </summary>
	public class SessionQueryStringEditor : BaseSessionDesignerUserControl
	{
		private FormConverter formConverter = new FormConverter();
		private Hashtable queryDataValues = null;
		private string _queryString = string.Empty;
		private string separator = string.Empty;
		private string pairSeparator = string.Empty;

		internal event UpdateSessionRequestEventHandler UpdateSessionRequestEvent;

		private Ecyware.GreenBlue.Controls.TreeEditor queryStringEditor;
		private System.Windows.Forms.GroupBox grpQueryStringEditor;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button bntApply;
		private System.Windows.Forms.TextBox txtQueryString;
		private System.Windows.Forms.TextBox txtSeparator;
		private System.Windows.Forms.TextBox txtAddonSeparator;
		private System.Windows.Forms.ContextMenu mnuQueryString;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnReset;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;


		/// <summary>
		/// Creates a new SessionQueryStringEditor.
		/// </summary>
		public SessionQueryStringEditor()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
			queryStringEditor.ItemHeight = 20;
		}

		/// <summary>
		/// Creates a new SessionQueryStringEditor.
		/// </summary>
		/// <param name="queryString"> Query string to load.</param>
		public SessionQueryStringEditor(string queryString) : this()
		{
			if ( queryString.Length > 0 )
			{
				// Load tree
				LoadQueryString(queryString);
			}
		}

		/// <summary>
		/// Gets or sets the query string.
		/// </summary>
		public string QueryString
		{
			get
			{
				return _queryString;
			}
			set
			{
				_queryString = value;
			}
		}

		/// <summary>
		/// Loads the current view
		/// </summary>
		public void LoadCurrentView()
		{
			// Show text
			LoadQueryString();
		}

		#region Methods
		/// <summary>
		/// Loads the query string tree.
		/// </summary>
		/// <param name="queryString"></param>
		public void LoadQueryStringTree(string queryString)
		{
			this.QueryString = queryString;
			LoadQueryStringTree();
		}

		/// <summary>
		/// Loads the query string tree.
		/// </summary>
		public void LoadQueryStringTree()
		{

			string separator = this.txtSeparator.Text;
			string addonSeparator = this.txtAddonSeparator.Text;

			if ( this.QueryString.Length > 0 )
			{
				queryStringEditor.Clear();				

				Hashtable queryStringValues = formConverter.ConvertQueryString(this.QueryString, separator, addonSeparator);

				// Create parent node
				TreeEditorNode parentNode = new TreeEditorNode();
				parentNode.Text = "Query String";

				queryStringEditor.Nodes.Add(parentNode);

				#region Load Query String Tree
				foreach ( DictionaryEntry de in queryStringValues )
				{
					string name = (string)de.Key;
					ArrayList values = (ArrayList)de.Value;
								
					TreeEditorNode labelNode = new TreeEditorNode();
					labelNode.Text = name;
					parentNode.Nodes.Add(labelNode);

					int i = 0;
					foreach ( object val in values )
					{
						string value = (string)val;
						queryStringEditor.AddTextBoxControl(labelNode, "Index " + i.ToString() + ":", value, 350);
										
						i++;
					}
				}

				queryStringEditor.ExpandAll();
				#endregion

				this.txtQueryString.Visible = true;
				this.txtQueryString.Dock = DockStyle.Top;
				this.txtQueryString.Height = 50;
				this.queryStringEditor.Visible = true;
			}
		}


		/// <summary>
		/// Loads the query string text.
		/// </summary>
		public void LoadQueryString()
		{
			this.txtQueryString.Text = this.QueryString;
			this.txtQueryString.Dock = DockStyle.Fill;
			this.txtQueryString.Visible = true;
			this.queryStringEditor.Visible = false;
		}

		/// <summary>
		/// Loads the query string text.
		/// </summary>
		public void LoadQueryString(string queryString)
		{
			this.QueryString = queryString;
			LoadQueryString();
		}

		#endregion
		/// <summary>
		/// Displays a message indicating that no data could be showed.
		/// </summary>
		public void DisplayNoDataMessage()
		{
			queryStringEditor.Clear();

			TreeEditorNode node = new TreeEditorNode();
			node.Text = "No data available for display.";
			queryStringEditor.Nodes.Add(node);
		}
		
		/// <summary>
		/// Saves the post data changes.
		/// </summary>
		private void SaveQueryStringChanges()
		{

			if ( queryStringEditor.Nodes.Count > 0 )
			{
				Hashtable values =  new Hashtable();

				foreach ( TreeEditorNode tn in queryStringEditor.Nodes[0].Nodes )
				{
					// get key
					string key = tn.Text;

					ArrayList indices = new ArrayList(tn.Nodes.Count);
					
					if ( tn.Nodes.Count > 0 )
					{
						// it has deeper nodes
						foreach ( TreeEditorNode valueNode in tn.Nodes )
						{
							indices.Add(queryStringEditor.GetTextBoxValue(valueNode));
						}
					} 
					else 
					{
						// else it has simple nodes
						indices.Add(queryStringEditor.GetTextBoxValue(tn));
					}

					// add item to hashtable
					values.Add(key, indices);
				}

				queryDataValues = values;
			}
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
			this.queryStringEditor = new Ecyware.GreenBlue.Controls.TreeEditor();
			this.grpQueryStringEditor = new System.Windows.Forms.GroupBox();
			this.bntApply = new System.Windows.Forms.Button();
			this.txtAddonSeparator = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtSeparator = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.mnuQueryString = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.txtQueryString = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnReset = new System.Windows.Forms.Button();
			this.grpQueryStringEditor.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// queryStringEditor
			// 
			this.queryStringEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.queryStringEditor.ImageIndex = -1;
			this.queryStringEditor.Location = new System.Drawing.Point(3, 16);
			this.queryStringEditor.Name = "queryStringEditor";
			this.queryStringEditor.SelectedImageIndex = -1;
			this.queryStringEditor.SelectedNode = null;
			this.queryStringEditor.Size = new System.Drawing.Size(570, 239);
			this.queryStringEditor.Sorted = true;
			this.queryStringEditor.TabIndex = 0;
			this.queryStringEditor.Visible = false;
			// 
			// grpQueryStringEditor
			// 
			this.grpQueryStringEditor.Controls.Add(this.btnReset);
			this.grpQueryStringEditor.Controls.Add(this.bntApply);
			this.grpQueryStringEditor.Controls.Add(this.txtAddonSeparator);
			this.grpQueryStringEditor.Controls.Add(this.label2);
			this.grpQueryStringEditor.Controls.Add(this.txtSeparator);
			this.grpQueryStringEditor.Controls.Add(this.label1);
			this.grpQueryStringEditor.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpQueryStringEditor.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.grpQueryStringEditor.Location = new System.Drawing.Point(0, 0);
			this.grpQueryStringEditor.Name = "grpQueryStringEditor";
			this.grpQueryStringEditor.Size = new System.Drawing.Size(576, 78);
			this.grpQueryStringEditor.TabIndex = 1;
			this.grpQueryStringEditor.TabStop = false;
			this.grpQueryStringEditor.Text = "Edit Query String";
			// 
			// bntApply
			// 
			this.bntApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.bntApply.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.bntApply.Location = new System.Drawing.Point(492, 18);
			this.bntApply.Name = "bntApply";
			this.bntApply.TabIndex = 6;
			this.bntApply.Text = "Apply";
			this.bntApply.Click += new System.EventHandler(this.bntApply_Click);
			// 
			// txtAddonSeparator
			// 
			this.txtAddonSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtAddonSeparator.BackColor = System.Drawing.Color.White;
			this.txtAddonSeparator.Location = new System.Drawing.Point(120, 42);
			this.txtAddonSeparator.MaxLength = 5;
			this.txtAddonSeparator.Name = "txtAddonSeparator";
			this.txtAddonSeparator.Size = new System.Drawing.Size(288, 20);
			this.txtAddonSeparator.TabIndex = 5;
			this.txtAddonSeparator.Text = "=";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 43);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(108, 18);
			this.label2.TabIndex = 4;
			this.label2.Text = "Additional Separator";
			// 
			// txtSeparator
			// 
			this.txtSeparator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.txtSeparator.BackColor = System.Drawing.Color.White;
			this.txtSeparator.Location = new System.Drawing.Point(120, 18);
			this.txtSeparator.MaxLength = 5;
			this.txtSeparator.Name = "txtSeparator";
			this.txtSeparator.Size = new System.Drawing.Size(288, 20);
			this.txtSeparator.TabIndex = 3;
			this.txtSeparator.Text = "&";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(60, 18);
			this.label1.TabIndex = 2;
			this.label1.Text = "Separator";
			// 
			// mnuQueryString
			// 
			this.mnuQueryString.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						   this.menuItem1,
																						   this.menuItem4,
																						   this.menuItem7,
																						   this.menuItem8,
																						   this.menuItem9});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem3,
																					  this.menuItem11});
			this.menuItem1.Text = "Encode";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "Url";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "Html";
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 2;
			this.menuItem11.Text = "Base64";
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 1;
			this.menuItem4.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem5,
																					  this.menuItem6,
																					  this.menuItem10});
			this.menuItem4.Text = "Decode";
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 0;
			this.menuItem5.Text = "Html";
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 1;
			this.menuItem6.Text = "Url";
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 2;
			this.menuItem10.Text = "Base64";
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 2;
			this.menuItem7.Text = "Generate Buffer...";
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 3;
			this.menuItem8.Text = "Add SQL Injection...";
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 4;
			this.menuItem9.Text = "Add XSS Attack...";
			// 
			// txtQueryString
			// 
			this.txtQueryString.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtQueryString.ForeColor = System.Drawing.Color.Blue;
			this.txtQueryString.Location = new System.Drawing.Point(3, 16);
			this.txtQueryString.Multiline = true;
			this.txtQueryString.Name = "txtQueryString";
			this.txtQueryString.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtQueryString.Size = new System.Drawing.Size(570, 239);
			this.txtQueryString.TabIndex = 2;
			this.txtQueryString.Text = "";
			this.txtQueryString.Visible = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.queryStringEditor);
			this.groupBox1.Controls.Add(this.txtQueryString);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(0, 78);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(576, 258);
			this.groupBox1.TabIndex = 3;
			this.groupBox1.TabStop = false;
			// 
			// btnReset
			// 
			this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnReset.Location = new System.Drawing.Point(492, 48);
			this.btnReset.Name = "btnReset";
			this.btnReset.TabIndex = 7;
			this.btnReset.Text = "Reset";
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// SessionQueryStringEditor
			// 
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.grpQueryStringEditor);
			this.Name = "SessionQueryStringEditor";
			this.Size = new System.Drawing.Size(576, 336);
			this.grpQueryStringEditor.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void bntApply_Click(object sender, System.EventArgs e)
		{
			this.separator = txtSeparator.Text;
			this.pairSeparator = txtAddonSeparator.Text;

			if ( ( this.txtAddonSeparator.Text.Length == 0 ) && ( this.txtSeparator.Text.Length == 0 ) )
			{
				this.txtQueryString.Dock = DockStyle.Fill;
				// this.txtQueryString.Height = 50;

				this.txtQueryString.Visible = true;
				this.queryStringEditor.Visible = false;
			} else {
				this.LoadQueryStringTree( this.txtQueryString.Text );
				this.SaveQueryStringChanges();

				if ( this.queryDataValues != null )
				{
					this.txtQueryString.Text = formConverter.ConvertQueryHashtable(this.queryDataValues, this.separator, this.pairSeparator);
				}
			}
		}


		/// <summary>
		/// Updates the session request data.
		/// </summary>
		public override void UpdateSessionRequestData()
		{
			if ( queryStringEditor.Nodes.Count > 0 )
			{
				this.SaveQueryStringChanges();
				UpdateSessionRequestEventArgs args = new UpdateSessionRequestEventArgs();
				args.UpdateType = UpdateSessionRequestType.QueryString;
				args.QueryString = formConverter.ConvertQueryHashtable(this.queryDataValues, this.separator, this.pairSeparator);
				this.txtQueryString.Text = args.QueryString;
				this.UpdateSessionRequestEvent(this, args);		
			}
		}

		private void btnReset_Click(object sender, System.EventArgs e)
		{
			LoadQueryString();
		}
	}
}
