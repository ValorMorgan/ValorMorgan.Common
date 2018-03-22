using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mail;
using ValorMorgan.Common.Mail.Helpers;

namespace ValorMorgan.Common.Mail
{
    /// <summary>
    /// [Static] Provides means for emailing messages.
    /// </summary>
    public static class StaticEmailer
    {
        #region SendEmail
        /// <summary>
        /// Sends an email with the provided subject and body to the provided recipient(s).
        /// </summary>
        /// <param name="smtp">The mail server to use for sending the email.</param>
        /// <param name="message">The email itself to send.</param>
        /// <exception cref="ArgumentNullException">If the provided smtp or message are NULL.</exception>
        public static void SendEmail(string smtp, MailMessage message)
        {
            if (string.IsNullOrWhiteSpace(smtp))
                throw new ArgumentNullException(nameof(smtp), "No mail server was specified for sending the email.");
            if (message == null)
                throw new ArgumentNullException(nameof(message), "No message was provided to send.");

            try
            {
                using (var mailServer = new SmtpClient(smtp))
                {
                    Debug.WriteLine($"Sending email through server \"{smtp}\" to \"{message.To}\" from \"{message.From}\" CC \"{message.CC}\" BCC \"{message.Bcc}\" with {message.Attachments.Count} attachments as {message.Priority.ToString()} priority.");
                    mailServer.Send(message);
                    Debug.WriteLine("Successfully sent the email.");
                }
            }
            catch
            {
                Debug.WriteLine("Failed to send the email.");
                throw;
            }
        }

        /// <summary>
        /// Sends an email with the provided subject and body to the provided recipient(s).
        /// </summary>
        /// <param name="smtp">The mail server to use for sending the email.</param>
        /// <param name="emailFrom">The sender of the email (who it's from).</param>
        /// <param name="emailTo">The recipient of the email (who it is going to).</param>
        /// <param name="subject">The subject line of the email.</param>
        /// <param name="body">The body of the email.</param>
        /// <exception cref="ArgumentNullException">If the provided smtp, emailTo, or emailFrom fields are NULL.</exception>
        public static void SendEmail(string smtp, string emailFrom, string emailTo, string subject, string body)
        {
            SendEmail(smtp, emailFrom, emailTo, subject, body, null, null);
        }

        /// <summary>
        /// Sends an email with the provided subject and body to the provided recipient(s).
        /// </summary>
        /// <param name="smtp">The mail server to use for sending the email.</param>
        /// <param name="emailFrom">A collection of senders of the email (who it's from).</param>
        /// <param name="emailTo">A collection of recipients of the email (who it is going to).</param>
        /// <param name="subject">The subject line of the email.</param>
        /// <param name="body">The body of the email.</param>
        /// <exception cref="ArgumentNullException">If the provided smtp, emailTo, or emailFrom fields are NULL.</exception>
        public static void SendEmail(string smtp, IEnumerable<string> emailFrom, IEnumerable<string> emailTo, string subject, string body)
        {
            SendEmail(smtp, emailFrom, emailTo, subject, body, null, null);
        }

        /// <summary>
        /// Sends an email with the provided subject and body to the provided recipient(s).
        /// </summary>
        /// <param name="smtp">The mail server to use for sending the email.</param>
        /// <param name="emailFrom">The sender of the email (who it's from).</param>
        /// <param name="emailTo">The recipient of the email (who it is going to).</param>
        /// <param name="subject">The subject line of the email.</param>
        /// <param name="body">The body of the email.</param>
        /// <param name="emailCc">The Carbon-Copy recipient of the email (who is included to be sent to).</param>
        /// <param name="emailBcc">The blind Carbon-Copy recipient of the email (who is included to be sent to).</param>
        /// <exception cref="ArgumentNullException">If the provided smtp, emailTo, or emailFrom fields are NULL.</exception>
        public static void SendEmail(string smtp, string emailFrom, string emailTo, string subject, string body, string emailCc, string emailBcc)
        {
            SendEmail(smtp, emailFrom, emailTo, subject, body, emailCc, emailBcc, EmailerConstants.DEFAULT_MAIL_PRIORITY);
        }

