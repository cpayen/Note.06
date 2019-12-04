using Note.Core.Enums;
using Note.Core.Helpers;
using Note.Core.Services.Commands.Base;
using System.Text;

namespace Note.Core.Services.Commands
{
    public class CreateBookCommand : ICommand
    {
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public Access ReadAccess { get; set; }
        public Access WriteAccess { get; set; }

        public CreateBookCommand(string title, string slug, string description, Access readAccess, Access writeAccess)
        {
            Title = title;
            Slug = slug;
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

                if (string.IsNullOrEmpty(Slug) || Slug.Length > 100 || !SlugHelper.Validate(Slug))
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
                .AppendLine($"   Slug = {Slug}")
                .AppendLine($"   Description = {Description}")
                .AppendLine($"   ReadAccess = {ReadAccess}")
                .AppendLine($"   WriteAccess = {WriteAccess}")
                .ToString();
        }

    }
}
