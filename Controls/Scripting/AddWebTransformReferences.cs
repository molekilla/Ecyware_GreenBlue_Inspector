using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.Transforms;
using Ecyware.GreenBlue.Engine.Transforms.Designers;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Engine;

namespace Ecyware.GreenBlue.Controls.Scripting
{
	/// <summary>
	/// Summary description for AddWebTransformReferences.
	/// </summary>
	public class AddWebTransformReferences : System.Windows.Forms.Form
	{
		Cursor tempCursor;
		static string configurationSection = "WebTransforms";
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListView lvWebTransforms;
		private System.Windows.Forms.ColumnHeader colAssemblyName;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem mnuRemove;
		private System.Windows.Forms.OpenFileDialog dlgOpenFile;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AddWebTransformReferences()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			LoadWebTransforms();
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(AddWebTransformReferences));
			this.lvWebTransforms = new System.Windows.Forms.ListView();
			this.colAssemblyName = new System.Windows.Forms.ColumnHeader();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.mnuRemove = new System.Windows.Forms.MenuItem();
			this.button1 = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnSave = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.dlgOpenFile = new System.Windows.Forms.OpenFileDialog();
			this.SuspendLayout();
			// 
			// lvWebTransforms
			// 
			this.lvWebTransforms.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							  this.colAssemblyName,
																							  this.columnHeader1,
																							  this.columnHeader3,
																							  this.columnHeader2});
			this.lvWebTransforms.ContextMenu = this.contextMenu1;
			this.lvWebTransforms.FullRowSelect = true;
			this.lvWebTransforms.Location = new System.Drawing.Point(12, 24);
			this.lvWebTransforms.Name = "lvWebTransforms";
			this.lvWebTransforms.Size = new System.Drawing.Size(420, 186);
			this.lvWebTransforms.TabIndex = 0;
			this.lvWebTransforms.View = System.Windows.Forms.View.Details;
			// 
			// colAssemblyName
			// 
			this.colAssemblyName.Text = "Type";
			this.colAssemblyName.Width = 150;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Version";
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Web Transform Type";
			this.columnHeader3.Width = 150;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Path";
			this.columnHeader2.Width = 150;
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.mnuRemove});
			// 
			// mnuRemove
			// 
			this.mnuRemove.Index = 0;
			this.mnuRemove.Text = "&Remove";
			this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Location = new System.Drawing.Point(438, 24);
			this.button1.Name = "button1";
			this.button1.TabIndex = 1;
			this.button1.Text = "Add...";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnCancel.Location = new System.Drawing.Point(354, 216);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.button2_Click);
			// 
			// btnSave
			// 
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.Location = new System.Drawing.Point(270, 216);
			this.btnSave.Name = "btnSave";
			this.btnSave.TabIndex = 3;
			this.btnSave.Text = "OK";
			this.btnSave.Click += new System.EventHandler(this.button3_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(282, 18);
			this.label1.TabIndex = 4;
			this.label1.Text = "Selected web transforms";
			// 
			// AddWebTransformReferences
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(520, 250);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.lvWebTransforms);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AddWebTransformReferences";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Add Web Transform References";
			this.ResumeLayout(false);

		}
		#endregion

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			AddWebTransformReference();
		}

		#region Methods
		/// <summary>
		/// Load the current web transforms.
		/// </summary>
		public void LoadWebTransforms()
		{
			lvWebTransforms.Items.Clear();

			// Get Configuration
			WebTransformConfiguration config = (WebTransformConfiguration)ConfigManager.Read(configurationSection, false);
			
			foreach ( TransformProvider provider in config.Transforms )
			{
				WebTransformPageUIHelper.LoadWebTransformAttribute(provider);

				ListViewItem item = new ListViewItem();				
				Type type = Type.GetType(provider.Type);
				
				// Name
				item.Text = provider.Name;
				Assembly assm = type.Assembly;

				// Version
				item.SubItems.Add(assm.GetName().Version.ToString());				

				// Transform type
				item.SubItems.Add(provider.TransformType);

				// Location
				item.SubItems.Add(assm.Location);

				// Type
				item.Tag = type;
				lvWebTransforms.Items.Add(item);
			}
		}


		/// <summary>
		/// Adds a web transform reference.
		/// </summary>
		public void AddWebTransformReference()
		{
			dlgOpenFile.CheckFileExists = true;
			dlgOpenFile.InitialDirectory = Application.StartupPath;
			dlgOpenFile.RestoreDirectory = true;
			dlgOpenFile.Filter = "Component files (*.dll)|*.dll";
			dlgOpenFile.Title = "Add Web Transform Reference";

			if ( dlgOpenFile.ShowDialog() == DialogResult.OK )
			{
				Application.DoEvents();
				tempCursor = Cursor.Current;
				Cursor.Current = Cursors.WaitCursor;

				try
				{
					AddAssembly(dlgOpenFile.FileName);
				}
				catch ( Exception ex )
				{
					MessageBox.Show(ex.Message, AppLocation.ApplicationName, MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
			}

			Cursor.Current = tempCursor;
		}


		/// <summary>
		/// Adds the assembly to start up path of the executable.
		/// </summary>
		/// <param name="fileName"> The component file name.</param>
		private void AddAssembly(string fileName)
		{
			Assembly assm = Assembly.LoadFile(fileName);
			Type[] transforms = WebTransform.LoadWebTransformsFromAssembly(assm);

			if ( transforms.Length > 0 )
			{
				// Load the types from the assembly.
				foreach ( Type type in transforms )
				{
					// Get WebTransformAttribute
					WebTransformAttribute attribute = (WebTransformAttribute)Attribute.GetCustomAttribute(type, typeof (WebTransformAttribute));

					ListViewItem item = new ListViewItem();
					// Name
					item.Text = attribute.Name;

					// Version
					item.SubItems.Add(assm.GetName().Version.ToString());

					// Transform type
					item.SubItems.Add(attribute.TransformProviderType);

					// Copy to startup path
					FileInfo fileInfo = new FileInfo(dlgOpenFile.FileName);
					string location = Application.StartupPath + Path.DirectorySeparatorChar + fileInfo.Name;					
					fileInfo.CopyTo(location, true);

					// Location
					item.SubItems.Add(location);

					// Type
					item.Tag = type;

					lvWebTransforms.Items.Add(item);					
				}
			} 
			else 
			{
				MessageBox.Show("Component does not have any type that implements the WebTransform type.", AppLocation.ApplicationName, MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
		}


		/// <summary>
		/// Updates the web transform references.
		/// </summary>
		private void UpdateWebTransformsReferences()
		{
			WebTransformConfiguration transforms = new WebTransformConfiguration();
			ArrayList list = new ArrayList();

			foreach ( ListViewItem item in lvWebTransforms.Items )
			{
				// Type name
				Type type = (Type)item.Tag;
				TransformProvider provider = new TransformProvider();
				provider.Type = type.AssemblyQualifiedName;
				list.Add(provider);
			}

			transforms.Transforms = (TransformProvider[])list.ToArray(typeof(TransformProvider));

			// Save Configuration
			ConfigManager.Write(configurationSection, transforms);
		}
		#endregion
		private void mnuRemove_Click(object sender, System.EventArgs e)
		{
			if ( this.lvWebTransforms.SelectedIndices.Count > 0 )
			{
				ListViewItem item = lvWebTransforms.SelectedItems[0];

				// Location
				string location = item.SubItems[3].Text;
				FileInfo fileInfo = new FileInfo(location);

				if ( fileInfo.Name.IndexOf("Ecyware.GreenBlue.Engine.dll") > -1 )
				{
					MessageBox.Show("Cannot remove system components", AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
				} 
				else 
				{
					lvWebTransforms.Items.Remove(item);

					// Remove from file.
					try
					{
						File.Delete(location);
					}
					catch ( Exception ex )
					{
						MessageBox.Show(ex.Message, AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
			}
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			UpdateWebTransformsReferences();
			this.Close();
		}
	}
}
