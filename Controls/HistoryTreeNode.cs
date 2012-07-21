// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2003

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Data;
using Ecyware.GreenBlue.Engine;

namespace Ecyware.GreenBlue.Controls
{
	/// <summary>
	/// Represents a node in a HistoryTree.
	/// </summary>
	public sealed class HistoryTreeNode : TreeNode
	{
		ResponseBuffer _responseBuffer=null;
		Uri _uri;

		/// <summary>
		/// Creates a new HistoryTreeNode.
		/// </summary>
		public HistoryTreeNode() : base()
		{
		}

		/// <summary>
		/// Creates a new HistoryTreeNode and sets the text, url and data properties.
		/// </summary>
		/// <param name="text"> The node text.</param>
		/// <param name="url"> The uri.</param>
		/// <param name="data"> The ResponseBuffer data.</param>
		public HistoryTreeNode(string text, Uri url, ResponseBuffer data)
		{
			this.Text=text;
			this.Url=url;
			this.HttpSiteData=data;
		}

		/// <summary>
		/// Gets or sets the Url.
		/// </summary>
		public Uri Url
		{
			get
			{
				return _uri;
			}
			set
			{
				_uri = value;
			}
		}

		/// <summary>
		/// Gets or sets the ResponseBuffer.
		/// </summary>
		public ResponseBuffer HttpSiteData
		{
			get
			{
				return _responseBuffer;
			}
			set
			{
				_responseBuffer = value;
			}
		}
	}
}
