using System;
using System.Runtime.Serialization;

namespace ValorMorgan.Common.ApplicationValues.Exceptions
{
    /// <summary>
    /// Represents errors that occur when a ValueRegistry is not found or registered
    /// to the ValueCache that is being searched through.
    /// </summary>
    [Serializable]
    public class ValueRegistryNotFoundException : Exception
    {
        #region CONSTRUCTORS
        /// <summary>
        /// Represents errors that occur when a ValueRegistry is not found or registered
        /// to the ValueCache that is being searched through.
        /// </summary>
        public ValueRegistryNotFoundException() { }

        /// <summary>
        /// Represents errors that occur when a ValueRegistry is not found or registered
        /// to the ValueCache that is being searched through.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        public ValueRegistryNotFoundException(string message)
            : base(message) { }

        /// <summary>
        /// Represents errors that occur when a ValueRegistry is not found or registered
        /// to the ValueCache that is being searched through.
        /// </summary>
        /// <param name="message">The error message that explains the reason for the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or a null reference
        ///     (Nothing in Visual Basic) if no inner exception is specified.</param>
        public ValueRegistryNotFoundException(string message, Exception innerException)
            : base(message, innerException) { }

        /// <summary>
        /// Represents errors that occur when a ValueRegistry is not found or registered
        /// to the ValueCache that is being searched through.
        /// </summary>
        /// <param name="info">The System.Runtime.Serialization.SerializationInfo that holds the serialized
        ///      object data about the exception being thrown.</param>
        /// <param name="context">The System.Runtime.Serialization.StreamingContext that contains contextual information
        ///     about the source or destination.</param>
        protected ValueRegistryNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
        #endregion
    }
}