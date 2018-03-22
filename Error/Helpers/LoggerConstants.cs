namespace ValorMorgan.Common.Error.Helpers
{
    /// <summary>
    /// Constant values for logging operations.
    /// </summary>
    public static class LoggerConstants
    {
        #region VARIABLES
        /// <summary>
        /// Default ID for logging.
        /// </summary>
        public const int DEFAULT_ID = 0;

        /// <summary>
        /// Default message when logging messages.
        /// </summary>
        public const string DEFAULT_LOG_MESSAGE = "No message or exception was provided to log or an unknown exception occurred.";

        /// <summary>
        /// Default source for logging.
        /// </summary>
        public const string DEFAULT_LOG_SOURCE = "Application";

        /// <summary>
        /// Default flag for logging verbose messages.
        /// </summary>
        public const bool DEFAULT_LOG_VERBOSE = false;

        /// <summary>
        /// Format dividing character when logging formatted exceptions.
        /// </summary>
        public const char LOGGER_FORMAT_DIVIDER_CHARACTER = '=';

        /// <summary>
        /// Format diving character length when logging formatted exceptions.
        /// </summary>
        public const int LOGGER_FORMAT_DIVIDER_LENGTH = 60;
        #endregion
    }
}