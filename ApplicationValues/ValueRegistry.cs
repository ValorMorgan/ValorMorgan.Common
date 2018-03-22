using System;
using Microsoft.Build.Framework;

namespace ValorMorgan.Common.ApplicationValues
{
    class ValueRegistry : IValueRegistry
    {
        #region VARIABLES
        internal const bool DEFAULT_REQUIRED = true;
        internal static readonly Type DEFAULT_TYPE = typeof(string);
        #endregion

        #region PROPERTIES
        public Func<string, dynamic> CustomTypeCastFunction { get; set; }

        public dynamic Default { get; set; }

        [Required]
        public string Name { get; set; }

        public bool Required { get; set; } = DEFAULT_REQUIRED;

        public Type ValueType { get; set; } = DEFAULT_TYPE;
        #endregion
    }
}