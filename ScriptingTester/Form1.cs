using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.HtmlDom;
using Ecyware.GreenBlue.Protocols.Http;
using Ecyware.GreenBlue.Protocols.Http.Scripting;

namespace ScriptingTester
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TreeView treeView1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.treeView1 = new System.Windows.Forms.TreeView();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(24, 42);
			this.button1.Name = "button1";
			this.button1.TabIndex = 0;
			this.button1.Text = "button1";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(24, 84);
			this.button2.Name = "button2";
			this.button2.TabIndex = 1;
			this.button2.Text = "Start Wizard";
			// 
			// treeView1
			// 
			this.treeView1.ImageIndex = -1;
			this.treeView1.Location = new System.Drawing.Point(168, 12);
			this.treeView1.Name = "treeView1";
			this.treeView1.SelectedImageIndex = -1;
			this.treeView1.Size = new System.Drawing.Size(522, 480);
			this.treeView1.TabIndex = 2;
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(694, 500);
			this.Controls.Add(this.treeView1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
		
			string section = "Ecyware.GreenBlue.ScriptingData";
			Hashtable handler = new Hashtable();
			handler.Add(section, typeof(Ecyware.GreenBlue.Protocols.Http.Scripting.SessionRequestSerializer));

			ConfigManager.SetSectionHandlersOverrides(handler);
			ConfigManager.SetConfigurationFilePathOverrides("C:\\Tester.xml");			

			HttpProperties client = new HttpProperties();
			client.UserAgent = "Mozilla";
			client.Pipeline = true;
			client.Referer = "";

			try
			{
				ScriptingData sd = new ScriptingData();
				GetWebRequest sr01 = new GetWebRequest();
				sr01.Url = "http://www.hotmail.com";
				sr01.RequestHttpSettings = client;
				//WebHeader.FillWebHeaderCollection(coll, sr01.RequestHeaders);				
				sd.AddWebRequest(sr01);

				//System.Security.Permissions.SecurityPermission perm = new System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityPermissionFlag.SerializationFormatter);
				
				XmlNode node = ConfigManager.WriteXmlNode(section, sd);
				MessageBox.Show(node.OuterXml);
			}
			catch ( Exception ex )
			{
				MessageBox.Show(ex.ToString());
			}

		}
	}
}