        /// <summary>
        /// Sends an email with the provided subject and body to the provided recipient(s).
        /// </summary>
        /// <param name="smtp">The mail server to use for sending the email.</param>
        /// <param name="emailFrom">The sender of the email (who it's from).</param>
        /// <param name="emailTo">The recipient of the email (who it is going to).</param>
        /// <param name="subject">The subject line of the email.</param>
        /// <param name="body">The body of the email.</param>
        /// <param name="emailCc">The Carbon-Copy recipient of the email (who is included to be sent to).</param>
        /// <param name="emailBcc">The blind Carbon-Copy recipient of the email (who is included to be sent to).</param>
        /// <param name="priority">The priority of the email.</param>
        /// <exception cref="ArgumentNullException">If the provided smtp, emailTo, or emailFrom fields are NULL.</exception>
        public static void SendEmail(string smtp, string emailFrom, string emailTo, string subject, string body, string emailCc, string emailBcc, MailPriority? priority)
        {
            SendEmail(smtp, emailFrom, emailTo, subject, body, emailCc, emailBcc, priority, null);
        }

        /// <summary>
        /// Sends an email with the provided subject and body to the provided recipient(s).
        /// </summary>
        /// <param name="smtp">The mail server to use for sending the email.</param>
        /// <param name="emailFrom">A collection of senders of the email (who it's from).</param>
        /// <param name="emailTo">A collection of recipients of the email (who it is going to).</param>
        /// <param name="subject">The subject line of the email.</param>
        /// <param name="body">The body of the email.</param>
        /// <param name="emailCc">A collection of Carbon-Copy recipients of the email (who is included to be sent to).</param>
        /// <param name="emailBcc">A collection of blind Carbon-Copy recipients of the email (who is included to be sent to).</param>
        /// <exception cref="ArgumentNullException">If the provided smtp, emailTo, or emailFrom fields are NULL.</exception>
        public static void SendEmail(string smtp, IEnumerable<string> emailFrom, IEnumerable<string> emailTo, string subject, string body, IEnumerable<string> emailCc, IEnumerable<string> emailBcc)
        {
            SendEmail(smtp, emailFrom, emailTo, subject, body, emailCc, emailBcc, EmailerConstants.DEFAULT_MAIL_PRIORITY);
        }

        /// <summary>
        /// Sends an email with the provided subject and body to the provided recipient(s).
        /// </summary>
        /// <param name="smtp">The mail server to use for sending the email.</param>
        /// <param name="emailFrom">A collection of senders of the email (who it's from).</param>
        /// <param name="emailTo">A collection of recipients of the email (who it is going to).</param>
        /// <param name="subject">The subject line of the email.</param>
        /// <param name="body">The body of the email.</param>
        /// <param name="emailCc">A collection of Carbon-Copy recipients of the email (who is included to be sent to).</param>
        /// <param name="emailBcc">A collection of blind Carbon-Copy recipients of the email (who is included to be sent to).</param>
        /// <param name="priority">The priority of the email.</param>
        /// <exception cref="ArgumentNullException">If the provided smtp, emailTo, or emailFrom fields are NULL.</exception>
        public static void SendEmail(string smtp, IEnumerable<string> emailFrom, IEnumerable<string> emailTo, string subject, string body, IEnumerable<string> emailCc, IEnumerable<string> emailBcc, MailPriority? priority)
        {
            SendEmail(smtp, emailFrom, emailTo, subject, body, emailCc, emailBcc, priority, null);
        }

