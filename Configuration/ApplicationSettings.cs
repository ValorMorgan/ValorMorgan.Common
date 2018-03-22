using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;

namespace ValorMorgan.Common.Configuration
{
    /// <summary>
    /// Reads and converts application settings to desired types.
    /// </summary>
    public static class ApplicationSettings
    {
        #region PROPERTIES
        /// <summary>
        /// All the keys within the AppSettings.
        /// </summary>
        public static string[] AllKeys => AppSettings?.AllKeys ?? new string[] { };

        /// <summary>
        /// The NameValueCollection "AppSettings" from the ConfigurationManger.
        /// </summary>
        public static NameValueCollection AppSettings => ConfigurationManager.AppSettings;
        #endregion

        /// <summary>
        /// Checks if the provided string has a matching key in the AppSettings.
        /// </summary>
        /// <param name="key">The key to search for in the AppSettings.</param>
        /// <returns>Whether or not the key exists in the AppSettings.</returns>
        /// <exception cref="ArgumentNullException">If the provided key is NULL or whitespace only.</exception>
        public static bool HasKey(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "The provided key is either NULL or whitespace only.");

            return AllKeys?.Contains(key) ?? false;
        }

        /// <summary>
        /// Checks if the provided string has a matching key with a valid value in the AppSettings.
        /// </summary>
        /// <param name="key">The key to search for in the AppSettings.</param>
        /// <returns>Whether or not the key exists and has a valid value in the AppSettings.</returns>
        /// <exception cref="ArgumentNullException">If the provided key is NULL or whitespace only.</exception>
        public static bool HasValue(string key) =>
            HasKey(key) && !string.IsNullOrWhiteSpace(AsString(key));

        /// <summary>
        /// Checks if theprovided string has a matching key in the AppSettings and throws
        /// a ConfigurationErrorsException if not found.
        /// </summary>
        /// <param name="key">The key to search for in the AppSettings.</param>
        /// <exception cref="ArgumentNullException">If the provided key is NULL or whitespace only.</exception>
        /// <exception cref="ConfigurationErrorsException">If the provided key is not found in the AppSettings.</exception>
        public static void ThrowExceptionIfKeyNotFound(string key)
        {
            if (!HasKey(key))
                throw new ConfigurationErrorsException($"No AppSetting by the name of \"{key}\" could be found. Please confirm the Configuration Settings are setup properly.");
        }

        #region AS <TYPE>
        /// <summary>
        /// Retrieves the value from the specified key within the AppSettings.
        /// </summary>
        /// <param name="key">The key of the setting to retrieve.</param>
        /// <returns>The value of the setting for the key provided.</returns>
        /// <exception cref="ArgumentNullException">If the provided key is NULL or whitespace only.</exception>
        /// <exception cref="ConfigurationErrorsException">If the key cannot be found meaning that the Configuration is not setup correctly.</exception>
        public static string AsString(string key)
        {
            ThrowExceptionIfKeyNotFound(key);
            if (AppSettings != null)
                return AppSettings[key] ?? string.Empty;

            throw new ConfigurationErrorsException($"{nameof(ConfigurationManager)}.{nameof(AppSettings)} could not be found. Perhaps you are missing a reference to (System.Configuration)?");
        }

        /// <summary>
        /// Retrieves the value from the specified key within the AppSettings.
        /// </summary>
        /// <typeparam name="T">The type to cast the setting to (must be a class type).</typeparam>
        /// <param name="key">The key of the setting to retrieve.</param>
        /// <returns>The value of the setting for the key provided.</returns>
        /// <exception cref="ArgumentNullException">If the provided key is NULL or whitespace only.</exception>
        /// <exception cref="ConfigurationErrorsException">If the key cannot be found meaning that the Configuration is not setup correctly.</exception>
        /// <remarks>
        /// The type is attempted to be directly cast to with the "as" keyword resulting in NULL
        /// if the type could not be cast to. It also restricts the type T to be a class type.
        /// </remarks>
        public static T As<T>(string key)
            where T : class => AsString(key) as T;

        /// <summary>
        /// Retrieves the value from the specified key within the AppSettings.
        /// </summary>
        /// <param name="key">The key of the setting to retrieve.</param>
        /// <returns>The value of the setting for the key provided.</returns>
        /// <exception cref="ArgumentNullException">If the provided key is NULL or whitespace only.</exception>
        /// <exception cref="ConfigurationErrorsException">If the key cannot be found meaning that the Configuration is not setup correctly.</exception>
        public static char AsChar(string key) => char.Parse(AsString(key) ?? string.Empty);

        /// <summary>
        /// Retrieves the value from the specified key within the AppSettings.
        /// </summary>
        /// <param name="key">The key of the setting to retrieve.</param>
        /// <returns>The value of the setting for the key provided.</returns>
        /// <exception cref="ArgumentNullException">If the provided key is NULL or whitespace only.</exception>
        /// <exception cref="ConfigurationErrorsException">If the key cannot be found meaning that the Configuration is not setup correctly.</exception>
        public static bool AsBool(string key) => bool.Parse(AsString(key) ?? string.Empty);

        /// <summary>
        /// Retrieves the value from the specified key within the AppSettings.
        /// </summary>
        /// <param name="key">The key of the setting to retrieve.</param>
        /// <returns>The value of the setting for the key provided.</returns>
        /// <exception cref="ArgumentNullException">If the provided key is NULL or whitespace only.</exception>
        /// <exception cref="ConfigurationErrorsException">If the key cannot be found meaning that the Configuration is not setup correctly.</exception>
        public static byte AsByte(string key) => byte.Parse(AsString(key) ?? string.Empty);

