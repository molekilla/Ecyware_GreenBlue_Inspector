using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using mshtml;

namespace Ecyware.GreenBlue.Protocols.Http.Scripting
{
	/// <summary>
	/// Summary description for BasicHtmlEditorControl.
	/// </summary>
	public class BasicHtmlEditorControl : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.ToolBarButton tbItalic;
		private System.Windows.Forms.ToolBar toolBar;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.FontDialog fontDialog1;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tbDesign;
		private System.Windows.Forms.TabPage tbHtml;
		private onlyconnect.HtmlEditor htmlEditor;
		private System.Windows.Forms.TextBox txtHtml;
		private System.Windows.Forms.ToolBarButton tbPaste;
		private System.Windows.Forms.ToolBarButton tbCut;
		private System.Windows.Forms.ToolBarButton tbCopy;
		private System.Windows.Forms.ToolBarButton tbJustifyLeft;
		private System.Windows.Forms.ToolBarButton tbJustifyCenter;
		private System.Windows.Forms.ToolBarButton tbJustifyRight;
		private System.Windows.Forms.ToolBarButton tbBackColor;
		private System.Windows.Forms.ToolBarButton tbForeColor;
		private System.Windows.Forms.ToolBarButton tbNumbering;
		private System.Windows.Forms.ToolBarButton tbBullets;
		private System.Windows.Forms.ToolBarButton tbFont;
		private System.Windows.Forms.ToolBarButton tbFind;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Creates a new BasicHtmlEditorControl.
		/// </summary>
		public BasicHtmlEditorControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			this.htmlEditor.LoadDocument("<html><body>Write here...</body></html>");
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(BasicHtmlEditorControl));
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.tbItalic = new System.Windows.Forms.ToolBarButton();
			this.toolBar = new System.Windows.Forms.ToolBar();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.fontDialog1 = new System.Windows.Forms.FontDialog();
			this.tabControl = new System.Windows.Forms.TabControl();
			this.tbDesign = new System.Windows.Forms.TabPage();
			this.tbHtml = new System.Windows.Forms.TabPage();
			this.htmlEditor = new onlyconnect.HtmlEditor();
			this.txtHtml = new System.Windows.Forms.TextBox();
			this.tbPaste = new System.Windows.Forms.ToolBarButton();
			this.tbCut = new System.Windows.Forms.ToolBarButton();
			this.tbCopy = new System.Windows.Forms.ToolBarButton();
			this.tbJustifyLeft = new System.Windows.Forms.ToolBarButton();
			this.tbJustifyCenter = new System.Windows.Forms.ToolBarButton();
			this.tbJustifyRight = new System.Windows.Forms.ToolBarButton();
			this.tbBackColor = new System.Windows.Forms.ToolBarButton();
			this.tbForeColor = new System.Windows.Forms.ToolBarButton();
			this.tbNumbering = new System.Windows.Forms.ToolBarButton();
			this.tbBullets = new System.Windows.Forms.ToolBarButton();
			this.tbFont = new System.Windows.Forms.ToolBarButton();
			this.tbFind = new System.Windows.Forms.ToolBarButton();
			this.tabControl.SuspendLayout();
			this.tbDesign.SuspendLayout();
			this.tbHtml.SuspendLayout();
			this.SuspendLayout();
			// 
			// contextMenu1
			// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						 this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.Text = "Font";
			// 
			// tbItalic
			// 
			this.tbItalic.ImageIndex = 4;
			// 
			// toolBar
			// 
			this.toolBar.Appearance = System.Windows.Forms.ToolBarAppearance.Flat;
			this.toolBar.Buttons.AddRange(new System.Windows.Forms.ToolBarButton[] {
																					   this.tbPaste,
																					   this.tbCut,
																					   this.tbCopy,
																					   this.tbJustifyLeft,
																					   this.tbJustifyCenter,
																					   this.tbJustifyRight,
																					   this.tbBackColor,
																					   this.tbForeColor,
																					   this.tbItalic,
																					   this.tbFont,
																					   this.tbBullets,
																					   this.tbNumbering,
																					   this.tbFind});
			this.toolBar.ButtonSize = new System.Drawing.Size(16, 16);
			this.toolBar.DropDownArrows = true;
			this.toolBar.ImageList = this.imageList1;
			this.toolBar.Location = new System.Drawing.Point(0, 0);
			this.toolBar.Name = "toolBar";
			this.toolBar.ShowToolTips = true;
			this.toolBar.Size = new System.Drawing.Size(378, 28);
			this.toolBar.TabIndex = 1;
			this.toolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(this.toolBar_ButtonClick);
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth24Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// tabControl
			// 
			this.tabControl.Controls.Add(this.tbDesign);
			this.tabControl.Controls.Add(this.tbHtml);
			this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl.ImageList = this.imageList1;
			this.tabControl.Location = new System.Drawing.Point(0, 28);
			this.tabControl.Name = "tabControl";
			this.tabControl.SelectedIndex = 0;
			this.tabControl.Size = new System.Drawing.Size(378, 308);
			this.tabControl.TabIndex = 2;
			this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
			// 
			// tbDesign
			// 
			this.tbDesign.Controls.Add(this.htmlEditor);
			this.tbDesign.ImageIndex = 17;
			this.tbDesign.Location = new System.Drawing.Point(4, 23);
			this.tbDesign.Name = "tbDesign";
			this.tbDesign.Size = new System.Drawing.Size(370, 281);
			this.tbDesign.TabIndex = 0;
			this.tbDesign.Text = "Design";
			// 
			// tbHtml
			// 
			this.tbHtml.Controls.Add(this.txtHtml);
			this.tbHtml.ImageIndex = 18;
			this.tbHtml.Location = new System.Drawing.Point(4, 23);
			this.tbHtml.Name = "tbHtml";
			this.tbHtml.Size = new System.Drawing.Size(370, 281);
			this.tbHtml.TabIndex = 1;
			this.tbHtml.Text = "HTML";
			// 
			// htmlEditor
			// 
			this.htmlEditor.ContextMenu = this.contextMenu1;
			this.htmlEditor.DefaultComposeSettings.BackColor = System.Drawing.Color.White;
			this.htmlEditor.DefaultComposeSettings.DefaultFont = new System.Drawing.Font("Arial", 10F);
			this.htmlEditor.DefaultComposeSettings.Enabled = false;
			this.htmlEditor.DefaultComposeSettings.ForeColor = System.Drawing.Color.Black;
			this.htmlEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.htmlEditor.DocumentEncoding = onlyconnect.EncodingType.WindowsCurrent;
			this.htmlEditor.IsActiveContentEnabled = false;
			this.htmlEditor.IsDesignMode = true;
			this.htmlEditor.Location = new System.Drawing.Point(0, 0);
			this.htmlEditor.Name = "htmlEditor";
			this.htmlEditor.OpenLinksInNewWindow = true;
			this.htmlEditor.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.htmlEditor.SelectionBackColor = System.Drawing.Color.Empty;
			this.htmlEditor.SelectionBullets = false;
			this.htmlEditor.SelectionFont = null;
			this.htmlEditor.SelectionForeColor = System.Drawing.Color.Empty;
			this.htmlEditor.SelectionNumbering = false;
			this.htmlEditor.Size = new System.Drawing.Size(370, 281);
			this.htmlEditor.TabIndex = 1;
			// 
			// txtHtml
			// 
			this.txtHtml.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtHtml.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtHtml.Location = new System.Drawing.Point(0, 0);
			this.txtHtml.Multiline = true;
			this.txtHtml.Name = "txtHtml";
			this.txtHtml.Size = new System.Drawing.Size(370, 281);
			this.txtHtml.TabIndex = 0;
			this.txtHtml.Text = "";
			// 
			// tbPaste
			// 
			this.tbPaste.ImageIndex = 14;
			// 
			// tbCut
			// 
			this.tbCut.ImageIndex = 3;
			// 
			// tbCopy
			// 
			this.tbCopy.ImageIndex = 2;
			// 
			// tbJustifyLeft
			// 
			this.tbJustifyLeft.ImageIndex = 7;
			// 
			// tbJustifyCenter
			// 
			this.tbJustifyCenter.ImageIndex = 5;
			// 
			// tbJustifyRight
			// 
			this.tbJustifyRight.ImageIndex = 8;
			// 
			// tbBackColor
			// 
			this.tbBackColor.ImageIndex = 15;
			// 
			// tbForeColor
			// 
			this.tbForeColor.ImageIndex = 16;
			// 
			// tbNumbering
			// 
			this.tbNumbering.ImageIndex = 9;
			// 
			// tbBullets
			// 
			this.tbBullets.ImageIndex = 1;
			// 
			// tbFont
			// 
			this.tbFont.ImageIndex = 19;
			// 
			// tbFind
			// 
			this.tbFind.ImageIndex = 20;
			// 
			// BasicHtmlEditorControl
			// 
			this.BackColor = System.Drawing.SystemColors.Control;
			this.Controls.Add(this.tabControl);
			this.Controls.Add(this.toolBar);
			this.Name = "BasicHtmlEditorControl";
			this.Size = new System.Drawing.Size(378, 336);
			this.tabControl.ResumeLayout(false);
			this.tbDesign.ResumeLayout(false);
			this.tbHtml.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void toolBar_ButtonClick(object sender, System.Windows.Forms.ToolBarButtonClickEventArgs e)
		{
			if ( e.Button == this.tbItalic )
			{
				this.htmlEditor.SetSelectionItalic();
			}

			if ( e.Button == this.tbFind )
			{
				this.htmlEditor.ShowFindDialog();
			}

			if ( e.Button == this.tbCut )
			{
				this.htmlEditor.Cut();
			}

			if ( e.Button == this.tbCopy )
			{
				this.htmlEditor.Copy();
			}

			if ( e.Button == this.tbPaste )
			{
				this.htmlEditor.Paste();
			}

			if ( e.Button == this.tbJustifyLeft )
			{
				this.htmlEditor.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
			}
			if ( e.Button == this.tbJustifyRight )
			{
				this.htmlEditor.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Right;
			}
			if ( e.Button == this.tbJustifyCenter )
			{
				this.htmlEditor.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Center;
			}
			if ( e.Button == this.tbBackColor )
			{
				if ( this.colorDialog1.ShowDialog() == DialogResult.OK )
				{
					this.htmlEditor.SelectionBackColor = colorDialog1.Color;
				}
			}
			if ( e.Button == this.tbForeColor )
			{
				if ( this.colorDialog1.ShowDialog() == DialogResult.OK )
				{
					this.htmlEditor.SelectionForeColor = colorDialog1.Color;
				}
			}
			if ( e.Button == this.tbFont )
			{
				if ( this.fontDialog1.ShowDialog() == DialogResult.OK )
				{
					this.htmlEditor.SelectionFont = fontDialog1.Font;
				}
			}
			if ( e.Button == this.tbBullets )
			{
				if ( this.htmlEditor.SelectionBullets )
				{
					this.htmlEditor.SelectionBullets = false;
				} 
				else 
				{
					this.htmlEditor.SelectionBullets = true;
				}
			}
			if ( e.Button == this.tbNumbering )
			{
				if ( this.htmlEditor.SelectionNumbering )
				{
					this.htmlEditor.SelectionNumbering = false;
				} 
				else 
				{
					this.htmlEditor.SelectionNumbering = true;
				}
			}



		}

		private void tabControl_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if ( tabControl.SelectedIndex == 0 )
			{
				if ( this.txtHtml.Text.Length > 0 )
				{
					this.htmlEditor.LoadDocument(this.txtHtml.Text);
				}
			} 
			else 
			{
				// Load Html
				this.txtHtml.Text = this.BodyOuterHtml;
			}
		
		}

		/// <summary>
		/// Gets or sets the toolbar visible.
		/// </summary>
		public bool ToolbarVisible
		{
			get
			{
				return this.toolBar.Visible;
			}
			set
			{
				this.toolBar.Visible = value;
			}
		}
		/// <summary>
		/// Gets or sets the design mode.
		/// </summary>
		public bool IsDesignMode
		{
			get
			{
				return this.htmlEditor.IsDesignMode;
			}
			set
			{
				this.htmlEditor.IsDesignMode = value;
			}
		}

		/// <summary>
		/// Loads a document.
		/// </summary>
		/// <param name="body"></param>
		public void LoadDocument(string body)
		{
			this.htmlEditor.LoadDocument(body);
		}
		/// <summary>
		/// Gets or sets the BODY inner html.
		/// </summary>
		public string BodyInnerHtml
		{
			get
			{
				IHTMLDocument2 document = (IHTMLDocument2)this.htmlEditor.Document;
				return document.body.innerHTML;
			}
			set
			{
				IHTMLDocument2 document = (IHTMLDocument2)this.htmlEditor.Document;
				document.body.innerHTML = value;
			}
		}

		/// <summary>
		/// Gets or sets the BODY outer html.
		/// </summary>
		public string BodyOuterHtml
		{
			get
			{
				IHTMLDocument2 document = (IHTMLDocument2)this.htmlEditor.Document;
				return document.body.outerHTML;
			}
			set
			{
				IHTMLDocument2 document = (IHTMLDocument2)this.htmlEditor.Document;
				document.body.outerHTML = value;
			}
		}
	}
}
