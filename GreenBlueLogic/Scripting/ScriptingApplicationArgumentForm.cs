using System;
using System.IO;
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
	/// Summary description for ScriptingApplicationArgumentForm.
	/// </summary>
	public class ScriptingApplicationArgumentForm : System.Windows.Forms.Form
	{
		int startX = 12;
		int startY = 18;
		int index = 0;
		int _requestResponseIndex = -1;
		ScriptingApplication _scrapp;
		ScriptingApplicationArgs _args;
		string _packageFile = string.Empty;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnNextRequest;
		private System.Windows.Forms.Button btnRun;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Panel panel;
		private ScriptingCommand scriptingCommand = new ScriptingCommand();
		private System.Windows.Forms.StatusBar status;
		private System.Windows.Forms.StatusBarPanel panel1;
		private System.Windows.Forms.ContextMenu mnuRunOptions;
		private System.Windows.Forms.MenuItem mnuRunWithDefaultDialog;
		private System.Windows.Forms.MenuItem mnuRunWithTestDialog;
		private System.ComponentModel.IContainer components;

		#region Constructors
		/// <summary>
		/// Creates a new ScriptingApplicationArgumentForm.
		/// </summary>
		public ScriptingApplicationArgumentForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			scriptingCommand.SessionAbortedEvent += new SessionAbortEventHandler(scriptingCommand_SessionAbortedEvent);
			scriptingCommand.OnRequestEnd += new OnRequestEndEventHandler(scriptingCommand_OnRequestEnd);
		}

		/// <summary>
		/// Creates a new ScriptingApplicationArgumentForm.
		/// </summary>
		/// <param name="file"> The scripting application package to run.</param>
