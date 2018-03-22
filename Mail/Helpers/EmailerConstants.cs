using System.Net.Mail;

namespace ValorMorgan.Common.Mail.Helpers
{
    /// <summary>
    /// Constant values for email operations.
    /// </summary>
    public static class EmailerConstants
    {
        #region VARIABLES
        /// <summary>
        /// Default mail priority for emails.
        /// </summary>
        public const MailPriority DEFAULT_MAIL_PRIORITY = MailPriority.Normal;

        /// <summary>
        /// Default SMTP server to use.
        /// </summary>
        public const string DEFAULT_SMTP = "smtp";

        /// <summary>
        /// Dividing character for CC lists.
        /// </summary>
        public const char EMAIL_CC_DIVIDER_CHARACTER = ',';

        /// <summary>
        /// Dividing character for formatting emails.
        /// </summary>
        public const char EMAIL_FORMAT_DIVIDER_CHARACTER = ';';
        #endregion
    }
}