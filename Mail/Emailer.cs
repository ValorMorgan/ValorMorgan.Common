using System;
using System.Collections.Generic;
using System.Net.Mail;
using ValorMorgan.Common.Mail.Helpers;

namespace ValorMorgan.Common.Mail
{
    /// <summary>
    /// Provides means for emailing messages.
    /// </summary>
    public class Emailer : IEmailer
    {
        #region PROPERTIES
        /// <summary>
        /// Any attachments to be attached to the email.
        /// </summary>
        public IEnumerable<Attachment> Attachments { get; set; }

        /// <summary>
        /// The blind Carbon-Copy recipient of the email (who is included to be sent to).
        /// </summary>
        public string EmailBcc { get; set; }

        /// <summary>
        /// The Carbon-Copy recipient of the email (who is included to be sent to).
        /// </summary>
        public string EmailCc { get; set; }

        /// <summary>
        /// The sender of the email (who it's from).
        /// </summary>
        public string EmailFrom { get; set; }

        /// <summary>
        /// The recipient of the email (who it is going to).
        /// </summary>
        public string EmailTo { get; set; }

        /// <summary>
        /// The priority of the email.
        /// </summary>
        public MailPriority? Priority { get; set; }

        /// <summary>
        /// The mail server to use for sending the email.
        /// </summary>
        public string Smtp { get; set; }

        /// <summary>
        /// The subject line of the email.
        /// </summary>
        public string Subject { get; set; }
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Provides means for emailing messages.
        /// </summary>
        public Emailer()
        {
            Smtp = EmailerConstants.DEFAULT_SMTP;
            Priority = EmailerConstants.DEFAULT_MAIL_PRIORITY;
        }

        /// <summary>
        /// Provides means for emailing messages.
        /// </summary>
        /// <param name="smtp">The mail server to use for sending the email.</param>
        public Emailer(string smtp)
            : this()
        {
            Smtp = smtp;
        }

        /// <summary>
        /// Provides means for emailing messages.
        /// </summary>
        /// <param name="smtp">The mail server to use for sending the email.</param>
        /// <param name="emailFrom">The sender of the email (who it's from).</param>
        /// <param name="emailTo">The recipient of the email (who it is going to).</param>
        public Emailer(string smtp, string emailFrom, string emailTo)
            : this(smtp)
        {
            EmailFrom = emailFrom;
            EmailTo = emailTo;
        }

        /// <summary>
        /// Provides means for emailing messages.
        /// </summary>
        /// <param name="smtp">The mail server to use for sending the email.</param>
        /// <param name="emailFrom">The sender of the email (who it's from).</param>
        /// <param name="emailTo">The recipient of the email (who it is going to).</param>
        /// <param name="subject">The subject line of the email.</param>
        public Emailer(string smtp, string emailFrom, string emailTo, string subject)
            : this(smtp, emailFrom, emailTo)
        {
            Subject = subject;
        }

        /// <summary>
        /// Provides means for emailing messages.
        /// </summary>
        /// <param name="smtp">The mail server to use for sending the email.</param>
        /// <param name="emailFrom">The sender of the email (who it's from).</param>
        /// <param name="emailTo">The recipient of the email (who it is going to).</param>
        /// <param name="subject">The subject line of the email.</param>
        /// <param name="emailCc">The Carbon-Copy recipient of the email (who is included to be sent to).</param>
        /// <param name="emailBcc">The blind Carbon-Copy recipient of the email (who is included to be sent to).</param>
        public Emailer(string smtp, string emailFrom, string emailTo, string subject, string emailCc, string emailBcc)
            : this(smtp, emailFrom, emailTo, subject)
        {
            EmailCc = emailCc;
            EmailBcc = emailBcc;
        }

        /// <summary>
        /// Provides means for emailing messages.
        /// </summary>
        /// <param name="smtp">The mail server to use for sending the email.</param>
        /// <param name="emailFrom">The sender of the email (who it's from).</param>
        /// <param name="emailTo">The recipient of the email (who it is going to).</param>
        /// <param name="subject">The subject line of the email.</param>
        /// <param name="emailCc">The Carbon-Copy recipient of the email (who is included to be sent to).</param>
        /// <param name="emailBcc">The blind Carbon-Copy recipient of the email (who is included to be sent to).</param>
        /// <param name="priority">The priority of the email.</param>
        public Emailer(string smtp, string emailFrom, string emailTo, string subject, string emailCc, string emailBcc, MailPriority? priority)
            : this(smtp, emailFrom, emailTo, subject, emailCc, emailBcc)
        {
            Priority = priority;
        }

        /// <summary>
        /// Provides means for emailing messages.
        /// </summary>
        /// <param name="smtp">The mail server to use for sending the email.</param>
        /// <param name="emailFrom">The sender of the email (who it's from).</param>
        /// <param name="emailTo">The recipient of the email (who it is going to).</param>
        /// <param name="subject">The subject line of the email.</param>
        /// <param name="emailCc">The Carbon-Copy recipient of the email (who is included to be sent to).</param>
        /// <param name="emailBcc">The blind Carbon-Copy recipient of the email (who is included to be sent to).</param>
        /// <param name="priority">The priority of the email.</param>
        /// <param name="attachments">Any attachments to be attached to the email.</param>
        public Emailer(string smtp, string emailFrom, string emailTo, string subject, string emailCc, string emailBcc, MailPriority? priority, IEnumerable<Attachment> attachments)
            : this(smtp, emailFrom, emailTo, subject, emailCc, emailBcc, priority)
        {
            Attachments = attachments;
        }
        #endregion

        /// <summary>
        /// Sends an email with the provided subject and body to the provided recipient(s).
        /// </summary>
        /// <param name="body">The body of the email.</param>
        /// <exception cref="ArgumentNullException">If the provided smtp, emailTo, or emailFrom fields are NULL.</exception>
        public void SendEmail(string body) =>
            StaticEmailer.SendEmail(Smtp, EmailFrom, EmailTo, Subject, body, EmailCc, EmailBcc, Priority, Attachments);
    }
}