//		public ScriptingApplicationArgumentForm(string file) : this()
//		{
//			FileInfo fileInfo = new FileInfo(file);
//			this.Text = "Scripting Application Form - " + fileInfo.Name.Replace(fileInfo.Extension, "");
//			_packageFile = file;
//			RunScriptingApplication(file);
//		}

		/// <summary>
		/// Creates a new ScriptingApplicationArgumentForm.
		/// </summary>
		/// <param name="application"> The ScriptingApplication type.</param>
		/// <param name="arguments"> The ScriptingApplicationArgs type.</param>
		public ScriptingApplicationArgumentForm(ScriptingApplication application, ScriptingApplicationArgs arguments) : this()
		{
			if ( arguments == null )
			{
				_args = new ScriptingApplicationArgs();
			} 
			else 
			{
				_args = arguments;
			}
			_scrapp = application;

		}
		#endregion
		#region Properties
		/// <summary>
		/// Gets or sets the ScriptingApplicationArgs.
		/// </summary>
		public ScriptingApplicationArgs ScriptingApplicationArgs
		{
			get
			{
				return _args;
			}
		}

		/// <summary>
		/// Gets the scripting application.
		/// </summary>
		public ScriptingApplication ScriptingApplication
		{
			get
			{
				return _scrapp;
			}
		}

		#endregion
		/// <summary>
		/// Runs the scripting application as a Windows Form application.
		/// </summary>
		/// <param name="file"> The ScriptingApplication or ScriptingApplicationPackage to run.</param>
		public void RunScriptingApplication(string file)
		{
			FileInfo fileInfo = new FileInfo(file);			
			_packageFile = file;
			this.Text = "Scripting Application Form - " + fileInfo.Name.Replace(fileInfo.Extension, "");
			string contentType = AppLocation.GetMIMEType(file);

			if ( contentType.IndexOf("xml") == -1 )
			{
				ScriptingApplicationPackage package = new ScriptingApplicationPackage(file);
				_args = package.ScriptingApplicationArguments;
				_scrapp = package.ScriptingApplication;
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ScriptingApplicationArgumentForm));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.panel = new System.Windows.Forms.Panel();
			this.btnNextRequest = new System.Windows.Forms.Button();
			this.btnRun = new System.Windows.Forms.Button();
			this.mnuRunOptions = new System.Windows.Forms.ContextMenu();
			this.mnuRunWithDefaultDialog = new System.Windows.Forms.MenuItem();
			this.mnuRunWithTestDialog = new System.Windows.Forms.MenuItem();
			this.btnClose = new System.Windows.Forms.Button();
			this.status = new System.Windows.Forms.StatusBar();
			this.panel1 = new System.Windows.Forms.StatusBarPanel();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.panel1)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.panel);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.groupBox1.Location = new System.Drawing.Point(6, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(622, 408);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Form";
			// 
			// panel
			// 
			this.panel.AutoScroll = true;
			this.panel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel.Location = new System.Drawing.Point(3, 16);
			this.panel.Name = "panel";
			this.panel.Size = new System.Drawing.Size(616, 389);
			this.panel.TabIndex = 0;
			// 
			// btnNextRequest
			// 
			this.btnNextRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNextRequest.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnNextRequest.Location = new System.Drawing.Point(447, 420);
			this.btnNextRequest.Name = "btnNextRequest";
			this.btnNextRequest.Size = new System.Drawing.Size(84, 23);
			this.btnNextRequest.TabIndex = 2;
			this.btnNextRequest.Text = "&Continue";
			this.btnNextRequest.Click += new System.EventHandler(this.btnNextRequest_Click);
			// 
			// btnRun
			// 
			this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRun.ContextMenu = this.mnuRunOptions;
			this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnRun.Location = new System.Drawing.Point(546, 420);
			this.btnRun.Name = "btnRun";
			this.btnRun.TabIndex = 3;
			this.btnRun.Text = "&Run";
			this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
			// 
			// mnuRunOptions
			// 
			this.mnuRunOptions.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						  this.mnuRunWithDefaultDialog,
																						  this.mnuRunWithTestDialog});
			// 
			// mnuRunWithDefaultDialog
			// 
			this.mnuRunWithDefaultDialog.Index = 0;
			this.mnuRunWithDefaultDialog.Text = "Run with &default response dialog";
			this.mnuRunWithDefaultDialog.Click += new System.EventHandler(this.mnuRunWithDefaultDialog_Click);
			// 
			// mnuRunWithTestDialog
			// 
			this.mnuRunWithTestDialog.Index = 1;
			this.mnuRunWithTestDialog.Text = "Run with &test dialog";
			this.mnuRunWithTestDialog.Click += new System.EventHandler(this.mnuRunWithTestDialog_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnClose.Location = new System.Drawing.Point(4, 420);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(84, 23);
			this.btnClose.TabIndex = 4;
			this.btnClose.Text = "&Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// status
			// 
			this.status.Location = new System.Drawing.Point(0, 454);
			this.status.Name = "status";
			this.status.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
																					  this.panel1});
			this.status.ShowPanels = true;
			this.status.Size = new System.Drawing.Size(632, 22);
			this.status.TabIndex = 7;
			// 
			// panel1
			// 
			this.panel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
			this.panel1.Width = 616;
			// 
			// ScriptingApplicationArgumentForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(632, 476);
			this.Controls.Add(this.status);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnRun);
			this.Controls.Add(this.btnNextRequest);
			this.Controls.Add(this.groupBox1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ScriptingApplicationArgumentForm";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Scripting Application Form";
			this.Load += new System.EventHandler(this.ScriptingApplicationArgumentForm_Load);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.panel1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void ScriptingApplicationArgumentForm_Load(object sender, System.EventArgs e)
		{
			LoadArguments();
		}

		#region Designer Methods
		/// <summary>
		/// Saves the arguments.
		/// </summary>
		private void SaveArguments()
		{
			WebRequestArgs currentRequest = this.ScriptingApplicationArgs.WebRequestArguments[index];
			if ( currentRequest.Arguments.Length > 0 )
			{
				int j = 1;
				for ( int i=1;i<panel.Controls.Count;i=(i+2))
				{
					currentRequest.Arguments[i-j].Value = LoadDesignerControlValue(currentRequest.Arguments[i-j].DesignProperty.DesignerControlType, panel.Controls[i]);
					j++;
				}
			}
		}

		/// <summary>
		/// Load the arguments.
		/// </summary>
		private void LoadArguments()
		{
			startY = 12;

			if ( this.ScriptingApplicationArgs != null )
			{
				WebRequestArgs currentRequest = this.ScriptingApplicationArgs.WebRequestArguments[index];
				if ( currentRequest.Arguments.Length > 0 )
				{
					foreach ( Argument argument in currentRequest.Arguments )
					{		
						// Label
						Label label = new Label();
						label.Location = new System.Drawing.Point(startX, startY);
						label.Name = "lbl" + argument.Name;					
					
						if ( argument.DesignProperty.Label.Length > 0 )
						{
							label.Text = argument.DesignProperty.Label;
						} 
						else 
						{
							label.Text = argument.Name;
						}

						Control control = CreateDesignerControl(currentRequest.WebRequestIndex, argument);

						if ( control != null )
						{
							startY += control.Size.Height + 10;

							panel.Controls.Add(label);
							panel.Controls.Add(control);
						}
					}
				} 
				else 
				{
					panel.Controls.Clear();
				}
			}
		}


		/// <summary>
		/// Loads the combo values.
		/// </summary>
		/// <param name="webRequestIndex"> The web request index.</param>
		/// <param name="combo"> The combo box to load.</param>
		/// <param name="htmlElementName"> The HTML Element name.</param>
		private void LoadComboValues(int webRequestIndex, ComboBox combo, string htmlElementName)
		{
			HtmlFormTagXml formTagXml = this.ScriptingApplication.WebRequests[webRequestIndex].Form;

			HtmlSelectTag select = null;
			if ( formTagXml.Elements.Length > 0 )
			{
				HtmlFormTag formTag = formTagXml.WriteHtmlFormTag();
				HtmlTagBaseList list = formTag[htmlElementName];

				if ( list != null )
				{
					if ( list.Count > 0 )
					{
						select = (HtmlSelectTag)list[0];
					}
				}
			}

			ArrayList items = new ArrayList();
			
			// Load combo box.
			foreach ( HtmlOptionTag option in select.Options )
			{
				NameValueObject nameValuePair = new NameValueObject(option.Text, option.Value);
				items.Add(nameValuePair);
			}
			combo.DataSource = items;
			combo.DisplayMember = "Name";
			combo.ValueMember = "Value";
		}
		/// <summary>
		/// Loads the designer control value.
		/// </summary>
		/// <param name="designerControlType"> The designer control type.</param>
		/// <param name="control"> The control.</param>
		/// <returns> A string representing the value.</returns>
		private string LoadDesignerControlValue(ArgumentProperty.DesignerControl designerControlType, Control control)
		{
			string result = string.Empty;

			switch ( designerControlType )
			{
				case ArgumentProperty.DesignerControl.TextBox:
					// Text				
					result = ((TextBox)control).Text;
					break;
				case ArgumentProperty.DesignerControl.RichTextBox:
					// Text
					result = ((RichTextBox)control).Text;
					break;
				case ArgumentProperty.DesignerControl.DropDownList:
					result = Convert.ToString(((ComboBox)control).SelectedValue);
					break;
				case ArgumentProperty.DesignerControl.DropDown:
					result = Convert.ToString(((ComboBox)control).Text);
					break;
				case ArgumentProperty.DesignerControl.HtmlEditor:
					result = ((BasicHtmlEditorControl)control).BodyInnerHtml;
					break;
				case ArgumentProperty.DesignerControl.CheckBox:
					CheckBox checkbox = (CheckBox)control;
					if ( checkbox.Checked )
					{
						result = "on";
					} 
					else 
					{
						result = "off";
					}
					break;
			}

			return result;
		}

		/// <summary>
		/// Creates a designer control.
		/// </summary>
		/// <param name="webRequestIndex"> The web request index.</param>
		/// <param name="argument"> An argument.</param>
		/// <returns>A control.</returns>
		private Control CreateDesignerControl(int webRequestIndex, Argument argument)
		{
			Control control = null;

			switch ( argument.DesignProperty.DesignerControlType )
			{
				case ArgumentProperty.DesignerControl.TextBox:
					// Text
					TextBox text = new TextBox();
					text.Location = new System.Drawing.Point(startX + 110, startY);
					text.Name = "txt" + argument.Name;
					text.Size = argument.DesignProperty.Size;
					text.PasswordChar = argument.DesignProperty.PasswordChar;
					text.Multiline = argument.DesignProperty.Multiline;
					text.ReadOnly = argument.DesignProperty.ReadOnly;
					text.WordWrap = argument.DesignProperty.WordWrap;
					text.ForeColor = argument.DesignProperty.ForeColor;
					text.Font = argument.DesignProperty.Font;
					control = text;
					break;
				case ArgumentProperty.DesignerControl.RichTextBox:
					// Text
					RichTextBox rtext = new RichTextBox();
					rtext.Location = new System.Drawing.Point(startX + 110, startY);
					rtext.Name = "txt" + argument.Name;
					rtext.Size = argument.DesignProperty.Size;
					rtext.Multiline = argument.DesignProperty.Multiline;
					rtext.ReadOnly = argument.DesignProperty.ReadOnly;
					rtext.WordWrap = argument.DesignProperty.WordWrap;
					rtext.ForeColor = argument.DesignProperty.ForeColor;
					rtext.Font = argument.DesignProperty.Font;
					control = rtext;
					break;
				case ArgumentProperty.DesignerControl.DropDownList:
					ComboBox comboList = new ComboBox();
					comboList.Location = new System.Drawing.Point(startX + 110, startY);
					comboList.Name = "txt" + argument.Name;
					comboList.Size = argument.DesignProperty.Size;
					comboList.ForeColor = argument.DesignProperty.ForeColor;
					comboList.Font = argument.DesignProperty.Font;
					comboList.DropDownStyle = ComboBoxStyle.DropDownList;
					LoadComboValues(webRequestIndex, comboList, argument.Name);
					control = comboList;
					break;
				case ArgumentProperty.DesignerControl.DropDown:
					ComboBox combo = new ComboBox();
					combo.Location = new System.Drawing.Point(startX + 110, startY);
					combo.Name = "txt" + argument.Name;
					combo.Size = argument.DesignProperty.Size;
					combo.ForeColor = argument.DesignProperty.ForeColor;
					combo.Font = argument.DesignProperty.Font;
					combo.DropDownStyle = ComboBoxStyle.DropDown;
					LoadComboValues(webRequestIndex, combo, argument.Name);
					control = combo;
					break;
				case ArgumentProperty.DesignerControl.HtmlEditor:
					BasicHtmlEditorControl htmlEditor = new BasicHtmlEditorControl();
					htmlEditor.Location = new System.Drawing.Point(startX + 110, startY);
					htmlEditor.Name = "txt" + argument.Name;
					htmlEditor.Size = argument.DesignProperty.Size;
					htmlEditor.ForeColor = argument.DesignProperty.ForeColor;
					htmlEditor.Font = argument.DesignProperty.Font;
					control = htmlEditor;
					break;
				case ArgumentProperty.DesignerControl.CheckBox:
					CheckBox checkBox = new CheckBox();
					checkBox.Location = new System.Drawing.Point(startX + 110, startY);
					checkBox.Name = "txt" + argument.Name;
					checkBox.Size = argument.DesignProperty.Size;
					checkBox.ForeColor = argument.DesignProperty.ForeColor;
					checkBox.Font = argument.DesignProperty.Font;
					control = checkBox;
					break;
			}

			return control;
		}

		#endregion
		/// <summary>
		/// Saves the values and displays the next window.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnNextRequest_Click(object sender, System.EventArgs e)
		{
			LoadWebRequestInputForm();
		}

		/// <summary>
		/// Sends a request response instead of batch mode.
		/// </summary>
		private void SendRequestResponse()
		{
			index = ScriptingApplicationArgs.WebRequestArguments.Length -1;
			_requestResponseIndex = ScriptingApplicationArgs.WebRequestArguments[index].WebRequestIndex;
			SaveArguments();
			_scrapp.LoadArgumentDefinition(_args);

			if ( _requestResponseIndex > -1 )
			{
				scriptingCommand.ExecuteSessionUntilEnd(_scrapp, _requestResponseIndex);
			}
		}

		/// <summary>
		/// Loads the web request input form.
		/// </summary>
		private void LoadWebRequestInputForm()
		{
			SaveArguments();
			index++;
			if (  index < ScriptingApplicationArgs.WebRequestArguments.Length )
			{			
				if ( ScriptingApplicationArgs.WebRequestArguments[index].Arguments.Length > 0 )
				{
					panel.Controls.Clear();
					LoadArguments();
				} 
				else 
				{
					LoadWebRequestInputForm();
				}
			} 
			else 
			{
				this.btnNextRequest.Enabled = false;
			}
		}

		private void btnRun_Click(object sender, System.EventArgs e)
		{
			Point ptClick = this.btnRun.PointToClient(Control.MousePosition); 
			this.mnuRunOptions.Show(btnRun, ptClick);
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		#region Request Response Methods
		private void button1_Click(object sender, System.EventArgs e)
		{
			SendRequestResponse();
			this.panel1.Text = "Sending request... Please wait";
			this.panel.Enabled = false;
			this.btnRun.Enabled = false;
			this.btnClose.Enabled = false;
			this.btnNextRequest.Enabled = false;
		}

		private void scriptingCommand_OnRequestEnd(object sender, RequestStartEndEventArgs e)
		{						
			Invoke(new OnRequestEndEventHandler(DisplayRequestResponseResult), new object[] {sender, e});
		}

		private void DisplayRequestResponseResult(object sender, RequestStartEndEventArgs e)
		{
			try
			{
				if ( e.CurrentIndex == _requestResponseIndex )
				{
					panel.Controls.Clear();

					startY = 12;

					Argument arg = new Argument();
					arg.Name = "Response Result";
					arg.Value = e.Request.WebResponse.HttpBody;
					arg.DesignProperty = new ArgumentProperty();
					arg.DesignProperty.DesignerControlType = ArgumentProperty.DesignerControl.HtmlEditor;
					Control control = CreateDesignerControl(e.CurrentIndex,arg);
					control.Width = 400;
					control.Height = 400;
					control.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Bottom;
					
			
					// Label
					Label label = new Label();
					label.Location = new System.Drawing.Point(startX, startY);
					label.Name = "lbl" + arg.Name;					
					
					if ( arg.DesignProperty.Label.Length > 0 )
					{
						label.Text = arg.DesignProperty.Label;
					} 
					else 
					{
						label.Text = arg.Name;
					}

					if ( control != null )
					{
						startY += control.Size.Height + 10;

						panel.Controls.Add(label);
						panel.Controls.Add(control);
					}

					((BasicHtmlEditorControl)control).LoadDocument(e.Request.WebResponse.HttpBody);
					((BasicHtmlEditorControl)control).IsDesignMode = false;
					((BasicHtmlEditorControl)control).ToolbarVisible = false;
					this.panel1.Text = "";
					this.panel.Enabled = true;
					this.btnRun.Enabled = false;
					this.btnClose.Enabled = true;
					this.btnNextRequest.Enabled = false;
				}
			}
			catch ( Exception ex )
			{
				MessageBox.Show(ex.Message, AppLocation.ApplicationName, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		private void scriptingCommand_SessionAbortedEvent(object sender, SessionAbortEventArgs e)
		{			
			this.panel1.Text = e.ErrorMessage;
			this.panel.Enabled = true;
			this.btnRun.Enabled = true;
			this.btnClose.Enabled = true;
			this.btnNextRequest.Enabled = false;
		}
		#endregion

		private void mnuRunWithDefaultDialog_Click(object sender, System.EventArgs e)
		{
			scriptingCommand.ResetCookies();
			SendRequestResponse();
			this.panel1.Text = "Sending request... Please wait";
			this.panel.Enabled = false;
			this.btnRun.Enabled = false;
			this.btnClose.Enabled = false;
			this.btnNextRequest.Enabled = false;		
		}

		private void mnuRunWithTestDialog_Click(object sender, System.EventArgs e)
		{			
			_scrapp.LoadArgumentDefinition(_args);
			// Send ok to parent, so we can show the test dialog.
			this.DialogResult = DialogResult.OK;
			this.Close();		
		}
	}
}
