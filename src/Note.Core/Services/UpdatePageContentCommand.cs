using Note.Core.Services.Commands.Base;
using System;
using System.Text;

namespace Note.Core.Services
{
    public class UpdatePageContentCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Content { get; set; }

        public UpdatePageContentCommand(Guid id, string content)
        {
            Id = id;
            Content = content;
        }

        public bool IsValid
        {
            get
            {
                if (Id == Guid.Empty)
                {
                    return false;
                }

                return true;
            }
        }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine($"UpdatePageContentCommand details:")
                .AppendLine($"   Id = {Id}")
                .AppendLine($"   Content = {Content}")
                .ToString();
        }
    }
}
