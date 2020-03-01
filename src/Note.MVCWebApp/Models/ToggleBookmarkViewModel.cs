using System;

namespace Note.MVCWebApp.Models
{
    public class ToggleBookmarkViewModel
    {
        public Guid PageId { get; set; }
        public bool BookmarkExists { get; set; }
    }
}
