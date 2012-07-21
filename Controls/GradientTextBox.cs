// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: January 2005
using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using Microsoft;


namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Summary description for GradientTextBox.
	/// </summary>
	public class GradientTextBox : System.Windows.Forms.TextBox
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		PictureBox _pictureBox = new PictureBox();
		Color _colorTop = Color.White ;
		Color _colorDown = Color.White ;
		float _orientacion = 90.0F;

		public GradientTextBox(System.ComponentModel.IContainer container)
		{
			///
			/// Required for Windows.Forms Class Composition Designer support
			///
			container.Add(this);
			InitializeComponent();

			_pictureBox.Dock = DockStyle.Fill;
			this.Controls.Add( _pictureBox );
		}

		public GradientTextBox()
		{
			///
			/// Required for Windows.Forms Class Composition Designer support
			///
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
			components = new System.ComponentModel.Container();
		}
		#endregion


		[
		Category("Appearance"),
		Description("Top Gradient Color"),
		Browsable(true),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)
		]
		public Color ColorTop
		{
			get {return _colorTop;}
			set {_colorTop = value;}
		}

		[
		Category("Appearance"),
		Description("Down Gradient Color"),
		Browsable(true),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)
		]
		public Color ColorDown
		{
			get {return _colorDown;}
			set {_colorDown = value;}
		}

		[
		Category("Appearance"),
		Description("Orientacion del Gradient"),
		Browsable(true),
		DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)
		]
		public float Orientacion
		{
			get {return _orientacion;}
			set {_orientacion = value;}
		}

		protected override void WndProc( ref Message m )
		{
			base.WndProc( ref m );     
			switch( m.Msg )
			{
				case win32.WM_PAINT :
					//this.BorderStyle= BorderStyle.None ;

					Bitmap bmpCaptured = new Bitmap( this.ClientRectangle.Width, this.ClientRectangle.Height );
					Bitmap bmpResult = new Bitmap( this.ClientRectangle.Width,this.ClientRectangle.Height );
					Rectangle r = new Rectangle( 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height );
					
					CaptureWindow( this, ref bmpCaptured ); 
					//this.SetStyle( ControlStyles.SupportsTransparentBackColor, true );
					//this.BackColor = Color.Transparent;
					 
					System.Drawing.Imaging.ImageAttributes imgAttrib = new System.Drawing.Imaging.ImageAttributes();
					 
					System.Drawing.Imaging.ColorMap[] colorMap = new System.Drawing.Imaging.ColorMap[1];
					colorMap[0] = new System.Drawing.Imaging.ColorMap();
					colorMap[0].OldColor = Color.White;
					colorMap[0].NewColor = Color.Transparent;
					imgAttrib.SetRemapTable( colorMap ); 

					Graphics g = Graphics.FromImage( bmpResult );


					g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality; 
					//Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
			 
					System.Drawing.Drawing2D.LinearGradientBrush lgBrush =
						new System.Drawing.Drawing2D.LinearGradientBrush(
						r, this._colorTop , this._colorDown , this._orientacion );
					g.FillRectangle(lgBrush,r);


					g.DrawImage( bmpCaptured, r, 1 , 1, this.ClientRectangle.Width, this.ClientRectangle.Height, GraphicsUnit.Pixel, imgAttrib );

					g.Dispose();

					_pictureBox.Image = ( Image )bmpResult.Clone(); 
					break;

				
				case win32.WM_HSCROLL:

				case win32.WM_VSCROLL:

					this.Invalidate(); // repaint
					// if you use scrolling then add these two case statements

					break;
			}
		}

		private static void CaptureWindow( Control control, ref Bitmap bitmap )
		{
			Graphics g = Graphics.FromImage( bitmap );
			//int i = ( int )( win32.PRF_CLIENT | win32.PRF_ERASEBKGND );
			IntPtr iPtr = new IntPtr( 14 );
			IntPtr hdc = g.GetHdc();
			win32.SendMessage( control.Handle, win32.WM_PRINT, hdc, iPtr ); 
			g.ReleaseHdc( hdc );
			g.Dispose();
		}

	}
}
