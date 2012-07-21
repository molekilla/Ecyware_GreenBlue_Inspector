// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: March 2005
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;
using System.Xml;
using System.IO;
using Compona;
using Compona.SourceCode;

namespace Ecyware.GreenBlue.Utils
{
	/// <summary>
	/// Summary description for RegExDesignerDialog.
	/// </summary>
	public class RegExDesignerDialog : System.Windows.Forms.Form
	{
		Hashtable specialTags = new Hashtable();
		string getElements;
		string getAttributes;
		string _query;		
		private string _text;
		private string textValue = string.Empty;		
		string htmlSyntaxFile = string.Empty;
		Compona.SourceCode.Language language;
		private System.Windows.Forms.GroupBox grpHTTPCommands;
		private System.Windows.Forms.Button cmdFilter;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		internal Compona.Windows.Forms.SyntaxBoxControl txtEditor;
		private Compona.SourceCode.SyntaxDocument syntaxDocument2;
		private System.Windows.Forms.Button btnSet;
		private System.Windows.Forms.TextBox txtRegEx;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtTag;
		private System.Windows.Forms.RadioButton rbHtmlElement;
		private System.Windows.Forms.RadioButton rbHtmlAttribute;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.RadioButton rbCustom;
		private System.Windows.Forms.TextBox txtGroup;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown numMatchFrom;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown numMatchTo;
		private System.Windows.Forms.NumericUpDown numGroupMatchTo;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.NumericUpDown numGroupMatchFrom;
		private System.Windows.Forms.CheckBox chkApplyGroup;
		private System.Windows.Forms.Label label9;
		private System.ComponentModel.IContainer components;

		/// <summary>
		/// Creates a new RegExDesignerDialog.
		/// </summary>
		public RegExDesignerDialog()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			htmlSyntaxFile = Application.StartupPath + @"\XML.syn";
			language = Compona.SourceCode.Language.FromSyntaxFile(htmlSyntaxFile);
			syntaxDocument2.Parser.Init(language);
			
			getElements = @"(?<element>((?<header><(?i:{0})[^>]*?((?<name>(\w+))=(""|'|)(?<value>.*?)(""|')*?)*?)(/>|>(?<source>[\w|\t|\r|\W]*?)</(?i:{0})>)))";
			getAttributes = @"(?<name>(\w+))=(""|')(?<value>.*?)(""|')";

