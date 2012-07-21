using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Ecyware.GreenBlue.Engine;
using Ecyware.GreenBlue.Engine.Transforms.Designers;

namespace Ecyware.GreenBlue.Controls.Scripting
{
	/// <summary>
	/// Summary description for TransformDocumentationControl.
	/// </summary>
	public class TransformDocumentationControl : UITransformEditor
	{
		private System.Windows.Forms.RichTextBox rtfEditor;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public TransformDocumentationControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			string transformMainPage = AppLocation.CommonFolder + "\\WebTransformDocRTF.rtf";
			this.rtfEditor.LoadFile(transformMainPage);	
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
			this.rtfEditor = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// rtfEditor
			// 
			this.rtfEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtfEditor.Location = new System.Drawing.Point(0, 0);
			this.rtfEditor.Name = "rtfEditor";
			this.rtfEditor.ReadOnly = true;
			this.rtfEditor.Size = new System.Drawing.Size(528, 414);
			this.rtfEditor.TabIndex = 0;
			this.rtfEditor.Text = "";
			// 
			// TransformDocumentationControl
			// 
			this.Controls.Add(this.rtfEditor);
			this.Name = "TransformDocumentationControl";
			this.Size = new System.Drawing.Size(528, 414);
			this.ResumeLayout(false);

		}
		#endregion
	}
}
