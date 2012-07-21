using System;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms;
using Ecyware.GreenBlue.Engine.Transforms.Designers;
using System.Text;
using System.Text.RegularExpressions;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for WebTestTransform.
	/// </summary>
	[Serializable]
	[WebTransform("Web Test Transform", "output", "Verifies a web page for any error or unexpected behaviour.")]
	[UITransformEditor(typeof(QueryTransformDesigner))]
	public class WebTestTransform : QueryTransform
	{
		private bool _matchType = true;

		/// <summary>
		/// Creates a new WebTestTransform
		/// </summary>
		public WebTestTransform()
		{
		}

		/// <summary>
		/// Gets or sets if the transforms validates for positive or negative matches. Default is positive (the regex query has matches).
		/// </summary>
		public bool CheckForPositivesMatches
		{
			get
			{
				return _matchType;
			}
			set
			{
				_matchType = value;
			}

		}

		/// <summary>
		/// Check the matches for a specific HTML.
		/// </summary>
		/// <param name="httpBody"> The HTTP Body.</param>
		/// <returns> Returns true if matches found, else false.</returns>
		private bool CheckMatches(string httpBody, string regexQuery)
		{
			Regex regex = new Regex(regexQuery, RegexOptions.IgnoreCase);
			MatchCollection matches = regex.Matches(httpBody);

			bool found = false;
			if ( matches.Count > 0 )
			{
				found = true;
			}

			return found;
		}

		public override Argument[] GetArguments()
		{
			if ( Transport.GetArguments() != null )
			{
				return Transport.GetArguments();
			} 
			else 
			{
				return null;
			}
		}

		public override void ApplyTransform(WebRequest request, WebResponse response)
		{
			//base.ApplyTransform(request, response);
			string httpBody = response.HttpBody;

			QueryCommandAction[] queryActions = this.QueryCommandActions;

			bool sumMatches = true;

			for (int i = 0; i < queryActions.Length; i++)
			{
				QueryCommandAction action = queryActions[i];

				if ( (action.Value is RegExQueryCommand) )
				{
					string regex = ((RegExQueryCommand)action.Value).Expression;
					
					// Apply regular expression
					sumMatches &= CheckMatches(httpBody, regex);
				}
			}

			// if matches, send message
			if ( sumMatches == _matchType )
			{
				if (this.Transport != null)
				{
					this.Transport.Send(new string[] {httpBody});
				}
			}
		}
	}
}
