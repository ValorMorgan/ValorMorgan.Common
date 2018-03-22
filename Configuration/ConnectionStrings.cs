using System;
using System.Configuration;

namespace ValorMorgan.Common.Configuration
{
    /// <summary>
    /// Reads and retrieves connection strings.
    /// </summary>
    public static class ConnectionStrings
    {
        #region PROPERTIES
        /// <summary>
        /// All the ConnectionStrings within the ConfigurationManager.
        /// </summary>
        public static ConnectionStringSettingsCollection ConnectStrings => ConfigurationManager.ConnectionStrings ?? new ConnectionStringSettingsCollection();
        #endregion

        /// <summary>
        /// Retrieves the provided connection string.
        /// </summary>
        /// <param name="connectionString">The connection string to retrieve.</param>
        /// <returns>The connection string if found.</returns>
        /// <exception cref="ConfigurationErrorsException">If the provided connection string is not found.</exception>
        public static string GetConnectionString(string connectionString)
        {
            ThrowExceptionIfConnectionStringNotFound(connectionString);
            
            // ReSharper disable once PossibleNullReferenceException
            // ReSharper disable once AssignNullToNotNullAttribute
            return ConnectStrings[connectionString]?.ConnectionString;
        }

        /// <summary>
        /// Checks if the provided connection string exists.
        /// </summary>
        /// <param name="connectionString">The connection string to check for.</param>
        /// <returns>True/false if the connection string exists.</returns>
        /// <exception cref="ArgumentNullException">If the provided connection string name is NULL or whitespace only.</exception>
        public static bool HasConnectionString(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString), "The provided connection string name is either NULL or whitespace only.");

            try
            {
                if (ConnectStrings == null || ConnectStrings.Count <= 0)
                    return false;

                return !string.IsNullOrWhiteSpace(ConnectStrings[connectionString]?.ConnectionString);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Checks if theprovided string has a matching key in the AppSettings and throws
        /// a ConfigurationErrorsException if not found.
        /// </summary>
        /// <param name="connectionString">The key to search for in the AppSettings.</param>
        /// <exception cref="ConfigurationErrorsException">If the provided key is not found in the AppSettings.</exception>
        public static void ThrowExceptionIfConnectionStringNotFound(string connectionString)
        {
            if (!HasConnectionString(connectionString))
                throw new ConfigurationErrorsException($"No Connection String by the name of \"{connectionString}\" could be found. Please confirm the Configuration Settings are setup properly.");
        }
    }
}