        /// <summary>
        /// Retrieves the value from the specified key within the AppSettings.
        /// </summary>
        /// <param name="key">The key of the setting to retrieve.</param>
        /// <returns>The value of the setting for the key provided.</returns>
        /// <exception cref="ArgumentNullException">If the provided key is NULL or whitespace only.</exception>
        /// <exception cref="ConfigurationErrorsException">If the key cannot be found meaning that the Configuration is not setup correctly.</exception>
        public static sbyte AsSByte(string key) => (sbyte)AsByte(key);

        /// <summary>
        /// Retrieves the value from the specified key within the AppSettings.
        /// </summary>
        /// <param name="key">The key of the setting to retrieve.</param>
        /// <returns>The value of the setting for the key provided.</returns>
        /// <exception cref="ArgumentNullException">If the provided key is NULL or whitespace only.</exception>
        /// <exception cref="ConfigurationErrorsException">If the key cannot be found meaning that the Configuration is not setup correctly.</exception>
        public static decimal AsDecimal(string key) => decimal.Parse(AsString(key) ?? string.Empty);

        /// <summary>
        /// Retrieves the value from the specified key within the AppSettings.
        /// </summary>
        /// <param name="key">The key of the setting to retrieve.</param>
        /// <returns>The value of the setting for the key provided.</returns>
        /// <exception cref="ArgumentNullException">If the provided key is NULL or whitespace only.</exception>
        /// <exception cref="ConfigurationErrorsException">If the key cannot be found meaning that the Configuration is not setup correctly.</exception>
        public static double AsDouble(string key) => double.Parse(AsString(key) ?? string.Empty);

        /// <summary>
        /// Retrieves the value from the specified key within the AppSettings.
        /// </summary>
        /// <param name="key">The key of the setting to retrieve.</param>
        /// <returns>The value of the setting for the key provided.</returns>
        /// <exception cref="ArgumentNullException">If the provided key is NULL or whitespace only.</exception>
        /// <exception cref="ConfigurationErrorsException">If the key cannot be found meaning that the Configuration is not setup correctly.</exception>
        public static float AsFloat(string key) => float.Parse(AsString(key) ?? string.Empty);

        /// <summary>
        /// Retrieves the value from the specified key within the AppSettings.
        /// </summary>
        /// <param name="key">The key of the setting to retrieve.</param>
        /// <returns>The value of the setting for the key provided.</returns>
        /// <exception cref="ArgumentNullException">If the provided key is NULL or whitespace only.</exception>
        /// <exception cref="ConfigurationErrorsException">If the key cannot be found meaning that the Configuration is not setup correctly.</exception>
        public static int AsInt(string key) => int.Parse(AsString(key) ?? string.Empty);

        /// <summary>
        /// Retrieves the value from the specified key within the AppSettings.
        /// </summary>
        /// <param name="key">The key of the setting to retrieve.</param>
        /// <returns>The value of the setting for the key provided.</returns>
        /// <exception cref="ArgumentNullException">If the provided key is NULL or whitespace only.</exception>
        /// <exception cref="ConfigurationErrorsException">If the key cannot be found meaning that the Configuration is not setup correctly.</exception>
        public static uint AsUInt(string key) => (uint)AsInt(key);

        /// <summary>
        /// Retrieves the value from the specified key within the AppSettings.
        /// </summary>
        /// <param name="key">The key of the setting to retrieve.</param>
        /// <returns>The value of the setting for the key provided.</returns>
        /// <exception cref="ArgumentNullException">If the provided key is NULL or whitespace only.</exception>
        /// <exception cref="ConfigurationErrorsException">If the key cannot be found meaning that the Configuration is not setup correctly.</exception>
        public static long AsLong(string key) => long.Parse(AsString(key) ?? string.Empty);

        /// <summary>
        /// Retrieves the value from the specified key within the AppSettings.
        /// </summary>
        /// <param name="key">The key of the setting to retrieve.</param>
        /// <returns>The value of the setting for the key provided.</returns>
        /// <exception cref="ArgumentNullException">If the provided key is NULL or whitespace only.</exception>
        /// <exception cref="ConfigurationErrorsException">If the key cannot be found meaning that the Configuration is not setup correctly.</exception>
        public static ulong AsULong(string key) => (ulong)AsLong(key);
        
        /// <summary>
        /// Retrieves the value from the specified key within the AppSettings.
        /// </summary>
        /// <param name="key">The key of the setting to retrieve.</param>
        /// <returns>The value of the setting for the key provided.</returns>
        /// <exception cref="ArgumentNullException">If the provided key is NULL or whitespace only.</exception>
        /// <exception cref="ConfigurationErrorsException">If the key cannot be found meaning that the Configuration is not setup correctly.</exception>
        public static short AsShort(string key) => short.Parse(AsString(key) ?? string.Empty);

        /// <summary>
        /// Retrieves the value from the specified key within the AppSettings.
        /// </summary>
        /// <param name="key">The key of the setting to retrieve.</param>
        /// <returns>The value of the setting for the key provided.</returns>
        /// <exception cref="ArgumentNullException">If the provided key is NULL or whitespace only.</exception>
        /// <exception cref="ConfigurationErrorsException">If the key cannot be found meaning that the Configuration is not setup correctly.</exception>
        public static ushort AsUShort(string key) => (ushort)AsShort(key);
        #endregion
    }
}