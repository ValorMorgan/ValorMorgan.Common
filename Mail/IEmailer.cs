using System;

namespace ValorMorgan.Common.Mail
{
    /// <summary>
    /// Provides means for emailing messages.
    /// </summary>
    public interface IEmailer
    {
        /// <summary>
        /// Sends an email with the provided subject and body to the provided recipient(s).
        /// </summary>
        /// <param name="body">The body of the email.</param>
        /// <exception cref="ArgumentNullException">If the provided smtp, emailTo, or emailFrom fields are NULL.</exception>
        void SendEmail(string body);
    }
}