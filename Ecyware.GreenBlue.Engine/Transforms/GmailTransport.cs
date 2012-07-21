// Ecyware - Rogelio Morrell C. All rights reserved.
// Title: Ecyware GreenBlue Project
// Author: Rogelio Morrell C.
// Date: December 2005
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
	/// Summary description for GmailTransport.
	/// </summary>
	[Serializable]
	public class GmailTransport : Transport
	{
		string _from = string.Empty;
		string _to = string.Empty;
		string _cc = string.Empty;
		string _bcc = string.Empty;
		string _subject = string.Empty;
		System.Web.Mail.MailFormat _mailFormat;
		string _username = string.Empty;
		string _password = string.Empty;

		public GmailTransport()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// Gets or sets the subject.
		/// </summary>
		public string Subject
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
//
//		public override Argument[] GetArguments()
//		{
//		
//			ArrayList arguments = new ArrayList();
//
//			Argument arg = new Argument();
//			arg.Name = "GmailTransport.From";
//			arguments.Add(arg);
//
//			arg = new Argument();
//			arg.Name = "GmailTransport.To";
//			arguments.Add(arg);
//
//			arg = new Argument();
//			arg.Name = "GmailTransport.Cc";
//			arguments.Add(arg);
//
//			arg = new Argument();
//			arg.Name = "GmailTransport.Bcc";
//			arguments.Add(arg);
//			
//			arg = new Argument();
//			arg.Name = "GmailTransport.Subject";
//			arguments.Add(arg);
//
//			arg = new Argument();
//			arg.Name = "GmailTransport.Username";
//			arguments.Add(arg);
//
//			arg = new Argument();
//			arg.Name = "GmailTransport.Password";
//			arguments.Add(arg);
//
//			if ( arguments.Count == 0 )
//			{
//				return null;
//			} 
//			else 
//			{
//				return (Argument[])arguments.ToArray(typeof(Argument));
//			}
//		}


		public override void Send(string[] payload)
		{
			string body = payload[0];

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
