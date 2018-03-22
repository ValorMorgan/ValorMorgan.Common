using System;

namespace ValorMorgan.Common.ApplicationValues
{
    /// <summary>
    /// Registry housing setting information for retrieving and, if needed,
    /// converting the setting to the specified type.
    /// </summary>
    public interface IValueRegistry
    {
        /// <summary>
        /// The operation used for casting a setting to a Custom Type (should match the type of valueType).
        /// </summary>
        Func<string, dynamic> CustomTypeCastFunction { get; set; }

        /// <summary>
        /// The value that should be used if the setting is not found and if the setting is not required.
        /// </summary>
        dynamic Default { get; set; }

        /// <summary>
        /// The name of the setting (case sensitive).
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Whether the setting is required or if a default value can be used.
        /// </summary>
        bool Required { get; set; }

        /// <summary>
        /// The type the setting should end up as (i.e. int).
        /// </summary>
        Type ValueType { get; set; }
    }
}