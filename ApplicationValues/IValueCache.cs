using System;
using System.Configuration;
using ValorMorgan.Common.ApplicationValues.Exceptions;

namespace ValorMorgan.Common.ApplicationValues
{
    /// <summary>
    /// Houses value registrations for retrieving and converting application settings as desired. Supports
    /// having values be cast to any type (including custom types), having default values, and custom logic
    /// for casting a setting to a custom type (custom logic can perform any operation desired).
    /// </summary>
    public interface IValueCache
    {
        /// <summary>
        /// Gets the Value Registry representing the setting provided.
        /// </summary>
        /// <param name="settingName">The name of the setting already registered (case sensitive).</param>
        /// <returns>The IValueRegistry object representing the setting provided.</returns>
        /// <exception cref="ValueRegistryNotFoundException">If the provided setting name is not found to be registered.</exception>
        IValueRegistry GetSettingRegistry(string settingName);

        /// <summary>
        /// Gets the value from the settings as defined by the setting registry.
        /// </summary>
        /// <param name="settingName">The name of the setting already registered (case sensitive).</param>
        /// <returns>The value of the setting requested as the Type it was registered as (wrapped by Type dynamic).</returns>
        /// <exception cref="ApplicationException">If an unknown error occurs.</exception>
        /// <exception cref="ConfigurationErrorsException">If the setting does not have a value and was marked as required.</exception>
        /// <exception cref="ValueRegistryNotFoundException">If the provided setting name is not found to be registered.</exception>
        dynamic GetValue(string settingName);

        /// <summary>
        /// Gets the value from the settings as defined by the setting registry.
        /// </summary>
        /// <typeparam name="T">The type the setting should return as (should match with the Type it was registered as).</typeparam>
        /// <param name="settingName">The name of the setting already registered (case sensitive).</param>
        /// <returns>The value of the setting requested as the Type in the template here.</returns>
        /// <exception cref="ApplicationException">If an unknown error occurs.</exception>
        /// <exception cref="ConfigurationErrorsException">If the setting does not have a value and was marked as required.</exception>
        /// <exception cref="ValueRegistryNotFoundException">If the provided setting name is not found to be registered.</exception>
        T GetValue<T>(string settingName);

        /// <summary>
        /// Registers a setting name to the Value Cache.
        /// </summary>
        /// <param name="settingName">The name of the setting (case sensitive).</param>
        void RegisterValue(string settingName);

        /// <summary>
        /// Registers a setting name to the Value Cache.
        /// </summary>
        /// <param name="settingName">The name of the setting (case sensitive).</param>
        /// <param name="valueType">The type the setting should end up as (i.e. int).</param>
        void RegisterValue(string settingName, Type valueType);

        /// <summary>
        /// Registers a setting name to the Value Cache.
        /// </summary>
        /// <param name="settingName">The name of the setting (case sensitive).</param>
        /// <param name="valueType">The type the setting should end up as (i.e. int).</param>
        /// <param name="isRequired">Whether the setting is required or if a default value can be used.</param>
        /// <param name="defaultValue">The value that should be used if the setting is not found and if the setting is not required.</param>
        void RegisterValue(string settingName, Type valueType, bool isRequired, dynamic defaultValue);

        /// <summary>
        /// Registers a setting name to the Value Cache.
        /// </summary>
        /// <param name="settingName">The name of the setting (case sensitive).</param>
        /// <param name="valueType">The type the setting should end up as (i.e. int).</param>
        /// <param name="isRequired">Whether the setting is required or if a default value can be used.</param>
        /// <param name="customTypeDelegate">The operation used for casting a setting to a Custom Type (should match the type of valueType).</param>
        void RegisterValue(string settingName, Type valueType, bool isRequired, Func<string, object> customTypeDelegate);

        /// <summary>
        /// Registers a setting name to the Value Cache.
        /// </summary>
        /// <param name="settingName">The name of the setting (case sensitive).</param>
        /// <param name="valueType">The type the setting should end up as (i.e. int).</param>
        /// <param name="isRequired">Whether the setting is required or if a default value can be used.</param>
        /// <param name="defaultValue">The value that should be used if the setting is not found and if the setting is not required.</param>
        /// <param name="customTypeDelegate">The operation used for casting a setting to a Custom Type (should match the type of valueType).</param>
        void RegisterValue(string settingName, Type valueType, bool isRequired, dynamic defaultValue, Func<string, object> customTypeDelegate);
    }
}