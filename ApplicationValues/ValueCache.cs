using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using ValorMorgan.Common.ApplicationValues.Exceptions;
using ValorMorgan.Common.Configuration;

namespace ValorMorgan.Common.ApplicationValues
{ 
    /// <summary>
    /// Houses value registrations for retrieving and converting application settings as desired. Supports
    /// having values be cast to any type (including custom types), having default values, and custom logic
    /// for casting a setting to a custom type (custom logic can perform any operation desired).
    /// </summary>
    public class ValueCache : IValueCache
    {
        #region VARIABLES
        private readonly IList<IValueRegistry> _values;
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Houses value registrations for retrieving and converting application settings as desired. Supports
        /// having values be cast to any type (including custom types), having default values, and custom logic
        /// for casting a setting to a custom type (custom logic can perform any operation desired).
        /// </summary>
        public ValueCache()
        {
            _values = new List<IValueRegistry>();
        }
        #endregion

        /// <summary>
        /// Gets the Value Registry representing the setting provided.
        /// </summary>
        /// <param name="settingName">The name of the setting already registered (case sensitive).</param>
        /// <returns>The IValueRegistry object representing the setting provided.</returns>
        /// <exception cref="ValueRegistryNotFoundException">If the provided setting name is not found to be registered.</exception>
        public IValueRegistry GetSettingRegistry(string settingName)
        {
            if (string.IsNullOrWhiteSpace(settingName))
                throw new ArgumentNullException(nameof(settingName), $"Cannot get the {nameof(ValueRegistry)} of a NULL or whitespace setting name.");
            
            IValueRegistry registry = _values.FirstOrDefault(val => val?.Name == settingName) as ValueRegistry;
            if (registry == default(ValueRegistry))
                throw new ValueRegistryNotFoundException($"{nameof(ValueRegistry)} for Setting Name \"{settingName}\" could not be found.");

            return registry;
        }

        /// <summary>
        /// Gets the value from the settings as defined by the setting registry.
        /// </summary>
        /// <typeparam name="T">The type the setting should return as (should match with the Type it was registered as).</typeparam>
        /// <param name="settingName">The name of the setting already registered (case sensitive).</param>
        /// <returns>The value of the setting requested as the Type in the template here.</returns>
        /// <exception cref="ApplicationException">If an unknown error occurs.</exception>
        /// <exception cref="ConfigurationErrorsException">If the setting does not have a value and was marked as required.</exception>
        /// <exception cref="ValueRegistryNotFoundException">If the provided setting name is not found to be registered.</exception>
        public T GetValue<T>(string settingName) => (T)GetValue(settingName);

        /// <summary>
        /// Gets the value from the settings as defined by the setting registry.
        /// </summary>
        /// <param name="settingName">The name of the setting already registered (case sensitive).</param>
        /// <returns>The value of the setting requested as the Type it was registered as (wrapped by Type dynamic).</returns>
        /// <exception cref="ApplicationException">If an unknown error occurs.</exception>
        /// <exception cref="ConfigurationErrorsException">If the setting does not have a value and was marked as required.</exception>
        /// <exception cref="ValueRegistryNotFoundException">If the provided setting name is not found to be registered.</exception>
        public dynamic GetValue(string settingName)
        {
            if (string.IsNullOrWhiteSpace(settingName))
                throw new ArgumentNullException(nameof(settingName), "Cannot get the value of a NULL or whitespace setting name.");
            
            if (_values.All(val => val?.Name != settingName))
                throw new ValueRegistryNotFoundException($"No value name \"{settingName}\" was found registered in the {nameof(ValueCache)}.");

            try
            {
                var registry = GetSettingRegistry(settingName);
                ValidateSettingHasValueOrIsNotRequired(settingName, registry);

                // NOTE: C# 7.0+ feature of inlining (Visual Studio 2017+ required)
                return TryGetValueAsType(settingName, registry, out var value) ?
                    value : registry?.Default;
            }
            catch (ConfigurationErrorsException)
            {
                throw;
            }
            catch (ValueRegistryNotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Failed to get the value for \"{settingName}\".", ex);
            }
        }

