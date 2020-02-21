using Note.Core.Enums;
using Note.Core.Helpers;
using Note.Core.Services.Commands.Base;
using System;
using System.Text;

namespace Note.Core.Services.Commands
{
    public class UpdatePageCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public PageType Type { get; set; }
        public State State { get; set; }

        public UpdatePageCommand(Guid id, string title, string slug, State state)
        {
            Id = id;
            Title = title;
            Slug = slug;
            State = state;
            Type = PageType.Article;
        }

        public UpdatePageCommand(Guid id, string title, string slug, State state, PageType type) : this(id, title, slug, state)
        {
            Type = type;
        }

        public bool IsValid
        {
            get
            {
                if (Id == Guid.Empty)
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
                .AppendLine($"UpdatePageCommand details:")
                .AppendLine($"   Id = {Id}")
                .AppendLine($"   Title = {Title}")
                .AppendLine($"   Slug = {Slug}")
                .AppendLine($"   Type = {Type}")
                .AppendLine($"   State = {State}")
                .ToString();
        }
    }
}
