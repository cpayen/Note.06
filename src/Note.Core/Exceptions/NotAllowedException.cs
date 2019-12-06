using System;

namespace Note.Core.Exceptions
{
    public class NotAllowedException : ApplicationException
    {
        public NotAllowedException() { }
        public NotAllowedException(string message) : base(message) { }
        public NotAllowedException(string message, Exception innerException) : base(message, innerException) { }

        public NotAllowedException(string userLogin, string entityType, Guid entityId) 
            : this($"User {userLogin} is not allowed to access {entityType} entity with ID [{entityId}]") { }

        public NotAllowedException(string userLogin, string entityType, string entitySlug)
            : this($"User {userLogin} is not allowed to access {entityType} entity with slug [{entitySlug}]") { }
    }
}