			specialTags.Add("meta", "(?i:<{0}).+>");
			specialTags.Add("br", "(?i:<{0}).+>");
			specialTags.Add("hr", "(?i:<{0}).+>");
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				this.syntaxDocument2.Dispose();
				txtEditor.Dispose();

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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(RegExDesignerDialog));
			this.grpHTTPCommands = new System.Windows.Forms.GroupBox();
			this.label9 = new System.Windows.Forms.Label();
			this.chkApplyGroup = new System.Windows.Forms.CheckBox();
			this.numGroupMatchTo = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.numGroupMatchFrom = new System.Windows.Forms.NumericUpDown();
			this.numMatchTo = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.numMatchFrom = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.txtGroup = new System.Windows.Forms.TextBox();
			this.rbCustom = new System.Windows.Forms.RadioButton();
			this.btnReset = new System.Windows.Forms.Button();
			this.rbHtmlAttribute = new System.Windows.Forms.RadioButton();
			this.rbHtmlElement = new System.Windows.Forms.RadioButton();
			this.label3 = new System.Windows.Forms.Label();
			this.txtTag = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtRegEx = new System.Windows.Forms.TextBox();
			this.btnSet = new System.Windows.Forms.Button();
			this.cmdFilter = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtEditor = new Compona.Windows.Forms.SyntaxBoxControl();
			this.syntaxDocument2 = new Compona.SourceCode.SyntaxDocument(this.components);
			this.mainMenu1 = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.grpHTTPCommands.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numGroupMatchTo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numGroupMatchFrom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numMatchTo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.numMatchFrom)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// grpHTTPCommands
			// 
			this.grpHTTPCommands.Controls.Add(this.label9);
			this.grpHTTPCommands.Controls.Add(this.chkApplyGroup);
			this.grpHTTPCommands.Controls.Add(this.numGroupMatchTo);
			this.grpHTTPCommands.Controls.Add(this.label8);
			this.grpHTTPCommands.Controls.Add(this.numGroupMatchFrom);
			this.grpHTTPCommands.Controls.Add(this.numMatchTo);
			this.grpHTTPCommands.Controls.Add(this.label4);
			this.grpHTTPCommands.Controls.Add(this.numMatchFrom);
			this.grpHTTPCommands.Controls.Add(this.label7);
			this.grpHTTPCommands.Controls.Add(this.label6);
			this.grpHTTPCommands.Controls.Add(this.label5);
			this.grpHTTPCommands.Controls.Add(this.txtGroup);
			this.grpHTTPCommands.Controls.Add(this.rbCustom);
			this.grpHTTPCommands.Controls.Add(this.btnReset);
			this.grpHTTPCommands.Controls.Add(this.rbHtmlAttribute);
			this.grpHTTPCommands.Controls.Add(this.rbHtmlElement);
			this.grpHTTPCommands.Controls.Add(this.label3);
			this.grpHTTPCommands.Controls.Add(this.txtTag);
			this.grpHTTPCommands.Controls.Add(this.label2);
			this.grpHTTPCommands.Controls.Add(this.txtRegEx);
			this.grpHTTPCommands.Controls.Add(this.btnSet);
			this.grpHTTPCommands.Controls.Add(this.cmdFilter);
			this.grpHTTPCommands.Controls.Add(this.label1);
			this.grpHTTPCommands.Dock = System.Windows.Forms.DockStyle.Top;
			this.grpHTTPCommands.Location = new System.Drawing.Point(0, 0);
			this.grpHTTPCommands.Name = "grpHTTPCommands";
			this.grpHTTPCommands.Size = new System.Drawing.Size(692, 234);
			this.grpHTTPCommands.TabIndex = 10;
			this.grpHTTPCommands.TabStop = false;
			this.grpHTTPCommands.Text = "Regular Expression Filter";
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(18, 180);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(100, 12);
			this.label9.TabIndex = 34;
			this.label9.Text = "From Match Index";
			// 
			// chkApplyGroup
			// 
			this.chkApplyGroup.Location = new System.Drawing.Point(282, 204);
			this.chkApplyGroup.Name = "chkApplyGroup";
			this.chkApplyGroup.Size = new System.Drawing.Size(114, 24);
			this.chkApplyGroup.TabIndex = 33;
			this.chkApplyGroup.Text = "Apply Group from";
			// 
			// numGroupMatchTo
			// 
			this.numGroupMatchTo.Location = new System.Drawing.Point(468, 204);
			this.numGroupMatchTo.Maximum = new System.Decimal(new int[] {
																			1000,
																			0,
																			0,
																			0});
			this.numGroupMatchTo.Minimum = new System.Decimal(new int[] {
																			1,
																			0,
																			0,
																			-2147483648});
			this.numGroupMatchTo.Name = "numGroupMatchTo";
			this.numGroupMatchTo.Size = new System.Drawing.Size(42, 20);
			this.numGroupMatchTo.TabIndex = 32;
			this.numGroupMatchTo.Value = new System.Decimal(new int[] {
																		  1,
																		  0,
																		  0,
																		  -2147483648});
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(444, 208);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(18, 12);
			this.label8.TabIndex = 31;
			this.label8.Text = "to";
			// 
			// numGroupMatchFrom
			// 
			this.numGroupMatchFrom.Location = new System.Drawing.Point(396, 204);
			this.numGroupMatchFrom.Maximum = new System.Decimal(new int[] {
																			  1000,
																			  0,
																			  0,
																			  0});
			this.numGroupMatchFrom.Minimum = new System.Decimal(new int[] {
																			  1,
																			  0,
																			  0,
																			  -2147483648});
			this.numGroupMatchFrom.Name = "numGroupMatchFrom";
			this.numGroupMatchFrom.Size = new System.Drawing.Size(42, 20);
			this.numGroupMatchFrom.TabIndex = 30;
			this.numGroupMatchFrom.Value = new System.Decimal(new int[] {
																			1,
																			0,
																			0,
																			-2147483648});
			// 
			// numMatchTo
			// 
			this.numMatchTo.Location = new System.Drawing.Point(198, 175);
			this.numMatchTo.Maximum = new System.Decimal(new int[] {
																	   1000,
																	   0,
																	   0,
																	   0});
			this.numMatchTo.Minimum = new System.Decimal(new int[] {
																	   1,
																	   0,
																	   0,
																	   -2147483648});
			this.numMatchTo.Name = "numMatchTo";
			this.numMatchTo.Size = new System.Drawing.Size(42, 20);
			this.numMatchTo.TabIndex = 28;
			this.numMatchTo.Value = new System.Decimal(new int[] {
																	 1,
																	 0,
																	 0,
																	 -2147483648});
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(174, 180);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(18, 12);
			this.label4.TabIndex = 27;
			this.label4.Text = "to";
			// 
			// numMatchFrom
			// 
			this.numMatchFrom.Location = new System.Drawing.Point(126, 175);
			this.numMatchFrom.Maximum = new System.Decimal(new int[] {
																		 1000,
																		 0,
																		 0,
																		 0});
			this.numMatchFrom.Minimum = new System.Decimal(new int[] {
																		 1,
																		 0,
																		 0,
																		 -2147483648});
			this.numMatchFrom.Name = "numMatchFrom";
			this.numMatchFrom.Size = new System.Drawing.Size(42, 20);
			this.numMatchFrom.TabIndex = 26;
			this.numMatchFrom.Value = new System.Decimal(new int[] {
																	   1,
																	   0,
																	   0,
																	   -2147483648});
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(276, 150);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 18);
			this.label7.TabIndex = 23;
			this.label7.Text = "Group Options";
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(6, 150);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 18);
			this.label6.TabIndex = 22;
			this.label6.Text = "Match Options";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(282, 177);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 18);
			this.label5.TabIndex = 21;
			this.label5.Text = "Use Group";
			// 
			// txtGroup
			// 
			this.txtGroup.Location = new System.Drawing.Point(360, 176);
			this.txtGroup.Name = "txtGroup";
			this.txtGroup.Size = new System.Drawing.Size(150, 20);
			this.txtGroup.TabIndex = 20;
			this.txtGroup.Text = "";
			// 
			// rbCustom
			// 
			this.rbCustom.Location = new System.Drawing.Point(330, 45);
			this.rbCustom.Name = "rbCustom";
			this.rbCustom.Size = new System.Drawing.Size(66, 24);
			this.rbCustom.TabIndex = 18;
			this.rbCustom.Text = "Custom";
			this.rbCustom.CheckedChanged += new System.EventHandler(this.rbCustom_CheckedChanged);
			// 
			// btnReset
			// 
			this.btnReset.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnReset.Location = new System.Drawing.Point(402, 78);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(66, 23);
			this.btnReset.TabIndex = 16;
			this.btnReset.Text = "Reset";
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// rbHtmlAttribute
			// 
			this.rbHtmlAttribute.Location = new System.Drawing.Point(216, 45);
			this.rbHtmlAttribute.Name = "rbHtmlAttribute";
			this.rbHtmlAttribute.Size = new System.Drawing.Size(108, 24);
			this.rbHtmlAttribute.TabIndex = 13;
			this.rbHtmlAttribute.Text = "HTML Attributes";
			this.rbHtmlAttribute.CheckedChanged += new System.EventHandler(this.rbHtmlAttribute_CheckedChanged);
			// 
			// rbHtmlElement
			// 
			this.rbHtmlElement.Checked = true;
			this.rbHtmlElement.Location = new System.Drawing.Point(102, 45);
			this.rbHtmlElement.Name = "rbHtmlElement";
			this.rbHtmlElement.Size = new System.Drawing.Size(108, 24);
			this.rbHtmlElement.TabIndex = 12;
			this.rbHtmlElement.TabStop = true;
			this.rbHtmlElement.Text = "HTML Elements";
			this.rbHtmlElement.CheckedChanged += new System.EventHandler(this.rbHtmlElement_CheckedChanged);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(6, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(84, 18);
			this.label3.TabIndex = 11;
			this.label3.Text = "Use Expression";
			// 
			// txtTag
			// 
			this.txtTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtTag.Location = new System.Drawing.Point(54, 20);
			this.txtTag.Name = "txtTag";
			this.txtTag.Size = new System.Drawing.Size(342, 20);
			this.txtTag.TabIndex = 10;
			this.txtTag.Text = "table";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(6, 21);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 18);
			this.label2.TabIndex = 9;
			this.label2.Text = "Tag";
			// 
			// txtRegEx
			// 
			this.txtRegEx.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtRegEx.Location = new System.Drawing.Point(54, 78);
			this.txtRegEx.Multiline = true;
			this.txtRegEx.Name = "txtRegEx";
			this.txtRegEx.Size = new System.Drawing.Size(342, 60);
			this.txtRegEx.TabIndex = 8;
			this.txtRegEx.Text = "";
			// 
			// btnSet
			// 
			this.btnSet.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.btnSet.Location = new System.Drawing.Point(582, 18);
			this.btnSet.Name = "btnSet";
			this.btnSet.Size = new System.Drawing.Size(66, 24);
			this.btnSet.TabIndex = 7;
			this.btnSet.Text = "&Set";
			this.btnSet.Visible = false;
			this.btnSet.Click += new System.EventHandler(this.btnSet_Click);
			// 
			// cmdFilter
			// 
			this.cmdFilter.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.cmdFilter.Location = new System.Drawing.Point(402, 18);
			this.cmdFilter.Name = "cmdFilter";
			this.cmdFilter.Size = new System.Drawing.Size(66, 24);
			this.cmdFilter.TabIndex = 5;
			this.cmdFilter.Text = "&Run";
			this.cmdFilter.Click += new System.EventHandler(this.cmdFilter_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 78);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Query";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtEditor);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox1.Location = new System.Drawing.Point(0, 234);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(692, 188);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Text";
			// 
			// txtEditor
			// 
			this.txtEditor.ActiveView = Compona.Windows.Forms.SyntaxBox.ActiveView.BottomRight;
			this.txtEditor.AutoListPosition = null;
			this.txtEditor.AutoListSelectedText = "a123";
			this.txtEditor.AutoListVisible = false;
			this.txtEditor.CopyAsRTF = false;
			this.txtEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtEditor.FontName = "Courier new";
			this.txtEditor.Indent = Compona.Windows.Forms.SyntaxBox.IndentStyle.Smart;
			this.txtEditor.InfoTipCount = 1;
			this.txtEditor.InfoTipPosition = null;
			this.txtEditor.InfoTipSelectedIndex = 1;
			this.txtEditor.InfoTipVisible = false;
			this.txtEditor.Location = new System.Drawing.Point(3, 16);
			this.txtEditor.LockCursorUpdate = false;
			this.txtEditor.Name = "txtEditor";
			this.txtEditor.ShowTabGuides = true;
			this.txtEditor.Size = new System.Drawing.Size(686, 169);
			this.txtEditor.SmoothScroll = false;
			this.txtEditor.SplitviewH = -4;
			this.txtEditor.SplitviewV = -4;
			this.txtEditor.TabGuideColor = System.Drawing.Color.FromArgb(((System.Byte)(228)), ((System.Byte)(231)), ((System.Byte)(235)));
			this.txtEditor.TabIndex = 12;
			this.txtEditor.WhitespaceColor = System.Drawing.SystemColors.ControlDark;
			// 
			// syntaxDocument2
			// 
			this.syntaxDocument2.Lines = new string[] {
														  ""};
			this.syntaxDocument2.MaxUndoBufferSize = 1000;
			this.syntaxDocument2.Modified = false;
			this.syntaxDocument2.UndoStep = 0;
			// 
			// mainMenu1
			// 
			this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem1});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2});
			this.menuItem1.Text = "&File";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "&Close";
			this.menuItem2.Click += new System.EventHandler(this.menuItem2_Click);
			// 
			// RegExDesignerDialog
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(692, 422);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.grpHTTPCommands);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Menu = this.mainMenu1;
			this.Name = "RegExDesignerDialog";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Regular Expression Designer Dialog";
			this.grpHTTPCommands.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.numGroupMatchTo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numGroupMatchFrom)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numMatchTo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.numMatchFrom)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Query Methods
		private bool IsSpecialTag(string tag)
		{
			if ( specialTags.Contains(tag.ToLower(System.Globalization.CultureInfo.InvariantCulture)) )
			{
				return true;
			} 
			else 
			{
				return false;
			}
		}

		private string GetRegExForTag(string tag)
		{
			string regex = (string)specialTags[tag.ToLower(System.Globalization.CultureInfo.InvariantCulture)];

			return regex.Replace("{0}", tag);
		}

		/// <summary>
		/// Executes the regular expression query.
		/// </summary>
		/// <param name="html"> The html text content.</param>
		/// <param name="matchIndices"> The match indices to select.</param>
		/// <param name="groupMatchIndices"> The group match indices to select.</param>
		/// <param name="applyGroupMatch"> Value for applying the group match selection.</param>
		/// <param name="group"> The selected group name.</param>
		private void ExecuteRegExFilter(string html, int[] matchIndices, int[] groupMatchIndices, bool applyGroupMatch, string group)
		{
			Application.DoEvents();

			string query = string.Empty;
			StringBuilder buffer = new StringBuilder();

			// Add tag
			if ( this.txtTag.Text.Length > 0 )
			{				
				if ( IsSpecialTag(txtTag.Text) )
				{
					string getSpecialTag = GetRegExForTag(txtTag.Text);
					buffer.AppendFormat(getSpecialTag,txtTag.Text);
					query = buffer.ToString();
				} 
				else 
				{
					buffer.AppendFormat(getElements,txtTag.Text);
					query = buffer.ToString();
				}

				txtRegEx.Text = query;
			} 
			else
			{
				query = txtRegEx.Text;
			}			

			try
			{
				EditorText = GetMatches(html,query, matchIndices,groupMatchIndices, applyGroupMatch, group);
			}
			catch (Exception ex)
			{
				EditorText = ex.ToString();
			}
		}

		/// <summary>
		/// Gets the regex matches.
		/// </summary>
		/// <param name="buffer"> The string builder buffer.</param>
		/// <param name="matches"> The MatchesCollection type.</param>
		/// <param name="matchFrom"></param>
		/// <param name="matchTo"></param>
		/// <param name="useGroup"></param>
		/// <param name="groupName"></param>
		/// <param name="groupCaptureFrom"></param>
		/// <param name="groupCaptureTo"></param>
		/// <param name="withLabels"> If we display labels for matches and captures.</param>
		/// <returns></returns>
		private void GetMatches(StringBuilder buffer, MatchCollection matches, int matchFrom, int matchTo, bool useGroup, string groupName, int groupCaptureFrom, int groupCaptureTo, bool withLabels)
		{
			// select from and to
			for( int i=matchFrom;i<matchTo;i++ )
			{
				string element = matches[i].Value;
				if ( withLabels )
				{
					buffer.AppendFormat("Match {0}\r\n", i);
				}
				buffer.Append(element);
				buffer.Append("\r\n\r\n");				

				if ( useGroup )
				{
					CaptureCollection captureCollection = matches[i].Groups[groupName].Captures;

					if ( groupCaptureFrom == -1 && groupCaptureTo == -1 )
					{
						groupCaptureFrom = 0;
						groupCaptureTo = captureCollection.Count;
					}
					
					for( int j=groupCaptureFrom;j<groupCaptureTo;j++ )
					{
						Capture capture = captureCollection[j];
						if ( withLabels )
						{
							buffer.AppendFormat("Capture {0}\r\n", j);
						}
						buffer.Append(capture.Value);
						buffer.Append("\r\n\r\n");
					}
				}
			}
		}

		/// <summary>
		/// Gets the regex matches.
		/// </summary>
		/// <param name="text"> The html text content.</param>
		/// <param name="matchIndices"> The match indices to select.</param>
		/// <param name="groupMatchIndices"> The group match indices to select.</param>
		/// <param name="applyGroupMatch"> Value for applying the group match selection.</param>
		/// <param name="group"> The selected group name.</param>
		/// <returns> Returns the matches for regex query.</returns>
		public string GetMatches(string text, string query, int[] matchIndices,int[] groupMatchIndices,bool applyGroupMatch, string group)
		{
			//string elementValue = string.Empty;
			Regex getElements = new Regex(query, RegexOptions.None);

			StringBuilder buffer = new StringBuilder();

			if ( getElements.IsMatch(text) )
			{
				// Get elements matches
				MatchCollection matches = getElements.Matches(text);
			
				int action = -1;
				// Case 1: Match Indices are -1, no use group, no apply group by match selection.
				if ( matchIndices[0] == -1 && matchIndices[1] == -1 )
				{
					action = 1;
				} 
				else 
				{
					// Case 2 and 3: Match Indices are not -1, no use group.
					// Case 4: Match Indices are not -1, use group, no group by match selection.
					// Case 5: Match Indices are not -1, use group, use apply group by selection.				
					if ( matchIndices[0] != -1 || matchIndices[1] != -1 )
					{
						if ( matchIndices[1] == -1 )
						{
							// select one match
							action = 2;
						} 
						else 
						{
							// select from and to match.
							action = 3;
						}					

						if ( group.Length > 0 )
						{
							if ( applyGroupMatch )
							{
								action = 5;
							} 
							else 
							{
								action = 4;
							}
						}
					}		
				}				
				

				int from;
				int to;

				switch ( action )
				{
					case 1:
						GetMatches(buffer,matches, 0, matches.Count, false, string.Empty, 0, 0, true);
						break;
					case 2:
						// select one match
						buffer.AppendFormat("Selected Match {0}\r\n", matchIndices[0]);
						buffer.Append(matches[matchIndices[0]].Value);
						break;
					case 3:
						from = matchIndices[0];
						to = matchIndices[1];

						GetMatches(buffer,matches, from, to, false, string.Empty, 0, 0, true);
						break;
					case 4:
						from = matchIndices[0];
						to = matchIndices[1];

						GetMatches(buffer,matches, from, to, true, group, -1, -1, true);
						break;
					case 5:
						from = matchIndices[0];
						to = matchIndices[1];

						int fromGroupCapture = groupMatchIndices[0];
						int toGroupCapture = groupMatchIndices[1];

						GetMatches(buffer, matches,from, to, true, group, fromGroupCapture, toGroupCapture, true);
						break;
				}
			} 
			else 
			{
				buffer.Append("No matches found.");
			}
			return buffer.ToString();
		}


		private void btnSet_Click(object sender, System.EventArgs e)
		{
			//this.RegExQuery = this.txtRegEx.Text;
			//this.DialogResult = DialogResult.OK;
		}

		private void cmdFilter_Click(object sender, System.EventArgs e)
		{
			int[] matchIndices = new int[] {(int)this.numMatchFrom.Value,
											   (int)this.numMatchTo.Value};
			int[] groupIndices = new int[] {(int)this.numGroupMatchFrom.Value,
											   (int)this.numGroupMatchTo.Value};
			bool applyGroupMatch = this.chkApplyGroup.Checked;

			string group = this.txtGroup.Text;

			if ( EditorText != null )
			{
				if ( EditorText.Length > 0 )
				{
					ExecuteRegExFilter(EditorText, matchIndices, groupIndices, applyGroupMatch, group);
				} 
				else 
				{
					ExecuteRegExFilter(TextContent, matchIndices, groupIndices, applyGroupMatch, group);
				}
			}			
		}


		#endregion

		private void btnReset_Click(object sender, System.EventArgs e)
		{
			EditorText = string.Empty;
		}

		private void rbHtmlElement_CheckedChanged(object sender, System.EventArgs e)
		{
			this.txtTag.Enabled = true;
			this.txtRegEx.Text = string.Empty;
		}

		private void rbHtmlAttribute_CheckedChanged(object sender, System.EventArgs e)
		{
			this.txtTag.Enabled = false;
			this.txtTag.Text = string.Empty;
			this.txtRegEx.Text = getAttributes;
		}

		private void rbCustom_CheckedChanged(object sender, System.EventArgs e)
		{
			this.txtTag.Enabled = false;
			this.txtRegEx.Text = string.Empty;
			this.txtTag.Text = string.Empty;
		}

		private void menuItem2_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		#region Editor Text Properties and Methods

		/// <summary>
		/// Gets or sets the Text Editor Text.
		/// </summary>
		internal string EditorText
		{
			get
			{
				return this.txtEditor.Document.Text;
			}
			set
			{
				//syntaxDocument2.clear();

				// save value in temp
				syntaxDocument2.Text = value;			
				txtEditor.Document = syntaxDocument2;
			}
		}


		#endregion

		/// <summary>
		/// Gets or sets the regex query.
		/// </summary>
		public string RegExQuery
		{
			get
			{
				return _query;
			}
			set
			{
				_query = value;
			}
		}

		/// <summary>
		/// Gets or sets the text string.
		/// </summary>
		public string TextContent
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
	}
}
