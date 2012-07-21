// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003
using System;

namespace Ecyware.GreenBlue.Controls
{
	public class ChangeStatusBarEventArgs:EventArgs
	{
		string _text=String.Empty;
		int _index;
		EventHandler _clickDelegate=null;

		/// <summary>
		/// Creates a new ChangeStatusBarEventArgs.
		/// </summary>
		public ChangeStatusBarEventArgs()
		{
		}

		/// <summary>
		/// Creates a new ChangeStatusBarEventArgs.
		/// </summary>
		/// <param name="index"> The panel index.</param>
		/// <param name="panelText"> The text message.</param>
		/// <param name="clickDelegate"> The click delegate.</param>
		public ChangeStatusBarEventArgs(int index,string panelText,EventHandler clickDelegate)
		{
			this.Index=index;
			this.Text=panelText;
			this.ClickDelegate=clickDelegate;
		}

		/// <summary>
		/// Gets or sets the click event handler.
		/// </summary>
		public EventHandler ClickDelegate
		{
			get
			{
				return _clickDelegate;
			}
			set
			{
				_clickDelegate = value;
			}
		}

		/// <summary>
		/// Gets or sets the text.
		/// </summary>
		public string Text
		{
			get
			{
				return _text;
			}
			set
			{
				_text = value;
			}
		}

		/// <summary>
		/// Gets or sets the panel index.
		/// </summary>
		public int Index
		{
			get
			{
				return _index;
			}
			set
			{
				_index = value;
			}
		}

	}
}
