// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: Abril 2005
using System;
using System.Text;
using System.Reflection;
using System.Collections;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Protocols.Http.Scripting;
using Ecyware.GreenBlue.Protocols.Http.Transforms;
using Ecyware.GreenBlue.Protocols.Http.Transforms.Designers;
using System.Web.Mail;

namespace Ecyware.GreenBlue.Protocols.Http.Transforms
{
	/// <summary>
	/// Contains logic for a GmailSmtpTransform.
	/// </summary>	
	[Serializable]
	[WebTransformAttribute("GMail SMTP Transform", "output","Sends an email using the smtp server from GMail.")]
	[UITransformEditor(typeof(GMailSmtpTransformDesigner))]	
	public class GmailSmtpTransform : WebTransform
	{
		string _from;
		string _to;
		string _cc;
		string _bcc;
		string _subject;
		System.Web.Mail.MailFormat _mailFormat;
		string _username;
		string _password;
		private ArrayList _commands = new ArrayList(10);
		
		
		/// <summary>
		/// Creates a new GmailSmtpTransform.
		/// </summary>
		public GmailSmtpTransform()
		{
			Name = "GMail SMTP Transform";
		}

		/// <summary>
		/// Gets or sets the subject.
		/// </summary>
		public DefaultTransformValue Subject
		{
			get
			{
				return _subject;
			}
			set
			{
				_subject = value;
			}
		}

		/// <summary>
		/// Gets or sets the the sender's email.
		/// </summary>
		public string From
		{
			get
			{
				return _from;
			}
			set
			{
				_from = value;
			}
		}

		/// <summary>
		/// Gets or sets the email of the recipient.
		/// </summary>
		public string To
		{
			get
			{
				return _to;
			}
			set
			{
				_to = value;
			}
		}

		/// <summary>
		/// Gets or sets the recipients to copy.
		/// </summary>
		public string Cc
		{
			get
			{
				return _cc;
			}
			set
			{
				_cc = value;
			}
		}

		/// <summary>
		/// Gets or sets the blind copy.
		/// </summary>
		public string Bcc
		{
			get
			{
				return _bcc;
			}
			set
			{
				_bcc = value;
			}
		}

		/// <summary>
		/// Gets or sets the mail format.
		/// </summary>
		public System.Web.Mail.MailFormat Format
		{
			get
			{
				return _mailFormat;
			}
			set
			{
				_mailFormat = value;
			}
		}

		/// <summary>
		/// Gets or sets username.
		/// </summary>
		public string Username
		{
			get
			{
				return _username;
			}
			set
			{
				_username = value;
			}
		}

		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				_password = value;
			}
		}

		/// <summary>
		/// Gets or sets the query command actions.
		/// </summary>
		public QueryCommandAction[] QueryCommandActions
		{
			get
			{
				return (QueryCommandAction[])_commands.ToArray(typeof(QueryCommandAction));
			}
			set
			{
				if ( value != null )
					_commands.AddRange(value);
			}
		}

		/// <summary>
		/// Adds a query command action.
		/// </summary>
		/// <param name="action"> The QueryCommandAction type.</param>
		public void AddQueryCommandAction(QueryCommandAction action)
		{
			_commands.Add(action);
		}

		/// <summary>
		/// Removes the query command value.
		/// </summary>
		/// <param name="index"> The index.</param>
		public void RemoveQueryCommandAction(int index)
		{
			_commands.RemoveAt(index);
		}

		/// <summary>
		/// Removes all query command actions.
		/// </summary>
		public void RemoveAllQueryCommandActions()
		{
			_commands.Clear();
		}

		/// <summary>
		/// Gets a query command action.
		/// </summary>
		/// <param name="index"> The index.</param>
		/// <returns> A QueryCommandAction type.</returns>
		public QueryCommandAction GetQueryCommandAction(int index)
		{
			return (QueryCommandAction)_commands[index];
		}

		/// <summary>
		/// Gets the arguments.
		/// </summary>
		/// <returns> An Argument type array.</returns>
		public override Argument[] GetArguments()
		{
			ArrayList arguments = new ArrayList();

			foreach ( FormField formField in this.FormFields )
			{
				if ( formField.TransformValue is DefaultTransformValue )
				{
					if ( ((DefaultTransformValue)formField.TransformValue).EnabledInputArgument )
					{
						Argument arg = new Argument();
						arg.Name = formField.FieldName;
						arguments.Add(arg);
					}
				}
			}

			if ( arguments.Count == 0 )
			{
				return null;
			} 
			else 
			{
				return (Argument[])arguments.ToArray(typeof(Argument));
			}
		}

		public override void ApplyTransform(WebRequest request, WebResponse response)
		{
			// base.ApplyTransform (request, response);
			string result = response.HttpBody;

			try
			{
				// Apply transform value and append each value.
				foreach ( QueryCommandAction action in this.QueryCommandActions )
				{
					if (( action.Value is XPathQueryCommand ) || ( action.Value is RegExQueryCommand ))
					{
						result = action.ApplyQueryCommandAction(result);
					}
				}

				SendMail(result);
			}
			catch ( Exception ex )
			{
				System.Windows.Forms.MessageBox.Show(ex.ToString());
			}
		}

		public void SendMail(string body) 
		{
			// Mail initialization 
			MailMessage mail = new MailMessage();
			mail.From = _from;
			mail.To = _to;
			mail.Cc = _cc;
			mail.Bcc = _bcc;
			mail.Subject = _subject;
			mail.BodyFormat = _mailFormat;
			mail.Body = body;

			// Smtp configuration
			SmtpMail.SmtpServer = "smtp.gmail.com";

			// - smtp.gmail.com use smtp authentication
			mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
			mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", _username);
			mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", _password);
			
			// - smtp.gmail.com use port 465 or 587
			mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "465");
			
			// - smtp.gmail.com use STARTTLS (some clients call this SSL)
			mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true");
			
			// Mail sending
			try 
			{
				SmtpMail.Send(mail);
				// return "";
			} 
			catch
			{
				// nothing
			} 

		}

	}
}
