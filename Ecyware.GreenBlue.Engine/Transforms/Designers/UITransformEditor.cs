using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms.Designers;
using Ecyware.GreenBlue.Engine.Transforms;
using Ecyware.GreenBlue.Engine.HtmlDom;


namespace Ecyware.GreenBlue.Engine.Transforms.Designers
{
	/// <summary>
	/// Summary description for UITransformEditor.
	/// </summary>
	public class UITransformEditor : System.Windows.Forms.UserControl
	{
		/// <summary>
		/// Contains the names of the transform value dialogs.
		/// </summary>
		protected static string[] TransformValueDialogs = new string[] {"Use a header value",
																		   "Use a default value",
																		   "Use a client setting value",
																		   "Use a HTML value",
																		   "Use a cookie value",
																		   "Use a XPath query value", 
																		   "Use a regular expression query value"};
		private TransformValue _transformValue;
		protected static string[] TransportValueDialogs = new string[] {"None","Smtp","Gmail", "Database", "Blogger"};
		private HeaderTransformValueDialog headerDialog = new HeaderTransformValueDialog();
		private DefaultTransformValueDialog defaultDialog;
		private ClientSettingsTransformValueDialog clientSettingsDialog;
		private HtmlTransformValueDialog htmlValueDialog = new HtmlTransformValueDialog();
		private CookiesTransformValueDialog cookiesTransformDialog = new CookiesTransformValueDialog();
		private XPathQueryCommandDialog xpathDialog;
		private RegExQueryCommandDialog regexDialog;

		private ScriptingApplication _scripting;
		private int _requestIndex;
		private WebTransform _transform;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Creates a new UITransformEditor
		/// </summary>
		public UITransformEditor()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call

		}

