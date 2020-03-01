using System;

namespace Note.Core.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }

        public NotFoundException(string entityType, Guid identifier) 
            : this($"{entityType} entity with ID [{identifier}] not found") { }

        public NotFoundException(string entityType, string identifier)
            : this($"{entityType} entity with slug [{identifier}] not found") { }
    }
}
