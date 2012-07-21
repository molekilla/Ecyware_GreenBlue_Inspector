using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for RegExQueryCommand.
	/// </summary>
	[Serializable]
	public class RegExQueryCommand : TransformValue
	{
		string _prefix;
		string _postfix;
		string _expression;
		int _selectMatchFrom = -1;
		int _selectMatchTo = -1;
		string _group;
		bool _applyGroup = false;
		int _groupCaptureIndexFrom;
		int _groupCaptureIndexTo;


		/// <summary>
		/// Creates a new RegExQueryCommand.
		/// </summary>
		public RegExQueryCommand()
		{
		}

		#region Properties
		/// <summary>
		/// Gets or sets the match from index.
		/// </summary>
		public int MatchFromIndex
		{
			get
			{
				return _selectMatchFrom;
			}
			set
			{
				_selectMatchFrom = value;
			}
		}
		/// <summary>
		/// Gets or sets the match to index.
		/// </summary>
		public int MatchToIndex
		{
			get
			{
				return _selectMatchTo;
			}
			set
			{
				_selectMatchTo = value;
			}
		}
		/// <summary>
		/// Gets or sets the group catpure from index.
		/// </summary>
		public int GroupCaptureFromIndex
		{
			get
			{
				return _groupCaptureIndexFrom;
			}
			set
			{
				_groupCaptureIndexFrom = value;
			}
		}
		/// <summary>
		/// Gets or sets the group capture to index.
		/// </summary>
		public int GroupCaptureToIndex
		{
			get
			{
				return _groupCaptureIndexTo;
			}
			set
			{
				_groupCaptureIndexTo = value;
			}
		}
		/// <summary>
		/// Gets or sets the if group capture is applied.
		/// </summary>
		public bool ApplyGroupCapture
		{
			get
			{
				return _applyGroup;
			}
			set
			{
				_applyGroup = value;
			}
		}
		/// <summary>
		/// Gets or sets the group name.
		/// </summary>
		public string GroupName
		{
			get
			{
				return _group;
			}
			set
			{
				_group = value;
			}
		}
		/// <summary>
		/// Gets or sets the expression.
		/// </summary>
		public string Expression
		{
			get
			{
				return _expression;
			}
			set
			{
				_expression = value;
			}
		}
		/// <summary>
		/// Gets or sets the prefix.
		/// </summary>
		public string Prefix
		{
			get
			{
				return _prefix;
			}
			set
			{
				_prefix = value;
			}
		}

		/// <summary>
		/// Gets or sets the postfix.
		/// </summary>
		public string Postfix
		{
			get
			{
				return _postfix;
			}
			set
			{
				_postfix = value;
			}
		}

		#endregion

		/// <summary>
		/// Gets the regex matches.
		/// </summary>
		/// <param name="text"> The html text content.</param>
		/// <param name="matchIndices"> The match indices to select.</param>
		/// <param name="groupMatchIndices"> The group match indices to select.</param>
		/// <param name="applyGroupMatch"> Value for applying the group match selection.</param>
		/// <param name="group"> The selected group name.</param>
		/// <returns> Returns the matches for regex query.</returns>
		protected string GetMatches(string text, string query, int[] matchIndices,int[] groupMatchIndices,bool applyGroupMatch, string group)
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
						GetMatches(buffer,matches, 0, matches.Count, false, string.Empty, 0, 0, false);
						break;
					case 2:
						// select one match
						//buffer.AppendFormat("Selected Match {0}\r\n", matchIndices[0]);
						buffer.Append(matches[matchIndices[0]].Value);
						break;
					case 3:
						from = matchIndices[0];
						to = matchIndices[1];

						GetMatches(buffer,matches, from, to, false, string.Empty, 0, 0, false);
						break;
					case 4:
						from = matchIndices[0];
						to = matchIndices[1];

						GetMatches(buffer,matches, from, to, true, group, -1, -1, false);
						break;
					case 5:
						from = matchIndices[0];
						to = matchIndices[1];

						int fromGroupCapture = groupMatchIndices[0];
						int toGroupCapture = groupMatchIndices[1];

						GetMatches(buffer, matches,from, to, true, group, fromGroupCapture, toGroupCapture, false);
						break;
				}
			} 
			else 
			{
				buffer.Append("No matches found.");
			}

			return buffer.ToString();
		}

		/// <summary>
		/// Gets the regex matches.
		/// </summary>
		/// <param name="buffer"> The string builder buffer.</param>
		/// <param name="matches"> The MatchesCollection type.</param>
		/// <param name="matchFrom"> The match from index.</param>
		/// <param name="matchTo"> The match to index.</param>
		/// <param name="useGroup"> If for each match, a selected group is display.</param>
		/// <param name="groupName"> The group name.</param>
		/// <param name="groupCaptureFrom"> The capture from index.</param>
		/// <param name="groupCaptureTo"> The capture to index.</param>
		/// <param name="withLabels"> If we display labels for matches and captures.</param>
		protected void GetMatches(StringBuilder buffer, MatchCollection matches, int matchFrom, int matchTo, bool useGroup, string groupName, int groupCaptureFrom, int groupCaptureTo, bool withLabels)
		{
			// select from and to
			for( int i=matchFrom;i<matchTo;i++ )
			{
				string element = matches[i].Value;
				if ( withLabels )
				{
					buffer.AppendFormat("\r\n\r\nMatch {0}\r\n", i);
				}
				buffer.Append(element);	

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
							buffer.AppendFormat("\r\n\r\nCapture {0}\r\n", j);
						}
						buffer.Append(capture.Value);
					}
				}
			}
		}

		/// <summary>
		/// Executes the regular expression query.
		/// </summary>
		/// <param name="html"> The HTML content.</param>
		/// <returns> A string result.</returns>
		public string ExecuteQuery(string html)
		{
			if  ( Expression.Length > 0 )
			{
				int[] matchIndices = new int[] {_selectMatchFrom, _selectMatchTo};
				int[] groupMatchIndices = new int[] {_groupCaptureIndexFrom, _groupCaptureIndexTo};
				bool applyGroupMatch = _applyGroup;
				string groupName = _group;
				string result = GetMatches(html, Expression, matchIndices, groupMatchIndices, applyGroupMatch, groupName);

				if ( Prefix.Length > 0 )
				{
					result = Prefix + result;
				}

				if ( Postfix.Length > 0 )
				{
					result = result + Postfix;
				}

				return result;
			} 
			else 
			{
				return string.Empty;
			}
		}

		public override object GetValue(Ecyware.GreenBlue.Engine.Scripting.WebResponse response)
		{
			string result = ExecuteQuery(response.HttpBody);

			if ( Prefix.Length > 0 )
			{
				result = Prefix + result;
			}

			if ( Postfix.Length > 0 )
			{
				result = result + Postfix;
			}

			return result;			
		}

	}
}
