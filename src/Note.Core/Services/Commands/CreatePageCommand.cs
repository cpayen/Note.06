﻿using Note.Core.Enums;
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
        public PageType Type { get; set; }
        public State State { get; set; }

        public CreatePageCommand(Guid bookId, string title, string slug, State state)
        {
            BookId = bookId;
            Title = title;
            Slug = slug;
            State = state;
            Type = PageType.Article;
        }

        public CreatePageCommand(Guid id, string title, string slug, State state, PageType type) : this(id, title, slug, state)
        {
            Type = type;
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
                .ToString();
        }

    }
}
