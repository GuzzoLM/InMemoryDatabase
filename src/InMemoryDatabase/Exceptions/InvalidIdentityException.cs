namespace InMemoryDatabase.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class InvalidIdentityException : SystemException
    {
        private string _message { get; set; }

        public override string Message => _message;

        private IList<string> _properties { get; set; }

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