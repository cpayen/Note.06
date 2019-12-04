using Note.Core.Enums;
using Note.Core.Services.Commands.Base;
using System.Text;

namespace Note.Core.Services.Commands
{
    public class CreateBookCommand : ICommand
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Access ReadAccess { get; set; }
        public Access WriteAccess { get; set; }

        public CreateBookCommand(string title, string description, Access readAccess, Access writeAccess)
        {
            Title = title;
            Description = description;
            ReadAccess = readAccess;
            WriteAccess = writeAccess;
        }

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(Title) || Title.Length > 250)
                {
                    return false;
                }

                return true;
            }
        }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine($"CreateBookCommand details:")
                .AppendLine($"   Title = {Title}")
                .AppendLine($"   Description = {Description}")
                .AppendLine($"   ReadAccess = {ReadAccess}")
                .AppendLine($"   WriteAccess = {WriteAccess}")
                .ToString();
        }

    }
}
