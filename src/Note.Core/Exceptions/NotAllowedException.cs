using System;

namespace Note.Core.Exceptions
{
    public class NotAllowedException : ApplicationException
    {
        public NotAllowedException(string message) : base(message) { }
    }
}
