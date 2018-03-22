using System;
using System.Diagnostics;
using ValorMorgan.Common.Error.Helpers;

namespace ValorMorgan.Common.Error
{
    /// <summary>
    /// [Static] Provides logging functionalities for exceptions and verbose messages.
    /// </summary>
    public static class StaticLogger
    {
        #region LogMessage
        /// <summary>
        /// Logs the provided message to the Event Viewer if Log Verbose is set to True.
        /// </summary>
        /// <param name="logMessage">The message to log.</param>
        /// <param name="logSource">The Event Viewer source to use.</param>
        /// <param name="logVerbose">A flag for if this should log or not (will use the default value if NULL).</param>
        /// <param name="logId">The ID to assign to the message (will use the default value if NULL).</param>
        /// <param name="logEntryType">Specifies the event type of an event log entry (will use the default value if NULL).</param>
        /// <exception cref="ArgumentNullException">If the provided logMessage or logSource are NULL.</exception>
        public static void LogMessage(string logMessage, string logSource, bool? logVerbose, int? logId, EventLogEntryType? logEntryType)
        {
            Debug.WriteLine($"DEBUG MESSAGE: {logMessage}");
            if (!(logVerbose ?? LoggerConstants.DEFAULT_LOG_VERBOSE))
                return;

            if (string.IsNullOrWhiteSpace(logMessage))
                throw new ArgumentNullException(nameof(logMessage), "No message was provided to be logged.");
            if (string.IsNullOrWhiteSpace(logSource))
                throw new ArgumentNullException(nameof(logSource), "No log source was provided for logging.");

            if (logId == null)
                logId = LoggerConstants.DEFAULT_ID;
            if (logEntryType == null)
                logEntryType = EventLogEntryType.Information;
            try
            {
                Debug.WriteLine($"Logging Message to Source \"{logSource}\" with ID \"{logId}\" as EntryType \"{logEntryType.ToString()}\". Message: \"{logMessage}\"");
                EventLog.WriteEntry(logSource, logMessage, (EventLogEntryType)logEntryType, (int)logId);
                Debug.WriteLine("Successfully Logged Message");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to Log Message because {ExceptionFormatter.GetExceptionMessagesOnly(ex)}");
            }
        }

        /// <summary>
        /// Logs the provided message to the Event Viewer if Log Verbose is set to True.
        /// </summary>
        /// <param name="logMessage">The message to log.</param>
        /// <param name="logSource">The Event Viewer source to use.</param>
        /// <exception cref="ArgumentNullException">If the provided logMessage or logSource are NULL.</exception>
        public static void LogMessage(string logMessage, string logSource)
        {
            LogMessage(
                logMessage,
                logSource,
                null,
                null,
                null
            );
        }

        /// <summary>
        /// Logs the provided message to the Event Viewer if Log Verbose is set to True.
        /// </summary>
        /// <param name="logMessage">The message to log.</param>
        /// <param name="logSource">The Event Viewer source to use.</param>
        /// <param name="logVerbose">A flag for if this should log or not (will use the default value if NULL).</param>
        /// <exception cref="ArgumentNullException">If the provided logMessage or logSource are NULL.</exception>
        public static void LogMessage(string logMessage, bool? logVerbose, string logSource)
        {
            LogMessage(
                logMessage,
                logSource,
                logVerbose,
                null,
                null
            );
        }

        /// <summary>
        /// Logs the provided message to the Event Viewer if Log Verbose is set to True.
        /// </summary>
        /// <param name="logMessage">The message to log.</param>
        /// <param name="logSource">The Event Viewer source to use.</param>
        /// <param name="logVerbose">A flag for if this should log or not (will use the default value if NULL).</param>
        /// <param name="logId">The ID to assign to the message (will use the default value if NULL).</param>
        /// <exception cref="ArgumentNullException">If the provided logMessage or logSource are NULL.</exception>
        public static void LogMessage(string logMessage, bool? logVerbose, string logSource, int? logId)
        {
            LogMessage(
                logMessage,
                logSource,
                logVerbose,
                logId,
                null
            );
        }
        #endregion

