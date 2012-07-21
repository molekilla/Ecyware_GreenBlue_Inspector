using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using mshtml;

namespace Ecyware.GreenBlue.GreenBlueMain
{
	/// <summary>
	/// Summary description for CaptureBrowserImage.
	/// </summary>
	public class CaptureBrowserImage : System.Windows.Forms.UserControl
	{

		const uint PW_CLIENTONLY = 0x00000001;
		const uint WM_PRINT = 0x0317;
		const uint WM_PRINTCLIENT = 0x0318; 
		const uint PRF_CHECKVISIBLE = 0x00000001;
		const uint PRF_NONCLIENT = 0x00000002;
		const uint PRF_CLIENT = 0x00000004;
		const uint PRF_ERASEBKGND = 0x00000008;
		const uint PRF_CHILDREN = 0x00000010;
		const uint PRF_OWNED = 0x00000020;
		private onlyconnect.HtmlEditor web;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new GenerateHtmlContentImage.
		/// </summary>
		public CaptureBrowserImage()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
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
			this.web = new onlyconnect.HtmlEditor();
			this.SuspendLayout();
			// 
			// web
			// 
			this.web.DefaultComposeSettings.BackColor = System.Drawing.Color.White;
			this.web.DefaultComposeSettings.DefaultFont = new System.Drawing.Font("Arial", 10F);
			this.web.DefaultComposeSettings.Enabled = false;
			this.web.DefaultComposeSettings.ForeColor = System.Drawing.Color.Black;
			this.web.Dock = System.Windows.Forms.DockStyle.Fill;
			this.web.DocumentEncoding = onlyconnect.EncodingType.WindowsCurrent;
			this.web.IsActiveContentEnabled = false;
			this.web.Location = new System.Drawing.Point(0, 0);
			this.web.Name = "web";
			this.web.SelectionAlignment = System.Windows.Forms.HorizontalAlignment.Left;
			this.web.SelectionBackColor = System.Drawing.Color.Empty;
			this.web.SelectionBullets = false;
			this.web.SelectionFont = null;
			this.web.SelectionForeColor = System.Drawing.Color.Empty;
			this.web.SelectionNumbering = false;
			this.web.Size = new System.Drawing.Size(640, 480);
			this.web.TabIndex = 0;
			// 
			// CaptureBrowserImage
			// 
			this.Controls.Add(this.web);
			this.Name = "CaptureBrowserImage";
			this.Size = new System.Drawing.Size(640, 480);
			this.ResumeLayout(false);

		}
		#endregion

		static public void SaveAsImage(string 
			url, string pngFile)
		{
			// create a hidden control and tell it to DoIt()
			// todo: cleanup after exceptions
			CaptureBrowserImage captureBrowser = new 
				CaptureBrowserImage();
			captureBrowser.CreateControl();
			captureBrowser.CaptureImage(url, pngFile);
			captureBrowser.Dispose();
		}

		private void CaptureImage(string url, string 
			pngFile)
		{

			while(!this.web.Created) 
				Application.DoEvents();

			// Tell ie to 'navigate' to the webpage of interest
			// if the page opens popups adds, things can get whacky... could possibly defeat that
			// by hooking into BeforeNavigate2() and cancelling the action??
			object arg1 = 0;
			object arg2 = ""; 
			object arg3 = ""; 
			object arg4 = "";
			//this.web.Navigate(url,ref arg1,ref arg2, ref arg3, ref arg4);

			this.web.LoadUrl(url);

			// Wait for the document to load
//			while (this.web.ReadyState != SHDocVw.tagREADYSTATE.READYSTATE_COMPLETE )
//			{
//				Application.DoEvents();
//			}

			// Wait for the DOM to be accessible 
			while(GetHtmlBody() == null)
				Application.DoEvents();
			
			// Capture the image
			CaptureImage(pngFile);

			return;
		}
		
		private IHTMLElement2 GetHtmlBody()
		{
			IHTMLDocument2 htmlDocument2 = (IHTMLDocument2)this.web.Document;
			return (IHTMLElement2)htmlDocument2.body;
		}

		private void CaptureImage(string pngFile)
		{
			try
			{
				// Figure out width and height of full page plus scroll bar slop
//				IHTMLElement2 htmlBody2 = GetHtmlBody();
//				int w = htmlBody2.scrollWidth;
//				int h = htmlBody2.scrollHeight;
//
//				// Make our embedded ie control that size
//				if((web.Height != h + 30) || (web.Width != w + 30))
//				{
//					web.Height = h + 30;
//					web.Width = w + 30;
//				}

				// Setup bitmap memory and wrap a DC around it
				Bitmap bitmap = new Bitmap(640, 480, PixelFormat.Format24bppRgb);
				Graphics graphics = Graphics.FromImage(bitmap);    
				IntPtr memdc = graphics.GetHdc();
				IntPtr hbitmap = bitmap.GetHbitmap();
				SelectObject(memdc, hbitmap);

				// Tell ie to print into our dc/bitmap
				// Use PrintWindow windows api
				//   
				//   
				bool rv = PrintWindow(web.Handle, memdc, 
					PW_CLIENTONLY);

				// Send it a print msg + flags

				int err = SendMessage(web.Handle, WM_PRINT, (uint)memdc, (uint) (PRF_CLIENT | PRF_NONCLIENT | PRF_OWNED | PRF_CHILDREN | PRF_ERASEBKGND));

				// Save image to disk as a PNG
				Bitmap bitmap2 = Bitmap.FromHbitmap(hbitmap);
				bitmap2.Save(pngFile, ImageFormat.Png);

				// Cleanup
				DeleteObject(hbitmap);
				graphics.ReleaseHdc( memdc );
				graphics.Dispose();
				bitmap.Dispose();
				bitmap2.Dispose();
			}
			catch(Exception ex)
			{
				Utils.ExceptionHandler.RegisterException(ex);
			}  
		}

		#region Win 32 api

		[DllImport("Gdi32.dll")]
		private static extern IntPtr SelectObject(
			IntPtr hdc,      // handle to DC
			IntPtr hgdiobj   // handle to object
			);

		[DllImport("Gdi32.dll")]
		private static extern bool DeleteObject(
			IntPtr hObject   // handle to graphic object
			);

		[DllImport("Gdi32.dll")]
		private static extern IntPtr 
			CreateCompatibleDC(
			IntPtr hdc   // handle to DC
			);

		[DllImport("Gdi32.dll")]
		private static extern bool DeleteDC(
			IntPtr hdc   // handle to DC
			);

		[DllImport("User32.dll")]
		private static extern bool PrintWindow(
			IntPtr hwnd,               // Window to copy
			IntPtr hdcBlt,             // HDC to print into
			uint nFlags              // Optional flags  
			// must contain PW_CLIENTONLY
			);

		[DllImport("User32.dll")]
		private static extern int SendMessage(
			IntPtr hWnd,  // handle to destination window
			uint Msg,  // message
			uint wParam,  // first message parameter
			uint lParam  // second message parameter
			);

		#endregion
	}
}
