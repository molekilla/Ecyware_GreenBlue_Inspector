using System;
using System.Drawing;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;

namespace Ecyware.GreenBlue.Engine.Scripting
{
	/// <summary>
	/// Summary description for ArgumentProperty.
	/// </summary>
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ArgumentProperty
	{
		/// <summary>
		/// Implements the designer control enum.
		/// </summary>
		public enum DesignerControl
		{
			TextBox,
			RichTextBox,
			DropDown,
			DropDownList,
			CheckBox,
			HtmlEditor
		}

		DesignerControl _designerControlType = DesignerControl.TextBox;
		char _passwordChar;
		bool _readOnly = false;
		bool _multiline = false;
		string _label = string.Empty;
		bool _wordWrap = false;
		string _foreColorString = string.Empty;
		Color _foreColor = Color.Green;
		string _fontString = string.Empty;
		Font _font = new Font("Arial", 0.9f);
		string _sizeString = string.Empty;
		Size _size = new Size(400, 20);
		string _comboElementName;

		/// <summary>
		/// Creates a new ArgumentProperty.
		/// </summary>
		public ArgumentProperty()
		{
		}

		/// <summary>
		/// Gets or sets the designer control type.
		/// </summary>
		[Description("Gets or sets the designer control type.")]
		public DesignerControl DesignerControlType
		{
			get
			{
				return _designerControlType;
			}
			set
			{
				_designerControlType = value;
			}
		}
//
//		#region ComboBox Properties
//		/// <summary>
//		/// Gets or sets the name of the Html element that contains the combo box data.
//		/// </summary>
//		[Description("Gets or sets the HTML form element to load the combo box data, if available.")]
//		[Category("Combo Box")]
//		public string LoadComboDataFromHtmlElement
//		{
//			get
//			{
//				return _comboElementName;
//			}
//			set
//			{
//				_comboElementName = value;
//			}
//		}
//		#endregion

		/// <summary>
		/// Gets or sets the password char.
		/// </summary>
		[Description("Gets or sets password char.")]
		public char PasswordChar
		{
			get
			{
				return _passwordChar;
			}
			set
			{
				_passwordChar = value;
			}
		}

		/// <summary>
		/// Gets or sets the multiline.
		/// </summary>
		[Description("Gets or sets if the text supports multiline.")]
		public bool Multiline
		{
			get
			{
				return _multiline;
			}
			set
			{
				_multiline = value;
			}
		}

		/// <summary>
		/// Gets or sets if the text is read only.
		/// </summary>
		[Description("Gets or sets if the text is read only.")]
		public bool ReadOnly
		{
			get
			{
				return _readOnly;
			}
			set
			{
				_readOnly = value;
			}
		}

		/// <summary>
		/// Gets or sets the label.
		/// </summary>
		[Description("Gets or sets if the label for the control.")]
		public string Label
		{
			get
			{
				return _label;
			}
			set
			{
				_label = value;
			}
		}

		/// <summary>
		/// Gets or sets the word wrap.
		/// </summary>
		[Description("Gets or sets if the text wrapped.")]
		public bool WordWrap
		{
			get
			{
				return _wordWrap;
			}
			set
			{
				_wordWrap = value;
			}
		}

		/// <summary>
		/// Gets or sets the fore color.
		/// </summary>
		[Description("Gets or sets the color")]
		[XmlIgnore]
		public Color ForeColor
		{
			get
			{
				if ( _foreColorString.Length > 0 )
				{
					ColorConverter converter = new ColorConverter();
					_foreColor = (Color)converter.ConvertFromInvariantString(_foreColorString);
				}

				return _foreColor;
			}
			set
			{
				ColorConverter converter = new ColorConverter();
				_foreColorString = converter.ConvertToInvariantString(value);
				_foreColor = value;
			}
		}

		/// <summary>
		/// Gets or sets the fore color.
		/// </summary>
		[Description("Gets or sets the color")]
		[Browsable(false)]
		public string ForeColorString
		{
			get
			{
				return _foreColorString;
			}
			set
			{
				_foreColorString = value;
			}
		}

		/// <summary>
		/// Gets or sets the font.
		/// </summary>
		[Browsable(false)]		
		public string FontString
		{
			get
			{
				return _fontString;
			}
			set
			{
				_fontString = value;
			}
		}


		/// <summary>
		/// Gets or sets the font.
		/// </summary>
		[Description("Gets or sets the font")]
		[XmlIgnore]
		public Font Font
		{
			get
			{
				if ( _fontString.Length > 0 )
				{
					_font = GetFont();
				}

				return _font;
			}
			set
			{
				if ( value != null )
				{
					SetFont(value);
				}
				_font = value;
			}
		}


		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		[Description("Gets or sets the size.")]
		[XmlIgnore]
		public Size Size
		{
			get
			{
				if ( _sizeString.Length > 0 )
				{
					_size = GetSize();
				}

				return _size;
			}
			set
			{
				SetSize(value);
				_size = value;
			}
		}
		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		[Browsable(false)]		
		public string SizeString
		{
			get
			{
				return _sizeString;
			}
			set
			{
				_sizeString = value;
			}
		}
		/// <summary>
		/// Gets the size.
		/// </summary>
		/// <returns></returns>
		public Size GetSize()
		{
			SizeConverter converter = new SizeConverter();
			return (Size)converter.ConvertFromInvariantString(_sizeString);
			
		}

		/// <summary>
		/// Sets the size.
		/// </summary>
		/// <param name="size"></param>
		public void SetSize(Size size)
		{
			SizeConverter converter = new SizeConverter();
			_sizeString = converter.ConvertToInvariantString(size);
		}

		/// <summary>
		/// Gets the font.
		/// </summary>
		/// <returns></returns>
		public Font GetFont()
		{
			FontConverter converter = new FontConverter();
			return (Font)converter.ConvertFromInvariantString(_fontString);
		}

		/// <summary>
		/// Sets the font.
		/// </summary>
		/// <param name="font"></param>
		public void SetFont(Font font)
		{
			FontConverter converter = new FontConverter();
			_fontString = converter.ConvertToInvariantString(font);
		}

	}
}