        #region LogException
        /// <summary>
        /// Logs the provided exception to the Event Viewer.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="logSource">The Event Viewer source to use.</param>
        /// <param name="logId">The ID to assign to the message (will use the default value if NULL).</param>
        /// <param name="logEntryType">Specifies the event type of an event log entry (will use the default value if NULL).</param>
        /// <exception cref="ArgumentNullException">If the provided exception is NULL or if the logSource is NULL.</exception>
        /// <exception cref="Exception">If EventLog.WriteEntry fails, the original provided exception will be re-thrown.</exception>
        public static void LogException(Exception exception, string logSource, int? logId, EventLogEntryType? logEntryType)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception), "Cannot log a NULL exception. Please confirm this function is called when an exception occurs and is caught properly.");

            string logMessage = exception.ToString();

            // Defaulting values that may be provided as NULL.
            if (string.IsNullOrWhiteSpace(logSource))
                throw new ArgumentNullException(nameof(logSource), "No log source was provided for logging.");

            if (logId == null)
                logId = LoggerConstants.DEFAULT_ID;
            if (logEntryType == null)
                logEntryType = EventLogEntryType.Error;
            try
            {
                Debug.WriteLine($"Logging Exception to Source \"{logSource}\" with ID \"{logId}\" as EntryType \"{logEntryType.ToString()}\". Exception Message: \"{exception.Message}\"");
                EventLog.WriteEntry(logSource, logMessage, (EventLogEntryType)logEntryType, (int)logId);
                Debug.WriteLine("Successfully Logged Exception");
            }
            catch
            {
                // Failed to log so throw original exception for default .NET handling
                Debug.WriteLine("Failed to Log Exception");
                throw exception;
            }
        }

        /// <summary>
        /// Logs the provided exception to the Event Viewer.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="logSource">The Event Viewer source to use.</param>
        /// <exception cref="ArgumentNullException">If the provided exception is NULL or if the logSource is NULL.</exception>
        /// <exception cref="Exception">If EventLog.WriteEntry fails, the original provided exception will be re-thrown.</exception>
        public static void LogException(Exception exception, string logSource)
        {
            LogException(
                exception,
                logSource,
                null,
                null
            );
        }

        /// <summary>
        /// Logs the provided exception to the Event Viewer.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="logSource">The Event Viewer source to use.</param>
        /// <param name="logId">The ID to assign to the message (will use the default value if NULL).</param>
        /// <exception cref="ArgumentNullException">If the provided exception is NULL or if the logSource is NULL.</exception>
        /// <exception cref="Exception">If EventLog.WriteEntry fails, the original provided exception will be re-thrown.</exception>
        public static void LogException(Exception exception, string logSource, int? logId)
        {
            LogException(
                exception,
                logSource,
                logId,
                null
            );
        }
        #endregion

        #region LogFormattedException
        /// <summary>
        /// Logs the provided exception using the ExceptionFormatter to the Event Viewer.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="logSource">The Event Viewer source to use.</param>
        /// <param name="logId">The ID to assign to the message (will use the default value if NULL).</param>
        /// <param name="logEntryType">Specifies the event type of an event log entry (will use the default value if NULL).</param>
        /// <exception cref="ArgumentNullException">If the provided exception is NULL or if the logSource is NULL.</exception>
        /// <exception cref="Exception">If EventLog.WriteEntry fails, the original provided exception will be re-thrown.</exception>
        public static void LogFormattedException(Exception exception, string logSource, int? logId, EventLogEntryType? logEntryType)
        {
            if (exception == null)
                throw new ArgumentNullException(nameof(exception), "Cannot log a NULL exception. Please confirm this function is called when an exception occurs and is caught properly.");

            string logMessage = ExceptionFormatter.GetFormatedException(exception);

            // Defaulting values that may be provided as NULL.
            if (string.IsNullOrWhiteSpace(logSource))
                throw new ArgumentNullException(nameof(logSource), "No log source was provided for logging.");

            if (logId == null)
                logId = LoggerConstants.DEFAULT_ID;
            if (logEntryType == null)
                logEntryType = EventLogEntryType.Error;
            try
            {
                Debug.WriteLine($"Logging Exception to Source \"{logSource}\" with ID \"{logId}\" as EntryType \"{logEntryType.ToString()}\". Exception Message: \"{exception.Message}\"");
                EventLog.WriteEntry(logSource, logMessage, (EventLogEntryType)logEntryType, (int)logId);
                Debug.WriteLine("Successfully Logged Exception");
            }
            catch
            {
                // Failed to log so throw original exception for default .NET handling
                Debug.WriteLine("Failed to Log Exception");
                throw exception;
            }
        }

        /// <summary>
        /// Logs the provided exception using the ExceptionFormatter to the Event Viewer.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="logSource">The Event Viewer source to use.</param>
        /// <exception cref="ArgumentNullException">If the provided exception is NULL or if the logSource is NULL.</exception>
        /// <exception cref="Exception">If EventLog.WriteEntry fails, the original provided exception will be re-thrown.</exception>
        public static void LogFormattedException(Exception exception, string logSource)
        {
            LogFormattedException(
                exception,
                logSource,
                null,
                null
            );
        }

        /// <summary>
        /// Logs the provided exception using the ExceptionFormatter to the Event Viewer.
        /// </summary>
        /// <param name="exception">The exception to log.</param>
        /// <param name="logSource">The Event Viewer source to use.</param>
        /// <param name="logId">The ID to assign to the message (will use the default value if NULL).</param>
        /// <exception cref="ArgumentNullException">If the provided exception is NULL or if the logSource is NULL.</exception>
        /// <exception cref="Exception">If EventLog.WriteEntry fails, the original provided exception will be re-thrown.</exception>
        public static void LogFormattedException(Exception exception, string logSource, int? logId)
        {
            LogFormattedException(
                exception,
                logSource,
                logId,
                null
            );
        }
        #endregion
    }
}