        /// <summary>
        /// Sends an email with the provided subject and body to the provided recipient(s).
        /// </summary>
        /// <param name="smtp">The mail server to use for sending the email.</param>
        /// <param name="emailFrom">The sender of the email (who it's from).</param>
        /// <param name="emailTo">The recipient of the email (who it is going to).</param>
        /// <param name="subject">The subject line of the email.</param>
        /// <param name="body">The body of the email.</param>
        /// <param name="emailCc">The Carbon-Copy recipient of the email (who is included to be sent to).</param>
        /// <param name="emailBcc">The blind Carbon-Copy recipient of the email (who is included to be sent to).</param>
        /// <param name="priority">The priority of the email.</param>
        /// <param name="attachments">Any attachments to be attached to the email.</param>
        /// <exception cref="ArgumentNullException">If the provided smtp, emailTo, or emailFrom fields are NULL.</exception>
        public static void SendEmail(string smtp, string emailFrom, string emailTo, string subject, string body, string emailCc, string emailBcc, MailPriority? priority, IEnumerable<Attachment> attachments)
        {
            if (string.IsNullOrWhiteSpace(smtp))
                throw new ArgumentNullException(nameof(smtp), "No mail server was specified for sending the email.");
            if (string.IsNullOrWhiteSpace(emailTo))
                throw new ArgumentNullException(nameof(emailTo), "No recipient for the email was specified to send the email to.");
            if (string.IsNullOrWhiteSpace(emailFrom))
                throw new ArgumentNullException(nameof(emailFrom), "No sender for the email was specified to send the email from.");

            try
            {
                using (var message = new MailMessage(emailFrom, emailTo, subject, body))
                {
                    if (!string.IsNullOrWhiteSpace(emailCc))
                        message.CC.Add(emailCc);
                    if (!string.IsNullOrWhiteSpace(emailBcc))
                        message.Bcc.Add(emailBcc);
                    if (priority != null)
                        message.Priority = (MailPriority)priority;
                    
                    foreach (var attachment in attachments ?? new List<Attachment>())
                        message.Attachments.Add(attachment);

                    SendEmail(smtp, message);
                }
            }
            catch
            {
                Debug.WriteLine("Failed to send the email.");
                throw;
            }
        }

        /// <summary>
        /// Sends an email with the provided subject and body to the provided recipient(s).
        /// </summary>
        /// <param name="smtp">The mail server to use for sending the email.</param>
        /// <param name="emailFrom">A collection of senders of the email (who it's from).</param>
        /// <param name="emailTo">A collection of recipients of the email (who it is going to).</param>
        /// <param name="subject">The subject line of the email.</param>
        /// <param name="body">The body of the email.</param>
        /// <param name="emailCc">A collection of Carbon-Copy recipients of the email (who is included to be sent to).</param>
        /// <param name="emailBcc">A collection of blind Carbon-Copy recipients of the email (who is included to be sent to).</param>
        /// <param name="priority">The priority of the email.</param>
        /// <param name="attachments">Any attachments to be attached to the email.</param>
        /// <exception cref="ArgumentNullException">If the provided smtp, emailTo, or emailFrom fields are NULL.</exception>
        public static void SendEmail(string smtp, IEnumerable<string> emailFrom, IEnumerable<string> emailTo, string subject, string body, IEnumerable<string> emailCc, IEnumerable<string> emailBcc, MailPriority? priority, IEnumerable<Attachment> attachments)
        {
            string emailToString = EmailFormatter.ConvertRecipientCollectionToString(emailTo);
            string emailFromString = EmailFormatter.ConvertRecipientCollectionToString(emailFrom);
            string emailCcString = EmailFormatter.ConvertRecipientCollectionToString(emailCc, EmailerConstants.EMAIL_CC_DIVIDER_CHARACTER);
            string emailBccString = EmailFormatter.ConvertRecipientCollectionToString(emailBcc, EmailerConstants.EMAIL_CC_DIVIDER_CHARACTER);
            SendEmail(smtp, emailFromString, emailToString, subject, body, emailCcString, emailBccString, priority, attachments);
        }
        #endregion
    }
}