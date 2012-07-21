using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms;
using Ecyware.GreenBlue.Engine.HtmlDom;


namespace Ecyware.GreenBlue.Protocols.Http.Scripting
{

	/// <summary>
	/// Summary description for ScriptingApplicationArgumentDesignerForm.
	/// </summary>
	public class ScriptingApplicationArgumentDesignerForm : System.Windows.Forms.Form
	{
		ScriptingApplicationArgs _applicationArgs;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.PropertyGrid pgArgumentProps;
		private System.Windows.Forms.TreeView tvArguments;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new ScriptingApplicationArgumentDesignerForm.
		/// </summary>
		public ScriptingApplicationArgumentDesignerForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Creates a new ScriptingApplicationArgumentDesignerForm.
		/// </summary>
		/// <param name="args"> The arguments to load.</param>
		public ScriptingApplicationArgumentDesignerForm(ScriptingApplicationArgs args) : this()
		{
			_applicationArgs = args;
			LoadArguments();
		}

		#region Methods
		/// <summary>
		/// Load the arguments.
		/// </summary>
		private void LoadArguments()
		{
			TreeNode parent = new TreeNode("Scripting Arguments");
			tvArguments.Nodes.Add(parent);

			int i = 0;
			foreach ( WebRequestArgs webRequestArg in _applicationArgs.WebRequestArguments )
			{
				int j = 0;
				foreach ( Argument argument in webRequestArg.Arguments )
				{
					TreeNode argumentNode = new TreeNode(argument.Name);
					CurrentArgumentState selectedArgument = new CurrentArgumentState();
					selectedArgument.SelectedArgument = argument;
					selectedArgument.WebRequestIndex = i;
					selectedArgument.ArgumentIndex = j;
					argumentNode.Tag = selectedArgument;
					parent.Nodes.Add(argumentNode);

					j++;
				}

				i++;
			}

			tvArguments.ExpandAll();
		}
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the application arguments.
		/// </summary>
		public ScriptingApplicationArgs ScriptingApplicationArgs
		{
			get
			{
				return _applicationArgs;
			}
			set
			{
				_applicationArgs= value;
			}
		}
		#endregion
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ScriptingApplicationArgumentDesignerForm));
			this.pgArgumentProps = new System.Windows.Forms.PropertyGrid();
			this.tvArguments = new System.Windows.Forms.TreeView();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pgArgumentProps
			// 
			this.pgArgumentProps.CommandsVisibleIfAvailable = true;
			this.pgArgumentProps.LargeButtons = false;
			this.pgArgumentProps.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.pgArgumentProps.Location = new System.Drawing.Point(234, 36);
			this.pgArgumentProps.Name = "pgArgumentProps";
			this.pgArgumentProps.Size = new System.Drawing.Size(282, 372);
			this.pgArgumentProps.TabIndex = 0;
			this.pgArgumentProps.Text = "propertyGrid1";
			this.pgArgumentProps.ViewBackColor = System.Drawing.SystemColors.Window;
			this.pgArgumentProps.ViewForeColor = System.Drawing.SystemColors.WindowText;
			// 
			// tvArguments
			// 
			this.tvArguments.HideSelection = false;
			this.tvArguments.ImageIndex = -1;
			this.tvArguments.Location = new System.Drawing.Point(6, 36);
			this.tvArguments.Name = "tvArguments";
			this.tvArguments.SelectedImageIndex = -1;
			this.tvArguments.Size = new System.Drawing.Size(222, 372);
			this.tvArguments.TabIndex = 1;
			this.tvArguments.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvArguments_AfterSelect);
			this.tvArguments.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvArguments_BeforeSelect);
			// 
			// btnSave
			// 
			this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSave.Location = new System.Drawing.Point(348, 420);
			this.btnSave.Name = "btnSave";
			this.btnSave.TabIndex = 2;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnClose
			// 
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.Location = new System.Drawing.Point(438, 420);
			this.btnClose.Name = "btnClose";
			this.btnClose.TabIndex = 3;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(216, 23);
			this.label1.TabIndex = 4;
			this.label1.Text = "Current arguments";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(234, 12);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(216, 23);
			this.label2.TabIndex = 5;
			this.label2.Text = "Argument Properties";
			// 
			// ScriptingApplicationArgumentDesignerForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(520, 452);
			this.ControlBox = false;
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(this.tvArguments);
			this.Controls.Add(this.pgArgumentProps);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ScriptingApplicationArgumentDesignerForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Scripting Application Arguments Designer";
			this.TopMost = true;
			this.ResumeLayout(false);

		}
		#endregion

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		#region Tree Selected Events
		private void tvArguments_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			// Load
			if ( e.Node.Parent != null )
			{
				if ( e.Node.Tag != null )
				{
					CurrentArgumentState argument = (CurrentArgumentState)e.Node.Tag;
					this.pgArgumentProps.SelectedObject =  argument.SelectedArgument;
					this.pgArgumentProps.ExpandAllGridItems();				
				}
			}
		}

		private void tvArguments_BeforeSelect(object sender, System.Windows.Forms.TreeViewCancelEventArgs e)
		{
			if ( tvArguments.SelectedNode != null )
			{
				// Save
				if ( tvArguments.SelectedNode.Tag != null )
				{
					CurrentArgumentState argument = (CurrentArgumentState)tvArguments.SelectedNode.Tag;
					_applicationArgs.WebRequestArguments[argument.WebRequestIndex].Arguments[argument.ArgumentIndex] = argument.SelectedArgument;
				}
			}
		}

		#endregion
	}
}
