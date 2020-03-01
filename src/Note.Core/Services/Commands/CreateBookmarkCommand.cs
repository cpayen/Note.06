using Note.Core.Services.Commands.Base;
using System;
using System.Text;

namespace Note.Core.Services.Commands
{
    public class CreateBookmarkCommand : ICommand
    {
        public string UserLogin { get; set; }
        public Guid PageId { get; set; }

        public CreateBookmarkCommand(string userLogin, Guid pageId)
        {
            UserLogin = userLogin;
            PageId = pageId;
        }

        public bool IsValid
        {
            get
            {
                if (string.IsNullOrEmpty(UserLogin))
                {
                    return false;
                }

                if (PageId == Guid.Empty)
                {
                    return false;
                }

                return true;
            }
        }

        public override string ToString()
        {
            return new StringBuilder()
                .AppendLine($"CreateBookmarkCommand details:")
                .AppendLine($"   UserLogin = {UserLogin}")
                .AppendLine($"   PageId = {PageId}")
                .ToString();
        }
    }
}
