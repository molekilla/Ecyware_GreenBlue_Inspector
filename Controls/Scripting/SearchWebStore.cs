using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Ecyware.GreenBlue.Protocols.Http.Transforms;

namespace Ecyware.GreenBlue.Controls.Scripting
{
	/// <summary>
	/// Summary description for SearchWebStore.
	/// </summary>
	public class SearchWebStore : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btnGo;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.TextBox txtSearch;
		private System.Windows.Forms.Button btnClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SearchWebStore()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			ArrayList items = new ArrayList();
			
			// Load combo box.		
			items.Add(new NameValueObject("Keyword",Ecyware.GreenBlue.LicenseServices.Client.WebStoreViewMessage.SearchType.ByKeyword.ToString()));
			items.Add(new NameValueObject("Publisher",Ecyware.GreenBlue.LicenseServices.Client.WebStoreViewMessage.SearchType.ByPublisher.ToString()));

			this.comboBox1.DataSource = items;
			this.comboBox1.DisplayMember = "Name";
			this.comboBox1.ValueMember = "Value";
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SearchWebStore));
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnGo = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.txtSearch = new System.Windows.Forms.TextBox();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btnClose);
			this.groupBox2.Controls.Add(this.btnGo);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.comboBox1);
			this.groupBox2.Controls.Add(this.txtSearch);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(2, 2);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(480, 88);
			this.groupBox2.TabIndex = 10;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Search";
			// 
			// btnClose
			// 
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.Location = new System.Drawing.Point(396, 54);
			this.btnClose.Name = "btnClose";
			this.btnClose.TabIndex = 5;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnGo
			// 
			this.btnGo.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnGo.Location = new System.Drawing.Point(396, 24);
			this.btnGo.Name = "btnGo";
			this.btnGo.TabIndex = 4;
			this.btnGo.Text = "&Go";
			this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(12, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 23);
			this.label2.TabIndex = 3;
			this.label2.Text = "Search";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 23);
			this.label1.TabIndex = 2;
			this.label1.Text = "Search By";
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.Location = new System.Drawing.Point(90, 25);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(288, 21);
			this.comboBox1.TabIndex = 1;
			// 
			// txtSearch
			// 
			this.txtSearch.Location = new System.Drawing.Point(90, 49);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.Size = new System.Drawing.Size(288, 20);
			this.txtSearch.TabIndex = 0;
			this.txtSearch.Text = "";
			// 
			// SearchWebStore
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(484, 92);
			this.Controls.Add(this.groupBox2);
			this.DockPadding.All = 2;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SearchWebStore";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Search";
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnGo_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			this.Close();		
		}

		/// <summary>
		/// Gets the search value.
		/// </summary>
		public string SearchValue
		{
			get
			{
				return this.txtSearch.Text;
			}
		}

		/// <summary>
		/// Gets the search type.
		/// </summary>
		public Ecyware.GreenBlue.LicenseServices.Client.WebStoreViewMessage.SearchType SearchType
		{
			get
			{
				return (Ecyware.GreenBlue.LicenseServices.Client.WebStoreViewMessage.SearchType)Enum.Parse(typeof(Ecyware.GreenBlue.LicenseServices.Client.WebStoreViewMessage.SearchType),(string)this.comboBox1.SelectedValue);
			}
		}

	}
}
