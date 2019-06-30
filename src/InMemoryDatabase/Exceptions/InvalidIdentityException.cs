namespace InMemoryDatabase.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The exception that is thrown when an entity has invalid identifier settings
    /// </summary>
    public class InvalidIdentityException : SystemException
    {
        private string _message { get; set; }

        /// <summary>
        /// More details about the exception.
        /// </summary>
        public override string Message => _message;

        private IList<string> _properties { get; set; }

        /// <summary>
        /// Properties that are related with the exception.
        /// </summary>
        public IList<string> Properties => _properties.ToList();

        public InvalidIdentityException(string message, params string[] properties)
            : base(message)
        {
            Initialize(message, properties);
        }

        public InvalidIdentityException(string message, Exception innerException, params string[] properties)
            : base(message, innerException)
        {
            _message = message;
        }

        private void Initialize(string message, string[] properties)
        {
            _message = message;
            _properties = properties;
        }
    }
}