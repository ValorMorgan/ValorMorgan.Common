using System;
using System.Diagnostics;

namespace ValorMorgan.Common.Error
{
    /// <summary>
    /// Provides logging functionalities for exceptions and verbose messages.
    /// </summary>
    public class Logger : ILogger
    {
        #region PROPERTIES
        /// <summary>
        /// The ID to use for LogException calls.
        /// </summary>
        public int? LogExceptionId { get; set; }

        /// <summary>
        /// The EntryType for LogException.
        /// </summary>
        public EventLogEntryType? LogExceptionType { get; set; }

        /// <summary>
        /// The ID to use for LogMessage calls.
        /// </summary>
        public int? LogMessageId { get; set; }

        /// <summary>
        /// The EntryType for LogMessage.
        /// </summary>
        public EventLogEntryType? LogMessageType { get; set; }

        /// <summary>
        /// The Event Viewer log source.
        /// </summary>
        public string LogSource { get; set; }

        /// <summary>
        /// A flag indicating if LogMessage should work.
        /// </summary>
        public bool? LogVerbose { get; set; }
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Provides logging functionalities for exceptions and verbose messages.
        /// </summary>
        public Logger() { }

        /// <summary>
        /// Provides logging functionalities for exceptions and verbose messages.
        /// </summary>
        /// <param name="logSource">The Event Viewer log source.</param>
        public Logger(string logSource)
            : this()
        {
            LogSource = logSource;
        }

        /// <summary>
        /// Provides logging functionalities for exceptions and verbose messages.
        /// </summary>
        /// <param name="logSource">The Event Viewer log source.</param>
        /// <param name="logVerbose">A flag indicating if LogMessage should work.</param>
        public Logger(string logSource, bool? logVerbose)
            : this(logSource)
        {
            LogVerbose = logVerbose;
        }

        /// <summary>
        /// Provides logging functionalities for exceptions and verbose messages.
        /// </summary>
        /// <param name="logSource">The Event Viewer log source.</param>
        /// <param name="logVerbose">A flag indicating if LogMessage should work.</param>
        /// <param name="logExceptionType">The EntryType for LogException.</param>
        /// <param name="logMessageType">The EntryType for LogMessage.</param>
        public Logger(string logSource, bool? logVerbose, EventLogEntryType? logExceptionType, EventLogEntryType? logMessageType)
            : this(logSource, logVerbose)
        {
            LogExceptionType = logExceptionType;
            LogMessageType = logMessageType;
        }

        /// <summary>
        /// Provides logging functionalities for exceptions and verbose messages.
        /// </summary>
        /// <param name="logSource">The Event Viewer log source.</param>
        /// <param name="logVerbose">A flag indicating if LogMessage should work.</param>
        /// <param name="logExceptionType">The EntryType for LogException.</param>
        /// <param name="logMessageType">The EntryType for LogMessage.</param>
        /// <param name="logExceptionId">The ID to use for LogException calls.</param>
        /// <param name="logMessageId">The ID to use for LogMessage calls.</param>
        public Logger(string logSource, bool? logVerbose, EventLogEntryType? logExceptionType, EventLogEntryType? logMessageType, int? logExceptionId, int? logMessageId)
            : this(logSource, logVerbose, logExceptionType, logMessageType)
        {
            LogExceptionId = logExceptionId;
            LogMessageId = logMessageId;
        }
        #endregion

        /// <summary>
        /// Logs the provided exception to the Event Viewer.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <exception cref="ArgumentNullException">If the provided exception is NULL or if the logSource is NULL.</exception>
        /// <exception cref="Exception">If EventLog.WriteEntry fails, the original provided exception will be re-thrown.</exception>
        public void LogException(Exception exception)
        {
            StaticLogger.LogException(
                exception,
                LogSource,
                LogExceptionId,
                LogExceptionType
            );
        }

        /// <summary>
        /// Logs the provided exception using the ExceptionFormatter to the Event Viewer.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <exception cref="ArgumentNullException">If the provided exception is NULL or if the logSource is NULL.</exception>
        /// <exception cref="Exception">If EventLog.WriteEntry fails, the original provided exception will be re-thrown.</exception>
        public void LogFormattedException(Exception exception)
        {
            StaticLogger.LogFormattedException(
                exception,
                LogSource,
                LogExceptionId,
                LogExceptionType
            );
        }

        /// <summary>
        /// Logs the provided message to the Event Viewer if Log Verbose is set to True.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <exception cref="ArgumentNullException">If the provided logMessage or logSource are NULL.</exception>
        public void LogMessage(string message)
        {
            StaticLogger.LogMessage(
                message,
                LogSource,
                LogVerbose,
                LogMessageId,
                LogMessageType
            );
        }
    }
}