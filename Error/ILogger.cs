using System;
using System.Diagnostics;

namespace ValorMorgan.Common.Error
{
    /// <summary>
    /// Provides logging functionalities for exceptions and verbose messages.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// The ID to use for LogException calls.
        /// </summary>
        int? LogExceptionId { get; set; }

        /// <summary>
        /// The EntryType for LogException.
        /// </summary>
        EventLogEntryType? LogExceptionType { get; set; }

        /// <summary>
        /// The ID to use for LogMessage calls.
        /// </summary>
        int? LogMessageId { get; set; }

        /// <summary>
        /// The EntryType for LogMessage.
        /// </summary>
        EventLogEntryType? LogMessageType { get; set; }

        /// <summary>
        /// The Event Viewer log source.
        /// </summary>
        string LogSource { get; set; }

        /// <summary>
        /// A flag indicating if LogMessage should work.
        /// </summary>
        bool? LogVerbose { get; set; }

        /// <summary>
        /// Logs the provided exception to the Event Viewer.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <exception cref="ArgumentNullException">If the provided exception is NULL or if the logSource is NULL.</exception>
        /// <exception cref="Exception">If EventLog.WriteEntry fails, the original provided exception will be re-thrown.</exception>
        void LogException(Exception exception);

        /// <summary>
        /// Logs the provided exception using the ExceptionFormatter to the Event Viewer.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <exception cref="ArgumentNullException">If the provided exception is NULL or if the logSource is NULL.</exception>
        /// <exception cref="Exception">If EventLog.WriteEntry fails, the original provided exception will be re-thrown.</exception>
        void LogFormattedException(Exception exception);

        /// <summary>
        /// Logs the provided message to the Event Viewer if Log Verbose is set to True.
        /// </summary>
        /// <param name="logMessage">The message to log.</param>
        /// <exception cref="ArgumentNullException">If the provided logMessage or logSource are NULL.</exception>
        void LogMessage(string logMessage);
    }
}