        /// <summary>
        /// Registers a setting name to the Value Cache.
        /// </summary>
        /// <param name="settingName">The name of the setting (case sensitive).</param>
        public void RegisterValue(string settingName)
        {
            RegisterValue(settingName, ValueRegistry.DEFAULT_TYPE, ValueRegistry.DEFAULT_REQUIRED, null, null);
        }

        /// <summary>
        /// Registers a setting name to the Value Cache.
        /// </summary>
        /// <param name="settingName">The name of the setting (case sensitive).</param>
        /// <param name="valueType">The type the setting should end up as (i.e. int).</param>
        public void RegisterValue(string settingName, Type valueType)
        {
            RegisterValue(settingName, valueType, ValueRegistry.DEFAULT_REQUIRED, null, null);
        }

        /// <summary>
        /// Registers a setting name to the Value Cache.
        /// </summary>
        /// <param name="settingName">The name of the setting (case sensitive).</param>
        /// <param name="valueType">The type the setting should end up as (i.e. int).</param>
        /// <param name="isRequired">Whether the setting is required or if a default value can be used.</param>
        /// <param name="defaultValue">The value that should be used if the setting is not found and if the setting is not required.</param>
        public void RegisterValue(string settingName, Type valueType, bool isRequired, dynamic defaultValue)
        {
            RegisterValue(settingName, valueType, isRequired, defaultValue, null);
        }

        /// <summary>
        /// Registers a setting name to the Value Cache.
        /// </summary>
        /// <param name="settingName">The name of the setting (case sensitive).</param>
        /// <param name="valueType">The type the setting should end up as (i.e. int).</param>
        /// <param name="isRequired">Whether the setting is required or if a default value can be used.</param>
        /// <param name="customTypeDelegate">The operation used for casting a setting to a Custom Type (should match the type of valueType).</param>
        /// <exception cref="ArgumentNullException">If either the settingName or valueType are not provided.</exception>
        /// <exception cref="InvalidOperationException">If the provided setting is already registered or its defaultValue type does not match its valueType.</exception>
        public void RegisterValue(string settingName, Type valueType, bool isRequired, Func<string, dynamic> customTypeDelegate)
        {
            RegisterValue(settingName, valueType, isRequired, null, customTypeDelegate);
        }

        /// <summary>
        /// Registers a setting name to the Value Cache.
        /// </summary>
        /// <param name="settingName">The name of the setting (case sensitive).</param>
        /// <param name="valueType">The type the setting should end up as (i.e. int).</param>
        /// <param name="isRequired">Whether the setting is required or if a default value can be used.</param>
        /// <param name="defaultValue">The value that should be used if the setting is not found and if the setting is not required.</param>
        /// <param name="customTypeDelegate">The operation used for casting a setting to a Custom Type (should match the type of valueType).</param>
        /// <exception cref="ArgumentNullException">If either the settingName or valueType are not provided.</exception>
        /// <exception cref="InvalidOperationException">If the provided setting is already registered or its defaultValue type does not match its valueType.</exception>
        public void RegisterValue(string settingName, Type valueType, bool isRequired, dynamic defaultValue, Func<string, dynamic> customTypeDelegate)
        {
            if (string.IsNullOrWhiteSpace(settingName))
                throw new ArgumentNullException(nameof(settingName), "Setting Name is required.");
            if (valueType == null)
                throw new ArgumentNullException(nameof(valueType), "Value Type is required.");

            ValidateSettingNotAlreadyRegistered(settingName);
            if (!isRequired)
                ValidateDefaultValueType(valueType, defaultValue);
            
            _values?.Add(new ValueRegistry
            {
                Name = settingName,
                ValueType = valueType,
                Required = isRequired,
                Default = defaultValue,
                CustomTypeCastFunction = customTypeDelegate
            });
        }

