// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: Abril 2005
using System;
using System.Text;
using System.Reflection;
using System.Collections;
using Ecyware.GreenBlue.Configuration;
using Ecyware.GreenBlue.Engine.Scripting;
using Ecyware.GreenBlue.Engine.Transforms;
using System.Web.Mail;
using Ecyware.GreenBlue.Engine.Transforms.Designers;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Contains logic for a GmailSmtpTransform.
	/// </summary>	
	[Serializable]
	[WebTransformAttribute("GMail SMTP Transform", "output","Sends an email using the smtp server from GMail.")]
	[UITransformEditor(typeof(GMailSmtpTransformDesigner))]	
	public class GmailSmtpTransform : WebTransform
	{
		Transport _transport;
		private ArrayList _commands = new ArrayList(10);
		
		
		/// <summary>
		/// Creates a new GmailSmtpTransform.
		/// </summary>
		public GmailSmtpTransform()
		{
			Name = "GMail SMTP Transform";
		}

		/// <summary>
		/// Gets or sets the transport
		/// </summary>
		public Transport Transport
		{
			get
			{
				return _transport;
			}
			set
			{
				_transport = value;
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

				if ( Transport != null )
				{
					// Send message to transport.
					Transport.Send(new string[] {result});
				}

			}
			catch ( Exception ex )
			{
				throw ex;
			}
		}

//		public void SendMail(string body) 
//		{
//			// Mail initialization 
//			MailMessage mail = new MailMessage();
//			mail.From = _from.Value;
//			mail.To = _to.Value;
//			mail.Cc = _cc.Value;
//			mail.Bcc = _bcc.Value;
//			mail.Subject = _subject.Value;
//			mail.BodyFormat = _mailFormat;
//			mail.Body = body;
//
//			// Smtp configuration
//			SmtpMail.SmtpServer = "smtp.gmail.com";
//
//			// - smtp.gmail.com use smtp authentication
//			mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
//			mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", _username.Value);
//			mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", _password.Value);
//			
//			// - smtp.gmail.com use port 465 or 587
//			mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "465");
//			
//			// - smtp.gmail.com use STARTTLS (some clients call this SSL)
//			mail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true");
//			
//			// Mail sending
//			try 
//			{
//				SmtpMail.Send(mail);
//				// return "";
//			} 
//			catch
//			{
//				// nothing
//			} 
//
//		}

	}
}
