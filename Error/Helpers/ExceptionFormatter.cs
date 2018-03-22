using System;
using System.Diagnostics;

namespace ValorMorgan.Common.Error.Helpers
{
    /// <summary>
    /// Provided format functionalities for interpreting exceptions.
    /// </summary>
    public static class ExceptionFormatter
    {
        #region PROPERTIES
        /// <summary>
        /// A simple divider for splitting up sections in the formated exception.
        /// </summary>
        public static string Divider => new string(LoggerConstants.LOGGER_FORMAT_DIVIDER_CHARACTER, LoggerConstants.LOGGER_FORMAT_DIVIDER_LENGTH);
        #endregion

        /// <summary>
        /// Returns the exception messages in a single line.
        /// </summary>
        /// <param name="exception">The exception to parse.</param>
        /// <returns>The exception messages in a single line.</returns>
        public static string GetExceptionMessagesOnly(Exception exception)
        {
            if (exception == null)
            {
                Debug.WriteLine("NULL Exception detected. Providing default message.");
                return LoggerConstants.DEFAULT_LOG_MESSAGE;
            }

            string exceptionMessage = exception.Message.Trim();
            string message = "";
            if (!string.IsNullOrEmpty(exceptionMessage))
                message = exceptionMessage.EndsWith(".") ? exceptionMessage : $"{exceptionMessage}.";
            return exception.InnerException != null ?
                $"{message} {GetExceptionMessagesOnly(exception.InnerException)}".Trim() :
                message.Trim();
        }

        /// <summary>
        /// Formats and returns the exception as an easily readable string.
        /// </summary>
        /// <param name="exception">The exception to format (will provide a default message if NULL).</param>
        /// <returns>The exception as an easily readable string.</returns>
        public static string GetFormatedException(Exception exception)
        {
            if (exception == null)
            {
                Debug.WriteLine("NULL Exception detected. Providing default message.");
                return LoggerConstants.DEFAULT_LOG_MESSAGE;
            }

            string formatedException = string.Format("{1}{0}Type: {2}{0}Message: {3}{0}Logged On: {4}{0}{1}{0}Stack Trace:{0}{5}{0}{1}",
                Environment.NewLine,
                Divider,
                exception.GetType().ToString().Trim(),
                exception.Message.Trim(),
                DateTime.Now,
                exception.StackTrace?.Trim()
            ).Trim();
            if (exception.InnerException == null)
                return formatedException;

            // Append inner exceptions to end (inner-most exception being at the bottom of the message).
            return string.Format("{1}{0}{0}Inner Exception:{0}{2}",
                Environment.NewLine,
                formatedException,
                GetFormatedException(exception.InnerException)
            );
        }
    }
}