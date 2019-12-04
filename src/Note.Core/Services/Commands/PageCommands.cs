using Note.Core.Enums;
using Note.Core.Services.Commands.Base;
using System;
using System.Text;

namespace Note.Core.Services.Commands
{
    #region Create

    public class CreatePageCommand : ICommand
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public Access ReadAccess { get; set; }
        public Access WriteAccess { get; set; }

        public CreatePageCommand(Guid bookId, string title, Access readAccess, Access writeAccess)
        {
            BookId = bookId;
            Title = title;
            ReadAccess = readAccess;
            WriteAccess = writeAccess;
        }

        public bool IsValid
        {
            get
            {
                if(BookId == null)
                {
                    return false;
                }

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
                .AppendLine($"CreatePageCommand details:")
                .AppendLine($"   BookId = {BookId}")
                .AppendLine($"   Title = {Title}")
                .AppendLine($"   ReadAccess = {ReadAccess}")
                .AppendLine($"   WriteAccess = {WriteAccess}")
                .ToString();
        }

    }

    #endregion

    #region Update

    public class UpdatePageCommand : ICommand
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Access ReadAccess { get; set; }
        public Access WriteAccess { get; set; }

        public UpdatePageCommand(Guid id, string title, Access readAccess, Access writeAccess)
        {
            Id = id;
            Title = title;
            ReadAccess = readAccess;
            WriteAccess = writeAccess;
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

                return true;
            }
        }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine($"UpdatePageCommand details:")
                .AppendLine($"   Id = {Id}")
                .AppendLine($"   Title = {Title}")
                .AppendLine($"   ReadAccess = {ReadAccess}")
                .AppendLine($"   WriteAccess = {WriteAccess}")
                .ToString();
        }
    }

    #endregion
}
