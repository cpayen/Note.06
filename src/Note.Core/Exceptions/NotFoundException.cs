using System;

namespace Note.Core.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException() { }
        public NotFoundException(string message) : base(message) { }
        public NotFoundException(string message, Exception innerException) : base(message, innerException) { }

        public NotFoundException(string EntityType, Guid EntityId) 
            : this($"{EntityType} entity with ID {EntityId} not found") { }
    }
}
