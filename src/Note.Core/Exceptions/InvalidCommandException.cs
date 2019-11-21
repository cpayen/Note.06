using Note.Core.Services.Commands.Base;
using System;

namespace Note.Core.Exceptions
{
    public class InvalidCommandException : ApplicationException
    {
        private ICommand _command { get; set; }

        public InvalidCommandException() { }
        public InvalidCommandException(string message) : base(message) { }
        public InvalidCommandException(string message, Exception innerException) : base(message, innerException) { }

        public InvalidCommandException(string commandType, ICommand commandValue)
            : this($"{commandType} command is not valid") 
        {
            _command = commandValue;
        }

        public override string ToString()
        {
            return $"{base.ToString()}{Environment.NewLine}{_command.ToString()}";
        }
    }
}
