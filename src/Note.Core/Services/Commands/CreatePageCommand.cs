using Note.Core.Enums;
using Note.Core.Helpers;
using Note.Core.Services.Commands.Base;
using System;
using System.Text;

namespace Note.Core.Services.Commands
{
    public class CreatePageCommand : ICommand
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public State State { get; set; }
        public Access ReadAccess { get; set; }
        public Access WriteAccess { get; set; }

        public CreatePageCommand(Guid bookId, string title, string slug, State state, Access readAccess, Access writeAccess)
        {
            BookId = bookId;
            Title = title;
            Slug = slug;
            State = state;
            ReadAccess = readAccess;
            WriteAccess = writeAccess;
        }

        public bool IsValid
        {
            get
            {
                if (BookId == null)
                {
                    return false;
                }

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
                .AppendLine($"CreatePageCommand details:")
                .AppendLine($"   BookId = {BookId}")
                .AppendLine($"   Title = {Title}")
                .AppendLine($"   Slug = {Slug}")
                .AppendLine($"   State = {State}")
                .AppendLine($"   ReadAccess = {ReadAccess}")
                .AppendLine($"   WriteAccess = {WriteAccess}")
                .ToString();
        }

    }
}