		/// <summary>
		/// Gets or sets the transform value.
		/// </summary>
		protected virtual TransformValue TransformValue
		{
			get
			{
				return _transformValue;
			}
			set
			{
				_transformValue = value;
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

		/// <summary>
		/// Gets the WebRequest.
		/// </summary>
		[Browsable(false)]
		public virtual WebTransform WebTransform
		{
			get
			{
				return _transform;
			}
		}

		/// <summary>
		/// Gets the ScriptingData.
		/// </summary>
		[Browsable(false)]
		public virtual ScriptingApplication SessionScripting
		{
			get
			{
				return _scripting;
			}
		}

		/// <summary>
		/// Gets the selected web request index.
		/// </summary>
		[Browsable(false)]
		public virtual int SelectedWebRequestIndex
		{
			get
			{
				return _requestIndex;
			}
		}
		
		protected virtual int GetTransportValueComboIndex(Transport transport)
		{
			int index = -1;

			if ( transport == null )
			{
				index = 0;
			}
			if ( transport is SmtpTransport )
			{
				index = 1;
			}
			if ( transport is GmailTransport )
			{
				index = 2;
			}
			if ( transport is DatabaseTransport )
			{
				index = 3;
			}
			if ( transport is BloggerTransport  )
			{
				index = 4;
			}

			return index;
		}

		protected virtual int GetTransformValueComboIndex(TransformValue tvalue)
		{
			int index = -1;

			if ( tvalue is HeaderTransformValue )
			{
				index = 0;
			}
			if ( tvalue is DefaultTransformValue )
			{
				index = 1;
			}
			if ( tvalue is ClientSettingsTransformValue )
			{
				index = 2;
			}
			if ( tvalue is  HtmlTransformValue )
			{
				index = 3;
			}
			if ( tvalue is CookieTransformValue )
			{
				index = 4;
			}
			if ( tvalue is XPathQueryCommand )
			{
				index = 5;
			}
			if ( tvalue is RegExQueryCommand )
			{
				index = 6;
			}
//			if ( tvalue is SessionTransformValue  )
//			{
//				index = 7;
//			}
			return index;
		}

		/// <summary>
		/// Loads a transform editor values.
		/// </summary>
		/// <param name="requestIndex"> The selected web request index.</param>
		/// <param name="scriptingData"> The session scripting data.</param>
		/// <param name="transform"> Loads a WebTransform.</param>
		public virtual void LoadTransformEditorValues(int requestIndex, ScriptingApplication scriptingData, WebTransform transform)
		{
			_requestIndex  = requestIndex;
			_transform = transform;
			_scripting = scriptingData;
		}

		/// <summary>
		/// Load the form values.
		/// </summary>
		/// <param name="request"> The current web request.</param>
		public virtual void LoadFormValues(WebRequest request)
		{
			ArrayList list = new ArrayList();

			foreach ( HtmlTagListXml tagList in request.Form.Elements )
			{
				list.Add(tagList.Name);
			}

			htmlValueDialog.LoadForm(list);
		}

		/// <summary>
		/// Load the cookie names.
		/// </summary>
		/// <param name="request">  The current web request.</param>
		public virtual void LoadCookieNames(WebRequest request)
		{
			ArrayList list = new ArrayList();

			foreach ( Cookie cookie in request.Cookies )
			{
				list.Add(cookie.Name);
			}

			cookiesTransformDialog.LoadCookies(list);
		}

		/// <summary>
		/// Loads the header list.
		/// </summary>
		/// <param name="list"> An Array with the values to load.</param>
		protected void LoadHeaderList(ArrayList list)
		{
			headerDialog.LoadHeaders(list);
		}

		protected virtual Transport ShowTransportValueDialog(int index, Transport transport)
		{
			switch ( index )
			{
				case 0:
					transport = null;
					break;
				case 1:
					// SMTP
					SmtpTransportDialog tdialog = new SmtpTransportDialog();
					tdialog.Transport = transport;
					tdialog.ShowDialog();

					if ( tdialog.DialogResult == DialogResult.OK )
					{
						// Update
						transport = tdialog.Transport;
					}
					break;
				case 2:
					// Gmail
					GmailTransportDialog gdialog = new GmailTransportDialog();
					gdialog.Transport = transport;
					gdialog.ShowDialog();

					if ( gdialog.DialogResult == DialogResult.OK )
					{
						// Update
						transport = gdialog.Transport;
					}
					break;
				case 3:
					// Database
					DatabaseTransportDialog ddialog = new DatabaseTransportDialog();
					ddialog.Transport = transport;
					ddialog.ShowDialog();

					if ( ddialog.DialogResult == DialogResult.OK )
					{
						// Update
						transport = ddialog.Transport;
					}
					break;
				case 4:
					// Blogger
					BloggerTransportDialog bldialog = new BloggerTransportDialog();
					bldialog.Transport = transport;
					bldialog.ShowDialog();

					if ( bldialog.DialogResult == DialogResult.OK )
					{
						// Update
						transport = bldialog.Transport;
					}
					break;
//				case 5:
//					// Session
//					SessionTransportDialog ssdialog = new SessionTransportDialog();
//					ssdialog.Transport = transport;
//					ssdialog.ShowDialog();
//
//					if ( ssdialog.DialogResult == DialogResult.OK )
//					{
//						// Update
//						transport = ssdialog.Transport;
//					}
//					break;
			}

			return transport;
		}

		/// <summary>
		/// Displays the transform value dialog.
		/// </summary>
		/// <param name="index"> The dialog index.</param>
		/// <returns> The description associated with the transform value.</returns>
		protected virtual string ShowTransformValueDialog(int index)
		{
			string description = string.Empty;

			switch ( index )
			{
				case 0:
					// Use header value										
					headerDialog.TransformValue = _transformValue;
					headerDialog.LoadTransformValue();
					headerDialog.ShowDialog();

					if ( headerDialog.DialogResult == DialogResult.OK )
					{
						// Update
						_transformValue = headerDialog.TransformValue;
						description = headerDialog.Description;
					}
					break;
				case 1:
					// Use a default value				
					defaultDialog = new DefaultTransformValueDialog();	
					defaultDialog.TransformValue = _transformValue;
					defaultDialog.LoadTransformValue();
					defaultDialog.ShowDialog();

					if ( defaultDialog.DialogResult == DialogResult.OK )
					{
						// Update
						_transformValue = defaultDialog.TransformValue;
						description = defaultDialog.Description;
					}
					defaultDialog.Close();
					break;
				case 2:
					// Use a client setting					
					clientSettingsDialog  = new ClientSettingsTransformValueDialog();
					clientSettingsDialog.TransformValue = _transformValue;
					clientSettingsDialog.LoadTransformValue();
					clientSettingsDialog.ShowDialog();

					if ( clientSettingsDialog.DialogResult == DialogResult.OK )
					{
						// Update
						_transformValue = clientSettingsDialog.TransformValue;
						description = clientSettingsDialog.Description;
					}
					clientSettingsDialog.Close();
					break;
				case 3:
					// Use a HTML Transform value
					// htmlValueDialog = new HtmlTransformValueDialog();
					htmlValueDialog.TransformValue = _transformValue;
					htmlValueDialog.LoadTransformValue();
					htmlValueDialog.ShowDialog();

					if ( htmlValueDialog.DialogResult == DialogResult.OK )
					{
						// Update
						_transformValue = htmlValueDialog.TransformValue;
						description = htmlValueDialog.Description;
					}
					// htmlValueDialog.Close();
					break;
				case 4:					
					// Use a Cookie Transform value	
					// cookiesTransformDialog = new CookiesTransformValueDialog();
					cookiesTransformDialog.TransformValue = _transformValue;
					cookiesTransformDialog.LoadTransformValue();
					cookiesTransformDialog.ShowDialog();

					if ( cookiesTransformDialog.DialogResult == DialogResult.OK )
					{
						// Update
						_transformValue = cookiesTransformDialog.TransformValue;
						description = cookiesTransformDialog.Description;
					}
					// cookiesTransformDialog.Close();
					break;
				case 5:
					// Use a XPath Transform value					
					xpathDialog = new XPathQueryCommandDialog();
					xpathDialog.TransformValue = _transformValue;
					xpathDialog.LoadTransformValue();
					xpathDialog.ShowDialog();

					if ( xpathDialog.DialogResult == DialogResult.OK )
					{
						// Update
						_transformValue = xpathDialog.TransformValue;
						description = xpathDialog.Description;
					}
					xpathDialog.Close();
					break;
				case 6:
					// Use a RegEx Transform value					
					regexDialog = new RegExQueryCommandDialog();
					regexDialog.TransformValue = _transformValue;
					regexDialog.LoadTransformValue();
					regexDialog.ShowDialog();

					if ( regexDialog.DialogResult == DialogResult.OK )
					{
						// Update
						_transformValue = regexDialog.TransformValue;
						description = regexDialog.Description;
					}
					regexDialog.Close();
					break;
			}

			return description;
		}


		/// <summary>
		/// Clears the editor.
		/// </summary>
		public virtual void Clear()
		{
		}
	}
}