        #region PRIVATE
        private static bool TryGetValueAsType(string settingName, IValueRegistry registry, out dynamic value)
        {
            value = null;
            if (registry?.ValueType == null)
                return false;
            
            try
            {
                // NOTE: C# 7.0+ feature of pattern matching (Visual Studio 2017+ required)
                switch (registry.ValueType)
                {
                    case Type _ when registry.ValueType == typeof(string):
                        value = ApplicationSettings.AsString(settingName);
                        break;
                    case Type _ when registry.ValueType == typeof(char):
                        value = ApplicationSettings.AsChar(settingName);
                        break;
                    case Type _ when registry.ValueType == typeof(bool):
                        value = ApplicationSettings.AsBool(settingName);
                        break;
                    case Type _ when registry.ValueType == typeof(byte):
                        value = ApplicationSettings.AsByte(settingName);
                        break;
                    case Type _ when registry.ValueType == typeof(sbyte):
                        value = ApplicationSettings.AsSByte(settingName);
                        break;
                    case Type _ when registry.ValueType == typeof(decimal):
                        value = ApplicationSettings.AsDecimal(settingName);
                        break;
                    case Type _ when registry.ValueType == typeof(double):
                        value = ApplicationSettings.AsDouble(settingName);
                        break;
                    case Type _ when registry.ValueType == typeof(float):
                        value = ApplicationSettings.AsFloat(settingName);
                        break;
                    case Type _ when registry.ValueType == typeof(int):
                        value = ApplicationSettings.AsInt(settingName);
                        break;
                    case Type _ when registry.ValueType == typeof(uint):
                        value = ApplicationSettings.AsUInt(settingName);
                        break;
                    case Type _ when registry.ValueType == typeof(long):
                        value = ApplicationSettings.AsLong(settingName);
                        break;
                    case Type _ when registry.ValueType == typeof(ulong):
                        value = ApplicationSettings.AsULong(settingName);
                        break;
                    case Type _ when registry.ValueType == typeof(short):
                        value = ApplicationSettings.AsShort(settingName);
                        break;
                    case Type _ when registry.ValueType == typeof(ushort):
                        value = ApplicationSettings.AsUShort(settingName);
                        break;
                    default:
                        if (registry.CustomTypeCastFunction == null)
                            throw new InvalidCastException($"Failed to retrieve \"{settingName}\" as Type \"{registry.ValueType.FullName}\".");
                        value = registry.CustomTypeCastFunction(ApplicationSettings.AsString(settingName));
                        break;
                }

                return true;
            }
            catch (InvalidCastException)
            {
                throw;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static void ValidateDefaultValueType(Type valueType, dynamic defaultValue)
        {
            if (valueType == null)
                throw new ArgumentNullException(nameof(valueType), "Cannot validate the default value with a NULL value type.");
            
            if (defaultValue != null)
            {
                Type defaultValueType = defaultValue.GetType() ?? typeof(object);
                if (defaultValueType != valueType)
                    throw new InvalidOperationException($"Incoming default value does not match the setting Type of \"{valueType.FullName}\". Default Value's type is \"{defaultValueType.FullName}\".");
            }
            // Default value is null AND a primitive
            else if (valueType.IsPrimitive && valueType != typeof(string))
                throw new InvalidOperationException($"Primitive type \"{valueType.FullName}\" cannot be assigned a NULL default value.");
        }

        private static void ValidateSettingHasValueOrIsNotRequired(string settingName, IValueRegistry registry)
        {
            if (!ApplicationSettings.HasValue(settingName) && (registry?.Required ?? ValueRegistry.DEFAULT_REQUIRED))
                throw new ConfigurationErrorsException($"No value for setting \"{settingName}\" is provided in the Configuration and this setting is marked as required.");
        }

        private void ValidateSettingNotAlreadyRegistered(string settingName)
        {
            if (_values?.Any(val => val?.Name == settingName) ?? false)
                throw new InvalidOperationException($"Setting \"{settingName}\" is already registered in the {nameof(ValueCache)}.");
        }
        #endregion
    }
}