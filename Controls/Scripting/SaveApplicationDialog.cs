using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using Ecyware.GreenBlue.Engine;

namespace Ecyware.GreenBlue.Controls.Scripting
{
	/// <summary>
	/// Summary description for SaveApplicationDialog.
	/// </summary>
	public class SaveApplicationDialog : System.Windows.Forms.Form
	{
		bool _doEncrypt = false;
		private bool _isNew = false;
		private string _currentFileName;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtFileName;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ErrorProvider errorProvider1;
		private System.Windows.Forms.CheckBox chkEncrypt;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new SaveApplicationDialog.
		/// </summary>
		public SaveApplicationDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}
		/// <summary>
		/// Creates a new SaveApplicationDialog.
		/// </summary>
		/// <param name="currentFileName"> The current file name.</param>
		public SaveApplicationDialog(string currentFileName) : this()
		{

			if ( currentFileName.Length > 0 )
			{
				FileInfo fileInfo = new FileInfo(currentFileName);
				_currentFileName = fileInfo.Name.Replace(fileInfo.Extension,"");

				this.txtFileName.Text = _currentFileName;
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SaveApplicationDialog));
			this.label1 = new System.Windows.Forms.Label();
			this.txtFileName = new System.Windows.Forms.TextBox();
			this.btnSave = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider();
			this.chkEncrypt = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(12, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(144, 18);
			this.label1.TabIndex = 11;
			this.label1.Text = "Scripting Application Name";
			// 
			// txtFileName
			// 
			this.txtFileName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtFileName.Location = new System.Drawing.Point(162, 29);
			this.txtFileName.Name = "txtFileName";
			this.txtFileName.Size = new System.Drawing.Size(246, 20);
			this.txtFileName.TabIndex = 10;
			this.txtFileName.Text = "";
			// 
			// btnSave
			// 
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.Location = new System.Drawing.Point(246, 84);
			this.btnSave.Name = "btnSave";
			this.btnSave.TabIndex = 9;
			this.btnSave.Text = "&Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// button1
			// 
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.button1.Location = new System.Drawing.Point(330, 84);
			this.button1.Name = "button1";
			this.button1.TabIndex = 12;
			this.button1.Text = "&Cancel";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// chkEncrypt
			// 
			this.chkEncrypt.Location = new System.Drawing.Point(162, 54);
			this.chkEncrypt.Name = "chkEncrypt";
			this.chkEncrypt.Size = new System.Drawing.Size(246, 24);
			this.chkEncrypt.TabIndex = 13;
			this.chkEncrypt.Text = "Encrypt Scripting Application";
			// 
			// SaveApplicationDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(480, 118);
			this.Controls.Add(this.chkEncrypt);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtFileName);
			this.Controls.Add(this.btnSave);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SaveApplicationDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Scripting Application Name";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			if ( this.txtFileName.Text.Length == 0 )
			{
				this.errorProvider1.SetError(txtFileName, "A file name is required.");
			} 
			else 
			{
				this.errorProvider1.SetError(txtFileName,"");
				this.DoEncrypt = this.chkEncrypt.Checked;

				if ( File.Exists(this.ScriptingApplicationFilePath) )
				{
					if ( MessageBox.Show("There is already a file with that name. Do you want to overwrite the existing file?",AppLocation.ApplicationName, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes )
					{
						this.DialogResult = DialogResult.OK;
						this.Close();
					}					
					this.IsNew = false;
				} 
				else 
				{
					this.IsNew = true;
					this.DialogResult = DialogResult.OK;
					this.Close();
				}
			}
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		/// <summary>
		/// Gets the scripting application file path.
		/// </summary>
		public string ScriptingApplicationFilePath
		{
			get
			{
				return AppLocation.DocumentFolder + "\\" + this.txtFileName.Text + ".gbscr";
			}
		}

		/// <summary>
		/// Gets or sets the encryption for a scripting application.
		/// </summary>
		public bool DoEncrypt
		{
			get
			{
				return _doEncrypt;
			}
			set
			{
				_doEncrypt = value;
			}

		}

		/// <summary>
		/// Gets or sets if the scripting application is new.
		/// </summary>
		public bool IsNew
		{
			get
			{
				return _isNew;
			}
			set
			{
				_isNew = value;
			}
		}
	}
}
