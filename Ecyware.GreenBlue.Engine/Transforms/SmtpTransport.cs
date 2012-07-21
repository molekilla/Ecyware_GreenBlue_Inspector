using System;
using System.Web.Mail;
using Ecyware.GreenBlue.Engine.Smtp;

namespace Ecyware.GreenBlue.Engine.Transforms
{
	/// <summary>
	/// Summary description for SmtpTransport.
	/// </summary>
	[Serializable]
	public class SmtpTransport : Transport
	{
		private MailFormat _format;
		private string _server;
		private string _to;
		private string _from;
		private string _subject;

		/// <summary>
		/// Creates a SmtpTransport.
		/// </summary>
		public SmtpTransport()
		{
		}

		/// <summary>
		/// Gets or sets the server url.
		/// </summary>
		public string ServerUrl
		{
			get
			{
				return _server;
			}
			set
			{
				_server = value;
			}
		}

		/// <summary>
		/// Gets or sets the to.
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
		/// Gets or sets the from.
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
		/// Gets or sets the mail message format.
		/// </summary>
		public MailFormat MessageFormat
		{
			get
			{
				return _format;
			}
			set
			{
				_format = value;
			}
		}

		public override void Send(string[] payload)
		{
			System.Web.Mail.MailMessage message = new MailMessage();

			message.Body = Convert.ToString(payload[0]);
			message.BodyFormat = MessageFormat;
			message.Subject = this.Subject;
			message.To = this.To;
			message.From = this.From;
			
			SmtpClient.SmtpServer = this.ServerUrl;
			SmtpClient.Send(message);
		}

	}
}
